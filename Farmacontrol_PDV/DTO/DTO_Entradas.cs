using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Entradas
	{
		public long entrada_id { set; get; }
		public int? terminal_id { set; get; }
		public long? mayorista_id { set; get; }
		public long empleado_id { set; get; }
		public long? termina_empleado_id { set; get; }

		public DateTime? fecha_creado { set; get; }
		public DateTime? fecha_recibido { set; get; }
		public DateTime? fecha_terminado { set; get; }

		public string factura { set; get; }
		public string tipo_entrada { set; get; }
		public string comentarios { set; get; }
		public string nombre_empleado_captura { set; get; }
		public string nombre_empleado_termina { set; get; }
	}
}
