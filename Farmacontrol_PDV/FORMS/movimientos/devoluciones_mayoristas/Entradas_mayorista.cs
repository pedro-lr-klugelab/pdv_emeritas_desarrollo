using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.FORMS.movimientos.devoluciones_mayoristas
{
	public partial class Entradas_mayorista : Form
	{
		DAO_Entradas dao_entradas = new DAO_Entradas();
		private long mayorista_id;
		public long entrada_id = 0;

		public Entradas_mayorista(long mayorista_id)
		{
			this.mayorista_id = mayorista_id;
			InitializeComponent();
		}

		public void get_entradas()
		{
			dgv_entradas.DataSource = dao_entradas.get_entradas_mayorista(mayorista_id);
			txt_factura.Focus();
			dgv_entradas.ClearSelection();
		}

		private void Entradas_mayorista_Shown(object sender, EventArgs e)
		{
			get_entradas();
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			entrada_id = 0;
			this.Close();
		}

		private void txt_factura_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 40:
					if (dgv_entradas.Rows.Count > 0)
					{
						dgv_entradas.CurrentCell = dgv_entradas.Rows[0].Cells["c_entrada_id"];
						dgv_entradas.Rows[0].Selected = true;
						dgv_entradas.Focus();
					}
				break;
				case 13:
					if(txt_factura.Text.Trim().Length > 0)
					{
						if(radio_folio.Checked)
						{
							dgv_entradas.DataSource = dao_entradas.get_entradas_mayorista(mayorista_id, Convert.ToInt32(txt_factura.Text));
						}
						else
						{
							dgv_entradas.DataSource = dao_entradas.get_entradas_mayorista(mayorista_id, txt_factura.Text.Trim());
						}

						dgv_entradas.ClearSelection();
						txt_factura.Focus();
					}
				break;
				case 27:
					if (txt_factura.Text.Trim().Length > 0)
					{
						txt_factura.Text = "";
					}
					else
					{
						entrada_id = 0;
						this.Close();
					}
				break;
			}
		}

		private void txt_factura_Enter(object sender, EventArgs e)
		{
			dgv_entradas.ClearSelection();
		}

		private void dgv_entradas_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 27:
					txt_factura.Focus();
				break;
				case 13:
					if(dgv_entradas.SelectedRows.Count > 0)
					{
						entrada_id = Convert.ToInt64(dgv_entradas.SelectedRows[0].Cells["c_entrada_id"].Value);
						this.Close();
					}
				break;
			}
		}

		private void btn_aceptar_Click(object sender, EventArgs e)
		{
			if (dgv_entradas.SelectedRows.Count > 0)
			{
				entrada_id = Convert.ToInt64(dgv_entradas.SelectedRows[0].Cells["c_entrada_id"].Value);
				this.Close();
			}
		}

		private void txt_factura_KeyPress(object sender, KeyPressEventArgs e)
		{
			if(radio_folio.Checked)
			{
				e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
			}
		}

		private void radio_folio_Click(object sender, EventArgs e)
		{
			txt_factura.Text = "";
			txt_factura.Focus();
		}

		private void radio_factura_Click(object sender, EventArgs e)
		{
			txt_factura.Text = "";
			txt_factura.Focus();
		}
	}
}
