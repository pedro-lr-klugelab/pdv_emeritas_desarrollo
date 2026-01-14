using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.FORMS.ventas.ventas
{
	public partial class Cupones : Form
	{
		public long cupon_id = 0;
		
		public Cupones()
		{
			InitializeComponent();
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void txt_codigo_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if (txt_codigo.Text.Trim().Length > 0)
					{
						DAO_Cupones dao_cupones = new DAO_Cupones();
						var validacion = dao_cupones.validar_cupon(txt_codigo.Text);

						if(validacion.status)
						{
							cupon_id = validacion.elemento_id;
							this.Close();
						}
						else
						{
							MessageBox.Show(this,validacion.informacion,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
							txt_codigo.SelectAll();
							txt_codigo.Focus();
						}
					}
				break;
				case 27:
					if(txt_codigo.Text.Trim().Length > 0)
					{
						txt_codigo.Text = "";
					}
					else
					{
						this.Close();
					}
				break;
			}
		}
	}
}
