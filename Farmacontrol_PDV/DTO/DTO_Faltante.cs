using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Faltante
	{
		public long reporte_faltantes_id { set; get; }
		public DateTime fecha_creado { set; get; }
		public DateTime fecha_terminado { set; get; }
		public string nombre_empleado { set; get; }
		public string almacen { set; get;}
		public string nombre_sucursales { set; get; }
	}

	class DTO_Detallado_faltante
	{
		public string amecop { set; get; }
		public string producto { set; get; }
		public long venta_jornada { set; get; }
		public long venta_1m { set; get; }
		public string existencia_sucursal { set; get; }
		public long existencia_almacen { set; get; }
		public long sugerid_almacen { set; get; }
		public string anaquel { set; get; }
		public string pr_cd { set; get; }
	}
}
