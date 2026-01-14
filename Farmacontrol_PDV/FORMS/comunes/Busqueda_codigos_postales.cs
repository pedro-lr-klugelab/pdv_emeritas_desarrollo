using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.FORMS.comunes
{
	public partial class Busqueda_codigos_postales : Form
	{
		public long? asentamiento_id_g = null;
		public string codigo_postal_cp = "";
		public string nombre_cp = "";
		public string municipio_cp = "";
		public string ciudad_cp = "";
		public string estado_cp = "";

		public Busqueda_codigos_postales()
		{
			InitializeComponent();
		}

		private void txt_codigo_postal_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(txt_codigo_postal.TextLength > 0)
					{
						dgv_codigos_postales.ClearSelection();
					}
				break;
				case 27:
					if(txt_codigo_postal.TextLength > 0)
					{
						txt_codigo_postal.Text = "";
					}
					else
					{
						asentamiento_id_g = null;
						this.Close();
					}
				break;
				case 40:
					if(dgv_codigos_postales.Rows.Count > 0)
					{
						dgv_codigos_postales.CurrentCell =  dgv_codigos_postales.Rows[0].Cells["nombre"];
						dgv_codigos_postales.Rows[0].Selected = true;
						dgv_codigos_postales.Focus();
					}
				break;
			}
		}

		private void dgv_codigos_postales_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 27:
					txt_codigo_postal.Focus();
					dgv_codigos_postales.ClearSelection();
				break;
				case 13:
					asentamiento_id_g = Convert.ToInt64(dgv_codigos_postales.SelectedRows[0].Cells["asentamiento_id"].Value);
					nombre_cp = dgv_codigos_postales.SelectedRows[0].Cells["nombre"].Value.ToString();
					municipio_cp = dgv_codigos_postales.SelectedRows[0].Cells["municipio"].Value.ToString();
					ciudad_cp = dgv_codigos_postales.SelectedRows[0].Cells["ciudad"].Value.ToString();
					estado_cp = dgv_codigos_postales.SelectedRows[0].Cells["estado"].Value.ToString();
					codigo_postal_cp = dgv_codigos_postales.SelectedRows[0].Cells["codigo_postal"].Value.ToString();
					this.Close();
				break;
			}
		}

		private void txt_codigo_postal_Enter(object sender, EventArgs e)
		{
			dgv_codigos_postales.ClearSelection();
		}
	}
}
