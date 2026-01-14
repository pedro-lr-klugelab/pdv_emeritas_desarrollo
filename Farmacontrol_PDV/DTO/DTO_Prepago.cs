using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace Farmacontrol_PDV.DTO
{
	public class DTO_Prepago_parcial_entregado
	{
		public long articulo_id { set; get; }
		public string amecop { set; get; }
		public string producto { set; get; }
		public string caducidad { set; get; }
		public string lote { set; get; }
		public long cantidad { set; get; }
		public long cantidad_conservar { set; get; }
	}

	class DTO_Prepago
	{
		public long prepago_id { set; get; }
		public string cliente_id { set; get; }
		public long pago_empleado_id { set; get; }
		public long? canje_empleado_id { set; get; }
		public long? cancela_empleado_id { set; get; }
		public long? corte_parcial_id { set; get; }
		public long? corte_total_id { set; get; }
		public string codigo { set; get; }
		public DateTime fecha_pago { set; get; }
		public DateTime? fecha_canje { set; get; }
		public DateTime? fecha_cancelado { set; get; }
		public string tipo_devolucion { set; get; }
		public decimal monto { set; get; }
		public string comentario { set; get; }

		public string nombre_empleado { set; get; }
		public string nombre_empleado_cancela { set; get; }
		public string nombre_cliente { set; get; }
	}

	public class DTO_Detallado_prepago : INotifyPropertyChanged
	{
		public long detallado_prepago_id { set; get; }

		public string amecop { set; get; }
		public string amecop_completo { set; get; }
		public string producto { set; get; }

		public long prepago_id { set; get; }
		public long articulo_id { set; get; }
		public decimal precio_publico { set; get; }
		public decimal pct_descuento { set; get; }
		public decimal importe_descuento { set; get; }
		public decimal importe { set; get; }
		public long cantidad { set; get; }
		public long cantidad_entregada { set; get; }
		public decimal subtotal { set; get; }
		public decimal pct_iva { set; get; }
		public decimal importe_iva { set; get; }
		public string tipo_ieps { set; get; }
		public decimal ieps { set; get; }
		public decimal importe_ieps { set; get; }
		public decimal total { set; get; }

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(string name)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}
	}
}
