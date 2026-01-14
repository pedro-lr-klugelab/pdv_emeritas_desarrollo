using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Ajustes_existencias
	{
		public long ajuste_existencia_id { set; get; }
		public int terminal_id { set; get; }
		public long empleado_id { set; get; }
		public long? termina_empleado_id { set; get; }
		public DateTime? fecha_creado { set; get; }
		public DateTime? fecha_terminado { set; get; }
		public string comentarios { set; get; }
		public string nombre_empleado_captura { set; get; }
		public string nombre_empleado_termina { set; get; }
	}

	class DTO_Detallado_ajustes_existencias
	{
		public long? detallado_ajuste_existencia_id { set; get; }
		public long ajuste_existencia_id  { set; get; }
		public int articulo_id { set; get; }
		public string caducidad { set; get; }
		public string lote { set; get; }
		public int existencia_anterior { set; get; }
		public int cantidad { set; get; }
		public int diferencia { set; get; }
	}

	class DTO_Detallado_ajuste_ticket
	{
		public int articulo_id { set; get; }
		public string amecop { set; get; }
		public string nombre { set; get; }
		public List<Tuple<string, string, int, int, int>> caducidades_lotes { set; get; }
	}

    public class DTO_Materia_Prima
    {
        public long articulo_id { get; set; }
        public string observaciones { get; set; }
		public decimal existencia_actual {  set; get; }
        public decimal volumen { set; get; }
		public decimal cantidad {  get; set; }
    }

}
