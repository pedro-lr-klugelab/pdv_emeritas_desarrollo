using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Farmacontrol_PDV.FORMS.comunes
{
	public partial class Registro_terminal : Form
	{
		public bool reiniciar = false;
		public Registro_terminal()
		{
			InitializeComponent();
		}

		private void txt_uuid_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					string uuid = txt_uuid.Text.Trim();

					if(uuid.Length > 0 && uuid.Length == 36)
					{
						Properties.Configuracion.Default.uuid_terminal = uuid;
						Properties.Configuracion.Default.Save();
						reiniciar = true;
						this.Close();
					}
					else
					{
						MessageBox.Show(this,"Codigo UUID invalido, verifique!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					}
				break;
				case 27:
					this.Close();
				break;
			}
		}
	}
}
