using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacontrol_PDV.DTO
{

    public class DTO_Tae_proveedores
    {
        public long fabricante_id   { set; get; }
        public string nombre        { set; get; }
    }

    public class DTO_servicios_proveedor
    {
        public long articulo_id     { set; get; }
        public long fabricante_id   { set; get; }
        public string nombre_fabricante { set; get; }
        public string nombre        { set; get; }
        public string sku           { set; get; }
        public decimal precio_publico { set; get; }
    }

    public class DTO_Log_tae
    {
        public long log_tae_diestel_id { set; get; }
        public DateTime fecha { set; get; }
        public string sku { set; get; }
        public long sucursal_id { set; get; }
        public long terminal_id { set; get; }
        public string referencia { set; get; }
        public long numero_autorizacion { set; get; }
        public long venta_id { set; get; }
        public long numero_transaccion { set; get; }
    }

    public class DTO_tae_diestel
    {
        public long articulo_id { set; get; }
        public long fabricante_id { set; get; }
        public string sku { set; get; }
        public decimal precio_publico { set; get; }
        public decimal descuento { set; get; }
    }
}
