using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.FORMS.comunes;

namespace Farmacontrol_PDV.FORMS.catalogos.catalogo_clientes
{
	public partial class Catalogo_clientes_principal : Form
	{
		DAO_Clientes dao_clientes = new DAO_Clientes();
        public int? empleado_id = 0;

		public Catalogo_clientes_principal()
		{
			InitializeComponent();

            progBar.Visible = false;

		}

		private void txt_busqueda_cliente_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 40:
					if(dgv_clientes.Rows.Count > 0)
					{
						dgv_clientes.CurrentCell = dgv_clientes.Rows[0].Cells["nombre"];
						dgv_clientes.Rows[0].Selected = true;
						dgv_clientes.Focus();
					}
				break;
				case 27:
					txt_busqueda_cliente.Text = "";
				break;
				case 13:
					if(txt_busqueda_cliente.TextLength > 0)
					{
                        progBar.Visible = true;
                        Cursor = Cursors.WaitCursor;
                        progBar.Value = 40;
                        buscar_clientes();
                        progBar.Value = 100;
                        progBar.Visible = false;
                        Cursor = Cursors.Default;
					}
				break;
			}
		}

        void buscar_clientes()
        {
            dgv_clientes.DataSource = dao_clientes.get_clientes_by_nombre(txt_busqueda_cliente.Text);
            dgv_clientes.ClearSelection();
            txt_busqueda_cliente.Focus();
        }

		private void dgv_clientes_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
                    Login_form login = new Login_form();
                    login.ShowDialog();

                    if (login.empleado_id != null)
                    {
                        empleado_id = login.empleado_id;
                        editar_cliente();
                    }
                    else
                    {
                        MessageBox.Show(this, "Debe identificarse para poder continuar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
				break;
				case 27:
					txt_busqueda_cliente.Focus();
					dgv_clientes.ClearSelection();
				break;
			}
		}

        void editar_cliente()
        {
            Editar_cliente editar_cliente = new Editar_cliente(dgv_clientes.SelectedRows[0].Cells["cliente_id"].Value.ToString(), empleado_id);
            editar_cliente.ShowDialog();
            buscar_clientes();
        }

		private void btn_registrar_cliente_Click(object sender, EventArgs e)
		{
			Registro_clientes registro_clientes = new Registro_clientes();
			registro_clientes.ShowDialog();
			if(registro_clientes.cliente_registrado != "")
			{
				dgv_clientes.DataSource = dao_clientes.get_clientes_by_nombre(registro_clientes.cliente_registrado);
				dgv_clientes.ClearSelection();
				txt_busqueda_cliente.Focus();
			}
		}

		private void btn_editar_cliente_Click(object sender, EventArgs e)
		{

		}

		private void Catalogo_clientes_principal_Shown(object sender, EventArgs e)
		{
			var mdi = this.MdiParent;
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

        private void dgv_clientes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dgv_clientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_clientes.SelectedRows.Count > 0)
            {
                Login_form login = new Login_form();
                login.ShowDialog();

                if (login.empleado_id != null)
                {
                    empleado_id = login.empleado_id;
                    editar_cliente();
                   
                }
                else
                {
                    MessageBox.Show(this, "Debe identificarse para poder continuar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgv_clientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
	}
}
