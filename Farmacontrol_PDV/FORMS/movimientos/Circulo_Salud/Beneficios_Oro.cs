using Farmacontrol_PDV.circulo_salud;
using Farmacontrol_PDV.CLASSES;
using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.HELPERS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Farmacontrol_PDV.FORMS.movimientos.Circulo_Salud
{
    public partial class Beneficios_Oro : Form
    {
        public Int64 venta_id;
        public string Sesion;
        public string Tarjeta;
        public Form  venta_oro  ;
       // public string ip_servidor = "10.202.1.172";
        public string ip_servidor = ConfigurationManager.AppSettings["server_beneficios"];


        public Beneficios_Oro(string session,string tarjeta,Int64 venta,Form circulo_oro)
        {
            InitializeComponent();
            this.Sesion = session;
            this.Tarjeta = tarjeta;
            this.venta_id = venta;
            this.venta_oro = circulo_oro;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblFolioVenta_Click(object sender, EventArgs e)
        {

        }

        private void Beneficios_Oro_Load(object sender, EventArgs e)
        {
            lblFolioVenta.Visible = true;
            lblSesion.Visible = true;

            lblFolioVenta.Text = venta_id.ToString();
            lblSesion.Text = Sesion;


            string resultado = HELPERS.Config_helper.get_config_local("parametros_circulo_salud");

            if (!resultado.Equals(""))
            {
                /*PONER PARAMETROS***/

                string[] parametros = resultado.Split('*');

                DTO_WebServiceCirculoOro val = new DTO_WebServiceCirculoOro();

                Rest_parameters datos = new Rest_parameters();

                datos.Add("Session", this.Sesion.ToString());
                datos.Add("NoTarjeta", this.Tarjeta.ToString());
                datos.Add("venta_id", this.venta_id);
                datos.Add("Usuario", parametros[0]);
                datos.Add("Contrasenia", parametros[1]);
                datos.Add("Cadena", parametros[2]);

                val = Rest_helper.enlace_webservice_Circulo_Oro<DTO_WebServiceCirculoOro>("webservice_Oro/getBonusProductList_WSDL", datos, ip_servidor);

                if (!val.huboError)
                {
                     dtgview.DataSource =val.datos;
                    
                }
                else
                {
                    MessageBox.Show(this, val.mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

              Login_form login = new Login_form();
              login.ShowDialog();

              if (login.empleado_id != null)
              {

                  string nombre_empleado = login.empleado_nombre.ToString().Length > 20 ?  login.empleado_nombre.ToString().Substring(0, 20) : login.empleado_nombre.ToString();

                  List<object> pedidoarticulos = new List<object>();
                  decimal total = 0;

                  Dictionary<string, int> articulosgratis = new Dictionary<string, int>();
                  DAO_Existencias existencias = new DAO_Existencias();

                 foreach (DataGridViewRow row in dtgview.Rows)
                  {
                      Dictionary<string, object> productos = new Dictionary<string, object>();
                      productos.Add("Sku", row.Cells["amecop"].Value.ToString() );
                      productos.Add("Precio", row.Cells["precio"].Value.ToString());
                      productos.Add("PrecioPOS", row.Cells["precio"].Value.ToString());
                      productos.Add("PrecioFijo", "0");
                      productos.Add("PorcentajeDescuento", "0");
                      productos.Add("MontoDescuento", "0");
                      productos.Add("PiezasPagadas", row.Cells["piezas"].Value.ToString());
                      productos.Add("PiezasGratis", row.Cells["beneficio"].Value.ToString());
                      productos.Add("IVA", "0");
                      productos.Add("PorcentajeIVA", "0");
                      pedidoarticulos.Add(productos);

                      total = total + Convert.ToDecimal(row.Cells["precio"].Value.ToString());

                    if (row.Cells["beneficio"].Value.ToString() != "0")
                    {
                        
                        //TENDRA QUE REVISAR SI HAY EXISTENCIA SUFICIENTE PARA CUMPLIR LA ENTREGA
                        int existencia = existencias.get_existencia_amecop(row.Cells["amecop"].Value.ToString());

                        if (existencia < Convert.ToInt32(row.Cells["beneficio"].Value.ToString()))
                        {
                            MessageBox.Show(this, "La existencia del codigo :  " + row.Cells["amecop"].Value.ToString() + " no cumple con la entrega ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return;
                        }
                        else
                        {
                            articulosgratis.Add(row.Cells["amecop"].Value.ToString(), Convert.ToInt32(row.Cells["beneficio"].Value.ToString()));

                        }
                       
                    }
            
                  }

                  var json_pedidoarticulos = JsonConvert.SerializeObject(pedidoarticulos);
      
                  DTO_WebServiceCirculoOro val = new DTO_WebServiceCirculoOro();
                  DAO_log_lealtad log_lealtad = new DAO_log_lealtad();

                  Rest_parameters datos = new Rest_parameters();

                  datos.Add("Session", this.Sesion.ToString());
                  datos.Add("NoTarjeta", this.Tarjeta.ToString());
                  datos.Add("Usuario", nombre_empleado);
                  datos.Add("venta_id", this.venta_id);
                  datos.Add("Total", total);
                  datos.Add("Articulos", json_pedidoarticulos);

                  val = Rest_helper.enlace_webservice_Circulo_Oro<DTO_WebServiceCirculoOro>("webservice_Oro/createSalesFolioReceta_WSDL", datos, ip_servidor);

                  if (!val.huboError)
                  {
                      /*IMPRESIONES*/
                      ticket_circulo_oro ticket_oro = new ticket_circulo_oro();
                      ticket_oro.construccion_ticket(this.Tarjeta.ToString(), val.NoAutorizacion, "", this.venta_id.ToString());
                      ticket_oro.print();

                     long id_insercion = log_lealtad.inserta_log_operacion(this.Tarjeta.ToString(), "CIRCULO_SALUD", this.venta_id, json_pedidoarticulos, "");

                    if (articulosgratis.Count > 0)
                    {
                        //registrar el log de la operacion para contabilizar los canges

                        string resultado = string.Join(", ", articulosgratis.Select(kv => $"{kv.Key}:{kv.Value}"));

                       bool respueta_actualizacion =  log_lealtad.actualiza_log_operacion(id_insercion, resultado);

                        if (respueta_actualizacion)
                        {
                            MessageBox.Show(this, "Log actualizado correctamente", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(this, "Error al actualizar el log, notifica el error a SISTEMAS", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        DAO_Ajustes_existencias ajusta_existencias = new DAO_Ajustes_existencias();

                        //Realizar el ajuste de los productos participantes
                        //crea el ajuste
                        long id_ajuste  = ajusta_existencias.crear_ajuste_existencia(  (long)login.empleado_id  );
                        if(id_ajuste > 0)
                             ajusta_existencias.set_inserta_detallado_ajuste(articulosgratis, id_ajuste);


                    }

                    MessageBox.Show(this, "Registrado correctamente", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);

                     venta_oro.Close();
                      this.Close();
                  }
                  else
                  {
                      MessageBox.Show(this, val.mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }     

             }
        }
    }
}
