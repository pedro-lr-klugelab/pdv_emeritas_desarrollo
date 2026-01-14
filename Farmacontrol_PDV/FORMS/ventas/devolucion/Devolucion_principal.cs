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
using System.Net.Mail;

namespace Farmacontrol_PDV.FORMS.ventas.devolucion
{
	public partial class Devoluciones_principal : Form
	{
		DAO_Ventas dao_ventas = new DAO_Ventas();
		DAO_Articulos dao_articulos = new DAO_Articulos();
		DAO_Cancelaciones dao_cancelaciones = new DAO_Cancelaciones();
		Dictionary<string,bool> productos_promocion =  new Dictionary<string,bool>();
		List<Tuple<int,string,string,decimal,int>> productos_no_cancelados = new List<Tuple<int,string,string,decimal,int>>();

        List<Tuple<int, string, string, decimal, int>> productos_cancelados = new List<Tuple<int, string, string, decimal, int>>();
        List<Tuple<int, string, string, decimal, int>> productos_comprados = new List<Tuple<int, string, string, decimal, int>>();

		FacturaWSP existe_factura_rest = new FacturaWSP();
		private int venta_id;
		bool emitir_nota_credito = false;
		bool emitir_cancelacion_factura = false;

		public Devoluciones_principal()
		{
			InitializeComponent();
		}

		private void Cancelacion_principal_Load(object sender, EventArgs e)
		{
			tp_contenido_venta.Parent = null;
			tp_productos_cancelar.Parent = null;
            tp_productos_lleva.Parent = null;
            tp_tipodev.Parent = null;
            tp_infocancela.Parent = null;
			txt_folio_venta.Focus();
			/*
			string id = "3997a545dd3d11e3aa7800e04c214685";
			CLASSES.PRINT.Vale_efectivo devolucion = new CLASSES.PRINT.Vale_efectivo();
			devolucion.construccion_ticket(id);
			devolucion.print();
			 * */
		}

		void siguiente_productos_venta()
		{
			tp_contenido_venta.Parent = tabControllleva;
			dgv_ventas.DataSource = dao_ventas.get_productos_venta(Convert.ToInt64(venta_id));
            get_totales_venta();

			tp_folio_venta.Parent = null;
			dgv_ventas.ClearSelection();
			btn_tp_2_siguiente.Focus();
		}


        public void get_totales_venta()
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


            txt_total_venta.Text = total.ToString("C2");
            txt_subtotal_venta.Text = string.Format("{0:C2}", subtotal);
            txt_ieps_venta.Text = string.Format("{0:C2}", ieps);
            txt_iva_venta.Text = string.Format("{0:C2}", iva);
            txt_piezas_venta.Text = piezas.ToString();
            txt_exento_venta.Text = string.Format("{0:C2}", exento);
            txt_gravable_venta.Text = string.Format("{0:C2}", gravado);
        }

        public void get_totales_cancelacion()
        {
            decimal total = 0;
            decimal subtotal = 0;
            int piezas = 0;
            decimal iva = 0;
            decimal ieps = 0;
            decimal gravado = 0;
            decimal exento = 0;

            if(dgv_venta_cencelacion.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgv_venta_cencelacion.Rows)
                {
                    total += Convert.ToDecimal(row.Cells["c_total"].Value);
                    subtotal += Convert.ToDecimal(row.Cells["c_subtotal"].Value);
                    iva += Convert.ToDecimal(row.Cells["c_importe_iva"].Value);
                    ieps += Convert.ToDecimal(row.Cells["c_importe_ieps"].Value);
                    piezas += Convert.ToInt32(row.Cells["c_cantidad"].Value);

                    if (Convert.ToDecimal(row.Cells["c_importe_iva"].Value) > ((decimal)0))
                    {
                        gravado += Convert.ToDecimal(row.Cells["c_subtotal"].Value);
                    }
                    else
                    {
                        exento += Convert.ToDecimal(row.Cells["c_subtotal"].Value);
                    }
                }
            }

            txt_total.Text = total.ToString("C2");
            txt_subtotal.Text = string.Format("{0:C2}", subtotal);
            txt_ieps.Text = string.Format("{0:C2}", ieps);
            txt_iva.Text = string.Format("{0:C2}", iva);
            txt_piezas.Text = piezas.ToString();
            txt_excento.Text = string.Format("{0:C2}", exento);
            txt_gravado.Text = string.Format("{0:C2}", gravado);
        }

		private void txt_folio_venta_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(txt_folio_venta.Text.Trim().Length > 0)
					{
                        if(Misc_helper.validar_codigo_venta(txt_folio_venta.Text.Trim()))
                        {
                            var array_codigo = txt_folio_venta.Text.Split('$');

                            if (Convert.ToInt32(array_codigo[0]) == Convert.ToInt32(Config_helper.get_config_local("sucursal_id")))
                            {
                                venta_id = Convert.ToInt32(array_codigo[1]);

                                var venta_data = dao_ventas.get_venta_data(Convert.ToInt64(venta_id));

                                if (venta_data.venta_id == 0)
                                {
                                    MessageBox.Show(this, " No existe el folio de la venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    
                                }
                                else
                                {
                                        if (!venta_data.cliente_credito_id.Equals(""))
                                        {
                                            txt_folio_venta.Text = "";
                                            MessageBox.Show(this, "No puede cancelar una venta a crédito", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                        else
                                        {
                                            if (venta_data.venta_id > 0)
                                            {
                                                //esto es nuevo 
                                                //valida si es en el mismo año 
                                                 var anio_venta = Convert.ToDateTime(venta_data.fecha_terminado).Year;
                                                 var anio_actual = Convert.ToDateTime(Misc_helper.fecha()).Year;

                                                 if (anio_venta.Equals(anio_actual))
                                                 {
                                                     var mes_de_venta = Convert.ToDateTime(venta_data.fecha_terminado).Month;
                                                     var mes_de_actual = Convert.ToDateTime(Misc_helper.fecha()).Month;

                                                     if (mes_de_venta.Equals(mes_de_actual))
                                                     {
                                                         DateTime fecha_venta = Convert.ToDateTime(venta_data.fecha_terminado);
                                                         DateTime fecha_actual = DateTime.Now;
                                                         TimeSpan ts = fecha_actual - fecha_venta;
                                                         int diferencia_dias = ts.Days;

                                                         if (diferencia_dias <= 7)
                                                         {
                                                             //SE REALIZA LA DEVOLUCION

                                                             if (dao_cancelaciones.existe_cancelacion(venta_id))
                                                             {
                                                                 MessageBox.Show(this, "Esta venta ya fue cancelada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                             }
                                                             else
                                                             {
                                                                 if (venta_data.fecha_terminado.Equals(null))
                                                                 {
                                                                     MessageBox.Show(this, "No puede cancelar una venta sin terminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                 }
                                                                 else
                                                                 {
                                                                     emitir_cancelacion_factura = false;
                                                                     emitir_nota_credito = false;
                                                                     //MOSTRAR ALERTA DE QUE SI ES FACTURADA AVISAR 
                                                                     

                                                                   decimal monto_venta = dao_ventas.get_montos_venta(Convert.ToInt64(venta_id));
                                                                    

                                                                     /////////////////////////////////
                                                                    
                                                                     if (dao_ventas.get_existe_venta_corte(venta_id))
                                                                     {
                                                                          //VENTA que no son del dia DIA
                                                                         var result_cancelacion = WebServicePac_helper.existe_factura(venta_id);
                                                                         if (result_cancelacion.status)
                                                                         {
                                                                             MessageBox.Show(this, "Esta venta fue facturada el dia " + result_cancelacion.fecha_timbrado + "", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                             emitir_nota_credito = true;
                                                                         }
                                                                         else
                                                                             emitir_nota_credito = true;

                                                                         siguiente_productos_venta();
                                                                     }
                                                                     else
                                                                     {
                                                                         //VENTAS QUE SON DEL DIA ANTES DEL CORTE TOTAL
                                                                         if (venta_data.fecha_facturada != null)
                                                                         {
                                                                             ////////////////NOTA FACTURADA
                                                                             var result_cancelacion = WebServicePac_helper.existe_factura(venta_id);
                                                                             if (result_cancelacion.status)
                                                                             {
                                                                                 MessageBox.Show(this, "Esta venta fue facturada el dia " + result_cancelacion.fecha_timbrado + "", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                                 if (diferencia_dias > 3)
                                                                                 {
                                                                                     //valida que el monto sea menor a 5000
                                                                                     if (monto_venta < 5000)
                                                                                         emitir_nota_credito = true;
                                                                                     else
                                                                                         MessageBox.Show(this, "notifique al cliente realizar la cancelacion de la FACTURA a traves de su Buzón tributario ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                                 }
                                                                                 else
                                                                                     emitir_nota_credito = true;
                                                                             }
                                                                             else
                                                                                 emitir_nota_credito = true;

                                                                             
                                                                             siguiente_productos_venta();

                                                                         }
                                                                         else
                                                                         {
                                                                             //VENTA NO FACTURADA y de dias anteriores
                                                                             emitir_nota_credito = true;
                                                                             siguiente_productos_venta();
                                                                         }
                                                                     }
                                                                 }
                                                             }


                                                         }
                                                         else
                                                         {
                                                             MessageBox.Show(this, "La cancelacion no puede realizarse, supera los 7 dias permitidos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                         }



                                                     }
                                                     else
                                                     {
                                                         MessageBox.Show(this, "La cancelacion no puede realizarse, supera el mes fiscal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                     }
                                                 }
                                                 else 
                                                 {

                                                     MessageBox.Show(this, "Esta venta pertenece a un año fiscal anterior", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                         
                                                 }

                                            }
                                            else
                                            {
                                                txt_folio_venta.SelectAll();
                                                MessageBox.Show(this, "Folio de venta no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                txt_folio_venta.Focus();
                                            }

                                        }
                              }
                            }
                            else
                            {
                                MessageBox.Show(this, "No puedes devolver esta venta, ya que fue hecha en otra sucursal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show(this,"Codigo Invalido","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
					}
				break;
				case 27:
					txt_folio_venta.Text = "";
				break;
			}
		}

		private void Cancelacion_principal_Shown(object sender, EventArgs e)
		{
			txt_folio_venta.Focus();
		}

		private void btn_tp2_atras_Click(object sender, EventArgs e)
		{
			tp_contenido_venta.Parent = null;
			tp_folio_venta.Parent = tabControllleva;
			txt_folio_venta.Focus();
			txt_folio_venta.SelectAll();
		}

		private void btn_tp_2_siguiente_Click(object sender, EventArgs e)
		{
			tp_contenido_venta.Parent = null;
            /*
			tp_productos_cancelar.Parent = tabControllleva;
			txt_motivo_cancelacion.Focus();
            get_totales_cancelacion();
             * */
            tp_tipodev.Parent = tabControllleva;
            
		}

		private void btn_tp3_atras_Click(object sender, EventArgs e)
		{
            tp_productos_cancelar.Parent = null;
            tp_tipodev.Parent = tabControllleva;
            dgv_ventas.ClearSelection();
            /*
			tp_productos_cancelar.Parent = null;
			tp_contenido_venta.Parent = tabControllleva;
			
			btn_tp_2_siguiente.Focus();
             */
		}

		private void txt_amecop_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 40:
					if (dgv_venta_cencelacion.Rows.Count > 0)
					{
						dgv_venta_cencelacion.CurrentCell = dgv_venta_cencelacion.Rows[0].Cells["c_amecop"];
						dgv_venta_cencelacion.Rows[0].Selected = true;
						dgv_venta_cencelacion.Focus();
					}
					break;
				case 13:
					if (txt_amecop.TextLength > 0)
					{
						validar_registro_producto();
					}
				break;
				case 27:
					limpiar_informacion();
				break;
			}
		}

		public int get_indice_producto_datagridview(DataRow producto)
		{
			string amecop = producto["amecop"].ToString();
			string caducidad = producto["caducidad"].ToString();
			caducidad = Misc_helper.fecha(caducidad, "CADUCIDAD");
			string lote = producto["lote"].ToString();

			int datagridview_indice = 0;

			foreach (DataGridViewRow row in dgv_venta_cencelacion.Rows)
			{
				string f_amecop = row.Cells["c_amecop"].Value.ToString();
				string f_caducidad = row.Cells["c_caducidad"].Value.ToString();
				string f_lote = row.Cells["c_lote"].Value.ToString();

				if (f_amecop.Equals(amecop) && f_caducidad.Equals(caducidad) && f_lote.Equals(lote))
				{
					datagridview_indice = Convert.ToInt32(row.Index);
				}
			}

			return datagridview_indice;
		}

		public int get_cantidad_producto_datagridview(DataRow producto)
		{
			string amecop = producto["amecop"].ToString();
            string caducidad = (producto["caducidad"].ToString().Equals("0000-00-00") || producto["caducidad"].ToString().Equals("0000-00-00 00:00:00")) ? "SIN CAD" : Misc_helper.fecha(producto["caducidad"].ToString(), "CADUCIDAD");
			string lote = producto["lote"].ToString();

			int datagridview_cantidad = 0;

			foreach (DataGridViewRow row in dgv_venta_cencelacion.Rows)
			{
				string f_amecop = row.Cells["c_amecop"].Value.ToString();
				string f_caducidad = row.Cells["c_caducidad"].Value.ToString();
				string f_lote = row.Cells["c_lote"].Value.ToString();

				if (f_amecop.Equals(amecop) && f_caducidad.Equals(caducidad) && f_lote.Equals(lote))
				{
					datagridview_cantidad = Convert.ToInt32(row.Cells["c_cantidad"].Value);
				}
			}	

			return datagridview_cantidad;
		}

		public bool validar_existencia_producto_datagridview(DataRow producto)
		{
			string amecop = producto["amecop"].ToString();
            string caducidad = (producto["caducidad"].ToString().Equals("0000-00-00") || producto["caducidad"].ToString().Equals("0000-00-00 00:00:00")) ? "SIN CAD" : Misc_helper.fecha(producto["caducidad"].ToString(), "CADUCIDAD");
			string lote = producto["lote"].ToString();

			bool es_promocion = Convert.ToBoolean(producto["es_promocion"]);
			bool existe_producto = false;


			foreach(DataGridViewRow row in dgv_venta_cencelacion.Rows)
			{
				string f_amecop = row.Cells["c_amecop"].Value.ToString();
				string f_caducidad = row.Cells["c_caducidad"].Value.ToString();
				string f_lote = row.Cells["c_lote"].Value.ToString();

				if(es_promocion)
				{
					if (f_amecop.Equals(amecop))
					{
						existe_producto = true;
					}
				}
				else
				{
					if (f_amecop.Equals(amecop) && f_caducidad.Equals(caducidad) && f_lote.Equals(lote))
					{
						existe_producto = true;
					}
				}
			}

			return existe_producto;
		}

		public void rellenar_caducidades(string amecop)
		{
			var caducidades = dao_ventas.get_caducidades(amecop, Convert.ToInt64(venta_id));

			cbb_caducidad.Enabled = true;

			if (caducidades.Rows.Count > 0)
			{
				cbb_caducidad.Items.Clear();

				foreach (DataRow row in caducidades.Rows)
				{
					ComboBoxItem item = new ComboBoxItem();
                    item.Text = Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");
					item.Value = row["caducidad"].ToString();
					item.elemento_id = Convert.ToInt32(row["articulo_id"]);
					cbb_caducidad.Items.Add(item);
				}
			}
			else
			{
				var info_producto = dao_articulos.get_articulo(amecop);
				ComboBoxItem item = new ComboBoxItem();
				item.Text = "SIN CAD";
				item.Value = "0000-00-00";
				item.elemento_id = Convert.ToInt32(info_producto.Articulo_id);
				cbb_caducidad.Items.Add(item);
			}

			cbb_caducidad.DroppedDown = true;
			cbb_caducidad.SelectedIndex = 0;
			cbb_caducidad.Focus();
            txt_amecop.Enabled = false;
		}

		public void validar_registro_producto()
		{
			var dto_producto = dao_articulos.get_articulo(txt_amecop.Text);

			if (dto_producto.Articulo_id != null)
			{
				var producto_data = dao_ventas.exite_producto_venta((int)dto_producto.Articulo_id, Convert.ToInt64(venta_id));

				if(producto_data.Rows.Count > 0)
				{
					bool es_promocion = Convert.ToBoolean(producto_data.Rows[0]["es_promocion"]);

					txt_producto.Text = dto_producto.Nombre;

					if (es_promocion)
					{
						bool existe_producto = validar_existencia_producto_datagridview(producto_data.Rows[0]);

						if (existe_producto)
						{
							MessageBox.Show(this, "Este producto ya se encuentra registrado en su totalidad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							limpiar_informacion();
						}
						else
						{
							DialogResult dr = MessageBox.Show(this, "Este producto fue vendido con promoción, se agregaran TODOS los productos a la devolución, ¿Desea Continuar?", "Importante", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

							if (dr == DialogResult.Yes)
							{
								agregar_producto_grid_cancelacion(producto_data);
							}
							else
							{
								limpiar_informacion();
							}
						}
					}
					else
					{
						rellenar_caducidades(producto_data.Rows[0]["amecop"].ToString());
					}
				}
				else
				{
					MessageBox.Show(this, "Este producto no se encuentra en la venta a cancelar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					limpiar_informacion();
				}
			}
			else
			{
				limpiar_informacion();
				MessageBox.Show(this, "Codigo de producto no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void busqueda_producto()
		{
			DTO_Articulo articulo = dao_articulos.get_articulo(txt_amecop.Text);

			if (articulo.Articulo_id != null)
			{
				rellenar_informacion_producto(articulo);
			}
			else
			{
				txt_amecop.Text = "";
				MessageBox.Show(this, "Producto no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		public void rellenar_informacion_producto(DTO_Articulo articulo)
		{
			//txt_amecop.ReadOnly = true;
			txt_producto.Text = articulo.Nombre;
			cbb_caducidad.Enabled = true;

			if (articulo.Caducidades.Rows.Count > 0)
			{
				cbb_caducidad.Items.Clear();

				foreach (DataRow row in articulo.Caducidades.Rows)
				{
					ComboBoxItem item = new ComboBoxItem();
                    item.Text = (row["caducidad"].ToString().Equals("0000-00-00") || row["caducidad"].ToString().Equals("0000-00-00 00:00:00")) ? " " : Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");
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
				limpiar_informacion();
			}
		}

		public void limpiar_informacion()
		{
            txt_amecop.Enabled = true;

			txt_cantidad.Enabled = false;
			txt_cantidad.Text = "";

			txt_amecop.Text = "";
			//txt_amecop.ReadOnly = false;

			cbb_caducidad.Enabled = false;
			cbb_lote.Enabled = false;

			cbb_caducidad.Items.Clear();
			cbb_lote.Items.Clear();

			txt_producto.Text = "";

			txt_amecop.Focus();

			dgv_venta_cencelacion.ClearSelection();
		}

		private void txt_cantidad_KeyDown(object sender, KeyEventArgs e)
		{
			if (Convert.ToInt32(e.KeyCode) == 27)
			{
				limpiar_informacion();
			}

			if (Convert.ToInt32(e.KeyCode) == 13 && txt_cantidad.TextLength > 0)
			{
				registrar_producto_cancelacion();
			}
		}

		public void registrar_producto_cancelacion()
		{
			ComboBoxItem item_cad = (ComboBoxItem)cbb_caducidad.SelectedItem;
			ComboBoxItem item_lote = (ComboBoxItem)cbb_lote.SelectedItem;
			
            int articulo_id = (int)item_cad.elemento_id;

			int cantidad = Convert.ToInt32(txt_cantidad.Text);

			//var producto_data = dao_ventas.exite_producto_venta(articulo_id, Convert.ToInt64(venta_id));
			var producto_data = dao_ventas.exite_producto_venta(articulo_id, item_cad.Value.ToString(),item_lote.Value.ToString(), Convert.ToInt64(venta_id));

			if(producto_data.Rows.Count > 0)
			{
				bool existe_producto = validar_existencia_producto_datagridview(producto_data.Rows[0]);

				if(existe_producto)
				{
					int cantidad_total = Convert.ToInt32(producto_data.Rows[0]["cantidad"]);
					int cantidad_capturada =  get_cantidad_producto_datagridview(producto_data.Rows[0]);

					int cantidad_disponible = (cantidad_total  -  cantidad_capturada);

					if(Convert.ToInt32(txt_cantidad.Text) <= cantidad_disponible)
					{
						//dgv_venta_cencelacion.Rows.RemoveAt(get_indice_producto_datagridview(producto_data.Rows[0]));
						//producto_data.Rows[0]["cantidad"] = ( Convert.ToInt32(txt_cantidad.Text) + cantidad_capturada );
						//agregar_producto_grid_cancelacion(producto_data);
                        dgv_venta_cencelacion.Rows.RemoveAt(get_indice_producto_datagridview(producto_data.Rows[0]));
                        int nueva_cantidad = (Convert.ToInt32(txt_cantidad.Text) + cantidad_capturada);
                        producto_data.Rows[0]["cantidad"] = nueva_cantidad;
                        producto_data.Rows[0]["subtotal"] = nueva_cantidad *Convert.ToDecimal(producto_data.Rows[0]["importe"]);
                        producto_data.Rows[0]["total"] = Convert.ToDecimal(producto_data.Rows[0]["subtotal"]) + (Convert.ToDecimal(producto_data.Rows[0]["subtotal"]) * Convert.ToDecimal(producto_data.Rows[0]["pct_iva"]));
                        agregar_producto_grid_cancelacion(producto_data);
					}
					else
					{
						if(cantidad_disponible == 0)
						{
							MessageBox.Show(this, "Ya se ha registrado la cantidad total de este producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							limpiar_informacion();	
						}
						else
						{
							string informacion = string.Format("Dispones de {0} piezas de {1}, hay {2} capturadas", cantidad_disponible, cantidad_total, cantidad_capturada);
							MessageBox.Show(this, informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							limpiar_informacion();
						}
					}
				}
				else
				{
					if (Convert.ToInt32(producto_data.Rows[0]["cantidad"]) >= cantidad)
					{
						producto_data.Rows[0]["cantidad"] = cantidad;
                        producto_data.Rows[0]["subtotal"] = cantidad*Convert.ToDecimal(producto_data.Rows[0]["importe"]);
                        producto_data.Rows[0]["total"] = Convert.ToDecimal( producto_data.Rows[0]["subtotal"] ) + ( Convert.ToDecimal(producto_data.Rows[0]["subtotal"])*Convert.ToDecimal(producto_data.Rows[0]["pct_iva"] ));
						agregar_producto_grid_cancelacion(producto_data);
                        //producto_data.Rows[0]["cantidad"] = cantidad;
                        //agregar_producto_grid_cancelacion(producto_data);
					}
					else
					{
						MessageBox.Show(this, string.Format("No puedes devolver mas piezas de las registradas, dispones de {0} piezas", producto_data.Rows[0]["cantidad"].ToString()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}	
				}

                get_totales_cancelacion();
			}
			else
			{
				MessageBox.Show(this, "Este producto no se encuentra registrado","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				limpiar_informacion();
			}
		}

		public void agregar_producto_grid_cancelacion(DataTable producto)
		{
			foreach(DataRow row in producto.Rows)
			{
				int articulo_id = Convert.ToInt32(row["articulo_id"]);
				string amecop = row["amecop"].ToString();
				string nombre_producto = row["nombre"].ToString();
				string caducidad = row["caducidad"].ToString();
				string lote = row["lote"].ToString();
                decimal importe_ieps = Convert.ToDecimal(row["importe_ieps"]);
				decimal importe_iva = Convert.ToDecimal(row["importe_iva"]);
				decimal importe = Convert.ToDecimal(row["importe"]);
				int cantidad = Convert.ToInt32(row["cantidad"]);
				decimal subtotal = Convert.ToDecimal(row["subtotal"]);
				decimal total = Convert.ToDecimal(row["total"]);
				bool es_promocion = Convert.ToBoolean(row["es_promocion"]);

				if(es_promocion)
				{
					if (!productos_promocion.ContainsKey(amecop)) {
						productos_promocion.Add(amecop,es_promocion);
					}
				}

				dgv_venta_cencelacion.Rows.Add(amecop, nombre_producto, (caducidad.Equals("0000-00-00") || caducidad.Equals("0000-00-00 00:00:00")) ? "SIN CAD" : Misc_helper.fecha(caducidad, "CADUCIDAD"), lote, importe_iva, importe, cantidad, subtotal, total, es_promocion, articulo_id,importe_ieps);
				limpiar_informacion();
				dgv_venta_cencelacion.ClearSelection();
			}
		}

		public void busqueda_lotes(ComboBoxItem item)
		{
			DataTable result_lotes = dao_ventas.get_lotes((int)item.elemento_id, item.Value.ToString(),Convert.ToInt64(venta_id));
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
				limpiar_informacion();
			}
		}

		private void dgv_venta_cencelacion_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 27:
					txt_amecop.Focus();
					dgv_venta_cencelacion.ClearSelection();
				break;
				case 46:
                    if(dgv_venta_cencelacion.SelectedRows.Count > 0)
                    {
                        string amecop = dgv_venta_cencelacion.SelectedRows[0].Cells["c_amecop"].Value.ToString();

                        var existe = productos_promocion.ContainsKey(amecop);

                        if (existe)
                        {
                            eliminar_productos_promocion(amecop);
                        }
                        else
                        {
                            int indice = dgv_venta_cencelacion.SelectedRows[0].Index;
                            dgv_venta_cencelacion.Rows.RemoveAt(indice);
                        }

                        get_totales_cancelacion();

                        limpiar_informacion();
                    }
				break;
			}
		}

		public void eliminar_productos_promocion(string amecop)
		{
			var grid_rows = dgv_venta_cencelacion.Rows;

			foreach(DataGridViewRow row in grid_rows)
			{
				string f_amecop = row.Cells["c_amecop"].Value.ToString();

				if(amecop.Equals(f_amecop))
				{
					dgv_venta_cencelacion.Rows[row.Index].Selected = true;
				}
			}
			
			foreach (DataGridViewRow selected_row in dgv_venta_cencelacion.SelectedRows)
			{
				dgv_venta_cencelacion.Rows.Remove(selected_row);
			}

			productos_promocion.Remove(amecop);
		}

		private void btn_tp3_finalizar_Click(object sender, EventArgs e)
		{
            
		}

		public bool cancelar_venta()
		{
			Cursor = Cursors.WaitCursor;

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			long empleado_id = (long)Principal.empleado_id;
            string nombre_cliente = txNombreClienteLleva.Text.ToString();
            long telefono = Convert.ToInt64(txtTelefonoLleva.Text.ToString());
            string correo = txtCorreoLleva.Text.ToString();
           
            bool validacion = false;
            validacion = dao_cancelaciones.set_ajuste_cancelacion(nombre_cliente, telefono, correo, Convert.ToInt64(venta_id), txt_motivo_cancelacion.Text, productos_cancelados, productos_comprados, emitir_cancelacion_factura, emitir_nota_credito);
            #region EmisionNotasCredito
            /*
			if (emitir_cancelacion_factura)
			{
				//PROCESO CANCELACION DE LA FACTURACIÓN 
                //validacion = dao_cancelaciones.set_ajuste_cancelacion(nombre_cliente,telefono,correo, Convert.ToInt64(venta_id), txt_motivo_cancelacion.Text, productos_cancelados, productos_comprados, emitir_cancelacion_factura, emitir_nota_credito);
				/*
				var result_cancelacion = WebServicePac_helper.cancelar(venta_id);

				if(result_cancelacion.status)
				{
					//validacion = dao_cancelaciones.cancelar_venta(Convert.ToInt64(venta_id), txt_motivo_cancelacion.Text, productos_no_cancelados, emitir_cancelacion_factura, emitir_nota_credito);}
                    //validacion = dao_cancelaciones.cancelar_venta_compra(Convert.ToInt64(venta_id), txt_motivo_cancelacion.Text, productos_cancelados, productos_comprados, emitir_cancelacion_factura, emitir_nota_credito);
				}
				else
				{
					validacion = result_cancelacion;
				}
                
			}
            else
			{
				if (emitir_nota_credito)
				{
                   // validacion = dao_cancelaciones.set_ajuste_cancelacion(nombre_cliente,telefono,correo,Convert.ToInt64(venta_id), txt_motivo_cancelacion.Text, productos_cancelados, productos_comprados, emitir_cancelacion_factura, emitir_nota_credito);
					
				}
				else
				{
                  //  validacion = dao_cancelaciones.set_ajuste_cancelacion(nombre_cliente,telefono,correo,Convert.ToInt64(venta_id), txt_motivo_cancelacion.Text, productos_cancelados, productos_comprados, emitir_cancelacion_factura, emitir_nota_credito);
				}
			}
            */
            #endregion
            if (validacion)
			{
				CLASSES.PRINT.Devolucion devolucion = new CLASSES.PRINT.Devolucion();
				devolucion.construccion_ticket(Convert.ToInt64(venta_id),false,false,true);
				devolucion.print();
                devolucion.print();
                #region
                //emitir NOTACREDITO PARCIAL
                //if (emitir_nota_credito)
                //{
                    /*
                    var result_cancelacion = WebServicePac_helper.existe_factura(venta_id);
                    if (result_cancelacion.status)
                    {

                        string uuid = result_cancelacion.uuid;
                        string receptor = result_cancelacion.rfc_receptor;
                       
                        string emisor = result_cancelacion.rfc_emisor;
                        string nombreemisor = result_cancelacion.razon_social;
                        this.set_cancelacion_parcial_xml(uuid, receptor, nombreemisor, emisor, productos_cancelados);
                       
                      }
                      else
                      {
                            var resultado_envio = WebServicePac_helper.envio_cancelacion(venta_id);
                      }
                     */
                //}
                #endregion

            }
			else
			{
				MessageBox.Show(this, "ERROR EN LA DEVOLUCION", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			Cursor = Cursors.Default;

			return validacion;
		}

		public void generar_productos_devolucion()
		{
			productos_no_cancelados = new List<Tuple<int, string, string,decimal, int>>();

			foreach (DataGridViewRow grid_venta_row in dgv_ventas.Rows)
			{
				int v_articulo_id = Convert.ToInt32(grid_venta_row.Cells["columna_articulo_id"].Value);
				string v_amecop = grid_venta_row.Cells["amecop"].Value.ToString();
				string v_caducidad = grid_venta_row.Cells["caducidad"].Value.ToString();
				string v_lote = grid_venta_row.Cells["lote"].Value.ToString();
				int v_cantidad = Convert.ToInt32(grid_venta_row.Cells["cantidad"].Value);
				decimal v_importe = Convert.ToDecimal(grid_venta_row.Cells["importe"].Value);
				bool cancelar = false;

				foreach (DataGridViewRow grid_cancelacion_row in dgv_venta_cencelacion.Rows)
				{
					int c_articulo_id = Convert.ToInt32(grid_cancelacion_row.Cells["c_columna_articulo_id"].Value);
					string c_amecop = grid_cancelacion_row.Cells["c_amecop"].Value.ToString();
					string c_caducidad = grid_cancelacion_row.Cells["c_caducidad"].Value.ToString();
					string c_lote = grid_cancelacion_row.Cells["c_lote"].Value.ToString();
					int c_cantidad = Convert.ToInt32(grid_cancelacion_row.Cells["c_cantidad"].Value);
					decimal c_importe = Convert.ToDecimal(grid_cancelacion_row.Cells["c_importe"].Value);

					if (!productos_promocion.ContainsKey(v_amecop))
					{
						if (v_articulo_id == c_articulo_id && v_caducidad == c_caducidad && v_lote == c_lote && v_importe == c_importe)
						{
							if ((v_cantidad - c_cantidad) > 0)
							{
								Tuple<int, string, string,decimal, int> producto = new Tuple<int, string, string,decimal, int>(v_articulo_id, Misc_helper.CadtoDate(v_caducidad), v_lote, v_importe, (v_cantidad - c_cantidad));
								productos_no_cancelados.Add(producto);
							}

							cancelar = true;
						}
					}
					else
					{
						cancelar = true;
					}
				}
                
				if (cancelar == false)
				{
					Tuple<int, string, string,decimal,int> producto = new Tuple<int, string, string,decimal, int>(v_articulo_id, Misc_helper.CadtoDate(v_caducidad), v_lote,v_importe, v_cantidad);
					productos_no_cancelados.Add(producto);
				}
                 
			}
		}
        public void generar_productos_devolucion_compra()
        {

            productos_cancelados = new List<Tuple<int, string, string, decimal, int>>();

            foreach (DataGridViewRow grid_cancelacion_row in dgv_venta_cencelacion.Rows)
            {
                int c_articulo_id = Convert.ToInt32(grid_cancelacion_row.Cells["c_columna_articulo_id"].Value);
                string c_amecop = grid_cancelacion_row.Cells["c_amecop"].Value.ToString();
                string c_caducidad = grid_cancelacion_row.Cells["c_caducidad"].Value.ToString();
                string c_lote = grid_cancelacion_row.Cells["c_lote"].Value.ToString();
                int c_cantidad = Convert.ToInt32(grid_cancelacion_row.Cells["c_cantidad"].Value);
                decimal c_importe = Convert.ToDecimal(grid_cancelacion_row.Cells["c_importe"].Value);

                Tuple<int, string, string, decimal, int> producto = new Tuple<int, string, string, decimal, int>(c_articulo_id, Misc_helper.CadtoDate(c_caducidad), c_lote, c_importe, c_cantidad);
                productos_cancelados.Add(producto);
                       
            }

            foreach (DataGridViewRow grid_lleva_row in dataGridVieweLleva.Rows)
            {
                int c_articulo_id = Convert.ToInt32(grid_lleva_row.Cells["lleva_articulo_id"].Value);
                string c_amecop = grid_lleva_row.Cells["lleva_amecop"].Value.ToString();
                string c_caducidad = grid_lleva_row.Cells["lleva_caducidad"].Value.ToString();
                string c_lote = grid_lleva_row.Cells["lleva_lote"].Value.ToString();
                int c_cantidad = Convert.ToInt32(grid_lleva_row.Cells["lleva_cantidad"].Value);
                decimal c_importe = Convert.ToDecimal(grid_lleva_row.Cells["lleva_importe"].Value);

                Tuple<int, string, string, decimal, int> producto = new Tuple<int, string, string, decimal, int>(c_articulo_id, Misc_helper.CadtoDate(c_caducidad), c_lote, c_importe, c_cantidad);
                productos_comprados.Add(producto);

            }

        
        }


		private void btn_tp3_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btn_tp2_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

        private void txt_motivo_cancelacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_motivo_cancelacion_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch(keycode)
            {
                case 13:
                    txt_amecop.Focus();
                break;
            }
        }

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txt_folio_venta_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonsigdev_Click(object sender, EventArgs e)
        {
            tp_contenido_venta.Parent = null;
            tp_productos_cancelar.Parent = tabControllleva;
            

        }

        private void btn_sig_lleva_Click(object sender, EventArgs e)
        {
            tp_productos_cancelar.Parent = null;
            tp_productos_lleva.Parent = tabControllleva;
            txtAmecopLleva.Focus();
            get_totales_productos_lleva();
        }

        private void btn_atras_lleva_Click(object sender, EventArgs e)
        {
            tp_contenido_venta.Parent = null;
            tp_productos_lleva.Parent = null;
            tp_productos_cancelar.Parent = tabControllleva;
            txt_motivo_cancelacion.Focus();
            get_totales_cancelacion();
        }

        private void btn_cancelar_lleva_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btn_tp3_finalizar_Click_1(object sender, EventArgs e)
        {
            /*
            try
            {
                
                if (txt_motivo_cancelacion.TextLength > 0)
                {
                    DialogResult dr = MessageBox.Show(this, "Esta a punto de terminar la cancelación, ¿Desea continuar?", "Devolución", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        if (dgv_venta_cencelacion.Rows.Count > 0)
                        {
                            //Tipo_devolucion tipo_devolucion = new Tipo_devolucion(venta_id);
                            //tipo_devolucion.ShowDialog();

                            //if (tipo_devolucion.tipo_devolucion != "")
                          //  {
                                //generar_productos_devolucion();
                                /*
                                if (cancelar_venta())
                                {
                                    var tmp = dao_ventas.get_venta_data(venta_id);
                                    if (!tmp.cliente_credito_id.Equals(""))
                                    {
                                        long empleado_id = (long)Principal.empleado_id;
                                        long sucursal_id = Convert.ToInt64(Config_helper.get_config_local("sucursal_id"));
                                        Dictionary<string, object> parametros = new Dictionary<string, object>();
                                        parametros.Add("cliente_id", tmp.cliente_credito_id);
                                        parametros.Add("sucursal_id", sucursal_id);
                                        parametros.Add("venta_id", venta_id);
                                        parametros.Add("fecha_cancelado", Misc_helper.fecha(DateTime.Now.ToString(), "ISO"));

                                        DAO_Cola_operaciones.insertar_cola_operaciones(empleado_id, "rest/ventas", "cancelar_venta_credito", parametros, "PARA ENVIO A SERVIDOR PRINCIPAL");
                                    }

                                    if (tipo_devolucion.tipo_devolucion == "VALE FARMACIA")
                                    {
                                        DAO_Vales_efectivo dao_vales_efectivo = new DAO_Vales_efectivo();
                                        string vale_efectivo_id = dao_vales_efectivo.generar_vale_efectivo(Convert.ToInt64(venta_id));

                                        if (vale_efectivo_id != "")
                                        {
                                            CLASSES.PRINT.Vale_efectivo devolucion = new CLASSES.PRINT.Vale_efectivo();
                                            devolucion.construccion_ticket(vale_efectivo_id);
                                            devolucion.print();

                                            MessageBox.Show(this, "Devolución afectada correctamente", "Devolución", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            txt_folio_venta.Text = "";
                                            tp_productos_cancelar.Parent = null;
                                            tp_folio_venta.Parent = tabControllleva;
                                            dgv_venta_cencelacion.Rows.Clear();
                                            txt_motivo_cancelacion.Text = "";
                                            txt_amecop.Text = "";
                                            txt_folio_venta.Focus();

                                            this.Close();
                                        }
                                        else
                                        {
                                            this.Close();
                                        }
                                    }
                                    else
                                    {
                                        txt_folio_venta.Text = "";
                                        MessageBox.Show(this, "Devolución afectada correctamente", "Devolucion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        tp_productos_cancelar.Parent = null;
                                        tp_folio_venta.Parent = tabControllleva;
                                        txt_folio_venta.Focus();
                                    }
                                }
                            
                           // }
                        }
                        else
                        {
                            MessageBox.Show(this, "No capturaste productos para cancelar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            limpiar_informacion();
                            limpiarinformacion_lleva();
                        }
                    }
                }
                else
                {
                    MessageBox.Show(this, "Es necesario registrar el motivo por el cual se estan devolviendo los productos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_motivo_cancelacion.Focus();
                }
            }
            catch (Exception ex)
            {
                Log_error.log(ex);
            }
            */

            
          try
          {



              if (txt_motivo_cancelacion.TextLength > 0 && !txNombreClienteLleva.Text.ToString().Equals("") && !txtTelefonoLleva.Text.ToString().Equals("") && !txtCorreoLleva.Text.ToString().Equals(""))
              {
                  DialogResult dr = MessageBox.Show(this, "Esta a punto de terminar la cancelación, ¿Desea continuar?", "Devolución", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                  if (dr == DialogResult.Yes)
                  {
                      if (dgv_venta_cencelacion.Rows.Count > 0)
                      {
                              generar_productos_devolucion_compra();       
                               
                              if (cancelar_venta())
                              {
                                  var tmp = dao_ventas.get_venta_data(venta_id);
                                  if (!tmp.cliente_credito_id.Equals(""))
                                  {
                                      long empleado_id = (long)Principal.empleado_id;
                                      long sucursal_id = Convert.ToInt64(Config_helper.get_config_local("sucursal_id"));
                                      Dictionary<string, object> parametros = new Dictionary<string, object>();
                                      parametros.Add("cliente_id", tmp.cliente_credito_id);
                                      parametros.Add("sucursal_id", sucursal_id);
                                      parametros.Add("venta_id", venta_id);
                                      parametros.Add("fecha_cancelado", Misc_helper.fecha(DateTime.Now.ToString(), "ISO"));

                                      DAO_Cola_operaciones.insertar_cola_operaciones(empleado_id, "rest/ventas", "cancelar_venta_credito", parametros, "PARA ENVIO A SERVIDOR PRINCIPAL");
                                  }

                                  MessageBox.Show(this, "Devolución afectada correctamente", "Devolución", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                  this.Close();
                                  /*
                                  if (tipo_devolucion.tipo_devolucion == "VALE FARMACIA")
                                  {
                                      DAO_Vales_efectivo dao_vales_efectivo = new DAO_Vales_efectivo();
                                      string vale_efectivo_id = dao_vales_efectivo.generar_vale_efectivo(Convert.ToInt64(venta_id));

                                      if (vale_efectivo_id != "")
                                      {
                                          CLASSES.PRINT.Vale_efectivo devolucion = new CLASSES.PRINT.Vale_efectivo();
                                          devolucion.construccion_ticket(vale_efectivo_id);
                                          devolucion.print();

                                          MessageBox.Show(this, "Devolución afectada correctamente", "Devolución", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                          txt_folio_venta.Text = "";
                                          tp_productos_cancelar.Parent = null;
                                          tp_folio_venta.Parent = tabControllleva;
                                          dgv_venta_cencelacion.Rows.Clear();
                                          txt_motivo_cancelacion.Text = "";
                                          txt_amecop.Text = "";
                                          txt_folio_venta.Focus();

                                          this.Close();
                                      }
                                      else
                                      {
                                          this.Close();
                                      }
                                  }
                                  else
                                  {
                                      txt_folio_venta.Text = "";
                                      MessageBox.Show(this, "Devolución afectada correctamente", "Devolucion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                      tp_productos_cancelar.Parent = null;
                                      tp_folio_venta.Parent = tabControllleva;
                                      txt_folio_venta.Focus();
                                  }
                                  */
                              }
                            
                         
                      }
                      else
                      {
                          MessageBox.Show(this, "No capturaste productos para cancelar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                          limpiar_informacion();
                          limpiarinformacion_lleva();
                      }
                  }
              }
              else
              {
                  MessageBox.Show(this, "Por favor,Complete los campos correctamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  txt_motivo_cancelacion.Focus();
              }
          }
          catch (Exception ex)
          {
              Log_error.log(ex);
          }
          
        }

        private void tabControllleva_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tp_productos_lleva_Click(object sender, EventArgs e)
        {

        }

        private void txt_amecop_TextChanged(object sender, EventArgs e)
        {

        }

        public void limpiarinformacion_lleva()
        {
            txtAmecopLleva.Enabled = true;

            txtCantidadLleva.Enabled = false;
            txtCantidadLleva.Text = "";

            txtAmecopLleva.Text = "";
            //txt_amecop.ReadOnly = false;

            cbxCaducidad.Enabled = false;
            cbxLote.Enabled = false;

            cbxCaducidad.Items.Clear();
            cbxLote.Items.Clear();

            txtCodigoProd.Text = "";

            txtAmecopLleva.Focus();

            dataGridVieweLleva.ClearSelection();

        }

        private void txtAmecopLleva_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 40:
                    /*
                    if (dgv_venta_cencelacion.Rows.Count > 0)
                    {
                        dgv_venta_cencelacion.CurrentCell = dgv_venta_cencelacion.Rows[0].Cells["c_amecop"];
                        dgv_venta_cencelacion.Rows[0].Selected = true;
                        dgv_venta_cencelacion.Focus();
                    }*/
                   // MessageBox.Show(this, "ORALE 40", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 13:
                    if (txtAmecopLleva.TextLength > 0)
                    {

                        busca_producto_lleva();
                    }
                    break;
                case 27:
                    limpiarinformacion_lleva();        
                    break;
            }
        }

        public void busca_producto_lleva()
        {
           
            DTO_Articulo articulo = dao_articulos.get_articulo(txtAmecopLleva.Text);

            if (articulo.Articulo_id != null)
            {
                if (articulo.activo)
                {
                    rellenar_informacion_productolleva(articulo);
                }
                else
                {
                    txtAmecopLleva.Text = "";
                    MessageBox.Show(this, "El producto se encuentra en el catalgo pero esta marcado como inactivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAmecopLleva.Focus();
                }
            }
            else
            {
                txtAmecopLleva.Text = "";
                MessageBox.Show(this, "Producto No encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        
        }

        public void rellenar_informacion_productolleva(DTO_Articulo articulo)
        {
            txtCodigoProd.Text = articulo.Nombre;
            //txt_descuento.Text = ((articulo.Pct_descuento > 0) ? Convert.ToDecimal(articulo.Pct_descuento * 100).ToString("#.##") : "0") + "%";
            cbxCaducidad.Enabled = true;

            if (articulo.Caducidades.Rows.Count > 0)
            {
                cbxCaducidad.Items.Clear();

                foreach (DataRow row in articulo.Caducidades.Rows)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Text = (row["caducidad"].ToString().Equals("0000-00-00") || row["caducidad"].ToString().Equals("0000-00-00 00:00:00")) ? "SIN CAD" : Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD") + " (" + row["existencia"] + ")";
                    item.Value = row["caducidad"].ToString();
                    item.elemento_id = articulo.Articulo_id;
                    cbxCaducidad.Items.Add(item);
                }
            }
            else
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = "SIN CAD";
                item.Value = "0000-00-00";
                item.elemento_id = articulo.Articulo_id;

                cbxCaducidad.Items.Add(item);
            }

            cbxCaducidad.DroppedDown = true;
            cbxCaducidad.SelectedIndex = 0;
            cbxCaducidad.Focus();
            txtAmecopLleva.Enabled = false;
        }

        private void cbxCaducidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                ComboBoxItem item = (ComboBoxItem)cbxCaducidad.SelectedItem;
                busqueda_lotes_lleva(item);
                cbxCaducidad.Text = item.Text;
                cbxCaducidad.Enabled = false;
                cbxLote.Enabled = true;
            }
            else if (Convert.ToInt32(e.KeyCode) == 27)
            {
                txtAmecopLleva.Enabled = true;
                txtAmecopLleva.Focus();
                txtCodigoProd.Text = "";
                cbxCaducidad.Items.Clear();
                cbxCaducidad.Enabled = false;
              
            }
        }

        public void busqueda_lotes_lleva(ComboBoxItem item)
        {
            DataTable result_lotes = dao_articulos.get_lotes((int)item.elemento_id, item.Value.ToString());
            cbxLote.Enabled = true;

            if (result_lotes.Rows.Count > 0)
            {
                cbxLote.Items.Clear();
                foreach (DataRow row in result_lotes.Rows)
                {
                    ComboBoxItem item_lote = new ComboBoxItem();
                    item_lote.Text = row["lote"].ToString();
                    item_lote.Value = row["lote"].ToString();
                    item.elemento_id = item.elemento_id;
                    cbxLote.Items.Add(item_lote);
                }
            }
            else
            {
                ComboBoxItem item_lote = new ComboBoxItem();
                item_lote.Text = "SIN LOTE";
                item_lote.Value = " ";
                item.elemento_id = item.elemento_id;
                cbxLote.Items.Add(item_lote);
            }

            cbxLote.DroppedDown = true;
            cbxLote.SelectedIndex = 0;
            cbxLote.Focus();
        }

        private void cbxLote_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                ComboBoxItem item = (ComboBoxItem)cbxLote.SelectedItem;
                txtCantidadLleva.Enabled = true;
                txtCantidadLleva.Text = "1";
                txtCantidadLleva.SelectAll();
                txtCantidadLleva.Focus();

                cbxLote.Text = item.Text;
                cbxLote.Enabled = false;
            }
            else if (Convert.ToInt32(e.KeyCode) == 27)
            {
                cbxCaducidad.Enabled = true;
                cbxCaducidad.Focus();
                cbxCaducidad.DroppedDown = true;
                cbxLote.Items.Clear();
                cbxLote.Enabled = false;

            }
        }

        private void txtCantidadLleva_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 27)
            {
                limpiarinformacion_lleva(); 
            }

            if (Convert.ToInt32(e.KeyCode) == 13 && txtAmecopLleva.TextLength > 0)
            {
                registra_producto_lleva();
            }
        }

        private void txtCantidadLleva_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        public void registra_producto_lleva()
        {
            //MessageBox.Show(this, "Ya se ha se va a registrar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            ComboBoxItem item_cad = (ComboBoxItem)cbxCaducidad.SelectedItem;
            ComboBoxItem item_lote = (ComboBoxItem)cbxLote.SelectedItem;

            int articulo_id = (int)item_cad.elemento_id;

            int cantidad = Convert.ToInt32(txtCantidadLleva.Text);
            string[] existe_producto = new string[2];
            existe_producto = validar_existencia_producto_datagridview_lleva(txtAmecopLleva.Text, (item_cad.Value.ToString() == " ") ? "0000-00-00" : item_cad.Value.ToString(), item_lote.Value.ToString());
            
            int existencia_vendible = dao_articulos.get_existencia_vendible(txtAmecopLleva.Text, (item_cad.Value.ToString() == " ") ? "0000-00-00" : item_cad.Value.ToString(), item_lote.Value.ToString());
            if (existe_producto[0].Equals("false") )
            {
               
                if (existencia_vendible >= Convert.ToInt64(txtCantidadLleva.Text))
                {

                    DataTable result_codigo = dao_articulos.get_informacion_codigo(txtAmecopLleva.Text, item_cad.Value.ToString(), item_lote.Value.ToString());
                    agregar_producto_grid_lleva(result_codigo, cantidad);
                    get_totales_productos_lleva();
                }
                else
                {
                    MessageBox.Show(this, "La cantidad disponible para vender es de " + existencia_vendible, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                int nueva_existencia = existencia_vendible - Convert.ToInt32(existe_producto[1]);
                if (nueva_existencia >= cantidad)
                {
                    actualiza_indice_producto_datagridview(txtAmecopLleva.Text, item_cad.Value.ToString(), item_lote.Value.ToString(), cantidad);

                    get_totales_productos_lleva();

                }
                else
                { 
                        MessageBox.Show(this, string.Format("No puedes agregar mas piezas , dispones de {0} piezas" ,nueva_existencia, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) );
                }
                
            }
            

        }

        public void agregar_producto_grid_lleva(DataTable producto,int cantidad)
        {
            foreach (DataRow row in producto.Rows)
            {
                int articulo_id = Convert.ToInt32(row["articulo_id"]);
                string amecop = row["amecop"].ToString();
                string nombre_producto = row["nombre"].ToString();
                string caducidad = row["caducidad"].ToString();
                string lote = row["lote"].ToString();

               

                decimal importe =  Convert.ToDecimal(row["precio_publico"]) - (Convert.ToDecimal(row["precio_publico"]) * Convert.ToDecimal(row["pct_descuento"]));
                decimal importe_iva = Convert.ToDecimal(row["pct_iva"]);

                //decimal porcentaje_descuento = Convert.ToDecimal(row["pct_descuento"]);
                decimal porcentaje_iva = Convert.ToDecimal(row["pct_iva"]);

                decimal importe_ieps = Convert.ToDecimal(row["ieps"]);
                

                
                //decimal importe = Convert.ToDecimal(row["importe"]);
                //int cantidad = Convert.ToInt32(row["cantidad"]);
                decimal subtotal = importe*cantidad;
                decimal total = subtotal + ( importe_iva * subtotal  );
                bool es_promocion = false ;

                if (es_promocion)
                {
                    if (!productos_promocion.ContainsKey(amecop))
                    {
                        productos_promocion.Add(amecop, es_promocion);
                    }
                }

                dataGridVieweLleva.Rows.Add(amecop, nombre_producto, (caducidad.Equals("0000-00-00") || caducidad.Equals("0000-00-00 00:00:00")) ? "SIN CAD" : Misc_helper.fecha(caducidad, "CADUCIDAD"), lote, importe_iva, importe, cantidad, subtotal, total, es_promocion, articulo_id, importe_ieps);
                limpiarinformacion_lleva();
                dataGridVieweLleva.ClearSelection();
            }
        }

        public void get_totales_productos_lleva()
        {
            decimal total = 0;
            decimal subtotal = 0;
            int piezas = 0;
            decimal iva = 0;
            decimal diferencia = 0;

            if (dataGridVieweLleva.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridVieweLleva.Rows)
                {
                    total += Convert.ToDecimal(row.Cells["lleva_total"].Value);
                    subtotal += Convert.ToDecimal(row.Cells["lleva_subtotal"].Value);
                    iva += Convert.ToDecimal(row.Cells["lleva_iva"].Value);
                    piezas += Convert.ToInt32(row.Cells["lleva_cantidad"].Value);
                }
            }

            string txtDiff = txt_total.Text.ToString();
            string digito = "0";
            if (!txtDiff.Equals(""))
            {
                digito = txtDiff.Replace("$", "").Replace(",","");
                diferencia = total - Convert.ToDecimal(digito) ; 
            }
           

            txtTotalLleva.Text = total.ToString("C2");
            txtSubtotalLleva.Text = string.Format("{0:C2}", subtotal);

            txtIvaSubtotal.Text = string.Format("{0:C2}", iva);
            txtPzasLleva.Text = piezas.ToString();
            txtDiferenciaLleva.Text = string.Format("{0:C2}", diferencia);

            //es nuevo 
            if (diferencia > 0)
                lblDiferenciaLleva.Text = "PAGA";
            else
                lblDiferenciaLleva.Text = "VALE";
        
        }

        public string[] validar_existencia_producto_datagridview_lleva(string amecop,string caducidad,string lote)
        {
            //bool existe_producto = false;
            string[] array = new string[]{"false","0"};
            caducidad = (caducidad.Equals("0000-00-00") || caducidad.ToString().Equals("0000-00-00 00:00:00")) ? "SIN CAD" : Misc_helper.fecha(caducidad, "CADUCIDAD");
            foreach (DataGridViewRow row in dataGridVieweLleva.Rows)
            {
                string f_amecop = row.Cells["lleva_amecop"].Value.ToString();
                string f_caducidad = row.Cells["lleva_caducidad"].Value.ToString();
                string f_lote = row.Cells["lleva_lote"].Value.ToString();
                string f_cantidad = row.Cells["lleva_cantidad"].Value.ToString();
               
                if (f_amecop.Equals(amecop) && f_caducidad.Equals(caducidad) && f_lote.Equals(lote))
                {
                    //existe_producto = true;

                    array[0] = "true";
                    array[1] = f_cantidad;
                }
               
            }
            
            

            return array;
        }

        public void actualiza_indice_producto_datagridview(string amecop,string caducidad,string lote,int cantidad)
        {
            caducidad = Misc_helper.fecha(caducidad, "CADUCIDAD");
           
            int datagridview_indice = 0;

            foreach (DataGridViewRow row in dataGridVieweLleva.Rows)
            {
                string f_amecop = row.Cells["lleva_amecop"].Value.ToString();
                string f_nombre = row.Cells["lleva_nombre"].Value.ToString();
                string f_caducidad = row.Cells["lleva_caducidad"].Value.ToString();
                string f_lote = row.Cells["lleva_lote"].Value.ToString();
                 int cantidad_nueva = Convert.ToInt32(row.Cells["lleva_cantidad"].Value) + cantidad;

                decimal importe_iva = Convert.ToDecimal(row.Cells["lleva_iva"].Value);
                decimal importe = Convert.ToDecimal(row.Cells["lleva_importe"].Value);
               
                bool es_promocion = false;
                int articulo_id = Convert.ToInt32(row.Cells["lleva_articulo_id"].Value);
                decimal importe_ieps = Convert.ToInt32(row.Cells["lleva_importe_ieps"].Value);
                decimal subtotal = importe*cantidad_nueva;
                decimal total = subtotal + (importe_iva * subtotal);


                if (f_amecop.Equals(amecop) && f_caducidad.Equals(caducidad) && f_lote.Equals(lote))
                {
                    datagridview_indice = Convert.ToInt32(row.Index);
                    dataGridVieweLleva.Rows.RemoveAt(datagridview_indice);
                    dataGridVieweLleva.Rows.Add(amecop, f_nombre, caducidad, f_lote, importe_iva, importe, cantidad_nueva, subtotal, total, es_promocion, articulo_id, importe_ieps);
                    limpiarinformacion_lleva();
                    dataGridVieweLleva.ClearSelection();

                }
            }

            //return datagridview_indice;
        }

        private void dataGridVieweLleva_KeyDown(object sender, KeyEventArgs e)
        {

            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 27:
                    txtAmecopLleva.Focus();
                    dataGridVieweLleva.ClearSelection();
                    break;
                case 46:
                    if (dataGridVieweLleva.SelectedRows.Count > 0)
                    {
                        string amecop = dataGridVieweLleva.SelectedRows[0].Cells["lleva_amecop"].Value.ToString();


                            int indice = dataGridVieweLleva.SelectedRows[0].Index;
                            dataGridVieweLleva.Rows.RemoveAt(indice);


                            get_totales_productos_lleva();

                           limpiarinformacion_lleva();
                    }
                    break;
            }

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            
            
        }

        private void btnCancelacionTotal_Click(object sender, EventArgs e)
        {
            //MOSTRARA LA VENTANA DE ESCRIBIR LOS DATOS DEL CLIENTE
            //NOMBRE
            //TELEFONO
            //MOTIVO
            //CORREO
            tp_tipodev.Parent = null;
            /*
			tp_productos_cancelar.Parent = tabControllleva;
			txt_motivo_cancelacion.Focus();
            get_totales_cancelacion();
             * */
            tp_infocancela.Parent = tabControllleva;
            

        }

        private void bntCancelarInfo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //CANCELACION TOTAL
        private void btnFinalizarCancelacion_Click(object sender, EventArgs e)
        {
            //Aqui se termina la cancelacion 
            //se valida los datos de nombre, telefono, correo y motivo por el cual se hara la cancelacion

             DialogResult dr = MessageBox.Show(this, "Esta a punto de terminar la cancelación, ¿Desea continuar?", "Devolución", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

             if (dr == DialogResult.Yes)
             {
                 string nombreCliente = txtNombreCliente.Text.ToString();
                 string telefono = txtTelefonoCliente.Text.ToString();
                 string correo = txtCorreoCliente.Text.ToString();
                 string motivo = txtMotivoCancelacion.Text.ToString();

                 bool validoEmail = false;
                 if( !correo.Trim().Equals("") )
                      validoEmail = validarEmail(correo);
  

                 if (!nombreCliente.Trim().Equals("") && !telefono.Trim().Equals("") && !telefono.Trim().Equals("") && !motivo.Trim().Equals("") && validoEmail == true)
                 {
                     Int64 Tel = Convert.ToInt64(telefono);

                     bool resultado = dao_cancelaciones.cancelacion_total_venta(venta_id,nombreCliente,Tel,correo,motivo);

                     if (resultado)
                     {

                         CLASSES.PRINT.Devolucion devolucion = new CLASSES.PRINT.Devolucion();
                         devolucion.construccion_ticket(Convert.ToInt64(venta_id), false);

                         devolucion.print();
                         devolucion.print();
                         //PROCESO  QUE EMITE UNA NC
                         if (emitir_nota_credito)
                         {

                              var result_cancelacion = WebServicePac_helper.existe_factura(venta_id);
                              if (result_cancelacion.status)
                              {
                                  var existe_factura = WebServicePac_helper.cancelar(venta_id);

                                  if (existe_factura.status)
                                      MessageBox.Show(this, string.Format("Factura cancelada correctamente !!!", MessageBoxButtons.OK, MessageBoxIcon.Error));
                                  else
                                  {
                                      var resultado_envio = WebServicePac_helper.envio_cancelacion(venta_id);

                                  }
                              }
                              else
                              {
                                  var resultado_envio = WebServicePac_helper.envio_cancelacion(venta_id);

                              }
                               
                         }




                         MessageBox.Show(this, string.Format("Cancelacion terminada correctamente !!!", MessageBoxButtons.OK, MessageBoxIcon.Error));
                         this.Close();
                     }
                     else
                     {
                         MessageBox.Show(this, string.Format("Cancelacion no terminada correctamente, comunicate con el Administrador !!!","ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error));
                     }


                 }
                 else
                 {

                     MessageBox.Show(this, string.Format("Completa los campos correctamente, para realizar la cancelacion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error));
                     if (validoEmail == false)
                     {
                         MessageBox.Show(this, string.Format("Escribe un correo valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error));
                         txtCorreoCliente.Focus();
                     }
                 }
             }
        }

        private void txtNombreCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            //txtNombreCliente.Text = txtNombreCliente.Text.ToString().ToUpper();
        }

        private void txtNombreCliente_Leave(object sender, EventArgs e)
        {
            txtNombreCliente.Text = mayusculas(txtNombreCliente.Text.ToString());
        }

        public string mayusculas( string texto )
        {

            return texto.ToUpper();
        }

        private void txtMotivoCancelacion_Leave(object sender, EventArgs e)
        {
            txtMotivoCancelacion.Text = mayusculas(txtMotivoCancelacion.Text.ToString());
        }

        private void txtTelefonoCliente_KeyPress(object sender, KeyPressEventArgs e)
        {

               if (Char.IsDigit(e.KeyChar))
               {
                   e.Handled = false;
               }
               else if (Char.IsControl(e.KeyChar))
               {
                    e.Handled = false;
               }
               else if (Char.IsSeparator(e.KeyChar))
               {
                   e.Handled = false;
               }
               else
               {
                   e.Handled = true;
               }

        }

        private void btnCancelacionParcial_Click(object sender, EventArgs e)
        {
            tp_tipodev.Parent = null;
            txt_motivo_cancelacion.Focus();
            get_totales_cancelacion(); 
            tp_productos_cancelar.Parent = tabControllleva;

        }

        private void bntatrasinfo_Click(object sender, EventArgs e)
        {
            tp_infocancela.Parent = null;
            tp_tipodev.Parent = tabControllleva;
        }

        private void txtTelefonoLleva_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public bool validarEmail(string email)
        {
            try
            {
                new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        
        }


        public void set_cancelacion_parcial_xml( string uuid,string rfc_receptor , string nombrereceptor,  string rfc_emisor,List<Tuple<int, string, string, decimal, int>> lista_productos_cancelados )
        {
            string serie = "";
            string folio = "";
            string fecha = "";
            string metodo_pago = "";
            string metodopagos = "";
            decimal subtotal = 0;
            decimal descuento = 0;
            decimal total = 0;

            string cabeza_xml = "<?xml version='1.0' encoding='UTF-8'?>";
            cabeza_xml       += "<cfdi:Comprobante xmlns:cfdi='http://www.sat.gob.mx/cfd/3' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='http://www.sat.gob.mx/cfd/3";
            cabeza_xml += "http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd' Version='3.3' Serie='" + serie + "' Folio='" + folio + "' Fecha='" + fecha + "' TipoDeComprobante='E' FormaPago='" + metodo_pago + "'";
            cabeza_xml += " MetodoPago='" + metodopagos + "' NoCertificado='' SubTotal='" + String.Format("{0:0.00}", subtotal) + "' Descuento='" + String.Format("{0:0.00}", descuento) + "' TipoCambio='1' Moneda='MXN' Total='" + String.Format("{0:0.00}", total) + "'";

            string cuerpo_xml = "";
            foreach (var detallado in lista_productos_cancelados)
            {
                
            }



        
        }

	}
}
