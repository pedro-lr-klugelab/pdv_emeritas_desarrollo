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
	public partial class Editar_nombre_anaquel : Form
	{
		string nombre;
		long anaquel_id;
		public bool anaquel_actualizado = false;

		public Editar_nombre_anaquel(long anaquel_id, string nombre)
		{
			this.nombre = nombre;
			this.anaquel_id = anaquel_id;

			InitializeComponent();
		}

		private void btn_guardar_Click(object sender, EventArgs e)
		{
			actualizar_anaquel();
		}

		void actualizar_anaquel()
		{
			if (txt_nombre.Text.Trim().Length > 0)
			{
				DAO_Anaqueles dao = new DAO_Anaqueles();
				bool actualizo = dao.actualizar_anaquel(anaquel_id, txt_nombre.Text.ToUpper());

				if (actualizo)
				{
					anaquel_actualizado = true;
					MessageBox.Show(this, "Anaquel actualizado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
					this.Close();
				}
				else
				{
					MessageBox.Show(this, "Ocurrio un error al intentar actualizar el anaquel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void txt_nombre_KeyUp(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(txt_nombre.Text.Trim().Length > 0)
					{
						actualizar_anaquel();			
					}
				break;
				case 27:
					if(txt_nombre.Text.Trim().Length > 0)
					{
						txt_nombre.Text = "";
						txt_nombre.Focus();
					}
					else
					{
						this.Close();
					}
				break;
			}
		}

		private void Editar_nombre_anaquel_Shown(object sender, EventArgs e)
		{
			txt_nombre.Text = nombre;
			txt_nombre.Focus();
		}

	}
}
