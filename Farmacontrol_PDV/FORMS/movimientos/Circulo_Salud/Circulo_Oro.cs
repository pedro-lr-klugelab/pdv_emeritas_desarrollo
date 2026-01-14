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
using System.Configuration;


namespace Farmacontrol_PDV.FORMS.movimientos.Circulo_Salud
{
    public partial class Circulo_Oro : Form
    {
       

        public string ip_servidor = ConfigurationManager.AppSettings["server_beneficios"];
        public Circulo_Oro()
        {
            InitializeComponent();
        }

        private void txtbox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtFolioVenta_TextChanged(object sender, EventArgs e)
        {
            

        } 

        private void txtTarjetaSalud_Leave(object sender, EventArgs e)
        {
        }

        private void txtFolioVenta_Leave(object sender, EventArgs e)
        {
        }

        private void txtTarjetaSalud_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtFolioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !(e.KeyChar == 36) ))
            {
                e.Handled = true;
            }
        }

        private void Circulo_Oro_Load(object sender, EventArgs e)
        {
            /*LECTURA DE LOS PARAMETROS DE CONEXION */

            string resultado = HELPERS.Config_helper.get_config_local("parametros_circulo_salud");

            if (!resultado.Equals(""))
            {
                #region Inicio de Login

                DTO_WebServiceCirculoOro val = new DTO_WebServiceCirculoOro();

                Rest_parameters datos = new Rest_parameters();

                string[] parametros = resultado.Split('*');

                
                datos.Add("Usuario", parametros[0]);
                datos.Add("Password", parametros[1]);
                datos.Add("CodigoCadena", parametros[2]);
                datos.Add("CodigoSucursal", parametros[3]);

                val = Rest_helper.enlace_webservice_Circulo_Oro<DTO_WebServiceCirculoOro>("webservice_Oro/login_WSDL", datos, ip_servidor);
                
                if (!val.huboError)
                {
                    LblSesion.Visible = true;
                      LblSesion.Text = val.sesion;
                }
                else
                {
                    LblSesion.Visible = false;
                  
                    MessageBox.Show(this, val.mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }     

                #endregion
            }
            else
            {
                MessageBox.Show(this, string.Format("Los parametros de Inicio de sesion no se encuentra configurado!"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            }



        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            Configuracion_Oro configuracion = new Configuracion_Oro();
            configuracion.Show();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
           
            /****cierra conexion si lo existiera*/;
            set_termina_sesion();
            this.Close();
 
        }

        private void btnRegClientes_Click(object sender, EventArgs e)
        {
           string sesion_login = LblSesion.Text.ToString();

           if (!sesion_login.Equals(""))
           {
               alta_clientes_oro alta_clientes = new alta_clientes_oro(sesion_login);
               alta_clientes.Show();
           }
           else
           {
               MessageBox.Show(this, string.Format("Los parametros de Inicio de sesion no se encuentra configurado!"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }

           
        }

        private void Circulo_Oro_FormClosing(object sender, FormClosingEventArgs e)
        {
          //  set_termina_sesion();
           // this.Close();
        }

        public bool set_termina_sesion()
        {

            string resultado = HELPERS.Config_helper.get_config_local("parametros_circulo_salud");

            if (!resultado.Equals(""))
            {
                #region Ciere de sesion Login

                DTO_WebServiceCirculoOro val = new DTO_WebServiceCirculoOro();

                Rest_parameters datos = new Rest_parameters();

                string session_vigente = LblSesion.Text;

                datos.Add("Session", session_vigente);

                val = Rest_helper.enlace_webservice_Circulo_Oro<DTO_WebServiceCirculoOro>("webservice_Oro/logout_WSDL", datos, ip_servidor);

                if (!val.huboError)
                {
                    LblSesion.Visible = false;
                    LblSesion.Text = "";
                }
                else
                {
                    LblSesion.Visible = false;

                    MessageBox.Show(this, val.mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                #endregion
 
            }

            return true;
        }

        private void btnValidaTarjeta_Click(object sender, EventArgs e)
        {

            string resultado = HELPERS.Config_helper.get_config_local("parametros_circulo_salud");

            if (!resultado.Equals("") && !LblSesion.Text.Equals(""))
            {
                Informacion_Tarjeta_Oro informacion = new Informacion_Tarjeta_Oro( LblSesion.Text.ToString() );
                informacion.Show();


            }
            else
            {
                MessageBox.Show(this, string.Format("Los parametros de Inicio de sesion no se encuentra configurado!"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);            
            }


        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            if (txtFolioVenta.Text.Equals("") || txtTarjetaSalud.Text.Equals(""))
            {
                MessageBox.Show(this, string.Format("Los campos se encuentran vacios !!!, completa correctamente"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (txtFolioVenta.Text.Equals(""))
                    txtFolioVenta.Focus();
                else
                    txtTarjetaSalud.Focus();

            }
            else
            { 
                /**Consulta ticket***/

                string[] codigo = txtFolioVenta.Text.Split('$');

                if (codigo.Length == 2)
                {
                    bool todos_numeros = true;

                    foreach (string items in codigo)
                    {
                        int n;
                        bool isNumeric = int.TryParse(items, out n);

                        if (isNumeric == false)
                        {
                            todos_numeros = false;
                            break;
                        }
                    }

                    if (todos_numeros)
                    {
                        if (Convert.ToInt64(codigo[0]).Equals(Convert.ToInt64(Config_helper.get_config_local("sucursal_id"))))
                        {
                             Int64 venta_id = Convert.ToInt64(codigo[1]);

                            DTO_WebServiceCirculoOro val = new DTO_WebServiceCirculoOro();
                            Rest_parameters datos = new Rest_parameters();
                            datos.Add("Session", LblSesion.Text.ToString());
                            datos.Add("Tarjeta", txtTarjetaSalud.Text.ToString());
    
                            val = Rest_helper.enlace_webservice_Circulo_Oro<DTO_WebServiceCirculoOro>("webservice_Oro/getInfoCard_WSDL", datos, ip_servidor);
                            if (!val.huboError)
                            {
                                Beneficios_Oro beneficios = new Beneficios_Oro(LblSesion.Text.ToString(), txtTarjetaSalud.Text.ToString(), venta_id,this);
                                beneficios.Show();

                            }
                            else
                            {
                                MessageBox.Show(this, val.mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            
                        }
                        else
                        {
                            MessageBox.Show(this, "Este codigo de venta pertenece a otra sucursal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Codigo de venta invalido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Codigo de venta invalido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
       



                /**/
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string resultado = HELPERS.Config_helper.get_config_local("parametros_circulo_salud");

            if (!resultado.Equals(""))
            {
                #region Inicio de Login

                DTO_WebServiceCirculoOro val = new DTO_WebServiceCirculoOro();

                Rest_parameters datos = new Rest_parameters();

                string[] parametros = resultado.Split('*');


                datos.Add("Usuario", parametros[0]);
                datos.Add("Password", parametros[1]);
                datos.Add("CodigoCadena", parametros[2]);

                val = Rest_helper.enlace_webservice_Circulo_Oro<DTO_WebServiceCirculoOro>("webservice_Oro/get_consulta_catalogo_cdo", datos, ip_servidor);

                if (!val.huboError)
                {
                    Catalogo_Oro catalogo = new Catalogo_Oro(val);
                    catalogo.Show();
                }
                else
                {
                   

                    MessageBox.Show(this, val.mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                #endregion
            }
            else
            {
                MessageBox.Show(this, string.Format("Los parametros de Inicio de sesion no se encuentra configurado!"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private void txtFolioVenta_Enter(object sender, EventArgs e)
        {
           

        }

        private void bntTarjetaConsulta_Click(object sender, EventArgs e)
        {
            TarjetaOro_compras consulta_compras = new TarjetaOro_compras(LblSesion.Text);
            consulta_compras.Show();
        }

        private void txtTarjetaSalud_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!txtTarjetaSalud.Text.Trim().Equals(""))
                    txtFolioVenta.Focus();
            }
        }

    }
}
