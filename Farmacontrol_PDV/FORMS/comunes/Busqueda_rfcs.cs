using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.FORMS.catalogos.catalogo_rfc;
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
	public partial class Busqueda_rfcs : Form
	{
		public string rfc_registro_id = "";

        public bool uso_factura;

		public Busqueda_rfcs(bool uso_factura = false)
		{
            this.uso_factura = uso_factura;
			InitializeComponent();
		}

		public void buscar_rfc()
		{
			DAO.DAO_Rfcs dao_rfcs = new DAO.DAO_Rfcs();
			dgv_rfc.DataSource = dao_rfcs.get_rfc_registros(txt_rfc.Text);
			dgv_rfc.ClearSelection();
		}

		private void txt_rfc_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					buscar_rfc();
				break;
				case 27:
					if(txt_rfc.TextLength > 0)
					{
						txt_rfc.Text = "";
					}
					else
					{
						rfc_registro_id = "";
						this.Close();
					}
				break;
				case 40:
					if(dgv_rfc.RowCount > 0)
						dgv_rfc.CurrentCell = dgv_rfc.Rows[0].Cells["rfc"];
						dgv_rfc.Rows[0].Selected = true;
						dgv_rfc.Focus();
				break;
			}
		}

		private void txt_rfc_Enter(object sender, EventArgs e)
		{
			dgv_rfc.ClearSelection();
		}

		private void dgv_rfc_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					rfc_registro_id = dgv_rfc.SelectedRows[0].Cells["columna_rfc_registro_id"].Value.ToString();
					this.Close();
				break;
				case 27:
					txt_rfc.Focus();
				break;
			}
		}

        private void btn_registrar_nuevo_rfc_Click(object sender, EventArgs e)
        {
            Editar_rfc editar_rfc = new Editar_rfc();
            editar_rfc.ShowDialog();

            if(editar_rfc.rfc_registro_id != "")
            {
                if(uso_factura)
                {
                    rfc_registro_id = editar_rfc.rfc_registro_id;
                    this.Close();
                }
                else
                {
                    DAO_Rfcs dao_rfcs = new DAO_Rfcs();
                    var rfc_data = dao_rfcs.get_data_rfc(editar_rfc.rfc_registro_id);
                    txt_rfc.Text = rfc_data.razon_social;

                    buscar_rfc();
                }
            }
        }
	}
}
