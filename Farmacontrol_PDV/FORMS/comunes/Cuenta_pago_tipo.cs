using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farmacontrol_PDV.FORMS.comunes
{
    public partial class Cuenta_pago_tipo : Form
    {
        public string metodo_pago = "";
        public string cuenta = "";

        public Cuenta_pago_tipo()
        {
            InitializeComponent();
            txt_cuenta.Enabled = false;
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            var pago_tipo = cbb_tipos_pago.SelectedItem as DTO_Pago_tipos;
            
            if(pago_tipo.usa_cuenta)
            {
                if (txt_cuenta.Text.Trim().Length > 0)
                {
                    asignar_metodo_pago(pago_tipo);
                }
                else
                {
                    MessageBox.Show(this,"Para este metodo de pago es necesario el numero de cheque/cuenta","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

                    txt_cuenta.Enabled = true;
                    txt_cuenta.Focus();
                    txt_cuenta.SelectAll();
                }
            }
            else
            {
                asignar_metodo_pago(pago_tipo);
            }
        }

        void asignar_metodo_pago(DTO_Pago_tipos pago_tipo)
        {
            DialogResult dr = MessageBox.Show(this, "La factura se amitira con el metodo de pago \"" + pago_tipo.nombre + "\", ¿Desea continuar?", "Metodo de pago", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dr == DialogResult.Yes)
            {
                metodo_pago = pago_tipo.nombre;
                cuenta = txt_cuenta.Text;
                this.Close();
            }
        }

        private void Cuenta_pago_tipo_Shown(object sender, EventArgs e)
        {
            DAO_Pago_tipos dao = new DAO_Pago_tipos();
            cbb_tipos_pago.DataSource = dao.get_pago_tipos(null, true);
            cbb_tipos_pago.DisplayMember = "nombre";
            cbb_tipos_pago.ValueMember = "nombre";

            cbb_tipos_pago.SelectedValue = "NO IDENTIFICADO";
            cbb_tipos_pago.DroppedDown = true;
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbb_tipos_pago_DropDownClosed(object sender, EventArgs e)
        {
            validar_usa_cuenta();
        }

        void validar_usa_cuenta()
        {
            var pago_tipo = cbb_tipos_pago.SelectedItem as DTO_Pago_tipos;

            if (pago_tipo.usa_cuenta)
            {
                txt_cuenta.Enabled = true;
                txt_cuenta.Focus();
            }
            else
            {
                txt_cuenta.Enabled = false;
                txt_cuenta.Text = "";
                btn_aceptar.Focus();
            }
        }

        private void txt_cuenta_Enter(object sender, EventArgs e)
        {
            txt_cuenta.SelectAll();
        }

        private void cbb_tipos_pago_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch(keycode)
            {
                case 13:
                    validar_usa_cuenta();
                break;
                case 27:
                    this.Close();
                break;
            }
        }

        private void txt_cuenta_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch(keycode)
            {
                case 13:
                    if(txt_cuenta.Text.Trim().Length >= 4)
                    {
                        btn_aceptar.Focus();
                    }
                    else
                    {
                        MessageBox.Show(this,"Es necesario incluir la cuenta del metodo de pago","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                break;
                case 27:
                    if (txt_cuenta.Text.Trim().Length > 0)
                    {
                        txt_cuenta.Text = "";
                    }
                    else
                    {
                        cbb_tipos_pago.Focus();
                        cbb_tipos_pago.DroppedDown = true;
                    }
                break;
            }
        }

        private void btn_aceptar_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch(keycode)
            {
                case 27:
                    var pago_tipo = cbb_tipos_pago.SelectedItem as DTO_Pago_tipos;

                    if (pago_tipo.usa_cuenta)
                    {
                        txt_cuenta.Enabled = true;
                        txt_cuenta.Focus();
                        txt_cuenta.SelectAll();
                    }
                    else
                    {
                        cbb_tipos_pago.Focus();
                        cbb_tipos_pago.DroppedDown = true;
                    }
                break;
            }
        }
    }
}
