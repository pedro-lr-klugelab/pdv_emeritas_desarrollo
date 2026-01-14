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
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.FORMS.comunes;

namespace Farmacontrol_PDV.FORMS.ventas.refacturacion
{
	public partial class Refacturacion_principal : Form
	{
		long venta_id;
		DAO_Ventas dao_ventas = new DAO_Ventas();
        DTO_Rfc datos_emisor = new DTO_Rfc();
		DAO_Cancelaciones dao_cancelaciones = new DAO_Cancelaciones();
		FacturaWSP existe_factura =  new FacturaWSP();
		Panel panel_contenedor = new Panel();
        private bool status_envio_correos;
        private string condiciones_pago;
        private int nueva_venta;


        List<string> correos_enviar = new List<string>();

        bool emitir_cancelacion_factura = false;
		bool emitir_nota_credito = false;


        bool status_timbrado = false;
        private string metodo_pago;
        private string cuenta;
        string rfc_registro_id = "";

        string mensaje_factura = "";

        string uuid_factura = "";

		public Refacturacion_principal()
		{
         
			InitializeComponent();
            
            Refacturacion_principal.CheckForIllegalCrossThreadCalls = false;
			panel_contenedor = panel1;
			panel_contenedor.BringToFront();
			txt_ticket_venta.Focus();
            
		}

        void check_venta_abierta()
        {
            Form fc = Application.OpenForms["Ventas_principal"];

            if (fc != null){
                fc.Close();
            }
        }

		void siguiente_motivo()
		{
            limpiar_controles();
            set_panel(panel2);
            btn_atras.Enabled = true;
            /*btn_atras.Enabled = false;
            btn_siguiente.Enabled = false;

            rb_mismo_rfc.Checked = false;
            rb_otro_rfc.Checked = false;*/
		}

        void limpiar_controles()
        {
            rb_mismo_rfc.Checked = false;
            rb_otro_rfc.Checked = false;

            rdb_pge.Checked = false;
            rdb_pgm.Checked = false;
            rdb_rfc_propio.Checked = false;

            btn_atras.Enabled = false;
            btn_siguiente.Enabled = false;
        }

		void validar_ticket()
		{
			if (txt_ticket_venta.TextLength > 0)
			{
				string[] array_codigo = txt_ticket_venta.Text.Split('$');
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

								if (dao_cancelaciones.existe_cancelacion(venta_id))
								{
									MessageBox.Show(this, "Esta venta ha sido cancelada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
								}
								else
								{
									var venta_data = dao_ventas.get_venta_data(Convert.ToInt64(venta_id));

									if (venta_data.venta_id > 0)
									{
										if (venta_data.fecha_terminado != null)
										{
											if (venta_data.fecha_facturada != null)
											{
                                                existe_factura = WebServicePac_helper.existe_factura(venta_id);

												var mes_venta = Convert.ToDateTime(venta_data.fecha_terminado).Month;
												var mes_actual = Convert.ToDateTime(Misc_helper.fecha()).Month;

												if (mes_venta.Equals(mes_actual))
												{
													emitir_cancelacion_factura = true;
													siguiente_motivo();
												}
												else
												{
                                                    if (dao_ventas.existe_nota_credito(venta_id))
                                                    {
                                                        emitir_nota_credito = false;
                                                    }
                                                    else {
                                                        emitir_nota_credito = true;
                                                    }
                                                    siguiente_motivo();													
												}
											}
											else
											{
												MessageBox.Show(this, "Esta venta no ha sido facturada, facturar de manera normal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
											}
										}
										else
										{
											MessageBox.Show(this, "No puedes refacturar una venta NO terminada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
										}
									}
									else
									{
										txt_ticket_venta.SelectAll();
										MessageBox.Show(this, "Ticket de venta no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
										txt_ticket_venta.Focus();
									}
								}
							}
							else
							{
								MessageBox.Show(this, "Ticket de venta no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
						else
						{
							MessageBox.Show(this, "No puedes devolver esta venta, ya que fue hecha en otra sucursal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
		}

		private void txt_ticket_venta_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 13:
					validar_ticket();	
				break;
				case 27:
					txt_ticket_venta.Text = "";
				break;
			}
		}

        void set_panel(Panel panel)
        {
            panel_contenedor = panel;
            panel_contenedor.BringToFront();
            panel_contenedor.Refresh();
        }

		private void btn_siguiente_Click(object sender, EventArgs e)
		{
            if (btn_siguiente.Text.Trim().Equals("Finalizar"))
            {
                this.Close();
            }
            else if(panel_contenedor.Tag.Equals("panel1"))
			{
				validar_ticket();
                btn_atras.Enabled = false;
                btn_siguiente.Enabled = false;

                rb_otro_rfc.Checked = false;
                rb_mismo_rfc.Checked = false;
			}
			else if(panel_contenedor.Tag.Equals("panel2"))
			{
				if(rb_mismo_rfc.Checked == false && rb_otro_rfc.Checked == false)
				{
					MessageBox.Show(this,"Es necesario seleccionar el motivo de la refacturación","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
				else
				{
                    if (rb_mismo_rfc.Checked)
                    {
                        set_panel(panel4);
                    }
                    else
                    {
                        set_panel(panel3);
                        validar_radio_rfc();
                        btn_atras.Enabled = false;
                        btn_siguiente.Enabled = false;
                        rdb_pge.Checked = false;
                        rdb_pgm.Checked = false;
                        rdb_rfc_propio.Checked = false;
                    }
                    
                }
            }
            else if(panel_contenedor.Tag.Equals("panel3")){
                validar_radio_rfc();
            }
            else if (panel_contenedor.Tag.Equals("panel4")) {
                
                validar_venta();
            }
            else if (panel_contenedor.Tag.Equals("panel5"))
            {
                
                //refacturacion();
                visualizar_correos();
                btn_atras.Enabled = false;
            }
            else if (panel_contenedor.Tag.Equals("panel6"))
            {
                btn_atras.Enabled = true;
                if (validar_correos_nuevos())
                {
                    unir_correos_nuevos();
                    mostrar_correos_rfc();
                }
            }
            else if (panel_contenedor.Tag.Equals(panel7.Tag))
            {
                btn_atras.Enabled = true;
                if (validar_correos_text_field())
                {
                    if (validar_correos_marcados())
                    {
                        if (correos_enviar.Count > 0)
                        {
                            set_panel(panel8);
                            worker_correo.RunWorkerAsync();
                            //btn_siguiente.Text = "Finalizar";
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
            else if (panel_contenedor.Tag.Equals(panel9.Tag))
            {
                visualizar_correos();
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

                var response = WebServicePac_helper.enviar(nueva_venta, correos_envios);

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
                set_panel(panel7);
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

        void visualizar_correos()
        {
            if (datos_emisor.correos_electronicos.Count == 0)
            {
                set_panel(panel6);
                txt_correos_nuevos.Focus();
            }
            else
            {
                mostrar_correos_rfc();
            }
        }

        bool validar_correo_individual(string correo)
        {
            return Regex.IsMatch(correo, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        bool validar_correos_text_field()
        {
            bool todos_correctos = true;

            if (txt_correo_1.Text.Trim().Length > 0)
            {
                todos_correctos = validar_correo_individual(txt_correo_1.Text.Trim());

                if (todos_correctos == false)
                {
                    MessageBox.Show(this, "El email \"" + txt_correo_1.Text.Trim() + "\" no tiene un formato valido");
                }
            }

            if (todos_correctos)
            {
                if (txt_correo_2.Text.Trim().Length > 0)
                {
                    todos_correctos = validar_correo_individual(txt_correo_2.Text.Trim());

                    if (todos_correctos == false)
                    {
                        MessageBox.Show(this, "El email \"" + txt_correo_2.Text.Trim() + "\" no tiene un formato valido");
                    }
                }
            }

            if (todos_correctos)
            {
                if (txt_correo_3.Text.Trim().Length > 0)
                {
                    todos_correctos = validar_correo_individual(txt_correo_3.Text.Trim());

                    if (todos_correctos == false)
                    {
                        MessageBox.Show(this, "El email \"" + txt_correo_3.Text.Trim() + "\" no tiene un formato valido");
                    }
                }
            }

            if (todos_correctos)
            {
                if (txt_correo_4.Text.Trim().Length > 0)
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

            set_panel(panel7);
        }

        bool validar_correos_nuevos()
        {
            bool todos_correctos = true;
            string tmp = "";

            if (txt_correos_nuevos.Text.Trim().Length > 0)
            {
                string[] correos_split = txt_correos_nuevos.Text.Trim().Split('\n');
                List<string> correos_nuevos = new List<string>();

                if (correos_split.Length > 0)
                {
                    foreach (string correo in correos_split)
                    {
                        if (correo.Trim().Length > 0)
                        {
                            tmp = correo.TrimEnd('\r', '\n');
                            if (!Regex.IsMatch(tmp, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                            {
                                MessageBox.Show(this, "El correo \"" + tmp + "\" no tiene un formato valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                todos_correctos = false;
                            }
                        }
                    }
                }
            }

            return todos_correctos;
        }

        void unir_correos_nuevos()
        {
            if (txt_correos_nuevos.Text.Trim().Length > 0)
            {
                string[] correos_split = txt_correos_nuevos.Text.Trim().Split('\n');
                List<string> correos_nuevos = new List<string>();

                if (correos_split.Length > 0)
                {
                    foreach (string correo in correos_split)
                    {
                        if (correo.Trim().Length > 0)
                        {
                            correos_nuevos.Add(correo);
                        }
                    }
                }

                datos_emisor.correos_electronicos = correos_nuevos;
            }
        }
        private void worker_timbrado_DoWork(object sender, DoWorkEventArgs e)
        {
            btn_siguiente.Enabled = false;
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
                var venta_data = dao_ventas.get_venta_data(nueva_venta);

                if (venta_data.corte_total_id > 0)
                {
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
                        string conector_txt_factura = dao_ventas.get_informacion_factura(venta_id, dto_rfc, condiciones_pago, metodo_pago, cuenta, string.Join(",", correos_enviar.ToArray()), false);

                        var facturawsp_factura = WebServicePac_helper.importar(venta_id, Misc_helper.EncodeTo64(conector_txt_factura), false);

                        status_timbrado = facturawsp_factura.status;
                        mensaje_factura = facturawsp_factura.mensaje;
                        uuid_factura = facturawsp_factura.uuid;

                        (sender as BackgroundWorker).ReportProgress(80);

                        if (status_timbrado)
                        {
                            DialogResult dr_ticket = MessageBox.Show(this, "¿Deseas imprimir el ticket de la factura?", "Imprimir Factura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (dr_ticket == DialogResult.Yes)
                            {
                                Facturacion ticket_factura = new Facturacion();
                                ticket_factura.construccion_ticket(venta_id, facturawsp_factura, false);
                                ticket_factura.print();
                            }
                        }
                    }
                }

                else
                {
                    //string conector_txt = dao_ventas.get_informacion_factura(venta_id, dto_rfc, correos, false);
                    string conector_txt = dao_ventas.get_informacion_factura(venta_data.venta_id, dto_rfc, condiciones_pago, metodo_pago, cuenta, string.Join(",", correos_enviar.ToArray()), false);

                    (sender as BackgroundWorker).ReportProgress(60);

                    var facturawsp = WebServicePac_helper.importar(venta_data.venta_id, Misc_helper.EncodeTo64(conector_txt), false);

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
                                ticket_factura.construccion_ticket(venta_data.venta_id, facturawsp, false);
                                ticket_factura.print();
                            }
                        }
                    }
                }
                btn_siguiente.Enabled = true;
                (sender as BackgroundWorker).ReportProgress(100);
            }
            catch (Exception ex)
            {
                Log_error.log(ex);
            }
        }

        void validar_venta()
        {
            DAO_Ventas dao_ventas = new DAO_Ventas();
            var pago_tipos = dao_ventas.get_pago_tipos_venta(venta_id);

            condiciones_pago = "CONTADO";

            if (pago_tipos.Count > 1)
            {
                metodo_pago = "NO IDENTIFICADO";
                cuenta = "";
            }
            else
            {
                if (pago_tipos[0].es_credito)
                {
                    condiciones_pago = "CREDITO";

                    MessageBox.Show(this, "Esta venta salio a CREDITO, seleccione el metodo de pago y la cuenta", "Venta a Credito", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    Cuenta_pago_tipo cuenta_pago_tipos = new Cuenta_pago_tipo();
                    cuenta_pago_tipos.ShowDialog();

                    if (!cuenta_pago_tipos.metodo_pago.Equals(""))
                    {
                        metodo_pago = cuenta_pago_tipos.metodo_pago;
                        cuenta = cuenta_pago_tipos.cuenta;
                        //show_importar_factura();
                    }
                    else
                    {
                        set_panel(panel4);
                    }
                }
                else
                {
                    metodo_pago = pago_tipos[0].nombre.ToUpper();
                    cuenta = pago_tipos[0].cuenta;
                    //show_importar_factura();
                }
            }
            refacturacion();
            //set_panel(panel5);
        }

        void show_importar_factura()
        {
            //btn_atras.Enabled = false;

            set_panel(panel5);
            worker_timbrado.RunWorkerAsync();
        }

        void validar_radio_rfc()
        {
            if (rdb_rfc_propio.Checked)
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
            else if (rdb_pgm.Checked)
            {
                DAO_Rfcs dao_rfc = new DAO_Rfcs();
                rfc_registro_id = dao_rfc.get_rfc_publico_general_mexicano().rfc_registro_id;
                rellenar_informacion_rfc();
                rellenar_informacion_rfc_sucursal();
                show_panel4();
            }
            else if (rdb_pge.Checked)
            {
                DAO_Rfcs dao_rfc = new DAO_Rfcs();
                rfc_registro_id = dao_rfc.get_rfc_publico_general_extrangero().rfc_registro_id;
                rellenar_informacion_rfc();
                rellenar_informacion_rfc_sucursal();
                show_panel4();
            }
        }

        public void show_panel4()
        {
            set_panel(panel4);
            btn_atras.Enabled = true;
            btn_siguiente.Enabled = true;
        }

        public void show_panel3()
        {
            set_panel(panel3);
            btn_siguiente.Enabled = false;
            btn_atras.Enabled = true;
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

        void rellenar_informacion_rfc()
        {
            

            if (rfc_registro_id.Equals(""))
            {
                var venta_data = dao_ventas.get_venta_data(venta_id);

                string rfc_mex;
                string rfc_ext;
                DAO_Rfcs dao_rfc = new DAO_Rfcs();
                var objFactura = WebServicePac_helper.obtenerDatos(venta_data.venta_id);
                DAO_Rfcs dao_rfcs = new DAO_Rfcs();
                var data = dao_rfcs.get_data_rfc_rfc(objFactura.rfc_receptor);

                rfc_mex = dao_rfc.get_rfc_publico_general_mexicano().rfc_registro_id;
                rfc_ext = dao_rfc.get_rfc_publico_general_extrangero().rfc_registro_id;

                rfc_registro_id = ((data.rfc_registro_id == rfc_mex) ? rfc_mex : ((data.rfc_registro_id == rfc_ext) ? rfc_ext : data.rfc_registro_id));

                var rfc_registros = dao_rfc.get_data_rfc(rfc_registro_id);

                datos_emisor = rfc_registros;

                if (data.rfc_registro_id == rfc_mex || data.rfc_registro_id == rfc_ext)
                {
                    txt_identificador_vista.Text = data.rfc_registro_id;
                    txt_rfc_vista.Text = data.rfc;
                    txt_razon_social.Text = data.razon_social;
                    rellenar_informacion_rfc_sucursal();
                }
                else
                {
                    rellena_campos(rfc_registro_id);
                }
            }
            else
            {
                rellena_campos(rfc_registro_id);                
            }
            set_panel(panel4);
            txt_rfc_vista.Enabled = false;
            btn_atras.Enabled = true;
            btn_siguiente.Enabled = true;

            rdb_pge.Checked = false;
            rdb_pgm.Checked = false;
            rdb_rfc_propio.Checked = false;
        }

        void rellena_campos(string rfc_registro)
        {
            DAO_Rfcs dao_rfc = new DAO_Rfcs();
            var rfc_registros = dao_rfc.get_data_rfc(rfc_registro);
            datos_emisor = rfc_registros;
            if (rb_mismo_rfc.Checked == true)
            {
          
                txt_estado.Text = existe_factura.estado;
                txt_ciudad.Text = existe_factura.ciudad;
                txt_municipio.Text = existe_factura.municipio;
                txt_colonia.Text = existe_factura.colonia;
                txt_codigo_postal.Text = existe_factura.codigo_postal.ToString();

                txt_identificador_vista.Text = rfc_registro_id.ToUpper();
                txt_rfc_vista.Text = rfc_registros.rfc;
                txt_razon_social.Text = existe_factura.razon_social;
                txt_calle.Text = existe_factura.calle;
                txt_numero_exterior.Text = existe_factura.numero_exterior;
                txt_numero_interior.Text = existe_factura.numero_interior;
            }
            else
            {
                txt_estado.Text = rfc_registros.estado;
                txt_ciudad.Text = rfc_registros.ciudad;
                txt_municipio.Text = rfc_registros.municipio;
                txt_colonia.Text = rfc_registros.colonia;
                txt_codigo_postal.Text = rfc_registros.codigo_postal.ToString();

                txt_identificador_vista.Text = rfc_registro_id.ToUpper();
                txt_rfc_vista.Text = rfc_registros.rfc;
                txt_razon_social.Text = rfc_registros.razon_social;
                txt_calle.Text = rfc_registros.calle;
                txt_numero_exterior.Text = rfc_registros.numero_exterior;
                txt_numero_interior.Text = rfc_registros.numero_interior;
            }
        }

        void refacturacion() {
         
            Cursor = Cursors.WaitCursor;

            set_panel(panel5);

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            long empleado_id = (long)Principal.empleado_id;

            DTO_Validacion validacion = new DTO_Validacion();

            if (emitir_cancelacion_factura)
            {
                // cancela la factura
                var result_cancelacion = WebServicePac_helper.cancelar(venta_id);

                if (!result_cancelacion.status)
                {
                    //validacion.status = cancelar_nota();
                    validacion.informacion = "No pudo ser cancelada la nota de venta";
                    MessageBox.Show(this, validacion.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //validacion = dao_cancelaciones.cancelar_venta(Convert.ToInt64(venta_id), txt_motivo_cancelacion.Text, productos_no_cancelados, emitir_cancelacion_factura, emitir_nota_credito);
                }
                else
                {
                    validacion = result_cancelacion;
                    validacion.status = cancelar_nota();
                }
            }
            else
            {
                if (emitir_nota_credito)
                {
                    // genera nota de credito
                    var result_nota_credito = WebServicePac_helper.importar(venta_id, Misc_helper.EncodeTo64(dao_ventas.get_informacion_nota_credito(venta_id)), true);

                    if (result_nota_credito.status)
                    {
                        DAO_Facturacion dao_facturacion = new DAO_Facturacion();
                        var venta_data = dao_ventas.get_venta_data(venta_id);

                        // se registra la nota de credito
                        dao_facturacion.registrar_nota_credito(venta_id, venta_data.terminal_id, "VENTA");

                         existe_factura = WebServicePac_helper.existe_factura(venta_id);

                        if (existe_factura.status)
                        {
                            DAO_Rfcs dao = new DAO_Rfcs();
                            var rfc_existe = dao.existe_rfc(existe_factura.rfc_receptor);
                            var rfc_data = dao.get_data_rfc(rfc_existe.informacion);

                            if (rfc_data.tipo_rfc.Equals("RFC"))
                            {
                                var status_email = WebServicePac_helper.get_email_factura(existe_factura.factura_dato_fiscal_id);

                                if (status_email.status)
                                {
                                    string[] correos = { status_email.informacion };
                                    WebServicePac_helper.enviar(venta_id, correos, true);
                                }

                                Farmacontrol_PDV.CLASSES.PRINT.Facturacion ticket_factura = new Farmacontrol_PDV.CLASSES.PRINT.Facturacion();
                                ticket_factura.construccion_ticket(venta_id, result_nota_credito, false, true);
                                ticket_factura.print();
                            }
                        }

                        validacion.status = cancelar_nota();
                        //validacion.informacion = "No pudo ser cancelada la nota de venta";
                        //validacion = dao_cancelaciones.cancelar_venta(Convert.ToInt64(venta_id), txt_motivo_cancelacion.Text, productos_no_cancelados, emitir_cancelacion_factura, emitir_nota_credito);
                    }
                    else
                    {
                        validacion.status = false;
                        validacion.informacion = result_nota_credito.mensaje;

                        MessageBox.Show(this, validacion.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    validacion.status = cancelar_nota();
                    validacion.informacion = "No pudo ser cancelada la nota de venta";
                    //validacion = dao_cancelaciones.cancelar_venta(Convert.ToInt64(venta_id), txt_motivo_cancelacion.Text, productos_no_cancelados, emitir_cancelacion_factura, emitir_nota_credito);
                }
            }

            if (!validacion.status)
            {
                /*CLASSES.PRINT.Devolucion devolucion = new CLASSES.PRINT.Devolucion();
                devolucion.construccion_ticket(Convert.ToInt64(venta_id), false);
                devolucion.print();
            }
            else
            {*/
                MessageBox.Show(this, validacion.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Cursor = Cursors.Default;

            if (validacion.status == false)
            {
                MessageBox.Show(this, validacion.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                worker_timbrado.RunWorkerAsync();
            }
        }

		bool cancelar_nota()
		{
			var detallado_ventas =  dao_ventas.get_detallado_ventas(venta_id);

			List<Tuple<int,string,string,decimal,int>> lista_no_cancelados = new List<Tuple<int,string,string,decimal,int>>();

			foreach(var item in detallado_ventas)
			{
				foreach(Tuple<string,string,int> det in item.caducidades_lotes)
				{
					Tuple<int,string,string,decimal,int> no_cancelado = new Tuple<int,string,string,decimal,int>(
						item.articulo_id,
						det.Item1,
						det.Item2,
						item.importe,
						det.Item3
					);

					lista_no_cancelados.Add(no_cancelado);
				}
			}

			var cancelacion = dao_cancelaciones.cancelar_venta(venta_id,"CANCELACION POR REFACTURACION, MOTIVO: "+((rb_mismo_rfc.Checked) ? "DATOS ERRONEOS" : "RFC EQUIVOCADO"),lista_no_cancelados,emitir_cancelacion_factura,emitir_nota_credito);

			if(cancelacion.status)
			{
				Devolucion devolucion = new Devolucion();
				devolucion.construccion_ticket(Convert.ToInt64(venta_id),true);
				devolucion.print();

				//MessageBox.Show(this,"Cancelación de venta exitosa, proceda a facturar de manera normal el ticket que se ha generado","Información",MessageBoxButtons.OK,MessageBoxIcon.Information);
                nueva_venta = cancelacion.elemento_id;
                return true;
			}

            return false;
		}

		private void btn_atras_Click(object sender, EventArgs e)
		{
			if(panel_contenedor.Tag.Equals("panel2"))
			{
                set_panel(panel1);
				txt_ticket_venta.Text = "";
				txt_ticket_venta.Focus();

                limpiar_controles();
            }
            else if (panel_contenedor.Tag.Equals("panel"))
            {
                panel_contenedor = panel2;
                panel_contenedor.BringToFront();
                rdb_pge.Checked = false;
                rdb_pgm.Checked = false;
                rdb_rfc_propio.Checked = false;
            }
            else if (panel_contenedor.Tag.Equals("panel3"))
            {
                limpiar_controles();
                set_panel(panel2);
                btn_atras.Enabled = true;

            }
            else if (panel_contenedor.Tag.Equals("panel4"))
            {
                if (rb_mismo_rfc.Checked)
                {
                    limpiar_controles();
                    set_panel(panel2);
                    btn_atras.Enabled = true;
                }else{
                    set_panel(panel3);
                    btn_atras.Enabled = true;
                    btn_siguiente.Enabled = false;
                }
            }
            else if (panel_contenedor.Tag.Equals("panel5"))
            {
                panel_contenedor = panel4;
                panel_contenedor.BringToFront();
            }
            else if (panel_contenedor.Tag.Equals("panel6"))
            {
                panel_contenedor = panel5;
                panel_contenedor.BringToFront();
                btn_atras.Enabled = false;
            }
            else if (panel_contenedor.Tag.Equals("panel7"))
            {
                panel_contenedor = panel6;
                panel_contenedor.BringToFront();
                btn_atras.Enabled = false;
            }
            else if (panel_contenedor.Tag.Equals("panel8"))
            {
                panel_contenedor = panel7;
                panel_contenedor.BringToFront();
            }
            else if (panel_contenedor.Tag.Equals("panel9"))
            {
                panel_contenedor = panel8;
                panel_contenedor.BringToFront();
            }
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void txt_ticket_venta_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '$'))
			{
				e.Handled = true;
			}

			if ((e.KeyChar == '$'))
			{
				e.Handled = false;
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
        private void txt_ticket_venta_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void rdb_pgm_CheckedChanged(object sender, EventArgs e)
        {
            var valor = sender as RadioButton;
            string rfc_mex;
  
            string mensaje_factura;
            DAO_Rfcs dao_rfc = new DAO_Rfcs();
            if (valor.Checked == true)
            {
                rfc_mex = dao_rfc.get_rfc_publico_general_mexicano().rfc;
                if (existe_factura.rfc_receptor != rfc_mex)
                {
                    validar_radio_rfc();
                }
                else
                {
                    mensaje_factura = "Se seleccionó el mismo RFC que ya tiene la factura. ¿Desea seleccionar otro RFC?";
                    if (MessageBox.Show(this, mensaje_factura, "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        rdb_pge.Checked = false;
                        rdb_pgm.Checked = false;
                        rdb_rfc_propio.Checked = false;
                    }
                    else
                    {
                        limpiar_controles();
                        set_panel(panel2);
                    }
                }
            }
        }

        private void rdb_pge_CheckedChanged(object sender, EventArgs e)
        {
            var valor = sender as RadioButton;
            string rfc_ext;
            DAO_Rfcs dao_rfc = new DAO_Rfcs();
            if (valor.Checked == true)
            {
                rfc_ext = dao_rfc.get_rfc_publico_general_extrangero().rfc;
                if (existe_factura.rfc_receptor != rfc_ext)
                {
                    validar_radio_rfc();
                }
                
            }
        }

        private void rdb_rfc_propio_CheckedChanged(object sender, EventArgs e)
        {
            var valor = sender as RadioButton;
            if (valor.Checked == true)
            {
                validar_radio_rfc();
            }
        }

        private void rb_otro_rfc_CheckedChanged(object sender, EventArgs e)
        {
            var valor = sender as RadioButton;
            if (valor.Checked == true)
            {
                set_panel(panel3);
                btn_atras.Enabled = true;
            }
        }

        private void rb_mismo_rfc_CheckedChanged(object sender, EventArgs e)
        {
            var valor = sender as RadioButton;
            if (valor.Checked == true) 
            {
                DAO_Rfcs  dao_rfc =  new DAO_Rfcs();
                DTO_Rfc data_rfc = new DTO_Rfc();
                
                data_rfc = dao_rfc.get_data_rfc_rfc(existe_factura.rfc_receptor);

                if (!data_rfc.rfc_registro_id.Equals(""))
                {
                    rfc_registro_id = data_rfc.rfc_registro_id;

                    rellenar_informacion_rfc();
                    //rellenar_informacion_rfc_sucursal();

                    txt_identificador_vista.Enabled = false;
                    txt_rfc_vista.Enabled = false;

                    set_panel(panel4);
                }
                else
                {
                    MessageBox.Show(this, "El RFC de la factura no se encuentra registrado, imposible refacturar al mismo registro fiscal" , "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);

                    limpiar_controles();
                }
            }
        }

        private void txt_razon_social_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_calle_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_numero_exterior_TextChanged(object sender, EventArgs e)
        {

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

        private void txt_pais_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Refacturacion_principal_Shown(object sender, EventArgs e)
        {
            DAO_Ventas dao_ventas = new DAO_Ventas();
			var val = dao_ventas.get_venta_pendiente_corte();

            if (!val.status)
            {
                if (!val.informacion.Equals("CIERRE"))
                {
                    MessageBox.Show(this, "No se puede realizar una refacturacion ya que existe una venta pendiente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                else
                {
                    check_venta_abierta();
                }
            }
        }

        private void txt_razon_social_TextChanged(object sender, EventArgs e)
        {

        }
	}
}
