using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.FORMS.comunes;

namespace Farmacontrol_PDV.FORMS.cortes.vales
{
    public partial class vales_farmacia : Form
    {
        public vales_farmacia()
        {
            InitializeComponent();

            txtCliente.Focus();
        }


        public void generar_vale()
        {

            if (this.validar_campos())
            {

                Login_form login = new Login_form();
                login.ShowDialog();

                if (login.empleado_id != null)
                {
                    DAO_Login dao = new DAO_Login();
                    long empleado_id = (long)login.empleado_id;


                    string cliente = txtCliente.Text.Trim();
                    decimal importe = Convert.ToDecimal(txtImporte.Value.ToString());
                    string comentarios = txtComentarios.Text.Trim();

                    Vales tickets_vales = new Vales();
                    tickets_vales.construccion_ticket(cliente, importe, comentarios,empleado_id);
                    tickets_vales.print();

                    MessageBox.Show(this, "Vale generado correctamente");
                    this.Close();
                }


            }
            else
            {
                MessageBox.Show(this, "Complete los datos correctamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }


        bool validar_campos()
        {
            if (String.IsNullOrEmpty(txtCliente.Text))
            {
                return false;
            }

            if (String.IsNullOrEmpty(txtImporte.Text))
            {
                return false;
            }

            if (String.IsNullOrEmpty(txtComentarios.Text))
            {
                return false;
            }

            return true;
     
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            generar_vale();
        }
    }
}
