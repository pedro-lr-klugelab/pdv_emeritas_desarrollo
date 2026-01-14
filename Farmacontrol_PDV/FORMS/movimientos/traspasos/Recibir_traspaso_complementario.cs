using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;

namespace Farmacontrol_PDV.FORMS.movimientos.traspasos
{
	public partial class Recibir_traspaso_complementario : Form
	{
		public Recibir_traspaso_complementario()
		{
			InitializeComponent();
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void txt_folio_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 27:
					if(txt_folio.Text.Trim().Length > 0)
					{
						txt_folio.Text = "";
					}
					else
					{
						this.Close();
					}
				break;
				case 13:
					if (txt_folio.Text.Trim().Length > 0)
					{
						validar_folio();	
					}
				break;
			}
		}

		public void validar_folio()
		{
			DAO_Sucursales dao_sucursales = new DAO_Sucursales();

			string [] split_folio = txt_folio.Text.Split('$');

			int sucursal_origen = Convert.ToInt32(split_folio[1]);
			int sucursal_destino = Convert.ToInt32(split_folio[2]);
			string uuid = split_folio[3];

			int mi_sucursal = Convert.ToInt32(Config_helper.get_config_local("sucursal_id"));
			DTO_Sucursal sucursal_data = dao_sucursales.get_sucursal_data(sucursal_origen);

			if(mi_sucursal == sucursal_destino)
			{
				if (Red_helper.checa_online(sucursal_data.ip_sucursal) == false)
				{
					MessageBox.Show(this,String.Format("La sucursal {0} ({1}) no responde, intente de nuevo más tarde.", sucursal_data.nombre, sucursal_data.ip_sucursal),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					return;
				}

				if (Red_helper.checa_rest(sucursal_data.ip_sucursal) == false)
				{
					MessageBox.Show(this, String.Format("La sucursal {0} ({1}) no responde la petición, intente de nuevo más tarde.", sucursal_data.nombre, sucursal_data.ip_sucursal), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				DAO_Traspasos dao_traspasos = new DAO_Traspasos();
				var result = dao_traspasos.importar_traspaso_complementario(txt_folio.Text);

				if(result.status)
				{
					MessageBox.Show(this,result.informacion,"Traspaso complementario",MessageBoxButtons.OK,MessageBoxIcon.Information);
					this.Close();
				}
				else
				{
					MessageBox.Show(this, result.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txt_folio.SelectAll();
					txt_folio.Focus();
				}
			}
			else
			{
				var dto_sucursal = dao_sucursales.get_sucursal_data(sucursal_destino);
				MessageBox.Show(this,"Este traspaso pertenece a la sucursal "+dto_sucursal.nombre,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				txt_folio.Text = "";
				txt_folio.Focus();
			}
		}
	}
}
