using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.FORMS.ventas.facturacion
{
	public partial class Venta_factura : Form
	{
		private long venta_id;
		public bool venta_correcta = false;

		public Venta_factura(long venta_id)
		{
			this.venta_id = venta_id;
			InitializeComponent();
		}

		private void Venta_factura_Shown(object sender, EventArgs e)
		{
			DAO_Ventas dao_ventas = new DAO_Ventas();
			dgv_ventas.DataSource = dao_ventas.get_productos_venta(venta_id);
			var totales = dao_ventas.get_totales(venta_id);

			txt_total.Text = totales.total.ToString("C2");
			txt_excento.Text = totales.excento.ToString("C2");
			txt_gravado.Text = totales.gravado.ToString("C2");
			txt_ieps.Text = totales.importe_ieps.ToString("C2");
			txt_iva.Text = totales.importe_iva.ToString("C2");
			txt_subtotal.Text = totales.subtotal.ToString("C2");
			txt_piezas.Text = totales.piezas.ToString();

			dgv_ventas.ClearSelection();
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btn_aceptar_Click(object sender, EventArgs e)
		{
			if(chb_acepto.Checked)
			{
				venta_correcta = true;
				this.Close();
			}
			else
			{
				MessageBox.Show(this,"Para continuar con la factura es necesario que confirme que los productos son los correctos.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}
	}
}
