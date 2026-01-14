using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Rfc
	{
		public string rfc_registro_id { set; get; }
		public string rfc { set; get; }
		public string razon_social { set; get; }
		public string calle { set; get; }
		public string numero_exterior { set; get; }
		public string numero_interior { set; get; }
		public string colonia { set; get; }
		public string ciudad { set; get; }
		public string municipio { set; get; }
		public string estado { set; get; }
		public string codigo_postal { set; get; }
		public string pais { set; get; }
		public string tipo_rfc { set; get; }

		public List<string> correos_electronicos { set; get; }

        public DTO_Rfc()
        {
            rfc_registro_id = "";
        }
   
	}

	class DTO_Rfc_factura
	{
		public long sucursal_id { set; get; }
		public long venta_id { set; get; }
		public string sucursal { set; get; }
		public long folio { set; get; }
		public string serie { set; get; }
		public DateTime fecha { set; get; }
	}
}
