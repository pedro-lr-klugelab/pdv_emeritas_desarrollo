using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Materias_primas
	{
		Conector conector = new Conector();

		public decimal get_precio_publico_conversion(string unidad, long materia_prima_id)
		{
			string sql = @"
				SELECT
					(materias_primas.precio_publico * unidades_equivalencias.cantidad) AS precio_publico
				FROM
					farmacontrol_global.materias_primas
				JOIN farmacontrol_global.unidades_equivalencias ON
					unidades_equivalencias.unidad_base = materias_primas.unidad
				WHERE
					materias_primas.materia_prima_id = @materia_prima_id
				AND
					unidades_equivalencias.unidad = @unidad
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("materia_prima_id",materia_prima_id);
			parametros.Add("unidad",unidad);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				return Convert.ToDecimal(conector.result_set.Rows[0]["precio_publico"]);
			}

			return 0;
		}

		public List<string> get_conversion_unidades(string unidad)
		{
			List<string> conversiones = new List<string>();

			string sql = @"
				SELECT
					GROUP_CONCAT(unidad) AS unidades
				FROM
					farmacontrol_global.unidades_equivalencias
				WHERE
					unidad_base = @unidad_base
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("unidad_base",unidad);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				string unidades = conector.result_set.Rows[0]["unidades"].ToString();

				conversiones = unidades.Split(',').ToList<string>();
			}

			return conversiones;
		}

		public DTO_Materia_prima get_materia_prima(long materia_prima_id)
		{
			DTO_Materia_prima materia = new DTO_Materia_prima();

			string sql = @"
				SELECT
					*
				FROM
					farmacontrol_global.materias_primas
				WHERE
					materia_prima_id = @materia_prima_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("materia_prima_id",materia_prima_id);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				var row = conector.result_set.Rows[0];
				materia.materia_prima_id = Convert.ToInt64(row["materia_prima_id"]);
				materia.nombre = row["nombre"].ToString();
				materia.pct_iva = Convert.ToDecimal(row["pct_iva"]);
				materia.precio_costo = Convert.ToDecimal(row["precio_costo"]);
				materia.precio_publico = Convert.ToDecimal(row["precio_publico"]);
				materia.minimo = Convert.ToInt64(row["minimo"]);
				materia.tipo_ieps = row["tipo_ieps"].ToString();
				materia.ieps = Convert.ToDecimal(row["ieps"]);
			}

			return materia;
		}
	}
}
