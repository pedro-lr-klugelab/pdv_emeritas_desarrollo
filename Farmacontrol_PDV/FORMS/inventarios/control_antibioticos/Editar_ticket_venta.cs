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
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.FORMS.inventarios.control_antibioticos
{
    public partial class Editar_ticket_venta : Form
    {
        public long control_ab_id;
        public long control_ab_receta_id;
        public long elemento_id;
        public string movimiento;
        public long folio_receta;
        public long nuevo_ticket;
        public long venta_id = 0;
        public long venta_folio = 0;

        DAO_Ventas dao_ventas = new DAO_Ventas();

        public Editar_ticket_venta(long control_ab_id, long control_ab_receta_id, long elemento_id, string movimiento, long folio_receta)
        {
            InitializeComponent();
            this.control_ab_id = control_ab_id;
            this.control_ab_receta_id = control_ab_receta_id;
            this.elemento_id = elemento_id;
            this.movimiento = movimiento;
            this.folio_receta = folio_receta;
        }

        public void validar_venta_id(long venta_id)
        {
            var venta_data = dao_ventas.get_venta_data(venta_id);

            if (venta_data.venta_id > 0)
            {
                this.venta_id = venta_id;
                this.venta_folio = venta_data.venta_folio;
                txt_folio_venta.Text = venta_folio.ToString();
                btn_guardar.Enabled = true;
            }
            else
            {
                MessageBox.Show(this, "Esta venta no se encuentra registrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_nuevo_ticket.SelectAll();
                txt_nuevo_ticket.Focus();
            }
        }

        private void Editar_ticket_venta_Load(object sender, EventArgs e)
        {
            txt_ticket_anterior.Text = elemento_id.ToString();
            txt_receta.Text = folio_receta.ToString();
            txt_nuevo_ticket.Focus();
        }

        private void txt_nuevo_ticket_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    if (Misc_helper.validar_codigo_venta(txt_nuevo_ticket.Text.Trim()))
                    {
                        string[] codigo = txt_nuevo_ticket.Text.Split('$');

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
                                    validar_venta_id(Convert.ToInt64(codigo[1]));
                                }
                                else
                                {
                                    MessageBox.Show(this, "Este codigo de venta pertenece a otra sucursal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show(this, "Codigo de venta inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Codigo Invalido!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                break;
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            DAO_Antibioticos dao_ab = new DAO_Antibioticos();

            if (txt_nuevo_ticket.Text.Trim().Length > 0)
            {
                string mensaje;
                mensaje = "Está a punto de guardar los datos capturados. ¿Desea continuar?";
                if (MessageBox.Show(this, mensaje, "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if(dao_ab.cambia_ticket_venta(venta_id, elemento_id, movimiento))
                    {
                        MessageBox.Show(this, "Cambios guardados con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(this, "Ocurrió un error al aplicar los cambios, comuníquese a sistemas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    this.Close();
                }
                else
                {
                    txt_nuevo_ticket.Text = "";
                    txt_nuevo_ticket.Focus();
                }
            }
        }
    }
}
