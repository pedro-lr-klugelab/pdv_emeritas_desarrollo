using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;
using System.Data;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Entradas
	{
		Conector conector = new Conector();
		DAO_Articulos dao_articulos = new DAO_Articulos();

		public bool existe_entrada_id(long entrada_id)
		{
			string sql = @"
				SELECT
					*
				FROM
					farmacontrol_local.entradas
				WHERE
					entrada_id = @entrada_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("entrada_id",entrada_id);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool es_producto_empaque(long detallado_entrada_id)
		{
			string sql = @"
				SELECT
					padre_articulo_id
				FROM
					farmacontrol_local.detallado_entradas
				WHERE
					detallado_entrada_id = @detallado_entrada_id
				AND
					padre_articulo_id IS NOT NULL
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("detallado_entrada_id",detallado_entrada_id);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public DataTable get_entradas_mayorista(long mayorista_id, string numero_factura)
		{
			string sql = @"
				SELECT
					entrada_id,
					factura,
					CAST(DATE_FORMAT(fecha_creado,'%Y-%m-%d %h:%i:%s') AS CHAR(50)) AS fecha_creado,
					CAST(DATE_FORMAT(fecha_terminado,'%Y-%m-%d %h:%i:%s') AS CHAR(50)) AS fecha_terminado,
					tipo_entrada,
					comentarios
				FROM
					farmacontrol_local.entradas
				WHERE
					mayorista_id = @mayorista_id
				AND
					fecha_terminado IS NOT NULL
				AND
					factura LIKE @factura
				AND
					factura NOT LIKE 'SF_%'
				ORDER BY entrada_id DESC
				LIMIT 20
			";


			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("mayorista_id", mayorista_id);
			parametros.Add("factura", "%" + numero_factura+"%");

			conector.Select(sql, parametros);

			var result = conector.result_set;

			if (result.Rows.Count > 0)
			{
				foreach (DataRow row in result.Rows)
				{
					row["fecha_creado"] = Misc_helper.fecha(row["fecha_creado"].ToString());
					row["fecha_terminado"] = Misc_helper.fecha(row["fecha_terminado"].ToString());

					string[] split_factura = row["factura"].ToString().Split('_');

					if (split_factura[0].ToString().Equals("SF"))
					{
						if (split_factura[1].ToString().Length == 32)
						{
							row["factura"] = "SIN FACTURA";
						}
					}
				}
			}

			return result;
		}

		public DataTable get_entradas_mayorista(long mayorista_id, int folio_recepcion = 0)
		{
			string complemento = "";

			if(folio_recepcion > 0)
			{
				complemento = @"
					AND
						entrada_id = @entrada_id
				";
			}

			string sql = string.Format(@"
				SELECT
					entrada_id,
					factura,
					CAST(DATE_FORMAT(fecha_creado,'%Y-%m-%d %h:%i:%s') AS CHAR(50)) AS fecha_creado,
					CAST(DATE_FORMAT(fecha_terminado,'%Y-%m-%d %h:%i:%s') AS CHAR(50)) AS fecha_terminado,
					tipo_entrada,
					comentarios
				FROM
					farmacontrol_local.entradas
				WHERE
					mayorista_id = @mayorista_id
				AND
					fecha_terminado IS NOT NULL
				{0}
				ORDER BY entrada_id DESC
				LIMIT 20
			", complemento);


			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("mayorista_id",mayorista_id);
			parametros.Add("entrada_id",folio_recepcion);

			conector.Select(sql,parametros);

			var result = conector.result_set;

			if(result.Rows.Count > 0)
			{
				foreach(DataRow row in result.Rows)
				{
					row["fecha_creado"] = Misc_helper.fecha(row["fecha_creado"].ToString());
					row["fecha_terminado"] = Misc_helper.fecha(row["fecha_terminado"].ToString());

					string[] split_factura = row["factura"].ToString().Split('_');

					if(split_factura[0].ToString().Equals("SF"))
					{
						if(split_factura[1].ToString().Length == 32)
						{
							row["factura"] = "SIN FACTURA";
						}	
					}
				}	
			}

			return result;
		}

		public void registrar_nombre_archivo(long entrada_id, string nombre)
		{
			string sql = @"
				INSERT INTO
					farmacontrol_local.entradas_scans
				SET
					entrada_id = @entrada_id,
					archivo = @nombre_archivo,
					fecha = NOW(),
					comentarios = ''
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("entrada_id",entrada_id);
			parametros.Add("nombre_archivo",nombre);

			conector.Insert(sql,parametros);
		}

		public DTO_Validacion desasociar_terminal(long entrada_id, long empleado_id)
		{
			DTO_Validacion val = new DTO_Validacion();
			int terminal_id = (int)Misc_helper.get_terminal_id();

			string sql = @"
				UPDATE
					farmacontrol_local.entradas
				SET
					terminal_id = NULL
				WHERE
					entrada_id = @entrada_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("entrada_id", entrada_id);

			conector.Update(sql, parametros);

			if (conector.filas_afectadas > 0)
			{
				val.status = true;
				val.informacion = "Terminal desasociada correctamente";
			}
			else
			{
				val.status = false;
				val.informacion = "Ocurrio un problema al intentar desasociar esta terminal, notifique a su administrador";
			}

			return val;
		}

		public DTO_Validacion asociar_terminal(long entrada_id, long empleado_id)
		{
			DTO_Validacion val = new DTO_Validacion();

			int terminal_id = (int)Misc_helper.get_terminal_id();
			string sql = @"
				UPDATE
					farmacontrol_local.entradas
				SET
					terminal_id = @terminal_id,
					empleado_id = @empleado_id
				WHERE
					entrada_id = @entrada_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("terminal_id",terminal_id);
			parametros.Add("empleado_id",empleado_id);
			parametros.Add("entrada_id",entrada_id);

			conector.Update(sql,parametros);

			if (conector.filas_afectadas > 0)
			{
				val.status = true;
				val.informacion = "Terminal asociada correctamente";
			}
			else
			{
				val.status = false;
				val.informacion = "Ocurrio un problema al intentar asociar esta terminal, notifique a su administrador";
			}

			return val;
		}

		public void afectar_entrada(long entrada_id)
		{
			string sql = @"
				SELECT
					articulo_id,
					CAST(DATE_FORMAT(caducidad,'%Y-%m-%d') AS CHAR(50)) AS caducidad,
					lote,
					cantidad
				FROM
					farmacontrol_local.detallado_entradas
				WHERE
					entrada_id = @entrada_id
				GROUP BY
					articulo_id,caducidad,lote
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("entrada_id", entrada_id);

			conector.Select(sql, parametros);

			var detallado = conector.result_set;

			List<Tuple<int, string, string, int>> lista_productos = new List<Tuple<int, string, string, int>>();

			foreach (DataRow row in detallado.Rows)
			{
				int articulo_id = Convert.ToInt32(row["articulo_id"]);
				string caducidad = row["caducidad"].ToString();
				string lote = row["lote"].ToString();
				int cantidad = Convert.ToInt32(row["cantidad"]);

				Tuple<int, string, string, int> tupla = new Tuple<int, string, string, int>(articulo_id, caducidad, lote, cantidad);
				lista_productos.Add(tupla);
			}

			dao_articulos.afectar_existencias_entrada(lista_productos, "ENTRADA",entrada_id, entrada_id);
		}

		public List<Tuple<string, string, int>> get_detallado_caducidades(long entrada_id, int articulo_id)
		{
			string sql = @"
				SELECT
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					COALESCE(cantidad,0) AS cantidad
				FROM
					farmacontrol_local.detallado_entradas
				WHERE
					entrada_id = @entrada_id
				AND
					articulo_id = @articulo_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("entrada_id", entrada_id);
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

		public DTO_Entrada_ticket get_entrada_ticket(long entrada_id)
		{
			DTO_Entrada_ticket entrada_ticket = new DTO_Entrada_ticket();

			string sql = @"
				SELECT
					entrada_id,
					terminal_id,
					factura,
					empleado_id,
					termina_empleado_id,
					tipo_entrada,
					CAST(DATE_FORMAT(fecha_creado,'%Y-%m-%d %H:%i:%s') AS CHAR(50)) AS fecha_creado,
					CAST(DATE_FORMAT(fecha_recibido,'%Y-%m-%d %H:%i:%s') AS CHAR(50)) AS fecha_recibido,
					CAST(DATE_FORMAT(fecha_terminado,'%Y-%m-%d %H:%i:%s') AS CHAR(50)) AS fecha_terminado,
					comentarios,
					IF(terminal_id IS NULL,'ENTRADA GENERADA POR EL SISTEMA',(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = entradas.empleado_id) ) AS nombre_empleado_captura,
					IF(terminal_id IS NULL,'ENTRADA GENERADA POR EL SISTEMA',(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = entradas.termina_empleado_id) ) AS nombre_empleado_termina
				FROM
					farmacontrol_local.entradas
				WHERE
					entrada_id = @entrada_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("entrada_id", entrada_id);
			conector.Select(sql, parametros);

			foreach (DataRow traspaso_row in conector.result_set.Rows)
			{
				entrada_ticket.entrada_id = Convert.ToInt32(traspaso_row["entrada_id"]);
				entrada_ticket.terminal_id = Convert.ToInt32(traspaso_row["terminal_id"]);
				entrada_ticket.empleado_id = Convert.ToInt32(traspaso_row["empleado_id"]);
				entrada_ticket.factura = traspaso_row["factura"].ToString();
				entrada_ticket.termina_empleado_id = Convert.ToInt32(traspaso_row["termina_empleado_id"]);
				entrada_ticket.tipo_entrada = traspaso_row["tipo_entrada"].ToString();

				DateTime? date_nullable = null;

				entrada_ticket.fecha_creado = (traspaso_row["fecha_creado"].ToString().Equals("")) ? date_nullable : Convert.ToDateTime(traspaso_row["fecha_creado"]);
				entrada_ticket.fecha_recibido = (traspaso_row["fecha_recibido"].ToString().Equals("")) ? date_nullable: Convert.ToDateTime(traspaso_row["fecha_recibido"]);
				entrada_ticket.fecha_terminado = (traspaso_row["fecha_terminado"].ToString().Equals("")) ? date_nullable :Convert.ToDateTime(traspaso_row["fecha_terminado"].ToString());

				entrada_ticket.comentarios = traspaso_row["comentarios"].ToString();
				entrada_ticket.nombre_empleado_captura = traspaso_row["nombre_empleado_captura"].ToString();
				entrada_ticket.nombre_empleado_termina = traspaso_row["nombre_empleado_termina"].ToString();
			}

			return entrada_ticket;
		}

		public List<DTO_Detallado_entradas_ticket> get_detallado_entrada_ticket(long entrada_id)
		{
			
			List<DTO_Detallado_entradas_ticket> lista_detallado_entrada = new List<DTO_Detallado_entradas_ticket>();

			string sql = @"
				SELECT
					articulos.articulo_id,
					RPAD(CONCAT('*',SUBSTRING(articulos_amecops.amecop, LENGTH(articulos_amecops.amecop)-3) ),5,' ') AS amecop,
					RPAD(nombre, 37, ' ') AS nombre,
					detallado_entradas.precio_costo,
					SUM(detallado_entradas.cantidad) AS cantidad,
					SUM(detallado_entradas.total) AS total
				FROM
					farmacontrol_local.detallado_entradas
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id)
				WHERE
					entrada_id = @entrada_id
				GROUP BY
					articulo_id
				ORDER BY nombre ASC
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("entrada_id", entrada_id);

			conector.Select(sql, parametros);

			var result_set = conector.result_set;

			foreach (DataRow row in result_set.Rows)
			{
				DTO_Detallado_entradas_ticket detallado_ticket = new DTO_Detallado_entradas_ticket();
				detallado_ticket.articulo_id = Convert.ToInt32(row["articulo_id"]);
				detallado_ticket.amecop = row["amecop"].ToString();
				detallado_ticket.nombre = row["nombre"].ToString();
				detallado_ticket.precio_costo = Convert.ToDecimal(row["precio_costo"]);
				detallado_ticket.total = Convert.ToDecimal(row["total"]);
				detallado_ticket.caducidades_lotes = get_detallado_caducidades(entrada_id, detallado_ticket.articulo_id);
				lista_detallado_entrada.Add(detallado_ticket);
			}

			return lista_detallado_entrada;
		}

		public void set_comentarios(long entrada_id, string comentarios)
		{
			string sql = @"
				UPDATE 
					farmacontrol_local.entradas
				SET
					comentarios = @comentarios
				WHERE
					entrada_id = @entrada_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("comentarios",comentarios);
			parametros.Add("entrada_id",entrada_id);

			conector.Update(sql,parametros);
		}

		public void set_factura(long entrada_id, string factura)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.entradas
				SET
					factura = @factura
				WHERE
					entrada_id = @entrada_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("factura",factura);
			parametros.Add("entrada_id",entrada_id);

			conector.Update(sql,parametros);
		}

		public DTO_Validacion terminar_entrada(long entrada_id,long mayorista_id, string factura, long empleado_id)
		{
			DTO_Validacion val = new DTO_Validacion();

			string sql = @"
				SELECT
					entrada_id
				FROM
					farmacontrol_local.entradas
				WHERE
					factura = @factura
				AND
					mayorista_id = @mayorista_id
				AND
					fecha_terminado IS NOT NULL
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("factura",factura);
			parametros.Add("mayorista_id",mayorista_id);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				val.status = false;
				val.informacion = "La factura ya se encuentra registrada para este mayorista";
			}
			else
			{
				sql = @"
					UPDATE
						farmacontrol_local.entradas
					SET
						termina_empleado_id = @empleado_id,
						fecha_terminado = NOW(),
						factura = @factura
					WHERE
						entrada_id = @entrada_id
				";

				parametros = new Dictionary<string, object>();
				parametros.Add("empleado_id", empleado_id);
				parametros.Add("entrada_id", entrada_id);
				parametros.Add("factura",factura);

				conector.Update(sql,parametros);

				if(conector.filas_afectadas > 0)
				{
					afectar_entrada(entrada_id);
					val.status = true;
					val.informacion = "Entrada terminada correctamente";
				}
				else
				{
					val.status = false;
					val.informacion = "Ocurrio un error al intentar terminar la entrada, notifique a su administrador";
				}
			}

			return val;
		}

		public void eliminar_detalado_entrada_paquete(long entrada_id, long detallado_entrada_id)
		{
			string sql = @"
				SELECT
					padre_articulo_id
				FROM
					farmacontrol_local.detallado_entradas
				WHERE
					detallado_entrada_id = @detallado_entrada_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("detallado_entrada_id", detallado_entrada_id);

			conector.Select(sql,parametros);

			long padre_articulo_id = Convert.ToInt64(conector.result_set.Rows[0]["padre_articulo_id"]);

			sql = @"
				DELETE FROM
					farmacontrol_local.detallado_entradas
				WHERE
					entrada_id = @entrada_id
				AND
					padre_articulo_id = @padre_articulo_id
			";

			parametros = new Dictionary<string,object>();
			parametros.Add("entrada_id",entrada_id);
			parametros.Add("padre_articulo_id",padre_articulo_id);

			conector.Delete(sql,parametros);
		}

		public void eliminar_detallado_entrada(long detallado_entrada_id)
		{
			
			string sql = @"
				DELETE FROM
					farmacontrol_local.detallado_entradas
				WHERE
					detallado_entrada_id = @detallado_entrada_id
			";	
			

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("detallado_entrada_id",detallado_entrada_id);

			conector.Delete(sql,parametros);
		}

		public void set_tipo(long entrada_id, string tipo)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.entradas
				SET
					tipo_entrada = @tipo
				WHERE
					entrada_id = @entrada_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("tipo",(tipo.Equals("-") ? null : tipo));
			parametros.Add("entrada_id",entrada_id);

			conector.Update(sql,parametros);
		}

		public void set_mayorista_id(long entrada_id, long? mayorista_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.entradas
				SET
					mayorista_id = @mayorista_id
				WHERE
					entrada_id = @entrada_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("entrada_id",entrada_id);
			parametros.Add("mayorista_id",mayorista_id);

			conector.Update(sql,parametros);
		}

		public void actualizar_oferta(long mayorista_id, long entrada_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.detallado_entradas
				LEFT JOIN
				(
					SELECT
	                    detallado_entrada_id,
	                    IF(articulos.afecta_cambios_precio = 1, IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo), articulos.precio_costo) AS precio_costo,
	                    0 AS pct_oferta,
	                    0 AS importe_oferta,
	                    IF(articulos.afecta_cambios_precio = 1, IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo), articulos.precio_costo) AS importe,
	                    (IF(articulos.afecta_cambios_precio = 1, IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo), articulos.precio_costo) * detallado_entradas.cantidad) AS subtotal,
	                    IF(articulos.tipo_ieps = 'PCT', ((IF(articulos.afecta_cambios_precio = 1, IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo), articulos.precio_costo)) * articulos.ieps) , articulos.ieps) AS importe_ieps,
	                    (((IF(articulos.afecta_cambios_precio = 1, IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo), articulos.precio_costo)) * detallado_entradas.cantidad) * articulos.pct_iva) AS importe_iva,
	                    ((IF(articulos.afecta_cambios_precio = 1, IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo), articulos.precio_costo) * detallado_entradas.cantidad) + (((IF(articulos.afecta_cambios_precio = 1, IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo), articulos.precio_costo)) * detallado_entradas.cantidad) * articulos.pct_iva)) AS total
                    FROM
						farmacontrol_local.detallado_entradas
					LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
					LEFT JOIN farmacontrol_global.articulos_mayoristas USING(articulo_id)
					WHERE
						detallado_entradas.entrada_id = @entrada_id
					GROUP BY
						detallado_entrada_id
				) AS tmp USING(detallado_entrada_id)
				SET
					detallado_entradas.pct_oferta = tmp.pct_oferta,
					detallado_entradas.importe_oferta = tmp.importe_oferta,
					detallado_entradas.precio_costo = tmp.precio_costo,
					detallado_entradas.importe_ieps = tmp.importe_ieps,
					detallado_entradas.importe = tmp.importe,
					detallado_entradas.subtotal = tmp.subtotal,
					detallado_entradas.importe_iva = tmp.importe_iva,
					detallado_entradas.total = tmp.total
                WHERE
                    detallado_entradas.entrada_id = @entrada_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("entrada_id",entrada_id);

			conector.Update(sql,parametros);
			
			sql = @"
				UPDATE
					farmacontrol_local.detallado_entradas
				LEFT JOIN
					(
					SELECT
						detallado_entrada_id,
						detallado_entradas.cantidad,
						COALESCE(MAX(ofertas.pct_oferta),0) AS pct_oferta,
						IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo) * COALESCE(MAX(ofertas.pct_oferta),0) AS importe_oferta,
						IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo) - IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo) * COALESCE(MAX(ofertas.pct_oferta),0) AS precio_costo,
						IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo) - IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo) * COALESCE(MAX(ofertas.pct_oferta),0) AS importe,
						(IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo) - IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo) * COALESCE(MAX(ofertas.pct_oferta),0)) * detallado_entradas.cantidad AS subtotal,
						((IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo) - IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo) * COALESCE(MAX(ofertas.pct_oferta),0)) * detallado_entradas.cantidad) * articulos.pct_iva AS importe_iva,
						((IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo) - IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo) * COALESCE(MAX(ofertas.pct_oferta),0)) * detallado_entradas.cantidad) + ( (IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo) - IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo) * COALESCE(MAX(ofertas.pct_oferta),0) * detallado_entradas.cantidad) * articulos.pct_iva ) AS total,
						IF(articulos.tipo_ieps = 'PCT', ((IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo) - IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo) * COALESCE(MAX(ofertas.pct_oferta),0)) * articulos.ieps) , articulos.ieps) AS importe_ieps
					FROM
						farmacontrol_local.detallado_entradas
					LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
					LEFT JOIN farmacontrol_global.articulos_mayoristas USING(articulo_id)
					LEFT JOIN farmacontrol_global.ofertas USING(articulo_id)
					WHERE
						detallado_entradas.entrada_id = @entrada_id
					AND
						ofertas.mayorista_id = @mayorista_id
					AND
						 ofertas.minimo_piezas <= COALESCE((SELECT SUM(cantidad) AS cantidad FROM farmacontrol_local.detallado_entradas tmp_de WHERE tmp_de.entrada_id = @entrada_id AND tmp_de.articulo_id = detallado_entradas.articulo_id),0)
					GROUP BY
						detallado_entradas.detallado_entrada_id
					) AS tmp USING(detallado_entrada_id)
				SET
					detallado_entradas.pct_oferta = tmp.pct_oferta,
					detallado_entradas.importe_oferta = tmp.importe_oferta,
					detallado_entradas.precio_costo = tmp.precio_costo,
					detallado_entradas.importe_ieps = tmp.importe_ieps,
					detallado_entradas.importe = tmp.importe,
					detallado_entradas.subtotal = tmp.subtotal,
					detallado_entradas.importe_iva = tmp.importe_iva,
					detallado_entradas.total = tmp.total
				WHERE
					detallado_entradas.detallado_entrada_id = tmp.detallado_entrada_id
			";

			parametros.Add("mayorista_id",mayorista_id);
			conector.Update(sql,parametros);
		}

		public long insertar_producto(long entrada_id, string amecop, string caducidad, string lote, long cantidad, long? padre_articulo_id = null)
		{
            //IF(articulos.afecta_cambios_precio = 1, IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo), articulos.precio_costo) AS precio_costo,
            //IF(articulos.afecta_cambios_precio = 1, IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo), articulos.precio_costo) AS importe,
            //FORMAT((IF(articulos.afecta_cambios_precio = 1, IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo), articulos.precio_costo) * 0 ), 4) AS importe_descuento,
            //(IF(articulos.afecta_cambios_precio = 1, IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo), articulos.precio_costo)) * @cantidad AS subtotal,
            // IF(tipo_ieps = 'PCT', ((IF(articulos.afecta_cambios_precio = 1, IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo), articulos.precio_costo)) * ieps) , ieps) AS importe_ieps,
            //(((IF(articulos.afecta_cambios_precio = 1, IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo), articulos.precio_costo)) * @cantidad) * pct_iva) AS importe_iva,
            //((IF(articulos.afecta_cambios_precio = 1, IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo), articulos.precio_costo)) * @cantidad) + (((IF(articulos.afecta_cambios_precio = 1, IF(articulos.afecta_cambios_precio = 1, IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo), articulos.precio_costo), articulos.precio_costo)) * @cantidad) * pct_iva) AS total,
			//FORMAT((IF(articulos.afecta_cambios_precio = 1, IF(articulos_mayoristas.precio_costo IS NULL,articulos.precio_costo,articulos_mayoristas.precio_costo), articulos.precio_costo) * 0 ), 4) AS importe_descuento,
            string sql = @"
				INSERT INTO
					farmacontrol_local.detallado_entradas
				(
                    SELECT
	                    0 as detallado_entrada_id,
	                    @entrada_id AS entrada_id,
	                    articulo_id,
	                    @padre_articulo_id AS padre_articulo_id,
	                    @caducidad AS caducidad,
	                    @lote AS lote,
	                    articulos.precio_costo AS precio_costo,
	                    0 AS pct_oferta,
	                    0 AS importe_oferta,
	                    0 AS pct_descuento,
	                    FORMAT(articulos.precio_costo, 4) AS importe_descuento,
	                    articulos.precio_costo AS importe,
	
	                    @cantidad AS cantidad,

	                    articulos.precio_costo * @cantidad AS subtotal,
	                    pct_iva,
	                    tipo_ieps,
	                    ieps,
	                    IF(tipo_ieps = 'PCT', articulos.precio_costo * ieps , ieps) AS importe_ieps,
	                    (articulos.precio_costo * @cantidad) * pct_iva AS importe_iva,
	                    (articulos.precio_costo * @cantidad) + ((articulos.precio_costo * @cantidad) * pct_iva) AS total,
	                    NOW() AS modified
                    FROM
	                    
                        farmacontrol_global.articulos
                    LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id)
                    LEFT JOIN farmacontrol_global.articulos_mayoristas USING(articulo_id)
                    WHERE
	                    articulos_amecops.amecop = @amecop
				)
				ON DUPLICATE KEY UPDATE
                    precio_costo = articulos.precio_costo,
					cantidad = cantidad + @cantidad,
					subtotal = subtotal + ((articulos.precio_costo - (articulos.precio_costo * 0)) * @cantidad),
					importe_iva = importe_iva + (((articulos.precio_costo - (articulos.precio_costo * 0 )) * @cantidad) * articulos.pct_iva),
					total = total + ((articulos.precio_costo - (articulos.precio_costo * 0)) * @cantidad) + (((articulos.precio_costo - (articulos.precio_costo * 0)) * @cantidad) * articulos.pct_iva)
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("entrada_id",entrada_id);
			parametros.Add("amecop",amecop);
			parametros.Add("caducidad",caducidad);
			parametros.Add("lote",lote);
			parametros.Add("cantidad",cantidad);
			parametros.Add("padre_articulo_id", padre_articulo_id);

			conector.Insert(sql,parametros);

			return conector.insert_id;
		}

		public DataTable get_detallado_entradas(long entrada_id)
		{
			string sql = @"
				SELECT
					detallado_entrada_id,
					detallado_entradas.articulo_id AS articulo_id,
					(
						SELECT 
							amecop
						FROM 
							farmacontrol_global.articulos_amecops 
						WHERE 
							articulo_id = detallado_entradas.articulo_id 
						ORDER BY
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS amecop,
					nombre AS producto,
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					detallado_entradas.precio_costo AS precio_costo,
					detallado_entradas.pct_oferta AS pct_oferta,
					detallado_entradas.importe_oferta AS importe_oferta,
					detallado_entradas.pct_descuento AS pct_descuento,
					detallado_entradas.importe_descuento AS importe_descuento,
					detallado_entradas.importe AS importe,
					detallado_entradas.cantidad AS cantidad,
					detallado_entradas.subtotal AS subtotal,
					detallado_entradas.pct_iva AS pct_iva,
					detallado_entradas.tipo_ieps AS tipo_ieps,
					detallado_entradas.ieps AS ieps,
					detallado_entradas.importe_ieps AS importe_ieps,
					detallado_entradas.importe_iva AS importe_iva,
					detallado_entradas.total AS total
				FROM
					farmacontrol_local.detallado_entradas
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					entrada_id = @entrada_id
				GROUP BY
					detallado_entrada_id
				ORDER BY
					detallado_entrada_id
				DESC
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("entrada_id",entrada_id);

			conector.Select(sql,parametros);

			var resul_set = conector.result_set;

			foreach(DataRow row in resul_set.Rows)
			{
                row["caducidad"] = Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");
			}

			return resul_set;
		}

		public DataTable get_detallado_entradas_devoluciones(long devolucion_id, long entrada_id)
		{
			string sql = @"
				SELECT
					detallado_entrada_id,
					detallado_entradas.articulo_id AS articulo_id,
					(
						SELECT 
							amecop
						FROM 
							farmacontrol_global.articulos_amecops 
						WHERE 
							articulo_id = detallado_entradas.articulo_id 
						ORDER BY
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS amecop,
					nombre AS producto,
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					detallado_entradas.precio_costo AS precio_costo,
					detallado_entradas.pct_oferta AS pct_oferta,
					detallado_entradas.importe_oferta AS importe_oferta,
					detallado_entradas.pct_descuento AS pct_descuento,
					detallado_entradas.importe_descuento AS importe_descuento,
					detallado_entradas.importe AS importe,
					detallado_entradas.cantidad AS cantidad,
					detallado_entradas.subtotal AS subtotal,
					detallado_entradas.pct_iva AS pct_iva,
					detallado_entradas.tipo_ieps AS tipo_ieps,
					detallado_entradas.ieps AS ieps,
					detallado_entradas.importe_ieps AS importe_ieps,
					detallado_entradas.importe_iva AS importe_iva,
					detallado_entradas.total AS total,
					COALESCE(tmp_dev.cantidad,0) AS cantidad_devoluciones,
					COALESCE(tmp_vendible.cantidad, 0) AS cantidad_vendible,
					COALESCE(tmp_dev_terminadas.cantidad,0) AS cantidad_terminadas,
					COALESCE(tmp_devolucion_actual.cantidad,0) AS cantidad_actual,
					tmp_devolucion_actual.motivo AS motivo_actual
				FROM
					farmacontrol_local.detallado_entradas
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				LEFT JOIN 
					(
						SELECT
							articulo_id,
							caducidad,
							lote,
							cantidad,
							motivo
						FROM
							farmacontrol_local.devoluciones
						LEFT JOIN farmacontrol_local.detallado_devoluciones USING(devolucion_id)
						WHERE
							devolucion_id = @devolucion_id
						AND
							entrada_id = @entrada_id
					) AS tmp_devolucion_actual USING(articulo_id,caducidad,lote)
				LEFT JOIN 
					(
						SELECT
							detallado_entrada_id,
							COALESCE(SUM(detallado_devoluciones.cantidad),0) AS cantidad
						FROM
							farmacontrol_local.detallado_entradas
						LEFT JOIN farmacontrol_local.devoluciones USING(entrada_id)
						LEFT JOIN farmacontrol_local.detallado_devoluciones USING(devolucion_id,articulo_id,caducidad,lote)
						WHERE
							entrada_id = @entrada_id
						AND
							devoluciones.fecha_terminado IS NOT NULL
						GROUP BY
							detallado_entradas.articulo_id, detallado_entradas.caducidad, detallado_entradas.lote
					) AS tmp_dev_terminadas USING(detallado_entrada_id)
				LEFT JOIN 
					(
						SELECT
							detallado_entrada_id,
							COALESCE(SUM(detallado_devoluciones.cantidad),0) AS cantidad
						FROM
							farmacontrol_local.detallado_entradas
						LEFT JOIN farmacontrol_local.devoluciones USING(entrada_id)
						LEFT JOIN farmacontrol_local.detallado_devoluciones USING(devolucion_id,articulo_id,caducidad,lote)
						WHERE
							entrada_id = @entrada_id
						AND
							devoluciones.fecha_terminado IS NULL
						GROUP BY
							detallado_entradas.articulo_id, detallado_entradas.caducidad, detallado_entradas.lote
					) AS tmp_dev USING(detallado_entrada_id)
				LEFT JOIN 
					(
						SELECT
							detallado_entrada_id,
							COALESCE(SUM(existencia), 0) - COALESCE(existencia_mermas, 0) - COALESCE(existencia_devoluciones, 0) - COALESCE(existencia_apartados, 0) - COALESCE(existencia_cambio_fisico, 0) - COALESCE(existencia_ventas, 0) - COALESCE(existencia_traspasos, 0)AS cantidad
						FROM
							farmacontrol_local.detallado_entradas
						LEFT JOIN farmacontrol_local.existencias USING(articulo_id,caducidad,lote)
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
						WHERE
							detallado_entradas.entrada_id = @entrada_id
						GROUP BY 
							detallado_entradas.articulo_id, detallado_entradas.caducidad, detallado_entradas.lote
		
					) AS tmp_vendible USING(detallado_entrada_id)
				WHERE
					entrada_id = @entrada_id
				GROUP BY
					detallado_entrada_id
				ORDER BY
					detallado_entrada_id
				DESC
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("entrada_id", entrada_id);
			parametros.Add("devolucion_id",devolucion_id);

			conector.Select(sql, parametros);

			var resul_set = conector.result_set;

			foreach (DataRow row in resul_set.Rows)
			{
                row["caducidad"] = Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");
			}

			return resul_set;
		}

		public long get_entrada_siguiente(long traspaso_id)
		{
			string sql = @"
				SELECT
					entrada_id
				FROM
					farmacontrol_local.entradas
				WHERE
					entrada_id > @entrada_id
				ORDER BY
					entrada_id
				ASC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("entrada_id", traspaso_id);

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["entrada_id"]);
			}

			return 0;
		}

		public long get_entrada_atras(long traspaso_id)
		{
			string sql = @"
				SELECT
					entrada_id
				FROM
					farmacontrol_local.entradas
				WHERE
					entrada_id < @entrada_id
				ORDER BY
					entrada_id
				DESC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("entrada_id", traspaso_id);

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["entrada_id"]);
			}

			return 0;
		}

		public long get_entrada_inicio()
		{
			string sql = @"
				SELECT
					entrada_id
				FROM
					farmacontrol_local.entradas
				ORDER BY
					entrada_id
				ASC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["entrada_id"]);
			}

			return 0;
		}

		public long get_entrada_fin()
		{
			string sql = @"
				SELECT
					entrada_id
				FROM
					farmacontrol_local.entradas
				ORDER BY
					entrada_id
				DESC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["entrada_id"]);
			}

			return 0;
		}

		public DTO_Entradas crear_entrada(long empleado_id)
		{
			string sql = @"
				INSERT INTO
					farmacontrol_local.entradas
				SET
					empleado_id = @empleado_id,
					factura = '',
					comentarios = '',
					terminal_id = @terminal_id,
					fecha_creado = NOW()
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("empleado_id",empleado_id);
			parametros.Add("terminal_id",Convert.ToInt32(Misc_helper.get_terminal_id()));

			conector.Insert(sql,parametros);

			long insert_id = 0;

			if(conector.insert_id > 0)
			{
				insert_id = Convert.ToInt64(conector.insert_id);
			}

			return get_informacion_entrada(insert_id);
		}

		public DTO_Entradas get_informacion_entrada(long entrada_id)
		{
			DTO_Entradas dto_entradas = new DTO_Entradas();

			string sql = @"
				SELECT
					entrada_id,
					IF(terminal_id IS NULL,0,terminal_id) AS terminal_id,
					IF(mayorista_id IS NULL,0,mayorista_id) AS mayorista_id,
					empleado_id,
					IF(termina_empleado_id IS NULL, 0, termina_empleado_id) AS termina_empleado_id,
					fecha_creado,
					fecha_recibido,
					fecha_terminado,
					factura,
					tipo_entrada,
					comentarios,
					(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = entradas.empleado_id) AS nombre_empleado_captura,
					IF(empleado_id IS NULL,'',(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = entradas.termina_empleado_id) ) AS nombre_empleado_termina
				FROM
					farmacontrol_local.entradas
				WHERE
					entrada_id = @entrada_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("entrada_id",entrada_id);

			conector.Select(sql,parametros);

			var result_set = conector.result_set;

			if(result_set.Rows.Count > 0)
			{
				var row = result_set.Rows[0];

				dto_entradas.entrada_id = Convert.ToInt64(row["entrada_id"]);
				dto_entradas.terminal_id = Convert.ToInt32(row["terminal_id"]);
				dto_entradas.mayorista_id = Convert.ToInt64(row["mayorista_id"]);
				dto_entradas.empleado_id = Convert.ToInt64(row["empleado_id"]);
				dto_entradas.termina_empleado_id = Convert.ToInt64(row["termina_empleado_id"]);
				
				DateTime? date_null = null;

				dto_entradas.fecha_creado = (row["fecha_creado"].ToString().Equals("")) ? date_null:  Convert.ToDateTime(row["fecha_creado"]);
				dto_entradas.fecha_recibido = (row["fecha_recibido"].ToString().Equals("") ) ? date_null : Convert.ToDateTime(row["fecha_recibido"]);
				dto_entradas.fecha_terminado = (row["fecha_terminado"].ToString().Equals("")) ? date_null : Convert.ToDateTime(row["fecha_terminado"].ToString());

				dto_entradas.factura = row["factura"].ToString();
				dto_entradas.tipo_entrada = row["tipo_entrada"].ToString();
				dto_entradas.comentarios = row["comentarios"].ToString();
				dto_entradas.nombre_empleado_captura = row["nombre_empleado_captura"].ToString();
				dto_entradas.nombre_empleado_termina = row["nombre_empleado_termina"].ToString();
			}
			else
			{
				dto_entradas.entrada_id = 0;
			}

			return dto_entradas;
		}

        public DataTable get_entradas_dia()
        {
            DataTable result = new DataTable();
            string sql = @"
				SELECT                   entrada_id as folio,                   nombre as mayorista,                   factura                FROM                   farmacontrol_local.entradas                INNER JOIN                    farmacontrol_global.mayoristas                USING(mayorista_id)                WHERE                   DATE( fecha_terminado ) = CURDATE()
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
           
            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                result = conector.result_set;
            }

            return result;
        }

        public DataTable get_entradas_pendientes()
        {
            DataTable result = new DataTable();
            string sql = @"
				SELECT	                entrada_id                FROM                  farmacontrol_local.entradas                WHERE                  fecha_terminado IS NULL
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                result = conector.result_set;
            }

            return result;
        }

    
    }
}
