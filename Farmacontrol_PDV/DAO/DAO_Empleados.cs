using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Farmacontrol_PDV.DTO;
using System.Data;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.DAO
{
    class DAO_Empleados
    {
        Conector conector = new Conector();

        public bool es_empleado_diligenciero(long empleado_id)
        {
            string sql = @"
                SELECT
                    empleado_id
                FROM
                    farmacontrol_global.sucursales_diligencieros
                WHERE
                    sucursal_id = @sucursal_id
                AND
                    empleado_id = @empleado_id
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("sucursal_id",Convert.ToInt64(Config_helper.get_config_local("sucursal_id")));
            parametros.Add("empleado_id",empleado_id);

            conector.Select(sql, parametros);

            if(conector.result_set.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

		public DTO_Empleado get_empleado_data(string fcid)
		{
            DTO_Empleado empleado = new DTO_Empleado();

            try
            {
                string sql = @"
				SELECT 
					empleado_id,
                    nombre,
                    activo,
                    fcid,
                    es_admin,
                    es_root,
                    puede_login,
                    usuario,
                    password,
                    fecha_agregado,
                    modified
				FROM
					farmacontrol_global.empleados
				WHERE 
					fcid = @fcid
			";

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("fcid",fcid);

                conector.Select(sql, parametros);

                if (conector.result_set.Rows.Count > 0)
                {
                    var row = conector.result_set.Rows[0];

                    empleado = new DTO_Empleado();
                    empleado.Empleado_id = int.Parse(row["empleado_id"].ToString());
                    empleado.Nombre = row["nombre"].ToString();
                    empleado.Activo = Convert.ToBoolean(row["activo"]);
                    empleado.Fcid = row["fcid"].ToString();
                    empleado.Es_admin = Convert.ToBoolean(row["es_admin"]);
                    empleado.Es_root = Convert.ToBoolean(row["es_root"]);
                    empleado.Puede_login = Convert.ToBoolean(row["puede_login"]);
                    empleado.Usuario = row["usuario"].ToString();
                    empleado.Password = row["password"].ToString();
                }
            }
            catch(Exception ex)
            {
                Log_error.log(ex);
            }

            return empleado;
		}

        public DTO_Empleado get_empleado_data(int empleado_id)
        {
            DTO_Empleado empleado = new DTO_Empleado();

            string sql = @"
				SELECT 
					* 
				FROM 
					farmacontrol_global.empleados 
				WHERE 
					empleado_id = @empleado_id
				LIMIT 1
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("empleado_id",empleado_id);

			conector.Select(sql,parametros);

		   DataTable result = conector.result_set;

		   if (result.Rows.Count > 0)
		   {
				empleado.Empleado_id = int.Parse(result.Rows[0]["empleado_id"].ToString());
				empleado.Nombre = result.Rows[0]["nombre"].ToString();
				empleado.Activo = Convert.ToBoolean(result.Rows[0]["activo"]);
				empleado.Fcid = result.Rows[0]["fcid"].ToString();
				empleado.Es_admin = Convert.ToBoolean(result.Rows[0]["es_admin"]);
				empleado.Es_root = Convert.ToBoolean(result.Rows[0]["es_root"]);
				empleado.Puede_login = Convert.ToBoolean(result.Rows[0]["puede_login"]);
				empleado.Usuario = result.Rows[0]["usuario"].ToString();
				empleado.Password = result.Rows[0]["password"].ToString();
		   }

            return empleado;
        }
    }
}
