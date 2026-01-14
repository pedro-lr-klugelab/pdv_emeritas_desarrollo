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

namespace Farmacontrol_PDV.FORMS.movimientos.devoluciones_mayoristas
{
	public partial class Productos_devolucion : Form
	{
		private long devolucion_id;
		private DataTable data_entradas;
		private long entrada_id;
		private long empleado_id;
		private string fecha;
		private string folio_devolucion;
		public bool terminado = false;

		public Productos_devolucion(long devolucion_id, long entrada_id, DataTable data_entradas, long empleado_id, string fecha, string folio_devolucion)
		{
			this.folio_devolucion = folio_devolucion;
			this.fecha = fecha;
			this.empleado_id = empleado_id;
			this.entrada_id = entrada_id;
			this.devolucion_id = devolucion_id;
			this.data_entradas = data_entradas;
			InitializeComponent();
			dgv_entradas.DataSource = this.data_entradas;
			dgv_entradas.ClearSelection();
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btn_continuar_Click(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show(this, "Si terminas la devolución ya no podras hacerle ningun tipo de cambio, ¿Deseas continuar?", "Terminar Devolución", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

			if (dr == DialogResult.Yes)
			{
				DAO_Devoluciones dao_devoluciones =  new DAO_Devoluciones();
				DAO_Entradas dao_entradas = new DAO_Entradas();

				var filas_afectadas = dao_devoluciones.terminar_devolucion(devolucion_id, empleado_id, fecha,folio_devolucion);

				if (filas_afectadas > 0)
				{
					MessageBox.Show(this, "Devolucion terminada correctamente", "Devolución", MessageBoxButtons.OK, MessageBoxIcon.Information);
					Devoluciones_mayorista ticket = new Devoluciones_mayorista();
					ticket.construccion_ticket(devolucion_id);
					ticket.print();
					terminado = true;
				}
				else
				{
					MessageBox.Show(this, "Ocurrio un error al intentar terminar la devolucion, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}	

				this.Close();
			}
		}

		private void Productos_devolucion_Shown(object sender, EventArgs e)
		{
			dgv_entradas.ClearSelection();
		}

		private void btn_continuar_Enter(object sender, EventArgs e)
		{
			dgv_entradas.ClearSelection();
		}

        private void dgv_entradas_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch(keycode)
            {
                case 67:
                    if (e.Control)
                    {
                        Clipboard.SetText(dgv_entradas.SelectedRows[0].Cells["c_amecop"].Value.ToString());
                    }
                break;
            }
        }
	}
}
