using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;
using System.Data;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Ventas_mayoreo
	{
		Conector conector = new Conector();

		public List<Tuple<string, string, int,int>> get_detallado_caducidades(long mayoreo_venta_id, int articulo_id)
		{
			string sql = @"
				SELECT
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					cantidad,
					cantidad_revision
				FROM
					farmacontrol_local.detallado_mayoreo_ventas
				WHERE
					mayoreo_venta_id = @mayoreo_venta_id
				AND
					articulo_id = @articulo_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("mayoreo_venta_id", mayoreo_venta_id);
			parametros.Add("articulo_id", articulo_id);

			conector.Select(sql, parametros);

			List<Tuple<string, string, int,int>> lista_caducidades = new List<Tuple<string, string, int,int>>();

			foreach (DataRow row in conector.result_set.Rows)
			{
				Tuple<string, string, int, int> tupla = new Tuple<string, string, int,int>(row["caducidad"].ToString(), row["lote"].ToString(), Convert.ToInt32(row["cantidad"]), Convert.ToInt32(row["cantidad_revision"]));
				lista_caducidades.Add(tupla);
			}

			return lista_caducidades;
		}

		public List<DTO_Detallado_mayoreo_ventas_ticket> get_detallado_ticket_mayoreo_ventas(long mayoreo_venta_id)
		{
			List<DTO_Detallado_mayoreo_ventas_ticket> detallado = new List<DTO_Detallado_mayoreo_ventas_ticket>();
            //RPAD(CONCAT('*',SUBSTRING(amecop
			string sql = @"
				SELECT
					articulos.articulo_id,
					(
						SELECT 
							amecop
						FROM 
							farmacontrol_global.articulos_amecops 
						WHERE 
							articulo_id = detallado_mayoreo_ventas.articulo_id 
						ORDER BY 
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS amecop,
					RPAD(nombre, 37, ' ') AS nombre,
					detallado_mayoreo_ventas.precio_costo,
					SUM(detallado_mayoreo_ventas.cantidad) AS cantidad,
					SUM(detallado_mayoreo_ventas.cantidad_revision) AS cantidad_revision,
					SUM(detallado_mayoreo_ventas.subtotal_captura) AS subtotal_captura,
					SUM(detallado_mayoreo_ventas.subtotal_revision) AS subtotal_revision,
					SUM(detallado_mayoreo_ventas.importe_iva_captura) AS importe_iva_captura,
					SUM(detallado_mayoreo_ventas.importe_iva_revision) AS importe_iva_revision,
					SUM(detallado_mayoreo_ventas.total_captura) AS total_captura,
					SUM(detallado_mayoreo_ventas.total_revision) AS total_revision
				FROM
					farmacontrol_local.detallado_mayoreo_ventas
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					mayoreo_venta_id = @mayoreo_venta_id
				GROUP BY
					detallado_mayoreo_ventas.articulo_id
				ORDER BY detallado_mayoreo_id ASC
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("mayoreo_venta_id", mayoreo_venta_id);

			conector.Select(sql, parametros);

			var result_set = conector.result_set;

			foreach (DataRow row in result_set.Rows)
			{
				DTO_Detallado_mayoreo_ventas_ticket detallado_ticket = new DTO_Detallado_mayoreo_ventas_ticket();
				detallado_ticket.articulo_id = Convert.ToInt32(row["articulo_id"]);

                string var_temp_amecop = row["amecop"].ToString();
                int tam_var = var_temp_amecop.Length;
                String Var_Sub = "*" + var_temp_amecop.Substring((tam_var - 3), 3);
                string amecop_temp = Var_Sub.PadRight(5, ' ');
                detallado_ticket.nombre = amecop_temp;

                //detallado_ticket.amecop = row["amecop"].ToString();
				detallado_ticket.nombre = row["nombre"].ToString();
				detallado_ticket.precio_unitario = Convert.ToDecimal(row["precio_costo"]);
				
				detallado_ticket.subtotal_captura = Convert.ToDecimal(row["subtotal_captura"]);
				detallado_ticket.total_captura = Convert.ToDecimal(row["total_captura"]);
				detallado_ticket.subtotal_revision = Convert.ToDecimal(row["subtotal_revision"]);
				detallado_ticket.total_revision = Convert.ToDecimal(row["total_revision"]);
				detallado_ticket.importe_iva_captura = Convert.ToDecimal(row["importe_iva_captura"]);
				detallado_ticket.importe_iva_revision = Convert.ToDecimal(row["importe_iva_revision"]);

				detallado_ticket.cantidad = Convert.ToInt32(row["cantidad"]);
				detallado_ticket.caducidades_lotes = get_detallado_caducidades(mayoreo_venta_id, detallado_ticket.articulo_id);
				detallado.Add(detallado_ticket);
			}

			return detallado;
		}

		public long existencia_captura_mayoreo(long mayoreo_venta_id, long articulo_id, string caducidad, string lote)
		{
			string sql = @"
				SELECT
					IF(cantidad = 0, cantidad_revision, cantidad) AS cantidad
				FROM
					farmacontrol_local.detallado_mayoreo_ventas
				WHERE
					mayoreo_venta_id = @mayoreo_venta_id
				AND
					caducidad = @caducidad
				AND
					lote = @lote
				AND
					articulo_id = @articulo_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("mayoreo_venta_id",mayoreo_venta_id);
			parametros.Add("articulo_id",articulo_id);
			parametros.Add("caducidad",caducidad);
			parametros.Add("lote",lote);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["cantidad"]);
			}

			return 0;
		}

		public DataTable get_productos_error(long mayoreo_venta_id)
		{
			string sql = @"
				SELECT
					articulos_amecops.amecop AS amecop,
					articulos.nombre AS producto,
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					cantidad,
					cantidad_revision,
					(cantidad_revision - cantidad) AS diferencia
				FROM
					farmacontrol_local.detallado_mayoreo_ventas
				LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id)
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					cantidad != cantidad_revision
				AND
					mayoreo_venta_id = @mayoreo_venta_id
				GROUP BY
					detallado_mayoreo_ventas.articulo_id, detallado_mayoreo_ventas.caducidad, detallado_mayoreo_ventas.lote
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("mayoreo_venta_id",mayoreo_venta_id);

			conector.Select(sql,parametros);

			var result_set = conector.result_set;

			foreach(DataRow row in result_set.Rows)
			{
                row["caducidad"] = Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");
			}

			return result_set;
		}

		public long finalizar_verificacion(long mayoreo_venta_id, long empleado_id)
		{
			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("mayoreo_venta_id", mayoreo_venta_id);

			string sql = @"
				SELECT
					articulo_id,
					CAST(caducidad as CHAR(50)) AS caducidad,
					lote,
					cantidad_revision AS cantidad
				FROM
					farmacontrol_local.detallado_mayoreo_ventas
				WHERE
					mayoreo_venta_id = @mayoreo_venta_id
				GROUP BY
					articulo_id,caducidad,lote
			";

			conector.Select(sql, parametros);

			var detallado_venta = conector.result_set;

			List<Tuple<int, string, string, int>> productos = new List<Tuple<int, string, string, int>>();

			foreach (DataRow row in detallado_venta.Rows)
			{
				int articulo_id = Convert.ToInt32(row["articulo_id"]);
				string caducidad = row["caducidad"].ToString();
				string lote = row["lote"].ToString();
				int cantidad = Convert.ToInt32(row["cantidad"]);

				Tuple<int, string, string, int> detallado = new Tuple<int, string, string, int>(articulo_id, caducidad, lote, cantidad);
				productos.Add(detallado);
			}

			sql = @"
				UPDATE
					farmacontrol_local.mayoreo_ventas
				SET
					fin_verificacion_empleado_id = @empleado_id,
					fecha_fin_verificacion = NOW(),
					fecha_impreso_revision = NOW()
				WHERE
					mayoreo_venta_id = @mayoreo_venta_id
			";

			parametros.Add("empleado_id", empleado_id);

			conector.Update(sql, parametros);

			if (conector.filas_afectadas > 0)
			{
				DAO_Articulos dao_articulos = new DAO_Articulos();
                dao_articulos.afectar_existencias_salida(productos, "MAYOREO_VENTA", mayoreo_venta_id, mayoreo_venta_id);
			}

			return conector.filas_afectadas;
		}

		public long iniciar_verificacion(long mayoreo_venta_id, long empleado_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.mayoreo_ventas
				SET
					inicio_verificacion_empleado_id = @empleado_id,
					fecha_inicio_verificacion = NOW(),
					terminal_id_revision = @terminal_id
				WHERE
					mayoreo_venta_id = @mayoreo_venta_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("empleado_id",empleado_id);
			parametros.Add("mayoreo_venta_id",mayoreo_venta_id);
			parametros.Add("terminal_id",Misc_helper.get_terminal_id());

			conector.Update(sql,parametros);

			return conector.filas_afectadas;
		}

		public long desasociar_terminal_revision(long mayoreo_venta_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.mayoreo_ventas
				SET
					terminal_id_revision = NULL
				WHERE
					mayoreo_venta_id = @mayoreo_venta_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("mayoreo_venta_id", mayoreo_venta_id);

			conector.Update(sql, parametros);

			return conector.filas_afectadas;
		}

		public long asociar_terminal_revision(long mayoreo_venta_id)
		{
			int terminal_id = (int)Misc_helper.get_terminal_id();

			string sql = @"
				UPDATE
					farmacontrol_local.mayoreo_ventas
				SET
					terminal_id_revision = @terminal_id
				WHERE
					mayoreo_venta_id = @mayoreo_venta_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("mayoreo_venta_id", mayoreo_venta_id);
			parametros.Add("terminal_id", terminal_id);

			conector.Update(sql, parametros);

			return conector.filas_afectadas;
		}

		public long asociar_terminal(long empleado_id, long mayoreo_venta_id)
		{
			int terminal_id = (int)Misc_helper.get_terminal_id();

			string sql = @"
				UPDATE
					farmacontrol_local.mayoreo_ventas
				SET
					terminal_id = @terminal_id,
					empleado_id = @empleado_id
				WHERE
					mayoreo_venta_id = @mayoreo_venta_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("mayoreo_venta_id",mayoreo_venta_id);
			parametros.Add("terminal_id",terminal_id);
			parametros.Add("empleado_id",empleado_id);

			conector.Update(sql,parametros);

			return conector.filas_afectadas;
		}

		public long desasociar_terminal(long mayoreo_venta_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.mayoreo_ventas
				SET
					terminal_id = NULL
				WHERE
					mayoreo_venta_id = @mayoreo_venta_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("mayoreo_venta_id",mayoreo_venta_id);

			conector.Update(sql,parametros);

			return conector.filas_afectadas;
		}

		public long terminar_mayoreo_venta(long empleado_id, long mayoreo_venta_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.mayoreo_ventas
				SET
					termina_empleado_id = @empleado_id,
					fecha_terminado = NOW(),
					fecha_impreso_captura = NOW()
				WHERE
					mayoreo_venta_id = @mayoreo_venta_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("empleado_id",empleado_id);
			parametros.Add("mayoreo_venta_id",mayoreo_venta_id);

			conector.Update(sql,parametros);

			return conector.filas_afectadas;
		}

		public void actualizar_cliente(long mayoreo_venta_id, string cliente_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.mayoreo_ventas
				SET
					cliente_id = @cliente_id
				WHERE
					mayoreo_venta_id = @mayoreo_venta_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("cliente_id",cliente_id);
			parametros.Add("mayoreo_venta_id",mayoreo_venta_id);

			conector.Update(sql,parametros);
		}

		public long registrar_mayoreo_venta(string cliente_id, long empleado_id)
		{
			int terminal_id = (int)Misc_helper.get_terminal_id();

			string sql = @"
				INSERT INTO
					farmacontrol_local.mayoreo_ventas
				SET
					terminal_id = @terminal_id,
					cliente_id = @cliente_id,
					empleado_id = @empleado_id,
					fecha_creado = NOW()
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("terminal_id",terminal_id);
			parametros.Add("cliente_id",cliente_id);
			parametros.Add("empleado_id",empleado_id);

			conector.Insert(sql,parametros);

			return conector.insert_id;
		}

		public List<DTO_Detallado_mayoreo_ventas> eliminar_detallado_mayoreo(long mayoreo_venta_id, long detallado_mayoreo_id)
		{
			string sql = @"
				DELETE FROM farmacontrol_local.detallado_mayoreo_ventas 
				WHERE
					detallado_mayoreo_id = @detallado_mayoreo_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("detallado_mayoreo_id",detallado_mayoreo_id);

			conector.Delete(sql,parametros);

			return get_detallado_mayoreo_ventas(mayoreo_venta_id);
		}

		public List<DTO_Detallado_mayoreo_ventas> eliminar_detallado_mayoreo_revision(long mayoreo_venta_id, long detallado_mayoreo_id)
		{
			string sql = @"
				SELECT
					cantidad
				FROM
					farmacontrol_local.detallado_mayoreo_ventas
				WHERE
					detallado_mayoreo_id = @detallado_mayoreo_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("detallado_mayoreo_id", detallado_mayoreo_id);

			conector.Select(sql,parametros);

			var result = conector.result_set;

			if(Convert.ToInt32(result.Rows[0]["cantidad"]) > 0)
			{
				sql = @"
					UPDATE
						farmacontrol_local.detallado_mayoreo_ventas
					SET
						cantidad_revision = 0,
						subtotal_revision = 0,
						importe_iva_revision = 0,
						total_revision = 0
					WHERE
						detallado_mayoreo_id = @detallado_mayoreo_id
				";

				conector.Update(sql,parametros);
			}
			else
			{
				sql = @"
					DELETE FROM
						farmacontrol_local.detallado_mayoreo_ventas
					WHERE
						detallado_mayoreo_id = @detallado_mayoreo_id
				";

				conector.Delete(sql, parametros);
			}

			return get_detallado_mayoreo_ventas(mayoreo_venta_id);
		}

		public List<DTO_Detallado_mayoreo_ventas> get_detallado_mayoreo_ventas(long mayoreo_venta_id)
		{
			List<DTO_Detallado_mayoreo_ventas> detallado = new List<DTO_Detallado_mayoreo_ventas>();

			string sql = @"
				SELECT
					detallado_mayoreo_id,
					articulos_amecops.amecop AS amecop,
					articulos.nombre AS producto,
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					detallado_mayoreo_ventas.precio_costo AS precio_costo,
					detallado_mayoreo_ventas.importe_iva_captura AS importe_iva_captura,
					detallado_mayoreo_ventas.importe_iva_revision AS importe_iva_revision,
					cantidad,
					cantidad_revision,
					(cantidad_revision - cantidad) AS diferencia,
					total_captura,
					total_revision
				FROM
					farmacontrol_local.detallado_mayoreo_ventas
				LEFT JOIN farmacontrol_local.mayoreo_ventas USING(mayoreo_venta_id)
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id)
				WHERE
					mayoreo_venta_id = @mayoreo_venta_id
				GROUP BY detallado_mayoreo_ventas.articulo_id, caducidad, lote
				ORDER BY detallado_mayoreo_ventas.detallado_mayoreo_id DESC
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("mayoreo_venta_id",mayoreo_venta_id);

			conector.Select(sql,parametros);

			var result = conector.result_set;

			foreach(DataRow row in result.Rows)
			{
				DTO_Detallado_mayoreo_ventas articulo = new DTO_Detallado_mayoreo_ventas();
				articulo.amecop = row["amecop"].ToString();
                articulo.caducidad = (row["caducidad"].ToString().Equals("0000-00-00")) ? "SIN CAD" : Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");
				articulo.cantidad_capturada = Convert.ToInt64(row["cantidad"]);
				articulo.cantidad_revision = Convert.ToInt64(row["cantidad_revision"]);
				articulo.detallado_mayoreo_id = Convert.ToInt64(row["detallado_mayoreo_id"]);
				articulo.diferencia = Convert.ToInt64(row["diferencia"]);
				articulo.importe_iva_captura = Convert.ToDecimal(row["importe_iva_captura"]);
				articulo.importe_iva_revision = Convert.ToDecimal(row["importe_iva_revision"]);
				articulo.lote = row["lote"].ToString();
				articulo.precio_costo = Convert.ToDecimal(row["precio_costo"]);
				articulo.producto = row["producto"].ToString();
				articulo.total_captura = Convert.ToDecimal(row["total_captura"]);
				articulo.total_revision = Convert.ToDecimal(row["total_revision"]);

				detallado.Add(articulo);
			}

			return detallado;
		}

		public List<DTO_Detallado_mayoreo_ventas> insertar_detallado_revision(long mayoreo_venta_id, long articulo_id, string caducidad, string lote, int cantidad)
		{
			string sql = @"
				SELECT
					detallado_mayoreo_id
				FROM
					farmacontrol_local.detallado_mayoreo_ventas
				WHERE
					mayoreo_venta_id = @mayoreo_venta_id
				AND
					caducidad = @caducidad
				AND
					lote = @lote
				AND
					articulo_id = @articulo_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("mayoreo_venta_id",mayoreo_venta_id);
			parametros.Add("caducidad",caducidad);
			parametros.Add("lote",lote);
			parametros.Add("articulo_id",articulo_id);

			conector.Select(sql,parametros);

			var result_set = conector.result_set;

			if(result_set.Rows.Count > 0)
			{
				long detallado_mayoreo_id = Convert.ToInt64(result_set.Rows[0]["detallado_mayoreo_id"]);

				sql = @"
					INSERT INTO
						farmacontrol_local.detallado_mayoreo_ventas
					(
						SELECT
							detallado_mayoreo_id,
							mayoreo_venta_id,
							articulo_id,
							caducidad,
							lote,
							precio_costo,
							cantidad,
							@cantidad AS cantidad_revision,
							pct_iva,
							subtotal_captura,
							importe_iva_captura,
							total_captura,
							subtotal_revision,
							importe_iva_revision,
							total_revision,
							NOW() AS modified
						FROM
							farmacontrol_local.detallado_mayoreo_ventas AS tmp
						WHERE
							detallado_mayoreo_id = @detallado_mayoreo_id
					)
					ON DUPLICATE KEY UPDATE
						cantidad_revision = tmp.cantidad_revision + @cantidad,
						subtotal_revision = tmp.subtotal_revision + (tmp.precio_costo * @cantidad),
						importe_iva_revision = tmp.importe_iva_revision + ((tmp.precio_costo * @cantidad) * tmp.pct_iva),
						total_revision = tmp.total_revision + ((tmp.precio_costo * @cantidad) + ((tmp.precio_costo * @cantidad) * tmp.pct_iva))
				";

				parametros.Add("detallado_mayoreo_id",detallado_mayoreo_id);
			}
			else
			{
				sql = @"
					INSERT INTO
						farmacontrol_local.detallado_mayoreo_ventas
					(
						SELECT
							0 as detallado_mayoreo_id,
							@mayoreo_venta_id AS mayoreo_venta_id,
							articulo_id,
							@caducidad AS caducidad,
							@lote AS lote,
							precio_costo,
							0 AS cantidad,
							@cantidad AS cantidad_revision,
							pct_iva,
							0 AS subtotal_captura,
							0 AS importe_iva_captura,
							0 AS total_captura,
							(precio_costo * @cantidad) AS subtotal_revision,
							((precio_costo * @cantidad) * pct_iva) AS importe_iva_revision,
							(precio_costo * @cantidad) + ((precio_costo * @cantidad) * pct_iva) AS total_revision,
							NOW() AS modified
						FROM
							farmacontrol_global.articulos
						WHERE
							articulo_id = @articulo_id
					)
					ON DUPLICATE KEY UPDATE
						cantidad_revision = cantidad_revision + @cantidad,
						subtotal_revision = subtotal_revision + (articulos.precio_costo * @cantidad),
						importe_iva_revision = importe_iva_revision + ((articulos.precio_costo * @cantidad) * articulos.pct_iva),
						total_revision = total_revision + ((articulos.precio_costo * @cantidad) + ((articulos.precio_costo * @cantidad) * articulos.pct_iva))
				";	
			}

			parametros.Add("cantidad", cantidad);

			conector.Insert(sql, parametros);

			return get_detallado_mayoreo_ventas(mayoreo_venta_id);
		}

		public List<DTO_Detallado_mayoreo_ventas> insertar_detallado(long mayoreo_venta_id, long articulo_id, string caducidad, string lote, int cantidad)
		{
			string sql = @"
				INSERT INTO
					farmacontrol_local.detallado_mayoreo_ventas
				(
					SELECT
						0 as detallado_mayoreo_id,
						@mayoreo_venta_id AS mayoreo_venta_id,
						articulo_id,
						@caducidad AS caducidad,
						@lote AS lote,
						precio_costo,
						@cantidad AS cantidad,
						0 AS cantidad_revision,
						pct_iva,
						(precio_costo * @cantidad) AS subtotal_captura,
						((precio_costo * @cantidad) * pct_iva) AS importe_iva_captura,
						(precio_costo * @cantidad) + ((precio_costo * @cantidad) * pct_iva) AS total_captura,
						0 AS subtotal_revision,
						0 AS importe_iva_revision,
						0 AS total_revision,
						NOW() AS modified
					FROM
						farmacontrol_global.articulos
					WHERE
						articulo_id = @articulo_id
				)
				ON DUPLICATE KEY UPDATE
					cantidad = cantidad + @cantidad,
					subtotal_captura = subtotal_captura + (articulos.precio_costo * @cantidad),
					importe_iva_captura = importe_iva_captura + ((articulos.precio_costo * @cantidad) * articulos.pct_iva),
					total_captura = total_captura + ((articulos.precio_costo * @cantidad) + ((articulos.precio_costo * @cantidad) * articulos.pct_iva))
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("mayoreo_venta_id",mayoreo_venta_id);
			parametros.Add("articulo_id",articulo_id);
			parametros.Add("caducidad",caducidad);
			parametros.Add("lote",lote);
			parametros.Add("cantidad",cantidad);

			conector.Insert(sql,parametros);

			return get_detallado_mayoreo_ventas(mayoreo_venta_id);
		}

		public DTO_Ventas_mayoreo get_venta_mayoreo_data(long mayoreo_venta_id)
		{
			DTO_Ventas_mayoreo venta_mayoreo = new DTO_Ventas_mayoreo();

			string sql = @"
				SELECT
					mayoreo_venta_id,
					terminal_id,
					terminal_id_revision,
					cliente_id AS cliente_id,
					COALESCE(mayoreo_ventas.empleado_id, 0) AS empleado_id,
					COALESCE(termina_empleado_id,0) AS termina_empleado_id,
					fecha_creado,
					fecha_impreso_captura,
					fecha_terminado,
					fecha_inicio_verificacion,
					fecha_fin_verificacion,
					mayoreo_ventas.comentarios AS comentarios,
					clientes.nombre AS nombre_cliente,
					COALESCE((SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = mayoreo_ventas.empleado_id),'') AS nombre_empleado_captura,
					COALESCE((SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = mayoreo_ventas.termina_empleado_id),'') AS nombre_empleado_termina,
					COALESCE((SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = mayoreo_ventas.inicio_verificacion_empleado_id),'') AS nombre_empleado_inicio_verificacion,
					COALESCE((SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = mayoreo_ventas.fin_verificacion_empleado_id),'') AS nombre_empleado_fin_verificacion
				FROM
					farmacontrol_local.mayoreo_ventas
				LEFT JOIN farmacontrol_global.clientes USING(cliente_id)
				WHERE
					mayoreo_venta_id = @mayoreo_venta_id
				LIMIT 1
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("mayoreo_venta_id",mayoreo_venta_id);

			conector.Select(sql,parametros);

			var result = conector.result_set;

			if(result.Rows.Count > 0)
			{
				long? nullable = null;
				var row = result.Rows[0];
				venta_mayoreo.mayoreo_venta_id = Convert.ToInt64(row["mayoreo_venta_id"]);
				venta_mayoreo.terminal_id = (row["terminal_id"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["terminal_id"]);
				venta_mayoreo.terminal_id_revision = (row["terminal_id_revision"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["terminal_id_revision"]);

				venta_mayoreo.cliente_id = row["cliente_id"].ToString();
				venta_mayoreo.empleado_id = Convert.ToInt64(row["empleado_id"]);
				venta_mayoreo.termina_empleado_id = Convert.ToInt64(row["termina_empleado_id"]);
				
				DateTime? date_nullable = null;

				venta_mayoreo.fecha_creado = (row["fecha_creado"].ToString().Equals("")) ? date_nullable : Convert.ToDateTime(row["fecha_creado"]);
				venta_mayoreo.fecha_impreso = (row["fecha_impreso_captura"].ToString().Equals("")) ? date_nullable : Convert.ToDateTime(row["fecha_impreso_captura"]);
				venta_mayoreo.fecha_terminado = (row["fecha_terminado"].ToString().Equals("")) ? date_nullable : Convert.ToDateTime(row["fecha_terminado"]);
				venta_mayoreo.fecha_inicio_verifiacion = (row["fecha_inicio_verificacion"].ToString().Equals("")) ? date_nullable : Convert.ToDateTime(row["fecha_inicio_verificacion"]);
				venta_mayoreo.fecha_fin_verificacion = (row["fecha_fin_verificacion"].ToString().Equals("")) ? date_nullable : Convert.ToDateTime(row["fecha_fin_verificacion"]);

				venta_mayoreo.comentarios = row["comentarios"].ToString();
				venta_mayoreo.nombre_cliente = row["nombre_cliente"].ToString();

				venta_mayoreo.nombre_empleado_captura = row["nombre_empleado_captura"].ToString();
				venta_mayoreo.nombre_empleado_termina = row["nombre_empleado_termina"].ToString();
				venta_mayoreo.nombre_empleado_inicio_verificacion = row["nombre_empleado_inicio_verificacion"].ToString();
				venta_mayoreo.nombre_empleado_fin_verificacion = row["nombre_empleado_fin_verificacion"].ToString();
			}

			return venta_mayoreo;
		}

		public long get_venta_mayoreo_siguiente(long venta_mayoreo_id)
		{
			string sql = @"
				SELECT
					mayoreo_venta_id
				FROM
					farmacontrol_local.mayoreo_ventas
				WHERE
					mayoreo_venta_id > @mayoreo_venta_id
				ORDER BY
					mayoreo_venta_id
				ASC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("mayoreo_venta_id", venta_mayoreo_id);

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["mayoreo_venta_id"]);
			}

			return 0;
		}

		public long get_venta_mayoreo_atras(long venta_mayoreo_id)
		{
			string sql = @"
				SELECT
					mayoreo_venta_id
				FROM
					farmacontrol_local.mayoreo_ventas
				WHERE
					mayoreo_venta_id < @mayoreo_venta_id
				ORDER BY
					mayoreo_venta_id
				DESC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("mayoreo_venta_id", venta_mayoreo_id);

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["mayoreo_venta_id"]);
			}

			return 0;
		}

		public long get_venta_mayoreo_inicio()
		{
			string sql = @"
				SELECT
					mayoreo_venta_id
				FROM
					farmacontrol_local.mayoreo_ventas
				ORDER BY
					mayoreo_venta_id
				ASC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["mayoreo_venta_id"]);
			}

			return 0;
		}

		public long get_venta_mayoreo_fin()
		{
			string sql = @"
				SELECT
					mayoreo_venta_id
				FROM
					farmacontrol_local.mayoreo_ventas
				ORDER BY
					mayoreo_venta_id
				DESC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["mayoreo_venta_id"]);
			}

			return 0;
		}
	}
}
