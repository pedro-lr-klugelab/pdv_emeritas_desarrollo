using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.CLASSES;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.FORMS.comunes;
using System.Configuration;

namespace Farmacontrol_PDV.FORMS.movimientos.Circulo_Salud
{
    public partial class alta_clientes_oro : Form
    {
        public string ip_servidor = ConfigurationManager.AppSettings["server_beneficios"];
     
        public string sesion = "";

        public alta_clientes_oro( string sesion_login)
        {
            InitializeComponent();
            this.sesion = sesion_login;
            lblSession.Text = this.sesion;
        }

        

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtTarjeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCodigoPostal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            try
            {
                return new System.Net.Mail.MailAddress(email).Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void txtCorreo_Leave(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text.Trim();

            if (!IsValidEmail(correo))
            {
                MessageBox.Show("Correo electrónico inválido");
                txtCorreo.Text = string.Empty;
                txtCorreo.Focus();
            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(this, "¿Desea guardar los datos ?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (dr == DialogResult.Yes)
            {

                Circulo_Oro circulo_oro = new Circulo_Oro();
                
                string nombre = txtNombre.Text.TrimEnd();
                string apellidoP = txtPaterno.Text.Trim();
                string apellidoM = txtMaterno.Text.Trim();
                string tarjeta = txtTarjeta.Text.Trim();
                string telefono = txtTel.Text.Trim();
                string correo = txtCorreo.Text.Trim();
                string codigoPostal = txtCodigoPostal.Text.Trim();
                string sexo =  Convert.ToString( cbxSexo.SelectedItem);
                string fecha_nacimiento = dtPickerFecha.Text;


                if (!nombre.Equals("") && !apellidoM.Equals("") && !apellidoP.Equals("") && !tarjeta.Equals("") && !telefono.Equals("") && !correo.Equals("") && !codigoPostal.Equals(""))
                {

                     Login_form login = new Login_form();
                     login.ShowDialog();

                     DateTime fecha_n = DateTime.Parse(dtPickerFecha.Text.ToString());
                     fecha_nacimiento = fecha_n.ToString("yyyy-MM-dd");

                     if (login.empleado_id != null)
                     {

                         string nombre_empleado = login.empleado_nombre.ToString().Length > 20 ? login.empleado_nombre.ToString().Substring(0, 20) : login.empleado_nombre.ToString();
                         sexo = sexo.Substring(0, 1);

                         DTO_WebServiceCirculoOro val = new DTO_WebServiceCirculoOro();
                         Rest_parameters datos = new Rest_parameters();
                         datos.Add("Session", lblSession.Text);
                         datos.Add("Usuario", nombre_empleado);
                         datos.Add("Tarjeta", tarjeta);
                         datos.Add("Nombre", nombre);
                         datos.Add("Paterno", apellidoP);
                         datos.Add("Materno", apellidoM);
                         datos.Add("Telefono", telefono);
                         datos.Add("Correo", correo);
                         datos.Add("Sexo", sexo);
                         datos.Add("fechanacimiento", fecha_nacimiento);
                         datos.Add("CodigoPostal", codigoPostal);
                       
                         val = Rest_helper.enlace_webservice_Circulo_Oro<DTO_WebServiceCirculoOro>("webservice_Oro/activateCardUser_WSDL", datos, ip_servidor);
                         if (!val.huboError)
                         {
                             MessageBox.Show(this, "Datos registrados correctamente");

                             this.Close();

                         }
                         else
                         {
                             MessageBox.Show(this, val.mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         }
                     }
                }
                else
                {
                    MessageBox.Show(this, "Datos incompletos !!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                }


            }
        
        }

        private void txtTel_Leave(object sender, EventArgs e)
        {
            if (txtTel.Text.ToString().Length < 10)
            {
                MessageBox.Show("Número telefonico debe ser a 10 dígitos");
                
                txtTel.Focus();
            }
        }


    }
}
