using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.FORMS.ventas.ventas
{
	public partial class Importar_cotizacion : Form
	{
		DAO_Cotizaciones dao_cotizaciones = new DAO_Cotizaciones();
		DAO_Ventas dao_ventas = new DAO_Ventas();
		private long venta_id;

		public Importar_cotizacion(long venta_id)
		{
			this.venta_id = venta_id;
			InitializeComponent();
			dgv_cotizaciones_ventas.ClearSelection();
		}

		private void Importar_cotizacion_Load(object sender, EventArgs e)
		{
			dgv_cotizaciones_ventas.DataSource = dao_cotizaciones.get_cotizaciones_venta();
		}

		private void txt_filtro_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(dgv_cotizaciones_ventas.Rows.Count > 0)
					{
                        if(txt_filtro.Text.Trim().Length > 0)
                        {
                            txt_filtro.Text = Convert.ToInt64(txt_filtro.Text).ToString();
                            (dgv_cotizaciones_ventas.DataSource as DataTable).DefaultView.RowFilter = string.Format("cotizacion_id LIKE '{0}%' OR cotizacion_id LIKE '% {0}%'", Convert.ToInt64(txt_filtro.Text));
                            dgv_cotizaciones_ventas.ClearSelection();
                        }
					}
				break;
				case 40:
                    if(dgv_cotizaciones_ventas.Rows.Count > 0)
                    {
                        dgv_cotizaciones_ventas.CurrentCell = dgv_cotizaciones_ventas.Rows[0].Cells["cotizacion_id"];
                        dgv_cotizaciones_ventas.Rows[0].Selected = true;
                        dgv_cotizaciones_ventas.Focus();
                    }
				break;
				case 27:
					if(txt_filtro.TextLength > 0)
						txt_filtro.Text = "";
						else
							this.Close();
				break;
			}
		}

		private void txt_filtro_Enter(object sender, EventArgs e)
		{
			dgv_cotizaciones_ventas.ClearSelection();
		}

		private void dgv_cotizaciones_ventas_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
                case 46:
                    if (dgv_cotizaciones_ventas.SelectedRows.Count > 0) {
                        eliminar_cotizacion();
                    }                    
                break;
				case 13:
                if (dgv_cotizaciones_ventas.SelectedRows.Count > 0)
                {
                    importar_cotizacion();
                }
				break;
				case 27:
					txt_filtro.Focus();
				break;
			}
		}

        public void eliminar_cotizacion()
        {
            DialogResult dr = MessageBox.Show(this, "¿Esta seguro de querer eliminar esta cotizacion?", "Eliminar Cotizacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                dgv_cotizaciones_ventas.DataSource = dao_cotizaciones.eliminar_cotizacion(Convert.ToInt64(dgv_cotizaciones_ventas.SelectedRows[0].Cells["cotizacion_id"].Value));
                
            }
        }

		public void importar_cotizacion()
		{
            long cotizacion_id = Convert.ToInt64(dgv_cotizaciones_ventas.SelectedRows[0].Cells["cotizacion_id"].Value);

            if(dao_cotizaciones.es_cotizacion_terminada(cotizacion_id))
            {
                if(!dao_cotizaciones.es_cotizacion_venta(cotizacion_id))
                {
                    DialogResult dr = MessageBox.Show(this, "¿Esta seguro de querer importar esta cotizacion?", "Importar Cotizacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        dao_ventas.importar_cotizacion(venta_id, Convert.ToInt64(dgv_cotizaciones_ventas.SelectedRows[0].Cells["cotizacion_id"].Value));
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(this,"Esta cotizacion ya ha sido asignada a una venta","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(this,"Esta cotizacion ha sido reabierta por alguna otra terminal","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
		}

		private void txt_filtro_KeyUp(object sender, KeyEventArgs e)
		{
			if (txt_filtro.Text.Trim().Length > 0)
			{
				(dgv_cotizaciones_ventas.DataSource as DataTable).DefaultView.RowFilter = string.Format("cotizacion_id LIKE '{0}%' OR cotizacion_id LIKE '% {0}%'", txt_filtro.Text);
				dgv_cotizaciones_ventas.ClearSelection();
			}
		}

		private void Importar_cotizacion_Shown(object sender, EventArgs e)
		{
			dgv_cotizaciones_ventas.ClearSelection();
		}
	}
}
