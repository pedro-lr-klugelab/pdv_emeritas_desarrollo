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
using Farmacontrol_PDV.CLASSES.PRINT;

namespace Farmacontrol_PDV.FORMS.catalogos.catalogo_rfc
{
	public partial class Catalogo_facturas : Form
	{
		public string rfc;

		public Catalogo_facturas(string rfc)
		{
			this.rfc = rfc;
			InitializeComponent();
			get_facturas();
		}

		public void get_facturas()
		{
			DAO_Rfcs dao_r = new DAO_Rfcs();
			dgv_facturas.DataSource = dao_r.get_facturas_rfc(rfc);
		}

		private void btn_reimprimir_Click(object sender, EventArgs e)
		{
			if(dgv_facturas.SelectedRows.Count > 0)
			{
				long venta_id = Convert.ToInt64(dgv_facturas.SelectedRows[0].Cells["venta_id"].Value);
				long sucursal_id = Convert.ToInt64(dgv_facturas.SelectedRows[0].Cells["sucursal_id"].Value);
				var facturawsp = WebServicePac_helper.obtenerDatos(venta_id, sucursal_id);

				Facturacion ticket_factura = new Facturacion();
				ticket_factura.construccion_ticket(venta_id, facturawsp, true);
				ticket_factura.print();
			}
			else
			{
				MessageBox.Show(this,"Es necesario seleccionar la factura a re-imprimir","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}
	}
}
