using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.FORMS.ventas.ventas_mayoreo_revision
{
	public partial class Revision_productos : Form
	{
		DAO_Ventas_mayoreo dao_mayoreo = new DAO_Ventas_mayoreo();

		private long mayoreo_venta_id;
		private long empleado_id;
		public bool terminado = false;

		public Revision_productos(long mayoreo_venta_id, long empleado_id)
		{
			this.mayoreo_venta_id = mayoreo_venta_id;
			this.empleado_id = empleado_id;
			InitializeComponent();
		}

		private void get_productos_conflicto()
		{
			dgv_productos.DataSource = dao_mayoreo.get_productos_error(mayoreo_venta_id);
			dgv_productos.ClearSelection();

			foreach (DataGridViewRow row in dgv_productos.Rows)
			{
				long cantidad_capturada = Convert.ToInt64(row.Cells["cantidad"].Value);
				long cantidad_revision = Convert.ToInt64(row.Cells["cantidad_revision"].Value);

				if (cantidad_capturada > 0 && cantidad_revision == 0)
				{
					row.Cells["c_conflicto"].Value = "NO SE REVISO";
					//row.DefaultCellStyle.BackColor = Color.FromArgb(255, 218, 218);
					//rojo
				}
				else if (cantidad_capturada == 0 && cantidad_revision > 0)
				{
					//amarillo
					row.Cells["c_conflicto"].Value = "AJENO A LA CAPTURA";
					//row.DefaultCellStyle.BackColor = Color.FromArgb(251, 249, 203);
				}
				else if (cantidad_capturada > 0 && cantidad_revision > 0 && cantidad_capturada != cantidad_revision)
				{
					//morado
					long diferencia = Convert.ToInt64(row.Cells["diferencia"].Value);
					row.Cells["c_conflicto"].Value = (diferencia > 0) ? "EXCESO DE PRODUCTO" : "FALTO PRODUCTO";
					//row.DefaultCellStyle.BackColor = Color.FromArgb(239, 191, 242);
				}
			}
		}

		private void Revision_productos_Shown(object sender, EventArgs e)
		{
			get_productos_conflicto();
		}

		private void btn_finalizar_Click(object sender, EventArgs e)
		{
			if(chb_acepto.Checked)
			{
				DialogResult dr = MessageBox.Show(this, "Esta a punto de Finalizar con la revision de la venta a mayoreo, ¿Desea continuar?", "Finalizar revision", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

				if (dr == DialogResult.Yes)
				{
					var fialas_afectadas = dao_mayoreo.finalizar_verificacion(mayoreo_venta_id,empleado_id);

					if(fialas_afectadas > 0)
					{
						MessageBox.Show(this,"Revision terminada correctamente","Información",MessageBoxButtons.OK,MessageBoxIcon.Information);
						terminado = true;
						this.Close();
					}
					else
					{
						MessageBox.Show(this, "Ocurrio un error al intentar finalizar la revision, por favor notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}	
			}
			else
			{
				MessageBox.Show(this,"Por favor, confirma que has revisado esta información marcando la casilla en la parte superior","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}
	}
}
