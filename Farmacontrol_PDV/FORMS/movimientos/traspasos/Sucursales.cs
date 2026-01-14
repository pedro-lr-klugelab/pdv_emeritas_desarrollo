using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.FORMS.movimientos.traspasos
{
	public partial class Sucursales : Form
	{
		public int? sucursal_id = null;
		DAO_Sucursales dao_sucursales = new DAO_Sucursales();
		Dictionary<string,int> sucursales_info = new Dictionary<string,int>();

		public Sucursales()
		{
			InitializeComponent();
		}

		public void get_sucursales()
		{
			try
			{
				var sucursales = dao_sucursales.get_all_sucursales();

                int sucursal_id = Convert.ToInt32(Config_helper.get_config_local("sucursal_id"));

				foreach (DataRow row in sucursales.Rows)
				{
					if (!sucursales_info.ContainsKey(row["nombre"].ToString()))
					{
						//int sucursal_id = Convert.ToInt32(Config_helper.get_config_local("sucursal_id"));

                        if (sucursal_id != Convert.ToInt32(row["sucursal_id"]))
                        {
                            sucursales_info.Add(row["nombre"].ToString(), Convert.ToInt32(row["sucursal_id"]));
                            cbb_sucursales.Items.Add(row["nombre"].ToString());
                        }
                       
					}
				}

				if(cbb_sucursales.Items.Count > 0)
				{
					cbb_sucursales.SelectedIndex = 0;
					cbb_sucursales.DroppedDown = true;
					cbb_sucursales.Focus();
				}
				else
				{
					MessageBox.Show(this,"No se encontro ninguna sucursal destino, notifique a su administrador","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					this.Close();
				}
			}
			catch(Exception){}
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Sucursales_Shown(object sender, EventArgs e)
		{
			get_sucursales();
		}

		private void cbb_sucursales_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(cbb_sucursales.SelectedIndex != -1)
					{
						sucursal_id = sucursales_info[cbb_sucursales.SelectedItem.ToString()];
						this.Close();
					}
				break;
				case 27:
					this.Close();
				break;
			}
		}

		private void btn_aceptar_Click(object sender, EventArgs e)
		{
			if (cbb_sucursales.SelectedIndex != -1)
			{
				sucursal_id = sucursales_info[cbb_sucursales.SelectedItem.ToString()];
				this.Close();
			}
		}

        private void cbb_sucursales_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
	}
}
