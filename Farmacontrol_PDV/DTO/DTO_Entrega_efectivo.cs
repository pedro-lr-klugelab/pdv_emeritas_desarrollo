using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacontrol_PDV.DTO
{
    class DTO_Entrega_efectivo
    {
        public long entrega_efectivo_id { set; get; }
        public long empleado_id { set; get; }
        public string nombre_empleado { set; get; }
        public string quien_recibe { set; get; }
        public long? corte_parcial_id { set; get; }
        public long? corte_total_id { set; get; }
        public decimal importe { set; get; }
        public string comentario { set; get; }
        public DateTime fecha { set; get; }
    }
}
