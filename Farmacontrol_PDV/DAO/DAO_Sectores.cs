using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using System.Data;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Sectores 
	{
		Conector conector = new Conector();

		public List<DTO_Sector> get_sectores()
		{
			string sql = @"SELECT sector_id, nombre FROM farmacontrol_global.sectores";
			Dictionary<string,object> parametros = new Dictionary<string,object>();
			conector.Select(sql,parametros);

			List<DTO_Sector> sectores = new List<DTO_Sector>();

			foreach(DataRow row in conector.result_set.Rows)
			{	
				DTO_Sector sector = new DTO_Sector();
				sector.sector_id = Convert.ToInt64(row["sector_id"]);
				sector.nombre = row["nombre"].ToString();

				sectores.Add(sector);
			}

			return sectores;
		}

		public string get_articulos_sectores(long sector_id)
		{
			string sql = @"
				SELECT
					GROUP_CONCAT(articulo_id) AS articulos_ids
				FROM
					farmacontrol_global.articulos_sectores
				WHERE
					sector_id = @sector_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("sector_id",sector_id);
			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				return conector.result_set.Rows[0]["articulos_ids"].ToString();
			}
			else
			{
				return "";
			}
		}
	}
}
