using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Farmacontrol_PDV.DTO
{
    class DTO_Detallado_existencia_cotizacion
    {
        public string amecop { set; get; }
        public string producto { set; get; }
        public long cantidad { set; get; }
        public long existencia_vendible { set; get; }
    }

	class DTO_Cotizacion
	{
		public long cotizacion_id { set; get; }
		public long? terminal_id { set; get; }
		public long? empleado_id { set; get; }
		public string cliente_credito_id { set; get; }
		public string cliente_domicilio_id { set; get; }
		public string rfc_registro_id { set; get; }
		public long? cupon_id { set; get; }
		public DateTime fecha_creado { set; get; }
		public DateTime? fecha_cerrado { set; get; }
		public long pausado { set; get; }
		public string comentarios { set; get; }
	}

	public class Plantilla_cotizacion_articulo
	{
		public string amecop { set; get; }
		public string producto { set; get; }
		public decimal tasa { set; get; }
		public decimal precio { set; get; }
		public int cantidad { set; get; }
		public decimal total { set; get; }
	}

	public class Plantilla_cotizacion
	{
		public long cotizacion_id { set; get; }
		public decimal excento { set; get; }
		public decimal gravado { set; get; }
		public decimal subtotal { set; get; }
		public decimal ieps { set; get; }
		public decimal iva { set; get; }
		public int piezas { set; get; }
		public decimal total { set; get; }
		public List<Plantilla_cotizacion_articulo> productos { set; get; }

		public string ToXML()
		{
			var stringwriter = new System.IO.StringWriter();
			var serializer = new XmlSerializer(this.GetType());
			serializer.Serialize(stringwriter, this);
			return stringwriter.ToString();
		}
	}

	class DTO_Cotizacion_ticket
	{
		public long? cotizacion_id { set; get; }
		public string nombre_empleado { set; get; }
		public string nombre_terminal { set; get; }
		public string credito { set; get; }
		public string domicilio { set; get; }
		public string rfc_registro { set; get; }
		public List<DTO_Detallado_cotizacion_ticket> detallado_cotizacion_ticket { set; get; }
		public decimal excento { set; get; }
		public decimal gravado { set; get; }
		public decimal subtotal { set; get; }
		public decimal ieps { set; get; }
		public decimal iva { set; get; }
		public int piezas { set; get; }
		public decimal total { set; get; }
	}

	class DTO_Detallado_cotizacion_ticket
	{
		public int articulo_id { set; get; }
		public string amecop { set; get; }
		public string nombre { set; get; }
		public decimal precio_unitario { set; get; }
		public int cantidad { set; get; }
		public decimal importe { set; get; }
		public decimal subtotal { set; get; }
		public decimal descuento { set; get; }
		public decimal total { set; get; }
        public decimal importe_ieps { set; get; }
		public List<Tuple<string, string, int>> caducidades_lotes { set; get; }
	}
}