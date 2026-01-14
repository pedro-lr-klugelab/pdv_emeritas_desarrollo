using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.CLASSES;

namespace Farmacontrol_PDV.FORMS.movimientos.traspasos
{
	public partial class Resolucion_conflictos : Form
	{
		DAO_Traspasos dao_traspasos = new DAO_Traspasos();
		private long traspaso_id;
		public bool conflictos_solucionados = false;

		public Resolucion_conflictos(long traspaso_id)
		{
			this.traspaso_id = traspaso_id;
			InitializeComponent();
		}

		public void load_productos_conflicto()
		{
			var data_trapaso = dao_traspasos.get_productos_conflictos(traspaso_id);
			dgv_conciliacion.DataSource = data_trapaso;
			dgv_conciliacion.ClearSelection();
		}

		private void Resolucion_conflictos_Load(object sender, EventArgs e)
		{
			load_productos_conflicto();
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btn_terminar_traspaso_Click(object sender, EventArgs e)
		{
			bool sin_seleccionar = false;
			Dictionary<long,string> lista_acciones = new Dictionary<long,string>();

			foreach(DataGridViewRow row in dgv_conciliacion.Rows)
			{
				lista_acciones.Add(Convert.ToInt64(row.Cells["detallado_traspaso_id"].Value), row.Cells["c_solucion"].Value.ToString());
			}

			if(sin_seleccionar)
			{
				MessageBox.Show(this,"Tienes que solucionar todos los conflictos antes de terminar el traspaso","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			else
			{
				DialogResult dr = MessageBox.Show(this,"Estas seguro de querer terminar este traspaso","Terminar traspaso",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

				if(dr == DialogResult.Yes)
				{
					int acciones_afectadas = dao_traspasos.set_resolucion_conflictos(lista_acciones);

					if (acciones_afectadas > 0)
					{
						conflictos_solucionados = true;
						this.Close();
					}
					else
					{
						MessageBox.Show(this, "Ocurrio un error al intentar efectuar la resolucion de conflictos, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}	
				}
			}
		}
	}
}
