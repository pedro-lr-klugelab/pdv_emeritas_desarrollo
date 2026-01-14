using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Detallado_traspaso
	{
		public int		detallado_traspaso_id	{ get; set; }
		public int		traspaso_id				{ get; set; }
		public int		articulo_id				{ get; set; }
		public string	caducidad				{ get; set; }
		public string	lote					{ get; set; }
		public decimal	precio_costo			{ get; set; }
		public int?		cantidad_origen			{ get; set; }
		public int?		cantidad_recibida		{ get; set; }
		public int?		cantidad				{ get; set; }
		public string	accion					{ get; set; }
		public decimal	total					{ get; set; }
	}
}
