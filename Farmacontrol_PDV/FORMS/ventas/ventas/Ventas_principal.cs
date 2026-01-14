using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.CLASSES;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.FORMS.ventas.devolucion;
using Farmacontrol_PDV.FORMS.ventas.tae;
using Farmacontrol_PDV.tae_diestel;

namespace Farmacontrol_PDV.FORMS.ventas.ventas
{
	public partial class Ventas_principal : Form
	{
		private int? empleado_id = null;
		private long? venta_id = null;
        public string numero_referencia;
        public string sku;
        public int numero_transaccion;
        public long monto;
        public bool es_tae;
		DAO_Articulos dao_articulos = new DAO_Articulos();
		DAO_Ventas dao_ventas = new DAO_Ventas();
        DAO_Exclusivos dao_exclusivos = new DAO_Exclusivos();

		public long? cupon_id = null;
        long sucursal_local = 0;

		public Ventas_principal()
		{
			InitializeComponent();
            this.venta_id = null;
		}

		public void crear_venta(bool tomar_empleado_sesion = false)
		{
            empleado_id = Convert.ToInt32(this.Tag);
            venta_id = dao_ventas.registrar_venta((int)empleado_id, tomar_empleado_sesion);
            rellenar_informacion_venta();
            sucursal_local = Convert.ToInt64(Config_helper.get_config_local("sucursal_id"));
		}

		public void limpiar_venta()
		{
			dao_ventas.limpiar_venta((long)venta_id);
			rellenar_informacion_venta();
		}

		public void agregar_producto_gratis(int piezas_gratis, DataTable articulos_existentes)
		{
			foreach (DataRow row in articulos_existentes.Rows)
			{
				int existencia_vendible = Convert.ToInt32(row["existencia_vendible"]);

				if (piezas_gratis > 0)
				{
					if (existencia_vendible > 0)
					{
						if (existencia_vendible > piezas_gratis)
						{
							dao_ventas.insertar_detallado(row["amecop"].ToString(), row["caducidad"].ToString(), row["lote"].ToString(), piezas_gratis, (long)venta_id, true);
							piezas_gratis = 0;
						}
						else
						{
							dao_ventas.insertar_detallado(row["amecop"].ToString(), row["caducidad"].ToString(), row["lote"].ToString(), existencia_vendible, (long)venta_id, true);
							piezas_gratis = piezas_gratis - existencia_vendible;
						}
					}
				}
			}


            get_productos_venta();

			if (piezas_gratis != 0)
			{
				MessageBox.Show(this, "" + piezas_gratis + " piezas gratis no pudieron ser agregadas por falta de existencias");
			}
		}

        void get_productos_venta()
        {
            dgv_ventas.DataSource = dao_ventas.get_productos_venta((long)venta_id);
            get_totales();
            validar_color_grid();
            dgv_ventas.ClearSelection();
            txt_amecop.Focus();
        }

        void validar_color_grid()
        {
            foreach (DataGridViewRow row in dgv_ventas.Rows)
            {
                if (Convert.ToBoolean(row.Cells["es_antibiotico"].Value))
                {
                    dgv_ventas.Rows[row.Index].DefaultCellStyle.BackColor = Color.FromArgb(179, 206, 252);
                }
                else
                {
                    dgv_ventas.Rows[row.Index].DefaultCellStyle.BackColor = Color.Empty;
                }
            }
        }

		public void validar_promocion_producto(int articulo_id, bool eliminar = false)
		{
			if (eliminar)
			{
				dao_ventas.eliminar_producto_gratis(articulo_id, (long)venta_id);
			}

			DataTable promocion = dao_articulos.validar_producto_promocion(articulo_id);

			if (promocion.Rows.Count > 0)
			{
				var productos_sin_promocion = dao_ventas.get_productos_sin_promocion(articulo_id, (long)venta_id);

				int cantidad_minima = Convert.ToInt32(promocion.Rows[0]["cantidad_minima"]);
				int cantidad_gratis = Convert.ToInt32(promocion.Rows[0]["cantidad_gratis"]);
				int piezas_gratis = 0;

				if (productos_sin_promocion >= cantidad_minima)
				{
					dao_ventas.eliminar_producto_gratis(articulo_id, (long)venta_id);

					piezas_gratis = Math.Abs((productos_sin_promocion / cantidad_minima) * cantidad_gratis);

					DialogResult dr = MessageBox.Show("Producto con promocion, piezas gratis " + piezas_gratis + ", ¿desea tomar la promocion?", "Promocion", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

					if (dr == DialogResult.Yes)
					{
						agregar_producto_gratis(piezas_gratis, dao_articulos.get_existencia_articulo(articulo_id));
					}
				}
			}

            get_productos_venta();
		}

		public void get_totales()
		{
			DataTable calculo_totales = dgv_ventas.DataSource as DataTable;

			decimal total = 0;
			decimal subtotal = 0;
			int piezas = 0;
			decimal iva = 0;
			decimal ieps = 0;
			decimal gravado = 0;
			decimal exento = 0;

			foreach (DataRow row in calculo_totales.Rows)
			{
				total += Convert.ToDecimal(row["total"]);
				subtotal += Convert.ToDecimal(row["subtotal"]);
				iva += Convert.ToDecimal(row["importe_iva"]);
				ieps += Convert.ToDecimal(row["importe_ieps"]);
				piezas += Convert.ToInt32(row["cantidad"]);

				if (Convert.ToDecimal(row["importe_iva"]) > ((decimal)0))
				{
					gravado += Convert.ToDecimal(row["subtotal"]);
				}
				else
				{
					exento += Convert.ToDecimal(row["subtotal"]);
				}
			}

            float precio_dolar = (float)dao_ventas.get_tipo_cambio();
            float dolar = (float)total / precio_dolar;


			txt_total.Text = total.ToString("C2");
			txt_subtotal.Text = string.Format("{0:C2}", subtotal);
			txt_ieps.Text = string.Format("{0:C2}", ieps);
			txt_iva.Text = string.Format("{0:C2}", iva);
			txt_piezas.Text = piezas.ToString();
			txt_excento.Text = string.Format("{0:C2}", exento);
			txt_gravado.Text = string.Format("{0:C2}", gravado);
            txtboxDollar.Text = string.Format("{0:C2}", dolar);

		}

		public void insertar_producto_venta()
		{
			ComboBoxItem item_cad = (ComboBoxItem)cbb_caducidad.SelectedItem;
			ComboBoxItem item_lote = (ComboBoxItem)cbb_lote.SelectedItem;

            int articulo_id = (int)item_cad.elemento_id;
            bool valido = dao_exclusivos.valida_exclusivo(sucursal_local,articulo_id);

            if (valido)
            {
                int existencia_vendible = dao_articulos.get_existencia_vendible(txt_amecop.Text, (item_cad.Value.ToString() == " ") ? "0000-00-00" : item_cad.Value.ToString(), item_lote.Value.ToString());

                if (existencia_vendible >= Convert.ToInt64(txt_cantidad.Text))
                {
                    DataTable result_insert = dao_ventas.insertar_detallado(txt_amecop.Text, item_cad.Value.ToString(), item_lote.Value.ToString(), Convert.ToInt64(txt_cantidad.Text), (long)venta_id);

                    limpiar_informacion();

                    if (result_insert.Rows.Count > 0)
                    {
                        dgv_ventas.DataSource = result_insert;
                        dgv_ventas.ClearSelection();
                        //validar_promocion_producto(articulo_id);
                        validar_color_grid();
                        txt_amecop.Focus();
                    }
                    else
                    {
                        MessageBox.Show(this, "Ocurrio un problema al intentar registrar el producto, notifica a tu administrador!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    get_totales();
                }
                else
                {
                    MessageBox.Show(this, "La cantidad disponible para vender es de " + existencia_vendible, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                //CODIGO PARTICIPANTE Y SUCURSAL NO PARTICIPANTE
                MessageBox.Show(this, "El codigo no puede ser agregado a la cotizacion porque esta sucursal no esta permitida para su venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

		}

		public void limpiar_informacion()
		{
			txt_amecop.Enabled = true;
			txt_cantidad.Text = "";

			txt_amecop.Text = "";

			cbb_caducidad.Items.Clear();
			cbb_lote.Items.Clear();

			txt_descuento.Text = "";
			txt_producto.Text = "";

			txt_amecop.Focus();
			txt_cantidad.Enabled = false;
		}

		private void txt_cantidad_KeyDown(object sender, KeyEventArgs e)
		{
			if (Convert.ToInt32(e.KeyCode) == 27)
			{
                cbb_lote.Enabled = true;
                cbb_lote.Focus();
                cbb_lote.DroppedDown = true;
                txt_cantidad.Text = "1";
                txt_cantidad.Enabled = false;
			}

			if (Convert.ToInt32(e.KeyCode) == 13 && txt_cantidad.TextLength > 0)
			{
				insertar_producto_venta();
			}
		}

		public void busqueda_lotes(ComboBoxItem item)
		{
			DataTable result_lotes = dao_articulos.get_lotes((int)item.elemento_id, item.Value.ToString());
			cbb_lote.Enabled = true;

			if (result_lotes.Rows.Count > 0)
			{
				cbb_lote.Items.Clear();
				foreach (DataRow row in result_lotes.Rows)
				{
					ComboBoxItem item_lote = new ComboBoxItem();
					item_lote.Text = row["lote"].ToString();
					item_lote.Value = row["lote"].ToString();
					item.elemento_id = item.elemento_id;
					cbb_lote.Items.Add(item_lote);
				}
			}
			else
			{
				ComboBoxItem item_lote = new ComboBoxItem();
				item_lote.Text = "SIN LOTE";
				item_lote.Value = " ";
				item.elemento_id = item.elemento_id;
				cbb_lote.Items.Add(item_lote);
			}

			cbb_lote.DroppedDown = true;
			cbb_lote.SelectedIndex = 0;
			cbb_lote.Focus();
		}

		public void rellenar_informacion_venta()
		{
			var venta_data = dao_ventas.get_venta_data((long)venta_id);
			lbl_nombre_terminal.Text = Misc_helper.get_nombre_terminal().ToUpper();
			txt_nombre_empleado.Text = venta_data.nombre_empleado;
			lbl_folio.Text = "VTA #"+venta_data.venta_folio;
			txt_fcid_empleado.Text = venta_data.empleado_fcid;
			txt_servicio_domicilio.Text = venta_data.servicio_domicilio;
			txt_venta_credito.Text = venta_data.nombre_cliente_credito;
            txt_comentarios.Text = venta_data.comentarios;

            get_productos_venta();

			txt_amecop.Enabled = ((venta_data.cupon_id != null) ? false : true);
            dgv_ventas.Enabled = ((venta_data.cupon_id != null) ? false : true);

			long? nullable = null;
			cupon_id = (venta_data.cupon_id != null) ? Convert.ToInt64(venta_data.cupon_id) : nullable;
			dgv_ventas.ClearSelection();
            validar_color_grid();
		}

		private void cbb_caducidad_KeyDown(object sender, KeyEventArgs e)
		{
			if (Convert.ToInt32(e.KeyCode) == 13)
			{
				ComboBoxItem item = (ComboBoxItem)cbb_caducidad.SelectedItem;
				busqueda_lotes(item);
				cbb_caducidad.Text = item.Text;
				cbb_caducidad.Enabled = false;
			}
			else if (Convert.ToInt32(e.KeyCode) == 27)
			{
				txt_amecop.Enabled = true;
				txt_amecop.Focus();
				txt_producto.Text = "";
				cbb_caducidad.Items.Clear();
				cbb_caducidad.Enabled = false;
			}
		}

		private void cbb_lote_KeyDown(object sender, KeyEventArgs e)
		{
			if (Convert.ToInt32(e.KeyCode) == 13)
			{
				ComboBoxItem item = (ComboBoxItem)cbb_lote.SelectedItem;
				txt_cantidad.Enabled = true;
				txt_cantidad.Text = "1";
				txt_cantidad.SelectAll();
				txt_cantidad.Focus();

				cbb_lote.Text = item.Text;
				cbb_lote.Enabled = false;
			}
			else if (Convert.ToInt32(e.KeyCode) == 27)
			{
				cbb_caducidad.Enabled = true;
				cbb_caducidad.Focus();
				cbb_caducidad.DroppedDown = true;
				cbb_lote.Items.Clear();
				cbb_lote.Enabled = false;

			}
		}

		private void dgv_ventas_KeyDown(object sender, KeyEventArgs e)
		{
			if (dgv_ventas.RowCount > 0 && dgv_ventas.SelectedRows.Count > 0)
			{
				var keycode = Convert.ToInt32(e.KeyCode);
				var row = dgv_ventas.Rows[dgv_ventas.SelectedRows[0].Index];
				int detallado_venta_id = int.Parse(row.Cells["detallado_venta_id"].Value.ToString());

				switch (keycode)
				{
                    case 67:
                        if (e.Control)
                        {
                            Clipboard.SetText(dgv_ventas.SelectedRows[0].Cells["amecop"].Value.ToString());
                        }
                    break;
					case 27:
						txt_amecop.Focus();
						break;
					case 45:
						long cantidad = Convert.ToInt64(row.Cells["cantidad"].Value.ToString());

						Cambiar_cantidad form_cantidad = new Cambiar_cantidad(cantidad);
						form_cantidad.ShowDialog();

						long existencia_vendible = dao_articulos.get_existencia_vendible(row.Cells["amecop"].Value.ToString(), row.Cells["caducidad_sin_formato"].Value.ToString(), row.Cells["lote"].Value.ToString());

                        existencia_vendible += cantidad;

						if (existencia_vendible >= form_cantidad.nueva_cantidad)
						{
							int articulo_id = Convert.ToInt32(row.Cells["columna_articulo_id"].Value);

							dgv_ventas.DataSource = dao_ventas.update_cantidad((long)venta_id, detallado_venta_id, form_cantidad.nueva_cantidad);
							//validar_promocion_producto(articulo_id);
							get_totales();
						}
						else
						{
							MessageBox.Show(this, "La cantidad solicitada es mayor a la disponible", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}

						txt_amecop.Focus();
						break;
					case 46:
						if (!Convert.ToBoolean(row.Cells["columna_es_promocion"].Value))
						{
							int articulo_id = Convert.ToInt32(row.Cells["columna_articulo_id"].Value);
							dgv_ventas.DataSource = dao_ventas.eliminar_detallado_venta(detallado_venta_id, (long)venta_id);
							//validar_promocion_producto(articulo_id, true);
							get_totales();
						}

						if (dgv_ventas.Rows.Count == 0)
						{
							txt_amecop.Focus();
						}

						break;
					default:
						break;
				}
			}
		}

		public void rellenar_informacion_producto(DTO_Articulo articulo)
		{
			txt_producto.Text = articulo.Nombre;
			txt_descuento.Text = ((articulo.Pct_descuento > 0) ? Convert.ToDecimal(articulo.Pct_descuento * 100).ToString("#.##") : "0")+"%";
			cbb_caducidad.Enabled = true;

			if (articulo.Caducidades.Rows.Count > 0)
			{
				cbb_caducidad.Items.Clear();

				foreach (DataRow row in articulo.Caducidades.Rows)
				{
					ComboBoxItem item = new ComboBoxItem();
                    item.Text = (row["caducidad"].ToString().Equals("0000-00-00") || row["caducidad"].ToString().Equals("0000-00-00 00:00:00")) ? "SIN CAD" : Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD") + " (" + row["existencia"] + ")";
					item.Value = row["caducidad"].ToString();
					item.elemento_id = articulo.Articulo_id;
					cbb_caducidad.Items.Add(item);
				}
			}
			else
			{
				ComboBoxItem item = new ComboBoxItem();
				item.Text = "SIN CAD";
				item.Value = "0000-00-00";
				item.elemento_id = articulo.Articulo_id;

				cbb_caducidad.Items.Add(item);
			}

			cbb_caducidad.DroppedDown = true;
			cbb_caducidad.SelectedIndex = 0;
			cbb_caducidad.Focus();
			txt_amecop.Enabled = false;
		}

		public void busqueda_producto()
		{
            if(txt_amecop.Text.Substring(0,1).Equals("?"))
            {
                form_busqueda_producto();
            }
            else
            {
                DTO_Articulo articulo = dao_articulos.get_articulo(txt_amecop.Text);

			    if (articulo.Articulo_id != null)
			    {
                    if (articulo.activo)
                    {
                        rellenar_informacion_producto(articulo);
                    }
                    else
                    {
                        txt_amecop.Text = "";
                        MessageBox.Show(this, "El producto se encuentra en el catalgo pero esta marcado como inactivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_amecop.Focus();
                    }
			    }
			    else
			    {
				    txt_amecop.Text = "";
				    MessageBox.Show(this, "Producto No encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			    }
            }
		}

		public void busqueda_productos_Close(object sender, FormClosedEventArgs e)
		{
			if (Busqueda_productos.articulo_articulo_id != null)
			{
				txt_amecop.Text = Busqueda_productos.articulo_amecop;
				txt_amecop.Enabled = false;

				txt_producto.Text = Busqueda_productos.articulo_producto;

				cbb_caducidad.Items.Add(Busqueda_productos.caducidad_item);
				cbb_lote.Items.Add(Busqueda_productos.lote_item);

				cbb_lote.SelectedIndex = cbb_lote.FindStringExact(Busqueda_productos.lote_item.Text.ToString());
				cbb_caducidad.SelectedIndex = cbb_caducidad.FindStringExact(Busqueda_productos.caducidad_item.Text.ToString());

				cbb_caducidad.Enabled = false;
				cbb_lote.Enabled = false;

				txt_cantidad.Enabled = true;
				txt_cantidad.Text = "1";
				
				txt_cantidad.SelectAll();
				txt_cantidad.Focus();
			}
		}

        void agregar_cupon_venta()
        {
            Cupones cupones = new Cupones();
            cupones.ShowDialog();

            if (cupones.cupon_id > 0)
            {
                DialogResult dr = MessageBox.Show(this, "Al agregar un cupon, no podra capturar mas productos, ¿Desea Continuar?", "Cupon", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    var result_asignar_cupon = dao_ventas.asignar_cupon(cupones.cupon_id, (long)venta_id);

                    if (result_asignar_cupon.status)
                    {
                        MessageBox.Show(this, result_asignar_cupon.informacion, "Asignar Cupón", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        rellenar_informacion_venta();
                    }
                    else
                    {
                        MessageBox.Show(this, result_asignar_cupon.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        void importar_traspaso_venta()
        {
            if (dgv_ventas.Rows.Count == 0)
            {
                Importar_traspaso_venta importar = new Importar_traspaso_venta();
                importar.ShowDialog();

                if ((DTO_Traspaso)importar.cast_dto_traspaso != null)
                {
                    Venta_traspaso venta_traspaso = new Venta_traspaso((long)venta_id, importar.cast_dto_traspaso, importar.hash);
                    venta_traspaso.ShowDialog();

                    if (venta_traspaso.venta_terminada)
                    {
                        crear_venta();
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "No puede hacer este proceso si tiene productos capturados en la venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void importar_cotizacion()
        {
            if (dgv_ventas.Rows.Count > 0)
            {
                MessageBox.Show(this, "Para poder importar una cotizacion, la venta actual no debe tener productos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Importar_cotizacion importar_cotizacion = new Importar_cotizacion((long)venta_id);
                importar_cotizacion.ShowDialog();

                rellenar_informacion_venta();
            }
        }

		private void txt_amecop_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 27:
					if(txt_amecop.Text.Trim().Length > 0)
					{
						txt_amecop.Text = "";
						txt_producto.Text = "";
					}
					else
					{
                        txt_comentarios.Focus();
					}
				break;
				case 40:
					if (dgv_ventas.Rows.Count > 0)
					{
						dgv_ventas.CurrentCell = dgv_ventas.Rows[0].Cells["amecop"];
						dgv_ventas.Rows[0].Selected = true;
						dgv_ventas.Focus();
					}
				break;
				case 13:
                if (txt_amecop.TextLength > 0)
                {
                    busqueda_producto();
                }
				break;
			}
		}

        void form_busqueda_producto()
        {
            string amecop = "";

            if(txt_amecop.Text.Trim().Length > 0)
            {
                if(txt_amecop.Text.Substring(0,1).Equals("?"))
                {
                    amecop = txt_amecop.Text.Substring(1, txt_amecop.Text.Length - 1).Trim();
                }
            }

            Busqueda_productos busqueda_productos = new Busqueda_productos(amecop);
            busqueda_productos.FormClosed += new FormClosedEventHandler(busqueda_productos_Close);
            busqueda_productos.ShowDialog();
            txt_cantidad.Focus();
        }

		public void registrar_rfc(string rfc_registro_id)
		{
			if (dao_ventas.registrar_rfc((long)venta_id, rfc_registro_id) > 0)
				rellenar_informacion_venta();
		}

		public void registrar_cliente_credito(string cliente_id)
		{
			if (dao_ventas.registrar_cliente_credito((long)venta_id, cliente_id) > 0)
				rellenar_informacion_venta();
		}

		public void registrar_cliente_domicilio(string cliente_domicilio_id)
		{
			int filas_afectadas = dao_ventas.registrar_cliente_domicilio((long)venta_id, cliente_domicilio_id);

			if (filas_afectadas > 0) { rellenar_informacion_venta(); }
			else { MessageBox.Show(this, "Ocurrio un error al intentar registrar el domicilio del cliente en la venta, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
		}

		private void Ventas_principal_Load(object sender, EventArgs e)
		{
			crear_venta(true);
            check_ventana_abierta();
		}

		private void txt_amecop_Enter(object sender, EventArgs e)
		{
			dgv_ventas.ClearSelection();
		}

		private void txt_fcid_empleado_Enter(object sender, EventArgs e)
		{
			dgv_ventas.ClearSelection();
		}

		public void terminar_venta(bool es_tae = false)
		{
            bool con_errores = false;
            if(dgv_ventas.Rows.Count > 0)
			{
				if (txt_venta_credito.Text.Equals(""))
				{
                    var detallado = dao_ventas.validar_existencias_venta((long)venta_id);

                    foreach (var det_tmp in detallado)
                    {
                        if (det_tmp.cantidad > det_tmp.existencia_vendible)
                        {
                            MessageBox.Show(this, "La existencia vendible de " + det_tmp.nombre + " " + Misc_helper.fecha(det_tmp.caducidad, "CADUCIDAD") + " " + ((det_tmp.lote == " ") ? "SIN LOTE" : det_tmp.lote) +" es insuficiente, por favor elimine e ingrese de nuevo el producto","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            con_errores = true;
                            break;
                        }
                    }

					if (con_errores == false)
					{
						decimal total_pagar = Convert.ToDecimal(txt_total.Text.ToString().Trim(new char[] { '$', ' ', ',' }));

                        Pago_tipos pago_tipos = new Pago_tipos((long)venta_id, total_pagar, es_tae, sku, numero_referencia, numero_transaccion);
						pago_tipos.ShowDialog();

						if (pago_tipos.venta_terminada)
						{
							crear_venta();
						}
                       /* else
                        {
                            limpiar_venta();
                        }*/
                        txt_amecop.Focus();
					}
                    else
                    {
                            MessageBox.Show(this, "Ocurrio un error al intentar registrar el domicilio del cliente en la venta, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
				}
				else
				{
                    

                    var detallado = dao_ventas.validar_existencias_venta((long)venta_id);
                    
                    foreach (var det_tmp in detallado)
                    {
                        if (det_tmp.cantidad > det_tmp.existencia_vendible)
                        {
                            MessageBox.Show(this, "La existencia vendible de " + det_tmp.nombre + " " + Misc_helper.fecha(det_tmp.caducidad, "CADUCIDAD") + " " + det_tmp.lote + " es insuficiente, por favor elimine e ingrese de nuevo el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            con_errores = true;
                            break;
                        }
                    }

                    /*
					bool con_errores = false;

					foreach (DataGridViewRow row in dgv_ventas.Rows)
					{
						var cell = row.Cells;
						long existencia_vendible = dao_articulos.get_existencia_vendible(cell["amecop"].Value.ToString(), cell["caducidad_sin_formato"].Value.ToString(), cell["lote"].Value.ToString());

						existencia_vendible += Convert.ToInt64(cell["cantidad"].Value);

						if (existencia_vendible < Convert.ToInt64(cell["cantidad"].Value))
						{
							con_errores = true;
							MessageBox.Show(this, cell["producto"].Value.ToString() + " NO hay suficientes existencias, cuentan con " + existencia_vendible + " piezas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							break;
						}
					}
                     */ 

					if (con_errores == false)
					{
						DialogResult dr = MessageBox.Show(this, "La venta se procesara como CRÉDITO FARMACIA, ¿desea continuar?", "Venta a Credito", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

						if (dr == DialogResult.Yes)
						{
							DTO_Validacion validacion = dao_ventas.terminar_venta_credito((long)venta_id);

							if (validacion.status)
							{
								Ticket_venta ticket_venta = new Ticket_venta();
								ticket_venta.construccion_ticket((long)venta_id);
								ticket_venta.print();
								MessageBox.Show(this, validacion.informacion, "Venta Credito", MessageBoxButtons.OK, MessageBoxIcon.Information);
								crear_venta();
							}
							else
							{
								MessageBox.Show(this, validacion.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
					}
				}	
			}
			else
			{
				MessageBox.Show(this,"No puedes terminar una venta vacia","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				txt_amecop.Focus();
			}
		}

        private void valida_ws()
        {

        }

		private void Ventas_principal_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
                case 114:
                    form_busqueda_producto();
                break;
                case 115:
                    if (e.Shift)
                    {
                        if (txt_servicio_domicilio.Text.Trim().Length > 0)
                        {
                            DialogResult dr = MessageBox.Show("¿Esta seguro de querer eliminar el domicilio?", "Cliente Domicilio", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (dr == DialogResult.Yes)
                            {
                                DTO_Validacion result = dao_ventas.desasocisar_cliente_domicilio((long)venta_id);

                                if (result.status)
                                {
                                    txt_servicio_domicilio.Text = "";
                                    MessageBox.Show(this, result.informacion, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show(this, result.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    else
                    {
                        Busqueda_clientes_domicilios servicio_domicilio = new Busqueda_clientes_domicilios();
                        servicio_domicilio.ShowDialog();
                        if (servicio_domicilio.cliente_domicilio_id != null)
                        {
                            registrar_cliente_domicilio(servicio_domicilio.cliente_domicilio_id);
                        }
                            
                    }
                break;
                case 116:
                    if (e.Shift)
                    {
                        if (txt_venta_credito.Text.Trim().Length > 0)
                        {
                            DialogResult dr = MessageBox.Show("¿Esta seguro de querer eliminar el crédito?", "Cliente Crédito", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (dr == DialogResult.Yes)
                            {
                                DTO_Validacion result = dao_ventas.desasociar_cliente_credito((long)venta_id);

                                if (result.status)
                                {
                                    txt_venta_credito.Text = "";
                                    MessageBox.Show(this, result.informacion, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show(this, result.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    else
                    {
                        Busqueda_creditos clientes_creditos = new Busqueda_creditos();
                        clientes_creditos.ShowDialog();
                        if (clientes_creditos.cliente_id != "")
                            registrar_cliente_credito(clientes_creditos.cliente_id);
                    }
                break;
                case 45:
                    if (e.Control)
                    {
                        importar_cotizacion();
                    }
                break;
                case 84:
                    if (e.Control)
                    {
                        if (e.Alt)
                        {
                            if (dgv_ventas.Rows.Count == 0)
                            {
                                Tae_principal tae_prin = new Tae_principal(this, venta_id);
                                tae_prin.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show(this, "Para poder realizar la recarga de tiempo aire es necesario que la venta se encuentre vacía (CTRL+L).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            
                        }
                        else
                        {
                            importar_traspaso_venta();
                        }
                    }
                break;
				case 67:
					if(e.Control)
					{
						if(e.Alt)
						{
							Devoluciones_principal modulo_devolucion = new Devoluciones_principal();
							modulo_devolucion.ShowDialog();
							crear_venta();
						}
					}
				break;
				case 35:
					terminar_venta();
				break;
				case 75:
					if(e.Control)
					{
						Ingresar_prepago prepago = new Ingresar_prepago((long)venta_id);
						prepago.ShowDialog();

						if(prepago.prepago_afectado)
						{
							crear_venta();
						}
					}
				break;
				case 70:
					if (e.Control)
					{
						Ingresar_formula_magistral formula = new Ingresar_formula_magistral((long)venta_id);
						formula.ShowDialog();						
						rellenar_informacion_venta();
					}
				break;
                case 76:
                    
                    if (e.Control)
                    {
                        DialogResult dr = MessageBox.Show(this, "Esta a punto de borrar permanentemente toda la informacion de la venta, ¿desea continuar?", "Limpiar Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dr == DialogResult.Yes)
                        {
                            limpiar_venta();
                        }
                    }
                break;
				case 68:
					if(e.Control)
					{
                        if (e.Shift)
                        {
                            if (cupon_id != null)
                            {
                                DialogResult dr = MessageBox.Show(this, "¿Esta seguro de querer desasociar el cupon de esta venta?", "Desasociar Cupón", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (dr == DialogResult.Yes)
                                {
                                    var validacion = dao_ventas.desasociar_cupon((long)cupon_id, (long)venta_id);
                                    if (validacion.status)
                                    {
                                        MessageBox.Show(this, validacion.informacion, "Cupón", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        rellenar_informacion_venta();
                                        txt_amecop.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show(this, validacion.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if(cupon_id == null)
                            {
                                agregar_cupon_venta();
                            }
                        }
					}
				break;
                case 117:
                    MessageBox.Show(this, "EN CONSTRUCCION...", "PROXIMAMENTE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;

			}
		}

		private void Ventas_principal_FormClosing(object sender, FormClosingEventArgs e)
		{
            if(this.venta_id != null)
            {
                var informacion_venta = dao_ventas.get_venta_data((long)venta_id);

                if (informacion_venta.fecha_terminado.Equals(null))
                {
                    if (dgv_ventas.Rows.Count > 0)
                    {
                        MessageBox.Show(this, "Para cerra el modulo Limpie o termine la venta primero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                    }
                }
            }
		}

        public void actualiza_dgv()
        {
            DAO_Ventas dao_ventas = new DAO_Ventas();
            dgv_ventas.DataSource = dao_ventas.get_detallado_ventas((long)venta_id);
            dgv_ventas.ClearSelection();
        }

        public void insertar_detallado_tae(long articulo_id)
        {
            DAO_Articulos dao_art = new DAO_Articulos();
            var art = dao_art.get_articulo(articulo_id);

            DAO_Ventas dao_ventas = new DAO_Ventas();
            dgv_ventas.DataSource = dao_ventas.insertar_detallado(art.Amecop, "0000-00-00", " ", 1, venta_id, false); ;
            dgv_ventas.ClearSelection();
            validar_color_grid();
            get_totales();
        }

		private void txt_fcid_empleado_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 27:
					rellenar_informacion_venta();
				break;
				case 13:
                    if (txt_fcid_empleado.Text.Trim().Length > 0)
                    {
                        validar_cambio_empleado();
                    }
				break;
			}
		}

		void validar_cambio_empleado()
		{
			//%32DAD23564 C3477EA280 759E22A6DA 80_

			if(txt_fcid_empleado.Text.Trim().Length == 34)
			{
				string caracter_inicio = txt_fcid_empleado.Text.Trim().Substring(0,1);
				string caracter_fin = txt_fcid_empleado.Text.Trim().Substring(txt_fcid_empleado.Text.Trim().Length -1, 1);

				if(caracter_inicio.Equals("%") && caracter_fin.Equals("_"))
				{
                    try
                    {
                        DAO_Empleados dao_empleados = new DAO_Empleados();
                        dao_empleados.get_empleado_data("%9F8E37F326AD41D1A4DE67811C15A719_");
                    }
                    catch(Exception es)
                    {
                        Log_error.log(es);
                    }

                    DAO_Login dao_login = new DAO_Login();
                    DTO_Validacion validacion = dao_login.validar_fcid(txt_fcid_empleado.Text.ToString());
                    if (validacion.status == true)
                    {
                        if ((int)validacion.elemento_id > 0 && venta_id != null)
                        {
                            dao_ventas.actualizar_empleado_venta((long)validacion.elemento_id, (long)venta_id);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, validacion.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
				}
				else
				{
					MessageBox.Show(this, "FCID invalido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				MessageBox.Show(this,"FCID invalido","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}

			rellenar_informacion_venta();
		}

        private void Ventas_principal_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.venta_id != null) 
            {
               // if (!dao_ventas.eliminar_venta((long)venta_id))
                //{
                   // MessageBox.Show(this, "Ocurrio un error al intentar liberar la venta por favor notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }   
        }

        private void txt_comentarios_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_comentarios_Leave(object sender, EventArgs e)
        {
            dao_ventas.set_comentario((long)venta_id, txt_comentarios.Text);
        }

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txt_comentarios_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch(keycode)
            {
                case 13:
                    txt_amecop.Focus();
                break;
                case 27:
                    txt_fcid_empleado.Focus();
					txt_fcid_empleado.SelectAll();
                break;
            }
        }

        private void txt_amecop_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_producto_TextChanged(object sender, EventArgs e)
        {

        }

        void check_ventana_abierta()
        {

            Form fp = Application.OpenForms["Corte_parcial_principal"];
            Form fc = Application.OpenForms["Corte_total_principal"];
            Form fr = Application.OpenForms["Refacturacion_principal"];

            if (fr != null)
            {
                fr.Close();
            }
            if(fp != null)
            {
                fp.Close();
            }
            if (fc != null)
            {
                fc.Close();
            }
            
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void dgv_ventas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lbl_nombre_terminal_Click(object sender, EventArgs e)
        {

        }

        private void txt_cantidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
	}
}
