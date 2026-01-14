using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.FORMS.comunes
{
	public partial class Sin_codigo_postal : Form
	{
		Dictionary<string,string> diccionario_colonias = new Dictionary<string,string>();
		public long? asentamiento_id = null;

		public Sin_codigo_postal()
		{
			InitializeComponent();
		}

		private void Sin_codigo_postal_Shown(object sender, EventArgs e)
		{
			cbb_estado.DroppedDown = true;
			cbb_estado.Focus();
		}

		private void cbb_estado_SelectionChangeCommitted(object sender, EventArgs e)
		{
			cbb_ciudad.Items.Clear();

			cbb_nombre.Items.Clear();
			cbb_nombre.Enabled = false;


			cbb_estado.Enabled = false;
			cbb_ciudad.Enabled = true;
			cbb_ciudad.DroppedDown = true;
			cbb_ciudad.Focus();
		}

		private void cbb_ciudad_SelectionChangeCommitted(object sender, EventArgs e)
		{
			cbb_nombre.Items.Clear();
			cbb_nombre.Enabled = false;


			cbb_estado.Enabled = false;
			cbb_ciudad.Enabled = false;

			cbb_nombre.Enabled = true;


			List<string> lista_colonias = new List<string>();

			foreach (var pair in diccionario_colonias)
			{
				lista_colonias.Add(pair.Key.ToString());
			}

			cbb_nombre.DataSource = lista_colonias;
			cbb_nombre.DroppedDown = true;
			cbb_nombre.Focus();
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void cbb_nombre_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 27:
					cbb_nombre.Enabled = false;
					cbb_ciudad.Enabled = true;
					cbb_ciudad.DroppedDown = true;
					cbb_ciudad.Focus();
					cbb_nombre.DataSource =  null;
				break;
			}
		}

		private void cbb_ciudad_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 27:
					cbb_ciudad.Enabled = false;
					cbb_ciudad.DataSource = null;

					cbb_estado.Enabled = true;
					cbb_estado.DroppedDown = true;
					cbb_estado.Focus();
				break;
			}
		}

		private void cbb_nombre_SelectionChangeCommitted(object sender, EventArgs e)
		{
			btn_aceptar.Focus();
		}

		private void btn_aceptar_Click(object sender, EventArgs e)
		{
			asentamiento_id = Convert.ToInt64(diccionario_colonias[cbb_nombre.SelectedItem.ToString()]);
			this.Close();
		}
	}
}
