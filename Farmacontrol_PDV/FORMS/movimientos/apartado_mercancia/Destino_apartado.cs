using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.FORMS.movimientos.apartado_mercancia
{
	public partial class Destino_apartado : Form
	{
		public int? sucursal_id = null;
		DAO_Sucursales dao_sucursales = new DAO_Sucursales();
		Dictionary<string, int> sucursales_info = new Dictionary<string, int>();
		public string tipo = "";

		public Destino_apartado()
		{
			InitializeComponent();
		}

		public void get_sucursales()
		{
			var sucursales = dao_sucursales.get_all_sucursales();

			foreach (DataRow row in sucursales.Rows)
			{
				if (!sucursales_info.ContainsKey(row["nombre"].ToString()))
				{
					int sucursal_id = Convert.ToInt32(Config_helper.get_config_local("sucursal_id"));

					if (sucursal_id != Convert.ToInt32(row["sucursal_id"]))
					{
						sucursales_info.Add(row["nombre"].ToString(), Convert.ToInt32(row["sucursal_id"]));
						cbb_destino.Items.Add(row["nombre"].ToString());
					}
				}
			}

			sucursales_info.Add("CAMBIO FISICO", 0);
			cbb_destino.Items.Add("CAMBIO FISICO");

			sucursales_info.Add("MERMA", 0);
			cbb_destino.Items.Add("MERMA");

			cbb_destino.SelectedIndex = 0;
			cbb_destino.Focus();
			cbb_destino.DroppedDown = true;

		}

		private void Destino_apartado_Shown(object sender, EventArgs e)
		{
			get_sucursales();	
		}

		private void cbb_destino_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 13:
					if (cbb_destino.SelectedIndex != -1)
					{
						sucursal_id = sucursales_info[cbb_destino.SelectedItem.ToString()];
						
						if(sucursal_id == 0)
						{
							tipo = (cbb_destino.SelectedItem.ToString().Equals("CAMBIO FISICO")) ? "CAMBIO_FISICO" : "MERMA";
						}

						this.Close();
					}
					break;
				case 27:
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
			if (cbb_destino.SelectedIndex != -1)
			{
				sucursal_id = sucursales_info[cbb_destino.SelectedItem.ToString()];

				if (sucursal_id == 0)
				{
					tipo = (cbb_destino.SelectedItem.ToString().Equals("CAMBIO FISICO")) ? "CAMBIO_FISICO" : "MERMA";
				}

				this.Close();
			}
		}
	}
}
