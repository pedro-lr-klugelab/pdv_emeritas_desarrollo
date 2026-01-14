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

namespace Farmacontrol_PDV.FORMS.movimientos.traspasos
{
    public partial class Datos_Enlace_Vital : Form
    {
        public string ticket;
        public string tarjeta;
        public string transaccion;
        public bool aceptar = false;

        public Datos_Enlace_Vital()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            aceptar = false;
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            this.guardar_datos();
            
        }


        public void guardar_datos()
        {

            aceptar = false;
            if (txtTicket.Text.Trim() != "" && txtTarjeta.Text.Trim() != "" && txtTransaccion.Text.Trim() != "")
            {

                if (Misc_helper.validar_codigo_venta(txtTicket.Text.Trim()))
                {
                    ticket = txtTicket.Text.Trim();
                    tarjeta = txtTarjeta.Text.Trim();
                    transaccion = txtTransaccion.Text.Trim();
                    aceptar = true;
                    this.Close();
                }
                else
                {

                    MessageBox.Show(this, "Formato erroneo de ticket, porfavor escanea el ticket correctamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTicket.Focus();
                }
            }
            else
            {

                MessageBox.Show(this, "Datos Errroneos,completa correctamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                txtTicket.Focus();
            }
                

        

        }

        private void txtTicket_KeyPress(object sender, KeyPressEventArgs e)
        {
          //  this.solo_numeros(e);
        }
        #region validadores
        public void solo_numeros(KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) )
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar) ) //permitir teclas de control como retroceso
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan
                    e.Handled = true;
                }
            }
        
        }

        private void txtTransaccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.solo_numeros(e);
        }

        private void txtTarjeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.solo_numeros(e);
        }
        #endregion
    }
}
