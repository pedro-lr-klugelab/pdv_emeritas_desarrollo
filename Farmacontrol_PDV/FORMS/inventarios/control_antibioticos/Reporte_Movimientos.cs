using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.FORMS.inventarios.control_antibioticos
{
    public partial class Reporte_Movimientos : Form
    {
        DAO_Antibioticos dao_ant = new DAO_Antibioticos();

        public string fecha_ini;
        public string fecha_fin;

        public Reporte_Movimientos()
        {
            InitializeComponent();
            DateTime result = DateTime.Today.Subtract(TimeSpan.FromDays(1));
            dt_fecha_inicial.Value = result;
            dt_fecha_final.Value = DateTime.Today;
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            fecha_ini = Misc_helper.fecha(dt_fecha_inicial.Value.ToString(), "ISO");
            fecha_fin = Misc_helper.fecha(dt_fecha_final.Value.ToString(), "ISO");

            dgv_reporte.DataSource = dao_ant.reporte_recetas(fecha_ini, fecha_fin);
        }

        private void dt_fecha_inicial_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void dgv_reporte_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /*
            string movimiento = "VENTA";
            long elemento_id;
            long control_ab_id;
            long control_ab_receta_id;
            long folio_receta;

            if (dgv_reporte.Rows.Count > 0)
            {
                control_ab_receta_id = (long)dgv_reporte.Rows[dgv_reporte.CurrentRow.Index].Cells["control_ab_receta_id"].Value;
                elemento_id = (long)dgv_reporte.Rows[dgv_reporte.CurrentRow.Index].Cells["venta_id"].Value;
                control_ab_id = (long)dgv_reporte.Rows[dgv_reporte.CurrentRow.Index].Cells["control_antibiotico_id"].Value;
                folio_receta = Convert.ToInt64(dgv_reporte.Rows[dgv_reporte.CurrentRow.Index].Cells["folio_receta"].Value.ToString());

                Editar_ticket_venta editar_ticket = new Editar_ticket_venta(control_ab_id, control_ab_receta_id, elemento_id, movimiento, folio_receta);
                editar_ticket.ShowDialog();

                DataGridViewRow r = dgv_reporte.SelectedRows[0];

                if (!editar_ticket.venta_id.Equals(0))
                {
                    dgv_reporte.DataSource = dao_ant.reporte_recetas(fecha_ini, fecha_fin);
                }
            }
             * */
        }
    }
}
