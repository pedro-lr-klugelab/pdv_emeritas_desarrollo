using Farmacontrol_PDV.CLASSES;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.FORMS.movimientos.canjes_lealtad;
using Farmacontrol_PDV.HELPERS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Farmacontrol_PDV.FORMS.movimientos.fanasa_fan
{
    public partial class inicio_lealtad : Form
    {
        public string ip_servidor = ConfigurationManager.AppSettings["server_beneficios"];
        public string cardAuthNum = "0";
        public inicio_lealtad()
        {
            InitializeComponent();
           
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            alta_usuarios_lealtad window_altauser = new alta_usuarios_lealtad();
            window_altauser.ShowDialog();

        }

        private void btnConfigurar_Click(object sender, EventArgs e)
        {

            configuracion_soyfan configuracion = new configuracion_soyfan();
            configuracion.ShowDialog();   

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (this.validar_data())
            {
               
                //muestra la siguiente ventana 
                //MessageBox.Show(this, "tarjeta valida con cardAuthNum : " + cardAuthNum, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                venta_beneficios_soyfan venta = new venta_beneficios_soyfan(txtVenta.Text.ToString(), cardAuthNum, txtTarjetaLealtad.Text.ToString());
                this.Close();
                venta.ShowDialog();
                
            }
            else
            {
                //MessageBox.Show(this, "Tarjeta no encontrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTarjetaLealtad.Focus();
            }

        }


        public bool validar_data()
        {
            bool operacion = true;
            string tarjeta = txtTarjetaLealtad.Text.ToString();
            string venta   = txtVenta.Text.ToString();
            if (tarjeta != "" && venta != "")
            {
                #region valida tarjeta soy fan

                DTO_WebServiceSoyFan val = new DTO_WebServiceSoyFan();
               
                Rest_parameters parametros = new Rest_parameters();
                string nombre_empleado = "farmaboot";
                parametros.Add("user", nombre_empleado);
                parametros.Add("transaction", venta);
                parametros.Add("idCRM", tarjeta);

               val = Rest_helper.enlace_webservice_soyfan<DTO_WebServiceSoyFan>("webservice/set_validar_tarjeta", parametros, ip_servidor);

               if (val.status)
               {

                   operacion = true;
                   cardAuthNum = val.cardAuthNum;
               }
               else
               {
                   operacion = false;
                   cardAuthNum = "";
                   MessageBox.Show(this, val.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               }     
                    
              
                #endregion




            }
            else
            {
                MessageBox.Show(this, "Datos incompetos,verifica!!" , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                operacion = false;
            }


            return operacion;
        
        }

        private void btnCatalogo_Click(object sender, EventArgs e)
        {
            Catalogo_completo ctalogo_soy_fan = new Catalogo_completo();
            ctalogo_soy_fan.ShowDialog();
        }

        private void txtTarjetaLealtad_KeyDown(object sender, KeyEventArgs e)
        {
           
    
        }

        private void txtTarjetaLealtad_Leave(object sender, EventArgs e)
        {
            if (txtTarjetaLealtad.Text.Length == 7)
            {
                btnAceptar.Enabled = true;
            }
            else
            {

                MessageBox.Show(this, "Esta Tarjeta de Lealtad corresponde a otro Programa!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnAceptar.Enabled = false;


            }
        }

        
    }
}
