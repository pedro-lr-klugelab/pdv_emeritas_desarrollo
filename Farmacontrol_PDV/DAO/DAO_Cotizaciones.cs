using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.FORMS.comunes;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Cotizaciones : IDisposable
	{
		Conector conector = new Conector();

		bool disposed = false;

        public void set_fecha_cerrado_null(long cotizacion_id)
        {
            string sql = @"
                UPDATE
                    farmacontrol_local.cotizaciones
                SET
                    fecha_cerrado = NULL
                WHERE
                    cotizacion_id = @cotizacion_id
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("cotizacion_id",cotizacion_id);

            conector.Update(sql, parametros);
        }

        public bool es_cotizacion_venta(long cotizacion_id)
        {
            //string sql = "farmacontrol_global.Cotizaciones_es_cotizacion_venta";
            string sql = @"
                SELECT
                    venta_id
                FROM
                    farmacontrol_local.ventas
                WHERE
                    cotizacion_id = @cotizacion_id
            ";
            
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("cotizacion_id", cotizacion_id);

            conector.Select(sql, parametros);
            //conector.Call(sql, parametros);

            return Convert.ToBoolean(conector.result_set.Rows.Count > 0);
        }

        public bool es_cotizacion_terminada(long cotizacion_id)
        {
            //string sql = "farmacontrol_global.Cotizaciones_es_cotizacion_terminada";
            
            string sql = @"
                SELECT
                    IF(fecha_cerrado IS NOT NULL, 1, 0) puede_reabrir
                FROM
                    farmacontrol_local.cotizaciones
                WHERE
                    cotizacion_id = @cotizacion_id
            ";
            
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("cotizacion_id", cotizacion_id);

            conector.Select(sql, parametros);
            //conector.Call(sql, parametros);

            return Convert.ToBoolean(conector.result_set.Rows[0]["puede_reabrir"]);
        }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposed)
				return;

			if (disposing)
			{
			}

			disposed = true;
		}

        public DataTable eliminar_cotizacion(long cotizacion_id)
        {
            string sql = @"
                DELETE FROM
                    farmacontrol_local.detallado_cotizaciones
                WHERE
                    cotizacion_id = @cotizacion_id
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("cotizacion_id",cotizacion_id);

            conector.Delete(sql, parametros);

            sql = @"
                DELETE FROM
                    farmacontrol_local.cotizaciones
                WHERE
                    cotizacion_id = @cotizacion_id
            ";

            conector.Delete(sql, parametros);

            return get_cotizaciones_venta();
        }

        public List<DTO_Detallado_existencia_cotizacion> valida_existencia_cotizacion(long cotizacion_id)
        {
            List<DTO_Detallado_existencia_cotizacion> detallado_cotizacion = new List<DTO_Detallado_existencia_cotizacion>();

            //string sql = "farmacontrol_global.Cotizaciones_valida_existencia_cotizacion";
            
            string sql = @"
                SELECT
                    (
                        SELECT
                           amecop
                        FROM
                            farmacontrol_global.articulos_amecops
                        WHERE
                            articulos_amecops.articulo_id = detallado_cotizaciones.articulo_id
                        ORDER BY articulos_amecops.amecop_principal DESC
                        LIMIT 1
                   ) AS amecop,
                    articulos.nombre AS producto,
                    detallado_cotizaciones.cantidad,
                    COALESCE(existencias.existencia, 0) AS existencia_vendible
                FROM
                    farmacontrol_local.detallado_cotizaciones
                LEFT JOIN farmacontrol_local.existencias USING(articulo_id, caducidad, lote)
                JOIN farmacontrol_global.articulos USING(articulo_id)
                WHERE
                    cotizacion_id = @cotizacion_id
            ";
            
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("cotizacion_id", cotizacion_id);

            conector.Select(sql, parametros);
            //conector.Call(sql, parametros);

            if(conector.result_set.Rows.Count > 0)
            {
                foreach(DataRow row in conector.result_set.Rows)
                {
                    DTO_Detallado_existencia_cotizacion det = new DTO_Detallado_existencia_cotizacion();
                    det.amecop = row["amecop"].ToString();
                    det.producto = row["producto"].ToString();
                    det.cantidad = Convert.ToInt64(row["cantidad"]);
                    det.existencia_vendible = Convert.ToInt64(row["existencia_vendible"]);

                    detallado_cotizacion.Add(det);
                }
            }

            return detallado_cotizacion;
        }

		public DataTable get_cotizaciones_venta()
		{
            //string sql = "farmacontrol_global.Cotizaciones_get_cotizaciones_venta";
            //CAST(cotizacion_id as CHAR(50)) AS cotizacion_id,
			string sql = @"
				SELECT
					cotizacion_id,
					empleados.nombre AS empleado,
                    clientes.nombre AS cliente,
                    SUM(detallado_cotizaciones.total) AS total,
					fecha_creado as fecha
				FROM
					farmacontrol_local.cotizaciones
                LEFT JOIN
                    farmacontrol_local.detallado_cotizaciones 
                USING(cotizacion_id)
				LEFT JOIN 
                     farmacontrol_global.empleados
                USING(empleado_id)
                LEFT JOIN 
                     farmacontrol_global.clientes_domicilios 
                USING(cliente_domicilio_id) 
                LEFT JOIN 
                     farmacontrol_global.clientes 
                ON clientes.cliente_id = clientes_domicilios.cliente_id
				WHERE
					fecha_cerrado IS NOT NULL
				AND
					cotizacion_id NOT IN( SELECT cotizacion_id FROM farmacontrol_local.ventas WHERE cotizacion_id IS NOT NULL )
                GROUP BY cotizaciones.cotizacion_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();

			conector.Select(sql, parametros);
            //conector.Call(sql, parametros);

			return conector.result_set;
		}

		public void limpiar_cotizacion(long cotizacion_id)
		{
			string sql = @"
				DELETE FROM
					farmacontrol_local.detallado_cotizaciones
				WHERE
					cotizacion_id = @cotizacion_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("cotizacion_id",cotizacion_id);

			conector.Delete(sql,parametros);
		}

		public DTO_Validacion terminar_cotizacion(long cotizacion_id, string comentarios)
		{
			DTO_Validacion validacion = new DTO_Validacion();
			validacion.status = false;
			validacion.informacion = "Ocurrió un problema al intentar terminar la cotización, inténtelo más tarde";

			string sql = @"
				UPDATE
					farmacontrol_local.cotizaciones
				SET
					fecha_cerrado = NOW(),
                    comentarios = @comentarios
				WHERE
					cotizacion_id = @cotizacion_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("cotizacion_id", cotizacion_id);
            parametros.Add("comentarios",comentarios);

			conector.Update(sql, parametros);

			if (conector.filas_afectadas > 0)
			{
				validacion.status = true;
				validacion.informacion = "Cotización cerrada correctamente.";
			}

			return validacion;
		}

		public DataTable get_cotizaciones_pausadas()
		{
            //string sql = "farmacontrol_global.Cotizaciones_get_cotizaciones_pausadas";
            //	CAST(cotizacion_id as CHAR(50)) AS cotizacion_id,
			string sql = @"
				SELECT
					cotizacion_id,
					empleados.nombre AS empleado,
                    clientes.nombre AS cliente,
					fecha_creado as fecha
				FROM
					farmacontrol_local.cotizaciones
				LEFT JOIN farmacontrol_global.empleados USING(empleado_id)
                LEFT JOIN farmacontrol_global.clientes_domicilios USING(cliente_domicilio_id)
                LEFT JOIN farmacontrol_global.clientes ON clientes.cliente_id = clientes_domicilios.cliente_id
				WHERE
					pausado IS TRUE
				AND
					fecha_cerrado IS NULL
			";
            
			Dictionary<string,object> parametros = new Dictionary<string,object>();

			conector.Select(sql,parametros);
            //conector.Call(sql, parametros);

			return conector.result_set;
		}

		public DTO_Validacion pausar_cotizacion(long cotizacion_id,string comentarios)
		{
			DTO_Validacion validacion = new DTO_Validacion();
			validacion.status = false;
			validacion.informacion = "Ocurrió un problema al intentar pausar la cotización, inténtelo más tarde.";

			string sql = @"
				UPDATE
					farmacontrol_local.cotizaciones
				SET
					pausado = 1,
                    comentarios = @comentarios
				WHERE
					cotizacion_id = @cotizacion_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("cotizacion_id", cotizacion_id);
            parametros.Add("comentarios", comentarios);

			conector.Update(sql, parametros);

			if (conector.filas_afectadas > 0)
			{
				validacion.status = true;
				validacion.informacion = "Cotización pausada correctamente";
			}

			return validacion;
		}

		public DTO_Validacion desasociar_facturacion(long cotizacion_id)
		{
			DTO_Validacion validacion = new DTO_Validacion();
			validacion.status = false;
			validacion.informacion = "Ocurrió un problema al intentar desasociar la facturación, inténtelo más tarde";

			string sql = @"
				UPDATE
					farmacontrol_local.cotizaciones
				SET
					rfc_registro_id = NULL
				WHERE
					cotizacion_id = @cotizacion_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("cotizacion_id", cotizacion_id);

			conector.Update(sql, parametros);

			if (conector.filas_afectadas > 0)
			{
				validacion.status = true;
				validacion.informacion = "Se desasoció la facturacion correctamente";
			}

			return validacion;
		}

		public DTO_Validacion desasociar_cliente_credito(long cotizacion_id)
		{
			DTO_Validacion validacion = new DTO_Validacion();
			validacion.status = false;
			validacion.informacion = "Ocurrió un problema al intentar eliminar el crédito, inténtelo más tarde";

			string sql = @"
				UPDATE
					farmacontrol_local.cotizaciones
				SET
					cliente_credito_id = NULL
				WHERE
					cotizacion_id = @cotizacion_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("cotizacion_id",cotizacion_id);

			conector.Update(sql,parametros);

			if(conector.filas_afectadas > 0)
			{
				validacion.status = true;
				validacion.informacion = "Se eliminó el crédito correctamente";
			}

			return validacion;
		}

		public DTO.DTO_Validacion desasocisar_cliente_domicilio(long cotizacion_id)
		{
			DTO.DTO_Validacion validacion = new DTO.DTO_Validacion();
			validacion.status = false;
			validacion.informacion = "Ocurrió un problema al intentar eliminar el domicilio, inténtelo más tarde";

			string sql = @"
				UPDATE
					farmacontrol_local.cotizaciones
				SET
					cliente_domicilio_id = NULL
				WHERE
					cotizacion_id = @cotizacion_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("cotizacion_id",cotizacion_id);

			conector.Update(sql,parametros);
			
			if(conector.filas_afectadas > 0)
			{
				validacion.status = true;
				validacion.informacion = "Se eliminó el domicilio correctamente";
			}

			return validacion;
		}

		public DataTable get_totales(long cotizacion_id)
		{
           // string sql = "farmacontrol_global.Cotizaciones_get_totales";
            //@cotizacion_id
			string sql = @"
				SELECT
	            COALESCE(tmp_totales.piezas, 0) AS piezas,
	            COALESCE(tmp_totales.subtotal, 0) AS subtotal,
	            COALESCE(tmp_excento.excento, 0) AS excento,
	            COALESCE(tmp_gravado.gravado, 0) AS gravado,
	            COALESCE(tmp_totales.iva, 0) AS iva,
                COALESCE(tmp_totales.ieps, 0) AS ieps,
	            COALESCE(tmp_totales.total, 0) AS total
            FROM
	            farmacontrol_local.cotizaciones
            LEFT JOIN
	            (
		            SELECT 
			            farmacontrol_local.detallado_cotizaciones.cotizacion_id AS cotizacion_id,
			            COALESCE(SUM(subtotal),0) AS gravado
		            FROM
			            farmacontrol_local.detallado_cotizaciones
		            WHERE
			            farmacontrol_local.detallado_cotizaciones.cotizacion_id = @cotizacion_id
		            AND
			            pct_iva <> 0
		            GROUP BY farmacontrol_local.detallado_cotizaciones.cotizacion_id
	            ) AS tmp_gravado USING(cotizacion_id)
            LEFT JOIN 
	            (
		            SELECT 
			            farmacontrol_local.detallado_cotizaciones.cotizacion_id AS cotizacion_id,
			            SUM(subtotal) AS excento
		            FROM
			            farmacontrol_local.detallado_cotizaciones
		            WHERE
			            farmacontrol_local.detallado_cotizaciones.cotizacion_id = @cotizacion_id
		            AND
			            pct_iva = 0
		            GROUP BY farmacontrol_local.detallado_cotizaciones.cotizacion_id
	            ) AS tmp_excento USING(cotizacion_id)
            LEFT JOIN 
	            (
		            SELECT 
			            farmacontrol_local.detallado_cotizaciones.cotizacion_id AS cotizacion_id,
			            SUM(subtotal) AS subtotal,
			            SUM(cantidad) AS piezas,
			            SUM(importe_iva) AS iva,
                        SUM(importe_ieps) AS ieps,
			            SUM(total) AS total
		            FROM
			            farmacontrol_local.detallado_cotizaciones
		            LEFT JOIN farmacontrol_local.cotizaciones USING(cotizacion_id)
		            WHERE
			            farmacontrol_local.cotizaciones.cotizacion_id = @cotizacion_id
	            ) AS tmp_totales USING(cotizacion_id)
            WHERE
	            cotizaciones.cotizacion_id = @cotizacion_id
			";
            



			Dictionary<string,object> parametros = new Dictionary<string,object>();
            parametros.Add("cotizacion_id", cotizacion_id);

			conector.Select(sql,parametros);
            //conector.Call(sql, parametros);

			return conector.result_set;
		}

		public DTO.DTO_Cotizacion_ticket get_informacion_ticket_cotizacion(long cotizacion_id)
		{
            //string sql = "farmacontrol_global.Cotizaciones_get_informacion_ticket_cotizacion";
            
			string sql = @"
				SELECT
					*,
					empleados.nombre AS nombre_empleado
				FROM
					farmacontrol_local.cotizaciones
				LEFT JOIN farmacontrol_global.empleados USING(empleado_id)
				WHERE
					cotizacion_id = @cotizacion_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
            parametros.Add("cotizacion_id", cotizacion_id);
			conector.Select(sql,parametros);

            //conector.Call(sql, parametros);

			DTO.DTO_Cotizacion_ticket cotizacion_ticket = new DTO.DTO_Cotizacion_ticket();
				
			foreach(DataRow row in conector.result_set.Rows)
			{
				cotizacion_ticket.cotizacion_id = Convert.ToInt32(row["cotizacion_id"]);
				cotizacion_ticket.nombre_empleado = row["nombre_empleado"].ToString();
				cotizacion_ticket.nombre_terminal = "TERMINAL 1";
				cotizacion_ticket.detallado_cotizacion_ticket = get_detallado_cotizacion(cotizacion_id);
				var totales = get_totales(cotizacion_id);
				cotizacion_ticket.excento = Convert.ToDecimal(totales.Rows[0]["excento"]);
				cotizacion_ticket.gravado = Convert.ToDecimal(totales.Rows[0]["gravado"]);
				cotizacion_ticket.subtotal = Convert.ToDecimal(totales.Rows[0]["subtotal"]);
				cotizacion_ticket.iva = Convert.ToDecimal(totales.Rows[0]["iva"]);
                cotizacion_ticket.ieps = Convert.ToDecimal(totales.Rows[0]["ieps"]);
				cotizacion_ticket.total = Convert.ToDecimal(totales.Rows[0]["total"]);
			}

			return cotizacion_ticket;
		}

		public List<Tuple<string, string, int>> get_detallado_caducidades(long cotizacion_id, int articulo_id, decimal importe)
		{
            //string sql = "farmacontrol_global.Cotizaciones_get_detallado_caducidades";
            
			string sql = @"
				SELECT
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					cantidad
				FROM
					farmacontrol_local.detallado_cotizaciones
				WHERE
					cotizacion_id = @cotizacion_id
				AND
					articulo_id = @articulo_id
				AND
					importe = @importe
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("cotizacion_id", cotizacion_id);
            parametros.Add("articulo_id", articulo_id);
            parametros.Add("importe", importe);

			conector.Select(sql, parametros);

           // conector.Call(sql, parametros);

			List<Tuple<string, string, int>> lista_caducidades = new List<Tuple<string, string, int>>();

			foreach (DataRow row in conector.result_set.Rows)
			{
				Tuple<string, string, int> tupla = new Tuple<string, string, int>(row["caducidad"].ToString(), row["lote"].ToString(), Convert.ToInt32(row["cantidad"]));
				lista_caducidades.Add(tupla);
			}

			return lista_caducidades;
		}

		public List<DTO.DTO_Detallado_cotizacion_ticket> get_detallado_cotizacion(long cotizacion_id)
		{
            //string sql = "farmacontrol_global.Cotizaciones_get_detallado_cotizacion";
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
							articulo_id = detallado_cotizaciones.articulo_id
						ORDER BY 
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS amecop,
					RPAD(nombre, 37, ' ') AS nombre,
					detallado_cotizaciones.precio_publico,
					detallado_cotizaciones.importe,
					SUM(detallado_cotizaciones.cantidad) AS cantidad,
					detallado_cotizaciones.subtotal,
					FORMAT(detallado_cotizaciones.pct_descuento, 2) AS pct_descuento,
					detallado_cotizaciones.importe_descuento,
                    detallado_cotizaciones.importe_ieps,
					detallado_cotizaciones.total,
                    IF(articulos.clase_antibiotico_id IS NULL, 0, 1) AS es_antibiotico
				FROM
					farmacontrol_local.detallado_cotizaciones
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					cotizacion_id = @cotizacion_id
				GROUP BY
					articulo_id,importe
				ORDER BY detallado_cotizacion_id ASC
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("cotizacion_id", cotizacion_id);

			conector.Select(sql, parametros);
            //conector.Call(sql, parametros);

			var result_set = conector.result_set;

			List<DTO.DTO_Detallado_cotizacion_ticket> lista_detallado_cotizacion = new List<DTO.DTO_Detallado_cotizacion_ticket>();

			foreach (DataRow row in result_set.Rows)
			{
				DTO.DTO_Detallado_cotizacion_ticket detallado_ticket = new DTO.DTO_Detallado_cotizacion_ticket();
				detallado_ticket.articulo_id = Convert.ToInt32(row["articulo_id"]);

                string var_temp_amecop = row["amecop"].ToString();
                int tam_var = var_temp_amecop.Length;
                String Var_Sub = "*" + var_temp_amecop.Substring((tam_var - 3), 3);
                string amecop_temp = Var_Sub.PadRight(5, ' ');

                detallado_ticket.amecop = amecop_temp;
				//detallado_ticket.amecop = row["amecop"].ToString();

				detallado_ticket.nombre = row["nombre"].ToString();
				detallado_ticket.precio_unitario = Convert.ToDecimal(row["precio_publico"]);
				detallado_ticket.subtotal = Convert.ToDecimal(row["subtotal"]);
				detallado_ticket.importe = Convert.ToDecimal(row["importe"]);
				detallado_ticket.descuento = Convert.ToDecimal(row["pct_descuento"]);
				detallado_ticket.total = Convert.ToDecimal(row["total"]);
				detallado_ticket.cantidad = Convert.ToInt32(row["cantidad"]);
                detallado_ticket.importe_ieps = Convert.ToDecimal(row["importe_ieps"]);
				detallado_ticket.caducidades_lotes = get_detallado_caducidades(cotizacion_id, detallado_ticket.articulo_id, Convert.ToDecimal(row["importe"]));
				lista_detallado_cotizacion.Add(detallado_ticket);
			}

			return lista_detallado_cotizacion;
		}

		public void eliminar_producto_gratis(int articulo_id, long cotizacion_id)
		{
			string sql = @"
				DELETE FROM
					farmacontrol_local.detallado_cotizaciones
				WHERE
					articulo_id = @articulo_id
				AND
					cotizacion_id = @cotizacion_id
				AND
					es_promocion = 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("articulo_id", articulo_id);
			parametros.Add("cotizacion_id", cotizacion_id);

			conector.Delete(sql, parametros);
		}

		public int get_productos_sin_promocion(int articulo_id, long cotizacion_id)
		{
			string sql = @"
				SELECT
					COALESCE(SUM(cantidad),0) AS articulos_sin_promocion
				FROM
					farmacontrol_local.detallado_cotizaciones
				WHERE
					articulo_id = @articulo_id
				AND
					cotizacion_id = @cotizacion_id
				AND
					es_promocion = 0
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("articulo_id", articulo_id);
			parametros.Add("cotizacion_id", cotizacion_id);

			conector.Select(sql, parametros);

			var result_query = conector.result_set;

			if (result_query.Rows.Count > 0)
			{
				return Convert.ToInt32(result_query.Rows[0]["articulos_sin_promocion"]);
			}

			return 0;
		}

		public int registrar_rfc(long cotizacion_id, string rfc_registro_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.cotizaciones
				SET
					rfc_registro_id = @rfc_registro_id
				WHERE
					cotizacion_id = @cotizacion_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("rfc_registro_id", rfc_registro_id);
			parametros.Add("cotizacion_id", cotizacion_id);

			conector.Update(sql, parametros);

			return conector.filas_afectadas;
		}

		public int registrar_cliente_credito(long cotizacion_id, string cliente_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.cotizaciones
				SET
					cliente_credito_id = @cliente_id
				WHERE
					cotizacion_id = @cotizacion_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("cliente_id", cliente_id);
			parametros.Add("cotizacion_id", cotizacion_id);

			conector.Update(sql, parametros);

			return conector.filas_afectadas;
		}

		public int registrar_cliente_domicilio(long cotizacion_id, string cliente_domicilio_id)
		{
			try
			{
				string sql = @"
					UPDATE
						farmacontrol_local.cotizaciones
					SET
						cliente_domicilio_id = @cliente_domicilio_id
					WHERE
						cotizacion_id = @cotizacion_id
				";

				Dictionary<string, object> parametros = new Dictionary<string, object>();
				parametros.Add("cliente_domicilio_id", cliente_domicilio_id);
				parametros.Add("cotizacion_id", cotizacion_id);

				conector.Update(sql, parametros);
			}
			catch (Exception exception)
			{
				Log_error.log(exception);
			}

			return conector.filas_afectadas;
		}

		public DataTable update_cantidad(long cotizacion_id, long detallado_cotizacion_id, long cantidad)
		{
            //total = ( ((precio_publico - ( precio_publico * pct_descuento ) ) * @cantidad ) + (((precio_publico - ( precio_publico * pct_descuento )) * @cantidad) * pct_iva ) )
			string sql = @"
				UPDATE
					farmacontrol_local.detallado_cotizaciones
				SET
					cantidad = @cantidad,
					subtotal = ((precio_publico - (precio_publico * pct_descuento )) * @cantidad),
					importe_iva = (((precio_publico - (precio_publico * pct_descuento)) * @cantidad) * pct_iva ),
                    importe_ieps =  ((precio_publico - (precio_publico * pct_descuento)) * ieps)*@cantidad,
                    total = ( ((precio_publico - ( precio_publico * pct_descuento ) ) * @cantidad ) + (((precio_publico - ( precio_publico * pct_descuento )) * @cantidad) * pct_iva ) ) +  ((precio_publico - (precio_publico * pct_descuento)) * ieps)*@cantidad 

				WHERE
					detallado_cotizacion_id = @detallado_cotizacion_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("cantidad", cantidad);
			parametros.Add("detallado_cotizacion_id", detallado_cotizacion_id);

			conector.Update(sql, parametros);

			return get_productos_cotizacion(cotizacion_id);
		}

		public DataTable eliminar_detallado_cotizacion(int detallado_cotizacion_id, long cotizacion_id)
		{
			string sql = @"
				DELETE FROM
					farmacontrol_local.detallado_cotizaciones
				WHERE
					detallado_cotizacion_id = @detallado_cotizacion_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("detallado_cotizacion_id", detallado_cotizacion_id);

			conector.Select(sql, parametros);

			return get_productos_cotizacion(cotizacion_id);
		}

		public DataTable get_productos_cotizacion(long cotizacion_id)
		{
			DataTable result_query = new DataTable();

			try
			{
              //  string sql = "farmacontrol_global.Cotizaciones_get_productos_cotizacion";
                /*
                 IF(caducidad = '0000-00-00', ' ', caducidad) AS caducidad,
				 IF(caducidad = '0000-00-00', ' ', caducidad) AS caducidad_sin_formato,
                 */
                string sql = @"
					SELECT 
						detallado_cotizacion_id,
						articulos.articulo_id AS articulo_id,
						es_promocion,
						(
							SELECT 
								amecop
							FROM 
								farmacontrol_global.articulos_amecops 
							WHERE 
								articulo_id = detallado_cotizaciones.articulo_id
							ORDER BY 
								articulos_amecops.amecop_principal 
							DESC
							LIMIT 1
						) AS amecop,
						nombre,
                        CAST( caducidad AS CHAR(10) ) AS caducidad,
						CAST( caducidad AS CHAR(10) ) AS caducidad_sin_formato,
						lote,
						farmacontrol_local.detallado_cotizaciones.precio_publico AS precio_publico,
						farmacontrol_local.detallado_cotizaciones.pct_descuento,
						importe_descuento AS importe_descuento,
						farmacontrol_local.detallado_cotizaciones.pct_iva,
						importe_iva AS importe_iva,
						farmacontrol_local.detallado_cotizaciones.importe_ieps,
						importe AS importe,
						cantidad,
						CONVERT(cantidad USING utf8) AS cantidad_con_formato,
						subtotal AS subtotal,
						farmacontrol_local.detallado_cotizaciones.total AS total,
                        IF(articulos.clase_antibiotico_id IS NULL, 0, 1) AS es_antibiotico,
						COALESCE((
							SELECT
								COALESCE(SUM(existencia), 0) - COALESCE(existencia_mermas, 0) - COALESCE(existencia_devoluciones, 0) - COALESCE(existencia_apartados, 0) - COALESCE(existencia_cambio_fisico, 0) - COALESCE(existencia_ventas, 0) - COALESCE(existencia_traspasos, 0) - COALESCE(existencia_mayoreo, 0) AS existencia_vendible
							FROM
								farmacontrol_global.articulos
							LEFT JOIN farmacontrol_local.existencias USING(articulo_id)
							NATURAL LEFT JOIN 
							(
								SELECT
									articulo_id,
									caducidad,
									lote,
									SUM(cantidad) AS existencia_devoluciones
								FROM
									farmacontrol_local.detallado_devoluciones
								JOIN farmacontrol_local.devoluciones ON
									farmacontrol_local.devoluciones.devolucion_id = farmacontrol_local.detallado_devoluciones.devolucion_id
								WHERE
					
									fecha_terminado IS NULL

								GROUP BY articulo_id, caducidad, lote
			
							) AS tmp_devoluciones
							NATURAL LEFT JOIN 
							(
								SELECT
									articulo_id,
									caducidad,
									lote,
									SUM(cantidad) AS existencia_mermas
								FROM
									farmacontrol_local.apartados
								WHERE
					
									tipo = 'MERMA'
								GROUP BY articulo_id, caducidad, lote
							) AS tmp_mermas
							NATURAL LEFT JOIN 
							(
								SELECT
									articulo_id,
									caducidad,
									lote,
									SUM(cantidad) AS existencia_apartados
								FROM
									farmacontrol_local.apartados
								WHERE
					
									tipo = 'SUCURSAL'
								GROUP BY articulo_id, caducidad, lote
							) AS tmp_apartados
							NATURAL LEFT JOIN 
							(
								SELECT
									articulo_id,
									caducidad,
									lote,
									SUM(cantidad) AS existencia_cambio_fisico
								FROM
									farmacontrol_local.apartados
								WHERE
					
									tipo = 'CAMBIO_FISICO'
								GROUP BY articulo_id, caducidad, lote
							) AS tmp_cambio_fisico
							NATURAL LEFT JOIN 
							(
								SELECT
									articulo_id,
									caducidad,
									lote,
									SUM(cantidad) AS existencia_traspasos
								FROM
									farmacontrol_local.detallado_traspasos
								LEFT JOIN farmacontrol_local.traspasos USING(traspaso_id)
								WHERE
									farmacontrol_local.traspasos.remote_id IS NULL
								GROUP BY articulo_id, caducidad, lote
							) AS tmp_traspasos
							NATURAL LEFT JOIN 
							(
								SELECT
									articulo_id,
									caducidad,
									lote,
									SUM(cantidad) AS existencia_ventas
								FROM
									farmacontrol_local.detallado_ventas
								LEFT JOIN farmacontrol_local.ventas USING(venta_id)
								WHERE
									farmacontrol_local.ventas.fecha_terminado IS NULL
								GROUP BY articulo_id, caducidad, lote
							) AS tmp_ventas
							NATURAL LEFT JOIN 
							(
								SELECT
									articulo_id,
									caducidad,
									lote,
									SUM(cantidad) AS existencia_mayoreo
								FROM
									farmacontrol_local.detallado_mayoreo_ventas
								LEFT JOIN farmacontrol_local.mayoreo_ventas USING(mayoreo_venta_id)
								WHERE
									mayoreo_ventas.fecha_fin_verificacion IS NULL
								GROUP BY articulo_id,caducidad,lote
							) AS tmp_mayoreo
							WHERE
								existencias.articulo_id = detallado_cotizaciones.articulo_id
							AND
								existencias.caducidad = detallado_cotizaciones.caducidad
							AND
								existencias.lote = detallado_cotizaciones.lote
							GROUP BY farmacontrol_global.articulos.articulo_id, caducidad, lote
						),0) AS existencia_vendible
					FROM
						farmacontrol_local.detallado_cotizaciones
					LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
					LEFT JOIN farmacontrol_local.cotizaciones USING(cotizacion_id)
					WHERE
						farmacontrol_local.detallado_cotizaciones.cotizacion_id = @cotizacion_id
					GROUP BY
						detallado_cotizacion_id
					ORDER BY
						farmacontrol_local.detallado_cotizaciones.detallado_cotizacion_id
					DESC
				";

				Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("cotizacion_id", cotizacion_id);

				conector.Select(sql, parametros);

                //conector.Call(sql, parametros);

				result_query = conector.result_set;

				if (result_query.Rows.Count > 0)
				{
					foreach(DataRow row in result_query.Rows)
					{
						/*if(!row["caducidad"].ToString().Equals(" "))
						{
                            row["caducidad"] = HELPERS.Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");
						}*/

                        if (!DBNull.Value.Equals(row["caducidad"]))
						{
                            row["caducidad"] = HELPERS.Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");
						}



						if(Convert.ToInt64(row["existencia_vendible"]) < Convert.ToInt64(row["cantidad"]))
						{
							row["cantidad_con_formato"] = string.Format("{0}({1})",row["cantidad"].ToString(),row["existencia_vendible"].ToString());
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

		public DataTable insertar_detallado(string amecop, string caducidad, string lote, long cantidad, long? cotizacion_id, bool es_promocion = false)
		{
			//string pct_descuento = "articulos.pct_descuento";

			string sql = string.Format(@"
				INSERT INTO
					farmacontrol_local.detallado_cotizaciones
					(
						SELECT
							0 as detallado_cotizacion_id,
							@cotizacion_id AS cotizacion_id,
							articulo_id,
							@caducidad AS caducidad,
							@lote AS lote,
							precio_publico,
							{0},
							FORMAT((precio_publico * {0}), 4) AS importe_descuento,
							precio_publico - (precio_publico * {0}) AS importe,
						
							@cantidad AS cantidad,
	
							(precio_publico - (precio_publico * {0})) * @cantidad AS subtotal,
							pct_iva,
							((precio_publico - (precio_publico * {0})) * @cantidad) * pct_iva AS importe_iva,
							tipo_ieps,
							ieps,
							IF(tipo_ieps = 'PCT', ((precio_publico - (precio_publico * {0})) * ieps)*@cantidad , ieps) AS importe_ieps,
							((precio_publico - (precio_publico * {0})) * @cantidad) + (  ((precio_publico - (precio_publico * {0})) * @cantidad) * ieps ) + (((precio_publico - (precio_publico * {0})) * @cantidad) + (  ((precio_publico - (precio_publico * {0})) * @cantidad) * ieps ) )*pct_iva  AS total,
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
					subtotal = subtotal + ((articulos.precio_publico - (articulos.precio_publico * {0})) * @cantidad),
					importe_iva = importe_iva + (((articulos.precio_publico - (articulos.precio_publico * {0})) * @cantidad) * articulos.pct_iva),
                    importe_ieps = importe_ieps + IF(articulos.tipo_ieps = 'PCT', ((articulos.precio_publico - (articulos.precio_publico * {0})) * articulos.ieps)*@cantidad , articulos.ieps),
					total = total + ((articulos.precio_publico - (articulos.precio_publico * {0})) * @cantidad) + (  ((articulos.precio_publico - (articulos.precio_publico * {0})) * @cantidad) * articulos.ieps ) + (((articulos.precio_publico - (articulos.precio_publico * {0})) * @cantidad) + (  ((articulos.precio_publico - (articulos.precio_publico * {0})) * @cantidad) * articulos.ieps ) )*articulos.pct_iva 

			", (es_promocion) ? "1" : "articulos.pct_descuento");

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("cotizacion_id", cotizacion_id);
			parametros.Add("amecop", amecop);
			parametros.Add("caducidad", caducidad);
			parametros.Add("lote", lote);
			parametros.Add("cantidad", cantidad);
			parametros.Add("es_promocion", es_promocion);

			conector.Insert(sql, parametros);

			return get_productos_cotizacion((long)cotizacion_id);
		}

		public void cambiar_usuario(int empleado_id, long? cotizacion_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.cotizaciones
				SET
					empleado_id = @empleado_id
				WHERE
					cotizacion_id = @cotizacion_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("empleado_id", empleado_id);
			parametros.Add("cotizacion_id", cotizacion_id);

			conector.Select(sql, parametros);
		}

		public DataTable get_cotizacion_data(long cotizacion_id)
		{
            //string sql = "farmacontrol_global.Cotizaciones_get_cotizacion_data";
            
			string sql = @"
				SELECT
					'' AS rfc_registro,
					(SELECT nombre FROM farmacontrol_global.clientes WHERE cliente_id = cliente_credito_id) as cliente_credito_nombre,
					CONCAT(clientes.nombre,' ','( ', CONCAT_WS(' ',clientes_domicilios.calle, CONCAT('#',REPLACE(clientes_domicilios.numero_exterior, '#','')),clientes_domicilios.numero_interior, clientes_domicilios.colonia, clientes_domicilios.municipio, clientes_domicilios.ciudad),' )') AS servicio_domicilio,
					cotizacion_id,
					empleados.nombre AS nombre,
                    cotizaciones.comentarios AS comentarios,
					fcid
				FROM
					farmacontrol_local.cotizaciones
				LEFT JOIN farmacontrol_global.empleados USING(empleado_id)
				LEFT JOIN farmacontrol_global.clientes_domicilios USING(cliente_domicilio_id)
				LEFT JOIN farmacontrol_global.clientes USING(cliente_id)
				WHERE
					cotizacion_id = @cotizacion_id
					
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("cotizacion_id", cotizacion_id);

			conector.Select(sql, parametros);
            //conector.Call(sql, parametros);

			return conector.result_set;
		}

		public DTO_Cotizacion get_cotizacion_all_data(long cotizacion_id)
		{
			DTO_Cotizacion cotizacion =  new DTO_Cotizacion();

            //string sql = "farmacontrol_global.Cotizaciones_get_cotizacion_all_data";
            
			string sql = @"
				SELECT
					cotizacion_id, 
					terminal_id,
					empleado_id,
					cliente_credito_id AS cliente_credito_id,
					cliente_domicilio_id AS cliente_domicilio_id,
					cupon_id,
					fecha_creado,
					fecha_cerrado,
					pausado,
					comentarios
				FROM
					farmacontrol_local.cotizaciones
				WHERE
					cotizacion_id = @cotizacion_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
            parametros.Add("cotizacion_id", cotizacion_id);

			conector.Select(sql,parametros);
            //conector.Call(sql, parametros);

			var result =  conector.result_set;

			if(result.Rows.Count > 0)
			{
				var row = result.Rows[0];

				long? nullable = null;
				DateTime? date_nullable = null;
				cotizacion.cotizacion_id = Convert.ToInt64(row["cotizacion_id"]);
				cotizacion.terminal_id = (row["terminal_id"].ToString() != "") ? Convert.ToInt64(row["terminal_id"]) : nullable ;
				cotizacion.empleado_id = (row["empleado_id"].ToString() != "") ? Convert.ToInt64(row["empleado_id"]) : nullable ;
				cotizacion.cliente_credito_id = row["cliente_credito_id"].ToString();
				cotizacion.cliente_domicilio_id = row["cliente_domicilio_id"].ToString();
				cotizacion.cupon_id = (row["cupon_id"].ToString() != "") ? Convert.ToInt64(row["cupon_id"]) : nullable ;
				cotizacion.fecha_creado = Convert.ToDateTime(row["fecha_creado"]);
				cotizacion.fecha_cerrado = (row["fecha_cerrado"].ToString() != "") ? Convert.ToDateTime(row["fecha_cerrado"]) : date_nullable;
				cotizacion.pausado = Convert.ToInt32(row["pausado"]);
				cotizacion.comentarios = row["comentarios"].ToString();
			}
			
			return cotizacion;
		}

		public long registrar_cotizacion(bool tomar_empleado_sesion)
		{
			int? terminal_id = HELPERS.Misc_helper.get_terminal_id();
            long empleado_id = (long)Principal.empleado_id;
            long last_insert_id = 0;
            string sql = "";
			try
			{
                if(tomar_empleado_sesion == false)
                {
                    
                    sql = @"
                        SELECT
                            empleado_id
                        FROM
                            farmacontrol_local.cotizaciones
                        WHERE
                            fecha_cerrado IS NOT NULL
                        AND
                            terminal_id = @terminal_id
                        ORDER BY
                            cotizacion_id DESC
                       LIMIT 1
                    ";
                     

                    //sql = "farmacontrol_global.Cotizaciones_get_last_empleado_cotizacion";
                    Dictionary<string, object> parametros = new Dictionary<string, object>();
                    parametros.Add("terminal_id", terminal_id);

                    conector.Select(sql, parametros);
                    //conector.Call(sql, parametros);

                    if (conector.result_set.Rows.Count > 0)
                    {
                        foreach(DataRow row in conector.result_set.Rows)
                        {
                            foreach(DataColumn col in conector.result_set.Columns)
                            {
                                Console.WriteLine("ColName: " + col.ColumnName.ToString() + "Value: " + row[col].ToString());
                            }
                        }

                        empleado_id = Convert.ToInt32(conector.result_set.Rows[0]["empleado_id"]);
                    }
                }

                
				sql = @"
					INSERT INTO
						farmacontrol_local.cotizaciones
					SET
						empleado_id = @empleado_id,
						terminal_id = @terminal_id,
						fecha_creado = NOW()
				";
                 
               // sql = "farmacontrol_global.Cotizaciones_registrar_cotizacion";
                Dictionary<string, object> parametros_registro = new Dictionary<string, object>();
                parametros_registro.Add("empleado_id", empleado_id);
                parametros_registro.Add("terminal_id", terminal_id);

               // conector.Call(sql, parametros_registro);
                conector.Insert(sql, parametros_registro);

                if (conector.insert_id > 0)
                {
                    //last_insert_id = Convert.ToInt64(conector.result_set.Rows[0]["LID"]);
                    last_insert_id = conector.insert_id;
                }

				//conector.Insert(sql, parametros_registro);
			}
			catch (MySqlException exception)
			{
				Log_error.log(exception);
			}
            return last_insert_id;
			
		}
	}
}
