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

namespace Farmacontrol_PDV.FORMS.movimientos.canjes_lealtad
{
    public partial class Programa_lealtad_principal : Form
    {

        string ip_conexion = "192.168.1.250";
        //string ip_conexion = "172.16.1.5";
        DateTime  tiempoInicialTarjeta,tiempoFinalTarjeta;
        int milisegundosT;
       


        public Programa_lealtad_principal()
        {
            InitializeComponent();
            txtTarjetaLealtad.ContextMenuStrip = new ContextMenuStrip();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(this," ¿Desea salir del programa?","Información",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void txtTarjetaLealtad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                MessageBox.Show(this, "Es necesario escanear la tarjeta de Lealtad");
            }


            long largo = txtTarjetaLealtad.Text.Length;
            if (largo == 1)
            {
                tiempoInicialTarjeta = DateTime.Now;
            }
            else
            {
                tiempoFinalTarjeta = DateTime.Now;
                TimeSpan diferencia = tiempoFinalTarjeta - tiempoInicialTarjeta;
                milisegundosT = diferencia.Milliseconds;
                if (milisegundosT >= 800)
                {
                    MessageBox.Show(this, "No se puede ingresar datos a traves del teclado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTarjetaLealtad.Text = "";
                }

            }

            

        }

        private void txtGafete_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                MessageBox.Show(this, "Es necesario escanear el gafete");
            }
    
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
             Login_form login = new Login_form();
             login.ShowDialog();
             if (login.empleado_id != null)
             {
 
                if (txtTarjetaLealtad.Text != "")
                {

                    string tarjeta_escaneada = txtTarjetaLealtad.Text;

                    //SE CONECTA A ENLACE VITAL

                    DTO_WebServiceEnlaceVital val = new DTO_WebServiceEnlaceVital();

                    Rest_parameters parametros = new Rest_parameters();
                    parametros.Add("tarjeta", txtTarjetaLealtad.Text);
                    parametros.Add("storeid", Config_helper.get_config_local("sucursal_id").ToString());
                    parametros.Add("posid", Misc_helper.get_terminal_id().ToString());
                    parametros.Add("employeeid", login.empleado_id.ToString());

                    val = Rest_helper.enlace_webservice_lealtad<DTO_WebServiceEnlaceVital>("webservice/set_transaccion", parametros, ip_conexion);

                    if (val.status)
                    {
                        this.Close();//CIERRA LA VENTANA ACTUAL Y ABRE LA VENTANA NUEVA
                        opciones_lealtad_principal opciones_lealtad = new opciones_lealtad_principal(tarjeta_escaneada, val.transaccion, login.empleado_id.ToString(), login.empleado_nombre.ToString() );
                        opciones_lealtad.ShowDialog(); 
                    }
                    else
                    {
                        MessageBox.Show(this, val.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {

                    MessageBox.Show(this, "Complete los datos correctamente, verifique!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (txtTarjetaLealtad.Text == "")
                        txtTarjetaLealtad.Focus();
                }
            }

        }

        private void txtGafete_KeyPress(object sender, KeyPressEventArgs e)
        {

            


        }
    }
}
