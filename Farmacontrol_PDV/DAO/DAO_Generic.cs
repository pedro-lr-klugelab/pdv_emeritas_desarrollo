using Farmacontrol_PDV.HELPERS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacontrol_PDV.DAO
{
    class DAO_Generic
    {
        Conector conector = new Conector();

        public static string get_fecha_now()
        {
            Conector conector = new Conector();
            string sql = "SELECT NOW( ) AS fecha";

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            conector.Select(sql, parametros);

            if(conector.result_set.Rows.Count > 0)
            {
                return Misc_helper.fecha(conector.result_set.Rows[0]["fecha"].ToString());
            }
            else
            {
                return Misc_helper.fecha(DateTime.Now.ToString());
            }
        }
    }
}
