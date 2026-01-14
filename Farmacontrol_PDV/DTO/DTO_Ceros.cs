using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacontrol_PDV.DTO
{
    class DTO_Ceros_reportes_disponibles
    {
        public long   reporte_id    { get; set; }
        public string fecha_inicial {get;set;}
        public string fecha_final     {get;set;}
        public string usuario       {get;set;}
        public string sucursales    { get; set; }
    }

    class Reporte_ceros
    {
        public long articulo_id { get; set; }
        public string amecop { get; set; }
       // public int piezas { get; set; }
        public string producto { get; set; }
        public int existencia { get; set; }
        public string ultima_venta { get; set; }
        public long venta_60dias { get; set; }
        //public int minimo { get; set; }
        //public int maximo { get; set; }
        public long existencia_almacen { get; set; }
        //public long existencia_transito { get; set; }
        //public long sugerido_almacen { get; set; }
        //public long sugerido_mayorista { get; set; }
    }

    class Valida_almacen
    {
        public bool status { get; set; }
        
    }

    class Termina_almacen
    {
        public bool status { get; set; }
    }

}
