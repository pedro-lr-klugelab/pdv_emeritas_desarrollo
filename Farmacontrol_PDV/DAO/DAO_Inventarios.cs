using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;
using System.Data;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Inventarios
	{
		Conector conector = new Conector();

		public Dictionary<string,List<DTO_Detallado_diferencias>> get_detallado_diferencias_ticket(long inventario_id)
		{
			List<DTO_Detallado_diferencias> lista_diferencias_sobrante = new List<DTO_Detallado_diferencias>();
			List<DTO_Detallado_diferencias> lista_diferencias_faltante = new List<DTO_Detallado_diferencias>();

			Dictionary<string,List<DTO_Detallado_diferencias>> dic_faltantes =  new Dictionary<string,List<DTO_Detallado_diferencias>>();

			string sql = @"
				(
					SELECT
						(
							SELECT 
								amecop 
							FROM 
								farmacontrol_global.articulos_amecops 
							WHERE 
								articulo_id = no_inventariados.articulo_id 
							ORDER BY 
								articulos_amecops.amecop_principal 
							DESC
							LIMIT 1
						) AS amecop,
						RPAD(articulos.nombre, 27, ' ') AS producto,
						CONVERT(CONCAT((ABS(existencia) * -1), ' NI') USING utf8) AS cantidad,
						NULL AS sobrante,
						IF(total > 0, (total * -1) , total) AS faltante
					FROM
						farmacontrol_local.no_inventariados
					LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
					WHERE
						inventario_id = @inventario_id
					ORDER BY articulos.nombre ASC
				)
					UNION ALL
				(
					SELECT
						(
							SELECT 
								amecop 
							FROM 
								farmacontrol_global.articulos_amecops 
							WHERE 
								articulo_id = detallado_inventarios_folios.articulo_id 
							ORDER BY 
								articulos_amecops.amecop_principal 
							DESC
							LIMIT 1
						) AS amecop,
						RPAD(articulos.nombre,27,' ') AS producto,
						CONVERT(SUM(diferencia) USING utf8) AS cantidad,
						IF(SUM(diferencia) > 0, (SUM(diferencia) * detallado_inventarios_folios.precio_costo), IF(SUM(0) = 0, NULL, SUM(0)) ) AS sobrante,
						IF(SUM(diferencia) < 0, (SUM(diferencia) * detallado_inventarios_folios.precio_costo), IF(SUM(0) = 0, NULL, SUM(0)) ) AS faltante
					FROM
						farmacontrol_local.detallado_inventarios_folios
					LEFT JOIN farmacontrol_local.inventarios_folios USING(inventario_folio_id)
					LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
					WHERE
						inventarios_folios.inventario_id = @inventario_id
					AND
						diferencia != 0
					GROUP BY
						articulo_id
				)
				ORDER BY producto ASC
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("inventario_id", inventario_id);

			conector.Select(sql, parametros);

			var result_set = conector.result_set;

			if (result_set.Rows.Count > 0)
			{
				decimal? nullable = null;

				foreach (DataRow row in result_set.Rows)
				{
					DTO_Detallado_diferencias detallado = new DTO_Detallado_diferencias();
					detallado.amecop = row["amecop"].ToString().Substring(row["amecop"].ToString().Length - 4, 4);
					detallado.nombre = row["producto"].ToString();
					detallado.diferencia = row["cantidad"].ToString();
					detallado.sobrante = (row["sobrante"].ToString().Equals("")) ? nullable : Convert.ToDecimal(row["sobrante"]);
					detallado.faltante = (row["faltante"].ToString().Equals("")) ? nullable : Convert.ToDecimal(row["faltante"]) ;

					if(detallado.sobrante == null)
					{
						lista_diferencias_faltante.Add(detallado);
					}
					else
					{
						lista_diferencias_sobrante.Add(detallado);
					}
				}
			}

			dic_faltantes.Add("faltantes",lista_diferencias_faltante);
			dic_faltantes.Add("sobrantes",lista_diferencias_sobrante);

			return dic_faltantes;
		}

		public List<DTO_Detallado_no_inventariados> get_detallado_no_inventariados(long inventario_id)
		{
			List<DTO_Detallado_no_inventariados> lista_no_inventariados =  new List<DTO_Detallado_no_inventariados>();

			string sql = @"
				SELECT
					no_inventariados.articulo_id AS articulo_id,
					(
						SELECT 
							amecop 
						FROM 
							farmacontrol_global.articulos_amecops 
						WHERE 
							articulo_id = no_inventariados.articulo_id 
						ORDER BY 
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS amecop,
					RPAD(articulos.nombre, 37,' ') AS producto,
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					existencia,
					no_inventariados.precio_costo,
					total
				FROM
					farmacontrol_local.no_inventariados
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					inventario_id = @inventario_id
				ORDER BY articulos.nombre ASC
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("inventario_id", inventario_id);

			conector.Select(sql, parametros);

			var result_set = conector.result_set;

			if (result_set.Rows.Count > 0)
			{
				foreach (DataRow row in result_set.Rows)
				{
					DTO_Detallado_no_inventariados detallado =  new DTO_Detallado_no_inventariados();
					detallado.amecop = row["amecop"].ToString().Substring(row["amecop"].ToString().Length -4,4);
					detallado.nombre = row["producto"].ToString();
                    detallado.caducidad = Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");
					detallado.lote = row["lote"].ToString();
					detallado.precio_costo = Convert.ToDecimal(row["precio_costo"]);
					detallado.caducidades_lotes = get_caducidades_lotes_no_inventariados(inventario_id, Convert.ToInt32(row["articulo_id"]));

					lista_no_inventariados.Add(detallado);
				}
			}

			return lista_no_inventariados;
		}

		private List<Tuple<string, string, int>> get_caducidades_lotes_no_inventariados(long inventario_id, int articulo_id)
		{
			string sql = @"
				SELECT
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					COALESCE(existencia,0) AS cantidad
				FROM
					farmacontrol_local.no_inventariados
				WHERE
					inventario_id = @inventario_id
				AND
					articulo_id = @articulo_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("inventario_id", inventario_id);
			parametros.Add("articulo_id", articulo_id);

			conector.Select(sql, parametros);

			List<Tuple<string, string, int>> lista_caducidades = new List<Tuple<string, string, int>>();

			foreach (DataRow row in conector.result_set.Rows)
			{
				Tuple<string, string, int> tupla = new Tuple<string, string, int>(row["caducidad"].ToString(), row["lote"].ToString(), Convert.ToInt32(row["cantidad"]));
				lista_caducidades.Add(tupla);
			}

			return lista_caducidades;
		}

		#region METODOS DE LA CAPTURA DE INVENTARIOS

		public DTO_Validacion validar_producto_unico(long inventario_id, long inventario_folio_id, int articulo_id)
		{
			DTO_Validacion val = new DTO_Validacion();

			string sql = @"
				SELECT
					inventarios_folios.inventario_folio_id,
					COALESCE(terminales.nombre,'TERMINAL NO ASIGNADA') AS nombre_terminal
				FROM
					farmacontrol_local.detallado_inventarios_folios
				LEFT JOIN farmacontrol_local.inventarios_folios USING(inventario_folio_id)
				LEFT JOIN farmacontrol_local.terminales USING(terminal_id)
				WHERE
					articulo_id = @articulo_id
				AND
					inventarios_folios.inventario_id = @inventario_id
				AND
					inventarios_folios.inventario_folio_id != @inventario_folio_id 
				LIMIT 1
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("articulo_id",articulo_id);
			parametros.Add("inventario_id",inventario_id);
			parametros.Add("inventario_folio_id",inventario_folio_id);

			conector.Select(sql,parametros);

			var result = conector.result_set;

			if(result.Rows.Count > 0)
			{
				val.status = false;
				val.informacion = string.Format("Este producto esta siendo capturado en el folio {0} en la terminal {1}", result.Rows[0]["inventario_folio_id"].ToString(), result.Rows[0]["nombre_terminal"].ToString());
			}
			else
			{
				val.status = true;
			}

			return val;
		}

		public bool get_aceptando_capturas(long inventario_id)
		{
			string sql = @"
				SELECT
					aceptando_capturas
				FROM
					farmacontrol_local.inventarios
				WHERE
					inventario_id = @inventario_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("inventario_id",inventario_id);

			conector.Select(sql,parametros);

			var result = conector.result_set;

			if(result.Rows.Count > 0)
			{
				return Convert.ToBoolean(result.Rows[0]["aceptando_capturas"]);
			}

			return false;
		}

		public void set_empleado_id(long inventario_folio_id, long empleado_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.inventarios_folios
				SET
					empleado_id = @empleado_id
				WHERE
					inventario_folio_id = @inventario_folio_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("empleado_id",empleado_id);
			parametros.Add("inventario_folio_id",inventario_folio_id);

			conector.Update(sql,parametros);
		}

		public void set_terminal_id(long inventario_folio_id, int? terminal_id = null)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.inventarios_folios
				SET
					terminal_id = @terminal_id
				WHERE
					inventario_folio_id = @inventario_folio_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("terminal_id",terminal_id);
			parametros.Add("inventario_folio_id",inventario_folio_id);

			conector.Update(sql,parametros);
		}

		public DataTable elimar_detallado_inventario_folio(long inventario_folio_id, long detallado_inventario_folio_id)
		{
			string sql = @"
				DELETE FROM
					farmacontrol_local.detallado_inventarios_folios
				WHERE
					detallado_inventario_folio_id = @detallado_inventario_folio_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("detallado_inventario_folio_id", detallado_inventario_folio_id);

			conector.Delete(sql,parametros);

			return get_detallado_inventarios_folios(inventario_folio_id);
		}

		public DataTable insertar_detallado_inventario_folio(long inventario_folio_id, int articulo_id, string caducidad, string lote, int cantidad)
		{
			string sql = @"
				INSERT INTO
					farmacontrol_local.detallado_inventarios_folios
					(
						SELECT
							NULL AS detallado_inventario_folio_id,
							@inventario_folio_id AS inventario_folio_id,
							articulo_id,
							@caducidad AS caducidad,
							@lote AS lote,
							COALESCE(tmp_existencia_anterior.existencia, 0) AS existencia_anterior,
							@cantidad AS cantidad,
							@cantidad AS existencia_posterior,
							COALESCE( (IF(tmp_existencia_anterior.existencia = @cantidad, 0,IF(tmp_existencia_anterior.existencia > 0, IF(tmp_existencia_anterior.existencia > @cantidad, (ABS(tmp_existencia_anterior.existencia - @cantidad) * -1), ABS(tmp_existencia_anterior.existencia - @cantidad)), ABS(tmp_existencia_anterior.existencia - @cantidad)))),  @cantidad) AS diferencia,
							articulos.precio_costo AS precio_costo,
							(@cantidad * precio_costo) AS total,
							NOW() AS modified
						FROM
							farmacontrol_global.articulos
						LEFT JOIN 
						(
							SELECT
								articulo_id,
								COALESCE(existencia, 0) AS existencia
							FROM
								farmacontrol_local.existencias
							WHERE
								articulo_id = @articulo_id
							AND
								caducidad = @caducidad
							AND
								lote = @lote
						) AS tmp_existencia_anterior USING(articulo_id)
						WHERE
							articulo_id = @articulo_id
					)
					ON DUPLICATE KEY UPDATE
					cantidad 				= cantidad + @cantidad,
					total 					= (cantidad + @cantidad) * articulos.precio_costo,
					existencia_posterior 	= existencia_posterior + @cantidad,
					diferencia 				= COALESCE( (IF(tmp_existencia_anterior.existencia = cantidad, 0,IF(tmp_existencia_anterior.existencia > 0, IF(tmp_existencia_anterior.existencia > cantidad, (ABS(tmp_existencia_anterior.existencia - cantidad) * -1), ABS(tmp_existencia_anterior.existencia - cantidad)), ABS(tmp_existencia_anterior.existencia - cantidad)))) , cantidad)
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("inventario_folio_id",inventario_folio_id);
			parametros.Add("articulo_id",articulo_id);
			parametros.Add("caducidad",caducidad);
			parametros.Add("lote",lote);
			parametros.Add("cantidad",cantidad);

			conector.Insert(sql,parametros);

			return get_detallado_inventarios_folios(inventario_folio_id);
		}

		public void set_comentario_inventario_folio(long inventario_folio_id, string comentario)
		{	
			string sql = @"
				UPDATE
					farmacontrol_local.inventarios_folios
				SET
					comentarios = @comentarios
				WHERE
					inventario_folio_id = @inventario_folio_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("inventario_folio_id",inventario_folio_id);
			parametros.Add("comentarios",comentario);

			conector.Update(sql,parametros);
		}

		public DataTable get_detallado_inventarios_folios(long inventario_folio_id)
		{
			string sql = @"
				SELECT
					detallado_inventario_folio_id,
					(
						SELECT 
							amecop 
						FROM 
							farmacontrol_global.articulos_amecops 
						WHERE 
							articulo_id = detallado_inventarios_folios.articulo_id 
						ORDER BY 
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS amecop,
					articulos.nombre AS producto,
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					existencia_anterior,
					cantidad,
					existencia_posterior,
					diferencia,
					detallado_inventarios_folios.precio_costo AS precio_costo,
					total
				FROM
					farmacontrol_local.detallado_inventarios_folios
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					inventario_folio_id = @inventario_folio_id
				GROUP BY
					detallado_inventario_folio_id
				ORDER BY
					inventario_folio_id 
				DESC
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("inventario_folio_id",inventario_folio_id);

			conector.Select(sql,parametros);

			var resulset = conector.result_set;

			if(resulset.Rows.Count > 0)
			{
				foreach(DataRow row in resulset.Rows)
				{
                    row["caducidad"] = Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");
				}
			}

			return resulset;
		}

		public DTO_Inventario_folio get_informacion_inventario_folio(long inventario_folio_id)
		{
			DTO_Inventario_folio dto_inventario_folio = new DTO_Inventario_folio();

			string sql = @"
				SELECT
					inventario_folio_id,
					IF(inventarios_folios.terminal_id IS NULL,0,inventarios_folios.terminal_id) AS terminal_id,
					inventario_id,
					empleado_id,
					IF(termina_empleado_id IS NULL,0,termina_empleado_id) AS termina_empleado_id,
					CAST(DATE_FORMAT(inventarios_folios.fecha_creado,'%Y-%m-%d %h:%i:%s') AS CHAR(50)) AS fecha_creado,
					CAST(DATE_FORMAT(inventarios_folios.fecha_terminado,'%Y-%m-%d %h:%i:%s') AS CHAR(50)) AS fecha_terminado,
					comentarios,
					(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = inventarios_folios.empleado_id) AS nombre_empleado_captura,
					IF(inventarios_folios.termina_empleado_id IS NULL,'',(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = inventarios_folios.termina_empleado_id) ) AS nombre_empleado_termina,
					COALESCE(terminales.nombre,'SIN TERMINAL') AS nombre_terminal
				FROM
					farmacontrol_local.inventarios_folios
				LEFT JOIN farmacontrol_local.terminales USING(terminal_id)
				WHERE
					inventario_folio_id = @inventario_folio_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("inventario_folio_id",inventario_folio_id);

			conector.Select(sql,parametros);

			var result_set = conector.result_set;

			if(result_set.Rows.Count > 0)
			{
				var row = result_set.Rows[0];

				dto_inventario_folio.inventario_folio_id = inventario_folio_id;
				dto_inventario_folio.terminal_id = Convert.ToInt32(row["terminal_id"]);
				dto_inventario_folio.inventario_id = Convert.ToInt32(row["inventario_id"]);
				dto_inventario_folio.empleado_id = Convert.ToInt64(row["empleado_id"]);
				dto_inventario_folio.termina_empleado_id = Convert.ToInt64(row["termina_empleado_id"]);
				dto_inventario_folio.fecha_creado = (row["fecha_creado"].ToString().Equals("")) ? "" : Misc_helper.fecha(row["fecha_creado"].ToString()) ;
				dto_inventario_folio.fecha_terminado = (row["fecha_terminado"].ToString().Equals("")) ? "" : Misc_helper.fecha(row["fecha_terminado"].ToString());
				dto_inventario_folio.nombre_empleado_captura = row["nombre_empleado_captura"].ToString();
				dto_inventario_folio.nombre_empleado_termina = row["nombre_empleado_termina"].ToString();
				dto_inventario_folio.nombre_terminal = row["nombre_terminal"].ToString();
				dto_inventario_folio.comentarios = row["comentarios"].ToString();
			}
			else
			{
				dto_inventario_folio.inventario_folio_id = 0;
			}

			return dto_inventario_folio;
		}

		public long get_inventario_folio_siguiente(long inventario_folio_id)
		{
			string sql = @"
				SELECT
					inventario_folio_id
				FROM
					farmacontrol_local.inventarios_folios
				WHERE
					inventario_folio_id > @inventario_folio_id
				ORDER BY
					inventario_folio_id
				ASC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("inventario_folio_id", inventario_folio_id);

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["inventario_folio_id"]);
			}

			return 0;
		}

		public long get_inventario_folio_atras(long inventario_folio_id)
		{
			string sql = @"
				SELECT
					inventario_folio_id
				FROM
					farmacontrol_local.inventarios_folios
				WHERE
					inventario_folio_id < @inventario_folio_id
				ORDER BY
					inventario_folio_id
				DESC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("inventario_folio_id", inventario_folio_id);

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["inventario_folio_id"]);
			}

			return 0;
		}

		public long get_inventario_folio_inicio()
		{
			string sql = @"
				SELECT
					inventario_folio_id
				FROM
					farmacontrol_local.inventarios_folios
				ORDER BY
					inventario_folio_id
				ASC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["inventario_folio_id"]);
			}

			return 0;
		}

		public long get_inventario_folio_fin()
		{
			string sql = @"
				SELECT
					inventario_folio_id
				FROM
					farmacontrol_local.inventarios_folios
				ORDER BY
					inventario_folio_id
				DESC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["inventario_folio_id"]);
			}

			return 0;
		}

		#endregion

		#region METODOS DE LAS JORNADAS

		public Dictionary<string,decimal> get_calculo_inventarios(long inventario_id)
		{
			string sql = @"
				SELECT
					COALESCE(SUM(existencia_anterior * precio_costo) , 0) + COALESCE(tmp.total_no_inventariados, 0) AS inventario_previo,
					COALESCE(SUM(cantidad * precio_costo),0) AS inventario_actual
				FROM
					farmacontrol_local.detallado_inventarios_folios
				LEFT JOIN farmacontrol_local.inventarios_folios USING(inventario_folio_id)
				LEFT JOIN 
					(
						SELECT
							@inventario_id AS inventario_id,
							SUM(total) AS total_no_inventariados
						FROM
							farmacontrol_local.no_inventariados
						WHERE
							inventario_id = @inventario_id
					) AS tmp ON tmp.inventario_id = inventarios_folios.inventario_id
				WHERE
					inventarios_folios.inventario_id = @inventario_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("inventario_id",inventario_id);
			conector.Select(sql,parametros);

			var result = conector.result_set;

			Dictionary<string,decimal> calculo_inventarios = new Dictionary<string,decimal>();

			if(result.Rows.Count > 0)
			{
				var row = result.Rows[0];
				calculo_inventarios.Add("inventario_previo",Convert.ToDecimal(row["inventario_previo"]));
				calculo_inventarios.Add("inventario_actual", Convert.ToDecimal(row["inventario_actual"]));
			}
			else
			{
				calculo_inventarios.Add("inventario_previo", 0);
				calculo_inventarios.Add("inventario_actual", 0);
			}

			return calculo_inventarios;
		}

		public DataTable get_productos_diferencias(long inventario_id)
		{
			/*
			string sql = @"
				SELECT
					ABS(articulos.amecop) AS amecop,
					articulos.nombre AS producto,
					SUM(diferencia) AS cantidad,
					IF(SUM(diferencia) > 0, (SUM(diferencia) * detallado_inventarios_folios.precio_costo), IF(SUM(0) = 0, NULL, SUM(0)) ) AS sobrante,
					IF(SUM(diferencia) < 0, (SUM(diferencia) * detallado_inventarios_folios.precio_costo), IF(SUM(0) = 0, NULL, SUM(0)) ) AS faltante
				FROM
					farmacontrol_local.detallado_inventarios_folios
				LEFT JOIN farmacontrol_local.inventarios_folios USING(inventario_folio_id)
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					inventarios_folios.inventario_id = @inventario_id
				AND
					diferencia != 0
				GROUP BY
					articulo_id
			";
			 * 
			 * */

			 string sql = @"
				(
					SELECT
						(
							SELECT
								amecop
							FROM
								farmacontrol_global.articulos_amecops
							WHERE
								articulos_amecops.articulo_id = articulos.articulo_id
							ORDER BY
								articulos_amecops.amecop_principal
							DESC
							LIMIT 1	
						) AS amecop,
						articulos.nombre AS producto,
						CONVERT(CONCAT(existencia, ' NI') USING utf8) AS cantidad,
						NULL AS sobrante,
						IF(total > 0, (total * -1) , total) AS faltante
					FROM
						farmacontrol_local.no_inventariados
					JOIN farmacontrol_global.articulos USING(articulo_id)
					WHERE
						inventario_id = @inventario_id
					ORDER BY articulos.nombre ASC
				)
					UNION ALL
				(
					SELECT
						(
							SELECT
								amecop
							FROM
								farmacontrol_global.articulos_amecops
							WHERE
								articulos_amecops.articulo_id = articulos.articulo_id
							ORDER BY
								articulos_amecops.amecop_principal
							DESC
							LIMIT 1	
						) AS amecop,
						articulos.nombre AS producto,
						SUM(diferencia) AS cantidad,
						IF(SUM(diferencia) > 0, (SUM(diferencia) * articulos.precio_costo), NULL) AS sobrante,
						IF(SUM(diferencia) < 0, (SUM(diferencia) * articulos.precio_costo), NULL ) AS faltante
					FROM
						farmacontrol_local.detallado_inventarios_folios
					JOIN farmacontrol_local.inventarios_folios USING(inventario_folio_id)
					JOIN farmacontrol_global.articulos USING(articulo_id)
					WHERE
						inventarios_folios.inventario_id = @inventario_id
					AND
						diferencia != 0
					GROUP BY
						articulo_id
				)
				ORDER BY producto ASC
			 ";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("inventario_id",inventario_id);

			conector.Select(sql,parametros);

			return conector.result_set;
		}

		public void registrar_no_inventariados(long inventario_id)
		{
			string sql = @"
				DELETE FROM
					farmacontrol_local.no_inventariados
				WHERE
					inventario_id = @inventario_id
			";

			Dictionary <string,object> parametros = new Dictionary<string,object>();
			parametros.Add("inventario_id",inventario_id);

			conector.Delete(sql,parametros);

			sql = @"
				INSERT INTO
					farmacontrol_local.no_inventariados
				(
					SELECT
						0 AS no_inventariado_id,
						@inventario_id AS inventario_id,
						existencias.articulo_id AS articulo_id,
						CONVERT(existencias.caducidad USING utf8) AS caducidad,
						existencias.lote AS lote,
						existencias.existencia AS existencia,
						articulos.precio_costo AS precio_costo,
						(existencias.existencia * articulos.precio_costo) AS total
					FROM
						farmacontrol_local.existencias
					LEFT JOIN 
					(
						SELECT
							articulo_id,
							caducidad,
							lote
						FROM
							farmacontrol_local.detallado_inventarios_folios
						LEFT JOIN farmacontrol_local.inventarios_folios USING(inventario_folio_id)
						WHERE
							inventarios_folios.inventario_id = @inventario_id
						GROUP BY
							articulo_id,caducidad,lote
					) AS tmp_detallado USING(articulo_id,caducidad,lote)
					LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
					WHERE
						tmp_detallado.articulo_id IS NULL
					AND
						tmp_detallado.caducidad IS NULL
					AND
						tmp_detallado.lote IS NULL
					ORDER BY articulos.nombre ASC
				)
			";

			conector.Insert(sql,parametros);
		}

		public DataTable get_no_inventariados(long inventario_id, bool registrar = false)
		{
			if(registrar)
			{
				registrar_no_inventariados(inventario_id);	
			}

			string sql = @"
				SELECT
					IF((COUNT(articulos_amecops.amecop)) > 1, CONCAT_WS(' ',(ABS(articulos_amecops.amecop)), ' *'), ABS(articulos_amecops.amecop)) AS amecop,
					articulos.nombre AS producto,
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					existencia,
					no_inventariados.precio_costo,
					total
				FROM
					farmacontrol_local.no_inventariados
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id)
				WHERE
					inventario_id = @inventario_id
				GROUP BY no_inventariados.articulo_id, no_inventariados.caducidad, no_inventariados.lote
				ORDER BY articulos.nombre ASC
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("inventario_id",inventario_id);

			conector.Select(sql,parametros);

			var result_set = conector.result_set;

			if(result_set.Rows.Count > 0)
			{
				foreach(DataRow row in result_set.Rows)
				{
                    row["caducidad"] = Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");
				}
			}

			return result_set;
		}

		public long set_aceptando_capturas(long inventario_id, bool aceptando_capturas)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.inventarios
				SET
					aceptando_capturas = @aceptando_capturas
				WHERE
					inventario_id = @inventario_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("aceptando_capturas", aceptando_capturas);
			parametros.Add("inventario_id", inventario_id);

			conector.Update(sql, parametros);

			return conector.filas_afectadas;
		}

		public DTO_Validacion crear_inventario_folio(long inventario_id, long empleado_id)
		{
			DTO_Validacion val = new DTO_Validacion();

			string sql = @"
				INSERT INTO
					farmacontrol_local.inventarios_folios
				SET
					inventario_id = @inventario_id,
					empleado_id = @empleado_id,
					fecha_creado = NOW(),
					comentarios = ''
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("inventario_id", inventario_id);
			parametros.Add("empleado_id", empleado_id);

			conector.Insert(sql, parametros);

			if (conector.insert_id > 0)
			{
				val.status = true;
				val.informacion = "El folio de captura fue creado correctamente con el folio #" + conector.insert_id;
			}
			else
			{
				val.status = true;
				val.informacion = "Ocurrio un error al intentar registrar el folio de captura, notifique a su administrador";
			}

			return val;
		}

		public void set_comentario(long inventario_id, string comentario)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.inventarios
				SET
					comentarios = @comentario
				WHERE
					inventario_id = @inventario_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("inventario_id", inventario_id);
			parametros.Add("comentario", comentario);

			conector.Update(sql, parametros);
		}

		public DTO_Validacion terminar_jornada_inventario(long inventario_id, long empleado_id)
		{
			DTO_Validacion val = new DTO_Validacion();

			string sql = @"
				UPDATE
					farmacontrol_local.inventarios_folios
				SET
					fecha_terminado = NOW(),
					termina_empleado_id = @empleado_id
				WHERE
					inventario_id = @inventario_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("empleado_id", empleado_id);
			parametros.Add("inventario_id", inventario_id);

			conector.Update(sql,parametros);

			sql = @"
				DELETE FROM
					farmacontrol_local.existencias
			";

			conector.Delete(sql,parametros);

			sql = @"
				INSERT INTO
					farmacontrol_local.existencias
				(
					SELECT
						0 AS existencia_id,
						articulo_id,
						caducidad,
						lote,
						cantidad AS existencia,
						NOW() AS modified
					FROM
						farmacontrol_local.detallado_inventarios_folios
					LEFT JOIN farmacontrol_local.inventarios_folios USING(inventario_folio_id)
					WHERE
						inventarios_folios.inventario_id = @inventario_id
				)
			";

			conector.Insert(sql,parametros);

            sql = @"
				INSERT INTO
					farmacontrol_local.kardex
				(
					SELECT
						0 AS kardex_id,
						@terminal_id AS terminal_id,
						NOW() AS fecha_datetime,
						NOW() AS fecha_date,
						detallado_inventarios_folios.articulo_id AS articulo_id,
                        detallado_inventarios_folios.caducidad AS caducidad,
                        detallado_inventarios_folios.lote AS lote,
                        'INVENTARIO' AS tipo,
                        detallado_inventarios_folios.inventario_folio_id AS elemento_id,
                        detallado_inventarios_folios.inventario_folio_id AS folio,
                        detallado_inventarios_folios.existencia_anterior AS existencia_anterior,
                        detallado_inventarios_folios.diferencia AS cantidad,
                        detallado_inventarios_folios.existencia_posterior AS existencia_posterior,
                        0 AS es_importado, 
						NOW() AS modified
					FROM
						farmacontrol_local.detallado_inventarios_folios
					LEFT JOIN farmacontrol_local.inventarios_folios USING(inventario_folio_id)
					WHERE
						inventarios_folios.inventario_id = @inventario_id
				)
                ON DUPLICATE KEY UPDATE
                    kardex_id = kardex_id
			";

            parametros.Add("terminal_id",(long)Misc_helper.get_terminal_id());

            conector.Insert(sql, parametros);

            sql = @"
				INSERT INTO
					farmacontrol_local.kardex
				(
					SELECT
						0 AS kardex_id,
						@terminal_id AS terminal_id,
						NOW() AS fecha_datetime,
						NOW() AS fecha_date,
						no_inventariados.articulo_id AS articulo_id,
                        no_inventariados.caducidad AS caducidad,
                        no_inventariados.lote AS lote,
                        'INVENTARIO' AS tipo,
                        no_inventariados.inventario_id AS elemento_id,
                        no_inventariados.inventario_id AS folio,
                        no_inventariados.existencia AS existencia_anterior,
                        (no_inventariados.existencia * -1) AS cantidad,
                        0 AS existencia_posterior,
                        0 AS es_importado,
						NOW() AS modified
					FROM
						farmacontrol_local.no_inventariados
					WHERE
						no_inventariados.inventario_id = @inventario_id
				)
                ON DUPLICATE KEY UPDATE
                    kardex_id = kardex_id
			";

            conector.Insert(sql, parametros);

			sql = @"
				UPDATE
					farmacontrol_local.inventarios
				SET
					termina_empleado_id = @empleado_id,
					fecha_fin = NOW()
				WHERE
					inventario_id = @inventario_id
			";

			conector.Update(sql, parametros);

			if (conector.filas_afectadas > 0)
			{
				val.status = true;
				val.informacion = "Jornada de inventario terminado correctamente";
			}
			else
			{
				val.status = false;
				val.informacion = "Ocurrio un problema interno al intentar terminar la jornada de inventario, notifique a su administrador";
			}

			return val;
		}

		public DTO_Validacion crear_jornada_inventario(long empleado_id)
		{
			DTO_Validacion dto_validacion = new DTO_Validacion();
			dto_validacion.status = false;

			string sql = @"
				SELECT
					inventario_id
				FROM
					farmacontrol_local.inventarios
				WHERE
					fecha_fin IS NULL
			";


			Dictionary<string, object> parametros = new Dictionary<string, object>();

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count == 0)
			{
				int terminal_id = (int)Misc_helper.get_terminal_id();

				sql = @"
					INSERT INTO
						farmacontrol_local.inventarios
					SET
						terminal_id = @terminal_id,
						empleado_id = @empleado_id,
						fecha_inicio = NOW()
				";

				parametros = new Dictionary<string, object>();
				parametros.Add("terminal_id", terminal_id);
				parametros.Add("empleado_id", empleado_id);

				conector.Insert(sql, parametros);

				if (conector.insert_id > 0)
				{
					dto_validacion.informacion = "Jornada de inventario creada correctamente";
					dto_validacion.status = true;
				}
				else
				{
					dto_validacion.status = false;
					dto_validacion.informacion = "Ocurrio un error interno al intentar crear el inventario, notifique a su administrador";
					dto_validacion.elemento_id = Convert.ToInt32(conector.insert_id);
				}
			}
			else
			{
				dto_validacion.status = false;
				dto_validacion.informacion = "Existe una jornada de inventario en proceso, imposible crear otra";
			}

			return dto_validacion;
		}

		public List<DTO_Inventario_folio_jornada> get_inventarios_folios(long inventario_id)
		{
			List<DTO_Inventario_folio_jornada> lista = new List<DTO_Inventario_folio_jornada>();

			string sql = @"
				SELECT
					inventario_folio_id,
					COALESCE(terminales.nombre,'SIN TERMINAL') AS nombre_terminal,
					comentarios,
					IF(fecha_terminado IS NULL, 'CAPTURANDO','CERRADO') AS estado
				FROM
					farmacontrol_local.inventarios_folios
				LEFT JOIN farmacontrol_local.terminales USING(terminal_id)
				WHERE
					inventario_id = @inventario_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("inventario_id", inventario_id);

			conector.Select(sql, parametros);

			var result = conector.result_set;

			foreach(DataRow row in result.Rows)
			{
				DTO_Inventario_folio_jornada folio = new DTO_Inventario_folio_jornada();
				folio.inventario_folio_id = Convert.ToInt64(row["inventario_folio_id"]);	
				folio.terminal = row["nombre_terminal"].ToString();
				folio.estado = row["estado"].ToString();
				folio.comentarios = row["comentarios"].ToString();

				lista.Add(folio);
			}

			return lista;
		}

		public DTO_Inventario get_informacion_inventario(long inventario_id)
		{
			DTO_Inventario dto_inventario = new DTO_Inventario();

			string sql = @"
				SELECT
					inventario_id,
					COALESCE(terminal_id, 0) AS terminal_id,
					empleado_id,
					aceptando_capturas,
					COALESCE(termina_empleado_id,0) AS termina_empleado_id,
					CAST(DATE_FORMAT(inventarios.fecha_inicio,'%Y-%m-%d %h:%i:%s') AS CHAR(50)) AS fecha_inicio,
					CAST(DATE_FORMAT(inventarios.fecha_fin,'%Y-%m-%d %h:%i:%s') AS CHAR(50)) AS fecha_fin,
					inventarios.comentarios AS comentarios,
					(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = inventarios.empleado_id) AS nombre_empleado_captura,
					IF(inventarios.termina_empleado_id IS NULL,'',(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = inventarios.termina_empleado_id) ) AS nombre_empleado_termina
				FROM
					farmacontrol_local.inventarios
				WHERE
					inventario_id = @inventario_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("inventario_id", inventario_id);

			conector.Select(sql, parametros);

			var result_set = conector.result_set;

			if (result_set.Rows.Count > 0)
			{
				var row = result_set.Rows[0];

				dto_inventario.inventario_id = inventario_id;
				dto_inventario.terminal_id = Convert.ToInt32(row["terminal_id"]);
				dto_inventario.empleado_id = Convert.ToInt32(row["empleado_id"]);
				dto_inventario.termina_empleado_id = Convert.ToInt32(row["termina_empleado_id"]);

				DateTime? date_null = null;

				dto_inventario.fecha_inicio = (row["fecha_inicio"].ToString().Equals("")) ? date_null : Convert.ToDateTime(row["fecha_inicio"]);
				dto_inventario.fecha_fin = (row["fecha_fin"].ToString().Equals("")) ? date_null : Convert.ToDateTime(row["fecha_fin"]);

				dto_inventario.comentarios = row["comentarios"].ToString();
				dto_inventario.nombre_empleado_captura = row["nombre_empleado_captura"].ToString();
				dto_inventario.nombre_empleado_termina = row["nombre_empleado_termina"].ToString();
				dto_inventario.aceptando_capturas = Convert.ToBoolean(row["aceptando_capturas"].ToString());
			}
			else
			{
				dto_inventario.inventario_id = 0;
			}

			return dto_inventario;
		}

		public long get_inventario_siguiente(long inventario_id)
		{
			string sql = @"
				SELECT
					inventario_id
				FROM
					farmacontrol_local.inventarios
				WHERE
					inventario_id > @inventario_id
				ORDER BY
					inventario_id
				ASC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("inventario_id", inventario_id);

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["inventario_id"]);
			}

			return 0;
		}

		public long get_inventario_atras(long inventario_id)
		{
			string sql = @"
				SELECT
					inventario_id
				FROM
					farmacontrol_local.inventarios
				WHERE
					inventario_id < @inventario_id
				ORDER BY
					inventario_id
				DESC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("inventario_id", inventario_id);

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["inventario_id"]);
			}

			return 0;
		}

		public long get_inventario_inicio()
		{
			string sql = @"
				SELECT
					inventario_id
				FROM
					farmacontrol_local.inventarios
				ORDER BY
					inventario_id
				ASC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["inventario_id"]);
			}

			return 0;
		}

		public long get_inventario_fin()
		{
			string sql = @"
				SELECT
					inventario_id
				FROM
					farmacontrol_local.inventarios
				ORDER BY
					inventario_id
				DESC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["inventario_id"]);
			}

			return 0;
		}
			
		#endregion
	}
}
