using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.FORMS.comunes
{
	public partial class Rastreo_piezas : Form
	{
		DTO_Articulo_generic articulo = new DTO_Articulo_generic();

		public Rastreo_piezas(DTO_Articulo_generic articulo)
		{
			this.articulo = articulo;
			InitializeComponent();
		}

		void  rastreo_productos()
		{
			txt_amecop.Text = articulo.amecop.Replace("*","");
			txt_producto.Text = articulo.producto;

			cbb_caducidad.DataSource =  new BindingSource(new Dictionary<string,string>(){
				{Misc_helper.fecha(articulo.caducidad,"CADUCIDAD"),Misc_helper.fecha(articulo.caducidad)}
			},null);

            //FEB 2019 2019-02-01

			cbb_caducidad.DisplayMember = "Key";
			cbb_caducidad.ValueMember = "Value";

			cbb_lote.DataSource = new BindingSource(new Dictionary<string, string>(){
				{articulo.lote,articulo.lote}
			}, null);

			cbb_lote.DisplayMember = "Key";
			cbb_lote.ValueMember = "Value";

			DAO_Existencias dao_existencias = new DAO_Existencias();
			
			dgv_ventas.DataSource = dao_existencias.get_rastreo_existencias_ventas(articulo);
			dgv_ventas.ClearSelection();

			dgv_devoluciones.DataSource = dao_existencias.get_rastreo_existencias_devoluciones(articulo);
			dgv_devoluciones.ClearSelection();

			dgv_mermas.DataSource = dao_existencias.get_rastreo_existencias_mermas(articulo);
			dgv_mermas.ClearSelection();

			dgv_apartado_mercancia.DataSource = dao_existencias.get_rastreo_apartado_mercancia(articulo);
			dgv_apartado_mercancia.ClearSelection();

			dgv_traspasos.DataSource = dao_existencias.get_rastreo_traspasos(articulo);
			dgv_traspasos.ClearSelection();

			dgv_mayoreo_ventas.DataSource = dao_existencias.get_rastreo_mayoreo_ventas(articulo);
			dgv_mayoreo_ventas.ClearSelection();
		}

		private void Rastreo_piezas_Shown(object sender, EventArgs e)
		{
			rastreo_productos();
		}
	}
}
