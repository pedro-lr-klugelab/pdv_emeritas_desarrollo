using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using System.Data;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Mermas
	{
		Conector conector = new Conector();

		public List<Tuple<string, string, int>> get_detallado_caducidades(long devolucion_id, int articulo_id)
		{
			string sql = @"
				SELECT
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					COALESCE(cantidad,0) AS cantidad
				FROM
					farmacontrol_local.detallado_mermas
				WHERE
					merma_id = @merma_id
				AND
					articulo_id = @articulo_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("merma_id", devolucion_id);
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

		public List<DTO_Detallado_merma> get_detallado_merma_ticket(long merma_id)
		{
			List<DTO_Detallado_merma> detallado = new List<DTO_Detallado_merma>();

			string sql = @"
				SELECT
					articulos.articulo_id,
					(
						SELECT 
						   amecop
						FROM 
							farmacontrol_global.articulos_amecops 
						WHERE 
							articulo_id = detallado_mermas.articulo_id 
						ORDER BY 
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS amecop,
					RPAD(nombre, 37, ' ') AS nombre,
					detallado_mermas.precio_costo,
					SUM(detallado_mermas.cantidad) AS cantidad,
					SUM(detallado_mermas.total) AS total
				FROM
					farmacontrol_local.detallado_mermas
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					merma_id = @merma_id
				GROUP BY
					articulo_id
				ORDER BY detallado_merma_id DESC
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("merma_id", merma_id);

			conector.Select(sql, parametros);

			var result_set = conector.result_set;

			foreach (DataRow row in result_set.Rows)
			{
				DTO_Detallado_merma detallado_ticket = new DTO_Detallado_merma();
				detallado_ticket.articulo_id = Convert.ToInt32(row["articulo_id"]);

                string var_temp_amecop = row["amecop"].ToString();
                int tam_var = var_temp_amecop.Length;
                String Var_Sub = "*" + var_temp_amecop.Substring((tam_var - 3), 3);
                string amecop_temp = Var_Sub.PadRight(5, ' ');
                detallado_ticket.amecop = amecop_temp;

				//detallado_ticket.amecop = row["amecop"].ToString();

				detallado_ticket.nombre = row["nombre"].ToString();
				detallado_ticket.precio_costo = Convert.ToDecimal(row["precio_costo"]);
				detallado_ticket.total = Convert.ToDecimal(row["total"]);
				detallado_ticket.caducidades_lotes = get_detallado_caducidades(merma_id, detallado_ticket.articulo_id);
				detallado.Add(detallado_ticket);
			}

			return detallado;
		}

		public long terminar_merma(long merma_id, long empleado_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.mermas
				SET
					termina_empleado_id = @empleado_id,
					fecha_terminado = NOW()
				WHERE
					merma_id = @merma_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("empleado_id",empleado_id);
			parametros.Add("merma_id",merma_id);

			conector.Update(sql,parametros);

			var filas_afectadas = conector.filas_afectadas;


			sql = @"
				SELECT
					articulo_id,
					CAST(caducidad as CHAR(50)) AS caducidad,
					lote,
					SUM(cantidad) AS cantidad
				FROM
					farmacontrol_local.detallado_mermas
				WHERE
					merma_id = @merma_id
				GROUP BY
					articulo_id,caducidad,lote
			";

			conector.Select(sql, parametros);

			var detallado_merma = conector.result_set;

			if(detallado_merma.Rows.Count > 0)
			{
				List<Tuple<int, string, string, int>> productos = new List<Tuple<int, string, string, int>>();

				foreach (DataRow row in detallado_merma.Rows)
				{
					int articulo_id = Convert.ToInt32(row["articulo_id"]);
					string caducidad = row["caducidad"].ToString();
					string lote = row["lote"].ToString();
					int cantidad = Convert.ToInt32(row["cantidad"]);

					Tuple<int, string, string, int> detallado = new Tuple<int, string, string, int>(articulo_id, caducidad, lote, cantidad);
					productos.Add(detallado);
				}

				DAO_Articulos dao_articulos = new DAO_Articulos();
                dao_articulos.afectar_existencias_salida(productos, "MERMA", merma_id, merma_id);
			}

			return filas_afectadas;
		}

		public long crear_merma(long empleado_id)
		{
			string sql = @"
				INSERT INTO
					farmacontrol_local.mermas
				SET
					empleado_id = @empleado_id,
					fecha_creado = NOW(),
					comentarios = ''
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("empleado_id",empleado_id);

			conector.Insert(sql,parametros);

			var insert_id = conector.insert_id;

			if(insert_id > 0)
			{	
				sql = @"
					INSERT INTO
						farmacontrol_local.detallado_mermas
					(
						SELECT
							0 AS detallado_merma_id,
							@merma_id AS merma_id,
							articulo_id,
							caducidad,
							lote,
							articulos.precio_costo AS precio_costo,
							cantidad,
							(cantidad * articulos.precio_costo) AS total,
							'' AS comentarios,
							NOW() AS modified
						FROM
							farmacontrol_local.apartados AS tmp
						LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
						WHERE
							tmp.tipo = 'MERMA'
					)
					ON DUPLICATE KEY UPDATE
						detallado_mermas.cantidad = detallado_mermas.cantidad + tmp.cantidad,
						detallado_mermas.total = detallado_mermas.total + ( tmp.cantidad * articulos.precio_costo)
				";

				parametros = new Dictionary<string,object>();
				parametros.Add("merma_id",insert_id);

				conector.Insert(sql,parametros);

				sql= @"
					DELETE FROM
						farmacontrol_local.apartados
					WHERE
						tipo = 'MERMA'
				";

				conector.Delete(sql,parametros);
			}

			return insert_id;
		}

		public DataTable limpiar_detallado_merma(long merma_id)
		{
			string sql = @"
				DELETE FROM
					farmacontrol_local.detallado_mermas
				WHERE
					merma_id = @merma_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("merma_id",merma_id);

			conector.Delete(sql,parametros);

			return get_detallado_merma(merma_id);
		}

		public DataTable eliminar_producto_merma(long merma_id, long detallado_merma_id)
		{
			string sql = @"
				DELETE FROM
					farmacontrol_local.detallado_mermas
				WHERE
					detallado_merma_id = @detallado_merma_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("detallado_merma_id",detallado_merma_id);

			conector.Delete(sql,parametros);
			
			return get_detallado_merma(merma_id);
		}

		public DataTable insertar_producto_merma(long merma_id, int articulo_id, string caducidad, string lote, int cantidad)
		{
			string sql = @"
				INSERT INTO
					farmacontrol_local.detallado_mermas
				(
					SELECT
						0 AS detallado_merma_id,
						@merma_id AS merma_id,
						@articulo_id AS articulo_id,
						@caducidad AS caducidad,
						@lote AS lote,
						precio_costo,
						@cantidad AS cantidad,
						(@cantidad * precio_costo) AS total,
						'' AS comentarios,
						NOW() AS modified
					FROM
						farmacontrol_global.articulos
					WHERE
						articulo_id = @articulo_id
				)
				ON DUPLICATE KEY UPDATE
					cantidad = cantidad + @cantidad,
					total = total + ( @cantidad * articulos.precio_costo)
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("merma_id",merma_id);
			parametros.Add("articulo_id",articulo_id);
			parametros.Add("caducidad",caducidad);
			parametros.Add("lote",lote);
			parametros.Add("cantidad",cantidad);

			conector.Insert(sql,parametros);
			
			return get_detallado_merma(merma_id);
		}

		public void set_comentario(long merma_id, string comentario)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.mermas
				SET
					comentarios = @comentario
				WHERE
					merma_id = @merma_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("comentario",comentario);
			parametros.Add("merma_id",merma_id);

			conector.Update(sql,parametros);
		}

		public DataTable get_detallado_merma(long merma_id)
		{
			string sql = @"
				SELECT
					detallado_merma_id,
					merma_id,
					(
						SELECT 
							amecop 
						FROM 
							farmacontrol_global.articulos_amecops 
						WHERE 
							articulo_id = detallado_mermas.articulo_id 
						ORDER BY 
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS amecop,
					articulos.nombre AS producto,
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					detallado_mermas.precio_costo,
					cantidad,
					total
				FROM
					farmacontrol_local.detallado_mermas
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					merma_id = @merma_id
				GROUP BY
					detallado_merma_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("merma_id",merma_id);
			
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

		public DTO_Merma get_informacion_merma(long merma_id)
		{
			DTO_Merma dto_merma = new DTO_Merma();

			string sql = @"
				SELECT
					merma_id,
					empleado_id,
					COALESCE(termina_empleado_id, 0) AS termina_empleado_id,
					mermas.fecha_creado,
					mermas.fecha_terminado,
					mermas.comentarios,
					(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = mermas.empleado_id) AS nombre_empleado_captura,
					IF(mermas.termina_empleado_id IS NULL,'',(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = mermas.termina_empleado_id) ) AS nombre_empleado_termina
				FROM
					farmacontrol_local.mermas
				WHERE
					merma_id = @merma_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("merma_id",merma_id);

			conector.Select(sql,parametros);

			var result_set = conector.result_set;

			if(result_set.Rows.Count > 0)
			{
				var row = result_set.Rows[0];
				dto_merma.merma_id = Convert.ToInt64(row["merma_id"]);
				dto_merma.empleado_id = Convert.ToInt64(row["empleado_id"]);
				dto_merma.termina_empleado_id = Convert.ToInt64(row["termina_empleado_id"]);

				DateTime? date_null = null;

				dto_merma.fecha_creado = (row["fecha_creado"].ToString().Equals("")) ? date_null : Convert.ToDateTime(row["fecha_creado"]);
				dto_merma.fecha_terminado = (row["fecha_terminado"].ToString().Equals("")) ? date_null : Convert.ToDateTime(row["fecha_terminado"]);
				dto_merma.comentarios = row["comentarios"].ToString();
				dto_merma.nombre_empleado_captura = row["nombre_empleado_captura"].ToString();
				dto_merma.nombre_empleado_termina = row["nombre_empleado_termina"].ToString();
			}
			else
			{
				dto_merma.merma_id = 0;
			}

			return dto_merma;
		}

		public long get_merma_siguiente(long merma_id)
		{
			string sql = @"
				SELECT
					merma_id
				FROM
					farmacontrol_local.mermas
				WHERE
					merma_id > @merma_id
				ORDER BY
					merma_id
				ASC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("merma_id", merma_id);

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["merma_id"]);
			}

			return 0;
		}

		public long get_merma_atras(long merma_id)
		{
			string sql = @"
				SELECT
					merma_id
				FROM
					farmacontrol_local.mermas
				WHERE
					merma_id < @merma_id
				ORDER BY
					merma_id
				DESC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("merma_id", merma_id);

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["merma_id"]);
			}

			return 0;
		}

		public long get_merma_inicio()
		{
			string sql = @"
				SELECT
					merma_id
				FROM
					farmacontrol_local.mermas
				ORDER BY
					merma_id
				ASC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["merma_id"]);
			}

			return 0;
		}

		public long get_merma_fin()
		{
			string sql = @"
				SELECT
					merma_id
				FROM
					farmacontrol_local.mermas
				ORDER BY
					merma_id
				DESC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["merma_id"]);
			}

			return 0;
		}
	}
}
