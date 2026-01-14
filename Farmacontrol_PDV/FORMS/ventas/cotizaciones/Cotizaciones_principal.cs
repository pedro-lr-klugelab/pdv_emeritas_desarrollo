using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.CLASSES;
using Farmacontrol_PDV.HELPERS;
using System.Web;
using MySql.Data.MySqlClient;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.IO;
using System.Xml.Xsl;
using System.Xml;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Diagnostics;
using Farmacontrol_PDV.CLASSES.PRINT;

namespace Farmacontrol_PDV.FORMS.ventas.cotizaciones
{
	public partial class Cotizaciones_principal : Form
	{
		//private int? empleado_id = null;
		private long? cotizacion_id = null;
		DAO_Articulos dao_articulos = new DAO_Articulos();
		DAO_Cotizaciones dao_cotizaciones = new DAO_Cotizaciones();
        DAO_Exclusivos dao_exclusivos = new DAO_Exclusivos();
		//System.Threading.Timer _timer_validar_existencias = null;
        DAO_promocion_lealtad promocion_lealtad = new DAO_promocion_lealtad();


		decimal iva = 0;
		decimal ieps = 0;
		decimal gravado = 0;
		decimal exento = 0;
		decimal total_global = 0;

        long sucursal_local = 0;
        /*
		private void validar_existencias_cotizacion()
		{
			if(dgv_cotizaciones.Rows.Count > 0)
			{
				try
				{
					using(DAO_Cotizaciones dao = new DAO_Cotizaciones())
					{
						var detallado = dao.get_productos_cotizacion((long)cotizacion_id);

						foreach (DataRow row in detallado.Rows)
						{
                            if (dgv_cotizaciones.Rows.Count > 0)
                            {
                                foreach (DataGridViewRow grid_row in dgv_cotizaciones.Rows)
                                {
                                    if (Convert.ToInt64(grid_row.Cells["detallado_cotizacion_id"].Value) == Convert.ToInt64(row["detallado_cotizacion_id"]))
                                    {
                                        if (!grid_row.Cells["cantidad_con_formato"].Value.ToString().Equals(row["cantidad_con_formato"]))
                                        {
                                            grid_row.Cells["cantidad_con_formato"].Value = row["cantidad_con_formato"];
                                        }
                                    }
                                }
                            }
						}	
					}
				}
				catch(Exception EX)
				{
					Log_error.log(EX);
				}
			}
		}
         * */

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
							dao_cotizaciones.insertar_detallado(row["amecop"].ToString(), row["caducidad"].ToString(), row["lote"].ToString(), piezas_gratis, (long)cotizacion_id, true);
							piezas_gratis = 0;
						}
						else
						{
							dao_cotizaciones.insertar_detallado(row["amecop"].ToString(), row["caducidad"].ToString(), row["lote"].ToString(), existencia_vendible, (long)cotizacion_id, true);
							piezas_gratis = piezas_gratis - existencia_vendible;
						}
					}
				}
			}

			//dgv_cotizaciones.DataSource = dao_cotizaciones.get_productos_cotizacion((long)cotizacion_id);
			//dgv_cotizaciones.ClearSelection();

            get_productos_cotizacion();

			if (piezas_gratis != 0)
			{
				MessageBox.Show(this, "" + piezas_gratis + " piezas gratis no pudieron ser agregadas por falta de existencias");
			}
		}

		public void validar_promocion_producto(int articulo_id, bool eliminar = false)
		{
			if (eliminar)
			{
				dao_cotizaciones.eliminar_producto_gratis(articulo_id, (long)cotizacion_id);
			}

			DataTable promocion = dao_articulos.validar_producto_promocion(articulo_id);

			if (promocion.Rows.Count > 0)
			{
				var productos_sin_promocion = dao_cotizaciones.get_productos_sin_promocion(articulo_id, (long)cotizacion_id);

				int cantidad_minima = Convert.ToInt32(promocion.Rows[0]["cantidad_minima"]);
				int cantidad_gratis = Convert.ToInt32(promocion.Rows[0]["cantidad_gratis"]);
				int piezas_gratis = 0;

				if (productos_sin_promocion >= cantidad_minima)
				{
					dao_cotizaciones.eliminar_producto_gratis(articulo_id, (long)cotizacion_id);

					piezas_gratis = Math.Abs((productos_sin_promocion / cantidad_minima) * cantidad_gratis);

					DialogResult dr = MessageBox.Show("Producto con promoción, piezas gratis " + piezas_gratis + ", ¿desea tomar la promoción?", "Promoción", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

					if (dr == DialogResult.Yes)
					{
						agregar_producto_gratis(piezas_gratis, dao_articulos.get_existencia_articulo(articulo_id));
					}
				}
			}

			/*dgv_cotizaciones.DataSource = dao_cotizaciones.get_productos_cotizacion((long)cotizacion_id);
			dgv_cotizaciones.ClearSelection();*/
            get_productos_cotizacion();

			txt_amecop.Focus();
		}

		public void get_totales()
		{
			DataTable calculo_totales = dgv_cotizaciones.DataSource as DataTable;

			decimal total = 0;
			decimal subtotal = 0;
			int piezas = 0;
			decimal iva = 0;
			decimal ieps = 0;
			decimal gravado = 0;
			decimal excento = 0;

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
					excento += Convert.ToDecimal(row["subtotal"]);
				}
			}
            DAO_Ventas dao_ventas = new DAO_Ventas();
            float precio_dolar = (float)dao_ventas.get_tipo_cambio();
            float dolar = (float)total / precio_dolar;

			txt_total.Text = string.Format("{0:C}", total);
			txt_subtotal.Text = string.Format("{0:C}", subtotal);
			txt_ieps.Text = string.Format("{0:C}", ieps);
			txt_iva.Text = string.Format("{0:C}", iva);
			txt_piezas.Text = piezas.ToString();
			txt_excento.Text = string.Format("{0:C}", excento);
			txt_gravado.Text = string.Format("{0:C}", gravado);
            txtBoxDoll.Text = string.Format("{0:C}", dolar);

			this.iva = iva;
			this.ieps = ieps;
			this.exento = excento;
			this.gravado = gravado;
			this.total_global = total;
		}

		public void insertar_producto_cotizacion()
		{
			ComboBoxItem item_cad = (ComboBoxItem)cbb_caducidad.SelectedItem;
			ComboBoxItem item_lote = (ComboBoxItem)cbb_lote.SelectedItem;

			int articulo_id = (int)item_cad.elemento_id;

            /*OBTENER LA SUCURSAL Y EL ARTICULO*/
           
            bool valido = dao_exclusivos.valida_exclusivo(sucursal_local,articulo_id);

            if (valido)
            {

                /*fin de la consulta*/
                //int existencia_vendible = dao_articulos.get_existencia_vendible(txt_amecop.Text, item_cad.Value.ToString(), item_lote.Value.ToString());

                //if (existencia_vendible >= Convert.ToInt32(txt_cantidad.Text))
                //{
                DataTable result_insert = dao_cotizaciones.insertar_detallado(txt_amecop.Text, item_cad.Value.ToString(), item_lote.Value.ToString(), Convert.ToInt64(txt_cantidad.Value), (long)cotizacion_id);

                limpiar_informacion();

                if (result_insert.Rows.Count > 0)
                {
                    dgv_cotizaciones.DataSource = result_insert;
                    dgv_cotizaciones.ClearSelection();
                    validar_promocion_producto(articulo_id);

                    this.get_productos_lealtad(articulo_id);
                    validar_colores_promocion_lealtad();

                }
                else
                {
                    MessageBox.Show(this, "Ocurrio un problema al intentar registrar el producto, notifica a tu administrador!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                get_totales();
            }
            else
            { 
                //CODIGO PARTICIPANTE Y SUCURSAL NO PARTICIPANTE
                MessageBox.Show(this, "El codigo no puede ser agregado a la cotizacion porque esta sucursal no esta permitida para su venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
			/*}
			else
			{
				MessageBox.Show(this, "La cantidad disponible para vender es de " + existencia_vendible, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}*/
		}

		public Cotizaciones_principal()
		{
			InitializeComponent();
            
			//_timer_validar_existencias = new System.Threading.Timer(validar_existencias_cotizacion, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
		}

		public void rellenar_informacion_cotizacion()
		{
			DataTable result = dao_cotizaciones.get_cotizacion_data((long)cotizacion_id);
			txt_nombre_empleado.Text = result.Rows[0]["nombre"].ToString();
			txt_fcid_empleado.Text = result.Rows[0]["fcid"].ToString();
			lbl_folio.Text = "#" + result.Rows[0]["cotizacion_id"].ToString();
			txt_servicio_domicilio.Text = result.Rows[0]["servicio_domicilio"].ToString();
			txt_venta_credito.Text = result.Rows[0]["cliente_credito_nombre"].ToString();
            txt_comentarios.Text = result.Rows[0]["comentarios"].ToString();

			/*dgv_cotizaciones.DataSource = dao_cotizaciones.get_productos_cotizacion((long)cotizacion_id);
			dgv_cotizaciones.ClearSelection();
			get_totales();
			txt_amecop.Focus();*/
            get_productos_cotizacion();
		}

		private void ventas_cotizacion_Load(object sender, EventArgs e)
		{
			creacion_cotizacion(true);
            sucursal_local = Convert.ToInt64(Config_helper.get_config_local("sucursal_id"));
		}

		public void creacion_cotizacion(bool tomar_empleado_sesion = false)
		{
			cotizacion_id = dao_cotizaciones.registrar_cotizacion(tomar_empleado_sesion);
			rellenar_informacion_cotizacion();
		}

		public void rellenar_informacion_producto(DTO_Articulo articulo)
		{
			cbb_caducidad.Enabled = true;
			txt_producto.Text = articulo.Nombre;

            decimal porciento_descuento = articulo.Pct_descuento * 100;

            txt_descuento.Text = ((porciento_descuento > 0) ? porciento_descuento.ToString("#.##") : "0") + "%";

			if (articulo.Caducidades.Rows.Count > 0)
			{
				cbb_caducidad.Items.Clear();

				foreach (DataRow row in articulo.Caducidades.Rows)
				{
					ComboBoxItem item = new ComboBoxItem();
                    item.Text = Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD") + " (" + row["existencia"] + ")";
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

		public void limpiar_informacion()
		{
			txt_amecop.Text = "";
			txt_amecop.Enabled = true;

			cbb_caducidad.Items.Clear();
			cbb_lote.Items.Clear();

			txt_descuento.Text = "";
			txt_producto.Text = "";

			txt_amecop.Focus();

			txt_cantidad.Enabled = false;
			txt_cantidad.Value = 1;
            txtBoxPromociones.Text = "";
            txtBoxPromociones.Visible = false;
		}

		#region Metodos de busqueda

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

            Busqueda_productos busqueda_productos = new Busqueda_productos(true,amecop);
            busqueda_productos.FormClosed += new FormClosedEventHandler(busqueda_productos_Close);
            busqueda_productos.ShowDialog();
            txt_cantidad.Focus();
        }

		public void busqueda_producto()
		{
            if (txt_amecop.Text.Substring(0, 1).Equals("?"))
            {
                form_busqueda_producto();
            }
            else
            {
                DTO_Articulo articulo = dao_articulos.get_articulo(txt_amecop.Text);

                if (articulo.Articulo_id != null)
                {
                    if(articulo.activo)
                    {
                        rellenar_informacion_producto(articulo);
                    }
                    else
                    {
                        txt_amecop.Text = "";
                        MessageBox.Show(this,"El producto se encuentra en el catalgo pero esta marcado como inactivo","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        txt_amecop.Focus();
                    }
                }
                else
                {
                    txt_amecop.Text = "";
                    MessageBox.Show(this, "Producto no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
		}

		public void busqueda_productos_Close(object sender, FormClosedEventArgs e)
		{
			if (Busqueda_productos.articulo_articulo_id != null)
			{
				txt_amecop.Text = Busqueda_productos.articulo_amecop;
				txt_amecop.Enabled = false;
                txt_descuento.Text = Convert.ToDecimal(Busqueda_productos.pct_descuento * 100).ToString("#.##")+"%";

				txt_producto.Text = Busqueda_productos.articulo_producto;

				cbb_caducidad.Items.Add(Busqueda_productos.caducidad_item);
				cbb_lote.Items.Add(Busqueda_productos.lote_item);

				cbb_lote.SelectedIndex = cbb_lote.FindStringExact(Busqueda_productos.lote_item.Text.ToString());
				cbb_caducidad.SelectedIndex = cbb_caducidad.FindStringExact(Busqueda_productos.caducidad_item.Text.ToString());

				cbb_caducidad.Enabled = false;
				cbb_lote.Enabled = false;

				txt_cantidad.Enabled = true;
				txt_cantidad.Value = 1;
				txt_cantidad.Select(0,txt_cantidad.Text.Length);
				txt_cantidad.Focus();
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

		#endregion

		#region Eventos_teclado

		private void txt_cantidad_KeyDown(object sender, KeyEventArgs e)
		{
			if (Convert.ToInt32(e.KeyCode) == 27)
			{
				limpiar_informacion();
			}

			if (Convert.ToInt32(e.KeyCode) == 13 && txt_cantidad.Value > 0)
			{
				insertar_producto_cotizacion();
			}
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

		private void txt_amecop_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 27:
					if (txt_amecop.TextLength == 0) {
                        txt_comentarios.Focus();
                    }
					else 
                    { 
                        txt_amecop.Text = ""; txt_producto.Text = "";
                    }
				break;
				case 40:
					if (dgv_cotizaciones.Rows.Count > 0)
					{
						dgv_cotizaciones.CurrentCell = dgv_cotizaciones.Rows[0].Cells["amecop"];
						dgv_cotizaciones.Rows[0].Selected = true;
						dgv_cotizaciones.Focus();
					}
					break;
				case 13:
                    
                    if (txt_amecop.TextLength > 0) {
                        busqueda_producto();
                    }

					break;
				case 114:
                    form_busqueda_producto();
				break;
			}
		}

		public void registrar_rfc(string rfc_registro_id)
		{
			if (dao_cotizaciones.registrar_rfc((long)cotizacion_id, rfc_registro_id) > 0)
				rellenar_informacion_cotizacion();
		}

		public void registrar_cliente_credito(string cliente_id)
		{
			if (dao_cotizaciones.registrar_cliente_credito((long)cotizacion_id, cliente_id) > 0)
				rellenar_informacion_cotizacion();
		}

		public void registrar_cliente_domicilio(string cliente_domicilio_id)
		{
			int filas_afectadas = dao_cotizaciones.registrar_cliente_domicilio((long)cotizacion_id, cliente_domicilio_id);

			if (filas_afectadas > 0) { rellenar_informacion_cotizacion(); }
			else { MessageBox.Show(this, "Ocurrio un error al intentar registrar el domicilio del cliente en la cotización, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
		}

		private void cbb_lote_KeyDown(object sender, KeyEventArgs e)
		{
			if (Convert.ToInt32(e.KeyCode) == 13)
			{
				ComboBoxItem item = (ComboBoxItem)cbb_lote.SelectedItem;
				txt_cantidad.Enabled = true;
				txt_cantidad.Value = 1;
				txt_cantidad.Select(0,txt_cantidad.Text.Length);
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

		private void txt_fcid_empleado_KeyDown(object sender, KeyEventArgs e)
		{
			if (Convert.ToInt32(e.KeyCode) == 13 && txt_fcid_empleado.TextLength > 0)
			{
				DAO_Login dao_login = new DAO_Login();
				DTO_Validacion validacion = dao_login.validar_fcid(txt_fcid_empleado.Text.ToString());

				if (validacion.status == true)
				{
					dao_cotizaciones.cambiar_usuario((int)validacion.elemento_id, cotizacion_id);
					rellenar_informacion_cotizacion();
				}
				else
				{
					MessageBox.Show(this, validacion.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			if (Convert.ToInt32(e.KeyCode) == 27)
			{
				txt_amecop.Focus();
			}
		}

		private void txt_amecop_Enter(object sender, EventArgs e)
		{
			dgv_cotizaciones.ClearSelection();
		}

		private void dgv_cotizaciones_KeyDown(object sender, KeyEventArgs e)
		{
			if (dgv_cotizaciones.RowCount > 0 && dgv_cotizaciones.SelectedRows.Count > 0)
			{
				var keycode = Convert.ToInt32(e.KeyCode);
				var row = dgv_cotizaciones.Rows[dgv_cotizaciones.SelectedRows[0].Index];
				int detallado_cotizacion_id = int.Parse(row.Cells["detallado_cotizacion_id"].Value.ToString());

				switch (keycode)
				{
                    case 67:
                        if (e.Control)
                        {
                            Clipboard.SetText(dgv_cotizaciones.SelectedRows[0].Cells["amecop"].Value.ToString());
                        }
                    break;
					case 27:
						txt_amecop.Focus();
						break;
					case 45:
						long cantidad = Convert.ToInt64(row.Cells["cantidad"].Value.ToString());
						Cambiar_cantidad form_cantidad = new Cambiar_cantidad(cantidad);
						form_cantidad.ShowDialog();

						//int existencia_vendible = dao_articulos.get_existencia_vendible(row.Cells["amecop"].Value.ToString(), row.Cells["caducidad_sin_formato"].Value.ToString(), row.Cells["lote"].Value.ToString());

						int articulo_id = Convert.ToInt32(row.Cells["columna_articulo_id"].Value);
							dgv_cotizaciones.DataSource = dao_cotizaciones.update_cantidad((long)cotizacion_id, detallado_cotizacion_id, form_cantidad.nueva_cantidad);
							validar_promocion_producto(articulo_id);
							get_totales();

						/*
						if (existencia_vendible >= form_cantidad.nueva_cantidad)
						{
							int articulo_id = Convert.ToInt32(row.Cells["columna_articulo_id"].Value);
							dgv_cotizaciones.DataSource = dao_cotizaciones.update_cantidad((long)cotizacion_id, detallado_cotizacion_id, form_cantidad.nueva_cantidad);
							validar_promocion_producto(articulo_id);
							get_totales();
						}
						else
						{
							MessageBox.Show(this, "La cantidad solicitada es mayor a la disponible", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						 * */

						txt_amecop.Focus();
						break;
					case 46:
						if (!Convert.ToBoolean(row.Cells["columna_es_promocion"].Value))
						{
							int articulo_id_promocion = Convert.ToInt32(row.Cells["columna_articulo_id"].Value);
							dgv_cotizaciones.DataSource = dao_cotizaciones.eliminar_detallado_cotizacion(detallado_cotizacion_id, (long)cotizacion_id);
							validar_promocion_producto(articulo_id_promocion, true);

                            txtBoxPromociones.Text = "";
                            txtBoxPromociones.Visible = false;
							get_totales();
                            validar_colores_promocion_lealtad();
						}

						if (dgv_cotizaciones.Rows.Count == 0)
						{
							txt_amecop.Focus();
						}

						break;
					default:
						break;
				}
			}
		}

		#endregion

		private void cotizaciones_principal_FormClosed(object sender, FormClosedEventArgs e)
		{
			
		}

		private void Cotizaciones_principal_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (dgv_cotizaciones.Rows.Count > 0 || txt_servicio_domicilio.Text != "" || txt_venta_credito.Text != "")
			{
                dao_cotizaciones.pausar_cotizacion((long)cotizacion_id, txt_comentarios.Text.ToString());
			}
		}

		private void enviarCotizaciónPorCorreoToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			enviar_correo();
		}

		private void enviar_correo()
		{
			try
			{
				if(dgv_cotizaciones.Rows.Count > 0)
				{
					using (Reports.plantilla_cotizacion plantilla = new Reports.plantilla_cotizacion())
					{
						List<Plantilla_cotizacion_articulo> productos = new List<Plantilla_cotizacion_articulo>();

						foreach (DataGridViewRow row in dgv_cotizaciones.Rows)
						{
							productos.Add(new Plantilla_cotizacion_articulo()
							{
								amecop = row.Cells["amecop"].Value.ToString().Replace("*", "").Trim(),
								producto = row.Cells["producto"].Value.ToString(),
								tasa = Convert.ToDecimal(row.Cells["pct_iva"].Value),
								precio = Convert.ToDecimal(row.Cells["importe"].Value),
								total = Convert.ToDecimal((Convert.ToDecimal(row.Cells["importe"].Value) * Convert.ToInt32(row.Cells["cantidad"].Value))),
								cantidad = Convert.ToInt32(row.Cells["cantidad"].Value)
							});
						}

						plantilla.SetDataSource(productos);

						Cotizacion_correo correo = new Cotizacion_correo((long)cotizacion_id);
						correo.ShowDialog();


						if (correo.envio_correo)
						{
							try
							{

								Cursor = Cursors.WaitCursor;
								DAO_Sucursales dao_sucursales = new DAO_Sucursales();
								var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(Config_helper.get_config_local("sucursal_id")));

								ReportDocument crystalReport = plantilla;

								crystalReport.SetDataSource(productos);
								crystalReport.SetParameterValue("nombre", correo.nombre_cliente);
								crystalReport.SetParameterValue("direccion", correo.direccion_cliente);
								crystalReport.SetParameterValue("total_letras", Misc_helper.NumtoLe(total_global));
								crystalReport.SetParameterValue("sucursal_nombre", sucursal_data.nombre);
								crystalReport.SetParameterValue("email_sucursal", sucursal_data.email);
								crystalReport.SetParameterValue("excento", exento);
								crystalReport.SetParameterValue("gravable", gravado);
								crystalReport.SetParameterValue("iva", iva);
								crystalReport.SetParameterValue("total", total_global);

                                string nombre_archivo = "cotizacion_" + cotizacion_id + "_" + Convert.ToDateTime(Misc_helper.fecha()).ToString("yyyy-MM-dd")+".pdf";
								
								var result = Email_helper.Envio_email(correo.destinatarios,"Cotización #"+cotizacion_id.ToString(),"","",correo.mensaje_personalizado,crystalReport.ExportToStream(ExportFormatType.PortableDocFormat),nombre_archivo);

								Cursor = Cursors.Default;

								if (Convert.ToBoolean(result))
								{
									MessageBox.Show(this, "Se ha enviado la cotización correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                    var confirmResult = MessageBox.Show(" ¿Descargar la cotizacion al equipo ?", "Descarga de cotizacion al equipo!!", MessageBoxButtons.YesNo);
                                    if (confirmResult == DialogResult.Yes)
                                    {
                                        string path = Directory.GetCurrentDirectory();
                                        using (var fbd = new FolderBrowserDialog())
                                        {
                                            DialogResult dialogform = fbd.ShowDialog();

                                            if (dialogform == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                                            {
                                                path = fbd.SelectedPath;
                                            }
                                        }



                                        ExportOptions CrExportOptions;
                                        DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                                        PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                                        string direccion_archivo = path + "\\" + Misc_helper.uuid() + "_COTIZACION_" + cotizacion_id + ".pdf";
                                        CrDiskFileDestinationOptions.DiskFileName = direccion_archivo;

                                        CrExportOptions = crystalReport.ExportOptions;
                                        {
                                            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                                            CrExportOptions.FormatOptions = CrFormatTypeOptions;
                                        }

                                        crystalReport.Export();

                                        MessageBox.Show(this, "Se ha descargado la cotización correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                      
                                    }
                                    else
                                    {
                                      
                                    }



                                   

								}
								else
								{
									MessageBox.Show(this, "Ocurrio un error al enviar el email, intentelo mas tarde", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
								}

							}
							catch(Exception ex)
							{
								Cursor = Cursors.Default;
								MessageBox.Show(this,ex.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
							}
						}
					}
				}
				else
				{
					MessageBox.Show(this,"No puede enviar una cotización vacia","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);	
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		void client_SendCompleted(object sender, AsyncCompletedEventArgs e)
		{
			
		}

		private void dgv_cotizaciones_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
           
		}

        void validar_colores_grid()
        {
            foreach(DataGridViewRow row in dgv_cotizaciones.Rows)
            {
                if(Convert.ToBoolean(row.Cells["es_antibiotico"].Value))
                {
                    if (row.Cells["cantidad_con_formato"].Value.ToString().IndexOf('(') != -1)
                    {
                        dgv_cotizaciones.Rows[row.Index].DefaultCellStyle.BackColor = Color.FromArgb(255, 218, 218);
                    }
                    else
                    {
                        dgv_cotizaciones.Rows[row.Index].DefaultCellStyle.BackColor = Color.FromArgb(179, 206, 252);
                    }
                }
                else
                {
                    if (row.Cells["cantidad_con_formato"].Value.ToString().IndexOf('(') != -1)
                    {
                        dgv_cotizaciones.Rows[row.Index].DefaultCellStyle.BackColor = Color.FromArgb(255, 218, 218);
                    }
                    else
                    {
                        dgv_cotizaciones.Rows[row.Index].DefaultCellStyle.BackColor = Color.Empty;
                    }
                }
            }
        }


        void validar_colores_promocion_lealtad()
        {
            foreach (DataGridViewRow row in dgv_cotizaciones.Rows)
            {
                if (Convert.ToBoolean(row.Cells["columna_articulo_id"].Value))
                {
                    //this.get_productos_lealtad();
                   int articulo_id = int.Parse(row.Cells["columna_articulo_id"].Value.ToString());
                    Dictionary<string, string> txtpromociones = promocion_lealtad.get_promocio_lealtad(articulo_id);


					if (txtpromociones.Count() > 0)
					{

                        foreach (KeyValuePair<string, string> par in txtpromociones)
                        {
							
							switch (par.Key)
							{
								case "NADRO":
                                    row.Cells["amecop"].Style.BackColor = Color.FromArgb(218, 247, 166);
                                break;
								case "FANASA":
                                    row.Cells["producto"].Style.BackColor = Color.FromArgb(75, 165, 204);
                                break;
							}
                        }

                  
					}
					else if( txtpromociones.Count == 0 )
					{
                        row.Cells["amecop"].Style.BackColor = BackColor = Color.Empty;
                        row.Cells["producto"].Style.BackColor = BackColor = Color.Empty;
                    }
					
                }
                
            }
        
        
        }

		private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
		}

        private void txt_comentarios_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void Cotizaciones_principal_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch(keycode)
            {
                case 82:
                    if(e.Control)
                    {
                        Reabrir_cotizaciones reabrir = new Reabrir_cotizaciones();
                        reabrir.ShowDialog();

                        if (reabrir.cotizacion_id != null)
                        {
                            if (dgv_cotizaciones.Rows.Count > 0 || txt_servicio_domicilio.Text != "" || txt_venta_credito.Text != "")
                            {
                                dao_cotizaciones.pausar_cotizacion((long)cotizacion_id, txt_comentarios.Text.ToString());
                            }

                            cotizacion_id = reabrir.cotizacion_id;

                            dao_cotizaciones.set_fecha_cerrado_null((long)cotizacion_id);
                            rellenar_informacion_cotizacion();
                        }
                    }
                break;
                case 115:
                    if (e.Shift)
                    {
                        if (txt_servicio_domicilio.Text.Trim().Length > 0)
                        {
                            DialogResult dr = MessageBox.Show("¿Esta seguro de querer eliminar el domicilio?", "Cliente Domicilio", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (dr == DialogResult.Yes)
                            {
                                DTO_Validacion result = dao_cotizaciones.desasocisar_cliente_domicilio((long)cotizacion_id);

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

                            DialogResult dr = MessageBox.Show("¿Esta seguro de querer eliminar el crédito?", "Cliente Credito", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (dr == DialogResult.Yes)
                            {
                                DTO_Validacion result = dao_cotizaciones.desasociar_cliente_credito((long)cotizacion_id);

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
                case 80:
                    if (e.Control)
                    {
                        /*
                        if (dgv_cotizaciones.Rows.Count > 0)
                        {
                            Ticket_cotizacion ticket_cotizacion = new Ticket_cotizacion();
                            ticket_cotizacion.construccion_ticket((long)cotizacion_id);
                            ticket_cotizacion.print();
                        }
                         * */
                    }
                break;
                case 76:
                    if (e.Control)
                    {
                        dao_cotizaciones.limpiar_cotizacion((long)cotizacion_id);
                        get_productos_cotizacion();
                    }
                    break;
                case 45:
                    if (e.Control)
                    {
                        Cotizaciones_pausadas cotizaciones_pausadas = new Cotizaciones_pausadas();
                        cotizaciones_pausadas.ShowDialog();

                        if (cotizaciones_pausadas.cotizacion_id > 0)
                        {
                            if (dgv_cotizaciones.Rows.Count > 0 || txt_servicio_domicilio.Text != "" || txt_venta_credito.Text != "")
                            {
                                dao_cotizaciones.pausar_cotizacion((long)cotizacion_id, txt_comentarios.Text.ToString());
                            }

                            cotizacion_id = cotizaciones_pausadas.cotizacion_id;
                            rellenar_informacion_cotizacion();
                        }
                    }
                    break;
                case 35:
                    if (e.Control)
                    {
                        if (dgv_cotizaciones.Rows.Count > 0)
                        {
                            DialogResult dr = MessageBox.Show("¿Esta seguro de querer pausar la cotizacion?", "Cotizacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (dr == DialogResult.Yes)
                            {
                                DTO_Validacion result = dao_cotizaciones.pausar_cotizacion((long)cotizacion_id,txt_comentarios.Text.ToString());

                                if (result.status)
                                {
                                    txt_servicio_domicilio.Text = "";
                                    MessageBox.Show(this, result.informacion, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    creacion_cotizacion();
                                }
                                else
                                {
                                    MessageBox.Show(this, result.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(this, "No puedes pausar una cotización vacia", "Pausar Cotizacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        //validar_existencias_cotizacion();

                        if (dgv_cotizaciones.Rows.Count > 0)
                        {
                            
                            bool sin_existencia = false;

                            foreach (DataGridViewRow row in dgv_cotizaciones.Rows)
                            {
                                if (Convert.ToInt32(row.Cells["existencia_vendible"].Value) < 1)
                                {
                                    sin_existencia = true;
                                    break;
                                }
                            }


                            if (sin_existencia)
                            {
                                MessageBox.Show(this, "No puedes terminar una cotizacion con productos sin existencia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                DialogResult dr = MessageBox.Show("¿Esta seguro de querer terminar la cotización?", "Cotizacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                                if (dr == DialogResult.Yes)
                                {
                                    DTO_Validacion result = dao_cotizaciones.terminar_cotizacion((long)cotizacion_id, txt_comentarios.Text);

                                    if (result.status)
                                    {
                                        
                                        //CLASSES.PRINT.Ticket_folio_cotizacion ticket_folio_cotizacion = new CLASSES.PRINT.Ticket_folio_cotizacion();
                                        //ticket_folio_cotizacion.construccion_ticket((long)cotizacion_id);
                                        //ticket_folio_cotizacion.print();
                                        MessageBox.Show(this, result.informacion, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        creacion_cotizacion();
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
                            MessageBox.Show(this, "Cotización vacia, imposible cerrarla", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_amecop.Focus();
                        }
                    }
                break;
            }
        }

        public void get_productos_cotizacion()
        {
            dgv_cotizaciones.DataSource = dao_cotizaciones.get_productos_cotizacion((long)cotizacion_id);
            dgv_cotizaciones.ClearSelection();
            get_totales();
            txt_amecop.Focus();

            validar_colores_grid();
        }


        public bool  get_productos_lealtad( int articulo_id )
        {
            Dictionary<string, string> txtpromociones = promocion_lealtad.get_promocio_lealtad(articulo_id);
			bool respuesta = false;

            txtBoxPromociones.Left = this.Width;

            timerPromociones.Start();

            if (txtpromociones.Count() > 0)
            {
              
                txtBoxPromociones.Visible = true;
                txtBoxPromociones.Text = "";
				string cadenapromosiones = "";
                foreach (KeyValuePair<string, string> par in txtpromociones)
                {
                    cadenapromosiones += par.Value + " ";
                }

                txtBoxPromociones.Text = cadenapromosiones;

                respuesta = true;

            }
            else if (txtpromociones.Count == 0)
            {
                txtBoxPromociones.Visible = false;
                txtBoxPromociones.Text = "";

                respuesta = false;
            }

			return respuesta;
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
                    txt_fcid_empleado.SelectAll(); 
                    txt_fcid_empleado.Focus(); 
                break;
            }
        }

        private void txt_amecop_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbb_caducidad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void dgv_cotizaciones_SelectionChanged(object sender, EventArgs e)
        {
            
            if (dgv_cotizaciones.RowCount > 0 && dgv_cotizaciones.SelectedRows.Count == 1)
            {
                var row = dgv_cotizaciones.Rows[dgv_cotizaciones.SelectedRows[0].Index];
                int articulo_id = int.Parse(row.Cells["columna_articulo_id"].Value.ToString());
				this.get_productos_lealtad(articulo_id);

                /*
                if( this.get_productos_lealtad(articulo_id) )
                    row.Cells["amecop"].Style.BackColor = Color.FromArgb(218, 247, 166);
                else
                    row.Cells["amecop"].Style.BackColor = BackColor = Color.Empty;
				*/
            }
            else
            {

                txtBoxPromociones.Text = "";
                txtBoxPromociones.Visible = false;
               
            }

        }

        private void dgv_cotizaciones_DataSourceChanged(object sender, EventArgs e)
        {
            
        }

        private void txt_cantidad_ValueChanged(object sender, EventArgs e)
        {

        }

        private void timerPromociones_Tick(object sender, EventArgs e)
        {

            txtBoxPromociones.Left -= 15;

            // Si ya salió completamente por la izquierda
            if (txtBoxPromociones.Right < 0)
            {
                txtBoxPromociones.Left = this.Width;
            }

        }

        private void txtBoxPromociones_TextChanged(object sender, EventArgs e)
        {
            Size textSize = TextRenderer.MeasureText(txtBoxPromociones.Text, txtBoxPromociones.Font);
            txtBoxPromociones.Width = textSize.Width + 10; // margen extra opcional
        }
    }
}
