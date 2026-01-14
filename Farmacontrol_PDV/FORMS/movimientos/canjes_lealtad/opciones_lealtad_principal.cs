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

namespace Farmacontrol_PDV.FORMS.movimientos.canjes_lealtad
{
    public partial class opciones_lealtad_principal : Form
    {
        //string ip_conexion = "192.168.1.250";
        string ip_conexion = "172.16.1.5";
        string tarjeta;
        string transaccion = "";
        string empleado_id;
        string nombre_empleado;

        public opciones_lealtad_principal( string tarjeta_escaneada, string transaccion_ws, string empleado , string nombre_mostrador  )
        {
            InitializeComponent();
            this.tarjeta = tarjeta_escaneada;
            this.transaccion = transaccion_ws;
            this.empleado_id = empleado;
            this.nombre_empleado = nombre_mostrador;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(this, " ¿Desea salir del programa?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dr == DialogResult.Yes)
            {

                this.set_elimina_transaccion();
            }
        }


        public void set_elimina_transaccion()
        {

            if (transaccion != "")
            {
                DTO_WebServiceEnlaceVital val = new DTO_WebServiceEnlaceVital();

                Rest_parameters parametros = new Rest_parameters();
                parametros.Add("tarjeta", tarjeta);
                parametros.Add("storeid", Config_helper.get_config_local("sucursal_id").ToString());
                parametros.Add("posid", Misc_helper.get_terminal_id().ToString());
                parametros.Add("employeeid", empleado_id.ToString());
                parametros.Add("transaccion", transaccion.ToString());

                val = Rest_helper.enlace_webservice_lealtad<DTO_WebServiceEnlaceVital>("webservice/set_cancelar_transaccion", parametros, ip_conexion);

                if (val.status)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show(this, val.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }     
            }
        
        }


        public void llena_informacion()
        {
            txtTarjetaOpcion.Visible = true;
            txtTransaccionLealtad.Visible = true;
            txtTarjetaOpcion.Text = this.tarjeta;
            txtTransaccionLealtad.Text = transaccion;
            txtNombreEmpleado.Text = nombre_empleado;
        }

        private void opciones_lealtad_principal_Load(object sender, EventArgs e)
        {
            this.llena_informacion();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {

        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            compras_beneficios_principal window_venta = new compras_beneficios_principal(tarjeta, nombre_empleado, empleado_id, transaccion);
            this.Close();
            window_venta.ShowDialog();
            
        }

    }
}
