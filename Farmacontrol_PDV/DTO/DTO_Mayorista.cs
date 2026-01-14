using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Mayorista
	{
		public long mayorista_id { set; get; }
		public string nombre { set; get; }
		public string direccoin { set; get; }
		public string colonia { set; get; }
		public string poblacion { set; get; }
		public long codigo_postal { set; get; }
		public long telefono { set; get; }
		public string rfc { set; get; }
		public string vendedor { set; get; }
		public string cuenta { set; get; }
		public string filial { set; get; }
		public long plazo { set; get; }
		public decimal pct_descuento { set; get; }
		public string fecha_agregado { set; get; }
	}
}
