using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Cambio_precio
	{
		public long cambio_precio_id { set; get; }
		public string mayorista { set; get; }
		public DateTime fecha_creado { set; get; }
		public long numero_productos { set; get; }
	}

	class DTO_Detallado_cambio_precios
	{
		public long amecop { set; get; }
		public string producto { set; get; }
		public decimal precio_publico_anterior { set; get; }
		public decimal precio_publico_nuevo { set; get; }
		public decimal incr_publico { set; get; }
		public decimal incr_costo { set; get; }
		public decimal precio_costo_anterior { set; get; }
		public decimal precio_costo_nuevo { set; get; }
		public long existencia { set; get; }
	}

	class DTO_Detallado_cambio_precios_reporte
	{
		public string amecop { set; get; }
		public string producto { set; get; }
		public decimal precio_publico_anterior { set; get; }
		public decimal precio_publico_nuevo { set; get; }
		public decimal incr_publico { set; get; }
		public decimal incr_costo { set; get; }
		public decimal precio_costo_anterior { set; get; }
		public decimal precio_costo_nuevo { set; get; }
		public long existencia { set; get; }
	}
}
