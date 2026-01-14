using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Cupones
	{
		Conector conector = new Conector();

		public DTO_Validacion validar_cupon(string codigo)
		{
			DTO_Validacion validacion = new DTO_Validacion();

			string sql = @"
				SELECT
					cupon_id,
					canje_sucursal_id,
					IF(canje_venta_id IS NULL,0,canje_venta_id) AS canje_venta_id,
					codigo,
					fecha_creado,
					fecha_vencimiento,
					UNIX_TIMESTAMP(fecha_vencimiento) fecha_vencimiento_timestamp,
					UNIX_TIMESTAMP(NOW()) fecha_actual_timestamp,
					IF(fecha_canjeado IS NULL, '',fecha_canjeado) AS fecha_canjeado
				FROM
					farmacontrol_global.cupones
				WHERE
					codigo = @codigo
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("codigo",codigo);

			conector.Select(sql,parametros);

			var result_codigo = conector.result_set;

			if(result_codigo.Rows.Count > 0)
			{
				var row_result_codigo = result_codigo.Rows[0];
				int sucursal_id_local = Convert.ToInt32(Config_helper.get_config_local("sucursal_id"));
				int sucursal_id_canje = Convert.ToInt32(row_result_codigo["canje_sucursal_id"]);

				if(sucursal_id_canje == sucursal_id_local)
				{
					long canje_venta_id = (row_result_codigo["canje_venta_id"].ToString().Equals("")) ? 0 : Convert.ToInt64(row_result_codigo["canje_venta_id"]);

					if(canje_venta_id == 0)
					{
						long fecha_vencimineto_timestamp = Convert.ToInt64(row_result_codigo["fecha_vencimiento_timestamp"]);
						long fecha_actual_timestamp = Convert.ToInt64(row_result_codigo["fecha_actual_timestamp"]);

						if(fecha_vencimineto_timestamp > fecha_actual_timestamp)
						{
							validacion.status = true;
							validacion.elemento_id = Convert.ToInt32(row_result_codigo["cupon_id"]);
						}
						else
						{
							validacion.status = false;
							validacion.informacion = "Cupon expirado";
						}
					}
					else
					{
						validacion.status = false;
						validacion.informacion = "Este cupon ya ha sido usado en la venta con el folio #" + canje_venta_id;	
					}
				}
				else
				{
					DAO_Sucursales dao_sucursales = new DAO_Sucursales();
					var sucursal_data = dao_sucursales.get_sucursal_data(sucursal_id_canje);

					validacion.status = false;
					validacion.informacion = "Este cupon solo es canjeable en la sucursal "+sucursal_data.nombre;
				}
			}
			else
			{
                //AQUI CONSULTARIA EN LA TABLA 





				validacion.status = false;
				validacion.informacion = "Codigo de cupon no encontrado";
			}

			return validacion;
		}
	}
}
