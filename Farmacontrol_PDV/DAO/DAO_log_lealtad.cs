using Farmacontrol_PDV.circulo_salud;
using Microsoft.SqlServer.Server;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacontrol_PDV.DAO
{
    internal class DAO_log_lealtad
    {
        Conector conector = new Conector();

        public long inserta_log_operacion(string tarjeta, string nombre_programa, long venta_id, string trama, string entregados)
        {
            
            string sql = @"
                CREATE TABLE IF NOT EXISTS farmacontrol_local.log_programas_lealtad (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        tarjeta VARCHAR(50),
                        nombre_programa VARCHAR(100),
                        venta_id INT,
                        fecha DATETIME,
                        trama TEXT,
                        entregados TEXT,
                        modified DATETIME
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
                

				INSERT INTO 
                    farmacontrol_local.log_programas_lealtad
                SET
                    tarjeta = @tarjeta,
                    nombre_programa =@nombre_programa,
                    venta_id = @venta_id,
                    fecha = NOW(),
                    trama =@trama,
                    entregados =@entregados,
                    modified = NOW()					
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("tarjeta", tarjeta);
            parametros.Add("nombre_programa", nombre_programa);
            parametros.Add("venta_id", venta_id);
            parametros.Add("trama", trama);
            parametros.Add("entregados", entregados);

            long result = conector.Insert(sql, parametros);

            return result;
        }


        public bool actualiza_log_operacion(long id_lealtad,string entregados)
        {
            conector.TransactionStart();
            
            string sql = @"
				UPDATE 
                    farmacontrol_local.log_programas_lealtad
                SET 
                    entregados = @entregados
                WHERE
                    id = @id_lealtad
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("id_lealtad", id_lealtad);
            parametros.Add("entregados", entregados);

            conector.Update(sql, parametros);

            return conector.TransactionCommit();

        }

    }
}
