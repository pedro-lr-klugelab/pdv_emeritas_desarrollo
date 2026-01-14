using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.HELPERS
{
    class Sucursales_helper
    {
		public static string get_ip_sucursal(int sucursal_id = 0)
		{
			if(sucursal_id == 0)
			{
				sucursal_id = Convert.ToInt32(Config_helper.get_config_local("sucursal_id"));
			}

			DAO_Sucursales dao_sucursales = new DAO_Sucursales();

			var result = dao_sucursales.get_sucursal_data(sucursal_id);

			return result.ip_sucursal;
		} 
    }
}
