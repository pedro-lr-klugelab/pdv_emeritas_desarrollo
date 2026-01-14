using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.CLASSES;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.tae_diestel_new;
using System.Text.RegularExpressions;
using Farmacontrol_PDV.FORMS.ventas.ventas;
using PXSecurity.Datalogic.Classes;

namespace Farmacontrol_PDV.FORMS.ventas.tae
{
    public partial class Tae_principal : Form
    {
        DTO_Tae_proveedores tae_prov = new DTO_Tae_proveedores();
        string tae_diestel_enc_key = Config_helper.get_config_global("tae_diestel_enc_key");

        public long fabricante_id;
        public string nombre_fabricante;
        public string servicio;
        public long articulo_id;
        public string sku;
        public long monto;

        public long? venta_id;

        public cCampo[] campos;

        Panel panel_contenedor = new Panel();
        Ventas_principal ventas_principal;
        public string numero_referencia;
        public Tae_principal(Ventas_principal form_principal, long? venta_id)
        {
            InitializeComponent();
            this.venta_id = venta_id;
            this.ventas_principal= form_principal;
            //this.Parent = ventas_principal;
        }

        private void Tae_principal_Load(object sender, EventArgs e)
        {
            
        }

        private void Tae_principal_Shown(object sender, EventArgs e)
        {
            show_panel(panel1);
        }

        //Paneles

        public void reset_panel()
        {
            DAO_Tae dao_tae = new DAO_Tae();
            var tmp = dao_tae.get_proveedores_tae();

            if (tmp.Count() > 0)
            {
                dgv_prov_tae.DataSource = tmp;
                dgv_prov_tae.Focus();

            }
            else
            {
                MessageBox.Show(this, "Ocurrio un error al obtener los proveedores de servicio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void set_panel(Panel panel)
        {
            panel_contenedor = panel;
            panel_contenedor.BringToFront();
            panel_contenedor.Refresh();
        }

        public void show_panel(Panel panel)
        {
            set_panel(panel);

            if (panel.Tag.Equals("panel1"))
            {
                reset_panel();
            }
            else if (panel.Tag.Equals("panel2"))
            {
                DAO_Tae dao_serv = new DAO_Tae();
                dgv_servicios_tae.DataSource = dao_serv.get_servicios_prov(tae_prov.fabricante_id);
                dgv_servicios_tae.Columns["dgv_serv_denominacion"].HeaderText = tae_prov.nombre.ToUpper();
                dgv_servicios_tae.Focus();
            }
            else if(panel.Tag.Equals("panel3"))
            {
                lbl_servicio.Text = servicio;
                lbl_proveedor.Text = nombre_fabricante;
                txt_numero.Text = "";
                txt_confirma_numero.Text = "";
                txt_numero.Focus();
            }
            else if (panel.Tag.Equals("panel4"))
            {
                btn_cobrar.Focus();
            }
        }

        //Métodos

        public void crea_respuesta_ws()
        {
            if (campos.Length > 2)
            {
                foreach (cCampo campo in campos)
                {
                    switch (campo.sCampo)
                    {
                        case "CODIGORESPUESTA":
                            MessageBox.Show(this, "Ocurrió un error al procesar el servicio TAE, comuníquese a sistemas.\n" + campo.sValor.ToString(), "Error con el proveedor TAE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "COMISION":
                            txt_comision.Text = campo.sValor.ToString();
                            break;
                        case "REFERENCIA":
                            txt_referencia.Text = PXCryptography.PXDecryptFX(campo.sValor.ToString(), tae_diestel_enc_key);
                            break;
                        case "MONTO":
                            txt_importe.Text = campo.sValor.ToString();
                            ventas_principal.monto = Convert.ToInt64(campo.sValor.ToString());
                            break;
                    }
                }

                DAO_Sucursales dao_sucursales = new DAO_Sucursales();
                DAO_Terminales dao_terminales = new DAO_Terminales();

                int sucursal_id = dao_sucursales.get_sucursal_data(Convert.ToInt32(Config_helper.get_config_local("sucursal_id"))).tae_diestel_tienda_id;
                int terminal_id = Convert.ToInt32(dao_terminales.get_tae_diestel_pos_id((int)Misc_helper.get_terminal_id()));

                DAO_Tae dato_tae = new DAO_Tae();
                int numero_transaccion = dato_tae.get_numero_transaccion(sucursal_id, terminal_id);

                ventas_principal.sku = sku;
                ventas_principal.numero_referencia = txt_numero.Text;
                ventas_principal.es_tae = true;
                ventas_principal.numero_transaccion = numero_transaccion;
            }
            else
            {
                MessageBox.Show(this, "Ocurrió un error al procesar el servicio TAE, comuníquese a sistemas.\n" + campos[0].sValor.ToString() + " : " + campos[1].sValor.ToString(), "Error con el proveedor TAE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        // eventos
        private void txt_numero_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txt_confirma_numero_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void dgv_prov_tae_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgv_prov_tae.Rows.Count > 0)
                {
                    if (dgv_prov_tae.SelectedRows.Count > 0)
                    {
                        DTO_Tae_proveedores dto_prov = new DTO_Tae_proveedores();
                        var row = dgv_prov_tae.SelectedRows[0];

                        dto_prov.fabricante_id = Convert.ToInt32(row.Cells["dgv_prov_fabricante_id"].Value);
                        dto_prov.nombre = row.Cells["dgv_prov_nombre"].Value.ToString();

                        this.fabricante_id = dto_prov.fabricante_id;
                        this.nombre_fabricante = dto_prov.nombre;
                        tae_prov = dto_prov;

                        show_panel(panel2);
                    }
                    else
                    {
                        MessageBox.Show(this, "Debe seleccionar un proveedor de servicio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Ocurrio un error al obtener los proveedores de servicio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void dgv_servicios_tae_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgv_servicios_tae.Rows.Count > 0)
                {
                    if (dgv_servicios_tae.SelectedRows.Count > 0)
                    {
                        var row = dgv_servicios_tae.SelectedRows[0];
                        this.articulo_id = Convert.ToInt64(row.Cells["dgv_serv_articulo_id"].Value);
                        this.servicio = row.Cells["dgv_serv_denominacion"].Value.ToString();
                        this.sku            = row.Cells["dgv_serv_sku"].Value.ToString();
                        show_panel(panel3);
                    }
                    else
                    {
                        MessageBox.Show(this, "Debe seleccionar un servicio TAE", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(this, "No existen servicios para este proveedor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                show_panel(panel1);
            }
        }

        private void txt_numero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_numero.Text.Length == 10)
                {
                    Regex regex = new Regex("^[0-9]+$");

                    Match match = regex.Match(txt_numero.Text);

                    if (match.Success)
                    {
                        txt_confirma_numero.Enabled = true;
                        txt_confirma_numero.Focus();
                        txt_numero.Enabled = false;
                    }
                }
                else
                {
                    txt_numero.Text = "";
                    MessageBox.Show(this, "Número invalido, son necesarios 10 dígitos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (txt_numero.Text.Length > 0)
                {
                    txt_numero.Text = "";
                }
                else
                {
                    show_panel(panel2);
                }                   
            }
        }

        private void txt_confirma_numero_KeyDown(object sender, KeyEventArgs e)
        {
        if (e.KeyCode == Keys.Enter)
            {
                if (txt_confirma_numero.Text.Length == 10)
                {
                    Regex regex = new Regex("^[0-9]+$");

                    Match match = regex.Match(txt_confirma_numero.Text);

                    if (match.Success)
                    {
                        if (txt_numero.Text == txt_confirma_numero.Text)
                        {
                            Login_form login = new Login_form();
                            login.ShowDialog();

                            if (login.empleado_id != null)
                            {

                                this.numero_referencia = txt_numero.Text;

                                Tae_helper tae_helper = new Tae_helper();

                                // se realiza la peticion al ws con el metodo info
                                Cursor = Cursors.WaitCursor;
                                campos = tae_helper.info_tae(login.empleado_id, sku, txt_numero.Text, venta_id);
                                Cursor = Cursors.Default;
                                
                                crea_respuesta_ws();
                                show_panel(panel4);
                            }
                        }
                        else
                        {
                            MessageBox.Show(this, "Los números no coinciden", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_confirma_numero.Text = "";
                            txt_confirma_numero.Focus();
                        }
                    }
                }
                else
                {
                    MessageBox.Show(this, "Número invalido, son necesarios 10 dígitos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (txt_confirma_numero.Text.Length > 0)
                {
                    txt_confirma_numero.Text = "";
                }
                else
                {
                    txt_numero.Enabled = true;
                    txt_numero.Focus();
                    txt_numero.SelectAll();
                    txt_confirma_numero.Enabled = false;
                    
                }
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgv_servicios_tae_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt_numero_TextChanged(object sender, EventArgs e)
        {

        }

        public void inserta_detallado()
        {
            try
            {
                ventas_principal.insertar_detallado_tae(articulo_id);
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.ToString(), "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(this, "¿Está seguro que desea cancelar el proceso?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btn_cobrar_Click(object sender, EventArgs e)
        {
            inserta_detallado();
            ventas_principal.terminar_venta(true);
            ventas_principal.es_tae = true;
            this.Close();
        }
    }
}
