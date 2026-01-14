using Farmacontrol_PDV.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.DAO
{
    class DAO_Ceros
    {
        Conector conector = new Conector();
        /*
        public List<DTO_Ceros_reportes_disponibles> get_reportes_disponibles(int sucursal_id)
        {
            
            List<DTO_Ceros_reportes_disponibles> list_ceros = new List<DTO_Ceros_reportes_disponibles>();

            string sql = @"farmacontrol_global.Get_reportes_ceros_disponibles";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("par_sucursal_id", sucursal_id);

            conector.Call(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {
                    DTO_Ceros_reportes_disponibles dto_ceros = new DTO_Ceros_reportes_disponibles();

                    dto_ceros.reporte_id = Convert.ToInt32(row["reporte_faltantes_id"]);
                    dto_ceros.fecha_inicial = row["fecha_ini"].ToString();
                    dto_ceros.fecha_final     = row["fecha_fin"].ToString();
                    dto_ceros.usuario       = row["usuario"].ToString();
                    dto_ceros.sucursales = row["sucursales"].ToString();

                    list_ceros.Add(dto_ceros);
                }
            }

            return list_ceros;
            
        }
        */
        public static DataTable get_reporte_ceros( string etiqueta)
        {
            
            Rest_parameters parametros = new Rest_parameters();
            //parametros.Add("reporte_faltantes_id", reporte_faltantes_id);
            parametros.Add("sucursal_etiqueta", etiqueta);
            List<Reporte_ceros> result_reporte_ceros = Rest_helper.make_request_obtener_ceros<List<Reporte_ceros>>("faltantes/get_faltantes_ceros_json", parametros);
            return result_reporte_ceros.ToDataTable();
       
        }
    }
}
