using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacontrol_PDV.DAO
{
    class DAO_Entregas_efectivo
    {
        Conector conector = new Conector();

        public List<DTO_Entrega_efectivo> get_entregas_efectivo_corte(long corte_id, bool total = false)
        {
            List<DTO_Entrega_efectivo> lista = new List<DTO_Entrega_efectivo>();
            string sql = string.Format(@"
                SELECT
                     entrega_efectivo_id,
                    empleado_id,
                    corte_parcial_id,
                    corte_total_id,
                    importe,
                    recipiente,
                    comentario,
                    fecha,
                    empleados.nombre AS entrega_empleado
                FROM
                    farmacontrol_local.entregas_efectivo
                JOIN farmacontrol_global.empleados USING(empleado_id)
                WHERE
                    {0} = @corte_id
            ",total ? "corte_total_id" : "corte_parcial_id");

            Dictionary<string,object> parametros = new Dictionary<string,object>();
            parametros.Add("corte_id",corte_id);

            conector.Select(sql, parametros);

            if(conector.result_set.Rows.Count > 0)
            {
                foreach(DataRow row in conector.result_set.Rows)
                {
                    DTO_Entrega_efectivo entrega_efectivo = new DTO_Entrega_efectivo();

                    entrega_efectivo.entrega_efectivo_id = Convert.ToInt64(row["entrega_efectivo_id"]);
                    entrega_efectivo.empleado_id = Convert.ToInt64(row["empleado_id"]);

                    long? nullable = null;

                    entrega_efectivo.corte_parcial_id = row["corte_parcial_id"].ToString().Trim().Length > 0 ? Convert.ToInt64(row["corte_parcial_id"]) : nullable;
                    entrega_efectivo.corte_total_id = row["corte_total_id"].ToString().Trim().Length > 0 ? Convert.ToInt64(row["corte_total_id"]) : nullable;
                    entrega_efectivo.importe = Convert.ToDecimal(row["importe"]);
                    entrega_efectivo.quien_recibe = row["recipiente"].ToString();
                    entrega_efectivo.comentario = row["comentario"].ToString();
                    entrega_efectivo.nombre_empleado = row["entrega_empleado"].ToString();
                    entrega_efectivo.fecha = Convert.ToDateTime(row["fecha"]);

                    lista.Add(entrega_efectivo);
                }
            }

            return lista;
        }

        public DTO_Entrega_efectivo get_entrega_efectivo_data(long entrega_efectivo_id)
        {
            DTO_Entrega_efectivo entrega_efectivo = new DTO_Entrega_efectivo();

            string sql = @"
                SELECT
                    entrega_efectivo_id,
                    empleado_id,
                    corte_parcial_id,
                    corte_total_id,
                    importe,
                    recipiente,
                    comentario,
                    fecha,
                    empleados.nombre AS entrega_empleado
                FROM
                    farmacontrol_local.entregas_efectivo
                JOIN farmacontrol_global.empleados USING(empleado_id)
                WHERE
                    entrega_efectivo_id = @entrega_efectivo_id
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("entrega_efectivo_id",entrega_efectivo_id);

            conector.Select(sql, parametros);

            if(conector.result_set.Rows.Count > 0)
            {
                var row = conector.result_set.Rows[0];
                entrega_efectivo.entrega_efectivo_id = Convert.ToInt64(row["entrega_efectivo_id"]);
                entrega_efectivo.empleado_id = Convert.ToInt64(row["empleado_id"]);

                long? nullable = null;

                entrega_efectivo.corte_parcial_id = row["corte_parcial_id"].ToString().Trim().Length > 0 ? Convert.ToInt64(row["corte_parcial_id"]) : nullable;
                entrega_efectivo.corte_total_id = row["corte_total_id"].ToString().Trim().Length > 0 ? Convert.ToInt64(row["corte_total_id"]) : nullable;
                entrega_efectivo.importe = Convert.ToDecimal(row["importe"]);
                entrega_efectivo.quien_recibe = row["recipiente"].ToString();
                entrega_efectivo.comentario = row["comentario"].ToString();
                entrega_efectivo.nombre_empleado = row["entrega_empleado"].ToString();
                entrega_efectivo.fecha = Convert.ToDateTime(row["fecha"]);
            }


            return entrega_efectivo;
        }

        public long registrar_entrega_efectivo(long empleado_id, decimal importe, string recipiente, string comentario)
        {

            string sql = @"
                INSERT INTO
                    farmacontrol_local.entregas_efectivo
                SET
                    terminal_id = @terminal_id,
                    empleado_id = @empleado_id,
                    importe = @importe,
                    recipiente = @recipiente,
                    comentario = @comentario,
                    fecha = NOW()
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("empleado_id",empleado_id);
            parametros.Add("importe",importe);
            parametros.Add("recipiente",recipiente);
            parametros.Add("comentario",comentario);
            parametros.Add("terminal_id",(int)Misc_helper.get_terminal_id());

            conector.Insert(sql, parametros);

            return conector.insert_id;
        }
    }
}
