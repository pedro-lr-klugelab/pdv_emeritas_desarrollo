using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;
using System.Data;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Formulas
	{
		Conector conector = new Conector();

        public bool registrar_formula_elaborada(long empleado_id, string formula_id, List<DTO_Detallado_formula_elaborada> detallado, string comentarios)
        {
            string sql = @"
        UPDATE
            farmacontrol_global.formulas
        SET
            empleado_id_elabora = @empleado_id,
            fecha_elaborado = @fecha_elaborado,
            comentarios_elaboracion = @comentarios
        WHERE
            formula_id = @formula_id
    ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("empleado_id", empleado_id);
            parametros.Add("formula_id", formula_id);
            parametros.Add("comentarios", comentarios);
            parametros.Add("fecha_elaborado", Misc_helper.fecha());

            conector.Update(sql, parametros);

            DAO_Cola_operaciones.insertar_cola_operaciones(
                empleado_id, "rest/formulas", "update_formula_elaborada", parametros, "PARA SERVIDOR PRINCIPAL"
            );

            List<Tuple<int, decimal>> articulos_volumen = new List<Tuple<int, decimal>>();

            foreach (DTO_Detallado_formula_elaborada item in detallado)
            {
                articulos_volumen.Add(new Tuple<int, decimal>((int)item.articulo_id, item.cantidad));
            }

            if (articulos_volumen.Count > 0)
            {
                DAO_Ajustes_existencias dao_articulos = new DAO_Ajustes_existencias();
                dao_articulos.descontar_volumen_materias_primas(articulos_volumen);
            }

            return true;
        }



        public static DTO_Formula_rest get_informacion_formula_rest(long sucursal_id, string formula_id)
		{
			DTO_Formula_rest formula = new DTO_Formula_rest();

			Rest_parameters parametros = new Rest_parameters();
			parametros.Add("sucursal_id",sucursal_id);
			parametros.Add("formula_id",formula_id);
			formula = Rest_helper.make_request<DTO_Formula_rest>("formulas/get_informacion_formula",parametros);
			return formula;
		}

		public List<DTO_Formulas_pendientes> get_formulas_pendientes()
		{
			List<DTO_Formulas_pendientes> lista_pendientes = new List<DTO_Formulas_pendientes>();

			string sql = @"
				SELECT
					formulas.*,
					sucursales.nombre AS sucursal
				FROM
					farmacontrol_global.formulas
				JOIN farmacontrol_global.sucursales USING(sucursal_id)
				WHERE
					fecha_elaborado IS NULL
				GROUP BY formula_id
			";

			conector.Select(sql,null);

			if(conector.result_set.Rows.Count > 0)
			{
				foreach(DataRow row in conector.result_set.Rows)
				{
					DTO_Formulas_pendientes pendiente = new DTO_Formulas_pendientes();
					pendiente.formula_id = 	row["formula_id"].ToString();
					pendiente.sucursal_folio = Convert.ToInt64(row["sucursal_folio"]);
					pendiente.sucursal = row["sucursal"].ToString();
					pendiente.sucursal_id = Convert.ToInt64(row["sucursal_id"]);
					pendiente.fecha_creado = Convert.ToDateTime(row["fecha_creado"]);
					pendiente.detallado = get_detallado_formulas(pendiente.formula_id);

					lista_pendientes.Add(pendiente);
				}

				foreach (DTO_Formulas_pendientes formula in lista_pendientes)
				{
					bool tengo_existencias_suficientes = true;
					string articulos_ids = "";

					foreach (DTO_Detallado_formulas det in formula.detallado)
					{
						if (det.articulo_id != null)
						{
							articulos_ids += (articulos_ids.Equals("")) ? det.articulo_id.ToString() : ", " + det.articulo_id;
						}
					}

					if (!articulos_ids.Equals(""))
					{
						DAO_Existencias dao_existencias = new DAO_Existencias();
						List<DTO_Existencia> lista_existencias_local = new List<DTO_Existencia>();
						lista_existencias_local = dao_existencias.get_existencias_articulos_formula(articulos_ids);

						foreach (DTO_Detallado_formulas det in formula.detallado)
						{
							if (det.articulo_id != null)
							{
								bool encontrado = false;

								foreach (DTO_Existencia existencia in lista_existencias_local)
								{
									if (existencia.articulo_id == det.articulo_id)
									{
										encontrado = true;

										if (existencia.existencia < det.cantidad)
										{
											tengo_existencias_suficientes = false;
											break;
										}
									}
								}

								if (encontrado == false)
								{
									tengo_existencias_suficientes = false;
									break;
								}
							}
						}

						if (articulos_ids.Equals("") || tengo_existencias_suficientes)
						{
							formula.status = "POR ELABORAR";
						}
						else
						{
							formula.status = "FALTAN EXISTENCIAS";
						}
					}
					else
					{
						formula.status = "POR ELABORAR";
					}
				}

			}

			return lista_pendientes;
		}

		public DTO_Validacion formula_disponible(string formula_id)
		{
			DTO_Validacion val = new DTO_Validacion();

			string sql = @"
				SELECT
					sucursal_folio, 
					venta_id,
					COALESCE(tmp.registrado, 0) AS registrados
				FROM
					farmacontrol_global.formulas
				LEFT JOIN
				(
					SELECT
						@sucursal_id AS sucursal_id,
						@sucursal_folio AS sucursal_folio,
						COUNT(detallado_venta_id) AS registrado
					FROM
						farmacontrol_local.detallado_ventas
					JOIN farmacontrol_local.ventas USING(venta_id)
					WHERE
						articulo_id = @formula_magistral_articulo_id
					AND
						lote = @sucursal_folio
				) AS tmp USING(sucursal_folio)
				WHERE
					sucursal_folio = @sucursal_folio
				AND
					formulas.sucursal_id = @sucursal_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("sucursal_folio",formula_id);
			parametros.Add("sucursal_id",Convert.ToInt32(Config_helper.get_config_local("sucursal_id")));

			parametros.Add("formula_magistral_articulo_id",Convert.ToInt64(Config_helper.get_config_global("formula_magistral_articulo_id")));

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				var row = conector.result_set.Rows[0];

				if(row["venta_id"].ToString().Equals("") && Convert.ToInt64(row["registrados"]) == 0)
				{
					val.status = true;	
				}
				else
				{
					val.status = false;
					val.informacion = "Esta formula ya ha sido añadida a una venta";	
				}
			}
			else
			{
				val.status = true;
			}

			return val;
		}

		public List<DTO_Detallado_formulas> get_detallado_formulas(string formula_id)
		{
			List<DTO_Detallado_formulas> detallado = new List<DTO_Detallado_formulas>();

			string sql = @"
				SELECT
					COALESCE( 
						(
						SELECT
							amecop
						FROM
							farmacontrol_global.articulos_amecops
						WHERE
							articulos_amecops.articulo_id = detallado_formulas.articulo_id
						ORDER BY articulos_amecops.amecop_principal DESC
						LIMIT 1
					), 'N/A' ) AS amecop, 
					(
						IF(detallado_formulas.articulo_id IS NULL, materias_primas.nombre, articulos.nombre)
					)as nombre,
					detallado_formula_id,
					formula_id,
					detallado_formulas.materia_prima_id AS materia_prima_id,
					detallado_formulas.articulo_id AS articulo_id,
					detallado_formulas.precio_publico AS precio_publico,
					importe,
					cantidad,
					subtotal,
					detallado_formulas.comentarios AS comentarios,
					total
				FROM
					farmacontrol_global.detallado_formulas
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				LEFT JOIN farmacontrol_global.materias_primas USING(materia_prima_id)
				WHERE
					formula_id = @formula_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("formula_id",formula_id);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				foreach(DataRow row in conector.result_set.Rows)
				{
					DTO_Detallado_formulas det = new DTO_Detallado_formulas();
					det.detallado_formula_id = Convert.ToInt64(row["detallado_formula_id"]);
					det.formula_id = row["formula_id"].ToString();
					det.amecop = row["amecop"].ToString();
					det.nombre = row["nombre"].ToString();

					long? nullable = null;

					det.materia_prima_id = (row["materia_prima_id"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["materia_prima_id"]);
					det.articulo_id = (row["articulo_id"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["articulo_id"]);
					det.precio_publico = Convert.ToDecimal(row["precio_publico"]);
					det.importe = Convert.ToDecimal(row["importe"]);
					det.cantidad = Convert.ToDecimal(row["cantidad"]);
					det.subtotal = Convert.ToDecimal(row["subtotal"]);
					det.comentarios = row["comentarios"].ToString();
					det.total = Convert.ToDecimal(row["total"]);

					detallado.Add(det);
				}
			}

			return detallado;
		}

		public DTO_Formula get_informacion_formula(string formula_id)
		{	
			DTO_Formula formula = new DTO_Formula();

			string sql = @"
				SELECT
					*
				FROM
					farmacontrol_global.formulas
				WHERE
					formula_id = @formula_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("formula_id",formula_id);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				var row = conector.result_set.Rows[0];

				formula.formula_id = row["formula_id"].ToString();
				formula.sucursal_id = Convert.ToInt64(row["sucursal_id"]);
				formula.sucursal_folio = Convert.ToInt64(row["sucursal_folio"]);
				long? nullable = null;
				formula.venta_id = (row["venta_id"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["venta_id"]);
				formula.empleado_id = Convert.ToInt64(row["empleado_id"]);
				formula.empleado_id_elabora = (row["empleado_id_elabora"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["empleado_id_elabora"]);
				formula.fecha_creado = Convert.ToDateTime(row["fecha_creado"]);
				
				DateTime? date_null = null;
				formula.fecha_elaborado = (row["fecha_elaborado"].ToString().Equals("")) ? date_null : Convert.ToDateTime(row["fecha_elaborado"]);

				formula.nombre_cliente = row["nombre_cliente"].ToString();
				formula.nombre_doctor = row["nombre_doctor"].ToString();
				formula.comentarios = row["comentarios"].ToString();
				formula.comentarios_elaboracion = row["comentarios_elaboracion"].ToString();
			}


			return formula;
		}

		public DTO_Validacion registrar_formula(long empleado_id, string cliente, string doctor, List<DTO_Detallado_formulas> detallado, string comentarios, int sucursal_id)
		{
			DTO_Validacion val = new DTO_Validacion();

			try
			{
				string sql = "";

				string uuid = Misc_helper.uuid().ToUpper();

				string fecha_creado = Misc_helper.fecha();

				sql = @"
					INSERT INTO
						farmacontrol_global.formulas
					SET
						formula_id = @formula_id,
						sucursal_id = @sucursal_id,
						sucursal_folio = 
							(
								SELECT 
									COALESCE( (MAX(sucursal_folio) + 1), 1) AS folio
								FROM
									farmacontrol_global.formulas AS tmp
								WHERE
									sucursal_id = @sucursal_id
							),
						empleado_id = @empleado_id,
						fecha_creado = @fecha_creado,
						nombre_cliente = @cliente,
						nombre_doctor = @doctor,
						comentarios = @comentarios
				";

				Dictionary<string,object> parametros = new Dictionary<string,object>();
				parametros.Add("formula_id",uuid);
				parametros.Add("sucursal_id",sucursal_id);
				parametros.Add("empleado_id",empleado_id);
				parametros.Add("fecha_creado",fecha_creado);
				parametros.Add("cliente",cliente);
				parametros.Add("doctor",doctor);
				parametros.Add("comentarios",comentarios);

				conector.Insert(sql,parametros);

				foreach(DTO_Detallado_formulas item in detallado)
				{
					sql = @"
						INSERT INTO
							farmacontrol_global.detallado_formulas
						SET
							formula_id = @formula_id,
							materia_prima_id = @materia_prima_id,
							articulo_id = @articulo_id,
							precio_publico = @precio_publico,
							importe = @importe,
							cantidad = @cantidad,
							subtotal = @subtotal,
							comentarios = @comentarios,
							total = @total
					";

					parametros = new Dictionary<string,object>();
					parametros.Add("formula_id",uuid);
					parametros.Add("materia_prima_id",item.materia_prima_id);
					parametros.Add("articulo_id",item.articulo_id);
					parametros.Add("precio_publico",item.precio_publico);
					parametros.Add("importe",item.importe);
					parametros.Add("cantidad",item.cantidad);
					parametros.Add("subtotal",item.subtotal);
					parametros.Add("comentarios",item.comentarios);
					parametros.Add("total",item.total);

					conector.Insert(sql,parametros);
				}

				parametros = new Dictionary<string, object>();
				parametros.Add("formula_id", uuid);
				parametros.Add("sucursal_id", Convert.ToInt64(Config_helper.get_config_local("sucursal_id")));

				DAO_Cola_operaciones.insertar_cola_operaciones(empleado_id, "rest/formulas", "registrar_formula", parametros, "ENVIO A SERVIDOR PRINCIPAL");

				val.status = true;
				val.elemento_nombre = uuid;
			}
			catch(Exception ex)
			{
				val.status = false;
				Log_error.log(ex);
			}

			return val;
		}
	}
}
