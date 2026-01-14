using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Inventario_folio_jornada : INotifyPropertyChanged
	{
		public long inventario_folio_id { set; get; }
		public string terminal { set; get; }
		public string comentarios { set; get; }
		public string estado { set; get; }

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(string name)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}
	}

	class DTO_Inventario
	{
		public long inventario_id { set; get; }
		public int terminal_id { set; get; }
		public long empleado_id { set; get; }
		public long termina_empleado_id { set; get; }
		public DateTime? fecha_inicio { set; get; }
		public DateTime? fecha_fin { set; get; }
		public string comentarios { set; get; }
		public string nombre_empleado_captura { set; get; }
		public string nombre_empleado_termina { set; get; }
		public bool aceptando_capturas { set; get; }
	}

	class DTO_Inventario_folio
	{
		public long inventario_folio_id { set; get; }
		public int terminal_id { set; get; }
		public long inventario_id { set; get; }
		public long empleado_id { set; get; }
		public long termina_empleado_id { set; get; }
		public string fecha_creado { set; get; }
		public string fecha_terminado { set; get; }
		public string comentarios { set; get; }
		public string nombre_empleado_captura { set; get; }
		public string nombre_empleado_termina { set; get; }
		public string nombre_terminal { set; get; }
	}

	class DTO_Detallado_no_inventariados
	{
		public string amecop { set; get; }
		public string nombre { set; get; }
		public string caducidad { set; get; }
		public string lote { set; get; }
		public decimal precio_costo { set; get; }
		public List<Tuple<string, string, int>> caducidades_lotes { set; get; }
	}

	class DTO_Detallado_diferencias
	{
		public string amecop { set; get; }
		public string nombre { set; get; }
		public string diferencia { set; get; }
		public decimal? sobrante { set; get; }
		public decimal? faltante { set; get; }
	}
}
