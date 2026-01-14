using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Farmacontrol_PDV.DTO;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Mayoristas
	{
		Conector conector = new Conector();

		public DTO_Mayorista get_mayorista(long mayorista_id)
		{
			DTO_Mayorista mayorista = new DTO_Mayorista();

			string sql = @"
				SELECT
					*
				FROM
					farmacontrol_global.mayoristas
				WHERE
					mayorista_id = @mayorista_id
				LIMIT 1
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("mayorista_id",mayorista_id);

			conector.Select(sql,parametros);

			var result = conector.result_set;

			if(result.Rows.Count > 0)
			{
				var row = result.Rows[0];
				mayorista.mayorista_id = Convert.ToInt64(row["mayorista_id"]);
				mayorista.nombre =  row["nombre"].ToString();
				mayorista.direccoin = row["direccion"].ToString();
				mayorista.colonia = row["colonia"].ToString();
				mayorista.poblacion = row["poblacion"].ToString();
				mayorista.codigo_postal = Convert.ToInt64(row["codigo_postal"]);
				mayorista.telefono = Convert.ToInt64(row["telefono"]);
				mayorista.rfc = row["rfc"].ToString();
				mayorista.vendedor = row["vendedor"].ToString();
				mayorista.cuenta = row["cuenta"].ToString();
				mayorista.filial = row["filial"].ToString();
				mayorista.plazo = Convert.ToInt64(row["plazo"]);
				mayorista.pct_descuento = Convert.ToDecimal(row["pct_descuento"]);
				mayorista.fecha_agregado = row["fecha_agregado"].ToString();
			}

			return mayorista;
		}

		public BindingSource get_all_mayoristas(bool sin_mayorista = false)
		{	
			Dictionary<int,string> contenido = new Dictionary<int,string>();
			
			string sql = @" SELECT * FROM farmacontrol_global.mayoristas ORDER BY nombre ASC";

			conector.Select(sql);

			if(conector.result_set.Rows.Count > 0)
			{
				if(!sin_mayorista)
				{
					contenido.Add(0, " * Selecciona un mayorista * ");	
				}

				foreach (DataRow row in conector.result_set.Rows)
				{
					contenido.Add(Convert.ToInt32(row["mayorista_id"]), row["nombre"].ToString());
				}
			}

			BindingSource source = new BindingSource(contenido,null);

			return source;
		}
	}
}
