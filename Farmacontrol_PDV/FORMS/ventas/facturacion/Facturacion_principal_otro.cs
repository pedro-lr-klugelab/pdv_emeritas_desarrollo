using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.FORMS.catalogos.catalogo_rfc;
using Farmacontrol_PDV.FORMS.comunes;
using System.Text.RegularExpressions;
using Farmacontrol_PDV.DTO;
using System.Threading;
using Farmacontrol_PDV.CLASSES.PRINT;
using System.IO;
using System.Diagnostics;

namespace Farmacontrol_PDV.FORMS.ventas.facturacion
{
	public partial class Facturacion_principal_otro : Form
	{
		private Panel panel_contenedor = new Panel();
		private string rfc_registro_id = "";
		private string correos = "";
		private long venta_id;
		string uuid_factura = "";
		DTO_Factura factura_descargada = new DTO_Factura();
		string mensaje_factura = "";

		private bool status_timbrado = false;
		private bool status_envio_correos = false;
		private bool status_descarga_pdf = false;

		public Facturacion_principal_otro()
		{
			InitializeComponent();
			Facturacion_principal_otro.CheckForIllegalCrossThreadCalls = false;
		}

		private void Facturacion_principal_otro_Shown(object sender, EventArgs e)
		{
			panel_contenedor = panel1;
			panel_contenedor.BringToFront();
			chb_guardar_cambios.Checked = false;
			this.Refresh();
		}

		private void txt_folio_venta_KeyPress(object sender, KeyPressEventArgs e)
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

		private void txt_folio_venta_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt64(e.KeyCode);

			switch(keycode)
			{
				case 13:
					validar_codigo_entrada();
				break;
				case 27:
					txt_folio_venta.Text = "";
				break;
			}
		}

		void validar_codigo_entrada()
		{
			btn_reimprimir_factura.Visible = false;

			if(txt_folio_venta.Text.Trim().Length > 0)
			{
				string[] contenido = txt_folio_venta.Text.Split('$');
				long sucursal_id;

				if (contenido.Length.Equals(2))
				{
					if (Misc_helper.es_numero(contenido[0]) && Misc_helper.es_numero(contenido[1]))
					{
						sucursal_id = Convert.ToInt64(contenido[0]);
						venta_id = Convert.ToInt64(contenido[1]);

						DAO_Ventas dao_ventas = new DAO_Ventas();
						DAO_Sucursales dao_sucursales = new DAO_Sucursales();

						long sucursal_local = Convert.ToInt64(Config_helper.get_config_local("sucursal_id"));

						if (sucursal_local.Equals(sucursal_id))
						{
							if (dao_ventas.existe_venta(venta_id))
							{
								DAO_Facturacion dao_facturacion = new DAO_Facturacion();
								status_timbrado = dao_facturacion.existe_factura(venta_id);

								btn_reimprimir_factura.Visible = status_timbrado;

								bool venta_correcta = false;

								if (status_timbrado)
								{
									MessageBox.Show(this,"Esta venta ya fue facturada","Información",MessageBoxButtons.OK,MessageBoxIcon.Information);

									venta_correcta = true;

									panel_contenedor = panel3;
									panel_contenedor.BringToFront();
									panel_contenedor.Refresh();

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

									panel_contenedor = panel2;
									panel_contenedor.BringToFront();
									panel_contenedor.Refresh();

									txt_rfc_vista.Focus();

									/*

									if (informacion_venta.rfc_registro_id.Equals(""))
									{
										rdb_rfc_propio.Checked = true;
										txt_rfc_vista.Focus();
									}
									else
									{
										DAO_Rfcs dao_rfc = new DAO_Rfcs();
										var informacin_rfc = dao_rfc.get_data_rfc(informacion_venta.rfc_registro_id);
										txt_razon_social.Text = informacin_rfc.razon_social;
										txt_identificador_vista.Text = informacion_venta.rfc_registro_id.ToUpper();
										txt_rfc_vista.Text = informacin_rfc.rfc;

										rfc_registro_id = informacion_venta.rfc_registro_id;

										rellenar_informacion_rfc();
									}

									if (status_timbrado)
									{
										panel_contenedor = panel3;
										panel_contenedor.BringToFront();
										panel_contenedor.Refresh();
									}
									else
									{
										panel_contenedor = panel2;
										panel_contenedor.BringToFront();
										panel_contenedor.Refresh();

										txt_rfc_vista.Focus();
									}
									 * 
									 * */
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
					else
					{
						MessageBox.Show(this, "Codigo inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						txt_folio_venta.Text = "";
						txt_folio_venta.Focus();
					}
				}
				else
				{
					MessageBox.Show(this, "Codigo inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txt_folio_venta.Text = "";
					txt_folio_venta.Focus();
				}	
			}
			else
			{
				MessageBox.Show(this,"Codigo de venta vacio","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				txt_folio_venta.Focus();
			}
		}

		private void btn_inicio_Click(object sender, EventArgs e)
		{
			panel_contenedor = panel1;
			panel_contenedor.BringToFront();
			panel_contenedor.Refresh();
			txt_folio_venta.Text = "";
			txt_folio_venta.Focus();
		}

		private void btn_atras_Click(object sender, EventArgs e)
		{
			if(panel_contenedor.Tag.Equals(panel2.Tag))
			{
				panel_contenedor = panel1;
				txt_folio_venta.Text = "";
				txt_folio_venta.Focus();
			}
			else if (panel_contenedor.Tag.Equals(panel2.Tag))
			{
				panel_contenedor = panel2;
			}
			else if(panel_contenedor.Tag.Equals(panel3.Tag))
			{
				if (status_timbrado)
				{
					MessageBox.Show(this, "No puedes regresar a la paso anterior (EDICION) por que la venta ya fue facturada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					panel_contenedor = panel2;
				}
			}
			else if (panel_contenedor.Tag.Equals(panel4.Tag))
			{
				panel_contenedor = panel3;
			}
			else if (panel_contenedor.Tag.Equals(panel5.Tag))
			{
				panel_contenedor = panel3;
			}
			
			panel_contenedor.BringToFront();
			panel_contenedor.Refresh();
		}

		void registrar_rfc_venta()
		{
			if(rfc_registro_id != "")
			{
				/*DAO_Ventas dao_ventas = new DAO_Ventas();
				dao_ventas.registrar_rfc(venta_id, rfc_registro_id);*/
			}
		}

		private void btn_siguiente_Click(object sender, EventArgs e)
		{
            if (btn_siguiente.Text.Trim().Equals("Finalizar"))
            {
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
                    unir_correos();

                    if (txt_identificador_vista.Text.Trim().Length > 0 && txt_razon_social.Text.Trim().Length > 0 && txt_rfc_vista.Text.Trim().Length > 0 && txt_pais.Text.Trim().Length > 0 && txt_codigo_postal.Text.PadLeft(5,'0').Trim().Length == 5)
                    {
                        //actualizar_rfc();
                        registrar_rfc_venta();

                        if (rdb_rfc_propio.Checked == true && chb_guardar_cambios.Checked == false)
                        {
                            panel_contenedor = panel3;
                        }
                        else
                        {
                            panel_contenedor = panel3;
                        }
                    }
                    else if (txt_identificador_vista.Text.Trim().Length == 0 && txt_razon_social.Text.Trim().Length > 0 && txt_rfc_vista.Text.Trim().Length > 0 && txt_pais.Text.Trim().Length > 0 && txt_codigo_postal.Text.PadLeft(5,'0').Trim().Length == 5)
                    {
                        var result = registrar_rfc();

                        if (result.status)
                        {
                            txt_identificador_vista.Text = result.elemento_nombre;
                            rfc_registro_id = result.elemento_nombre;
                            rellenar_informacion_rfc();

                            DialogResult dr = MessageBox.Show(this, "¿La información capturada es correcta?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                            if (dr == DialogResult.Yes)
                            {
                                //registrar_rfc_venta();
                                procesar_factura();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Los siguientes campos son obligatorios: RFC, RAZON SOCIAL, CODIGO POSTAL y PAIS.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (panel_contenedor.Tag.Equals(panel3.Tag))
                {
                    //actualizar_rfc();

                    if (status_timbrado)
                    {
                        panel_correos();
                    }
                    else
                    {
                        procesar_factura();
                    }
                }

                panel_contenedor.BringToFront();
                panel_contenedor.Refresh();
            }
		}

		private void txt_busqueda_rfc_KeyDown(object sender, KeyEventArgs e)
		{
			
		}

		private void cbb_busqueda_TextChanged(object sender, EventArgs e)
		{
			/*
			if(cbb_busqueda.Text.Trim().Length > 0)
			{
				DAO_Rfcs dao_rfc = new DAO_Rfcs();

				AutoCompleteStringCollection datasource = new AutoCompleteStringCollection();
				datasource.AddRange(dao_rfc.get_rfc_registros_combobusqueda(cbb_busqueda.Text));

				cbb_busqueda.AutoCompleteCustomSource = datasource;
				cbb_busqueda.AutoCompleteMode = AutoCompleteMode.Suggest;
				cbb_busqueda.AutoCompleteSource = AutoCompleteSource.CustomSource;

				cbb_busqueda.Select(cbb_busqueda.Text.Length, 1);
			}
			 * */
		}

		private void cbb_busqueda_KeyDown(object sender, KeyEventArgs e)
		{
			/*
			if (cbb_busqueda.Text.Trim().Length > 0)
			{
				if (Convert.ToInt64(e.KeyCode) == 13)
				{
					buscar_rfc();
				}
			}
			 * */
		}

		void rellenar_informacion_rfc()
		{
			limpiar_informacion();

			DAO_Rfcs dao_rfc = new DAO_Rfcs();
			var rfc_registros = dao_rfc.get_data_rfc(rfc_registro_id);
			
			if(rfc_registros.tipo_rfc.Equals("RFC"))
			{
				rdb_rfc_propio.Checked = true;
			}else if(rfc_registros.tipo_rfc.Equals("PGM"))
			{
				rdb_pgm.Checked = true;
			}
			else
			{
				rdb_pge.Checked = true;
			}
			
			int count = 1;

			foreach(string correo in rfc_registros.correos_electronicos)
			{
				
				switch(count)
				{
					case 1:
						txt_correo1.Text = correo.Trim();
					break;
					case 2:
						txt_correo2.Text = correo.Trim();
					break;
					case 3:
						txt_correo3.Text = correo.Trim();
					break;
					case 4:
						txt_correo3.Text = correo.Trim();
					break; 
				}

				count++;
			}

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

		void rellenar_informacion_rfc_sucursal()
		{
			DAO_Sucursales dao_sucursales = new DAO_Sucursales();
			var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(Config_helper.get_config_local("sucursal_id")));

			txt_ciudad.Text = sucursal_data.ciudad;
			txt_estado.Text = sucursal_data.estado;
		}

		private void btn_nuevo_rfc_Click(object sender, EventArgs e)
		{
			txt_identificador_vista.Text = "";
			txt_rfc_vista.Text = "";
			txt_razon_social.Text = "";
			txt_calle.Text = "";
			txt_numero_exterior.Text = "";
			txt_numero_interior.Text = "";
			txt_estado.Text = "";
			txt_municipio.Text = "";
			txt_ciudad.Text = "";
			txt_colonia.Text = "";
			
			txt_rfc_vista.Focus();
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btn_buscar_rfc_Click(object sender, EventArgs e)
		{
			Busqueda_rfcs busqueda_rfc = new Busqueda_rfcs();
			busqueda_rfc.ShowDialog();

			if(!busqueda_rfc.rfc_registro_id.Equals(""))
			{
				rfc_registro_id = busqueda_rfc.rfc_registro_id;
				rellenar_informacion_rfc();
			}
		}

		private void txt_rfc_vista_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt64(e.KeyCode);

			switch(keycode)
			{
				case 114:
					
				break;
			}
		}

		private void Facturacion_principal_otro_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt64(e.KeyCode);

			switch(keycode)
			{
				case 114:
					if(panel_contenedor.Tag.Equals(panel2.Tag) && rdb_rfc_propio.Checked.Equals(true))
					{
						Busqueda_rfcs busqueda = new Busqueda_rfcs();
						busqueda.ShowDialog();

						if (!busqueda.rfc_registro_id.Equals(""))
						{
							rfc_registro_id = busqueda.rfc_registro_id;
							rellenar_informacion_rfc();
						}	
					}
				break;
			}
		}

		private void txt_rfc_vista_Leave(object sender, EventArgs e)
		{
			if(txt_rfc_vista.Text.Trim().Length > 0)
			{
				Regex regex = new Regex(@"([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])([A-Z0-9]){2}[0-9A]{1})");
				Match match = regex.Match(txt_rfc_vista.Text);

				if (match.Success)
				{
					DAO_Rfcs dao_rfc = new DAO_Rfcs();
					var existe_rfc = dao_rfc.existe_rfc(txt_rfc_vista.Text);

					if(existe_rfc.status)
					{
						var info_rfc = dao_rfc.get_data_rfc(existe_rfc.informacion);

						if(txt_identificador_vista.Text.Equals(""))
						{
							DialogResult dr = MessageBox.Show(this,string.Format("Este RFC pertenece a {0}, ¿Deseas usar su información?",info_rfc.razon_social),"RFC existente",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

							if(dr == DialogResult.Yes)
							{
								rfc_registro_id = existe_rfc.informacion;
								rellenar_informacion_rfc();	
							}
							else
							{
								txt_rfc_vista.Text = "";
								txt_rfc_vista.Focus();
							}
						}
						else if(!txt_identificador_vista.Text.Equals(existe_rfc.informacion))
						{
							MessageBox.Show(this, string.Format("No se puede usar este RFC por que esta asignado a otro registro ({0})", info_rfc.razon_social),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
							var rfc_anterior = dao_rfc.get_data_rfc(rfc_registro_id);
							txt_rfc_vista.Text = rfc_anterior.rfc;
							txt_rfc_vista.Focus();
						}
					}	
				}
				else
				{
					MessageBox.Show(this, "El RFC ingresado tiene errores de formato, verifique.", "RFC Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txt_rfc_vista.Focus();
				}
			}
		}

		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
		}

		private void rdb_pgm_CheckedChanged(object sender, EventArgs e)
		{
			if(rdb_pgm.Checked)
			{
				limpiar_informacion();
				DAO_Rfcs dao_rfc = new DAO_Rfcs();
				var informacion_pgm = dao_rfc.get_rfc_publico_general_mexicano();

				txt_rfc_vista.Text = informacion_pgm.rfc;
				txt_identificador_vista.Text = informacion_pgm.rfc_registro_id.ToUpper();
				rfc_registro_id = informacion_pgm.rfc_registro_id.ToUpper();
				txt_razon_social.Text = informacion_pgm.razon_social;
                txt_codigo_postal.Text = informacion_pgm.codigo_postal.ToString();

				txt_rfc_vista.Enabled = false;
				btn_buscar_rfc.Enabled = false;
				btn_nuevo_rfc.Enabled = false;

				chb_guardar_cambios.Visible = false;
				chb_guardar_cambios.Checked = false;
				rellenar_informacion_rfc_sucursal();
			}
		}

		private void rdb_pge_CheckedChanged(object sender, EventArgs e)
		{
			if (rdb_pge.Checked)
			{
				limpiar_informacion();
				DAO_Rfcs dao_rfc = new DAO_Rfcs();
				var informacion_pgm = dao_rfc.get_rfc_publico_general_extrangero();

				txt_rfc_vista.Text = informacion_pgm.rfc;
				txt_identificador_vista.Text = informacion_pgm.rfc_registro_id.ToUpper();
				rfc_registro_id = informacion_pgm.rfc_registro_id.ToUpper();
				txt_razon_social.Text = informacion_pgm.razon_social;
                txt_codigo_postal.Text = informacion_pgm.codigo_postal.ToString();

				txt_rfc_vista.Enabled = false;
				btn_buscar_rfc.Enabled = false;
				btn_nuevo_rfc.Enabled = false;

				chb_guardar_cambios.Visible = false;
				chb_guardar_cambios.Checked = false;
				rellenar_informacion_rfc_sucursal();
			}
		}

		private void rdb_rfc_propio_CheckedChanged(object sender, EventArgs e)
		{
			if (rdb_rfc_propio.Checked)
			{
				limpiar_informacion();

				txt_rfc_vista.Enabled = true;
				btn_buscar_rfc.Enabled = true;
				btn_nuevo_rfc.Enabled = true;

				txt_rfc_vista.Focus();

				chb_guardar_cambios.Visible = true;
				chb_guardar_cambios.Checked = false;
			}
		}

		void limpiar_informacion()
		{
			txt_identificador_vista.Text = "";
			txt_calle.Text = "";
			txt_ciudad.Text = "";
			txt_codigo_postal.Text = "";
			txt_colonia.Text = "";
			txt_estado.Text = "";
			txt_municipio.Text = "";
			txt_numero_exterior.Text = "";
			txt_numero_interior.Text = "";
			txt_razon_social.Text = "";
			txt_rfc_vista.Text = "";

			txt_correo1.Text = "";
			txt_correo2.Text = "";
			txt_correo3.Text = "";
			txt_correo4.Text = "";
		}

		DTO_Validacion actualizar_rfc()
		{
			unir_correos();
			DAO_Rfcs dao_rfc = new DAO_Rfcs();
			DTO_Rfc dto_rfc = new DTO_Rfc();

			dto_rfc.rfc_registro_id = txt_identificador_vista.Text;
			dto_rfc.calle = txt_calle.Text;
			dto_rfc.ciudad = txt_ciudad.Text;
			dto_rfc.codigo_postal = (txt_codigo_postal.Text.Trim().Equals("")) ? "00000" : txt_codigo_postal.Text.ToString();
			dto_rfc.colonia = txt_colonia.Text;
			dto_rfc.correos_electronicos = correos.Split(',').ToList();
			dto_rfc.estado = txt_estado.Text;
			dto_rfc.municipio = txt_municipio.Text;
			dto_rfc.numero_exterior = txt_numero_exterior.Text;
			dto_rfc.numero_interior = txt_numero_interior.Text;
			dto_rfc.pais = txt_pais.Text;
			dto_rfc.razon_social = txt_razon_social.Text;
			dto_rfc.rfc = txt_rfc_vista.Text;

			return dao_rfc.actualizar_rfc(dto_rfc);
		}

		DTO_Validacion registrar_rfc()
		{
			unir_correos();
			DAO_Rfcs dao_rfc = new DAO_Rfcs();
			DTO_Rfc dto_rfc = new DTO_Rfc();

			dto_rfc.rfc_registro_id = txt_identificador_vista.Text;
			dto_rfc.calle = txt_calle.Text;
			dto_rfc.ciudad = txt_ciudad.Text;
			dto_rfc.codigo_postal = txt_codigo_postal.Text.ToString().Trim().Length > 0 ? txt_codigo_postal.Text.ToString() : "0";
			dto_rfc.colonia = txt_colonia.Text;
			dto_rfc.correos_electronicos = correos.Split(',').ToList();
			dto_rfc.estado = txt_estado.Text;
			dto_rfc.municipio = txt_municipio.Text;
			dto_rfc.numero_exterior = txt_numero_exterior.Text;
			dto_rfc.numero_interior = txt_numero_interior.Text;
			dto_rfc.pais = txt_pais.Text;
			dto_rfc.razon_social = txt_razon_social.Text;
			dto_rfc.rfc = txt_rfc_vista.Text;

			return dao_rfc.registrar_rfc(dto_rfc);
		}

		private void txt_correo1_Leave(object sender, EventArgs e)
		{
			if(txt_correo1.Text.Trim().Length > 0)
			{
				if(!validar_email(txt_correo1.Text))
				{
					MessageBox.Show(this,"La dirección de correo "+txt_correo1.Text+" es inválida","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					txt_correo1.Focus();
				}
			}
		}

		private void txt_correo2_Leave(object sender, EventArgs e)
		{
			if (txt_correo2.Text.Trim().Length > 0)
			{
				if (!validar_email(txt_correo2.Text))
				{
					MessageBox.Show(this, "La dirección de correo " + txt_correo2.Text + " es inválida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txt_correo2.Focus();
				}
			}
		}

		private void txt_correo3_Leave(object sender, EventArgs e)
		{
			if (txt_correo3.Text.Trim().Length > 0)
			{
				if (!validar_email(txt_correo3.Text))
				{
					MessageBox.Show(this, "La dirección de correo " + txt_correo3.Text + " es inválida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txt_correo3.Focus();
				}
			}
		}

		private void txt_correo4_Leave(object sender, EventArgs e)
		{
			if (txt_correo4.Text.Trim().Length > 0)
			{
				if (!validar_email(txt_correo4.Text))
				{
					MessageBox.Show(this, "La dirección de correo " + txt_correo4.Text + " es inválida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txt_correo4.Focus();
				}
			}
		}

		public Boolean validar_email(String email)
		{
			String expresion;
			expresion = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
			  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
			if (Regex.IsMatch(email, expresion))
			{
				if (Regex.Replace(email, expresion, String.Empty).Length == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		private void txt_correo1_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = (e.KeyChar.Equals(' '));
		}

		private void txt_correo2_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = (e.KeyChar.Equals(' '));
		}

		private void txt_correo3_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = (e.KeyChar.Equals(' '));
		}

		private void txt_correo4_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = (e.KeyChar.Equals(' '));
		}

		public void unir_correos()
		{
			correos = "";

			if (txt_correo1.Text.Trim().Length > 0)
			{
				correos += (correos.Equals("")) ? txt_correo1.Text.Trim() : ", " + txt_correo1.Text.Trim();
			}

			if (txt_correo2.Text.Trim().Length > 0)
			{
				correos += (correos.Equals("")) ? txt_correo2.Text.Trim() : ", " + txt_correo2.Text.Trim();
			}

			if (txt_correo3.Text.Trim().Length > 0)
			{
				correos += (correos.Equals("")) ? txt_correo3.Text.Trim() : ", " + txt_correo3.Text.Trim();
			}

			if (txt_correo4.Text.Trim().Length > 0)
			{
				correos += (correos.Equals("")) ? txt_correo4.Text.Trim() : ", " + txt_correo4.Text.Trim();
			}
		}

		public void procesar_factura()
		{
			panel_contenedor = panel4;
			panel_contenedor.BringToFront();
			panel_contenedor.Refresh();

			pgb_timbrado.Value = 0;
			unir_correos();

			worker_timbrado_factura.RunWorkerAsync();
		}

		private void worker_timbrado_factura_DoWork(object sender, DoWorkEventArgs e)
		{
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
				dto_rfc.correos_electronicos = correos.Split(',').ToList();

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
                    /*
                    DAO_Facturacion dao_facturacion = new DAO_Facturacion();

                    (sender as BackgroundWorker).ReportProgress(45);

                    string conector_txt_nota_credito = dao_ventas.get_informacion_nota_credito(venta_id,false);

                    //MessageBox.Show(conector_txt_nota_credito);

                    var facturawsp_nc = WebServicePac_helper.importar((long)venta_data.corte_total_id, Misc_helper.EncodeTo64(conector_txt_nota_credito),true,false);

                    status_timbrado = facturawsp_nc.status;
                    mensaje_factura = facturawsp_nc.mensaje;

                    if (facturawsp_nc.status)
                    {
                     **/
                        string[] correo_nc = { Config_helper.get_config_global("facturacion_vm_email") };

                        //WebServicePac_helper.enviar((long)venta_data.corte_total_id, correo_nc, true);

                        //dao_facturacion.registrar_nota_credito((long)venta_data.corte_total_id, venta_data.terminal_id, "CORTE");

                        (sender as BackgroundWorker).ReportProgress(60);

                        //string conector_txt_factura = dao_ventas.get_informacion_factura(venta_id, dto_rfc, correos, false);

                        string conector_txt_factura = "";

                        var facturawsp_factura = WebServicePac_helper.importar(venta_id, Misc_helper.EncodeTo64(conector_txt_factura), false);

                        status_timbrado = facturawsp_factura.status;
                        mensaje_factura = facturawsp_factura.mensaje;
                        uuid_factura = facturawsp_factura.uuid;

                        (sender as BackgroundWorker).ReportProgress(80);

                        if (status_timbrado)
                        {
                            Facturacion ticket_factura = new Facturacion();
                            ticket_factura.construccion_ticket(venta_id, facturawsp_factura, false);
                            ticket_factura.print();
                        }
                    //}
                }
                else
                {
                    //string conector_txt = dao_ventas.get_informacion_factura(venta_id, dto_rfc, correos, false);
                    string conector_txt = "";
                    (sender as BackgroundWorker).ReportProgress(60);

                    var facturawsp = WebServicePac_helper.importar(venta_id, Misc_helper.EncodeTo64(conector_txt), false);

                    status_timbrado = facturawsp.status;
                    mensaje_factura = facturawsp.mensaje;
                    uuid_factura = facturawsp.uuid;

                    (sender as BackgroundWorker).ReportProgress(80);

                    if (status_timbrado)
                    {
                        Facturacion ticket_factura = new Facturacion();
                        ticket_factura.construccion_ticket(venta_id, facturawsp, false);
                        ticket_factura.print();
                    }
                }

				(sender as BackgroundWorker).ReportProgress(100);
			}
			catch(Exception ex)
			{
				Log_error.log(ex);
			}
		}

		private void worker_timbrado_factura_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			pgb_timbrado.Value = e.ProgressPercentage;
		}

		private void worker_timbrado_factura_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if(status_timbrado)
			{
				MessageBox.Show(this,mensaje_factura,"Información",MessageBoxButtons.OK,MessageBoxIcon.Information);
				btn_reintentar_factura.Enabled = false;
				panel_correos();
			}
			else
			{
				MessageBox.Show(this, mensaje_factura, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				pgb_timbrado.Value = 0;
				btn_reintentar_factura.Enabled = true;
			}
		}

		void registrar_venta_facturada()
		{
			DAO_Ventas dao_ventas = new DAO_Ventas();
		}

		private void btn_reintentar_factura_Click(object sender, EventArgs e)
		{
			procesar_factura();
		}

		void panel_correos()
		{
			txt_ec_correo1.Text = txt_correo1.Text;
			txt_ec_correo2.Text = txt_correo2.Text;
			txt_ec_correo3.Text = txt_correo3.Text;
			txt_ec_correo4.Text = txt_correo4.Text;

			chb_correo_1.Enabled = (txt_correo1.Text.Length > 0);
			chb_correo2.Enabled = (txt_correo2.Text.Length > 0);
			chb_correo3.Enabled = (txt_correo3.Text.Length > 0);
			chb_correo4.Enabled = (txt_correo4.Text.Length > 0);

			panel_contenedor = panel5;
			panel_contenedor.BringToFront();
			panel_contenedor.Refresh();

            btn_atras.Enabled = false;
            btn_siguiente.Text = "Finalizar";
            btn_cancelar.Enabled = false;
            btn_inicio.Enabled = false;
		}

		void procesar_correos()
		{
			pgb_envio_correos.Value = 0;
			worker_envio_correos.RunWorkerAsync();
		}

		void process_descargar_pdf()
		{
			//pgb_descargar_pdf.Value = 0;
			worker_descargar_pdf.RunWorkerAsync();
		}

		private void worker_envio_correos_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				(sender as BackgroundWorker).ReportProgress(20);

				string correos = "";

				if(chb_correo_1.Checked)
				{
					correos += (correos.Equals("")) ? txt_correo1.Text : ", "+txt_correo1.Text;
				}

				if (chb_correo2.Checked)
				{
					correos += (correos.Equals("")) ? txt_correo2.Text : ", " + txt_correo2.Text;
				}

				(sender as BackgroundWorker).ReportProgress(40);

				if (chb_correo3.Checked)
				{
					correos += (correos.Equals("")) ? txt_correo3.Text : ", " + txt_correo3.Text;
				}

				if (chb_correo4.Checked)
				{
					correos += (correos.Equals("")) ? txt_correo4.Text : ", " + txt_correo4.Text;
				}

				(sender as BackgroundWorker).ReportProgress(60);

				string[] correos_envios = correos.Split(',');

				var response = WebServicePac_helper.enviar(venta_id, correos_envios);

				(sender as BackgroundWorker).ReportProgress(80);
				(sender as BackgroundWorker).ReportProgress(100);


				status_envio_correos = response.status;
			}
			catch (Exception ex)
			{
				Log_error.log(ex);
			}
		}

		private void worker_envio_correos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if(status_envio_correos)
			{
				MessageBox.Show(this,"Factura enviada correctamente a los destinatarios asignados","Información",MessageBoxButtons.OK,MessageBoxIcon.Information);
				panel_correos();
			}
			else
			{
				MessageBox.Show(this, "Ocurrio un error al intentar enviar la factura, reintente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				pgb_envio_correos.Value = 0;
				btn_reintentar_factura.Enabled = true;
				panel_contenedor.Refresh();
			}
		}

		private void btn_enviar_correos_Click(object sender, EventArgs e)
		{
			if(!txt_ec_correo1.Text.Equals("") || !txt_ec_correo1.Text.Equals("") || !txt_ec_correo1.Text.Equals("") || !txt_ec_correo1.Text.Equals(""))
			{
				if(chb_correo_1.Enabled || chb_correo2.Enabled || chb_correo3.Enabled || chb_correo4.Enabled)
				{
					if (chb_correo_1.Checked.Equals(true) || chb_correo2.Checked.Equals(true) || chb_correo3.Checked.Equals(true) || chb_correo4.Checked.Equals(true))
					{
						procesar_correos();
					}
					else
					{
						MessageBox.Show(this, "Para enviar la factura es necesario seleccionar minimo un destinatario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		private void worker_descargar_pdf_DoWork(object sender, DoWorkEventArgs e)
		{
			
		}

		private void worker_descargar_pdf_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (status_descarga_pdf)
			{
				MessageBox.Show(this, "PDF de factura descargada correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
				save_dialog_factura.FileName = factura_descargada.nombre_archivo;

				save_dialog_factura.Filter = "PDF Files | *.pdf";
				save_dialog_factura.DefaultExt = "pdf";

				save_dialog_factura.ShowDialog();
			}
			else
			{
				MessageBox.Show(this, "Ocurrio un error al intentar descargar la factura, reintente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				//pgb_descargar_pdf.Value = 0;
			}
		}

		private void btn_descargar_pdf_Click(object sender, EventArgs e)
		{
			process_descargar_pdf();
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

		private void worker_descargar_pdf_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			//pgb_descargar_pdf.Value = e.ProgressPercentage;
		}

		private void worker_envio_correos_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			pgb_envio_correos.Value = e.ProgressPercentage;
		}

		private void save_dialog_factura_FileOk(object sender, CancelEventArgs e)
		{
			try
			{
				string nombre = save_dialog_factura.FileName;
				byte[] archivo = Convert.FromBase64String(factura_descargada.pdf_base64);
				File.WriteAllBytes(nombre, archivo);
				Process.Start(nombre);
			}
			catch(Exception)
			{
				MessageBox.Show(this, "Archivo en uso, por favor cierre el documento e intente de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btn_reimprimir_factura_Click(object sender, EventArgs e)
		{
			var objFactura = WebServicePac_helper.obtenerDatos(venta_id);

			Facturacion ticket_factura = new Facturacion();
			ticket_factura.construccion_ticket(venta_id, objFactura, true);
			ticket_factura.print();
		}

	}
}
