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
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.CLASSES;

namespace Farmacontrol_PDV.FORMS.cortes.tipos_pagos
{
    public partial class tipos_pagos_original : Form
    {

        long venta_id = 0;
        long venta_pago_id = 0;

        public tipos_pagos_original()
        {
            InitializeComponent();
        }

        void validar_codigo_entrada()
        {
            if (Misc_helper.validar_codigo_venta(txtTicket.Text.Trim()))
            {
                string[] split_folio = txtTicket.Text.Trim().Split('$');
                venta_id = Convert.ToInt64(split_folio[1]);

                if (Misc_helper.es_numero(split_folio[0]) && Misc_helper.es_numero(split_folio[1]))
                {
                   long sucursal_id = Convert.ToInt64(split_folio[0]);

                    DAO_Ventas dao_ventas = new DAO_Ventas();
                    DAO_Sucursales dao_sucursales = new DAO_Sucursales();

                    long sucursal_local = Convert.ToInt64(Config_helper.get_config_local("sucursal_id"));

                    if (sucursal_local.Equals(sucursal_id))
                    {
                        if (dao_ventas.existe_venta(venta_id))
                        {

                            DAO_Pago_tipos pagos_tipos = new DAO_Pago_tipos();

                              
                              DataTable dtpagos = pagos_tipos.get_pago_venta(venta_id);

                              if (dtpagos.Rows.Count > 0)
                              {
                                  bntAceptarCambio.Visible = true;
                                  foreach (DataRow dr in dtpagos.Rows)
                                  {
                                      lblFormasPago.Text = dr["pagos"].ToString();
                                      venta_pago_id = Convert.ToInt64(dr["venta_pago_id"]);
                                  }

                                  //LLENANDO EL COMBO DE DATOS DISPONIBLE

                                  DataTable dt = pagos_tipos.get_pago_tipos_disponible(venta_id);

                                  cbxPagosDisp.Enabled = true;
                                  cbxPagosDisp.Items.Clear();
                                  foreach (DataRow row in dt.Rows)
                                  {
                                      ComboBoxItem item = new ComboBoxItem();
                                      item.Text = row["nombre"].ToString();
                                      item.Value = row["pago_tipo_id"];
                                      item.elemento_id = Convert.ToInt32(row["pago_tipo_id"]);
                                      cbxPagosDisp.Items.Add(item);
                                  }

                                  cbxPagosDisp.DroppedDown = true;
                                  cbxPagosDisp.SelectedIndex = 0;

                                  cbxPagosDisp.Focus();
                              }
                              else
                              {
                                  MessageBox.Show(this, "El tipo de pago no puede ser cambiado, debido a que esta dentro del corte  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                              
                              }

                        }
                        else
                        {
                            MessageBox.Show(this, "Venta no registrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtTicket.Text = "";
                            txtTicket.Focus();
                        }
                    }
                    else
                    {
                        var dto_sucursal = dao_sucursales.get_sucursal_data((int)sucursal_id);

                        if (dto_sucursal.sucursal_id > 0)
                        {
                            MessageBox.Show(this, string.Format("Esta venta pertenece a la sucursal {0}", dto_sucursal.nombre));
                            txtTicket.Text = "";
                            txtTicket.Focus();
                        }
                        else
                        {
                            MessageBox.Show(this, "Codigo inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtTicket.Text = "";
                            txtTicket.Focus();
                        }
                    }
                }
            }
            else
            {
                
                MessageBox.Show(this, "Ticket de venta invalido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                limpiar_informacion();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTicket_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    if (txtTicket.Text.Trim().Length > 0)
                    {
                        validar_codigo_entrada();
                    }
                    break;
            }
        }

        private void bntAceptarCambio_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show(this,"¿Esta seguro de querer cambiar el tipo de pago?", "Información",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

            if (dr == DialogResult.Yes)
            {

                string tipo_pago = cbxPagosDisp.SelectedItem.ToString();
               
                DAO_Pago_tipos pagos_tipos = new DAO_Pago_tipos();
                
                bool afectacion = pagos_tipos.set_nuevo_pago(venta_pago_id, tipo_pago, venta_id);

                if (afectacion)
                {
                    MessageBox.Show(this, "Forma de pago actualizado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.limpiar_informacion();
                }
                else
                {
                    MessageBox.Show(this, "Ocurrio un error al intentar cambiar la forma de pago del ticket, notifique a su adminstrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        void limpiar_informacion()
        { 
            txtTicket.Text = "";
            lblFormasPago.Text = "********";
            cbxPagosDisp.Items.Clear();
            venta_id = 0;
            venta_pago_id = 0;
            txtTicket.Focus();
            bntAceptarCambio.Visible = false;
        }



    }
}
