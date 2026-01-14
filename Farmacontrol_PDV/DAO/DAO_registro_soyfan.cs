using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farmacontrol_PDV.DTO;
using System.Data;

namespace Farmacontrol_PDV.DAO
{
    class DAO_registro_soyfan
    {

        Conector conector = new Conector();

        public long set_registro_operacion(long venta_folio = 0, string trama = "")///falta los productos
        {
            string sql = @"
                INSERT INTO
                    farmacontrol_local.registro_soyfan
                SET
                    venta_folio = @venta_folio,
                    fecha = NOW(),
                    trama = @trama,
                    modified = NOW();
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("venta_folio", venta_folio);
            parametros.Add("trama", trama);
           
            conector.Insert(sql, parametros);

            long insert_id = 0;

            if (conector.insert_id > 0)
            {
                insert_id = Convert.ToInt64(conector.insert_id);

            }

            return insert_id;
        }

        public bool get_registro_venta(string folio_venta)
        {
            bool valido = false;

            string sql = @"
				SELECT
				    idregistro_soyfan 
				FROM
					farmacontrol_local.registro_soyfan
                WHERE
                    venta_folio = @venta_folio
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("venta_folio", folio_venta);
            

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                valido = true;
            }
           
            return valido;
        
        }


        public bool set_beneficio( int registro_operacion, string trama_registro  )
        {
            bool valido = false;

            string sql = @"
				UPDATE
                    farmacontrol_local.registro_soyfan
                SET 
                    beneficio = @trama_registro
                WHERE
                    idregistro_soyfan = @registro_operacion
                LIMIT 1;
			";


            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("trama_registro", trama_registro);
            parametros.Add("registro_operacion", registro_operacion);
            conector.Update(sql, parametros);

            if (conector.filas_afectadas > 0)
            {

                valido = !valido;
            }
            return valido;
        
        }
        


    }
}






