using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Windows.Forms;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Ajustes_existencias
	{
		Conector conector = new Conector();



        public static void set_materia_prima(List<DTO_Materia_Prima> lista)
        {
            try
            {
                Conector conector = new Conector();

                foreach (var item in lista)
                {
                    string sql = @"
                INSERT INTO farmacontrol_local.materias_primas (
                    articulo_id,
                    observaciones,
                    fecha_modificado,
                    volumen,
					unidad,
					existencia_actual
                )
                VALUES (
                    @articulo_id,
                    @observaciones,
                    NOW(),
                    @volumen,
					@unidad,
					@existencia_actual
                )
                ON DUPLICATE KEY UPDATE
                    observaciones = VALUES(observaciones),
                     volumen = farmacontrol_local.materias_primas.volumen + @volumen,
                    fecha_modificado = NOW(),
					existencia_actual = farmacontrol_local.materias_primas.existencia_actual + @existencia_actual;
            ";

                    Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "articulo_id", item.articulo_id },
                { "observaciones", item.observaciones ?? "" },
                { "volumen", item.volumen },
                { "unidad", item.cantidad },
                 { "existencia_actual", item.existencia_actual }
            };

                    conector.Non_Query(sql, parametros);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar materias primas:\n\n" + ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                Console.WriteLine("ERROR COMPLETO:\n" + ex.ToString());
            }
        }


        public void descontar_volumen_materias_primas(List<Tuple<int, decimal>> articulos_volumen)
        {
            foreach (var item in articulos_volumen)
            {
                int articulo_id = item.Item1;
                decimal volumen_a_descontar = item.Item2;

                string sql_select = @"
            SELECT * FROM farmacontrol_local.materias_primas
            WHERE articulo_id = @articulo_id
            LIMIT 1
        ";

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("articulo_id", articulo_id);
                conector.Select(sql_select, parametros);

                if (conector.result_set.Rows.Count > 0)
                {
                    DataRow row = conector.result_set.Rows[0];
                    decimal volumen_actual = Convert.ToDecimal(row["volumen"]);
                    decimal unidad = Convert.ToDecimal(row["unidad"]); // volumen por unidad
                    int existencia_actual = Convert.ToInt32(row["existencia_actual"]);
                    int materia_prima_id = Convert.ToInt32(row["materia_prima_id"]);

                    // Calcular nuevo volumen
                    decimal nuevo_volumen = volumen_actual - volumen_a_descontar;

                    if (nuevo_volumen <= 0)
                    {
                        // Eliminar si ya no queda volumen
                        string delete_sql = "DELETE FROM farmacontrol_local.materias_primas WHERE materia_prima_id = @id";
                        Dictionary<string, object> delete_params = new Dictionary<string, object>();
                        delete_params.Add("id", materia_prima_id);
                        conector.Delete(delete_sql, delete_params);
                    }
                    else
                    {
                        // Calcular cuántas unidades deben restarse de la existencia según el volumen usado
                        decimal volumen_usado = volumen_actual - nuevo_volumen;
                        int unidades_a_restar = (int)Math.Floor(volumen_usado / unidad);
                        int nueva_existencia = existencia_actual - unidades_a_restar;

                        if (nueva_existencia < 0)
                            nueva_existencia = 0;

                        string update_sql = @"
                    UPDATE farmacontrol_local.materias_primas
                    SET volumen = @volumen, existencia_actual = @existencia
                    WHERE materia_prima_id = @id
                ";

                        Dictionary<string, object> update_params = new Dictionary<string, object>();
                        update_params.Add("volumen", nuevo_volumen);
                        update_params.Add("existencia", nueva_existencia);
                        update_params.Add("id", materia_prima_id);

                       /* MessageBox.Show(
                            $"DEBUG >> Volumen actual: {volumen_actual}\n" +
                            $"Descontar: {volumen_a_descontar}\n" +
                            $"Nuevo volumen: {nuevo_volumen}\n" +
                            $"Unidad: {unidad}\n" +
                            $"Volumen usado: {volumen_usado}\n" +
                            $"Unidades a restar: {unidades_a_restar}\n" +
                            $"Existencia actual: {existencia_actual}\n" +
                            $"Nueva existencia: {nueva_existencia}"
                        );*/

                        conector.Update(update_sql, update_params);
                    }
                }
            }
        }



        public void set_comentario(long ajuste_existencia_id, string comentario)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.ajustes_existencias
				SET
					comentarios = @comentario
				WHERE
					ajuste_existencia_id = @ajuste_existencia_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("ajuste_existencia_id", ajuste_existencia_id);
			parametros.Add("comentario",comentario);
			
			conector.Update(sql,parametros);
		}

		public List<Tuple<string, string, int, int, int>> get_detallado_caducidades(long ajuste_existencia_id, int articulo_id)
		{
            //string sql = "farmacontrol_global.ajusteExistencias_get_detallado_caducidades";
            string sql = @"
				SELECT
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					existencia_anterior,
					cantidad,
					diferencia
				FROM
					farmacontrol_local.detallado_ajustes_existencias
				WHERE
					ajuste_existencia_id = @ajuste_existencia_id
				AND
					articulo_id = @articulo_id
			";
             

			Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("ajuste_existencia_id", ajuste_existencia_id);
            parametros.Add("articulo_id", articulo_id);

			conector.Select(sql, parametros);
           //conector.Call(sql, parametros);

			List<Tuple<string, string, int, int, int>> lista_caducidades = new List<Tuple<string, string, int, int, int>>();

			foreach (DataRow row in conector.result_set.Rows)
			{
				Tuple<string, string, int, int, int> tupla = new Tuple<string, string, int, int, int>(row["caducidad"].ToString(), row["lote"].ToString(), Convert.ToInt32(row["existencia_anterior"]), Convert.ToInt32(row["cantidad"]), Convert.ToInt32(row["diferencia"]));
				lista_caducidades.Add(tupla);
			}

			return lista_caducidades;
		}

		public List<DTO_Detallado_ajuste_ticket> get_detallado_ajuste_ticket(long ajuste_existencia_id)
		{
            //string sql = "farmacontrol_global.ajusteExistencias_get_detallado_ajuste_ticket";
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
							articulo_id = detallado_ajustes_existencias.articulo_id
						ORDER BY
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS amecop,
					RPAD(nombre, 50, ' ') AS nombre
				FROM
					farmacontrol_local.detallado_ajustes_existencias
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					ajuste_existencia_id = @ajuste_existencia_id
				GROUP BY
					articulo_id
				ORDER BY detallado_ajuste_existencia_id ASC
			";
             

			Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("ajuste_existencia_id", ajuste_existencia_id);

            conector.Select(sql, parametros);
            //conector.Call(sql, parametros);

			var result_set = conector.result_set;

			List<DTO_Detallado_ajuste_ticket> lista_detallado_ajuste = new List<DTO_Detallado_ajuste_ticket>();

			foreach (DataRow row in result_set.Rows)
			{
				DTO_Detallado_ajuste_ticket detallado_ticket = new DTO_Detallado_ajuste_ticket();
				detallado_ticket.articulo_id = Convert.ToInt32(row["articulo_id"]);

                string var_temp_amecop = row["amecop"].ToString();
                int tam_var = var_temp_amecop.Length;
                String Var_Sub = "*" + var_temp_amecop.Substring((tam_var-3), 3);
                string amecop_temp = Var_Sub.PadRight(5, ' ');
                detallado_ticket.amecop = amecop_temp;

                //detallado_ticket.amecop=row["amecop"].ToString();
				detallado_ticket.nombre = row["nombre"].ToString();
				detallado_ticket.caducidades_lotes = get_detallado_caducidades(ajuste_existencia_id, detallado_ticket.articulo_id);
				lista_detallado_ajuste.Add(detallado_ticket);
			}

			return lista_detallado_ajuste;
		}

		public DataTable eliminar_producto_detallado_ajuste_existencia(long ajuste_existencia_id, long detallado_ajuste_existencia_id)
		{
			string sql = @"
				DELETE FROM
					farmacontrol_local.detallado_ajustes_existencias
				WHERE
					detallado_ajuste_existencia_id = @detallado_ajuste_existencia_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("detallado_ajuste_existencia_id",detallado_ajuste_existencia_id);

			conector.Delete(sql,parametros);

			return get_detallado_ajustes_axistencias(ajuste_existencia_id);
		}

		public DTO_Ajustes_existencias get_informacion_ajuste_existencia(long ajuste_existencia_id)
		{
			DTO_Ajustes_existencias dto_ajuste_existencia = new DTO_Ajustes_existencias();

			try
			{
				string sql = @"
				SELECT
					ajuste_existencia_id,
					terminal_id,
					empleado_id,
					termina_empleado_id,
					fecha_creado AS fecha_creado,
					fecha_terminado AS fecha_terminado,
					comentarios,
					IF(empleado_id IS NULL,'TRASPASO GENERADO POR EL SISTEMA',(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = ajustes_existencias.empleado_id) ) AS nombre_empleado_captura,
					IF(empleado_id IS NULL,'TRASPASO GENERADO POR EL SISTEMA',(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = ajustes_existencias.termina_empleado_id) ) AS nombre_empleado_termina
				FROM
					farmacontrol_local.ajustes_existencias
				WHERE
					ajuste_existencia_id = @ajuste_existencia_id
			";
                

               // string sql = "farmacontrol_global.AjusteExistencias_get_info_ajuste_existencia";

				Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("ajuste_existencia_id", ajuste_existencia_id);

				conector.Select(sql, parametros);
                //conector.Call(sql, parametros);

				var ajuste_data = conector.result_set;

				if (ajuste_data.Rows.Count > 0)
				{
					var ajuste_row = ajuste_data.Rows[0];
					int? nullable = null;

					dto_ajuste_existencia.ajuste_existencia_id = Convert.ToInt32(ajuste_row["ajuste_existencia_id"]);
					dto_ajuste_existencia.terminal_id = Convert.ToInt32(ajuste_row["terminal_id"]);
					dto_ajuste_existencia.empleado_id = Convert.ToInt32(ajuste_row["empleado_id"]);
					dto_ajuste_existencia.termina_empleado_id = (ajuste_row["termina_empleado_id"].ToString() != "") ? Convert.ToInt32(ajuste_row["termina_empleado_id"]) : nullable;

					DateTime? date_null = null;

					dto_ajuste_existencia.fecha_creado = (ajuste_row["fecha_creado"].ToString().Equals("")) ? date_null : Convert.ToDateTime(ajuste_row["fecha_creado"]);
					dto_ajuste_existencia.fecha_terminado = (ajuste_row["fecha_terminado"].ToString().Equals("")) ? date_null :Convert.ToDateTime(ajuste_row["fecha_terminado"]);

					dto_ajuste_existencia.comentarios = ajuste_row["comentarios"].ToString();
					dto_ajuste_existencia.nombre_empleado_captura = ajuste_row["nombre_empleado_captura"].ToString();
					dto_ajuste_existencia.nombre_empleado_termina = ajuste_row["nombre_empleado_termina"].ToString();
				}
				else
				{
					dto_ajuste_existencia.ajuste_existencia_id = 0;
				}
			}
			catch(Exception e)
			{
				Log_error.log(e);
			}

			return dto_ajuste_existencia;
		}

		public long get_ajuste_existencia_siguiente(long ajuste_existencia_id)
		{
           // string sql = "farmacontrol_global.ajusteExistencias_get_ajuste_existencia_siguiente";
            
			string sql = @"
				SELECT
					ajuste_existencia_id
				FROM
					farmacontrol_local.ajustes_existencias
				WHERE
					ajuste_existencia_id > @ajuste_existencia_id
				ORDER BY
					ajuste_existencia_id
				ASC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("ajuste_existencia_id", ajuste_existencia_id);

		    conector.Select(sql, parametros);
            //conector.Call(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["ajuste_existencia_id"]);
			}

			return 0;
		}

		public long get_ajuste_existencia_atras(long ajuste_existencia_id)
		{
            //string sql = "farmacontrol_global.ajusteExistencias_get_ajuste_existencia_atras";
			string sql = @"
				SELECT
					ajuste_existencia_id
				FROM
					farmacontrol_local.ajustes_existencias
				WHERE
					ajuste_existencia_id < @ajuste_existencia_id
				ORDER BY
					ajuste_existencia_id
				DESC
				LIMIT 1
			";
            
			Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("ajuste_existencia_id", ajuste_existencia_id);

			conector.Select(sql, parametros);
            //conector.Call(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["ajuste_existencia_id"]);
			}

			return 0;
		}

		public long get_ajuste_existencia_inicio()
		{
            //string sql = "farmacontrol_global.ajusteExistencias_get_ajuste_existencia_inicio";
            
			string sql = @"
				SELECT
					ajuste_existencia_id
				FROM
					farmacontrol_local.ajustes_existencias
				ORDER BY
					ajuste_existencia_id
				ASC
				LIMIT 1
			";
            
			Dictionary<string, object> parametros = new Dictionary<string, object>();

			conector.Select(sql, parametros);
           //conector.Call(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["ajuste_existencia_id"]);
			}

			return 0;
		}

		public long get_ajuste_existencia_fin()
		{
            //string sql = "farmacontrol_global.ajusteExistencias_get_ajuste_existencia_fin";
            
			string sql = @"
				SELECT
					ajuste_existencia_id
				FROM
					farmacontrol_local.ajustes_existencias
				ORDER BY
					ajuste_existencia_id
				DESC
				LIMIT 1
			";
            
			Dictionary<string, object> parametros = new Dictionary<string, object>();

			conector.Select(sql, parametros);
            //conector.Call(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["ajuste_existencia_id"]);
			}

			return 0;
		}

		public void ajustar_existencias(long ajuste_existencia_id)
		{
            try
            {
                //string sql = "farmacontrol_global.ajusteExistencias_ajustar_existencias";
                
                string sql = @"
                    SELECT
					    articulo_id,
					    CAST(caducidad AS CHAR(10)) AS caducidad,
					    lote,
					    cantidad,
                        diferencia
				    FROM
					    farmacontrol_local.detallado_ajustes_existencias
				    WHERE
					    ajuste_existencia_id = @ajuste_existencia_id
                ";
                
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("ajuste_existencia_id", ajuste_existencia_id);

                conector.Select(sql, parametros);
                //conector.Call(sql, parametros);

                if (conector.result_set.Rows.Count > 0)
                {
                    List<Tuple<int, string, string, int>> lista_agrega = new List<Tuple<int, string, string, int>>();
                    List<Tuple<int, string, string, int>> lista_descuenta = new List<Tuple<int, string, string, int>>();

                    foreach (DataRow row in conector.result_set.Rows)
                    {
                        Tuple<int, string, string, int> tupla_articulo = new Tuple<int, string, string, int>(
                            Convert.ToInt32(row["articulo_id"]),
                            row["caducidad"].ToString(),
                            row["lote"].ToString(),
                            Math.Abs(Convert.ToInt32(row["diferencia"]))
                        );

                        if (Convert.ToInt32(row["diferencia"]) > 0)
                        {
                            lista_agrega.Add(tupla_articulo);
                        }
                        else
                        {
                            lista_descuenta.Add(tupla_articulo);
                        }
                    }

                    DAO_Articulos dao_articulos = new DAO_Articulos();
                    dao_articulos.afectar_existencias_salida(lista_descuenta, "AJUSTE_EXISTENCIA", ajuste_existencia_id, ajuste_existencia_id);
                    dao_articulos.afectar_existencias_entrada(lista_agrega, "AJUSTE_EXISTENCIA", ajuste_existencia_id, ajuste_existencia_id);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
		}

		public long terminar_ajustes_existencias(long ajuste_existencia_id, long empleado_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.ajustes_existencias
				SET
					fecha_terminado = NOW(),
					termina_empleado_id = @empleado_id
                WHERE
                    ajuste_existencia_id = @ajuste_existencia_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("empleado_id",empleado_id);
            parametros.Add("ajuste_existencia_id",ajuste_existencia_id);

			conector.Update(sql,parametros);

			var filas_afectadas = conector.filas_afectadas;

			if (filas_afectadas > 0)
			{
                ajustar_existencias(ajuste_existencia_id);	
			}

			return filas_afectadas;
		}

		public long crear_ajuste_existencia(long empleado_id)
		{
			string sql = @"
				INSERT INTO
					farmacontrol_local.ajustes_existencias
				SET 
					terminal_id = @terminal_id,
					empleado_id = @empleado_id,
					fecha_creado = NOW()
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("terminal_id", Misc_helper.get_terminal_id());
			parametros.Add("empleado_id", empleado_id);

			conector.Insert(sql, parametros);

			return conector.insert_id;
		}

		public DataTable registrar_detallado_ajuste_existencia(DTO_Detallado_ajustes_existencias dto_detallado_ajuste)
		{
			string sql = @"
				INSERT INTO
					farmacontrol_local.detallado_ajustes_existencias
				SET
					ajuste_existencia_id = @ajuste_existencia_id,
					articulo_id = @articulo_id,
					caducidad = @caducidad,
					lote = @lote,
					existencia_anterior = COALESCE( (
												SELECT 
													existencia 
												FROM 
													farmacontrol_local.existencias 
												WHERE 
													articulo_id = @articulo_id 
												AND 
													caducidad = @caducidad 
												AND 
													lote = @lote 
											) ,0),
					cantidad = @cantidad,
					diferencia = @cantidad - COALESCE( (
												SELECT 
													existencia 
												FROM 
													farmacontrol_local.existencias 
												WHERE 
													articulo_id = @articulo_id 
												AND 
													caducidad = @caducidad 
												AND 
													lote = @lote 
											) ,0)
				ON DUPLICATE KEY UPDATE
					cantidad = cantidad + @cantidad,
					diferencia = diferencia + @cantidad
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("ajuste_existencia_id", dto_detallado_ajuste.ajuste_existencia_id);
			parametros.Add("articulo_id", dto_detallado_ajuste.articulo_id);
			parametros.Add("caducidad", dto_detallado_ajuste.caducidad);
			parametros.Add("lote", dto_detallado_ajuste.lote);
			parametros.Add("cantidad", dto_detallado_ajuste.cantidad);

			conector.Insert(sql,parametros);


			return get_detallado_ajustes_axistencias(dto_detallado_ajuste.ajuste_existencia_id);
		}

		public void registrar_detallado_ajuste_existencia(List<DTO_Detallado_ajustes_existencias> dto_detallado_ajuste)
		{
			foreach (DTO_Detallado_ajustes_existencias detallado_ajuste_existencia in dto_detallado_ajuste)
			{
				string sql = @"
					INSERT INTO
						farmacontrol_local.detallado_ajustes_existencias
					SET
						ajuste_existencia_id = @ajuste_existencia_id,
						articulo_id = @articulo_id,
						caducidad = @caducidad,
						lote = @lote,
						existencia_anterior = COALESCE( (
													SELECT 
														existencia 
													FROM 
														farmacontrol_local.existencias 
													WHERE 
														articulo_id = @articulo_id 
													AND 
														caducidad = @caducidad 
													AND 
														lote = @lote 
												) ,0),
						cantidad = @cantidad,
						diferencia = @cantidad - COALESCE( (
													SELECT 
														existencia 
													FROM 
														farmacontrol_local.existencias 
													WHERE 
														articulo_id = @articulo_id 
													AND 
														caducidad = @caducidad 
													AND 
														lote = @lote 
												) ,0)
					ON DUPLICATE KEY UPDATE
						cantidad = cantidad + @cantidad,
						diferencia = diferencia + @cantidad
				";

				Dictionary<string, object> parametros = new Dictionary<string, object>();
				parametros.Add("ajuste_existencia_id", detallado_ajuste_existencia.ajuste_existencia_id);
				parametros.Add("articulo_id", detallado_ajuste_existencia.articulo_id);
				parametros.Add("caducidad", detallado_ajuste_existencia.caducidad);
				parametros.Add("lote", detallado_ajuste_existencia.lote);
				parametros.Add("cantidad", detallado_ajuste_existencia.cantidad);

				conector.Insert(sql, parametros);
			}
		}

		public DataTable get_detallado_ajustes_axistencias(long ajuste_existencia_id)
		{
            //string sql = "farmacontrol_global.AjusteExistencias_get_detallado_ajustes_axistencias";
            
			string sql = @"
				SELECT
					detallado_ajuste_existencia_id,
					(
						SELECT 
							amecop 
						FROM 
							farmacontrol_global.articulos_amecops 
						WHERE 
							articulo_id = detallado_ajustes_existencias.articulo_id
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
					diferencia
				FROM
					farmacontrol_local.detallado_ajustes_existencias
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					ajuste_existencia_id = @ajuste_Existencia_id
				GROUP BY
					detallado_ajuste_existencia_id
				ORDER BY
					detallado_ajuste_existencia_id
				ASC
			";
            
			Dictionary<string,object> parametros = new Dictionary<string,object>();
            parametros.Add("ajuste_Existencia_id", ajuste_existencia_id);

			conector.Select(sql,parametros);
            //conector.Call(sql, parametros);

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

		public bool set_inserta_detallado_ajuste(Dictionary<string, int> obsequios, long id_ajuste)
		{
			bool error = false;

		//	conector.TransactionStart();

            foreach (var item in obsequios)
            {
                string sql = @"
					SELECT
						articulo_id,
						existencia,
						CONVERT(caducidad USING utf8) AS caducidad,
						lote
					FROM
						farmacontrol_local.existencias
					LEFT JOIN 
						farmacontrol_global.articulos
					USING( articulo_id )
					WHERE
						amecop_original = @codigo
					AND 
						existencia >= @cantidad_obsequiada
					ORDER BY 
							caducidad DESC
					LIMIT 1
					
				";
				
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("codigo", item.Key); 
				parametros.Add("cantidad_obsequiada", item.Value);

                conector.Select(sql, parametros);

                if (conector.result_set.Rows.Count > 0)
				{
					foreach (DataRow row in conector.result_set.Rows)
					{

						long articulo_id = Convert.ToInt64(row["articulo_id"]);
						int existencia = Convert.ToInt32(row["existencia"]);
                        string caducidad = row["caducidad"].ToString();
                        string lote = row["lote"].ToString();

                        sql = @"
							INSERT INTO
								farmacontrol_local.detallado_ajustes_existencias
							SET
								ajuste_existencia_id = @id_existencia,
								articulo_id = @articulo_id,
								caducidad   = @caducidad,
								lote        = @lote,
								existencia_anterior = @existencia,
								cantidad = @existencia - @cantidad,
								diferencia =  (@existencia - @cantidad) - existencia_anterior 
					
						";

                       
                        parametros.Add("id_existencia", id_ajuste);
                        parametros.Add("articulo_id", articulo_id);
                        parametros.Add("caducidad", caducidad);
                        parametros.Add("lote", lote);
                        parametros.Add("existencia", existencia);
                        parametros.Add("cantidad", item.Value);

						conector.Insert(sql, parametros);

                        sql = @"
							INSERT INTO
								farmacontrol_local.kardex
							SET
								terminal_id = @terminal_id,
								fecha_datetime = NOW(),
								fecha_date  = CURDATE(),
								articulo_id = @articulo_id,
								caducidad   = @caducidad,
								lote        = @lote,
								tipo        = 'AJUSTE_EXISTENCIA',
								elemento_id = @id_existencia,
								folio       = @id_existencia,
								existencia_anterior = @existencia,
								cantidad = -@cantidad,
								existencia_posterior = @existencia - @cantidad,
								es_importado = 0,
								modified = NOW()
					
						";

                        parametros.Add("terminal_id", Misc_helper.get_terminal_id());
                        conector.Insert(sql, parametros);


                        //afectar existencia 

                        sql = @"
						
							SELECT
							    existencia_id,
								articulo_id,
								existencia	
							FROM
								farmacontrol_local.existencias
							WHERE
								articulo_id = @articulo_id
							AND	
								caducidad = @caducidad
							AND
								lote = @lote

						";
							
						conector.Select(sql, parametros);
						if (conector.result_set.Rows.Count > 0)
						{
							DataRow row_existencia = conector.result_set.Rows[0];
							int existencia_id = Convert.ToInt32(row_existencia["existencia_id"]);
							int existencia_actual = Convert.ToInt32(row_existencia["existencia"]);

							if (existencia_actual >= 1)
							{
								//actualiza la existencia del producto disminuyendo a la cantidad de ajuste
								sql = @"
										UPDATE
											farmacontrol_local.existencias
										SET
											existencia = existencia - @cantidad
										WHERE
											existencia_id = @existencia_id
										AND
											articulo_id = @articulo_id	
										AND
											caducidad = @caducidad
										AND
											lote = @lote
										LIMIT 1
								";
								
                                parametros.Add("existencia_id", existencia_id);
                                conector.Update(sql, parametros);

                                /*
								 * ACTUALIZA EL ID AJUSTE_EXISTENCIAS
								 * 
								 */

                                sql = @"
										UPDATE
											farmacontrol_local.ajustes_existencias
										SET
											fecha_terminado = NOW(),
											termina_empleado_id = empleado_id,
											comentarios = 'AJUSTE REALIZADO POR ENTREGA DE CIRCULO DE LA SALUD ORO DEL TICKET'
										WHERE
											ajuste_existencia_id = @ajuste_existencia_id_nuevo
										
								";

                                parametros.Add("ajuste_existencia_id_nuevo", id_ajuste);
                                conector.Update(sql, parametros);



                            }
                            else
							{
								//Elimina el renglon con existencia igual a 1 
								

							}


							
						}
                    }

                }
				else 
				{
                 //   conector.TransactionCommit();
                    

					return false;


                }



            }

			error = true;//conector.TransactionCommit();

            return error;
		}
	
	
	}
}
