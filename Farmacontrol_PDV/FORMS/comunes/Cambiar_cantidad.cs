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
	public partial class Cambiar_cantidad : Form
	{
		public long nueva_cantidad = 0;
		private DataGridViewRow row = new DataGridViewRow();

		public Cambiar_cantidad(long cantidad_inicial)
		{
			InitializeComponent();
			nueva_cantidad = cantidad_inicial;
			txt_cantidad.Text = nueva_cantidad.ToString();
			txt_cantidad.SelectAll();
			txt_cantidad.Focus();
		}

		private void txt_cantidad_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 27:
					this.Close();
				break;
				case 13:
					if(txt_cantidad.TextLength > 0)
					{
						if(Convert.ToInt32(txt_cantidad.Text) > 0)
						{
							nueva_cantidad = Convert.ToInt32(txt_cantidad.Text);
							this.Close();	
						}
						else
						{
							MessageBox.Show(this,"Imposible incluir cantidad 0","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
							txt_cantidad.SelectAll();
							txt_cantidad.Focus();
						}

						/*
						DAO.DAO_Cotizaciones dao_cotizaciones = new DAO.DAO_Cotizaciones();
						dao_cotizaciones.insertar_detallado(row.Cells["amecop"].Value.ToString(),row.Cells["caducidad_sin_formato"].Value.ToString(),row.Cells["lote"].Value.ToString(),Convert.ToInt32(txt_cantidad.Text),cotizacion_id);
						this.Close();
						 **/
					}
				break;
			}
		}

		private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (Char.IsDigit(e.KeyChar))
			{
				e.Handled = false;
			}
			else if (Char.IsControl(e.KeyChar))
			{
				e.Handled = false;
			}
			else
			{
				e.Handled = true;
			}
		}

	}
}
