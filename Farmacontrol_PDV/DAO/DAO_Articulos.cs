using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.HELPERS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Farmacontrol_PDV.DAO
{
    class DAO_Articulos
    {
        Conector conector = new Conector();

        public static List<GetArticulosDTO> GetArticulos(string valor)
        {
            List<GetArticulosDTO> lista = new List<GetArticulosDTO>();

            try
            {
                Conector conector = new Conector();

                string sql = @"
						   SELECT
					g.articulo_id,
					CAST(g.amecop_original AS UNSIGNED) AS codigo,
					g.nombre,
					COALESCE(SUM(e.existencia), 0) AS existencia_total
				FROM
					farmacontrol_global.articulos g
				LEFT JOIN
					farmacontrol_local.existencias e ON g.articulo_id = e.articulo_id
				JOIN
					farmacontrol_global.articulo l ON l.a_amecop = g.amecop_original
				WHERE
					g.activo = 1
					AND l.a_sector = 20
					AND (
						CAST(g.amecop_original AS UNSIGNED) LIKE CONCAT('%', @valor, '%')
						OR g.nombre LIKE CONCAT('%', @valor, '%')
					)
				GROUP BY
					g.articulo_id, g.amecop_original, g.nombre
				LIMIT 50;

        ";

                Dictionary<string, object> parametros = new Dictionary<string, object>
        {
            { "valor", valor }
        };

                conector.Select(sql, parametros);

                if (conector.result_set != null && conector.result_set.Rows.Count > 0)
                {
                    foreach (DataRow row in conector.result_set.Rows)
                    {
                        lista.Add(new GetArticulosDTO
                        {
                            articulo_id = Convert.ToInt32(row["articulo_id"]),
                            codigo = row["codigo"].ToString(),
                            nombre = row["nombre"].ToString(),
                            existencia_total = Convert.ToInt32(row["existencia_total"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Mostrar el SQL, el parámetro y el mensaje de error
                string errorDetalle = $"ERROR: {ex.Message}\n\nConsulta SQL:\n{ex.Source}\n\nParámetro:\nvalor = {valor}";

                MessageBox.Show("Ocurrió un error al obtener los artículos:\n\n" + errorDetalle,
                                "Error en GetArticulos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                Console.WriteLine("ERROR COMPLETO:\n" + ex.ToString());
            }

            return lista;
        }

        public static List<GetArticulosDTO> GetArticulos_materia_prima(string valor)
        {
            List<GetArticulosDTO> lista = new List<GetArticulosDTO>();

            try
            {
                Conector conector = new Conector();

                string sql = @"
						   SELECT 
                                a.articulo_id, 
                                CAST(g.amecop_original AS UNSIGNED) AS codigo,
                                g.nombre, 
                               CAST(a.caducidad AS CHAR) AS caducidad,

                                a.lote,
                                a.existencia
                            FROM 
                                farmacontrol_local.existencias a
                            INNER JOIN 
                                farmacontrol_global.articulos g ON g.articulo_id = a.articulo_id
                            WHERE 
                                a.existencia > 0
                                AND (
                                    CAST(g.amecop_original AS UNSIGNED) LIKE CONCAT('%', @valor, '%')
                                    OR g.nombre LIKE CONCAT('%', @valor, '%')
                                )
                            LIMIT 50;



        ";

                Dictionary<string, object> parametros = new Dictionary<string, object>
        {
            { "valor", valor }
        };

                conector.Select(sql, parametros);

                if (conector.result_set != null && conector.result_set.Rows.Count > 0)
                {
                    foreach (DataRow row in conector.result_set.Rows)
                    {
                        lista.Add(new GetArticulosDTO
                        {
                            articulo_id = Convert.ToInt32(row["articulo_id"]),
                            codigo = row["codigo"].ToString(),
                            nombre = row["nombre"].ToString(),
                            existencia_total = Convert.ToInt32(row["existencia"]),
                            lote = row["lote"].ToString(),
                            caducidad = row["caducidad"].ToString()


                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Mostrar el SQL, el parámetro y el mensaje de error
                string errorDetalle = $"ERROR: {ex.Message}\n\nConsulta SQL:\n{ex.Source}\n\nParámetro:\nvalor = {valor}";

                MessageBox.Show("Ocurrió un error al obtener los artículos:\n\n" + errorDetalle,
                                "Error en GetArticulos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                Console.WriteLine("ERROR COMPLETO:\n" + ex.ToString());
            }

            return lista;
        }





        public bool actualizar_articulo_clase_antibiotico(long articulo_id, long clase_antibiotico_id)
        {
            string sql = @"
                UPDATE
                    farmacontrol_global.articulos
                SET
                    clase_antibiotico_id = @clase_antibiotico_id
                WHERE
                    articulo_id = @articulo_id
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("clase_antibiotico_id", clase_antibiotico_id);
            parametros.Add("articulo_id", articulo_id);

            conector.Update(sql, parametros);

            bool result = false;

            if (conector.filas_afectadas > 0)
            {
                result = true;
                DAO_Cola_operaciones.insertar_cola_operaciones((long)Principal.empleado_id, "rest/articulos", "actualizar_articulo_clase_antibiotico", parametros, "PARA ENVIO A SERVIDOR PRINCIPAL");
            }

            return result;
        }

        public DataTable get_busqueda_productos_materias_primas(string busqueda)
        {
            string sql = @"
				(
					SELECT
						articulo_id,
						NULL AS materia_prima_id,
						nombre,
						precio_costo,
						precio_publico,
						0 AS es_materia_prima
					FROM
						farmacontrol_global.articulos
					WHERE
						nombre LIKE @busqueda
				)
				UNION
				(
					SELECT
						NULL AS articulo_id,
						materia_prima_id,
						nombre,
						precio_costo,
						precio_publico,
						1 AS es_materia_prima
					FROM
						farmacontrol_global.materias_primas
					WHERE
						nombre LIKE @busqueda
				)
				ORDER BY nombre
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("busqueda", "%" + busqueda.Replace(" ", "%") + "%");

            conector.Select(sql, parametros);

            return conector.result_set;
        }

        public DataTable get_articulos_empaque(long padre_articulo_id, string caducidad, string lote, int cantidad)
        {
            string sql = @"
				SELECT
					articulos_menudeo.componente_articulo_id AS articulo_id,
					(
						SELECT 
							amecop
						FROM 
							farmacontrol_global.articulos_amecops 
						WHERE 
							articulo_id = farmacontrol_global.articulos_menudeo.componente_articulo_id
						ORDER BY
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS amecop,
					articulos.nombre AS producto,
					@caducidad AS caducidad,
					@lote AS lote,
					@cantidad * articulos_menudeo.cantidad AS cantidad
				FROM
					farmacontrol_global.articulos_menudeo
				LEFT JOIN farmacontrol_global.articulos ON
					articulos.articulo_id = articulos_menudeo.componente_articulo_id
				WHERE
					articulos_menudeo.padre_articulo_id = @articulo_id
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("articulo_id", padre_articulo_id);
            parametros.Add("caducidad", caducidad);
            parametros.Add("lote", lote);
            parametros.Add("cantidad", cantidad);

            conector.Select(sql, parametros);

            return conector.result_set;
        }

        public bool es_empaque(long articulo_id)
        {
            string sql = @"
				SELECT
					tipo_empaque
				FROM
					farmacontrol_global.articulos
				WHERE
					articulo_id = @articulo_id
				AND
					tipo_empaque IS NOT NULL
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("articulo_id", articulo_id);

            conector.Select(sql, parametros);

            var result = conector.result_set;

            if (result.Rows.Count > 0)
            {
                if (result.Rows[0]["tipo_empaque"].ToString().Equals("EMPAQUE"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /*
		 * 
		 * METODOS REST
		 * 
		 * */
        public static DataTable get_existencia_caducidades(long sucursal_id, long articulo_id)
        {
            Rest_parameters parametros = new Rest_parameters();
            parametros.Add("sucursal_id", sucursal_id);
            parametros.Add("articulo_id", articulo_id);
            List<Result_existencia_caducidades> result_existencia_caducidades = Rest_helper.make_request<List<Result_existencia_caducidades>>("articulos/get_existencia_caducidades", parametros);
            return result_existencia_caducidades.ToDataTable();
        }

        public static DataTable get_consulta_existencias(long articulo_id)
        {
            Rest_parameters parametros = new Rest_parameters();
            parametros.Add("articulo_id", articulo_id);
            List<DTO_Consulta_existencias> result_consulta_existencias = Rest_helper.make_request<List<DTO_Consulta_existencias>>("articulos/get_consulta_existencias", parametros);
            return result_consulta_existencias.ToDataTable();
        }

        public static DataTable busqueda_articulos_existencias(long sucursal_id, string busqueda)
        {
            Rest_parameters parametros = new Rest_parameters();
            parametros.Add("sucursal_id", sucursal_id);
            parametros.Add("busqueda", busqueda);
            List<Busqueda_articulos_existencias> result_articulos_existencias = Rest_helper.make_request<List<Busqueda_articulos_existencias>>("articulos/busqueda_articulos_existencias", parametros);
            return result_articulos_existencias.ToDataTable();
        }

        /*
         * FIN METODOS REST
         * 
         * */

        public long execution_time = 0;

        public long get_num_rows_kardex(int articulo_id)
        {
            string sql = @"
				SELECT
					COUNT(kardex_id) AS num_rows
				FROM
					farmacontrol_local.kardex
				WHERE
					articulo_id = @articulo_id
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("articulo_id", articulo_id);

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                return Convert.ToInt64(conector.result_set.Rows[0]["num_rows"]);
            }

            return 0;
        }

        public DataTable get_kardex_articulo(int articulo_id, long fila_inicio)
        {

            /*
             * SELECT
                    terminales.nombre AS terminal,
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					tipo,
					folio,
					existencia_anterior,
					cantidad,
					existencia_posterior,
					fecha_datetime AS fecha,
                    es_importado
				FROM
					farmacontrol_local.kardex
                JOIN farmacontrol_local.terminales USING(terminal_id)
				WHERE
					articulo_id = @articulo_id
				ORDER BY kardex_id DESC
				LIMIT 100 OFFSET @fila_inicio 
             * 
             * */
            string sql = @"
				SELECT
                    terminales.nombre AS terminal,
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					tipo,
					folio,
					existencia_anterior,
					cantidad,
					existencia_posterior,
					fecha_datetime AS fecha,
                    es_importado
				FROM
					farmacontrol_local.kardex
                JOIN farmacontrol_local.terminales USING(terminal_id)
				WHERE
					articulo_id = @articulo_id

				GROUP BY 
					caducidad,lote,kardex_id
				ORDER BY kardex_id DESC
				LIMIT 100 OFFSET @fila_inicio
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("articulo_id", articulo_id);
            parametros.Add("fila_inicio", fila_inicio);

            conector.Select(sql, parametros);

            var result_set = conector.result_set;

            foreach (DataRow row in result_set.Rows)
            {
                row["caducidad"] = row["caducidad"].ToString().Equals("VARIOS") ? "VARIOS" : Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");
            }

            return result_set;
        }

        public Dictionary<string, Tuple<int, string>> get_cantidad_articulo_modulos(string amecop, string caducidad = "", string lote = "")
        {
            Dictionary<string, Tuple<int, string>> lista_cantidades_uso = new Dictionary<string, Tuple<int, string>>();

            if (caducidad.Equals("") && lote.Equals(""))
            {
                lista_cantidades_uso.Add("APARTADOS", get_cantidad_articulo_apartados(amecop));
                lista_cantidades_uso.Add("VENTAS", get_cantidad_articulo_ventas(amecop));
                lista_cantidades_uso.Add("TRASPASOS (ENVIAR)", get_cantidad_articulo_traspasos_enviar(amecop));
                lista_cantidades_uso.Add("TRASPASOS (RECIBIR)", get_cantidad_articulo_traspasos_recibir(amecop));
                lista_cantidades_uso.Add("MERMAS", get_cantidad_articulo_merma(amecop));
                lista_cantidades_uso.Add("ENTRADAS", get_cantidad_articulo_entradas(amecop));
                lista_cantidades_uso.Add("DEVOLUCIONES", get_cantidad_articulo_devoluciones(amecop));
                lista_cantidades_uso.Add("AJUSTES DE EXISTENCIAS", get_cantidad_articulo_ajustes_existencias(amecop));
                lista_cantidades_uso.Add("total", get_cantidad_articulo_existencias(amecop));
            }
            else
            {
                lista_cantidades_uso.Add("APARTADOS", get_cantidad_articulo_apartados(amecop, caducidad, lote));
                lista_cantidades_uso.Add("VENTAS", get_cantidad_articulo_ventas(amecop, caducidad, lote));
                lista_cantidades_uso.Add("TRASPASOS (ENVIAR)", get_cantidad_articulo_traspasos_enviar(amecop));
                lista_cantidades_uso.Add("TRASPASOS (RECIBIR)", get_cantidad_articulo_traspasos_recibir(amecop));
                lista_cantidades_uso.Add("MERMAS", get_cantidad_articulo_merma(amecop, caducidad, lote));
                lista_cantidades_uso.Add("ENTRADAS", get_cantidad_articulo_entradas(amecop, caducidad, lote));
                lista_cantidades_uso.Add("DEVOLUCIONES", get_cantidad_articulo_devoluciones(amecop, caducidad, lote));
                lista_cantidades_uso.Add("AJUSTES DE EXISTENCIAS", get_cantidad_articulo_ajustes_existencias(amecop, caducidad, lote));
                lista_cantidades_uso.Add("total", get_cantidad_articulo_existencias(amecop, caducidad, lote));
            }

            return lista_cantidades_uso;
        }

        /*
		 *	INICIO CANTIDADES EN USO
		 * */

        public Tuple<int, string> get_cantidad_articulo_apartados(string amecop, string caducidad = "", string lote = "")
        {
            string sql = "";

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            if (caducidad.Equals("") && lote.Equals(""))
            {
                sql = @"
					SELECT
						SUM(COALESCE(cantidad,0)) AS existencia,
						GROUP_CONCAT(DISTINCT apartado_id) AS folios
					FROM
						farmacontrol_local.apartados
					LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
					LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id)
					WHERE
						articulos_amecops.amecop = @amecop
                    HAVING existencia IS NOT NULL
				";

                parametros.Add("amecop", amecop);
            }
            else
            {
                sql = @"
					SELECT
						COALESCE(SUM(COALESCE(cantidad,0)), 0) AS existencia,
						COALESCE(GROUP_CONCAT(DISTINCT apartado_id), 0) AS folios
					FROM
						farmacontrol_local.apartados
					JOIN farmacontrol_global.articulos USING(articulo_id)
					WHERE
						apartados.caducidad = @caducidad
					AND
						apartados.lote = @lote
                    HAVING existencia IS NOT NULL
				";

                parametros.Add("amecop", amecop);
                parametros.Add("caducidad", caducidad);
                parametros.Add("lote", lote);
            }

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                Tuple<int, string> tupla = new Tuple<int, string>(Convert.ToInt32(conector.result_set.Rows[0]["existencia"]), conector.result_set.Rows[0]["folios"].ToString());
                return tupla;
            }

            return new Tuple<int, string>(Convert.ToInt32(0), "");
        }

        public Tuple<int, string> get_cantidad_articulo_ventas(string amecop, string caducidad = "", string lote = "")
        {
            string sql = "";

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            if (caducidad.Equals("") && lote.Equals(""))
            {
                sql = @"
					SELECT
						COALESCE(SUM(cantidad),0) AS existencia,
						GROUP_CONCAT(DISTINCT venta_id) AS folios
					FROM
						farmacontrol_local.detallado_ventas
					LEFT JOIN farmacontrol_local.ventas USING(venta_id)
					LEFT JOIN articulos USING(articulo_id)
					LEFT JOIN articulos_amecops USING(articulo_id)
					WHERE
						ventas.fecha_terminado IS NULL
					AND
						articulos_amecops.amecop = @amecop
				";

                parametros.Add("amecop", amecop);
            }
            else
            {
                sql = @"
					SELECT
						COALESCE(SUM(cantidad),0) AS existencia,
						GROUP_CONCAT(DISTINCT venta_id) AS folios
					FROM
						farmacontrol_local.detallado_ventas
					LEFT JOIN farmacontrol_local.ventas USING(venta_id)
					LEFT JOIN articulos USING(articulo_id)
					WHERE
						ventas.fecha_terminado IS NULL
					AND
						detallado_ventas.caducidad = @caducidad
					AND
						detallado_ventas.lote = @lote
				";

                parametros.Add("amecop", amecop);
                parametros.Add("caducidad", caducidad);
                parametros.Add("lote", lote);
            }

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                Tuple<int, string> tupla = new Tuple<int, string>(Convert.ToInt32(conector.result_set.Rows[0]["existencia"]), conector.result_set.Rows[0]["folios"].ToString());
                return tupla;
            }

            return new Tuple<int, string>(Convert.ToInt32(0), "");
        }

        public Tuple<int, string> get_cantidad_articulo_entradas(string amecop, string caducidad = "", string lote = "")
        {
            string sql = "";

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            if (caducidad.Equals("") && lote.Equals(""))
            {
                sql = @"
					SELECT
						COALESCE(SUM(cantidad),0) AS existencia,
						GROUP_CONCAT(DISTINCT entrada_id) AS folios
					FROM
						farmacontrol_local.detallado_entradas
					LEFT JOIN farmacontrol_local.entradas USING(entrada_id)
					LEFT JOIN articulos USING(articulo_id)
					LEFT JOIN articulos_amecops USING(articulo_id)
					WHERE
						entradas.fecha_terminado IS NULL
					AND
						articulos_amecops.amecop = @amecop
				";

                parametros.Add("amecop", amecop);
            }
            else
            {
                sql = @"
					SELECT
						COALESCE(SUM(cantidad),0) AS existencia,
						GROUP_CONCAT(DISTINCT entrada_id) AS folios
					FROM
						farmacontrol_local.detallado_entradas
					LEFT JOIN farmacontrol_local.entradas USING(entrada_id)
					LEFT JOIN articulos USING(articulo_id)
					WHERE
						entradas.fecha_terminado IS NULL
					AND
						detallado_entradas.caducidad = @caducidad
					AND
						detallado_entradas.lote = @lote
				";

                parametros.Add("amecop", amecop);
                parametros.Add("caducidad", caducidad);
                parametros.Add("lote", lote);
            }

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                Tuple<int, string> tupla = new Tuple<int, string>(Convert.ToInt32(conector.result_set.Rows[0]["existencia"]), conector.result_set.Rows[0]["folios"].ToString());
                return tupla;
            }

            return new Tuple<int, string>(Convert.ToInt32(0), "");
        }

        public Tuple<int, string> get_cantidad_articulo_devoluciones(string amecop, string caducidad = "", string lote = "")
        {
            string sql = "";

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            if (caducidad.Equals("") && lote.Equals(""))
            {
                sql = @"
					SELECT
						COALESCE(SUM(cantidad),0) AS existencia,
						GROUP_CONCAT(DISTINCT devolucion_id) AS folios
					FROM
						farmacontrol_local.detallado_devoluciones
					LEFT JOIN farmacontrol_local.devoluciones USING(devolucion_id)
					LEFT JOIN articulos USING(articulo_id)
					LEFT JOIN articulos_amecops USING(articulo_id)
					WHERE
						devoluciones.fecha_terminado IS NULL
					AND
						articulos_amecops.amecop = @amecop
				";

                parametros.Add("amecop", amecop);
            }
            else
            {
                sql = @"
					SELECT
						COALESCE(SUM(cantidad),0) AS existencia,
						GROUP_CONCAT(DISTINCT devolucion_id) AS folios
					FROM
						farmacontrol_local.detallado_devoluciones
					LEFT JOIN farmacontrol_local.devoluciones USING(devolucion_id)
					LEFT JOIN articulos USING(articulo_id)
					WHERE
						devoluciones.fecha_terminado IS NULL
					AND
						detallado_devoluciones.caducidad = @caducidad
					AND
						detallado_devoluciones.lote = @lote
				";

                parametros.Add("amecop", amecop);
                parametros.Add("caducidad", caducidad);
                parametros.Add("lote", lote);
            }

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                Tuple<int, string> tupla = new Tuple<int, string>(Convert.ToInt32(conector.result_set.Rows[0]["existencia"]), conector.result_set.Rows[0]["folios"].ToString());
                return tupla;
            }

            return new Tuple<int, string>(Convert.ToInt32(0), "");
        }

        public Tuple<int, string> get_cantidad_articulo_ajustes_existencias(string amecop, string caducidad = "", string lote = "")
        {
            string sql = "";

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            if (caducidad.Equals("") && lote.Equals(""))
            {
                sql = @"
					SELECT
						COALESCE(SUM(cantidad),0) AS existencia,
						GROUP_CONCAT(DISTINCT ajuste_existencia_id) AS folios
					FROM
						farmacontrol_local.detallado_ajustes_existencias
					LEFT JOIN farmacontrol_local.ajustes_existencias USING(ajuste_existencia_id)
					LEFT JOIN articulos USING(articulo_id)
					LEFT JOIN articulos_amecops USING(articulo_id)
					WHERE
						ajustes_existencias.fecha_terminado IS NULL
					AND
						articulos_amecops.amecop = @amecop
				";

                parametros.Add("amecop", amecop);
            }
            else
            {
                sql = @"
					SELECT
						COALESCE(SUM(cantidad),0) AS existencia,
						GROUP_CONCAT(DISTINCT ajuste_existencia_id) AS folios
					FROM
						farmacontrol_local.detallado_ajustes_existencias
					LEFT JOIN farmacontrol_local.ajustes_existencias USING(ajuste_existencia_id)
					LEFT JOIN articulos USING(articulo_id)
					WHERE
						ajustes_existencias.fecha_terminado IS NULL
					AND
						detallado_ajustes_existencias.caducidad = @caducidad
					AND
						detallado_ajustes_existencias.lote = @lote
				";

                parametros.Add("amecop", amecop);
                parametros.Add("caducidad", caducidad);
                parametros.Add("lote", lote);
            }

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                Tuple<int, string> tupla = new Tuple<int, string>(Convert.ToInt32(conector.result_set.Rows[0]["existencia"]), conector.result_set.Rows[0]["folios"].ToString());
                return tupla;
            }

            return new Tuple<int, string>(Convert.ToInt32(0), "");
        }

        public Tuple<int, string> get_cantidad_articulo_traspasos_enviar(string amecop, string caducidad = "", string lote = "")
        {
            string sql = "";

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            if (caducidad.Equals("") && lote.Equals(""))
            {
                sql = @"
					SELECT
						COALESCE(SUM(cantidad),0) AS existencia,
						GROUP_CONCAT(DISTINCT traspaso_id) AS folios
					FROM
						farmacontrol_local.detallado_traspasos
					LEFT JOIN farmacontrol_local.traspasos USING(traspaso_id)
					LEFT JOIN articulos USING(articulo_id)
					LEFT JOIN articulos_amecops USING(articulo_id)
					WHERE
						traspasos.fecha_terminado_destino IS NULL
					AND
						traspasos.tipo = 'ENVIAR'
					AND
						articulos_amecops.amecop = @amecop
				";

                parametros.Add("amecop", amecop);
            }
            else
            {
                sql = @"
					SELECT
						COALESCE(SUM(cantidad),0) AS existencia,
						GROUP_CONCAT(DISTINCT traspaso_id) AS folios
					FROM
						farmacontrol_local.detallado_traspasos
					LEFT JOIN farmacontrol_local.traspasos USING(traspaso_id)
					LEFT JOIN articulos USING(articulo_id)
					WHERE
						traspasos.fecha_terminado_destino IS NULL
					AND
						traspasos.tipo = 'ENVIAR'
					AND
						detallado_traspasos.caducidad = @caducidad
					AND
						detallado_traspasos.lote = @lote
				";

                parametros.Add("amecop", amecop);
                parametros.Add("caducidad", caducidad);
                parametros.Add("lote", lote);
            }

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                Tuple<int, string> tupla = new Tuple<int, string>(Convert.ToInt32(conector.result_set.Rows[0]["existencia"]), conector.result_set.Rows[0]["folios"].ToString());
                return tupla;
            }

            return new Tuple<int, string>(Convert.ToInt32(0), "");
        }

        public Tuple<int, string> get_cantidad_articulo_traspasos_recibir(string amecop, string caducidad = "", string lote = "")
        {
            string sql = "";

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            if (caducidad.Equals("") && lote.Equals(""))
            {
                sql = @"
					SELECT
						COALESCE(SUM(cantidad),0) AS existencia,
						GROUP_CONCAT(DISTINCT traspaso_id) AS folios
					FROM
						farmacontrol_local.detallado_traspasos
					LEFT JOIN farmacontrol_local.traspasos USING(traspaso_id)
					LEFT JOIN articulos USING(articulo_id)
					LEFT JOIN articulos_amecops USING(articulo_id)
					WHERE
						traspasos.fecha_terminado IS NULL
					AND
						traspasos.tipo = 'RECIBIR'
					AND
						articulos_amecops.amecop = @amecop
				";

                parametros.Add("amecop", amecop);
            }
            else
            {
                sql = @"
					SELECT
						COALESCE(SUM(cantidad),0) AS existencia,
						GROUP_CONCAT(DISTINCT traspaso_id) AS folios
					FROM
						farmacontrol_local.detallado_traspasos
					LEFT JOIN farmacontrol_local.traspasos USING(traspaso_id)
					LEFT JOIN articulos USING(articulo_id)
					WHERE
						traspasos.fecha_terminado IS NULL
					AND
						traspasos.tipo = 'RECIBIR'
					AND
						detallado_traspasos.caducidad = @caducidad
					AND
						detallado_traspasos.lote = @lote
				";

                parametros.Add("amecop", amecop);
                parametros.Add("caducidad", caducidad);
                parametros.Add("lote", lote);
            }

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                Tuple<int, string> tupla = new Tuple<int, string>(Convert.ToInt32(conector.result_set.Rows[0]["existencia"]), conector.result_set.Rows[0]["folios"].ToString());
                return tupla;
            }

            return new Tuple<int, string>(Convert.ToInt32(0), "");
        }

        public Tuple<int, string> get_cantidad_articulo_merma(string amecop, string caducidad = "", string lote = "")
        {
            string sql = "";

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            if (caducidad.Equals("") && lote.Equals(""))
            {
                sql = @"
					SELECT
						COALESCE(SUM(cantidad),0) AS existencia,
						GROUP_CONCAT(DISTINCT merma_id) AS folios
					FROM
						farmacontrol_local.detallado_mermas
					LEFT JOIN farmacontrol_local.mermas USING(merma_id)
					LEFT JOIN articulos USING(articulo_id)
					LEFT JOIN articulos_amecops USING(articulo_id)
					WHERE
						mermas.fecha_terminado IS NULL
					AND
						articulos_amecops.amecop = @amecop
				";

                parametros.Add("amecop", amecop);
            }
            else
            {
                sql = @"
					SELECT
						COALESCE(SUM(cantidad),0) AS existencia,
						GROUP_CONCAT(DISTINCT merma_id) AS folios
					FROM
						farmacontrol_local.detallado_mermas
					LEFT JOIN farmacontrol_local.mermas USING(merma_id)
					LEFT JOIN articulos USING(articulo_id)
					WHERE
						mermas.fecha_terminado IS NULL
					AND
						detallado_mermas.caducidad = @caducidad
					AND
						detallado_mermas.lote = @lote
				";

                parametros.Add("amecop", amecop);
                parametros.Add("caducidad", caducidad);
                parametros.Add("lote", lote);
            }

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                Tuple<int, string> tupla = new Tuple<int, string>(Convert.ToInt32(conector.result_set.Rows[0]["existencia"]), conector.result_set.Rows[0]["folios"].ToString());
                return tupla;
            }

            return new Tuple<int, string>(Convert.ToInt32(0), "");
        }

        public Tuple<int, string> get_cantidad_articulo_existencias(string amecop, string caducidad = "", string lote = "")
        {
            string sql = "";

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            if (caducidad.Equals("") && lote.Equals(""))
            {
                sql = @"
					SELECT
						COALESCE(SUM(existencia),0) AS existencia
					FROM
						farmacontrol_local.existencias
					LEFT JOIN articulos USING(articulo_id)
					LEFT JOIN articulos_amecops USING(articulo_id)
					WHERE
						articulos_amecops.amecop = @amecop
				";

                parametros.Add("amecop", amecop);
            }
            else
            {
                sql = @"
					SELECT
						COALESCE(SUM(existencia),0) AS existencia
					FROM
						farmacontrol_local.existencias
					LEFT JOIN articulos USING(articulo_id)
					LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id)
					WHERE
						articulos_amecops.amecop = @amecop
					AND
						existencias.caducidad = @caducidad
					AND
						existencias.lote = @lote
				";

                parametros.Add("amecop", amecop);
                parametros.Add("caducidad", caducidad);
                parametros.Add("lote", lote);
            }

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                Tuple<int, string> tupla = new Tuple<int, string>(Convert.ToInt32(conector.result_set.Rows[0]["existencia"]), "");
                return tupla;
            }

            return new Tuple<int, string>(Convert.ToInt32(0), "");
        }

        /*
		 *	FIN CANTIDADES EN USO
		 * */

        public void registrar_kardex_articulo(int articulo_id, string caducidad, string lote, string tipo, object elemento_id, object folio, int existencia_anterior, int cantidad, int existencia_posterior)
        {
            string sql = @"
				INSERT INTO
					farmacontrol_local.kardex
				SET
					terminal_id = @terminal_id,
                    fecha_datetime = NOW(),
                    fecha_date = CURDATE(),
					articulo_id = @articulo_id,
					caducidad = @caducidad,
					lote = @lote,
					tipo = @tipo,
                    elemento_id = @elemento_id,
					folio = @folio,
					existencia_anterior = @existencia_anterior,
					cantidad = @cantidad,
					existencia_posterior = @existencia_posterior
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("terminal_id", HELPERS.Misc_helper.get_terminal_id());
            parametros.Add("articulo_id", articulo_id);
            parametros.Add("caducidad", caducidad);
            parametros.Add("lote", lote);
            parametros.Add("tipo", tipo);
            parametros.Add("elemento_id", elemento_id);
            parametros.Add("folio", folio);
            parametros.Add("existencia_anterior", existencia_anterior);
            parametros.Add("cantidad", cantidad);
            parametros.Add("existencia_posterior", existencia_posterior);

            conector.Insert(sql, parametros);
        }

        public bool validar_existencia_actual(int articulo_id, string caducidad, string lote, int cantidad)
        {
            string sql = @"
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
				LIMIT 1
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("articulo_id", articulo_id);
            parametros.Add("caducidad", caducidad);
            parametros.Add("lote", lote);

            conector.Select(sql, parametros);

            DataTable result_existencia = conector.result_set;

            if (result_existencia.Rows.Count > 0)
            {
                if (Convert.ToInt32(result_existencia.Rows[0]["existencia"]) > cantidad)
                {
                    return true;
                }
            }

            return false;
        }

        public bool get_afecta_existencias(int articulo_id)
        {
            bool response = false;

            try
            {
                string sql = @"
					SELECT
						afecta_Existencias
					FROM
						farmacontrol_global.articulos
					WHERE
						articulo_id = @articulo_id
				";

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("articulo_id", articulo_id);

                conector.Select(sql, parametros);

                if (conector.result_set.Rows.Count > 0)
                {
                    response = Convert.ToBoolean(conector.result_set.Rows[0]["afecta_existencias"]);
                }
            }
            catch (Exception ex)
            {
                Log_error.log(ex);
            }

            return response;
        }

        public void afectar_existencias_entrada(List<Tuple<int, string, string, int>> lista_productos, string tipo, object elemento_id, object folio)
        {
            foreach (Tuple<int, string, string, int> producto in lista_productos)
            {
                int articulo_id = producto.Item1;

                if (get_afecta_existencias(articulo_id))
                {
                    int existencia_anterior = 0;
                    int cantidad = producto.Item4;
                    int existencia_posterior = 0;

                    Dictionary<string, object> parametros = new Dictionary<string, object>();

                    string sql = @"
						SELECT
							existencia_id,
							existencia
						FROM
							farmacontrol_local.existencias
						WHERE
							articulo_id = @articulo_id
						AND
							caducidad = @caducidad
						AND
							lote = @lote
						LIMIT 1
					";

                    parametros.Add("articulo_id", producto.Item1);
                    parametros.Add("caducidad", producto.Item2);
                    parametros.Add("lote", producto.Item3);

                    conector.Select(sql, parametros);

                    var result_existencias = conector.result_set;

                    if (result_existencias.Rows.Count > 0)
                    {
                        existencia_anterior = Convert.ToInt32(result_existencias.Rows[0]["existencia"]);

                        existencia_posterior = existencia_anterior + cantidad;

                        Dictionary<string, object> parametros_existencia = new Dictionary<string, object>();
                        parametros_existencia.Add("existencia_posterior", existencia_posterior);
                        parametros_existencia.Add("existencia_id", Convert.ToInt32(result_existencias.Rows[0]["existencia_id"]));

                        sql = @"
						UPDATE
							farmacontrol_local.existencias
						SET
							existencia = @existencia_posterior
						WHERE
							existencia_id = @existencia_id
					";

                        conector.Update(sql, parametros_existencia);

                        registrar_kardex_articulo(producto.Item1, producto.Item2, producto.Item3, tipo, elemento_id, folio, existencia_anterior, cantidad, existencia_posterior);
                    }
                    else
                    {
                        existencia_anterior = 0;
                        existencia_posterior = 0 + producto.Item4;

                        sql = @"
						INSERT INTO
							farmacontrol_local.existencias
						SET
							articulo_id = @articulo_id,
							caducidad = @caducidad,
							lote = @lote,
							existencia = @existencia
					";

                        parametros = new Dictionary<string, object>();
                        parametros.Add("articulo_id", producto.Item1);
                        parametros.Add("caducidad", producto.Item2);
                        parametros.Add("lote", producto.Item3);
                        parametros.Add("existencia", producto.Item4);

                        conector.Insert(sql, parametros);

                        registrar_kardex_articulo(producto.Item1, producto.Item2, producto.Item3, tipo, elemento_id, folio, existencia_anterior, cantidad, existencia_posterior);
                    }
                }
            }
        }

        public void afectar_existencias_salida(List<Tuple<int, string, string, int>> lista_productos, string tipo, object elemento_id, object folio)
        {
            foreach (Tuple<int, string, string, int> producto in lista_productos)
            {
                int articulo_id = producto.Item1;

                if (get_afecta_existencias(articulo_id))
                {
                    int existencia_anterior = 0;
                    int cantidad = producto.Item4;
                    int existencia_posterior = 0;

                    Dictionary<string, object> parametros = new Dictionary<string, object>();

                    string sql = @"
						SELECT
							existencia_id,
							existencia
						FROM
							farmacontrol_local.existencias
						WHERE
							articulo_id = @articulo_id
						AND
							caducidad = @caducidad
						AND
							lote = @lote
						LIMIT 1
					";

                    parametros.Add("articulo_id", producto.Item1);
                    parametros.Add("caducidad", producto.Item2);
                    parametros.Add("lote", producto.Item3);

                    conector.Select(sql, parametros);

                    var result_existencias = conector.result_set;

                    if (result_existencias.Rows.Count > 0)
                    {
                        existencia_anterior = Convert.ToInt32(result_existencias.Rows[0]["existencia"]);

                        existencia_posterior = existencia_anterior - cantidad;

                        Dictionary<string, object> parametros_existencia = new Dictionary<string, object>();
                        parametros_existencia.Add("existencia_posterior", existencia_posterior);
                        parametros_existencia.Add("existencia_id", Convert.ToInt32(result_existencias.Rows[0]["existencia_id"]));

                        if (existencia_posterior.Equals(Convert.ToInt32("0")) == true)
                        {
                            sql = @"
                                DELETE FROM
                                    farmacontrol_local.existencias
                                WHERE
                                    existencia_id = @existencia_id
                            ";

                            conector.Delete(sql, parametros_existencia);
                        }
                        else
                        {
                            sql = @"
						        UPDATE
							        farmacontrol_local.existencias
						        SET
							        existencia = @existencia_posterior
						        WHERE
							        existencia_id = @existencia_id
					        ";

                            conector.Update(sql, parametros_existencia);
                        }

                        registrar_kardex_articulo(producto.Item1, producto.Item2, producto.Item3, tipo, elemento_id, folio, existencia_anterior, (cantidad * -1), existencia_posterior);
                    }
                    else
                    {
                        existencia_anterior = 0;
                        existencia_posterior = producto.Item4 * -1;

                        sql = @"
						INSERT INTO
							farmacontrol_local.existencias
						SET
							articulo_id = @articulo_id,
							caducidad = @caducidad,
							lote = @lote,
							existencia = @existencia
					";

                        parametros = new Dictionary<string, object>();
                        parametros.Add("articulo_id", producto.Item1);
                        parametros.Add("caducidad", producto.Item2);
                        parametros.Add("lote", producto.Item3);
                        parametros.Add("existencia", (producto.Item4 * -1));

                        conector.Insert(sql, parametros);

                        registrar_kardex_articulo(producto.Item1, producto.Item2, producto.Item3, tipo, elemento_id, folio, existencia_anterior, (cantidad * -1), existencia_posterior);
                    }
                }
            }
        }

        public DataTable get_existencia_articulo(int articulo_id)
        {
            string sql = @"
				SELECT
					(
						SELECT 
							amecop 
						FROM 
							farmacontrol_global.articulos_amecops 
						WHERE 
							articulo_id = farmacontrol_local.existencias.articulo_id
						ORDER BY
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS amecop,
					CAST(existencias.caducidad AS CHAR(10)) AS caducidad,
					existencias.lote AS lote,
					COALESCE(SUM(existencia), 0) - COALESCE(existencia_mermas, 0) - COALESCE(existencia_devoluciones, 0) - COALESCE(existencia_apartados, 0) - COALESCE(existencia_cambio_fisico, 0) - COALESCE(existencia_ventas, 0) - COALESCE(existencia_traspasos, 0)AS existencia_vendible
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
					existencias.articulo_id = @articulo_id
				GROUP BY existencias.articulo_id, caducidad, lote
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("articulo_id", articulo_id);

            conector.Select(sql, parametros);

            return conector.result_set;
        }

        public DataTable validar_producto_promocion(int articulo_id)
        {
            string sql = @"
				SELECT
					*
				FROM
					promociones
				WHERE
					articulo_id = @articulo_id
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("articulo_id", articulo_id);

            conector.Select(sql, parametros);

            return conector.result_set;
        }

        public int get_existencia_vendible(string amecop, string caducidad, string lote)
        {
            DAO_Articulos dao_articulos = new DAO_Articulos();

            var articulo = dao_articulos.get_articulo(amecop);

            if (get_afecta_existencias((int)articulo.Articulo_id))
            {
                //IF( COUNT(amecop) > 1, CONCAT(ABS(amecop),'*'), ABS(amecop) ) AS amecop
                //
                // IF(tmp_existencias.caducidad = '0000-00-00', ' ', COALESCE(tmp_existencias.caducidad, ' ') ) AS caducidad,
                //				IF(tmp_existencias.caducidad = '0000-00-00', ' ', COALESCE(tmp_existencias.caducidad, ' ') ) AS caducidad_sin_formato,
                string sql = string.Format(@"
							SELECT
								articulos.articulo_id AS articulo_id,
                                articulos.pct_descuento AS pct_descuento,
								articulos.activo AS activo,
								(
									SELECT
										 amecop
									FROM
										farmacontrol_global.articulos_amecops
									WHERE
										articulos_amecops.articulo_id = articulos.articulo_id
									ORDER BY articulos_amecops.amecop_principal DESC
									LIMIT 1
								) AS amecop,
								nombre,
								COALESCE ( CAST( tmp_existencias.caducidad AS CHAR(10)) , ' ') AS caducidad,
                                COALESCE( CAST( tmp_existencias.caducidad AS CHAR(10) ),' ') AS caducidad_sin_formato,
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
									existencias.articulo_id = {0}
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
									detallado_devoluciones.articulo_id = {0}
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
											apartados.articulo_id = {0}
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
											detallado_mermas.articulo_id = {0}
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
									apartados.articulo_id = {0}
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
									apartados.articulo_id = {0}
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
									apartados.articulo_id = {0}
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
									detallado_traspasos.articulo_id = {0}
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
									detallado_ventas.articulo_id = {0}
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
								articulos.articulo_id = {0}
                            AND
                                tmp_existencias.caducidad = @caducidad
                            AND
                                tmp_existencias.lote = @lote
							AND 
                                articulos.activo = 1
							GROUP BY articulos.articulo_id, tmp_existencias.caducidad, tmp_existencias.lote
						 ", (long)articulo.Articulo_id);

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("caducidad", caducidad);
                parametros.Add("lote", lote);

                conector.Select(sql, parametros);

                var result = conector.result_set;

                if (result.Rows.Count > 0)
                {
                    return Convert.ToInt32(result.Rows[0]["existencia_vendible"]);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 100;
            }
        }

        public DataTable get_lotes(int articulo_id, string caducidad)
        {
            DataTable result = new DataTable();

            try
            {
                string sql = @"
					SELECT
						lote
					FROM
						farmacontrol_local.existencias
					WHERE
						articulo_id = @articulo_id
					AND
						caducidad = @caducidad
				";

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("articulo_id", articulo_id);
                parametros.Add("caducidad", caducidad);

                conector.Select(sql, parametros);

                result = conector.result_set;
            }
            catch (Exception excepcion)
            {
                Log_error.log(excepcion);
            }

            return result;
        }

        public DataTable get_caducidades(int? articulo_id)
        {
            DataTable result = new DataTable();
            ///*COALESCE((farmacontrol_global.Articulos_get_existencias_articulo(articulo_id, caducidad, 1)),0) as existencia*/
			try
            {
                /*string sql = @"
					SELECT
						DISTINCT(CAST(caducidad AS CHAR(10))) AS caducidad,
	                    
					FROM
						farmacontrol_local.existencias
					WHERE
						articulo_id = @articulo_id
				";*/
                string sql = @"
                    SELECT
                        CAST( tmp_existencias.caducidad AS CHAR(10) ) as caducidad,
	                    (
		                    COALESCE(existencia, 0) - COALESCE(existencia_mermas, 0) - 
		                    COALESCE(existencia_devoluciones, 0) - COALESCE(existencia_apartados, 0) - 
		                    COALESCE(existencia_cambio_fisico, 0) - COALESCE(existencia_traspasos, 0) - 
		                    COALESCE(existencia_mayoreo, 0) - COALESCE(existencia_ventas, 0) - 
		                    COALESCE(existencia_parcial_prepago, 0) 
	                    ) AS existencia
  
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
		                    existencias.articulo_id = @articulo_id
	                    GROUP BY
		                    articulo_id,caducidad
                    ) AS tmp_existencias
                    NATURAL LEFT JOIN 
                    (
	                    SELECT
		                    farmacontrol_local.detallado_devoluciones.articulo_id as articulo_id,
		                    farmacontrol_local.detallado_devoluciones.caducidad as caducidad,
		                    farmacontrol_local.detallado_devoluciones.lote as lote,
		                    SUM(cantidad) AS existencia_devoluciones
	                    FROM
		                    farmacontrol_local.detallado_devoluciones
	                    JOIN farmacontrol_local.devoluciones ON
		                    farmacontrol_local.devoluciones.devolucion_id = farmacontrol_local.detallado_devoluciones.devolucion_id
	                    LEFT JOIN farmacontrol_local.existencias ON farmacontrol_local.existencias.articulo_id = farmacontrol_local.detallado_devoluciones.articulo_id
	                    WHERE
		                    fecha_terminado IS NULL
	                    AND
		                    detallado_devoluciones.articulo_id = @articulo_id and detallado_devoluciones.caducidad = farmacontrol_local.existencias.caducidad
	                    GROUP BY farmacontrol_local.detallado_devoluciones.articulo_id, farmacontrol_local.detallado_devoluciones.caducidad

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
				                    farmacontrol_local.apartados.articulo_id as articulo_id,
				                    farmacontrol_local.apartados.caducidad as caducidad,
				                    farmacontrol_local.apartados.lote as lote,
				                    SUM(cantidad) AS cantidad
			                    FROM
				                    farmacontrol_local.apartados
			                    LEFT JOIN farmacontrol_local.existencias ON farmacontrol_local.existencias.articulo_id = farmacontrol_local.apartados.articulo_id
			                    WHERE
				                    apartados.tipo = 'MERMA'
			                    AND
				                    apartados.articulo_id = @articulo_id and apartados.caducidad = farmacontrol_local.existencias.caducidad
			                    GROUP BY farmacontrol_local.apartados.articulo_id,farmacontrol_local.apartados.caducidad
		                    )
		                    UNION
		                    (
			                    SELECT
				                    UUID(),
				                    farmacontrol_local.detallado_mermas.articulo_id as articulo_id,
				                    farmacontrol_local.detallado_mermas.caducidad as caducidad,
				                    farmacontrol_local.detallado_mermas.lote as lote,
				                    cantidad
			                    FROM
				                    farmacontrol_local.detallado_mermas
			                    LEFT JOIN farmacontrol_local.existencias ON farmacontrol_local.detallado_mermas.articulo_id = farmacontrol_local.existencias.articulo_id
			                    JOIN farmacontrol_local.mermas USING(merma_id)
			                    WHERE
				                    mermas.fecha_terminado IS NULL
			                    AND
				                    detallado_mermas.articulo_id = @articulo_id and detallado_mermas.caducidad = farmacontrol_local.existencias.caducidad
		                    )
	                    ) AS new_table
	                    GROUP BY
		                    new_table.articulo_id,new_table.caducidad,new_table.lote
                    ) AS tmp_mermas
                    NATURAL LEFT JOIN 
                    (
	                    SELECT
		                    farmacontrol_local.apartados.articulo_id as articulo_id,
		                    farmacontrol_local.apartados.caducidad as caducidad,
		                    farmacontrol_local.apartados.lote as lote,
		                    SUM(cantidad) AS existencia_apartados
	                    FROM
		                    farmacontrol_local.apartados
	                    LEFT JOIN farmacontrol_local.existencias ON farmacontrol_local.existencias.articulo_id = farmacontrol_local.apartados.articulo_id
	                    WHERE
		                    tipo = 'SUCURSAL'
	                    AND
		                    apartados.articulo_id = @articulo_id and apartados.caducidad = farmacontrol_local.existencias.caducidad
	                    GROUP BY  farmacontrol_local.apartados.articulo_id, farmacontrol_local.apartados.caducidad
                    ) AS tmp_apartados
                    NATURAL LEFT JOIN 
                    (
	                    SELECT
		                    farmacontrol_local.apartados.articulo_id as articulo_id,
		                    farmacontrol_local.apartados.caducidad as caducidad,
		                    farmacontrol_local.apartados.lote as lote,
		                    SUM(cantidad) AS existencia_parcial_prepago
	                    FROM
		                    farmacontrol_local.apartados
	                    LEFT JOIN farmacontrol_local.existencias ON farmacontrol_local.existencias.articulo_id = farmacontrol_local.apartados.articulo_id
	                    WHERE
		                    tipo = 'ENTREGA_PARCIAL_PREPAGO'
	                    AND
		                    apartados.articulo_id = @articulo_id and apartados.caducidad = farmacontrol_local.existencias.caducidad
	                    GROUP BY  farmacontrol_local.apartados.articulo_id, farmacontrol_local.apartados.caducidad
                    ) AS tmp_entrega_parcial_prepago
                    NATURAL LEFT JOIN 
                    (
	                    SELECT
		                    farmacontrol_local.apartados.articulo_id as articulo_id,
		                    farmacontrol_local.apartados.caducidad as caducidad,
		                    farmacontrol_local.apartados.lote as lote,
		                    SUM(cantidad) AS existencia_cambio_fisico
	                    FROM
		                    farmacontrol_local.apartados
	                    LEFT JOIN farmacontrol_local.existencias ON farmacontrol_local.existencias.articulo_id = farmacontrol_local.apartados.articulo_id
	                    WHERE
		                    tipo = 'CAMBIO_FISICO'
	                    AND
		                    apartados.articulo_id = @articulo_id and apartados.caducidad = farmacontrol_local.existencias.caducidad
	                    GROUP BY farmacontrol_local.apartados.articulo_id, farmacontrol_local.apartados.caducidad
                    ) AS tmp_cambio_fisico
                    NATURAL LEFT JOIN 
                    (
	                    SELECT
		                    farmacontrol_local.detallado_traspasos.articulo_id as articulo_id,
		                    farmacontrol_local.detallado_traspasos.caducidad as caducidad,
		                    farmacontrol_local.detallado_traspasos.lote as lote,
		                    SUM(cantidad) AS existencia_traspasos
	                    FROM
		                    farmacontrol_local.detallado_traspasos
	                    LEFT JOIN farmacontrol_local.traspasos USING(traspaso_id)
	                    LEFT JOIN farmacontrol_local.existencias ON farmacontrol_local.existencias.articulo_id = farmacontrol_local.detallado_traspasos.articulo_id
	                    WHERE
		                    farmacontrol_local.traspasos.remote_id IS NULL
	                    AND
		                    detallado_traspasos.articulo_id = @articulo_id and detallado_traspasos.caducidad = farmacontrol_local.existencias.caducidad
	                    GROUP BY farmacontrol_local.detallado_traspasos.articulo_id, farmacontrol_local.detallado_traspasos.caducidad
                    ) AS tmp_traspasos
                    NATURAL LEFT JOIN 
                    (
	                    SELECT
		                    farmacontrol_local.detallado_ventas.articulo_id as articulo_id,
		                    farmacontrol_local.detallado_ventas.caducidad as caducidad,
		                    farmacontrol_local.detallado_ventas.lote as lote,
		                    SUM(cantidad) AS existencia_ventas
	                    FROM
		                    farmacontrol_local.detallado_ventas
	                    LEFT JOIN farmacontrol_local.ventas USING(venta_id)
	                    LEFT JOIN farmacontrol_local.existencias ON farmacontrol_local.existencias.articulo_id = farmacontrol_local.detallado_ventas.articulo_id
	                    WHERE
		                    farmacontrol_local.ventas.fecha_terminado IS NULL
	                    AND
		                    detallado_ventas.articulo_id = @articulo_id and detallado_ventas.caducidad = farmacontrol_local.existencias.caducidad
	                    GROUP BY farmacontrol_local.detallado_ventas.articulo_id, farmacontrol_local.detallado_ventas.caducidad
                    ) AS tmp_ventas
                    NATURAL LEFT JOIN 
                    (
	                    SELECT
		                    farmacontrol_local.detallado_mayoreo_ventas.articulo_id as articulo_id,
		                    farmacontrol_local.detallado_mayoreo_ventas.caducidad as caducidad,
		                    farmacontrol_local.detallado_mayoreo_ventas.lote as lote,
		                    SUM(cantidad) + (IF(cantidad = 0,SUM(cantidad_revision),0)) AS existencia_mayoreo
	                    FROM
		                    farmacontrol_local.detallado_mayoreo_ventas
	                    LEFT JOIN farmacontrol_local.mayoreo_ventas USING(mayoreo_venta_id)
	                    LEFT JOIN farmacontrol_local.existencias ON farmacontrol_local.existencias.articulo_id = farmacontrol_local.detallado_mayoreo_ventas.articulo_id
	                    WHERE
		                    mayoreo_ventas.fecha_fin_verificacion IS NULL and detallado_mayoreo_ventas.articulo_id = @articulo_id
		                    AND detallado_mayoreo_ventas.caducidad = farmacontrol_local.existencias.caducidad
	                    GROUP BY farmacontrol_local.detallado_mayoreo_ventas.articulo_id,farmacontrol_local.detallado_mayoreo_ventas.caducidad
                    ) AS tmp_mayoreo
                    WHERE
	                    articulos.articulo_id = @articulo_id AND articulos.activo = 1
                    GROUP BY tmp_existencias.articulo_id,tmp_existencias.caducidad
		
                ";

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("articulo_id", articulo_id);

                conector.Select(sql, parametros);

                result = conector.result_set;
            }
            catch (Exception excepcion)
            {
                Log_error.log(excepcion);
            }

            return result;
        }

        public DataTable get_caducidades_global(int? articulo_id)
        {
            DataTable result = new DataTable();

            try
            {
                //COALESCE((farmacontrol_global.Articulos_get_existencias_articulo(articulo_id, caducidad, 1)),0) as existencia
                /*string sql = @"
					SELECT
						
                      (  ) as existencia
					FROM
						farmacontrol_local.existencias AS principal
					WHERE
						principal.articulo_id = @articulo_id
				";*/
                string sql = @"
                    SELECT
                        CAST( tmp_existencias.caducidad AS CHAR(10) ) as caducidad,
	                    SUM((
		                    COALESCE(existencia, 0) - COALESCE(existencia_mermas, 0) - 
		                    COALESCE(existencia_devoluciones, 0) - COALESCE(existencia_apartados, 0) - 
		                    COALESCE(existencia_cambio_fisico, 0) - COALESCE(existencia_traspasos, 0) - 
		                    COALESCE(existencia_mayoreo, 0) - COALESCE(existencia_ventas, 0) - 
		                    COALESCE(existencia_parcial_prepago, 0) 
	                    )) AS existencia
  
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
		                    existencias.articulo_id = @articulo_id
	                    GROUP BY
		                    articulo_id,caducidad
                    ) AS tmp_existencias
                    NATURAL LEFT JOIN 
                    (
	                    SELECT
		                    farmacontrol_local.detallado_devoluciones.articulo_id as articulo_id,
		                    farmacontrol_local.detallado_devoluciones.caducidad as caducidad,
		                    farmacontrol_local.detallado_devoluciones.lote as lote,
		                    SUM(cantidad) AS existencia_devoluciones
	                    FROM
		                    farmacontrol_local.detallado_devoluciones
	                    JOIN farmacontrol_local.devoluciones ON
		                    farmacontrol_local.devoluciones.devolucion_id = farmacontrol_local.detallado_devoluciones.devolucion_id
	                    LEFT JOIN farmacontrol_local.existencias ON farmacontrol_local.existencias.articulo_id = farmacontrol_local.detallado_devoluciones.articulo_id
	                    WHERE
		                    fecha_terminado IS NULL
	                    AND
		                    detallado_devoluciones.articulo_id = @articulo_id and detallado_devoluciones.caducidad = farmacontrol_local.existencias.caducidad
	                    GROUP BY farmacontrol_local.detallado_devoluciones.articulo_id, farmacontrol_local.detallado_devoluciones.caducidad

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
				                    farmacontrol_local.apartados.articulo_id as articulo_id,
				                    farmacontrol_local.apartados.caducidad as caducidad,
				                    farmacontrol_local.apartados.lote as lote,
				                    SUM(cantidad) AS cantidad
			                    FROM
				                    farmacontrol_local.apartados
			                    LEFT JOIN farmacontrol_local.existencias ON farmacontrol_local.existencias.articulo_id = farmacontrol_local.apartados.articulo_id
			                    WHERE
				                    apartados.tipo = 'MERMA'
			                    AND
				                    apartados.articulo_id = @articulo_id and apartados.caducidad = farmacontrol_local.existencias.caducidad
			                    GROUP BY farmacontrol_local.apartados.articulo_id,farmacontrol_local.apartados.caducidad
		                    )
		                    UNION
		                    (
			                    SELECT
				                    UUID(),
				                    farmacontrol_local.detallado_mermas.articulo_id as articulo_id,
				                    farmacontrol_local.detallado_mermas.caducidad as caducidad,
				                    farmacontrol_local.detallado_mermas.lote as lote,
				                    cantidad
			                    FROM
				                    farmacontrol_local.detallado_mermas
			                    LEFT JOIN farmacontrol_local.existencias ON farmacontrol_local.detallado_mermas.articulo_id = farmacontrol_local.existencias.articulo_id
			                    JOIN farmacontrol_local.mermas USING(merma_id)
			                    WHERE
				                    mermas.fecha_terminado IS NULL
			                    AND
				                    detallado_mermas.articulo_id = @articulo_id and detallado_mermas.caducidad = farmacontrol_local.existencias.caducidad
		                    )
	                    ) AS new_table
	                    GROUP BY
		                    new_table.articulo_id,new_table.caducidad,new_table.lote
                    ) AS tmp_mermas
                    NATURAL LEFT JOIN 
                    (
	                    SELECT
		                    farmacontrol_local.apartados.articulo_id as articulo_id,
		                    farmacontrol_local.apartados.caducidad as caducidad,
		                    farmacontrol_local.apartados.lote as lote,
		                    SUM(cantidad) AS existencia_apartados
	                    FROM
		                    farmacontrol_local.apartados
	                    LEFT JOIN farmacontrol_local.existencias ON farmacontrol_local.existencias.articulo_id = farmacontrol_local.apartados.articulo_id
	                    WHERE
		                    tipo = 'SUCURSAL'
	                    AND
		                    apartados.articulo_id = @articulo_id and apartados.caducidad = farmacontrol_local.existencias.caducidad
	                    GROUP BY  farmacontrol_local.apartados.articulo_id, farmacontrol_local.apartados.caducidad
                    ) AS tmp_apartados
                    NATURAL LEFT JOIN 
                    (
	                    SELECT
		                    farmacontrol_local.apartados.articulo_id as articulo_id,
		                    farmacontrol_local.apartados.caducidad as caducidad,
		                    farmacontrol_local.apartados.lote as lote,
		                    SUM(cantidad) AS existencia_parcial_prepago
	                    FROM
		                    farmacontrol_local.apartados
	                    LEFT JOIN farmacontrol_local.existencias ON farmacontrol_local.existencias.articulo_id = farmacontrol_local.apartados.articulo_id
	                    WHERE
		                    tipo = 'ENTREGA_PARCIAL_PREPAGO'
	                    AND
		                    apartados.articulo_id = @articulo_id and apartados.caducidad = farmacontrol_local.existencias.caducidad
	                    GROUP BY  farmacontrol_local.apartados.articulo_id, farmacontrol_local.apartados.caducidad
                    ) AS tmp_entrega_parcial_prepago
                    NATURAL LEFT JOIN 
                    (
	                    SELECT
		                    farmacontrol_local.apartados.articulo_id as articulo_id,
		                    farmacontrol_local.apartados.caducidad as caducidad,
		                    farmacontrol_local.apartados.lote as lote,
		                    SUM(cantidad) AS existencia_cambio_fisico
	                    FROM
		                    farmacontrol_local.apartados
	                    LEFT JOIN farmacontrol_local.existencias ON farmacontrol_local.existencias.articulo_id = farmacontrol_local.apartados.articulo_id
	                    WHERE
		                    tipo = 'CAMBIO_FISICO'
	                    AND
		                    apartados.articulo_id = @articulo_id and apartados.caducidad = farmacontrol_local.existencias.caducidad
	                    GROUP BY farmacontrol_local.apartados.articulo_id, farmacontrol_local.apartados.caducidad
                    ) AS tmp_cambio_fisico
                    NATURAL LEFT JOIN 
                    (
	                    SELECT
		                    farmacontrol_local.detallado_traspasos.articulo_id as articulo_id,
		                    farmacontrol_local.detallado_traspasos.caducidad as caducidad,
		                    farmacontrol_local.detallado_traspasos.lote as lote,
		                    SUM(cantidad) AS existencia_traspasos
	                    FROM
		                    farmacontrol_local.detallado_traspasos
	                    LEFT JOIN farmacontrol_local.traspasos USING(traspaso_id)
	                    LEFT JOIN farmacontrol_local.existencias ON farmacontrol_local.existencias.articulo_id = farmacontrol_local.detallado_traspasos.articulo_id
	                    WHERE
		                    farmacontrol_local.traspasos.remote_id IS NULL
	                    AND
		                    detallado_traspasos.articulo_id = @articulo_id and detallado_traspasos.caducidad = farmacontrol_local.existencias.caducidad
	                    GROUP BY farmacontrol_local.detallado_traspasos.articulo_id, farmacontrol_local.detallado_traspasos.caducidad
                    ) AS tmp_traspasos
                    NATURAL LEFT JOIN 
                    (
	                    SELECT
		                    farmacontrol_local.detallado_ventas.articulo_id as articulo_id,
		                    farmacontrol_local.detallado_ventas.caducidad as caducidad,
		                    farmacontrol_local.detallado_ventas.lote as lote,
		                    SUM(cantidad) AS existencia_ventas
	                    FROM
		                    farmacontrol_local.detallado_ventas
	                    LEFT JOIN farmacontrol_local.ventas USING(venta_id)
	                    LEFT JOIN farmacontrol_local.existencias ON farmacontrol_local.existencias.articulo_id = farmacontrol_local.detallado_ventas.articulo_id
	                    WHERE
		                    farmacontrol_local.ventas.fecha_terminado IS NULL
	                    AND
		                    detallado_ventas.articulo_id = @articulo_id and detallado_ventas.caducidad = farmacontrol_local.existencias.caducidad
	                    GROUP BY farmacontrol_local.detallado_ventas.articulo_id, farmacontrol_local.detallado_ventas.caducidad
                    ) AS tmp_ventas
                    NATURAL LEFT JOIN 
                    (
	                    SELECT
		                    farmacontrol_local.detallado_mayoreo_ventas.articulo_id as articulo_id,
		                    farmacontrol_local.detallado_mayoreo_ventas.caducidad as caducidad,
		                    farmacontrol_local.detallado_mayoreo_ventas.lote as lote,
		                    SUM(cantidad) + (IF(cantidad = 0,SUM(cantidad_revision),0)) AS existencia_mayoreo
	                    FROM
		                    farmacontrol_local.detallado_mayoreo_ventas
	                    LEFT JOIN farmacontrol_local.mayoreo_ventas USING(mayoreo_venta_id)
	                    LEFT JOIN farmacontrol_local.existencias ON farmacontrol_local.existencias.articulo_id = farmacontrol_local.detallado_mayoreo_ventas.articulo_id
	                    WHERE
		                    mayoreo_ventas.fecha_fin_verificacion IS NULL and detallado_mayoreo_ventas.articulo_id = @articulo_id
		                    AND detallado_mayoreo_ventas.caducidad = farmacontrol_local.existencias.caducidad
	                    GROUP BY farmacontrol_local.detallado_mayoreo_ventas.articulo_id,farmacontrol_local.detallado_mayoreo_ventas.caducidad
                    ) AS tmp_mayoreo
                    WHERE
	                    articulos.articulo_id = @articulo_id AND articulos.activo = 1
                    GROUP BY tmp_existencias.articulo_id,tmp_existencias.caducidad
		
		";

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("articulo_id", articulo_id);

                conector.Select(sql, parametros);

                result = conector.result_set;
            }
            catch (Exception excepcion)
            {
                Log_error.log(excepcion);
            }

            return result;
        }

        public DTO_Articulo get_articulo(string amecop)
        {
            DTO_Articulo articulo = new DTO_Articulo();

            try
            {
                string sql = @"
					SELECT
						*
					FROM
						farmacontrol_global.articulos
					LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id)
					WHERE
						articulos_amecops.amecop = CAST(@amecop AS UNSIGNED)
                    AND   
						farmacontrol_global.articulos.activo = '1'
				";

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("amecop", amecop);

                conector.Select(sql, parametros);

                DataTable result = conector.result_set;

                if (result.Rows.Count > 0)
                {
                    articulo.Articulo_id = int.Parse(result.Rows[0]["articulo_id"].ToString());
                    articulo.activo = Convert.ToBoolean(result.Rows[0]["activo"]);
                    articulo.Amecop = result.Rows[0]["amecop"].ToString();
                    articulo.Nombre = result.Rows[0]["nombre"].ToString();
                    articulo.Pct_descuento = decimal.Parse(result.Rows[0]["pct_descuento"].ToString());
                    articulo.Pct_iva = decimal.Parse(result.Rows[0]["pct_iva"].ToString());
                    articulo.Precio_costo = decimal.Parse(result.Rows[0]["precio_costo"].ToString());
                    articulo.Precio_publico = Convert.ToDecimal(result.Rows[0]["precio_publico"]);
                    articulo.Caducidades = get_caducidades_global(articulo.Articulo_id);
                    articulo.ieps = Convert.ToDecimal(result.Rows[0]["ieps"]);
                    articulo.tipo_ieps = result.Rows[0]["tipo_ieps"].ToString();

                    long? nullabled = null;
                    articulo.clase_antibiotico_id = (result.Rows[0]["clase_antibiotico_id"].ToString().Trim().Equals("")) ? nullabled : Convert.ToInt64(result.Rows[0]["clase_antibiotico_id"]);
                }
            }
            catch (Exception exception)
            {
                Log_error.log(exception);
            }

            return articulo;
        }

        public DTO_Articulo get_articulo(long articulo_id)
        {
            DTO_Articulo articulo = new DTO_Articulo();

            try
            {
                string sql = @"
					SELECT
						*
					FROM
						farmacontrol_global.articulos
					LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id)
					WHERE
						articulo_id = @articulo_id
				";

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("articulo_id", articulo_id);

                conector.Select(sql, parametros);

                DataTable result = conector.result_set;

                if (result.Rows.Count > 0)
                {
                    articulo.Articulo_id = int.Parse(result.Rows[0]["articulo_id"].ToString());
                    articulo.Amecop = result.Rows[0]["amecop"].ToString();
                    articulo.activo = Convert.ToBoolean(result.Rows[0]["activo"]);
                    articulo.Nombre = result.Rows[0]["nombre"].ToString();
                    articulo.Pct_descuento = decimal.Parse(result.Rows[0]["pct_descuento"].ToString());
                    articulo.Pct_iva = decimal.Parse(result.Rows[0]["pct_iva"].ToString());
                    articulo.Precio_costo = decimal.Parse(result.Rows[0]["precio_costo"].ToString());
                    articulo.Precio_publico = Convert.ToDecimal(result.Rows[0]["precio_publico"]);
                    articulo.Caducidades = get_caducidades(articulo.Articulo_id);
                    articulo.ieps = Convert.ToDecimal(result.Rows[0]["ieps"]);
                    articulo.tipo_ieps = result.Rows[0]["tipo_ieps"].ToString();
                }
            }
            catch (Exception exception)
            {
                Log_error.log(exception);
            }

            return articulo;
        }

        public List<Busqueda_articulos_existencias> get_articulos_data(string nombre, bool inactivos = false)
        {
            List<Busqueda_articulos_existencias> lista_productos = new List<Busqueda_articulos_existencias>();

            string articulo_ids;

            string sql = @"
                SELECT
					DISTINCT(articulo_id) AS articulo_id
				FROM
					farmacontrol_global.articulos
                LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id)
				WHERE
					(articulos.nombre LIKE @nombre OR CAST(articulos.amecop_original AS UNSIGNED) = CAST(REPLACE(@nombre, '%', '') as UNSIGNED))
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("nombre", "%" + nombre.Replace(" ", "%") + "%");

            conector.Select(sql, parametros);

            string articulos_ids_string = "";

            if (conector.result_set.Rows.Count > 0)
            {
                var resulset = conector.result_set.Rows;

                foreach (DataRow row in resulset)
                {
                    if (articulos_ids_string.Equals(""))
                    {
                        articulos_ids_string = row["articulo_id"].ToString();
                    }
                    else
                    {
                        articulos_ids_string += ", " + row["articulo_id"];
                    }
                }
            }

            if (conector.result_set.Rows.Count > 0)
            {
                try
                {
                    if (!articulos_ids_string.Equals(""))
                    {
                        #region 
                        /*
                               sql = string.Format(@"
                                   SELECT
                                       articulos.articulo_id AS articulo_id,
                                       articulos.pct_descuento AS pct_descuento,
                                       articulos.activo AS activo,
                                       CAST( articulos.amecop_original as UNSIGNED ) AS amecop,
                                       nombre,
                                       CAST( tmp_existencias.caducidad AS CHAR(10) )  AS caducidad,
                                       CAST( tmp_existencias.caducidad AS CHAR(10) ) AS caducidad_sin_formato,
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
                                       COALESCE(existencia, 0) - COALESCE(existencia_mermas, 0) - COALESCE(existencia_devoluciones, 0) - COALESCE(existencia_apartados, 0) - COALESCE(existencia_cambio_fisico, 0) - COALESCE(existencia_traspasos, 0) - COALESCE(existencia_mayoreo, 0) - COALESCE(existencia_ventas, 0) - COALESCE(existencia_parcial_prepago, 0) AS existencia_vendible,
                                       IF(articulos.clase_antibiotico_id IS NULL, 0, 1) As es_antibiotico
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
                                           mayoreo_ventas.fecha_fin_verificacion IS NULL AND detallado_mayoreo_ventas.articulo_id IN ({0})
                                       GROUP BY articulo_id,caducidad,lote
                                   ) AS tmp_mayoreo
                                   WHERE
                                       articulos.articulo_id IN ({0})
                                   {1}
                                   GROUP BY articulos.articulo_id, tmp_existencias.caducidad, tmp_existencias.lote
                                ", articulos_ids_string, (inactivos) ? "" : "AND articulos.activo = 1");
                        */
                        #endregion
                        sql = string.Format(@"
                                   	SELECT
                                       articulos.articulo_id AS articulo_id,
                                       articulos.pct_descuento AS pct_descuento,
                                       articulos.activo AS activo,
                                       CAST( articulos.amecop_original as UNSIGNED ) AS amecop,
                                       nombre,
                                       CAST( tmp_existencias.caducidad AS CHAR(10) )  AS caducidad,
                                       CAST( tmp_existencias.caducidad AS CHAR(10) ) AS caducidad_sin_formato,
                                       COALESCE(tmp_existencias.lote, ' ') AS lote,
                                       COALESCE(tmp_existencias.existencia, 0) AS existencia_total,
                                        '0' AS existencia_devoluciones,
                                        '0' AS existencia_mermas,
                                        '0' AS existencia_cambio_fisico,
                                       COALESCE(existencia_apartados, 0) AS existencia_apartados,
                                       COALESCE(existencia_traspasos, 0) AS existencia_traspasos,
                                       '0' AS existencia_mayoreo,
                                       '0' AS existencia_parcial_prepago,
                                       '0' AS existencia_ventas,
                                       (articulos.precio_publico * articulos.pct_iva ) + articulos.precio_publico AS precio_publico,
                                       COALESCE(existencia, 0) - COALESCE(existencia_apartados, 0) - COALESCE(existencia_traspasos, 0)  AS existencia_vendible,
                                       IF(articulos.clase_antibiotico_id IS NULL, 0, 1) As es_antibiotico
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
                                           SUM(cantidad) AS existencia_traspasos
                                       FROM
                                           farmacontrol_local.traspasos
                                       LEFT JOIN farmacontrol_local.detallado_traspasos USING(traspaso_id)
                                       WHERE
                                           farmacontrol_local.traspasos.remote_id IS NULL
                                       AND
                                           detallado_traspasos.articulo_id IN ({0})
                                       GROUP BY articulo_id, caducidad, lote
                                   ) AS tmp_traspasos
                                   WHERE
                                       articulos.articulo_id IN ({0})
                                   {1}
                                   GROUP BY articulos.articulo_id, tmp_existencias.caducidad, tmp_existencias.lote
                                ", articulos_ids_string, (inactivos) ? "" : "AND articulos.activo = 1");




                        Console.Write(sql);

                        parametros = new Dictionary<string, object>();

                        conector.Select(sql, parametros);

                        execution_time = conector.execution_time;

                        var result_query = conector.result_set;

                        if (result_query.Rows.Count > 0)
                        {
                            foreach (DataRow row in result_query.Rows)
                            {
                                Busqueda_articulos_existencias articulo = new Busqueda_articulos_existencias();
                                articulo.amecop = row["amecop"].ToString();
                                articulo.articulo_id = Convert.ToInt64(row["articulo_id"]);


                                if (row["caducidad"].ToString().Equals("0000-00-00") || DBNull.Value.Equals(row["caducidad"]))
                                {
                                    row["caducidad"] = "SIN CAD";
                                }

                                articulo.caducidad = (row["caducidad"].ToString().Equals("SIN CAD")) ? "SIN CAD" : Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");

                                articulo.caducidad_sin_formato = row["caducidad"].ToString();

                                articulo.existencia_apartados = Convert.ToInt64(row["existencia_apartados"]);
                                articulo.existencia_cambio_fisico = Convert.ToInt64(row["existencia_cambio_fisico"]);
                                articulo.existencia_devoluciones = Convert.ToInt64(row["existencia_devoluciones"]);
                                articulo.existencia_mayoreo = Convert.ToInt64(row["existencia_mayoreo"]);
                                articulo.existencia_mermas = Convert.ToInt64(row["existencia_mermas"]);
                                articulo.existencia_prepago = Convert.ToInt64(row["existencia_parcial_prepago"]);
                                articulo.existencia_total = Convert.ToInt64(row["existencia_total"]);
                                articulo.existencia_traspasos = Convert.ToInt64(row["existencia_traspasos"]);
                                articulo.existencia_vendible = Convert.ToInt64(row["existencia_vendible"]);
                                articulo.existencia_ventas = Convert.ToInt64(row["existencia_ventas"]);
                                articulo.lote = row["lote"].ToString();
                                articulo.nombre = row["nombre"].ToString();
                                articulo.precio_publico = Convert.ToDecimal(row["precio_publico"]);
                                articulo.activo = Convert.ToInt32(row["activo"]);
                                articulo.es_antibiotico = Convert.ToBoolean(row["es_antibiotico"]);
                                articulo.pct_descuento = Convert.ToDecimal(row["pct_descuento"]);

                                lista_productos.Add(articulo);
                            }
                        }

                    }

                }
                catch (Exception exception)
                {
                    Log_error.log(exception);
                }
            }

            return lista_productos;
        }
        public List<Busqueda_articulos_existencias> get_articulos_data_existencias(string nombre, bool inactivos = false)
        {
            List<Busqueda_articulos_existencias> lista_productos = new List<Busqueda_articulos_existencias>();

            //int isInicial = ((nombre.Length > 1) ? 0 : 1);
            int isInicial = nombre.Length;
            int len = isInicial > 4 ? 0 : 1;
            /*
             string sql = @"
				SELECT
					SUBSTRING_INDEX(GROUP_CONCAT(DISTINCT(articulo_id)), ',', 100) AS articulo_ids
				FROM
					farmacontrol_global.articulos
                LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id)
				WHERE
					articulos.nombre LIKE @nombre	
                OR
                    articulos_amecops.amecop LIKE @nombre
			";
            */
            // string sql = "Articulos_get_articulos_data_existencias";

            /*
              IF(tmp_existencias.caducidad = '0000-00-00', 'SIN CAD', COALESCE(tmp_existencias.caducidad, 'SIN CAD') ) AS caducidad,
              IF(tmp_existencias.caducidad = '0000-00-00', 'SIN CAD', COALESCE(tmp_existencias.caducidad, 'SIN CAD') ) AS caducidad_sin_formato,
             */

            string sql = @"

                SELECT
                    articulos.articulo_id AS articulo_id,
                    articulos.pct_descuento AS pct_descuento,
                    articulos.activo AS activo,
                    (
                        SELECT
                            amecop
                        FROM
                            farmacontrol_global.articulos_amecops
                        WHERE
                            articulos_amecops.articulo_id = articulos.articulo_id
                        ORDER BY articulos_amecops.amecop_principal DESC
                        LIMIT 1
                    ) AS amecop,
                    articulos.nombre,
                    CAST( tmp_existencias.caducidad AS CHAR(10) )  AS caducidad,
                    CAST( tmp_existencias.caducidad AS CHAR(10) ) AS caducidad_sin_formato,
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
                    COALESCE(existencia, 0) - COALESCE(existencia_mermas, 0) - COALESCE(existencia_devoluciones, 0) - COALESCE(existencia_apartados, 0) - COALESCE(existencia_cambio_fisico, 0) - COALESCE(existencia_traspasos, 0) - COALESCE(existencia_mayoreo, 0) - COALESCE(existencia_ventas, 0) - COALESCE(existencia_parcial_prepago, 0) AS existencia_vendible,
                    IF(articulos.clase_antibiotico_id IS NULL, 0, 1) As es_antibiotico
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
                        existencias.articulo_id IN (SELECT DISTINCT(articulo_id) FROM farmacontrol_global.articulos LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id) left JOIN farmacontrol_local.existencias USING(articulo_id)	WHERE (((@len = 0 and articulos.nombre LIKE CONCAT ('%', @nombre, '%')) or articulos.nombre LIKE CONCAT (@nombre, '%')) OR articulos_amecops.amecop = CAST(@nombre as unsigned)) and existencia > 0)
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
                        detallado_devoluciones.articulo_id IN (SELECT DISTINCT(articulo_id) FROM farmacontrol_global.articulos LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id) left JOIN farmacontrol_local.existencias USING(articulo_id)	WHERE (((@len = 0 and articulos.nombre LIKE CONCAT ('%', @nombre, '%')) or articulos.nombre LIKE CONCAT (@nombre, '%')) OR articulos_amecops.amecop = CAST(@nombre as unsigned)) and existencia > 0)
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
                                apartados.articulo_id IN (SELECT DISTINCT(articulo_id) FROM farmacontrol_global.articulos LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id) left JOIN farmacontrol_local.existencias USING(articulo_id)	WHERE (((@len = 0 and articulos.nombre LIKE CONCAT ('%', @nombre, '%')) or articulos.nombre LIKE CONCAT (@nombre, '%')) OR articulos_amecops.amecop = CAST(@nombre as unsigned)) and existencia > 0)
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
                                detallado_mermas.articulo_id IN (SELECT DISTINCT(articulo_id) FROM farmacontrol_global.articulos LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id) left JOIN farmacontrol_local.existencias USING(articulo_id)	WHERE (((@len = 0 and articulos.nombre LIKE CONCAT ('%', @nombre, '%')) or articulos.nombre LIKE CONCAT (@nombre, '%')) OR articulos_amecops.amecop = CAST(@nombre as unsigned)) and existencia > 0)
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
                        apartados.articulo_id IN (SELECT DISTINCT(articulo_id) FROM farmacontrol_global.articulos LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id) left JOIN farmacontrol_local.existencias USING(articulo_id)	WHERE (((@len = 0 and articulos.nombre LIKE CONCAT ('%', @nombre, '%')) or articulos.nombre LIKE CONCAT (@nombre, '%')) OR articulos_amecops.amecop = CAST(@nombre as unsigned)) and existencia > 0)
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
                        apartados.articulo_id IN (SELECT DISTINCT(articulo_id) FROM farmacontrol_global.articulos LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id) left JOIN farmacontrol_local.existencias USING(articulo_id)	WHERE (((@len = 0 and articulos.nombre LIKE CONCAT ('%', @nombre, '%')) or articulos.nombre LIKE CONCAT (@nombre, '%')) OR articulos_amecops.amecop = CAST(@nombre as unsigned)) and existencia > 0)
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
                        apartados.articulo_id IN (SELECT DISTINCT(articulo_id) FROM farmacontrol_global.articulos LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id) left JOIN farmacontrol_local.existencias USING(articulo_id)	WHERE (((@len = 0 and articulos.nombre LIKE CONCAT ('%', @nombre, '%')) or articulos.nombre LIKE CONCAT (@nombre, '%')) OR articulos_amecops.amecop = CAST(@nombre as unsigned)) and existencia > 0)
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
                        detallado_traspasos.articulo_id IN (SELECT DISTINCT(articulo_id) FROM farmacontrol_global.articulos LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id) left JOIN farmacontrol_local.existencias USING(articulo_id)	WHERE (((@len = 0 and articulos.nombre LIKE CONCAT ('%', @nombre, '%')) or articulos.nombre LIKE CONCAT (@nombre, '%')) OR articulos_amecops.amecop = CAST(@nombre as unsigned)) and existencia > 0)
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
                        detallado_ventas.articulo_id IN (SELECT DISTINCT(articulo_id) FROM farmacontrol_global.articulos LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id) left JOIN farmacontrol_local.existencias USING(articulo_id)	WHERE (((@len = 0 and articulos.nombre LIKE CONCAT ('%', @nombre, '%')) or articulos.nombre LIKE CONCAT (@nombre, '%')) OR articulos_amecops.amecop = CAST(@nombre as unsigned)) and existencia > 0)
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
                        mayoreo_ventas.fecha_fin_verificacion IS NULL and detallado_mayoreo_ventas.articulo_id IN (SELECT DISTINCT(articulo_id) FROM farmacontrol_global.articulos LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id) left JOIN farmacontrol_local.existencias USING(articulo_id)	WHERE (((@len = 0 and articulos.nombre LIKE CONCAT ('%', @nombre, '%')) or articulos.nombre LIKE CONCAT (@nombre, '%')) OR articulos_amecops.amecop = CAST(@nombre as unsigned)) and existencia > 0)
                    GROUP BY articulo_id,caducidad,lote
                ) AS tmp_mayoreo
                WHERE
                    articulos.articulo_id IN (SELECT DISTINCT(articulo_id) FROM farmacontrol_global.articulos LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id) left JOIN farmacontrol_local.existencias USING(articulo_id)	WHERE (((@len = 0 and articulos.nombre LIKE CONCAT ('%', @nombre, '%')) or articulos.nombre LIKE CONCAT (@nombre, '%')) OR articulos_amecops.amecop = CAST(@nombre as unsigned)) and existencia > 0)
                ORDER BY articulos.nombre;

             ";
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("nombre", nombre.Replace(" ", "%") + "%");
            parametros.Add("len", len);
            //parametros.Add("isInicial", isInicial);

            conector.Select(sql, parametros);
            //conector.Call(sql, parametros);

            execution_time = conector.execution_time;

            if (conector.result_set.Rows.Count > 0)
            {
                try
                {
                    var result_query = conector.result_set;

                    if (result_query.Rows.Count > 0)
                    {
                        foreach (DataRow row in result_query.Rows)
                        {
                            Busqueda_articulos_existencias articulo = new Busqueda_articulos_existencias();
                            articulo.amecop = row["amecop"].ToString();
                            articulo.articulo_id = Convert.ToInt64(row["articulo_id"]);

                            if (row["caducidad"].ToString().Equals("0000-00-00") || DBNull.Value.Equals(row["caducidad"]))
                            {
                                row["caducidad"] = "SIN CAD";
                            }

                            articulo.caducidad = (row["caducidad"].ToString().Equals("SIN CAD")) ? "SIN CAD" : Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");
                            articulo.caducidad_sin_formato = row["caducidad"].ToString();


                            articulo.existencia_apartados = Convert.ToInt64(row["existencia_apartados"]);
                            articulo.existencia_cambio_fisico = Convert.ToInt64(row["existencia_cambio_fisico"]);
                            articulo.existencia_devoluciones = Convert.ToInt64(row["existencia_devoluciones"]);
                            articulo.existencia_mayoreo = Convert.ToInt64(row["existencia_mayoreo"]);
                            articulo.existencia_mermas = Convert.ToInt64(row["existencia_mermas"]);
                            articulo.existencia_prepago = Convert.ToInt64(row["existencia_parcial_prepago"]);
                            articulo.existencia_total = Convert.ToInt64(row["existencia_total"]);
                            articulo.existencia_traspasos = Convert.ToInt64(row["existencia_traspasos"]);
                            articulo.existencia_vendible = Convert.ToInt64(row["existencia_vendible"]);
                            articulo.existencia_ventas = Convert.ToInt64(row["existencia_ventas"]);
                            articulo.lote = row["lote"].ToString();
                            articulo.nombre = row["nombre"].ToString();
                            articulo.precio_publico = Convert.ToDecimal(row["precio_publico"]);
                            articulo.activo = Convert.ToInt32(row["activo"]);
                            articulo.es_antibiotico = Convert.ToBoolean(row["es_antibiotico"]);
                            articulo.pct_descuento = Convert.ToDecimal(row["pct_descuento"]);

                            lista_productos.Add(articulo);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Log_error.log(exception);
                }
            }
            lista_productos = lista_productos.OrderBy(a => a.nombre).ToList();
            return lista_productos;
        }
        //FUNCION NUEVA QUE OBTIENE LA INFORMACION DE UN PRODUCTO
        public DataTable get_informacion_codigo(string amecop, string caducidad, string lote)
        {
            DataTable result = new DataTable();
            try
            {
                DAO_Articulos dao_articulos = new DAO_Articulos();

                var articulo = dao_articulos.get_articulo(amecop);

                if (get_afecta_existencias((int)articulo.Articulo_id))
                {
                    string sql = string.Format(@"
							SELECT
                                articulo_id,
                                CAST(amecop_original AS UNSIGNED ) as amecop,
                                nombre,
                                @caducidad as caducidad,
                                lote,
                                precio_publico,
                                pct_iva,
                                pct_descuento,
                                ieps
                    
                            FROM 
                                farmacontrol_global.articulos
                            LEFT JOIN
                                farmacontrol_local.existencias
                            USING(articulo_id)
                            WHERE
                               articulo_id ={0}
                            AND
                               caducidad = @caducidad
                            AND
                               lote = @lote
                            AND
                                activo = 1
						 ", (long)articulo.Articulo_id);

                    Dictionary<string, object> parametros = new Dictionary<string, object>();
                    parametros.Add("caducidad", caducidad);
                    parametros.Add("lote", lote);

                    conector.Select(sql, parametros);

                    result = conector.result_set;

                }
            }
            catch (Exception ex)
            {
                Log_error.log(ex);
            }
            return result;
        }


        public Int32 get_existencia_total(string amecop)
        {
            Int32 existencia_total = 0;

            string sql = @"
					SELECT
						COALESCE(SUM(existencia),0) AS existencia
					FROM
						farmacontrol_local.existencias
					LEFT JOIN articulos USING(articulo_id)
					LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id)
					WHERE
						articulos_amecops.amecop = @amecop
				
				";

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("amecop", amecop);

            conector.Select(sql, parametros);

            var result = conector.result_set;

            if (result.Rows.Count > 0)
            {
                existencia_total = Convert.ToInt32(result.Rows[0]["existencia"]);
            }
            else
            {
                existencia_total = 0;
            }

            return existencia_total;

        }

        public string get_nombre_codigo(string amecop)
        {
            string nombre_codigo = "";

            string sql = @"
					SELECT
					   replace(nombre,'*','') as nombre
					FROM
						farmacontrol_global.articulos
					WHERE
						amecop_original = @amecop
				
				";

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("amecop", amecop);

            conector.Select(sql, parametros);

            var result = conector.result_set;

            if (result.Rows.Count > 0)
            {
                nombre_codigo = result.Rows[0]["nombre"].ToString();
            }
            else
            {
                nombre_codigo = "SIN CODIGO EXISTENCIA";
            }

            return nombre_codigo;
        }

    }
}
