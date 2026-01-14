using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
    class DTO_Sucursal
    {
		public int		sucursal_id				{ get; set; }
		public string	nombre					{ get; set; }
		public string	direccion				{ get; set; }
		public string	colonia					{ get; set; }
		public string	codigo_postal			{ get; set; }
        public string   municipio               { get; set; }
		public string	ciudad					{ get; set; }
		public string	estado					{ get; set; }

		public string	telefono				{ get; set; }
		public string	razon_social			{ get; set; }
		public string	rfc						{ get; set; }
		public string	domicilio_fiscal		{ get; set; }
		public string	registro_ssa			{ get; set; }
		public string	ip_sucursal				{ get; set; }

		public string	usuario_pc				{ set; get; }
		public string	password_pac			{ set; get; }
		public long		plantilla_email_id		{ set; get; }
		public long		pdf_id					{ set; get; }

		public int		es_almacen				{ get; set; }
		public int		puerto_espejo			{ get; set; }
		public int		es_local				{ get; set; }

		public int		es_farmacontrol			{ set; get; }

        public string email_nombre              { set; get; }
		public string	email					{ get; set; }
		public string	email_password			{ get; set; }

        public int tae_diestel_tienda_id { set; get; }

        public string etiqueta { set; get; }
    }
}