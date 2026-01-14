using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Anaquel
	{
		public long anaquel_id { set; get; }
		public string nombre { set; get; }
		public long posicion { set; get; }
		public long numero_productos { set; get; }
	}

	class DTO_Detallado_anaquel
	{
		public long articulo_id { set; get; }
		public long amecop { set; get; }
		public string nombre { set; get; }
	}
}
