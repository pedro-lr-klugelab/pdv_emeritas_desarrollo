using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Farmacontrol_PDV.DTO;

namespace Farmacontrol_PDV.DAO
{
    class DAO_excepciones_codigos
    {
        Conector conector = new Conector();

        public string get_excepciones_codigos()
        {
            string codigos_excepciones = "";
            string sql = @"
				SELECT
					GROUP_CONCAT(codigo_original SEPARATOR '*' ) AS excepcion
				FROM
					farmacontrol_global.excepciones_codigos
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
           
            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {

                    codigos_excepciones = row["excepcion"].ToString();

                }
            }



            return codigos_excepciones;
        }

        public string get_codigo_cero(string codigo)
        {
            string codigo_cero = "";
            string sql = @"
				SELECT
					codigo_excepcion
				FROM
					farmacontrol_global.excepciones_codigos     
                WHERE 
                    codigo_original = @codigo
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("codigo", codigo);

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {

                    codigo_cero = row["codigo_excepcion"].ToString();

                }
            }


            return codigo_cero;
        }


    }
}
