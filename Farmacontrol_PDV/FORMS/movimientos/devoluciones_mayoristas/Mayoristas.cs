using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.FORMS.movimientos.devoluciones_mayoristas
{
	public partial class Mayoristas : Form
	{
		DAO_Mayoristas dao_mayoristas = new DAO_Mayoristas();
		public long? mayorista_id = null;

		public Mayoristas()
		{
			InitializeComponent();
		}

		private void Mayoristas_Shown(object sender, EventArgs e)
		{
			cbb_mayoristas.DataSource = dao_mayoristas.get_all_mayoristas(true);
			cbb_mayoristas.DisplayMember = "Value";
			cbb_mayoristas.ValueMember = "Key";
			cbb_mayoristas.Focus();
			cbb_mayoristas.DroppedDown = true;
		}

		private void cbb_mayoristas_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					mayorista_id = Convert.ToInt64(cbb_mayoristas.SelectedValue);
					this.Close();
				break;
			}
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btn_aceptar_Click(object sender, EventArgs e)
		{
			mayorista_id = Convert.ToInt64(cbb_mayoristas.SelectedValue);
			this.Close();
		}
	}
}
