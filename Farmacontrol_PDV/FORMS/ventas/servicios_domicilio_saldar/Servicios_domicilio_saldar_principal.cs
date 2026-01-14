using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.FORMS.comunes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farmacontrol_PDV.FORMS.ventas.servicios_domicilio_saldar
{
    public partial class Servicios_domicilio_saldar_principal : Form
    {
        public Servicios_domicilio_saldar_principal()
        {
            InitializeComponent();
        }

        private void Servicios_domicilio_saldar_principal_Shown(object sender, EventArgs e)
        {
            get_ventas_envios_saldar();
        }

        void get_ventas_envios_saldar()
        {
            DAO_Ventas dao_vents = new DAO_Ventas();
            dgv_ventas.DataSource = dao_vents.get_ventas_envios_saldar();
            dgv_ventas.ClearSelection();

            if(dgv_ventas.Rows.Count == 0)
            {
                MessageBox.Show(this,"No existen envios por saldar","Información",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void btn_ticket_Click(object sender, EventArgs e)
        {
            if (dgv_ventas.SelectedRows.Count > 0)
            {
                Ventas_envios ticket = new Ventas_envios();
                long venta_folio_envio = Convert.ToInt64(dgv_ventas.SelectedRows[0].Cells["folio"].Value);

                ticket.construccion_ticket(venta_folio_envio,true);
                ticket.print();
            }
            else
            {
                MessageBox.Show(this,"Es necesario seleccionar el folio a reimprimir","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btn_saldar_Click(object sender, EventArgs e)
        {
            if (dgv_ventas.SelectedRows.Count > 0)
            {
                Login_form login = new Login_form();
                login.ShowDialog();

                if(login.empleado_id != null)
                {
                    DAO_Login dao_login = new DAO_Login();
                    bool es_encargado = dao_login.empleado_es_encargado((long)login.empleado_id);

                    if (es_encargado)
                    {
                        long venta_folio_envio = Convert.ToInt64(dgv_ventas.SelectedRows[0].Cells["folio"].Value);
                        DAO_Ventas dao_ventas = new DAO_Ventas();
                        if (dao_ventas.saldar_ventas_envios(venta_folio_envio) > 0)
                        {
                            MessageBox.Show(this, "Ventas saldadas correctamente");
                            get_ventas_envios_saldar();
                        }
                        else
                        {
                            MessageBox.Show(this, "Ocurrio un error al tratar de saldar las ventas, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Solo el encargado puede saldar las ventas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "Es necesario seleccionar el folio a reimprimir", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
