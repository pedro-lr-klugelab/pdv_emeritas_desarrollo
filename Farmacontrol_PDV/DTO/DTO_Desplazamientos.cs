using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Desplazamientos_item
	{
		public long articulo_id { set; get; }
		public string amecop { set; get; }
		public string producto { get; set; }
		public int existencia { get; set; }
		public int ventas { get; set; }
		public string prox_cd { get; set; }
	}
}
