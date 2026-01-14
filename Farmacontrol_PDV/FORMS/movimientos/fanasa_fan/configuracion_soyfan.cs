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
using Farmacontrol_PDV.DTO;
using System.Configuration;

namespace Farmacontrol_PDV.FORMS.movimientos.canjes_lealtad
{
    public partial class configuracion_soyfan : Form
    {
        public string ip_servidor = ConfigurationManager.AppSettings["server_beneficios"];

        public configuracion_soyfan()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if( this.valida_datos() )
            {
            
                     DialogResult dr = MessageBox.Show(this, " ¿Guardar parametros ?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                     if (dr == DialogResult.Yes)
                     {


                         DTO_WebServiceSoyFan val = new DTO_WebServiceSoyFan();

                         Rest_parameters parametros = new Rest_parameters();
                        
                         parametros.Add("subsidiaryId", txtId.Text.ToString().Trim() );
                         parametros.Add("pos", txtPos.Text.ToString().Trim() );
                         parametros.Add("address", txtDireccion.Text.ToString().Trim());
                         parametros.Add("token", txtToken.Text.ToString().Trim());

                         val = Rest_helper.enlace_webservice_soyfan<DTO_WebServiceSoyFan>("webservice/get_gestionar_sucursales", parametros, ip_servidor);

                         if (val.status)
                         {
                             MessageBox.Show(this, val.mensaje, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            /*SE GUARDARA */     


                         }
                         else
                         {
                             MessageBox.Show(this, val.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         }


                     }

              }
            else
            {
                     MessageBox.Show(this, "Datos erroneos o incompletos, revisar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            }


        }

        public bool valida_datos()
        {
            bool valido = false;


            string subsidiary = txtId.Text.ToString().Trim();
            string pos = txtPos.Text.ToString().Trim();
            string token = txtToken.Text.ToString().Trim();
            string direccion =  txtDireccion.Text.ToString().Trim();

            if (subsidiary != "" && pos != "" && token != "" && direccion != "")
                valido = true; 

            return valido;
            

        
        }


    }
}
