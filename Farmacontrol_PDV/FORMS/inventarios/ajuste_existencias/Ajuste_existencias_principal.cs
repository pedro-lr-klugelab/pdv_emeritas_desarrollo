using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.CLASSES;
using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.FORMS.inventarios.ajuste_existencias
{
	public partial class Ajuste_existencias_principal : Form
	{
		DTO_Ajustes_existencias dto_ajustes_existencias = new DTO_Ajustes_existencias();
		DAO_Ajustes_existencias dao_ajustes_existencias = new DAO_Ajustes_existencias();
		DAO_Articulos dao_articulos = new DAO_Articulos();
		DTO_Articulo articulo_registro = new DTO_Articulo();

		public Ajuste_existencias_principal()
		{
			InitializeComponent();
		}

		private void Ajuste_existencias_principal_Load(object sender, EventArgs e)
		{
			var ajuste_existencia_id = dao_ajustes_existencias.get_ajuste_existencia_fin();

			if(ajuste_existencia_id > 0)
			{
				txt_amecop.Enabled = true;
				dto_ajustes_existencias = dao_ajustes_existencias.get_informacion_ajuste_existencia(ajuste_existencia_id);
				rellenar_informacion_ajuste_existencia();
			}
			else
			{
				txt_amecop.Enabled = false;
				lbl_mensaje_bloqueo.Parent = dgv_ajuste_existencias;
				lbl_mensaje_bloqueo.Visible = true;
				lbl_mensaje_bloqueo.Text = "No se encontro ningun Ajuste de Existecia";
			}
		}

		public void valida_bloqueo()
		{
			if(dto_ajustes_existencias.fecha_terminado.Equals(null))
			{
				lbl_mensaje_bloqueo.Visible = false;
				lbl_mensaje_bloqueo.Text = "";
				txt_amecop.Enabled = true;
				txt_amecop.Focus();
			}
			else
			{
				txt_folio_busqueda_ajuste.Focus();
				txt_amecop.Enabled = false;

				if (dgv_ajuste_existencias.Rows.Count == 0)
				{
					dgv_ajuste_existencias.Enabled =  true;
					lbl_mensaje_bloqueo.Visible = true;
					lbl_mensaje_bloqueo.Parent = dgv_ajuste_existencias;
					lbl_mensaje_bloqueo.Text = "Ajuste de Existencias terminado vacio";
				}
				else
				{
					lbl_mensaje_bloqueo.Visible = false;
					lbl_mensaje_bloqueo.Text = "";
				}
			}
		}

		public void rellenar_informacion_ajuste_existencia(long? ajuste_existencia_id = null)
		{
            if(ajuste_existencia_id != null)
            {
                dto_ajustes_existencias = dao_ajustes_existencias.get_informacion_ajuste_existencia((long)ajuste_existencia_id);
            }

			txt_empleado_captura.Text = dto_ajustes_existencias.nombre_empleado_captura;
			txt_empleado_termina.Text = dto_ajustes_existencias.nombre_empleado_termina;
            txt_fecha_creado.Text = (dto_ajustes_existencias.fecha_creado != null) ? Misc_helper.fecha(dto_ajustes_existencias.fecha_creado.ToString(), "LEGIBLE") : " - ";
            txt_fecha_terminado.Text = (dto_ajustes_existencias.fecha_terminado != null) ? Misc_helper.fecha(dto_ajustes_existencias.fecha_terminado.ToString(), "LEGIBLE") : " - ";
			txt_folio_busqueda_ajuste.Text = ""+dto_ajustes_existencias.ajuste_existencia_id;
			txt_estado.Text = (dto_ajustes_existencias.fecha_terminado.Equals(null)) ? "ABIERTO" : "TERMINADO";
			txt_estado.BackColor = (dto_ajustes_existencias.fecha_terminado.Equals(null)) ? Color.Green : Color.Red;
			txt_comentarios.Text = dto_ajustes_existencias.comentarios;

            txt_comentarios.Enabled = false; btn_editar_guardar_comentario.Text = "Editar";

			dgv_ajuste_existencias.DataSource = dao_ajustes_existencias.get_detallado_ajustes_axistencias(dto_ajustes_existencias.ajuste_existencia_id);

			valida_bloqueo();
		}

		private void btn_inicio_Click(object sender, EventArgs e)
		{
			long ajuste_existencia_id =  dao_ajustes_existencias.get_ajuste_existencia_inicio();

			if(ajuste_existencia_id > 0)
			{
				dto_ajustes_existencias = dao_ajustes_existencias.get_informacion_ajuste_existencia(ajuste_existencia_id);
				rellenar_informacion_ajuste_existencia();
			}
		}

		private void btn_atras_Click(object sender, EventArgs e)
		{
			long ajuste_existencia_id = dao_ajustes_existencias.get_ajuste_existencia_atras(dto_ajustes_existencias.ajuste_existencia_id);

			if (ajuste_existencia_id > 0)
			{
				dto_ajustes_existencias = dao_ajustes_existencias.get_informacion_ajuste_existencia(ajuste_existencia_id);
				rellenar_informacion_ajuste_existencia();
			}
		}

		private void btn_siguiente_Click(object sender, EventArgs e)
		{
			long ajuste_existencia_id = dao_ajustes_existencias.get_ajuste_existencia_siguiente(dto_ajustes_existencias.ajuste_existencia_id);

			if (ajuste_existencia_id > 0)
			{
				dto_ajustes_existencias = dao_ajustes_existencias.get_informacion_ajuste_existencia(ajuste_existencia_id);
				rellenar_informacion_ajuste_existencia();
			}
		}

		private void btn_fin_Click(object sender, EventArgs e)
		{
			long ajuste_existencia_id = dao_ajustes_existencias.get_ajuste_existencia_fin();

			if (ajuste_existencia_id > 0)
			{
				dto_ajustes_existencias = dao_ajustes_existencias.get_informacion_ajuste_existencia(ajuste_existencia_id);
				rellenar_informacion_ajuste_existencia();
			}
		}

		private void txt_folio_busqueda_ajuste_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(txt_folio_busqueda_ajuste.Value > 0)
					{
						if(Convert.ToInt32(txt_folio_busqueda_ajuste.Value) > 0)
						{
							var ajuste = dao_ajustes_existencias.get_informacion_ajuste_existencia(Convert.ToInt32(txt_folio_busqueda_ajuste.Value));

							if(ajuste.ajuste_existencia_id > 0)
							{
								dto_ajustes_existencias = ajuste;
								rellenar_informacion_ajuste_existencia();
							}
						}
					}
				break;
			}
		}

		private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
		{
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
		}

		private void txt_folio_busqueda_ajuste_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
			{
				e.Handled = true;
			}
		}

		private void btn_nuevo_Click(object sender, EventArgs e)
		{
			Login_form login = new Login_form("crear_ajustes_existencias");
			login.ShowDialog();

			if(login.empleado_id != null)
			{
				DialogResult dr = MessageBox.Show(this,"Esta a punto de crear un ajuste de existencias, ¿Desea continuar?","Crear Ajuste de Existencias",MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

				if(dr == DialogResult.Yes)
				{
					long ajuste_existencia_id = dao_ajustes_existencias.crear_ajuste_existencia((long)login.empleado_id);

					if (ajuste_existencia_id > 0)
					{
						dto_ajustes_existencias = dao_ajustes_existencias.get_informacion_ajuste_existencia(ajuste_existencia_id);
						rellenar_informacion_ajuste_existencia();
					}
					else
					{
						MessageBox.Show(this, "Ocurrio un error al crear el ajuste de existencia, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}	
				}
			}
		}

		public void busqueda_productos_Close(object sender, FormClosedEventArgs e)
		{
			if (Busqueda_productos.articulo_articulo_id != null)
			{
				txt_amecop.Text = Busqueda_productos.articulo_amecop;
				txt_producto.Text = Busqueda_productos.articulo_producto;

                cbb_caducidad.DataSource = new BindingSource(new Dictionary<string, string>() { { Busqueda_productos.caducidad_item.Text, Busqueda_productos.caducidad_item.Value.ToString() } }, null);

                cbb_caducidad.DisplayMember = "Key";
                cbb_caducidad.ValueMember = "Value";

                cbb_lote.DataSource = new BindingSource(new Dictionary<string, string>() { { Busqueda_productos.lote_item.Text, Busqueda_productos.lote_item.Value.ToString()} }, null);

                cbb_lote.DisplayMember = "Key";
                cbb_lote.ValueMember = "Value";

				//cbb_caducidad.Items.Add(Busqueda_productos.caducidad_item);
				//cbb_lote.Items.Add(Busqueda_productos.lote_item);

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

            Busqueda_productos busqueda_productos = new Busqueda_productos(amecop);
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
                articulo_registro = dao_articulos.get_articulo(txt_amecop.Text);

                if (articulo_registro.Articulo_id != null)
                {
                    rellenar_informacion_producto(articulo_registro);
                }
                else
                {
                    txt_amecop.Text = "";
                    MessageBox.Show(this, "Producto No encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
		}

		public void rellenar_informacion_producto(DTO_Articulo articulo)
		{
			txt_producto.Text = articulo.Nombre;
			cbb_caducidad.Enabled = true;

			Dictionary<string,string> source_caducidades = new Dictionary<string,string>();

			if (articulo.Caducidades.Rows.Count > 0)
			{
				foreach (DataRow row in articulo.Caducidades.Rows)
				{
                    string key = Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD") + " (" + row["existencia"] + ")";
					string value = row["caducidad"].ToString();

					if(!source_caducidades.ContainsKey(key))
					{
						source_caducidades.Add(key,value);
					}
				}

				if (!source_caducidades.ContainsKey("SIN CAD"))
				{
					source_caducidades.Add("SIN CAD", "0000-00-00");
				}

				source_caducidades.Add("OTRO", "OTRO");
			}
			else
			{
				source_caducidades.Add("SIN CAD", "0000-00-00");
				source_caducidades.Add("OTRO", "OTRO");
			}


			cbb_caducidad.DataSource = new BindingSource(source_caducidades, null);
			cbb_caducidad.DisplayMember = "Key";
			cbb_caducidad.ValueMember = "Value";

			cbb_caducidad.Focus();
			cbb_caducidad.DroppedDown = true;
		}

		private void txt_amecop_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 27:
					limpiar_informacion();
				break;
				case 13:
					if(txt_amecop.Text.Trim().Length > 0)
					{
						busqueda_producto();	
					}
				break;
				case 114:
                form_busqueda_producto();
					/*Busqueda_productos busqueda_productos = new Busqueda_productos(true);
					busqueda_productos.FormClosed += new FormClosedEventHandler(busqueda_productos_Close);
					busqueda_productos.ShowDialog();
					txt_cantidad.Focus();*/
				break;
				case 40:
				if (dgv_ajuste_existencias.Rows.Count > 0)
				{
					dgv_ajuste_existencias.Enabled = true;
					dgv_ajuste_existencias.Focus();
					dgv_ajuste_existencias.CurrentCell = dgv_ajuste_existencias.Rows[0].Cells["c_amecop"];
					dgv_ajuste_existencias.Rows[0].Selected = true;
				}
				break;
			}
		}

		public void cargar_mes()
		{
			cbb_mes.DataSource = new BindingSource(new Dictionary<string,string>(){
                {"ENE","01-01"},
                {"FEB","02-01"},
                {"MAR","03-01"},
                {"ABR","04-01"},
                {"MAY","05-01"},
                {"JUN","06-01"},
                {"JUL","07-01"},
                {"AGO","08-01"},
                {"SEP","09-01"},
                {"OCT","10-01"},
                {"NOV","11-01"},
                {"DIC","12-01"}
            },null);

            cbb_mes.DisplayMember = "Key";
            cbb_mes.ValueMember = "Value";
		}

		public void cargar_anios()
		{
			try
			{
                int anio = Convert.ToDateTime(Misc_helper.fecha()).Year;

                cbb_anio.DataSource = new BindingSource(new Dictionary<string, string>() { 
                    {"",""}
                },null);

                Dictionary<int, int> source_anios = new Dictionary<int, int>();


				for (int i = (anio - 1); i < (anio + 10); i++)
				{
					source_anios.Add(i,i);
				}

                cbb_anio.DataSource = new BindingSource(source_anios, null);
                cbb_anio.DisplayMember = "Key";
                cbb_anio.ValueMember = "Value";
			}
			catch (Exception e)
			{
				Log_error.log(e);
			}
		}

		private void cbb_caducidad_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					

					if (cbb_caducidad.SelectedValue.Equals("OTRO"))
					{
						cbb_mes.Enabled = true;
						cbb_mes.DroppedDown = true;
						cbb_mes.Focus();

						cbb_caducidad.Enabled = false;
					}
					else
					{
						busqueda_lotes();
						cbb_caducidad.Enabled = false;
					}
				break;
				case 27:
					txt_producto.Text = "";
					txt_amecop.Focus();
					
					cbb_caducidad.DataSource = new BindingSource(new Dictionary<string,string>(){{"",""}}, null);
					cbb_caducidad.DisplayMember = "Key";
					cbb_caducidad.ValueMember = "Value";

					cbb_caducidad.Enabled = false;
				break;
			}
		}

		private void cbb_lote_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if (cbb_lote.SelectedValue.ToString().Equals("OTRO"))
					{
						txt_otro_lote.Enabled = true;
						txt_otro_lote.Focus();
						cbb_lote.Enabled = false;
					}
					else
					{
						txt_cantidad.Enabled = true;
						txt_cantidad.Value = 1;
						txt_cantidad.Select(0,txt_cantidad.Text.Length);
						txt_cantidad.Focus();

						cbb_lote.Enabled = false;
					}
				break;
				case 27:
					cbb_lote.DataSource = new BindingSource(new Dictionary<string,string>(){{"",""}},null);
					cbb_lote.DisplayMember = "Key";
					cbb_lote.ValueMember = "Value";

					cbb_caducidad.Enabled = true;
					cbb_caducidad.Focus();
					cbb_caducidad.DroppedDown = true;
					cbb_lote.Enabled = false;
				break;
			}
		}

		private void cbb_caducidad_Enter(object sender, EventArgs e)
		{
			cargar_mes();
			cargar_anios();
		}

		private void cbb_mes_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 27:
					cbb_caducidad.Enabled = true;
					cbb_caducidad.DroppedDown = true;
					cbb_caducidad.Focus();

					cbb_mes.Enabled = false;
					break;
				case 13:
					cbb_anio.Enabled = true;
					cbb_anio.Focus();

					cbb_mes.Enabled = false;
					cbb_anio.DroppedDown = true;
					break;
			}
		}

		private void cbb_anio_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 13:
					busqueda_lotes();
					cbb_lote.Enabled = true;
					cbb_lote.Focus();

					cbb_anio.Enabled = false;
					cbb_lote.DroppedDown = true;
					break;
				case 27:
					cbb_mes.Enabled = true;
					cbb_mes.DroppedDown = true;
					cbb_mes.Focus();
					cargar_anios();
					cbb_anio.Enabled = false;
					break;
			}
		}

		private void cbb_mes_DropDown(object sender, EventArgs e)
		{
			if (cbb_mes.SelectedIndex == -1)
			{
				cbb_mes.SelectedIndex = 0;
			}
		}

		private void cbb_anio_DropDown(object sender, EventArgs e)
		{
            /*
			if (cbb_anio.SelectedIndex == -1)
			{
				cbb_anio.SelectedIndex = 0;
			}
             */
		}

		private void txt_otro_lote_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 13:
					if (txt_otro_lote.TextLength > 0)
					{
						txt_cantidad.Enabled = true;
						txt_cantidad.Value = 1;
						txt_cantidad.Select(0,txt_cantidad.Text.Length);
						txt_cantidad.Focus();

						txt_otro_lote.Enabled = false;
					}
					break;
				case 27:
					if (txt_otro_lote.TextLength > 0)
					{
						txt_otro_lote.Text = "";
					}
					else
					{
						cbb_lote.Enabled = true;
						cbb_lote.DroppedDown = true;
						cbb_lote.Focus();
						txt_otro_lote.Enabled = false;
					}
					break;
			}
		}

		public void busqueda_lotes()
		{
            Console.WriteLine(string.Format("{0}-{1}", cbb_anio.SelectedValue.ToString(), cbb_mes.SelectedValue.ToString()));

			DataTable result_lotes = dao_articulos.get_lotes((int)articulo_registro.Articulo_id, (cbb_caducidad.SelectedValue.Equals("OTRO")) 
			?  string.Format("{0}-{1}", cbb_anio.SelectedValue.ToString(), cbb_mes.SelectedValue.ToString())
			: cbb_caducidad.SelectedValue.ToString());
			
			cbb_lote.Enabled = true;

			Dictionary<string,string> source_lotes = new Dictionary<string,string>();

			if (result_lotes.Rows.Count > 0)
			{
				foreach (DataRow row in result_lotes.Rows)
				{
					string lote_key = (row["lote"].ToString().Equals(" ")) ? "SIN LOTE" : row["lote"].ToString();
                    string lote_value = row["lote"].ToString();
					if(!source_lotes.ContainsKey(lote_key))
					{
						source_lotes.Add(lote_key,lote_value);
					}
				}

				if (!source_lotes.ContainsKey("SIN LOTE"))
				{
					source_lotes.Add("SIN LOTE", " ");
				}

				source_lotes.Add("OTRO", "OTRO");
			}
			else
			{
				source_lotes.Add("SIN LOTE", " ");
				source_lotes.Add("OTRO", "OTRO");
			}

			cbb_lote.DataSource = new BindingSource(source_lotes,null);
			cbb_lote.DisplayMember = "Key";
			cbb_lote.ValueMember = "Value";

			cbb_lote.Focus();
			cbb_lote.DroppedDown = true;
		}

		public void limpiar_informacion()
		{
			txt_cantidad.Enabled = false;
			txt_cantidad.Value = 1;

			txt_amecop.Text = "";
			txt_amecop.ReadOnly = false;

			cbb_caducidad.DataSource = new BindingSource(new Dictionary<string,string>(){{"",""}},null);
			cbb_caducidad.DisplayMember = "Key";
			cbb_caducidad.ValueMember = "Value";

			cbb_lote.DataSource = new BindingSource(new Dictionary<string, string>() { { "", "" } }, null);
			cbb_lote.DisplayMember = "Key";
			cbb_lote.ValueMember = "Value";

			txt_producto.Text = "";

			cargar_anios();
			cargar_mes();

			txt_otro_lote.Text = "";
			txt_otro_lote.Enabled = false;

			txt_amecop.Focus();
		}

		public void agregar_producto_ajustes_existencias()
		{
			string caducidad	= ( cbb_caducidad.SelectedValue.ToString().Equals("OTRO") ) ? string.Format("{0}-{1}",cbb_anio.SelectedValue.ToString(),cbb_mes.SelectedValue.ToString()) : cbb_caducidad.SelectedValue.ToString();
			string lote			= ( cbb_lote.SelectedValue.ToString().Equals("OTRO") ) ? txt_otro_lote.Text : cbb_lote.SelectedValue.ToString();

			DAO_Articulos dao_articulos = new DAO_Articulos();
			var articulo_data = dao_articulos.get_articulo(txt_amecop.Text);

			DTO_Detallado_ajustes_existencias dto_detallado_ajuste = new DTO_Detallado_ajustes_existencias();
			dto_detallado_ajuste.ajuste_existencia_id = dto_ajustes_existencias.ajuste_existencia_id;
			dto_detallado_ajuste.articulo_id = (int)articulo_data.Articulo_id;
			dto_detallado_ajuste.caducidad = caducidad;
			dto_detallado_ajuste.lote = lote;
			dto_detallado_ajuste.cantidad = Convert.ToInt32(txt_cantidad.Value);

			dgv_ajuste_existencias.DataSource = dao_ajustes_existencias.registrar_detallado_ajuste_existencia(dto_detallado_ajuste);
			limpiar_informacion();
		}

		private void txt_cantidad_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 27:
					limpiar_informacion();
				break;
				case 13:
                if (dto_ajustes_existencias.fecha_terminado.Equals(null))
                {
                    agregar_producto_ajustes_existencias();
                }
                else
                {
                    limpiar_informacion();
                }		    
				break;
			}
		}

		private void txt_amecop_Enter(object sender, EventArgs e)
		{
			dgv_ajuste_existencias.ClearSelection();
		}

		private void dgv_ajuste_existencias_Leave(object sender, EventArgs e)
		{
			dgv_ajuste_existencias.Enabled = false;
		}

		private void dgv_ajuste_existencias_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
                case 67:
                    if (e.Control)
                    {
                        Clipboard.SetText(dgv_ajuste_existencias.SelectedRows[0].Cells["c_amecop"].Value.ToString());
                    }
                break;
				case 13:
				break;
				case 27:
					txt_amecop.Focus();
					dgv_ajuste_existencias.ClearSelection();
				break;
				case 46:
					if(dgv_ajuste_existencias.SelectedRows.Count > 0)
					{
						long detallado_ajuste_existencia_id = Convert.ToInt64(dgv_ajuste_existencias.SelectedRows[0].Cells["detallado_ajuste_existencia_id"].Value);
						dgv_ajuste_existencias.DataSource = dao_ajustes_existencias.eliminar_producto_detallado_ajuste_existencia(dto_ajustes_existencias.ajuste_existencia_id, detallado_ajuste_existencia_id);
						txt_amecop.Focus();
					}
				break;
			}
		}

		private void btn_terminar_Click(object sender, EventArgs e)
		{
			if(dto_ajustes_existencias.fecha_terminado.Equals(null))
			{
                //if (dto_ajustes_existencias.terminal_id == Misc_helper.get_terminal_id())
                //{
                    if (dto_ajustes_existencias.comentarios.Length > 0)
                    {
                        Login_form login = new Login_form("terminar_ajustes_existencias");
                        login.ShowDialog();

                        if (login.empleado_id != null)
                        {
                            DialogResult dr = MessageBox.Show(this, "Se afectaran las existencias permanentemente, ¿Desea continuar?", "Terminar Ajuste Existencias", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                            if (dr == DialogResult.Yes)
                            {
                                var result = dao_ajustes_existencias.terminar_ajustes_existencias(dto_ajustes_existencias.ajuste_existencia_id, (long)login.empleado_id);

                                if (result > 0)
                                {
                                    MessageBox.Show(this, "Los cambios fueron afectados correctamente", "Ajuste de Existencias", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Ticket_Ajustes_existencias ticket = new Ticket_Ajustes_existencias();
                                    ticket.construccion_ticket(dto_ajustes_existencias.ajuste_existencia_id);
                                    ticket.print();
                                    dto_ajustes_existencias = dao_ajustes_existencias.get_informacion_ajuste_existencia(dto_ajustes_existencias.ajuste_existencia_id);
                                    rellenar_informacion_ajuste_existencia();
                                }
                                else
                                {
                                    MessageBox.Show(this, "Ocurrio un problema al intentar afectar los cambios correspondientes, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Es obligatorio incluir un comentario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                /*}
                else
                {
                    MessageBox.Show(this, "Este ajuste no pertenece a tu terminal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }*/
			}
		}

		private void txt_folio_busqueda_ajuste_Enter(object sender, EventArgs e)
		{
			dgv_ajuste_existencias.ClearSelection();
		}

		private void btn_imprimir_Click(object sender, EventArgs e)
		{
			Ticket_Ajustes_existencias ticket = new Ticket_Ajustes_existencias();
			ticket.construccion_ticket(dto_ajustes_existencias.ajuste_existencia_id,true);
			ticket.print();
		}

		private void btn_editar_guardar_comentario_Click(object sender, EventArgs e)
		{
			if(dto_ajustes_existencias.fecha_terminado.Equals(null))
			{
				if(btn_editar_guardar_comentario.Text.Equals("Editar"))
				{
					txt_comentarios.Enabled = true;
					txt_comentarios.Focus();
					btn_editar_guardar_comentario.Text = "Guardar";	
				}
				else
				{
                    btn_editar_guardar_comentario.Text = "Editar";
					dao_ajustes_existencias.set_comentario(dto_ajustes_existencias.ajuste_existencia_id,txt_comentarios.Text);
					rellenar_informacion_ajuste_existencia(dto_ajustes_existencias.ajuste_existencia_id);
                    txt_amecop.Focus();
				}
			}
		}

        private void txt_otro_lote_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_comentarios_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_cantidad_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txt_comentarios_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    if (!txt_comentarios.Text.Equals(""))
                    {
                        dao_ajustes_existencias.set_comentario(dto_ajustes_existencias.ajuste_existencia_id, txt_comentarios.Text);
                        rellenar_informacion_ajuste_existencia(dto_ajustes_existencias.ajuste_existencia_id);
                        txt_comentarios.Enabled = false;
                        btn_editar_guardar_comentario.Text = "Editar";
                        txt_amecop.Focus();
                    }
                break;
            }

        }

        private void cbb_caducidad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_comentarios_TextChanged(object sender, EventArgs e)
        {

        }
	}
}
