using Farmacontrol_PDV.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farmacontrol_PDV.FORMS.ventas.cotizaciones
{
    public partial class Reabrir_cotizaciones : Form
    {
        public long? cotizacion_id = null;

        public Reabrir_cotizaciones()
        {
            InitializeComponent();
        }

        private void Reabrir_cotizaciones_Load(object sender, EventArgs e)
        {
            DAO_Cotizaciones dao_cotizaciones = new DAO_Cotizaciones();
            dgv_cotizaciones.DataSource = dao_cotizaciones.get_cotizaciones_venta();
            dgv_cotizaciones.ClearSelection();
        }

        private void txt_filtro_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch(keycode)
            {
                case 13:
                    if (txt_filtro.TextLength > 0)
                    {
                        (dgv_cotizaciones.DataSource as DataTable).DefaultView.RowFilter = string.Format("cotizacion_id LIKE '{0}%' OR cotizacion_id LIKE '% {0}%'", txt_filtro.Text);
                    }
                break;
                case 27:
                    if (txt_filtro.TextLength > 0)
                    {
                        txt_filtro.Text = "";
                    }
                    else
                    {
                        cotizacion_id = null;
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

        private void dgv_cotizaciones_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch(keycode)
            {
                case 27:
                    txt_filtro.Focus();
                    dgv_cotizaciones.ClearSelection();
                break;
                case 13:
                    if(dgv_cotizaciones.SelectedRows.Count > 0)
                    {
                        long cotizacion_id = Convert.ToInt64(dgv_cotizaciones.SelectedRows[0].Cells["folio"].Value);

                        DAO_Cotizaciones dao = new DAO_Cotizaciones();

                        if(dao.es_cotizacion_terminada(cotizacion_id))
                        {
                            if(!dao.es_cotizacion_venta(cotizacion_id))
                            {
                                this.cotizacion_id = cotizacion_id;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show(this, "Esta cotizacion ya forma parte de una venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show(this,"Esta cotizacion ha sido reabierta por otra terminal","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                    }
                break;
            }
        }
    }
}
