using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;
using System.Data;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.CLASSES.PRINT;


namespace Farmacontrol_PDV.DAO
{
	class DAO_Devoluciones
	{
		Conector conector = new Conector();

        public void set_fecha_devolucion(long devolucion_id, string fecha_devolucion)
        {
            string sql = @"
                UPDATE
                    farmacontrol_local.devoluciones
                SET
                    solicitud_devolucion_fecha = @fecha_devolucion
                WHERE
                    devolucion_id = @devolucion_id
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("devolucion_id", devolucion_id);
            parametros.Add("fecha_devolucion", fecha_devolucion);

            conector.Update(sql, parametros);
        }

        public void set_folio_devolucion(long devolucion_id, string folio_devolucion)
        {
            string sql = @"
                UPDATE
                    farmacontrol_local.devoluciones
                SET
                    solicitud_devolucion_folio = @folio
                WHERE
                    devolucion_id = @devolucion_id
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("devolucion_id",devolucion_id);
            parametros.Add("folio",folio_devolucion);

            conector.Update(sql, parametros);
        }

		public bool asociar_terminal(long devolucion_id, long empleado_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.devoluciones
				SET
					terminal_id = @terminal_id,
					empleado_id = @empleado_id
				WHERE
					devolucion_id = @devolucion_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("devolucion_id", devolucion_id);
			parametros.Add("terminal_id",Misc_helper.get_terminal_id());
			parametros.Add("empleado_id",empleado_id);

			conector.Update(sql, parametros);

			return (conector.filas_afectadas > 0);
		}

		public bool desasociar_terminal(long devolucion_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.devoluciones
				SET
					terminal_id = NULL
				WHERE
					devolucion_id = @devolucion_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("devolucion_id",devolucion_id);

			conector.Update(sql,parametros);

			return (conector.filas_afectadas > 0);
		}

		public void crear_devolucion_hija(long empleado_id, long devolucion_id, List<Tuple<int,string,string,int>> productos_aceptados)
		{
			string sql = @"
				INSERT INTO
					farmacontrol_local.devoluciones
				(
					SELECT
						0 AS devolucion_id,
						@devolucion_id AS devolucion_padre_id,
						terminal_id,
						mayorista_id,
						@empleado_id AS empleado_id,
						@empelado_id AS termina_empleado_id,
						audita_empleado_id,
						entrada_id,
						solicitud_devolucion_folio,
						solicitud_devolucion_fecha,
						NOW() AS fecha_creado,
						fecha_cerrado,
						NOW() AS fecha_terminado,
						NOW() AS fecha_auditada,
						NULL AS fecha_rechazado,
						NULL AS fecha_afectado,
						nota_credito_folio,
						nota_credito_fecha,
						nota_credito_importe,
						comentarios,
						modified
					FROM
						farmacontrol_local.devoluciones
					WHERE
						devolucion_id = @devolucion_id
				)
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("devolucion_id",devolucion_id);
			parametros.Add("empleado_id",empleado_id);

			conector.Insert(sql,parametros);

			long devolucion_hija_id = conector.insert_id;

			foreach(var producto in productos_aceptados)
			{
				registrar_detallado_devolucion(devolucion_hija_id,producto.Item1,producto.Item2,producto.Item3,(int)producto.Item4);
			}

			DAO_Articulos dao_articulos = new DAO_Articulos();
            dao_articulos.afectar_existencias_salida(productos_aceptados, "DEVOLUCION_MAYORISTA", devolucion_hija_id, devolucion_hija_id);

			Devoluciones_mayorista ticket = new Devoluciones_mayorista();
			ticket.construccion_ticket(devolucion_hija_id);
			ticket.print();

		}

		public long enviar_productos_apartado_mercancia(long devolucion_id, List<Tuple<int,string,string,int>> lista_rechazados)
		{
			string sql ="";

			if(lista_rechazados.Count > 0)
			{
				foreach(var tupla in lista_rechazados)
				{
					sql = @"
						INSERT INTO
							farmacontrol_local.apartados
						(
							SELECT
								0 AS apartado_id,
								articulo_id,
								NULL AS sucursal_id,
								NULL AS prepago_id,
								'DEVOLUCION' AS tipo,
								caducidad,
								lote,
								@cantidad AS cantidad,
								NOW() AS fecha_apartado,
								DATE_ADD(NOW(), INTERVAL 1 DAY)	AS fecha_expiracion
							FROM
								farmacontrol_local.detallado_devoluciones
							WHERE
								devolucion_id = @devolucion_id
							AND
								articulo_id = @articulo_id
							AND
								caducidad = @caducidad
							AND
								lote = @lote
						)
						ON DUPLICATE KEY UPDATE
							cantidad = apartados.cantidad + detallado_devoluciones.cantidad
					";

					Dictionary<string,object> parametros = new Dictionary<string,object>();
					parametros.Add("devolucion_id",devolucion_id);
					parametros.Add("articulo_id",tupla.Item1);
					parametros.Add("caducidad",tupla.Item2);
					parametros.Add("lote",tupla.Item3);
					parametros.Add("cantidad",tupla.Item4);

					conector.Insert(sql,parametros);
				}
			}
			else
			{
				sql = @"
					INSERT INTO
						farmacontrol_local.apartados
					(
						SELECT
							0 AS apartado_id,
							articulo_id,
							NULL AS sucursal_id,
							'DEVOLUCION' AS tipo,
							caducidad,
							lote,
							cantidad,
							NOW() AS fecha_apartado,
							DATE_ADD(NOW(), INTERVAL 1 DAY)	AS fecha_expiracion
						FROM
							farmacontrol_local.detallado_devoluciones
						WHERE
							devolucion_id = @devolucion_id
					)
					ON DUPLICATE KEY UPDATE
						cantidad = apartados.cantidad + detallado_devoluciones.cantidad
				";

				Dictionary<string,object> parametros = new Dictionary<string,object>();
				parametros.Add("devolucion_id",devolucion_id);

				conector.Insert(sql,parametros);
			}

			return conector.filas_afectadas;
		}

		public long enviar_devolucion_auditoria(long devolucion_id, long empleado_id, List<Tuple<int,string,string,int>> lista_rechazados)
		{
			DAO_Traspasos dao_traspaso = new DAO_Traspasos();
			
			int filas_afectadas = 0;

			long traspaso_id = dao_traspaso.crear_trapaso(Convert.ToInt64(Principal.empleado_id),  Convert.ToInt32(Config_helper.get_config_global("sucursal_auditoria")));

			if(traspaso_id > 0)
			{
				string sql = "";

				if(lista_rechazados.Count > 0)
				{
					foreach(var tupla in  lista_rechazados)
					{
						sql = @"
							INSERT INTO
								farmacontrol_local.detallado_traspasos
							(
								SELECT
									0 as detallado_traspaso_id,
									@traspaso_id AS traspaso_id,
									articulo_id,
									caducidad,
									lote,
									precio_costo,
									NULL AS cantidad_origen,
									0 AS cantidad_recibida,
									@cantidad AS cantidad,
									NULL AS accion,
									(precio_costo * cantidad) AS total,
									NOW() AS modified
								FROM
									farmacontrol_local.detallado_devoluciones
								WHERE
									devolucion_id = @devolucion_id
								AND
									articulo_id = @articulo_id
								AND
									caducidad = @caducidad
								AND
									lote = @lote
							)
							ON DUPLICATE KEY UPDATE
								cantidad = COALESCE(detallado_traspasos.cantidad,0) + @cantidad,
								total = detallado_traspasos.total + (detallado_devoluciones.precio_costo * @cantidad)
						";

						Dictionary<string, object> parametros = new Dictionary<string, object>();
						parametros.Add("devolucion_id", devolucion_id);
						parametros.Add("traspaso_id", traspaso_id);
						parametros.Add("articulo_id",tupla.Item1);
						parametros.Add("caducidad",tupla.Item2);
						parametros.Add("lote",tupla.Item3);
						parametros.Add("cantidad",tupla.Item4);

						conector.Insert(sql, parametros);
					}
				}
				else
				{
					sql = @"
						INSERT INTO
							farmacontrol_local.detallado_traspasos
						(
							SELECT
								0 as detallado_traspaso_id,
								@traspaso_id AS traspaso_id,
								articulo_id,
								caducidad,
								lote,
								precio_costo,
								NULL AS cantidad_origen,
								0 AS cantidad_recibida,
								cantidad,
								NULL AS accion,
								(precio_costo * cantidad) AS total,
								NOW() AS modified
							FROM
								farmacontrol_local.detallado_devoluciones
							WHERE
								devolucion_id = @devolucion_id
						)
						ON DUPLICATE KEY UPDATE
							cantidad = COALESCE(detallado_traspasos.cantidad,0) + detallado_devoluciones.cantidad,
							total = detallado_traspasos.total + (detallado_devoluciones.precio_costo * detallado_devoluciones.cantidad)
					";

					Dictionary<string, object> parametros = new Dictionary<string, object>();
					parametros.Add("devolucion_id", devolucion_id);
					parametros.Add("traspaso_id", traspaso_id);

					conector.Insert(sql, parametros);
				}

				dao_traspaso.guardar_comentario(traspaso_id,"TRASPASO GENERADO COMO PARTE DE UN RECHAZO DE UNA DEVOLUCION A MAYORISTA");

				Cambiar_cantidad cantidad = new Cambiar_cantidad(1);
				cantidad.Text = "Etiquetado de Bultos";
				cantidad.ShowDialog();
				long cantidad_bultos = cantidad.nueva_cantidad;

				filas_afectadas =  dao_traspaso.terminar_traspaso(traspaso_id,cantidad_bultos,(int)empleado_id);

				if(filas_afectadas > 0)
				{
					Traspaso impresion_ticket = new Traspaso();
					impresion_ticket.construccion_ticket(traspaso_id);
					impresion_ticket.print();
				}
			}

			return filas_afectadas;
		}

		public long set_devolucion_rechazada(long devolucion_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.devoluciones
				SET
					fecha_rechazado = NOW()
				WHERE
					devolucion_id = @devolucion_id
			";


			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("devolucion_id",devolucion_id);

			conector.Update(sql,parametros);

			return conector.filas_afectadas;
		}

		public void afectar_entradas_devolucion(long devolucion_id)
		{
			string sql = @"
				SELECT
					articulo_id,
					CAST(DATE_FORMAT(caducidad,'%Y-%m-%d') AS CHAR(50)) AS caducidad,
					lote,
					cantidad
				FROM
					farmacontrol_local.detallado_devoluciones
				WHERE
					devolucion_id = @devolucion_id
				GROUP BY
					articulo_id,caducidad,lote
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("devolucion_id", devolucion_id);

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

			DAO_Articulos dao_articulos = new DAO_Articulos();
            dao_articulos.afectar_existencias_entrada(lista_productos, "RECHAZO_DEVOLUCION_MAYORISTA", devolucion_id, devolucion_id);
		}

		public DataTable busqueda_devolucion(string solicitud_folio_devolucion, bool es_solicitud)
		{
			string sql = @"
				SELECT
					devolucion_id,
					devoluciones.entrada_id AS entrada_id,
					devoluciones.solicitud_devolucion_folio AS solicitud_devolucion_folio,
					IF(entradas.factura IS NULL, 'SIN FACTURA', entradas.factura ) AS factura,
					IF(devoluciones.entrada_id IS NULL, 'DEVOLCIÓN POR CADUCIDAD', mayoristas.nombre) AS mayorista,
					IF(devolucion_padre_id IS NULL, 0, devolucion_padre_id) AS devolucion_padre_id,
					fecha_auditada
				FROM
					farmacontrol_local.devoluciones
				LEFT JOIN farmacontrol_local.entradas USING(entrada_id)
				LEFT JOIN farmacontrol_global.mayoristas ON
					mayoristas.mayorista_id = entradas.mayorista_id
				WHERE
					devoluciones.fecha_rechazado IS NULL
				AND
					devoluciones.fecha_terminado IS NOT NULL
				AND
					devoluciones.solicitud_devolucion_folio = @solicitud_folio_devolucion
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("solicitud_folio_devolucion", solicitud_folio_devolucion);

			conector.Select(sql, parametros);

			var result = conector.result_set;

			foreach(DataRow row in result.Rows)
			{
				if(row["factura"].ToString().Length > 3)
				{
					string inicio_factura = row["factura"].ToString().Substring(0, 3);

					if (inicio_factura.Equals("SF_"))
					{
						row["factura"] = "SIN FACTURA";
					}
				}
			}

			return result;
		}

		public DataTable busqueda_devolucion(string fecha)
		{
			string sql = @"
				SELECT
					devolucion_id,
					devoluciones.entrada_id AS entrada_id,
					devoluciones.solicitud_devolucion_folio AS solicitud_devolucion_folio,
					IF(entradas.factura IS NULL, 'SIN FACTURA', entradas.factura ) AS factura,
					IF(devoluciones.entrada_id IS NULL, 'DEVOLCIÓN POR CADUCIDAD', mayoristas.nombre) AS mayorista,
					IF(devolucion_padre_id IS NULL, 0, devolucion_padre_id) AS devolucion_padre_id,
					fecha_auditada
				FROM
					farmacontrol_local.devoluciones
				LEFT JOIN farmacontrol_local.entradas USING(entrada_id)
				LEFT JOIN farmacontrol_global.mayoristas ON
					mayoristas.mayorista_id = entradas.mayorista_id
				WHERE
					devoluciones.fecha_rechazado IS NULL
				AND
					devoluciones.fecha_terminado IS NOT NULL
				AND
					DATE(devoluciones.fecha_terminado) = @fecha
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("fecha",fecha);

			conector.Select(sql,parametros);

			conector.Select(sql, parametros);

			var result = conector.result_set;

			foreach (DataRow row in result.Rows)
			{
				if(row["factura"].ToString().Length > 3)
				{
					string inicio_factura = row["factura"].ToString().Substring(0, 3);

					if (inicio_factura.Equals("SF_"))
					{
						row["factura"] = "SIN FACTURA";
					}
				}
			}

			return result;
		}

		public void eliminar_detallado_devolucion(long detallado_devolucion_id)
		{
			string sql = @"
				DELETE FROM
					farmacontrol_local.detallado_devoluciones
				WHERE
					detallado_devolucion_id = @detallado_devolucion_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("detallado_devolucion_id",detallado_devolucion_id);

			conector.Delete(sql,parametros);
		}

		public void registrar_nombre_archivo(long devolucion_id, string nombre)
		{
			string sql = @"
				INSERT INTO
					farmacontrol_local.devoluciones_scans
				SET
					devolucion_id = @devolucion_id,
					archivo = @nombre_archivo,
					fecha = NOW(),
					comentarios = 'SOLICITUD DEVOLUCION'
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("devolucion_id", devolucion_id);
			parametros.Add("nombre_archivo", nombre);

			conector.Insert(sql, parametros);
		}

		public List<Tuple<string, string, int>> get_detallado_caducidades(long devolucion_id, int articulo_id)
		{
			string sql = @"
				SELECT
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					COALESCE(cantidad,0) AS cantidad
				FROM
					farmacontrol_local.detallado_devoluciones
				WHERE
					devolucion_id = @devolucion_id
				AND
					articulo_id = @articulo_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("devolucion_id", devolucion_id);
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

		public List<DTO_Detallado_devolucion_mayorista_ticket> get_detallado_devolucion_ticket(long devolucion_id)
		{
			List<DTO_Detallado_devolucion_mayorista_ticket> lista_detallado_devolucion = new List<DTO_Detallado_devolucion_mayorista_ticket>();
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
							articulo_id = detallado_devoluciones.articulo_id 
						ORDER BY
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS amecop,
					RPAD(nombre, 37, ' ') AS nombre,
					detallado_devoluciones.precio_costo,
					SUM(detallado_devoluciones.cantidad) AS cantidad,
					SUM(detallado_devoluciones.total) AS total
				FROM
					farmacontrol_local.detallado_devoluciones
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					devolucion_id = @devolucion_id
				GROUP BY
					articulo_id
				ORDER BY detallado_devolucion_id DESC
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("devolucion_id", devolucion_id);

			conector.Select(sql, parametros);

			var result_set = conector.result_set;

			foreach (DataRow row in result_set.Rows)
			{
				DTO_Detallado_devolucion_mayorista_ticket detallado_ticket = new DTO_Detallado_devolucion_mayorista_ticket();
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
				detallado_ticket.caducidades_lotes = get_detallado_caducidades(devolucion_id, detallado_ticket.articulo_id);
				lista_detallado_devolucion.Add(detallado_ticket);
			}

			return lista_detallado_devolucion;
		}

		public DTO_Devolucion_mayorista_ticket get_devolucion_ticket(long devolucion_id)
		{
			DTO_Devolucion_mayorista_ticket devolucion_ticket = new DTO_Devolucion_mayorista_ticket();

			string sql = @"
				SELECT
					devolucion_id,
					terminal_id,
					mayorista_id,
					empleado_id,
					COALESCE(termina_empleado_id,0) AS termina_empleado_id,
					COALESCE(entrada_id,0) AS entrada_id,
					solicitud_devolucion_folio,
					solicitud_devolucion_fecha,
					fecha_creado,
					fecha_terminado,
					comentarios,
					IF(terminal_id IS NULL,'ENTRADA GENERADA POR EL SISTEMA',(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = devoluciones.empleado_id) ) AS nombre_empleado_captura,
					IF(terminal_id IS NULL,'ENTRADA GENERADA POR EL SISTEMA',(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = devoluciones.termina_empleado_id) ) AS nombre_empleado_termina
				FROM
					farmacontrol_local.devoluciones
				WHERE
					devolucion_id = @devolucion_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("devolucion_id", devolucion_id);
			conector.Select(sql, parametros);

			foreach (DataRow devolucion_row in conector.result_set.Rows)
			{
				devolucion_ticket.devolucion_id = Convert.ToInt64(devolucion_row["devolucion_id"]);
				devolucion_ticket.terminal_id = Convert.ToInt32(devolucion_row["terminal_id"]);
				devolucion_ticket.mayorista_id = Convert.ToInt64(devolucion_row["mayorista_id"]);
				devolucion_ticket.empleado_id = Convert.ToInt32(devolucion_row["empleado_id"]);
				devolucion_ticket.termina_empleado_id = Convert.ToInt32(devolucion_row["termina_empleado_id"]);
				devolucion_ticket.entrada_id = Convert.ToInt32(devolucion_row["entrada_id"]);
				devolucion_ticket.solicitud_devolucion_folio = devolucion_row["solicitud_devolucion_folio"].ToString();
				devolucion_ticket.solicitud_devolucion_fecha = devolucion_row["solicitud_devolucion_fecha"].ToString();

				DateTime? date_null = null;

				devolucion_ticket.fecha_creado = (devolucion_row["fecha_creado"].ToString().Equals("")) ? date_null : Convert.ToDateTime(devolucion_row["fecha_creado"].ToString());
				devolucion_ticket.fecha_terminado = (devolucion_row["fecha_terminado"].ToString().Equals("")) ? date_null : Convert.ToDateTime(devolucion_row["fecha_terminado"].ToString());

				devolucion_ticket.comentarios = devolucion_row["comentarios"].ToString();
				devolucion_ticket.nombre_empleado_captura = devolucion_row["nombre_empleado_captura"].ToString();
				devolucion_ticket.nombre_empleado_termina = devolucion_row["nombre_empleado_termina"].ToString();
			}

			return devolucion_ticket;
		}

		public void set_comentario(long devolucion_id, string comentario)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.devoluciones
				SET
					comentarios = @comentario
				WHERE
					devolucion_id = @devolucion_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("comentario",comentario);
			parametros.Add("devolucion_id",devolucion_id);

			conector.Update(sql,parametros);
		}

		public void update_motivo_detallado(long detallado_devolucion_id, string motivo)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.detallado_devoluciones
				SET
					motivo = @motivo
				WHERE
					detallado_devolucion_id = @detallado_devolucion_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("motivo",motivo);
			parametros.Add("detallado_devolucion_id",detallado_devolucion_id);

			conector.Update(sql,parametros);
		}

		public void update_cantidad_detallado(long detallado_devolucion_id, int cantidad)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.detallado_devoluciones
				SET
					cantidad = @cantidad,
					subtotal = (importe * @cantidad),
					importe_iva = ( (importe * @cantidad) * pct_iva ),
					total = ( ( (importe * @cantidad) * pct_iva ) + (importe * @cantidad) )
				WHERE
					detallado_devolucion_id = @detallado_devolucion_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("cantidad",cantidad);
			parametros.Add("detallado_devolucion_id",detallado_devolucion_id);

			conector.Update(sql,parametros);
		}

		public DataTable get_detallado_devoluciones(long devolucion_id)
		{
			string sql = @"
				SELECT
					detallado_devoluciones.detallado_devolucion_id,
					detallado_devoluciones.articulo_id AS articulo_id,
					(
						SELECT 
							amecop
						FROM 
							farmacontrol_global.articulos_amecops 
						WHERE 
							articulo_id = detallado_devoluciones.articulo_id 
						ORDER BY
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS amecop,
					nombre AS producto,
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					detallado_devoluciones.precio_costo AS precio_costo,
					detallado_devoluciones.pct_descuento AS pct_descuento,
					detallado_devoluciones.importe_descuento AS importe_descuento,
					detallado_devoluciones.importe AS importe,
					detallado_devoluciones.cantidad AS cantidad,
					detallado_devoluciones.subtotal AS subtotal,
					detallado_devoluciones.pct_iva AS pct_iva,
					detallado_devoluciones.importe_iva AS importe_iva,
					detallado_devoluciones.total AS total,
					0 AS cantidad_devoluciones,
					0 AS cantidad_vendible,
					0 AS cantidad_terminadas,
					detallado_devoluciones.cantidad AS cantidad_actual,
					detallado_devoluciones.motivo AS motivo_actual,
					detallado_devoluciones.cantidad AS cantidad_entradas
				FROM
					farmacontrol_local.detallado_devoluciones
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					devolucion_id = @devolucion_id
				GROUP BY
					detallado_devoluciones.detallado_devolucion_id
				ORDER BY
					detallado_devolucion_id
				DESC
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("devolucion_id", devolucion_id);

			conector.Select(sql, parametros);

			var result_set = conector.result_set;

			if (result_set.Rows.Count > 0)
			{
				foreach (DataRow row in result_set.Rows)
				{
                    row["caducidad"] = Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");
				}
			}

			return conector.result_set;
		}

		public DataTable get_detallado_devoluciones(long devolucion_id, long entrada_id)
		{
			string sql = @"
				SELECT
					detallado_devoluciones.detallado_devolucion_id,
					detallado_devoluciones.articulo_id AS articulo_id,
					(
						SELECT 
							amecop
						FROM 
							farmacontrol_global.articulos_amecops 
						WHERE 
							articulo_id = detallado_devoluciones.articulo_id 
						ORDER BY
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS amecop,
					nombre AS producto,
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					detallado_devoluciones.precio_costo AS precio_costo,
					detallado_devoluciones.pct_descuento AS pct_descuento,
					detallado_devoluciones.importe_descuento AS importe_descuento,
					detallado_devoluciones.importe AS importe,
					detallado_devoluciones.cantidad AS cantidad,
					detallado_devoluciones.subtotal AS subtotal,
					detallado_devoluciones.pct_iva AS pct_iva,
					detallado_devoluciones.importe_iva AS importe_iva,
					detallado_devoluciones.total AS total,
					COALESCE(tmp_dev.cantidad,0) AS cantidad_devoluciones,
					COALESCE(tmp_vendible.cantidad, 0) AS cantidad_vendible,
					COALESCE(tmp_dev_terminadas.cantidad,0) AS cantidad_terminadas,
					COALESCE(tmp_devolucion_actual.cantidad,0) AS cantidad_actual,
					tmp_devolucion_actual.motivo AS motivo_actual,
					tmp_entradas.cantidad AS cantidad_entradas
				FROM
					farmacontrol_local.detallado_devoluciones
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id)
				LEFT JOIN
					(
						SELECT
							articulo_id,
							caducidad,
							lote,
							cantidad
						FROM
							farmacontrol_local.detallado_entradas
						WHERE
							entrada_id = @entrada_id
					) AS tmp_entradas USING(articulo_id,caducidad,lote)
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
							articulo_id,
							caducidad,
							lote,
							COALESCE(SUM(detallado_devoluciones.cantidad),0) AS cantidad
						FROM
							farmacontrol_local.detallado_entradas
						LEFT JOIN farmacontrol_local.devoluciones USING(entrada_id)
						LEFT JOIN farmacontrol_local.detallado_devoluciones USING(devolucion_id,articulo_id,caducidad,lote)
						WHERE
							entrada_id = @entrada_id
						AND
							devoluciones.fecha_terminado IS NOT NULL
						AND
							devoluciones.fecha_rechazado IS NOT NULL
						GROUP BY
							detallado_devoluciones.articulo_id, detallado_devoluciones.caducidad, detallado_devoluciones.lote
					) AS tmp_dev_terminadas USING(articulo_id,caducidad,lote)
				LEFT JOIN 
					(
						SELECT
							articulo_id,
							caducidad,
							lote,
							COALESCE(SUM(detallado_devoluciones.cantidad),0) AS cantidad
						FROM
							farmacontrol_local.devoluciones
						LEFT JOIN farmacontrol_local.detallado_devoluciones USING(devolucion_id)
						WHERE
							devoluciones.entrada_id = @entrada_id
						AND
							devoluciones.fecha_terminado IS NULL
						GROUP BY
							detallado_devoluciones.articulo_id, detallado_devoluciones.caducidad, detallado_devoluciones.lote
					) AS tmp_dev USING(articulo_id,caducidad,lote)
				LEFT JOIN 
					(
						SELECT
							detallado_devolucion_id,
							COALESCE(SUM(existencia), 0) - COALESCE(existencia_mermas, 0) - COALESCE(existencia_devoluciones, 0) - COALESCE(existencia_apartados, 0) - COALESCE(existencia_cambio_fisico, 0) - COALESCE(existencia_ventas, 0) - COALESCE(existencia_traspasos, 0)AS cantidad
						FROM
							farmacontrol_local.detallado_devoluciones
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
							detallado_devoluciones.devolucion_id = @devolucion_id
						GROUP BY 
							detallado_devoluciones.articulo_id, detallado_devoluciones.caducidad, detallado_devoluciones.lote
		
					) AS tmp_vendible USING(detallado_devolucion_id)
				WHERE
					devolucion_id = @devolucion_id
				GROUP BY
					detallado_devoluciones.detallado_devolucion_id
				ORDER BY
					detallado_devolucion_id
				DESC
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("devolucion_id",devolucion_id);
			parametros.Add("entrada_id",entrada_id);

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

		public void vaciar_detallado_devoluciones(long devolucion_id)
		{
			string sql = @"
				DELETE FROM
					farmacontrol_local.detallado_devoluciones
				WHERE
					devolucion_id = @devolucion_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("devolucion_id",devolucion_id);

			conector.Delete(sql,parametros);
		}

		public void rellenar_detallado_devolucion(long devolucion_id, long entrada_id)
		{
			vaciar_detallado_devoluciones(devolucion_id);
			
			string sql = @"
				INSERT INTO
					farmacontrol_local.detallado_devoluciones
				(
					SELECT
						0 AS detallado_devolucion_id,
						@devolucion_id AS devolucion_id,
						articulo_id,
						caducidad,
						lote,
						precio_costo,
						pct_descuento,
						importe_descuento,
						importe,
						0 AS cantidad,
						(0 * importe) AS subtotal,
						pct_iva,
						( (0 * importe) * pct_iva ) AS importe_iva,
						( (0 * importe) + ( (1 * importe) * pct_iva ) ) AS total,
						NULL AS motivo,
						NOW() AS modified
					FROM
						farmacontrol_local.detallado_entradas
					WHERE
						entrada_id = @entrada_id
					ORDER BY
						detallado_entrada_id
					DESC
				)
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("devolucion_id",devolucion_id);
			parametros.Add("entrada_id",entrada_id);

			conector.Insert(sql,parametros);
		}

		public int terminar_devolucion(long devolucion_id, long empleado_id, string fecha, string folio_devolucion)
		{
			string sql = @"
				DELETE FROM
					farmacontrol_local.detallado_devoluciones
				WHERE
					cantidad = 0
				AND
					devolucion_id = @devolucion_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("devolucion_id", devolucion_id);

			conector.Delete(sql, parametros);

			sql = @"
				SELECT
					articulo_id,
                    CAST(DATE_FORMAT(caducidad,'%Y-%m-%d') AS CHAR(50)) AS caducidad,
					lote,
					SUM(cantidad) AS cantidad
				FROM
					farmacontrol_local.detallado_devoluciones
				WHERE
					devolucion_id = @devolucion_id
				GROUP BY
					articulo_id,caducidad,lote
			";

			conector.Select(sql, parametros);

			var detallado_venta = conector.result_set;

			List<Tuple<int, string, string, int>> productos = new List<Tuple<int, string, string, int>>();

            //ESTO ES NUEVO
            bool bExistencia = true;
            
            
			foreach (DataRow row in detallado_venta.Rows)
			{
                parametros = new Dictionary<string, object>();

				int articulo_id = Convert.ToInt32(row["articulo_id"]);
				string caducidad = row["caducidad"].ToString();
                //string caducidad = Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");
				string lote = row["lote"].ToString();
				int cantidad = Convert.ToInt32(row["cantidad"]);

                //AQUI HABRIA QUE CHECAR LA EXISTENCIA ACTUAL AL SISTEMA, SI ES IGUAL AL LOTE Y CADUCIDAD , CONTINUA SI NO, SE TERMINA EL PROCESO.
                //ESTO ES NUEVO
                sql = @"                    SELECT	                    articulo_id,	                    lote,	                     CAST(DATE_FORMAT(caducidad,'%Y-%m-%d') AS CHAR(50)) AS caducidad,	                    SUM( existencia ) as existencia                    FROM                       farmacontrol_local.existencias                    WHERE	                    articulo_id = @articulo_id                    AND	                    lote = @lote                    AND	                    caducidad = @caducidad                    GROUP BY                         articulo_id,lote,caducidad
			    ";

                parametros.Add("lote", lote);
                parametros.Add("caducidad", caducidad);
                parametros.Add("articulo_id", articulo_id);


                conector.Select(sql, parametros);

                var resultado_existencia = conector.result_set;

                if(resultado_existencia.Rows.Count <= 0)
                {
                    bExistencia = false;
                }
                //FIN DE LO NUEVO
				Tuple<int, string, string, int> detallado = new Tuple<int, string, string, int>(articulo_id, caducidad, lote, cantidad);
				productos.Add(detallado);
			}

            //Esto es nuevo 
            if (bExistencia == false)
            {
                return 0; 
            }


			
			sql = @"
				UPDATE
					farmacontrol_local.devoluciones
				SET
					fecha_terminado = NOW(),
					termina_empleado_id = @empleado_id,
					solicitud_devolucion_folio = @folio_devolucion,
					solicitud_devolucion_fecha = CURDATE(),
					fecha_afectado = NOW()
				WHERE
					devolucion_id = @devolucion_id
			";

			parametros = new Dictionary<string,object>();
			parametros.Add("devolucion_id",devolucion_id);
			parametros.Add("empleado_id",empleado_id);
			parametros.Add("folio_devolucion",folio_devolucion);
			parametros.Add("fecha",(fecha.Equals("") ? null : fecha));

			conector.Update(sql,parametros);

			var filas_afectadas = conector.filas_afectadas;

			DAO_Articulos dao_articulos = new DAO_Articulos();
            dao_articulos.afectar_existencias_salida(productos, "DEVOLUCION_MAYORISTA", devolucion_id, devolucion_id);
			

			return filas_afectadas;
		}

		public void registrar_detallado_devolucion(long devolucion_id, long articulo_id, string caducidad, string lote, int cantidad)
		{
			string sql = @"
				INSERT INTO
					farmacontrol_local.detallado_devoluciones
					(
						SELECT
							0 AS detallado_devolucion_id,
							@devolucion_id AS devolucion_id,
							tmp.articulo_id AS articulo_id,
							@caducidad AS caducidad,
							@lote AS lote,
							tmp.precio_costo AS precio_costo,
							tmp.pct_descuento AS pct_descuento,
							FORMAT((tmp.precio_costo * tmp.pct_descuento), 4) AS importe_descuento,
							tmp.precio_costo - (tmp.precio_costo * tmp.pct_descuento) AS importe,
							@cantidad AS cantidad,
							(tmp.precio_costo * @cantidad) AS subtotal,
							tmp.pct_iva AS pct_iva,
							((tmp.precio_costo * @cantidad) * tmp.pct_iva) AS importe_iva,
							(tmp.precio_costo * @cantidad) + ((tmp.precio_costo * @cantidad) * tmp.pct_iva) AS total,
							'CADUCIDAD' AS motivo,
							NOW() AS modified
						FROM
							farmacontrol_global.articulos AS tmp
						WHERE
							articulo_id = @articulo_id
					)
				ON DUPLICATE KEY UPDATE
					cantidad = cantidad + @cantidad,
					subtotal = subtotal + (tmp.precio_costo * @cantidad),
					importe_iva = importe_iva + ((tmp.precio_costo * @cantidad) * tmp.pct_iva),
					total = total + (tmp.precio_costo * @cantidad) + ((tmp.precio_costo * @cantidad) * tmp.pct_iva)
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("devolucion_id",devolucion_id);
			parametros.Add("articulo_id",articulo_id);
			parametros.Add("caducidad",caducidad);
			parametros.Add("lote",lote);
			parametros.Add("cantidad",cantidad);

			conector.Insert(sql,parametros);
		}

		public long registrar_devolucion(long empleado_id, long mayorista_id)
		{
			int terminal_id = (int)Misc_helper.get_terminal_id();

			string sql = @"
				INSERT INTO
					farmacontrol_local.devoluciones
				SET
					terminal_id = @terminal_id,
					mayorista_id = @mayorista_id,
					empleado_id = @empleado_id,
					entrada_id = NULL,
					solicitud_devolucion_folio = '',
					fecha_creado = NOW(),
					nota_credito_importe = 0,
					comentarios = ''
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("terminal_id",terminal_id);
			parametros.Add("mayorista_id",mayorista_id);
			parametros.Add("empleado_id",empleado_id);

			conector.Insert(sql,parametros);

			long insert_id = conector.insert_id;

			return insert_id;
		}

		public int set_entrada_id(long devolucion_id, long entrada_id)
		{
			string sql = "";

			if(entrada_id > 0)
			{
				sql = @"
					UPDATE
						farmacontrol_local.devoluciones
					SET
						entrada_id = @entrada_id
					WHERE
						devolucion_id = @devolucion_id
				";
			}
			else
			{
				sql = @"
					UPDATE
						farmacontrol_local.devoluciones
					SET
						entrada_id = NULL
					WHERE
						devolucion_id = @devolucion_id
				";	
			}

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("devolucion_id", devolucion_id);
			parametros.Add("entrada_id", entrada_id);

			conector.Update(sql, parametros);

			int filas_afectadas = conector.filas_afectadas;

			if(entrada_id > 0)
			{
				rellenar_detallado_devolucion(devolucion_id, entrada_id);
			}
			else
			{
				vaciar_detallado_devoluciones(devolucion_id);
			}

			return filas_afectadas;
		}

		public int set_mayorista_id(long devolucion_id, long mayorista_id)
		{
			string sql ="";

			if(mayorista_id > 0)
			{
				sql = @"
					UPDATE
						farmacontrol_local.devoluciones
					SET
						mayorista_id = @mayorista_id,
						entrada_id = NULL
					WHERE
						devolucion_id = @devolucion_id
				";	
			}
			else
			{
				sql = @"
					UPDATE
						farmacontrol_local.devoluciones
					SET
						mayorista_id = NULL,
						entrada_id = NULL
					WHERE
						devolucion_id = @devolucion_id
				";
			}

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("devolucion_id",devolucion_id);
			parametros.Add("mayorista_id",mayorista_id);

			conector.Update(sql,parametros);

			vaciar_detallado_devoluciones(devolucion_id);

			return conector.filas_afectadas;
		}

		public DTO_Devoluciones get_informacion_devoluciones(long devolucion_id)
		{
			DTO_Devoluciones dto_devoluciones = new DTO_Devoluciones();

			try
			{
				string sql = @"
				SELECT
					devolucion_id,
					IF(devoluciones.terminal_id IS NULL,0,devoluciones.terminal_id) AS terminal_id,
					devoluciones.mayorista_id,
					devoluciones.empleado_id,
					IF(devoluciones.termina_empleado_id IS NULL,0,devoluciones.termina_empleado_id) AS termina_empleado_id,
					IF(entrada_id IS NULL,0,entrada_id) AS entrada_id,
					solicitud_devolucion_folio,
					solicitud_devolucion_fecha,
					solicitud_devolucion_fecha AS solicitud_devolucion_fecha_formato,
					devoluciones.fecha_creado AS fecha_creado,
					devoluciones.fecha_terminado AS fecha_terminado,
					fecha_afectado,
					fecha_auditada,
					IF(nota_credito_folio IS NULL, '',nota_credito_folio) AS nota_credito_folio,
					CAST(DATE_FORMAT(nota_credito_fecha,'%Y-%m-%d %h:%i:%s') AS CHAR(50)) AS nota_credito_fecha,
					nota_credito_importe,
					devoluciones.comentarios,
					(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = devoluciones.empleado_id) AS nombre_empleado_captura,
					IF(devoluciones.termina_empleado_id IS NULL,'',(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = devoluciones.termina_empleado_id) ) AS nombre_empleado_termina
				FROM
					farmacontrol_local.devoluciones
				WHERE
					devolucion_id = @devolucion_id
			";

				Dictionary<string, object> parametros = new Dictionary<string, object>();
				parametros.Add("devolucion_id", devolucion_id);

				conector.Select(sql, parametros);

				var result = conector.result_set;

				if (result.Rows.Count > 0)
				{
					var row = result.Rows[0];

					dto_devoluciones.devolucion_id = Convert.ToInt64(row["devolucion_id"]);
					dto_devoluciones.terminal_id = Convert.ToInt32(row["terminal_id"]);
					dto_devoluciones.mayorista_id = Convert.ToInt64(row["mayorista_id"]);
					dto_devoluciones.empleado_id = Convert.ToInt64(row["empleado_id"]);
					dto_devoluciones.termina_empleado_id = Convert.ToInt64(row["termina_empleado_id"]);
					dto_devoluciones.entrada_id = Convert.ToInt64(row["entrada_id"]);
					dto_devoluciones.solicitud_devolucion_folio = row["solicitud_devolucion_folio"].ToString();

					DateTime? date_null = null;

					dto_devoluciones.solicitud_devolucion_fecha_formato = (row["solicitud_devolucion_fecha_formato"].ToString().Equals("")) ? date_null : Convert.ToDateTime(row["solicitud_devolucion_fecha_formato"]);
					dto_devoluciones.solicitud_devolucion_fecha = (row["solicitud_devolucion_fecha"].ToString().Equals("")) ? date_null : Convert.ToDateTime(row["solicitud_devolucion_fecha"]) ;
					dto_devoluciones.fecha_creado = (row["fecha_creado"].ToString().Equals("")) ? date_null: Convert.ToDateTime(row["fecha_creado"].ToString());
					dto_devoluciones.fecha_terminado = (row["fecha_terminado"].ToString().Equals("")) ? date_null: Convert.ToDateTime(row["fecha_terminado"].ToString());
					dto_devoluciones.fecha_afectado = (row["fecha_afectado"].ToString().Equals("")) ? date_null: Convert.ToDateTime(row["fecha_afectado"].ToString());
					dto_devoluciones.fecha_auditada = (row["fecha_auditada"].ToString().Equals("")) ? date_null : Convert.ToDateTime(row["fecha_auditada"].ToString()); 

					dto_devoluciones.nota_credito_folio = row["nota_credito_folio"].ToString();
					dto_devoluciones.nota_credito_fecha = row["nota_credito_fecha"].ToString();
					dto_devoluciones.nota_credito_importe = Convert.ToDecimal(row["nota_credito_importe"]);
					dto_devoluciones.comentarios = row["comentarios"].ToString();
					dto_devoluciones.nombre_empleado_captura = row["nombre_empleado_captura"].ToString();
					dto_devoluciones.nombre_empleado_termina = row["nombre_empleado_termina"].ToString();
				}
				else
				{
					dto_devoluciones.devolucion_id = 0;
				}
			}
			catch(Exception ex)
			{
				Log_error.log(ex);
			}
			
			return dto_devoluciones;	
		}

		public long get_devolucion_siguiente(long devolucion_id)
		{
			string sql = @"
				SELECT
					devolucion_id
				FROM
					farmacontrol_local.devoluciones
				WHERE
					devolucion_id > @devolucion_id
				ORDER BY
					devolucion_id
				ASC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("devolucion_id", devolucion_id);

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["devolucion_id"]);
			}

			return 0;
		}

		public long get_devolucion_atras(long devolucion_id)
		{
			string sql = @"
				SELECT
					devolucion_id
				FROM
					farmacontrol_local.devoluciones
				WHERE
					devolucion_id < @devolucion_id
				ORDER BY
					devolucion_id
				DESC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("devolucion_id", devolucion_id);

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["devolucion_id"]);
			}

			return 0;
		}

		public long get_devolucion_inicio()
		{
			string sql = @"
				SELECT
					devolucion_id
				FROM
					farmacontrol_local.devoluciones
				ORDER BY
					devolucion_id
				ASC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["devolucion_id"]);
			}

			return 0;
		}

		public long get_devolucion_fin()
		{
			string sql = @"
				SELECT
					devolucion_id
				FROM
					farmacontrol_local.devoluciones
				ORDER BY
					devolucion_id
				DESC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["devolucion_id"]);
			}

			return 0;
		}
	}
}
