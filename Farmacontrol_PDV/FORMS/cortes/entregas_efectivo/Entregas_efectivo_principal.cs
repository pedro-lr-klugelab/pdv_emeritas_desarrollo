using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.FORMS.comunes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farmacontrol_PDV.FORMS.cortes.entregas_efectivo
{
    public partial class Entregas_efectivo_principal : Form
    {
        public Entregas_efectivo_principal()
        {
            InitializeComponent();
        }

        bool validar_form()
        {
            if(txt_quien_recibe.Text.Trim().Length > 0)
            {
                if (txt_cantidad.Value > 0)
                {
                    if(txt_cantidad.Value.Equals(txt_confirmar.Value))
                    {
                        if (txt_comentarios.Text.Trim().Length > 0)
                        {
                            return true;
                        }
                        else
                        {
                            MessageBox.Show(this, "Es necesario detallar el motivo de la entrega de efectivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_comentarios.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Las cantidades no coinciden, verifique!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_cantidad.Focus();
                    }
                }
                else
                {
                    MessageBox.Show(this, "Es necesario especificar una cantidad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_cantidad.Focus();
                }
            }
            else
            {
                MessageBox.Show(this,"Es necesario especificar quien esta recibiendo el efectivo", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txt_quien_recibe.Focus();
            }

            return false; 
        }

        private void btn_procesar_Click(object sender, EventArgs e)
        {
            if(validar_form())
            {
                Login_form login = new Login_form();
                login.ShowDialog();

                if(login.empleado_id != null)
                {
                    DAO_Login dao = new DAO_Login();
                    long empleado_id = (long)login.empleado_id;

                    if (dao.empleado_es_encargado(empleado_id))
                    {
                        DAO_Entregas_efectivo dao_entergas = new DAO_Entregas_efectivo();

                        long entrega_efectivo_id = dao_entergas.registrar_entrega_efectivo(empleado_id, Convert.ToDecimal(txt_cantidad.Value), txt_quien_recibe.Text, txt_comentarios.Text);

                        if (entrega_efectivo_id > 0)
                        {
                            Entrega_efectivo entrega_efectivo = new Entrega_efectivo();
                            entrega_efectivo.construccion_ticket(entrega_efectivo_id);
                            entrega_efectivo.print();

                            MessageBox.Show(this,"Entrega de efectivo registrada correctamente");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(this, "Solo el encargado puede hacer la entrega de efectivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Solo el encargado puede hacer la entrega de efectivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void txt_quien_recibe_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_comentarios_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_quien_recibe_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch(keycode)
            {
                case 13:
                    txt_cantidad.Focus();
                    txt_cantidad.Value = 1;
                break;
            }
        }

        private void txt_cantidad_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch(keycode)
            {
                case 13:
                    txt_confirmar.Focus();
                break;
            }
        }

        private void Entregas_efectivo_principal_Shown(object sender, EventArgs e)
        {
            txt_quien_recibe.Focus();
        }

        private void txt_cantidad_Enter(object sender, EventArgs e)
        {
            txt_cantidad.Select(0, txt_cantidad.Value.ToString().Length+3);
        }

        private void txt_confirmar_Enter(object sender, EventArgs e)
        {
            txt_confirmar.Select(0, txt_confirmar.Value.ToString().Length+3);
        }

        private void txt_confirmar_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    txt_comentarios.Focus();
                break;
            }
        }
    }
}
