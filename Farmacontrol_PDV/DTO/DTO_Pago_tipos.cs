using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	public class DTO_Pago_tipos
	{
		public long pago_tipo_id { set; get; }
		public string nombre { set; get; }
        public string etiqueta { set; get; }
		public bool es_credito { set; get; }
		public bool entrega_cambio { set; get; }
		public bool usa_cuenta { set; get; }
		public bool es_prepago { set; get; }
	}
}
