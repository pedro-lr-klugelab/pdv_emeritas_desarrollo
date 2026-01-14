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
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.FORMS.ventas.bonificaciones
{
    public partial class bonificacion_principal : Form
    {
        DTO_Articulo dto_articulo = new DTO_Articulo();
        DAO_Articulos dao_articulos = new DAO_Articulos();
        Dao_Bonificaciones dao_bonificaciones = new Dao_Bonificaciones();

        Int32 existencia_total = 0;
        public bonificacion_principal()
        {
            InitializeComponent();
            txtbxTicket.Focus();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bonificacion_principal_Load(object sender, EventArgs e)
        {
            txtbxTicket.Focus();
        }
        private void bonificacion_principal_Activated(object sender, EventArgs e)
        {
            txtbxTicket.Focus();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
     
        #region validadores
        public void solo_numeros(KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.solo_numeros(e);
        }

        private void txtTransaccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.solo_numeros(e);
        }
        private void txtTransaccion_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        private void txtamecop_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
  
                case 40:
                    /*
                    if (dgv_mermas.Rows.Count > 0)
                    {
                        dgv_mermas.CurrentCell = dgv_mermas.Rows[0].Cells["c_amecop"];
                        dgv_mermas.Rows[0].Selected = true;
                        dgv_mermas.Focus();
                    }*/
                    break;
                case 13:
                    if (txt_amecop.TextLength > 0)
                    {
                       busqueda_producto();
                    }
                    break;
                case 27:
                    txt_amecop.Text = "";
                    existencia_total = 0;
                break;
                case 114:
                  //  form_busqueda_producto();
                  
                    break;
            }
        }

        public void limpiar_bonificacion()
        {
            txt_amecop.Text = "";
            txtbxTicket.Text = "";
            txttarjeta.Text = "";
            txtbxTicket.Focus();
            txtDescripcion.Text = "";
            txtTransaccion.Text = "";
            existencia_total = 0;

        
        }

        private void txtbxTicket_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 76:
                    if (e.Control)
                    {
                        DialogResult dr = MessageBox.Show(this, "Esta a punto de borrar permanentemente toda la informacion capturada de la merma, ¿desea continuar?", "Limpiar bonificacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dr == DialogResult.Yes)
                        {
                            limpiar_bonificacion();

                        }
                    }
                    break;
                case 13:
                    
                if (valida_ticket())
                {
                    DialogResult result1 = MessageBox.Show("Ticket valido, deseas continuar? ", "Aviso",MessageBoxButtons.YesNo);

                    if (result1 == DialogResult.Yes)
                    {
                        txttarjeta.Focus();
                    }
                    else
                    {
                        txtbxTicket.Focus();
                    }
                                        

                }
                else
                {
                    MessageBox.Show(this, "Codigo inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtbxTicket.Focus();
                }
                            
                   
                 break;

            }


        }

        public void busqueda_producto()
        {
            if (txt_amecop.Text.Substring(0, 1).Equals("?"))
            {
                form_busqueda_producto();
            }
            else
            {
                dto_articulo = dao_articulos.get_articulo(txt_amecop.Text);
                
                if (dto_articulo.Articulo_id != null)
                {
                    rellenar_informacion_producto();
                }
                else
                {
                    txt_amecop.Text = "";
                    existencia_total = 0;
                    MessageBox.Show(this, "Producto no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
        }

        public void rellenar_informacion_producto()
        {
            txt_amecop.Enabled = false;
            txtDescripcion.Text = dto_articulo.Nombre;
            existencia_total = 0;
            
            if (dto_articulo.Caducidades.Rows.Count > 0)
            {
               
                Dictionary<string, string> dic_cad = new Dictionary<string, string>();
               
                foreach (DataRow row in dto_articulo.Caducidades.Rows)
                {
                     existencia_total += Convert.ToInt32(row["existencia"].ToString());
                }


                if (existencia_total > 0)
                {
                    nmbCantidad.Select();
                    nmbCantidad.Focus();
                   
                }
              
            }
            else
            {
                MessageBox.Show(this, "Este producto no tiene existencias, imposible agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private void nmbCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 27:
                    txt_amecop.Enabled = true;
                    txt_amecop.Text = "";
                    txtDescripcion.Text = "";
                    txt_amecop.Focus();
                    nmbCantidad.Value = 1;
                break;

                case 13 :
                if (nmbCantidad.Value > 0 &&  existencia_total > 0 )
                {
                    Int32 cantidad_bonificado = Convert.ToInt32(nmbCantidad.Value.ToString());

                    insertar_producto_bonificado(cantidad_bonificado);
                }
                else
                {
                    MessageBox.Show(this, " Imposible bonificar cantidades vacias, verique!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    nmbCantidad.Focus();
                }
                break;


            }
        }

        public void insertar_producto_bonificado(Int32 cantidad_bonificado)
        {
            try
            {

                string[] split_folio = txtbxTicket.Text.Trim().Split('$');
                long venta_id = Convert.ToInt64(split_folio[1].ToString());

                bool existe_articulo = dao_bonificaciones.is_existe_producto(txt_amecop.Text.ToString().Trim(), venta_id);

                if (existe_articulo)
                {
                    long cantidad_maxima_vendida = dao_bonificaciones.max_existe_producto(txt_amecop.Text.ToString().Trim(), venta_id);

                    if (cantidad_bonificado <= cantidad_maxima_vendida)
                    {
                        DAO_Articulos dao = new DAO_Articulos();
                        Int32 existencia_vendible = dao.get_existencia_total(txt_amecop.Text.ToString().Trim());

                        if (existencia_vendible >= Convert.ToInt32(nmbCantidad.Value))
                        {


                            bool existe = false;
                            foreach (DataGridViewRow row in dgwbonificacion.Rows)
                            {
                                string codigo = row.Cells["amecop"].Value.ToString();

                                if (txt_amecop.Text.ToString().Trim() == codigo)
                                {
                                    existe = true;  
                                }

                            }

                            if (existe == false)
                            {

                                DataGridViewRow fila = new DataGridViewRow();
                                fila.CreateCells(dgwbonificacion);

                                fila.Cells[0].Value = txt_amecop.Text.ToString();
                                fila.Cells[1].Value = txt_amecop.Text.ToString();
                                fila.Cells[2].Value = txtDescripcion.Text.ToString();
                                fila.Cells[3].Value = cantidad_bonificado;
                                dgwbonificacion.Rows.Add(fila);

                            }
                            else
                            {
                                foreach (DataGridViewRow row in dgwbonificacion.Rows)
                                {
                                    string codigo = row.Cells["amecop"].Value.ToString();

                                    if (txt_amecop.Text.ToString().Trim() == codigo)
                                    {
                                        row.Cells["cantidad"].Value = Convert.ToInt32(row.Cells["cantidad"].Value.ToString()) + Convert.ToInt32(nmbCantidad.Value);
                                    }

                                }
                        
                        
                            }




                            txt_amecop.Text = "";
                            txtDescripcion.Text = "";
                            nmbCantidad.Value = 1;
                            existencia_total = 0;
                            txt_amecop.Enabled = true;
                            txt_amecop.Focus();



                        }
                        else
                        {
                            MessageBox.Show(this, "No cuentas con la sufiencte existencia para dar bonificar este producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show(this, "El Producto no puede bonificarse debido a que excede la cantidad de piezas de la nota", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(this, "El producto no es disponible para bonificar, debido a que no pertenece al ticket de venta ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_amecop.Text = "";
                    txtDescripcion.Text = "";
                    nmbCantidad.Value = 1;
                    existencia_total = 0;
                    txt_amecop.Enabled = true;
                    txt_amecop.Focus();
                }


               
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.ToString());
            }

        }

        private void dgwbonificacion_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
              
                case 46:

                    dgwbonificacion.Rows.Remove(dgwbonificacion.CurrentRow);
                    
                    txt_amecop.Focus();
                   
                    break;
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
            nmbCantidad.Focus();
        }

        public void busqueda_productos_Close(object sender, FormClosedEventArgs e)
        {
            try
            {
                long? articulo_id = Busqueda_productos.articulo_articulo_id;
                if (articulo_id != null)
                {
                    dto_articulo = new DTO_Articulo();
                    dto_articulo.Articulo_id = (int)articulo_id;
                    txt_amecop.Text = Busqueda_productos.articulo_amecop;

                    txtDescripcion.Text = Busqueda_productos.articulo_producto;
                    existencia_total = Convert.ToInt32(Busqueda_productos.articulo_existencia_vendible);
                    nmbCantidad.Value = 1;
                    nmbCantidad.Select(0, nmbCantidad.Text.Length);
                    nmbCantidad.Focus();
                    


                }
            }
            catch (Exception ex)
            {
                Log_error.log(ex);
            }
        }

        public bool valida_ticket()
        {
            bool valido = false;

        
            if (Misc_helper.validar_codigo_venta(txtbxTicket.Text.Trim()) && txtbxTicket.Text.Trim() != ""  )
            {
                string[] split_folio = txtbxTicket.Text.Trim().Split('$');
                long venta_id = Convert.ToInt64(split_folio[1].ToString());

                if (Misc_helper.es_numero(split_folio[0]) && Misc_helper.es_numero(split_folio[1]))
                {
                    long sucursal_id = Convert.ToInt64(split_folio[0]);

                    DAO_Ventas dao_ventas = new DAO_Ventas();
                    DAO_Sucursales dao_sucursales = new DAO_Sucursales();

                    long sucursal_local = Convert.ToInt64(Config_helper.get_config_local("sucursal_id"));

                    if (sucursal_local.Equals(sucursal_id))
                    {
                        if (dao_ventas.existe_venta(venta_id))
                            valido = true;
                        else
                            MessageBox.Show(this, "Ticket inválido, registro de venta no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        
                    }
                    else
                    {
                        var dto_sucursal = dao_sucursales.get_sucursal_data((int)sucursal_id);

                        if (dto_sucursal.sucursal_id > 0)
                        {
                            MessageBox.Show(this, string.Format("Esta venta pertenece a la sucursal {0}, no puede ser bonificada aqui", dto_sucursal.nombre));
                            txt_amecop.Text = "";
                            txt_amecop.Focus();
                        }
                        else
                        {
                            MessageBox.Show(this, "Codigo inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_amecop.Text = "";
                            txt_amecop.Focus();
                        }
                    }

                }




            }
            else
            {

                MessageBox.Show(this, "Formato erroneo de ticket, porfavor escanea el ticket correctamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_amecop.Focus();
            }

            return valido;
                  
        }

        public bool valida_datos_operacion()
        {
            bool valido = false;

            if(txtbxTicket.Text.Trim() != "" && txttarjeta.Text.Trim() != "" && txtTransaccion.Text.Trim() != "")
                valido = true;
            else
            {

                if (txtbxTicket.Text.Trim() == "")
                    txtbxTicket.Focus();
                else if (txttarjeta.Text.Trim() == "")
                    txttarjeta.Focus();
                else if (txtTransaccion.Text.Trim() == "")
                    txtTransaccion.Focus();

                MessageBox.Show(this, "Datos incompletos, completa correctamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);            
            
            }


            return valido;
        
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Login_form login = new Login_form();
            login.ShowDialog();

            if (login.empleado_id != null)
            {
                DAO_Login dao = new DAO_Login();
                long empleado_id = (long)login.empleado_id;

                if (valida_ticket())
                {
                    if (valida_datos_operacion())
                    {
                        if (dgwbonificacion.RowCount > 0)
                        {                            
                            string[] split_folio = txtbxTicket.Text.Trim().Split('$');
                            long venta_id = Convert.ToInt64(split_folio[1].ToString());
                            string tarjeta = txttarjeta.Text.ToString().Trim();
                            string transaccion = txtTransaccion.Text.ToString().Trim();
                            

                            long id_bonificacion = dao_bonificaciones.set_bonificacion(venta_id, tarjeta,transaccion,empleado_id);
                            
                            if (id_bonificacion > 0)
                            {

                                long id_ajuste = dao_bonificaciones.set_ajuste(empleado_id, id_bonificacion, venta_id);

                                if (id_ajuste > 0)
                                {
                                    foreach (DataGridViewRow row in dgwbonificacion.Rows)
                                    {

                                        long cantidad = Convert.ToInt64(row.Cells["cantidad"].Value.ToString());

                                        bool valido = dao_bonificaciones.bonificacion_insertada(id_bonificacion, row.Cells["amecop"].Value.ToString(), cantidad, id_ajuste); 
                                        
                                    }

                                    MessageBox.Show(this, "Bonificacion terminada");
                                    this.Close();

                                }

                               
                            }
                            else
                            {
                                MessageBox.Show(this, "Error al registrar la bonificacion, contacte a su administrador de base de datos para mas informacion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            
                            }

                        }
                        else
                        {
                            MessageBox.Show(this, " Imposible bonificar .productos no registrados, verique!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_amecop.Focus();
                        }
                    }


                }
                else
                    txtbxTicket.Focus();
            }
            
        }


    }
}
