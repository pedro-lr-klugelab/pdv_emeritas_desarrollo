using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacontrol_PDV.DAO
{
    class DAO_Exclusivos
    {

        Conector conector = new Conector();

        public bool valida_exclusivo(long sucursal_id, int codigo_id)
        {
            bool es_exlusivo = true;
            string sql = @"
				SELECT
                    exclusivo_id					
				FROM
					farmacontrol_global.exclusivos
				WHERE
				   codigo_id = @codigo_id

			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("codigo_id", codigo_id);

            conector.Select(sql, parametros);
            var data_exclusivo = conector.result_set;

            if (data_exclusivo.Rows.Count > 0)
            {

                sql = @"
				SELECT
                    exclusivo_id					
				FROM
					farmacontrol_global.exclusivos
				WHERE
				   sucursal_id = @sucursal_id
                AND
                   codigo_id = @codigo_id
			    ";

                parametros.Add("sucursal_id", sucursal_id);

                conector.Select(sql, parametros);

                var data = conector.result_set;

                if (data.Rows.Count > 0)
                {
                    es_exlusivo = true;
                }
                else
                {
                    es_exlusivo = false;
                }
            }
            else
            {
                es_exlusivo = true;
            }

            return es_exlusivo;
        }


        public bool valida_traspaso(long sucursal_local, long sucursal_destino,int codigo_id)
        {
            bool valido = true;


             string sql = @"
				SELECT
                    exclusivo_id					
				FROM
					farmacontrol_global.exclusivos
				WHERE
				   codigo_id = @codigo_id

			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("codigo_id", codigo_id);

            conector.Select(sql, parametros);
            var data_exclusivo = conector.result_set;

            if (data_exclusivo.Rows.Count > 0)
            {

                sql = @"
				SELECT
                    sucursal_id					
				FROM
					farmacontrol_global.exclusivos
				WHERE
				   sucursal_id != @sucursal_local
                AND 
                   sucursal_id IN( @sucursal_destino )
                
			   ";

                parametros = new Dictionary<string, object>();
                parametros.Add("sucursal_local", sucursal_local);
                parametros.Add("sucursal_destino", sucursal_destino);
                conector.Select(sql, parametros);
                var data = conector.result_set;

                if (data.Rows.Count > 0)
                {
                    valido = true;
                }
                else
                {
                    valido = false;
                }



            }
            else
            {
                valido = true;
            }

          return valido;

        }
    }
}

