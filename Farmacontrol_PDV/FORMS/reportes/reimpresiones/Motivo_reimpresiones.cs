using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;


using System.Net.Mail;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.FORMS.reportes.reimpresiones
{
    public partial class Motivo_reimpresiones : Form
    {
        private long impresion_id;

       
        public Motivo_reimpresiones(long impresion_id,long empleado_id)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.impresion_id = impresion_id;
            this.empleado_id = empleado_id;
            txtMotivoImpresion.Focus();
        }

        public void set_envio_codigo()
        {

            long sucursal_local = Convert.ToInt64(Config_helper.get_config_local("sucursal_id"));
            int sucursal_id = Convert.ToInt32(sucursal_local);
            DAO_Sucursales dao_sucursales = new DAO_Sucursales();
            DTO_Sucursal sucursal_data = dao_sucursales.get_sucursal_data(sucursal_id);

            string email_suc = sucursal_data.email;

            MailMessage correo = new MailMessage();

            string hash = Misc_helper.EncodeTo64(impresion_id.ToString());

            correo.From = new MailAddress("informatica@emeritafarmacias.com", "Boot Farmacontrol", System.Text.Encoding.UTF8);

            correo.To.Add(email_suc);

            correo.Subject = "Solicitud de codigo de reimpresion ";

            correo.Body = "Codigo solicitado :  " + hash + " <br><br> Copia y pega el codigo anterior para la reimpresion";

            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Host = "server297.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("informatica@emeritafarmacias.com", "efarmacias1");
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            smtp.EnableSsl = true;
            smtp.Send(correo);

            MessageBox.Show(this, "Solicitud enviada correctamente, en breve se recibira el codigo  al correo " +email_suc , "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            this.set_envio_codigo();
        }

        private void btnreimprimir_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Trim() != "" && txtMotivoImpresion.Text.Trim() != "")
            {

                string codigob64 = txtCodigo.Text.Trim();

                try
                { 
                    long folio =  Convert.ToInt64( Misc_helper.DecodeBase64(codigob64));
                    if (folio == impresion_id)
                    {
                        //REGISTRAR MOTIVO

                       DAO_motivos_reimpresion m_reimpresion = new DAO_motivos_reimpresion();

                        bool bcomentarios  = m_reimpresion.setMotivo_impresion(impresion_id, txtMotivoImpresion.Text, empleado_id);

                        if (!bcomentarios)
                        {
                            MessageBox.Show(this, "Solicitud registrada ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Print_new_helper.print_force(impresion_id);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(this, "Error al registrar el motivo de la reimpresion, notifica a sistemas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                        }

                        
                    }
                    else
                    {
                        MessageBox.Show(this, "Codigo invalido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigo.Focus();
                    }
                }
                catch (FormatException folio)
                {

                   // Console.WriteLine("{0} Exception caught.", e);
                    MessageBox.Show(this, "Codigo invalido {0} Exception caught." + folio, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }

                
            }
            else
            {

                MessageBox.Show(this, "Datos incompletos, completa los campos vacios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (txtMotivoImpresion.Text.Trim() == "")
                    txtMotivoImpresion.Focus();
                else
                    txtCodigo.Focus();

            }
            

        }

        private void Motivo_reimpresiones_Load(object sender, EventArgs e)
        {
            txtMotivoImpresion.Focus();
        }

        private void Motivo_reimpresiones_Activated(object sender, EventArgs e)
        {
            txtMotivoImpresion.Focus();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                txtCodigo.UseSystemPasswordChar = false;
            
            }
            else
                txtCodigo.UseSystemPasswordChar = true;
        }









        public long empleado_id { get; set; }
    }
}
