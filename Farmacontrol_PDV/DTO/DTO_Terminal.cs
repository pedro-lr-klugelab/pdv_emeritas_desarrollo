using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Terminal
	{
		public int terminal_id { set; get; }
		public string nombre { set; get; }
		public string direccion_ip { set; get; }
		public bool es_caja { set; get; }
		public string serie_facturas { set; get; }
		public string serie_facturas_cortes { set; get; }
		public string serie_nc { set; get; }
		public bool permitir_impresion_remota { set; get; }
	}
}
