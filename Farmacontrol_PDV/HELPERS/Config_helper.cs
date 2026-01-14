using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.HELPERS
{
	class Config_helper
	{
		public static string get_config_local(string nombre)
		{
			Conector conector = new Conector();

			string sql = @"
				SELECT
					valor
				FROM
					farmacontrol_local.config
				WHERE
					nombre = @nombre
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("nombre", nombre);

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return conector.result_set.Rows[0]["valor"].ToString();
			}

			return "";
		}

        public static bool set_config_local(string nombre, string valor)
        {
            bool ok = false;
            Conector conector = new Conector();

            string sql = @"
				UPDATE farmacontrol_local.config
                SET
					valor = @valor
				WHERE
					nombre = @nombre
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("nombre", nombre);
            parametros.Add("valor", valor);

            conector.Update(sql, parametros);

            ok = Convert.ToBoolean(conector.filas_afectadas);

            return ok;
        }

		public static string get_config_global(string nombre)
		{
			Conector conector = new Conector();

			string sql = @"
				SELECT
					valor
				FROM
					farmacontrol_global.config
				WHERE
					nombre = @nombre
				LIMIT 1
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("nombre",nombre);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				return conector.result_set.Rows[0]["valor"].ToString();
			}

			return "";
		}

        public static bool set_update_config_oro( string valor )
        {
            bool ok = false;

            Conector conector = new Conector();

            string sql = @"
				SELECT
					valor
				FROM
					farmacontrol_local.config
				WHERE
					nombre = 'parametros_circulo_salud'
				LIMIT 1
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
           
            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                /****EXISTE , SOLO SE ACTUALIZA**/

                sql = @"
				UPDATE farmacontrol_local.config
                SET
					valor = @valor
				WHERE
					nombre = 'parametros_circulo_salud'
			";

                parametros = new Dictionary<string, object>();
                parametros.Add("valor", valor);

                conector.Update(sql, parametros);

                ok = Convert.ToBoolean(conector.filas_afectadas);



            }
            else
            { 
                /*****NO EXISTE EL RENGLON DE CONFIG**/
                sql = @"
				    INSERT INTO
                        farmacontrol_local.config
                    SET
                        nombre = 'parametros_circulo_salud',
                        valor = @valor,
                        datatype = 'string'
			    ";

                parametros = new Dictionary<string, object>();
                parametros.Add("valor", valor);

                conector.Insert(sql, parametros);

                if (conector.insert_id > 0)
                    ok = true;

            
            }



            return ok;
        }

     

	}
}
