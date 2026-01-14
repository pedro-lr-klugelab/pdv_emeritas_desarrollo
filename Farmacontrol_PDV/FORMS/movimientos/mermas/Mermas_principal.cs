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
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.CLASSES.PRINT;

namespace Farmacontrol_PDV.FORMS.movimientos.mermas
{
	public partial class Mermas_principal : Form
	{
		DAO_Mermas dao_mermas = new DAO_Mermas();
		DAO_Articulos dao_articulos = new DAO_Articulos();
		DTO_Articulo dto_articulo = new DTO_Articulo();
		DTO_Merma dto_merma = new DTO_Merma();
		
		public Mermas_principal()
		{
			InitializeComponent();
		}

		private void btn_nuevo_Click(object sender, EventArgs e)
		{
			Login_form login = new Login_form("crear_merma");
			login.ShowDialog();

			if(login.empleado_id != null)
			{
				DialogResult dr = MessageBox.Show(this,"Al crear un folio de merma nuevo se importaran todos los productos que se encuentren en el APARTADO DE MERCANCIA con destino MERMA, ¿Desea Continuar?","Crear Merma",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

				if(dr == DialogResult.Yes)
				{
					long merma_id = dao_mermas.crear_merma((long)login.empleado_id);

					if(merma_id > 0)
					{
						//MessageBox.Show(this,"Captura de merma creada correctamente con el folio #"+merma_id,"Merma",MessageBoxButtons.OK,MessageBoxIcon.Information);
						rellenar_informacion_merma(merma_id);
					}
					else
					{
						MessageBox.Show(this, "Ocurrio un problema al intentar crear la captura de merma, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		public void rellenar_informacion_merma(long merma_id)
		{
			var dto = dao_mermas.get_informacion_merma(merma_id);

			if(dto.merma_id > 0)
			{
				dto_merma = dto;
				txt_empleado_captura.Text = dto_merma.nombre_empleado_captura;
				txt_empleado_termina.Text = dto_merma.nombre_empleado_termina;
                txt_fecha_creado.Text = (dto_merma.fecha_creado != null) ? Misc_helper.fecha(dto_merma.fecha_creado.ToString().ToUpper(), "LEGIBLE") : " - ";
                txt_fecha_terminado.Text = (dto_merma.fecha_terminado != null) ? Misc_helper.fecha(dto_merma.fecha_terminado.ToString(), "LEGIBLE") : " - ";
				txt_comentarios_entrada.Text = dto_merma.comentarios;
				txt_folio_busqueda_traspaso.Value = dto_merma.merma_id;

				txt_estado.Text = (dto_merma.fecha_terminado.Equals(null)) ? "ABIERTO" : "CERRADO";
				txt_estado.BackColor = (dto_merma.fecha_terminado.Equals(null)) ? Color.Green : Color.Red;

				dgv_mermas.DataSource = dao_mermas.get_detallado_merma(merma_id);
				dgv_mermas.ClearSelection();

				validar_bloqueo();
			}
		}

		public void validar_bloqueo()
		{
			if(dto_merma.fecha_terminado.Equals(null))
			{
				txt_comentarios_entrada.Enabled = true;
				txt_amecop.Enabled = true;
				dgv_mermas.ReadOnly = false;

				txt_amecop.Focus();

				lbl_mensaje_bloqueo.Text = "";
				lbl_mensaje_bloqueo.Visible = false;
				lbl_mensaje_bloqueo.Parent = null;
			}
			else
			{
				txt_comentarios_entrada.Enabled = false;
				txt_amecop.Enabled = false;
				dgv_mermas.ReadOnly = true;

				txt_folio_busqueda_traspaso.Focus();

				if(dgv_mermas.Rows.Count > 0)
				{
					lbl_mensaje_bloqueo.Text = "";
					lbl_mensaje_bloqueo.Visible = false;
					lbl_mensaje_bloqueo.Parent = null;
				}
				else
				{
					lbl_mensaje_bloqueo.Parent = dgv_mermas;
					lbl_mensaje_bloqueo.Visible = true;
					lbl_mensaje_bloqueo.Text = "Captura de merma terminada vacia";
					lbl_mensaje_bloqueo.Parent = dgv_mermas;
				}
			}
		}

		private void Mermas_principal_Shown(object sender, EventArgs e)
		{
			var merma_id = dao_mermas.get_merma_fin();

			if(merma_id > 0)
			{
				lbl_mensaje_bloqueo.Parent = null;
				lbl_mensaje_bloqueo.Visible = false;
				lbl_mensaje_bloqueo.Text = "";
				rellenar_informacion_merma(merma_id);
			}
			else
			{
				lbl_mensaje_bloqueo.Text = "No se encontro ningun registro de mermas anteriores";
				lbl_mensaje_bloqueo.Parent = dgv_mermas;
				lbl_mensaje_bloqueo.Visible = true;
			}
		}

		private void txt_amecop_Enter(object sender, EventArgs e)
		{
			dgv_mermas.ClearSelection();
		}

		private void btn_inicio_Click(object sender, EventArgs e)
		{
			var merma_id = dao_mermas.get_merma_inicio();

			if(merma_id > 0)
			{
				rellenar_informacion_merma(merma_id);
			}
		}

		private void btn_fin_Click(object sender, EventArgs e)
		{
			var merma_id = dao_mermas.get_merma_fin();

			if (merma_id > 0)
			{
				rellenar_informacion_merma(merma_id);
			}
		}

		private void btn_atras_Click(object sender, EventArgs e)
		{
			var merma_id = dao_mermas.get_merma_atras(dto_merma.merma_id);

			if (merma_id > 0)
			{
				rellenar_informacion_merma(merma_id);
			}
		}

		private void btn_siguiente_Click(object sender, EventArgs e)
		{
			var merma_id = dao_mermas.get_merma_siguiente(dto_merma.merma_id);

			if (merma_id > 0)
			{
				rellenar_informacion_merma(merma_id);
			}
		}

		private void txt_comentarios_entrada_Leave(object sender, EventArgs e)
		{
			dao_mermas.set_comentario(dto_merma.merma_id,txt_comentarios_entrada.Text);
		}

		#region EVENTOS DE CAPTURA

		private void txt_amecop_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 76:
					if (e.Control)
					{
						DialogResult dr = MessageBox.Show(this, "Esta a punto de borrar permanentemente toda la informacion de la merma, ¿desea continuar?", "Limpiar Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

						if (dr == DialogResult.Yes)
						{
							limpiar_merma();
						}
					}
					break;
				case 40:
					if (dgv_mermas.Rows.Count > 0)
					{
						dgv_mermas.CurrentCell = dgv_mermas.Rows[0].Cells["c_amecop"];
						dgv_mermas.Rows[0].Selected = true;
						dgv_mermas.Focus();
					}
					break;
				case 13:
                    if (txt_amecop.TextLength > 0)
                    { 
                        busqueda_producto(); 
                    }
					break;
                case 27:
                    txt_amecop.Text = "";
                break;
				case 114:
                    form_busqueda_producto();
					/*Busqueda_productos busqueda_productos = new Busqueda_productos("");
					busqueda_productos.FormClosed += new FormClosedEventHandler(busqueda_productos_Close);
					busqueda_productos.ShowDialog();
					txt_cantidad.Focus();*/
				break;
			}
		}

		public void limpiar_informacion()
		{
			txt_amecop.Enabled = true;
			txt_amecop.Text = "";
			txt_amecop.Focus();

			txt_producto.Text = "";
			cbb_caducidad.DataSource = new BindingSource(new Dictionary<string,string>(){{"",""}},null);
			cbb_caducidad.Enabled =  false;
			cbb_caducidad.DisplayMember = "Key";
			cbb_caducidad.ValueMember = "Value";

			cbb_lote.DataSource = new BindingSource(new Dictionary<string, string>() { { "", "" } }, null);
			cbb_lote.DisplayMember = "Key";
			cbb_lote.ValueMember = "Value";

			cbb_lote.Enabled = false;

			txt_cantidad.Value = 1;
			txt_cantidad.Enabled = false;
			dto_articulo = null;
		}

		public void rellenar_informacion_producto()
		{
			txt_amecop.Enabled = false;
			txt_producto.Text = dto_articulo.Nombre;
			cbb_caducidad.Enabled = true;

			if (dto_articulo.Caducidades.Rows.Count > 0)
			{
				cbb_caducidad.DataSource = null;
				Dictionary<string, string> dic_cad = new Dictionary<string, string>();

				foreach (DataRow row in dto_articulo.Caducidades.Rows)
				{
                    string caducidad = Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD") + " (" + row["existencia"] + ")";

					if(!dic_cad.ContainsKey(caducidad))
					{
						dic_cad.Add(caducidad, row["caducidad"].ToString());	
					}
				}

				BindingSource source = new BindingSource(dic_cad,null);
				cbb_caducidad.DataSource = source;
				cbb_caducidad.DisplayMember = "Key";
				cbb_caducidad.ValueMember = "Value";

				cbb_caducidad.DroppedDown = true;
				cbb_caducidad.SelectedIndex = 0;
				cbb_caducidad.Focus();
			}
			else
			{
				MessageBox.Show(this,"Este producto no tiene existencias, imposible agregar","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				limpiar_informacion();
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
                dto_articulo = dao_articulos.get_articulo(txt_amecop.Text);

                if (dto_articulo.Articulo_id != null)
                {
                    rellenar_informacion_producto();
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
			try
			{
				long? articulo_id = Busqueda_productos.articulo_articulo_id;
				if (articulo_id != null)
				{
					dto_articulo = new DTO_Articulo();
					dto_articulo.Articulo_id = (int)articulo_id;
					txt_amecop.Text = Busqueda_productos.articulo_amecop;
					txt_producto.Text = Busqueda_productos.articulo_producto;

					cbb_caducidad.DataSource = new BindingSource(new Dictionary<string, string>(){
					{Busqueda_productos.caducidad_item.Text,Busqueda_productos.caducidad_item.Value.ToString()}
				}, null);

					cbb_caducidad.DisplayMember = "Key";
					cbb_caducidad.ValueMember = "Value";

					cbb_lote.DataSource = new BindingSource(new Dictionary<string, string>(){
					{Busqueda_productos.lote_item.Text,Busqueda_productos.lote_item.Text}
				}, null);

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
			catch(Exception ex)
			{
				Log_error.log(ex);
			}
		}

		private void cbb_caducidad_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					busqueda_lotes();
					cbb_caducidad.Enabled = false;	
				break;
				case 27:
					limpiar_informacion();
				break;
			}
		}

		public void busqueda_lotes()
		{
			DataTable result_lotes = dao_articulos.get_lotes((int)dto_articulo.Articulo_id, cbb_caducidad.SelectedValue.ToString());
			cbb_lote.Enabled = true;

			cbb_lote.DataSource = null;
			Dictionary<string,string> dic_lote = new Dictionary<string,string>();

			foreach (DataRow row in result_lotes.Rows)
			{
				dic_lote.Add((row["lote"].ToString().Equals(" ")) ? "SIN LOTE" : row["lote"].ToString(), row["lote"].ToString());
			}

			BindingSource source = new BindingSource(dic_lote,null);

			cbb_lote.DataSource = source;
			cbb_lote.DisplayMember = "Key";
			cbb_lote.ValueMember = "Value";

			cbb_lote.DroppedDown = true;
			cbb_lote.Focus();
		}

		private void cbb_lote_KeyDown(object sender, KeyEventArgs e)
		{
			if (Convert.ToInt32(e.KeyCode) == 13)
			{
				cbb_lote.Enabled = false;
				txt_cantidad.Enabled = true;
				txt_cantidad.Value = 1;
				txt_cantidad.Select(0,txt_cantidad.Text.Length);
				txt_cantidad.Focus();
			}
			else if (Convert.ToInt32(e.KeyCode) == 27)
			{
				cbb_caducidad.Enabled = true;
				cbb_caducidad.Focus();
				cbb_caducidad.DroppedDown = true;
				cbb_lote.DataSource = null;
				cbb_lote.Enabled = false;
			}
		}

		private void txt_cantidad_KeyDown(object sender, KeyEventArgs e)
		{
			if (Convert.ToInt32(e.KeyCode) == 27)
			{
				limpiar_informacion();
			}

			if (Convert.ToInt32(e.KeyCode) == 13 && txt_cantidad.Value > 0)
			{
				insertar_producto_merma();
			}
		}

		#endregion

		public void insertar_producto_merma()
		{
			try
			{
                DAO_Articulos dao = new DAO_Articulos();
                long existencia_vendible =  dao.get_existencia_vendible(txt_amecop.Text.Trim(), cbb_caducidad.SelectedValue.ToString(), cbb_lote.SelectedValue.ToString());

                if (existencia_vendible >= Convert.ToInt32(txt_cantidad.Value))
                {
                    dgv_mermas.DataSource = dao_mermas.insertar_producto_merma(dto_merma.merma_id, (int)dto_articulo.Articulo_id, cbb_caducidad.SelectedValue.ToString(), cbb_lote.SelectedValue.ToString(), Convert.ToInt32(txt_cantidad.Value));
                    limpiar_informacion();
                }
                else
                {
                    MessageBox.Show(this,"No cuentas con la sufiencte existencia para dar de baja este producto","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
			}
			catch(Exception EX)
			{
				MessageBox.Show(EX.ToString());
			}
		}

		private void dgv_mermas_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
                case 67:
                    if (e.Control)
                    {
                        Clipboard.SetText(dgv_mermas.SelectedRows[0].Cells["c_amecop"].Value.ToString());
                    }
                break;
				case 27:
					txt_amecop.Focus();
				break;
				case 46:
					if(dgv_mermas.SelectedRows.Count > 0)
					{
						var detallado_merma_id = Convert.ToInt32(dgv_mermas.SelectedRows[0].Cells["c_detallado_merma_id"].Value);
						dgv_mermas.DataSource = dao_mermas.eliminar_producto_merma(dto_merma.merma_id,detallado_merma_id);
						txt_amecop.Focus();
					}
				break;
			}
		}

		public void limpiar_merma()
		{
			dgv_mermas.DataSource = dao_mermas.limpiar_detallado_merma(dto_merma.merma_id);
			limpiar_informacion();
		}

		private void btn_terminar_Click(object sender, EventArgs e)
		{
			if(dto_merma.merma_id > 0)
			{
				if (dto_merma.fecha_terminado.Equals(null))
				{
					Login_form login = new Login_form("terminar_merma");
					login.ShowDialog();

					if (login.empleado_id != null)
					{
						DialogResult dr = MessageBox.Show(this, "Al terminar la merma se afectaran los cambios permanentemente, ¿Desea Continuar?", "Terminar Merma", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

						if (dr == DialogResult.Yes)
						{
							long filas_afectadas = dao_mermas.terminar_merma(dto_merma.merma_id, (long)login.empleado_id);

							if (filas_afectadas > 0)
							{
								Mermas ticket = new Mermas();
								ticket.construccion_ticket(dto_merma.merma_id);
								ticket.print();
								MessageBox.Show(this, "Captura de merma terminada correctamente", "Merma", MessageBoxButtons.OK, MessageBoxIcon.Information);
								rellenar_informacion_merma(dto_merma.merma_id);
							}
							else
							{
								MessageBox.Show(this, "Ocurrio un problema al intentar terminar la captura de merma, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
					}
				}	
			}	
		}

		private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			rellenar_informacion_merma(dto_merma.merma_id);
		}

		private void txt_folio_busqueda_traspaso_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(txt_folio_busqueda_traspaso.Value > 0)
					{
						try
						{
							long merma_id = Convert.ToInt64(txt_folio_busqueda_traspaso.Value);
							rellenar_informacion_merma(merma_id);
						}
						catch(Exception ex)
						{
							Log_error.log(ex);
						}
					}
				break;
			}
		}

		private void txt_folio_busqueda_traspaso_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
			}
		}

		private void btn_imprimir_Click(object sender, EventArgs e)
		{
			if(dto_merma.merma_id > 0)
			{
				Mermas ticket = new Mermas();
				ticket.construccion_ticket(dto_merma.merma_id, true);
				ticket.print();	
			}
		}

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbb_caducidad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
	}
}
