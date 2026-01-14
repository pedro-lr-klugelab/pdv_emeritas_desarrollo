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
    class DAO_promocion_lealtad
    {

        Conector conector = new Conector();
        
        public Dictionary<string,string> get_promocio_lealtad( int articulo_id )
        {
            Dictionary<string, string> txtpromocion = new Dictionary<string, string>();

            string sql = @"
				SELECT
                    mayorista,
				    CONCAT(mayorista, ' : ',regla , ' Promocion : ', farmacontrol_global.promociones_lealtad.comentarios,' Vigencia : ', fecha_vence  ) as promocion
				FROM 
                    farmacontrol_global.promociones_lealtad
                INNER JOIN 
                    farmacontrol_global.articulos
                USING(articulo_id)
                WHERE
                    articulo_id = @articulo_id
					
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("articulo_id", articulo_id);

            conector.Select(sql, parametros);
           
            var result = conector.result_set;

            if (result.Rows.Count > 0)
            {
                
                if (result.Rows.Count == 2)
                {
                    txtpromocion.Add(result.Rows[0]["mayorista"].ToString(), result.Rows[0]["promocion"].ToString());
                    txtpromocion.Add(result.Rows[1]["mayorista"].ToString(), result.Rows[1]["promocion"].ToString());

                }
                else if (result.Rows.Count == 1)
                {
                    txtpromocion.Add(result.Rows[0]["mayorista"].ToString(), result.Rows[0]["promocion"].ToString());
                  
                }

            }
            else
            {
                txtpromocion.Add("","");
            }

            return txtpromocion;
            
        
        }


    }
}
