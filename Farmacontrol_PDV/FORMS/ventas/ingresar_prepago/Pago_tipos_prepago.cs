using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;

namespace Farmacontrol_PDV.FORMS.ventas.ingresar_prepago
{
	public partial class Pago_tipos_prepago : Form
	{
		decimal total = 0;
		public bool terminar_prepago = false;
		DAO_Pago_tipos dao_pago_tipos = new DAO_Pago_tipos();

		public Pago_tipos_prepago(decimal total)
		{
			this.total = total;
			InitializeComponent();
			lbl_total_pagar.Text = total.ToString("C2");
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void chb_confirmacion_CheckedChanged(object sender, EventArgs e)
		{
			btn_generar_prepago.Enabled = chb_confirmacion.Checked;
		}

		private void btn_generar_prepago_Click(object sender, EventArgs e)
		{
			terminar_prepago = true;
			this.Close();
		}
	}
}
