using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using System.Data;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Cancelaciones
	{
		Conector conector = new Conector();

		public bool existe_cancelacion(long venta_id)
		{
			string sql= @"
				SELECT
					cancelacion_id
				FROM
					farmacontrol_local.cancelaciones 
				WHERE
					venta_id = @venta_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("venta_id",venta_id);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				return true;
			}

			return false;
		}

		public DataTable get_cancelacion_data(long venta_id)
		{
			string sql = @"
				SELECT
					cancelaciones.*,
					ventas.venta_folio
				FROM
					farmacontrol_local.cancelaciones
				JOIN farmacontrol_local.ventas USING(venta_id)
				WHERE
					cancelaciones.venta_id = @venta_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("venta_id",venta_id);

			conector.Select(sql,parametros);

			return conector.result_set;
		}

		public DTO_Validacion cancelar_venta(long venta_id, string comentarios, List<Tuple<int, string, string, decimal, int>> lista_productos, bool cancela_factura, bool emite_nota_credito, bool es_tae = false)
		{
			DTO_Validacion validacion = new DTO_Validacion();
			validacion.status = false;
			validacion.informacion = "Ocurrio un problema al intentar generar la devolucion, notifique a su administrador";

			long cancelacion_id = 0;
            long venta_id_entrada = 0;

			Dictionary<string, object> parametros;

			try
			{
				conector.TransactionStart();

				DAO_Ventas dao_ventas = new DAO_Ventas();
				int empleado_id = (int)FORMS.comunes.Principal.empleado_id;

				string sql = @"
					INSERT INTO
						farmacontrol_local.cancelaciones
					SET
						venta_id = @venta_id,
						comentarios = @comentarios,
						empleado_id = @empleado_id,
						terminal_id = @terminal_id
				";

				parametros = new Dictionary<string, object>();
				parametros.Add("venta_id", venta_id);
				parametros.Add("empleado_id", empleado_id);
				parametros.Add("comentarios", comentarios);
				parametros.Add("terminal_id",Convert.ToInt64(Misc_helper.get_terminal_id()));

				conector.Insert(sql,parametros);

				sql = @"
					SET @insert_id_cancelacion = LAST_INSERT_ID();
				";

				conector.Select(sql);

				sql = @"
					INSERT INTO
						farmacontrol_local.detallado_cancelaciones
					(
						SELECT
							0 AS detallado_cancelacion_id,
							@insert_id_cancelacion AS cancelacion_id,
							articulo_id,
							caducidad,
							lote,
							precio_publico,
							pct_descuento,
							importe_descuento,
							importe,
							cantidad,
							subtotal,
							pct_iva,
							importe_iva,
							tipo_ieps,
							ieps,
							importe_ieps,
							total,
							es_promocion,
							comentarios,
							NOW()
						FROM
							farmacontrol_local.detallado_ventas	
						WHERE
							venta_id = @venta_id
						GROUP BY
							articulo_id,caducidad,lote,importe
					)
				";

				parametros =  new Dictionary<string,object>();
				parametros.Add("venta_id",venta_id);
				parametros.Add("cancelacion_id",cancelacion_id);

				conector.Insert(sql, parametros);

                parametros.Add("terminal_id",Convert.ToInt32(Misc_helper.get_terminal_id()));

                venta_id_entrada = venta_id;
                if (!es_tae)
                {
                    sql = @"
                        INSERT INTO
                            farmacontrol_local.kardex (terminal_id, fecha_datetime, fecha_date, articulo_id, caducidad, lote, tipo, elemento_id, folio, existencia_anterior, cantidad, existencia_posterior)
                        (
                            SELECT
                                @terminal_id AS terminal_id,
                                NOW() AS fecha_datetime,
                                CURDATE() AS fecha_date,
                                articulo_id,
                                caducidad,
                                lote,
                                'DEVOLUCION_CLIENTE' AS tipo,
                                ventas.venta_id AS elemento_id,
                                ventas.venta_folio AS folio,
                                COALESCE(existencias.existencia, 0) AS existencia_anterior,
                                detallado_ventas.cantidad AS cantidad,
                                COALESCE(existencias.existencia, 0) + detallado_ventas.cantidad
                            FROM
                                farmacontrol_local.detallado_ventas
                            JOIN farmacontrol_local.ventas USING(venta_id)
                            LEFT JOIN farmacontrol_local.existencias USING(articulo_id, caducidad, lote)
                            WHERE
                                venta_id = @venta_id
                        )
                    ";

                    conector.Insert(sql, parametros);

                    sql = @"
                        INSERT INTO
                            farmacontrol_local.existencias (articulo_id, caducidad, lote, existencia)
                        (
                            SELECT
                                articulo_id,
                                caducidad,
                                lote,
                                cantidad
                            FROM
                                farmacontrol_local.detallado_ventas
                            WHERE
                                venta_id = @venta_id
                        )
                        ON DUPLICATE KEY UPDATE
                            existencia = existencia + cantidad
                    ";

                    conector.Insert(sql, parametros);
                }

				long? venta_id_origen = null;

				if(lista_productos.Count > 0)
				{
					int terminal_id = (int)HELPERS.Misc_helper.get_terminal_id();

                    // crea la nueva venta
					venta_id_origen = dao_ventas.registrar_venta(empleado_id);

					sql = @"
						INSERT INTO
							farmacontrol_local.ventas
						(
								SELECT
									0 AS venta_id,
									@terminal_id AS terminal_id,
									COALESCE(MAX(venta_folio), 0) + 1 AS venta_folio,
									@empleado_id AS empleado_id,
									NULL AS cotizacion_id,
									NULL AS traspaso_id,
									NULL AS cliente_credito_id,
									NULL AS cliente_domicilio_id,
									NULL AS cupon_id,
									NULL AS prepago_id,
									NULL AS corte_parcial_id,
									NULL AS corte_total_id,
									NOW() AS fecha_creado,
									NULL AS fecha_terminado,
									NULL AS fecha_facturado,
									'' AS comentarios,
									NOW() AS modified
								FROM
									farmacontrol_local.ventas
								WHERE
									terminal_id = @terminal_id
								ORDER BY venta_folio DESC
								LIMIT 1
						)
					";

					parametros = new Dictionary<string, object>();
					parametros.Add("empleado_id", empleado_id);
					parametros.Add("terminal_id", terminal_id);

					conector.Insert(sql, parametros);

					sql = @"
						SET @insert_id_nueva_venta = LAST_INSERT_ID();
					";

					conector.Select(sql);

					sql = @"
						UPDATE
							farmacontrol_local.ventas
						LEFT JOIN
							(
								SELECT
									*
								FROM
									farmacontrol_local.ventas
								WHERE
									venta_id = @venta_id_origen
							) AS tmp ON
						tmp.venta_id = @venta_id_origen
						SET
							ventas.empleado_id = tmp.empleado_id,
							ventas.cotizacion_id = tmp.cotizacion_id,
							ventas.traspaso_id = tmp.traspaso_id,
							ventas.cliente_credito_id = tmp.cliente_credito_id,
							ventas.cliente_domicilio_id = tmp.cliente_domicilio_id,
							ventas.cupon_id = tmp.cupon_id
						WHERE
							ventas.venta_id = @insert_id_nueva_venta
					";

					parametros = new Dictionary<string, object>();
					parametros.Add("venta_id_origen", venta_id_origen);

					conector.Select(sql, parametros);

					sql = @"
						INSERT INTO
							farmacontrol_local.detallado_ventas
						(
							SELECT
								0 AS detallado_venta_id,
								@insert_id_nueva_venta AS venta_id,
								articulo_id,
								caducidad,
								lote,
								precio_publico,
								pct_descuento,
								importe_descuento,
								importe,
								cantidad,
								subtotal,
								pct_iva,
								importe_iva,
								tipo_ieps,
								ieps,
								importe_ieps,
								total,
								es_promocion,
								comentarios,
								modified
							FROM
								farmacontrol_local.detallado_ventas
							WHERE
								venta_id = @venta_id_origen
						)
					";

					conector.Insert(sql, parametros);

					sql = @"
						UPDATE
							farmacontrol_local.ventas
						SET
							cotizacion_id = NULL,
							traspaso_id = NULL,
							cliente_credito_id = NULL,
							cliente_domicilio_id = NULL
						WHERE
							venta_id = @venta_id
					";

					parametros = new Dictionary<string, object>();
					parametros.Add("venta_id", venta_id_origen);

					conector.Update(sql, parametros);

					sql = @"
						DELETE FROM
							farmacontrol_local.detallado_ventas
						WHERE
							venta_id = @venta_id
					";

					conector.Delete(sql, parametros);

                    sql = @"
						UPDATE
							farmacontrol_local.ventas
						LEFT JOIN
							(
								SELECT
									*
								FROM
									farmacontrol_local.ventas
								WHERE
									venta_id = @venta_id_cancelacion
							) AS tmp ON
						tmp.venta_id = @venta_id_cancelacion
						SET
							ventas.empleado_id = tmp.empleado_id,
							ventas.cotizacion_id = tmp.cotizacion_id,
							ventas.traspaso_id = tmp.traspaso_id,
							ventas.cliente_credito_id = tmp.cliente_credito_id,
							ventas.cliente_domicilio_id = tmp.cliente_domicilio_id,
							ventas.cupon_id = tmp.cupon_id
						WHERE
							ventas.venta_id = @venta_id
					";

                    //parametros = new Dictionary<string, object>();
                    parametros.Add("venta_id_cancelacion", venta_id);

                    conector.Update(sql, parametros);

					foreach (var detallado in lista_productos)
					{
						sql = @"
							INSERT INTO
								farmacontrol_local.detallado_ventas
							(
								SELECT
									0 AS detallado_venta_id,
									@venta_origen AS venta_id,
									articulo_id,
									caducidad,
									lote,
									precio_publico,
									pct_descuento,
									importe_descuento,
									importe,
									@cantidad AS cantidad,
									(precio_publico - (precio_publico * pct_descuento)) * @cantidad AS subtotal,
									pct_iva,
									((precio_publico - (precio_publico * pct_descuento)) * @cantidad) * pct_iva,
									tipo_ieps,
									ieps,
									IF(tipo_ieps = 'PCT', ((precio_publico - (precio_publico * pct_descuento)) * ieps) , ieps) AS importe_ieps,
									((precio_publico - (precio_publico * pct_descuento)) * @cantidad) + (((precio_publico - (precio_publico * pct_descuento)) * @cantidad) * pct_iva) AS total,
									es_promocion,
									comentarios,
									modified
								FROM
									farmacontrol_local.detallado_ventas
								WHERE
									venta_id = @venta_id
								AND
									articulo_id = @articulo_id
								AND
									caducidad = @caducidad
								AND
									lote = @lote
								AND
									importe = @importe
								GROUP BY
									articulo_id,caducidad,lote,precio_publico,importe
							)
						";

						parametros = new Dictionary<string, object>();
						parametros.Add("venta_origen",venta_id_origen);
						parametros.Add("venta_id", venta_id);
						parametros.Add("articulo_id", detallado.Item1);
						parametros.Add("caducidad", detallado.Item2);
						parametros.Add("lote", detallado.Item3);
						parametros.Add("importe", detallado.Item4);
						parametros.Add("cantidad",detallado.Item5);

						conector.Insert(sql, parametros);
					}


                    sql = @"
                        INSERT INTO
                            farmacontrol_local.kardex (terminal_id, fecha_datetime, fecha_date, articulo_id, caducidad, lote, tipo, elemento_id, folio, existencia_anterior, cantidad, existencia_posterior)
                        (
                            SELECT
                                @terminal_id AS terminal_id,
                                NOW() AS fecha_datetime,
                                CURDATE() AS fecha_date,
                                articulo_id,
                                caducidad,
                                lote,
                                'VENTA' AS tipo,
                                ventas.venta_id AS elemento_id,
                                ventas.venta_folio AS folio,
                                COALESCE(existencias.existencia, 0) AS existencia_anterior,
                                (detallado_ventas.cantidad * -1) AS cantidad,
                                COALESCE(existencias.existencia, 0) - detallado_ventas.cantidad
                            FROM
                                farmacontrol_local.detallado_ventas
                            JOIN farmacontrol_local.ventas USING(venta_id)
                            LEFT JOIN farmacontrol_local.existencias USING(articulo_id, caducidad, lote)
                            WHERE
                                venta_id = @venta_id_origen
                        )
                    ";

                    parametros = new Dictionary<string, object>();
                    parametros.Add("terminal_id",Misc_helper.get_terminal_id());
                    parametros.Add("venta_id_origen", venta_id_origen);

                    conector.Insert(sql, parametros);

                    sql = @"
                        UPDATE
                            farmacontrol_local.existencias
                        JOIN farmacontrol_local.detallado_ventas USING(articulo_id,caducidad,lote)
                        SET
                            existencia = (existencia - detallado_ventas.cantidad)
                        WHERE
                            detallado_ventas.venta_id = @venta_id_origen
                    ";

                    conector.Insert(sql, parametros);

                    sql = @"
						UPDATE
							farmacontrol_local.ventas
						SET
							fecha_terminado = NOW()
						WHERE
							venta_id = @venta_id_origen
					";

                    parametros = new Dictionary<string, object>();
                    parametros.Add("venta_id_origen", venta_id_origen);

                    conector.Update(sql, parametros);

                    sql = @"
							INSERT INTO
								farmacontrol_local.ventas_pagos
							(
                                SELECT
                                    0 AS venta_pago_id,    
    								@venta_id_origen AS venta_id,
                                    (SELECT pago_tipo_id FROM farmacontrol_global.pago_tipos WHERE etiqueta = 'RCAN' LIMIT 1) AS pago_tipo_id,
                                    '' AS cuenta,    
                                    SUM(detallado_ventas.total) AS importe,
                                    SUM(detallado_ventas.total) AS monto
                                FROM
                                    farmacontrol_local.detallado_ventas
                                WHERE
                                    detallado_ventas.venta_id = @venta_id_origen
                                GROUP BY detallado_ventas.venta_id
                                LIMIT 1
							)
                            ON DUPLICATE KEY UPDATE
                                venta_id = venta_id,
                                pago_tipo_id = pago_tipo_id
						";

                    parametros.Add("venta_id", venta_id);
                    conector.Insert(sql, parametros);
				}

				sql= @"
					UPDATE
						farmacontrol_local.cancelaciones
					SET
						nueva_venta_id = @venta_origen,
						fecha =  NOW(),
						termina_empleado_id = @empleado_id
					WHERE
						cancelacion_id = @insert_id_cancelacion
				";

				parametros = new Dictionary<string,object>();
				parametros.Add("empleado_id", FORMS.comunes.Principal.empleado_id);
				parametros.Add("venta_origen",venta_id_origen);
				conector.Update(sql,parametros);

				if(conector.TransactionCommit())
				{
                    sql = @"
                        SELECT
                            GROUP_CONCAT(DISTINCT(articulo_id)) articulo_ids
                        FROM
                            farmacontrol_local.detallado_ventas
                        WHERE
                            venta_id = @venta_id
                    ";

                    parametros.Add("venta_id",venta_id);

                    conector.Select(sql, parametros);

                    if(conector.result_set.Rows.Count > 0)
                    {
                        string articulo_dis = conector.result_set.Rows[0]["articulo_ids"].ToString();

                        sql = string.Format(@"
                            DELETE FROM
                                farmacontrol_local.existencias
                            WHERE
                                existencia = 0
                            AND
                                articulo_id IN({0})
                        ",articulo_dis);

                        conector.Delete(sql, parametros);
                    }

					/*
					if(cancela_factura)
					{
						DAO_Facturacion dao_facturacion = new DAO_Facturacion();
						parametros = new Dictionary<string,object>();
						parametros.Add("sucursal_id",Convert.ToInt32(Config_helper.get_config_local("sucursal_id")));
						parametros.Add("venta_id",venta_id);
						DAO_Cola_operaciones.insertar_cola_operaciones(empleado_id,"rest/facturacion_sf","cancelar",parametros,"PARA ENVIO AL SERVIDOR PRINCIPAL");
					}

					if(emite_nota_credito)
					{
						DAO_Terminales dao_terminales = new DAO_Terminales();
						string serie_nc = dao_terminales.get_terminal_serie_facturas();
						
						parametros = new Dictionary<string,object>();
						parametros.Add("conector_txt", Misc_helper.EncodeTo64(dao_ventas.get_informacion_nota_credito(venta_id)));
						parametros.Add("venta_id", venta_id);
						parametros.Add("serie", serie_nc);
						var venta_data = dao_ventas.get_venta_data(venta_id);
						parametros.Add("folio", venta_data.venta_folio);
						parametros.Add("sucursal_id", Convert.ToInt32(Config_helper.get_config_local("sucursal_id")));
						parametros.Add("tipo","NC");

						DAO_Cola_operaciones.insertar_cola_operaciones(empleado_id,"rest/facturacion_sf","importar",parametros,"PARA ENVIO SERVIDOR PRINCIPAL");
					}
					 */

                    validacion.elemento_id = ((venta_id_origen.Equals(null))?(int)venta_id:(int)venta_id_origen);
					validacion.status = true;
					validacion.informacion = "Venta cancelada correctamente";
				}
			}
			catch(Exception exception)
			{
				Log_error.log(exception);
			}

			return validacion;
		}

        public DTO_Validacion cancelar_venta_compra(long venta_id, string comentarios, List<Tuple<int, string, string, decimal, int>> lista_productos_cancelados, List<Tuple<int, string, string, decimal, int>> lista_productos_comprados, bool cancela_factura, bool emite_nota_credito, bool es_tae = false)
        {
            DTO_Validacion validacion = new DTO_Validacion();
            validacion.status = false;
            validacion.informacion = "Ocurrio un problema al intentar generar la devolucion, notifique a su administrador";

            long cancelacion_id = 0;
            long venta_id_entrada = 0;


            Dictionary<string, object> parametros;

            try
            {
                conector.TransactionStart();

                DAO_Ventas dao_ventas = new DAO_Ventas();
                int empleado_id = (int)FORMS.comunes.Principal.empleado_id;

                string sql = @"
					INSERT INTO
						farmacontrol_local.cancelaciones
					SET
						venta_id = @venta_id,
						comentarios = @comentarios,
						empleado_id = @empleado_id,
						terminal_id = @terminal_id,
                        fecha = NOW()
				";

                parametros = new Dictionary<string, object>();
                parametros.Add("venta_id", venta_id);
                parametros.Add("empleado_id", empleado_id);
                parametros.Add("comentarios", comentarios);
                parametros.Add("terminal_id", Convert.ToInt64(Misc_helper.get_terminal_id()));

                conector.Insert(sql, parametros);
                conector.TransactionCommit();

                sql = @"
					SELECT
                        cancelacion_id
                    FROM
                        farmacontrol_local.cancelaciones
                    ORDER BY 
                        cancelacion_id DESC
                    LIMIT 1
                    
				";

                conector.Select(sql);
               
                var result_set_cancelacion = conector.result_set;
                int insert_id_cancelacion = 0;
                foreach (DataRow row in result_set_cancelacion.Rows)
                {
                    insert_id_cancelacion = Convert.ToInt32(row["cancelacion_id"].ToString());
                  
                }


                
               
                foreach (var detallado in lista_productos_cancelados)
                {
                    decimal p_publico = 0;
                    decimal pct_iva = 0;
                    decimal pct_descuento = 0;
                    decimal importe = 0;
                    decimal subtotal = 0;
                    decimal importe_descuento = 0;
                    decimal importe_iva = 0;
                    decimal total = 0;
                    sql = @"
                        SELECT
                            precio_publico,
                            pct_iva,
                            pct_descuento
                        FROM
                            farmacontrol_global.articulos
                        WHERE 
                            articulo_id = @articulo_id
                    ";

                    parametros = new Dictionary<string, object>();
                   
                    parametros.Add("articulo_id",Convert.ToInt32(detallado.Item1));

                    conector.Select(sql, parametros);

                    var result_set = conector.result_set;

                    foreach (DataRow row in result_set.Rows)
                    {
                        p_publico = Convert.ToDecimal(row["precio_publico"].ToString());
                        pct_iva = Convert.ToDecimal(row["pct_iva"].ToString());
                        pct_descuento = Convert.ToDecimal(row["pct_descuento"].ToString());
                    }
                    //conector.TransactionStart();

                    importe = (p_publico - (p_publico * pct_descuento));
                    subtotal = importe * detallado.Item5;
                    importe_descuento = (p_publico * pct_descuento);
                    importe_iva = subtotal * pct_iva;
                    total = subtotal + importe_iva;
                    sql = @"
							INSERT INTO
								farmacontrol_local.detallado_cancelaciones
							SET 
                                cancelacion_id = @insert_id_cancelacion,
                                articulo_id = @articulo_id,
                                caducidad = @caducidad,
                                lote = @lote,
                                precio_publico =@precio_publico,
                                pct_descuento = @pct_descuento, 
                                importe_descuento =@importe_descuento , 
                                importe = @importe,
                                cantidad = @cantidad,
                                subtotal = @subtotal,
                                pct_iva = @pct_iva,
                                importe_iva=@importe_iva,
                                tipo_ieps = 'PCT',
                                ieps = 0.00 ,
                                importe_ieps = 0.00,
                                total =@total,
                                es_promocion = 0,
                                comentarios = '',
                                modified = NOW()
                                
						";

                    parametros = new Dictionary<string, object>();
                    parametros.Add("articulo_id", detallado.Item1);
                    parametros.Add("caducidad", detallado.Item2);
                    parametros.Add("lote", detallado.Item3);
                    parametros.Add("importe", importe);
                    parametros.Add("cantidad", detallado.Item5);
                    parametros.Add("precio_publico", p_publico);
                    parametros.Add("pct_iva", pct_iva);
                    parametros.Add("subtotal", subtotal);
                    parametros.Add("pct_descuento", pct_descuento);
                    parametros.Add("importe_descuento", importe_descuento);
                    parametros.Add("importe_iva", importe_iva);
                    parametros.Add("total", total);
                    parametros.Add("insert_id_cancelacion", insert_id_cancelacion);
                    conector.Insert(sql, parametros);

                    if (!es_tae)
                    {
                        sql = @"
                        INSERT INTO
                            farmacontrol_local.kardex (terminal_id, fecha_datetime, fecha_date, articulo_id, caducidad, lote, tipo, elemento_id, folio, existencia_anterior, cantidad, existencia_posterior)
                        (
                            SELECT
                                @terminal_id AS terminal_id,
                                NOW() AS fecha_datetime,
                                CURDATE() AS fecha_date,
                                @articulo_id,
                                @caducidad,
                                @lote,
                                'DEVOLUCION_CLIENTE' AS tipo,
                                @venta_id AS elemento_id,
                                ventas.venta_folio AS folio,
                                COALESCE(existencias.existencia, 0) AS existencia_anterior,
                                @cantidad AS cantidad,
                                COALESCE(existencias.existencia, 0) + @cantidad
                            FROM
                               farmacontrol_local.ventas          
                            LEFT JOIN 
                               farmacontrol_local.detallado_ventas
                            USING(venta_id)
                            LEFT JOIN 
                               farmacontrol_local.existencias USING(articulo_id, caducidad, lote)
                            WHERE
                                venta_id = @venta_id
                            AND
                                articulo_id = @articulo_id
                            AND
                                lote = @lote
                            AND
                                caducidad = @caducidad
                        )
                       ";

                        parametros = new Dictionary<string, object>();
                        parametros.Add("terminal_id", Convert.ToInt32(Misc_helper.get_terminal_id()));
                        parametros.Add("articulo_id", detallado.Item1);
                        parametros.Add("caducidad", detallado.Item2);
                        parametros.Add("lote", detallado.Item3);
                        parametros.Add("venta_id", venta_id);
                        parametros.Add("cantidad", detallado.Item5);
                        venta_id_entrada = venta_id;

                        conector.Insert(sql, parametros);
                        
                        sql = @"
                        INSERT INTO
                            farmacontrol_local.existencias
                        SET 
                            articulo_id = @articulo_id,
                            caducidad = @caducidad,
                            lote = @lote,
                            existencia = @cantidad
                        ON DUPLICATE KEY UPDATE
                            existencia = existencia + @cantidad
                         ";


                        parametros = new Dictionary<string, object>();
                        parametros.Add("articulo_id", detallado.Item1);
                        parametros.Add("caducidad", detallado.Item2);
                        parametros.Add("lote", detallado.Item3);
                        parametros.Add("cantidad", detallado.Item5);
                        venta_id_entrada = venta_id;


                        conector.Insert(sql, parametros);
                      //  conector.TransactionCommit();
                    }

                }

                long? venta_id_origen = null;

                if (lista_productos_comprados.Count > 0)
                {
                    int terminal_id = (int)HELPERS.Misc_helper.get_terminal_id();
                    // crea la nueva venta
                    venta_id_origen = dao_ventas.registrar_venta(empleado_id);

                    sql = @"
						INSERT INTO
							farmacontrol_local.ventas
						(
								SELECT
									0 AS venta_id,
									@terminal_id AS terminal_id,
									COALESCE(MAX(venta_folio), 0) + 1 AS venta_folio,
									@empleado_id AS empleado_id,
									NULL AS cotizacion_id,
									NULL AS traspaso_id,
									NULL AS cliente_credito_id,
									NULL AS cliente_domicilio_id,
									NULL AS cupon_id,
									NULL AS prepago_id,
									NULL AS corte_parcial_id,
									NULL AS corte_total_id,
									NOW() AS fecha_creado,
									NULL AS fecha_terminado,
									NULL AS fecha_facturado,
									'' AS comentarios,
									NOW() AS modified
								FROM
									farmacontrol_local.ventas
								WHERE
									terminal_id = @terminal_id
								ORDER BY venta_folio DESC
								LIMIT 1
						)
					";

                    parametros = new Dictionary<string, object>();
                    parametros.Add("empleado_id", empleado_id);
                    parametros.Add("terminal_id", terminal_id);

                    conector.Insert(sql, parametros);
                    
                    sql = @"
						SELECT
                           venta_id
                        FROM 
                            farmacontrol_local.ventas
                        ORDER BY    
                             venta_id DESC
                        LIMIT 1
                            
					";

                    conector.Select(sql);

                    var result_set_venta = conector.result_set;
                    int insert_id_nueva_venta = 0;
                    foreach (DataRow row in result_set_venta.Rows)
                    {
                        insert_id_nueva_venta = Convert.ToInt32(row["venta_id"].ToString()); 
                    }




                    sql = @"
						UPDATE
							farmacontrol_local.ventas
						LEFT JOIN
							(
								SELECT
									*
								FROM
									farmacontrol_local.ventas
								WHERE
									venta_id = @venta_id_origen
							) AS tmp ON
						tmp.venta_id = @venta_id_origen
						SET
							ventas.empleado_id = tmp.empleado_id,
							ventas.cotizacion_id = tmp.cotizacion_id,
							ventas.traspaso_id = tmp.traspaso_id,
							ventas.cliente_credito_id = tmp.cliente_credito_id,
							ventas.cliente_domicilio_id = tmp.cliente_domicilio_id,
							ventas.cupon_id = tmp.cupon_id
						WHERE
							ventas.venta_id = @insert_id_nueva_venta
					";

                    parametros = new Dictionary<string, object>();
                    parametros.Add("venta_id_origen", venta_id_origen);
                    parametros.Add("insert_id_nueva_venta", insert_id_nueva_venta);

                    conector.Select(sql, parametros);

                    foreach (var detallado in lista_productos_comprados)
                    {
                        sql = @"
							INSERT INTO
								farmacontrol_local.detallado_ventas
							(
								SELECT
									0 AS detallado_venta_id,
									@venta_origen AS venta_id,
									@articulo_id,
									@caducidad,
									@lote,
									precio_publico,
									pct_descuento,
									(precio_publico*pct_descuento) as importe_descuento,
									(precio_publico - (precio_publico * pct_descuento)) as importe,
									@cantidad AS cantidad,
									(precio_publico - (precio_publico * pct_descuento)) * @cantidad AS subtotal,
									pct_iva,
									((precio_publico - (precio_publico * pct_descuento)) * @cantidad) * pct_iva,
									tipo_ieps,
									ieps,
									IF(tipo_ieps = 'PCT', ((precio_publico - (precio_publico * pct_descuento)) * ieps) , ieps) AS importe_ieps,
									((precio_publico - (precio_publico * pct_descuento)) * @cantidad) + (((precio_publico - (precio_publico * pct_descuento)) * @cantidad) * pct_iva) AS total,
									0,
									'',
									NOW()
								FROM
									farmacontrol_global.articulos
                                LEFT JOIN
                                    farmacontrol_local.existencias
                                USING(articulo_id)
								WHERE
									articulo_id = @articulo_id
								AND
									caducidad = @caducidad
								AND
									lote = @lote
							)
						";

                            parametros = new Dictionary<string, object>();
                            parametros.Add("venta_origen", insert_id_nueva_venta);
                            parametros.Add("articulo_id", detallado.Item1);
                            parametros.Add("caducidad", detallado.Item2);
                            parametros.Add("lote", detallado.Item3);
                            parametros.Add("cantidad", detallado.Item5);

                            conector.Insert(sql, parametros);

                             sql = @"
                                INSERT INTO
                                    farmacontrol_local.kardex (terminal_id, fecha_datetime, fecha_date, articulo_id, caducidad, lote, tipo, elemento_id, folio, existencia_anterior, cantidad, existencia_posterior)
                                (
                                    SELECT
                                        @terminal_id AS terminal_id,
                                        NOW() AS fecha_datetime,
                                        CURDATE() AS fecha_date,
                                        @articulo_id,
                                        @caducidad,
                                        @lote,
                                        'VENTA' AS tipo,
                                        ventas.venta_id AS elemento_id,
                                        ventas.venta_folio AS folio,
                                        COALESCE(existencias.existencia, 0) AS existencia_anterior,
                                        (@cantidad * -1) AS cantidad,
                                        COALESCE(existencias.existencia, 0) -  @cantidad
                                    FROM
                                        farmacontrol_local.ventas
                                    LEFT JOIN 
                                        farmacontrol_local.detallado_ventas USING(venta_id)
                                    LEFT JOIN 
                                        farmacontrol_local.existencias USING(articulo_id, caducidad, lote)
                                    WHERE
                                        venta_id = @venta_id_origen
                                    AND
                                        articulo_id = @articulo_id
                                    AND
                                        caducidad = @caducidad
                                    AND
                                        lote =@lote
                                )
                            ";

                           parametros = new Dictionary<string, object>();
                           parametros.Add("terminal_id", Misc_helper.get_terminal_id());
                           parametros.Add("venta_id_origen", insert_id_nueva_venta);
                           parametros.Add("caducidad", detallado.Item2);
                           parametros.Add("lote", detallado.Item3);
                           parametros.Add("articulo_id", detallado.Item1);
                            parametros.Add("cantidad", detallado.Item5);
                           
                           conector.Insert(sql, parametros);

                            sql = @"
                                UPDATE
                                    farmacontrol_local.existencias
                                SET
                                    existencia = (existencia - @cantidad)
                                WHERE
                                    caducidad = @caducidad
                                AND
                                    lote = @lote
                                AND     
                                    articulo_id =@articulo_id
                            ";

                           parametros = new Dictionary<string, object>();
                           parametros.Add("caducidad", detallado.Item2);
                           parametros.Add("lote", detallado.Item3);
                           parametros.Add("articulo_id", detallado.Item1);
                            parametros.Add("cantidad", detallado.Item5);
                            conector.Insert(sql, parametros);

                    }


                  
                    sql = @"
						UPDATE
							farmacontrol_local.ventas
						SET
							fecha_terminado = NOW()
						WHERE
							venta_id = @venta_id_origen
					";

                    parametros = new Dictionary<string, object>();
                    parametros.Add("venta_id_origen", insert_id_nueva_venta);

                    conector.Update(sql, parametros);

                    sql = @"
							INSERT INTO
								farmacontrol_local.ventas_pagos
							(
                                SELECT
                                    0 AS venta_pago_id,    
    								@venta_id_origen AS venta_id,
                                    (SELECT pago_tipo_id FROM farmacontrol_global.pago_tipos WHERE etiqueta = 'RCAN' LIMIT 1) AS pago_tipo_id,
                                    '' AS cuenta,    
                                    SUM(detallado_ventas.total) AS importe,
                                    SUM(detallado_ventas.total) AS monto
                                FROM
                                    farmacontrol_local.detallado_ventas
                                WHERE
                                    detallado_ventas.venta_id = @venta_id
                                GROUP BY detallado_ventas.venta_id
                                LIMIT 1
							)
                            ON DUPLICATE KEY UPDATE
                                venta_id = venta_id,
                                pago_tipo_id = pago_tipo_id
						";

                    parametros.Add("venta_id", insert_id_nueva_venta);
                    conector.Insert(sql, parametros);
              
                     sql = @"
					    UPDATE
						    farmacontrol_local.cancelaciones
					    SET
						    nueva_venta_id = @venta_origen,
						    fecha =  NOW(),
						    termina_empleado_id = @empleado_id
					    WHERE
						    cancelacion_id = @insert_id_cancelacion
				    ";

                    parametros = new Dictionary<string, object>();
                    parametros.Add("empleado_id", FORMS.comunes.Principal.empleado_id);
                    parametros.Add("venta_origen", insert_id_nueva_venta);
                    parametros.Add("insert_id_cancelacion", insert_id_cancelacion);
                    conector.Update(sql, parametros);

                    }
                    conector.TransactionCommit();
                    validacion.elemento_id = ((venta_id_origen.Equals(null)) ? (int)venta_id : (int)venta_id_origen);
                    validacion.status = true;
                    validacion.informacion = "Venta cancelada correctamente";
          
            }
            catch (Exception exception)
            {
                Log_error.log(exception);
            }

            return validacion;

        }

        public DataTable get_cancelacion_info(long venta_id)
        { 
            
            string sql = @"
				SELECT
					amecop_original,
                    nombre,
                    detallado_cancelaciones.precio_publico as precio_publico,
                    detallado_cancelaciones.pct_descuento as porcentaje_descuento,
                    importe_descuento,
                    importe,
                    cantidad,
                    SUM( subtotal ) AS subtotal,
                    detallado_cancelaciones.pct_iva as porcentaje_iva,
                    importe_iva,
                    SUM(total) as total 
				FROM
					farmacontrol_local.cancelaciones
				LEFT JOIN 
                    farmacontrol_local.detallado_cancelaciones
                USING(cancelacion_id)
                LEFT JOIN
                    farmacontrol_global.articulos
                USING(articulo_id)
				WHERE
					cancelaciones.venta_id = @venta_id
                GROUP BY 
                    articulo_id,lote,caducidad
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("venta_id", venta_id);

            conector.Select(sql, parametros);

            return conector.result_set;

        }

        //METODO  QUE TERMINA LA CANCELACION TOTAL DEL PRODUCTO
        public bool cancelacion_total_venta(long venta_id, string nombrecliente, long telefono, string correo, string comentarios)
        {
            bool terminado = false;
           
           
            string sql = @"
				SELECT
					venta_id
				FROM
					farmacontrol_local.ventas
                INNER JOIN
                    farmacontrol_local.detallado_ventas
                USING( venta_id )
				WHERE
					venta_id = @venta_id
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("venta_id", venta_id);

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {

                //crear registro en la tabla cancelaciones
                try
                {
                    conector.TransactionStart();
                    //tiene que afectar igual el Kardex
                    int empleado_id = (int)FORMS.comunes.Principal.empleado_id;

                    sql = @"
					INSERT INTO
						farmacontrol_local.cancelaciones
					SET
						venta_id = @venta_id,
						comentarios = @comentarios,
						empleado_id = @empleado_id,
						terminal_id = @terminal_id,
                        nombre_cliente = @nombre_cliente,
                        telefono   = @telefono,
                        correo     = @correo
				    ";

                    parametros = new Dictionary<string, object>();
                    parametros.Add("venta_id", venta_id);
                    parametros.Add("empleado_id", empleado_id);
                    parametros.Add("comentarios", comentarios);
                    parametros.Add("terminal_id", Convert.ToInt64(Misc_helper.get_terminal_id()));
                    parametros.Add("nombre_cliente", nombrecliente);
                    parametros.Add("telefono", telefono);
                    parametros.Add("correo", correo);

                    conector.Insert(sql, parametros);

                    sql = @"
					    SET @insert_id_cancelacion = LAST_INSERT_ID();
				    ";

                    conector.Select(sql);

                    sql = @"
					INSERT INTO
						farmacontrol_local.detallado_cancelaciones
					(
						SELECT
							0 AS detallado_cancelacion_id,
							@insert_id_cancelacion AS cancelacion_id,
							articulo_id,
							caducidad,
							lote,
							precio_publico,
							pct_descuento,
							importe_descuento,
							importe,
							cantidad,
							subtotal,
							pct_iva,
							importe_iva,
							tipo_ieps,
							ieps,
							importe_ieps,
							total,
							es_promocion,
							comentarios,
							NOW()
						FROM
							farmacontrol_local.detallado_ventas	
						WHERE
							venta_id = @venta_id
						GROUP BY
							articulo_id,caducidad,lote,importe
					)
				   ";

                    parametros = new Dictionary<string, object>();
                    parametros.Add("venta_id", venta_id);
                    conector.Insert(sql, parametros);

                    parametros.Add("terminal_id", Convert.ToInt32(Misc_helper.get_terminal_id()));

                    sql = @"
                    INSERT INTO
                        farmacontrol_local.kardex (terminal_id, fecha_datetime, fecha_date, articulo_id, caducidad, lote, tipo, elemento_id, folio, existencia_anterior, cantidad, existencia_posterior)
                    (
                        SELECT
                            @terminal_id AS terminal_id,
                            NOW() AS fecha_datetime,
                            CURDATE() AS fecha_date,
                            articulo_id,
                            caducidad,
                            lote,
                            'DEVOLUCION_CLIENTE' AS tipo,
                            ventas.venta_id AS elemento_id,
                            ventas.venta_folio AS folio,
                            COALESCE(existencias.existencia, 0) AS existencia_anterior,
                            detallado_ventas.cantidad AS cantidad,
                            COALESCE(existencias.existencia, 0) + detallado_ventas.cantidad
                        FROM
                            farmacontrol_local.detallado_ventas
                        JOIN farmacontrol_local.ventas USING(venta_id)
                        LEFT JOIN farmacontrol_local.existencias USING(articulo_id, caducidad, lote)
                        WHERE
                            venta_id = @venta_id
                    )
                  ";

                   conector.Insert(sql, parametros);

                    sql = @"
                    INSERT INTO
                        farmacontrol_local.existencias (articulo_id, caducidad, lote, existencia)
                    (
                        SELECT
                            articulo_id,
                            caducidad,
                            lote,
                            cantidad
                        FROM
                            farmacontrol_local.detallado_ventas
                        WHERE
                            venta_id = @venta_id
                    )
                    ON DUPLICATE KEY UPDATE
                        existencia = existencia + cantidad
                    ";

                     conector.Insert(sql, parametros);

                     sql = @"
					     UPDATE
						    farmacontrol_local.cancelaciones
					     SET
						    fecha =  NOW(),
						    termina_empleado_id = @empleado_id
					     WHERE
						    cancelacion_id = @insert_id_cancelacion
				     ";

                     conector.Update(sql, parametros);

                     if (conector.TransactionCommit())
                     {
                         terminado = true;
                     }

                }
                catch (Exception exception)
                {
                    Log_error.log(exception);
                }

            }
            else
            {
                terminado = false;
            
            }
           
            return terminado;
        
        }
     
        //METODO QUE REALIZA LA DEVOLUCION PARCIAL CON AJUSTE 

        public bool set_ajuste_cancelacion( string nombre_cliente ,long telefono,string correo,long venta_id, string comentarios, List<Tuple<int, string, string, decimal, int>> lista_productos_cancelados, List<Tuple<int, string, string, decimal, int>> lista_productos_comprados, bool cancela_factura, bool emite_nota_credito, bool es_tae = false)
        { 
          bool terminado = false;
            
          Dictionary<string, object> parametros;

          try
          {
              int empleado_id = (int)FORMS.comunes.Principal.empleado_id;
              conector.TransactionStart();
             
              //VERIFICA SI HAY UN AJUSTE REALIZADO PREVIAMENTE DEL TICKET

              string  sql = @"
					SELECT
                       ajuste_existencia_id
                    FROM
                       farmacontrol_local.ajustes_existencias
                    WHERE
                       comentarios = CONCAT('DEVOLUCION-TICKET','#',@venta_id)       
                    
			  ";

              conector.Select(sql);

              var result_set_afectacion = conector.result_set;
              bool ajuste_existencia = false;
              foreach (DataRow row in result_set_afectacion.Rows)
              {
                  ajuste_existencia = true;
              }

              if( ajuste_existencia )
              {
                  conector.TransactionCommit();
                  return true;
              }

              sql = @"
				INSERT INTO
					farmacontrol_local.ajustes_existencias
				SET
                    terminal_id = @terminal_id,
                    empleado_id = @empleado_id,
                    termina_empleado_id = @empleado_id,
					fecha_creado = NOW(),
                    fecha_terminado = NOW(),
					comentarios = CONCAT('DEVOLUCION-TICKET','#',@venta_id)			
				";

              parametros = new Dictionary<string, object>();
              parametros.Add("venta_id", venta_id);
              parametros.Add("empleado_id", empleado_id);
              parametros.Add("comentarios", comentarios);
              parametros.Add("terminal_id", Convert.ToInt64(Misc_helper.get_terminal_id()));

              conector.Insert(sql, parametros);

              sql = @"
					INSERT INTO
						farmacontrol_local.cancelaciones
					SET
                        terminal_id = @terminal_id,
                        venta_id = @venta_id,
                        empleado_id = @empleado_id,
                        termina_empleado_id = @empleado_id,
						fecha = NOW(),
                        nombre_cliente = @nombre_cliente,
                        telefono = @telefono,
                        correo = @correo,
						comentarios = @comentarios
				";

              parametros = new Dictionary<string, object>();
              parametros.Add("terminal_id", Convert.ToInt64(Misc_helper.get_terminal_id()));
              parametros.Add("venta_id", venta_id);
              parametros.Add("empleado_id", empleado_id);
              parametros.Add("nombre_cliente", nombre_cliente);
              parametros.Add("telefono", telefono);
              parametros.Add("correo", correo);
              parametros.Add("comentarios", comentarios);

              conector.Insert(sql, parametros);

              if (conector.TransactionCommit())
              {
                  sql = @"
					SELECT
                        ajuste_existencia_id
                    FROM
                        farmacontrol_local.ajustes_existencias
                    ORDER BY 
                        ajuste_existencia_id DESC
                    LIMIT 1
                    
				  ";

                  conector.Select(sql);

                  var result_set_cancelacion = conector.result_set;
                  int insert_id_cancelacion = 0;
                  foreach (DataRow row in result_set_cancelacion.Rows)
                  {
                      insert_id_cancelacion = Convert.ToInt32(row["ajuste_existencia_id"].ToString());
                  }

                  #region PRODUCTOS QUE DEVUELVE EL CLIENTE
                  foreach (var detallado in lista_productos_cancelados)
                  {
                      //@cantidad as cantidad,
                      sql = @"
                            SELECT
                                @insert_id_cancelacion as ajuste_existencia_id,
                                @articulo_id as articulo_id,
                                @caducidad as caducidad,
                                @lote as lote,
                                COALESCE(existencias.existencia, 0) as existencia_anterior,
                                ( COALESCE(existencias.existencia, 0) + @cantidad ) as cantidad,
                                (( COALESCE(existencias.existencia, 0) + @cantidad ) - ( COALESCE(existencias.existencia, 0) )) as diferencia
                            FROM
                                farmacontrol_local.existencias
                            WHERE    
                                articulo_id = @articulo_id
                            AND
                                lote = @lote
                            AND
                                caducidad = @caducidad   
					  ";
                      parametros = new Dictionary<string, object>();
                      parametros.Add("articulo_id", detallado.Item1);
                      parametros.Add("caducidad", detallado.Item2);
                      parametros.Add("lote", detallado.Item3);
                      parametros.Add("cantidad", detallado.Item5);
                      parametros.Add("insert_id_cancelacion", insert_id_cancelacion);
                      conector.Select(sql, parametros);
                      
                      int existencia_anterior = 0;
                      int diferencia = 0;
                      int cantidad_modificada = 0;
                      if (conector.result_set.Rows.Count > 0)
                      {
                          var result_get_existencia = conector.result_set;

                          foreach (DataRow row in result_get_existencia.Rows)
                          {
                              existencia_anterior = Convert.ToInt32(row["existencia_anterior"].ToString());
                              diferencia = Convert.ToInt32(row["diferencia"].ToString());
                              cantidad_modificada = Convert.ToInt32(row["cantidad"].ToString());
                          }

                      }
                      else
                      {
                          existencia_anterior = 0;
                          diferencia = detallado.Item5;
                          cantidad_modificada = detallado.Item5;
                      }


                      sql = @"
							INSERT INTO
								farmacontrol_local.detallado_ajustes_existencias
							SET
                               ajuste_existencia_id = @insert_id_cancelacion,
                               articulo_id = @articulo_id,
                               caducidad = @caducidad,
                               lote = @lote,
                               existencia_anterior = @existencia_anterior,
                               cantidad = @cantidad,
                               diferencia = @diferencia
						";

                      parametros = new Dictionary<string, object>();
                      parametros.Add("articulo_id", detallado.Item1);
                      parametros.Add("caducidad", detallado.Item2);
                      parametros.Add("lote", detallado.Item3);
                      parametros.Add("cantidad", cantidad_modificada);
                      parametros.Add("insert_id_cancelacion", insert_id_cancelacion);
                      parametros.Add("existencia_anterior",  existencia_anterior);
                      parametros.Add("diferencia", diferencia);
                      conector.Insert(sql, parametros);


                      if (!es_tae)
                      {
                          sql = @"
                        INSERT INTO
                            farmacontrol_local.kardex( kardex_id,terminal_id,fecha_datetime,fecha_date, articulo_id,caducidad,lote,tipo,elemento_id,folio,existencia_anterior,cantidad,existencia_posterior,es_importado )
                        (
                            SELECT
                                COALESCE(sum(0),0) as kardex_id,
                                @terminal_id AS terminal_id,
                                NOW() AS fecha_datetime,
                                CURDATE() AS fecha_date,
                                @articulo_id,
                                @caducidad,
                                @lote,
                                'DEVOLUCION_CLIENTE' AS tipo,
                                @venta_id AS elemento_id,
                                @insert_id_cancelacion AS folio,
                                COALESCE(existencias.existencia, 0) AS existencia_anterior,
                                @cantidad AS cantidad,
                                COALESCE(existencias.existencia, 0) + @cantidad as existencia_posterior,
                                0 as es_importado
                            FROM
                               farmacontrol_local.existencias
                            WHERE
                                articulo_id = @articulo_id
                            AND
                                lote = @lote
                            AND
                                caducidad = @caducidad
                        )
                       ";

                          parametros = new Dictionary<string, object>();
                          parametros.Add("terminal_id", Convert.ToInt32(Misc_helper.get_terminal_id()));
                          parametros.Add("articulo_id", detallado.Item1);
                          parametros.Add("caducidad", detallado.Item2);
                          parametros.Add("lote", detallado.Item3);
                          parametros.Add("venta_id", venta_id);
                          parametros.Add("cantidad", detallado.Item5);
                          parametros.Add("insert_id_cancelacion", insert_id_cancelacion);
                          conector.Insert(sql, parametros);

          
                          sql = @"
                            SELECT 
                                existencia_id 
                            FROM
                                farmacontrol_local.existencias 
                            WHERE   
                                articulo_id = @articulo_id
                            AND
                                caducidad = @caducidad
                            AND
                                lote = @lote
                           
                         ";

                          parametros = new Dictionary<string, object>();
                          parametros.Add("articulo_id", detallado.Item1);
                          parametros.Add("caducidad", detallado.Item2);
                          parametros.Add("lote", detallado.Item3);       
                         
                          conector.Select(sql, parametros);

                          if (conector.result_set.Rows.Count > 0)
                          {
                              sql = @"
                               UPDATE
                                    farmacontrol_local.existencias
                                SET 
                                    existencia = (existencia + @cantidad)
                                WHERE
                                    articulo_id = @articulo_id
                                AND
                                    caducidad = @caducidad
                                AND
                                    lote = @lote   
                             ";

                              parametros = new Dictionary<string, object>();
                              parametros.Add("articulo_id", detallado.Item1);
                              parametros.Add("caducidad", detallado.Item2);
                              parametros.Add("lote", detallado.Item3);
                              parametros.Add("cantidad", detallado.Item5);
                              conector.Insert(sql, parametros);


                          }
                          else
                          {
                              //NO HAY EXISTENCIA
                              sql = @"
                                INSERT INTO
                                    farmacontrol_local.existencias
                                SET 
                                    articulo_id = @articulo_id,
                                    caducidad = @caducidad,
                                    lote = @lote,
                                    existencia = @cantidad
                             ";


                              parametros = new Dictionary<string, object>();
                              parametros.Add("articulo_id", detallado.Item1);
                              parametros.Add("caducidad", detallado.Item2);
                              parametros.Add("lote", detallado.Item3);
                              parametros.Add("cantidad", detallado.Item5);
                              conector.Insert(sql, parametros);
                              //conector.TransactionCommit();
                          
                          }


                      }

                  }
                  #endregion
                  #region  PRODUCTOS QUE LLEVA EL CLIENTE
                  if (lista_productos_comprados.Count > 0)
                  {
                      int terminal_id = (int)HELPERS.Misc_helper.get_terminal_id();
                   
                      foreach (var detallado in lista_productos_comprados)
                      {
                          //@cantidad as cantidad
                          sql = @"
                              SELECT 
                                  @insert_id_cancelacion as ajuste_existencia_id,
                                  @articulo_id as articulo_id,
                                  @caducidad as caducidad,
                                  @lote as lote,
                                  COALESCE(existencias.existencia, 0) as existencia_anterior,
                                  ( COALESCE(existencias.existencia, 0) - @cantidad ) as cantidad,
                                  ( COALESCE(existencias.existencia, 0) - @cantidad ) - COALESCE(existencias.existencia, 0) as diferencia
                               FROM
                                   farmacontrol_local.existencias
                               WHERE    
                                   articulo_id = @articulo_id
                               AND
                                   lote = @lote
                               AND
                                   caducidad = @caducidad                  
						";

                          parametros = new Dictionary<string, object>();
                          parametros.Add("articulo_id", detallado.Item1);
                          parametros.Add("caducidad", detallado.Item2);
                          parametros.Add("lote", detallado.Item3);
                          parametros.Add("cantidad", detallado.Item5);
                          parametros.Add("insert_id_cancelacion", insert_id_cancelacion);
                          conector.Select(sql, parametros);

                          int existencia_anterior = 0;
                          int diferencia = 0;
                          int cantidad_convertida_lleva = 0;
                          if (conector.result_set.Rows.Count > 0)
                          {
                              var result_get_existencia_lleva = conector.result_set;

                              foreach (DataRow row in result_get_existencia_lleva.Rows)
                              {
                                  existencia_anterior = Convert.ToInt32(row["existencia_anterior"].ToString());
                                  diferencia = Convert.ToInt32(row["diferencia"].ToString());
                                  cantidad_convertida_lleva = Convert.ToInt32(row["cantidad"].ToString());
                              }

                              sql = @"
							    INSERT INTO
								    farmacontrol_local.detallado_ajustes_existencias
							    SET
                                   ajuste_existencia_id = @insert_id_cancelacion,
                                   articulo_id = @articulo_id,
                                   caducidad = @caducidad,
                                   lote = @lote,
                                   existencia_anterior = @existencia_anterior,
                                   cantidad = @cantidad,
                                   diferencia = @diferencia
						      ";

                                parametros = new Dictionary<string, object>();
                                parametros.Add("articulo_id", detallado.Item1);
                                parametros.Add("caducidad", detallado.Item2);
                                parametros.Add("lote", detallado.Item3);
                                parametros.Add("cantidad", cantidad_convertida_lleva);
                                parametros.Add("insert_id_cancelacion", insert_id_cancelacion);
                                parametros.Add("existencia_anterior", existencia_anterior);
                                parametros.Add("diferencia", diferencia);
                                conector.Insert(sql, parametros);

                                sql = @"
                                INSERT INTO
                                    farmacontrol_local.kardex( kardex_id,terminal_id,fecha_datetime,fecha_date, articulo_id,caducidad,lote,tipo,elemento_id,folio,existencia_anterior,cantidad,existencia_posterior,es_importado )
                                (
                                    SELECT
                                        COALESCE(sum(0),0) as kardex_id,
                                        @terminal_id AS terminal_id,
                                        NOW() AS fecha_datetime,
                                        CURDATE() AS fecha_date,
                                        @articulo_id,
                                        @caducidad,
                                        @lote,
                                        'DEVOLUCION_CLIENTE' AS tipo,
                                        @venta_id AS elemento_id,
                                        @insert_id_cancelacion AS folio,
                                        COALESCE(existencias.existencia, 0) AS existencia_anterior,
                                        -@cantidad AS cantidad,
                                        COALESCE(existencias.existencia, 0) - @cantidad as existencia_posterior,
                                        0 as es_importado
                                    FROM
                                       farmacontrol_local.existencias
                                    WHERE
                                        articulo_id = @articulo_id
                                    AND
                                        lote = @lote
                                    AND
                                        caducidad = @caducidad
                                )
                                ";

                                parametros = new Dictionary<string, object>();
                                parametros.Add("terminal_id", terminal_id);
                                parametros.Add("articulo_id", detallado.Item1);
                                parametros.Add("caducidad", detallado.Item2);
                                parametros.Add("lote", detallado.Item3);
                                parametros.Add("venta_id", venta_id);
                                parametros.Add("cantidad", detallado.Item5);
                                parametros.Add("insert_id_cancelacion", insert_id_cancelacion);

                                conector.Insert(sql, parametros);

                                sql = @"
                                UPDATE
                                    farmacontrol_local.existencias
                                SET
                                    existencia = (existencia - @cantidad)
                                WHERE
                                    caducidad = @caducidad
                                AND
                                    lote = @lote
                                AND     
                                    articulo_id =@articulo_id
                               ";

                                parametros = new Dictionary<string, object>();
                                parametros.Add("caducidad", detallado.Item2);
                                parametros.Add("lote", detallado.Item3);
                                parametros.Add("articulo_id", detallado.Item1);
                                parametros.Add("cantidad", detallado.Item5);
                                conector.Insert(sql, parametros);

                                sql = @"
                                    DELETE
                                    FROM
                                        farmacontrol_local.existencias
                                    WHERE
                                        caducidad = @caducidad
                                    AND
                                        lote = @lote
                                    AND     
                                        articulo_id =@articulo_id
                                    AND
                                        existencia = 0
                                ";

                                parametros = new Dictionary<string, object>();
                                parametros.Add("caducidad", detallado.Item2);
                                parametros.Add("lote", detallado.Item3);
                                parametros.Add("articulo_id", detallado.Item1);
                                parametros.Add("cantidad", detallado.Item5);
                                conector.Insert(sql, parametros);

                          }

                      }
                  }
                  #endregion

                  terminado = true;
              }
              else
              {
                  terminado = false;
              }

          }
          catch (Exception exception)
          {
              Log_error.log(exception);
              terminado = false;
          }

          return terminado;
        }

        public DataTable get_cancelacion_parcial(long venta_id)
        {

            string busqueda_comentarios = "DEVOLUCION-TICKET#"+venta_id;

            DataTable tmp = new DataTable();

            string sql = @"
                SELECT 
                  ajuste_existencia_id 
                FROM 
                  farmacontrol_local.ajustes_existencias
               WHERE
                  comentarios = @busqueda
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("busqueda", busqueda_comentarios);
            conector.Select(sql, parametros);
            Int32 ajuste_existencia_id = 0;
            if (conector.result_set.Rows.Count > 0)
            {

                foreach (DataRow row in conector.result_set.Rows)
                {
                    ajuste_existencia_id = Convert.ToInt32(row["ajuste_existencia_id"].ToString());
                }

                sql = @"
				SELECT
                  articulo_id,
                  ajuste_existencia_id as folio_ajuste,
                  CONCAT('*',SUBSTRING(amecop_original,-4)) AS codigo,
                  nombre,
                  abs(cantidad - existencia_anterior)  as cantidad,
                  format(( precio_publico -(precio_publico*pct_descuento)  ) + ( precio_publico -(precio_publico*pct_descuento) )*pct_iva ,2 ) as precio_venta,
                  if( diferencia > 0,'DEVOLVIO','LLEVO' ) as accion
                FROM
                  farmacontrol_local.detallado_ajustes_existencias
                LEFT JOIN 
                   farmacontrol_global.articulos
                USING(articulo_id)   
                WHERE
                   ajuste_existencia_id = @ajuste_existencia_id
                AND 
                   activo = 1 
			";

                parametros = new Dictionary<string, object>();
                parametros.Add("ajuste_existencia_id", ajuste_existencia_id);

                conector.Select(sql, parametros);

                tmp = conector.result_set;
            }

            return tmp;
        
        }


        public float precio_venta_cancelacion(long venta_folio, long articulo_id)
        { 
          float precio_venta_ticket = 0;

          string sql = @"

                    SELECT                        COALESCE(total/cantidad) as precio_venta                    FROM                       farmacontrol_local.ventas                    INNER JOIN                        farmacontrol_local.detallado_ventas                    USING(venta_id)                    WHERE                        venta_folio = @venta_folio                    AND                        articulo_id = @articulo_id
    
				";

          Dictionary<string, object> parametros = new Dictionary<string, object>();
          parametros.Add("venta_folio", venta_folio);
          parametros.Add("articulo_id", articulo_id);
          conector.Select(sql, parametros);
          var result_set_cancelacion = conector.result_set;

          foreach (DataRow row in result_set_cancelacion.Rows)
          {
              precio_venta_ticket = float.Parse(row["precio_venta"].ToString());
          }

         

          return precio_venta_ticket;
        
        }

        public string total_venta_cancelada(long venta_id)
        {
           string total = "0";
           string sql = @"
					SELECT
                        SUM( total ) as total
                    FROM
                        farmacontrol_local.ventas
                    INNER JOIN 
                        farmacontrol_local.detallado_ventas
                    USING( venta_id )
                    WHERE 
                        venta_id = @venta_id
                    
				";

           Dictionary<string, object> parametros = new Dictionary<string, object>();
           parametros.Add("venta_id", venta_id);
           conector.Select(sql, parametros);
           var result_set_cancelacion = conector.result_set;

           foreach (DataRow row in result_set_cancelacion.Rows)
           {
               total =  row["total"].ToString();
           }

           return total;
        
        }
    
    }
}
