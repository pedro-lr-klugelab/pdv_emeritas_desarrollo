using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Vale
	{
		public string vale_efectivo_id { set; get; }
		public long sucursal_id { set; get; }
		public long empleado_id { set; get; }
		public string tipo_creacion { set; get; }
		public long elemento_id { set; get; }
		public long? canje_sucursal_id { set; get; }
		public long? canje_empleado_id { set; get; }
		public long? canje_venta_id { set; get; }
		public DateTime fecha_creacion { set; get; }
		public DateTime? fecha_canje { set; get; }
        public DateTime? fecha_cancelacion { set; get; }
		public decimal total { set; get; }
	}
}
