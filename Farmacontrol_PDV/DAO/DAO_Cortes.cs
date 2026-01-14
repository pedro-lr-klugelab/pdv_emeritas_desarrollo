using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using System.Data;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.CLASSES.PRINT;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Cortes
	{
		Conector conector  = new Conector();

		public decimal get_total_encargos_prepagados(bool es_total = false)
		{
			string sql = string.Format(@"
				SELECT
					COALESCE(SUM(COALESCE(monto,0)),0) AS total
				FROM
					farmacontrol_local.prepagos
				WHERE
					terminal_id = @terminal_id
                AND 
                    fecha_canje IS NULL
                AND
                    fecha_cancelado IS NULL
                AND
                    {0} IS NULL
                AND
                    fecha_pago BETWEEN
                (
                    SELECT
                        fecha
                    FROM
                        farmacontrol_local.cortes
                    WHERE
                        cortes.tipo = @tipo
                    AND
                        cortes.terminal_id = @terminal_id
                    ORDER BY cortes.corte_id DESC
                    LIMIT 1
                )
                AND NOW()", es_total ? "corte_total_id" : "corte_parcial_id" );

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("terminal_id",Convert.ToInt32(Misc_helper.get_terminal_id()));
            parametros.Add("tipo", es_total ? "TOTAL" : "PARCIAL");

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				return Convert.ToDecimal(conector.result_set.Rows[0]["total"]);
			}

			return 0;
		}

		public decimal get_total_encargos_prepagados_canjeados(bool es_total = false)
		{

			string sql = string.Format(@"
				SELECT
					COALESCE(SUM(COALESCE(monto,0)),0) AS total
				FROM
					farmacontrol_local.prepagos
				WHERE
					fecha_canje IS NOT NULL
				AND
					terminal_id = @terminal_id
                AND
                    fecha_canje BETWEEN
                (
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
                )
                AND NOW()
			",es_total ? "TOTAL" : "PARCIAL");

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("terminal_id", Convert.ToInt32(Misc_helper.get_terminal_id()));
            parametros.Add("tipo",es_total ? "TOTAL" : "PARCIAL");

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToDecimal(conector.result_set.Rows[0]["total"]);
			}

			return 0;
		}

		public decimal get_total_encargos_prepagados_cancelados(bool es_total = false)
		{
			string sql = @"
			    SELECT
					COALESCE(SUM(COALESCE(monto,0)),0) AS total
				FROM
					farmacontrol_local.prepagos
				WHERE
					fecha_cancelado IS NOT NULL
                AND
                    tipo_devolucion = 'EFECTIVO'
				AND
					cancela_terminal_id = @terminal_id
                AND
                    fecha_cancelado BETWEEN
                (
                    SELECT
                        fecha
                    FROM
                        farmacontrol_local.cortes
                    WHERE
                        cortes.tipo = @tipo
                    AND
                        cortes.terminal_id = @terminal_id
                    ORDER BY cortes.corte_id DESC
                    LIMIT 1
                )
                AND NOW()";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("terminal_id", Convert.ToInt32(Misc_helper.get_terminal_id()));
            parametros.Add("tipo", es_total ? "TOTAL" : "PARCIAL");

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToDecimal(conector.result_set.Rows[0]["total"]);
			}

			return 0;
		}

		public List<List<Tuple<string,string,decimal>>> get_cuentas(long corte_id)
		{
			List<List<Tuple<string,string,decimal>>> lista_cuentas = new List<List<Tuple<string,string,decimal>>>();

			string sql = @"
				SELECT
					nombre,
					pago_tipo_id
				FROM
					farmacontrol_global.pago_tipos
				WHERE
					usa_cuenta IS TRUE
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();

			conector.Select(sql,parametros);

			var result_cuentas = conector.result_set;

			foreach(DataRow row in result_cuentas.Rows)
			{	
				
			}


			return lista_cuentas;
		}

		public DTO_Corte get_informacion_corte(long corte_id)
		{
			DTO_Corte corte = new DTO_Corte();

			string sql = @"
				SELECT
					corte_id,
					COALESCE(MIN(ventas.venta_folio), 0) AS venta_inicial,
					COALESCE(MAX(ventas.venta_folio),0) AS venta_final,
					cortes.terminal_id AS terminal_id,
					corte_folio,
					cortes.empleado_id AS empleado_id,
					fecha,
					tipo,
					importe_bruto,
					importe_prepago_realizado,
					importe_prepago_canjeado,
					importe_prepago_cancelado,
					importe_excento,
					importe_descuento_excento,
					importe_gravado,
					importe_descuento_gravado,
					importe_iva,
					importe_ieps,
					importe_total,
                    importe_vales_emitidos,
					empleados.nombre AS nombre_empleado,
					terminales.nombre AS nombre_terminal
				FROM
					farmacontrol_local.cortes
				LEFT JOIN farmacontrol_global.empleados USING(empleado_id)
				LEFT JOIN farmacontrol_local.terminales USING(terminal_id)
				LEFT JOIN farmacontrol_local.ventas ON
					IF(tipo = 'TOTAL',ventas.corte_total_id = cortes.corte_id, ventas.corte_parcial_id = cortes.corte_id)
				WHERE
					corte_id = @corte_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("corte_id",corte_id);

			conector.Select(sql,parametros);

			var result = conector.result_set;

			if(result.Rows.Count > 0)
			{
				var row = result.Rows[0];
				corte.venta_inicial = Convert.ToInt32(row["venta_inicial"]);
				corte.venta_final = Convert.ToInt32(row["venta_final"]);
				corte.corte_id = Convert.ToInt64(row["corte_id"]);
				corte.corte_folio = Convert.ToInt64(row["corte_folio"]);
				corte.empleado_id = Convert.ToInt64(row["empleado_id"]);
				corte.tipo = row["tipo"].ToString();

				DateTime? date_null = null;

				corte.fecha = (row["fecha"].ToString().Equals("")) ? date_null : Convert.ToDateTime(row["fecha"]);
				corte.importe_bruto = Convert.ToDecimal(row["importe_bruto"]);
				corte.importe_prepagado = Convert.ToDecimal(row["importe_prepago_realizado"]);
				corte.importe_prepagado_cancelado = Convert.ToDecimal(row["importe_prepago_cancelado"]);
				corte.importe_prepagado_canjeado = Convert.ToDecimal(row["importe_prepago_canjeado"]);
				corte.importe_excento = Convert.ToDecimal(row["importe_excento"]);
				corte.importe_descuento_excento = Convert.ToDecimal(row["importe_descuento_excento"]);
				corte.importe_gravado = Convert.ToDecimal(row["importe_gravado"]);
				corte.importe_descuento_gravado = Convert.ToDecimal(row["importe_descuento_gravado"]);
				corte.importe_iva = Convert.ToDecimal(row["importe_iva"]);
				corte.importe_ieps = Convert.ToDecimal(row["importe_ieps"]);
				corte.importe_total = Convert.ToDecimal(row["importe_total"]);
                corte.vales_emitidos = Convert.ToDecimal(row["importe_vales_emitidos"]);
				corte.nombre_empleado = row["nombre_empleado"].ToString();
				corte.nombre_terminal = row["nombre_terminal"].ToString();
			}

			return corte;
		}

		public DTO_Validacion generar_corte_parcial(long empleado_id, DTO_Corte corte)
		{
			DTO_Validacion val = new DTO_Validacion();

			int terminal_id = (int)Misc_helper.get_terminal_id();

			string sql = @"
				INSERT INTO
					farmacontrol_local.cortes
				(
					SELECT
						0 AS corte_id,
						@terminal_id AS terminal_id,
						COALESCE(MAX(corte_folio), 0) + 1 AS corte_folio,
						@empleado_id AS empleado_id,
						NOW() AS fecha,
						'PARCIAL' AS tipo,
						@importe_bruto AS importe_bruto,
						@importe_prepago_realizado AS importe_prepago_realizado,
						@importe_prepago_canjeado AS importe_prepago_canjeado,
						@importe_prepago_cancelado AS importe_prepago_cancelado,
						@importe_excento AS importe_excento,
						@importe_descuento_excento AS importe_descuento_excento,
						@importe_gravado AS importe_gravado,
						@importe_descuento_gravado AS importe_descuento_gravado,
						@importe_iva AS importe_iva,
						@importe_ieps AS importe_ieps,
						@importe_total AS total,
                        @importe_vales_emitidos AS importe_vales_emitidos,
						NULL AS fecha_facturado,
						NOW() AS modified
					FROM
						farmacontrol_local.cortes
					WHERE
						terminal_id = @terminal_id
					ORDER BY corte_id DESC
					LIMIT 1
				)
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("terminal_id",terminal_id);
			parametros.Add("empleado_id",empleado_id);
			parametros.Add("importe_bruto",corte.importe_bruto);
			parametros.Add("importe_prepago_realizado",corte.importe_prepagado);
			parametros.Add("importe_prepago_canjeado",corte.importe_prepagado_canjeado);
			parametros.Add("importe_prepago_cancelado",corte.importe_prepagado_cancelado);
			parametros.Add("importe_excento",corte.importe_excento);
			parametros.Add("importe_descuento_excento",corte.importe_descuento_excento);
			parametros.Add("importe_gravado",corte.importe_gravado);
			parametros.Add("importe_descuento_gravado",corte.importe_descuento_gravado);
			parametros.Add("importe_iva",corte.importe_iva);
			parametros.Add("importe_ieps",corte.importe_ieps);
            parametros.Add("importe_vales_emitidos",corte.vales_emitidos);
			parametros.Add("importe_total",corte.importe_total);

			conector.Insert(sql,parametros);

			long corte_parcial_id = conector.insert_id;

			if(corte_parcial_id > 0)
			{
				parametros = new Dictionary<string, object>();
				parametros.Add("corte_parcial_id", corte_parcial_id);
				parametros.Add("terminal_id", Convert.ToInt64(Misc_helper.get_terminal_id()));

				sql = @"
					UPDATE
						farmacontrol_local.prepagos
					SET
						corte_parcial_id = @corte_parcial_id
					WHERE
						terminal_id = @terminal_id
					AND
						corte_parcial_id IS NULL
				";

				conector.Update(sql,parametros);

				sql = @"
					UPDATE
						farmacontrol_local.ventas
					SET
						corte_parcial_id = @corte_parcial_id
					WHERE
						farmacontrol_local.ventas.corte_parcial_id IS NULL
					AND
						farmacontrol_local.ventas.fecha_terminado IS NOT NULL
					AND
						farmacontrol_local.ventas.terminal_id = @terminal_id
				";

				conector.Update(sql, parametros);

				sql = @"
					UPDATE
						farmacontrol_local.cancelaciones
					SET
						corte_parcial_id = @corte_parcial_id
					WHERE
						farmacontrol_local.cancelaciones.corte_parcial_id IS NULL
					AND
						farmacontrol_local.cancelaciones.terminal_id = @terminal_id
				";

				conector.Update(sql, parametros);	

				val.status = true;
				val.informacion = "Corte parcial realizado correctamente con el folio #"+corte_parcial_id;
				val.elemento_id = Convert.ToInt32(corte_parcial_id);
			}
			else
			{
				val.status = false;
				val.informacion = "Ocurrio un problema al intentar registrar el corte parcial, notifique a su administrador!";
			}

            sql = @"
                UPDATE
                    farmacontrol_local.entregas_efectivo
                SET
                    corte_parcial_id = @corte_parcial_id
                WHERE
                    terminal_id = @terminal_id
                AND
                    corte_parcial_id IS NULL
            ";

            conector.Update(sql, parametros);

			return val;
		}

		public DTO_Validacion generar_corte_total(long empleado_id, DTO_Corte corte)
		{
			DTO_Validacion val = new DTO_Validacion();

			var val_cp = generar_corte_parcial(empleado_id,get_corte_parcial());

			if(val_cp.status)
			{
				int terminal_id = (int)Misc_helper.get_terminal_id();

				string sql = @"
				INSERT INTO
					farmacontrol_local.cortes
				(
					SELECT
						0 AS corte_id,
						@terminal_id AS terminal_id,
						COALESCE(MAX(corte_folio), 0) + 1 AS corte_folio,
						@empleado_id AS empleado_id,
						NOW() AS fecha,
						'TOTAL' AS tipo,
						@importe_bruto AS importe_bruto,
						@importe_prepago_realizado AS importe_prepago_realizado,
						@importe_prepago_canjeado AS importe_prepago_canjeado,
						@importe_prepago_cancelado AS importe_prepago_cancelado, 
						@importe_excento AS importe_excento,
						@importe_descuento_excento AS importe_descuento_excento,
						@importe_gravado AS importe_gravado,
						@importe_descuento_gravado AS importe_descuento_gravado,
						@importe_iva AS importe_iva,
						@importe_ieps AS importe_ieps,
						@importe_total AS total,
                        @importe_vales_emitidos AS importe_vales_emitidos,
						NULL AS fecha_facturado,
						NOW() AS modified
					FROM
						farmacontrol_local.cortes
					WHERE
						terminal_id = @terminal_id
					ORDER BY corte_id DESC
					LIMIT 1
				)
			";

				Dictionary<string, object> parametros = new Dictionary<string, object>();
				parametros.Add("terminal_id", terminal_id);
				parametros.Add("empleado_id", empleado_id);
				parametros.Add("importe_bruto", corte.importe_bruto);
				parametros.Add("importe_prepago_realizado",corte.importe_prepagado);
				parametros.Add("importe_prepago_canjeado",corte.importe_prepagado_canjeado);
				parametros.Add("importe_prepago_cancelado",corte.importe_prepagado_cancelado);
				parametros.Add("importe_excento", corte.importe_excento);
				parametros.Add("importe_descuento_excento", corte.importe_descuento_excento);
				parametros.Add("importe_gravado", corte.importe_gravado);
				parametros.Add("importe_descuento_gravado", corte.importe_descuento_gravado);
				parametros.Add("importe_iva", corte.importe_iva);
				parametros.Add("importe_ieps", corte.importe_ieps);
				parametros.Add("importe_total", corte.importe_total);
                parametros.Add("importe_vales_emitidos",corte.vales_emitidos);

				conector.Insert(sql, parametros);

				long corte_total_id = conector.insert_id;

				if (corte_total_id > 0)
				{
					parametros = new Dictionary<string, object>();
					parametros.Add("corte_total_id", corte_total_id);
					parametros.Add("terminal_id", Convert.ToInt64(Misc_helper.get_terminal_id()));

					sql = @"
						UPDATE
							farmacontrol_local.prepagos
						SET
							corte_total_id = @corte_total_id
						WHERE
							terminal_id = @terminal_id
						AND
							corte_total_id IS NULL
					";

					conector.Update(sql, parametros);

					sql = @"
						UPDATE
							farmacontrol_local.ventas
						SET
							corte_total_id = @corte_total_id
						WHERE
							farmacontrol_local.ventas.corte_total_id IS NULL
						AND
							farmacontrol_local.ventas.fecha_terminado IS NOT NULL
						AND
							farmacontrol_local.ventas.terminal_id = @terminal_id
					";

					conector.Update(sql, parametros);

					sql = @"
					UPDATE
						farmacontrol_local.cancelaciones
					SET
						corte_total_id = @corte_total_id
					WHERE
						farmacontrol_local.cancelaciones.corte_total_id IS NULL
					AND
						farmacontrol_local.cancelaciones.terminal_id = @terminal_id
				";

					conector.Update(sql, parametros);

					val.status = true;
					val.informacion = "Corte total realizado correctamente con el folio #" + corte_total_id;
					val.elemento_id = Convert.ToInt32(corte_total_id);

					Cortes ticket = new Cortes();
					ticket.construccion_ticket(val_cp.elemento_id);
					ticket.print();
				}
				else
				{
					val.status = false;
					val.informacion = "Ocurrio un problema al intentar registrar el corte total, notifique a su administrador!";
				}

                sql = @"
                    UPDATE
                        farmacontrol_local.entregas_efectivo
                    SET
                        corte_total_id = @corte_total_id
                    WHERE
                        terminal_id = @terminal_id
                    AND
                        corte_total_id IS NULL
                ";

                conector.Update(sql, parametros);
			}
			else
			{
				val = val_cp;
			}

			return val;
		}

		public decimal total_ventas_canceladas(bool es_corte_total = false)
		{
			string sql = string.Format(@"
				SELECT
					COALESCE(SUM(farmacontrol_local.detallado_cancelaciones.total), 0) AS totales
				FROM
					farmacontrol_local.cancelaciones
				LEFT JOIN farmacontrol_local.detallado_cancelaciones USING(cancelacion_id)
				WHERE
					farmacontrol_local.cancelaciones.{0} IS NULL
				AND
					farmacontrol_local.cancelaciones.terminal_id = @terminal_id
			",(es_corte_total == true) ? "corte_total_id" : "corte_parcial_id");

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("terminal_id",Convert.ToInt64(Misc_helper.get_terminal_id()));

			conector.Select(sql, parametros);

			return Convert.ToDecimal(conector.result_set.Rows[0]["totales"]);
		}

		public decimal total_ventas_canceladas(long corte_id, bool es_corte_total = false)
		{
			string sql = string.Format(@"
				SELECT
					COALESCE(SUM(farmacontrol_local.detallado_cancelaciones.total), 0) AS totales
				FROM
					farmacontrol_local.cancelaciones
				LEFT JOIN farmacontrol_local.detallado_cancelaciones USING(cancelacion_id)
				WHERE
					farmacontrol_local.cancelaciones.{0} = @corte_id
				AND
					farmacontrol_local.cancelaciones.terminal_id = @terminal_id
			",(es_corte_total == true) ? "corte_total_id" : "corte_parcial_id");

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("corte_id",corte_id);
			parametros.Add("terminal_id",Convert.ToInt64(Misc_helper.get_terminal_id()));

			conector.Select(sql, parametros);

			return Convert.ToDecimal(conector.result_set.Rows[0]["totales"]);
		}

		public DTO_Corte get_ultimo_corte(bool es_total = false)
		{
			DTO_Corte dto_corte = new DTO_Corte();

			string sql = @"
				SELECT
					corte_id,
					corte_folio,
					empleados.nombre AS nombre_empleado,
					fecha
				FROM
					farmacontrol_local.cortes
				LEFT JOIN farmacontrol_global.empleados USING(empleado_id)
				WHERE
					tipo = @tipo
				AND
					terminal_id = @terminal_id
				ORDER BY
					corte_id
				DESC
				LIMIT 1
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("tipo", (es_total == true) ? "TOTAL" : "PARCIAL");
			parametros.Add("terminal_id",Misc_helper.get_terminal_id());

			conector.Select(sql,parametros);

			var result = conector.result_set;

			if(result.Rows.Count > 0)
			{
				var row = result.Rows[0];
				dto_corte.corte_id = Convert.ToInt64(row["corte_id"]);
				dto_corte.corte_folio = Convert.ToInt64(row["corte_folio"]);
				dto_corte.nombre_empleado = row["nombre_empleado"].ToString();
				DateTime? date_null = null;
				dto_corte.fecha = (row["fecha"].ToString().Equals("")) ? date_null : Convert.ToDateTime(row["fecha"]);
			}

			return dto_corte;
		}

		public string get_ventas_canceladas(bool es_corte_total = false)
		{
			string sql = string.Format(@"
				SELECT
					COALESCE(GROUP_CONCAT(venta_id),0) AS cancelaciones
				FROM
					farmacontrol_local.cancelaciones
				WHERE
					{0} IS NULL
				AND
					terminal_id = @terminal_id
			", (es_corte_total) ? "corte_total_id" : "corte_parcial_id");

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("terminal_id",Convert.ToInt64(Misc_helper.get_terminal_id()));

			conector.Select(sql, parametros);

			var resul_cancelaciones = conector.result_set;

           /* string text = string.Join(",", resul_cancelaciones.AsEnumerable()
                                    .Select(x => x["cancelaciones"].ToString())
                                    .ToArray());
            */
			return resul_cancelaciones.Rows[0]["cancelaciones"].ToString();
		}

		public List<Tuple<string,decimal>> get_tipos_cambio(long corte_id)
		{
			List<Tuple<string,decimal>> lista_tipos_cambio =  new List<Tuple<string,decimal>>();
          
			string sql = @"
				SELECT
					nombre,
					COALESCE(tmp_ventas.total, 0) as total
				FROM
					farmacontrol_global.pago_tipos
				LEFT JOIN
					(
						SELECT 
							farmacontrol_local.ventas_pagos.pago_tipo_id,
							COALESCE(SUM(ventas_pagos.importe),0) AS total
						FROM
							farmacontrol_local.ventas_pagos
						LEFT JOIN farmacontrol_global.pago_tipos USING(pago_tipo_id)
						LEFT JOIN farmacontrol_local.ventas USING(venta_id)
						WHERE
							farmacontrol_local.ventas.fecha_terminado IS NOT NULL
						AND
							farmacontrol_local.ventas.corte_parcial_id = @corte_id
						GROUP BY
							farmacontrol_global.pago_tipos.pago_tipo_id
					) as tmp_ventas USING(pago_tipo_id)
				GROUP BY 
					farmacontrol_global.pago_tipos.pago_tipo_id
				ORDER BY
					nombre
				DESC
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("corte_id",corte_id);

			conector.Select(sql,parametros);

			var result = conector.result_set;

			foreach(DataRow row in result.Rows)
			{
				Tuple<string,decimal> tipo_cambio = new Tuple<string,decimal>(row["nombre"].ToString(),Convert.ToDecimal(row["total"]));
				lista_tipos_cambio.Add(tipo_cambio);
			}

			return lista_tipos_cambio;
		}

		public List<Tuple<string, decimal>> get_tipos_cambio_total(long corte_id)
		{
            List<Tuple<string, decimal>> lista_tipos_cambio = new List<Tuple<string, decimal>>();

			string sql = @"
				SELECT
					nombre,
					COALESCE(tmp_ventas.total, 0) as total
				FROM
					farmacontrol_global.pago_tipos
				LEFT JOIN
					(
						SELECT 
							farmacontrol_local.ventas_pagos.pago_tipo_id,
							COALESCE(SUM(ventas_pagos.importe),0) AS total
						FROM
							farmacontrol_local.ventas_pagos
						LEFT JOIN farmacontrol_global.pago_tipos USING(pago_tipo_id)
						LEFT JOIN farmacontrol_local.ventas USING(venta_id)
						WHERE
							farmacontrol_local.ventas.fecha_terminado IS NOT NULL
						AND
							farmacontrol_local.ventas.corte_total_id = @corte_id
						GROUP BY
							farmacontrol_global.pago_tipos.pago_tipo_id
					) as tmp_ventas USING(pago_tipo_id)
				GROUP BY 
					farmacontrol_global.pago_tipos.pago_tipo_id
				ORDER BY
					nombre
				DESC
			";

			Dictionary<string,object> parametros = new Dictionary<string, object>();
			parametros.Add("corte_id", corte_id);

			conector.Select(sql, parametros);

			var result = conector.result_set;

			foreach (DataRow row in result.Rows)
			{
				Tuple<string, decimal> tipo_cambio = new Tuple<string, decimal>(row["nombre"].ToString(), Convert.ToDecimal(row["total"]));
				lista_tipos_cambio.Add(tipo_cambio);
			}

			return lista_tipos_cambio;
		}

		public DTO_Corte get_corte_parcial()
		{
			DTO_Corte corte = new DTO_Corte();
			corte.importe_prepagado = get_total_encargos_prepagados();
			corte.importe_prepagado_cancelado = get_total_encargos_prepagados_cancelados();
			corte.importe_prepagado_canjeado = get_total_encargos_prepagados_canjeados();
			corte.importe_cancelaciones = total_ventas_canceladas();
			string ventas_canceladas = get_ventas_canceladas();
            /*
            ventas_canceladas = get_ventas_parciales_totales(ventas_canceladas);
            if (ventas_canceladas == "")
                ventas_canceladas = "0";



            corte.importe_cancelaciones = get_totales_venta_canceladas_parcial_total(ventas_canceladas);
            */
			string sql = @"
				SELECT
					COALESCE(MIN(venta_folio),0) AS venta_inicial,
					COALESCE(MAX(venta_folio),0) AS venta_final,
					COALESCE(SUM(total), 0) AS importe_bruto
				FROM
					farmacontrol_local.ventas
				LEFT JOIN farmacontrol_local.detallado_ventas USING(venta_id)
				WHERE
					farmacontrol_local.ventas.fecha_terminado IS NOT NULL
				AND
					farmacontrol_local.ventas.corte_parcial_id IS NULL
				AND
					farmacontrol_local.ventas.terminal_id = @terminal_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("terminal_id",Convert.ToInt64(Misc_helper.get_terminal_id()));

			conector.Select(sql,parametros);
			var result = conector.result_set;

			if(result.Rows.Count > 0)
			{
				var row = result.Rows[0];
				corte.venta_inicial = Convert.ToInt32(row["venta_inicial"]);
				corte.venta_final = Convert.ToInt32(row["venta_final"]);
				corte.importe_bruto = Convert.ToDecimal(row["importe_bruto"]);
				corte.importe_total = (Convert.ToDecimal(row["importe_bruto"]) - (corte.importe_cancelaciones));
			}

			parametros = new Dictionary<string, object>();
            parametros.Add("cancelaciones", ventas_canceladas);
			parametros.Add("terminal_id", Convert.ToInt64(Misc_helper.get_terminal_id()));
            /*
			sql = string.Format(@"
				SELECT
					COALESCE(SUM(COALESCE(farmacontrol_local.detallado_ventas.importe_iva, 0)), 0) AS importe_iva,
					COALESCE(SUM(farmacontrol_local.detallado_ventas.importe_ieps), 0) AS importe_ieps
				FROM
					farmacontrol_local.ventas
				LEFT JOIN farmacontrol_local.detallado_ventas USING(venta_id)
				WHERE
					farmacontrol_local.ventas.fecha_terminado IS NOT NULL
				AND
					farmacontrol_local.ventas.corte_parcial_id IS NULL
				AND
					farmacontrol_local.ventas.venta_id NOT IN ({0})
				AND
					farmacontrol_local.ventas.terminal_id = @terminal_id
			",ventas_canceladas);
            */


            sql = string.Format(@"
				SELECT
					(COALESCE(SUM(COALESCE(farmacontrol_local.detallado_ventas.importe_iva, 0)), 0) ) - TOTAL_DEV AS importe_iva,
					COALESCE(SUM(farmacontrol_local.detallado_ventas.importe_ieps), 0) AS importe_ieps
				FROM
					farmacontrol_local.ventas
				LEFT JOIN farmacontrol_local.detallado_ventas USING(venta_id)
				JOIN
				(  
				    SELECT
					        COALESCE(SUM(COALESCE(farmacontrol_local.detallado_ventas.importe_iva, 0)), 0) AS TOTAL_DEV
					FROM 
							farmacontrol_local.ventas
					INNER JOIN farmacontrol_local.detallado_ventas USING(venta_id)		
					WHERE
					        pct_iva != 0
					AND
							farmacontrol_local.ventas.venta_id IN({0})
				) AS TEMP_CANCELA
				WHERE
					farmacontrol_local.ventas.fecha_terminado IS NOT NULL
				AND
					farmacontrol_local.ventas.corte_parcial_id IS NULL
				
				AND
					farmacontrol_local.ventas.terminal_id = @terminal_id
			", ventas_canceladas);

           
		    conector.Select(sql,parametros);

			result = conector.result_set;

			if(result.Rows.Count > 0)
			{
				var row = conector.result_set.Rows[0];

				corte.importe_iva = Convert.ToDecimal(row["importe_iva"]);
				corte.importe_ieps = Convert.ToDecimal(row["importe_ieps"]);
			}

            /*
			sql =string.Format( @"
				SELECT
					COALESCE(SUM(COALESCE((detallado_ventas.total - detallado_ventas.importe_iva), 0)), 0) AS gravado,
					COALESCE(SUM(COALESCE((importe_descuento * cantidad), 0)), 0) AS importe_descuento_gravado
				FROM
					farmacontrol_local.ventas
				LEFT JOIN farmacontrol_local.detallado_ventas USING(venta_id)
				WHERE
					pct_iva != 0
				AND
					farmacontrol_local.ventas.fecha_terminado IS NOT NULL
				AND
					farmacontrol_local.ventas.corte_parcial_id IS NULL
				AND
					farmacontrol_local.ventas.venta_id NOT IN ({0})
				AND
					farmacontrol_local.ventas.terminal_id = @terminal_id
			", ventas_canceladas);
            */
            
            sql = string.Format(@"
				 SELECT
					( COALESCE(SUM(detallado_ventas.total - detallado_ventas.importe_ieps), 0) - COALESCE(SUM(detallado_ventas.importe_iva), 0) ) - TEMP_CANCELA.TOTAL_DEV   AS gravado,
					COALESCE(SUM(COALESCE((importe_descuento * cantidad), 0)), 0) AS importe_descuento_gravado
				FROM
					farmacontrol_local.ventas
				INNER JOIN farmacontrol_local.detallado_ventas USING(venta_id)
				JOIN
				(  
				    SELECT
                          COALESCE(SUM(detallado_ventas.total), 0) - COALESCE(SUM(COALESCE(farmacontrol_local.detallado_ventas.importe_iva, 0)), 0) AS TOTAL_DEV
					FROM 
							farmacontrol_local.ventas
					INNER JOIN farmacontrol_local.detallado_ventas USING(venta_id)		
					WHERE
					        pct_iva != 0
					AND
							farmacontrol_local.ventas.venta_id IN({0})
				) AS TEMP_CANCELA
				WHERE
					pct_iva != 0
				AND
					farmacontrol_local.ventas.fecha_terminado IS NOT NULL
				AND
					farmacontrol_local.ventas.corte_parcial_id IS NULL
				
		         AND
					farmacontrol_local.ventas.terminal_id = @terminal_id
			", ventas_canceladas);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				var row = conector.result_set.Rows[0];

				corte.importe_gravado = Convert.ToDecimal(row["gravado"]);
				corte.importe_descuento_gravado = Convert.ToDecimal(row["importe_descuento_gravado"]);
			}
            /*
			sql = string.Format( @"
				SELECT
					COALESCE(SUM(COALESCE((detallado_ventas.total - detallado_ventas.importe_ieps), 0)), 0) AS excento,
					COALESCE(SUM(COALESCE((importe_descuento * cantidad), 0)), 0) AS importe_descuento_excento
				FROM
					farmacontrol_local.ventas
				LEFT JOIN farmacontrol_local.detallado_ventas USING(venta_id)
				WHERE
					pct_iva = 0
				AND
					farmacontrol_local.ventas.fecha_terminado IS NOT NULL
				AND
					farmacontrol_local.ventas.corte_parcial_id IS NULL
				AND
					farmacontrol_local.ventas.venta_id NOT IN ({0})
				AND
					farmacontrol_local.ventas.terminal_id = @terminal_id
			", ventas_canceladas);
            */

            sql = string.Format(@"
				 SELECT
					COALESCE(SUM(COALESCE((detallado_ventas.total - detallado_ventas.importe_ieps), 0)), 0) - TEMP_CANCELA.TOTAL_DEV AS excento,
					COALESCE(SUM(COALESCE((importe_descuento * cantidad), 0)), 0) AS importe_descuento_excento
				FROM
					farmacontrol_local.ventas
				LEFT JOIN farmacontrol_local.detallado_ventas USING(venta_id)
				JOIN
				(  
				    SELECT
					        COALESCE(SUM(detallado_ventas.total), 0) AS TOTAL_DEV
					FROM 
							farmacontrol_local.ventas
					INNER JOIN farmacontrol_local.detallado_ventas USING(venta_id)		
					WHERE
					        pct_iva = 0
					AND
							farmacontrol_local.ventas.venta_id IN({0})
				) AS TEMP_CANCELA
				WHERE
					pct_iva = 0
				AND
					farmacontrol_local.ventas.fecha_terminado IS NOT NULL
				AND
					farmacontrol_local.ventas.corte_parcial_id IS NULL
				AND
					farmacontrol_local.ventas.terminal_id = @terminal_id
			", ventas_canceladas);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				var row = conector.result_set.Rows[0];
				corte.importe_excento = Convert.ToDecimal(row["excento"]);
				corte.importe_descuento_excento = Convert.ToDecimal(row["importe_descuento_excento"]);
			}

            long terminal_id = (long)Misc_helper.get_terminal_id();

            sql = @"
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
                    tipo = 'PARCIAL' 
                AND
                    terminal_id = @terminal_id
                ORDER 
                BY corte_id DESC 
                LIMIT 1
            ";

            parametros = new Dictionary<string, object>();
            parametros.Add("terminal_id",terminal_id);

            conector.Select(sql, parametros);

            string fecha_inicio = Misc_helper.fecha();

            if(conector.result_set.Rows.Count > 0)
            {
                fecha_inicio = Misc_helper.fecha(conector.result_set.Rows[0]["fecha"].ToString());
            }

            sql = @"
                SELECT
                    COALESCE(SUM(vales_efectivo.total), 0) AS total_vales
                FROM
                    farmacontrol_global.vales_efectivo
                JOIN farmacontrol_local.ventas ON ventas.venta_id = vales_efectivo.elemento_id
                WHERE
                    vales_efectivo.fecha_creacion BETWEEN @fecha_inicio AND NOW() 
                AND
                    vales_efectivo.fecha_cancelacion IS NULL
                AND
                    vales_efectivo.tipo_creacion != 'PREPAGO'
                AND
                    vales_efectivo.sucursal_id = @sucursal_id
                AND
                    ventas.terminal_id = @terminal_id
                AND
                    vales_efectivo.vale_efectivo_id NOT IN
                (
                    SELECT
                        cuenta AS vale_efectivo_id
                    FROM
                        farmacontrol_local.ventas_pagos
                    JOIN farmacontrol_local.ventas USING(venta_id)
                    JOIN farmacontrol_global.pago_tipos USING(pago_tipo_id)
                    WHERE
                        pago_tipos.etiqueta = @etiqueta
                    AND
                        ventas.terminal_id = @terminal_id
                    AND ventas.fecha_terminado BETWEEN @fecha_inicio AND NOW()
                )
            ";

            parametros = new Dictionary<string, object>();
            parametros.Add("terminal_id",terminal_id);
            parametros.Add("sucursal_id",Convert.ToInt64(Config_helper.get_config_local("sucursal_id")));
            parametros.Add("etiqueta","VFAR");
            parametros.Add("fecha_inicio",fecha_inicio);

            conector.Select(sql, parametros);

            if(conector.result_set.Rows.Count > 0)
            {
                var row = conector.result_set.Rows[0];
                corte.vales_emitidos = Convert.ToDecimal(row["total_Vales"]);
            }
            /*
            sql = @"
                SELECT
	                COALESCE(SUM(vales_efectivo.total), 0) AS total_vales
                FROM
	                farmacontrol_global.vales_efectivo
                JOIN farmacontrol_local.prepagos ON prepagos.prepago_id = vales_efectivo.elemento_id
                WHERE
	                vales_efectivo.fecha_creacion BETWEEN @fecha_inicio AND NOW() 
                AND
	                vales_efectivo.sucursal_id = @sucursal_id
                AND
	                prepagos.terminal_id = @terminal_id
                AND
	                vales_efectivo.vale_efectivo_id NOT IN
                (
	                SELECT
		                cuenta AS vale_efectivo_id
	                FROM
		                farmacontrol_local.ventas_pagos
	                JOIN farmacontrol_local.ventas USING(venta_id)
	                JOIN farmacontrol_global.pago_tipos USING(pago_tipo_id)
	                WHERE
		                pago_tipos.etiqueta = @etiqueta
	                AND
		                ventas.terminal_id = @terminal_id
	                AND ventas.fecha_terminado BETWEEN @fecha_inicio AND NOW()
                )
            ";

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                var row = conector.result_set.Rows[0];
                corte.vales_emitidos = Convert.ToDecimal(row["total_Vales"]);
            }
             */

			return corte;
		}

		public DTO_Corte get_corte_total()
		{
			DTO_Corte corte = new DTO_Corte();
			corte.importe_prepagado = get_total_encargos_prepagados(true);
			corte.importe_prepagado_cancelado = get_total_encargos_prepagados_cancelados(true);
			//corte.importe_prepagado_canjeado = get_total_encargos_prepagados_canjeados(true);
			//corte.importe_cancelaciones = total_ventas_canceladas(true);
			string ventas_canceladas = get_ventas_canceladas(true);
            ventas_canceladas = get_ventas_parciales_totales(ventas_canceladas);
            corte.importe_cancelaciones = get_totales_venta_canceladas_parcial_total(ventas_canceladas);

            if (ventas_canceladas == "")
                ventas_canceladas = "0";


			string sql = @"
				SELECT
					COALESCE(MIN(venta_folio),0) AS venta_inicial,
					COALESCE(MAX(venta_folio),0) AS venta_final,
					COALESCE(SUM(total), 0) AS importe_bruto
				FROM
					farmacontrol_local.ventas
				LEFT JOIN farmacontrol_local.detallado_ventas USING(venta_id)
				WHERE
					farmacontrol_local.ventas.fecha_terminado IS NOT NULL
				AND
					farmacontrol_local.ventas.corte_total_id IS NULL
				AND
					farmacontrol_local.ventas.terminal_id = @terminal_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("terminal_id", Convert.ToInt64(Misc_helper.get_terminal_id()));

			conector.Select(sql, parametros);
			var result = conector.result_set;

			if (result.Rows.Count > 0)
			{
				var row = result.Rows[0];
				corte.venta_inicial = Convert.ToInt32(row["venta_inicial"]);
				corte.venta_final = Convert.ToInt32(row["venta_final"]);
				corte.importe_bruto = Convert.ToDecimal(row["importe_bruto"]);
				corte.importe_total = ((Convert.ToDecimal(row["importe_bruto"])) - (corte.importe_cancelaciones));
			}

			parametros = new Dictionary<string, object>();
			parametros.Add("terminal_id", Convert.ToInt64(Misc_helper.get_terminal_id()));
            parametros.Add("canceladas", ventas_canceladas);
            /*
			sql = string.Format( @"
				SELECT
					COALESCE(SUM(COALESCE(farmacontrol_local.detallado_ventas.importe_iva, 0)), 0) AS importe_iva,
					COALESCE(SUM(farmacontrol_local.detallado_ventas.importe_ieps), 0) AS importe_ieps
				FROM
					farmacontrol_local.ventas
				LEFT JOIN farmacontrol_local.detallado_ventas USING(venta_id)
				WHERE
					farmacontrol_local.ventas.fecha_terminado IS NOT NULL
				AND
					farmacontrol_local.ventas.corte_total_id IS NULL
				AND
					farmacontrol_local.ventas.venta_id NOT IN ({0})
				AND
					farmacontrol_local.ventas.terminal_id = @terminal_id
			", ventas_canceladas);
            */

            sql = string.Format(@"
			   SELECT
					(COALESCE(SUM(COALESCE(farmacontrol_local.detallado_ventas.importe_iva, 0)), 0) ) - TOTAL_DEV AS importe_iva,
					COALESCE(SUM(farmacontrol_local.detallado_ventas.importe_ieps), 0) AS importe_ieps
				FROM
					farmacontrol_local.ventas
				LEFT JOIN farmacontrol_local.detallado_ventas USING(venta_id)
				JOIN
				(  
				    SELECT
					        COALESCE(SUM(COALESCE(farmacontrol_local.detallado_ventas.importe_iva, 0)), 0) AS TOTAL_DEV
					FROM 
							farmacontrol_local.ventas
					INNER JOIN farmacontrol_local.detallado_ventas USING(venta_id)		
					WHERE
					        pct_iva != 0
					AND
							farmacontrol_local.ventas.venta_id IN({0})
				) AS TEMP_CANCELA
				WHERE
					farmacontrol_local.ventas.fecha_terminado IS NOT NULL
				AND
					farmacontrol_local.ventas.corte_total_id IS NULL
				AND
					farmacontrol_local.ventas.terminal_id = @terminal_id	
			", ventas_canceladas);

           	

			conector.Select(sql, parametros);

			result = conector.result_set;

			if (result.Rows.Count > 0)
			{
				var row = conector.result_set.Rows[0];

				corte.importe_iva = Convert.ToDecimal(row["importe_iva"]);
				corte.importe_ieps = Convert.ToDecimal(row["importe_ieps"]);
			}
            /*
			sql = string.Format(@"
				SELECT
					COALESCE(SUM(COALESCE((detallado_ventas.total - detallado_ventas.importe_iva), 0)), 0) AS gravado,
					COALESCE(SUM(COALESCE((importe_descuento * cantidad), 0)), 0) AS importe_descuento_gravado
				FROM
					farmacontrol_local.ventas
				LEFT JOIN farmacontrol_local.detallado_ventas USING(venta_id)
				WHERE
					pct_iva != 0
				AND
					farmacontrol_local.ventas.fecha_terminado IS NOT NULL
				AND
					farmacontrol_local.ventas.corte_total_id IS NULL
				AND
					farmacontrol_local.ventas.venta_id NOT IN ({0})
				AND
					farmacontrol_local.ventas.terminal_id = @terminal_id
			", ventas_canceladas);
            */

              sql = string.Format(@"
			        SELECT
					    (COALESCE(SUM(COALESCE((detallado_ventas.total - detallado_ventas.importe_iva), 0)), 0) ) - TEMP_CANCELA.TOTAL_DEV  AS gravado,
					    COALESCE(SUM(COALESCE((importe_descuento * cantidad), 0)), 0) AS importe_descuento_gravado
				    FROM
					    farmacontrol_local.ventas
				    LEFT JOIN farmacontrol_local.detallado_ventas USING(venta_id)
				    JOIN
				    (  
				        SELECT
					            COALESCE(SUM(detallado_ventas.total), 0) AS TOTAL_DEV
					    FROM 
							    farmacontrol_local.ventas
					    INNER JOIN farmacontrol_local.detallado_ventas USING(venta_id)		
					    WHERE
					            pct_iva != 0
					    AND
							    farmacontrol_local.ventas.venta_id IN({0})
				    ) AS TEMP_CANCELA
				    WHERE
					    pct_iva != 0
				    AND
					    farmacontrol_local.ventas.fecha_terminado IS NOT NULL
				    AND
					    farmacontrol_local.ventas.corte_total_id IS NULL
				
				    AND
					    farmacontrol_local.ventas.terminal_id = @terminal_id
			", ventas_canceladas);

            
			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				var row = conector.result_set.Rows[0];

				corte.importe_gravado = Convert.ToDecimal(row["gravado"]);
				corte.importe_descuento_gravado = Convert.ToDecimal(row["importe_descuento_gravado"]);
			}
            /*
			sql = string.Format(@"
				SELECT
					COALESCE(SUM(COALESCE((detallado_ventas.total - detallado_ventas.importe_ieps), 0)), 0) AS excento,
					COALESCE(SUM(COALESCE((importe_descuento * cantidad), 0)), 0) AS importe_descuento_excento
				FROM
					farmacontrol_local.ventas
				LEFT JOIN farmacontrol_local.detallado_ventas USING(venta_id)
				WHERE
					pct_iva = 0
				AND
					farmacontrol_local.ventas.fecha_terminado IS NOT NULL
				AND
					farmacontrol_local.ventas.corte_total_id IS NULL
				AND
					farmacontrol_local.ventas.venta_id NOT IN ({0})
				AND
					farmacontrol_local.ventas.terminal_id = @terminal_id
			", ventas_canceladas);
            */


            sql = string.Format(@"
				 SELECT
					(COALESCE(SUM(COALESCE((detallado_ventas.total - detallado_ventas.importe_ieps), 0)), 0)) - TEMP_CANCELA.TOTAL_DEV AS excento,
					COALESCE(SUM(COALESCE((importe_descuento * cantidad), 0)), 0) AS importe_descuento_excento
				FROM
					farmacontrol_local.ventas
				LEFT JOIN farmacontrol_local.detallado_ventas USING(venta_id)
				JOIN
				(  
				    SELECT
					        COALESCE(SUM(detallado_ventas.total), 0) AS TOTAL_DEV
					FROM 
							farmacontrol_local.ventas
					INNER JOIN farmacontrol_local.detallado_ventas USING(venta_id)		
					WHERE
					        pct_iva = 0
					AND
							farmacontrol_local.ventas.venta_id IN({0})
				) AS TEMP_CANCELA
				WHERE
					pct_iva = 0
				AND
					farmacontrol_local.ventas.fecha_terminado IS NOT NULL
				AND
					farmacontrol_local.ventas.corte_total_id IS NULL

				AND
					farmacontrol_local.ventas.terminal_id = @terminal_id
			", ventas_canceladas);


			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				var row = conector.result_set.Rows[0];
				corte.importe_excento = Convert.ToDecimal(row["excento"]);
				corte.importe_descuento_excento = Convert.ToDecimal(row["importe_descuento_excento"]);
			}

            long terminal_id = (long)Misc_helper.get_terminal_id();

            sql = @"
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
                    tipo = 'TOTAL' 
                AND
                    terminal_id = @terminal_id
                ORDER 
                BY corte_id DESC 
                LIMIT 1
            ";

            parametros = new Dictionary<string, object>();
            parametros.Add("terminal_id", terminal_id);

            conector.Select(sql, parametros);

            string fecha_inicio = Misc_helper.fecha();

            if(conector.result_set.Rows.Count > 0)
            {
                fecha_inicio = Misc_helper.fecha(conector.result_set.Rows[0]["fecha"].ToString());
            }

            sql = @"
                SELECT
                    COALESCE(SUM(vales_efectivo.total), 0) AS total_vales
                FROM
                    farmacontrol_global.vales_efectivo
                JOIN farmacontrol_local.ventas ON ventas.venta_id = vales_efectivo.elemento_id
                WHERE
                    vales_efectivo.fecha_creacion BETWEEN @fecha_inicio AND NOW() 
                AND
                    vales_efectivo.fecha_cancelacion IS NULL
                AND
                    vales_efectivo.tipo_creacion != 'PREPAGO'
                AND
                    vales_efectivo.sucursal_id = @sucursal_id
                AND
                    ventas.terminal_id = @terminal_id
                AND
                    vales_efectivo.vale_efectivo_id NOT IN
                (
                    SELECT
                        cuenta AS vale_efectivo_id
                    FROM
                        farmacontrol_local.ventas_pagos
                    JOIN farmacontrol_local.ventas USING(venta_id)
                    JOIN farmacontrol_global.pago_tipos USING(pago_tipo_id)
                    WHERE
                        pago_tipos.etiqueta = @etiqueta
                    AND
                        ventas.terminal_id = @terminal_id
                    AND ventas.fecha_terminado BETWEEN @fecha_inicio AND NOW()
                )
            ";

            parametros = new Dictionary<string, object>();
            parametros.Add("terminal_id", terminal_id);
            parametros.Add("sucursal_id", Convert.ToInt64(Config_helper.get_config_local("sucursal_id")));
            parametros.Add("fecha_inicio", fecha_inicio);
            parametros.Add("etiqueta","VFAR");

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                var row = conector.result_set.Rows[0];
                corte.vales_emitidos = Convert.ToDecimal(row["total_Vales"]);
            }

            /*
            sql = @"
                SELECT
	                COALESCE(SUM(vales_efectivo.total), 0) AS total_vales
                FROM
	                farmacontrol_global.vales_efectivo
                JOIN farmacontrol_local.prepagos ON prepagos.prepago_id = vales_efectivo.elemento_id
                WHERE
	                vales_efectivo.fecha_creacion BETWEEN @fecha_inicio AND NOW() 
                AND
	                vales_efectivo.sucursal_id = @sucursal_id
                AND
	                prepagos.terminal_id = @terminal_id
                AND
	                vales_efectivo.vale_efectivo_id NOT IN
                (
	                SELECT
		                cuenta AS vale_efectivo_id
	                FROM
		                farmacontrol_local.ventas_pagos
	                JOIN farmacontrol_local.ventas USING(venta_id)
	                JOIN farmacontrol_global.pago_tipos USING(pago_tipo_id)
	                WHERE
		                pago_tipos.etiqueta = @etiqueta
	                AND
		                ventas.terminal_id = @terminal_id
	                AND ventas.fecha_terminado BETWEEN @fecha_inicio AND NOW()
                )
            ";

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                var row = conector.result_set.Rows[0];
                corte.vales_emitidos = Convert.ToDecimal(row["total_Vales"]);
            }*/

			return corte;
		}

        public decimal get_totales_venta_canceladas_parcial_total( string folioscancelados )
        {

            if (folioscancelados == "")
                folioscancelados = "0";

            string sql = string.Format(@"
				SELECT
					COALESCE(SUM(COALESCE(farmacontrol_local.detallado_ventas.total, 0)), 0) AS total	
				FROM
					farmacontrol_local.ventas
				INNER JOIN 
                    farmacontrol_local.detallado_ventas 
                USING(venta_id)
				WHERE
					farmacontrol_local.ventas.fecha_terminado IS NOT NULL
				AND
					farmacontrol_local.ventas.venta_id IN({0})
				AND
					farmacontrol_local.ventas.terminal_id = @terminal_id
			", folioscancelados);

          
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("terminal_id", Convert.ToInt64(Misc_helper.get_terminal_id()));

            conector.Select(sql, parametros);
            var result = conector.result_set;
            decimal total_cancelaciones = 0;
            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                total_cancelaciones = Convert.ToDecimal(row["total"]);
                
            }

            return total_cancelaciones;

        }

        public string get_ventas_parciales_totales(string folioscancelados)
        {
             string  text = "";
             if (folioscancelados != "")
             {

                 string sql = string.Format(@"
                SELECT
                       venta_id as venta, 
                       COUNT( detallado_cancelacion_id ) as total
                    FROM
                        farmacontrol_local.cancelaciones
                    LEFT JOIN
                        farmacontrol_local.detallado_cancelaciones	
                    USING(cancelacion_id)
                    WHERE
                        venta_id IN( {0} )
                    GROUP BY 	
                         venta_id
                    HAVING 
                          total > 0
			", folioscancelados);


                 Dictionary<string, object> parametros = new Dictionary<string, object>();
                 parametros.Add("terminal_id", Convert.ToInt64(Misc_helper.get_terminal_id()));

                 conector.Select(sql, parametros);

                 var resul_cancelaciones = conector.result_set;

                 text = string.Join(",", resul_cancelaciones.AsEnumerable()
                                         .Select(x => x["venta"].ToString())
                                         .ToArray());
             }
            return text;
        }

        #region horario de corte parcial
        public string get_horario_corte_parcial()
        {
            string sql = @"
				SELECT
					corte_parcial
				FROM
					farmacontrol_global.sucursales
				WHERE
                    sucursal_id = @sucursal_id
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("sucursal_id", Convert.ToInt64(Config_helper.get_config_local("sucursal_id")));

            conector.Select(sql, parametros);
            var result = conector.result_set;
            string corte_parcial_autorizado = "00:00:00";
            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                corte_parcial_autorizado = row["corte_parcial"].ToString();

            }


            return corte_parcial_autorizado;

        }
        #endregion

    }
}
