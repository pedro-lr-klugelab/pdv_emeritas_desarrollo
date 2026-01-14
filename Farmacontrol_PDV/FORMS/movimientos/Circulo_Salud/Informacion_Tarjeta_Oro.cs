using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Farmacontrol_PDV.CLASSES;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;
using System.Configuration;


namespace Farmacontrol_PDV.FORMS.movimientos.Circulo_Salud
{
    public partial class Informacion_Tarjeta_Oro : Form
    {
        public string sesion = "";
        public string ip_servidor = ConfigurationManager.AppSettings["server_beneficios"];

        public Informacion_Tarjeta_Oro(string sesion_login)
        {
            InitializeComponent();
            sesion = sesion_login;
            lblSesion.Text = sesion;
            txtTarjeta.Focus();
        }

        private void txtTarjeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {

            DTO_WebServiceCirculoOro val = new DTO_WebServiceCirculoOro();
            Rest_parameters datos = new Rest_parameters();
            datos.Add("Session", lblSesion.Text.ToString());
            datos.Add("Tarjeta", txtTarjeta.Text.ToString());


            val = Rest_helper.enlace_webservice_Circulo_Oro<DTO_WebServiceCirculoOro>("webservice_Oro/getInfoCard_WSDL", datos, ip_servidor);
            if (!val.huboError)
            {
                lblnombres.Text = val.nombre_completo;
                lblActivacion.Text = val.fecha_activacion;
                lblvigencia.Text = val.fecha_vigencia;

            }
            else
            {
                MessageBox.Show(this, val.mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void bntAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
