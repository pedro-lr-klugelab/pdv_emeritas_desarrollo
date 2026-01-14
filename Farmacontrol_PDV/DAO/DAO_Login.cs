using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using MySql.Data.MySqlClient;
using System.Data;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.DAO
{
    class DAO_Login
    {
        Conector conector = new Conector();

		public bool modulo_usa_caja(int modulo_id)
		{
			string sql = @"	
				SELECT
					usa_caja
				FROM
					farmacontrol_global.modulos
				WHERE
					modulo_id = @modulo_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("modulo_id",modulo_id);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				return Convert.ToBoolean(conector.result_set.Rows[0]["usa_caja"]);
			}

			return false;
		}

		public bool empleado_es_encargado(long empleado_id)
		{
			string sql = @"
				SELECT
					empleado_id
				FROM
					farmacontrol_global.sucursales_encargados
				WHERE
					empleado_id = @empleado_id
				AND
					sucursal_id = @sucursal_id
				LIMIT 1
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("empleado_id",empleado_id);
			parametros.Add("sucursal_id", Convert.ToInt64(Config_helper.get_config_local("sucursal_id")));

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				return true;
			}

			return false;
		}

		public DTO_Validacion validar_funcion_empleado(int empleado_id, string funcion)
		{
			DTO_Validacion validacion = new DTO_Validacion();
			validacion.status = false;
			validacion.informacion = "No tienes los permisos necesarios para realizar esta accion";

			string sql = @"
				SELECT
					funcion_empleado_id
				FROM
					farmacontrol_global.funciones_empleados
				LEFT JOIN farmacontrol_global.funciones USING(funcion_id)
				WHERE
					funcion = @funcion
				AND
					empleado_id = @empleado_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("funcion",funcion);
			parametros.Add("empleado_id",empleado_id);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				validacion.status = true;
				validacion.informacion = "";
			}

			return validacion;
		}

        public DTO_Validacion validar_usuario(string usuario)
        {
            DTO_Validacion validacion = new DTO_Validacion();

            string sql = @"
				SELECT 
					empleado_id 
				FROM 
					empleados 
				WHERE 
					usuario  = @usuario 
				AND 
					puede_login = 1
				AND
					activo = 1
				LIMIT 1
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("usuario",usuario);

			conector.Select(sql,parametros);

			DataTable result = conector.result_set;

			if (result.Rows.Count > 0)
			{
				validacion.status = true;
			}

            return validacion;
        }

		public DTO_Validacion validar_fcid(string fcid)
		{
			DTO_Validacion validacion = new DTO_Validacion();

			string sql = @"
				SELECT 
					empleado_id,
					nombre 
				FROM 
					empleados 
				WHERE 
					fcid  = @fcid
				AND
					activo = 1
				LIMIT 1
			";

			Dictionary<string,object> parametros= new Dictionary<string,object>();
			parametros.Add("fcid",fcid);

			conector.Select(sql,parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				DataRow row = conector.result_set.Rows[0];
				validacion.status = true;
				validacion.elemento_id = int.Parse(row["empleado_id"].ToString());
				validacion.elemento_nombre = row["nombre"].ToString();
			}

			return validacion;
		}

        public DTO_Validacion validar_permisos_ventana(int? empleado_id, int modulo_id)
        {
            DTO_Validacion validacion = new DTO_Validacion();

			if(empleado_id == null)
			{
				return validacion;
			}

			string sql = @"
				SELECT 
					empleado_id,
					nombre
				FROM 
					farmacontrol_global.empleados
				LEFT JOIN 
					farmacontrol_global.modulos_empleados USING(empleado_id) 
				WHERE 
                    empleado_id = @empleado_id AND empleados.es_root IS TRUE
                OR
					modulo_id = @modulo_id AND empleado_id = @empleado_id
				LIMIT 1
			";

			Dictionary<string,object> parametros= new Dictionary<string,object>();
			parametros.Add("modulo_id",modulo_id);
			parametros.Add("empleado_id",empleado_id);

			try
			{
				conector.Select(sql,parametros);

				DataTable result = conector.result_set;

				if (result.Rows.Count > 0)
				{
					validacion.status = true;
					validacion.elemento_id = int.Parse(result.Rows[0]["empleado_id"].ToString());
					validacion.elemento_nombre = result.Rows[0]["nombre"].ToString();
				}
			}
			catch(MySqlException exception)
			{
				Log_error.log(exception);
			}

			return validacion;
        }

		public DTO_Validacion validar_usuario_password(string usuario, string password)
		{
			DTO_Validacion validacion = new DTO_Validacion();

			string sql = @"
				SELECT
                    empleado_id,
					nombre
                FROM
                    farmacontrol_global.empleados
                WHERE
					puede_login = 1
				AND
					activo = 1
				AND
                    usuario = @usuario
				AND 
					password = md5(@password) 
				LIMIT 1;
			";

			Dictionary<string,object> parametros= new Dictionary<string,object>();
			parametros.Add("usuario",usuario);
			parametros.Add("password",password);

			conector.Select(sql,parametros);

			DataTable result = conector.result_set;

			if (result.Rows.Count > 0)
			{
				validacion.status = true;
				validacion.elemento_id = int.Parse(result.Rows[0]["empleado_id"].ToString());
				validacion.elemento_nombre = result.Rows[0]["nombre"].ToString();
			}
			else
			{
				validacion.informacion = "Password incorrecto";
			}

			return validacion;
		}
		
        public string get_controller(int modulo_id)
        {
            string controller = "";

            string sql = @"
                SELECT
	                controller
                FROM
	                farmacontrol_global.modulos
                WHERE
                    modulo_id = @modulo_id
				LIMIT 1
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("modulo_id",modulo_id);

			conector.Select(sql,parametros);

			DataTable result = conector.result_set;

			if(result.Rows.Count > 0)
			{
				controller = result.Rows[0]["controller"].ToString();
			}

            return controller;
        }
    }
}
