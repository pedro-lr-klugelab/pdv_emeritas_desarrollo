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
using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.FORMS.ventas.cotizar_formula
{
	public partial class Cotizar_formula_principal : Form
	{
		DTO_Articulo articulo_registrar = new DTO_Articulo();
		DTO_Materia_prima materia_prima = new DTO_Materia_prima();
		BindingList<DTO_Detallado_formulas> data = new BindingList<DTO_Detallado_formulas>();

		public bool cotizacion_impresa = false;

		decimal pct_iva_global = Convert.ToDecimal(Config_helper.get_config_global("pct_iva"));

		public Cotizar_formula_principal()
		{
			InitializeComponent();
			dgv_detallado_formulas.DataSource = data;
			get_sucursales();

        }

		private void txt_amecop_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 27:
					txt_amecop.Text = "";
				break;
				case 13:
					if(txt_amecop.Text.Trim().Length > 0)
					{
						buscar_articulo(txt_amecop.Text.Trim());
					}
				break;
				case 114:
					Busqueda_productos_materias_primas busqueda = new Busqueda_productos_materias_primas();
					busqueda.ShowDialog();

					if(busqueda.select_articulo_id != null)
					{
						busqueda_informacion_articulo((long)busqueda.select_articulo_id);
					}
					else if(busqueda.select_materia_prima_id != null)
					{
						busqueda_informacion_materia((long)busqueda.select_materia_prima_id);
					}
				break;
				case 40:
					if (dgv_detallado_formulas.Rows.Count > 0)
					{
						dgv_detallado_formulas.CurrentCell = dgv_detallado_formulas.Rows[0].Cells["amecop"];
						dgv_detallado_formulas.Focus();
						dgv_detallado_formulas.Rows[0].Selected = true;
					}
				break;
			}
		}

		void registrar_detallado_articulo()
		{
			DTO_Detallado_formulas det_materia_prima = new DTO_Detallado_formulas();
			det_materia_prima.detallado_formula_id = null;
			det_materia_prima.amecop = articulo_registrar.Amecop;
			det_materia_prima.nombre = articulo_registrar.Nombre;
			det_materia_prima.formula_id = null;
			det_materia_prima.materia_prima_id = null;
			det_materia_prima.articulo_id = articulo_registrar.Articulo_id;
			det_materia_prima.precio_publico = articulo_registrar.Precio_publico;
			det_materia_prima.importe = articulo_registrar.Precio_publico;
			det_materia_prima.cantidad = Convert.ToDecimal(txt_cantidad.Value);
			det_materia_prima.subtotal = (det_materia_prima.precio_publico *det_materia_prima.cantidad);
			det_materia_prima.comentarios = txt_comentario_producto.Text;
			det_materia_prima.total = (det_materia_prima.subtotal + (det_materia_prima.subtotal * pct_iva_global));

			bool actualizado = false;

			foreach (DTO_Detallado_formulas item in data)
			{
				if(item.articulo_id == det_materia_prima.articulo_id)
				{
					actualizado = true;

					item.detallado_formula_id = det_materia_prima.detallado_formula_id;
					item.amecop = det_materia_prima.amecop;
					item.nombre = det_materia_prima.nombre;
					item.formula_id = det_materia_prima.formula_id;
					item.materia_prima_id = det_materia_prima.materia_prima_id;

					item.precio_publico = det_materia_prima.precio_publico;
					item.importe = det_materia_prima.importe;
					item.cantidad = det_materia_prima.cantidad;
					item.subtotal = det_materia_prima.subtotal;
					item.comentarios = det_materia_prima.comentarios;
					item.total = det_materia_prima.total;
				}
			}
			
			if(actualizado)
			{
				dgv_detallado_formulas.Refresh();
				dgv_detallado_formulas.ClearSelection();
			}
			else
			{
				data.Add(det_materia_prima);
				dgv_detallado_formulas.ClearSelection();	
			}

			limpiar_informacion_articulo();
			get_totales();
		}

		void registrar_detallado_materia()
		{
			if(Convert.ToDecimal(materia_prima.minimo) > Convert.ToDecimal(txt_cantidad.Text.Trim()))
			{
				MessageBox.Show(this, "La cantidad minima para la venta de este producto es de " + materia_prima.minimo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				DTO_Detallado_formulas det_materia_prima = new DTO_Detallado_formulas();
				det_materia_prima.detallado_formula_id = null;
				det_materia_prima.amecop = "N/A";
				det_materia_prima.nombre = materia_prima.nombre;
				det_materia_prima.formula_id = null;
				det_materia_prima.materia_prima_id = materia_prima.materia_prima_id;
				det_materia_prima.articulo_id = null;
				det_materia_prima.precio_publico = materia_prima.precio_publico;
				det_materia_prima.importe = det_materia_prima.precio_publico;
				det_materia_prima.cantidad = Convert.ToDecimal(txt_cantidad.Value);
				det_materia_prima.subtotal = (det_materia_prima.precio_publico * det_materia_prima.cantidad);
				det_materia_prima.comentarios = txt_comentario_producto.Text;
				det_materia_prima.total = det_materia_prima.subtotal + (det_materia_prima.subtotal * pct_iva_global);

				bool actualizado = false;

				foreach (DTO_Detallado_formulas item in data)
				{
					if (item.materia_prima_id == det_materia_prima.materia_prima_id)
					{
						actualizado = true;

						item.detallado_formula_id = det_materia_prima.detallado_formula_id;
						item.amecop = det_materia_prima.amecop;
						item.nombre = det_materia_prima.nombre;
						item.formula_id = det_materia_prima.formula_id;
						
						item.articulo_id = det_materia_prima.articulo_id;
						item.precio_publico = det_materia_prima.precio_publico;
						item.importe = det_materia_prima.importe;
						item.cantidad = det_materia_prima.cantidad;
						item.subtotal = det_materia_prima.subtotal;
						item.comentarios = det_materia_prima.comentarios;
						item.total = det_materia_prima.total ;
					}
				}

				if (actualizado)
				{
					dgv_detallado_formulas.Refresh();
					dgv_detallado_formulas.ClearSelection();
				}
				else
				{
					data.Add(det_materia_prima);
					dgv_detallado_formulas.ClearSelection();
				}

				limpiar_informacion_materia();
				get_totales();
			}
		}

		void buscar_articulo(string amecop)
		{
			DAO_Articulos dao_articulos = new DAO_Articulos();
			var response = dao_articulos.get_articulo(amecop);

			if(response.Articulo_id != null)
			{
				rellenar_informacion_articulo(response);
			}
			else
			{
				MessageBox.Show(this,"Error","Producto no encontrado",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		void busqueda_informacion_articulo(long articulo_id)
		{
			DAO_Articulos dao_articulos = new DAO_Articulos();
			var articulo = dao_articulos.get_articulo(articulo_id);
			rellenar_informacion_articulo(articulo);
		}

		void rellenar_informacion_articulo(DTO_Articulo articulo)
		{
			articulo_registrar = articulo;

			txt_comentario_producto.Enabled = true;
			txt_amecop.Text = 	articulo.Amecop;
			txt_producto.Text = articulo.Nombre;

			txt_comentario_producto.Focus();

			/*txt_cantidad.Text = "1";
			txt_cantidad.Select(0, txt_cantidad.Text.ToString().Length);
			txt_cantidad.Focus();*/

			txt_amecop.Enabled = false;
		}

		void rellenar_informacion_materia(DTO_Materia_prima materia)
		{
			txt_amecop.Text = "0000000000000";
			txt_producto.Text = materia.nombre;

			txt_comentario_producto.Enabled = true;
			txt_comentario_producto.Focus();

			txt_cantidad.Enabled = false;
			txt_amecop.Enabled = false;
		}

		void busqueda_informacion_materia(long materia_prima_id)
		{
			DAO_Materias_primas dao_materias = new DAO_Materias_primas();
			var materia = dao_materias.get_materia_prima(materia_prima_id);
			materia_prima = materia;
			rellenar_informacion_materia(materia);
		}

		private void txt_cantidad_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(txt_cantidad.Value.ToString().Trim().Length > 0)
					{
						
						if (Convert.ToDecimal(txt_cantidad.Value) > 0)
						{
							try
							{
								if (articulo_registrar.Articulo_id != null)
								{
									registrar_detallado_articulo();
								}
								else
								{
									registrar_detallado_materia();
								}
							}
							catch(Exception ex)
							{
								Log_error.log(ex);
							}		
						}
						else
						{
							MessageBox.Show(this,"La cantidad minima es de 1","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
							txt_cantidad.Text = "1";
						}
					}
				break;
				case 27:
					if(articulo_registrar.Articulo_id != null)
					{
						txt_comentario_producto.Enabled = true;
						txt_comentario_producto.Focus();

						txt_cantidad.Value = 1;
						txt_cantidad.Enabled = false;

						//limpiar_informacion_articulo();
					}
					else
					{	
						//txt_cantidad.Text = "";
						//txt_cantidad.Enabled = false;
					}
				break;
			}
		}

		void limpiar_informacion_articulo()
		{
			articulo_registrar.Articulo_id = null;
			txt_comentario_producto.Enabled = false;
			txt_comentario_producto.Text = "";
			txt_amecop.Enabled = true;
			txt_amecop.Focus();
			txt_amecop.Text = "";
			txt_producto.Text = "";
			txt_cantidad.Text = "";
			txt_cantidad.Enabled = false;
		}

		private void cbb_unidades_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 27:
					limpiar_informacion_materia();
				break;
				case 13:
					txt_cantidad.Enabled = true;
					txt_cantidad.Text = "1";
					txt_cantidad.Select(1, txt_cantidad.Text.ToString().Length);
					txt_cantidad.Focus();

					//cbb_unidades.Enabled = false;
				break;
			}
		}

		void limpiar_informacion_materia()
		{
			materia_prima.materia_prima_id = null;
			txt_comentario_producto.Enabled = false;
			txt_comentario_producto.Text = "";
			txt_amecop.Enabled = true;
			txt_amecop.Focus();
			txt_amecop.Text = "";
			txt_producto.Text = "";
			txt_cantidad.Enabled = false;
			txt_cantidad.Text = "";
		}

		void get_totales()
		{
			decimal subtotal = 0;
			decimal importe_iva = 0;
			decimal importe_ieps = 0;
			decimal total = 0;

			foreach(DTO_Detallado_formulas item in data)
			{
				subtotal += item.subtotal;
				total += item.total;
			}

			importe_iva = Convert.ToDecimal(subtotal * Convert.ToDecimal(0.16));

			txt_iva.Text = importe_iva.ToString("C2");
			txt_ieps.Text = importe_ieps.ToString("C2");
			txt_total.Text = total.ToString("C2");
			txt_subtotal.Text = subtotal.ToString("C2");
		}

		private void dgv_detallado_formulas_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 27:
					txt_amecop.Focus();
					dgv_detallado_formulas.ClearSelection();
				break;
			}
		}

		private void dgv_detallado_formulas_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			
		}

		private void txt_amecop_Leave(object sender, EventArgs e)
		{
			dgv_detallado_formulas.ClearSelection();
		}

		private void dgv_detallado_formulas_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
		{
			txt_amecop.Focus();
			dgv_detallado_formulas.ClearSelection();
			get_totales();
		}

		private void btn_imprimir_Click(object sender, EventArgs e)
		{
			if(txt_cliente.Text.Trim().Length > 0)
			{
				if(dgv_detallado_formulas.Rows.Count > 0)
				{
					bool existe_mano_obra = false;

					foreach(var item in data)
					{
						if(item.materia_prima_id == Convert.ToInt64(Config_helper.get_config_global("mano_obra_id")))
						{
							existe_mano_obra = true;
						}
					}

					if(existe_mano_obra)
					{
						imprimir_cotizacion();	
					}
					else
					{
						MessageBox.Show(this, "Indique el costo de mano de obra", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						txt_amecop.Focus();
					}
				}
				else
				{
					MessageBox.Show(this,"No puedes imprimir una cotización vacia","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					txt_amecop.Focus();
				}
			}
			else
			{
				MessageBox.Show(this,"Es necesario ingresar el nombre del cliente","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				txt_cliente.Focus();
			}
		}

		void imprimir_cotizacion()
		{
			Cotizar_formula ticket = new Cotizar_formula();

			List<DTO_Detallado_formulas> detallado = new List<DTO_Detallado_formulas>();

			foreach (DTO_Detallado_formulas item in data)
			{
				detallado.Add(item);
			}

			cotizacion_impresa = true;

			ticket.construccion_ticket(txt_cliente.Text, txt_doctor.Text, detallado,txt_instrucciones.Text);
			ticket.print();
			btn_terminar.Enabled = true;	
		}

		private void btn_limpiar_Click(object sender, EventArgs e)
		{
			cotizacion_impresa = false;
			btn_terminar.Enabled = false;
			limpiar_informacion_articulo();
			limpiar_informacion_materia();

			data.Clear();
			txt_cliente.Text = "";
			txt_doctor.Text = "";

			txt_instrucciones.Text = "";

			get_totales();
			txt_amecop.Focus();
		}

		private void chb_mano_obra_CheckedChanged(object sender, EventArgs e)
		{	
			
		}

		private void txt_mano_obra_KeyPress(object sender, KeyPressEventArgs e)
		{
			//e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
		}

		private void txt_amecop_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
		}

		private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
		{
			//e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
		}

		private void txt_mano_obra_Leave(object sender, EventArgs e)
		{
			get_totales();
		}

		private void txt_mano_obra_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					get_totales();
					btn_imprimir.Focus();
				break;
			}
		}

		private void txt_cliente_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Char.ToUpper(e.KeyChar);
		}

		private void txt_doctor_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Char.ToUpper(e.KeyChar);
		}

		private void txt_instrucciones_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Char.ToUpper(e.KeyChar);
		}

		private void btn_terminar_Click(object sender, EventArgs e)
		{
			if (txt_cliente.Text.Trim().Length > 0)
			{
				if (dgv_detallado_formulas.Rows.Count > 0)
				{
					bool existe_mano_obra = false;

					foreach (var item in data)
					{
						if (item.materia_prima_id == Convert.ToInt64(Config_helper.get_config_global("mano_obra_id")))
						{
							existe_mano_obra = true;
						}
					}

					if (existe_mano_obra)
					{
						terminar_cotizacion();
					}
					else
					{
						MessageBox.Show(this, "Indique el costo de mano de obra", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						txt_amecop.Focus();
					}
				}
				else
				{
					MessageBox.Show(this, "No puedes terminar una cotización vacia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txt_amecop.Focus();
				}
			}
			else
			{
				MessageBox.Show(this, "Es necesario ingresar el nombre del cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				txt_cliente.Focus();
			}
		}

		void terminar_cotizacion()
		{
			DAO_Formulas formulas = new DAO_Formulas();

			List<DTO_Detallado_formulas> detallado = new List<DTO_Detallado_formulas>();

			foreach(DTO_Detallado_formulas item in data)
			{
				detallado.Add(item);
			}
            string sucursalNombreSeleccionada = comboBox1.SelectedItem? .ToString();

            if (!sucursales_info.TryGetValue(sucursalNombreSeleccionada, out int sucursalSeleccionadaId))
            {
                MessageBox.Show(this, "No se pudo obtener el ID de la sucursal seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Login_form login = new Login_form();
			login.ShowDialog();

			if(login.empleado_id != null)
			{
                
                var validacion = formulas.registrar_formula((long)login.empleado_id,txt_cliente.Text, txt_doctor.Text, detallado, txt_instrucciones.Text, sucursalSeleccionadaId);

				if (validacion.status)
				{
					MessageBox.Show(this, "Formula registrada correctamenta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
					Cotizar_formula ticket = new Cotizar_formula();

                    ticket.construccion_ticket(validacion.elemento_nombre, sucursalSeleccionadaId);

                    ticket.print();

					this.Close();
				}
				else
				{
					MessageBox.Show(this, "Ocurrio un error al intentar registrar la formula, intentalo mas tarde", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}	
			}
		}
		//Probando

		private void Cotizar_formula_principal_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 77:
					if(e.Control)
					{
						DAO_Materias_primas dao_materias = new DAO_Materias_primas();
						var materia = dao_materias.get_materia_prima(Convert.ToInt64(Config_helper.get_config_global("mano_obra_id")));
						materia_prima = materia;
						rellenar_informacion_materia(materia);
					}
				break;	
			}
		}

		private void txt_comentario_producto_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					txt_cantidad.Enabled = true;
					txt_cantidad.Value = 1;
					txt_cantidad.Text = "1";
					txt_cantidad.Select(0,txt_cantidad.Text.Length);
					txt_cantidad.Focus();
					txt_comentario_producto.Enabled = false;
				break;
				case 27:
					if(txt_comentario_producto.Text.Trim().Length > 0)
					{
						txt_comentario_producto.Text = "";
					}
					else
					{
						limpiar_informacion_articulo();
					}
				break;
			}
		}

		private void txt_comentario_producto_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Char.ToUpper(e.KeyChar);
		}
        public int? sucursal_id = null;
        DAO_Sucursales dao_sucursales = new DAO_Sucursales();
        Dictionary<string, int> sucursales_info = new Dictionary<string, int>();
        public void get_sucursales()
        {
            try
            {
                var sucursales = dao_sucursales.get_all_sucursales();

                int sucursal_id = Convert.ToInt32(Config_helper.get_config_local("sucursal_id"));

                foreach (DataRow row in sucursales.Rows)
                {
                    string nombreSucursal = row["nombre"].ToString().ToUpper();
                    int idSucursal = Convert.ToInt32(row["sucursal_id"]);

                    if (
                        idSucursal != sucursal_id &&
                        nombreSucursal != "ALMACEN PRINCIPAL" &&
                        nombreSucursal != "AUDITORIA" &&
                        nombreSucursal != "ALMACEN QUIMICOS"
                    )
                    {
                        if (!sucursales_info.ContainsKey(nombreSucursal))
                        {
                            sucursales_info.Add(row["nombre"].ToString(), idSucursal);
                            comboBox1.Items.Add(row["nombre"].ToString());
                        }
                    }
                }


                if (comboBox1.Items.Count > 0)
                {
                    comboBox1.SelectedIndex = 0;
                    //comboBox1.DroppedDown = true;
                    comboBox1.Focus();
                }
                else
                {
                    MessageBox.Show(this, "No se encontro ninguna sucursal destino, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception) { }
        }
    }
}
