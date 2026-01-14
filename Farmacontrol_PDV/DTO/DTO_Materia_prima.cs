using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Materia_prima
	{
		public long? materia_prima_id { set; get; }
		public string nombre { set; get; }
		public long minimo { set; get;}
		public decimal precio_costo { set; get; }
		public decimal precio_publico { set; get; }
		public decimal pct_iva { set; get; }
		public string tipo_ieps { set; get; }
		public decimal ieps { set; get; }
	}
}
