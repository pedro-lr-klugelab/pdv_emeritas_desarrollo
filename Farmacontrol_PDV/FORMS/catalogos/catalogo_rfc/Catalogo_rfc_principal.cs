using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.FORMS.catalogos.catalogo_rfc
{
	public partial class Catalogo_rfc_principal : Form
	{
		DAO_Rfcs dao_rfcs = new DAO_Rfcs();

		public Catalogo_rfc_principal()
		{
			InitializeComponent();
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void txt_busqueda_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode =  Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				
				case 13:
					if(txt_busqueda.Text.Trim().Length > 0)
					{
						dgv_rfc.DataSource = dao_rfcs.get_rfc_registros(txt_busqueda.Text);
						dgv_rfc.ClearSelection();
					}
				break;
				case 27:
					dgv_rfc.AutoGenerateColumns = false;
					dgv_rfc.DataSource = null;
					txt_busqueda.Text = "";
				break;
				case 40:
					if(dgv_rfc.Rows.Count > 0)
					{
						dgv_rfc.CurrentCell = dgv_rfc.Rows[0].Cells["c_rfc"];
						dgv_rfc.Rows[0].Selected = true;
						dgv_rfc.Focus();
					}
				break;
			}
		}

		private void txt_busqueda_Enter(object sender, EventArgs e)
		{
			dgv_rfc.ClearSelection();
		}

		private void dgv_rfc_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(dgv_rfc.SelectedRows.Count > 0)
					{
						string rfc_registro_id = dgv_rfc.SelectedRows[0].Cells["c_rfc_registro_id"].Value.ToString();
						Editar_rfc editar_rfc = new Editar_rfc(rfc_registro_id);
						editar_rfc.ShowDialog();
					}
				break;
				case 27:
					txt_busqueda.Focus();
					dgv_rfc.ClearSelection();
				break;
			}
		}

		private void btn_registrar_rfc_Click(object sender, EventArgs e)
		{
			Editar_rfc editar_rfc = new Editar_rfc();
			editar_rfc.ShowDialog();
			if(editar_rfc.registro)
			{
				txt_busqueda.Text = editar_rfc.razon_social;
				dgv_rfc.DataSource = dao_rfcs.get_rfc_registros(editar_rfc.razon_social);
				dgv_rfc.ClearSelection();
				txt_busqueda.Focus();
			}
		}

		private void verFacturasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if(dgv_rfc.SelectedRows.Count > 0)
			{
				Catalogo_facturas cat_fac = new Catalogo_facturas(dgv_rfc.SelectedRows[0].Cells["c_rfc"].Value.ToString());
				cat_fac.ShowDialog();
			}
		}

        private void dgv_rfc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_rfc.SelectedRows.Count > 0)
            {
                string rfc_registro_id = dgv_rfc.SelectedRows[0].Cells["c_rfc_registro_id"].Value.ToString();
                Editar_rfc editar_rfc = new Editar_rfc(rfc_registro_id);
                editar_rfc.ShowDialog();
            }
        }
	}
}
