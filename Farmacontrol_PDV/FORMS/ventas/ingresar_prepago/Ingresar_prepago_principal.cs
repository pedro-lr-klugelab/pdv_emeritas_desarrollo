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
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.FORMS.ventas.ingresar_prepago
{
	public partial class Ingresar_prepago_principal : Form
	{
		long? empleado_empleado_id = null;
		long? encargado_empelado_id = null;
		DTO_Validacion validacion = new DTO_Validacion();
		DAO_Login login = new DAO_Login();
		DAO_Articulos dao_articulos = new DAO_Articulos();
		long? articulo_id = null;
		BindingList<DTO_Detallado_prepago> detallado_prepago = new BindingList<DTO_Detallado_prepago>();
		string cliente_id;

		public Ingresar_prepago_principal()
		{
			InitializeComponent();
			lbl_empleado.Visible = false;
			lbl_nombre_empleado.Visible = false;
			lbl_encargado.Visible = false;
			lbl_nombre_encargado.Visible = false;
			tbp_captura_productos.Parent = null;
			dgv_ventas.DataSource = detallado_prepago;
			get_totales();
		}

		bool validar_existencias_articulos_prepagos()
		{
			List<DTO_Detallado_prepago> detallado = new List<DTO_Detallado_prepago>();

			foreach (DataGridViewRow row in dgv_ventas.Rows)
			{
				DTO_Detallado_prepago item = new DTO_Detallado_prepago();
				item.articulo_id = Convert.ToInt64(row.Cells["c_articulo_id"].Value);
				item.cantidad = Convert.ToInt64(row.Cells["cantidad"].Value);
				item.amecop = row.Cells["amecop"].Value.ToString();
				item.producto = row.Cells["producto"].Value.ToString();
				detallado.Add(item);
			}

			DAO_Existencias dao_existencias = new DAO_Existencias();

			List<DTO_Existencia> existencia_articulos_prepago = new List<DTO_Existencia>();

			string[] articulos_ids = new string[detallado.Count];

			int count = 0;

			foreach (DTO_Detallado_prepago item in detallado_prepago)
			{
				articulos_ids[count] = item.articulo_id.ToString();
				count++;
			}

			existencia_articulos_prepago = dao_existencias.get_articulos_existencias_prepago(articulos_ids);

			if(existencia_articulos_prepago.Count > 0)
			{
				return true;
			}

			return false;
		}

		private void btn_procesar_Click(object sender, EventArgs e)
		{
			if(detallado_prepago.Count > 0)
			{
				/*if (cliente_id != "")
				{
					if(validar_existencias_articulos_prepagos())
					{
						DialogResult productos_existente = MessageBox.Show(this, "¿Deseas entregar los productos del prepago que tengas en tu inventario?", "Entrega parcial", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

						if (productos_existente == DialogResult.Yes)
						{
							List<DTO_Detallado_prepago> detallado = new List<DTO_Detallado_prepago>();

							foreach (DataGridViewRow row in dgv_ventas.Rows)
							{
								DTO_Detallado_prepago item = new DTO_Detallado_prepago();
								item.articulo_id = Convert.ToInt64(row.Cells["c_articulo_id"].Value);
								item.cantidad = Convert.ToInt64(row.Cells["cantidad"].Value);
								item.amecop = row.Cells["amecop"].Value.ToString();
								item.producto = row.Cells["producto"].Value.ToString();
								detallado.Add(item);
							}

							Entrega_parcial_prepago entrega_parcial = new Entrega_parcial_prepago(detallado);
							entrega_parcial.ShowDialog();

							if (entrega_parcial.para_prepago)
							{
								Pago_tipos_prepago pagos = new Pago_tipos_prepago(detallado_prepago.Sum(item => item.total));
								pagos.ShowDialog();

								if (pagos.terminar_prepago)
								{
									generar_prepago(entrega_parcial.detallado_ventas_vista_previa);
								}
							}
						}
						else
						{
							Pago_tipos_prepago pagos = new Pago_tipos_prepago(detallado_prepago.Sum(item => item.total));
							pagos.ShowDialog();

							if (pagos.terminar_prepago)
							{
								generar_prepago();
							}
						}
					}
					else
					{*/
						Pago_tipos_prepago pagos = new Pago_tipos_prepago(detallado_prepago.Sum(item => item.total));
						pagos.ShowDialog();

						if (pagos.terminar_prepago)
						{
							generar_prepago();
						}
					//}
				/*}
				else
				{
					MessageBox.Show(this, "Es necesario asignar un cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					btn_asignar_cliente.Focus();
				}*/	
			}
			else
			{
				MessageBox.Show(this, "No puedes generar un prepago sin productos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				txt_amecop.Focus();
			}
		}

		private void btn_tbp_ingrese_permisos_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btn_tbp_ingrese_permisos_continuar_Click(object sender, EventArgs e)
		{
			if(empleado_empleado_id != null && encargado_empelado_id != null)
			{
				tbp_captura_productos.Parent = tbc_principal;
				tbp_login.Parent = null;
				txt_amecop.Focus();
			}
			else
			{
				MessageBox.Show(this,"Es necesario que un ENCARGADO y un EMPLEADO confirmen que tienen los PERMISOS para generar el ingreso del prepago","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				
				if(encargado_empelado_id != null)
				{
					txt_usuario_empleado.Focus();
				}
				else
				{
					txt_usuario_encargado.Focus();
				}
			}
		}

		private void txt_usuario_encargado_KeyDown(object sender, KeyEventArgs e)
		{
			var txt_usuario = txt_usuario_encargado;
			var txt_password = txt_password_encargado;

			if (Convert.ToInt32(e.KeyCode) == 27 && Convert.ToInt32(txt_usuario.TextLength) > 0)
			{
				txt_usuario.Text = "";
			}

			if (Convert.ToInt32(e.KeyCode) == 13 && Convert.ToInt32(txt_usuario.TextLength) > 0)
			{
				validacion = new DTO_Validacion();

				if (txt_usuario.Text.Substring(0, 1).Equals("%") && txt_usuario.Text.Substring(txt_usuario.TextLength - 1).Equals("_"))
				{
					validacion = login.validar_fcid(txt_usuario.Text);

					if (validacion.status == true)
					{
						/*if (validar_funcion)
						{
							validar_permisos_funcion((int)validacion.elemento_id, validacion.elemento_nombre);
						}
						else
						{*/
							if(login.empleado_es_encargado(validacion.elemento_id))
							{
								encargado_empelado_id = validacion.elemento_id;
								lbl_nombre_encargado.Text = validacion.elemento_nombre;
								lbl_encargado.Visible = true;
								lbl_nombre_encargado.Visible = true;
								txt_usuario_empleado.Focus();
							}
							else
							{
								MessageBox.Show(this,"No eres encargado","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
							}
						//}
					}
					else
					{
						MessageBox.Show(this, "El FCID es invalido o se encuentra desactivado");
					}
				}
				else
				{
					validacion = login.validar_usuario(txt_usuario.Text.ToString());

					if (validacion.status == true)
					{
						txt_usuario.Enabled = false;
						txt_password.Enabled = true;
						txt_password.Focus();
					}
					else
					{
						MessageBox.Show(this, "Usuario no registrado");
					}
				}
			}
		}

		private void txt_password_encargado_KeyDown(object sender, KeyEventArgs e)
		{
			var txt_usuario = txt_usuario_encargado;
			var txt_password = txt_password_encargado;

			if (Convert.ToInt32(e.KeyCode) == 27)
			{
				txt_usuario.Enabled = true;
				txt_password.Text = "";
				txt_password.Enabled = false;
				txt_usuario.Focus();
			}

			if (Convert.ToInt32(e.KeyCode) == 13 && Convert.ToInt32(txt_password.TextLength) > 0)
			{
				if (Convert.ToInt32(txt_usuario.TextLength) > 0)
				{
					validacion = login.validar_usuario_password(txt_usuario.Text, txt_password.Text);

					if (validacion.status == true)
					{

						if (login.empleado_es_encargado(validacion.elemento_id))
						{
							txt_password.Enabled = false;
							encargado_empelado_id = validacion.elemento_id;
							lbl_nombre_encargado.Text = validacion.elemento_nombre;
							lbl_encargado.Visible = true;
							lbl_nombre_encargado.Visible = true;
							txt_usuario_empleado.Focus();
						}
						else
						{
							MessageBox.Show(this, "No eres encargado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							txt_usuario_encargado.Text = "";
							txt_usuario_encargado.Enabled = true;
							txt_password_encargado.Enabled = false;
							txt_password_encargado.Text = "";
							txt_usuario_encargado.Focus();
						}
					}
					else
					{
						MessageBox.Show(this, validacion.informacion);
						txt_password.Text = "";
						txt_password.Focus();
					}
				}
				else
				{
					txt_usuario.Focus();
				}
			}   
		}

		private void txt_usuario_empleado_KeyDown(object sender, KeyEventArgs e)
		{
			var txt_usuario = txt_usuario_empleado;
			var txt_password = txt_password_empleado;

			if (Convert.ToInt32(e.KeyCode) == 27 && Convert.ToInt32(txt_usuario.TextLength) > 0)
			{
				txt_usuario.Text = "";
			}

			if (Convert.ToInt32(e.KeyCode) == 13 && Convert.ToInt32(txt_usuario.TextLength) > 0)
			{
				validacion = new DTO_Validacion();

				if (txt_usuario.Text.Substring(0, 1).Equals("%") && txt_usuario.Text.Substring(txt_usuario.TextLength - 1).Equals("_"))
				{
					validacion = login.validar_fcid(txt_usuario.Text);

					if (validacion.status == true)
					{
						/*if (validar_funcion)
						{
							validar_permisos_funcion((int)validacion.elemento_id, validacion.elemento_nombre);
						}
						else
						{*/
						empleado_empleado_id = validacion.elemento_id;
						lbl_nombre_empleado.Text = validacion.elemento_nombre;
						lbl_empleado.Visible = true;
						lbl_nombre_empleado.Visible = true;
                        btn_tbp_ingrese_permisos_continuar.Focus();
						//}
					}
					else
					{
						MessageBox.Show(this, "El FCID es invalido o se encuentra desactivado");
					}
				}
				else
				{
					validacion = login.validar_usuario(txt_usuario.Text.ToString());

					if (validacion.status == true)
					{
						txt_usuario.Enabled = false;
						txt_password.Enabled = true;
						txt_password.Focus();
					}
					else
					{
						MessageBox.Show(this, "Usuario no registrado");
					}
				}
			}
		}

		private void txt_password_empleado_KeyDown(object sender, KeyEventArgs e)
		{
			var txt_usuario = txt_usuario_empleado;
			var txt_password = txt_password_empleado;

			if (Convert.ToInt32(e.KeyCode) == 27)
			{
				txt_usuario.Enabled = true;
				txt_password.Text = "";
				txt_password.Enabled = false;
				txt_usuario.Focus();
			}

			if (Convert.ToInt32(e.KeyCode) == 13 && Convert.ToInt32(txt_password.TextLength) > 0)
			{
				if (Convert.ToInt32(txt_usuario.TextLength) > 0)
				{
					validacion = login.validar_usuario_password(txt_usuario.Text, txt_password.Text);

					if (validacion.status == true)
					{
						txt_password.Enabled = false;
						empleado_empleado_id = validacion.elemento_id;
						lbl_nombre_empleado.Text = validacion.elemento_nombre;
						lbl_empleado.Visible = true;
						lbl_nombre_empleado.Visible = true;
					}
					else
					{
						MessageBox.Show(this, validacion.informacion);
						txt_password.Text = "";
						txt_password.Focus();
					}
				}
				else
				{
					txt_usuario.Focus();
				}
			}   
		}

		private void get_totales()
		{
			decimal total = 0;
			decimal subtotal = 0;
			int piezas = 0;
			decimal iva = 0;
			decimal ieps = 0;
			decimal gravado = 0;
			decimal excento = 0;

			foreach (var row in detallado_prepago)
			{
				total += row.total;
				subtotal += row.subtotal;
				iva += row.importe_iva;
				ieps += row.importe_ieps;
				piezas += (int)row.cantidad;

				if (row.importe_iva > (decimal)0)
				{
					gravado += row.subtotal;
				}
				else
				{
					excento += row.subtotal;
				}
			}

			txt_total.Text = string.Format("{0:C}", total);
			txt_subtotal.Text = string.Format("{0:C}", subtotal);
			txt_ieps.Text = string.Format("{0:C}", ieps);
			txt_iva.Text = string.Format("{0:C}", iva);
			txt_piezas.Text = piezas.ToString();
			txt_excento.Text = string.Format("{0:C}", excento);
			txt_gravado.Text = string.Format("{0:C}", gravado);
		}

		public void busqueda_productos_Close(object sender, FormClosedEventArgs e)
		{
			if (Busqueda_productos.articulo_articulo_id != null)
			{
				txt_amecop.Text = Busqueda_productos.articulo_amecop;
				txt_producto.Text = Busqueda_productos.articulo_producto;
				articulo_id = Busqueda_productos.articulo_articulo_id;

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
				case 27:
					if (txt_amecop.Text.Trim().Length > 0)
					{
						txt_amecop.Text = "";
						txt_producto.Text = "";
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
                    txt_amecop.Text = articulo.Amecop;
                    txt_producto.Text = articulo.Nombre;
                    articulo_id = articulo.Articulo_id;

                    txt_cantidad.Enabled = true;
                    txt_cantidad.Text = "1";
                    txt_cantidad.SelectAll();
                    txt_cantidad.Focus();

                    txt_amecop.Enabled = false;
                }
                else
                {
                    txt_amecop.Text = "";
                    MessageBox.Show(this, "Producto no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
		}

		void limpiar_informacion()
		{
			articulo_id = null;
			txt_amecop.Text = "";
			txt_producto.Text = "";
			txt_cantidad.Text = "";

			txt_amecop.Enabled = true;
			txt_amecop.Focus();
			txt_cantidad.Enabled = false;
			dgv_ventas.ClearSelection();
		}

		void insertar_productos()
		{
			var articulo = dao_articulos.get_articulo(txt_amecop.Text);

			DTO_Detallado_prepago detallado = new DTO_Detallado_prepago();
			detallado.articulo_id = (long)articulo.Articulo_id;
			detallado.amecop = txt_amecop.Text;
			detallado.producto = txt_producto.Text;
			detallado.precio_publico = articulo.Precio_publico;
			detallado.pct_descuento = articulo.Pct_descuento;
			detallado.importe_descuento = (articulo.Pct_descuento * articulo.Precio_publico);
			detallado.importe = articulo.Precio_publico - detallado.importe_descuento;

			bool existe_producto = false;

			foreach(var detallado_item in detallado_prepago)
			{
				if(detallado_item.articulo_id == detallado.articulo_id)
				{
					detallado_item.cantidad += Convert.ToInt64(txt_cantidad.Text);
					detallado_item.subtotal = (detallado_item.cantidad * detallado_item.importe);
					detallado_item.pct_iva = articulo.Pct_iva;
					detallado_item.importe_iva = (articulo.Pct_iva * detallado_item.subtotal);
					detallado_item.tipo_ieps = articulo.tipo_ieps;
					detallado_item.ieps = articulo.ieps;
					detallado_item.importe_ieps = (detallado_item.tipo_ieps.Equals("PCT")) ? (detallado_item.ieps * detallado_item.importe) : (detallado_item.ieps);
					detallado_item.total = ((detallado_item.importe * detallado_item.cantidad) + detallado_item.importe_iva);
					dgv_ventas.Refresh();
					existe_producto = true;
				}
			}

			if(!existe_producto)
			{
				detallado.cantidad = Convert.ToInt64(txt_cantidad.Text);
				detallado.subtotal = (detallado.cantidad * detallado.importe);
				detallado.pct_iva = articulo.Pct_iva;
				detallado.importe_iva = (articulo.Pct_iva * detallado.subtotal);
				detallado.tipo_ieps = articulo.tipo_ieps;
				detallado.ieps = articulo.ieps;
				detallado.importe_ieps = (detallado.tipo_ieps.Equals("PCT")) ? (detallado.ieps * detallado.importe) : (detallado.ieps);
				detallado.total = ((detallado.importe * detallado.cantidad) + detallado.importe_iva);

				detallado_prepago.Add(detallado);
			}

			dgv_ventas.ClearSelection();
			limpiar_informacion();	
			get_totales();
		}

		private void txt_cantidad_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(txt_cantidad.Text.Trim().Length > 0)
					{
						if (Convert.ToInt32(txt_cantidad.Text) > 0)
						{
							insertar_productos();
						}	
					}
				break;
				case 27:
					limpiar_informacion();
				break;
			}

		}

		private void dgv_ventas_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
		{
			get_totales();
			txt_amecop.Focus();
			dgv_ventas.Refresh();
			dgv_ventas.ClearSelection();
		}

		private void Ingresar_prepago_principal_Shown(object sender, EventArgs e)
		{
			txt_usuario_encargado.Focus();
		}

		private void txt_amecop_Enter(object sender, EventArgs e)
		{
			dgv_ventas.ClearSelection();
		}

		private void btn_asignar_cliente_Click(object sender, EventArgs e)
		{
			Busqueda_cliente cliente = new Busqueda_cliente();
			cliente.ShowDialog();

			if(!cliente.cliente_id.Equals(""))
			{
				cliente_id = cliente.cliente_id;
				txt_nombre_cliente.Text = cliente.nombre_cliente;
				btn_asignar_cliente.Text = "Cambiar cliente";
				txt_amecop.Focus();
			}
		}

		void generar_prepago(List<DTO_Detallado_ventas_vista_previa> detallado_vista_previa = null)
		{
			decimal total = 0;
			foreach(var det in detallado_prepago)
			{
				total += det.total;
			}

			DTO_Prepago prepago = new DTO_Prepago();
            prepago.cliente_id = (cliente_id == "") ? "" : cliente_id;
			prepago.pago_empleado_id = (long)empleado_empleado_id;
			prepago.codigo = Misc_helper.uuid_small();
			prepago.monto = total;
			prepago.comentario = txt_justificacion.Text;

			DAO_Prepago dao_prepago = new DAO_Prepago();

			List<DTO_Detallado_prepago> lista_detallado = new List<DTO_Detallado_prepago>();


			foreach(DTO_Detallado_prepago det in detallado_prepago)
			{
				lista_detallado.Add(det);
			}

			var result = dao_prepago.generar_prepago(prepago,lista_detallado,detallado_vista_previa);

			if(result)
			{
				MessageBox.Show(this,"Prepago generado correctamente","Información",MessageBoxButtons.OK,MessageBoxIcon.Information);
				
                this.Close();
			}
			else
			{
				MessageBox.Show(this, "Ocurrio un error al intentar generar el prepago, intentelo mas tarde", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void txt_justificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btn_tbp_capturar_productos_cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
	}
}
