using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Existencia
	{
		public long articulo_id { set; get; }
		public string caducidad { set; get; }
		public string lote { set; get; }
		public long existencia { set; get; }
        public decimal volumen { get; set; }
    }
}
