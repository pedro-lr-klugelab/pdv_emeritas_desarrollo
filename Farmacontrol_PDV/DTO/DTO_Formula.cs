using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Farmacontrol_PDV.DTO
{
	public class DTO_Detallado_formula_elaborada
	{
		public string amecop { set; get; }
		public string nombre { set; get; }
		public long? articulo_id { set; get; }
		public long? materia_prima_id { set; get; }
		public string caducidad { set; get; }
		public string lote { set; get; }
		public decimal cantidad { set; get; }
	}
		
	class DTO_Formula_rest
	{
		public string formula_id { set; get; }
		public long sucursal_id {set; get; }
		public long? venta_id { set; get; }
		public long empleado_id { set; get; }
		public long? empleado_id_elabora { set; get; }
		public DateTime fecha_creado { set; get; }
		public string nombre_cliente { set; get; }
		public string nombre_doctor { set; get; }
		public string comentarios { set; get; }
		public List<DTO_Detallado_formulas> detallado { set; get; }
	}

	class DTO_Formulas_pendientes : INotifyPropertyChanged
	{
		public long sucursal_id { set; get; }
		public string formula_id { set; get; }
		public long sucursal_folio {set; get; }
		public string sucursal { set; get; }
		public DateTime fecha_creado { set; get; }
		public DateTime? fecha_elaborado { set; get; }
		public string status { set; get; }
		public List<DTO_Detallado_formulas> detallado { set; get; }

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(string name)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}
	}

	class DTO_Formula
	{
		public string formula_id { set; get; }
		public long sucursal_id { set; get; }
		public long sucursal_folio { set; get; }
		public long? venta_id { set; get; }
		public long empleado_id { set; get; }
		public long? empleado_id_elabora { set; get; }
		public DateTime fecha_creado { set; get; }
		public DateTime? fecha_elaborado { set; get; }
		public string nombre_cliente { set; get; }
		public string nombre_doctor { set; get; }
		public string comentarios { set; get; }
		public string comentarios_elaboracion { set; get; }
	}

	class DTO_Detallado_formulas : INotifyPropertyChanged
	{
		public long? detallado_formula_id { set; get; }
		public string formula_id { set; get; }
		public long? materia_prima_id { set; get; }
		public string amecop { set; get; }
		public string nombre { set; get; }
		public long? articulo_id { set; get; }
		public decimal precio_publico { set; get; }
		public decimal importe { set; get; }
		public decimal cantidad { set; get; }
		public decimal subtotal { set; get; }
		public decimal total { set; get; }
		public string comentarios { set; get; }

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(string name)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}
	}
}
