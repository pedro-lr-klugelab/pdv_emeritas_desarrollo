using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.FORMS.ventas.ventas;
using Farmacontrol_PDV.HELPERS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farmacontrol_PDV.FORMS.ventas.cancelar_prepago
{
    public partial class Cancelar_prepago_principal : Form
    {
        public Cancelar_prepago_principal()
        {
            InitializeComponent();
        }

        private void ttx_codigo_prepago_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    //cancelar_prepago();
                    break;
                case 27:
                    if (txt_codigo_prepago.Text.Trim().Length > 0)
                    {
                        txt_codigo_prepago.Text = "";
                    }
                    else
                    {
                        this.Close();
                    }
                    break;
            }
        }
        /*
        void cancelar_prepago()
        {
            try
            {
                string[] ingreso_prepago = txt_codigo_prepago.Text.Trim().Split('$');

                if (ingreso_prepago.Length > 0 && ingreso_prepago.Length == 2)
                {
                    long sucursal_id = 0;
                    bool conversion_sucursal = long.TryParse(ingreso_prepago[0], out sucursal_id);

                    if (conversion_sucursal)
                    {
                        if (Convert.ToInt64(Config_helper.get_config_local("sucursal_id")) == sucursal_id)
                        {
                            DAO_Prepago dao_prepago = new DAO_Prepago();
                            DTO_Prepago prepago = new DTO_Prepago();

                            prepago = dao_prepago.get_informacion_prepago(ingreso_prepago[1]);

                            if (prepago.prepago_id > 0)
                            {
                                if (prepago.fecha_canje == null)
                                {
                                    if (prepago.fecha_cancelado == null)
                                    {
                                        
                                    }
                                    else
                                    {
                                        DialogResult dr = MessageBox.Show(
                                            this,
                                            string.Format("Este encargo ha sido cancelado por {0} el {1}, se entregara al cliente {2} {3} equivalente a {4}, ¿desea proceder con la devolucion? ", prepago.nombre_empleado_cancela.ToUpper(), Convert.ToDateTime(prepago.fecha_cancelado).ToString("dd-MMM-yyy h:mm:ss tt").ToUpper().Replace(".", ""), (prepago.tipo_devolucion.Equals("VALE")) ? "un" : "el", prepago.tipo_devolucion, prepago.monto.ToString("C2")),
                                            "Prepago Cancelado",
                                            MessageBoxButtons.YesNoCancel,
                                            MessageBoxIcon.Question,
                                            MessageBoxDefaultButton.Button2
                                        );

                                        if (dr == DialogResult.Yes)
                                        {
                                            var detallado_entrega_parcial = dao_prepago.get_productos_entrega_parcial(prepago.prepago_id);

                                            if (detallado_entrega_parcial.Count > 0)
                                            {
                                                Cancelar_prepago cancelacion_prepago = new Cancelar_prepago(prepago.prepago_id, detallado_entrega_parcial);
                                                cancelacion_prepago.ShowDialog();

                                                if (cancelacion_prepago.prepago_afectado)
                                                {
                                                    prepago_afectado = cancelacion_prepago.prepago_afectado;
                                                }
                                            }
                                            else
                                            {
                                                DialogResult dr2 = MessageBox.Show(this, "Al iniciar el proceso de devolución de prepago no podra cancelar ni modificar nada, ¿Desea continuar?", "Devolución prepago", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                                                if (dr2 == DialogResult.Yes)
                                                {
                                                    if (dao_prepago.cancelar_prepago(prepago_id, detallado_entrega_parcial))
                                                    {
                                                        MessageBox.Show(this, "Prepago cancelado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        prepago_afectado = true;
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show(this, "Ocurrio un error al intentar cancelar el prepago, notifique a su adminstrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    }
                                                }
                                            }
                                        }

                                        if (prepago_afectado)
                                        {
                                            DAO_Vales_efectivo dao_vales = new DAO_Vales_efectivo();
                                            decimal total_devolucion = dao_vales.get_total_devolucion_prepago(prepago.prepago_id);

                                            if (prepago.tipo_devolucion.Equals("VALE"))
                                            {
                                                if (total_devolucion > 0)
                                                {
                                                    string vale_id = dao_vales.generar_vale_efectivo_prepago(prepago.prepago_id);
                                                    Vale_efectivo ticket_vale = new Vale_efectivo();
                                                    ticket_vale.construccion_ticket(vale_id, true);
                                                    ticket_vale.print();
                                                }
                                            }
                                            else
                                            {
                                                if (total_devolucion > 0)
                                                {
                                                    Recibo_efectivo_prepago ticket_efectivo = new Recibo_efectivo_prepago();
                                                    ticket_efectivo.construccion_ticket(prepago.prepago_id);
                                                    ticket_efectivo.print();
                                                }
                                            }

                                            this.Close();
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(this, "Este prepago ya fue canjeado, retenga el prepago y reportelo a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show(this, "Codigo de prepago no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            DAO_Sucursales dao_sucursales = new DAO_Sucursales();
                            var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(sucursal_id));

                            if (sucursal_data.sucursal_id > 0)
                            {
                                MessageBox.Show(this, string.Format("Este prepago solo es canjeable en la sucursal *{0}*", sucursal_data.nombre.ToUpper()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show(this, "Código Inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Código Inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Código Inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }*/
    }
}
