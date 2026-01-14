using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.FORMS.ventas.cotizar_formula
{
	public partial class Busqueda_productos_materias_primas : Form
	{
		public long? select_articulo_id = null;
		public long? select_materia_prima_id = null;

		public Busqueda_productos_materias_primas()
		{
			InitializeComponent();
		}

		private void txt_busqueda_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 40:
					if(dgv_productos_materias.Rows.Count > 0)
					{
						dgv_productos_materias.Focus();
						dgv_productos_materias.CurrentCell = dgv_productos_materias.Rows[0].Cells["nombre"];
						dgv_productos_materias.Rows[0].Selected = true;
					}
				break;
				case 13:
					if(txt_busqueda.Text.Trim().Length > 0)
					{
						buscar();	
					}
				break;
				case 27:
					if(txt_busqueda.Text.Trim().Length > 0)
					{
						txt_busqueda.Text = "";
					}
					else
					{
						this.Close();
					}
				break;
			}
		}

		void buscar()
		{
			DAO_Articulos dao_articulos = new DAO_Articulos();
			dgv_productos_materias.DataSource = dao_articulos.get_busqueda_productos_materias_primas(txt_busqueda.Text);
			dgv_productos_materias.ClearSelection();
		}

		private void dgv_productos_materias_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(dgv_productos_materias.SelectedRows.Count > 0)
					{
						if (!dgv_productos_materias.SelectedRows[0].Cells["articulo_id"].Value.ToString().Equals(""))
						{
							select_articulo_id = Convert.ToInt64(dgv_productos_materias.SelectedRows[0].Cells["articulo_id"].Value);
						}

						if (!dgv_productos_materias.SelectedRows[0].Cells["materia_prima_id"].Value.ToString().Equals(""))
						{
							select_materia_prima_id = Convert.ToInt64(dgv_productos_materias.SelectedRows[0].Cells["materia_prima_id"].Value);
						}

						this.Close();
					}
				break;
				case 27:
					txt_busqueda.Focus();
					dgv_productos_materias.ClearSelection();
				break;
			}
		}

		private void txt_busqueda_Leave(object sender, EventArgs e)
		{
			dgv_productos_materias.ClearSelection();
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btn_aceptar_Click(object sender, EventArgs e)
		{
			if(dgv_productos_materias.SelectedRows.Count > 0)
			{
				if(!dgv_productos_materias.SelectedRows[0].Cells["articulo_id"].Value.ToString().Equals(""))
				{
					select_articulo_id = Convert.ToInt64(dgv_productos_materias.SelectedRows[0].Cells["articulo_id"].Value);
				}

				if (!dgv_productos_materias.SelectedRows[0].Cells["materia_prima_id"].Value.ToString().Equals(""))
				{
					select_materia_prima_id = Convert.ToInt64(dgv_productos_materias.SelectedRows[0].Cells["materia_prima_id"].Value);
				}

				this.Close();
			}
		}
	}
}
