using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;
using System.Data;
using System.Windows.Forms;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Vales_efectivo
	{
		Conector conector = new Conector();

		public DTO_Vale vale_data(string vale_efectivo_id)
		{
			string sql = @"
				SELECT
					*
				FROM
					farmacontrol_global.vales_efectivo
				WHERE
					vales_efectivo.vale_efectivo_id = @vale_efectivo_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("vale_efectivo_id", vale_efectivo_id);

			conector.Select(sql,parametros);

			DTO_Vale vale = new DTO_Vale();

			if(conector.result_set.Rows.Count > 0)
			{
				var row = conector.result_set.Rows[0];
				vale.vale_efectivo_id = row["vale_efectivo_id"].ToString();
				vale.sucursal_id = Convert.ToInt64(row["sucursal_id"]);
				vale.empleado_id = Convert.ToInt64(row["empleado_id"]);
				vale.tipo_creacion = row["tipo_creacion"].ToString();
				vale.elemento_id = Convert.ToInt64(row["elemento_id"]);

				long? long_null = null;

				vale.canje_sucursal_id = (row["canje_sucursal_id"].ToString().Equals("")) ? long_null : Convert.ToInt64(row["canje_sucursal_id"]) ;
				vale.canje_empleado_id = (row["canje_empleado_id"].ToString().Equals("")) ? long_null : Convert.ToInt64(row["canje_empleado_id"]);
				vale.canje_venta_id = (row["canje_venta_id"].ToString().Equals("")) ? long_null : Convert.ToInt64(row["canje_venta_id"]);

				vale.fecha_creacion = Convert.ToDateTime(row["fecha_creacion"]);

				DateTime? date_null = null;

				vale.fecha_canje = (row["fecha_canje"].ToString().Equals("")) ? date_null : Convert.ToDateTime(row["fecha_canje"]);
                vale.fecha_cancelacion = (row["fecha_cancelacion"].ToString().Equals("")) ? date_null : Convert.ToDateTime(row["fecha_cancelacion"]);
				vale.total = Convert.ToDecimal(row["total"]);
			}

			return vale;
		}

        public List<DTO_Vale> get_vales_cancelados(long corte_id)
        {
            List<DTO_Vale> lista_cancelados = new List<DTO_Vale>();

            DAO_Cortes dao_cortes = new DAO_Cortes();

            var corte_data = dao_cortes.get_informacion_corte(corte_id);

            string sql = @"
                 SELECT
                    vales_efectivo.*
                FROM
                    farmacontrol_global.vales_efectivo
                JOIN farmacontrol_local.ventas ON ventas.venta_id = vales_efectivo.elemento_id
                WHERE
                    vales_efectivo.fecha_cancelacion BETWEEN (
                        SELECT
                            fecha
                        FROM
                            farmacontrol_local.cortes
                        WHERE
                            cortes.tipo = @tipo
                        AND
                            cortes.terminal_id = @terminal_id
                        ORDER BY cortes.corte_id DESC
                        LIMIT 1,1
                    ) AND @fecha_corte
                AND
                    vales_efectivo.tipo_creacion != 'PREPAGO'
                AND
                    vales_efectivo.sucursal_id = @sucursal_id
                AND
                    ventas.terminal_id = @terminal_id
                AND
                    vales_efectivo.fecha_cancelacion IS NOT NULL
                AND
                    vales_efectivo.fecha_canje IS NOT NULL
                ORDER BY vales_efectivo.fecha_cancelacion ASC
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("sucursal_id", Convert.ToInt64(Config_helper.get_config_local("sucursal_id")));
            parametros.Add("terminal_id", Convert.ToInt64(Misc_helper.get_terminal_id()));
            parametros.Add("fecha_corte",Misc_helper.fecha(corte_data.fecha.ToString()));
            parametros.Add("tipo",corte_data.tipo);

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {
                    DTO_Vale vale = new DTO_Vale();
                    vale.vale_efectivo_id = row["vale_efectivo_id"].ToString();
                    vale.sucursal_id = Convert.ToInt64(row["sucursal_id"]);
                    vale.empleado_id = Convert.ToInt64(row["empleado_id"]);
                    vale.tipo_creacion = row["tipo_creacion"].ToString();
                    vale.elemento_id = Convert.ToInt64(row["elemento_id"]);

                    long? long_null = null;

                    vale.canje_sucursal_id = (row["canje_sucursal_id"].ToString().Equals("")) ? long_null : Convert.ToInt64(row["canje_sucursal_id"]);
                    vale.canje_empleado_id = (row["canje_empleado_id"].ToString().Equals("")) ? long_null : Convert.ToInt64(row["canje_empleado_id"]);
                    vale.canje_venta_id = (row["canje_venta_id"].ToString().Equals("")) ? long_null : Convert.ToInt64(row["canje_venta_id"]);

                    vale.fecha_creacion = Convert.ToDateTime(row["fecha_creacion"]);

                    DateTime? date_null = null;

                    vale.fecha_canje = (row["fecha_canje"].ToString().Equals("")) ? date_null : Convert.ToDateTime(row["fecha_canje"]);
                    vale.fecha_cancelacion = (row["fecha_cancelacion"].ToString().Equals("")) ? date_null : Convert.ToDateTime(row["fecha_cancelacion"]);
                    vale.total = Convert.ToDecimal(row["total"]);

                    lista_cancelados.Add(vale);
                }
            }

            return lista_cancelados;
        }

        public List<DTO_Vale> get_vales_canjeados(long corte_id)
        {
            List<DTO_Vale> lista_canjeados = new List<DTO_Vale>();

            DAO_Cortes dao_cortes = new DAO_Cortes();
            var corte_data = dao_cortes.get_informacion_corte(corte_id);

            string sql = @"
                SELECT
                    vales_efectivo.*
                FROM
                    farmacontrol_global.vales_efectivo
                JOIN farmacontrol_local.ventas ON ventas.venta_id = vales_efectivo.elemento_id
                WHERE
                    vales_efectivo.fecha_canje BETWEEN (
                        SELECT
                            fecha
                        FROM
                            farmacontrol_local.cortes
                        WHERE
                            cortes.tipo = @tipo
                        AND
                            cortes.terminal_id = @terminal_id
                        ORDER BY cortes.corte_id DESC
                        LIMIT 1,1
                    ) AND @fecha_corte
                AND
                    vales_efectivo.tipo_creacion != 'PREPAGO'
                AND
                    vales_efectivo.sucursal_id = @sucursal_id
                AND
                    ventas.terminal_id = @terminal_id
                AND
                    vales_efectivo.fecha_cancelacion IS NULL
                AND
                    vales_efectivo.fecha_canje IS NOT NULL
                ORDER BY vales_efectivo.fecha_canje ASC
            ";

            Console.WriteLine(Misc_helper.fecha(corte_data.fecha.ToString()));

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("sucursal_id",Convert.ToInt64(Config_helper.get_config_local("sucursal_id")));
            parametros.Add("terminal_id", Convert.ToInt64(Misc_helper.get_terminal_id()));
            parametros.Add("fecha_corte", Misc_helper.fecha(corte_data.fecha.ToString()));
            parametros.Add("tipo",corte_data.tipo);

            conector.Select(sql,parametros);

            if(conector.result_set.Rows.Count > 0)
            {
                foreach(DataRow row in conector.result_set.Rows)
                {
                    DTO_Vale vale = new DTO_Vale();
                    vale.vale_efectivo_id = row["vale_efectivo_id"].ToString();
                    vale.sucursal_id = Convert.ToInt64(row["sucursal_id"]);
                    vale.empleado_id = Convert.ToInt64(row["empleado_id"]);
                    vale.tipo_creacion = row["tipo_creacion"].ToString();
                    vale.elemento_id = Convert.ToInt64(row["elemento_id"]);

                    long? long_null = null;

                    vale.canje_sucursal_id = (row["canje_sucursal_id"].ToString().Equals("")) ? long_null : Convert.ToInt64(row["canje_sucursal_id"]);
                    vale.canje_empleado_id = (row["canje_empleado_id"].ToString().Equals("")) ? long_null : Convert.ToInt64(row["canje_empleado_id"]);
                    vale.canje_venta_id = (row["canje_venta_id"].ToString().Equals("")) ? long_null : Convert.ToInt64(row["canje_venta_id"]);

                    vale.fecha_creacion = Convert.ToDateTime(row["fecha_creacion"]);

                    DateTime? date_null = null;

                    vale.fecha_canje = (row["fecha_canje"].ToString().Equals("")) ? date_null : Convert.ToDateTime(row["fecha_canje"]);
                    vale.fecha_cancelacion = (row["fecha_cancelacion"].ToString().Equals("")) ? date_null : Convert.ToDateTime(row["fecha_cancelacion"]);
                    vale.total = Convert.ToDecimal(row["total"]);

                    lista_canjeados.Add(vale);
                }
            }

            return lista_canjeados;
        }

        public void set_fecha_cancelado(string vale_efectivo_id, long empleado_id)
        {
            string sql = @"
				UPDATE
					farmacontrol_global.vales_efectivo
				SET
					fecha_cancelacion = @fecha_cancelacion
				WHERE
					vale_efectivo_id = @vale_efectivo_id
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("fecha_cancelacion", Misc_helper.fecha());
            parametros.Add("vale_efectivo_id", vale_efectivo_id);

            conector.Update(sql, parametros);

            if (conector.filas_afectadas > 0)
            {
                DAO_Cola_operaciones.insertar_cola_operaciones(empleado_id, "rest/vales_efectivo", "set_fecha_cancelado", parametros, "PARA ENVIO A SERVIDOR PRINCIPAL");
            }
        }

		public void set_fecha_canje(string vale_efectivo_id, long empleado_id, long venta_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_global.vales_efectivo
				SET
					fecha_canje = @fecha_canje,
					canje_sucursal_id = @canje_sucursal_id,
					canje_empleado_id = @canje_empleado_id,
					canje_venta_id = @canje_venta_id
				WHERE
					vale_efectivo_id = @vale_efectivo_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("fecha_canje",Misc_helper.fecha());
			parametros.Add("canje_sucursal_id",Convert.ToInt64(Config_helper.get_config_local("sucursal_id")));
			parametros.Add("canje_empleado_id",empleado_id);
			parametros.Add("canje_venta_id",venta_id);
			parametros.Add("vale_efectivo_id",vale_efectivo_id);

			conector.Update(sql,parametros);

			if(conector.filas_afectadas > 0)
			{
				DAO_Cola_operaciones.insertar_cola_operaciones(empleado_id, "rest/vales_efectivo", "set_fecha_canje", parametros, "PARA ENVIO A SERVIDOR PRINCIPAL");
			}
		}

		public decimal get_total_vale_efectivo(string vale_efectivo_id)
		{
			string sql = @"
				SELECT
					total
				FROM
					farmacontrol_global.vales_efectivo
				WHERE
					vale_efectivo_id = @vale_efectivo_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("vale_efectivo_id",vale_efectivo_id);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				return Convert.ToDecimal(conector.result_set.Rows[0]["total"]);
			}

			return 0;
		}

		public DataTable get_vale_data(string vale_efectivo_id)
		{
			string sql = @"
				SELECT
					*,
					empleados.nombre AS nombre_empleado
				FROM
					vales_efectivo
				LEFT JOIN farmacontrol_global.empleados USING(empleado_id)
				WHERE
					vale_efectivo_id = @vale_efectivo_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("vale_efectivo_id",vale_efectivo_id);

			conector.Select(sql,parametros);

			return conector.result_set;
		}

		public decimal get_total_venta_nueva(long venta_id)
		{
			string sql = @"
				SELECT
					COALESCE(SUM(detallado_ventas.total), 0) AS total_venta_nueva
				FROM
					farmacontrol_local.cancelaciones
				LEFT JOIN farmacontrol_local.detallado_ventas ON
					detallado_ventas.venta_id = cancelaciones.nueva_venta_id
				WHERE
					cancelaciones.venta_id = @venta_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("venta_id",venta_id);

			conector.Select(sql,parametros);

			var result = conector.result_set;

			if (result.Rows.Count > 0)
			{
				return Convert.ToDecimal(conector.result_set.Rows[0]["total_venta_nueva"]);
			}

			return 0;
		}

		public decimal get_total_venta_original(long venta_id)
		{
			string sql = @"
				SELECT
					COALESCE(SUM(total), 0) AS total_venta_original
				FROM
					farmacontrol_local.detallado_ventas
				WHERE
					venta_id = @venta_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("venta_id", venta_id);

			conector.Select(sql, parametros);

			var result =  conector.result_set;

			if(result.Rows.Count > 0)
			{
				return Convert.ToDecimal(conector.result_set.Rows[0]["total_venta_original"]);
			}

			return 0;
		}

		public string generar_vale_efectivo(long venta_id)
		{
            try
            {
                string uuid = Misc_helper.uuid();
                int empleado_id = (int)FORMS.comunes.Principal.empleado_id;
                int sucursal_id = Convert.ToInt32(Config_helper.get_config_local("sucursal_id"));
                decimal total = (get_total_venta_original(venta_id) - get_total_venta_nueva(venta_id));

                if (total > 0)
                {
                    string sql = @"
				    INSERT INTO
					    farmacontrol_global.vales_efectivo
				    SET
					    vale_efectivo_id = @vale_efectivo_id,
					    sucursal_id = @sucursal_id,
					    empleado_id = @empleado_id,
					    elemento_id = @elemento_id,
					    fecha_creacion = @fecha_creacion,
					    tipo_creacion = @tipo_creacion,
					    total = @total
                    ON DUPLICATE KEY UPDATE
                        vale_efectivo_id = vale_efectivo_id
			    ";

                    Dictionary<string, object> parametros_vale_efectivo = new Dictionary<string, object>();
                    parametros_vale_efectivo.Add("vale_efectivo_id", uuid);
                    parametros_vale_efectivo.Add("sucursal_id", sucursal_id);
                    parametros_vale_efectivo.Add("empleado_id", empleado_id);
                    parametros_vale_efectivo.Add("elemento_id", venta_id);
                    parametros_vale_efectivo.Add("fecha_creacion", Misc_helper.fecha());
                    parametros_vale_efectivo.Add("tipo_creacion", "CANCELA");
                    parametros_vale_efectivo.Add("total", total);

                    conector.Insert(sql, parametros_vale_efectivo);

                    sql = @"
				    SELECT
					    vale_efectivo_id AS vale_efectivo_id
				    FROM
					    farmacontrol_global.vales_efectivo
				    WHERE
					    elemento_id = @venta_id
                    AND
                        sucursal_id = @sucursal_id
                    AND
                        vale_efectivo_id = @vale_efectivo_id
			    ";

                    Dictionary<string, object> parametros = new Dictionary<string, object>();
                    parametros.Add("venta_id", venta_id);
                    parametros.Add("sucursal_id", sucursal_id);
                    parametros.Add("vale_efectivo_id", uuid);

                    conector.Select(sql, parametros);

                    if (conector.result_set.Rows.Count > 0)
                    {
                        DAO_Cola_operaciones.insertar_cola_operaciones(empleado_id, "rest/vales_efectivo", "registrar_vale_efectivo", parametros_vale_efectivo, "PARA ENVIO SERVIDOR PRINCIPAL");
                        return conector.result_set.Rows[0]["vale_efectivo_id"].ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                Log_error.log(ex);
            }

			return "";
		}

		public string generar_vale_efectivo_canjpar(long venta_id, decimal total_vale)
		{
            try
            {
                string uuid = Misc_helper.uuid();
			    int empleado_id = (int)FORMS.comunes.Principal.empleado_id;
			    int sucursal_id = Convert.ToInt32(Config_helper.get_config_local("sucursal_id"));
			    decimal total = total_vale;


			    //FALTA REGISTRAR EL VALE EFECTIVO CON REST EN LA TABLA GLOBAL DEL SERVIDOR PRINCIPAL

			    string sql = @"
				    INSERT INTO
					    farmacontrol_global.vales_efectivo
				    SET
					    vale_efectivo_id = @vale_efectivo_id,
					    sucursal_id = @sucursal_id,
					    empleado_id = @empleado_id,
					    elemento_id = @elemento_id,
					    fecha_creacion = @fecha_creacion,
					    tipo_creacion = @tipo_creacion,
					    total = @total
                    ON DUPLICATE KEY UPDATE
                        vale_efectivo_id = vale_efectivo_id
			    ";

			    Dictionary<string, object> parametros_vale_efectivo = new Dictionary<string, object>();
			    parametros_vale_efectivo.Add("vale_efectivo_id", uuid);
			    parametros_vale_efectivo.Add("sucursal_id", sucursal_id);
			    parametros_vale_efectivo.Add("empleado_id", empleado_id);
                parametros_vale_efectivo.Add("tipo_creacion","CANJPAR");
                parametros_vale_efectivo.Add("fecha_creacion", Misc_helper.fecha());
			    parametros_vale_efectivo.Add("elemento_id", venta_id);
			    parametros_vale_efectivo.Add("total", total);

			    conector.Insert(sql, parametros_vale_efectivo);

			    sql = @"
				    SELECT
					    vale_efectivo_id AS vale_efectivo_id
				    FROM
					    farmacontrol_global.vales_efectivo
				    WHERE
					    vale_efectivo_id = @vale_efectivo_id
				    LIMIT 1
			    ";

			    Dictionary<string,object> parametros = new Dictionary<string, object>();
			    parametros.Add("vale_efectivo_id", uuid);

			    conector.Select(sql, parametros);

			    if (conector.result_set.Rows.Count > 0)
			    {
                    DAO_Cola_operaciones.insertar_cola_operaciones(empleado_id, "rest/vales_efectivo", "registrar_vale_efectivo", parametros_vale_efectivo, "PARA ENVIO SERVIDOR PRINCIPAL");
				    return conector.result_set.Rows[0]["vale_efectivo_id"].ToString();
			    }
            }
            catch(Exception ex)
            {
                Log_error.log(ex);
            }

			return "";
		}

		public decimal get_total_devolucion_prepago(long prepago_id)
		{
			string sql = @"
				SELECT
					COALESCE(SUM( (((tmp.precio_publico - tmp.importe_descuento) + tmp.importe_iva) * tmp.cantidad) ), 0) AS total
				FROM
					(
						SELECT
							articulos.precio_publico,
							(articulos.pct_descuento * articulos.precio_publico) AS importe_descuento,
							(articulos.pct_iva * (articulos.precio_publico - (articulos.pct_descuento * articulos.precio_publico))) AS importe_iva,
							(detallado_prepagos.cantidad - detallado_prepagos.cantidad_entregada) AS cantidad
						FROM
							farmacontrol_global.articulos
						JOIN farmacontrol_local.detallado_prepagos USING(articulo_id)
						WHERE
							detallado_prepagos.prepago_id = @prepago_id
					) AS tmp
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("prepago_id", prepago_id);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				return Convert.ToDecimal(conector.result_set.Rows[0]["total"]);
			}

			return 0;
		}

		public string generar_vale_efectivo_prepago(long prepago_id)
		{
			string uuid = Misc_helper.uuid();
			int empleado_id = (int)FORMS.comunes.Principal.empleado_id;
			int sucursal_id = Convert.ToInt32(Config_helper.get_config_local("sucursal_id"));
			decimal total = get_total_devolucion_prepago(prepago_id);

			//FALTA REGISTRAR EL VALE EFECTIVO CON REST EN LA TABLA GLOBAL DEL SERVIDOR PRINCIPAL

			string sql = @"
				INSERT INTO
					farmacontrol_global.vales_efectivo
				SET
					vale_efectivo_id = @vale_efectivo_id,
					sucursal_id = @sucursal_id,
					empleado_id = @empleado_id,
					elemento_id = @elemento_id,
					fecha_creacion = @fecha_creacion,
					tipo_creacion = @tipo_creacion,
					total = @total
                ON DUPLICATE KEY UPDATE
                    vale_efectivo_id = vale_efectivo_id
			";

			Dictionary<string, object> parametros_vale_efectivo = new Dictionary<string, object>();
			parametros_vale_efectivo.Add("vale_efectivo_id", uuid);
			parametros_vale_efectivo.Add("sucursal_id", sucursal_id);
			parametros_vale_efectivo.Add("empleado_id", empleado_id);
			parametros_vale_efectivo.Add("elemento_id", prepago_id);
            parametros_vale_efectivo.Add("fecha_creacion", Misc_helper.fecha());
            parametros_vale_efectivo.Add("tipo_creacion","PREPAGO");
			parametros_vale_efectivo.Add("total", total);

			conector.Insert(sql, parametros_vale_efectivo);

			sql = @"
				SELECT
					vale_efectivo_id AS vale_efectivo_id
				FROM
					farmacontrol_global.vales_efectivo
				WHERE
					vale_efectivo_id = @vale_efectivo_id
				LIMIT 1
			";

			Dictionary<string,object> parametros = new Dictionary<string, object>();
			parametros.Add("vale_efectivo_id", uuid);

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
                DAO_Cola_operaciones.insertar_cola_operaciones(empleado_id, "rest/vales_efectivo", "registrar_vale_efectivo", parametros_vale_efectivo, "PARA ENVIO SERVIDOR PRINCIPAL");
				return conector.result_set.Rows[0]["vale_efectivo_id"].ToString();
			}

			return "";
		}
	}
}
