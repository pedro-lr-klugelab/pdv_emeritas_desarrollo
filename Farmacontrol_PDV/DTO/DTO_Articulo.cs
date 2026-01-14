using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;

namespace Farmacontrol_PDV.DTO
{
	public class DTO_busqueda_articulo_data
	{
		public long articulo_id { set; get; }
		public int activo {set; get; }
		public string amecop { set; get; }
		public string producto { set; get; }
		public string caducidad { set; get; }
		public string lote { set; get; }
		public long existencia_total { set; get; }
		public long existencia_devoluciones { set; get; }
		public long existencia_mermas { set; get; }

	}

	public class DTO_Articulo_generic
	{
		public long articulo_id { set; get; }
		public string amecop { set; get; }
		public string producto { set; get; }
		public string caducidad { set; get; }
		public string lote {set; get; }
	}

	public class DTO_Articulo
	{
		private int? articulo_id = null;
        public bool activo { set; get; }
        public long? clase_antibiotico_id { set; get; }
		private string amecop;
		private string nombre;
		private decimal pct_iva;
		private string tipo_descuento;
		private decimal pct_descuento;
		private decimal precio_publico;
		private decimal precio_costo;

		public string tipo_ieps { set; get; }
		public decimal ieps { set; get; }

		private DataTable caducidades;
		private DataTable lotes;

		public int? Articulo_id
		{
			get { return articulo_id; }
			set { articulo_id = value; }
		}

		public string Amecop
		{
			get { return amecop; }
			set { amecop = value; }
		}

		public string Nombre
		{
			get { return nombre; }
			set { nombre = value; }
		}

		public decimal Precio_costo
		{
			get { return precio_costo; }
			set { precio_costo = value; }
		}

		public decimal Precio_publico
		{
			get { return precio_publico; }
			set { precio_publico = value; }
		}

		public decimal Pct_iva
		{
			get { return pct_iva; }
			set { pct_iva = value; }
		}

		public string Tipo_descuento
		{
			get { return tipo_descuento; }
			set { tipo_descuento = value; }
		}

		public decimal Pct_descuento
		{
			get { return pct_descuento; }
			set { pct_descuento = value; }
		}

		public DataTable Caducidades
		{
			get { return caducidades; }
			set { caducidades = value; }
		}

		public DataTable Lotes
		{
			get { return lotes; }
			set { lotes = value; }
		}
	}

	class Busqueda_articulos_existencias : INotifyPropertyChanged
	{
		public string amecop { set; get; }
		public int activo { set; get; }
        public decimal pct_descuento { set; get; }
		public string nombre { set; get; }
		public long articulo_id { set; get; }
		public string caducidad { set; get; }
		public string lote { set; get; }
		public string caducidad_sin_formato { set; get; }
		public long existencia_total { set; get; }
		public long existencia_ventas { set; get; }
		public long existencia_devoluciones { set; get; }
		public long existencia_mermas { set; get; }
		public long existencia_cambio_fisico { set; get; }
		public long existencia_apartados { set; get; }
		public long existencia_traspasos { set; get; }
		public long existencia_mayoreo { set; get; }
		public long existencia_prepago { set; get; }
		public long existencia_vendible { set; get; }
        public bool es_antibiotico { set; get; }
		public decimal precio_publico { set; get; }

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(string name)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}
	}

	class Result_existencia_caducidades
	{
		public string caducidad { set; get; }
		public long existencia { set; get; }
	}

    public class GetArticulosDTO
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public int articulo_id { get; set; }
        public int existencia_total { get; set; }
		public string lote {  get; set; }
		public string caducidad {  get; set; }

        
    }
}
