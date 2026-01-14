using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Farmacontrol_PDV.FORMS.comunes
{
	public partial class Busqueda_creditos : Form
	{
		public string cliente_id = "";
		public Busqueda_creditos()
		{
			InitializeComponent();
            progBarCredito.Hide();
		}

		public void get_clientes_creditos()
		{
            progBarCredito.Show();
            progBarCredito.Value = 50;
			DAO.DAO_Clientes dao_clientes = new DAO.DAO_Clientes();
            /*DETECTAR SI ES NUMERO O LETRAS*/
            string cadena_entrada = txt_nombre_cliente.Text;
            bool bandera_numeros = false;
            if (cadena_entrada.All(char.IsDigit))
            {
                bandera_numeros = true;
            }

            dgv_clientes_creditos.DataSource = dao_clientes.get_clientes_creditos(txt_nombre_cliente.Text, bandera_numeros);
            progBarCredito.Value = 80;
			dgv_clientes_creditos.ClearSelection();
            progBarCredito.Value = 100;
            progBarCredito.Hide();
            txt_nombre_cliente.Enabled = true;
            txt_nombre_cliente.Focus();
		}

		private void txt_nombre_cliente_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
                    if (txt_nombre_cliente.TextLength > 3)
                    {
                        txt_nombre_cliente.Enabled = false;
                        get_clientes_creditos();

                    }
				break;
				case 27:
					if(txt_nombre_cliente.TextLength > 0)
						txt_nombre_cliente.Text = "";
						else
							cliente_id = "";
							this.Close();
				break;
				case 40:
                if (dgv_clientes_creditos.RowCount > 0)
                {
                    dgv_clientes_creditos.CurrentCell = dgv_clientes_creditos.Rows[0].Cells["nombre"];
                    dgv_clientes_creditos.Rows[0].Selected = true;
                    dgv_clientes_creditos.Focus();

                }
				break;
			}
		}

		private void dgv_clientes_creditos_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode =  Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:

                    if (  Convert.ToBoolean(dgv_clientes_creditos.SelectedRows[0].Cells["cliente_activo"].Value.ToString()))
                    {
                        if (Convert.ToDecimal(dgv_clientes_creditos.SelectedRows[0].Cells["saldo_disponible"].Value) > (decimal)0)
                        {
                            cliente_id = dgv_clientes_creditos.SelectedRows[0].Cells["columna_cliente_id"].Value.ToString();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(this, "Cliente sin crédito disponible", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show(this, "Cliente con crédito no activado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

					
							
				break;
				case 27:
					txt_nombre_cliente.Focus();
				break;
			}
		}

		private void txt_nombre_cliente_Enter(object sender, EventArgs e)
		{
			dgv_clientes_creditos.ClearSelection();
		}

		private void dgv_clientes_creditos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (this.dgv_clientes_creditos.Columns[e.ColumnIndex].Name == "saldo_disponible")
			{
				try
				{
					if (Convert.ToDecimal(e.Value) == (decimal)0)
						e.CellStyle.ForeColor = Color.Red;
				}
				catch (NullReferenceException exception)
				{
					Log_error.log(exception);
				}
			}
		}
	}
}
