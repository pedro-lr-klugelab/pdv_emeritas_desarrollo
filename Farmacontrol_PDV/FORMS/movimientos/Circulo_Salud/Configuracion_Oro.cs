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

namespace Farmacontrol_PDV.FORMS.movimientos.Circulo_Salud
{
    public partial class Configuracion_Oro : Form
    {
        public Configuracion_Oro()
        {
            InitializeComponent();
            get_parametros_oro();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            /**GUARDAR LOS PARAMETROS*/

            string Usr = txtUsuario.Text.Trim();
            string Pass = txtPass.Text.Trim();
            string CodCadena = txtCadena.Text.Trim();
            string Sucursal = txtSucursal.Text.Trim();


            if (!Usr.Equals("") && !Pass.Equals("") && !Usr.Equals("") && !CodCadena.Equals("") && !Sucursal.Equals(""))
            {

                string valor = Usr + "*" + Pass + "*" + CodCadena + "*" + Sucursal;

                bool res = HELPERS.Config_helper.set_update_config_oro(valor);

                if( res )
                    MessageBox.Show(this, "Parametros registrados correctamente");
                else
                    MessageBox.Show(this, res.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                MessageBox.Show(this, "Datos incompletos,verifica !!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            }


        }


        public void get_parametros_oro()
        {
            string resultado = HELPERS.Config_helper.get_config_local("parametros_circulo_salud");



            if(  !resultado.Equals("") )
            {
                /*PONER PARAMETROS***/

                string[] parametros  = resultado.Split('*');

                txtUsuario.Text = parametros[0];
                txtPass.Text = parametros[1];
                txtCadena.Text = parametros[2];
                txtSucursal.Text = parametros[3];


            }
            
        
        }

    }
}
