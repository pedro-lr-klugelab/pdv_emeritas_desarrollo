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

namespace Farmacontrol_PDV.FORMS.comunes
{
	public partial class Registro_clientes : Form
	{
		//private long? asentamiento_id = null;
        public string cliente_id= "";
        public string cliente_domicilio_id = "";
		public string cliente_registrado = "";
        DTO_Cliente cliente_encontrado = new DTO_Cliente();
        public bool usar_cliente_domicilio_id = false;
        public bool desde_busqueda = false;
        public int? empleado_id = 0;

		public Registro_clientes(bool desde_busqueda = false)
		{
            this.desde_busqueda = desde_busqueda;
			InitializeComponent();
		}

		private void btn_buscar_cp_Click(object sender, EventArgs e)
		{
			
		}

		private void button4_Click(object sender, EventArgs e)
		{
			Sin_codigo_postal sin_codigo_postal = new Sin_codigo_postal();
			sin_codigo_postal.ShowDialog();
		}

		private void txt_nombre_cliente_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
                case 9:
					if(txt_nombre_cliente.TextLength > 0)
					{
                        txt_Apellido.Focus();
						//txt_tipo.Focus();
					}
				break;
				case 27:
				break;
                
			}
		}

		private void txt_tipo_KeyDown(object sender, KeyEventArgs e)
		{
			
		}

		private void txt_direccion_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 13:
					
					break;
				case 27:
					break;
			}
		}

		private void mtb_telefono_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);
          
			switch (keycode)
			{
				case 13:
					if (mtb_telefono.TextLength > 0)
					{
						txt_comentarios.Focus();
					}
				break;
				case 27:
				break;
			}
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btn_guardar_cliente_Click(object sender, EventArgs e)
		{
			if(validar_registro())
			{
				registrar_cliente();	
			}
		}

		private void txt_comentarios_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 13:
					btn_guardar_cliente.Focus();
				break;
				case 27:
				break;
			}
		}

		public void registrar_cliente()
		{
            btn_guardar_cliente.Enabled = false;
            Cursor = Cursors.WaitCursor;

			string telefono_original = mtb_telefono.Text.Replace("-", "");
			string sin_parentesis = telefono_original.Replace("(", "");
			string telefono = sin_parentesis.Replace(")", "");

			Dictionary<string,string> datos_cliente = new Dictionary<string,string>();

            string n_clompleto = txt_nombre_cliente.Text + " " + txt_Apellido.Text;

            datos_cliente.Add("nombre", n_clompleto);
			datos_cliente.Add("tipo", txt_tipo.Text);
			datos_cliente.Add("calle", txt_calle.Text);
			datos_cliente.Add("numero_exterior", txt_numero_exterior.Text);
			datos_cliente.Add("numero_interior", txt_numero_interior.Text);
			datos_cliente.Add("colonia", txt_colonia.Text);
			datos_cliente.Add("ciudad", txt_ciudad.Text);
			datos_cliente.Add("municipio", txt_municipio.Text);
			datos_cliente.Add("estado", txt_estado.Text);
			datos_cliente.Add("codigo_postal", txt_codigo_postal.Text.PadLeft(5,'0'));
			datos_cliente.Add("telefono", telefono);
			datos_cliente.Add("pais", txt_pais.Text);
			datos_cliente.Add("comentarios", txt_comentarios.Text);
            datos_cliente.Add("empleado_id", empleado_id.ToString());

			DAO_Clientes dao_clientes = new DAO_Clientes();
			DTO.DTO_Validacion validacion = dao_clientes.registrar_cliente(datos_cliente);

			if(validacion.status)
			{
				cliente_registrado = txt_nombre_cliente.Text;

                if(desde_busqueda)
                {
                    cliente_domicilio_id = validacion.informacion;
                    usar_cliente_domicilio_id = true;
                }

				MessageBox.Show(this,"Cliente registrado correctamente","Registro Correcto",MessageBoxButtons.OK,MessageBoxIcon.Information);
				this.Close();
			}
			else
			{
				MessageBox.Show(this, validacion.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

            Cursor = Cursors.Default;
            btn_guardar_cliente.Enabled = true;
		}

		bool validar_registro()
		{
			if (txt_tipo.Text.Trim().Length > 0)
			{
                if(txt_calle.Text.Trim().Length > 0)
                {
                    if(txt_colonia.Text.Trim().Length > 0)
                    {
                        if(txt_ciudad.Text.Trim().Length > 0)
                        {
                            if(txt_nombre_cliente.Text.Trim().Length > 0 && txt_Apellido.Text.Trim().Length > 0 )
                            {
                                if(mtb_telefono.Text.Trim().Length == 10)
                                {
                                    return true;
                                }
                                else
                                {
                                    MessageBox.Show(this, "El campo TELEFONO debe ser de 10 digitos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    mtb_telefono.Focus();
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show(this, "Es necesario los campos NOMBRES Y APELLIDOS correctamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txt_nombre_cliente.Focus();
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show(this, "Es necesario el campo CIUDAD", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_ciudad.Focus();
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Es necesario el campo COLONIA", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_colonia.Focus();
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(this, "Es necesario el campo CALLE", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_calle.Focus();
                    return false;
                }
			}
            else
            {
                MessageBox.Show(this, "Es necesario el TIPO de domicilio Ej: CASA, NEGOCIO, OFICINA", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_tipo.Focus();
            }

			return false;
		}

		private void txt_tipo_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Char.ToUpper(e.KeyChar);
		}

		private void txt_calle_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Char.ToUpper(e.KeyChar);
		}

		private void txt_numero_exterior_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Char.ToUpper(e.KeyChar);
		}

		private void txt_numero_interior_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Char.ToUpper(e.KeyChar);
		}

		private void txt_colonia_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Char.ToUpper(e.KeyChar);
		}

		private void txt_ciudad_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Char.ToUpper(e.KeyChar);
		}

		private void txt_municipio_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Char.ToUpper(e.KeyChar);
		}

		private void txt_estado_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Char.ToUpper(e.KeyChar);
		}

		private void txt_codigo_postal_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
		}

		private void txt_comentarios_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Char.ToUpper(e.KeyChar);
		}

		private void txt_nombre_cliente_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Char.ToUpper(e.KeyChar);

            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space) && (e.KeyChar != (char)Keys.Enter))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }


		}

        private void txt_ciudad_Enter(object sender, EventArgs e)
        {
            txt_ciudad.SelectAll();
        }

        private void mtb_telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void mtb_telefono_Leave(object sender, EventArgs e)
        {
            mtb_telefono.Text = mtb_telefono.Text.Replace(" ","");
            /*
            if(mtb_telefono.Text.Trim().Length > 0)
            {
                validar_existe_cliente();
            }
            else
            {
                habilitar_campos(false);
            }*/
        }

        void validar_existe_cliente()
        {
            if (mtb_telefono.Text.Trim().Replace(" ","").Length == 10)
            {
                DAO_Clientes dao_clientes = new DAO_Clientes();

                cliente_id = dao_clientes.get_cliente_id_by_telefono(Convert.ToInt64(mtb_telefono.Text.Trim()));

                if (!cliente_id.Equals(""))
                {
                    cliente_encontrado = dao_clientes.get_informacion_cliente(cliente_id);

                    if (desde_busqueda)
                    {
                        DialogResult dr = MessageBox.Show(this, string.Format("Este telefono se encuentra asignado al cliente {0}, desea usar los datos de este cliente?", cliente_encontrado.nombre.ToUpper()), "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dr == DialogResult.Yes)
                        {
                            var domicilio = dao_clientes.get_domicilio_default(cliente_encontrado.cliente_id);
                            cliente_domicilio_id = domicilio.cliente_domicilio_id;

                            usar_cliente_domicilio_id = true;
                            this.Close();
                        }
                        else
                        {
                            if(pide_credenciales())
                            {
                                habilitar_campos(true);
                                txt_nombre_cliente.Focus();
                            }
                            else
                            {
                                MessageBox.Show(this, "Debe identificarse para poder continuar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                mtb_telefono.Focus();
                            }
                        }
                    }
                    else
                    {
                        DataTable data_dom = dao_clientes.get_cliente_domicilio_data(cliente_encontrado.cliente_id);
                         string direccion_cliente = "";
                        for (int i = 0; i < data_dom.Rows.Count; i++)
                        {
                            direccion_cliente = Convert.ToString(data_dom.Rows[i]["direccion"]);
                        
                        }

                        MessageBox.Show("Este telefono se encuentra asignado al cliente " + cliente_encontrado.nombre.ToUpper() + " con la siguiente direccion  " + direccion_cliente, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                      
                    }
                }
                else
                {
                    if(pide_credenciales())
                    {
                        habilitar_campos(true);
                    }
                    else
                    {
                        MessageBox.Show(this, "Debe identificarse para poder continuar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        mtb_telefono.Focus();
                    }
                }
            }
            else
            {
                MessageBox.Show(this,"La longitud del numero de telefono es de 10 digitos","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                mtb_telefono.Focus();
            }
        }

        void habilitar_campos(bool habilitar)
        {
            txt_nombre_cliente.Enabled = habilitar;
            txt_tipo.Enabled = habilitar;
            txt_calle.Enabled = habilitar;
            txt_numero_exterior.Enabled = habilitar;
            txt_numero_interior.Enabled = habilitar;
            txt_colonia.Enabled = habilitar;
            txt_codigo_postal.Enabled = habilitar;
            txt_ciudad.Enabled = habilitar;
            txt_municipio.Enabled = habilitar;
            txt_estado.Enabled = habilitar;
            txt_pais.Enabled = habilitar;
            txt_comentarios.Enabled = habilitar;
            btn_guardar_cliente.Enabled = habilitar;
            txt_Apellido.Enabled = habilitar;
            txt_nombre_cliente.Focus();

            
        }

        private bool pide_credenciales()
        {
            Login_form login = new Login_form();
            login.ShowDialog();

            if (login.empleado_id != null)
            {
                empleado_id = login.empleado_id;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void mtb_telefono_KeyDown_1(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch(keycode)
            {
                case 13:
                    if(mtb_telefono.Text.Trim().Length > 0)
                    {
                        validar_existe_cliente();
                    }
                break;
            }
        }

        private void txt_Apellido_Enter(object sender, EventArgs e)
        {

        }

        private void txt_Apellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);

            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space) && (e.KeyChar != (char)Keys.Enter))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }


        }

        private void txt_Apellido_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    txt_tipo.Focus();
                break;
                case 27:
                    break;
            }
        }
	}
}
