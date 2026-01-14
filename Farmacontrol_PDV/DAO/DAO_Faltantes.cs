using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Faltantes
	{
		public static List<DTO_Detallado_faltante> get_detallado_faltantes(long sucursal_id, long reporte_faltantes_id)
		{
			List<DTO_Detallado_faltante> lista_sucursales = new List<DTO_Detallado_faltante>();
			Rest_parameters parametros = new Rest_parameters();
			parametros.Add("reporte_faltantes_id", reporte_faltantes_id);
			parametros.Add("sucursal_id",sucursal_id);

			lista_sucursales = Rest_helper.make_request<List<DTO_Detallado_faltante>>("faltantes/get_detallado_faltantes", parametros);
			return lista_sucursales;
		}

		public static List<DTO_Sucursal> get_sucursales_faltantes(long reporte_faltantes_id)
		{
			List<DTO_Sucursal> lista_sucursales = new List<DTO_Sucursal>();
			Rest_parameters parametros = new Rest_parameters();
			parametros.Add("reporte_faltantes_id",reporte_faltantes_id);

			lista_sucursales = Rest_helper.make_request<List<DTO_Sucursal>>("faltantes/get_sucursales_faltantes", parametros);
			return lista_sucursales;
		}

		public static List<DTO_Faltante> get_faltantes()
		{
			List<DTO_Faltante> lista_faltantes = new List<DTO_Faltante>();
			lista_faltantes =  Rest_helper.make_request<List<DTO_Faltante>>("faltantes/get_faltantes_pdv");
			return lista_faltantes;
		}
	}
}
