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
using Farmacontrol_PDV.CLASSES;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.DAO;


namespace Farmacontrol_PDV.FORMS.movimientos.canjes_lealtad
{
    public partial class compras_beneficios_principal : Form
    {

        string ip_conexion = "192.168.1.250";
        //string ip_conexion = "172.16.1.5";
        string tarjeta;
        string nombre_empleado;
        string id_empleado;
        string transaccion;
        long venta_id;

        public compras_beneficios_principal( string tarjeta_ev, string empleado_nombre_ev, string empleado_id_ev ,string transaccion_ev   )
        {

            InitializeComponent();

            this.tarjeta = tarjeta_ev;
            this.nombre_empleado = empleado_nombre_ev;
            this.id_empleado = empleado_id_ev;
            this.transaccion = transaccion_ev;
            compras_beneficios_principal.CheckForIllegalCrossThreadCalls = false;
        }

        private void compras_beneficios_principal_Load(object sender, EventArgs e)
        {
            txtBFolio.Focus();
            llenar_informacion();
        }

        private void bntCancelar_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(this, " ¿Desea salir del registro?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
            
       
        }

        

        public void llenar_informacion()
        {
            txtTarjeta.Text = tarjeta;
            txtBTransaccion.Text = transaccion;
            txtNombreEmpleado.Text = nombre_empleado;
        
        }

        private void btnConsultarfolio_Click(object sender, EventArgs e)
        {
            if (this.set_valida_sucursal())
            { 
                DAO_Ventas dao_ventas = new DAO_Ventas(); 

                DataTable dt = dao_ventas.get_productosunicos_venta(venta_id);

                dtgvProductos.DataSource = dt;

                string productos = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        productos += dt.Rows[i]["codigo"].ToString() + "," + dt.Rows[i]["cantidad"].ToString() + "," + dt.Rows[i]["precio_unitario"].ToString() + "," + dt.Rows[i]["importe"].ToString() + "|";
                    }
                    char[] charsToTrim = { '|'};
                    productos = productos.TrimEnd(charsToTrim);
                }


                DTO_WebServiceEnlaceVital val = new DTO_WebServiceEnlaceVital();

                Rest_parameters parametros = new Rest_parameters();
                parametros.Add("tarjeta", tarjeta);
                parametros.Add("storeid", Config_helper.get_config_local("sucursal_id").ToString());
                parametros.Add("posid", Misc_helper.get_terminal_id().ToString());
                parametros.Add("employeeid", id_empleado.ToString());
                parametros.Add("transaccion", transaccion.ToString());
                parametros.Add("transactionitems", productos.ToString());

                val = Rest_helper.enlace_webservice_lealtad<DTO_WebServiceEnlaceVital>("webservice/get_beneficios", parametros, ip_conexion);

                if (val.status)
                {
                    string productoswb = val.transactionitems.ToString();

                    string[] a_producto = productoswb.Split('|');

                    
                    DAO_Articulos dao_articulo = new DAO_Articulos();
                    foreach (var prod in a_producto)
                    {
                        string[] info_producto = prod.Split(',');

                         var informacion_articulo = dao_articulo.get_articulo(info_producto[0].ToString());
                        

                        /*
                         0 producto
                         1 cantdescuento
                         2 descuento
                         3 obsequios
                         4 puntos
                         */

                        if (Int32.Parse(info_producto[3].ToString()) > 0)  //ENTREGA OBSEQUIO Y SE AGREGA AL DATAGRED
                        {
                            //CONSULTA DE ARTICULO_ID Y DESCRIPCION

                            DataTable dt2 = new DataTable();
                            dt2 = dtgvProductos.DataSource as DataTable;

                            DataRow datarow;
                            datarow = dt2.NewRow(); //Con esto le indica que es una nueva fila.

                            datarow["articulo_id"] = informacion_articulo.Articulo_id;
                            datarow["codigo"] = info_producto[0].ToString();
                            datarow["descripcion"] = informacion_articulo.Nombre;
                            datarow["precio_unitario"] = 0.00f;
                            datarow["cantidad"] = Int32.Parse(info_producto[3].ToString());
                            datarow["importe"] = 0.00f;
                                                
                            dt2.Rows.Add(datarow);  

                        }
                    
                    }

                     

                }
                else
                {
                    MessageBox.Show(this, val.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }     


            }

            







        }

        private bool set_valida_sucursal()
        {
            bool valido = false;

            if (txtBFolio.TextLength > 0)
            {
                string[] array_codigo = txtBFolio.Text.Split('$');
                long sucursal_id;

                if (array_codigo.Length.Equals(2))
                {
                    if (Misc_helper.es_numero(array_codigo[0]) && Misc_helper.es_numero(array_codigo[1]))
                    {
                        sucursal_id = Convert.ToInt64(array_codigo[0]);
                        venta_id = Convert.ToInt64(array_codigo[1]);

                        DAO_Ventas dao_ventas = new DAO_Ventas();
                        DAO_Sucursales dao_sucursales = new DAO_Sucursales();

                        long sucursal_local = Convert.ToInt64(Config_helper.get_config_local("sucursal_id"));

                        if (sucursal_local.Equals(sucursal_id))
                        {
                            if (dao_ventas.existe_venta(venta_id))
                            {
                                venta_id = Convert.ToInt32(array_codigo[1]);

                              //  if (dao_cancelaciones.existe_cancelacion(venta_id))//VENTA YA REGISTRADA CON ACUMULACION
                               // {
                                 //   MessageBox.Show(this, "Esta venta ha sido cancelada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //}
                               // else
                                //{
                                    var venta_data = dao_ventas.get_venta_data(Convert.ToInt64(venta_id));

                                    if (venta_data.venta_id > 0)
                                    {
                                        if (venta_data.fecha_terminado != null)
                                        {
                                            valido = true;
                                        }
                                        else
                                        {
                                            MessageBox.Show(this, "No puedes registrar una venta NO terminada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        txtBFolio.SelectAll();
                                        MessageBox.Show(this, "Ticket de venta no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txtBFolio.Focus();
                                    }
                              ///  }
                            }
                            else
                            {
                                MessageBox.Show(this, "Ticket de venta no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show(this, "No puedes registrar esta venta, ya que fue realizada en otra sucursal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Codigo inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Codigo inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return valido;
        
        }

        private void txtBFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (this.set_valida_sucursal())
                {
                    DAO_Ventas dao_ventas = new DAO_Ventas();

                    DataTable dt = dao_ventas.get_productosunicos_venta(venta_id);

                    dtgvProductos.DataSource = dt;

                    string productos = "";
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            productos += dt.Rows[i]["codigo"].ToString() + "," + dt.Rows[i]["cantidad"].ToString() + "," + dt.Rows[i]["precio_unitario"].ToString() + "," + dt.Rows[i]["importe"].ToString() + "|";
                        }
                        char[] charsToTrim = { '|' };
                        productos = productos.TrimEnd(charsToTrim);
                    }


                    DTO_WebServiceEnlaceVital val = new DTO_WebServiceEnlaceVital();

                    Rest_parameters parametros = new Rest_parameters();
                    parametros.Add("tarjeta", tarjeta);
                    parametros.Add("storeid", Config_helper.get_config_local("sucursal_id").ToString());
                    parametros.Add("posid", Misc_helper.get_terminal_id().ToString());
                    parametros.Add("employeeid", id_empleado.ToString());
                    parametros.Add("transaccion", transaccion.ToString());
                    parametros.Add("transactionitems", productos.ToString());

                    val = Rest_helper.enlace_webservice_lealtad<DTO_WebServiceEnlaceVital>("webservice/get_beneficios", parametros, ip_conexion);

                    if (val.status)
                    {
                        string productoswb = val.transactionitems.ToString();

                        string[] a_producto = productoswb.Split('|');


                        DAO_Articulos dao_articulo = new DAO_Articulos();
                        foreach (var prod in a_producto)
                        {
                            string[] info_producto = prod.Split(',');

                            var informacion_articulo = dao_articulo.get_articulo(info_producto[0].ToString());


                            /*
                             0 producto
                             1 cantdescuento
                             2 descuento
                             3 obsequios
                             4 puntos
                             */

                            if (Int32.Parse(info_producto[3].ToString()) > 0)  //ENTREGA OBSEQUIO Y SE AGREGA AL DATAGRED
                            {
                                //CONSULTA DE ARTICULO_ID Y DESCRIPCION

                                DataTable dt2 = new DataTable();
                                dt2 = dtgvProductos.DataSource as DataTable;

                                DataRow datarow;
                                datarow = dt2.NewRow(); //Con esto le indica que es una nueva fila.

                                datarow["articulo_id"] = informacion_articulo.Articulo_id;
                                datarow["codigo"] = info_producto[0].ToString();
                                datarow["descripcion"] = informacion_articulo.Nombre;
                                datarow["precio_unitario"] = 0.00f;
                                datarow["cantidad"] = Int32.Parse(info_producto[3].ToString());
                                datarow["importe"] = 0.00f;

                                dt2.Rows.Add(datarow);

                            }

                        }



                    }
                    else
                    {
                        MessageBox.Show(this, val.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }



            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show(this, " ¿Finalizar el registro de compra ?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dr == DialogResult.Yes)
            {
                if (!backGWorkerBeneficios.IsBusy)
                {
                    ProBarTermina.Visible = true;
                    backGWorkerBeneficios.RunWorkerAsync();
                }
                
            }


        }


        private void backGWorkerBeneficios_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            { 
                string itemsventa = "";
                string articulos_ajustar = "";
                
                bool  bExistenciaValida = true;
                char[] charsToTrim = { '|' };
                char[] charsToTrimAjuste = { ',' };
                decimal montototal = 0;
                DAO_Existencias existencias = new DAO_Existencias();

                

                (sender as BackgroundWorker).ReportProgress(15);

                for (int i = 0; i < dtgvProductos.Rows.Count; i++)
                {
                    string art_id =  dtgvProductos.Rows[i].Cells[0].Value.ToString();
                    string codigo = dtgvProductos.Rows[i].Cells[1].Value.ToString();
                    string cantidad = dtgvProductos.Rows[i].Cells[4].Value.ToString();
                    string precio_costo = dtgvProductos.Rows[i].Cells[3].Value.ToString();
                    string importe = dtgvProductos.Rows[i].Cells[5].Value.ToString();
                    itemsventa += codigo + "," + cantidad + "," + precio_costo.Replace(",","") + "," + importe.Replace(",","") + "|";

                    montototal = montototal + Convert.ToDecimal(importe);


                    if (Convert.ToDecimal(precio_costo.ToString()) == 0 && Convert.ToDecimal(importe.ToString()) == 0) //ES ENLACE VITAL
                    {
                        int existencia_sistema = existencias.get_existencia_articulo(Convert.ToInt64(art_id));

                        if (existencia_sistema > 0)
                        {
                            if (existencia_sistema >= Convert.ToInt32(cantidad))
                            {
                                //TENDRA QUE AJUSTAREL ARTICULO CON SU EXISTENCIA , SE ADECUA EL ARTICULO CON SU CANTIDAD PARA PROCEDER AL AJUSTE 
                                articulos_ajustar += art_id + "-" + cantidad + ",";
                            }
                            else
                            {
                                bExistenciaValida = false;
                                MessageBox.Show(this, "Existencia en el sistema insuficiente, realiza el registro del beneficio y/o compra mas tarde", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        }

                    }
                }
     
                itemsventa = itemsventa.TrimEnd(charsToTrim);
                articulos_ajustar = articulos_ajustar.TrimEnd(charsToTrimAjuste);

                (sender as BackgroundWorker).ReportProgress(40);

                if (!bExistenciaValida)
                {
                    (sender as BackgroundWorker).ReportProgress(100);

                    if (!backGWorkerBeneficios.IsBusy)
                    {
                        ProBarTermina.Visible = false;
                        backGWorkerBeneficios.CancelAsync();
                    }
                }
                else
                {
                    (sender as BackgroundWorker).ReportProgress(50);

                    //ESTABLECE CONEXION CON EL SERVIDOR ORBISAPP
                
                     DTO_WebServiceEnlaceVital val = new DTO_WebServiceEnlaceVital();
                     Rest_parameters parametros = new Rest_parameters();
                     parametros.Add("tarjeta", tarjeta);
                     parametros.Add("storeid", Config_helper.get_config_local("sucursal_id").ToString());
                     parametros.Add("posid", Misc_helper.get_terminal_id().ToString());
                     parametros.Add("employeeid", id_empleado.ToString());
                     parametros.Add("transaccion", transaccion.ToString());
                     parametros.Add("transactionitems", itemsventa.ToString());

                     parametros.Add("invoicenumber", venta_id.ToString());
                     parametros.Add("invoicedate", Misc_helper.fecha());
                     parametros.Add("invoiceamount", montototal.ToString());
                     (sender as BackgroundWorker).ReportProgress(50);
                     val = Rest_helper.enlace_webservice_lealtad<DTO_WebServiceEnlaceVital>("webservice/set_venta_beneficios", parametros, ip_conexion);
                     (sender as BackgroundWorker).ReportProgress(70);
                     if (val.status)
                     {
                         //AJUSTA EXISTENCIAS EN LA SUCURSAL
                         if(articulos_ajustar != "")
                         {

                             DTO_WebServiceEnlaceVital ajustes_ev = new DTO_WebServiceEnlaceVital();
                             Rest_parameters param = new Rest_parameters();
                                                  
                             param.Add("posid", Misc_helper.get_terminal_id().ToString());
                             param.Add("employeeid", id_empleado.ToString());
                             param.Add("articulo_ajustar", articulos_ajustar.ToString());
                             param.Add("venta_id", venta_id.ToString());
                             param.Add("tarjeta", tarjeta);
                             param.Add("transaccion", transaccion.ToString());
                             

                             (sender as BackgroundWorker).ReportProgress(75);
                             ajustes_ev = Rest_helper.enlace_webservice_lealtad<DTO_WebServiceEnlaceVital>("webservice/set_ajuste_beneficio", param, ip_conexion);

                             if (ajustes_ev.status)
                             {
                                 MessageBox.Show(this, ajustes_ev.mensaje, "Existencia afectada", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                             
                             }
                             else
                                 MessageBox.Show(this, ajustes_ev.mensaje, "Error !!", MessageBoxButtons.OK, MessageBoxIcon.Information); 

                             (sender as BackgroundWorker).ReportProgress(90);

                         }

                         MessageBox.Show(this, "Venta y Registro terminada correctamente "+ val.mensaje + " , Transaccion :  " + transaccion.ToString() + " Autorizacion : " + val.autorizacion + " Mensaje ticket : " + val.mensajeticket, "Registro de compra finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                         /*IMPRIMIR TICKET*/
                         (sender as BackgroundWorker).ReportProgress(100);


                         this.Close();
                     }
                     else 
                     {
                         MessageBox.Show(this, val.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }

                     (sender as BackgroundWorker).ReportProgress(90);
                     (sender as BackgroundWorker).ReportProgress(100);
                     

                }

            }
            catch(Exception ex)
            {
                Log_error.log(ex);
            }


        }

        private void backGWorkerBeneficios_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProBarTermina.Value = e.ProgressPercentage;
        }

        private void backGWorkerBeneficios_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProBarTermina.Visible = false;
        }



    }
}
