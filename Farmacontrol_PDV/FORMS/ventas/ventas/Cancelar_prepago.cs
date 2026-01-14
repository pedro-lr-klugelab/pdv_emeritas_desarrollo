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
	public partial class Cancelar_prepago : Form
	{
		List<DTO_Prepago_parcial_entregado> detallado_venta = new List<DTO_Prepago_parcial_entregado>();
		public bool prepago_afectado = false;
		long prepago_id;

		public Cancelar_prepago(long prepago_id, List<DTO_Prepago_parcial_entregado> detallado_venta)
		{
			this.prepago_id = prepago_id;
			this.detallado_venta = detallado_venta;
			InitializeComponent();
		}

		private void Cancelar_prepago_Shown(object sender, EventArgs e)
		{	
			dgv_entrega_parcial.DataSource = detallado_venta;
			dgv_entrega_parcial.ClearSelection();
		}

		private void dgv_entrega_parcial_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			Log_error.log(e.Exception);
		}

		private void dgv_entrega_parcial_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			long cantidad = Convert.ToInt64(dgv_entrega_parcial.Rows[e.RowIndex].Cells["cantidad"].Value);
			BindingList<string> contenido = new BindingList<string>();

			for(long x=0; x <= cantidad; x++)
			{
				contenido.Add(x.ToString());
			}

			DataGridViewComboBoxCell box = dgv_entrega_parcial.Rows[e.RowIndex].Cells["cantidad_conservar"] as DataGridViewComboBoxCell;
			dgv_entrega_parcial.Rows[e.RowIndex].Cells["c_cantidad_conservar"].Value = 0;
			box.DataSource = contenido;
			box.Value = "0";
			dgv_entrega_parcial.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dgv_entrega_parcial_EditingControlShowing);
		}

		private void dgv_entrega_parcial_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			ComboBox combo = e.Control as ComboBox;
			if (combo != null)
			{
				combo.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
				combo.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
			}
		}

		private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				ComboBox cb = (ComboBox)sender;

				string a_num = cb.Text.Trim();
				long numero = 0;
				bool es_numero = long.TryParse(a_num,out numero);

				if(es_numero)
				{	
					numero = Convert.ToInt64(a_num);
					dgv_entrega_parcial.SelectedRows[0].Cells["c_cantidad_conservar"].Value = numero;
				}
			}
			catch(Exception ex){
				Log_error.log(ex);
			}
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btn_procesar_cancelacion_Click(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show(this, "Si continua, ya no podra realizar ningun cambio, ¿Desea continuar?","Procesar cancelación",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
			if(dr == DialogResult.Yes)
			{
				List<DTO_Prepago_parcial_entregado> det_prepago = new List<DTO_Prepago_parcial_entregado>();
				List<DTO_Prepago_parcial_entregado> det_prepago_datagrid = dgv_entrega_parcial.DataSource as List<DTO_Prepago_parcial_entregado>;

				foreach(DTO_Prepago_parcial_entregado det in det_prepago_datagrid)
				{
					if(det.cantidad_conservar > 0)
					{
						det_prepago.Add(det);
					}
				}

				DAO_Prepago dao_prepago = new DAO_Prepago();
				
				if(dao_prepago.cancelar_prepago(prepago_id,det_prepago))
				{
					MessageBox.Show(this,"Prepago cancelado correctamente","Información",MessageBoxButtons.OK,MessageBoxIcon.Information);
					prepago_afectado = true;
					this.Close();
				}
				else
				{
					MessageBox.Show(this, "Ocurrio un error al intentar cancelar el prepago, notifique a su adminstrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
