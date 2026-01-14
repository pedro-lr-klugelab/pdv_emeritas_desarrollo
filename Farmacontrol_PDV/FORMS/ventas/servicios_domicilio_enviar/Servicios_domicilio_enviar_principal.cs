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

namespace Farmacontrol_PDV.FORMS.ventas.servicios_domicilio_enviar
{
    public partial class Servicios_domicilio_enviar_principal : Form
    {
        public Servicios_domicilio_enviar_principal()
        {
            InitializeComponent();
        }

        private void Servicios_domicilio_enviar_principal_Shown(object sender, EventArgs e)
        {
            get_ventas_domicilio();
        }

        void get_ventas_domicilio()
        {
            DAO_Ventas dao_ventas = new DAO_Ventas();
            dgv_ventas.DataSource = dao_ventas.get_ventas_domicilio_enviar();
            dgv_ventas.ClearSelection();
        }

        private void btn_enviar_Click(object sender, EventArgs e)
        {
            registrar_ventas_envios();
        }

        void registrar_ventas_envios()
        {
            List<long> ventas_folios = new List<long>();

            foreach(DataGridViewRow row in dgv_ventas.Rows)
            {
                if(Convert.ToBoolean(row.Cells["enviar"].Value))
                {
                    ventas_folios.Add(Convert.ToInt64(row.Cells["venta_id"].Value));
                }
            }

            if (ventas_folios.Count > 0)
            {
                Login_form login = new Login_form();
                login.ShowDialog();

                if(login.empleado_id != null)
                {
                    DAO_Empleados dao_empleados = new DAO_Empleados();

                    if (dao_empleados.es_empleado_diligenciero((long)login.empleado_id))
                    {
                        DAO_Ventas dao_ventas = new DAO_Ventas();

                        long venta_envio_folio = dao_ventas.registrar_ventas_envios((long)login.empleado_id, ventas_folios);

                        if (venta_envio_folio > 0)
                        {
                            Ventas_envios ticket = new Ventas_envios();
                            ticket.construccion_ticket(venta_envio_folio);
                            ticket.print();

                            MessageBox.Show(this, "Ventas enviadas correctamente");
                            get_ventas_domicilio();
                        }
                        else
                        {
                            MessageBox.Show(this, "Ocurrio un error al intentar asignar las ventas como enviadas, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this,"Solo el diligenciero que trasportara las ventas puede autorizar el envio", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(this,"Es necesario seleccion alguna venta a enviar","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
