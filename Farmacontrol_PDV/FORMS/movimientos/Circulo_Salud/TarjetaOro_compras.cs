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
using System.Configuration;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.CLASSES;

namespace Farmacontrol_PDV.FORMS.movimientos.Circulo_Salud
{
    public partial class TarjetaOro_compras : Form
    {
        string sesion = "";
        public string ip_servidor = ConfigurationManager.AppSettings["server_beneficios"];
        public TarjetaOro_compras( string Session )
        {
            sesion = Session;
            InitializeComponent();
            
        }



        private void bntAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TarjetaOro_compras_Load(object sender, EventArgs e)
        {
            txtTarjeta.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CONSULTA LAS COMPRAS DEL CLIENTE
            string tarjeta = txtTarjeta.Text.ToString().Trim();
            if (!tarjeta.Equals(""))
            {

                DTO_WebServiceCirculoOro val = new DTO_WebServiceCirculoOro();

                Rest_parameters datos = new Rest_parameters();

                datos.Add("Sesion", sesion);
                datos.Add("NoTarjeta", tarjeta);

                val = Rest_helper.enlace_webservice_Circulo_Oro<DTO_WebServiceCirculoOro>("webservice_Oro/GetProductosPaciente", datos, ip_servidor);

                if (!val.huboError)
                {
                    dtgCompras.DataSource = val.compras;
                    txtTarjeta.Focus();
                    txtTarjeta.Select();
                }
                else
                {
                    MessageBox.Show(this, val.mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show(this,"El campo no puede estar vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTarjeta.Focus();
            }
        }

        private void txtTarjeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
