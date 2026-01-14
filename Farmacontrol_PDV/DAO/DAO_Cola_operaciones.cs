using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Cola_operaciones
	{
		public static void insertar_cola_operaciones(long empleado_id, string controller, string method, Dictionary<string,object> parameters_envio, string error_message)
		{
            try
            {
                Conector conector = new Conector();

                string data = JsonConvert.SerializeObject(parameters_envio).ToString();

                string sql = @"
					INSERT INTO
						farmacontrol_local.cola_operaciones
					SET
						empleado_id = @empleado_id,
						fecha_solicitado = NOW(),
						server = @server,
						controller = @controller,
						method = @method,
						data = @data,
						error = @error
				";

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("empleado_id", empleado_id);
                parametros.Add("server", Properties.Configuracion.Default.main_server);
                parametros.Add("controller", controller);
                parametros.Add("method", method);
                parametros.Add("data", data);
                parametros.Add("error", error_message);

                conector.Insert(sql, parametros);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
		}
	}
}
