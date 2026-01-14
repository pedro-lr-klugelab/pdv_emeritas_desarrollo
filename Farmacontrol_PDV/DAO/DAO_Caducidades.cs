using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using System.Data;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Caducidades
	{
		Conector conector = new Conector();

		public List<DTO_Caducidades> get_lista_caducidades(int cantidad_meses)
		{
			var caducidades = get_caducidades(cantidad_meses);

			List<DTO_Caducidades> lista_caducidades = new List<DTO_Caducidades>();

			foreach(DataRow row in caducidades.Rows)
			{
				DTO_Caducidades dto = new DTO_Caducidades();
				dto.articulo_id	= Convert.ToInt64(row["articulo_id"]);
				dto.amecop		= row["amecop"].ToString();
				dto.caducidad	= Misc_helper.fecha(row["caducidad"].ToString(),"CADUCIDAD");
				dto.existencia	= Convert.ToInt64(row["existencia"]);
				dto.producto	= row["producto"].ToString();

				lista_caducidades.Add(dto);
			}

			return lista_caducidades;
		}

		DataTable get_caducidades(int cantidad_meses)
		{
			List<DTO_Caducidades> lista_caducidades = new List<DTO_Caducidades>();

			string sql = @"
				SELECT
					articulos.articulo_id AS articulo_id,
					(
						SELECT
							ABS(amecop) AS amecop
						FROM
							farmacontrol_global.articulos_amecops
						WHERE
							articulos_amecops.articulo_id = existencias.articulo_id
						ORDER BY articulos_amecops.amecop_principal DESC
						LIMIT 1
					) AS amecop,
					articulos.nombre AS producto,
					CONVERT(existencias.caducidad USING utf8) AS caducidad,
					existencias.existencia AS existencia
				FROM
					farmacontrol_local.existencias
				JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					CURDATE() > DATE_SUB(caducidad, INTERVAL @cantidad_meses MONTH)
				ORDER BY existencias.caducidad, articulos.nombre ASC
			";

			//PERIOD_DIFF(caducidad,DATE_ADD(CURDATE(), INTERVAL 6 MONTH)) <= 6
			
			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("cantidad_meses",cantidad_meses);

			conector.Select(sql,parametros);

			return conector.result_set;
		}

	}
}
