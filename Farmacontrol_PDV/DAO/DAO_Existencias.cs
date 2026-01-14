using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Farmacontrol_PDV.DTO;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Existencias
	{
		Conector conector = new Conector();

		public List<DTO_Existencia> get_articulos_existencias(string articulos_ids)
		{
			List<DTO_Existencia> lista_existencias = new List<DTO_Existencia>();

			string lista_articulos = articulos_ids;

			string sql = string.Format(@"
				SELECT
					existencias.articulo_id,
					CONVERT(existencias.caducidad USING utf8) AS caducidad,
					existencias.lote,
					COALESCE(SUM(existencia), 0) - COALESCE(existencia_mermas, 0) - COALESCE(existencia_devoluciones, 0) - COALESCE(existencia_apartados, 0) - COALESCE(existencia_cambio_fisico, 0) - COALESCE(existencia_ventas, 0) - COALESCE(existencia_traspasos, 0) - COALESCE(existencia_mayoreo, 0) AS existencia_vendible
				FROM
					farmacontrol_local.existencias
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
					articulo_id IN({0})
				GROUP BY articulo_id, caducidad, lote
				ORDER BY caducidad ASC
			", lista_articulos);

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			conector.Select(sql, parametros);

			var result = conector.result_set;

			foreach (DataRow row in result.Rows)
			{
				DTO_Existencia articulo_existencia = new DTO_Existencia();
				articulo_existencia.articulo_id = Convert.ToInt64(row["articulo_id"]);
				articulo_existencia.caducidad = row["caducidad"].ToString();
				articulo_existencia.existencia = Convert.ToInt64(row["existencia_vendible"]);
				articulo_existencia.lote = row["lote"].ToString();

				lista_existencias.Add(articulo_existencia);
			}

			return lista_existencias;	
		}

		public List<DTO_Existencia> get_existencias_articulos_formula(string articulos_ids)
		{
			List<DTO_Existencia> lista_existencias = new List<DTO_Existencia>();

            string sql = string.Format(@"
				SELECT
					articulo_id,
					volumen,
					existencia_actual AS existencia
				FROM
					farmacontrol_local.materias_primas
				WHERE
					articulo_id IN ({0})
				GROUP BY articulo_id
			", articulos_ids);


            conector.Select(sql,null);

			foreach(DataRow row in conector.result_set.Rows)
			{
				DTO_Existencia existencia = new DTO_Existencia();
				existencia.articulo_id = Convert.ToInt64(row["articulo_id"]);
                existencia.volumen = Convert.ToDecimal(row["volumen"]);
                existencia.existencia = Convert.ToInt64(row["existencia"]);
				existencia.caducidad = "";
				existencia.lote = "";

				lista_existencias.Add(existencia);
			}

			return lista_existencias;
		}

		public DataTable get_rastreo_mayoreo_ventas(DTO_Articulo_generic articulo)
		{
			string sql = @"
				SELECT
					terminales.nombre AS terminal,
					mayoreo_venta_id AS folio,
					IF(
						fecha_impreso_captura IS NULL,
							'CAPTURANDO',
							IF(
								fecha_impreso_revision IS NULL,
									'CAPTURANDO',
									'EN REVISION'
							)
					) AS status,
					SUM(detallado_mayoreo_ventas.cantidad) AS cantidad
				FROM
					farmacontrol_local.mayoreo_ventas
				JOIN farmacontrol_local.detallado_mayoreo_ventas USING(mayoreo_venta_id)
				JOIN farmacontrol_local.terminales USING(terminal_id)
				WHERE
					mayoreo_ventas.fecha_terminado IS NULL
				AND
					detallado_mayoreo_ventas.articulo_id = @articulo_id
				AND
					detallado_mayoreo_ventas.caducidad = @caducidad
				AND
					detallado_mayoreo_ventas.lote = @lote
				GROUP BY
					detallado_mayoreo_ventas.articulo_id,detallado_mayoreo_ventas.caducidad,detallado_mayoreo_ventas.lote
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("articulo_id",articulo.articulo_id);
			parametros.Add("caducidad",articulo.caducidad);
			parametros.Add("lote",articulo.lote);

			conector.Select(sql,parametros);

			return conector.result_set;
		}

		public DataTable get_rastreo_traspasos(DTO_Articulo_generic articulo)
		{
			string sql = @"
				SELECT
					terminales.nombre AS terminal,
					traspaso_id AS folio,
					traspasos.tipo AS tipo,
					IF(
						traspasos.tipo = 'ENVIAR',
						CONCAT('ENVIADO A ',sucursales.nombre),
						CONCAT('RECIBIDO DE ',sucursales.nombre)
					) AS tipo_formato,
					IF(fecha_terminado IS NULL, 'CAPTURANDO', 'EN TRANSITO') AS status,
					SUM(detallado_traspasos.cantidad) AS cantidad
				FROM
					farmacontrol_local.traspasos
				JOIN farmacontrol_local.detallado_traspasos USING(traspaso_id)
				JOIN farmacontrol_local.terminales USING(terminal_id)
				JOIN farmacontrol_global.sucursales USING(sucursal_id)
				WHERE
					traspasos.fecha_recibido IS NULL
				AND
					detallado_traspasos.articulo_id = @articulo_id
				AND
					detallado_traspasos.caducidad = @caducidad
				AND
					detallado_traspasos.lote = @lote
				GROUP BY
					detallado_traspasos.articulo_id,detallado_traspasos.caducidad,detallado_traspasos.lote
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("articulo_id",articulo.articulo_id);
			parametros.Add("caducidad",articulo.caducidad);
			parametros.Add("lote",articulo.lote);

			conector.Select(sql,parametros);

			return conector.result_set;
		}

		public DataTable get_rastreo_apartado_mercancia(DTO_Articulo_generic articulo)
		{
			string sql = @"
				SELECT
					IF(apartados.tipo = 'SUCURSAL', sucursales.nombre, apartados.tipo) AS destino,
					fecha_apartado,
					SUM(cantidad) AS cantidad
				FROM
					farmacontrol_local.apartados
				LEFT JOIN farmacontrol_global.sucursales USING(sucursal_id)
				WHERE
					apartados.articulo_id = @articulo_id
				AND
					apartados.caducidad = @caducidad
				AND
					apartados.lote = @lote
				GROUP BY
					apartados.articulo_id, apartados.caducidad, apartados.lote, apartados.tipo
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("articulo_id", articulo.articulo_id);
			parametros.Add("caducidad", articulo.caducidad);
			parametros.Add("lote", articulo.lote);

			conector.Select(sql, parametros);

			return conector.result_set;
		}

		public DataTable get_rastreo_existencias_mermas(DTO_Articulo_generic articulo)
		{
			string sql = @"
				SELECT
					merma_id AS folio,
					SUM(cantidad) AS cantidad
				FROM
					farmacontrol_local.mermas
				JOIN farmacontrol_local.detallado_mermas USING(merma_id)
				WHERE
					mermas.fecha_terminado IS NULL
				AND
					detallado_mermas.articulo_id = @articulo_id
				AND
					detallado_mermas.caducidad = @caducidad
				AND
					detallado_mermas.lote = @lote
				GROUP BY
					detallado_mermas.articulo_id, detallado_mermas.caducidad, detallado_mermas.lote
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("articulo_id", articulo.articulo_id);
			parametros.Add("caducidad", articulo.caducidad);
			parametros.Add("lote", articulo.lote);

			conector.Select(sql, parametros);

			return conector.result_set;
		}

		public DataTable get_rastreo_existencias_devoluciones(DTO_Articulo_generic articulo)
		{
			string sql = @"
				SELECT
					terminales.nombre AS terminal,
					devolucion_id AS folio,
					SUM(cantidad) AS cantidad
				FROM
					farmacontrol_local.devoluciones
				JOIN farmacontrol_local.detallado_devoluciones USING(devolucion_id)
				JOIN farmacontrol_local.terminales USING(terminal_id)
				WHERE
					devoluciones.fecha_terminado IS NULL
				AND
					detallado_devoluciones.articulo_id = @articulo_id
				AND
					detallado_devoluciones.caducidad = @caducidad
				AND
					detallado_devoluciones.lote = @lote
				GROUP BY
					detallado_devoluciones.articulo_id, detallado_devoluciones.caducidad, detallado_devoluciones.lote
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("articulo_id", articulo.articulo_id);
			parametros.Add("caducidad", articulo.caducidad);
			parametros.Add("lote", articulo.lote);

			conector.Select(sql, parametros);

			return conector.result_set;
		}

		public DataTable get_rastreo_existencias_ventas(DTO_Articulo_generic articulo)
		{
			string sql = @"
				SELECT
					terminales.nombre AS terminal,
					venta_folio AS folio,
					SUM(cantidad) AS cantidad
				FROM
					farmacontrol_local.ventas
				JOIN farmacontrol_local.detallado_ventas USING(venta_id)
				JOIN farmacontrol_local.terminales USING(terminal_id)
				WHERE
					ventas.fecha_terminado IS NULL
				AND
					detallado_ventas.articulo_id = @articulo_id
				AND
					detallado_ventas.caducidad = @caducidad
				AND
					detallado_ventas.lote = @lote
				GROUP BY
					detallado_ventas.articulo_id, detallado_ventas.caducidad, detallado_ventas.lote
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("articulo_id",articulo.articulo_id);
			parametros.Add("caducidad",articulo.caducidad);
			parametros.Add("lote",articulo.lote);

			conector.Select(sql,parametros);

			return conector.result_set;
		}

		public List<DTO_Existencia> get_articulos_existencias_prepago(string[] articulos_ids)
		{
			List<DTO_Existencia> lista_existencias = new List<DTO_Existencia>();

			string lista_articulos = string.Join(",",articulos_ids);

			string sql = string.Format(@"
				SELECT
					existencias.articulo_id,
					CONVERT(existencias.caducidad USING utf8) AS caducidad,
					existencias.lote,
					COALESCE(SUM(existencia), 0) - COALESCE(existencia_mermas, 0) - COALESCE(existencia_devoluciones, 0) - COALESCE(existencia_apartados, 0) - COALESCE(existencia_cambio_fisico, 0) - COALESCE(existencia_ventas, 0) - COALESCE(existencia_traspasos, 0) - COALESCE(existencia_mayoreo, 0) AS existencia_vendible
				FROM
					farmacontrol_local.existencias
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
					articulo_id IN({0})
				GROUP BY articulo_id, caducidad, lote
				ORDER BY caducidad ASC
			",lista_articulos);

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			conector.Select(sql, parametros);

			var result = conector.result_set;

			foreach (DataRow row in result.Rows)
			{
				DTO_Existencia articulo_existencia = new DTO_Existencia();
				articulo_existencia.articulo_id = Convert.ToInt64(row["articulo_id"]);
				articulo_existencia.caducidad = row["caducidad"].ToString();
				articulo_existencia.existencia = Convert.ToInt64(row["existencia_vendible"]);
				articulo_existencia.lote = row["lote"].ToString();

				lista_existencias.Add(articulo_existencia);
			}

			return lista_existencias;
		}

		public List<DTO_Existencia> get_articulos_existencias_prepago(string codigo)
		{
			List<DTO_Existencia> lista_existencias = new List<DTO_Existencia>();

            string sql = @"
                SELECT
					GROUP_CONCAT(articulo_id) AS articulo_ids
				FROM
					farmacontrol_local.detallado_prepagos
				LEFT JOIN farmacontrol_local.prepagos USING(prepago_id)
				WHERE
					prepagos.codigo = @codigo
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("codigo",codigo);

            conector.Select(sql, parametros);

            string articulo_ids = "";

            articulo_ids = (conector.result_set.Rows.Count > 0) ? conector.result_set.Rows[0]["articulo_ids"].ToString() : "0";

            if (conector.result_set.Rows.Count > 0)
            {
                sql = string.Format(@"
				SELECT
					articulos.articulo_id AS articulo_id,
                    articulos.pct_descuento AS pct_descuento,
					articulos.activo AS activo,
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
					nombre,
					IF(tmp_existencias.caducidad = '0000-00-00', ' ', COALESCE(tmp_existencias.caducidad, ' ') ) AS caducidad,
					IF(tmp_existencias.caducidad = '0000-00-00', ' ', COALESCE(tmp_existencias.caducidad, ' ') ) AS caducidad_sin_formato,
					COALESCE(tmp_existencias.lote, ' ') AS lote,
					COALESCE(tmp_existencias.existencia, 0) AS existencia_total,
					COALESCE(existencia_devoluciones, 0) AS existencia_devoluciones,
					COALESCE(existencia_mermas, 0) AS existencia_mermas,
					COALESCE(existencia_cambio_fisico, 0) AS existencia_cambio_fisico,
					COALESCE(existencia_apartados, 0) AS existencia_apartados,
					COALESCE(existencia_traspasos, 0) AS existencia_traspasos,
					COALESCE(existencia_mayoreo, 0) AS existencia_mayoreo,
					COALESCE(existencia_parcial_prepago, 0) AS existencia_parcial_prepago,
					COALESCE(existencia_ventas, 0) AS existencia_ventas,
					(articulos.precio_publico * articulos.pct_iva ) + articulos.precio_publico AS precio_publico,
					COALESCE(existencia, 0) - COALESCE(existencia_mermas, 0) - COALESCE(existencia_devoluciones, 0) - COALESCE(existencia_apartados, 0) - COALESCE(existencia_cambio_fisico, 0) - COALESCE(existencia_traspasos, 0) - COALESCE(existencia_mayoreo, 0) - COALESCE(existencia_ventas, 0) - COALESCE(existencia_parcial_prepago, 0) AS existencia_vendible
				FROM
					farmacontrol_global.articulos
				NATURAL LEFT JOIN
				(
					SELECT
						articulo_id,
						caducidad,
						lote,
						SUM(existencia) AS existencia
					FROM
						farmacontrol_local.existencias
					WHERE
						existencias.articulo_id IN ({0})
					GROUP BY
						articulo_id,caducidad,lote
				) AS tmp_existencias
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
					AND
						detallado_devoluciones.articulo_id IN ({0})
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
					(
						(
							SELECT
								UUID(),
								articulo_id,
								caducidad,
								lote,
								SUM(cantidad) AS cantidad
							FROM
								farmacontrol_local.apartados
							WHERE
								apartados.tipo = 'MERMA'
							AND
								apartados.articulo_id IN ({0})
							GROUP BY articulo_id, caducidad, lote
						)
						UNION
						(
							SELECT
								UUID(),
								articulo_id,
								caducidad,
								lote,
								cantidad
							FROM
								farmacontrol_local.detallado_mermas
							JOIN farmacontrol_local.mermas USING(merma_id)
							WHERE
								mermas.fecha_terminado IS NULL
							AND
								detallado_mermas.articulo_id IN ({0})
						)
					) AS new_table
					GROUP BY
						new_table.articulo_id,new_table.caducidad,new_table.lote
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
					AND
						apartados.articulo_id IN ({0})
					GROUP BY articulo_id, caducidad, lote
				) AS tmp_apartados
				NATURAL LEFT JOIN 
				(
					SELECT
						articulo_id,
						caducidad,
						lote,
						SUM(cantidad) AS existencia_parcial_prepago
					FROM
						farmacontrol_local.apartados
					WHERE
						tipo = 'ENTREGA_PARCIAL_PREPAGO'
					AND
						apartados.articulo_id IN ({0})
					GROUP BY articulo_id, caducidad, lote
				) AS tmp_entrega_parcial_prepago
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
					AND
						apartados.articulo_id IN ({0})
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
					AND
						detallado_traspasos.articulo_id IN ({0})
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
					AND
						detallado_ventas.articulo_id IN ({0})
					GROUP BY articulo_id, caducidad, lote
				) AS tmp_ventas
				NATURAL LEFT JOIN 
				(
					SELECT
						articulo_id,
						caducidad,
						lote,
						SUM(cantidad) + (IF(cantidad = 0,SUM(cantidad_revision),0)) AS existencia_mayoreo
					FROM
						farmacontrol_local.detallado_mayoreo_ventas
					LEFT JOIN farmacontrol_local.mayoreo_ventas USING(mayoreo_venta_id)
					WHERE
						mayoreo_ventas.fecha_fin_verificacion IS NULL
					GROUP BY articulo_id,caducidad,lote
				) AS tmp_mayoreo
				WHERE
					articulos.articulo_id IN ({0})
				{1}
				GROUP BY articulos.articulo_id, tmp_existencias.caducidad, tmp_existencias.lote
				", articulo_ids, "AND articulos.activo = 1");

                parametros = new Dictionary<string, object>();
                parametros.Add("codigo", codigo);

                conector.Select(sql, parametros);

                var result = conector.result_set;

                foreach (DataRow row in result.Rows)
                {
                    DTO_Existencia articulo_existencia = new DTO_Existencia();
                    articulo_existencia.articulo_id = Convert.ToInt64(row["articulo_id"]);
                    articulo_existencia.caducidad = row["caducidad"].ToString();
                    articulo_existencia.existencia = Convert.ToInt64(row["existencia_vendible"]);
                    articulo_existencia.lote = row["lote"].ToString();

                    lista_existencias.Add(articulo_existencia);
                }   
            }

			return lista_existencias;
		}

		public List<DTO_Producto_inventario> get_valuacion_inventario()
		{
			List<DTO_Producto_inventario> lista_productos = new List<DTO_Producto_inventario>();

			string sql= @"
				SELECT
					articulos.amecop_original AS amecop,
					articulos.nombre AS nombre,
					SUM(COALESCE(existencia,0)) AS cantidad,
					articulos.precio_costo AS precio,
					articulos.precio_costo * SUM(COALESCE(existencias.existencia, 0)) AS valor
				FROM
					farmacontrol_global.articulos
				INNER JOIN farmacontrol_local.existencias USING(articulo_id)
				GROUP BY
					farmacontrol_global.articulos.articulo_id
				ORDER BY 
					articulos.nombre ASC
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			conector.Select(sql,parametros);

			foreach(DataRow row in conector.result_set.Rows)
			{
				DTO_Producto_inventario producto = new DTO_Producto_inventario();
				producto.codigo = row["amecop"].ToString();
				producto.nombre = row["nombre"].ToString();
				producto.existencia = Convert.ToInt32(row["cantidad"]);
				producto.precio = Convert.ToDecimal(row["precio"]);
				producto.valor = Convert.ToDecimal(row["valor"]);

				lista_productos.Add(producto);
			}

			return lista_productos;
		}

        public Int32 get_existencia_articulo( long articulo_id )
        {
            int existencia = 0;
            string sql = @"
				SELECT
					SUM( existencia  ) AS existencia
				FROM
					farmacontrol_local.existencias
                WHERE
                    articulo_id = @articulo_id

				
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("articulo_id", articulo_id);
            conector.Select(sql, parametros);

            foreach (DataRow row in conector.result_set.Rows)
            {
              existencia = Convert.ToInt32(row["existencia"].ToString());
            }

            return existencia;
        
        }

        public int get_existencia_amecop( string amecop)
        {
            int existencia = 0;
            string sql = @"
				SELECT
					COALESCE(SUM( existencia ), 0) AS existencia
				FROM
					farmacontrol_local.existencias
                LEFT JOIN
                    farmacontrol_global.articulos
                USING(articulo_id)               
                WHERE
                    CAST(amecop_original AS UNSIGNED ) = @amecop
				

				
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("amecop", amecop);
            conector.Select(sql, parametros);

            foreach (DataRow row in conector.result_set.Rows)
            {
                existencia = Convert.ToInt32(row["existencia"].ToString());
            }

            return existencia;
        
        
        }
	}
}
