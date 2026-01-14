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
    public partial class Info_generica : Form
    {

        public string movimiento;
        public long elemento_id;
        public bool exito_receta = false;
        public long control_ab_id;
        public int total_ab = 0;
        public List<long?> lista_recetas = new List<long?>();

        public Info_generica(string movimiento = "", long elemento_id = 0, long control_ab_id = 0)
        {
            InitializeComponent();
            this.movimiento = movimiento;
            this.elemento_id = elemento_id;
            this.control_ab_id = control_ab_id;
            llena_combobox();
            lbl_mensaje.Visible = true;
        }

        public void llena_combobox()
        {
            Dictionary<string, string> tipos_mov = new Dictionary<string, string>();
            tipos_mov.Add("VENTA", "VENTA");
            tipos_mov.Add("ENTRADA", "ENTRADA");
            tipos_mov.Add("DEVOLUCION CLIENTE", "DEVOLUCION_CLIENTE");
            tipos_mov.Add("DEVOLUCION MAYORISTA", "DEVOLUCION_MAYORISTA");
            tipos_mov.Add("TRASPASO ENTRANTE", "TRASPASO_ENTRANTE");
            tipos_mov.Add("MERMA", "MERMA");

            cbb_tipos_movimiento.DataSource = new BindingSource(tipos_mov, null);
            cbb_tipos_movimiento.DisplayMember = "Key";
            cbb_tipos_movimiento.ValueMember = "Value";
            
        }

        private void carga_info(string movimiento = "", long elemento_id = 0)
        {
            DAO_Antibioticos ant = new DAO_Antibioticos();
            DTO_info_generica info_generica = ant.get_info_generica(movimiento, elemento_id);
           
            dgv_info_generica.DataSource = ant.get_detallado_generico(movimiento, elemento_id);
            dgv_info_generica.ClearSelection();
            dgv_info_generica.Focus();

            if (dgv_info_generica.Rows.Count > 0)
            {
                //dgv_info_generica.Rows[0].Selected = true;
                //dgv_info_generica.Focus();
                validar_color_grid();
            }
            
            rellena_campos(info_generica);
        }

        private void rellena_campos(DTO_info_generica info_generica)
        {
            txt_fecha.Text = info_generica.fecha;
            txt_folio.Text = info_generica.folio.ToString();
            txt_comentarios.Text = info_generica.comentarios;
        }

        private void buscar()
        {
            this.movimiento = cbb_tipos_movimiento.SelectedValue.ToString();
            this.elemento_id = Convert.ToInt64(txt_busqueda.Text);

            carga_info(movimiento, elemento_id);
        }

        private bool contiene_antibiotico()
        {
            bool es_antibiotico = false;

            foreach (DataGridViewRow row in dgv_info_generica.Rows)
            {
                es_antibiotico = Convert.ToBoolean(dgv_info_generica.Rows[row.Index].Cells["es_antibiotico"].Value);

                if (es_antibiotico)
                {
                    es_antibiotico = true;

                    break;
                }
            }

            return es_antibiotico;
        }

        private bool valida_antibioticos()
        {
            if (contiene_antibiotico())
            {
                string movimiento = cbb_tipos_movimiento.SelectedValue.ToString();
                long elemento_id = Convert.ToInt64(txt_busqueda.Text);
                
                if (movimiento == "VENTA")
                {
                    if (lista_recetas.Count > 0)
                    {
                        return registra_movimiento_venta(movimiento, elemento_id);
                    }
                    else
                    {
                        MessageBox.Show(this, "No hay recetas asignadas a la venta, no se puede guardar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    return registra_movimiento(movimiento, elemento_id);

                }
            }
            return false;
        }

        private bool checa_movimiento(string movimiento, long elemento_id)
        {
            DAO_Antibioticos dao_ant = new DAO_Antibioticos();
            return dao_ant.checa_movimiento(movimiento, elemento_id);
        }

        private bool registra_movimiento_venta(string movimiento, long elemento_id)
        {
            int cheked_ab = 0;
            if (lista_recetas.Count > 0)
            {
                DAO_Antibioticos dao_ant = new DAO_Antibioticos();
                DTO_info_generica info_gen = dao_ant.get_info_generica(movimiento, elemento_id);

                List<DTO_detallado_generico> list_dto = new List<DTO_detallado_generico>();

                foreach (DataGridViewRow art in dgv_info_generica.Rows)
                {
                    if (Convert.ToBoolean(art.Cells["es_antibiotico"].Value) == true)
                    {
                        DTO_detallado_generico articulo = new DTO_detallado_generico();
                        articulo.articulo_id = Convert.ToInt64(art.Cells["articulo_id"].Value);
                        articulo.movimiento = art.Cells["dgv_movimiento"].Value.ToString();
                        articulo.receta_id = Convert.ToInt64(art.Cells["receta_id"].Value);
                        articulo.elemento_id = Convert.ToInt64(art.Cells["dgv_elemento_id"].Value);
                        list_dto.Add(articulo);
                        cheked_ab++;
                    }
                }

                if (total_ab == cheked_ab)
                {
                    if (info_gen.folio > 0)
                    {
                        return dao_ant.registra_movimiento_venta(movimiento, elemento_id, list_dto);
                    }
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        private bool registra_movimiento(string movimiento, long elemento_id)
        {
            DAO_Antibioticos dao_ant = new DAO_Antibioticos();
            DTO_info_generica info_gen = dao_ant.get_info_generica(movimiento, elemento_id);

            if (info_gen.folio > 0)
            {
                return dao_ant.registra_movimiento(movimiento, elemento_id);
            }

            return false;
        }

        private void validar_color_grid()
        {
            if(movimiento == "VENTA")
            {
                dgv_info_generica.Columns["check"].Visible = true;
                activa_botones();
            }
            else
            {
                dgv_info_generica.Columns["check"].Visible = false;
                desactiva_botones();
            }

            foreach (DataGridViewRow row in dgv_info_generica.Rows)
            {
                if (Convert.ToBoolean(row.Cells["es_antibiotico"].Value))
                {
                    dgv_info_generica.Rows[row.Index].DefaultCellStyle.BackColor = Color.FromArgb(179, 206, 252);
                    total_ab++;
                }
                else
                {
                    row.ReadOnly = true;
                    dgv_info_generica.Rows[row.Index].DefaultCellStyle.BackColor = Color.Empty;
                }
            }
        }

        public void activa_botones()
        {
            btn_receta.Visible = true;
            btn_receta.Enabled = true;
            btn_anadir_receta.Visible = true;
            btn_anadir_receta.Enabled = true;
            btn_desasociar_receta.Visible = true;
            btn_desasociar_receta.Enabled = true;
        }

        public void desactiva_botones()
        {
            btn_receta.Visible = false;
            btn_receta.Enabled = false;
            btn_anadir_receta.Visible = false;
            btn_anadir_receta.Enabled = false;
            btn_desasociar_receta.Visible = false;
            btn_desasociar_receta.Enabled = false;
        }
    
        private void valida_folio()
        {
            if (Misc_helper.validar_codigo_venta(txt_busqueda.Text.Trim()))
            {
                string[] codigo = txt_busqueda.Text.Split('$');

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
                            txt_busqueda.Text = codigo[1];
                            buscar();
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
                else
                {
                    
                    DAO_Ventas dao_ventas = new DAO_Ventas();
                    long sucursal_id;
                    sucursal_id = Convert.ToInt64(Config_helper.get_config_local("sucursal_id"));
                    txt_busqueda.Text = dao_ventas.get_venta_id_por_venta_folio(Convert.ToInt64(txt_busqueda.Text.Trim())).ToString();
                    buscar();
                    
                   // MessageBox.Show(this, "No se puede leer el código de barras, formato erróneo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(this, "Codigo Invalido!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btn_buscar_Click(object sender, EventArgs e)
        {
            string movimiento = cbb_tipos_movimiento.SelectedValue.ToString();
            if (movimiento == "VENTA")
            {
                valida_folio();
            }
            else
            {
                buscar();
            }
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            bool tmp = false;
            if(lista_recetas.Count>0)
            {
                DAO_Antibioticos dao_ab = new DAO_Antibioticos();
                tmp = dao_ab.limpia_recetas(lista_recetas);
            }
            this.Close();
        }

        private void cbb_tipos_movimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_tipos_movimiento.SelectedValue.ToString().Equals("VENTA"))
            {
                lbl_mensaje.Visible = true;
                if (dgv_info_generica.Rows.Count > 0)
                {
                    btn_receta.Visible = true;
                    btn_receta.Enabled = true;
                    activa_botones();
                }
            }
            else
            {
                btn_receta.Visible = false;
                btn_receta.Enabled = false;
                lbl_mensaje.Visible = false;
                desactiva_botones();
            }
            if (txt_busqueda.Text.Trim().Length > 0)
            {
                buscar();
                
            }
            txt_busqueda.Focus();
        }

        private void txt_busqueda_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    string movimiento = cbb_tipos_movimiento.SelectedValue.ToString();
                    if (movimiento == "VENTA")
                    {
                        valida_folio();
                    }
                    else
                    {
                        buscar();
                    }
                    
                break;
            }
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            if(txt_busqueda.Text.Trim().Length > 0)
            {
                string movimiento = cbb_tipos_movimiento.SelectedValue.ToString();
                long folio = Convert.ToInt64(txt_busqueda.Text);

                if (!checa_movimiento(movimiento, folio))
                {
                    if (contiene_antibiotico())
                    {
                        if (valida_antibioticos())
                        {
                            MessageBox.Show(this, "Registro exitoso", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(this, "Ocurrio un error al registrar el movimiento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "No existen antibioticos en este movimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Este movimiento ya ha sido registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txt_busqueda_TextChanged(object sender, EventArgs e)
        {

        }

        private void Info_generica_Shown(object sender, EventArgs e)
        {
            if (movimiento.Equals(""))
            {
                cbb_tipos_movimiento.Enabled = true;
                lbl_folio.Visible = true;
                txt_busqueda.Enabled = true;
                txt_busqueda.Visible = true;
                btn_buscar.Enabled = true;
                btn_buscar.Visible = true;
                btn_guardar.Enabled = true;
                btn_guardar.Visible = true;
                txt_busqueda.Focus();
            }
            else
            {
                cbb_tipos_movimiento.SelectedValue = movimiento.Replace(" ", "_");
                carga_info(movimiento, elemento_id);
                if(movimiento.Equals("VENTA"))
                {
                    //lbl_mensaje.Visible = true;
                    activa_botones();
                }
            }
        }

        private void btn_receta_Click(object sender, EventArgs e)
        {
            if (dgv_info_generica.SelectedRows.Count > 0)
            {
                DAO_Antibioticos dao_ant = new DAO_Antibioticos();
                long control_ab_receta_id = dao_ant.get_control_receta_id(control_ab_id);

                if (control_ab_receta_id > 0)
                {
                    Recetas_new recetas = new Recetas_new(control_ab_receta_id);
                    recetas.ShowDialog();
                }
            }
        }

        private void Info_generica_Load(object sender, EventArgs e)
        {

        }

        public List<DTO_detallado_generico> get_articulos_seleccionados()
        {
            List<DTO_detallado_generico> articulos = new List<DTO_detallado_generico>();
            foreach(DataGridViewRow art in dgv_info_generica.Rows)
            {
                if(Convert.ToBoolean(art.Cells["check"].Value) && Convert.ToInt64(art.Cells["receta_id"].Value) < 1)
                {
                    DTO_detallado_generico articulo = new DTO_detallado_generico();

                    articulo.movimiento = art.Cells["dgv_movimiento"].Value.ToString();
                    articulo.articulo_id = Convert.ToInt64(art.Cells["articulo_id"].Value);
                    articulo.elemento_id = Convert.ToInt64(art.Cells["dgv_elemento_id"].Value);
                    articulo.producto = art.Cells["producto"].Value.ToString();
                    articulo.caducidad = Misc_helper.fecha(art.Cells["producto"].Value.ToString(), "CADUCIDAD");
                    articulo.cantidad = Convert.ToInt64(art.Cells["cantidad"].Value);
                    articulo.lote = art.Cells["lote"].Value.ToString();
                    articulo.receta_id = Convert.ToInt64(art.Cells["receta_id"].Value);

                    articulos.Add(articulo);
                }
            }
            return articulos;
        }

        public void asigna_receta_articulos(long? receta_id)
        {
            foreach(DataGridViewRow art in dgv_info_generica.Rows)
            {
                if (Convert.ToBoolean(art.Cells["check"].Value) && Convert.ToInt64(art.Cells["receta_id"].Value) < 1)
                {
                    art.Cells["receta_id"].Value = receta_id;
                    art.ReadOnly = true;
                }
            }
        }

        private void btn_anadir_receta_Click(object sender, EventArgs e)
        {
            List<DTO_detallado_generico> lista_articulos = new List<DTO_detallado_generico>();
            lista_articulos = get_articulos_seleccionados();

            if(txt_busqueda.Text.Trim().Length > 0)
            {
                long? receta_id;

                if (lista_articulos.Count > 0)
                {
                    Recetas_new recetas = new Recetas_new(lista_articulos);
                    recetas.ShowDialog();
                    receta_id = recetas.control_ab_receta_id;
                    if (receta_id > 0 || receta_id != null)
                    {
                        asigna_receta_articulos(receta_id);
                        lista_recetas.Add(receta_id);
                        marca_productos_con_receta();
                    }
                }
                else
                {
                    MessageBox.Show(this, "Es necesario seleccionar los productos que contendrá la receta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }           
        }

        /*private bool desasociar_receta()
        {
            
            List<DTO_detallado_generico> articulos = new List<DTO_detallado_generico>();

            DAO_Antibioticos dao_ant = new DAO_Antibioticos();

            foreach(DataGridViewRow art in dgv_info_generica.Rows)
            {
                if(Convert.ToBoolean(art.Cells["check"].Value))
                {
                    dao_ant.desasociar_receta()
                }
        }*/

        private bool reiniciar_proceso()
        {
            bool ok = false;
            DAO_Antibioticos ant = new DAO_Antibioticos();

            if(lista_recetas.Count > 0)
            {
                foreach (DataGridViewRow art in dgv_info_generica.Rows)
                {
                    art.Cells["check"].Value = false;
                    art.Cells["receta_id"].Value = null;
                    art.ReadOnly = false;
                }

                ok = ant.limpia_recetas(lista_recetas);

                lista_recetas.RemoveAll(item => item > 0);
            }
            else
            {
                ok = true;
            }

            return ok;
        }

        private void btn_desasociar_receta_Click(object sender, EventArgs e)
        {
            if (lista_recetas.Count > 0)
            {
                if (reiniciar_proceso())
                {
                    MessageBox.Show(this, "Se han desasociado la(s) receta(s) al ticket de venta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "Ocurrió un error al desasociar la(s) receta(s), comuníquese a sistemas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgv_info_generica_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        } 
 
        private void marca_productos_con_receta()
        {
            foreach (DataGridViewRow row in dgv_info_generica.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value) == true)
                {
                    dgv_info_generica.Rows[row.Index].DefaultCellStyle.BackColor = Color.FromArgb(248, 219, 219);
                }
            }
        }
     }
}
