using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.HELPERS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farmacontrol_PDV.FORMS.ventas.facturacion
{
    public partial class Facturacion_v12 : Form
    {
        private Panel panel_contenedor = new Panel();
        private string rfc_registro_id;
        private long venta_id;
        private string metodo_pago;
        private string cuenta;
        private string condiciones_pago;
        private long sucursal_id;
        DTO_Rfc datos_emisor = new DTO_Rfc();
        private bool status_envio_correos;
        string uso_del_cfdi = "";
        string uso_regimen = "";
        List<string> correos_enviar = new List<string>();

        bool status_timbrado = false;
        string mensaje_factura = "";
        string uuid_factura = "";

        public Facturacion_v12()
        {
            InitializeComponent();
            Facturacion_v12.CheckForIllegalCrossThreadCalls = false;
        }

        void set_panel(Panel panel)
        {
            panel_contenedor = panel;
            panel_contenedor.BringToFront();
            panel_contenedor.Refresh();
        }

        void validar_codigo_entrada()
        {
            if (Misc_helper.validar_codigo_venta(txt_folio_venta.Text.Trim()))
            {
                string[] split_folio = txt_folio_venta.Text.Trim().Split('$');
                venta_id = Convert.ToInt64(split_folio[1]);

                if (Misc_helper.es_numero(split_folio[0]) && Misc_helper.es_numero(split_folio[1]))
                {
                    sucursal_id = Convert.ToInt64(split_folio[0]);

                    DAO_Ventas dao_ventas = new DAO_Ventas();
                    DAO_Sucursales dao_sucursales = new DAO_Sucursales();

                    long sucursal_local = Convert.ToInt64(Config_helper.get_config_local("sucursal_id"));

                    if (sucursal_local.Equals(sucursal_id))
                    {
                        if (dao_ventas.existe_venta(venta_id))
                        {
                            DAO_Facturacion dao_facturacion = new DAO_Facturacion();
                            status_timbrado = dao_facturacion.existe_factura(venta_id);

                            //btn_reimprimir_factura.Visible = status_timbrado;

                            bool venta_correcta = false;

                            if (status_timbrado)
                            {
                                //var factura = WebServicePac_helper.obtenerDatos(venta_id,sucursal_id);

                                //DAO_Rfcs dao_rfc = new DAO_Rfcs();
                               // datos_emisor = dao_rfc.get_data_rfc_rfc(factura.rfc_receptor);

                                string fecha_facturado = dao_facturacion.fecha_existe_factura(venta_id);

                                MessageBox.Show(this, string.Format("Esta venta ya fue facturada {0}", Misc_helper.fecha(fecha_facturado, "LEGIBLE")
                                ), "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                venta_correcta = true;

                                reimprimir_factura();
                            }
                            else
                            {
                                Venta_factura venta_factura = new Venta_factura(venta_id);
                                venta_factura.ShowDialog();
                                venta_correcta = venta_factura.venta_correcta;
                            }

                            if (venta_correcta == true && status_timbrado == false)
                            {
                                var informacion_venta = dao_ventas.get_venta_data(venta_id);

                                set_panel(panel2);
                                rdb_pge.Checked = false;
                                rdb_pgm.Checked = false;
                                rdb_rfc_propio.Checked = false;

                                btn_anterior.Enabled = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show(this, "Venta no registrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_folio_venta.Text = "";
                            txt_folio_venta.Focus();
                        }
                    }
                    else
                    {
                        var dto_sucursal = dao_sucursales.get_sucursal_data((int)sucursal_id);

                        if (dto_sucursal.sucursal_id > 0)
                        {
                            MessageBox.Show(this, string.Format("Esta venta pertenece a la sucursal {0}, no puede ser facturada aqui", dto_sucursal.nombre));
                            txt_folio_venta.Text = "";
                            txt_folio_venta.Focus();
                        }
                        else
                        {
                            MessageBox.Show(this, "Codigo inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_folio_venta.Text = "";
                            txt_folio_venta.Focus();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(this,"Ticket de venta invalido","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        void reimprimir_factura()
        {
            btn_anterior.Enabled = true;
            btn_siguiente.Enabled = true;
            set_panel(panel8);
        }

        private void txt_folio_venta_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch(keycode)
            {
                case 13:
                    if(txt_folio_venta.Text.Trim().Length > 0)
                    {
                        validar_codigo_entrada();
                    }
                break;
            }
        }

        private void btn_siguiente_Click(object sender, EventArgs e)
        {
            if(btn_siguiente.Text.Trim().Equals("Finalizar"))
            {
                if(rdb_rfc_propio.Checked && chb_guardar_cambios.Checked.Equals(false))
                {
                    DAO_Rfcs dao_rfc = new DAO_Rfcs();
                    datos_emisor.codigo_postal = datos_emisor.codigo_postal.ToString().PadLeft(5, '0');
                    dao_rfc.actualizar_rfc(datos_emisor);
                }

                this.Close();
            }
            else
            {
                if (panel_contenedor.Tag.Equals(panel1.Tag))
                {
                    validar_codigo_entrada();
                }
                else if (panel_contenedor.Tag.Equals(panel2.Tag))
                {
                    validar_radio_rfc();
                }
                else if (panel_contenedor.Tag.Equals(panel3.Tag))
                {
                    //NUEVO CAMBIO 

                    uso_factura uso_cfdi = new uso_factura();
                    uso_cfdi.ShowDialog();

                     uso_del_cfdi = uso_cfdi.tipo_uso_cfdi;
                     uso_regimen = uso_cfdi.tipo_regimen;

                     if (uso_del_cfdi != "" && uso_regimen != "")
                     {
                         validar_venta();
                     }
                     else
                     {
                         MessageBox.Show(this, "EL USO DE CFDI Y REGIMEN FISCAL SON DATOS NECESARIOS PARA ELABORAR LA FACTURA, SOLICITAR CORRECTAMENTE LOS DATOS AL CLIENTE ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       
                     }

                    
                }
                else if (panel_contenedor.Tag.Equals(panel4.Tag))
                {
                    visualizar_correos();
                }
                else if (panel_contenedor.Tag.Equals(panel5.Tag))
                {
                    if (validar_correos_nuevos())
                    {
                        unir_correos_nuevos();
                        mostrar_correos_rfc();
                    }
                }
                else if (panel_contenedor.Tag.Equals(panel6.Tag))
                {
                    if (validar_correos_text_field())
                    {
                        if (validar_correos_marcados())
                        {
                            if (correos_enviar.Count > 0)
                            {
                                set_panel(panel7);
                                worker_correo.RunWorkerAsync();
                            }
                            else
                            {
                                MessageBox.Show(this, "Para enviar correos es necesario ingresar y seleccionar almenos uno", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show(this, "Alguno de los correos seleccionados para enviar la factura esta vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else if(panel_contenedor.Tag.Equals(panel8.Tag))
                {
                  
                    set_panel(panel5);
                    txt_correos_nuevos.Focus();
                }
            }
        }

        bool validar_correos_marcados()
        {
            bool todo_correcto = true;

            if (chb_correo_1.Checked)
            {
                if (txt_correo_1.Text.Trim().Length == 0)
                {
                    todo_correcto = false;
                }
                else
                {
                    correos_enviar.Add(txt_correo_1.Text.Trim());
                }
            }

            if (chb_correo_2.Checked)
            {
                if (txt_correo_2.Text.Trim().Length == 0)
                {
                    todo_correcto = false;
                }
                else
                {
                    correos_enviar.Add(txt_correo_2.Text.Trim());
                }
            }

            if (chb_correo_3.Checked)
            {
                if (txt_correo_3.Text.Trim().Length == 0)
                {
                    todo_correcto = false;
                }
                else
                {
                    correos_enviar.Add(txt_correo_3.Text.Trim());
                }
            }

            if (chb_correo_4.Checked)
            {
                if (txt_correo_4.Text.Trim().Length == 0)
                {
                    todo_correcto = false;
                }
                else
                {
                    correos_enviar.Add(txt_correo_4.Text.Trim());
                }
            }

            return todo_correcto;
        }

        bool validar_correos_nuevos()
        {
            bool todos_correctos = true;
            string tmp = "";

            if(txt_correos_nuevos.Text.Trim().Length > 0)
            {
                string[] correos_split = txt_correos_nuevos.Text.Trim().Split('\n');
                List<string> correos_nuevos = new List<string>();

                if(correos_split.Length > 0)
                {
                    foreach(string correo in correos_split)
                    {
                        if(correo.Trim().Length > 0)
                        {
                            tmp = correo.TrimEnd('\r', '\n');
                            if(!Regex.IsMatch(tmp, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                            {
                                MessageBox.Show(this,"El correo \""+tmp+"\" no tiene un formato valido","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                todos_correctos = false;
                            }
                        }
                    }
                }
            }

            return todos_correctos;
        }

        bool validar_correos_text_field()
        {
            bool todos_correctos = true;

            if(txt_correo_1.Text.Trim().Length > 0)
            {
                todos_correctos = validar_correo_individual(txt_correo_1.Text.Trim());

                if(todos_correctos == false)
                {
                    MessageBox.Show(this,"El email \""+txt_correo_1.Text.Trim()+"\" no tiene un formato valido");
                }
            }

            if(todos_correctos)
            {
                if(txt_correo_2.Text.Trim().Length > 0)
                {
                    todos_correctos = validar_correo_individual(txt_correo_2.Text.Trim());

                    if (todos_correctos == false)
                    {
                        MessageBox.Show(this, "El email \"" + txt_correo_2.Text.Trim() + "\" no tiene un formato valido");
                    }
                }
            }

            if(todos_correctos)
            {
                if(txt_correo_3.Text.Trim().Length > 0)
                {
                    todos_correctos = validar_correo_individual(txt_correo_3.Text.Trim());

                    if (todos_correctos == false)
                    {
                        MessageBox.Show(this, "El email \"" + txt_correo_3.Text.Trim() + "\" no tiene un formato valido");
                    }
                }
            }

            if(todos_correctos)
            {
                if(txt_correo_4.Text.Trim().Length > 0)
                {
                    todos_correctos = validar_correo_individual(txt_correo_4.Text.Trim());

                    if (todos_correctos == false)
                    {
                        MessageBox.Show(this, "El email \"" + txt_correo_4.Text.Trim() + "\" no tiene un formato valido");
                    }
                }
            }

            return todos_correctos;
        }

        bool validar_correo_individual(string correo)
        {
            return Regex.IsMatch(correo, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        void visualizar_correos()
        {
            if (datos_emisor.correos_electronicos.Count == 0)
            {
                set_panel(panel5);
                txt_correos_nuevos.Focus();
            }
            else
            {
                mostrar_correos_rfc();
            }
        }

        void unir_correos_nuevos()
        {
            if(txt_correos_nuevos.Text.Trim().Length > 0)
            {
                string[] correos_split = txt_correos_nuevos.Text.Trim().Split('\n');
                List<string> correos_nuevos = new List<string>();

                if(correos_split.Length > 0)
                {
                    foreach(string correo in correos_split)
                    {
                        if(correo.Trim().Length > 0)
                        {
                            correos_nuevos.Add(correo);
                        }
                    }
                }

                datos_emisor.correos_electronicos = correos_nuevos;
            }
        }

        void limpiar_correos()
        {
            txt_correo_1.Text = "";
            txt_correo_2.Text = "";
            txt_correo_3.Text = "";
            txt_correo_4.Text = "";

            chb_correo_1.Checked = false;
            chb_correo_2.Checked = false;
            chb_correo_3.Checked = false;
            chb_correo_4.Checked = false;
        }

        void importar_correos_actuales()
        {
            txt_correos_nuevos.Text = "";

            foreach (string correo in datos_emisor.correos_electronicos)
            {
                if(correo.Trim().Length > 0)
                {
                    txt_correos_nuevos.AppendText(correo + "\n");
                }
            }
        }

        void mostrar_correos_rfc()
        {
            limpiar_correos();

            foreach (string correo in datos_emisor.correos_electronicos)
            {
                if (txt_correo_1.Text.Trim().Length == 0)
                {
                    if (correo.Trim().Length > 0)
                    {
                        txt_correo_1.Text = correo;
                        chb_correo_1.Checked = true;
                    }
                }
                else if (txt_correo_2.Text.Trim().Length == 0)
                {
                    if (correo.Trim().Length > 0)
                    {
                        txt_correo_2.Text = correo;
                        chb_correo_2.Checked = true;
                    }
                }
                else if (txt_correo_3.Text.Trim().Length == 0)
                {
                    txt_correo_3.Text = correo;
                    chb_correo_3.Checked = true;
                }
                else if (txt_correo_4.Text.Trim().Length == 0)
                {
                    txt_correo_4.Text = correo;
                    chb_correo_4.Checked = true;
                }
            }

            set_panel(panel6);
        }

        void validar_venta()
        {
            DAO_Ventas dao_ventas = new DAO_Ventas();
            var pago_tipos = dao_ventas.get_pago_tipos_venta(venta_id);

            condiciones_pago = "CONTADO";

            if(pago_tipos.Count > 1)
            {
                metodo_pago = "NO IDENTIFICADO";
                cuenta = "";
                show_importar_factura();
            }
            else
            {
               
                if(pago_tipos[0].es_credito)
                {
                    condiciones_pago = "CREDITO";

                    MessageBox.Show(this,"Esta venta salio a CREDITO, seleccione el metodo de pago y la cuenta","Venta a Credito",MessageBoxButtons.OK,MessageBoxIcon.Warning);

                    Cuenta_pago_tipo cuenta_pago_tipos = new Cuenta_pago_tipo();
                    cuenta_pago_tipos.ShowDialog();

                    if (!cuenta_pago_tipos.metodo_pago.Equals(""))
                    {
                        metodo_pago = cuenta_pago_tipos.metodo_pago;
                        cuenta = cuenta_pago_tipos.cuenta;
                        show_importar_factura();
                    }
                    else
                    {
                        set_panel(panel3);
                    }
                }
                else
                {

                    //AQUI SE CORRIGE LO DEL PREPAGO y REVOLVENTE POR CANCELACION
                    string nombrepago = pago_tipos[0].nombre.ToUpper();
                    if (nombrepago.Equals("ENCARGO PREPAGADO") || nombrepago.Equals("REVOLVENTE POR CANCELACION"))
                    {
                        MessageBox.Show(this, "Esta venta salio como  " + nombrepago + ", seleccione el metodo de pago ", "Venta por " + nombrepago, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        Cuenta_pago_tipo cuenta_pago_tipos = new Cuenta_pago_tipo();
                        cuenta_pago_tipos.ShowDialog();

                        if (!cuenta_pago_tipos.metodo_pago.Equals(""))
                        {
                            metodo_pago = cuenta_pago_tipos.metodo_pago;
                            cuenta = pago_tipos[0].cuenta;
                            show_importar_factura();
                        }
                        else
                        {
                            set_panel(panel3);
                        }
                    }
                    else
                    {
                        metodo_pago = pago_tipos[0].nombre.ToUpper();
                        cuenta = pago_tipos[0].cuenta;
                        show_importar_factura();
                    }
                    
                }
            }
        }

        void show_importar_factura()
        {
            btn_anterior.Enabled = false;

            set_panel(panel4);
            worker_timbrado.RunWorkerAsync();
        }

        private void btn_anterior_Click(object sender, EventArgs e)
        {
            if(panel_contenedor.Tag.Equals(panel1.Tag))
            {
                txt_folio_venta.Focus();
            }
            else if(panel_contenedor.Tag.Equals(panel2.Tag))
            {
                set_panel(panel1);
            }
            else if(panel_contenedor.Tag.Equals(panel3.Tag))
            {
                rdb_pge.Checked = false;
                rdb_pgm.Checked = false;
                rdb_rfc_propio.Checked = false;
                btn_siguiente.Enabled = false;
                set_panel(panel2);
            }else if(panel_contenedor.Tag.Equals(panel6.Tag))
            {
                set_panel(panel5);
                importar_correos_actuales();
                btn_anterior.Enabled = false;
                txt_correos_nuevos.Focus();
            }else if(panel_contenedor.Tag.Equals(panel8.Tag))
            {
                set_panel(panel1);
                txt_folio_venta.Focus();
            }
        }

        private void Facturacion_v12_Shown(object sender, EventArgs e)
        {
            set_panel(panel1);
            btn_anterior.Enabled = false;
            btn_siguiente.Enabled = false;
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdb_rfc_propio_CheckedChanged(object sender, EventArgs e)
        {
            validar_radio_rfc();
        }

        void rellenar_informacion_rfc()
        {
            //limpiar_informacion();

            DAO_Rfcs dao_rfc = new DAO_Rfcs();
            var rfc_registros = dao_rfc.get_data_rfc(rfc_registro_id);
            datos_emisor = rfc_registros;

            txt_estado.Text = rfc_registros.estado;
            txt_ciudad.Text = rfc_registros.ciudad;
            txt_municipio.Text = rfc_registros.municipio;
            txt_colonia.Text = rfc_registros.colonia;
            txt_codigo_postal.Text = rfc_registros.codigo_postal.ToString();

            txt_identificador_vista.Text = rfc_registro_id.ToUpper();
            txt_rfc_vista.Text = rfc_registros.rfc;
            txt_razon_social.Text = rfc_registros.razon_social.Replace("&", "&amp");
            txt_calle.Text = rfc_registros.calle;
            txt_numero_exterior.Text = rfc_registros.numero_exterior;
            txt_numero_interior.Text = rfc_registros.numero_interior;

            set_panel(panel3);

            btn_siguiente.Enabled = true;
        }

        void validar_radio_rfc()
        {
            if(rdb_rfc_propio.Checked)
            {
                Busqueda_rfcs busqueda_rfc = new Busqueda_rfcs(true);
                busqueda_rfc.ShowDialog();

                if (!busqueda_rfc.rfc_registro_id.Equals(""))
                {
                    rfc_registro_id = busqueda_rfc.rfc_registro_id;
                    rellenar_informacion_rfc();
                }
                else
                {
                    rdb_rfc_propio.Checked = false;
                }
            }
            else if(rdb_pgm.Checked)
            {
                DAO_Rfcs dao_rfc = new DAO_Rfcs();
                rfc_registro_id = dao_rfc.get_rfc_publico_general_mexicano().rfc_registro_id;
                rellenar_informacion_rfc();
                rellenar_informacion_rfc_sucursal();
            }else if(rdb_pge.Checked)
            {
                DAO_Rfcs dao_rfc = new DAO_Rfcs();
                rfc_registro_id = dao_rfc.get_rfc_publico_general_extrangero().rfc_registro_id;
                rellenar_informacion_rfc();
                rellenar_informacion_rfc_sucursal();
            }
        }

        void rellenar_informacion_rfc_sucursal()
        {
            DAO_Sucursales dao_sucursales = new DAO_Sucursales();
            var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(Config_helper.get_config_local("sucursal_id")));

            txt_calle.Text = sucursal_data.direccion;
            txt_colonia.Text = sucursal_data.colonia;
            txt_ciudad.Text = sucursal_data.ciudad;
            txt_estado.Text = sucursal_data.estado;
            txt_municipio.Text = sucursal_data.municipio;
            txt_codigo_postal.Text = sucursal_data.codigo_postal;
        }

        private void rdb_pgm_CheckedChanged(object sender, EventArgs e)
        {
            validar_radio_rfc();
        }

        private void rdb_pge_CheckedChanged(object sender, EventArgs e)
        {
            validar_radio_rfc();
        }

        private void worker_timbrado_DoWork(object sender, DoWorkEventArgs e)
        {

            string num_metodo_pago = "";
            switch (metodo_pago)
            { 
                case "EFECTIVO" :
                    num_metodo_pago = "01";
                break;
                case "CHEQUE" :
                    num_metodo_pago = "02";
                break;
                case "TRANSFERENCIA":
                    num_metodo_pago = "03";
                break;
                case "TARJETA CREDITO":
                    num_metodo_pago = "04";
                break;
                case "TARJETA DEBITO":
                    num_metodo_pago = "28";
                break;
                case  "VALES DESPENSA":
                    num_metodo_pago = "08";
                break;
                case "NO IDENTIFICADO":
                    num_metodo_pago = "99";
                break;
    
                default :
                    num_metodo_pago = metodo_pago;
                break;

            }

            try
            {
                (sender as BackgroundWorker).ReportProgress(5);
                DAO_Rfcs dao_rfc = new DAO_Rfcs();
                DTO_Rfc dto_rfc = new DTO_Rfc();
                dto_rfc.rfc_registro_id = txt_identificador_vista.Text;
                dto_rfc.calle = txt_calle.Text;
                dto_rfc.ciudad = txt_ciudad.Text;
                dto_rfc.codigo_postal = (txt_codigo_postal.Text.Trim().Equals("")) ? "0" : txt_codigo_postal.Text.ToString();
                dto_rfc.colonia = txt_colonia.Text;

                (sender as BackgroundWorker).ReportProgress(10);
                dto_rfc.estado = txt_estado.Text;
                dto_rfc.municipio = txt_municipio.Text;
                dto_rfc.numero_exterior = txt_numero_exterior.Text;
                dto_rfc.numero_interior = txt_numero_interior.Text;
                dto_rfc.pais = txt_pais.Text;
                dto_rfc.razon_social = txt_razon_social.Text;
                dto_rfc.rfc = txt_rfc_vista.Text;

                (sender as BackgroundWorker).ReportProgress(30);
                DAO_Ventas dao_ventas = new DAO_Ventas();
                var venta_data = dao_ventas.get_venta_data(venta_id);

                if (venta_data.corte_total_id > 0)
                {

                    MessageBox.Show(this, "EL FOLIO DE VENTA ESTA INCLUIDO EN LA VENTA GLOBAL DE DIAS ANTERIORES, IMPOSIBLE FACTURAR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    /*
                    //ESA VENTA ES DE OTRO DIA AL NO AACTUAL
                    DAO_Facturacion dao_facturacion = new DAO_Facturacion();

                    (sender as BackgroundWorker).ReportProgress(45);

                    string conector_txt_nota_credito = dao_ventas.get_informacion_nota_credito(venta_id, false);

                    var facturawsp_nc = WebServicePac_helper.importar((long)venta_data.corte_total_id, Misc_helper.EncodeTo64(conector_txt_nota_credito), true, false);

                    status_timbrado = facturawsp_nc.status;
                    mensaje_factura = facturawsp_nc.mensaje;

                    if (facturawsp_nc.status)
                    {
                        string[] correo_nc = { Config_helper.get_config_global("facturacion_vm_email") };

                        WebServicePac_helper.enviar((long)venta_data.corte_total_id, correo_nc, true, dao_facturacion.get_folio_nc());

                        dao_facturacion.registrar_nota_credito((long)venta_data.corte_total_id, venta_data.terminal_id, "CORTE");

                        (sender as BackgroundWorker).ReportProgress(60);

                        //string conector_txt_factura = dao_ventas.get_informacion_factura(venta_id, dto_rfc, correos, false);
                        
                        //string conector_txt_factura = dao_ventas.get_informacion_factura(venta_id, dto_rfc,condiciones_pago,metodo_pago,cuenta,string.Join(",", correos_enviar.ToArray()),false);
                       //se cambio la variable metodo_pago por el numero de metodo de pago
                        string conector_txt_factura = dao_ventas.get_informacion_factura(venta_id, dto_rfc, condiciones_pago, num_metodo_pago, cuenta, string.Join(",", correos_enviar.ToArray()), false);

                        var facturawsp_factura = WebServicePac_helper.importar(venta_id, Misc_helper.EncodeTo64(conector_txt_factura), false);

                        status_timbrado = facturawsp_factura.status;
                        mensaje_factura = facturawsp_factura.mensaje;
                        uuid_factura = facturawsp_factura.uuid;

                        (sender as BackgroundWorker).ReportProgress(80);

                        if (status_timbrado)
                        {
                            DialogResult dr_ticket = MessageBox.Show(this,"¿Deseas imprimir el ticket de la factura?","Imprimir Factura",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                            if(dr_ticket == DialogResult.Yes)
                            {
                                Facturacion ticket_factura = new Facturacion();
                                ticket_factura.construccion_ticket(venta_id, facturawsp_factura, false);
                                ticket_factura.print();   
                            }
                        }
                    }
                    */
                }
                else
                {
                    //string conector_txt = dao_ventas.get_informacion_factura(venta_id, dto_rfc, correos, false);
                    //string conector_txt = dao_ventas.get_informacion_factura(venta_id, dto_rfc, condiciones_pago, metodo_pago, cuenta, string.Join(",", correos_enviar.ToArray()), false);
                    //SE CAMBIO LA VARIABLE METODO DE PAGO por el NUMERO DE METODO DE PAGO
                    //string conector_txt = dao_ventas.get_informacion_factura(venta_id, dto_rfc, condiciones_pago, num_metodo_pago, cuenta, string.Join(",", correos_enviar.ToArray()), false);
                    string conector_txt = dao_ventas.get_informacion_factura_33(venta_id, dto_rfc, condiciones_pago, num_metodo_pago, cuenta, string.Join(",", correos_enviar.ToArray()), false,uso_del_cfdi,uso_regimen);
                    (sender as BackgroundWorker).ReportProgress(60);


                  //  string prueba = Misc_helper.EncodeTo64(conector_txt);
                   // return ;

                    var facturawsp = WebServicePac_helper.importar(venta_id, Misc_helper.Base64Encode(conector_txt), false);

                    status_timbrado = facturawsp.status;
                    mensaje_factura = facturawsp.mensaje;
                    uuid_factura = facturawsp.uuid;

                    (sender as BackgroundWorker).ReportProgress(80);

                    if (status_timbrado)
                    {
                        if (status_timbrado)
                        {
                            DialogResult dr_ticket = MessageBox.Show(this, "¿Deseas imprimir el ticket de la factura?", "Imprimir Factura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (dr_ticket == DialogResult.Yes)
                            {
                                Facturacion ticket_factura = new Facturacion();
                                ticket_factura.construccion_ticket(venta_id, facturawsp, false);
                                ticket_factura.print();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "ERROR : " + mensaje_factura, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                    }
                }

                (sender as BackgroundWorker).ReportProgress(100);
            }
            catch (Exception ex)
            {
                Log_error.log(ex);
            }
        }

        private void worker_timbrado_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pgb_timbrado.Value = e.ProgressPercentage;
        }

        private void worker_timbrado_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (status_timbrado)
            {
                MessageBox.Show(this, mensaje_factura, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_reintentar_factura.Enabled = false;
                visualizar_correos();
            }
            else
            {
                MessageBox.Show(this, mensaje_factura, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pgb_timbrado.Value = 0;
                btn_reintentar_factura.Enabled = true;
            }
        }

        private void worker_correo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<string> correos_nuevos = new List<string>();

                if (txt_correo_1.Text.Trim().Length > 0)
                {
                    correos_nuevos.Add(txt_correo_1.Text.Trim());
                }

                if (txt_correo_2.Text.Trim().Length > 0)
                {
                    correos_nuevos.Add(txt_correo_2.Text.Trim());
                }

                if (txt_correo_3.Text.Trim().Length > 0)
                {
                    correos_nuevos.Add(txt_correo_3.Text.Trim());
                }

                if (txt_correo_4.Text.Trim().Length > 0)
                {
                    correos_nuevos.Add(txt_correo_4.Text.Trim());
                }

                datos_emisor.correos_electronicos = correos_nuevos;

                btn_siguiente.Enabled = false;

                (sender as BackgroundWorker).ReportProgress(20);
                (sender as BackgroundWorker).ReportProgress(40);
                (sender as BackgroundWorker).ReportProgress(60);

                string[] correos_envios = correos_enviar.ToArray();

                var response = WebServicePac_helper.enviar(venta_id, correos_envios);

                (sender as BackgroundWorker).ReportProgress(80);
                (sender as BackgroundWorker).ReportProgress(100);

                status_envio_correos = response.status;
                btn_siguiente.Enabled = true;
            }
            catch (Exception ex)
            {
                Log_error.log(ex);
            }
        }

        private void worker_correo_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pgb_envio_correos.Value = e.ProgressPercentage;
        }

        private void worker_correo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (status_envio_correos)
            {
                MessageBox.Show(this, "Factura enviada correctamente a los destinatarios asignados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_siguiente.Text = "Finalizar";
                btn_siguiente.Focus();
            }
            else
            {
                MessageBox.Show(this, "Ocurrio un error al intentar enviar la factura, reintente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                set_panel(panel6);
            }
        }

        private void txt_correo_1_KeyUp(object sender, KeyEventArgs e)
        {
            chb_correo_1.Checked = txt_correo_1.Text.Trim().Length > 0 ? true : false;
        }

        private void txt_correo_2_KeyUp(object sender, KeyEventArgs e)
        {
            chb_correo_2.Checked = txt_correo_2.Text.Trim().Length > 0 ? true : false;
        }

        private void txt_correo_3_KeyUp(object sender, KeyEventArgs e)
        {
            chb_correo_3.Checked = txt_correo_3.Text.Trim().Length > 0 ? true : false;
        }

        private void txt_correo_4_KeyUp(object sender, KeyEventArgs e)
        {
            chb_correo_4.Checked = txt_correo_4.Text.Trim().Length > 0 ? true : false;
        }

        private void btn_reimprimir_factura_Click(object sender, EventArgs e)
        {
           
           // var objFactura = WebServicePac_helper.obtenerDatos(venta_id);

           // Facturacion ticket_factura = new Facturacion();
          //  ticket_factura.construccion_ticket(venta_id, objFactura, true);
           // ticket_factura.print();



            DAO_Ventas dao_ventas = new DAO_Ventas();
            DAO_Impresiones dao_impresiones = new DAO_Impresiones();
            
            DTO_Ventas_ticket venta_ticket = dao_ventas.get_informacion_ticket_venta(venta_id);
            long folio =  venta_ticket.venta_folio;
            string tipo = "FACTURACION";

            long terminal_id  = dao_impresiones.get_terminal_impresion_id(tipo, folio);
            if (terminal_id != 0)
                HELPERS.Print_new_helper.print(terminal_id, "", tipo, folio, true, false, false, true);
            else
            {

                MessageBox.Show(this, "Solicita la factura al departamento de credito y cobranza", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void txt_rfc_vista_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_razon_social_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_calle_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_numero_exterior_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_numero_interior_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_colonia_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_ciudad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_municipio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_estado_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_codigo_postal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txt_pais_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_folio_venta_TextChanged(object sender, EventArgs e)
        {

        }

      
        private void txt_correos_nuevos_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
