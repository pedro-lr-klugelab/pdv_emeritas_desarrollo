using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	public class DTO_Producto_inventario
	{
		public string codigo { set; get; }
		public string nombre { set; get; }
		public int existencia { set; get; }
		public decimal precio { set; get; }
		public decimal valor { set; get; }
	}
}
