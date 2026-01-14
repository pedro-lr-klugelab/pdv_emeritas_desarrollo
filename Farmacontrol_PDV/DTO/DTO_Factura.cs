using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Factura
	{
		public bool status { set; get; }
		public string mensaje { set; get; }
		public string nombre_archivo { set; get; }
		public string cfdi { set; get; }
		public string pdf_base64 { set; get; }
	}

	class FacturaWSP
	{
		public bool status				{ set; get; }
		public string mensaje			{ set; get; }

        public DateTime? fecha_cancelado { set; get; }

		public long factura_dato_fiscal_id			{ set; get; }
		public long venta_id			{ set; get; }
		public string serie				{ set; get; }
		public long folio				{ set; get; }
		public string tipo				{ set; get; }
		public string version			{ set; get; }
		public string uuid				{ set; get; }
		public string detallado			{ set; get; }
		public string fecha_timbrado	{ set; get; }
		public string sello_digital		{ set; get; }
		public string certificado_sat	{ set; get; }
		public string sello_sat			{ set; get; }
		public decimal importe_total	{ set; get; }
		public decimal importe_ieps		{ set; get; }
		public decimal importe_iva		{ set; get; }
		public string tipo_pago			{ set; get; }
		public string cuenta			{ set; get; }
		public string rfc_emisor		{ set; get; }
		public string rfc_receptor		{ set; get; }

		public string archivo_pdf		{ set; get; }
		public string archivo_xml		{ set; get; }

        public string razon_social      { set; get; }
        public string calle             { set; get; }
        public string numero_exterior   { set; get; }
        public string numero_interior   { set; get; }
        public string colonia           { set; get; }
        public string ciudad            { set; get; }
        public string municipio         { set; get; }
        public string estado            { set; get; }
        public string codigo_postal     { set; get; }
        public string pais              { set; get; }
	}
}
