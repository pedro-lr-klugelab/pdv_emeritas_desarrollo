using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacontrol_PDV.DAO
{
    class DAO_motivos_reimpresion
    {

        Conector conector = new Conector();

        public bool setMotivo_impresion( long impresion_id, string motivo, long empleado_id  )
        {

            bool berror = true;

            string sql = @"
                INSERT INTO
                    farmacontrol_local.motivos_reimpresion
                SET
                    impresion_id = @impresion_id,
                    motivo = @motivo,
                    empleado_id = @empleado_id,  
                    fecha_reimpresion = NOW(),              
                    modified = NOW()
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("impresion_id", impresion_id);
            parametros.Add("motivo", motivo);
            parametros.Add("empleado_id", empleado_id);
            

            conector.Insert(sql, parametros);

            if (conector.insert_id > 0)
            {
                berror = false;
            }

            return berror;
        }


    }
}
