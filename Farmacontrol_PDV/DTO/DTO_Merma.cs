using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Merma
	{
		public long merma_id { set; get; }
		public long empleado_id { set; get; }
		public long termina_empleado_id { set; get; }
		public DateTime? fecha_creado { set; get; }
		public DateTime? fecha_terminado { set; get; }
		public string comentarios { set; get; }
		public string nombre_empleado_captura { set; get; }
		public string nombre_empleado_termina { set; get; }
	}

	class DTO_Detallado_merma
	{
		public int articulo_id { set; get; }
		public string amecop { set; get; }
		public string nombre { set; get; }
		public decimal precio_costo { set; get; }
		public decimal total { set; get; }
		public List<Tuple<string, string, int>> caducidades_lotes { set; get; }
	}
}
