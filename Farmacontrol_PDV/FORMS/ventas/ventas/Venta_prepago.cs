using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.CLASSES.PRINT;

namespace Farmacontrol_PDV.FORMS.ventas.ventas
{
	public partial class Venta_prepago : Form
	{
		public bool prepago_afectado = false;
		List<DTO_Detallado_ventas_vista_previa> detallado = new List<DTO_Detallado_ventas_vista_previa>();
		decimal monto;
		long venta_id;
		long prepago_id;

		public Venta_prepago(long venta_id, List<DTO_Detallado_ventas_vista_previa> detallado, decimal monto, long prepago_id)
		{
			this.venta_id = venta_id;
			this.detallado = detallado;
			this.monto = monto;
			this.prepago_id = prepago_id;
			InitializeComponent();
			dgv_venta_previa.DataSource = detallado;
		}

		private void Venta_prepago_Shown(object sender, EventArgs e)
		{
			dgv_venta_previa.ClearSelection();
			btn_procesar_prepago.Enabled = chb_confirm.Checked;
		}

		private void chb_confirm_CheckedChanged(object sender, EventArgs e)
		{
			btn_procesar_prepago.Enabled = chb_confirm.Checked;
		}

		private void btn_procesar_prepago_Click(object sender, EventArgs e)
		{
			Login_form login = new Login_form();
			login.ShowDialog();

			if(login.empleado_id != null)
			{
				DialogResult drd = MessageBox.Show(this,"¿Este prepago se hara a domicilio?","Informacion Prepago",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

				if(drd == DialogResult.Yes)
				{
					Busqueda_clientes_domicilios clientes_domicilios = new Busqueda_clientes_domicilios();
					clientes_domicilios.ShowDialog();

					if(clientes_domicilios.cliente_domicilio_id != null)
					{
						procesar_prepago((long)login.empleado_id, clientes_domicilios.cliente_domicilio_id);
					}
				}
				else
				{
					procesar_prepago((long)login.empleado_id);
				}	
			}
		}

		void procesar_prepago(long empleado_id, string cliente_domicilio_id = "")
		{
			DialogResult dr = MessageBox.Show(this, "Procesar el prepago afectara las existencias de tus productos permanentemente, ¿Deseas continuar?", "Prepago", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

			if (dr == DialogResult.Yes)
			{
				DAO_Ventas dao_ventas = new DAO_Ventas();

				DAO_Apartado_mercancia dao_apartado = new DAO_Apartado_mercancia();
				
				dao_apartado.eliminar_apartado_prepago(prepago_id);
				long venta_id_prepago = dao_ventas.canjear_prepago(Convert.ToInt64(empleado_id), venta_id, detallado, monto, cliente_domicilio_id);

				if (venta_id_prepago > 0)
				{
					Ticket_venta ticket_venta = new Ticket_venta();
					ticket_venta.construccion_ticket(venta_id);
					ticket_venta.print();
					MessageBox.Show(this, "El prepago fue canjeado correctamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
					prepago_afectado = true;
					this.Close();
				}
			}
		}
	}
}
