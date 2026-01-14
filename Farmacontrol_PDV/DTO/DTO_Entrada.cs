using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Entrada_ticket
	{
		public long		entrada_id			{ set; get; }
		public int		terminal_id			{ set; get; }
		public long		mayorista_id		{ set; get; }
		public long		empleado_id			{ set; get; }
		public long		termina_empleado_id { set; get; }

		public DateTime? fecha_creado		{ set; get; }
		public DateTime? fecha_recibido		{ set; get; }
		public DateTime? fecha_terminado	{ set; get; }

		public string	factura				{ set; get; }
		public string	tipo_entrada		{ set; get; }
		public string	comentarios			{ set; get; }
		public List<DTO_Detallado_entradas_ticket> detallado_entrada_ticket { get; set; }
		public string nombre_empleado_captura { set; get; }
		public string nombre_empleado_termina { set; get; }
	}

	class DTO_Detallado_entradas_ticket
	{
		public int articulo_id		{ set; get; }
		public string amecop		{ set; get; }
		public string nombre		{ set; get; }
		public decimal precio_costo { set; get; }
		public decimal total		{ set; get; }
		public List<Tuple<string, string, int>> caducidades_lotes { set; get; }
	}
}
