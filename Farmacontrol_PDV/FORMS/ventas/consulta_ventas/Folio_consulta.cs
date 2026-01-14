using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.HELPERS;
using System.Text.RegularExpressions;

namespace Farmacontrol_PDV.FORMS.ventas.consulta_ventas
{
	public partial class Folio_consulta : Form
	{
		DAO_Ventas dao_ventas = new DAO_Ventas();
		public long venta_id = 0;

		public Folio_consulta()
		{
			InitializeComponent();
		}

		public void validar_venta_id(long venta_id)
		{
			var venta_data = dao_ventas.get_venta_data(venta_id);

			if(venta_data.venta_id > 0)
			{
				this.venta_id = venta_id;
				this.Close();
			}
			else
			{
				MessageBox.Show(this,"Esta venta no se encuentra registrada","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				txt_venta_id.SelectAll();
				txt_venta_id.Focus();
			}
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void txt_venta_id_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:

                    if (Misc_helper.validar_codigo_venta(txt_venta_id.Text.Trim()))
                    {
                        string[] codigo = txt_venta_id.Text.Split('$');

                        if (codigo.Length == 2)
                        {
                            bool todos_numeros = true;

                            foreach (string items in codigo)
                            {
                                int n;
                                bool isNumeric = int.TryParse(items, out n);

                                if (isNumeric == false)
                                {
                                    todos_numeros = false;
                                    break;
                                }
                            }

                            if (todos_numeros)
                            {
                                if (Convert.ToInt64(codigo[0]).Equals(Convert.ToInt64(Config_helper.get_config_local("sucursal_id"))))
                                {
                                    validar_venta_id(Convert.ToInt64(codigo[1]));
                                }
                                else
                                {
                                    MessageBox.Show(this, "Este codigo de venta pertenece a otra sucursal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show(this, "Codigo de venta inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            
                            long sucursal_id;
                            sucursal_id = Convert.ToInt64(Config_helper.get_config_local("sucursal_id"));
                            long tmp_venta_id;
                            tmp_venta_id = (long)dao_ventas.get_venta_id_por_venta_folio(Convert.ToInt64(txt_venta_id.Text.Trim()));
                            validar_venta_id(tmp_venta_id);
                            
                            //MessageBox.Show(this, "Codigo de venta inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this,"Codigo Invalido!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
				break;
				case 27:
					if(txt_venta_id.TextLength > 0)
					{
						txt_venta_id.Text = "";
					}
					else
					{
						this.Close();
					}
				break;
			}
		}

		private void txt_venta_id_KeyPress(object sender, KeyPressEventArgs e)
		{
			//e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
		}

        private void txt_venta_id_TextChanged(object sender, EventArgs e)
        {

        }
	}
}
