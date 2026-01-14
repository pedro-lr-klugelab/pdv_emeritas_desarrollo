using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Caducidades
	{
		public long articulo_id { set; get; }
		public string amecop { set; get; }
		public string producto { set; get; }
		public string caducidad { set; get; }
		public long existencia { set; get; }
	}
}
