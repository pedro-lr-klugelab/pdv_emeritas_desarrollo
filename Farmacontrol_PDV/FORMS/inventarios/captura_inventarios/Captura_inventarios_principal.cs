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

namespace Farmacontrol_PDV.FORMS.inventarios.captura_inventarios
{
	public partial class Captura_inventarios_principal : Form
	{
		DAO_Inventarios dao_inventarios = new DAO_Inventarios();
		DTO_Inventario_folio dto_inventario_folio = new DTO_Inventario_folio();
		DAO_Articulos dao_articulos = new DAO_Articulos();
		DTO_Articulo dto_articulo = new DTO_Articulo();

		public Captura_inventarios_principal()
		{
			InitializeComponent();
		}

		private void Captura_inventarios_principal_Shown(object sender, EventArgs e)
		{
			long inventario_folio_id = dao_inventarios.get_inventario_folio_fin();

			if(inventario_folio_id > 0)
			{
				lbl_mensaje_bloqueo.Text = "";
				lbl_mensaje_bloqueo.Visible = false;
				lbl_mensaje_bloqueo.Parent = null;
				rellenar_informacion_inventario_folio(inventario_folio_id);
			}
			else
			{
				lbl_mensaje_bloqueo.Text = "No se encontraron folios de captura registrados";
				lbl_mensaje_bloqueo.Parent = dgv_inventarios_folios;
				lbl_mensaje_bloqueo.Visible = true;
			}
		}

		public void rellenar_informacion_inventario_folio(long inventario_folio_id)
		{
			dto_inventario_folio = dao_inventarios.get_informacion_inventario_folio(inventario_folio_id);

			txt_inventario_id.Text = ""+ dto_inventario_folio.inventario_id;
			txt_fecha_creado.Text = dto_inventario_folio.fecha_creado;
			txt_folio_busqueda_traspaso.Text = ""+dto_inventario_folio.inventario_folio_id;
			txt_empleado_captura.Text = dto_inventario_folio.nombre_empleado_captura;
			txt_nombre_terminal.Text = dto_inventario_folio.nombre_terminal;
			txt_comentarios.Text = dto_inventario_folio.comentarios;

			dgv_inventarios_folios.DataSource = dao_inventarios.get_detallado_inventarios_folios(dto_inventario_folio.inventario_folio_id);
			dgv_inventarios_folios.ClearSelection();

			load_mes();
			load_anios();

			validar_bloqueo();
		}

		public void validar_bloqueo()
		{
			if(dto_inventario_folio.fecha_terminado.Equals(""))
			{
				txt_amecop.Enabled = true;
				txt_comentarios.Enabled = true;

				if(dao_inventarios.get_aceptando_capturas(dto_inventario_folio.inventario_id))
				{
					lbl_mensaje_bloqueo.Text = "";
					lbl_mensaje_bloqueo.Parent = null;
					lbl_mensaje_bloqueo.Visible = false;

					validar_terminal();
				}
				else
				{
					lbl_mensaje_bloqueo.Text = "LA CAPTURA DE PRODUCTOS FUE DETENIDA";
					lbl_mensaje_bloqueo.Parent = dgv_inventarios_folios;
					lbl_mensaje_bloqueo.Visible = true;

					txt_amecop.Enabled = false;
					txt_comentarios.Enabled = false;
				}
			}
			else
			{
				txt_amecop.Enabled = false;
				txt_comentarios.Enabled = false;
				validar_terminal();
			}
		}

		public void validar_terminal()
		{
			if (dto_inventario_folio.terminal_id == 0)
			{
				lbl_mensaje_bloqueo.Text = "FOLIO SIN TERMINAL";
				lbl_mensaje_bloqueo.Parent = dgv_inventarios_folios;
				lbl_mensaje_bloqueo.Visible = true;

				txt_amecop.Enabled = false;
				txt_comentarios.Enabled = false;
			}
			else
			{
				if (dto_inventario_folio.terminal_id == (int)Misc_helper.get_terminal_id())
				{
					lbl_mensaje_bloqueo.Visible = false;
					lbl_mensaje_bloqueo.Text = "";
					lbl_mensaje_bloqueo.Parent = null;

					txt_amecop.Focus();
				}
				else
				{
					lbl_mensaje_bloqueo.Text = "ESTE FOLIO PERTENECE A " + dto_inventario_folio.nombre_terminal.ToUpper();
					lbl_mensaje_bloqueo.Parent = dgv_inventarios_folios;
					lbl_mensaje_bloqueo.Visible = true;

					txt_amecop.Enabled = false;
					txt_comentarios.Enabled = false;
				}
			}
		}

		private void btn_inicio_Click(object sender, EventArgs e)
		{
			long inventario_folio = dao_inventarios.get_inventario_folio_inicio();

			if(inventario_folio > 0)
			{
				rellenar_informacion_inventario_folio(inventario_folio);
			}
		}

		private void btn_atras_Click(object sender, EventArgs e)
		{
			long inventario_folio = dao_inventarios.get_inventario_folio_atras(dto_inventario_folio.inventario_folio_id);

			if (inventario_folio > 0)
			{
				rellenar_informacion_inventario_folio(inventario_folio);
			}
		}

		private void btn_siguiente_Click(object sender, EventArgs e)
		{
			long inventario_folio = dao_inventarios.get_inventario_folio_siguiente(dto_inventario_folio.inventario_folio_id);

			if (inventario_folio > 0)
			{
				rellenar_informacion_inventario_folio(inventario_folio);
			}
		}

		private void btn_fin_Click(object sender, EventArgs e)
		{
			long inventario_folio = dao_inventarios.get_inventario_folio_fin();

			if (inventario_folio > 0)
			{
				rellenar_informacion_inventario_folio(inventario_folio);
			}
		}

		public void busqueda_productos_Close(object sender, FormClosedEventArgs e)
		{
			if (Busqueda_productos.articulo_articulo_id != null)
			{
				dto_articulo.Articulo_id = Busqueda_productos.articulo_articulo_id;
				txt_amecop.Text = Busqueda_productos.articulo_amecop;
				txt_producto.Text = Busqueda_productos.articulo_producto;

				string caducidad = Busqueda_productos.caducidad_item.Value.ToString();
				string lote = Busqueda_productos.lote_item.Value.ToString();

				if(caducidad.Trim().Equals(""))
				{
					chb_sin_caducidad.Checked = true;
				}
				else
				{
					string[] split_caducidad = caducidad.Split('-');

					string anio = split_caducidad[0].ToString();
					string mes = split_caducidad[1].ToString();
					string dia = split_caducidad[2].ToString();

					cbb_anio.SelectedValue = anio;
					cbb_mes.SelectedValue = mes + "-" + dia;	
				}

				if(lote.Equals(" "))
				{
					chb_lote.Checked = true;
				}
				else
				{
					txt_lote.Text = lote;
					chb_lote.Checked = false;
				}

				txt_cantidad.Enabled = true;
				txt_cantidad.Text = "1";
				txt_cantidad.SelectAll();
				txt_cantidad.Focus();
				txt_amecop.Enabled = false;
			}
		}

		private void txt_amecop_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 114:
					Busqueda_productos busqueda_productos = new Busqueda_productos(true);
					busqueda_productos.FormClosed += new FormClosedEventHandler(busqueda_productos_Close);
					busqueda_productos.ShowDialog();
					txt_cantidad.Focus();
				break;
				case 13:
					if(txt_amecop.Text.Trim().Length > 0)
					{
						dto_articulo = dao_articulos.get_articulo(txt_amecop.Text);

						if(dto_articulo.Articulo_id != null)
						{
							txt_amecop.Enabled = false;
							txt_producto.Text = dto_articulo.Nombre;
							chb_sin_caducidad.Enabled = true;
							cbb_anio.Enabled = true;
							cbb_mes.Enabled = true;
							cbb_mes.Focus();
						}
						else
						{
							MessageBox.Show(this,"Codigo de producto no encontrado","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
						}
					}
				break;
				case 40:
					if (dgv_inventarios_folios.Rows.Count > 0)
					{
						dgv_inventarios_folios.Focus();
						dgv_inventarios_folios.CurrentCell = dgv_inventarios_folios.Rows[0].Cells["c_amecop"];
						dgv_inventarios_folios.Rows[0].Selected = true;
					}
				break;
			}
		}

		public void limpiar_informacion()
		{
			txt_amecop.Enabled = true;
			txt_amecop.Text = "";
			txt_producto.Text = "";
			txt_lote.Text = "";
			txt_cantidad.Text = "";
			txt_lote.Enabled = false;
			txt_cantidad.Enabled = false;

			chb_sin_caducidad.Checked = false;
			cbb_anio.Enabled = false;
			cbb_mes.Enabled = false;

			chb_lote.Checked = false;
			chb_lote.Enabled = false;

			chb_sin_caducidad.Enabled = false;

			load_mes();
			load_anios();

			txt_amecop.Focus();
		}

		public void load_mes()
		{
			Dictionary<string,string> meses = new Dictionary<string,string>();
			meses.Add("ENE","01-01");
			meses.Add("FEB", "02-01");
			meses.Add("MAR", "03-01");
			meses.Add("ABRI", "04-01");
			meses.Add("MAY", "05-01");
			meses.Add("JUN", "06-01");
			meses.Add("JUL", "07-01");
			meses.Add("AGO", "08-01");
			meses.Add("SEP", "09-01");
			meses.Add("OCT", "10-01");
			meses.Add("NOV", "11-01");
			meses.Add("DIC", "12-01");

			BindingSource source = new BindingSource(meses,null);
			cbb_mes.DataSource = source;

			cbb_mes.DisplayMember = "Key";
			cbb_mes.ValueMember = "Value";
		}

		public void load_anios()
		{
			Dictionary<string, string> anios = new Dictionary<string, string>();
			int anio_inicio = (Convert.ToDateTime(Misc_helper.fecha()).Year) - 1;
            int anio_fin = (Convert.ToDateTime(Misc_helper.fecha()).Year) + 5;

			for(int x = anio_inicio; x < anio_fin; x++)
			{
				anios.Add(""+x,""+x);
			}

			BindingSource source = new BindingSource(anios, null);
			cbb_anio.DataSource = source;

			cbb_anio.DisplayMember = "Key";
			cbb_anio.ValueMember = "Value";
		}

		private void dgv_inventarios_folios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if(e.ColumnIndex == 8 )
			{
				if(Convert.ToInt32(e.Value) != 0)
				{
					dgv_inventarios_folios.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 218, 218);
				}
			}
		}

		private void txt_comentarios_Leave(object sender, EventArgs e)
		{
			dao_inventarios.set_comentario_inventario_folio(dto_inventario_folio.inventario_folio_id,txt_comentarios.Text);
		}

		private void cbb_mes_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					cbb_anio.Enabled = true;
					cbb_anio.Focus();
					cbb_anio.DroppedDown = true;
					cbb_mes.Enabled = false;
				break;
				case 27:
					limpiar_informacion();
				break;
			}
		}

		private void cbb_anio_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					txt_lote.Enabled = true;
					txt_lote.Focus();
					cbb_anio.Enabled = false;
					cbb_mes.Enabled = false;

					string caducidad = cbb_anio.SelectedValue + "-" + cbb_mes.SelectedValue;

					var lotes = dao_articulos.get_lotes((int)dto_articulo.Articulo_id,caducidad);

					var source = new AutoCompleteStringCollection();

					foreach(DataRow row in lotes.Rows)
					{
						if(!row["lote"].ToString().Equals(" "))
						{
							source.Add(row["lote"].ToString());	
						}
					}

					 txt_lote.AutoCompleteCustomSource = source;
					 txt_lote.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                     txt_lote.AutoCompleteSource = AutoCompleteSource.CustomSource;

					 chb_lote.Enabled = true;
					 chb_lote.Checked = false;
					 chb_sin_caducidad.Enabled = false;
				break;
				case 27:
					cbb_mes.Enabled = true;
					cbb_mes.Focus();
					cbb_mes.DroppedDown = true;
					cbb_anio.Enabled = false;
				break;
			}
		}

		private void txt_lote_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(txt_lote.Text.Trim().Length > 0)
					{
						txt_cantidad.Enabled = true;
						txt_cantidad.Text = "1";
						txt_cantidad.Focus();
						txt_cantidad.SelectAll();
						txt_lote.Enabled = false;
						chb_lote.Enabled = false;
						chb_lote.Checked = false;
					}
				break;
				case 27:
					if(txt_lote.TextLength > 0)
					{
						txt_lote.Text = "";
					}
					else
					{
						if(chb_sin_caducidad.Checked)
						{
							chb_sin_caducidad.Enabled = true;
							chb_sin_caducidad.Checked = false;
							cbb_mes.Focus();
						}
						else
						{
							chb_sin_caducidad.Enabled = true;
							cbb_mes.Enabled = true;
							cbb_anio.Enabled = true;

							txt_lote.Enabled = false;
							txt_lote.Text = "";
							cbb_mes.Focus();
						}
					}
				break;
			}
		}

		private void txt_cantidad_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					long cantidad = Convert.ToInt64(txt_cantidad.Text.Trim());

					if(cantidad > 0)
					{
						insertar_producto_inventario_folio();
					}
				break;
				case 27:
					chb_lote.Enabled = true;
					chb_lote.Checked = false;
					txt_lote.Enabled = true;
					txt_lote.Focus();

					txt_cantidad.Text = "";
					txt_cantidad.Enabled = false;
				break;
			}
		}

		public void insertar_producto_inventario_folio()
		{
			if(dao_inventarios.get_aceptando_capturas(dto_inventario_folio.inventario_id))
			{	
				var result = dao_inventarios.validar_producto_unico(dto_inventario_folio.inventario_id,dto_inventario_folio.inventario_folio_id, (int)dto_articulo.Articulo_id);

				if(result.status)
				{
					int articulo_id = (int)dto_articulo.Articulo_id;
					string caducidad = (chb_sin_caducidad.Checked)  ? "0000-00-00" : cbb_anio.SelectedValue + "-" + cbb_mes.SelectedValue;

					string lote = (chb_lote.Checked) ? " " : txt_lote.Text;
					int cantidad = Convert.ToInt32(txt_cantidad.Text);

					dgv_inventarios_folios.DataSource = dao_inventarios.insertar_detallado_inventario_folio(dto_inventario_folio.inventario_folio_id, articulo_id, caducidad, lote, cantidad);
					dgv_inventarios_folios.ClearSelection();
					limpiar_informacion();
					validar_bloqueo();
				}
				else
				{
					MessageBox.Show(this,result.informacion,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					limpiar_informacion();
					validar_bloqueo();
				}
			}
			else
			{
				limpiar_informacion();
				validar_bloqueo();
			}
		}

		private void chb_lote_CheckedChanged(object sender, EventArgs e)
		{
			if(chb_lote.Checked)
			{
				txt_lote.Text = "";
				txt_lote.Enabled = false;
				txt_cantidad.Text = "1";
				txt_cantidad.Enabled = true;
				txt_cantidad.Focus();
				txt_cantidad.SelectAll();
			}
			else
			{
				txt_cantidad.Text = "";
				txt_cantidad.Enabled = false;
				txt_lote.Text = "";
				txt_lote.Enabled = true;
				txt_lote.Focus();
			}
		}

		private void dgv_inventarios_folios_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
                case 67:
                    if (e.Control)
                    {
                        Clipboard.SetText(dgv_inventarios_folios.SelectedRows[0].Cells["c_amecop"].Value.ToString());
                    }
                break;
				case 27:
					txt_amecop.Focus();
					dgv_inventarios_folios.ClearSelection();
				break;
				case 46:
					if(dao_inventarios.get_aceptando_capturas(dto_inventario_folio.inventario_id))
					{
						var inventario_data = dao_inventarios.get_informacion_inventario(dto_inventario_folio.inventario_id);

						if(inventario_data.fecha_fin.Equals(null))
						{
							Login_form login = new Login_form("eliminar_detallado_inventario_folio");
							login.ShowDialog();

							if (login.empleado_id != null)
							{
								long detallado_inventario_folio_id = Convert.ToInt32(dgv_inventarios_folios.SelectedRows[0].Cells["c_detallado_inventario_folio_id"].Value);
								dgv_inventarios_folios.DataSource = dao_inventarios.elimar_detallado_inventario_folio(dto_inventario_folio.inventario_folio_id, detallado_inventario_folio_id);
								dgv_inventarios_folios.ClearSelection();
								txt_amecop.Focus();

								MessageBox.Show(this, "Producto eliminado correctamente", "Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
						}
					}
				break;
			}
		}

		private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			rellenar_informacion_inventario_folio(dto_inventario_folio.inventario_folio_id);
		}

		private void desasociar_terminal_Click(object sender, EventArgs e)
		{
			if(dao_inventarios.get_aceptando_capturas(dto_inventario_folio.inventario_id))
			{
				if (dto_inventario_folio.terminal_id != 0)
				{
					Login_form login = new Login_form();
					login.ShowDialog();

					if (login.empleado_id != null)
					{
						if ((int)login.empleado_id == dto_inventario_folio.empleado_id)
						{
							dao_inventarios.set_terminal_id(dto_inventario_folio.inventario_folio_id, null);
							rellenar_informacion_inventario_folio(dto_inventario_folio.inventario_folio_id);
						}
						else
						{
							MessageBox.Show(this, "Solo el propietario del folio de captura puede desasociarla", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
				else
				{
					MessageBox.Show(this, "Este folio de captura no tiene terminal, imposible desasociar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				rellenar_informacion_inventario_folio(dto_inventario_folio.inventario_folio_id);
			}
		}

		private void asociar_terminal_Click(object sender, EventArgs e)
		{
			if(dao_inventarios.get_aceptando_capturas(dto_inventario_folio.inventario_id))
			{
				if (dto_inventario_folio.terminal_id == 0)
				{
					Login_form login = new Login_form();
					login.ShowDialog();

					if (login.empleado_id != null)
					{
						dao_inventarios.set_empleado_id(dto_inventario_folio.inventario_folio_id, (int)login.empleado_id);
						dao_inventarios.set_terminal_id(dto_inventario_folio.inventario_folio_id, Misc_helper.get_terminal_id());
						rellenar_informacion_inventario_folio(dto_inventario_folio.inventario_folio_id);
					}
				}
				else
				{
					MessageBox.Show(this, "Este folio de captura ya tiene asociada una terminal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				rellenar_informacion_inventario_folio(dto_inventario_folio.inventario_folio_id);
			}
		}

		private void txt_amecop_Enter(object sender, EventArgs e)
		{
			dgv_inventarios_folios.ClearSelection();
		}

		private void actualizarToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			rellenar_informacion_inventario_folio(dto_inventario_folio.inventario_folio_id);
		}

		private void terninarCapturaToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void chb_sin_caducidad_CheckedChanged(object sender, EventArgs e)
		{
			cbb_anio.Enabled = (chb_sin_caducidad.Checked == true) ? false : true;
			cbb_mes.Enabled = (chb_sin_caducidad.Checked == true) ? false : true;
			txt_lote.Enabled = (chb_sin_caducidad.Checked);
			chb_lote.Enabled = true;
			
			if(chb_sin_caducidad.Checked)
			{
				txt_lote.Focus();
			}
		}

		private void Captura_inventarios_principal_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 27:
					if(cbb_mes.Focused == true)
					{
						limpiar_informacion();
					}
				break;
			}
		}

		private void txt_folio_busqueda_traspaso_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					long inventario_folio = Convert.ToInt64(txt_folio_busqueda_traspaso.Text.Trim());

					var data_inventario_folio = dao_inventarios.get_informacion_inventario_folio(inventario_folio);

					if(data_inventario_folio.inventario_folio_id > 0)
					{
						dto_inventario_folio = data_inventario_folio;
						rellenar_informacion_inventario_folio(dto_inventario_folio.inventario_folio_id);
					}
				break;
			}
		}

		private void txt_folio_busqueda_traspaso_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
		}

        private void txt_lote_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
	}
}
