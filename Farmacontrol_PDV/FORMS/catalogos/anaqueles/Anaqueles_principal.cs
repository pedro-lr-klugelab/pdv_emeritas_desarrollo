using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.FORMS.comunes;

namespace Farmacontrol_PDV.FORMS.catalogos.anaqueles
{
	public partial class Anaqueles_principal : Form
	{
		public Anaqueles_principal()
		{
			InitializeComponent();
			get_anaqueles();
		}

		void get_anaqueles()
		{
			DAO_Anaqueles dao_anaqueles = new DAO_Anaqueles();
			dgv_anaqueles.DataSource = dao_anaqueles.get_anaqueles();
			dgv_anaqueles.ClearSelection();
		}

		private void btn_agregar_Click(object sender, EventArgs e)
		{
			registrar_anaquel();
		}

		void registrar_anaquel()
		{
			DAO_Anaqueles dao = new DAO_Anaqueles();

			bool existe_anaquel = dao.existe_anaquel(txt_nombre.Text);

			if (existe_anaquel)
			{
				MessageBox.Show(this, "El nombre de este anaquel ya se encuentra registrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				txt_nombre.Focus();
			}
			else
			{
				if(txt_nombre.Text.Trim().Length > 0)
				{
					dgv_anaqueles.DataSource = dao.registrar_anaquel(txt_nombre.Text.ToUpper(), 1);
					dgv_anaqueles.ClearSelection();

					//MessageBox.Show(this, "El anaquel \"" + txt_nombre.Text.ToUpper() + "\" fue registrado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

					txt_nombre.Text = "";
					txt_nombre.Focus();
				}
				else
				{
					MessageBox.Show(this,"Es necesario definir el nombre del anaquel","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					txt_nombre.Text = "";
					txt_nombre.Focus();
				}
			}
		}

		private void txt_nombre_KeyUp(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToUInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(txt_nombre.Text.Trim().Length > 0)
					{
						registrar_anaquel();
					}	
				break;
				case 27:
					txt_nombre.Text = "";
					txt_nombre.Focus();
				break;
			}
		}

		private void btn_editar_Click(object sender, EventArgs e)
		{
			if(dgv_anaqueles.SelectedRows.Count > 0)
			{
				long anaquel_id = Convert.ToInt64(dgv_anaqueles.SelectedRows[0].Cells["anaquel_id"].Value);
				string nombre = dgv_anaqueles.SelectedRows[0].Cells["nombre"].Value.ToString();
				Editar_nombre_anaquel edit = new Editar_nombre_anaquel(anaquel_id, nombre);
				edit.ShowDialog();

				if (edit.anaquel_actualizado)
				{
					get_anaqueles();
				}
			}
			else
			{
				MessageBox.Show(this,"Es necesario seleccionar el anaquel a editar","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		private void btn_eliminar_Click(object sender, EventArgs e)
		{
			if(dgv_anaqueles.SelectedRows.Count > 0)
			{
				long anaquel_id = Convert.ToInt64(dgv_anaqueles.SelectedRows[0].Cells["anaquel_id"].Value);
				if(Convert.ToInt64(dgv_anaqueles.SelectedRows[0].Cells["numero_productos"].Value) > 0)
				{
					DialogResult dr = MessageBox.Show(this, "Este anaquel contiene productos, al eliminarlo se desasociaran los mismos. ¿Desea continuar?","Eliminar anaquel",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

					if(dr == DialogResult.Yes)
					{
						DAO_Anaqueles dao = new DAO_Anaqueles();
						dgv_anaqueles.DataSource =  dao.eliminar_anaquel(anaquel_id);
						MessageBox.Show(this, "Anaquel eliminado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
				else
				{
					DialogResult dr = MessageBox.Show(this, "Esta a punto de eliminar permanentemente el anaquel, ¿Desea continuar?", "Eliminar anaquel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

					if (dr == DialogResult.Yes)
					{
						DAO_Anaqueles dao = new DAO_Anaqueles();
						dgv_anaqueles.DataSource = dao.eliminar_anaquel(anaquel_id);
						MessageBox.Show(this,"Anaquel eliminado correctamente","Información",MessageBoxButtons.OK,MessageBoxIcon.Information);
					}
				}
			}
		}

		private void btn_agregar_productos_Click(object sender, EventArgs e)
		{
			if(dgv_anaqueles.SelectedRows.Count > 0)
			{
				long anaquel_id = Convert.ToInt64(dgv_anaqueles.SelectedRows[0].Cells["anaquel_id"].Value);
				Agregar_productos_anaquel add_articulo = new Agregar_productos_anaquel(anaquel_id);
				add_articulo.ShowDialog();
			}
			else
			{
				MessageBox.Show(this,"Es necesario seleccionar el anaquel","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		private void Anaqueles_principal_Load(object sender, EventArgs e)
		{
		}

		private void Anaqueles_principal_Shown(object sender, EventArgs e)
		{
			DAO_Login dao = new DAO_Login();
			bool es_encargado = dao.empleado_es_encargado((long)Principal.empleado_id);

			if (es_encargado == false)
			{
				Login_form login = new Login_form();
				login.ShowDialog();

				if (login.empleado_id != null)
				{
					es_encargado = dao.empleado_es_encargado((long)login.empleado_id);

					if (es_encargado == false)
					{
						MessageBox.Show(this,"Solo los encargados pueden entrar a este módulo","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
						this.Close();
					}
				}
				else
				{
					this.Close();
				}
			}
		}
	}
}
