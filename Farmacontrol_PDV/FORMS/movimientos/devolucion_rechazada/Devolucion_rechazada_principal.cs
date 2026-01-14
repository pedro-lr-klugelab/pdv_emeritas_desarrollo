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
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.CLASSES.PRINT;

namespace Farmacontrol_PDV.FORMS.movimientos.devolucion_rechazada
{
	public partial class Devolucion_rechazada_principal : Form
	{
		private long devolucion_id;
		DAO_Devoluciones dao_devoluciones = new DAO_Devoluciones();
		DTO_Devoluciones dto_devolucion = new DTO_Devoluciones();
		private int cantidad_limite;

		public Devolucion_rechazada_principal()
		{
			InitializeComponent();
			tbp_informacion_devolucion.Parent = null;
			tbp_captura_productos.Parent = null;
		}

		private void rellenar_informacion_devolucion()
		{
			txt_empleado_captura.Text = dto_devolucion.nombre_empleado_captura;
			txt_empleado_termina.Text = dto_devolucion.nombre_empleado_termina;
            txt_fecha_creado.Text = (dto_devolucion.fecha_creado != null) ? Misc_helper.fecha(dto_devolucion.fecha_creado.ToString(), "LEGIBLE") : " - ";
            txt_fecha_terminado.Text = (dto_devolucion.fecha_terminado != null) ? Misc_helper.fecha(dto_devolucion.fecha_terminado.ToString(), "LEGIBLE") : " - ";
			txt_folio_solicitud_devolucion.Text = dto_devolucion.solicitud_devolucion_folio;
            txt_fecha_folio_devolucion.Text = (dto_devolucion.solicitud_devolucion_fecha_formato != null) ? Misc_helper.fecha(dto_devolucion.solicitud_devolucion_fecha_formato.ToString(), "LEGIBLE") : " - ";
			comentarios.Text = dto_devolucion.comentarios;

			DAO_Mayoristas dao_mayoristas = new DAO_Mayoristas();
			var dto_mayorista = dao_mayoristas.get_mayorista(dto_devolucion.mayorista_id);
			txt_mayorista.Text = dto_mayorista.nombre;
			dgv_detallado_devolucion.DataSource = dao_devoluciones.get_detallado_devoluciones(dto_devolucion.devolucion_id);
			dgv_detallado_devolucion.ClearSelection();
		}

		private void tbp_tipo_busqueda_Click(object sender, EventArgs e)
		{

		}

		private void tabPage1_Click(object sender, EventArgs e)
		{

		}

		private void radio_fecha_Click(object sender, EventArgs e)
		{
			dtp_fecha_devolucion.Enabled = radio_fecha.Checked;
			txt_folio_devolucion.Enabled = radio_id_devolucion.Checked;

			if(radio_id_devolucion.Checked)
			{
				txt_folio_devolucion.Focus();
			}

			txt_folio_devolucion.Text = "";
		}

		private void radio_id_devolucion_Click(object sender, EventArgs e)
		{
			dtp_fecha_devolucion.Enabled = radio_fecha.Checked;
			txt_folio_devolucion.Enabled = radio_id_devolucion.Checked;

			if (radio_id_devolucion.Checked)
			{
				txt_folio_devolucion.Focus();
			}

			txt_folio_devolucion.Text = "";
		}

		private void Devolucion_rechazada_principal_Shown(object sender, EventArgs e)
		{
			dtp_fecha_devolucion.Enabled = radio_fecha.Checked;
			txt_folio_devolucion.Enabled = radio_id_devolucion.Checked;

			if (radio_id_devolucion.Checked)
			{
				txt_folio_devolucion.Focus();
			}

			txt_folio_devolucion.Text = "";
		}

		private void buscar()
		{
			if (radio_id_devolucion.Checked)
			{
				//Busqueda por id de devolucion
				var result = dao_devoluciones.busqueda_devolucion(txt_folio_devolucion.Text,true);

				if (result.Rows.Count > 0)
				{
					dgv_busqueda_devoluciones.DataSource = result;
					dgv_busqueda_devoluciones.ClearSelection();
				}
				else
				{
					MessageBox.Show(this, "Id de solicitud de devolución no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				//Busqueda por fecha
				var result = dao_devoluciones.busqueda_devolucion(dtp_fecha_devolucion.Value.ToString("yyyy-MM-dd"));

				if (result.Rows.Count > 0)
				{
					dgv_busqueda_devoluciones.DataSource = result;
					dgv_busqueda_devoluciones.ClearSelection();
				}
				else
				{
					MessageBox.Show(this, "No se encontro ninguna devolución", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btn_buscar_Click(object sender, EventArgs e)
		{
			buscar();	
		}

		private void tab1_boton_siguiente_Click(object sender, EventArgs e)
		{
			if(dgv_busqueda_devoluciones.SelectedRows.Count > 0)
			{
				string fecha_auditada = dgv_busqueda_devoluciones.SelectedRows[0].Cells["c_fecha_auditada"].Value.ToString();

				if(!fecha_auditada.Equals(""))
				{
					long devolucion_padre_id = Convert.ToInt64(dgv_busqueda_devoluciones.SelectedRows[0].Cells["c_devolucion_padre_id"].Value);

					if(devolucion_padre_id == 0)
					{
						devolucion_id = Convert.ToInt64(dgv_busqueda_devoluciones.SelectedRows[0].Cells["c_devolucion_id"].Value);

						if (devolucion_id > 0)
						{
							dto_devolucion = dao_devoluciones.get_informacion_devoluciones(devolucion_id);
							tbp_tipo_busqueda.Parent = null;
							tbp_informacion_devolucion.Parent = tab_control_principal;
							rellenar_informacion_devolucion();
						}
						else
						{
							MessageBox.Show(this, "Error interno, id no localizado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}	
					}
					else
					{
						MessageBox.Show(this, "Esta devolución ya forma parte de un rechazo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				else
				{
					MessageBox.Show(this, "Devolución sin auditar, solo pueden rechazarse devoluciones ya auditadas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				MessageBox.Show(this,"Para continuar es necesario seleccionar una devolución a rechazar","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		private void tab1_boton_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void tab2_btn_atras_Click(object sender, EventArgs e)
		{
			tbp_informacion_devolucion.Parent = null;
			tbp_tipo_busqueda.Parent = tab_control_principal;
		}

		private void tab2_btn_siguiente_Click(object sender, EventArgs e)
		{
			tbp_informacion_devolucion.Parent = null;
			tbp_captura_productos.Parent = tab_control_principal;
			txt_amecop.Focus();
		}

		private void tab3_btn_atras_Click(object sender, EventArgs e)
		{
			tbp_informacion_devolucion.Parent = tab_control_principal;
			tbp_captura_productos.Parent = null;
		}

		private void txt_folio_devolucion_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					buscar();
				break;
			}
		}

		private void txt_amecop_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 40:
					if(dgv_productos_rechazados.Rows.Count > 0)
					{
						dgv_productos_rechazados.CurrentCell = dgv_productos_rechazados.Rows[0].Cells["c_amecop_rechazados"];
						dgv_productos_rechazados.Rows[0].Selected = true;
						dgv_productos_rechazados.Focus();
					}
				break;
				case 13:
					if(txt_amecop.Text.Trim().Length > 0)
					{
						buscar_producto_devolucion();	
					}
				break;
				case 27:
					txt_amecop.Text = "";
				break;
			}
		}

		private void buscar_producto_devolucion()
		{
			string amecop = txt_amecop.Text;

			bool existe_amecop = false;
			string nombre_producto = "";

			foreach(DataGridViewRow row in dgv_detallado_devolucion.Rows)
			{
				string amecop_devolucion = row.Cells["c_amecop"].Value.ToString();

				if(amecop_devolucion.Equals(amecop))
				{
					existe_amecop = true;
					nombre_producto = row.Cells["c_producto"].Value.ToString();
					buscar_caducidades_devolucion();
					break;
				}
			}

			if(existe_amecop)
			{
				txt_amecop.Enabled = false;
				txt_producto.Text = nombre_producto;
				buscar_caducidades_devolucion();
			}
			else
			{
				MessageBox.Show(this,"Producto no encontrado dentro de la devolución","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				txt_amecop.Focus();
				txt_amecop.SelectAll();
			}
		}

		private void buscar_caducidades_devolucion()
		{
			Dictionary<string,string> lista_caducidades = new Dictionary<string,string>();

			foreach(DataGridViewRow row in dgv_detallado_devolucion.Rows)
			{
				string amecop_devolucion = row.Cells["c_amecop"].Value.ToString().Trim();

				if(amecop_devolucion.Equals(txt_amecop.Text.Trim()))
				{
					string key = row.Cells["c_caducidad"].Value.ToString();
					string value = Misc_helper.CadtoDate(key);

					if(!lista_caducidades.ContainsKey(key))
					{
						lista_caducidades.Add(key,value);
					}
				}
			}

			if(lista_caducidades.Count > 0)
			{
				cbb_caducidad.Enabled = true;
				cbb_caducidad.DataSource =  new BindingSource(lista_caducidades,null);
				cbb_caducidad.DisplayMember = "Key";
				cbb_caducidad.ValueMember = "Value";
				cbb_caducidad.Focus();
				cbb_caducidad.DroppedDown = true;
			}
			else
			{
				MessageBox.Show(this,"No se encontraron caducidades disponibles para este producto","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				limpiar_informacion();
			}
		}

		private void cbb_mes_KeyDown(object sender, KeyEventArgs e)
		{

		}

		private bool validar_existencia_caducidad()
		{
			bool existe =  false;

			foreach(DataGridViewRow row in dgv_detallado_devolucion.Rows)
			{
				if(row.Cells["c_amecop"].Value.ToString().Equals(txt_amecop.Text))
				{
					string caducidad = cbb_lote.SelectedValue + "-" + cbb_caducidad.SelectedValue;

					if(caducidad.Equals(Misc_helper.CadtoDate(row.Cells["c_caducidad"].Value.ToString())))
					{
						existe = true;
					}
				}
			}

			return existe;
		}

		private bool validar_producto_capturado()
		{
			bool existe = false;

			foreach(DataGridViewRow row in dgv_productos_rechazados.Rows)
			{
				if(txt_amecop.Text.Equals(row.Cells["c_amecop_rechazados"].Value.ToString()))
				{
					if(cbb_caducidad.SelectedValue.ToString().Equals(Misc_helper.CadtoDate(row.Cells["c_caducidad_rechazados"].Value.ToString())))
					{
						if(cbb_lote.SelectedValue.ToString().Equals(row.Cells["c_lote_rechazados"].Value.ToString()))
						{
							existe = true;
						}
					}
				}
			}

			return existe;
		}

		private void cbb_anio_KeyDown(object sender, KeyEventArgs e)
		{

		}

		private bool validar_existencia_lote()
		{
			bool existe_lote = false;

			foreach (DataGridViewRow row in dgv_detallado_devolucion.Rows)
			{
				if (row.Cells["c_amecop"].Value.ToString().Equals(txt_amecop.Text))
				{
					string caducidad = cbb_lote.SelectedValue + "-" + cbb_caducidad.SelectedValue;

					if (caducidad.Equals(Misc_helper.CadtoDate(row.Cells["c_caducidad"].Value.ToString())))
					{
						if(row.Cells["c_lote"].Value.ToString().Equals(cbb_lote.Text.Trim()))
						{
							cantidad_limite = Convert.ToInt32(row.Cells["c_cant_dev"].Value);
							existe_lote = true;	
						}
					}
				}
			}

			return existe_lote;
		}

		private void agregar_cantidad()
		{
			string caducidad = cbb_caducidad.SelectedValue.ToString();
			string lote = cbb_lote.SelectedValue.ToString();

			cantidad_limite = 0;

			foreach (DataGridViewRow row in dgv_detallado_devolucion.Rows)
			{
				if (row.Cells["c_amecop"].Value.ToString().Equals(txt_amecop.Text))
				{
					if (caducidad.Equals(Misc_helper.CadtoDate(row.Cells["c_caducidad"].Value.ToString())))
					{
						if (row.Cells["c_lote"].Value.ToString().Equals(lote))
						{
							cantidad_limite = Convert.ToInt32(row.Cells["c_cant_dev"].Value);
						}
					}
				}
			}

			Dictionary<string,string> cantidades = new Dictionary<string,string>();

			for(int x=1; x<= cantidad_limite; x++)
			{
				cantidades.Add(""+x,""+x);
			}

			cbb_cantidad.DataSource = new BindingSource(cantidades,null);
			cbb_cantidad.DisplayMember = "Key";
			cbb_cantidad.ValueMember = "Value";

			cbb_cantidad.Enabled = true;
			cbb_cantidad.Focus();
			cbb_cantidad.DroppedDown = true;
			cbb_lote.Enabled = false;
		}

		private void registrar_producto_rechazado()
		{
			try
			{
				string amecop = txt_amecop.Text;
				string producto = txt_producto.Text;
				string caducidad = Misc_helper.fecha(cbb_caducidad.SelectedValue.ToString(),"CADUCIDAD");
				string lote = cbb_lote.SelectedValue.ToString();
				int cantidad = Convert.ToInt32(cbb_cantidad.SelectedValue);

				if (validar_producto_capturado())
				{
					foreach (DataGridViewRow row in dgv_productos_rechazados.Rows)
					{
						if (txt_amecop.Text.Equals(row.Cells["c_amecop_rechazados"].Value.ToString()))
						{
							if (cbb_caducidad.SelectedValue.ToString().Equals(Misc_helper.CadtoDate(row.Cells["c_caducidad_rechazados"].Value.ToString())))
							{
								if (cbb_lote.SelectedValue.ToString().Equals(row.Cells["c_lote_rechazados"].Value.ToString()))
								{
									row.Cells["c_cantidad_rechazados"].Value = cantidad;
								}
							}
						}
					}
				}
				else
				{
					dgv_productos_rechazados.Rows.Add(amecop, producto, caducidad, lote, cantidad);
				}

				dgv_productos_rechazados.ClearSelection();
				limpiar_informacion();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void limpiar_informacion()
		{
			txt_amecop.Text = "";
			txt_producto.Text = "";

			cbb_caducidad.DataSource = null;
			cbb_lote.DataSource = null;

			cbb_lote.Enabled = false;
			cbb_caducidad.Enabled = false;

			Dictionary<string, string> cantidades = new Dictionary<string, string>();
			cantidades.Add("0","0");

			cbb_cantidad.DataSource = new BindingSource(cantidades,null);
			cbb_cantidad.DisplayMember = "Key";
			cbb_cantidad.ValueMember = "Value";

			cbb_cantidad.Enabled = false;

			txt_amecop.Enabled = true;
			txt_amecop.Focus();
		}

		private void cbb_caducidad_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					buscar_lote_devolucion();
				break;
				case 27: 
					txt_amecop.Enabled = true;
					txt_amecop.Focus();
					cbb_caducidad.DataSource = null;
					cbb_caducidad.Enabled = false;
					txt_producto.Text = "";
				break;
			}
		}

		private void  buscar_lote_devolucion()
		{
			Dictionary<string, string> lista_lotes = new Dictionary<string, string>();

			foreach (DataGridViewRow row in dgv_detallado_devolucion.Rows)
			{
				string amecop_devolucion = row.Cells["c_amecop"].Value.ToString().Trim();

				if (amecop_devolucion.Equals(txt_amecop.Text.Trim()))
				{
					string key = row.Cells["c_caducidad"].Value.ToString();
					string value = Misc_helper.CadtoDate(key);
					string lote = row.Cells["c_lote"].Value.ToString();

					if(value.Equals(cbb_caducidad.SelectedValue.ToString()))
					{
						if(lote.Equals(" "))
						{
							lote = "SIN LOTE";
						}

						if(!lista_lotes.ContainsKey(lote))
						{
							lista_lotes.Add(lote,(lote == "SIN LOTE") ? " " : lote);
						}
					}
				}
			}

			if (lista_lotes.Count > 0)
			{
				cbb_lote.Enabled = true;
				cbb_lote.DataSource = new BindingSource(lista_lotes, null);
				cbb_lote.DisplayMember = "Key";
				cbb_lote.ValueMember = "Value";
				cbb_lote.Focus();
				cbb_lote.DroppedDown = true;
				cbb_caducidad.Enabled = false;
			}
			else
			{
				MessageBox.Show(this, "No se encontraron lotes disponibles para este producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				limpiar_informacion();
			}
		}

		private void cbb_lote_KeyDown_1(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(validar_producto_capturado())
					{
						MessageBox.Show(this, "Esta caducidad y lote ya fue capturada para este producto, la cantidad que seleccione sustituira la capturada anteriormente","Informacion",MessageBoxButtons.OK,MessageBoxIcon.Warning);
					}

					agregar_cantidad();
				break;
				case 27:
					cbb_caducidad.Enabled = true;
					cbb_caducidad.Focus();
					cbb_caducidad.DroppedDown = true;
					cbb_lote.DataSource = null;
					cbb_lote.Enabled = false;
				break;
			}
		}

		private void cbb_cantidad_KeyDown_1(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					registrar_producto_rechazado();
				break;
				case 27:
					cbb_lote.Enabled = true;
					cbb_lote.Focus();
					cbb_lote.DroppedDown = true;
					cbb_cantidad.DataSource = null;
					cbb_cantidad.Enabled = false;
				break;
			}
		}

		private void txt_amecop_Enter(object sender, EventArgs e)
		{
			dgv_productos_rechazados.ClearSelection();
		}

		private void dgv_productos_rechazados_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 27:
					txt_amecop.Focus();
					dgv_productos_rechazados.ClearSelection();
				break;
			}
		}

		private void dgv_productos_rechazados_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
		{
			txt_amecop.Focus();
			dgv_productos_rechazados.ClearSelection();
		}

		private void tab3_btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void txt_amecop_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
		}

		private void tab3_btn_finalizar_Click(object sender, EventArgs e)
		{
			List<Tuple<int,string,string,int>> lista_aceptados = new List<Tuple<int,string,string,int>>();
			List<Tuple<int, string, string, int>> lista_rechazados = new List<Tuple<int, string, string, int>>();

			foreach(DataGridViewRow row_devolucion in dgv_detallado_devolucion.Rows)
			{
				int articulo_id = Convert.ToInt32(row_devolucion.Cells["c_articulo_id"].Value);
				string caducidad = Misc_helper.CadtoDate(row_devolucion.Cells["c_caducidad"].Value.ToString());
				string lote = row_devolucion.Cells["c_lote"].Value.ToString();
				int cantidad_aceptada = Convert.ToInt32(row_devolucion.Cells["c_cantidad"].Value);

				bool rechazado = false;

				foreach(DataGridViewRow row_rechazado in dgv_productos_rechazados.Rows)
				{
					if(row_devolucion.Cells["c_amecop"].Value.ToString().Equals(row_rechazado.Cells["c_amecop_rechazados"].Value.ToString()))
					{
						if(row_devolucion.Cells["c_caducidad"].Value.ToString().Equals(row_rechazado.Cells["c_caducidad_rechazados"].Value.ToString()))
						{
							if(row_devolucion.Cells["c_lote"].Value.ToString().Equals(row_rechazado.Cells["c_lote_rechazados"].Value.ToString()))
							{
								int cantidad_d = Convert.ToInt32(row_devolucion.Cells["c_cantidad"].Value);
								int cantidad_r = Convert.ToInt32(row_rechazado.Cells["c_cantidad_rechazados"].Value);

								if(cantidad_d != cantidad_r)
								{
									rechazado = false;
									cantidad_aceptada = cantidad_d - cantidad_r;
								}
								else
								{
									rechazado = true;
								}

								lista_rechazados.Add(new Tuple<int, string, string, int>(articulo_id, caducidad, lote, cantidad_r));
							}
						}
					}
				}

				if(rechazado == false)
				{
					lista_aceptados.Add(new Tuple<int, string, string, int>(articulo_id, caducidad, lote, cantidad_aceptada));
				}
			}

			DialogResult dr = MessageBox.Show(this,"Si finaliza el proceso ya no podrá realizar ningun cambio, ¿Desea continuar?","Información",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

			if(dr == DialogResult.Yes)
			{
				Login_form login = new Login_form("afectar_rechazo_devolucion");
				login.ShowDialog();

				long? empleado_id = login.empleado_id;


				if (empleado_id != null)
				{
					if (lista_aceptados.Count == 0)
					{
						rechazo_total((long)empleado_id,lista_rechazados);
					}
					else
					{
						rechazo_parcial((long)empleado_id, lista_aceptados, lista_rechazados);
					}
				}	
			}
		}

		private void rechazo_total(long empleado_id, List<Tuple<int, string, string, int>> productos_rechazados)
		{
			dao_devoluciones.afectar_entradas_devolucion(dto_devolucion.devolucion_id);
			dao_devoluciones.set_devolucion_rechazada(dto_devolucion.devolucion_id);

			if(dto_devolucion.entrada_id == 0)
			{
				dao_devoluciones.enviar_devolucion_auditoria(dto_devolucion.devolucion_id,empleado_id,productos_rechazados);
			}
			else
			{
				Rechazo_con_factura rechazo_factura = new Rechazo_con_factura();
				rechazo_factura.ShowDialog();

				if(rechazo_factura.traspaso)
				{
					dao_devoluciones.enviar_devolucion_auditoria(dto_devolucion.devolucion_id, empleado_id,productos_rechazados);
				}
				else if(rechazo_factura.apartado_mercancia)
				{
					dao_devoluciones.enviar_productos_apartado_mercancia(dto_devolucion.devolucion_id,productos_rechazados);
				}
				else
				{
					MessageBox.Show(this,"Se conservaran todos los productos que han sido rechazados");
				}
			}

			MessageBox.Show(this, "Termino proceso correctamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
			reiniciar_devolucion_rechazada();
		}

		private void rechazo_parcial(long empleado_id, List<Tuple<int, string, string, int>> productos_aceptados, List<Tuple<int, string, string, int>> productos_rechazados)
		{
			if (dto_devolucion.entrada_id == 0)
			{
				dao_devoluciones.afectar_entradas_devolucion(dto_devolucion.devolucion_id);
				dao_devoluciones.set_devolucion_rechazada(dto_devolucion.devolucion_id);
				dao_devoluciones.crear_devolucion_hija(empleado_id, dto_devolucion.devolucion_id, productos_aceptados);
				dao_devoluciones.enviar_devolucion_auditoria(dto_devolucion.devolucion_id, empleado_id,productos_rechazados);
			}
			else
			{
				Rechazo_con_factura rechazo_factura = new Rechazo_con_factura();
				rechazo_factura.ShowDialog();

				dao_devoluciones.afectar_entradas_devolucion(dto_devolucion.devolucion_id);
				dao_devoluciones.set_devolucion_rechazada(dto_devolucion.devolucion_id);
				dao_devoluciones.crear_devolucion_hija(empleado_id, dto_devolucion.devolucion_id, productos_aceptados);

				if (rechazo_factura.traspaso)
				{
					dao_devoluciones.enviar_devolucion_auditoria(dto_devolucion.devolucion_id, empleado_id,productos_rechazados);
				}
				else if (rechazo_factura.apartado_mercancia)
				{
					dao_devoluciones.enviar_productos_apartado_mercancia(dto_devolucion.devolucion_id,productos_rechazados);
				}
				else
				{
					MessageBox.Show(this, "Se conservaran todos los productos que han sido rechazados");
				}
			}

			MessageBox.Show(this, "Termino proceso correctamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
			reiniciar_devolucion_rechazada();
		}

		private void reiniciar_devolucion_rechazada()
		{
			tbp_captura_productos.Parent = null;
			tbp_tipo_busqueda.Parent = tab_control_principal;
			txt_folio_devolucion.Text = "";
			dgv_busqueda_devoluciones.DataSource = null;
			txt_folio_devolucion.Focus();
		}
	}
}
