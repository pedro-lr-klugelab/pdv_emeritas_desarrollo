using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Consulta_existencias
	{
		public long sucursal_id { set; get; }
		public string sucursal { set; get; }
		public long existencia_total { set; get; }
		public long? existencia_devoluciones { set; get; }
		public long? existencia_mermas { set; get; }
		public long? existencia_cambio_fisico { set; get; }
		public long? existencia_apartados { set; get; }
		public long? existencia_traspasos { set; get; }
		public long? existencia_vendible { set; get; }
		public string pr_cd { set; get; }
	}
}
