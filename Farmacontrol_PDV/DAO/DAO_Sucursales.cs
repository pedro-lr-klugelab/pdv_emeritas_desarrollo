using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using MySql.Data.MySqlClient;
using System.IO;
using System.Data;
using Farmacontrol_PDV.HELPERS;


namespace Farmacontrol_PDV.DAO
{
	class DAO_Sucursales
	{
		Conector conector = new Conector();

		public static List<DTO_Sucursal> get_sucursales(string tipo_sucursal = "")
		{
			Rest_parameters parameters = new Rest_parameters();
			parameters.Add("tipo_sucursal", tipo_sucursal);

			List<DTO_Sucursal> sucursales = Rest_helper.make_request<List<DTO_Sucursal>>("sucursales/get_sucursales");

			return sucursales;
		}

		public DataTable get_all_sucursales()
		{
			string sql = @"SELECT sucursal_id, nombre
			FROM farmacontrol_global.sucursales
			WHERE sucursal_id NOT IN (15, 39)
			ORDER BY nombre ASC;
			";

			conector.Select(sql);

			return conector.result_set;
		}

		public DTO_Sucursal get_sucursal_data(int sucursal_id)
		{
			DTO_Sucursal sucursal_data = new DTO_Sucursal();

			try
			{
				string sql = @"
					SELECT
						*
					FROM
						farmacontrol_global.sucursales
					WHERE
						sucursal_id = {0}
					LIMIT 1
				";

				conector.Select(String.Format(sql, sucursal_id));

				var result = conector.result_set;

				if (result.Rows.Count > 0)
				{
					var row = result.Rows[0];
					sucursal_data.sucursal_id			= int.Parse(row["sucursal_id"].ToString());
					sucursal_data.nombre				= row["nombre"].ToString();
					sucursal_data.direccion				= row["direccion"].ToString();
					sucursal_data.colonia				= row["colonia"].ToString();
					sucursal_data.codigo_postal			= row["codigo_postal"].ToString();
					sucursal_data.telefono				= row["telefono"].ToString();
					sucursal_data.razon_social			= row["razon_social"].ToString();
					sucursal_data.rfc					= row["rfc"].ToString();
					sucursal_data.domicilio_fiscal		= row["domicilio_fiscal"].ToString();
					sucursal_data.registro_ssa			= row["registro_ssa"].ToString();
					sucursal_data.ip_sucursal			= row["ip_sucursal"].ToString();

					sucursal_data.ciudad				= row["ciudad"].ToString();
					sucursal_data.estado				= row["estado"].ToString();
                    sucursal_data.municipio             = row["municipio"].ToString();
					sucursal_data.es_farmacontrol		= Convert.ToInt32(row["es_farmacontrol"]);

					sucursal_data.usuario_pc			= row["usuario_pac"].ToString();
					sucursal_data.password_pac			= row["password_pac"].ToString();
					sucursal_data.plantilla_email_id	= Convert.ToInt64(row["plantilla_email_id"]);
					sucursal_data.pdf_id				= Convert.ToInt64(row["pdf_id"]);

					sucursal_data.es_almacen			= Convert.ToInt32(row["es_almacen"]);
					sucursal_data.puerto_espejo			= int.Parse(row["puerto_espejo"].ToString());
					sucursal_data.email					= row["email"].ToString();
					sucursal_data.email_password		= row["email_password"].ToString();
                    sucursal_data.email_nombre          = row["email_nombre"].ToString();
                    sucursal_data.tae_diestel_tienda_id = Convert.ToInt32(row["tae_diestel_tienda_id"]);

                    sucursal_data.etiqueta              = row["etiqueta"].ToString();
				}
				
				sql = @"
					SELECT
						valor
					FROM
						farmacontrol_local.config
					WHERE
						nombre = 'sucursal_id'
					LIMIT 1
				";

				conector.Select(String.Format(sql, sucursal_id));

				result = conector.result_set;

				if (result.Rows.Count > 0)
				{
					if (sucursal_data.sucursal_id == int.Parse(result.Rows[0]["valor"].ToString()))
					{
						sucursal_data.es_local = 1;
					}
				}	
			}
			catch(MySqlException exception)
			{
				Log_error.log(exception);
			}

			return sucursal_data;
		}
	}
}
