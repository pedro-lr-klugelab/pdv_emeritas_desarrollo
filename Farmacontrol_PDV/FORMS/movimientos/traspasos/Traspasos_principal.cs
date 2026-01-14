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

namespace Farmacontrol_PDV.FORMS.movimientos.traspasos
{
	public partial class Traspasos_principal : Form
	{
		private long traspaso_id;
		private long traspaso_padre_id;
		DAO_Traspasos dao_traspasos = new DAO_Traspasos();
		DAO_Articulos dao_articulos = new DAO_Articulos();
		DTO_Traspaso dto_traspaso = new DTO_Traspaso();
		DTO_Articulo articulo_registro = new DTO_Articulo();
        DAO_Exclusivos dao_exclusivos = new DAO_Exclusivos();
        long sucursal_local = 0;
        long sucursal_destino = 0;

		public Traspasos_principal()
		{
			InitializeComponent();

			lbl_mensaje_bloqueo.Parent = dgv_traspasos;
			lbl_mensaje_bloqueo.BackColor = Color.Transparent;
			traspaso_id = dao_traspasos.get_traspaso_fin();
            sucursal_local = Convert.ToInt64(Config_helper.get_config_local("sucursal_id"));
			validar_traspaso();
		}

		public void validar_traspaso()
		{
			if(traspaso_id > 0)
			{
				rellenar_informacion_traspaso();
			}
			else
			{
				lbl_mensaje_bloqueo.Text = "No se encontro ningun traspaso";
				txt_tipo_transaccion.Text = "";
				txt_estado.Text = "";
				lbl_mensaje_complemento.Visible = false;
				link_complemento.Visible = false;
				btn_capturar_traspaso.Visible = false;
				bloquear_captura(true);
			}
		}

		public void rellenar_informacion_traspaso()
		{
			DAO_Sucursales dao_sucursales = new DAO_Sucursales();
			dto_traspaso = dao_traspasos.get_informacion_traspaso(Convert.ToInt64(traspaso_id));
			var sucursal_data = dao_sucursales.get_sucursal_data(dto_traspaso.sucursal_id);

			lbl_origen_destino.Text = (dto_traspaso.tipo.Equals("ENVIAR")) ? "Destino:" : "Origen:";

			lbl_folio_origen.Visible = (dto_traspaso.tipo.Equals("ENVIAR")) ? false : true;
			txt_remote_id.Visible = (dto_traspaso.tipo.Equals("ENVIAR")) ? false : true;

			txt_remote_id.Text = dto_traspaso.remote_id.ToString();

			txt_empleado_captura.Text = dto_traspaso.nombre_empleado_captura;
			txt_empleado_termina.Text = dto_traspaso.nombre_empleado_termina;
			txt_folio_busqueda_traspaso.Text = ""+dto_traspaso.traspaso_id;

            int sucursal_id = Convert.ToInt32(Config_helper.get_config_local("sucursal_id"));

            if (dto_traspaso.sucursal_id == sucursal_id)
                txt_sucursal_destino.Text = "ENLACE VITAL";
            else 
			    txt_sucursal_destino.Text = sucursal_data.nombre;

            sucursal_destino = sucursal_data.sucursal_id;

			txt_tipo_transaccion.Text = dto_traspaso.tipo;

            txt_fecha_creado.Text = (dto_traspaso.fecha_creado != null) ? Misc_helper.fecha(dto_traspaso.fecha_creado.ToString(),"LEGIBLE") : " - ";//Convert.ToDateTime(dto_traspaso.fecha_creado).ToString("dd/MMM/yyyy h:mm:ss tt").ToUpper().Replace(".", "") : " - ";

            txt_fecha_terminado.Text = (dto_traspaso.fecha_terminado != null) ? Misc_helper.fecha(dto_traspaso.fecha_terminado.ToString(),"LEGIBLE") : " - ";
			txt_comentarios.Text = dto_traspaso.comentarios;

			txt_amecop.Focus();

			dgv_traspasos.DataSource = dao_traspasos.get_detallado_traspaso(traspaso_id);
			dgv_traspasos.ClearSelection();


			if (dto_traspaso.traspado_padre_id == null)
			{
				string[] split_hash = dto_traspaso.hash.Split('$');

				if (split_hash[0].Equals("TV"))
				{
					txt_hash.Visible = true;
					//txt_hash.Text = dto_traspaso.hash;
					txt_hash.Text = dto_traspaso.hash.Replace("-", "");
				}
				else
				{
					txt_hash.Visible = false;
					txt_hash.Text = "";
				}

				lbl_mensaje_complemento.Visible = false;
				link_complemento.Visible = false;

				txt_tipo_traspaso.Text = "NORMAL";
			}
			else
			{
				txt_tipo_traspaso.Text = "COMPLEMENTARIO";
				traspaso_padre_id = Convert.ToInt64((int)dto_traspaso.traspado_padre_id);
				lbl_mensaje_complemento.Visible = true;
				link_complemento.Visible = true;
				link_complemento.Text = "#" + dto_traspaso.traspado_padre_id;

				if (dto_traspaso.fecha_terminado.Equals(null))
				{
					string[] split_hash = dto_traspaso.hash.Split('$');

					if (split_hash[0].Equals("TC"))
					{
						txt_hash.Visible = true;
						//txt_hash.Text = dto_traspaso.hash; QUITAR GUIONES PARA MOSTRAR
						txt_hash.Text = dto_traspaso.hash.Replace("-","");
					}
					else
					{
						txt_hash.Visible = false;
						txt_hash.Text = "";	
					}
				}
				else
				{
					txt_hash.Visible = true;
					txt_hash.Text = dto_traspaso.hash.Replace("-", "");
					//txt_hash.Text = dto_traspaso.hash;
				}
			}

			if(dto_traspaso.es_para_venta > 0)
			{
				txt_tipo_traspaso.Text = "PARA VENTA";
			}

			this.dgv_traspasos.Columns["c_cantidad_origen"].Visible = (dto_traspaso.tipo.Equals("RECIBIR")) ? true : false;

			txt_comentarios.Enabled = false;
			btn_editar_guardar_comentario.Text = "Editar";

			if(dto_traspaso.fecha_terminado != null)
			{
				if(dto_traspaso.fecha_cancelado != null)
				{
					txt_estado.Text = "CANCELADO";
					txt_estado.BackColor = Color.Red;
					txt_estado.ForeColor = Color.White;
				}
				else
				{
                    
                    if (dto_traspaso.tipo == "RECIBIR")
                    {
                        
                        if(dto_traspaso.fecha_recibido != null  )
                        {
                            txt_estado.Text = "TERMINADO";
                            txt_estado.BackColor = Color.White;
                            txt_estado.ForeColor = Color.Black;	
                        }
                        else
                        {
                            txt_estado.Text = "EN TRANSITO";
                            txt_estado.BackColor = Color.White;
                            txt_estado.ForeColor = Color.Black;
                        }

                    }
                    else
                    {
                        if (dto_traspaso.fecha_recibido != null && dto_traspaso.fecha_terminado_destino != null)
                        {
                            txt_estado.Text = "TERMINADO";
                            txt_estado.BackColor = Color.White;
                            txt_estado.ForeColor = Color.Black;
                        }
                        else
                        {
                            if (dto_traspaso.fecha_terminado_destino == null && dto_traspaso.fecha_recibido != null)
                            {
                                txt_estado.Text = "CAPTURANDO";
                                txt_estado.BackColor = Color.Yellow;
                                txt_estado.ForeColor = Color.Black;
                            }
                            else
                            {

                                txt_estado.Text = "EN TRANSITO";
                                txt_estado.BackColor = Color.White;
                                txt_estado.ForeColor = Color.Black;
                            }

                        }

                    }

                   

				}
			}
			else
			{
				txt_estado.Text = "ABIERTO";
				txt_estado.BackColor = Color.Green;
				txt_estado.ForeColor = Color.White;
			}

			validar_mensaje_bloqueo();
		}

		public void bloquear_captura(bool bloquear = true)
		{
			if(bloquear)
			{
				txt_amecop.Enabled = false;
				txt_folio_busqueda_traspaso.Focus();
				btn_editar_guardar_comentario.Enabled = false;
			}
			else
			{
				btn_editar_guardar_comentario.Enabled = true;
				txt_amecop.Enabled = true;
				txt_amecop.Focus();
			}
		}

		public void validar_mensaje_bloqueo()
		{
			string mensaje = "";
			bool visible = false;

			int terminal_id = (int)Misc_helper.get_terminal_id();

			if(terminal_id == dto_traspaso.terminal_id)
			{
				bloquear_captura(false);

				if(dgv_traspasos.Rows.Count > 0)
				{
					mensaje = "";
					visible = false;

					if(dto_traspaso.fecha_terminado != null)
					{
						bloquear_captura();
					}
					else if(dto_traspaso.traspado_padre_id != null)
					{
						bloquear_captura();
					}
				}
				else
				{
					if (dto_traspaso.fecha_terminado != null)
					{
						mensaje = "Traspaso terminado vacio";
						visible = true;
						bloquear_captura();
					}
				}
			}
			else
			{
				if(dto_traspaso.terminal_id != null)
				{
					mensaje = "Este traspaso pertenece a "+Misc_helper.get_nombre_terminal((int)dto_traspaso.terminal_id);
					visible = true;
				}
				else
				{
					if(dto_traspaso.empleado_id != null)
					{
						mensaje = "Traspaso sin terminal";
						visible = true;
					}
					else
					{
						mensaje = "";
						visible = false;
					}
				}

				bloquear_captura();
			}


			if (dto_traspaso.empleado_id == null && dto_traspaso.tipo == "RECIBIR" && dto_traspaso.fecha_terminado.Equals(null))
			{
				string[] hash = dto_traspaso.hash.Split('$');
				btn_capturar_traspaso.Visible = (hash[0].Equals("TC") || hash[0].Equals("TV") ? false : true);
				txt_estado.Text = "PENDIENTE";
				txt_estado.BackColor = Color.Yellow;
				txt_estado.ForeColor = Color.Black;
			}
			else
			{
				btn_capturar_traspaso.Visible = false;
			}

			lbl_mensaje_bloqueo.Text = mensaje;
			lbl_mensaje_bloqueo.Visible = visible;
		}

		private void txt_amecop_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 27:
					limpiar_informacion();
				break;
				case 40:
					if(dgv_traspasos.Rows.Count > 0)
					{
						dgv_traspasos.CurrentCell = dgv_traspasos.Rows[0].Cells["c_amecop"];
						dgv_traspasos.Rows[0].Selected = true;
						dgv_traspasos.Focus();
					}
				break;
				case 13:
					if (txt_amecop.TextLength > 0)
					{
						busqueda_producto();
					}
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

			if(dto_traspaso.tipo.Equals("RECIBIR"))
			{
				articulo.Caducidades = dao_traspasos.get_caducidades(txt_amecop.Text, traspaso_id);
			}

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
			articulo_registro = articulo;
			Dictionary<string, string> dic_caducidades = new Dictionary<string, string>();

			txt_producto.Text = articulo.Nombre;
			cbb_caducidad.Enabled = true;

			if (articulo.Caducidades.Rows.Count > 0)
			{
				cbb_caducidad.DataSource = null;

				foreach (DataRow row in articulo.Caducidades.Rows)
				{
					if(!dic_caducidades.ContainsValue(row["caducidad"].ToString()))
					{
                        dic_caducidades.Add(Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD") + " (" + row["existencia"] + ")", row["caducidad"].ToString());
					}
				}

				if(dto_traspaso.tipo.Equals("RECIBIR"))
				{
					if(!dic_caducidades.ContainsValue("0000-00-00"))
					{
						dic_caducidades.Add("SIN CAD", "0000-00-00");
					}

					if(!dic_caducidades.ContainsValue("OTRO"))
					{
						dic_caducidades.Add("OTRO","OTRO");
					}
				}
			}
			else
			{

				if (dto_traspaso.tipo.Equals("RECIBIR"))
				{
					if (!dic_caducidades.ContainsValue("0000-00-00"))
					{
						dic_caducidades.Add("SIN CAD", "0000-00-00");
					}
				}

				if (!dic_caducidades.ContainsValue("OTRO"))
				{
					dic_caducidades.Add("OTRO", "OTRO");
				}
			}

			cbb_caducidad.DataSource = new BindingSource(dic_caducidades, null);
			cbb_caducidad.DisplayMember = "Key";
			cbb_caducidad.ValueMember = "Value";

			cbb_caducidad.Focus();
			cbb_caducidad.DroppedDown = true;
            txt_amecop.Enabled = false;
		}

		public void busqueda_productos_Close(object sender, FormClosedEventArgs e)
		{
			if (Busqueda_productos.articulo_articulo_id != null)
			{
				articulo_registro.Articulo_id = Busqueda_productos.articulo_articulo_id;

				txt_amecop.Text = Busqueda_productos.articulo_amecop;
				txt_producto.Text = Busqueda_productos.articulo_producto;

				cbb_caducidad.DataSource = new BindingSource(new Dictionary<string,string>(){
					{Busqueda_productos.caducidad_item.Text.ToString(),Busqueda_productos.caducidad_item.Value.ToString()}
				},null);

				cbb_lote.DataSource = new BindingSource(new Dictionary<string, string>(){
					{Busqueda_productos.lote_item.Text.ToString(),Busqueda_productos.lote_item.Value.ToString()}
				}, null);


				cbb_caducidad.DisplayMember = "Key";
				cbb_caducidad.ValueMember = "Value";

				cbb_lote.DisplayMember = "Key";
				cbb_lote.ValueMember = "Value";

				cbb_caducidad.Enabled = false;
				cbb_lote.Enabled = false;

				txt_cantidad.Enabled = true;
				txt_cantidad.Value = 1;
				txt_cantidad.Select(0,txt_cantidad.Text.Length);
				txt_cantidad.Focus();
			}
		}

		private void cbb_caducidad_KeyDown(object sender, KeyEventArgs e)
		{
			if (Convert.ToInt32(e.KeyCode) == 13)
			{
				if(cbb_caducidad.SelectedValue.ToString().Equals("OTRO"))
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
			}
			else if (Convert.ToInt32(e.KeyCode) == 27)
			{             
                cbb_caducidad.Enabled = false;
				cbb_caducidad.DataSource = null;
                txt_producto.Text = "";
                txt_amecop.Enabled = true;
                txt_amecop.Focus();
				
			}
            
		}

		private void cbb_lote_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{

				var keycode = Convert.ToInt32(e.KeyCode);

				switch (keycode)
				{
					case 13:
						if (cbb_lote.Items.Count > 0)
						{
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
                                txt_cantidad.Select(0, txt_cantidad.Text.Length);
								txt_cantidad.Focus();

								cbb_lote.Enabled = false;
							}
						}
						break;
					case 27:
						try
						{
							cbb_caducidad.Enabled = true;
							cbb_caducidad.Focus();
							cbb_caducidad.DroppedDown = true;
                            cbb_lote.Enabled = false;	
							cbb_lote.DataSource = null;
						}
						catch(Exception ex)
						{
							Log_error.log(ex);
						}
					break;
				}
			}
			catch(Exception ex)
			{
				Log_error.log(ex);
			}
		}

		public void busqueda_lotes()
		{
			DataTable result_lotes = dao_articulos.get_lotes((int)articulo_registro.Articulo_id, cbb_caducidad.SelectedValue.ToString());
			cbb_lote.Enabled = true;
			string caducidad = "";

			if(dto_traspaso.tipo.Equals("RECIBIR"))
			{
				if(cbb_lote.Items.Count > 0)
				{
					if (cbb_lote.SelectedValue.ToString().Equals("OTRO"))
					{
						caducidad = string.Format("{0}-{1}", cbb_anio.SelectedValue.ToString(), cbb_mes.SelectedValue.ToString());
					}

					result_lotes = dao_traspasos.get_lotes((int)articulo_registro.Articulo_id, caducidad, traspaso_id);
				}
			}

			Dictionary<string, string> dic_lotes = new Dictionary<string, string>();

			if (result_lotes.Rows.Count > 0)
			{
				cbb_lote.DataSource = null;

				foreach (DataRow row in result_lotes.Rows)
				{
					if(!dic_lotes.ContainsKey(row["lote"].ToString()))
					{
						dic_lotes.Add(row["lote"].ToString(),row["lote"].ToString());
					}
				}

				if(dto_traspaso.tipo.Equals("RECIBIR"))
				{
					if(!dic_lotes.ContainsKey("SIN LOTE"))
					{
						dic_lotes.Add("SIN LOTE"," ");
					}

					if (!dic_lotes.ContainsKey("OTRO"))
					{
						dic_lotes.Add("OTRO", "OTRO");
					}
				}
			}
			else
			{
				if (!dic_lotes.ContainsKey("SIN LOTE"))
				{
					dic_lotes.Add("SIN LOTE", " ");
				}


				if (dto_traspaso.tipo.Equals("RECIBIR"))
				{
					if (!dic_lotes.ContainsKey("OTRO"))
					{
						dic_lotes.Add("OTRO", "OTRO");
					}
				}
			}

			cbb_lote.DataSource = new BindingSource(dic_lotes, null);
			cbb_lote.DisplayMember = "Key";
			cbb_lote.ValueMember = "Value";

			cbb_lote.Focus();
			cbb_lote.DroppedDown = true;
		}

		private void txt_cantidad_KeyDown(object sender, KeyEventArgs e)
		{
			if (Convert.ToInt32(e.KeyCode) == 27)
			{
				limpiar_informacion();
			}

			if (Convert.ToInt32(e.KeyCode) == 13 && txt_cantidad.Value > 0)
			{
				insertar_producto_traspaso();
			}
		}

		public void insertar_producto_traspaso()
		{
            int articulo_id = (int)articulo_registro.Articulo_id;
            bool valido = dao_exclusivos.valida_exclusivo(sucursal_local,articulo_id);

            if (valido)
            {

                bool valido_sucursal_traspasar = dao_exclusivos.valida_traspaso(sucursal_local, sucursal_destino, articulo_id);

                if (valido_sucursal_traspasar)
                {

                    //Log_error.log("caducidad: "+cbb_caducidad.SelectedValue.ToString() + " lote: "+cbb_lote.SelectedValue.ToString());

                    int existencia_vendible = dao_articulos.get_existencia_vendible(txt_amecop.Text, cbb_caducidad.SelectedValue.ToString(), cbb_lote.SelectedValue.ToString());

                    if (dto_traspaso.tipo.Equals("RECIBIR") || existencia_vendible >= Convert.ToInt32(txt_cantidad.Value))
                    {
                        string caducidad = (cbb_caducidad.SelectedValue.ToString().Equals("OTRO")) ? (cbb_anio.SelectedValue.ToString() + "-" + cbb_mes.SelectedValue.ToString()) : cbb_caducidad.SelectedValue.ToString();
                        string lote = (cbb_lote.SelectedValue.ToString().Equals("OTRO") ? txt_otro_lote.Text : cbb_lote.SelectedValue.ToString());

                        DataTable result_insert = dao_traspasos.insertar_detallado(txt_amecop.Text, caducidad, lote, Convert.ToInt32(txt_cantidad.Value), (long)traspaso_id, dto_traspaso.tipo);

                        limpiar_informacion();

                        if (result_insert.Rows.Count > 0)
                        {
                            dgv_traspasos.DataSource = result_insert;
                            dgv_traspasos.ClearSelection();
                        }
                        else
                        {
                            MessageBox.Show(this, "Ocurrio un problema al intentar registrar el producto, notifica a tu administrador!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "La cantidad disponible para vender es de " + existencia_vendible, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {

                    MessageBox.Show(this, "El codigo no puede ser traspasado a otra sucursal no autorizada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                }
            }
            else
            {
                //CODIGO PARTICIPANTE Y SUCURSAL NO PARTICIPANTE
                MessageBox.Show(this, "El codigo no puede ser agregado al traspaso porque este codigo no se puede mover hacia otra sucursal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

		}

		public void limpiar_informacion()
		{
            txt_amecop.Enabled = true;
            txt_amecop.Focus();

			txt_cantidad.Enabled = false;
			txt_cantidad.Value = 1;

			txt_amecop.Text = "";

			cbb_caducidad.DataSource =  null;
			cbb_lote.DataSource =  null;

			txt_producto.Text = "";

			cargar_anios();
			cargar_mes();

			txt_otro_lote.Text = "";
			txt_otro_lote.Enabled = false;
			articulo_registro = new DTO_Articulo();
		}

		private void btn_inicio_Click(object sender, EventArgs e)
		{
			long traspaso_id_local = dao_traspasos.get_traspaso_inicio();
			if(traspaso_id_local > 0)
			{
				traspaso_id = traspaso_id_local;
				rellenar_informacion_traspaso();
			}
		}

		private void btn_fin_Click(object sender, EventArgs e)
		{
			long traspaso_id_local = dao_traspasos.get_traspaso_fin();
			if (traspaso_id_local > 0)
			{
				traspaso_id = traspaso_id_local;
				rellenar_informacion_traspaso();
			}
		}

		private void btn_atras_Click(object sender, EventArgs e)
		{
			long traspaso_id_local = dao_traspasos.get_traspaso_atras(traspaso_id);
			if (traspaso_id_local > 0)
			{
				traspaso_id = traspaso_id_local;
				rellenar_informacion_traspaso();
			}
		}

		private void btn_siguiente_Click(object sender, EventArgs e)
		{
			long traspaso_id_local = dao_traspasos.get_traspaso_siguiente(traspaso_id);
			if (traspaso_id_local > 0)
			{
				traspaso_id = traspaso_id_local;
				rellenar_informacion_traspaso();
			}
		}

		private void link_complemento_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			traspaso_id =  traspaso_padre_id;
			rellenar_informacion_traspaso();
		}

		private void txt_folio_busqueda_traspaso_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(txt_folio_busqueda_traspaso.Value > 0)
					{
						var traspaso = dao_traspasos.get_informacion_traspaso(Convert.ToInt64(txt_folio_busqueda_traspaso.Text));

						if(traspaso.traspaso_id > 0)
						{
							traspaso_id = traspaso.traspaso_id;
							rellenar_informacion_traspaso();
						}
						else
						{
							MessageBox.Show(this,"Folio de traspaso no encontrado","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
							txt_folio_busqueda_traspaso.Text = ""+traspaso_id;
							txt_folio_busqueda_traspaso.Select(0,txt_folio_busqueda_traspaso.Text.Length);
						}
					}
				break;
			}
		}

		private void txt_folio_busqueda_traspaso_Leave(object sender, EventArgs e)
		{
			txt_folio_busqueda_traspaso.Text = "" + traspaso_id;
		}

		private void txt_amecop_Enter(object sender, EventArgs e)
		{
			dgv_traspasos.ClearSelection();
		}

		private void txt_folio_busqueda_traspaso_Enter(object sender, EventArgs e)
		{
			dgv_traspasos.ClearSelection();
		}

		private void btn_nuevo_Click(object sender, EventArgs e)
		{
            construccion_nuevo_traspaso();
		}

        void construccion_nuevo_traspaso()
        {
            Sucursales sucursales = new Sucursales();
            sucursales.ShowDialog();

            if (sucursales.sucursal_id != null)
            {
                try
                {
                    DAO_Apartado_mercancia dao_apartados = new DAO_Apartado_mercancia();

                    bool productos_apartados = dao_apartados.existe_apartado_sucursal((long)sucursales.sucursal_id);

                    if (productos_apartados)
                    {
                        DialogResult dr = MessageBox.Show(this, "Existen productos apartados para esta sucursal, quieres incluirlos en el traspaso", "Traspaso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dr == DialogResult.Yes)
                        {
                            crear_traspaso((int)sucursales.sucursal_id, true);
                        }
                        else
                        {
                            crear_traspaso((int)sucursales.sucursal_id);
                        }
                    }
                    else
                    {
                        crear_traspaso((int)sucursales.sucursal_id);
                    }
                }
                catch (Exception ex)
                {
                    Log_error.log(ex);
                }
            }
        }

		public void crear_traspaso(int sucursal_id, bool incluir_apartado = false)
		{

            Login_form login = new Login_form();
            login.ShowDialog();

            if(login.empleado_id != null)
            {
                DAO_Sucursales dao_sucursales = new DAO_Sucursales();
                var sucursal = dao_sucursales.get_sucursal_data(sucursal_id);


                int sucursal_id_local = Convert.ToInt32(Config_helper.get_config_local("sucursal_id"));
                DialogResult dr;
                if( sucursal_id_local == sucursal_id )
                    dr = MessageBox.Show(this, "Se creara un traspaso a " + "ENLACE VITAL" + ", ¿Desea continuar?", "Crear Traspaso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                else  
                    dr = MessageBox.Show(this, "Se creara un traspaso a " + sucursal.nombre.ToUpper() + ", ¿Desea continuar?", "Crear Traspaso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                if (dr == DialogResult.Yes)
                {
                    long traspaso_id_creado = dao_traspasos.crear_trapaso(Convert.ToInt64(login.empleado_id),sucursal_id, null, incluir_apartado);

                    if (traspaso_id_creado > 0)
                    {
                        traspaso_id = traspaso_id_creado;
                        rellenar_informacion_traspaso();
                        MessageBox.Show(this, "Traspaso generado correctamente con el folio #" + traspaso_id);
                    }
                    else
                    {
                        MessageBox.Show(this, "Ocurrio un error al intentar registrar el traspaso, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
		}

		private void btn_editar_guardar_comentario_Click(object sender, EventArgs e)
		{
			if (dto_traspaso.fecha_terminado.Equals(null))
			{
				if (btn_editar_guardar_comentario.Text.Equals("Editar"))
				{
					txt_comentarios.Enabled = true;
					txt_comentarios.Focus();
					btn_editar_guardar_comentario.Text = "Guardar";
				}
				else
				{
					txt_comentarios.Enabled = false;
					btn_editar_guardar_comentario.Text = "Editar";
					txt_amecop.Focus();
					int comentario_afectado = dao_traspasos.guardar_comentario(traspaso_id,txt_comentarios.Text);
				}
			}
		}

		private void btn_terminar_Click(object sender, EventArgs e)
		{
            finalizar_traspaso();
		}

        void finalizar_traspaso()
        {

            dto_traspaso = dao_traspasos.get_informacion_traspaso(Convert.ToInt64(traspaso_id));

            if (!dto_traspaso.fecha_terminado.Equals(null))
            {
                MessageBox.Show(this, "El traspaso ya habia sido terminado y no puede volver a terminarse", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rellenar_informacion_traspaso();
                return;
            }



            if (dto_traspaso.fecha_terminado.Equals(null) && txt_estado.Text.ToString().Equals("ABIERTO"))
            {
                if (dto_traspaso.tipo.Equals("ENVIAR"))
                {
                    DialogResult dr = MessageBox.Show(this, "¿Estas seguro de querer terminar el traspaso?", "Terminar traspaso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        Login_form login = new Login_form("terminar_traspaso");
                        login.ShowDialog();

                        if (login.empleado_id != null)
                        {
                            if (dto_traspaso.es_para_venta > 0)
                            {
                                int filas_afectadas = dao_traspasos.terminar_traspaso(traspaso_id, 0, (int)login.empleado_id, false, true);

                                if (filas_afectadas > 0)
                                {
                                    MessageBox.Show(this, "Traspaso terminado correctamente", "Terminar traspaso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Traspaso impresion_ticket = new Traspaso();
                                    impresion_ticket.construccion_ticket(traspaso_id);
                                    impresion_ticket.print();
                                    rellenar_informacion_traspaso();
                                }
                                else
                                {
                                    MessageBox.Show(this, "Ocurrio un error al intentar terminar el traspaso, notifica a tu administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                if (dto_traspaso.traspado_padre_id != null)
                                {
                                    int filas_afectadas = dao_traspasos.terminar_traspaso(traspaso_id, 0, (int)login.empleado_id, true);

                                    if (filas_afectadas > 0)
                                    {
                                        MessageBox.Show(this, "Traspaso terminado correctamente", "Terminar traspaso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Traspaso impresion_ticket = new Traspaso();
                                        impresion_ticket.construccion_ticket(traspaso_id);
                                        impresion_ticket.print();
                                        rellenar_informacion_traspaso();
                                    }
                                    else
                                    {
                                        MessageBox.Show(this, "Ocurrio un error al intentar terminar el traspaso, notifica a tu administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    if (dgv_traspasos.Rows.Count > 0)
                                    {
                                        Cambiar_cantidad cantidad = new Cambiar_cantidad(1);
                                        cantidad.Text = "Etiquetado de Bultos";
                                        cantidad.ShowDialog();
                                        long cantidad_bultos = cantidad.nueva_cantidad;

                                        dr = MessageBox.Show(this, string.Format("Se generara el traspaso con {0} bultos, ¿desea continuar?", cantidad_bultos), "Bultos", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                                        if (dr == DialogResult.Yes)
                                        {

                                            int sucursal_id_local = Convert.ToInt32(Config_helper.get_config_local("sucursal_id"));

                                            if (sucursal_id_local == dto_traspaso.sucursal_id)
                                            {

                                                Datos_Enlace_Vital enlacevital = new Datos_Enlace_Vital();
                                                enlacevital.ShowDialog();

                                                if (enlacevital.aceptar)
                                                {

                                                    string tarjeta = enlacevital.tarjeta;
                                                    string folio = enlacevital.ticket;
                                                    string transaccion = enlacevital.transaccion;

                                                    //
                                                    string[] split_folio = folio.Trim().Split('$');
                                                    long venta_id = Convert.ToInt64(split_folio[1]);

                                                    bool validocodigopromocion = dao_traspasos.existe_articulos_venta(traspaso_id,venta_id);
                                                    if (validocodigopromocion)
                                                    { 
                                                    
                                                        //
                                                        string datos_entrega = "Tarjeta " + tarjeta.ToString() + " del Ticket " + folio.ToString() + " con la Transaccion " + transaccion.ToString();

                                                        int comentario_afectado = dao_traspasos.guardar_comentario(traspaso_id, datos_entrega);

                                                        txt_comentarios.Text = datos_entrega.ToString();
                                                        //
                                                        int filas_afectadas = dao_traspasos.terminar_traspaso(traspaso_id, cantidad_bultos, (int)login.empleado_id);

                                                        if (filas_afectadas > 0)
                                                        {

                                                            Traspaso impresion_ticket = new Traspaso();
                                                            impresion_ticket.construccion_ticket(traspaso_id);
                                                            impresion_ticket.print();
                                                            MessageBox.Show(this, "Traspaso terminado correctamente", "Terminar traspaso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            rellenar_informacion_traspaso();
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show(this, "Ocurrio un error al intentar terminar el traspaso, notifica a tu administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        }
                                           
                                                    }
                                                    else
                                                    {

                                                        MessageBox.Show(this, "Error, los productos capturados como bonificacion no pertenecen a una venta valida para canjear", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                                    }
                                                }
                                                else
                                                    MessageBox.Show(this, "Error, para terminar con el registro de Bonificaciones, se deben registrar los datos completos y correctamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                            else
                                            {
                                                int filas_afectadas = dao_traspasos.terminar_traspaso(traspaso_id, cantidad_bultos, (int)login.empleado_id);

                                                if (filas_afectadas > 0)
                                                {

                                                    Traspaso impresion_ticket = new Traspaso();
                                                    impresion_ticket.construccion_ticket(traspaso_id);
                                                    impresion_ticket.print();
                                                    MessageBox.Show(this, "Traspaso terminado correctamente", "Terminar traspaso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    rellenar_informacion_traspaso();
                                                }
                                                else
                                                {
                                                    MessageBox.Show(this, "Ocurrio un error al intentar terminar el traspaso, notifica a tu administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            
                                            }


                                        }
                                    }
                                    else
                                    {
                                        if (dr == DialogResult.Yes)
                                        {
                                            int filas_afectadas = dao_traspasos.terminar_traspaso(traspaso_id, 0, (int)login.empleado_id);

                                            if (filas_afectadas > 0)
                                            {
                                                Traspaso impresion_ticket = new Traspaso();
                                                impresion_ticket.construccion_ticket(traspaso_id);
                                                impresion_ticket.print();
                                                MessageBox.Show(this, "Traspaso terminado correctamente", "Terminar traspaso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                rellenar_informacion_traspaso();
                                            }
                                            else
                                            {
                                                MessageBox.Show(this, "Ocurrio un error al intentar terminar el traspaso, notifica a tu administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    DialogResult dr = MessageBox.Show(this, "¿Estas seguro de querer terminar el traspaso?", "Terminar traspaso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        Login_form login = new Login_form("terminar_traspaso");
                        login.ShowDialog();

                        if (login.empleado_id != null)
                        {
                            bool diferencias = false;

                            foreach (DataGridViewRow row in dgv_traspasos.Rows)
                            {
                                if (Convert.ToInt32(row.Cells["c_cantidad_origen"].Value) != Convert.ToInt32(row.Cells["c_cantidad"].Value))
                                {
                                    diferencias = true;
                                }
                            }

                            if (diferencias)//  CUANDO NO SE RECIBE LO QUE SE MANDO Y SE RECIBE DE MAS O PRODUCTOS QUE NO APARECEN EN EL TRASPASO
                            {
                                dr = MessageBox.Show(this, "Este traspaso tiene errores, se procederá con la resolución de conflictos, ¿Desea continuar?", "Traspaso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                                if (dr == DialogResult.Yes)
                                {
                                    Resolucion_conflictos conciliacion = new Resolucion_conflictos((long)dto_traspaso.traspaso_id);
                                    conciliacion.ShowDialog();

                                    if (conciliacion.conflictos_solucionados)
                                    {
                                       // DTO_Validacion val = dao_traspasos.afectar_traspaso_origen(Convert.ToInt32(dto_traspaso.remote_id), dto_traspaso.sucursal_id, dto_traspaso.traspaso_id);

                                        //if (val.status)
                                         Boolean status = Red_helper.afectar_traspaso_origen_sucursal(Convert.ToInt32(dto_traspaso.remote_id), dto_traspaso.sucursal_id, dto_traspaso.traspaso_id);
                                        
                                        if (status)
                                        {
                                            dao_traspasos.afectar_traspaso_local(dto_traspaso.traspaso_id);

                                            // dao_traspasos.afectar_traspaso_local_conflicto(dto_traspaso.traspaso_id);//AFECTA EL TRASPASO , SOLO DONDE RECIBIERON FISICAMENTE

                                            int filas_afectadas = dao_traspasos.terminar_traspaso(traspaso_id, 0, (int)login.empleado_id);

                                            if (filas_afectadas > 0)
                                            {
                                                Diferencias impresion_diferencias = new Diferencias();
                                                impresion_diferencias.construccion_ticket(traspaso_id);
                                                impresion_diferencias.print();

                                                Traspaso impresion_ticket = new Traspaso();
                                                impresion_ticket.construccion_ticket(traspaso_id);
                                                impresion_ticket.print();

                                                MessageBox.Show(this, "Traspaso terminado correctamente", "Traspaso Terminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }

                                            rellenar_informacion_traspaso();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //DTO_Validacion val = dao_traspasos.afectar_traspaso_origen(Convert.ToInt32(dto_traspaso.remote_id), dto_traspaso.sucursal_id, dto_traspaso.traspaso_id);
                               // DTO_Validacion val = dao_traspasos.afectar_traspaso_origen_sucursal(Convert.ToInt32(dto_traspaso.remote_id), dto_traspaso.sucursal_id, dto_traspaso.traspaso_id);
                               // MessageBox.Show(this, "EL TRASPASO REMOTE_ID : " + Convert.ToInt32(dto_traspaso.remote_id) + "SUCURSAL_ID : " + dto_traspaso.sucursal_id + " TRASPASOS ORIGEN : " + dto_traspaso.traspaso_id, "Traspaso Terminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Boolean status = false;
                                status = Red_helper.afectar_traspaso_origen_sucursal(Convert.ToInt32(dto_traspaso.remote_id), dto_traspaso.sucursal_id, dto_traspaso.traspaso_id);
                               
                                if (status == true)
                                {
                                    dao_traspasos.afectar_traspaso_local(dto_traspaso.traspaso_id);
                                    int filas_afectadas = dao_traspasos.terminar_traspaso(traspaso_id, 0, (int)login.empleado_id);

                                    if (filas_afectadas > 0)
                                    {
                                        MessageBox.Show(this, "Traspaso terminado correctamente!!!", "Traspaso Terminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    rellenar_informacion_traspaso();
                                }
                                else
                                {
                                       MessageBox.Show(this, "El Traspaso no pudo ser terminado", "Traspaso Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                string[] hash = dto_traspaso.hash.Split('$');

                if (hash[0].Equals("TC") && dto_traspaso.fecha_terminado.Equals(null))
                {
                    DialogResult dr = MessageBox.Show(this, "¿Estas seguro de querer terminar el traspaso?", "Terminar traspaso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        Login_form login = new Login_form("terminar_traspaso");
                        login.ShowDialog();

                        if (login.empleado_id != null)
                        {
                            DTO_Validacion val = dao_traspasos.afectar_traspaso_origen(dto_traspaso.hash);

                            if (val.status)
                            {
                                int filas_afectadas = dao_traspasos.terminar_traspaso(traspaso_id, 0, (int)login.empleado_id);

                                if (filas_afectadas > 0)
                                {
                                    dao_traspasos.afectar_traspaso_local(dto_traspaso.traspaso_id);

                                    DTO_Validacion validacion = dao_traspasos.asociar_terminal(traspaso_id, (int)login.empleado_id);

                                    if (validacion.status)
                                    {
                                        /*
                                        Log_error.log("Traspaso_padre_id: " + dto_traspaso.traspado_padre_id+"|");

                                        if(dto_traspaso.traspado_padre_id != null)
                                        {
                                            var traspaso_padre_data = dao_traspasos.get_informacion_traspaso((long)dto_traspaso.traspado_padre_id);

                                            Log_error.log("Traspaso_padre_id: "+traspaso_padre_data.traspaso_id);
                                            Log_error.log("tipo: "+traspaso_padre_data.tipo);

                                            if(traspaso_padre_data.Equals("RECIBIR"))
                                            {
                                                dao_traspasos.igualar_traspaso_padre((long)dto_traspaso.traspado_padre_id);	
                                            }
                                        }
                                         * */
                                        MessageBox.Show(this, "Traspaso terminado correctamente", "Traspaso Terminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        rellenar_informacion_traspaso();
                                    }
                                    else
                                    {
                                        MessageBox.Show(this, validacion.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(this, "Ocurrio un error al intentar terminar el traspaso complementario, notifique a su administrador");
                                }
                            }
                            else
                            {
                                MessageBox.Show(this, val.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }


		private void btn_imprimir_Click(object sender, EventArgs e)
		{
			if (dto_traspaso.fecha_terminado != null)
			{
				if (dto_traspaso.tipo.Equals("ENVIAR"))
				{
					Traspaso impresion_ticket = new Traspaso();
					impresion_ticket.construccion_ticket(traspaso_id, true);
					impresion_ticket.print();
				}
				else
				{
					bool diferencias = false;

					foreach (DataGridViewRow row in dgv_traspasos.Rows)
					{
						if (Convert.ToInt32(row.Cells["c_cantidad_origen"].Value) != Convert.ToInt32(row.Cells["c_cantidad"].Value))
						{
							diferencias = true;
						}
					}

					if (diferencias)
					{
						DialogResult dr = MessageBox.Show(this, "¿Desea imprimir la lista de productos con diferencias?", "Imprimir Traspaso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

						if (dr == DialogResult.Yes)
						{
							Diferencias impresion_diferencias = new Diferencias();
							impresion_diferencias.construccion_ticket(traspaso_id);
							impresion_diferencias.print();
						}

						if (dto_traspaso.fecha_terminado != null)
						{
							Traspaso impresion_ticket = new Traspaso();
							impresion_ticket.construccion_ticket(traspaso_id, true);
							impresion_ticket.print();
						}
					}
					else
					{
						if (dto_traspaso.fecha_terminado != null)
						{
							Traspaso impresion_ticket = new Traspaso();
							impresion_ticket.construccion_ticket(traspaso_id, true);
							impresion_ticket.print();
						}
						else
						{
							MessageBox.Show(this, "Este traspaso no se puede imprimir ya que no esta terminado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}	
			}
		}

		private void cancelar_traspaso_Click(object sender, EventArgs e)
		{
			if(dto_traspaso.tipo.Equals("ENVIAR"))
			{
				if (dto_traspaso.fecha_terminado.Equals(null))
				{
					MessageBox.Show(this, "No puedes cancelar un traspaso que no ha sido terminado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					DTO_Validacion val = dao_traspasos.get_traspaso_recibido(dto_traspaso.traspaso_id, dto_traspaso.sucursal_id);

					if(val.status)
					{
						DialogResult dr = MessageBox.Show(this, "Estas a punto de cancelar este traspaso, ¿Deseas continuar?", "Cancelar traspaso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

						if (dr == DialogResult.Yes)
						{
							Login_form login = new Login_form("cancelar_traspaso");
							login.ShowDialog();

							if (login.empleado_id != null)
							{
								int filas_afectadas = dao_traspasos.cancelar_traspaso(traspaso_id, Convert.ToInt64(login.empleado_id));

								if (filas_afectadas > 0)
								{
									MessageBox.Show(this, "El traspaso fue cancelado correctamente", "Traspaso cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
									rellenar_informacion_traspaso();
								}
								else
								{
									MessageBox.Show(this, "Ocurrio un error al intentar cancelar este traspaso, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
								}
							}
						}
					}
					else
					{
						MessageBox.Show(this,val.informacion,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					}
				}	
			}
		}

		private void desasociar_terminal_Click(object sender, EventArgs e)
		{
			if(traspaso_id > 0)
			{
				if (dto_traspaso.terminal_id != null)
				{
					if (dto_traspaso.terminal_id == Misc_helper.get_terminal_id())
					{
						Login_form login = new Login_form();
						login.ShowDialog();

						if (login.empleado_id == dto_traspaso.empleado_id)
						{
							dao_traspasos.desasociar_terminal((int)dto_traspaso.traspaso_id, (int)login.empleado_id);
							rellenar_informacion_traspaso();
						}
						else
						{
							MessageBox.Show(this, "Solo el empleado que creo el traspaso puede desasociarlo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
					else
					{
						MessageBox.Show(this, "El traspaso solo puede ser desasociado de la terminal donde se creo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}	
			}
		}

		private void asociar_terminal_Click(object sender, EventArgs e)
		{
			if(traspaso_id > 0)
			{
				if (dto_traspaso.terminal_id == null)
				{
					Login_form login = new Login_form();
					login.ShowDialog();
					
                    if(login.empleado_id != null)
                    {
                        dao_traspasos.asociar_terminal(dto_traspaso.traspaso_id, (int)login.empleado_id);
                        rellenar_informacion_traspaso();
                    }
				}
				else
				{
					MessageBox.Show(this, "Este traspaso ya cuenta con un terminal asignada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}	
			}
		}

		private void btn_capturar_traspaso_Click(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show(this,"Esta a punto de habilitar la captura para este traspaso,¿Desea continuar?","Capturar traspaso",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

			if(dr == DialogResult.Yes)
			{
				Login_form login = new Login_form();
				login.ShowDialog();

				if (login.empleado_id != null)
				{
					dao_traspasos.asociar_terminal(traspaso_id, (int)login.empleado_id);
					rellenar_informacion_traspaso();
				}	
			}
		}

		private void dgv_traspasos_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
                case 67:
                    if (e.Control)
                    {
                        Clipboard.SetText(dgv_traspasos.SelectedRows[0].Cells["c_amecop"].Value.ToString());
                    }
                break;
				case 27:
					if(txt_amecop.Enabled)
					{
						txt_amecop.Focus();
					}
					else
					{
						txt_folio_busqueda_traspaso.Focus();
					}
				break;
				case 46:
					if(dgv_traspasos.SelectedRows.Count > 0)
					{
						if (dto_traspaso.fecha_terminado == null)
						{
							long detallado_traspaso_id = Convert.ToInt64(dgv_traspasos.SelectedRows[0].Cells["detallado_traspaso_id"].Value);
							dgv_traspasos.DataSource = dao_traspasos.eliminar_detallado_traspaso(dto_traspaso.traspaso_id, detallado_traspaso_id);
							txt_amecop.Focus();
							dgv_traspasos.ClearSelection();
						}
					}
				break;
			}
		}

		private void Traspasos_principal_Load(object sender, EventArgs e)
		{

		}

		public void cargar_mes()
		{
			Dictionary<string,string> mes = new Dictionary<string,string>();

			mes.Add("ENE", "01-01");
			mes.Add("FEB", "02-01");
			mes.Add("MAR", "03-01");
			mes.Add("ABR", "04-01");
			mes.Add("MAY", "05-01");
			mes.Add("JUN", "06-01");
			mes.Add("JUL", "07-01");
			mes.Add("AGO", "08-01");
			mes.Add("SEP", "09-01");
			mes.Add("OCT", "10-01");
			mes.Add("NOV", "11-01");
			mes.Add("DIC", "12-01");

			cbb_mes.DataSource = new BindingSource(mes,null);
			cbb_mes.DisplayMember = "Key";
			cbb_mes.ValueMember = "Value";
		}

		public void cargar_anios()
		{
			try
			{
				Dictionary<int,int> anios = new Dictionary<int,int>();
				int anio = Convert.ToDateTime(Misc_helper.fecha()).Year;

				anios.Clear();
                //cambio de año para aceptar años posteriores al 2020  
                //original for(int i = (anio - 1); i < (anio + 5); i++)
				for(int i = (anio - 1); i < (anio + 16); i++)
				{
					anios.Add(i,i);
				}

				cbb_anio.DataSource = new BindingSource(anios,null);
				cbb_anio.DisplayMember = "Key";
				cbb_anio.ValueMember = "Value";
			}
			catch(Exception e)
			{
				Log_error.log(e);
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

			switch(keycode)
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

			switch(keycode)
			{
				case 13:
					busqueda_lotes();
					cbb_lote.Enabled =  true;
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
			
		}

		private void cbb_anio_DropDown(object sender, EventArgs e)
		{
			
		}

		private void txt_otro_lote_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(txt_otro_lote.TextLength > 0)
					{
						txt_cantidad.Enabled = true;
						txt_cantidad.Value = 1;
                        txt_cantidad.Select(0, txt_cantidad.Text.Length);
						txt_cantidad.Focus();

						txt_otro_lote.Enabled = false;
					}
				break;
				case 27:
					if(txt_otro_lote.TextLength > 0)
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

		public void validar_traspaso_recibir()
		{
			foreach (DataGridViewRow row in dgv_traspasos.Rows)
			{
				int cantidad_origen = Convert.ToInt32(dgv_traspasos.Rows[row.Index].Cells["c_cantidad_origen"].Value);
				int cantidad = Convert.ToInt32(dgv_traspasos.Rows[row.Index].Cells["c_cantidad"].Value);

				if (cantidad_origen > 0)
				{
					if (cantidad_origen == cantidad)
					{

						row.DefaultCellStyle.BackColor = Color.FromArgb(210, 246, 206);
					}
					else
					{
						row.DefaultCellStyle.BackColor = Color.FromArgb(255, 218, 218);
					}
				}
				else
				{
					row.DefaultCellStyle.BackColor = Color.FromArgb(251, 249, 203);
				}
			}
		}

		private void btn_buscar_Click(object sender, EventArgs e)
		{
			
		}

		private void dgv_traspasos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			if(dto_traspaso.tipo.Equals("RECIBIR"))
			{
				validar_traspaso_recibir();	
			}
		}

		private void enviar_traspaso_complementario_Click(object sender, EventArgs e)
		{
			if(traspaso_id > 0)
			{

				if (dto_traspaso.tipo.Equals("RECIBIR"))
				{
					Traspaso_complementario traspaso_complementario = new Traspaso_complementario(dto_traspaso.traspaso_id);
					traspaso_complementario.ShowDialog();

					long traspaso_id_local = dao_traspasos.get_traspaso_fin();

					if (traspaso_id_local > 0)
					{
						traspaso_id = traspaso_id_local;
						rellenar_informacion_traspaso();
					}
				}
				else
				{
					if (dto_traspaso.remote_id != null)
					{
						Traspaso_complementario traspaso_complementario = new Traspaso_complementario(dto_traspaso.traspaso_id);
						traspaso_complementario.ShowDialog();

						long traspaso_id_local = dao_traspasos.get_traspaso_fin();

						if (traspaso_id_local > 0)
						{
							traspaso_id = traspaso_id_local;
							rellenar_informacion_traspaso();
						}
					}
					else
					{
						MessageBox.Show(this, "No puedes enviar un traspaso complementario si la sucursal_destino no ha capturado el traspaso enviado");
					}
				}	
			}
		}

		private void recibir_trapaso_complementario_Click(object sender, EventArgs e)
		{
			if(traspaso_id > 0)
			{
				Recibir_traspaso_complementario rtc = new Recibir_traspaso_complementario();
				rtc.ShowDialog();

				long traspaso_id_local = dao_traspasos.get_traspaso_fin();

				if (traspaso_id_local > 0)
				{
					traspaso_id = traspaso_id_local;
					rellenar_informacion_traspaso();
				}	
			}
		}

		private void crear_traspaso_venta_Click(object sender, EventArgs e)
		{
			Login_form login = new Login_form();
			login.ShowDialog();

			if(login.empleado_id != null)
			{	
				Sucursales suc = new Sucursales();
				suc.ShowDialog();
				
				if(suc.sucursal_id != null)
				{
					DialogResult dr = MessageBox.Show(this,"¿Esta seguro de querer crear el traspaso para venta?","Traspaso para venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

					if(dr == DialogResult.Yes)
					{
						long traspaso_id_para_venta =  dao_traspasos.crear_traspaso_venta((long)login.empleado_id,(int)suc.sucursal_id);

						if(traspaso_id_para_venta > 0)
						{
							//MessageBox.Show(this,"Traspaso creado correctamente con el folio #"+traspaso_id_para_venta);
							traspaso_id = traspaso_id_para_venta;
							rellenar_informacion_traspaso();
						}
						else
						{
							MessageBox.Show(this,"Ocurrio un error al intentar crear el traspaso para la venta, notifique a su administrador","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
						}
					}
				}
			}
		}

		private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			validar_traspaso();
		}

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Traspasos_principal_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch(keycode)
            {
                case 78:
                    if(e.Control)
                    {
                        construccion_nuevo_traspaso();
                    }
                break;
                case 35:
                    if(e.Control)
                    {
                        finalizar_traspaso();
                    }
                break;
            }
        }

        private void txt_folio_busqueda_traspaso_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txt_comentarios_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void cbb_caducidad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_amecop_TextChanged(object sender, EventArgs e)
        {

        }

	}
}
