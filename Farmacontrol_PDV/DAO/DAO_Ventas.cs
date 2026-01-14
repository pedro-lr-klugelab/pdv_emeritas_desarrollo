using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;
using Newtonsoft.Json;
using System.Windows.Forms;
using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.FORMS.comunes;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Ventas
	{
		Conector conector = new Conector();

        public List<DTO_Ventas_pagos> get_pago_tipos_venta(long venta_id)
        {
            List<DTO_Ventas_pagos> tipos_pago = new List<DTO_Ventas_pagos>();

            string sql = @"
                SELECT
                    pago_tipos.pago_tipo_id As pago_tipo_id,
                    pago_tipos.nombre AS nombre,
                    ventas_pagos.cuenta As cuenta,
                    pago_tipos.es_credito AS es_credito,
                    pago_tipos.usa_cuenta AS usa_cuenta
                FROM
                    farmacontrol_local.ventas_pagos
                JOIN farmacontrol_global.pago_tipos USING(pago_tipo_id)
                WHERE
                    venta_id = @venta_id
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("venta_id",venta_id);

            conector.Select(sql, parametros);

            if(conector.result_set.Rows.Count > 0)
            {
                foreach(DataRow row in conector.result_set.Rows)
                {
                    DTO_Ventas_pagos pago = new DTO_Ventas_pagos();
                    pago.pago_tipo_id = Convert.ToInt64(row["pago_tipo_id"]);
                    pago.nombre = row["nombre"].ToString();
                    pago.cuenta = row["cuenta"].ToString();
                    pago.es_credito = Convert.ToBoolean(row["es_credito"]);
                    pago.usa_cuenta = Convert.ToBoolean(row["usa_cuenta"]);

                    tipos_pago.Add(pago);
                }
            }

            return tipos_pago;
        }

        public List<DTO_Ventas_facturadas> get_ventas_facturadas_corte_parcial(long corte_parcial_id)
        {
            List<DTO_Ventas_facturadas> lista = new List<DTO_Ventas_facturadas>();

            string sql = @"
                SELECT
                    ventas.venta_id,
                    venta_folio,
                    SUM(detallado_ventas.total) AS importe,
                    tmp_ventas_pagos.metodo_pago AS metodo_pago
                FROM
                    farmacontrol_local.detallado_ventas
                JOIN farmacontrol_local.ventas USING(venta_id)
                JOIN (
                    SELECT
                        ventas.venta_id,
                        COUNT(ventas_pagos.pago_tipo_id),
                        IF(COUNT(ventas_pagos.pago_tipo_id) > 1, 'VARIOS', pago_tipos.nombre) AS metodo_pago
                    FROM
                        farmacontrol_local.ventas
                    JOIN farmacontrol_local.ventas_pagos USING(venta_id)
                    JOIN farmacontrol_global.pago_tipos USING(pago_tipo_id)
                    WHERE
                        ventas.fecha_facturado IS NOT NULL
                    AND
                        ventas.corte_parcial_id = @corte_parcial_id
                    GROUP BY ventas.venta_id
                ) AS tmp_ventas_pagos USING(venta_id)
                WHERE
                    ventas.fecha_facturado IS NOT NULL
                AND
                    ventas.corte_parcial_id = @corte_parcial_id
                GROUP BY ventas.venta_id
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("corte_parcial_id",corte_parcial_id);

            conector.Select(sql,parametros);

            if(conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {
                    DTO_Ventas_facturadas tmp = new DTO_Ventas_facturadas();
                    tmp.folio = Convert.ToInt64(row["venta_folio"]);
                    tmp.importe = Convert.ToDecimal(row["importe"]);
                    tmp.metodo_pago = row["metodo_pago"].ToString();

                    lista.Add(tmp);
                }
            }

            return lista;
        }

        public long get_venta_id_por_venta_folio(long venta_folio)
        {
            long venta_id;

            string sql = @"
                SELECT 
                    venta_id from farmacontrol_local.ventas 
                WHERE 
                    venta_folio = @venta_folio
                LIMIT 1
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("venta_folio", venta_folio);

            conector.Select(sql, parametros);

            venta_id = Convert.ToInt64(conector.result_set.Rows[0]["venta_id"]);

            return venta_id;
        }

        public List<DTO_Formas_pago> get_tipos_pagos_venta(long venta_id)
        {
            List<DTO_Formas_pago> lista_pagos = new List<DTO_Formas_pago>();

            string sql = @"
                SELECT
                    pago_tipos.nombre AS nombre,
                    cuenta,
                    importe,
                    monto,
                    pago_tipos.entrega_cambio AS entrega_cambio
                FROM
                    farmacontrol_local.ventas_pagos
                JOIN farmacontrol_global.pago_tipos USING(pago_tipo_id)
                WHERE
                    ventas_pagos.venta_id = @venta_id
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("venta_id",venta_id);

            conector.Select(sql,parametros);

            if(conector.result_set.Rows.Count > 0)
            {
                foreach(DataRow row in conector.result_set.Rows)
                {
                    DTO_Formas_pago tmp = new DTO_Formas_pago();
                    tmp.nombre = row["nombre"].ToString();
                    tmp.cuenta = row["cuenta"].ToString();

                    string[] tmp_cuenta = tmp.cuenta.Split(new char[] { '$' });

                    if(tmp_cuenta.Length == 2)
                    {
                        tmp.cuenta = tmp_cuenta[1].ToUpper();
                    }

                    tmp.importe = Convert.ToDecimal(row["importe"]);
                    tmp.monto = Convert.ToDecimal(row["monto"]);
                    tmp.entrega_cambio = Convert.ToBoolean(row["entrega_cambio"]);

                    lista_pagos.Add(tmp);
                }
            }

            return lista_pagos;
        }

        public List<Tuple<string,decimal>> get_pago_tipos_ventas_envios(long venta_envio_folio)
        {
            string sql = @"
                SELECT
                    pago_tipos.nombre AS nombre,
                    SUM(monto) AS total
                FROM
                    farmacontrol_local.ventas_pagos
                JOIN farmacontrol_global.pago_tipos USING(pago_tipo_id)
                JOIN farmacontrol_local.ventas_envios USING(venta_id)
                WHERE
                    ventas_envios.venta_envio_folio = @venta_envio_folio
                GROUP BY venta_envio_folio, pago_tipos.pago_tipo_id
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("venta_envio_folio",venta_envio_folio);

            conector.Select(sql, parametros);

            List<Tuple<string, decimal>> tipos_pago = new List<Tuple<string, decimal>>();

            foreach(DataRow row in conector.result_set.Rows)
            {
                Tuple<string, decimal> tmp = new Tuple<string, decimal>(
                    row["nombre"].ToString(),
                    Convert.ToDecimal(row["total"])
                );

                tipos_pago.Add(tmp);
            }

            return tipos_pago;
        }

        public DataTable get_ventas_envios_saldar()
        {
            string sql = @"
                SELECT
                    venta_envio_folio,
                    fecha_envio,
                    empleados.nombre AS diligenciero,
                    SUM(tmp_totales.importe) AS importe,
                    SUM(tmp_totales.total) AS total
                FROM
                    farmacontrol_local.ventas_envios
                JOIN
                (
                    SELECT 
                        ventas_pagos.venta_id AS venta_id,
                        SUM(ventas_pagos.importe) AS importe,
                        SUM(ventas_pagos.monto) AS total
                    FROM 
                        farmacontrol_local.ventas_pagos 
                    JOIN farmacontrol_local.ventas_envios USING(venta_id)
                    WHERE 
                        ventas_envios.fecha_retorno IS NULL
                    GROUP BY ventas_pagos.venta_id
                ) AS tmp_totales USING(venta_id)
                JOIN farmacontrol_global.empleados USING(empleado_id)
                WHERE
                    fecha_retorno IS NULL
                GROUP BY
                    venta_envio_folio
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            conector.Select(sql, parametros);

            return conector.result_set;
        }

        public List<DTO_Envio_ventas_detallado> get_ventas_envios(long venta_envio_folio)
        {
            string sql = @"
                SELECT
	                terminales.nombre AS caja,
	                ventas.venta_folio AS folio,
	                ventas.fecha_terminado AS fecha_terminado,
	                ventas_envios.fecha_envio AS fecha_envio,
                    ventas.cliente_domicilio_id,
                    clientes.nombre,
	                COUNT(DISTINCT(detallado_ventas.articulo_id)) AS articulos,
	                SUM(detallado_ventas.cantidad) AS piezas,
	                SUM(detallado_ventas.total) AS importe,
	                (
		                SELECT
			                SUM(ventas_pagos.monto) AS total
		                FROM
			                farmacontrol_local.ventas_pagos
		                WHERE
			                ventas_pagos.venta_id = ventas.venta_id
		                GROUP BY ventas_pagos.venta_id
		                LIMIT 1
	                ) AS total,
	                empleados.nombre AS empleado
                FROM
	                farmacontrol_local.ventas_envios
                JOIN farmacontrol_local.ventas USING(venta_id)
                JOIN farmacontrol_local.detallado_ventas USING(venta_id)
                JOIN farmacontrol_local.terminales USING(terminal_id)
                JOIN farmacontrol_global.empleados ON empleados.empleado_id = ventas_envios.empleado_id
                LEFT JOIN farmacontrol_global.clientes_domicilios USING(cliente_domicilio_id)
                LEFT JOIN farmacontrol_global.clientes ON clientes.cliente_id = clientes_domicilios.cliente_id
                WHERE
                    ventas_envios.venta_envio_folio = @venta_envio_folio
                GROUP BY ventas_envios.venta_id
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("venta_envio_folio",venta_envio_folio);

            conector.Select(sql, parametros);

            List<DTO_Envio_ventas_detallado> lista = new List<DTO_Envio_ventas_detallado>();

            if(conector.result_set.Rows.Count > 0)
            {
                foreach(DataRow row in conector.result_set.Rows)
                {
                    DTO_Envio_ventas_detallado tmp = new DTO_Envio_ventas_detallado();
                    tmp.empleado = row["empleado"].ToString();
                    tmp.folio = Convert.ToInt64(row["folio"]);
                    tmp.caja = row["caja"].ToString();
                    tmp.fecha_terminado = Convert.ToDateTime(row["fecha_terminado"]);
                    tmp.fecha_envio = Convert.ToDateTime(row["fecha_envio"]);
                    tmp.articulos = Convert.ToInt64(row["articulos"]);
                    tmp.piezas = Convert.ToInt64(row["piezas"]);
                    tmp.importe = Convert.ToDecimal(row["importe"]);
                    tmp.total = Convert.ToDecimal(row["total"]);
                    tmp.cliente_domicilio_id = row["cliente_domicilio_id"].ToString();
                    tmp.nombre_cliente = row["nombre"].ToString();

                    lista.Add(tmp);
                }
            }

            return lista;
        }

        public long saldar_ventas_envios(long venta_envio_folio)
        {
            string sql = @"
                UPDATE
                    farmacontrol_local.ventas_envios
                SET
                    fecha_retorno = NOW()
                WHERE
                    venta_envio_folio = @venta_envio_folio
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("venta_envio_folio",venta_envio_folio);

            conector.Update(sql, parametros);

            return conector.filas_afectadas;
        }

        public long registrar_ventas_envios(long empleado_id, List<long> venta_ids)
        {
            string sql = @"
                SELECT
                    COALESCE(MAX(venta_envio_folio), 0) + 1 AS folio
                FROM
                    farmacontrol_local.ventas_envios
                LIMIT 1
            ";

            Dictionary<string,object> parametros = new Dictionary<string, object>();

            conector.Select(sql, parametros);

            if(conector.result_set.Rows.Count > 0)
            {
                long venta_envio_folio = Convert.ToInt64(conector.result_set.Rows[0]["folio"]);

                string ventas = string.Join(",", venta_ids.Select(x => x.ToString()).ToArray());

                sql = string.Format(@"
                    INSERT INTO
                        farmacontrol_local.ventas_envios
                    (
                        SELECT
                            0 AS venta_envio_id,
                            @venta_envio_folio AS venta_envio_folio,
                            ventas.venta_id AS venta_id,
                            @empleado_id AS empleado_id,
                            NOW() AS fecha_envio,
                            NULL AS fecha_retorno
                        FROM
                            farmacontrol_local.ventas
                        WHERE
                            ventas.venta_id IN({0})
                    )
                ", ventas);

                parametros = new Dictionary<string, object>();
                parametros.Add("venta_envio_folio", venta_envio_folio);
                parametros.Add("empleado_id",empleado_id);

                conector.Insert(sql, parametros);

                if(conector.filas_afectadas > 0)
                {
                    return venta_envio_folio;
                }
            }

            return 0;
        }

        public DataTable get_ventas_domicilio_enviar()
        {
            string sql = @"
                SELECT
                    ventas.venta_id AS venta_id,
                    terminales.nombre AS caja,
                    ventas.venta_folio  AS venta_folio,
                    empleados.nombre AS empleado,
                    clientes.nombre AS cliente,
                    SUM(detallado_ventas.total) AS total
                FROM
                    farmacontrol_local.detallado_ventas
                JOIN farmacontrol_local.ventas USING(venta_id)
                LEFT JOIN farmacontrol_global.clientes_domicilios USING(cliente_domicilio_id)
                LEFT JOIN farmacontrol_global.clientes ON clientes.cliente_id = clientes_domicilios.cliente_id
                JOIN farmacontrol_local.terminales USING(terminal_id)
                JOIN farmacontrol_global.empleados ON empleados.empleado_id = ventas.empleado_id
                LEFT JOIN farmacontrol_local.ventas_envios USING(venta_id)
                WHERE
                    ventas_envios.venta_id IS NULL
                AND
                    ventas.cliente_domicilio_id IS NOT NULL
                AND
                    DATE(ventas.fecha_terminado) = CURDATE()
                GROUP BY ventas.venta_id
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            conector.Select(sql, parametros);

            return conector.result_set;
        }

        public List<DTO_Reporte_venta> get_reporte_ventas(string fecha)
        {
            List<DTO_Reporte_venta> lista = new List<DTO_Reporte_venta>();

            string sql = @"
                SELECT
                    venta_id,
                    venta_folio,
                    terminales.nombre AS terminal,
                    empleados.nombre AS empleado,
                    COUNT(DISTINCT(detallado_ventas.articulo_id)) AS articulos,
                    ventas.fecha_terminado AS fecha,
                    SUM(detallado_ventas.cantidad) AS piezas,
                    SUM(detallado_ventas.total) AS total
                FROM
                    farmacontrol_local.ventas
                JOIN farmacontrol_local.detallado_ventas USING(venta_id)
                JOIN farmacontrol_local.terminales USING(terminal_id)
                JOIN farmacontrol_global.empleados USING(empleado_id)
                WHERE
                    DATE(ventas.fecha_terminado) = DATE(@fecha)
                GROUP BY ventas.venta_id
                ORDER BY ventas.fecha_terminado DESC
            ";

            Dictionary<string, object> parameros = new Dictionary<string, object>();
            parameros.Add("fecha",fecha);

            conector.Select(sql, parameros);

            foreach(DataRow row in conector.result_set.Rows)
            {
                DTO_Reporte_venta tmp = new DTO_Reporte_venta();
                tmp.venta_id = Convert.ToInt64(row["venta_id"]);
                tmp.venta_folio = Convert.ToInt64(row["venta_folio"]);
                tmp.terminal = row["terminal"].ToString();
                tmp.empleado = row["empleado"].ToString();
                tmp.fecha = Convert.ToDateTime(row["fecha"]).ToString("dd-MMM-yy H:mm:ss").Replace(".","").ToUpper();
                tmp.articulos = Convert.ToInt64(row["articulos"]);
                tmp.piezas = Convert.ToInt64(row["piezas"]);
                tmp.total = Convert.ToDecimal(row["total"]);

                lista.Add(tmp);
            }

            return lista;
        }

        public void set_comentario(long venta_id, string comentarios)
        {
            string sql = @"
                UPDATE
                    farmacontrol_local.ventas
                SET
                    comentarios = @comentarios
                WHERE
                    venta_id = @venta_id
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("comentarios",comentarios);
            parametros.Add("venta_id",venta_id);

            conector.Update(sql, parametros);
        }

		private int? terminal_id = HELPERS.Misc_helper.get_terminal_id();

		public bool actualizar_empleado_venta(long empleado_id, long venta_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.ventas
				SET
					empleado_id = @empleado_id
				WHERE
					venta_id = @venta_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("empleado_id",empleado_id);
			parametros.Add("venta_id",venta_id);

			conector.Update(sql,parametros);

			return (conector.filas_afectadas > 0);
		}

		public bool get_existe_venta_corte(long venta_id)
		{
			string sql = @"
				SELECT
					corte_total_id
				FROM
					farmacontrol_local.ventas
				WHERE
					venta_id = @venta_id
				AND
					corte_total_id IS NOT NULL
                AND 
                    fecha_facturado IS NULL
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

		public void venta_formula_magistral(long venta_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_global.formulas
				SET
					venta_id = @venta_id
				WHERE
					formula_id IN (
						SELECT
							lote
						FROM
							farmacontrol_local.detallado_ventas
						WHERE
							articulo_id = @articulo_id
						AND
							venta_id = @venta_id
					)
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("articulo_id",Convert.ToInt64(Config_helper.get_config_global("formula_magistral_articulo_id")));
			parametros.Add("venta_id",venta_id);

			conector.Update(sql,parametros);
		}

        public bool existe_nota_credito(long venta_id) {
            string sql = @"
                    SELECT 
                        count(*) as existe 
                    FROM 
                        farmacontrol_local.notas_credito 
                    WHERE 
                        elemento_id = @venta_id 
                    AND 
                        tipo_padre = 'VENTA'";
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("venta_id", venta_id);

            conector.Select(sql, parametros);

            if (Convert.ToInt64(conector.result_set.Rows[0]["existe"]) > 0)
            {
                return true;
            }

            return false;
        }

		public bool existe_venta(long venta_id)
		{
			string sql = @"
				SELECT
					venta_id
				FROM
					farmacontrol_local.ventas
				WHERE
					venta_id = @venta_id
				AND
					fecha_terminado IS NOT NULL
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

		public DTO_Validacion get_venta_pendiente_corte()
		{
			DTO_Validacion val = new DTO_Validacion();

			string sql = @"
				SELECT
					COALESCE(COUNT(detallado_venta_id),0) AS total_detallado
				FROM
					farmacontrol_local.ventas
				LEFT JOIN farmacontrol_local.detallado_ventas USING(venta_id)
				WHERE
					ventas.fecha_terminado IS NULL
				AND
					terminal_id = @terminal_id
				GROUP BY
					ventas.venta_id
				LIMIT 1
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("terminal_id",terminal_id);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				val.status = false;

				if(Convert.ToInt64(conector.result_set.Rows[0]["total_detallado"]) > 0)
				{
					val.informacion = "No se puede hacer el corte de caja debido a que hay una venta por cerrar";
				}
				else
				{
					val.informacion = "CIERRE";
				}
			}
			else
			{
				val.status = true;
			}

			return val;
		}

		/*
		 * METODOS REST
		 */

		public static List<DTO_Desplazamientos_item> get_desplazamientos(string articulo_ids, string fecha_inicial, string fecha_final, long sucursal_id)
		{
			Rest_parameters parameters = new Rest_parameters();  
			parameters.Add("articulo_ids", articulo_ids);
			parameters.Add("fecha_inicial", fecha_inicial);
			parameters.Add("fecha_final", fecha_final);
			parameters.Add("sucursal_id", sucursal_id);

			List<DTO_Desplazamientos_item> desplazamientos = Rest_helper.make_request<List<DTO_Desplazamientos_item>>("reportes/get_desplazamientos_pdv", parameters);

			return desplazamientos;
		}

		public static List<DTO_Desplazamientos_item> get_desplazamientos(long articulo_ids, string fecha_inicial, string fecha_final, long sucursal_id)
		{
			Rest_parameters parameters = new Rest_parameters();
			parameters.Add("articulo_ids", articulo_ids);
			parameters.Add("fecha_inicial", fecha_inicial);
			parameters.Add("fecha_final", fecha_final);
			parameters.Add("sucursal_id",sucursal_id);

			List<DTO_Desplazamientos_item> desplazamientos = Rest_helper.make_request<List<DTO_Desplazamientos_item>>("reportes/get_desplazamientos_pdv", parameters);

			return desplazamientos;
		}

		 /*
		  * FIN REST
		  */
		
		public bool es_venta_facturada(long venta_id)
		{
			string sql = @"
				SELECT
					venta_id
				FROM
					farmacontrol_local.detallado_facturas
				WHERE
					venta_id = @venta_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("venta_id",venta_id);

			conector.Select(sql,parametros);

			return (conector.result_set.Rows.Count > 0) ? true : false;
		}

		public bool eliminar_venta(long venta_id)
		{
			string sql = @"
				DELETE FROM
					farmacontrol_local.detallado_ventas
				WHERE
					venta_id = @venta_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("venta_id", venta_id);

			conector.Delete(sql, parametros);		

			sql = @"
				DELETE FROM 
					farmacontrol_local.ventas
				WHERE
					venta_id = @venta_id
			";

			conector.Delete(sql, parametros);

			if(conector.filas_afectadas > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public void importar_informacion_venta(long venta_id_origen, long venta_id_destino)
		{
			string sql = @"
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
					ventas.venta_id = @venta_id_destino
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("venta_id_origen",venta_id_origen);
			parametros.Add("venta_id_destino",venta_id_destino);

			conector.Select(sql,parametros);

			sql = @"
				INSERT INTO
					farmacontrol_local.detallado_ventas
				(
					SELECT
						0 AS detallado_venta_id,
						@venta_id_destino AS venta_id,
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

			conector.Insert(sql,parametros);
		}

		public long canjear_prepago(long empleado_id, long venta_id_origen, List<DTO_Detallado_ventas_vista_previa> detallado, decimal monto, string cliente_domicilio_id)
		{
			string sql = @"
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

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("empleado_id", empleado_id);
			parametros.Add("terminal_id", terminal_id);

			conector.Insert(sql, parametros);

			long venta_id_destino = conector.insert_id;

			if(venta_id_destino > 0)
			{
				importar_informacion_venta(venta_id_origen,venta_id_destino);
				limpiar_venta(venta_id_origen);

				foreach(DTO_Detallado_ventas_vista_previa producto in detallado)
				{
					insertar_detallado(producto.amecop,Misc_helper.CadtoDate(producto.caducidad),producto.lote,Convert.ToInt32(producto.cantidad),venta_id_origen);
				}
			}

			if(!cliente_domicilio_id.Equals(""))
			{
				sql = @"
					UPDATE
						farmacontrol_local.ventas
					SET
						cliente_domicilio_id = @cliente_domicilio_id
					WHERE
						venta_id = @venta_id
				";

				parametros = new Dictionary<string,object>();
				parametros.Add("venta_id",venta_id_origen);
				parametros.Add("cliente_domicilio_id",cliente_domicilio_id);

				conector.Update(sql,parametros);
			}

			DataTable pago_tipos = new DataTable();
			pago_tipos.Columns.Add("metodo_pago", typeof(string));
			pago_tipos.Columns.Add("cuenta", typeof(string));
            pago_tipos.Columns.Add("importe", typeof(string));
			pago_tipos.Columns.Add("monto", typeof(string));

			DAO_Pago_tipos dao_pag = new DAO_Pago_tipos();
			var tipos_pago = dao_pag.get_pago_tipos(null,false);

			foreach(var item in tipos_pago)
			{
				if(item.es_prepago)
				{
                    pago_tipos.Rows.Add(item.nombre, "", monto, monto);
					break;
				}
			}

			terminar_venta(venta_id_origen,pago_tipos);

			return venta_id_destino;
		}

		public void registrar_cupon(long venta_id)
		{
			string sql = @"
				SELECT
					IF(cupon_id IS NULL,0,cupon_id) AS cupon_id
				FROM
					farmacontrol_local.ventas
				WHERE
					venta_id = @venta_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("venta_id",venta_id);

			conector.Select(sql,parametros);

			long cupon_id = Convert.ToInt64(conector.result_set.Rows[0]["cupon_id"]);

			parametros.Add("cupon_id",cupon_id);

			if(cupon_id > 0)
			{
				sql = @"
					UPDATE
						farmacontrol_global.cupones
					SET
						canje_venta_id = @venta_id,
                        fecha_canjeado = @fecha_canjeado
					WHERE
						cupon_id = @cupon_id
				";

                parametros.Add("fecha_canjeado",Misc_helper.fecha());
				conector.Update(sql,parametros);

				string data = JsonConvert.SerializeObject(parametros).ToString();
                DAO_Ventas dao_ventas = new DAO_Ventas();
                var venta_data = dao_ventas.get_venta_data(venta_id);

                DAO_Cola_operaciones.insertar_cola_operaciones((long)venta_data.empleado_id, "rest/cupones", "registrar_venta_cupon", parametros, "PARA ENVIO AL SERVIDOR PRINCIPAL");
			}	
		}

		public DTO_Validacion desasociar_cupon(long cupon_id, long venta_id)
		{
			DTO_Validacion val = new DTO_Validacion();

			string sql = @"
				UPDATE
					farmacontrol_local.ventas
				SET
					cupon_id = NULL
				WHERE
					venta_id = @venta_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("cupon_id", cupon_id);
			parametros.Add("venta_id", venta_id);

			conector.Update(sql, parametros);

			if (conector.filas_afectadas > 0)
			{
				sql = @"
					UPDATE
						farmacontrol_local.detallado_ventas
					LEFT JOIN
					(
						SELECT
							detallado_ventas.detallado_venta_id,
							articulos.pct_descuento AS pct_descuento,
							(articulos.precio_publico * articulos.pct_descuento) AS importe_descuento,
							articulos.precio_publico - (articulos.precio_publico * articulos.pct_descuento) AS importe,
							(articulos.precio_publico - (articulos.precio_publico * articulos.pct_descuento)) * detallado_ventas.cantidad AS subtotal,
							((articulos.precio_publico - (articulos.precio_publico * articulos.pct_descuento)) * detallado_ventas.cantidad) * articulos.pct_iva AS importe_iva,
							IF(articulos.tipo_ieps = 'PCT', ((articulos.precio_publico - (articulos.precio_publico * articulos.pct_descuento)) * articulos.ieps) , articulos.ieps) AS importe_ieps,
							((articulos.precio_publico - (articulos.precio_publico * articulos.pct_descuento)) * detallado_ventas.cantidad) + (((articulos.precio_publico - (articulos.precio_publico * articulos.pct_descuento)) * detallado_ventas.cantidad) * articulos.pct_iva) AS total
						FROM
							farmacontrol_local.detallado_ventas
						LEFT JOIN farmacontrol_global.detallado_cupones USING(articulo_id)
						LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
						WHERE
							detallado_ventas.venta_id = @venta_id
						AND
							detallado_cupones.cupon_id = @cupon_id
						AND
							detallado_ventas.es_promocion IS FALSE
					) AS tmp_detallado USING(detallado_venta_id)
					SET
						detallado_ventas.pct_descuento = tmp_detallado.pct_descuento,
						detallado_ventas.importe_descuento = tmp_detallado.importe_descuento,
						detallado_ventas.importe = tmp_detallado.importe,
						detallado_ventas.subtotal = tmp_detallado.subtotal,
						detallado_ventas.importe_iva = tmp_detallado.importe_iva,
						detallado_ventas.importe_ieps = tmp_detallado.importe_ieps,
						detallado_ventas.total = tmp_detallado.total
					WHERE
						detallado_venta_id = tmp_detallado.detallado_venta_id
				";

				conector.Update(sql, parametros);

				if (conector.filas_afectadas > 0)
				{
					val.status = true;
					val.informacion = "Cupón desasociado correctamente";
				}
				else
				{
					val.status = false;
					val.informacion = "No se afecto ningun producto de esta venta, por favor notifique a su administrador";
				}
			}
			else
			{
				val.status = false;
				val.informacion = "Ocurrio un error al intentar desasociar el cupon a la venta, notifique a su administrador";
			}

			return val;
		}

		public DTO_Validacion asignar_cupon(long cupon_id, long venta_id)
		{
			DTO_Validacion val = new DTO_Validacion();

			string sql = @"
				UPDATE
					farmacontrol_local.ventas
				SET
					cupon_id = @cupon_id
				WHERE
					venta_id = @venta_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("cupon_id",cupon_id);
			parametros.Add("venta_id",venta_id);

			conector.Update(sql,parametros);

			if(conector.filas_afectadas > 0)
			{
				sql = @"
					UPDATE
						farmacontrol_local.detallado_ventas
					LEFT JOIN
					(
						SELECT
							detallado_ventas.detallado_venta_id,
							detallado_cupones.pct_descuento AS pct_descuento,
							(articulos.precio_publico * detallado_cupones.pct_descuento) AS importe_descuento,
							articulos.precio_publico - (articulos.precio_publico * detallado_cupones.pct_descuento) AS importe,
							(articulos.precio_publico - (articulos.precio_publico * detallado_cupones.pct_descuento)) * detallado_ventas.cantidad AS subtotal,
							((articulos.precio_publico - (articulos.precio_publico * detallado_cupones.pct_descuento)) * detallado_ventas.cantidad) * articulos.pct_iva AS importe_iva,
							IF(articulos.tipo_ieps = 'PCT', ((articulos.precio_publico - (articulos.precio_publico * detallado_cupones.pct_descuento)) * articulos.ieps) , articulos.ieps) AS importe_ieps,
							((articulos.precio_publico - (articulos.precio_publico * detallado_cupones.pct_descuento)) * detallado_ventas.cantidad) + (((articulos.precio_publico - (articulos.precio_publico * detallado_cupones.pct_descuento)) * detallado_ventas.cantidad) * articulos.pct_iva) AS total
						FROM
							farmacontrol_local.detallado_ventas
						LEFT JOIN farmacontrol_global.detallado_cupones USING(articulo_id)
						LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
						WHERE
							detallado_ventas.venta_id = @venta_id
						AND
							detallado_cupones.cupon_id = @cupon_id
						AND
							detallado_ventas.es_promocion IS FALSE
					) AS tmp_detallado USING(detallado_venta_id)
					SET
						detallado_ventas.pct_descuento = tmp_detallado.pct_descuento,
						detallado_ventas.importe_descuento = tmp_detallado.importe_descuento,
						detallado_ventas.importe = tmp_detallado.importe,
						detallado_ventas.subtotal = tmp_detallado.subtotal,
						detallado_ventas.importe_iva = tmp_detallado.importe_iva,
						detallado_ventas.importe_ieps = tmp_detallado.importe_ieps,
						detallado_ventas.total = tmp_detallado.total
					WHERE
						detallado_venta_id = tmp_detallado.detallado_venta_id
				";

				conector.Update(sql,parametros);

				if(conector.filas_afectadas > 0)
				{
					val.status = true;
					val.informacion = "Cupón aplicado correctamente";
				}
				else
				{
					val.status = false;
					val.informacion = "No se afecto ningun producto de esta venta, por favor notifique a su administrador";
				}
			}
			else
			{
				val.status = false;
				val.informacion = "Ocurrio un error al intentar asignar el cupon a la venta, notifique a su administrador";
			}

			return val;
		}

		public DTO_Validacion generar_venta_complemento(long venta_id_padre, List<Tuple<int,string,string,int>> lista_productos)
		{
			DTO_Validacion validacion = new DTO_Validacion();
			validacion.status = false;
			validacion.informacion = "Ocurrio un error al intentar generar la nueva venta de la devolucion, por favor notifique a su administrador";

			string sql = @"
				SELECT
					cancelacion_id
				FROM
					farmacontrol_local.cancelaciones
				WHERE
					venta_id = @venta_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("venta_id",venta_id_padre);

			conector.Select(sql,parametros);

			var result = conector.result_set;

			if(result.Rows.Count > 0)
			{
				conector.TransactionStart();

				sql = @"
					INSERT INTO
						farmacontrol_local.ventas
					SET
						empleado_id = @empleado_id,
						fecha_creado = NOW(),
						terminal_id = @terminal_id
				";

				parametros = new Dictionary<string, object>();
				parametros.Add("empleado_id", FORMS.comunes.Principal.empleado_id);
				parametros.Add("terminal_id", terminal_id);

				conector.Insert(sql, parametros);

				foreach(var detallado in lista_productos)
				{
					sql = @"
					INSERT INTO
						farmacontrol_local.detallado_ventas
					(
						SELECT
							0 AS detallado_venta_id,
							LAST_INSERT_ID() AS venta_id,
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
						LIMIT 1
					)
				";
				
				parametros = new Dictionary<string,object>();
				parametros.Add("venta_id",venta_id_padre);
				parametros.Add("articulo_id",detallado.Item1);
				parametros.Add("caducidad", detallado.Item2);
				parametros.Add("lote", detallado.Item3);
				parametros.Add("cantidad", detallado.Item4);

				conector.Insert(sql,parametros);
					
				}

				if(conector.TransactionCommit())
				{
					sql= @"
						SELECT
							venta_id
						FROM
							farmacontrol_local.detallado_ventas
						WHERE
							detallado_venta_id = LAST_INSERT_ID()
					";

					conector.Select(sql);

					sql = @"
						UPDATE
							farmacontrol_local.cancelaciones
						SET
							nueva_venta_id = LAST_INSERT_ID()
					";

					validacion.status = true;
					validacion.informacion = "Nueva venta generada correctamente";
				}

				return validacion;
			}
			else
			{
				return validacion;
			}

		}

		public DataTable get_lotes(int articulo_id, string caducidad, long venta_id)
		{
			DataTable result = new DataTable();

			try
			{
				string sql = @"
					SELECT
						lote
					FROM
						farmacontrol_local.detallado_ventas
					WHERE
						articulo_id = @articulo_id
					AND
						caducidad = @caducidad
					AND
						venta_id = @venta_id
				";

				Dictionary<string, object> parametros = new Dictionary<string, object>();
				parametros.Add("articulo_id", articulo_id);
				parametros.Add("caducidad", caducidad);
				parametros.Add("venta_id",venta_id);

				conector.Select(sql, parametros);

				result = conector.result_set;
			}
			catch (Exception excepcion)
			{
				Log_error.log(excepcion);
			}

			return result;
		}

		public DataTable get_caducidades(string amecop, long venta_id)
		{
			DataTable result = new DataTable();

			try
			{
				string sql = @"
					SELECT
						DATE_FORMAT(caducidad,'%Y-%m-%d %H:%i:%s') AS caducidad,
						detallado_ventas.articulo_id
					FROM
						farmacontrol_local.detallado_ventas
					LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
					WHERE
						articulos.articulo_id = (
							SELECT
								articulos_amecops.articulo_id
							FROM
								farmacontrol_global.articulos_amecops
							WHERE
								articulos_amecops.amecop = @amecop
							LIMIT 1
						)
					AND
						detallado_ventas.venta_id = @venta_id
				";

				Dictionary<string, object> parametros = new Dictionary<string, object>();
				parametros.Add("amecop", amecop);
				parametros.Add("venta_id",venta_id);

				conector.Select(sql, parametros);

				result = conector.result_set;
			}
			catch (Exception excepcion)
			{
				Log_error.log(excepcion);
			}

			return result;
		}

		public DataTable exite_producto_venta(int articulo_id, string caducidad, string lote, long venta_id)
		{
			string sql = @"
				SELECT
					articulos.articulo_id,
					(
						SELECT
							ABS(amecop) AS amecop
						FROM
							farmacontrol_global.articulos_amecops
						WHERE
							articulo_id = detallado_ventas.articulo_id
						ORDER BY amecop_principal DESC
						LIMIT 1
					) AS amecop,
					articulos.nombre,
					DATE_FORMAT(caducidad,'%Y-%m-%d %H:%i:%s') AS caducidad,
					lote,
					importe,
					detallado_ventas.cantidad,
					subtotal,
					detallado_ventas.pct_iva,
					importe_iva,
                    importe_ieps,
					total,
					es_promocion
				FROM
					farmacontrol_local.detallado_ventas
				LEFT JOIN articulos USING(articulo_id)
				WHERE
					venta_id = @venta_id
				AND
					detallado_ventas.articulo_id = @articulo_id
				AND
					caducidad = @caducidad
				AND
					lote = @lote
				GROUP BY
					detallado_ventas.articulo_id, detallado_ventas.caducidad, detallado_ventas.lote
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();

			parametros.Add("venta_id", venta_id);
			parametros.Add("articulo_id", articulo_id);
			parametros.Add("caducidad",caducidad);
			parametros.Add("lote",lote);

			conector.Select(sql, parametros);

			return conector.result_set;
		}

		public DataTable exite_producto_venta(int articulo_id, long venta_id)
		{
			string sql = @"
				SELECT
					articulos.articulo_id,
					IF((COUNT(articulos_amecops.amecop)) > 1, CONCAT_WS(' ',(ABS(articulos_amecops.amecop)), ' *'), ABS(articulos_amecops.amecop)) AS amecop,
					nombre,
					DATE_FORMAT(caducidad,'%Y-%m-%d %H:%i:%s') AS caducidad,
					lote,
					importe,
					detallado_ventas.cantidad,
					subtotal,
					detallado_ventas.pct_iva,
					importe_iva,
                    importe_ieps,
					total,
					es_promocion
				FROM
					farmacontrol_local.detallado_ventas
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id)
				WHERE
					venta_id = @venta_id
				AND
					detallado_ventas.articulo_id = @articulo_id
				GROUP BY
					detallado_ventas.articulo_id, detallado_ventas.caducidad, detallado_ventas.lote, detallado_ventas.importe
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();

			parametros.Add("venta_id",venta_id);
			parametros.Add("articulo_id",articulo_id);

			conector.Select(sql,parametros);

			return conector.result_set;
		}

		public void registrar_venta_credito(long venta_id, string cliente_id)
		{
			string cliente_credito_id = HELPERS.Misc_helper.uuid();
			int sucursal_id = Convert.ToInt32(HELPERS.Config_helper.get_config_local("sucursal_id"));
			
            string sql = @"
                SELECT
					@cliente_credito_id AS cliente_credito_id,
					@cliente_id AS cliente_id,
					@sucursal_id AS sucursal_id,
					venta_id,
					terminales.nombre AS terminal,
					ventas.venta_folio AS venta_folio,
					CAST(fecha_terminado AS CHAR(19)) AS fecha_venta,
					NULL AS fecha_saldado,
					NULL AS fecha_cancelado,
					SUM(detallado_ventas.total) AS total,
					NOW() AS modified
				FROM
					farmacontrol_local.ventas
				LEFT JOIN farmacontrol_local.detallado_ventas USING(venta_id)
				LEFT JOIN farmacontrol_local.terminales ON
					terminales.terminal_id = ventas.terminal_id
				WHERE
					venta_id = @venta_id
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("cliente_credito_id", cliente_credito_id);
            parametros.Add("cliente_id", cliente_id);
            parametros.Add("sucursal_id", sucursal_id);
            parametros.Add("venta_id", venta_id);

            conector.Select(sql, parametros);

            if(conector.result_set.Rows.Count > 0)
            {
                sql = @"
				    INSERT INTO
					    farmacontrol_global.clientes_creditos
				    SET
						cliente_credito_id = @cliente_credito_id,
						cliente_id = @cliente_id,
						sucursal_id = @sucursal_id,
						venta_id = @venta_id,
						terminal = @terminal,
						venta_folio = @venta_folio,
						fecha_venta = @fecha_venta,
						total = @total
			    ";

                var row = conector.result_set.Rows[0];

                parametros = new Dictionary<string, object>();
                parametros.Add("cliente_credito_id",row["cliente_credito_id"].ToString());
                parametros.Add("cliente_id",row["cliente_id"].ToString());
                parametros.Add("sucursal_id",Convert.ToInt32(row["sucursal_id"]));
                parametros.Add("venta_id",Convert.ToInt32(row["venta_id"]));
                parametros.Add("terminal",row["terminal"]);
                parametros.Add("venta_folio",Convert.ToInt32(row["venta_folio"]));
                parametros.Add("fecha_venta",row["fecha_venta"].ToString());
                parametros.Add("total",Convert.ToDecimal(row["total"]));

                conector.Insert(sql, parametros);

                if (conector.filas_afectadas > 0)
                {
                    DAO_Ventas dao = new DAO_Ventas();
                    var venta_data = dao.get_venta_data(venta_id);

                    DAO_Cola_operaciones.insertar_cola_operaciones(venta_data.empleado_id, "rest/clientes", "registrar_venta_credito", parametros, "PARA REGISTRO AL SERVIDOR PRINCIPAL");
                }
            }
		}

		public DTO_Validacion terminar_venta_credito(long venta_id)
		{
			DTO_Validacion validacion = new DTO_Validacion();

			/*string sql = @"
				SELECT
	                clientes.cliente_id AS cliente_id,
	                SUM(detallado_ventas.total) AS total_venta,
	                clientes.limite_credito AS limite_credito,
	                COALESCE(tmp.adeudo_total, 0) AS adeudo_total
                FROM
	                farmacontrol_local.detallado_ventas
                JOIN farmacontrol_local.ventas USING(venta_id)
                LEFT JOIN farmacontrol_global.clientes ON clientes.cliente_id = ventas.cliente_credito_id
                LEFT JOIN (
	                SELECT
		                cliente_id,
		                SUM(COALESCE(IF(clientes_creditos.fecha_saldado IS NULL, clientes_creditos.total, 0), 0)) AS adeudo_total
	                FROM	
		                farmacontrol_global.clientes_creditos
	                GROUP BY cliente_id
                ) AS tmp ON
	                tmp.cliente_id = ventas.cliente_credito_id
                WHERE
	                ventas.venta_id = @venta_id
			";
            */
            string sql = @"
                SELECT
	                clientes.cliente_id AS cliente_id,
	                SUM(detallado_ventas.total) AS total_venta,
	                clientes.limite_credito AS limite_credito,
	                COALESCE(tmp.adeudo_total, 0) AS adeudo_total
                FROM
	                farmacontrol_local.detallado_ventas
                JOIN farmacontrol_local.ventas USING(venta_id)
                LEFT JOIN farmacontrol_global.clientes ON clientes.cliente_id = ventas.cliente_credito_id
				LEFT JOIN(					   
									   SELECT
										   temp.cliente_id as cliente_id,
										   SUM( total_disponible ) as adeudo_total
										 FROM
									   (
											SELECT
												clientes.cliente_id AS cliente_id, 
												clientes.nombre AS nombre,
												clientes.limite_credito AS credito_total,
												total - COALESCE(SUM(clientes_creditos_abonos.importe), 0) AS total_disponible
											FROM
												farmacontrol_global.clientes_creditos
											JOIN farmacontrol_global.clientes USING(cliente_id)
											LEFT JOIN 
												 farmacontrol_global.clientes_creditos_abonos
											USING( cliente_credito_id )	
											LEFT JOIN farmacontrol_global.clientes_domicilios USING(cliente_id)
											WHERE
												clientes_creditos.fecha_saldado IS NULL
											GROUP BY cliente_credito_id
										) as temp
										GROUP BY 
											cliente_id
				) as tmp 
				ON 
				   tmp.cliente_id = ventas.cliente_credito_id
	    WHERE
		    ventas.venta_id = @venta_id
            ";


			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("venta_id",venta_id);

			conector.Select(sql,parametros);

			var informacion_crediticia = conector.result_set;

			string cliente_id = informacion_crediticia.Rows[0]["cliente_id"].ToString();
			decimal total_venta = Convert.ToDecimal(informacion_crediticia.Rows[0]["total_venta"]);
			decimal limite_credito = Convert.ToDecimal(informacion_crediticia.Rows[0]["limite_credito"]);
			decimal adeudo_total = Convert.ToDecimal(informacion_crediticia.Rows[0]["adeudo_total"]);

			if(limite_credito > (adeudo_total + total_venta))
			{
				sql = @"
					SELECT
						nombre
					FROM
						pago_tipos
					WHERE
						es_credito IS TRUE
					LIMIT 1
				";

				parametros = new Dictionary<string,object>();

				conector.Select(sql,parametros);

				DataTable pago_tipos = new DataTable();
				pago_tipos.Columns.Add("metodo_pago",typeof(string));
				pago_tipos.Columns.Add("cuenta", typeof(string));
                pago_tipos.Columns.Add("importe", typeof(decimal));
				pago_tipos.Columns.Add("monto", typeof(decimal));

				DataRow row = pago_tipos.NewRow();
				row["metodo_pago"] = conector.result_set.Rows[0]["nombre"].ToString();
				row["cuenta"] = "0000";
                row["importe"] = total_venta;
				row["monto"] = total_venta;

				pago_tipos.Rows.Add(row);

				if(terminar_venta((long)venta_id,pago_tipos) > 0)
				{
					registrar_venta_credito(venta_id,cliente_id);
					registrar_cupon(venta_id);
					validacion.status = true;
					validacion.informacion = "Venta terminada a credito correctamente";	
				}
				else
				{
					validacion.status = false;
					validacion.informacion = "Ocurrio un problema al intentar terminar la venta, notifique a su administrador";	
				}
			}
			else
			{
				validacion.status = false;
				validacion.informacion = string.Format("El cliente no cuenta con el credito suficiente para terminar con la venta \n Saldo disponible: {0,15:C2}",(limite_credito - adeudo_total));
			}

			return validacion;
		}

		public void registrar_pago_tipos(long venta_id, DataTable pago_tipos)
		{
			string vale_efectivo_id = "";
			decimal total_vales = 0;

			var venta_data = get_venta_data(venta_id);
			long empleado_id = venta_data.empleado_id;

			DAO_Vales_efectivo dao_vales_efectivo = new DAO_Vales_efectivo();
            var totales_ventas = get_totales(venta_id);

            List<string> vales = new List<string>();

            foreach (DataRow row in pago_tipos.Rows)
            {
                if (row["metodo_pago"].ToString().Equals("VALE FARMACIA"))
                {
                    try
                    {
                        vale_efectivo_id = row["cuenta"].ToString();
                        total_vales += Convert.ToDecimal(row["monto"]);

                        var temp_explode = vale_efectivo_id.Split('$');

                        dao_vales_efectivo.set_fecha_canje(temp_explode[1].ToString(), empleado_id, venta_id);
                        vales.Add(temp_explode[1].ToString());
                    }
                    catch (Exception ex)
                    {
                        Log_error.log(ex);
                    }
                }
            }

            decimal total_vale_canjpar = (total_vales - totales_ventas.total);

            if (total_vale_canjpar > 0)
            {
                foreach (string id in vales)
                {
                    dao_vales_efectivo.set_fecha_cancelado(id, empleado_id);
                }

                string vale_efectivo_id_canjpar_canjeado = dao_vales_efectivo.generar_vale_efectivo_canjpar(venta_id, totales_ventas.total);
                dao_vales_efectivo.set_fecha_canje(vale_efectivo_id_canjpar_canjeado, empleado_id, venta_id);

                string sql = @"
					INSERT INTO
						farmacontrol_local.ventas_pagos
					(
						SELECT
							0 AS venta_pago_id,
							@venta_id AS venta_id,
							pago_tipo_id,
							@cuenta AS cuenta,
                            @importe AS importe,
							@monto AS monto
						FROM
							farmacontrol_global.pago_tipos
						WHERE
							nombre = @nombre
					)
                    ON DUPLICATE KEY UPDATE
                        venta_id = venta_id,
                        pago_tipo_id = pago_tipos.pago_tipo_id
				";

                DAO_Pago_tipos dao_pagos = new DAO_Pago_tipos();

                Dictionary<string, object> parametros = new Dictionary<string, object>();

                parametros.Add("venta_id", venta_id);
                parametros.Add("cuenta", vale_efectivo_id_canjpar_canjeado.ToUpper());
                parametros.Add("importe", totales_ventas.total);
                parametros.Add("monto", totales_ventas.total);
                parametros.Add("nombre", dao_pagos.get_pago_tipo("VFAR").nombre.ToUpper());

                conector.Insert(sql, parametros);

                Vale_efectivo ticket_vale_canjeado = new Vale_efectivo();
                ticket_vale_canjeado.construccion_ticket(vale_efectivo_id_canjpar_canjeado, false, true);
                ticket_vale_canjeado.print();

                string vale_efectivo_id_canjpar = dao_vales_efectivo.generar_vale_efectivo_canjpar(venta_id, total_vale_canjpar);

                Vale_efectivo ticket_vale = new Vale_efectivo();
                ticket_vale.construccion_ticket(vale_efectivo_id_canjpar);
                ticket_vale.print();
            }
            else
            {
                foreach (DataRow row in pago_tipos.Rows)
                {
                    string sql = @"
					    INSERT INTO
						    farmacontrol_local.ventas_pagos
					    (
						    SELECT
							    0 AS venta_pago_id,
							    @venta_id AS venta_id,
							    pago_tipo_id,
							    @cuenta AS cuenta,
                                @importe AS importe,
							    @monto AS monto
						    FROM
							    farmacontrol_global.pago_tipos
						    WHERE
							    nombre = @nombre
					    )
                        ON DUPLICATE KEY UPDATE
                            venta_id = venta_id
				    ";

                    Dictionary<string, object> parametros = new Dictionary<string, object>();

                    parametros.Add("venta_id", venta_id);

                    string[] tmp_cuenta = row["cuenta"].ToString().Split(new char[] { '$' });

                    if (tmp_cuenta.Length == 2)
                    {
                        parametros.Add("cuenta", tmp_cuenta[1]);
                    }
                    else
                    {
                        parametros.Add("cuenta", row["cuenta"].ToString());
                    }

                    
                    parametros.Add("importe", Convert.ToDecimal(row["importe"]));
                    parametros.Add("monto", Convert.ToDecimal(row["monto"]));
                    parametros.Add("nombre", row["metodo_pago"].ToString());

                    conector.Insert(sql, parametros);
                }
            }
		}

		public int terminar_venta(long venta_id, DataTable pago_tipos)
		{	
			
            Dictionary<string,object> parametros = new Dictionary<string,object>();
	
			string sql = string.Format(@"
				UPDATE
					farmacontrol_local.ventas
				SET
					fecha_terminado = NOW()
				WHERE
					venta_id = {0}
			",venta_id);

			conector.Update(sql,parametros);
            //VERIFICAR SI LA VENTA FUE TERMINADA EN CASO CONTRARIO DEVOLVER EL ERROR

            sql = @"
				SELECT
				   venta_folio 
                FROM
                    farmacontrol_local.ventas
                WHERE  
                    venta_id = @venta_id
                AND
                     fecha_terminado IS NOT NULL
			";

            parametros = new Dictionary<string, object>();
            parametros.Add("venta_id", venta_id);

            conector.Select(sql, parametros);

            int valido = 0;

            foreach (DataRow row in conector.result_set.Rows)
            {
                //SI ME DA RESULTADO ES QUE SI TODO BIEN
                //NO DA RESULTADO , HUBO UN ERROR
                valido = Convert.ToInt32(row["venta_folio"]);
            }

            if (valido == 0)
                return 0;
           
            //FIN DEL ERRRO

            //AGREGADO JS
            var terminal_id_venta = Misc_helper.get_terminal_id();

            if (terminal_id_venta == null)
                return 0;

            conector.TransactionStart();

			sql = string.Format(@"
				INSERT INTO
					farmacontrol_local.kardex
				(
					SELECT
						0 AS kardex_id,
						{1} AS terminal_id,
						NOW() AS fecha_date_time,
						NOW() AS fecha_date,
						detallado_ventas.articulo_id AS articulo_id,
						detallado_ventas.caducidad AS caducidad,
						detallado_ventas.lote AS lote,
						'VENTA' AS tipo,
                        ventas.venta_id AS elemento_id,
						ventas.venta_folio AS folio,
						existencias.existencia AS existencia_anterior,
						(detallado_ventas.cantidad * -1) AS cantidad,
						(existencias.existencia - detallado_ventas.cantidad) AS existencia_posterior,
                        0 AS es_importado,
						NOW() as modified
					FROM
						farmacontrol_local.detallado_ventas
                    JOIN farmacontrol_local.ventas USING(venta_id)
					JOIN farmacontrol_local.existencias USING(articulo_id,caducidad,lote)
					JOIN farmacontrol_global.articulos USING(articulo_id)
					WHERE
						detallado_ventas.venta_id = {0}
					AND
						farmacontrol_global.articulos.afecta_existencias IS TRUE
				)
			", venta_id, terminal_id_venta);//Misc_helper.get_terminal_id() MODIFICADO POR JS

			//conector.Update(sql, parametros);
            conector.Insert(sql, parametros);
				
			sql = string.Format(@"
				UPDATE
					farmacontrol_local.existencias
				JOIN farmacontrol_local.detallado_ventas USING(articulo_id,caducidad,lote)
				JOIN farmacontrol_global.articulos USING(articulo_id)
					SET
						existencias.existencia = (existencias.existencia - detallado_ventas.cantidad)
				WHERE
					detallado_ventas.venta_id = {0}
				AND
					farmacontrol_global.articulos.afecta_existencias IS TRUE
			", venta_id);

			conector.Update(sql, parametros);
            #region
            /*sql = string.Format(@"
				UPDATE
					farmacontrol_global.formulas
				SET
					venta_id = {0}
				WHERE
					formula_id IN (
						SELECT
							lote
						FROM
							farmacontrol_local.detallado_ventas
						WHERE
							articulo_id = {1}
						AND
							venta_id = {0}
					)
			", venta_id, Convert.ToInt64(Config_helper.get_config_global("formula_magistral_articulo_id")));

			conector.Update(sql,parametros);

            */
            /*
			sql = string.Format(@"
				DELETE 
					farmacontrol_local.existencias 
				FROM 
					farmacontrol_local.existencias
				JOIN farmacontrol_local.detallado_ventas USING(articulo_id,caducidad,lote)
				WHERE
					existencias.existencia = 0
				AND
					detallado_ventas.venta_id = {0}
			", venta_id);

			conector.Update(sql,parametros);

            /*
			conector.TransactionCommit();

			registrar_pago_tipos(venta_id, pago_tipos);
			registrar_cupon(venta_id);

			return 1;*/
            #endregion
            //AGREGADO POR JS
            if (conector.TransactionCommit())
            {

                sql = string.Format(@"
				    DELETE 
                    FROM
					    farmacontrol_local.existencias 
				    WHERE
					    existencia = 0
				   
		 	   ", venta_id);

                conector.Select(sql, parametros); 

                registrar_pago_tipos(venta_id, pago_tipos);
			    registrar_cupon(venta_id);
			    return 1;
            }
            else
            {
                return 0;
            }

		}

		public DataTable venta_facturada_data(long venta_id)
		{
			string sql = @"
				SELECT
					CONCAT_WS('|','|',version,uuid,fecha_timbrado,sello_digital,certificado_sat,'|') AS cadena_original,
					sello_digital,
					sello_sat AS timbre_fiscal,
					CONCAT('?re=',rfc_emisor,'&rr=',rfc_receptor,'&tt=',importe_total,'&id=',uuid) AS codigo_qr
				FROM
					farmacontrol_local.facturas
				JOIN farmacontrol_local.detallado_facturas USING(factura_id)
				WHERE
					detallado_facturas.venta_id = @venta_id
			";

			/*
			 *	CONVERT(UNCOMPRESS(cadena_original) USING utf8) AS cadena_original,
				CONVERT(UNCOMPRESS(sello_digital) USING utf8) AS sello_digital,
				CONVERT(UNCOMPRESS(timbre_fiscal) USING utf8) AS timbre_fiscal,
			 */

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("venta_id",venta_id);

			conector.Select(sql,parametros);

			return conector.result_set;
		}

		public DTO_Ventas_ticket get_informacion_ticket_venta(long venta_id)
		{
			string sql = @"
				SELECT
					venta_id,
                    cotizacion_id,
					venta_folio,
					ventas.fecha_creado AS fecha_creado,
					ventas.fecha_terminado AS fecha_terminado,
					empleados.nombre AS nombre_empleado,
					ventas.cliente_credito_id AS credito,
					ventas.cliente_domicilio_id AS domicilio,
                    ventas.comentarios AS comentarios,
                    empleados_cotizacion.nombre AS empleado_atendio,
                    COALESCE((SELECT t1.numero_transaccion FROM farmacontrol_local.log_tae_diestel AS t1 WHERE t1.venta_id = ventas.venta_id and tipo_ws = 'EJECUTA'),0) as numero_transaccion
				FROM
					farmacontrol_local.ventas
				LEFT JOIN farmacontrol_global.empleados USING(empleado_id)
                LEFT JOIN farmacontrol_local.cotizaciones USING(cotizacion_id)
                LEFT JOIN farmacontrol_global.empleados AS empleados_cotizacion ON empleados_cotizacion.empleado_id = cotizaciones.empleado_id
				WHERE
					venta_id = @venta_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("venta_id", venta_id);
			conector.Select(sql, parametros);

			DTO_Ventas_ticket ventas = new DTO_Ventas_ticket();

			foreach (DataRow row in conector.result_set.Rows)
			{

                DateTime? date_nullable = null;
                long? long_nullable = null;


				ventas.venta_id = Convert.ToInt32(row["venta_id"]);
				ventas.nombre_empleado = row["nombre_empleado"].ToString();
				ventas.venta_folio = Convert.ToInt64(row["venta_folio"]);
				ventas.nombre_terminal = Misc_helper.get_nombre_terminal();
				ventas.fecha_creado = Convert.ToDateTime(row["fecha_creado"]);
				ventas.fecha_terminado = (row["fecha_terminado"].ToString().Equals("")) ? date_nullable : Convert.ToDateTime(row["fecha_terminado"]);
				ventas.detallado_ventas_ticket = get_detallado_ventas(venta_id);
				ventas.domicilio = row["domicilio"].ToString();
				ventas.credito = row["credito"].ToString();
                ventas.comentarios = row["comentarios"].ToString();
                ventas.empleado_atendio = row["empleado_atendio"].ToString();
                ventas.numero_transaccion = Convert.ToInt64(row["numero_transaccion"]);
                ventas.cotizacion_id = (row["cotizacion_id"].ToString().Trim().Equals("")) ? long_nullable : Convert.ToInt64(row["cotizacion_id"]);
				var totales = get_totales(venta_id);
				ventas.excento = totales.excento;
				ventas.gravado = totales.gravado;
				ventas.subtotal = totales.subtotal;
				ventas.iva = totales.importe_iva;
                ventas.ieps = totales.importe_ieps;
				ventas.total = totales.total;
			}

			return ventas;
		}

		public List<Tuple<string, string, int>> get_detallado_caducidades(long venta_id, int articulo_id, decimal importe)
		{
			string sql = @"
				SELECT
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					cantidad
				FROM
					farmacontrol_local.detallado_ventas
				WHERE
					venta_id = @venta_id
				AND
					articulo_id = @articulo_id
				AND
					importe = @importe
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("venta_id", venta_id);
			parametros.Add("articulo_id", articulo_id);
			parametros.Add("importe", importe);

			conector.Select(sql, parametros);

			List<Tuple<string, string, int>> lista_caducidades = new List<Tuple<string, string, int>>();

			foreach (DataRow row in conector.result_set.Rows)
			{
				Tuple<string, string, int> tupla = new Tuple<string, string, int>(row["caducidad"].ToString(), row["lote"].ToString(), Convert.ToInt32(row["cantidad"]));
				lista_caducidades.Add(tupla);
			}

			return lista_caducidades;
		}

		public List<DTO_Detallado_ventas_ticket> get_detallado_ventas(long venta_id)
		{
            //RPAD(CONCAT('*',SUBSTRING(amecop, LENGTH(amecop)-3) ),5,' ') AS amecop
			string sql = @"
				SELECT
					articulos.articulo_id,
					(
						SELECT
							amecop
						FROM
							farmacontrol_global.articulos_amecops
						WHERE
							articulos_amecops.articulo_id = detallado_ventas.articulo_id
						ORDER BY articulos_amecops.amecop_principal DESC
						LIMIT 1
					) AS amecop,
					RPAD(nombre, 37, ' ') AS nombre,
					detallado_ventas.precio_publico,
					detallado_ventas.importe,
					SUM(detallado_ventas.cantidad) AS cantidad,
					detallado_ventas.subtotal,
					detallado_ventas.pct_descuento AS pct_descuento,
					detallado_ventas.importe_descuento,
					detallado_ventas.total,
                    (SELECT nombre from farmacontrol_global.fabricantes WHERE fabricante_id = articulos.fabricante_id) as nombre_fabricante
				FROM
					farmacontrol_local.detallado_ventas
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					venta_id = @venta_id
				GROUP BY
					articulo_id,importe
				ORDER BY detallado_venta_id ASC
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("venta_id", venta_id);

			conector.Select(sql, parametros);

			var result_set = conector.result_set;

			List<DTO_Detallado_ventas_ticket> lista_detallado_venta = new List<DTO_Detallado_ventas_ticket>();

			foreach (DataRow row in result_set.Rows)
			{
				DTO_Detallado_ventas_ticket detallado_ticket = new DTO_Detallado_ventas_ticket();
				detallado_ticket.articulo_id = Convert.ToInt32(row["articulo_id"]);
                
                string var_temp_amecop = row["amecop"].ToString();
                int tam_var = var_temp_amecop.Length;
                String Var_Sub = "*" + var_temp_amecop.Substring((tam_var - 3), 3);
                string amecop_temp = Var_Sub.PadRight(5, ' ');

                detallado_ticket.amecop = amecop_temp;
                //detallado_ticket.amecop = row["amecop"].ToString();

				detallado_ticket.nombre = row["nombre"].ToString().Replace("NO*","").Replace("CDD*","").Replace("SE*","").Replace("EF*","").Replace("(S)","").Replace("PN*","").Replace("BR*","").Replace("OF*","").Replace("H*","").Replace("W*","").Replace("NV*","").Replace("CD*","").Replace("NP*","").Replace("CT*","").Replace("N-M*","");

				detallado_ticket.precio_unitario = Convert.ToDecimal(row["precio_publico"]);
				detallado_ticket.subtotal = Convert.ToDecimal(row["subtotal"]);
				detallado_ticket.importe = Convert.ToDecimal(row["importe"]);
				detallado_ticket.descuento = Convert.ToDecimal(row["pct_descuento"]);
				detallado_ticket.total = Convert.ToDecimal(row["total"]);
				detallado_ticket.cantidad = Convert.ToInt32(row["cantidad"]);
				detallado_ticket.caducidades_lotes = get_detallado_caducidades(venta_id, detallado_ticket.articulo_id, Convert.ToDecimal(row["importe"]));
                detallado_ticket.nombre_fabricante = row["nombre_fabricante"].ToString();
				lista_detallado_venta.Add(detallado_ticket);
			}

			return lista_detallado_venta;
		}

		public DTO_Totales get_totales(long venta_id)
		{
			DTO_Totales totales = new DTO_Totales();

			string sql = @"
				SELECT
					SUM(cantidad) AS piezas,
					SUM(subtotal) AS subtotal,
					SUM(IF(detallado_ventas.pct_iva > 0, detallado_ventas.total, 0)) AS gravado,
					SUM(IF(detallado_ventas.pct_iva = 0, detallado_ventas.total, 0)) AS excento,
					SUM(detallado_ventas.importe_iva) AS importe_iva,
					SUM(detallado_ventas.importe_ieps) AS importe_ieps,
					SUM(detallado_ventas.total)  AS total
				FROM
					farmacontrol_local.ventas
				JOIN farmacontrol_local.detallado_ventas USING(venta_id)
				WHERE
					ventas.venta_id = @venta_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("venta_id", venta_id);

			conector.Select(sql, parametros);

			foreach(DataRow row in conector.result_set.Rows)
			{
				totales.piezas = Convert.ToInt64(row["piezas"]);
				totales.subtotal = Convert.ToDecimal(row["subtotal"]);
				totales.gravado = Convert.ToDecimal(row["gravado"]);
				totales.excento = Convert.ToDecimal(row["excento"]);
				totales.importe_iva = Convert.ToDecimal(row["importe_iva"]);
				totales.importe_ieps = Convert.ToDecimal(row["importe_ieps"]);
				totales.total = Convert.ToDecimal(row["total"]);
			}

			return totales;
		}

        public long get_folio_nota_credito()
        {
            string sql = @"
                SELECT
                    COALESCE(MAX(folio), 0) +1 AS folio
                FROM
                    farmacontrol_local.notas_credito
                LIMIT 1
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            conector.Select(sql, parametros);

            return Convert.ToInt64(conector.result_set.Rows[0]["folio"]);
        }

		public string get_informacion_nota_credito(long venta_id, bool es_cancelacion = true)
		{
            long folio = get_folio_nota_credito();

            /*terminales_serie_nc*/
            //RPAD(CONCAT('*',SUBSTRING(amecop, LENGTH(amecop)-3) ),5,' ') AS CODIGO
			string sql = @"
				SELECT
					@folio AS FOLIO,
					terminales.serie_nc AS SERIE,
					rfc_registros.razon_social AS RAZON_SOCIAL,
					rfc_registros.rfc AS RFC,
					'MEXICO' AS PAIS_FISCAL,
					IF(rfc_registros.tipo_rfc = 'RFC', rfc_registros.estado, tmp_rfc.estado) AS ESTADO_FISCAL,
					IF(rfc_registros.tipo_rfc = 'RFC', rfc_registros.municipio, tmp_rfc.municipio) AS MUNICIPIO_FISCAL,
					IF(rfc_registros.tipo_rfc = 'RFC', rfc_registros.ciudad, tmp_rfc.ciudad) AS CIUDAD_FISCAL,
					rfc_registros.calle AS CALLE_FISCAL,
					rfc_registros.numero_exterior AS NUMERO_EXT_FISCAL,
					rfc_registros.numero_interior AS NUMERO_INT_FISCAL,
					rfc_registros.colonia AS COLONIA_FISCAL,
					IF(rfc_registros.tipo_rfc = 'RFC', rfc_registros.codigo_postal, tmp_rfc.codigo_postal) AS CP_FISCAL,
					 DATE_FORMAT(NOW(), '%d%m%Y:%H:%i:%s') AS FECHA,
					'PZA' AS UNIDAD,
					farmacontrol_local.detallado_ventas.cantidad AS CANTIDAD,
					farmacontrol_local.detallado_ventas.importe AS PRECIO,
					articulos.nombre AS CONCEPTO,
					(
						SELECT 
							RPAD(CONCAT('*',SUBSTRING(amecop, LENGTH(amecop)-3) ),5,' ') AS CODIGO
						FROM 
							farmacontrol_global.articulos_amecops 
						WHERE 
							articulo_id = detallado_ventas.articulo_id
						ORDER BY 
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS CODIGO,
					(farmacontrol_local.detallado_ventas.pct_iva * 100) AS TASA_IVA,
					'TRUE' AS STATUS,
					0 AS TASA_DESCUENTO,
					0 AS TASA_IEPS,
					0 AS TASA_RET_ISR,
					0 AS TASA_RET_IVA,
					'' AS NOTAS,
					'PESOS' AS MONEDA_NOMBRE,
					'MXN' AS MONEDA_SIMBOLO,
					1 AS TIPO_CAMBIO,
					'PAGO EN UNA SOLA EXHIBICION' AS FORMA_PAGO,
					'CONTADO' AS CONDICIONES_PAGO,
					'99' AS METODO_PAGO,
					'' AS EMAIL_CLIENTE,
					'LEYENDA1' AS LEYENDA1,
					'LEYENDA2' AS LEYENDA2,
					ventas_pagos.cuenta AS NUM_CTA_PAGO,
					'Mérida, Yucatan' AS LUGAR_EXPEDICION,
					'Regimen General de ley Personas Morales' AS REGIMEN,
					'' AS IMPORTE_IEPS_FACTURA,
					'' AS TASA_IEPS_FACTURA
				FROM
					farmacontrol_local.ventas
				LEFT JOIN farmacontrol_local.terminales USING(terminal_id)
				LEFT JOIN farmacontrol_local.detallado_ventas USING(venta_id)
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				LEFT JOIN farmacontrol_local.ventas_pagos USING(venta_id)
				LEFT JOIN farmacontrol_global.pago_tipos USING(pago_tipo_id)
				LEFT JOIN farmacontrol_global.rfc_registros ON
					rfc_registros.rfc_registro_id = @rfc_registro_id
                LEFT JOIN 
                (
                    SELECT
                        rfc_registro_id,
                        sucursales.ciudad AS ciudad,
                        sucursales.municipio AS municipio,
                        sucursales.estado AS estado,
                        sucursales.codigo_postal AS codigo_postal
                    FROM
                        farmacontrol_global.rfc_registros
                    LEFT JOIN farmacontrol_global.sucursales ON
                        sucursales.sucursal_id = @sucursal_id
                    WHERE
                        rfc_registros.rfc_registro_id = @rfc_registro_id
                ) AS tmp_rfc USING(rfc_registro_id)
				WHERE
					farmacontrol_local.ventas.venta_id = @venta_id
				GROUP BY
					detallado_ventas.articulo_id,detallado_ventas.caducidad, detallado_ventas.lote, detallado_ventas.importe
			 ";


			var existe_factura = WebServicePac_helper.existe_factura(venta_id);

			DTO_Rfc dto_rfc = new DTO_Rfc();
			DAO_Rfcs dao_rfc = new DAO_Rfcs();

			if (existe_factura.status)
			{	
				var dto_Existe = dao_rfc.existe_rfc(existe_factura.rfc_receptor);	

				if(dto_Existe.status)
				{
					dto_rfc = dao_rfc.get_data_rfc(dto_Existe.informacion);
				}
			}
			else
			{
				dto_rfc = dao_rfc.get_rfc_publico_general_mexicano();
			}

			var venta_data = get_venta_data(venta_id);

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("venta_id",venta_id);
            parametros.Add("folio",folio);
			parametros.Add("rfc_registro_id",dto_rfc.rfc_registro_id);
            parametros.Add("sucursal_id",Config_helper.get_config_local("sucursal_id"));

			conector.Select(sql, parametros);

			StringBuilder conector_txt = new StringBuilder();

			DataRow row_nota_credito = conector.result_set.NewRow();

			try
			{

				foreach(DataColumn col in conector.result_set.Columns)
				{
					if(col.ColumnName.Equals("CANTIDAD"))
					{
						row_nota_credito["CANTIDAD"] = 1;
					}
					else if (col.ColumnName.Equals("PRECIO"))
					{
						row_nota_credito["PRECIO"] = 0;
					}
					else if (col.ColumnName.Equals("CONCEPTO"))
					{
                        if(es_cancelacion)
                        {
                            if (existe_factura.status)
                            {
                                row_nota_credito["CONCEPTO"] = string.Format("EMITIDA POR DEVOLUCION EN CONTRAPARTE DE FACTURA SERIE {0} FOLIO #{1}", existe_factura.serie, existe_factura.folio);
                            }
                            else
                            {
                                DAO_Cortes dao_cortes = new DAO_Cortes();
                                var corte_data = dao_cortes.get_informacion_corte((long)venta_data.corte_total_id);
                                DAO_Terminales dao_terminales = new DAO_Terminales();

                                row_nota_credito["CONCEPTO"] = string.Format("EMITIDA POR DEVOLUCION EN CONTRAPARTE DE FACTURA SERIE {0} FOLIO #{1}", dao_terminales.get_terminal_serie_facturas_cortes((int)venta_data.terminal_id), corte_data.corte_folio);
                            }
                            
                        }
                        else
                        {
                            DAO_Cortes dao_cortes = new DAO_Cortes();
                            var corte_data = dao_cortes.get_informacion_corte((long)venta_data.corte_total_id);
                            DAO_Terminales dao_terminales = new DAO_Terminales();

                            row_nota_credito["CONCEPTO"] = string.Format("EMITIDA POR FACTURACION EN CONTRAPARTE DE FACTURA SERIE {0} FOLIO #{1}", dao_terminales.get_terminal_serie_facturas_cortes((int)venta_data.terminal_id), corte_data.corte_folio);
                        }
					}
					else if (col.ColumnName.Equals("CODIGO"))
					{
						row_nota_credito["CODIGO"] = "0000";
					}
					else if (col.ColumnName.Equals("TASA_IVA"))
					{
						row_nota_credito["TASA_IVA"] = 0;
					}
					else
					{
						row_nota_credito[col.ColumnName] = conector.result_set.Rows[0][col.ColumnName];
					}
				}

			}
			catch (Exception ex)
			{
				Log_error.log(ex);
			}

			conector.result_set.Rows.Add(row_nota_credito);

			foreach (DataRow row in conector.result_set.Rows)
			{
				object[] columnas = row.ItemArray;

				conector_txt.Append("|");

				for (int i = 0; i < columnas.Length; i++)
				{
					conector_txt.Append(columnas[i].ToString() + "|");
				}

				conector_txt.Append("\n");
			}


			return conector_txt.ToString();
		}

       


        public string show_pagos()
        {
            Tipos_pago tipos = new Tipos_pago();
            tipos.ShowDialog();

            if (tipos.return_pago_tipos.pago_tipo_id == 0)
            {
                return show_pagos();
            }

            return tipos.return_pago_tipos.nombre;
        }

		public string get_informacion_factura(long venta_id,DTO_Rfc dto_rfc, string condiciones_pago, string metodo_pago, string cuenta, string correos, bool es_nota_credito)
		{
			string sql = "";
			//string metodo_pago = "";
			//string cuenta = "";
			//string condiciones_pago = "";
            /*
			sql = @"
				SELECT
					pago_tipos.nombre AS nombre,
					pago_tipos.es_credito AS es_credito,
					ventas_pagos.cuenta AS cuenta
				FROM
					farmacontrol_local.ventas_pagos
				LEFT JOIN farmacontrol_global.pago_tipos USING(pago_tipo_id)
				WHERE
					venta_id = @venta_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("venta_id",venta_id);

			conector.Select(sql,parametros);
			cuenta = "";
			condiciones_pago = "EFECTIVO";

			var pago_tipos = conector.result_set;

            bool es_credito = false;

			foreach (DataRow row in pago_tipos.Rows)
			{
				cuenta = row["cuenta"].ToString();
				metodo_pago = row["nombre"].ToString();

				if (Convert.ToBoolean(row["es_credito"]))
				{
                    es_credito = true;
					condiciones_pago = "CREDITO";
				}
			}

            if(es_credito)
            {
                metodo_pago = show_pagos();
            }
            else
            {
                if (pago_tipos.Rows.Count > 1)
                {
                    metodo_pago = "NO IDENTIFICADO";
                    cuenta = "";
                }
            }
            */

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("venta_id", venta_id);
            //RPAD(CONCAT('*',SUBSTRING(amecop, LENGTH(amecop)-3) ),5,' ') AS CODIGO
			 sql = string.Format(@"
				SELECT
					(farmacontrol_local.ventas.venta_folio) AS FOLIO,
					{0} AS SERIE,
					@razon_social AS RAZON_SOCIAL,
					@rfc AS RFC,
					'MEXICO' AS PAIS_FISCAL,
					@estado AS ESTADO_FISCAL,
					@municipio AS MUNICIPIO_FISCAL,
					@ciudad AS CIUDAD_FISCAL,
					@calle AS CALLE_FISCAL,
					@numero_exterior AS NUMERO_EXT_FISCAL,
					@numero_interior AS NUMERO_INT_FISCAL,
					@colonia AS COLONIA_FISCAL,
					@codigo_postal AS CP_FISCAL,
					 DATE_FORMAT(NOW(), '%d%m%Y:%H:%i:%s') AS FECHA,
					'PZA' AS UNIDAD,
					farmacontrol_local.detallado_ventas.cantidad AS CANTIDAD,
					farmacontrol_local.detallado_ventas.importe AS PRECIO,
					articulos.nombre AS CONCEPTO,
					(
						SELECT 
							amecop AS CODIGO
						FROM 
							farmacontrol_global.articulos_amecops 
						WHERE 
							articulo_id = detallado_ventas.articulo_id
						ORDER BY 
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS CODIGO,
					(farmacontrol_local.detallado_ventas.pct_iva * 100) AS TASA_IVA,
					'TRUE' AS STATUS,
					0 AS TASA_DESCUENTO,
					0 AS TASA_IEPS,
					0 AS TASA_RET_ISR,
					0 AS TASA_RET_IVA,
					'' AS NOTAS,
					'PESOS' AS MONEDA_NOMBRE,
					'MXN' AS MONEDA_SIMBOLO,
					1 AS TIPO_CAMBIO,
					'PAGO EN UNA SOLA EXHIBICION' AS FORMA_PAGO,
					@condiciones_pago AS CONDICIONES_PAGO,
					@metodo_pago AS METODO_PAGO,
					@correos AS EMAIL_CLIENTE,
					'LEYENDA1' AS LEYENDA1,
					'LEYENDA2' AS LEYENDA2,
					@cuenta AS NUM_CTA_PAGO,
					'Merida, Yucatan' AS LUGAR_EXPEDICION,
					'Regimen General de ley Personas Morales' AS REGIMEN,
					'' AS IMPORTE_IEPS_FACTURA,
					'' AS TASA_IEPS_FACTURA
				FROM
					farmacontrol_local.ventas
				JOIN farmacontrol_local.terminales USING(terminal_id)
				LEFT JOIN farmacontrol_local.detallado_ventas USING(venta_id)
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				LEFT JOIN farmacontrol_local.ventas_pagos USING(venta_id)
				LEFT JOIN farmacontrol_global.pago_tipos USING(pago_tipo_id)
				WHERE
					farmacontrol_local.ventas.venta_id = @venta_id
				GROUP BY
					detallado_ventas.articulo_id,detallado_ventas.caducidad, detallado_ventas.lote, detallado_ventas.importe
			 ", (es_nota_credito) ? "terminales.serie_nc" : "terminales.serie_facturas");

			 DAO_Terminales dao_terminales = new DAO_Terminales();

			parametros.Add("condiciones_pago",condiciones_pago);

			parametros.Add("razon_social",dto_rfc.razon_social);
			parametros.Add("rfc",dto_rfc.rfc);
			parametros.Add("calle",dto_rfc.calle);

			parametros.Add("numero_exterior",dto_rfc.numero_exterior);
			parametros.Add("numero_interior",dto_rfc.numero_interior);
			parametros.Add("codigo_postal",(dto_rfc.codigo_postal.ToString().Length == 5) ? dto_rfc.codigo_postal.ToString() : "00000");
			parametros.Add("colonia",dto_rfc.colonia);
			parametros.Add("municipio",dto_rfc.municipio);
			parametros.Add("ciudad",dto_rfc.ciudad);
			parametros.Add("estado",dto_rfc.estado);

			parametros.Add("metodo_pago",metodo_pago);
			parametros.Add("cuenta",cuenta);
			parametros.Add("correos",correos);

			conector.Select(sql, parametros);

			StringBuilder conector_txt = new StringBuilder();

			foreach (DataRow row in conector.result_set.Rows)
			{
				object [] columnas = row.ItemArray;

				conector_txt.Append("|");

				for(int i = 0; i < columnas.Length; i++)
				{
					conector_txt.Append(columnas[i].ToString()+"|");
				}

				conector_txt.Append("\n");
			}
            /*
            conector_txt.Clear();
            conector_txt.Append("|172|E|PÚBLICO EN GENERAL|XAXX010101000|MÉXICO||||NEBULOSA|||||05012011|EA|7|5|CONCEPTO DOS|16|FALSE|0||||COMPROBANTE DE EJEMPLO|PESOS MEXICANOS|MXN|1.00|PAGO EN UNA SOLA EXHIBICIÓN|CONTADO|EFECTIVO|CD JUAREZ; CD MEXICO, GUADALAJARA|01013254; 05032985 , 06236589|651651875,65169813268 ; 614651989853|");
            */
			return conector_txt.ToString();
		}


        public string get_informacion_factura_33(long venta_id, DTO_Rfc dto_rfc, string condiciones_pago, string metodo_pago, string cuenta, string correos, bool es_nota_credito,string uso_del_cfdi,string regimen_fiscal)
        {

            string sql = "";
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("venta_id", venta_id);
            //RPAD(CONCAT('*',SUBSTRING(amecop, LENGTH(amecop)-3) ),5,' ') AS CODIGO
            //LEFT JOIN farmacontrol_local.ventas_pagos USING(venta_id)
            //LEFT JOIN farmacontrol_global.pago_tipos USING(pago_tipo_id)
            sql = string.Format(@"
				SELECT
					(farmacontrol_local.ventas.venta_folio) AS FOLIO,
					{0} AS SERIE,
					@razon_social AS RAZON_SOCIAL,
					@rfc AS RFC,
					'MEXICO' AS PAIS_FISCAL,
					@estado AS ESTADO_FISCAL,
					@municipio AS MUNICIPIO_FISCAL,
					@ciudad AS CIUDAD_FISCAL,
					@calle AS CALLE_FISCAL,
					@numero_exterior AS NUMERO_EXT_FISCAL,
					@numero_interior AS NUMERO_INT_FISCAL,
					@colonia AS COLONIA_FISCAL,
					@codigo_postal AS CP_FISCAL,
                    @uso_del_cfdi  AS uso_del_cfdi,
					DATE_FORMAT( DATE_SUB(NOW(),INTERVAL 10 MINUTE) , '%Y-%m-%dT%H:%i:%s') AS FECHA,
					'PZA' AS UNIDAD,
					SUM( farmacontrol_local.detallado_ventas.cantidad ) AS CANTIDAD,
					farmacontrol_local.detallado_ventas.importe AS PRECIO,
					REPLACE(articulos.nombre,'&',' ') AS CONCEPTO,
					(
						SELECT 
							amecop AS CODIGO
						FROM 
							farmacontrol_global.articulos_amecops 
						WHERE 
							articulo_id = detallado_ventas.articulo_id
						ORDER BY 
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS CODIGO,
					FORMAT(farmacontrol_local.detallado_ventas.pct_iva,6 ) AS TASA_IVA,
					'TRUE' AS STATUS,
					0 AS TASA_DESCUENTO,
					0 AS TASA_IEPS,
					0 AS TASA_RET_ISR,
					0 AS TASA_RET_IVA,
					'' AS NOTAS,
					'PESOS' AS MONEDA_NOMBRE,
					'MXN' AS MONEDA_SIMBOLO,
					1 AS TIPO_CAMBIO,
					'PAGO EN UNA SOLA EXHIBICION' AS FORMA_PAGO,
					@condiciones_pago AS CONDICIONES_PAGO,
					@metodo_pago AS METODO_PAGO,
					@correos AS EMAIL_CLIENTE,
					'LEYENDA1' AS LEYENDA1,
					'LEYENDA2' AS LEYENDA2,
					@cuenta AS NUM_CTA_PAGO,
					'Merida, Yucatan' AS LUGAR_EXPEDICION,
					'Regimen General de ley Personas Morales' AS REGIMEN,
					'' AS IMPORTE_IEPS_FACTURA,
					'' AS TASA_IEPS_FACTURA,
                    SUM( total ) as total,
                    SUM( subtotal ) as subtotal,
                     IF( cliente_credito_id IS NULL, 'PUE', 'PPD' )  as tipo_venta,
                     FORMAT( SUM( subtotal )* FORMAT(farmacontrol_local.detallado_ventas.pct_iva,2 )  ,2) as porcentaje
				FROM
					farmacontrol_local.ventas
				JOIN farmacontrol_local.terminales USING(terminal_id)
				LEFT JOIN farmacontrol_local.detallado_ventas USING(venta_id)
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)		
				WHERE
					farmacontrol_local.ventas.venta_id = @venta_id
				GROUP BY
					detallado_ventas.articulo_id
			 ", (es_nota_credito) ? "terminales.serie_nc" : "terminales.serie_facturas");

            DAO_Terminales dao_terminales = new DAO_Terminales();
            
            parametros.Add("condiciones_pago", condiciones_pago);

            parametros.Add("razon_social", dto_rfc.razon_social);
            parametros.Add("rfc", dto_rfc.rfc);
            parametros.Add("calle", dto_rfc.calle);
            parametros.Add("uso_del_cfdi",uso_del_cfdi);

            parametros.Add("numero_exterior", dto_rfc.numero_exterior);
            parametros.Add("numero_interior", dto_rfc.numero_interior);

            parametros.Add("codigo_postal", (dto_rfc.codigo_postal.ToString().Length == 5) ? dto_rfc.codigo_postal.ToString() : "00000" );


            parametros.Add("colonia", dto_rfc.colonia);
            parametros.Add("municipio", dto_rfc.municipio);
            parametros.Add("ciudad", dto_rfc.ciudad);
            parametros.Add("estado", dto_rfc.estado);

            parametros.Add("metodo_pago", metodo_pago);
            parametros.Add("cuenta", cuenta);
            parametros.Add("correos", correos);

            conector.Select(sql, parametros);

            //StringBuilder conector_txt = new StringBuilder();
            decimal total_impuestos_traslados = 0;
            string conector_txt="";
            string conector_txt_inicio = "";
            string serie = "";
            string folio = "";
            string fecha = "";
            string metodopagos = "";
            decimal subtotal = 0;
            decimal descuento = 0;
            decimal total = 0;
            int codigo_postal = 0;
            string rfc_emisor = "FCO030103ES3";
            string emisor_nombre = "FARMACIAS COMERCIO SA DE CV";
            string direccion_cliente = "Calle " + dto_rfc.calle + "Num. " + dto_rfc.numero_exterior + " Col. " + dto_rfc.colonia;


            DataTable dt_original = conector.result_set;
          
            conector_txt += " <cfdi:Conceptos> ";
            bool hay_cero = false;
            bool hay_iva  = false;
            decimal base_cero = 0;
            decimal base_iva  = 0;

            foreach (DataRow row in dt_original.Rows)
            {

                string codigo = row[19].ToString();
                string codigo_clave_serv = "01010101";
                sql = string.Format(@"
                   SELECT
                       IFNULL( codigo_sat , '01010101' ) AS codigo_sat  
                   FROM 
                      farmacontrol_global.articulos
                   WHERE
                      amecop_original = @codigo
                   LIMIT 1
                ");

                parametros = new Dictionary<string, object>();
                parametros.Add("codigo", codigo);
                conector.Select(sql, parametros);

                foreach (DataRow row_sat in conector.result_set.Rows)
                {
                    codigo_clave_serv = row_sat[0].ToString();
                    
                }
                if( codigo_clave_serv == "" )
                    codigo_clave_serv = "01010101";

               string descripcion = row[18].ToString();
               metodopagos = row[43].ToString();
              
               decimal tasa_concepto = Convert.ToDecimal(row[20].ToString());
               decimal precio_unitario = Convert.ToDecimal(row[17].ToString());
               decimal descuento_concepto = 0;
               int cantidad = Convert.ToInt32(row[16].ToString());
               decimal importe = cantidad*precio_unitario;

               serie = row[1].ToString();
               //serie = "E";
               folio = row[0].ToString();
               fecha = row[14].ToString();
               decimal total_parcial = Convert.ToDecimal(row[41].ToString());
               decimal subtotal_parcial = Convert.ToDecimal(row[42].ToString());
               decimal importe_iva = Convert.ToDecimal(row[44].ToString());
               conector_txt += "<cfdi:Concepto ClaveProdServ='" + codigo_clave_serv + "' Cantidad='" + cantidad + "' ClaveUnidad='H87' Unidad='Pieza' ObjetoImp='02' NoIdentificacion='" + codigo + "' Descripcion='" + descripcion + "' ValorUnitario='" + precio_unitario + "' Descuento='" + String.Format("{0:0.00}", descuento_concepto) + "' Importe='" + String.Format("{0:0.00}", subtotal_parcial) + "'>";
               if (importe_iva > 0)
               {
                   hay_iva = true;
                   conector_txt += "<cfdi:Impuestos>";
                   conector_txt += "<cfdi:Traslados>";
                   conector_txt += "<cfdi:Traslado Base='" + String.Format("{0:0.00}", subtotal_parcial) + "' Impuesto='002' TipoFactor='Tasa' TasaOCuota='" + tasa_concepto + "' Importe='" + String.Format("{0:0.00}", importe_iva) + "'/>";
                   conector_txt += "</cfdi:Traslados>";
                   conector_txt += "</cfdi:Impuestos>";
                   base_iva += subtotal_parcial;
               }
               else
               { 
                  //cambio en caso de no tener iva 
                   hay_cero = true;
                   conector_txt += "<cfdi:Impuestos>";
                   conector_txt += "<cfdi:Traslados>";
                   conector_txt += "<cfdi:Traslado Base='" + String.Format("{0:0.00}", subtotal_parcial) + "' Impuesto='002' TipoFactor='Tasa' TasaOCuota='0.000000' Importe='0.00'/>";
                   conector_txt += "</cfdi:Traslados>";
                   conector_txt += "</cfdi:Impuestos>";
                   base_cero += subtotal_parcial;
               }

                 //total += total_parcial;
                subtotal += Convert.ToDecimal(String.Format("{0:0.00}",subtotal_parcial));
				conector_txt += "</cfdi:Concepto>";

                total_impuestos_traslados += Convert.ToDecimal(String.Format("{0:0.00}", importe_iva));
            }
            conector_txt +="</cfdi:Conceptos>";

            if (total_impuestos_traslados > 0)
            {

              

                conector_txt += "<cfdi:Impuestos TotalImpuestosTrasladados='" + String.Format("{0:0.00}", total_impuestos_traslados) + "'>";
                conector_txt += "<cfdi:Traslados>";
                conector_txt += "<cfdi:Traslado Base = '" + String.Format("{0:0.00}", base_iva) + "'  Impuesto='002' TipoFactor='Tasa' TasaOCuota='0.160000' Importe='" + String.Format("{0:0.00}", total_impuestos_traslados) + "'/>";
                if( hay_iva && hay_cero )
                    conector_txt += "<cfdi:Traslado  Base='" + String.Format("{0:0.00}", base_cero) + "' Impuesto='002' TipoFactor='Tasa' TasaOCuota='0.000000' Importe='0.00'/>";
                     
                conector_txt += "</cfdi:Traslados></cfdi:Impuestos>";
            }
            else
            { 
                // cambio en caso de no tener iva
                conector_txt += "<cfdi:Impuestos TotalImpuestosTrasladados='0.00'>";
                conector_txt += "<cfdi:Traslados>";
                conector_txt += "<cfdi:Traslado Base='" + String.Format("{0:0.00}", base_cero) + "'  Impuesto='002' TipoFactor='Tasa' TasaOCuota='0.000000' Importe='0.00'/>";
                conector_txt += "</cfdi:Traslados></cfdi:Impuestos>";
            }

            //total = subtotal + total_impuestos_traslados;
            total = Convert.ToDecimal(String.Format("{0:0.00}", subtotal)) +Convert.ToDecimal( String.Format("{0:0.00}", total_impuestos_traslados));

            /*
            conector_txt += "<cfdi:Addenda xmlns:cfdi='http://www.sat.gob.mx/cfd/4'>";
            conector_txt += "<acpaddenda>";

            conector_txt += "<acpleyenda nombre='LEYENDA1' valor='' />";
            conector_txt += "<acpleyenda nombre='LEYENDA2' valor='' />";
            conector_txt += "<acpleyenda nombre='LEYENDA3' valor='" + direccion_cliente + "' />";
            conector_txt += "<acpnotas nombre='' valor='' />";
            conector_txt += "<acpemail valor='' />";
            conector_txt += "</acpaddenda>";
            conector_txt += "</cfdi:Addenda>";

            */


			conector_txt +="</cfdi:Comprobante>";

            conector_txt_inicio = "<?xml version='1.0' encoding='utf-8'?>";
            conector_txt_inicio += "<cfdi:Comprobante xmlns:cfdi='http://www.sat.gob.mx/cfd/4' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='http://www.sat.gob.mx/cfd/4 ";
            conector_txt_inicio += "http://www.sat.gob.mx/sitio_internet/cfd/4/cfdv40.xsd' Version='4.0' Serie='" + serie + "' Folio='" + folio + "' Fecha='" + fecha + "' TipoDeComprobante='I' Exportacion='01' FormaPago='" + metodo_pago + "'";
            conector_txt_inicio += " MetodoPago='" + metodopagos + "' NoCertificado='' SubTotal='" + String.Format("{0:0.00}",subtotal) + "' Descuento='" + String.Format("{0:0.00}",descuento) + "' TipoCambio='1' Moneda='MXN' Total='" + String.Format("{0:0.00}",total) + "'";

            sql = string.Format(@"
                   SELECT
                      codigo_postal,
                       rfc,
                       razon_social
                   FROM 
                      farmacontrol_local.config
                  LEFT JOIN 
                      farmacontrol_global.sucursales
                   ON   
                      valor = sucursal_id
                   WHERE
                       farmacontrol_local.config.nombre = 'sucursal_id'
            ");

            parametros = new Dictionary<string, object>();
            conector.Select(sql, parametros);
            DataTable helper = conector.result_set;
            foreach (DataRow row in helper.Rows)
            {
                rfc_emisor = row[1].ToString();
                emisor_nombre = row[2].ToString();
                codigo_postal = Convert.ToInt32( row[0].ToString() );
            }

            //rfc_emisor = "LAN7008173R5";
            //emisor_nombre = "EMPRESA DEMO SA DE CV";

            
            

            conector_txt_inicio += " LugarExpedicion='"+codigo_postal+"' Sello=''>";

            if (dto_rfc.rfc == "XAXX010101000")
            {

                DateTime fecha_operacion = DateTime.Today;
                string mes = fecha_operacion.Month.ToString("D2");
                string anio = fecha_operacion.Year.ToString();
                conector_txt_inicio += "<cfdi:InformacionGlobal Periodicidad='01' Meses='" + mes + "' Año='" + anio + "' />";
            }

            conector_txt_inicio += " <cfdi:Emisor Rfc='" + rfc_emisor + "' Nombre='" + emisor_nombre + "' RegimenFiscal='601'/><cfdi:Receptor UsoCFDI='" + uso_del_cfdi + "' Rfc='" + dto_rfc.rfc + "' Nombre='" + dto_rfc.razon_social + "' DomicilioFiscalReceptor='" + dto_rfc.codigo_postal.ToString() + "' RegimenFiscalReceptor='" + regimen_fiscal + "' />";
            conector_txt_inicio += conector_txt;

            return conector_txt_inicio.ToString();
        
        }



		public DataTable ventas_por_facturar(long venta_id)
		{
			string sql = @"
				SELECT
					CAST(venta_id AS CHAR(50)) AS venta_id,
					rfc_registro_id AS rfc_registro_id,
					ventas.fecha_terminado AS fecha_terminado,
					CONCAT(rfc_registros.razon_social,' ( ',rfc_registros.rfc,' )') AS rfc
				FROM
					farmacontrol_local.ventas
				LEFT JOIN farmacontrol_global.rfc_registros USING(rfc_registro_id)
				WHERE
					venta_id NOT IN ( SELECT venta_id FROM farmacontrol_local.detallado_ventas_facturadas )
				AND
					venta_id LIKE @venta_id
				AND
					fecha_terminado IS NOT NULL
				AND
					corte_total_id IS NULL
				ORDER BY
					venta_id
				DESC
			";
			
			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("venta_id",venta_id+"%");
			
			conector.Select(sql,parametros);

			return conector.result_set;
		}

		public void limpiar_venta(long venta_id)
		{
			string sql= @"
				UPDATE
					farmacontrol_local.ventas
				SET
					cotizacion_id = NULL,
					traspaso_id = NULL,
					cliente_credito_id = NULL,
					cliente_domicilio_id = NULL,
                    comentarios = NULL
				WHERE
					venta_id = @venta_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("venta_id",venta_id);

			conector.Update(sql,parametros);

			sql = @"
				DELETE FROM
					farmacontrol_local.detallado_ventas
				WHERE
					venta_id = @venta_id
			";

			conector.Delete(sql,parametros);
		}

        public List<DTO_Detallado_ventas_existencia> validar_existencias_venta(long venta_id)
        {
            List<DTO_Detallado_ventas_existencia> lista = new List<DTO_Detallado_ventas_existencia>();

            string sql = string.Format(@"
						    SELECT
							    articulos.articulo_id AS articulo_id,
							    (
								    SELECT
									    IF( COUNT(amecop) > 1, CONCAT(ABS(amecop),'*'), ABS(amecop) ) AS amecop
								    FROM
									    farmacontrol_global.articulos_amecops
								    WHERE
									    articulos_amecops.articulo_id = articulos.articulo_id
								    ORDER BY articulos_amecops.amecop_principal DESC
								    LIMIT 1
							    ) AS amecop,
							    articulos.nombre,
                                CAST(detallado_ventas.caducidad AS CHAR(10)) AS caducidad,
                                detallado_ventas.lote AS lote,
                                detallado_ventas.cantidad AS cantidad,
							    IF(farmacontrol_global.articulos.afecta_existencias is TRUE, COALESCE(existencia, 0) - COALESCE(existencia_mermas, 0) - COALESCE(existencia_devoluciones, 0) - COALESCE(existencia_apartados, 0) - COALESCE(existencia_cambio_fisico, 0) - COALESCE(existencia_traspasos, 0) - COALESCE(existencia_mayoreo, 0) - COALESCE(existencia_ventas, 0) - COALESCE(existencia_parcial_prepago, 0), 1000) AS existencia_vendible
						    FROM
                                farmacontrol_local.detallado_ventas
							JOIN farmacontrol_global.articulos USING(articulo_id)
						    NATURAL LEFT JOIN
						    (
							    SELECT
								    detallado_ventas.articulo_id AS articulo_id,
								    detallado_ventas.caducidad As caducidad,
								    detallado_ventas.lote AS lote,
								    SUM(COALESCE(existencias.existencia, 0)) AS existencia
							    FROM
								    farmacontrol_local.detallado_ventas
                                LEFT JOIN farmacontrol_local.existencias USING(articulo_id,caducidad,lote)
                                WHERE
                                    detallado_ventas.venta_id = {0}
							    GROUP BY
								    detallado_ventas.articulo_id,detallado_ventas.caducidad,detallado_ventas.lote
						    ) AS tmp_existencias
						    NATURAL LEFT JOIN 
						    (
							    SELECT
								    detallado_ventas.articulo_id AS articulo_id,
								    detallado_ventas.caducidad As caducidad,
								    detallado_ventas.lote AS lote,
								    SUM(COALESCE(detallado_devoluciones.cantidad, 0)) AS existencia_devoluciones
							    FROM
                                    farmacontrol_local.detallado_ventas
                                LEFT JOIN farmacontrol_local.detallado_devoluciones USING(articulo_id,caducidad,lote)
							    LEFT JOIN farmacontrol_local.devoluciones USING(devolucion_id)
							    WHERE
								    devoluciones.fecha_terminado IS NULL
                                AND
                                    detallado_ventas.venta_id = {0}
							    GROUP BY  detallado_ventas.articulo_id,detallado_ventas.caducidad,detallado_ventas.lote

						    ) AS tmp_devoluciones
						    NATURAL LEFT JOIN 
						    (
							    SELECT
								    detallado_ventas.articulo_id AS articulo_id,
								    detallado_ventas.caducidad As caducidad,
								    detallado_ventas.lote AS lote,
								    SUM(COALESCE(new_table.cantidad, 0)) AS existencia_mermas
							    FROM
                                    farmacontrol_local.detallado_ventas
                                LEFT JOIN 
							    (
								    (
									    SELECT
										    UUID(),
										    detallado_ventas.articulo_id AS articulo_id,
								            detallado_ventas.caducidad As caducidad,
								            detallado_ventas.lote AS lote,
										    SUM(COALESCE(apartados.cantidad,0)) AS cantidad
									    FROM
                                            farmacontrol_local.detallado_ventas
										LEFT JOIN farmacontrol_local.apartados USING(articulo_id,caducidad,lote)
									    WHERE
										    apartados.tipo = 'MERMA'
                                        AND
                                            detallado_ventas.venta_id = {0}
									    GROUP BY detallado_ventas.articulo_id,detallado_ventas.caducidad,detallado_ventas.lote
								    )
								    UNION
								    (
									    SELECT
										    UUID(),
										    detallado_ventas.articulo_id AS articulo_id,
								            detallado_ventas.caducidad As caducidad,
								            detallado_ventas.lote AS lote,
										    COALESCE(detallado_mermas.cantidad, 0) AS cantidad
									    FROM
                                            farmacontrol_local.detallado_ventas
										LEFT JOIN farmacontrol_local.detallado_mermas USING(articulo_id, caducidad, lote)
									    JOIN farmacontrol_local.mermas USING(merma_id)
									    WHERE
										    mermas.fecha_terminado IS NULL
                                        AND
                                            detallado_ventas.venta_id = {0}
								    )
							    ) AS new_table USING(articulo_id,caducidad,lote)
                                WHERE
                                    detallado_ventas.venta_id = {0}
							    GROUP BY
								    detallado_ventas.articulo_id,detallado_ventas.caducidad,detallado_ventas.lote
						    ) AS tmp_mermas
						    NATURAL LEFT JOIN 
						    (
							    SELECT
								    detallado_ventas.articulo_id AS articulo_id,
								    detallado_ventas.caducidad As caducidad,
								    detallado_ventas.lote AS lote,
								    SUM(COALESCE(apartados.cantidad, 0)) AS existencia_apartados
							    FROM
                                    farmacontrol_local.detallado_ventas    
								LEFT JOIN farmacontrol_local.apartados USING(articulo_id,caducidad,lote)
							    WHERE
								    tipo = 'SUCURSAL'
                                AND
                                    detallado_ventas.venta_id = {0}
							    GROUP BY detallado_ventas.articulo_id,detallado_ventas.caducidad,detallado_ventas.lote
						    ) AS tmp_apartados
						    NATURAL LEFT JOIN 
						    (
							    SELECT
								    detallado_ventas.articulo_id AS articulo_id,
								    detallado_ventas.caducidad As caducidad,
								    detallado_ventas.lote AS lote,
								    SUM(COALESCE(apartados.cantidad, 0)) AS existencia_parcial_prepago
							    FROM
                                    farmacontrol_local.detallado_ventas
								LEFT JOIN farmacontrol_local.apartados USING(articulo_id,caducidad,lote)
							    WHERE
								    tipo = 'ENTREGA_PARCIAL_PREPAGO'
                                AND
                                    detallado_ventas.venta_id = {0}
							    GROUP BY detallado_ventas.articulo_id,detallado_ventas.caducidad,detallado_ventas.lote
						    ) AS tmp_entrega_parcial_prepago
						    NATURAL LEFT JOIN 
						    (
							    SELECT
								    detallado_ventas.articulo_id AS articulo_id,
								    detallado_ventas.caducidad As caducidad,
								    detallado_ventas.lote AS lote,
								    SUM(COALESCE(apartados.cantidad, 0)) AS existencia_cambio_fisico
							    FROM
                                    farmacontrol_local.detallado_ventas
								LEFT JOIN farmacontrol_local.apartados USING(articulo_id,caducidad,lote)
							    WHERE
								    tipo = 'CAMBIO_FISICO'
                                AND
                                    detallado_ventas.venta_id = {0}
							    GROUP BY detallado_ventas.articulo_id,detallado_ventas.caducidad,detallado_ventas.lote
						    ) AS tmp_cambio_fisico
						    NATURAL LEFT JOIN 
						    (
							    SELECT
								    detallado_ventas.articulo_id AS articulo_id,
								    detallado_ventas.caducidad As caducidad,
								    detallado_ventas.lote AS lote,
								    SUM(COALESCE(detallado_traspasos.cantidad,0)) AS existencia_traspasos
							    FROM
                                    farmacontrol_local.detallado_ventas
								LEFT JOIN farmacontrol_local.detallado_traspasos USING(articulo_id, caducidad, lote)
							    JOIN farmacontrol_local.traspasos USING(traspaso_id)
							    WHERE
								    farmacontrol_local.traspasos.remote_id IS NULL
                                AND
                                    detallado_ventas.venta_id = {0}
							    GROUP BY detallado_ventas.articulo_id,detallado_ventas.caducidad,detallado_ventas.lote
						    ) AS tmp_traspasos
						    NATURAL LEFT JOIN 
						    (
							    SELECT
								    detallado_ventas.articulo_id AS articulo_id,
								    detallado_ventas.caducidad As caducidad,
								    detallado_ventas.lote AS lote,
								    SUM(cantidad) AS existencia_ventas
							    FROM
								    farmacontrol_local.detallado_ventas
							    JOIN farmacontrol_local.ventas USING(venta_id)
							    WHERE
								    farmacontrol_local.ventas.fecha_terminado IS NULL
							    AND
								    detallado_ventas.venta_id != {0}
							    GROUP BY detallado_ventas.articulo_id,detallado_ventas.caducidad,detallado_ventas.lote
						    ) AS tmp_ventas
						    NATURAL LEFT JOIN 
						    (
							    SELECT
								    detallado_ventas.articulo_id AS articulo_id,
								    detallado_ventas.caducidad As caducidad,
								    detallado_ventas.lote AS lote,
								    SUM(COALESCE(detallado_mayoreo_ventas.cantidad, 0)) + (IF(COALESCE(detallado_mayoreo_ventas.cantidad, 0) = 0,SUM(cantidad_revision),0)) AS existencia_mayoreo
							    FROM
                                    farmacontrol_local.detallado_ventas
								LEFT JOIN farmacontrol_local.detallado_mayoreo_ventas USING(articulo_id,caducidad,lote)
							    JOIN farmacontrol_local.mayoreo_ventas USING(mayoreo_venta_id)
							    WHERE
								    mayoreo_ventas.fecha_fin_verificacion IS NULL
                                AND
                                    detallado_ventas.venta_id = {0}
							    GROUP BY detallado_ventas.articulo_id,detallado_ventas.caducidad,detallado_ventas.lote
						    ) AS tmp_mayoreo
                            WHERE
                                 detallado_ventas.venta_id = {0}
						    GROUP BY detallado_ventas.articulo_id, detallado_ventas.caducidad, detallado_ventas.lote
						    ", venta_id);
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            conector.Select(sql, parametros);

            if(conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {
                    DTO_Detallado_ventas_existencia tmp = new DTO_Detallado_ventas_existencia();
                    tmp.amecop = row["amecop"].ToString();
                    tmp.nombre = row["nombre"].ToString();
                    tmp.caducidad = row["caducidad"].ToString();
                    tmp.lote = row["lote"].ToString();
                    tmp.cantidad = Convert.ToInt64(row["cantidad"]);
                    tmp.existencia_vendible = Convert.ToInt64(row["existencia_vendible"]);

                    lista.Add(tmp);
                }
            }

            return lista;
        }

		public void importar_cotizacion(long venta_id, long cotizacion_id)
		{
			//string sql = "farmacontrol_global.Ventas_importar_cotizacion";
            string sql = @"
				SELECT
					(
						SELECT 
							amecop
						FROM 
							farmacontrol_global.articulos_amecops 
						WHERE 
							articulo_id = farmacontrol_local.detallado_cotizaciones.articulo_id
						ORDER BY
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS amecop,
					CAST(caducidad AS CHAR(50)) AS caducidad,
					lote,
					cantidad,
					es_promocion
				FROM
					farmacontrol_local.detallado_cotizaciones
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					cotizacion_id = @cotizacion_id
			";
            

			Dictionary<string,object> parametros = new Dictionary<string,object>();
            parametros.Add("cotizacion_id", cotizacion_id);

			//conector.Call(sql,parametros);
            conector.Select(sql, parametros);

			var detallado_cotizaciones = conector.result_set;

			foreach(DataRow row in detallado_cotizaciones.Rows)
			{
				string amecop = row["amecop"].ToString();
				string caducidad = row["caducidad"].ToString();
				string lote = row["lote"].ToString();
				int cantidad = Convert.ToInt32(row["cantidad"]);
				bool es_promocion = Convert.ToBoolean(row["es_promocion"]);

				insertar_detallado(amecop,caducidad,lote,cantidad,venta_id,es_promocion);
			}
            //cotizacion_id = @par_cotizacion_id
			sql = @"
				SELECT
					cliente_credito_id AS cliente_credito_id,
					cliente_domicilio_id AS cliente_domicilio_id,
                    comentarios
				FROM
					farmacontrol_local.cotizaciones
				WHERE
					cotizacion_id = @cotizacion_id
			";

			parametros.Add("venta_id", venta_id);

			conector.Select(sql,parametros);
			
			var informacion_cotizacion = conector.result_set;

			sql = @"
				UPDATE
					farmacontrol_local.ventas
				SET
					cliente_credito_id = @cliente_credito_id,
					cliente_domicilio_id = @cliente_domicilio_id,
					cotizacion_id = @cotizacion_id,
                    comentarios = @comentarios
				WHERE
					venta_id = @venta_id
			";

			var row_cot = informacion_cotizacion.Rows[0];

			parametros.Add("cliente_credito_id", (row_cot["cliente_credito_id"].ToString().Equals("")) ? null : row_cot["cliente_credito_id"].ToString());
			parametros.Add("cliente_domicilio_id", (row_cot["cliente_domicilio_id"].ToString().Equals("")) ? null : row_cot["cliente_domicilio_id"].ToString());
            parametros.Add("comentarios",row_cot["comentarios"].ToString());

			conector.Update(sql,parametros);
		}

		public DataTable update_cantidad(long venta_id, long detallado_venta_id, long cantidad)
		{
            //total   = (importe * @cantidad) + ((importe * @cantidad) * pct_iva)
			string sql = @"
				UPDATE
					farmacontrol_local.detallado_ventas
				SET
					cantidad = @cantidad,
					subtotal = (importe * @cantidad),
					importe_iva = ((importe * @cantidad) * pct_iva),
					importe_ieps =  ((precio_publico - (precio_publico * pct_descuento)) * ieps)*@cantidad,
                    total = ( ((precio_publico - ( precio_publico * pct_descuento ) ) * @cantidad ) + (((precio_publico - ( precio_publico * pct_descuento )) * @cantidad) * pct_iva ) ) +  ((precio_publico - (precio_publico * pct_descuento)) * ieps)*@cantidad 

				WHERE
					detallado_venta_id = @detallado_venta_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("cantidad", cantidad);
			parametros.Add("detallado_venta_id", detallado_venta_id);

			conector.Update(sql, parametros);

			return get_productos_venta(venta_id);
		}

		public DataTable eliminar_detallado_venta(int detallado_venta_id, long venta_id)
		{
			string sql = @"
				DELETE FROM
					farmacontrol_local.detallado_ventas
				WHERE
					detallado_venta_id = @detallado_venta_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("detallado_venta_id", detallado_venta_id);

			conector.Select(sql, parametros);

			return get_productos_venta(venta_id);
		}

		public void eliminar_producto_gratis(int articulo_id, long venta_id)
		{
			string sql = @"
				DELETE FROM
					farmacontrol_local.detallado_ventas
				WHERE
					articulo_id = @articulo_id
				AND
					venta_id = @venta_id
				AND
					es_promocion = 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("articulo_id", articulo_id);
			parametros.Add("venta_id", venta_id);

			conector.Delete(sql, parametros);
		}

		public int get_productos_sin_promocion(int articulo_id, long venta_id)
		{
			string sql = @"
				SELECT
					COALESCE(SUM(cantidad),0) AS articulos_sin_promocion
				FROM
					farmacontrol_local.detallado_ventas
				WHERE
					articulo_id = @articulo_id
				AND
					venta_id = @venta_id
				AND
					es_promocion = 0
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("articulo_id", articulo_id);
			parametros.Add("venta_id", venta_id);

			conector.Select(sql, parametros);

			var result_query = conector.result_set;

			if (result_query.Rows.Count > 0)
			{
				return Convert.ToInt32(result_query.Rows[0]["articulos_sin_promocion"]);
			}

			return 0;
		}

		public DataTable insertar_detallado(string amecop, string caducidad, string lote, long cantidad, long? venta_id, bool es_promocion = false)
		{
			//string pct_descuento = "pct_descuento";

			string sql = string.Format(@"
				INSERT INTO
					farmacontrol_local.detallado_ventas
					(
						SELECT
							0 as detallado_venta_id,
							@venta_id AS cotizacion_id,
							articulo_id,
							@caducidad AS caducidad,
							@lote AS lote,
							precio_publico,
							{0},
							(precio_publico * {0}) AS importe_descuento,
							precio_publico - (precio_publico * {0}) AS importe,
						
							@cantidad AS cantidad,
	
							(precio_publico - (precio_publico * {0})) * @cantidad AS subtotal,
							pct_iva,
							((precio_publico - (precio_publico * {0})) * @cantidad) * pct_iva AS importe_iva,
							tipo_ieps,
							ieps,
							IF(tipo_ieps = 'PCT', ((precio_publico - (precio_publico * {0})) * ieps) * @cantidad , ieps) AS importe_ieps,
							((precio_publico - (precio_publico * {0})) * @cantidad) + (  ((precio_publico - (precio_publico * {0})) * @cantidad) * ieps ) + (((precio_publico - (precio_publico * {0})) * @cantidad) + (  ((precio_publico - (precio_publico * {0})) * @cantidad) * ieps ) )*pct_iva  AS total,
							@es_promocion AS es_promocion,
							'' AS comentarios,
							NOW() AS modified
						FROM
							farmacontrol_global.articulos
						LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id)
						WHERE
							articulos_amecops.amecop = @amecop	
					)
				ON DUPLICATE KEY UPDATE
					cantidad = cantidad + @cantidad,
					subtotal = subtotal + ((articulos.precio_publico - (articulos.precio_publico * {0})) * @cantidad),
					importe_iva = importe_iva + (((articulos.precio_publico - (articulos.precio_publico * {0})) * @cantidad) * articulos.pct_iva),
                    importe_ieps = importe_ieps + IF(articulos.tipo_ieps = 'PCT', ((articulos.precio_publico - (articulos.precio_publico * {0})) * articulos.ieps) * @cantidad , articulos.ieps),
					total = total + ((articulos.precio_publico - (articulos.precio_publico * {0})) * @cantidad) + (  ((articulos.precio_publico - (articulos.precio_publico * {0})) * @cantidad) * articulos.ieps ) + (((articulos.precio_publico - (articulos.precio_publico * {0})) * @cantidad) + (  ((articulos.precio_publico - (articulos.precio_publico * {0})) * @cantidad) * articulos.ieps ) )*articulos.pct_iva 
			",
                (es_promocion) ? "1" : "articulos.pct_descuento");

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("venta_id", venta_id);
			parametros.Add("amecop", amecop);
			parametros.Add("caducidad", caducidad);
			parametros.Add("lote", lote);
			parametros.Add("cantidad", cantidad);
			parametros.Add("es_promocion", es_promocion);

			conector.Insert(sql, parametros);

			return get_productos_venta((long)venta_id);
		}

        public bool insertar_formula_detallado(long venta_id, long articulo_id, string formula_id)
        {
            string sql = @"
        INSERT INTO
            farmacontrol_local.detallado_ventas
        (
            SELECT
                0 AS detallado_venta_id,
                @venta_id AS venta_id,
                @articulo_id AS articulo_id,
                '0000-00-00' AS caducidad,
                ' ' AS lote,
                SUM(subtotal) precio_publico,
                0 AS pct_descuento,
                0 AS importe_descuento,
                SUM(subtotal) AS importe,
                1 AS cantidad,
                SUM(subtotal) AS subtotal,
                @pct_iva AS pct_iva,
                (SUM(subtotal) * @pct_iva) AS importe_iva,
                'PES' AS tipo_ieps,
                0 AS ieps,
                0 AS importe_ieps,
                (SUM(subtotal) + (SUM(subtotal) * @pct_iva)) AS total,
                0 AS es_promocion,
                '' AS comentarios,
                NOW() AS modified
            FROM
                farmacontrol_global.detallado_formulas
            JOIN farmacontrol_global.formulas USING(formula_id)
            WHERE
                formulas.sucursal_folio = @formula_id
            AND
                formulas.sucursal_id = @sucursal_id
        )
    ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("venta_id", venta_id);
            parametros.Add("formula_id", formula_id);
            parametros.Add("articulo_id", articulo_id);
            parametros.Add("sucursal_id", Convert.ToInt32(Config_helper.get_config_local("sucursal_id")));
            parametros.Add("pct_iva", Convert.ToDecimal(Config_helper.get_config_global("pct_iva")));

            try
            {
                conector.Insert(sql, parametros);
                return conector.insert_id > 0;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Exception($"Error al insertar fórmula con artículo_id={articulo_id}, fórmula_id={formula_id}. Detalle: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al insertar fórmula. Artículo_id={articulo_id}, fórmula_id={formula_id}. Detalle: {ex.Message}", ex);
            }
        }


        public DataTable get_productos_venta(long venta_id)
		{
			DataTable result_query = new DataTable();

			try
			{
                //string sql = "Ventas_get_productos_venta";
                string sql = @"
					SELECT 
						detallado_venta_id,
						articulos.articulo_id AS articulo_id,
						es_promocion,
						(
							SELECT
								amecop
							FROM
								farmacontrol_global.articulos_amecops
							WHERE
								articulos_amecops.articulo_id = detallado_ventas.articulo_id
							ORDER BY articulos_amecops.amecop_principal DESC
							LIMIT 1
						) AS amecop,
						articulos.nombre,
						CAST(caducidad AS CHAR(10)) AS caducidad,
						CAST(caducidad AS CHAR(10)) AS caducidad_sin_formato,
						lote,
						farmacontrol_local.detallado_ventas.precio_publico AS precio_publico,
						farmacontrol_local.detallado_ventas.pct_descuento,
						importe_descuento AS importe_descuento,
						importe AS importe,
						cantidad,
						subtotal AS subtotal,
						farmacontrol_local.detallado_ventas.pct_iva,
						farmacontrol_local.detallado_ventas.importe_ieps,
						importe_iva AS importe_iva,
						farmacontrol_local.detallado_ventas.total AS total,
                        IF(articulos.clase_antibiotico_id IS NULL, 0, 1) AS es_antibiotico
					FROM
						farmacontrol_local.detallado_ventas
					LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
					LEFT JOIN farmacontrol_local.ventas USING(venta_id)
					WHERE
						farmacontrol_local.detallado_ventas.venta_id = @venta_id
					GROUP BY
						detallado_venta_id
					ORDER BY
						farmacontrol_local.detallado_ventas.detallado_venta_id
					DESC
				 ";
                
				Dictionary<string, object> parametros = new Dictionary<string, object>();
				parametros.Add("venta_id", venta_id);


				conector.Select(sql, parametros);

				result_query = conector.result_set;

				if (result_query.Rows.Count > 0)
				{
					for (int i = 0; i < result_query.Rows.Count; i++)
					{
						if (result_query.Rows[i]["caducidad"].ToString() != " ")
						{
							result_query.Rows[i]["caducidad"] = HELPERS.Misc_helper.fecha(result_query.Rows[i]["caducidad"].ToString(),"CADUCIDAD");
						}
					}
				}

			}
			catch (Exception exception)
			{
				Log_error.log(exception);
			}

			return result_query;
		}

		public DTO_Validacion desasociar_facturacion(long venta_id)
		{
			DTO_Validacion validacion = new DTO_Validacion();
			validacion.status = false;
			validacion.informacion = "Ocurrio un problema al intentar desasociar la facturación, intentelo mas tarde";

			string sql = @"
				UPDATE
					farmacontrol_local.ventas
				SET
					rfc_registro_id = NULL
				WHERE
					venta_id = @venta_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("venta_id", venta_id);

			conector.Update(sql, parametros);

			if (conector.filas_afectadas > 0)
			{
				validacion.status = true;
				validacion.informacion = "Se desasoció la facturacion correctamente";
			}

			return validacion;
		}

		public int registrar_rfc(long venta_id, string rfc_registro_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.ventas
				SET
					rfc_registro_id = @rfc_registro_id
				WHERE
					venta_id = @venta_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("rfc_registro_id", rfc_registro_id);
			parametros.Add("venta_id", venta_id);

			conector.Update(sql, parametros);

			return conector.filas_afectadas;
		}

		public DTO_Validacion desasociar_cliente_credito(long venta_id)
		{
			DTO_Validacion validacion = new DTO_Validacion();
			validacion.status = false;
			validacion.informacion = "Ocurrio un problema al intentar eliminar el credito, intentelo mas tarde";

			string sql = @"
				UPDATE
					farmacontrol_local.ventas
				SET
					cliente_credito_id = NULL
				WHERE
					venta_id = @venta_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("venta_id", venta_id);

			conector.Update(sql, parametros);

			if (conector.filas_afectadas > 0)
			{
				validacion.status = true;
				validacion.informacion = "Se elimino el credito correctamente";
			}

			return validacion;
		}

		public int registrar_cliente_credito(long venta_id, string cliente_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.ventas
				SET
					cliente_credito_id = @cliente_id
				WHERE
					venta_id = @venta_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("cliente_id", cliente_id);
			parametros.Add("venta_id", venta_id);

			conector.Update(sql, parametros);

			return conector.filas_afectadas;
		}

		public DTO_Validacion desasocisar_cliente_domicilio(long venta_id)
		{
			DTO_Validacion validacion = new DTO_Validacion();
			validacion.status = false;
			validacion.informacion = "Ocurrió un problema al intentar eliminar el domicilio, intentelo mas tarde";

			string sql = @"
				UPDATE
					farmacontrol_local.ventas
				SET
					cliente_domicilio_id = NULL
				WHERE
					venta_id = @venta_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("venta_id", venta_id);

			conector.Update(sql, parametros);

			if (conector.filas_afectadas > 0)
			{
				validacion.status = true;
				validacion.informacion = "Se elimino el domicilio correctamente";
			}

			return validacion;
		}

		public int registrar_cliente_domicilio(long venta_id, string cliente_domicilio_id)
		{
			try
			{
				string sql = @"
					UPDATE
						farmacontrol_local.ventas
					SET
						cliente_domicilio_id = @cliente_domicilio_id
					WHERE
						venta_id = @venta_id
				";

				Dictionary<string, object> parametros = new Dictionary<string, object>();
				parametros.Add("cliente_domicilio_id", cliente_domicilio_id);
				parametros.Add("venta_id", venta_id);

				conector.Update(sql, parametros);
			}
			catch (Exception exception)
			{
				Log_error.log(exception);
			}

			return conector.filas_afectadas;
		}

		public DTO_Venta get_venta_data(long venta_id)
		{
			DTO_Venta dto_venta = new DTO_Venta();

            //string sql = "Ventas_get_venta_data";
            string sql = @"
                SELECT
					venta_id,
					terminal_id,
					venta_folio,
					ventas.empleado_id AS empleado_id,
					cotizacion_id,
					traspaso_id,
					cliente_credito_id AS cliente_credito_id,
					cliente_domicilio_id AS cliente_domicilio_id,
					cupon_id,
					corte_parcial_id,
					corte_total_id,
					fecha_creado,
					fecha_terminado,
					fecha_facturado,
					ventas.comentarios AS comentarios,
					(SELECT nombre FROM farmacontrol_global.clientes WHERE cliente_id = cliente_credito_id) as nombre_cliente_credito,
					CONCAT(clientes.nombre,' ','( ', CONCAT_WS(' ',clientes_domicilios.calle, CONCAT('#',REPLACE(clientes_domicilios.numero_exterior, '#','')),clientes_domicilios.numero_interior, clientes_domicilios.colonia, clientes_domicilios.municipio, clientes_domicilios.ciudad),' )') AS servicio_domicilio,
					empleados.nombre AS nombre_empleado,
					empleados.fcid AS empleado_fcid
				FROM
					farmacontrol_local.ventas
				LEFT JOIN farmacontrol_global.empleados USING(empleado_id)
				LEFT JOIN farmacontrol_global.clientes_domicilios USING(cliente_domicilio_id)
				LEFT JOIN farmacontrol_global.clientes USING(cliente_id)
				WHERE
					venta_id = @venta_id
			";
            
			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("venta_id",venta_id);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				var row = conector.result_set.Rows[0];
				dto_venta.venta_id = Convert.ToInt64(row["venta_id"]);
				dto_venta.terminal_id = Convert.ToInt64(row["terminal_id"]);
				dto_venta.venta_folio = Convert.ToInt64(row["venta_folio"]);
				dto_venta.empleado_id = Convert.ToInt64(row["empleado_id"]);

				long? long_null = null;

				dto_venta.cotizacion_id = (row["cotizacion_id"].ToString().Equals("")) ? long_null : Convert.ToInt64(row["cotizacion_id"]);
				dto_venta.traspaso_id = (row["traspaso_id"].ToString().Equals("")) ? long_null : Convert.ToInt64(row["traspaso_id"]);
				dto_venta.cliente_credito_id = row["cliente_credito_id"].ToString();
				dto_venta.cliente_domicilio_id = row["cliente_domicilio_id"].ToString();
				dto_venta.cupon_id = (row["cupon_id"].ToString().Equals("")) ? long_null : Convert.ToInt64(row["cupon_id"]);
				dto_venta.corte_parcial_id = (row["corte_parcial_id"].ToString().Equals("")) ? long_null : Convert.ToInt64(row["corte_parcial_id"]);
				dto_venta.corte_total_id = (row["corte_total_id"].ToString().Equals("")) ? long_null : Convert.ToInt64(row["corte_total_id"]);

				DateTime? date_null = null;

				dto_venta.fecha_creado = (row["fecha_creado"].ToString().Equals("")) ? date_null :Convert.ToDateTime(row["fecha_creado"]) ;
				dto_venta.fecha_terminado = (row["fecha_terminado"].ToString().Equals("")) ? date_null : Convert.ToDateTime(row["fecha_terminado"]);
				dto_venta.fecha_facturada = (row["fecha_facturado"].ToString().Equals("")) ? date_null : Convert.ToDateTime(row["fecha_facturado"]);

				dto_venta.comentarios = row["comentarios"].ToString();
				dto_venta.nombre_cliente_credito = row["nombre_cliente_credito"].ToString();
				dto_venta.servicio_domicilio = row["servicio_domicilio"].ToString();
				dto_venta.nombre_empleado = row["nombre_empleado"].ToString();
				dto_venta.empleado_fcid = row["empleado_fcid"].ToString();
			}

			return dto_venta;
		}

		public DataTable get_venta_pendiente()
		{
			string sql = @"
				SELECT
					venta_id
				FROM
					farmacontrol_local.ventas
				WHERE
					fecha_terminado IS NULL
				AND
					terminal_id = @terminal_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("terminal_id",terminal_id);

			conector.Select(sql,parametros);

			return conector.result_set;
		}

		public long registrar_venta(int empleado_id, bool tomar_empleado_sesion = true)
		{
            string sql = "";
			var venta_pendiente = get_venta_pendiente();

			if(venta_pendiente.Rows.Count > 0)
			{
				return Convert.ToInt64(venta_pendiente.Rows[0]["venta_id"]);
			}

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            if (tomar_empleado_sesion == false)
            {
                sql = @"
                    SELECT
                        empleado_id
                    FROM
                        farmacontrol_local.ventas
                    WHERE
                        fecha_terminado IS NOT NULL
                    AND
                        terminal_id = @terminal_id
                    ORDER BY
                        venta_id DESC
                    LIMIT 1
                ";

                parametros = new Dictionary<string, object>();
                parametros.Add("terminal_id", terminal_id);

                conector.Select(sql, parametros);

                if (conector.result_set.Rows.Count > 0)
                {
                    empleado_id = Convert.ToInt32(conector.result_set.Rows[0]["empleado_id"]);
                }
            }

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

			parametros = new Dictionary<string,object>();
			parametros.Add("empleado_id",empleado_id);
			parametros.Add("terminal_id",terminal_id);

			conector.Insert(sql,parametros);

			return conector.insert_id;
		}

        public Decimal get_montos_venta( long venta_id)
        {
            string sql = @"
				SELECT
                   SUM( total ) as monto
                FROM
                   farmacontrol_local.ventas
                INNER JOIN
                    farmacontrol_local.detallado_ventas
                USING(venta_id)
                WHERE
                  venta_id = @venta_id
					
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("venta_id", venta_id);

            conector.Select(sql, parametros);

            var result_query = conector.result_set;

            if (result_query.Rows.Count > 0)
            {
                return Convert.ToDecimal(result_query.Rows[0]["monto"]);
            }

            return 0;
        }

        public Decimal get_tipo_cambio()
        {

            string sql = @"
				SELECT
                   valor
                FROM
                   farmacontrol_global.config
                WHERE
                   nombre = 'tipo_cambio_dolar'
				LIMIT 1	
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
          
            conector.Select(sql, parametros);

            var result_query = conector.result_set;

            if (result_query.Rows.Count > 0)
            {
                return Convert.ToDecimal(result_query.Rows[0]["valor"]);
            }

            return 0;

        }


        public List<DTO_Reporte_articulo_vendidos> get_productos_vendidos(string fecha_inicial,string fecha_final)
        {
            List<DTO_Reporte_articulo_vendidos> lista = new List<DTO_Reporte_articulo_vendidos>();

            string sql = @"
                SELECT
                   articulo_id,
                   amecop_original,
                   nombre, 
                   ABS(SUM(cantidad)) AS vendido,
                   MAX(fecha_date) AS fecha_date
                FROM
                   farmacontrol_local.kardex
                LEFT JOIN 
                   farmacontrol_global.articulos
                USING(articulo_id)
                WHERE
                   fecha_date BETWEEN DATE(@fecha_inicial) AND DATE(@fecha_final)
                AND 
                   tipo = 'VENTA'	
                GROUP BY 
                   articulo_id  
                ORDER BY 
                    nombre
            ";

            Dictionary<string, object> parameros = new Dictionary<string, object>();

            parameros.Add("fecha_inicial", fecha_inicial);
            parameros.Add("fecha_final", fecha_final);

            conector.Select(sql, parameros);

            foreach (DataRow row in conector.result_set.Rows)
            {
                DTO_Reporte_articulo_vendidos tmp = new DTO_Reporte_articulo_vendidos();
                tmp.articulo_id = Convert.ToInt64(row["articulo_id"]);
                tmp.amecop_original = row["amecop_original"].ToString();
                tmp.nombre = row["nombre"].ToString();
                tmp.vendido = Convert.ToInt64(row["vendido"]);
                tmp.fecha_date = Convert.ToDateTime(row["fecha_date"]).ToString("dd-MMM-yy").Replace(".", "").ToUpper();
                lista.Add(tmp);
            }

            return lista;
        }

        public bool is_venta_antibiotico(long venta_id)
        { 
            bool venta_anti = false;

            string sql = @"
			    SELECT
			        articulo_id
			    FROM
			       farmacontrol_local.detallado_ventas
			    INNER JOIN 
			       farmacontrol_global.articulos
			    USING(articulo_id)
			    WHERE
			       venta_id = @venta_id		
			    AND  
			       clase_antibiotico_id IS NOT NULL
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("venta_id", venta_id);

            conector.Select(sql, parametros);

            var result_query = conector.result_set;

            if (result_query.Rows.Count > 0)
            {
                venta_anti = true;
            }

            return venta_anti;
        }


        public DataTable get_productosunicos_venta(long venta_id)
        {
            DataTable result_query = new DataTable();

            try
            {
                
                string sql = @"
					SELECT
                       farmacontrol_local.detallado_ventas.articulo_id as articulo_id,
                       CAST( amecop_original AS UNSIGNED ) AS  codigo,
                       nombre as descripcion,
                       REPLACE( FORMAT( ( SUM(total) / SUM(cantidad) ),2 ),',','')  as precio_unitario,
                       SUM(cantidad) as cantidad,
                       SUM(total) as importe
                    FROM
                       farmacontrol_local.ventas
                    INNER JOIN
	                    farmacontrol_local.detallado_ventas
                    USING(venta_id) 
                    INNER JOIN 
                        farmacontrol_global.articulos
                    USING(articulo_id)
                    WHERE
                       venta_id = @venta_id
                    GROUP BY 
                        articulo_id
				 ";

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("venta_id", venta_id);

                conector.Select(sql, parametros);

                if (conector.result_set.Rows.Count > 0)
                {
                    result_query = conector.result_set;                 
                }

            }
            catch (Exception exception)
            {
                Log_error.log(exception);
            }

            return result_query;
        }

        public long get_venta_idxfolio(string venta_folio)
        {

            string sql = @"
				SELECT
                    venta_id	
                FROM
                  farmacontrol_local.ventas
                WHERE
                  venta_folio = @venta_folio
                LIMIT 1  
				
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("venta_folio", venta_folio);

            conector.Select(sql, parametros);

            var result_query = conector.result_set;

            if (result_query.Rows.Count > 0)
            {
                return Convert.ToInt32(result_query.Rows[0]["venta_id"]);
            }

            return 0;
        }

	}
}
