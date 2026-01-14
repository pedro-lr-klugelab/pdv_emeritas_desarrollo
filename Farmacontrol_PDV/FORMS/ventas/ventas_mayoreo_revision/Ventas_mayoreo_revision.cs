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
using System.Threading;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.CLASSES.PRINT;

namespace Farmacontrol_PDV.FORMS.ventas.ventas_mayoreo_revision
{
	public partial class Ventas_mayoreo_revision : Form
	{
		DAO_Ventas_mayoreo dao_mayoreo = new DAO_Ventas_mayoreo();
		DAO_Articulos dao_articulos = new DAO_Articulos();

		private long mayoreo_venta_id;
		DTO_Ventas_mayoreo dto_mayoreo = new DTO_Ventas_mayoreo();
		BindingList<DTO_Detallado_mayoreo_ventas> data = new BindingList<DTO_Detallado_mayoreo_ventas>();

		private long? articulo_id = null;
		private long? terminal_id = null;
		private bool cambios = false;
		System.Threading.Timer _timer_busqueda = null;

		public Ventas_mayoreo_revision()
		{
			Ventas_mayoreo_revision.CheckForIllegalCrossThreadCalls = false;
			InitializeComponent();
			lbl_mensaje_bloqueo.Parent = dgv_mayoreo;
			dgv_mayoreo.DataSource = data;
			_timer_busqueda = new System.Threading.Timer(funcion_hilo, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

		}

		private void funcion_hilo(object obj)
		{
			try
			{
				existen_cambios();

				if (cambios)
				{
					rellenar_informacion();
				}

				if (mayoreo_venta_id > 0)
				{
					DAO_Ventas_mayoreo dao_mayoreo = new DAO_Ventas_mayoreo();
					var result = dao_mayoreo.get_detallado_mayoreo_ventas(mayoreo_venta_id);

					if (result.Count.Equals(data.Count))
					{
						foreach (DTO_Detallado_mayoreo_ventas art in data)
						{
							foreach (DTO_Detallado_mayoreo_ventas art_re in result)
							{
								if (art.amecop.Equals(art_re.amecop) && art.caducidad.Equals(art_re.caducidad) && art.lote.Equals(art_re.lote))
								{
									art.cantidad_capturada = art_re.cantidad_capturada;
									art.cantidad_revision = art_re.cantidad_revision;
									art.diferencia = art_re.diferencia;
									art.importe_iva_captura = art_re.importe_iva_captura;
									art.importe_iva_revision = art_re.importe_iva_revision;
									art.precio_costo = art_re.precio_costo;
									art.total_captura = art_re.total_captura;
									art.total_revision = art_re.total_revision;
								}
							}
						}
					}
					else
					{
						data.Clear();

						foreach (DTO_Detallado_mayoreo_ventas articulo in result)
						{
							data.Add(articulo);
						}

						dgv_mayoreo.ClearSelection();
					}
				}

			}
			catch (Exception ex)
			{
				Log_error.log(ex);
			}
		}

		private void existen_cambios()
		{
			DAO_Ventas_mayoreo tmp_dao = new DAO_Ventas_mayoreo();
			var tmp_dto_mayoreo = tmp_dao.get_venta_mayoreo_data(mayoreo_venta_id);

			if (tmp_dto_mayoreo.fecha_creado == dto_mayoreo.fecha_creado)
			{
				if (tmp_dto_mayoreo.fecha_terminado == dto_mayoreo.fecha_terminado)
				{
					if (tmp_dto_mayoreo.fecha_inicio_verifiacion == dto_mayoreo.fecha_inicio_verifiacion)
					{
						if (tmp_dto_mayoreo.fecha_fin_verificacion == dto_mayoreo.fecha_fin_verificacion)
						{
							cambios = false;
						}
						else
						{
							cambios = true;
						}
					}
					else
					{
						cambios = true;
					}
				}
				else
				{
					cambios = true;
				}
			}
			else
			{
				cambios = true;
			}

			var tmp_result_rows = dao_mayoreo.get_detallado_mayoreo_ventas(dto_mayoreo.mayoreo_venta_id);

			if(tmp_result_rows.Count != dgv_mayoreo.Rows.Count)
			{
				cambios = true;
			}

		}

		private void get_informacion_mayoreo_ventas()
		{
			DAO_Ventas_mayoreo tmp_dao_mayoreo = new DAO_Ventas_mayoreo();
			mayoreo_venta_id = tmp_dao_mayoreo.get_venta_mayoreo_fin();

			if (mayoreo_venta_id > 0)
			{
				rellenar_informacion();
			}
			else
			{
				bloqueo(true, "No se encontro ninguna captura");
			}
		}

		private void rellenar_informacion(bool pedir_ultimo_folio = true)
		{
			try
			{

				DAO_Ventas_mayoreo tmp_ventas_mayoreo = new DAO_Ventas_mayoreo();

				if (pedir_ultimo_folio)
				{
					dto_mayoreo = tmp_ventas_mayoreo.get_venta_mayoreo_data(mayoreo_venta_id);
				}

				txt_empleado_captura.Text = dto_mayoreo.nombre_empleado_captura;
				txt_empleado_termina.Text = dto_mayoreo.nombre_empleado_termina;
				txt_empleado_inicia_revicion.Text = dto_mayoreo.nombre_empleado_inicio_verificacion;
				txt_empleado_fin_revision.Text = dto_mayoreo.nombre_empleado_fin_verificacion;

                txt_fecha_creado.Text = (dto_mayoreo.fecha_creado != null) ? Misc_helper.fecha(dto_mayoreo.fecha_creado.ToString(), "LEGIBLE") : " - ";
                txt_fecha_terminado.Text = (dto_mayoreo.fecha_terminado != null) ? Misc_helper.fecha(dto_mayoreo.fecha_terminado.ToString(), "LEGIBLE") : " - ";
				txt_folio_busqueda.Text = dto_mayoreo.mayoreo_venta_id.ToString();
                txt_fecha_inicio_revision.Text = (dto_mayoreo.fecha_inicio_verifiacion != null) ? Misc_helper.fecha(dto_mayoreo.fecha_inicio_verifiacion.ToString(), "LEGIBLE") : " - ";
                txt_fecha_fin_revision.Text = (dto_mayoreo.fecha_fin_verificacion != null) ? Misc_helper.fecha(dto_mayoreo.fecha_fin_verificacion.ToString(), "LEGIBLE") : " - ";


				txt_nombre_cliente.Text = dto_mayoreo.nombre_cliente;

				var result= tmp_ventas_mayoreo.get_detallado_mayoreo_ventas(mayoreo_venta_id);

				data.Clear();

				foreach(DTO_Detallado_mayoreo_ventas articulo in result)
				{
					data.Add(articulo);
				}

				dgv_mayoreo.ClearSelection();

				colorear_cantidades();

				txt_amecop.Focus();
				validar_mayoreo_venta();
				colorear_fecha();
			}
			catch(Exception)
			{
				MessageBox.Show(this,"Ocurrio un algun problema durante la carga del modulo, notifique a su administrador","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		private void colorear_cantidades()
		{
			foreach(DataGridViewRow row in dgv_mayoreo.Rows)
			{
				long cantidad_capturada = Convert.ToInt64(row.Cells["c_cantidad"].Value);
				long cantidad_revision = Convert.ToInt64(row.Cells["c_cantidad_revision"].Value);

				if(cantidad_capturada > 0 && cantidad_revision == 0)
				{
					row.DefaultCellStyle.BackColor = Color.FromArgb(255, 218, 218);
					//rojo
				}
				else if (cantidad_capturada > 0 && cantidad_revision == cantidad_capturada)
				{
					//verde
					row.DefaultCellStyle.BackColor = Color.FromArgb(210, 246, 206);
				}
				else if (cantidad_capturada == 0 && cantidad_revision > 0)
				{
					//amarillo
					row.DefaultCellStyle.BackColor = Color.FromArgb(251, 249, 203);
				}
				else if (cantidad_capturada > 0 && cantidad_revision > 0 && cantidad_capturada != cantidad_revision)
				{
					//morado
					row.DefaultCellStyle.BackColor = Color.FromArgb(239, 191, 242);
				}
			}
		}

		private void colorear_fecha()
		{
			txt_empleado_captura.BackColor = (dto_mayoreo.fecha_creado.Equals(null)) ? Color.FromArgb(210, 246, 206) : Color.FromArgb(255, 218, 218);
			txt_fecha_creado.BackColor = (dto_mayoreo.fecha_creado.Equals(null)) ? Color.FromArgb(210, 246, 206) : Color.FromArgb(255, 218, 218);

			txt_empleado_termina.BackColor = (dto_mayoreo.fecha_terminado.Equals(null)) ? Color.FromArgb(210, 246, 206) : Color.FromArgb(255, 218, 218);
			txt_fecha_terminado.BackColor = (dto_mayoreo.fecha_terminado.Equals(null)) ? Color.FromArgb(210, 246, 206) : Color.FromArgb(255, 218, 218);

			txt_fecha_inicio_revision.BackColor = (dto_mayoreo.fecha_inicio_verifiacion.Equals(null)) ? Color.FromArgb(210, 246, 206) : Color.FromArgb(255, 218, 218);
			txt_empleado_inicia_revicion.BackColor = (dto_mayoreo.fecha_inicio_verifiacion.Equals(null)) ? Color.FromArgb(210, 246, 206) : Color.FromArgb(255, 218, 218);

			txt_fecha_fin_revision.BackColor = (dto_mayoreo.fecha_fin_verificacion.Equals(null)) ? Color.FromArgb(210, 246, 206) : Color.FromArgb(255, 218, 218);
			txt_empleado_fin_revision.BackColor = (dto_mayoreo.fecha_fin_verificacion.Equals(null)) ? Color.FromArgb(210, 246, 206) : Color.FromArgb(255, 218, 218);

			if (dto_mayoreo.fecha_terminado != null)
			{
				if (dto_mayoreo.fecha_inicio_verifiacion != null)
				{
					if (dto_mayoreo.fecha_fin_verificacion != null)
					{
						txt_estado.Text = "TERMINADO";
						txt_estado.BackColor = Color.Red;
					}
					else
					{
						txt_estado.Text = "EN REVISION";
						txt_estado.BackColor = Color.Green;
					}
				}
				else
				{
					txt_estado.Text = "REVISION PENDIENTE";
					txt_estado.BackColor = Color.Red;

					if (dgv_mayoreo.Rows.Count == 0)
					{
						txt_estado.Text = "TERMINADO";
						bloqueo(true, "Captura de mayoreo venta terminada vacia");
					}
					else
					{
						bloqueo();
					}
				}
			}
			else
			{
				txt_estado.Text = "CAPTURANDO";
				txt_estado.BackColor = Color.Green;
			}
		}

		private void validar_mayoreo_venta()
		{
			terminal_id = Misc_helper.get_terminal_id();

			if (terminal_id != dto_mayoreo.terminal_id_revision)
			{
				DAO_Terminales dao_terminales = new DAO_Terminales();
				bloqueo(true, (dto_mayoreo.terminal_id_revision != null) ? "La revision de mayoreo venta esta siendo usada por la terminal " + dao_terminales.get_terminal_nombre((int)dto_mayoreo.terminal_id_revision) : "Revision de mayoreo venta sin terminal asignada");
			}
			else
			{
				bloqueo(false);
			}
		}

		private void bloqueo(bool bloquear = false, string mensaje = "")
		{
			if (bloquear)
			{
				txt_amecop.Enabled = false;
				lbl_mensaje_bloqueo.Text = mensaje;
				lbl_mensaje_bloqueo.Parent = dgv_mayoreo;
				lbl_mensaje_bloqueo.Visible = true;

				txt_folio_busqueda.Focus();
			}
			else
			{
				if (dto_mayoreo.fecha_terminado != null)
				{
					if (dto_mayoreo.fecha_inicio_verifiacion != null)
					{
						if (dto_mayoreo.fecha_fin_verificacion != null)
						{
							txt_amecop.Enabled = false;
							lbl_mensaje_bloqueo.Text = "";
							lbl_mensaje_bloqueo.Visible = false;
							lbl_mensaje_bloqueo.Parent = null;
							txt_folio_busqueda.Focus();
						}
						else
						{
							txt_amecop.Enabled = true;
							lbl_mensaje_bloqueo.Text = "";
							lbl_mensaje_bloqueo.Visible = false;
							lbl_mensaje_bloqueo.Parent = null;
							txt_amecop.Focus();
						}
					}
					else
					{
						txt_amecop.Enabled = false;

						if (dgv_mayoreo.Rows.Count > 0)
						{
							lbl_mensaje_bloqueo.Text = "";
							lbl_mensaje_bloqueo.Visible = false;
							lbl_mensaje_bloqueo.Parent = null;
							txt_folio_busqueda.Focus();
						}
						else
						{
							lbl_mensaje_bloqueo.Text = "Captura de mayoreo venta terminada vacia";
							lbl_mensaje_bloqueo.Visible = true;
							lbl_mensaje_bloqueo.Parent = dgv_mayoreo;
						}		
					}
				}
				else
				{
					txt_amecop.Enabled = false;

					if (dgv_mayoreo.Rows.Count > 0)
					{
						lbl_mensaje_bloqueo.Text = "";
						lbl_mensaje_bloqueo.Visible = false;
						lbl_mensaje_bloqueo.Parent = null;
						txt_folio_busqueda.Focus();
					}
					else
					{
						lbl_mensaje_bloqueo.Text = "Captura terminada vacia";
						lbl_mensaje_bloqueo.Visible = true;
						lbl_mensaje_bloqueo.Parent = dgv_mayoreo;
					}		
				}
			}
		}

		private void Ventas_mayoreo_captura_Shown(object sender, EventArgs e)
		{
			get_informacion_mayoreo_ventas();
		}

		private void txt_amecop_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 27:
					limpiar_informacion();
					break;
				case 40:
					if (dgv_mayoreo.Rows.Count > 0)
					{
						dgv_mayoreo.CurrentCell = dgv_mayoreo.Rows[0].Cells["c_amecop"];
						dgv_mayoreo.Rows[0].Selected = true;
						dgv_mayoreo.Focus();
					}
					break;
				case 13:
					if (txt_amecop.TextLength > 0)
						busqueda_producto();
					break;
				case 114:
					Busqueda_productos busqueda_productos = new Busqueda_productos(true);
					busqueda_productos.FormClosed += new FormClosedEventHandler(busqueda_productos_Close);
					busqueda_productos.ShowDialog();
					txt_cantidad.Focus();
					break;
			}
		}

		public void busqueda_producto()
		{
			DTO_Articulo articulo = dao_articulos.get_articulo(txt_amecop.Text);

			if (articulo.Articulo_id != null)
			{
				articulo_id = articulo.Articulo_id;
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
			txt_producto.Text = articulo.Nombre;
			cbb_caducidad.Enabled = true;

			if (articulo.Caducidades.Rows.Count > 0)
			{
				Dictionary<string, string> caducidades = new Dictionary<string, string>();

				foreach (DataRow row in articulo.Caducidades.Rows)
				{
                    caducidades.Add(Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD"), row["caducidad"].ToString());
				}

				cbb_caducidad.DataSource = new BindingSource(caducidades, null);

				cbb_caducidad.DisplayMember = "Key";
				cbb_caducidad.ValueMember = "Value";

				cbb_caducidad.DroppedDown = true;
				cbb_caducidad.Focus();
			}
			else
			{
				MessageBox.Show(this,"Producto sin existencias","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				limpiar_informacion();
			}
		}

		public void busqueda_productos_Close(object sender, FormClosedEventArgs e)
		{
			if (Busqueda_productos.articulo_articulo_id != null)
			{
				articulo_id = Busqueda_productos.articulo_articulo_id;
				txt_amecop.Text = Busqueda_productos.articulo_amecop;
				txt_producto.Text = Busqueda_productos.articulo_producto;

				cbb_caducidad.DataSource = new BindingSource(new Dictionary<string, string>(){
					{Busqueda_productos.caducidad_item.Text,Busqueda_productos.caducidad_item.Value.ToString()}
				}, null);

				cbb_caducidad.DisplayMember = "Key";
				cbb_caducidad.ValueMember = "Value";

				cbb_lote.DataSource = new BindingSource(new Dictionary<string, string>(){
					{Busqueda_productos.lote_item.Text,Busqueda_productos.lote_item.Value.ToString()}
				}, null);

				cbb_lote.DisplayMember = "Key";
				cbb_lote.ValueMember = "Value";

				cbb_caducidad.Enabled = false;
				cbb_lote.Enabled = false;

				txt_amecop.Enabled = false;

				txt_cantidad.Enabled = true;
				txt_cantidad.Text = "1";
				txt_cantidad.SelectAll();
				txt_cantidad.Focus();
			}
		}

		public void limpiar_informacion()
		{
			articulo_id = null;
			txt_amecop.Enabled = true;

			txt_cantidad.Enabled = false;
			txt_cantidad.Text = "";

			txt_amecop.Text = "";

			cbb_caducidad.DataSource = new BindingSource(new Dictionary<string, string>() { { "", "" } }, null);
			cbb_caducidad.DisplayMember = "Key";
			cbb_caducidad.ValueMember = "Value";

			cbb_lote.DataSource = new BindingSource(new Dictionary<string, string>() { { "", "" } }, null);
			cbb_lote.DisplayMember = "Key";
			cbb_lote.ValueMember = "Value";

			cbb_caducidad.Enabled = false;
			cbb_lote.Enabled = false;

			txt_producto.Text = "";

			txt_amecop.Focus();
		}

		private void cbb_caducidad_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt64(e.KeyCode);

			switch (keycode)
			{
				case 13:
					busqueda_lotes();
					break;
				case 27:
					txt_producto.Text = "";
					txt_amecop.Enabled = true;
					txt_amecop.Focus();

					cbb_caducidad.DataSource = new BindingSource(new Dictionary<string, string>(){
						{"",""}
					}, null);
					cbb_caducidad.DisplayMember = "Key";
					cbb_caducidad.ValueMember = "Value";

					cbb_caducidad.Enabled = false;
					break;
			}
		}

		private void cbb_lote_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 13:
					txt_cantidad.Enabled = true;
					txt_cantidad.Text = "1";
					txt_cantidad.SelectAll();
					txt_cantidad.Focus();
					cbb_lote.Enabled = false;
					break;
				case 27:

					cbb_lote.DataSource = new BindingSource(new Dictionary<string, string>(){
						{"",""}
					}, null);

					cbb_lote.DisplayMember = "Key";
					cbb_lote.ValueMember = "Value";

					cbb_caducidad.Enabled = true;
					cbb_caducidad.Focus();
					cbb_caducidad.DroppedDown = true;
					cbb_lote.Enabled = false;
					break;
			}
		}

		public void busqueda_lotes()
		{
			DataTable result_lotes = dao_articulos.get_lotes((int)articulo_id, cbb_caducidad.SelectedValue.ToString());
			cbb_lote.Enabled = true;

			if(result_lotes.Rows.Count > 0)
			{
				Dictionary<string, string> lotes = new Dictionary<string, string>();

				foreach (DataRow row in result_lotes.Rows)
				{
					lotes.Add(row["lote"].ToString(), row["lote"].ToString());
				}

				cbb_lote.DataSource = new BindingSource(lotes, null);
				cbb_lote.DisplayMember = "Key";
				cbb_lote.ValueMember = "Value";

				cbb_lote.DroppedDown = true;
				cbb_lote.Focus();
			}
		}

		private void txt_cantidad_KeyDown(object sender, KeyEventArgs e)
		{
			if (Convert.ToInt32(e.KeyCode) == 27)
			{
				limpiar_informacion();
			}

			if (Convert.ToInt32(e.KeyCode) == 13 && txt_cantidad.TextLength > 0)
			{
				insertar_producto_mayoreo_venta();
			}
		}

		private void insertar_producto_mayoreo_venta()
		{
			try
			{
				long existencia_mayoreo_venta = dao_mayoreo.existencia_captura_mayoreo(dto_mayoreo.mayoreo_venta_id, (long)articulo_id, cbb_caducidad.SelectedValue.ToString(), cbb_lote.SelectedValue.ToString());

				int existencia_vendible = dao_articulos.get_existencia_vendible(txt_amecop.Text, cbb_caducidad.SelectedValue.ToString(), cbb_lote.SelectedValue.ToString());

				existencia_vendible += Convert.ToInt32(existencia_mayoreo_venta);

				if (existencia_vendible >= Convert.ToInt32(txt_cantidad.Text))
				{
					dgv_mayoreo.DataSource = dao_mayoreo.insertar_detallado_revision(dto_mayoreo.mayoreo_venta_id, (long)articulo_id, cbb_caducidad.SelectedValue.ToString(), cbb_lote.SelectedValue.ToString(), Convert.ToInt32(txt_cantidad.Text));
					limpiar_informacion();
					colorear_cantidades();
				}
				else
				{
					MessageBox.Show(this, "La cantidad disponible para vender es de " + existencia_vendible, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				Log_error.log(ex);
			}
		}

		private void txt_amecop_Enter(object sender, EventArgs e)
		{
			dgv_mayoreo.ClearSelection();
		}

		private void dgv_mayoreo_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
                case 67:
                    if (e.Control)
                    {
                        Clipboard.SetText(dgv_mayoreo.SelectedRows[0].Cells["c_amecop"].Value.ToString());
                    }
                break;
				case 27:
					txt_amecop.Focus();
					break;
				case 46:
					long detallado_mayoreo_id = Convert.ToInt64(dgv_mayoreo.SelectedRows[0].Cells["detallado_mayoreo_id"].Value);
					dgv_mayoreo.DataSource = dao_mayoreo.eliminar_detallado_mayoreo_revision(dto_mayoreo.mayoreo_venta_id, detallado_mayoreo_id);
					colorear_cantidades();
					txt_amecop.Focus();
					break;
			}
		}

		private void btn_nuevo_Click(object sender, EventArgs e)
		{
			if (dto_mayoreo.fecha_inicio_verifiacion == null)
			{
				if (dto_mayoreo.fecha_terminado != null)
				{
					Login_form login = new Login_form("iniciar_revision");
					login.ShowDialog();

					if (login.empleado_id != null)
					{
						if (login.empleado_id == dto_mayoreo.empleado_id || login.empleado_id == dto_mayoreo.termina_empleado_id)
						{
							MessageBox.Show(this, "Los empleados que capturan no puede participar en la revisión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						else
						{
							DialogResult dr = MessageBox.Show(this, "Esta a punto de iniciar con la revisión de la venta a mayoreo, ¿Desea continuar?", "Iniciar revision", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

							if (dr == DialogResult.Yes)
							{
								dao_mayoreo.iniciar_verificacion(dto_mayoreo.mayoreo_venta_id, (long)login.empleado_id);
								rellenar_informacion();
							}
						}
					}
				}
			}
		}

		private void btn_inicio_Click(object sender, EventArgs e)
		{
			DAO_Ventas_mayoreo tmp_dao_mayoreo = new DAO_Ventas_mayoreo();
			long id = tmp_dao_mayoreo.get_venta_mayoreo_inicio();

			if (id > 0)
			{
				if(id != dto_mayoreo.mayoreo_venta_id)
				{
					mayoreo_venta_id = id;
					rellenar_informacion();
				}
			}
		}

		private void btn_atras_Click(object sender, EventArgs e)
		{
			DAO_Ventas_mayoreo tmp_dao_mayoreo = new DAO_Ventas_mayoreo();
			long id = tmp_dao_mayoreo.get_venta_mayoreo_atras(mayoreo_venta_id);

			if (id > 0)
			{
				mayoreo_venta_id = id;
				rellenar_informacion();
			}
		}

		private void btn_siguiente_Click(object sender, EventArgs e)
		{
			DAO_Ventas_mayoreo tmp_dao_mayoreo = new DAO_Ventas_mayoreo();
			long id = tmp_dao_mayoreo.get_venta_mayoreo_siguiente(mayoreo_venta_id);

			if (id > 0)
			{
				mayoreo_venta_id = id;
				rellenar_informacion();
			}
		}

		private void btn_fin_Click(object sender, EventArgs e)
		{
			DAO_Ventas_mayoreo tmp_dao_mayoreo = new DAO_Ventas_mayoreo();
			long id = tmp_dao_mayoreo.get_venta_mayoreo_fin();

			if (id > 0)
			{
				if (id != dto_mayoreo.mayoreo_venta_id)
				{
					mayoreo_venta_id = id;
					rellenar_informacion();
				}
			}
		}

		private void btn_cambiar_cliente_Click(object sender, EventArgs e)
		{

		}

		private bool existen_conflictos()
		{
			foreach(DataGridViewRow row in dgv_mayoreo.Rows)
			{
				long cantidad_capturada = Convert.ToInt64(row.Cells["c_cantidad"].Value);
				long cantidad_revision = Convert.ToInt64(row.Cells["c_cantidad_revision"].Value);

				if(cantidad_capturada != cantidad_revision)
				{
					return true;
				}
			}

			return false;
		}

		private void btn_terminar_Click(object sender, EventArgs e)
		{
			if (dto_mayoreo.fecha_terminado != null && dto_mayoreo.fecha_inicio_verifiacion != null && dto_mayoreo.fecha_fin_verificacion == null)
			{
				Login_form login = new Login_form("fin_revision");
				login.ShowDialog();

				if (login.empleado_id != null)
				{
					if(dto_mayoreo.empleado_id != login.empleado_id && dto_mayoreo.termina_empleado_id != login.empleado_id)
					{
						if(!existen_conflictos())
						{
							DialogResult dr = MessageBox.Show(this, "Esta a punto de Finalizar con la revisión de la venta a mayoreo, ¿Desea continuar?", "Finalizar revision", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

							if (dr == DialogResult.Yes)
							{
								var fialas_afectadas = dao_mayoreo.finalizar_verificacion(mayoreo_venta_id, (long)login.empleado_id);

								if (fialas_afectadas > 0)
								{
									Mayoreo_ventas ticket = new Mayoreo_ventas();
									ticket.construccion_ticket(dto_mayoreo.mayoreo_venta_id,false,true);
									ticket.print();
									rellenar_informacion();
									MessageBox.Show(this, "Revision terminada correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
								}
								else
								{
									MessageBox.Show(this, "Ocurrio un error al intentar finalizar la revisión, por favor notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
								}
							}	
						}
						else
						{
							MessageBox.Show(this,"Algunos productos en la revisión tiene conflictos, por favor antes de finalizar verifique que la revision haya sido correcta","Advertencia",MessageBoxButtons.OK,MessageBoxIcon.Warning);
							Revision_productos revision = new Revision_productos(dto_mayoreo.mayoreo_venta_id,(long)login.empleado_id);
							revision.ShowDialog();

							if(revision.terminado)
							{
								Mayoreo_ventas ticket = new Mayoreo_ventas();
								ticket.construccion_ticket(dto_mayoreo.mayoreo_venta_id, false, true);
								ticket.print();
								rellenar_informacion();
							}
						}
					}
					else
					{
						MessageBox.Show(this, "Los empleados que capturan no puede participar en la revisión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		private void desasociar_terminal_Click(object sender, EventArgs e)
		{
			if (dto_mayoreo.fecha_fin_verificacion.Equals(null))
			{
				if (dto_mayoreo.terminal_id_revision != null)
				{
					Login_form login = new Login_form();
					login.ShowDialog();

					if (login.empleado_id != null)
					{
						long filas_afectadas = dao_mayoreo.desasociar_terminal_revision(mayoreo_venta_id);

						if (filas_afectadas > 0)
						{
							MessageBox.Show(this, "Terminal desasociada correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
							rellenar_informacion();
						}
						else
						{
							MessageBox.Show(this, "Ocurrio un error al intentar desasociar la terminal de la revision, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
				else
				{
					MessageBox.Show(this, "Revision sin terminal, imposible desasociar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void asociar_terminal_Click(object sender, EventArgs e)
		{
			if (dto_mayoreo.fecha_fin_verificacion.Equals(null))
			{
				if (dto_mayoreo.terminal_id_revision == null)
				{
					Login_form login = new Login_form();
					login.ShowDialog();

					if (login.empleado_id != null)
					{
						long filas_afectadas = dao_mayoreo.asociar_terminal_revision(mayoreo_venta_id);

						if (filas_afectadas > 0)
						{
							MessageBox.Show(this, "Terminal asociada correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
							rellenar_informacion();
						}
						else
						{
							MessageBox.Show(this, "Ocurrio un error al intentar asociar la terminal de la revisión, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
				else
				{
					MessageBox.Show(this, "Esta revisión ya tiene una terminal asociada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void Ventas_mayoreo_captura_FormClosing(object sender, FormClosingEventArgs e)
		{
			_timer_busqueda.Dispose();
			/*ejecucion_subproceso = false;
			thread.Join();*/
		}

		private void btn_imprimir_Click(object sender, EventArgs e)
		{
			if(mayoreo_venta_id > 0)
			{
				Mayoreo_ventas ticket = new Mayoreo_ventas();
				ticket.construccion_ticket(dto_mayoreo.mayoreo_venta_id, true, true);
				ticket.print();	
			}
		}

		private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			rellenar_informacion();
		}

		private void txt_folio_busqueda_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 13:
					DAO_Ventas_mayoreo dao = new DAO_Ventas_mayoreo();
					var mayoreo_data = dao.get_venta_mayoreo_data(Convert.ToInt64(txt_folio_busqueda.Text.Trim()));

					if (mayoreo_data.mayoreo_venta_id > 0)
					{
						dto_mayoreo = mayoreo_data;
						rellenar_informacion(false);
					}
					break;
			}
		}

		private void txt_folio_busqueda_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
		}

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
	}
}
