using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacontrol_PDV.DTO
{
    public class DTO_control_antibiotico
    {
        public long control_antibiotico_id { set; get; }
        public string amecop { set; get; }
        public string fecha { set; get; }
        public long articulo_id { set; get; }
        public string movimiento { set; get; }
        public long cantidad { set; get; }
        public string caducidad { set; get; }
        public string lote { set; get; }
        public long? control_antibioticos_receta_id { set; get; }
        public long elemento_id { set; get; }
    }

    public class DTO_Clase_antibiotico
    {
        public long clase_antibiotico_id { set; get; }
        public string nombre { set; get; }
    }

    public class DTO_Control_AB_receta
    {
        public long control_antibiotico_id { set; get; }
        public long articulo_id { set; get; }
        public string tipo_movimiento { set; get; }
        public string doctor { set; get; }
        public string cedula { set; get; }
        public string receta { set; get; }
    }

    public class DTO_info_generica
    {
        public string fecha { set; get; }
        public long folio { set; get; }
        public string comentarios { set; get; }
    }

    public class DTO_detallado_generico
    {
        public string movimiento { set; get; }
        public long elemento_id { set; get; }
        public string amecop { set; get; }
        public long articulo_id { set; get; }
        public string producto { set; get; }
        public string caducidad { set; get; }
        public string lote { set; get; }
        public long cantidad { set; get; }
        public int es_antibiotico { set; get; }
        public int contiene_controlados { set; get; }
        public bool check { set; get; }
        public long? receta_id { set; get; }
    }

    public class DTO_control_ab_recetas
    {
        public long? control_antibioticos_receta_id { set; get; }
        public string doctor { set; get; }
        public string direccion_consultorio { set; get; }
        public long? cedula_profesional { set; get; }
        public string folio_receta { set; get; }
        public string comentarios { set; get; }
        public string tipo_receta { set; get; }
    }

    public class DTO_reporte_recetas
    {
        public string   fecha_receta            { set; get; }
        public string   folio_receta            { set; get; }
        public string   fecha_venta             { set; get; }
        public long     venta_folio             { set; get; }
        public string   doctor                  { set; get; }
        public long?    cedula_profesional      { set; get; }
        public string direccion_consultorio { set; get; }
        public long? control_ab_receta_id { set; get; }
        public long? control_antibiotico_id { set; get; }
        public string comentarios { set; get; }
        public long elemento_id { set; get; }
        public string codigos { set; get; }
    }

    public class DTO_reporte_ab
    {
        public long   folio {set; get;}
        public string mayorista {set; get;}
        public string termino {set; get;}
        public string comentarios {set; get;}
        public string fecha_terminado {set; get;}
    }
}
