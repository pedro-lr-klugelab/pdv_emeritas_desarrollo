using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacontrol_PDV.DTO
{
    class DTO_Impresiones
    {
        public long impresion_id { set; get; }
        public long terminal_id { set; get; }
        public string nombre_terminal { set; get; }
        public long empleado_id { set; get; }
        public string nombre_empleado { set; get; }
        public string tipo { set; get; }
        public string folio { set; get; }
        public string impresora { set; get; }
        public DateTime fecha { set; get; }
    }
}
