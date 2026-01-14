using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Devolucion_mayorista_ticket
	{
		public long devolucion_id { set; get; }
		public int terminal_id { set; get; }
		public long mayorista_id { set; get; }
		public long empleado_id { set; get; }
		public long termina_empleado_id { set; get; }
		public long entrada_id { set; get; }
		public string solicitud_devolucion_folio { set; get; }
		public string solicitud_devolucion_fecha { set; get; }
		
		public DateTime? fecha_creado { set; get; }
		public DateTime? fecha_terminado { set; get; }

		public string comentarios { set; get; }
		public List<DTO_Detallado_devolucion_mayorista_ticket> detallado_devolucion_ticket { get; set; }
		public string nombre_empleado_captura { set; get; }
		public string nombre_empleado_termina { set; get; }
	}

	class DTO_Detallado_devolucion_mayorista_ticket
	{
		public int articulo_id { set; get; }
		public string amecop { set; get; }
		public string nombre { set; get; }
		public decimal precio_costo { set; get; }
		public decimal total { set; get; }
		public List<Tuple<string, string, int>> caducidades_lotes { set; get; }
	}
}
