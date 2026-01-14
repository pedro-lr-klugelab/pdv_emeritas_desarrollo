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

namespace Farmacontrol_PDV.FORMS.ventas.devolucion
{
	public partial class Devoluciones_principal : Form
	{
		DAO_Ventas dao_ventas = new DAO_Ventas();
		DAO_Articulos dao_articulos = new DAO_Articulos();
		DAO_Cancelaciones dao_cancelaciones = new DAO_Cancelaciones();
		Dictionary<string,bool> productos_promocion =  new Dictionary<string,bool>();
		List<Tuple<int,string,string,decimal,int>> productos_no_cancelados = new List<Tuple<int,string,string,decimal,int>>();
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
			tp_contenido_venta.Parent = tabControl1;
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

                                DateTime hoy = DateTime.Now;
                                DateTime fecha_nota = Convert.ToDateTime(venta_data.fecha_terminado);

                                TimeSpan tSpan = hoy - fecha_nota;

                                int dias = tSpan.Days;
                                if (dias > 7)
                                {
                                    MessageBox.Show(this, "Solo se pueden realizar devoluciones 3 dias posteriores a la venta, solicita AJUSTE al departamento de Auditoria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                                                    if (dao_ventas.get_existe_venta_corte(venta_id))//ESTA EN ALGUN CORTE Y LA NOTA NO FUE FACTURADA
                                                    {///LA VENTA HA SIDO AGREGADO A LA VT  CON RFC PUBLICO EN GRAL

                                                        emitir_nota_credito = true;
                                                        siguiente_productos_venta();
                                                    }
                                                    else
                                                    {   //LA VENTA ES FACTURADA
                                                        if (venta_data.fecha_facturada != null)
                                                        {

                                                            emitir_cancelacion_factura = true;
                                                            emitir_nota_credito = true;
                                                            siguiente_productos_venta();
                                                        }
                                                        else
                                                        {
                                                            //LA VENTA ES DEL DIA 
                                                            siguiente_productos_venta();
                                                        }
                                                    }
                                                }
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
			tp_folio_venta.Parent = tabControl1;
			txt_folio_venta.Focus();
			txt_folio_venta.SelectAll();
		}

		private void btn_tp_2_siguiente_Click(object sender, EventArgs e)
		{
			tp_contenido_venta.Parent = null;
			tp_productos_cancelar.Parent = tabControl1;
			txt_motivo_cancelacion.Focus();
            get_totales_cancelacion();
		}

		private void btn_tp3_atras_Click(object sender, EventArgs e)
		{
			tp_productos_cancelar.Parent = null;
			tp_contenido_venta.Parent = tabControl1;
			dgv_ventas.ClearSelection();
			btn_tp_2_siguiente.Focus();
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
            string caducidad = (producto["caducidad"].ToString().Equals("0000-00-00") || producto["caducidad"].ToString().Equals("0000-00-00 00:00:00")) ? " " : Misc_helper.fecha(producto["caducidad"].ToString(), "CADUCIDAD");
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
            string caducidad = (producto["caducidad"].ToString().Equals("0000-00-00") || producto["caducidad"].ToString().Equals("0000-00-00 00:00:00")) ? " " : Misc_helper.fecha(producto["caducidad"].ToString(), "CADUCIDAD");
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
						dgv_venta_cencelacion.Rows.RemoveAt(get_indice_producto_datagridview(producto_data.Rows[0]));
						producto_data.Rows[0]["cantidad"] = ( Convert.ToInt32(txt_cantidad.Text) + cantidad_capturada );
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
						agregar_producto_grid_cancelacion(producto_data);
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
            try
            {
                if (txt_motivo_cancelacion.TextLength > 0)
                {
                    DialogResult dr = MessageBox.Show(this, "Esta a punto de terminar la cancelación, ¿Desea continuar?", "Devolución", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        if (dgv_venta_cencelacion.Rows.Count > 0)
                        {
                            Tipo_devolucion tipo_devolucion = new Tipo_devolucion(venta_id);
                            tipo_devolucion.ShowDialog();

                            if (tipo_devolucion.tipo_devolucion != "")
                            {
                                generar_productos_devolucion();

                                if (cancelar_venta())
                                {
                                    var tmp =  dao_ventas.get_venta_data(venta_id);
                                    if(!tmp.cliente_credito_id.Equals(""))
                                    {
                                        long empleado_id = (long)Principal.empleado_id;
                                        long sucursal_id = Convert.ToInt64(Config_helper.get_config_local("sucursal_id"));
                                        Dictionary<string, object> parametros = new Dictionary<string,object>();
                                        parametros.Add("cliente_id", tmp.cliente_credito_id );
                                        parametros.Add("sucursal_id", sucursal_id);
                                        parametros.Add("venta_id", venta_id );
                                        parametros.Add("fecha_cancelado", Misc_helper.fecha(DateTime.Now.ToString(), "ISO") );

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
                                            tp_folio_venta.Parent = tabControl1;
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
                                        tp_folio_venta.Parent = tabControl1;
                                        txt_folio_venta.Focus();
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(this, "No capturaste productos para cancelar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            limpiar_informacion();
                        }
                    }
                }
                else
                {
                    MessageBox.Show(this, "Es necesario registrar el motivo por el cual se estan devolviendo los productos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_motivo_cancelacion.Focus();
                }
            }
            catch(Exception ex)
            {
                Log_error.log(ex);
            }
		}

		public bool cancelar_venta()
		{
			Cursor = Cursors.WaitCursor;

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			long empleado_id = (long)Principal.empleado_id;

			DTO_Validacion validacion = new DTO_Validacion();

			if (emitir_cancelacion_factura)
			{
				//PROCESO CANCELACION DE LA VENTA FACTURADA EN ALGUN DIA

                ////////////////////var result_cancelacion = WebServicePac_helper.cancelar(venta_id);//EMITIR UNA NOTA DE CREDITO POR EL TOTAL DE LA NOTA
                //if (result_cancelacion.status)
                if (true)
				{
					validacion = dao_cancelaciones.cancelar_venta(Convert.ToInt64(venta_id), txt_motivo_cancelacion.Text, productos_no_cancelados, emitir_cancelacion_factura, emitir_nota_credito);
				}
				else
				{
                    MessageBox.Show(this, "Factura fiscal no cancelada. notifica a tu contador ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
            else
			{
				if (emitir_nota_credito)
				{
                   // var result_nota_credito = WebServicePac_helper.cancelar_nota_vm(venta_id);

					//if(result_nota_credito.status)
                    if (true)
					{
                        /*
                        DAO_Facturacion dao_facturacion = new DAO_Facturacion();
                        var venta_data = dao_ventas.get_venta_data(venta_id);
                        dao_facturacion.registrar_nota_credito(venta_id, venta_data.terminal_id, "VENTA");
                        
                        string[] correo_vm = { Config_helper.get_config_global("facturacion_vm_email") };
                        WebServicePac_helper.enviar(venta_id, correo_vm, true, result_nota_credito.folio);

						var existe_factura = WebServicePac_helper.existe_factura(venta_id);

						if(existe_factura.status)
						{
							DAO_Rfcs dao = new DAO_Rfcs();
							var rfc_existe = dao.existe_rfc(existe_factura.rfc_receptor);
							var rfc_data = dao.get_data_rfc(rfc_existe.informacion);

							if(rfc_data.tipo_rfc.Equals("RFC"))
							{
								var status_email = WebServicePac_helper.get_email_factura(existe_factura.factura_dato_fiscal_id);

								if(status_email.status)
								{
									string[] correos = { status_email.informacion };
									WebServicePac_helper.enviar(venta_id,correos,false);
								}

								Farmacontrol_PDV.CLASSES.PRINT.Facturacion ticket_factura = new Farmacontrol_PDV.CLASSES.PRINT.Facturacion();
								ticket_factura.construccion_ticket(venta_id, result_nota_credito, false, true);
								ticket_factura.print();
							}
						}
                        */
						validacion = dao_cancelaciones.cancelar_venta(Convert.ToInt64(venta_id), txt_motivo_cancelacion.Text, productos_no_cancelados, emitir_cancelacion_factura, emitir_nota_credito);
					}
					else
					{
						validacion.status = false;
						
						MessageBox.Show(this, "Error en la generacion de nota de credito, notifica a tu administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				else
				{
					validacion = dao_cancelaciones.cancelar_venta(Convert.ToInt64(venta_id), txt_motivo_cancelacion.Text, productos_no_cancelados, emitir_cancelacion_factura, emitir_nota_credito);
				}
			}

			if(validacion.status)
			{
				CLASSES.PRINT.Devolucion devolucion = new CLASSES.PRINT.Devolucion();
				devolucion.construccion_ticket(Convert.ToInt64(venta_id),false);
				devolucion.print();
			}
			else
			{
				MessageBox.Show(this, validacion.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			Cursor = Cursors.Default;

			return validacion.status;
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

	}
}
