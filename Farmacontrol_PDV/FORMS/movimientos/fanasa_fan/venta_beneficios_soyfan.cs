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
using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.FORMS.comunes;
using System.Configuration;

namespace Farmacontrol_PDV.FORMS.movimientos.fanasa_fan
{
    public partial class venta_beneficios_soyfan : Form
    {
        public string folio_venta = "";
        public string autorizacion = "";
        public string giftAuthNum = "";
        public string ip_servidor = ConfigurationManager.AppSettings["server_beneficios"];

        string tarjeta = "";
        string guarda_gift = "";
        string guarda_list = "";
        string beneficios_otorgados = "";
        string codigos_blockeados = "7501008496701,7501065054043,7501008497593,7501008498309,7501300407108,7501008485316";
        int empleado_id = 0; 

        public venta_beneficios_soyfan(string folio,string num_autorizacion,string tarjeta_beneficios )
        {
            InitializeComponent();

            this.folio_venta = folio;
            this.autorizacion = num_autorizacion;
            this.tarjeta = tarjeta_beneficios;

            txtFolioVenta.Text = this.folio_venta;
            txtAutorizacion.Text = this.autorizacion;
            txtTarjeta.Text = tarjeta_beneficios;

            /*CHECAR SI LA NOTA NO ESTA REGISTRADA COMO BENEFICIO*/
            DAO_registro_soyfan soyfanobj = new DAO_registro_soyfan();
            bool valido = soyfanobj.get_registro_venta(this.folio_venta);

            if (!valido)
            {
                this.set_envio_nota();
            }
            else
            {
                MessageBox.Show(this, "La nota "+this.folio_venta +" ya ha sido registrada anteriormente", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnTerminar.Visible = false;
            }

        }

        #region ENVIO DE NOTA
        public void set_envio_nota()
        {

                beneficios_otorgados = "";
                DTO_WebServiceSoyFan val = new DTO_WebServiceSoyFan();
                DTO_WebServiceSoyFan valor = new DTO_WebServiceSoyFan();
                Rest_parameters parametros = new Rest_parameters();
                Rest_parameters param = new Rest_parameters();

                DataTable venta_codigos = new DataTable();
                DAO_Ventas ventas = new DAO_Ventas();
                DAO_Existencias existencia = new DAO_Existencias();
                DAO_excepciones_codigos excepcion = new DAO_excepciones_codigos();

                string cadena_codigos_vendidos = "";

                long id_folio_venta  = ventas.get_venta_idxfolio( this.folio_venta);

                venta_codigos = ventas.get_productosunicos_venta(id_folio_venta);

                /*   AQUI HAY QUE OBTENER LA LISTA DE PRODUCTOS PARTICIPANTES DE SOLO PIEZA X PIEZA */
                valor = Rest_helper.enlace_webservice_soyfan<DTO_WebServiceSoyFan>("webservice/set_conexion_lealtad", param, ip_servidor);

                string ex_codigos = excepcion.get_excepciones_codigos();
                string codigos_participantes = "";

                if (ex_codigos != "")
                    codigos_participantes = valor.codigos_participantes + "*" + ex_codigos;
                else
                    codigos_participantes = valor.codigos_participantes;


                    
                string[] codigos = codigos_participantes.Split('*');
            
                string[] codigos_block = codigos_blockeados.Split(',');///codigos bloqueados 

                int veces = 0, veces_block = 0 ;
                foreach (DataRow row in venta_codigos.Rows)
                {
                    int index = Array.IndexOf(codigos, row["codigo"].ToString());
                    int index_block = Array.IndexOf(codigos_block, row["codigo"].ToString());

                    if (index_block > -1)
                    {
                        veces_block++;
                    }

                    if (index > -1)
                    {
                        veces++;

                        string cd_cero = excepcion.get_codigo_cero(row["codigo"].ToString());
                      
                        cadena_codigos_vendidos += ( cd_cero == "" ? row["codigo"].ToString() : cd_cero) + "-" + row["cantidad"].ToString() + "-" + row["precio_unitario"].ToString() + "*";


                    }

                }

                if (veces > 0 && veces_block == 0 )
                {
                    btnTerminar.Visible = true;
                    dvgVentas.DataSource = venta_codigos;

                    string nombre_empleado = "farmaboot";

                    parametros.Add("user", nombre_empleado);
                    parametros.Add("transaction", folio_venta);
                    parametros.Add("cardAuthNum", autorizacion);
                    parametros.Add("folio", tarjeta);
                    parametros.Add("codigos", cadena_codigos_vendidos);

                    val = Rest_helper.enlace_webservice_soyfan<DTO_WebServiceSoyFan>("webservice/get_beneficios_lealtad", parametros, ip_servidor);

                    if (val.status)
                    {

                        giftAuthNum = val.giftAuthNum;
                        this.guarda_gift = val.guardar_gif;
                        this.guarda_list = val.guardar_list;

                        if (val.promocion != "0")
                        {
                            
                            List<string> articulos_promociones = new List<string>();
                            DAO.DAO_Articulos articulos = new DAO_Articulos();
                            string temp = val.promocion.ToString();

                            articulos_promociones = temp.Split(',').ToList();

                            foreach( var item in articulos_promociones )
                            {
                                    List<string> promocion = new List<string>();
                                    promocion = item.Split('*').ToList();

                                    int ex = existencia.get_existencia_amecop(promocion[0]);
                                    string descripcion_producto = articulos.get_nombre_codigo(promocion[0]);

                                    if (ex > 0 && ex >= Convert.ToInt32(promocion[1].ToString()))
                                    {
                                        DataTable dt2 = new DataTable();
                                        dt2 = dvgVentas.DataSource as DataTable;


                                        DataRow datarow;
                                        datarow = dt2.NewRow();

                                        datarow["articulo_id"] = "0";
                                        datarow["codigo"] = promocion[0];
                                        datarow["descripcion"] = descripcion_producto;
                                        datarow["cantidad"] = promocion[1];
                                        datarow["importe"] = 0;
                                        datarow["precio_unitario"] = 0;

                                        dt2.Rows.Add(datarow);

                                        beneficios_otorgados += promocion[0] + "*" + descripcion_producto + "*" + promocion[1] + ",";
                                    }
                                    else
                                    {
                                        btnTerminar.Visible = false;
                                        MessageBox.Show(this, "Existencia insuficiente para entrega de beneficio del producto " + promocion[0] + " " + descripcion_producto + "(" + promocion[1] + " pz)", "Existencia insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }


                            }


                        }
                        else
                        {
                            MessageBox.Show(this, val.mensaje, "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                           
                        }

                        //MessageBox.Show(this, val.giftAuthNum, "gift", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        giftAuthNum = "";
                        MessageBox.Show(this, val.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }     


                }
                else
                {

                    MessageBox.Show(this, " La venta contiene productos no autorizados y/o blockeados para recibir Bonificacion", "Productos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dvgVentas.DataSource = null;
                    btnTerminar.Visible = false;

                }
                    
        }
        #endregion
        #region cancelar venta

        public bool cancelar_operacion()
        {
            bool bterminado = false;
            DTO_WebServiceSoyFan val = new DTO_WebServiceSoyFan();

            Rest_parameters parametros = new Rest_parameters();
            string nombre_empleado = "farmaboot";

            parametros.Add("user", nombre_empleado);
            parametros.Add("transaction", folio_venta);
            parametros.Add("cardAuthNum", autorizacion);
            parametros.Add("giftAuthNum", giftAuthNum);

            val = Rest_helper.enlace_webservice_soyfan<DTO_WebServiceSoyFan>("webservice/set_cancelar_venta", parametros, ip_servidor);

            if (val.status)
            {
                MessageBox.Show(this, val.mensaje, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bterminado = true; 
            }
            else
            {
                giftAuthNum = "";
                bterminado = false;
                MessageBox.Show(this, val.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return bterminado;
        
        }


        #endregion

        #region consulta ticket

      //  public void 

        #endregion  

        private void btnCancelar_Click(object sender, EventArgs e)
        {
         
            bool resultado = this.cancelar_operacion();
            this.Close();
           // if (resultado)
            //{
               
            //}
           
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            bool resultado = this.cancelar_operacion();
            this.Close();
         //   if (resultado)
           // {
             //   inicio_lealtad inicio = new inicio_lealtad();
              //  this.Close();
                //inicio.ShowDialog();
              
         //   }
           
               
        }

        private void dvgVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnTerminar_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show(this, " ¿Finalizar registro?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dr == DialogResult.Yes)
            {
                Login_form login = new Login_form();
                login.ShowDialog();
                if (login.empleado_id != null)
                {
                        this.empleado_id = Convert.ToInt32( login.empleado_id );
                        DTO_WebServiceSoyFan val = new DTO_WebServiceSoyFan();

                        Rest_parameters parametros = new Rest_parameters();
                        
                        string nombre_empleado = login.empleado_nombre.ToString();

                        parametros.Add("user", nombre_empleado);
                        parametros.Add("transaction", folio_venta);
                        parametros.Add("cardAuthNum", autorizacion);
                        parametros.Add("giftAuthNum", giftAuthNum);
                        parametros.Add("guarda_gift", this.guarda_gift);
                        parametros.Add("guarda_list", this.guarda_list);

                        val = Rest_helper.enlace_webservice_soyfan<DTO_WebServiceSoyFan>("webservice/set_aplicarVenta", parametros, ip_servidor);

                        if (val.status)
                        {
                            MessageBox.Show(this, val.mensaje, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //AQUI AJUSTA     
                        }
                        else
                        {
                            giftAuthNum = "";
                   
                            MessageBox.Show(this, val.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        DAO_registro_soyfan objSoyFan = new DAO_registro_soyfan();

                        string trama = nombre_empleado+"*"+folio_venta+"*"+autorizacion+"*"+giftAuthNum+"*"+this.guarda_gift+"*"+this.guarda_list;

                        long reg = objSoyFan.set_registro_operacion(Convert.ToInt32(folio_venta), trama);
   
                        if (beneficios_otorgados != "")
                        {

                            beneficios_otorgados = beneficios_otorgados.TrimEnd(',');
                            DAO_Articulos dao = new DAO_Articulos();
                            Dao_Bonificaciones dao_bonificaciones = new Dao_Bonificaciones();
                            List<string> articulos_promociones = new List<string>();

                            articulos_promociones = beneficios_otorgados.Split(',').ToList();

                            foreach (var item in articulos_promociones)
                            {
                                List<string> promocion = new List<string>();

                                promocion = item.Split('*').ToList();

                                Int32 existencia_vendible = dao.get_existencia_total(promocion[0].ToString());

                                if (existencia_vendible >= Convert.ToInt32(promocion[2].ToString()))
                                {

                                    string comentarios = "BONIFICACION SOY TU FAN NOTA #" + folio_venta;

                                    long id_ajuste = dao_bonificaciones.set_ajuste(empleado_id, reg, Convert.ToInt32(folio_venta), comentarios);


                                    bool valido = dao_bonificaciones.bonificacion_insertada(0, promocion[0].ToString(), Convert.ToInt32(promocion[2].ToString()), id_ajuste);

                                }
                                else
                                {
                                    MessageBox.Show(this, "Imposible realizar el ajuste, notificar a su administrador del sistema sobre el error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                  
                        }
                        
                    if (reg > 0)
                        {
                            MessageBox.Show(this, "Registro de venta , guardado correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            /*IMPRIMIR TICKET PARA EL CLIENTE*/
                            ticket_lealtad ticket_fan = new ticket_lealtad();
                            ticket_fan.construccion_ticket(tarjeta, autorizacion, beneficios_otorgados, folio_venta);
                            ticket_fan.print();
                        }

                        this.Close();
                }
            }
            

        }
    }
}
