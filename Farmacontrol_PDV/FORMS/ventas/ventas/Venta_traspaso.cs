using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.FORMS.ventas.ventas
{
	public partial class Venta_traspaso : Form
	{
		private long venta_id;
		DTO_Traspaso dto_traspaso;
		DAO_Traspasos dao_traspaso = new DAO_Traspasos();
		private string hash;
		public bool venta_terminada = false;

		public Venta_traspaso(long venta_id, object dto_traspaso, string hash)
		{
			this.hash = hash;
			this.venta_id = venta_id;
			this.dto_traspaso = (DTO_Traspaso)dto_traspaso;
			InitializeComponent();
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Venta_traspaso_Shown(object sender, EventArgs e)
		{

			List<Tuple<int,string,string,int>> lista_productos = new List<Tuple<int,string,string,int>>();

			foreach (DTO_Detallado_traspaso detallado in dto_traspaso.detallado_traspaso)
			{
				Tuple<int,string,string,int> tupla = new Tuple<int,string,string,int>((int)detallado.articulo_id,detallado.caducidad,detallado.lote,(int)detallado.cantidad);
				lista_productos.Add(tupla);
			}

			dgv_ventas.DataSource = dao_traspaso.get_productos_para_venta(lista_productos);
			get_totales();

			dgv_ventas.ClearSelection();
		}

		public void get_totales()
		{
			DataTable calculo_totales = dgv_ventas.DataSource as DataTable;

			decimal total = 0;
			decimal subtotal = 0;
			int piezas = 0;
			decimal iva = 0;
			decimal ieps = 0;
			decimal gravado = 0;
			decimal excento = 0;

			foreach (DataRow row in calculo_totales.Rows)
			{
				total += Convert.ToDecimal(row["total"]);
				subtotal += Convert.ToDecimal(row["subtotal"]);
				iva += Convert.ToDecimal(row["importe_iva"]);
				ieps += Convert.ToDecimal(row["importe_ieps"]);
				piezas += Convert.ToInt32(row["cantidad"]);

				if (Convert.ToDecimal(row["importe_iva"]) > ((decimal)0))
				{
					gravado += Convert.ToDecimal(row["subtotal"]);
				}
				else
				{
					excento += Convert.ToDecimal(row["subtotal"]);
				}
			}

			txt_total.Text = string.Format("{0:C}", total);
			txt_subtotal.Text = string.Format("{0:C}", subtotal);
			txt_ieps.Text = string.Format("{0:C}", ieps);
			txt_iva.Text = string.Format("{0:C}", iva);
			txt_piezas.Text = piezas.ToString();
			txt_excento.Text = string.Format("{0:C}", excento);
			txt_gravado.Text = string.Format("{0:C}", gravado);
		}

		private void btn_terminar_venta_Click(object sender, EventArgs e)
		{
			decimal total_pagar = Convert.ToDecimal(txt_total.Text.ToString().Trim(new char[] { '$', ' ', ',' }));
			Pago_tipos pago_tipos = new Pago_tipos((long)venta_id, total_pagar,(DataTable)dgv_ventas.DataSource,hash);
			pago_tipos.ShowDialog();

			if (pago_tipos.venta_terminada)
			{
				venta_terminada = true;
				this.Close();
			}
		}
	}
}
