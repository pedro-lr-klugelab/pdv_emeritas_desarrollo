using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
    class DTO_Cliente_domicilio_data
    {
        public string cliente_domicilio_id { set; get; }
        public string cliente_id { set; get; }
        public string etiqueta { set; get; }
        public string calle { set; get; }
        public string numero_exterior { set; get; }
        public string numero_interior { set; get; }
        public string colonia { set; get; }
        public string ciudad { set; get; }
        public string municipio { set; get; }
        public string estado { set; get; }
        public string codigo_postal { set; get; }
        public string pais { set; get; }
        public long telefono { set; get; }
        public string comentarios { set; get; }
        public DateTime fecha_agregado { set; get; }
    }

	class DTO_Ventas_domicilio
	{
		public string sucursal { set; get; }	
		public string folio { set; get; }
		public DateTime fecha { set; get; }
		public decimal total { set; get; }
		public string es_factura { set; get; }
	}

	class DTO_Ventas_credito
	{
		public string cliente_credito_id { set; get; }
		public string sucursal { set; get; }
		public int sucursal_id { set; get; }
		public long venta_id { set; get; }
		public string folio { set; get; }
		public DateTime fecha_compra { set; get; }
		public decimal importe_compra { set; get; }
		public decimal por_pagar { set; get; }
		public string es_factura { set; get; }
	}

	class DTO_Cliente_correo_informacion_direccion
	{
		public string direccion { set; get; }
		public string colonia { set; get; }
		public string ciudad { set; get; }
		public string estado { set; get; }
	}

	class DTO_Cliente_correo
	{
		public string elemento_id { set; get; }
		public string tipo { set; get; }
		public string nombre { set; get; }
	}

	class DTO_Cliente
	{
		public string cliente_id { set; get; }
		public long? empleado_id { set; get; }
		public long? carnet_id { set; get; }
		public string nombre { set; get; }
		public decimal limite_credito { set; get; }
		public bool bloqueo_morosidad { set; get; }
		public int plazo { set; get; }
		public int credito_activado { set; get; }
		public decimal pct_descuento_adicional { set; get; }
		public int ventas_mayoreo { set; get; }
		public string comentarios { set; get; }
	}

	class DTO_Cliente_domicilios
	{
        public string cliente_domicilio_id { set; get; }
		public string tipo { set; get; }
		public string direccion { set; get; }
		public string colonia { set; get; }
		public string ciudad { set; get; }
		public string estado { set; get; }
		public string telefono { set; get; }
	}
}
