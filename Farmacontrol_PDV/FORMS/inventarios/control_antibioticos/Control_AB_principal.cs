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
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.CLASSES;
using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.FORMS.inventarios.control_antibioticos
{
    public partial class Control_AB_principal : Form
    {
        DAO_Articulos dao_articulos = new DAO_Articulos();
        DTO_Articulo articulo_registro = new DTO_Articulo();
        long? articulo_id_encontrado = null;

        public Control_AB_principal()
        {
            InitializeComponent();
        }

        public void busqueda_producto()
        {
            articulo_registro = dao_articulos.get_articulo(txt_amecop.Text);
            this.articulo_id_encontrado = articulo_registro.Articulo_id;
            if (articulo_registro.Articulo_id != null)
            {
                if (articulo_registro.clase_antibiotico_id.Equals(null))
                {
                    txt_amecop.Text = "";
                    MessageBox.Show(this, "El producto seleccionado no es un antibiótico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    txt_producto.Text = articulo_registro.Nombre;
                    //AGREGA LA SUSTANCIA DEL MEDICAMENTO
                    buscar((int)articulo_registro.Articulo_id);
                    DAO_Antibioticos ant = new DAO_Antibioticos();

                    lblSustancia.Text = ant.get_tipo_antibiotico((long)articulo_registro.clase_antibiotico_id);

                    


                }
            }
            else
            {
                txt_amecop.Text = "";
                txt_producto.Text = "";
                if (dgv_control_AB.Rows.Count > 0)
                {
                    List<DTO_control_antibiotico> lista_antibioticos = new List<DTO_control_antibiotico>();

                    dgv_control_AB.DataSource = lista_antibioticos;
                }
                
                MessageBox.Show(this, "Producto No encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_amecop.Focus();
            }
        }

        private void txt_amecop_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    if (txt_amecop.Text.Trim().Length > 0)
                    {
                        busqueda_producto();
                    }
                break;
                /*
                case 114:
                    form_busqueda_producto();
                break;

                case 116:
                    muestra_ventas_sin_receta();
                break;
                 */
            }
        }

        /*        private void muestra_ventas_sin_receta()
        {

            articulos_pendientes datos_pendientes = new articulos_pendientes();
            datos_pendientes.ShowDialog();
            
            if (datos_pendientes.dto_tmp.articulo_id > 0)
            {
                DAO_Antibioticos dao_control_ab = new DAO_Antibioticos();
                dgv_control_AB.DataSource = dao_control_ab.get_control_antibioticos_por_amecop(datos_pendientes.dto_tmp.amecop);
                DTO_Articulo articulo = dao_articulos.get_articulo(datos_pendientes.dto_tmp.amecop);
                txt_amecop.Text = articulo.Amecop;
                txt_producto.Text = articulo.Nombre;
                dgv_control_AB.ClearSelection();
                //muestra_ab_por_ajustar();
                dgv_control_AB.Focus();
                foco_grid();
            }
            else
            {
                txt_amecop.Focus();
            }
        }
        */

        private void buscar(long articulo_id)
        {
            Cursor = Cursors.WaitCursor;
            DAO_Antibioticos ant = new DAO_Antibioticos();
            dgv_control_AB.DataSource = ant.get_control_antibioticos_por_amecop(txt_amecop.Text);
            lbl_tiempo.Text = ant.get_existencias_control_antibioticos(txt_amecop.Text).ToString();
            txt_existencia.Text = ant.get_tiempo_existencias(articulo_id).ToString();
            //REVISAR EL TOTAL DE DIFERENCIA QUE ARROJA EL SISTEMA
            lblExistenciaInicial.Text = ant.total_movimiento_existencia_inicial(txt_amecop.Text);
            lblFechaEntradaInicial.Text = "2013-02-15";

            Cursor = Cursors.Default;

            dgv_control_AB.ClearSelection();
            dgv_control_AB.Focus();
            if (dgv_control_AB.Rows.Count > 0)
            {
                dgv_control_AB.CurrentCell = dgv_control_AB.Rows[0].Cells[2];  
            }
                   
        }

        void form_busqueda_producto()
        {
            string amecop = "";

            if (txt_amecop.Text.Trim().Length > 0)
            {
                if (txt_amecop.Text.Substring(0, 1).Equals("?"))
                {
                    amecop = txt_amecop.Text.Substring(1, txt_amecop.Text.Length - 1).Trim();
                }
            }

            Busqueda_productos busqueda_productos = new Busqueda_productos(amecop);
            busqueda_productos.FormClosed += new FormClosedEventHandler(busqueda_productos_Close);
            busqueda_productos.ShowDialog();
            txt_amecop.Focus();
        }

        public void busqueda_productos_Close(object sender, FormClosedEventArgs e)
        {
            if (Busqueda_productos.articulo_articulo_id != null)
            {
                txt_amecop.Text = Busqueda_productos.articulo_amecop;
 
                txt_producto.Text = Busqueda_productos.articulo_producto;
                dgv_control_AB.Focus();
            }
        }

        void limpiar_busqueda()
        {
            txt_amecop.Text = "";
            txt_producto.Text = "";
        }

        private void Control_AB_principal_Load(object sender, EventArgs e)
        {
            
        }

        private void dgv_control_AB_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 45:
                    if (e.Control)
                    {
                        if (dgv_control_AB.Rows.Count < 1)
                        {
                            MessageBox.Show(this, "Para poder agregar datos de doctor, cédula y receta; seleccione un articulo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            DTO_Control_AB_receta dto_receta = new DTO_Control_AB_receta();

                            DataGridViewRow r = dgv_control_AB.SelectedRows[0];

                            dto_receta.control_antibiotico_id = (long)r.Cells["control_antibiotico_id"].Value;
                            dto_receta.articulo_id            = (long)r.Cells["articulo_id"].Value;
                            dto_receta.tipo_movimiento        = (string)r.Cells["tipo_movimiento"].Value;
                            dto_receta.doctor                 = (string)r.Cells["doctor"].Value;
                            dto_receta.cedula                 = (string)r.Cells["cedula"].Value;
                            dto_receta.receta                 = (string)r.Cells["receta"].Value;
                            
                           if (dto_receta.tipo_movimiento.Equals("VENTA"))
                           {
                               Datos_receta datos_receta = new Datos_receta(dto_receta);
                               datos_receta.ShowDialog();

                               if (!datos_receta.dto_tmp.Equals(null))
                               {
                                   r.Cells["doctor"].Value = datos_receta.dto_tmp.doctor;
                                   r.Cells["cedula"].Value = datos_receta.dto_tmp.cedula;
                                   r.Cells["receta"].Value = datos_receta.dto_tmp.receta;
                               }
                           } 
                        }
                    }
                break;
                case 65:
                if (e.Control)
                {
                    if (dgv_control_AB.Rows.Count < 1)
                    {
                        MessageBox.Show(this, "Para poder registrar la venta, por favor seleccione un articulo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DTO_control_antibiotico dto_ab = new DTO_control_antibiotico();
                        DataGridViewRow r = dgv_control_AB.SelectedRows[0];

                        dto_ab.control_antibiotico_id         = (long)r.Cells["control_antibiotico_id"].Value;
                        dto_ab.amecop                         = (string)r.Cells["amecop"].Value;
                        dto_ab.fecha                          = (string)r.Cells["fecha"].Value;
                        dto_ab.articulo_id                    = (long)r.Cells["articulo_id"].Value;
                        dto_ab.movimiento                     = (string)r.Cells["movimiento"].Value;
                        dto_ab.cantidad                       = (long)r.Cells["cantidad"].Value;
                        dto_ab.lote                           = (string)r.Cells["lote"].Value;
                        dto_ab.caducidad                      = (string)r.Cells["caducidad"].Value;
                        dto_ab.elemento_id                    = Convert.ToInt64(r.Cells["elemento_id"].Value);
                        dto_ab.control_antibioticos_receta_id = (long)r.Cells["control_antibioticos_receta_id"].Value;

                        if (dto_ab.movimiento.Equals("VENTA"))
                        {
                           
                            string mensaje;
                            mensaje = "Está a punto de ajustar el registro seleccionado. ¿Desea continuar?";
                            if (MessageBox.Show(this, mensaje, "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                Cursor = Cursors.WaitCursor;
                                DAO_Antibioticos ant = new DAO_Antibioticos();

                                /*if (dto_ab.por_ajustar > 0)
                                {
                                    mensaje = "El registro está marcado como ajuste manual, ¿Desea realizar la operación?";
                                    if (MessageBox.Show(this, mensaje, "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                    {
                                        if (ant.borrado_manual(dto_ab.control_antibiotico_id, dto_ab.movimiento, dto_ab.articulo_id))
                                        {
                                            MessageBox.Show(this, "El registro se ha ajustado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            dgv_control_AB.DataSource = ant.get_control_antibioticos_por_amecop(txt_amecop.Text);
                                            dgv_control_AB.ClearSelection();
                                            muestra_ab_por_ajustar();
                                            Cursor = Cursors.Default;
                                        }
                                    }
                                    else
                                    {
                                        Cursor = Cursors.Default;
                                    }
                                }
                                else
                                {
                                    string mensajes = "";
                                    string tmp = ant.ajuste_de_antibioticos(dto_ab.control_antibiotico_id, dto_ab.articulo_id, dto_ab.movimiento, dto_ab.cantidad);
                                    if (tmp == "eliminar")
                                    {
                                        mensajes = "Actualización realizada con éxito";
                                        MessageBox.Show(this, mensajes, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        dgv_control_AB.DataSource = ant.get_control_antibioticos_por_amecop(txt_amecop.Text);
                                        dgv_control_AB.ClearSelection();
                                        //muestra_ab_por_ajustar();
                                        Cursor = Cursors.Default;
                                    }
                                    else if (tmp == "actualizar")
                                    {
                                        mensajes = "Se marcó el registro que se necesita ajustar manualmente";
                                        MessageBox.Show(this, mensajes, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        r.DefaultCellStyle.BackColor = Color.FromArgb(255, 218, 218);
                                        //r.Cells["por_ajustar"].Value = 1;
                                        Cursor = Cursors.Default;
                                    }
                                    else
                                    {
                                        Cursor = Cursors.Default;
                                        MessageBox.Show(this, "Ocurrio un error al actualizar los datos, comuníquese a sistemas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    Cursor = Cursors.Default;
                                //}
                            }
                            else
                            {
                                Cursor = Cursors.Default;
                            }
                        }
                    }
                }
                break;
                case 27:
                    txt_amecop.Focus();
                    txt_amecop.SelectAll();
                break;
            }
             * */
        }

        public void muestra_ab_por_ajustar()
        {
            foreach (DataGridViewRow row in dgv_control_AB.Rows)
            {
                int activo = Convert.ToInt32(dgv_control_AB.Rows[row.Index].Cells["por_ajustar"].Value);

                if (activo == 1)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 218, 218);
                }
            }
        }

        private void txt_amecop_TextChanged(object sender, EventArgs e)
        {

        }

        private void Control_AB_principal_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);
/*
            switch (keycode)
            {
                
                case 114:
                    form_busqueda_producto();
                break;
                case 116:
                     muestra_ventas_sin_receta();
                break;
                
            }*/
        }

        private void dgv_control_AB_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string movimiento;
            long elemento_id;
            long control_ab_id;

            if (dgv_control_AB.Rows.Count > 0)
            {
                movimiento = dgv_control_AB.Rows[dgv_control_AB.CurrentRow.Index].Cells["movimiento"].Value.ToString();
                elemento_id = (long)dgv_control_AB.Rows[dgv_control_AB.CurrentRow.Index].Cells["elemento_id"].Value;
                control_ab_id = (long)dgv_control_AB.Rows[dgv_control_AB.CurrentRow.Index].Cells["control_antibiotico_id"].Value;

                Info_generica info = new Info_generica(movimiento, elemento_id, control_ab_id);
                info.ShowDialog();
            }
        }

        private void btn_reg_mov_Click(object sender, EventArgs e)
        {
            Info_generica info = new Info_generica("", 0);
            info.Text = "Registrar Movimiento";
            info.ShowDialog();
            txt_amecop.Focus();
            if (articulo_id_encontrado != null)
            {
                buscar((long)articulo_id_encontrado);
            }
        }

        private void Control_AB_principal_Shown(object sender, EventArgs e)
        {
            txt_amecop.Focus();
        }

        private void txt_amecop_Click(object sender, EventArgs e)
        {
            if (txt_amecop.Text.Trim().Length > 0)
            {
                txt_amecop.Text = "";
            }
        }

        private void txt_amecop_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btn_reporte_Click(object sender, EventArgs e)
        {
            Reporte_Movimientos reporte = new Reporte_Movimientos();
            reporte.ShowDialog();
        }

        private void dgv_control_AB_Click(object sender, EventArgs e)
        {

        }
    }
}
