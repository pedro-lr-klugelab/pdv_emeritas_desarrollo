using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.FORMS.catalogos.anaqueles
{
	public partial class Agregar_productos_anaquel : Form
	{
		long anaquel_id;
		public Agregar_productos_anaquel(long anaquel_id)
		{
			this.anaquel_id = anaquel_id;
			InitializeComponent();
			get_detallado_anaqueles();
		}

		void get_detallado_anaqueles()
		{
			DAO_Anaqueles dao_anaqueles = new DAO_Anaqueles();
			dgv_articulos.DataSource = dao_anaqueles.get_detallado_anaqueles(anaquel_id);
		}

		private void txt_amecop_KeyUp(object sender, KeyEventArgs e)
		{
			
		}

		private void dgv_articulos_KeyUp(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt64(e.KeyCode);

			switch(keycode)
			{
				case 46:
					if(dgv_articulos.SelectedRows.Count > 0)
					{
						DAO_Anaqueles dao = new DAO_Anaqueles();
						dao.eliminar_articulo_detallado_anaquel(Convert.ToInt64(dgv_articulos.SelectedRows[0].Cells["articulo_id"].Value));
						get_detallado_anaqueles();
					}
				break;
			}
		}

		private void txt_amecop_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt64(e.KeyCode);

			switch (keycode)
			{
				case 13:
					if (txt_amecop.Text.Trim().Length > 0)
					{
						DAO_Articulos dao = new DAO_Articulos();
						var articulo = dao.get_articulo(txt_amecop.Text);

						if (articulo.Articulo_id != null)
						{
							DAO_Anaqueles dao_anaqueles = new DAO_Anaqueles();
							dgv_articulos.DataSource = dao_anaqueles.registrar_articulo_anaquel(anaquel_id, (long)articulo.Articulo_id);
							txt_amecop.Text = "";
							txt_amecop.Focus();
							dgv_articulos.ClearSelection();
						}
						else
						{
							MessageBox.Show(this, "Codigo de producto no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
					break;
			}	
		}

		private void btn_cerrar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

        private void txt_amecop_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
	}
}
