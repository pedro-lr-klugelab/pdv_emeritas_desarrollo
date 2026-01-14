using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.CLASSES.PRINT;

namespace Farmacontrol_PDV.FORMS.ventas.consulta_ventas
{
	public partial class Consulta_ventas_principal : Form
	{
		DAO_Ventas dao_ventas = new DAO_Ventas();
		private long venta_id;

		public Consulta_ventas_principal(long venta_id)
		{
			InitializeComponent();
			this.venta_id = venta_id;
		}

		public Consulta_ventas_principal()
		{
			InitializeComponent();
			this.venta_id = 0;
		}

		public void rellenar_informacion_venta()
		{
			var venta_data = dao_ventas.get_venta_data(venta_id);
			txt_nombre_empleado.Text = venta_data.nombre_empleado;
			lbl_folio.Text = "#" + venta_data.venta_folio;
			txt_servicio_domicilio.Text = venta_data.servicio_domicilio;
			txt_venta_credito.Text = venta_data.nombre_cliente_credito;

			dgv_ventas.DataSource = dao_ventas.get_productos_venta((long)venta_id);

            //revisar si tiene Antibioticos
            
            if (dao_ventas.is_venta_antibiotico((long)venta_id))
                btn_reimprimir_venta.Visible = true;            
            else
                btn_reimprimir_venta.Visible = false;
            
            //
            

			get_totales();
		}

		public void validar_venta(long folio_venta)
		{
			if(folio_venta == 0)
			{
				Folio_consulta folio_consulta = new Folio_consulta();
				folio_consulta.ShowDialog();
				if(folio_consulta.venta_id != 0)
				{
					this.venta_id = folio_consulta.venta_id;
					rellenar_informacion_venta();	
				}
				else
				{
					
					folio_consulta.Close();
					this.Close();
				}
			}
			else
			{
				this.venta_id = folio_venta;
				rellenar_informacion_venta();
			}
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
			btn_consultar.Text = string.Format("{0:C}", iva);
			txt_piezas.Text = piezas.ToString();
			txt_excento.Text = string.Format("{0:C}", excento);
			txt_gravado.Text = string.Format("{0:C}", gravado);
		}

		private void Consulta_ventas_principal_Shown(object sender, EventArgs e)
		{
			validar_venta(venta_id);
			btn_reimprimir_venta.Focus();
			dgv_ventas.ClearSelection();
		}

		private void btn_reimprimir_venta_Click(object sender, EventArgs e)
		{
			Ticket_venta ticket_venta = new Ticket_venta();
			ticket_venta.construccion_ticket((long)venta_id,true);
			ticket_venta.print();
		}

		private void btn_reimprimir_factura_Click(object sender, EventArgs e)
		{
			Facturacion facturacion =  new Facturacion();
			//facturacion.construccion_ticket((long)venta_id,true);
			facturacion.print();
		}

        private void dgv_ventas_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch(keycode)
            {
                case 67:
                    if (e.Control)
                    {
                        Clipboard.SetText(dgv_ventas.SelectedRows[0].Cells["amecop"].Value.ToString());
                    }
                break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            validar_venta(0);
        }
	}
}
