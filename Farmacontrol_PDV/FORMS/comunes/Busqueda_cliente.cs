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
	public partial class Busqueda_cliente : Form
	{
		DAO_Clientes dao_clientes = new DAO_Clientes();
		public string cliente_id = "";
		public string nombre_cliente = "";

		public Busqueda_cliente()
		{
			InitializeComponent();
		}

		private void txt_busqueda_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 40:
					if (dgv_clientes.Rows.Count > 0)
					{
						dgv_clientes.CurrentCell = dgv_clientes.Rows[0].Cells["nombre"];
						dgv_clientes.Rows[0].Selected = true;
						dgv_clientes.Focus();
					}
					break;
				case 13:
                    if (txt_busqueda.Text.Trim().Length > 4)
                    {
                        txt_busqueda.Text = txt_busqueda.Text.Trim().Replace("?", "");
                        buscar();
                    }
                    else
                    {
                        MessageBox.Show(this, "Error,la busqueda debe de tener al menos 4 elementos para poder realizarla ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    }
					break;
				case 27:
					txt_busqueda.Text = "";
					break;
			}
		}

		private void buscar()
		{
			dgv_clientes.DataSource = dao_clientes.get_clientes(txt_busqueda.Text);
			dgv_clientes.ClearSelection();
			txt_busqueda.Focus();
		}

		private void dgv_clientes_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 13:
					if (dgv_clientes.SelectedRows.Count > 0)
					{
						cliente_id = dgv_clientes.SelectedRows[0].Cells["c_cliente_id"].Value.ToString();
						nombre_cliente = dgv_clientes.SelectedRows[0].Cells["nombre"].Value.ToString();
						this.Close();
					}
					break;
				case 27:
					txt_busqueda.Focus();
					break;
			}
		}

		private void txt_busqueda_Enter(object sender, EventArgs e)
		{
			dgv_clientes.ClearSelection();
		}

		private void Busqueda_cliente_Shown(object sender, EventArgs e)
		{
			txt_busqueda.Focus();
		}
	}
}
