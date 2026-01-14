using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.CLASSES.PRINT;
using System.Data;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Prepago
	{
		Conector conector = new Conector();

		public bool cancelar_prepago(long prepago_id, List<DTO_Prepago_parcial_entregado> lista_productos)
		{
			string sql = "";

			try
			{
				Dictionary<string, object> parametros;
				conector.TransactionStart();

				DAO_Ventas dao_ventas = new DAO_Ventas();
				int empleado_id = (int)FORMS.comunes.Principal.empleado_id;

				int terminal_id = (int)HELPERS.Misc_helper.get_terminal_id();

				long? venta_id_origen = null;

                sql = @"
					UPDATE 
						farmacontrol_local.prepagos
					SET
                        cancela_terminal_id = @terminal_id
                    WHERE
						prepago_id = @prepago_id
				";

                parametros = new Dictionary<string, object>();
                parametros.Add("prepago_id", prepago_id);
                parametros.Add("terminal_id", Misc_helper.get_terminal_id());

                conector.Update(sql, parametros);

				if(lista_productos.Count > 0)
				{
					venta_id_origen = dao_ventas.registrar_venta(empleado_id);

					sql = @"
						DELETE FROM
							farmacontrol_local.apartados
						WHERE
							prepago_id = @prepago_id
					";

					parametros  = new Dictionary<string,object>();
					parametros.Add("prepago_id",prepago_id);

					conector.Delete(sql,parametros);

					/*
					 * IMPORTAR VENTA ACTUAL A UNA NUEVA VENTA PARA PROCESAR LA CANCELACION EN LA VENTA ACTUAL
					 */

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


					/*
					* FIN DEL PROCESO DE IMPORTACION
					*/

					foreach(DTO_Prepago_parcial_entregado det in lista_productos)
					{

						sql = @"
							INSERT INTO
								farmacontrol_local.detallado_ventas
								(
									SELECT
										0 as detallado_venta_id,
										@venta_id AS venta_id,
										articulo_id,
										@caducidad AS caducidad,
										@lote AS lote,
										precio_publico,
										articulos.pct_descuento,
										FORMAT((precio_publico * articulos.pct_descuento), 4) AS importe_descuento,
										precio_publico - (precio_publico * articulos.pct_descuento) AS importe,
						
										@cantidad AS cantidad,
	
										(precio_publico - (precio_publico * articulos.pct_descuento)) * @cantidad AS subtotal,
										pct_iva,
										((precio_publico - (precio_publico * articulos.pct_descuento)) * @cantidad) * pct_iva AS importe_iva,
										tipo_ieps,
										ieps,
										IF(tipo_ieps = 'PCT', ((precio_publico - (precio_publico * articulos.pct_descuento)) * ieps) , ieps) AS importe_ieps,
										CAST( ((precio_publico - (precio_publico * articulos.pct_descuento)) * @cantidad) AS DECIMAL(13,2))
                                        + CAST( (((precio_publico - (precio_publico * articulos.pct_descuento)) * @cantidad) * pct_iva)  AS DECIMAL(13,2)) AS total,
										@es_promocion AS es_promocion,
										'' AS comentarios,
										NOW() AS modified
									FROM
										articulos
									LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id)
									WHERE
										articulos_amecops.amecop = @amecop	
								)
							ON DUPLICATE KEY UPDATE
								cantidad = cantidad + @cantidad,
								subtotal = subtotal + ((articulos.precio_publico - (articulos.precio_publico * articulos.pct_descuento)) * @cantidad),
								importe_iva = importe_iva + (((articulos.precio_publico - (articulos.precio_publico * articulos.pct_descuento)) * @cantidad) * articulos.pct_iva),
								total = total + 
                                CAST(  (((articulos.precio_publico - (articulos.precio_publico * articulos.pct_descuento)) * @cantidad)  AS DECIMAL(13,2) ) + 
                                CAST(  (((articulos.precio_publico - (articulos.precio_publico * articulos.pct_descuento)) * @cantidad) * articulos.pct_iva)) AS DECIMAL(13,2) )
			
						";

						parametros = new Dictionary<string, object>();
						parametros.Add("venta_id", venta_id_origen);
						parametros.Add("amecop", det.amecop);
						parametros.Add("caducidad", Misc_helper.CadtoDate(det.caducidad));
						parametros.Add("lote", det.lote);
						parametros.Add("cantidad", det.cantidad_conservar);
						parametros.Add("es_promocion", 0);
						parametros.Add("pct_descuento", "articulos.pct_descuento");

						conector.Insert(sql, parametros);
					}

					sql = @"
						UPDATE
							farmacontrol_local.detallado_prepagos
						SET
							cantidad_entregada = 0
						WHERE
							prepago_id = @prepago_id
					";

					parametros = new Dictionary<string,object>();
					parametros.Add("prepago_id",prepago_id);

					conector.Update(sql,parametros);


					sql= @"
						UPDATE
							farmacontrol_local.detallado_prepagos
						JOIN
							(
								SELECT
									articulo_id,
									COALESCE(SUM(detallado_ventas.cantidad), 0) AS cantidad
								FROM
									farmacontrol_local.detallado_prepagos
								LEFT JOIN farmacontrol_local.detallado_ventas USING(articulo_id)
								WHERE
									detallado_prepagos.prepago_id = @prepago_id
								AND
									detallado_ventas.venta_id = @venta_id
								GROUP BY detallado_prepagos.articulo_id
							) AS tmp USING(articulo_id)
							SET
								detallado_prepagos.cantidad_entregada = tmp.cantidad
							WHERE
								detallado_prepagos.prepago_id = @prepago_id
					";

					parametros = new Dictionary<string,object>();
					parametros.Add("prepago_id",prepago_id);
					parametros.Add("venta_id",venta_id_origen);

					conector.Update(sql,parametros);
				}
				else
				{
					sql = @"
						UPDATE
							farmacontrol_local.detallado_prepagos
						SET
							cantidad_entregada = 0
						WHERE
							prepago_id = @prepago_id
					";

					parametros = new Dictionary<string, object>();
					parametros.Add("prepago_id", prepago_id);

					conector.Update(sql, parametros);	
				}

				if(venta_id_origen != null)
				{
					sql = @"
						UPDATE
							farmacontrol_local.ventas
						SET
							fecha_terminado = NOW()
						WHERE
							venta_id = @venta_id
					";

					parametros = new Dictionary<string,object>();
					parametros.Add("venta_id",venta_id_origen);

					conector.Update(sql,parametros);

					sql = @"
						INSERT INTO
							farmacontrol_local.ventas_pagos
						(		
							SELECT
								0 AS venta_pago_id,
								@venta_id AS venta_id,
								(
									SELECT
										pago_tipo_id
									FROM
										farmacontrol_global.pago_tipos
									WHERE
										nombre LIKE '%EFECTIVO%'
									LIMIT 1
								) as pago_tipo_id,
								' ' AS cuenta,
                                SUM(detallado_ventas.total) AS importe,
								SUM(detallado_ventas.total) AS monto
							FROM
								farmacontrol_local.detallado_ventas
							WHERE
								detallado_ventas.venta_id = @venta_id
							GROUP BY detallado_ventas.venta_id
						)	
                        ON DUPLICATE KEY UPDATE
                            venta_id = venta_id
					";

					conector.Insert(sql,parametros);
				}

				conector.TransactionCommit();

				if(venta_id_origen != null)
				{
					Ticket_venta ticket = new Ticket_venta();
					ticket.construccion_ticket((long)venta_id_origen);
					ticket.print();
				}

				return true;
			}
			catch(Exception)
			{
				conector.TransactionRollback();
			}

			return false;
		}

		public List<DTO_Prepago_parcial_entregado> get_productos_entrega_parcial(long prepago_id)
		{
			List<DTO_Prepago_parcial_entregado> lista_entraga_parcial = new List<DTO_Prepago_parcial_entregado>();

			string sql = @"
				SELECT
					(
						SELECT
							amecop
						FROM
							farmacontrol_global.articulos_amecops
						WHERE
							articulos_amecops.articulo_id = apartados.articulo_id
						ORDER BY amecop_principal DESC
						LIMIT 1
					) AS amecop,
					articulos.articulo_id AS articulo_id,
					articulos.nombre AS producto,
					DATE_FORMAT(caducidad, '%Y-%m-%d') AS caducidad,
					apartados.lote,
					apartados.cantidad
				FROM
					farmacontrol_local.apartados
				JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					prepago_id = @prepago_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("prepago_id",prepago_id);

			conector.Select(sql,parametros);
			
			foreach(DataRow row in conector.result_set.Rows)
			{
				DTO_Prepago_parcial_entregado det_prepago = new DTO_Prepago_parcial_entregado();
				det_prepago.amecop = row["amecop"].ToString();
				det_prepago.articulo_id = Convert.ToInt64(row["articulo_id"]);
				det_prepago.cantidad = Convert.ToInt64(row["cantidad"]);
				det_prepago.producto = row["producto"].ToString();
                det_prepago.caducidad = Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");
				det_prepago.lote = row["lote"].ToString();

				lista_entraga_parcial.Add(det_prepago);
			}

			return lista_entraga_parcial;
		}

		public List<DTO_Prepago> get_prepagos_cancelados_corte(long corte_id, bool corte_total = false)
		{
			List<DTO_Prepago> prepagos = new List<DTO_Prepago>();

            DAO_Cortes dao_cortes = new DAO_Cortes();
            var corte_data = dao_cortes.get_informacion_corte(corte_id);

            string sql = @"
                SELECT 
                    COALESCE(cortes.fecha, (
                        SELECT 
                            MIN(ventas.fecha_terminado) 
                        FROM 
                            farmacontrol_local.ventas 
                        WHERE 
                            ventas.terminal_id = @terminal_id
                    )) AS fecha
                FROM 
                    farmacontrol_local.cortes 
                WHERE 
                    tipo = @tipo 
                AND
                    terminal_id = @terminal_id
                ORDER 
                BY corte_id DESC 
                LIMIT 1
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("terminal_id",Misc_helper.get_terminal_id());
            parametros.Add("tipo", corte_data.tipo);

            conector.Select(sql,parametros);

            string fecha_inicio = Misc_helper.fecha(conector.result_set.Rows[0]["fecha"].ToString());

			sql = @"
				SELECT
					*,
					(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = prepagos.cancela_empleado_id) AS nombre_empleado_cancela,
					clientes.nombre AS nombre_cliente,
					empleados.nombre AS nombre_empleado
				FROM
					farmacontrol_local.prepagos
				LEFT JOIN farmacontrol_global.clientes USING(cliente_id)
				LEFT JOIN farmacontrol_global.empleados ON
					empleados.empleado_id = prepagos.pago_empleado_id
				WHERE
					fecha_canje IS NULL
				AND
					fecha_cancelado IS NOT NULL
				AND
					terminal_id = @terminal_id
				AND
					fecha_cancelado BETWEEN @fecha_inicio AND @fecha_fin
			";

            parametros.Add("fecha_inicio",fecha_inicio);
            parametros.Add("fecha_fin",Misc_helper.fecha(corte_data.fecha.ToString()));

			conector.Select(sql,parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				foreach(DataRow row in conector.result_set.Rows)
				{
					DTO_Prepago prepago = new DTO_Prepago();
					long? nullable = null;
					DateTime? nullable_date = null;

					prepago.prepago_id = Convert.ToInt64(row["prepago_id"]);
					prepago.cliente_id = row["cliente_id"].ToString();
					prepago.pago_empleado_id = Convert.ToInt64(row["pago_empleado_id"]);
					prepago.canje_empleado_id = (row["canje_empleado_id"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["canje_empleado_id"]);
					prepago.cancela_empleado_id = (row["cancela_empleado_id"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["cancela_empleado_id"]);
					prepago.corte_parcial_id = (row["corte_parcial_id"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["corte_parcial_id"]);
					prepago.codigo = row["codigo"].ToString();
					prepago.fecha_pago = Convert.ToDateTime(row["fecha_pago"].ToString());
					prepago.fecha_canje = (row["fecha_canje"].ToString().Equals("")) ? nullable_date : Convert.ToDateTime(row["fecha_canje"].ToString());
					prepago.fecha_cancelado = (row["fecha_cancelado"].ToString().Equals("")) ? nullable_date : Convert.ToDateTime(row["fecha_cancelado"].ToString());
					prepago.monto = Convert.ToDecimal(row["monto"]);
					prepago.tipo_devolucion = row["tipo_devolucion"].ToString();
					prepago.comentario = row["comentario"].ToString();
					prepago.nombre_cliente = row["nombre_cliente"].ToString().ToUpper();
					prepago.nombre_empleado = row["nombre_empleado"].ToString().ToUpper();
					prepago.nombre_empleado_cancela = row["nombre_empleado_cancela"].ToString();	

					prepagos.Add(prepago);
				}
			}

			return prepagos;
		}

		public List<DTO_Prepago> get_prepagos_canjeados_corte(long corte_id, bool corte_total = false)
		{
			List<DTO_Prepago> prepagos = new List<DTO_Prepago>();
            DAO_Cortes dao_cortes = new DAO_Cortes();

            var corte_data = dao_cortes.get_informacion_corte(corte_id);

			string sql = string.Format(@"
				SELECT
					*,
					(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = prepagos.cancela_empleado_id) AS nombre_empleado_cancela,
					clientes.nombre AS nombre_cliente,
					empleados.nombre AS nombre_empleado
				FROM
					farmacontrol_local.prepagos
				LEFT JOIN farmacontrol_global.clientes USING(cliente_id)
				LEFT JOIN farmacontrol_global.empleados ON
					empleados.empleado_id = prepagos.pago_empleado_id
				WHERE
					fecha_canje IS NOT NULL
				AND
					terminal_id = @terminal_id
				AND
					fecha_canje BETWEEN (
                    SELECT 
                        fecha
                    FROM
                        farmacontrol_local.cortes
                    WHERE
                        terminal_id = @terminal_id
                    AND
                        tipo = @tipo
                    ORDER By cortes.corte_id DESC
                    LIMIT 1,1
                ) AND @fecha_corte
			", (corte_total == true) ? "corte_total_id" : "corte_parcial_id");

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("terminal_id", Misc_helper.get_terminal_id());
			parametros.Add("tipo", corte_data.tipo);
            parametros.Add("fecha_corte",Misc_helper.fecha(corte_data.fecha.ToString()));

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				foreach (DataRow row in conector.result_set.Rows)
				{
					DTO_Prepago prepago = new DTO_Prepago();
					long? nullable = null;
					DateTime? nullable_date = null;

					prepago.prepago_id = Convert.ToInt64(row["prepago_id"]);
					prepago.cliente_id = row["cliente_id"].ToString();
					prepago.pago_empleado_id = Convert.ToInt64(row["pago_empleado_id"]);
					prepago.canje_empleado_id = (row["canje_empleado_id"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["canje_empleado_id"]);
					prepago.cancela_empleado_id = (row["cancela_empleado_id"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["cancela_empleado_id"]);
					prepago.corte_parcial_id = (row["corte_parcial_id"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["corte_parcial_id"]);
					prepago.codigo = row["codigo"].ToString();
					prepago.fecha_pago = Convert.ToDateTime(row["fecha_pago"].ToString());
					prepago.fecha_canje = (row["fecha_canje"].ToString().Equals("")) ? nullable_date : Convert.ToDateTime(row["fecha_canje"].ToString());
					prepago.fecha_cancelado = (row["fecha_cancelado"].ToString().Equals("")) ? nullable_date : Convert.ToDateTime(row["fecha_cancelado"].ToString());
					prepago.monto = Convert.ToDecimal(row["monto"]);
					prepago.tipo_devolucion = row["tipo_devolucion"].ToString();
					prepago.comentario = row["comentario"].ToString();
					prepago.nombre_cliente = row["nombre_cliente"].ToString().ToUpper();
					prepago.nombre_empleado = row["nombre_empleado"].ToString().ToUpper();
					prepago.nombre_empleado_cancela = row["nombre_empleado_cancela"].ToString();

					prepagos.Add(prepago);
				}
			}

			return prepagos;
		}

		public List<DTO_Prepago> get_prepagos_realizados_corte(long corte_id, bool corte_total = false)
		{
			List<DTO_Prepago> prepagos = new List<DTO_Prepago>();
			
			string sql = string.Format(@"
				SELECT
					*,
					(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = prepagos.cancela_empleado_id) AS nombre_empleado_cancela,
					clientes.nombre AS nombre_cliente,
					empleados.nombre AS nombre_empleado
				FROM
					farmacontrol_local.prepagos
				LEFT JOIN farmacontrol_global.clientes USING(cliente_id)
				LEFT JOIN farmacontrol_global.empleados ON
					empleados.empleado_id = prepagos.pago_empleado_id
                WHERE
					terminal_id = @terminal_id
				AND
					{0} = @corte_id	
			", (corte_total == true) ? "corte_total_id" : "corte_parcial_id");

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("terminal_id",Misc_helper.get_terminal_id());
			parametros.Add("corte_id",corte_id);

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				foreach (DataRow row in conector.result_set.Rows)
				{
					DTO_Prepago prepago = new DTO_Prepago();
					long? nullable = null;
					DateTime? nullable_date = null;

					prepago.prepago_id = Convert.ToInt64(row["prepago_id"]);
					prepago.cliente_id = row["cliente_id"].ToString();
					prepago.pago_empleado_id = Convert.ToInt64(row["pago_empleado_id"]);
					prepago.canje_empleado_id = (row["canje_empleado_id"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["canje_empleado_id"]);
					prepago.cancela_empleado_id = (row["cancela_empleado_id"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["cancela_empleado_id"]);
					prepago.corte_parcial_id = (row["corte_parcial_id"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["corte_parcial_id"]);
					prepago.codigo = row["codigo"].ToString();
					prepago.fecha_pago = Convert.ToDateTime(row["fecha_pago"].ToString());
					prepago.fecha_canje = (row["fecha_canje"].ToString().Equals("")) ? nullable_date : Convert.ToDateTime(row["fecha_canje"].ToString());
					prepago.fecha_cancelado = (row["fecha_cancelado"].ToString().Equals("")) ? nullable_date : Convert.ToDateTime(row["fecha_cancelado"].ToString());
					prepago.monto = Convert.ToDecimal(row["monto"]);
					prepago.tipo_devolucion = row["tipo_devolucion"].ToString();
					prepago.comentario = row["comentario"].ToString();
					prepago.nombre_cliente = row["nombre_cliente"].ToString().ToUpper();
					prepago.nombre_empleado = row["nombre_empleado"].ToString().ToUpper();
					prepago.nombre_empleado_cancela = row["nombre_empleado_cancela"].ToString();

					prepagos.Add(prepago);
				}
			}
			
			return prepagos;	
		}

		public DTO_Validacion set_devolucion_prepago(long prepago_id)
		{
			DTO_Validacion dto_validacion = new DTO_Validacion();




			return dto_validacion;
		}

		public void set_prepago_canje(long prepago_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.prepagos
				SET
					fecha_canje = NOW()
				WHERE
					prepago_id = @prepago_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("prepago_id",prepago_id);

			conector.Update(sql,parametros);
		}

		public bool generar_prepago(DTO_Prepago prepago, List<DTO_Detallado_prepago> detallado_prepago, List<DTO_Detallado_ventas_vista_previa> entrega_parcial = null)
		{
			long prepago_id;

			string sql = @"
				INSERT INTO
					farmacontrol_local.prepagos
				SET
					cliente_id = @cliente_id,
					pago_empleado_id = @pago_empleado_id,
					terminal_id = @terminal_id,
					codigo = @codigo,
					fecha_pago = NOW(),
					monto = @monto,
					comentario = @comentario
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("cliente_id",prepago.cliente_id);
			parametros.Add("pago_empleado_id",prepago.pago_empleado_id);
			parametros.Add("codigo",prepago.codigo);
			parametros.Add("monto",prepago.monto);
			parametros.Add("comentario",prepago.comentario);
			parametros.Add("terminal_id",Misc_helper.get_terminal_id());

			conector.Insert(sql,parametros);

			prepago_id = conector.insert_id;

			foreach(var detallado in detallado_prepago)
			{
				sql = @"
					INSERT INTO
						farmacontrol_local.detallado_prepagos
					SET
						prepago_id = @prepago_id,
						articulo_id = @articulo_id,
						precio_publico = @precio_publico,
						pct_descuento = @pct_descuento,
						importe_descuento = @importe_descuento,
						importe = @importe,
						cantidad = @cantidad,
						cantidad_entregada = @cantidad_entregada,
						subtotal = @subtotal,
						pct_iva = @pct_iva,
						importe_iva = @importe_iva,
						tipo_ieps = @tipo_ieps,
						ieps = @ieps,
						importe_ieps = @importe_ieps,
						total = @total
				";	

				parametros = new Dictionary<string,object>();
				parametros.Add("prepago_id",prepago_id);
				parametros.Add("articulo_id",detallado.articulo_id);
				parametros.Add("precio_publico", detallado.precio_publico);
				parametros.Add("pct_descuento", detallado.pct_descuento);
				parametros.Add("importe_descuento", detallado.importe_descuento);
				parametros.Add("importe", detallado.importe);
				parametros.Add("cantidad", detallado.cantidad);
				parametros.Add("subtotal", detallado.subtotal);
				parametros.Add("pct_iva", detallado.pct_iva);
				parametros.Add("importe_iva", detallado.importe_iva);
				parametros.Add("tipo_ieps", detallado.tipo_ieps);
				parametros.Add("ieps", detallado.ieps);
				parametros.Add("importe_ieps", detallado.importe_ieps);
				parametros.Add("total", detallado.total);

				long cantidad_entregada = 0;

				if (entrega_parcial != null)
				{
					foreach (DTO_Detallado_ventas_vista_previa det in entrega_parcial)
					{
						if(det.articulo_id == detallado.articulo_id)
						{
							cantidad_entregada += det.cantidad;
						}
					}
				}

				parametros.Add("cantidad_entregada",cantidad_entregada);

				conector.Insert(sql,parametros);
			}

			if (entrega_parcial != null)
			{
				foreach (DTO_Detallado_ventas_vista_previa det in entrega_parcial)
				{
					DAO_Apartado_mercancia dao_apartado = new DAO_Apartado_mercancia();
					dao_apartado.agregar_producto_apartado_mercancia(
						Convert.ToInt32(det.articulo_id),
						Misc_helper.CadtoDate(det.caducidad),
						det.lote,
						Convert.ToInt32(det.cantidad),
						"ENTREGA_PARCIAL_PREPAGO",
						null,
						prepago_id
					);
				}
			}

			if (prepago_id > 0)
			{
				Prepago ticket = new Prepago();
				ticket.construccion_ticket(prepago_id);
				ticket.print();

				Prepago_sucursal ticket_sucursal = new Prepago_sucursal();
				ticket_sucursal.construccion_ticket(prepago_id);
				ticket_sucursal.print();

				Prepago_sucursal ticket_oficina = new Prepago_sucursal();
				ticket_oficina.construccion_ticket(prepago_id,"COPIA OFICINA");
				ticket_oficina.print();
			}

			return (prepago_id > 0) ? true : false;
		}

		public DTO_Prepago get_informacion_prepago(string codigo)
		{
			DTO_Prepago prepago = new DTO_Prepago();

			string sql = @"
				SELECT
					prepagos.*,
					(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = prepagos.cancela_empleado_id) AS nombre_empleado_cancela,
					clientes.nombre AS nombre_cliente,
					empleados.nombre AS nombre_empleado
				FROM
					farmacontrol_local.prepagos
				LEFT JOIN farmacontrol_global.clientes USING(cliente_id)
				LEFT JOIN farmacontrol_global.empleados ON
					empleados.empleado_id = prepagos.pago_empleado_id
				WHERE
					codigo = @codigo
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("codigo", codigo);

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				long? nullable = null;
				DateTime? nullable_date = null;

				var row = conector.result_set.Rows[0];

				prepago.prepago_id = Convert.ToInt64(row["prepago_id"]);
				prepago.cliente_id = row["cliente_id"].ToString();
				prepago.pago_empleado_id = Convert.ToInt64(row["pago_empleado_id"]);
				prepago.canje_empleado_id = (row["canje_empleado_id"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["canje_empleado_id"]);
				prepago.cancela_empleado_id = (row["cancela_empleado_id"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["cancela_empleado_id"]);
				prepago.corte_parcial_id = (row["corte_parcial_id"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["corte_parcial_id"]);
				prepago.codigo = row["codigo"].ToString();
				prepago.fecha_pago = Convert.ToDateTime(row["fecha_pago"].ToString());
				prepago.fecha_canje = (row["fecha_canje"].ToString().Equals("")) ? nullable_date : Convert.ToDateTime(row["fecha_canje"].ToString());
				prepago.fecha_cancelado = (row["fecha_cancelado"].ToString().Equals("")) ? nullable_date : Convert.ToDateTime(row["fecha_cancelado"].ToString());
				prepago.monto = Convert.ToDecimal(row["monto"]);
				prepago.tipo_devolucion = row["tipo_devolucion"].ToString();
				prepago.comentario = row["comentario"].ToString();
				prepago.nombre_cliente = row["nombre_cliente"].ToString().ToUpper();
				prepago.nombre_empleado = row["nombre_empleado"].ToString().ToUpper();
				prepago.nombre_empleado_cancela = row["nombre_empleado_cancela"].ToString();
			}

			return prepago;
		}

		public DTO_Prepago get_informacion_prepago(long prepago_id)
		{
			DTO_Prepago prepago = new DTO_Prepago();

			string sql = @"
				SELECT
					*,
					clientes.nombre AS nombre_cliente,
					empleados.nombre AS nombre_empleado
				FROM
					farmacontrol_local.prepagos
				LEFT JOIN farmacontrol_global.clientes USING(cliente_id)
				LEFT JOIN farmacontrol_global.empleados ON
					empleados.empleado_id = prepagos.pago_empleado_id
				WHERE
					prepago_id = @prepago_id
				LIMIT 1
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("prepago_id",prepago_id);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				long? nullable = null;
				DateTime? nullable_date = null;

				var row = conector.result_set.Rows[0];

				prepago.prepago_id = Convert.ToInt64(row["prepago_id"]);
				prepago.cliente_id = row["cliente_id"].ToString();
				prepago.pago_empleado_id = Convert.ToInt64(row["pago_empleado_id"]);
				prepago.canje_empleado_id = (row["canje_empleado_id"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["canje_empleado_id"]) ;
				prepago.cancela_empleado_id = (row["cancela_empleado_id"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["cancela_empleado_id"]);
				prepago.corte_parcial_id = (row["corte_parcial_id"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["corte_parcial_id"]);
				prepago.codigo = row["codigo"].ToString();
				prepago.fecha_pago = Convert.ToDateTime(row["fecha_pago"].ToString());
				prepago.fecha_canje = (row["fecha_canje"].ToString().Equals("")) ? nullable_date : Convert.ToDateTime(row["fecha_canje"].ToString());
				prepago.fecha_cancelado = (row["fecha_cancelado"].ToString().Equals("")) ? nullable_date : Convert.ToDateTime(row["fecha_cancelado"].ToString());
				prepago.monto = Convert.ToDecimal(row["monto"]);
				prepago.tipo_devolucion =  row["tipo_devolucion"].ToString();
				prepago.comentario = row["comentario"].ToString();
				prepago.nombre_cliente = row["nombre_cliente"].ToString().ToUpper();
				prepago.nombre_empleado = row["nombre_empleado"].ToString().ToUpper();
			}

			return prepago;
		}

		public List<DTO_Detallado_prepago> get_detallado_prepago(long prepago_id)
		{
			List<DTO_Detallado_prepago> detallado_prepago = new List<DTO_Detallado_prepago>();

			string sql = @"
				SELECT
					(
						SELECT 
							amecop
						FROM 
							farmacontrol_global.articulos_amecops 
						WHERE 
							articulo_id = detallado_prepagos.articulo_id
						ORDER BY 
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS amecop,
					(
						SELECT 
							amecop
						FROM 
							farmacontrol_global.articulos_amecops 
						WHERE 
							articulo_id = detallado_prepagos.articulo_id
						ORDER BY 
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS amecop_completo,
					articulos.nombre AS producto,
					detallado_prepago_id,
					articulo_id,
					detallado_prepagos.precio_publico AS precio_publico,
					detallado_prepagos.pct_descuento AS pct_descuento,
					importe_descuento,
					importe,
					cantidad,
					cantidad_entregada,
					subtotal,
					detallado_prepagos.pct_iva AS pct_iva,
					importe_iva,
					detallado_prepagos.tipo_ieps AS tipo_ieps,
					detallado_prepagos.ieps AS ieps,
					importe_ieps,
					total
				FROM
					farmacontrol_local.detallado_prepagos
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					prepago_id = @prepago_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("prepago_id",prepago_id);

			conector.Select(sql,parametros);

			foreach(DataRow row in conector.result_set.Rows)
			{
				DTO_Detallado_prepago detallado =  new DTO_Detallado_prepago();
				detallado.amecop_completo = row["amecop_completo"].ToString();

                string var_temp_amecop = row["amecop"].ToString();
                int tam_var = var_temp_amecop.Length;
                String Var_Sub = "*" + var_temp_amecop.Substring((tam_var - 3), 3);
                string amecop_temp = Var_Sub.PadRight(5, ' ');
                detallado.amecop = amecop_temp;

                //detallado.amecop = row["amecop"].ToString();
				detallado.producto = row["producto"].ToString();
				detallado.articulo_id = Convert.ToInt64(row["articulo_id"]);
				detallado.precio_publico = Convert.ToDecimal(row["precio_publico"]);
				detallado.pct_descuento = Convert.ToDecimal(row["pct_descuento"]);
				detallado.importe_descuento = Convert.ToDecimal(row["importe_descuento"]);
				detallado.importe = Convert.ToDecimal(row["importe"]);
				detallado.cantidad = Convert.ToInt32(row["cantidad"]);
				detallado.cantidad_entregada = Convert.ToInt64(row["cantidad_entregada"]);
				detallado.subtotal = Convert.ToDecimal(row["subtotal"]);
				detallado.pct_iva = Convert.ToDecimal(row["pct_iva"]);
				detallado.importe_iva = Convert.ToDecimal(row["importe_iva"]);
				detallado.tipo_ieps = row["tipo_ieps"].ToString();
				detallado.ieps = Convert.ToDecimal(row["ieps"]);
				detallado.importe_ieps = Convert.ToDecimal(row["importe_ieps"]);
				detallado.total = Convert.ToDecimal(row["total"]);

				detallado_prepago.Add(detallado);
			}

			return detallado_prepago;
		}
	}
}
