using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Apartado_mercancia : INotifyPropertyChanged
	{
		public long apartado_id { set; get; }
		public string destino_sin_formato { set; get; }
		public string destino { set; get; }
		public string amecop { set; get; }
		public string producto { set; get; }
		public string caducidad { set; get;}
		public string lote { set; get; }
		public long cantidad { set; get; }
		public DateTime fecha_apartado { set; get; }
		public DateTime fecha_expiracion { set; get; }

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(string name)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}
	}
}
