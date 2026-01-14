using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Generico
	{
		Conector conector = new Conector();

		public Array get_enum(string tabla, string columna)
		{
			string[] enums = {} ;

			string sql = string.Format(@"
				SHOW COLUMNS FROM 
					{0}
				WHERE 
					Field = @field
			",tabla);

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("field",columna);

			conector.Select(sql,parametros);

			var result = conector.result_set;

			if(result.Rows.Count > 0)
			{
				string type = result.Rows[0]["Type"].ToString();
				string sin_enum = type.Replace("enum(","");
				string sin_comillas = sin_enum.Replace("'","");
				string sin_parentesis = sin_comillas.Replace(")","");

				enums = sin_parentesis.Split(',');
			}

			return enums;
		}
	}
}
