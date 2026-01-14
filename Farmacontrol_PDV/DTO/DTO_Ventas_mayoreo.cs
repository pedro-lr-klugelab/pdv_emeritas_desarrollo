using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Detallado_mayoreo_ventas : INotifyPropertyChanged
	{
		public long detallado_mayoreo_id { set; get;}
		public string amecop { set; get; }
		public string producto { set; get; }
		public string caducidad { set; get; }
		public string lote { set; get; }
		public decimal precio_costo { set; get; }
		public decimal importe_iva_captura { set; get; }
		public long cantidad_capturada { set; get; }
		public long cantidad_revision { set; get; }
		public long diferencia { set; get; }
		public decimal total_revision { set; get; }
		public decimal total_captura { set; get; }
		public decimal importe_iva_revision { set; get; }

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(string name)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}
	}

	class DTO_Ventas_mayoreo
	{
		public long mayoreo_venta_id { set; get; }
		public long? terminal_id { set; get; }
		public long? terminal_id_revision { set; get; }
		public string cliente_id { set; get; }
		public long empleado_id { set; get; }
		public long termina_empleado_id { set; get; }
		public DateTime? fecha_creado { set; get; }
		public DateTime? fecha_impreso { set; get; }
		public DateTime? fecha_inicio_verifiacion { set; get; }
		public DateTime? fecha_fin_verificacion { set; get; }
		public DateTime? fecha_terminado { set; get; }

		public string comentarios { set; get; }
		public string nombre_empleado_captura { set; get; }
		public string nombre_empleado_termina { set; get; }
		public string nombre_empleado_inicio_verificacion { set; get; }
		public string nombre_empleado_fin_verificacion { set; get; }

		public string nombre_cliente { set; get; }
	}


	class DTO_Detallado_mayoreo_ventas_ticket
	{
		public int articulo_id { set; get; }
		public string amecop { set; get; }
		public string nombre { set; get; }
		public decimal precio_unitario { set; get; }
		public int cantidad { set; get; }
		public decimal subtotal_captura { set; get; }
		public decimal total_captura { set; get; }
		public decimal subtotal_revision { set; get; }
		public decimal total_revision { set; get; }
		public decimal importe_iva_captura { set; get; }
		public decimal importe_iva_revision { set; get; }

		public List<Tuple<string, string, int, int>> caducidades_lotes { set; get; }
	}
}
