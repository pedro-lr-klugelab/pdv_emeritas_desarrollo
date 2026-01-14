using Farmacontrol_PDV.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Farmacontrol_PDV.FORMS.ventas.cotizaciones
{
	public partial class Cotizaciones_pausadas : Form
	{
		public long cotizacion_id = 0;

		public Cotizaciones_pausadas()
		{
			InitializeComponent();
            buscar_cotizaciones_pausadas();
		}

        void buscar_cotizaciones_pausadas()
        {
            DAO_Cotizaciones dao_cotizaciones = new DAO_Cotizaciones();
            dgv_cotizaciones.DataSource = dao_cotizaciones.get_cotizaciones_pausadas();
            dgv_cotizaciones.ClearSelection();
        }

		private void Cotizaciones_pausadas_Load(object sender, EventArgs e)
		{

		}

		private void txt_filtro_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(txt_filtro.TextLength > 0)
					{
						(dgv_cotizaciones.DataSource as DataTable).DefaultView.RowFilter = string.Format("cotizacion_id LIKE '{0}%' OR cotizacion_id LIKE '% {0}%'", txt_filtro.Text);
					}
				break;
				case 27:
					if(txt_filtro.TextLength > 0)
					{
						txt_filtro.Text = "";
					}
					else
					{
						cotizacion_id = 0;
						this.Close();
					}
				break;
				case 40:
					if(dgv_cotizaciones.Rows.Count > 0)
					{
						dgv_cotizaciones.CurrentCell = dgv_cotizaciones.Rows[0].Cells["folio"];
						dgv_cotizaciones.Rows[0].Selected = true;
						dgv_cotizaciones.Focus();
					}
				break;
			}
		}

		private void txt_filtro_Enter(object sender, EventArgs e)
		{
			dgv_cotizaciones.ClearSelection();
		}

		private void dgv_cotizaciones_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 27:
					txt_filtro.Focus();
				break;
				case 13:
					cotizacion_id = Convert.ToInt64(dgv_cotizaciones.Rows[dgv_cotizaciones.SelectedRows[0].Index].Cells["folio"].Value);
					this.Close();
				break;
                case 46:
                    if(dgv_cotizaciones.SelectedRows.Count > 0)
                    {
                        DialogResult dr = MessageBox.Show(this,"¿Esta seguro de querer eliminar la cotización pausada?", "Información",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

                        if(dr == DialogResult.Yes)
                        {
                            long cotizacion_eliminar_id = Convert.ToInt64(dgv_cotizaciones.SelectedRows[0].Cells["folio"].Value);

                            DAO_Cotizaciones dao_cotizaciones = new DAO_Cotizaciones();
                            dao_cotizaciones.eliminar_cotizacion(cotizacion_eliminar_id);

                            buscar_cotizaciones_pausadas();
                            txt_filtro.Focus();
                        }
                    }
                break;
			}
		}

		private void txt_filtro_KeyUp(object sender, KeyEventArgs e)
		{
			if (txt_filtro.Text.Trim().Length > 0)
			{
				(dgv_cotizaciones.DataSource as DataTable).DefaultView.RowFilter = string.Format("cotizacion_id LIKE '{0}%' OR cotizacion_id LIKE '%{0}%'", txt_filtro.Text);
				dgv_cotizaciones.ClearSelection();
			}
            else
            {
                buscar_cotizaciones_pausadas();
            }
		}
	}
}
