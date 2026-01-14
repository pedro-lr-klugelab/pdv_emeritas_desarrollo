using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.FORMS.catalogos.catalogo_clientes
{
	public partial class Registrar_domicilios : Form
	{
		private string cliente_id_hex = "";
        public string cliente_domicilio_id = "";
        public string tipo = "";

		public Registrar_domicilios(string cliente_id, string cliente_domicilio_id, string tipo)
		{
			this.cliente_id_hex = cliente_id;
            this.cliente_domicilio_id = cliente_domicilio_id;
            this.tipo = tipo;
			InitializeComponent();
		}

		private void txt_tipo_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 13:
					if (txt_tipo.TextLength > 0)
					{
						//txt_calle.Focus();
					}
					break;
				case 27:
					break;
			}
		}

		private void txt_direccion_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 13:
					if (txt_calle.TextLength > 0)
					{
						txt_numero_exterior.Focus();
					}
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

		private void btn_sin_cp_Click(object sender, EventArgs e)
		{
			Sin_codigo_postal sin_codigo_postal = new Sin_codigo_postal();
			sin_codigo_postal.ShowDialog();
		}

		private void btn_guardar_cliente_Click(object sender, EventArgs e)
		{
			if(validar_registro())
			{
				registrar_domicilio();
			}
		}

		bool validar_registro()
		{
            if (txt_tipo.Text.Trim().Length > 0)
            {
                if (txt_calle.Text.Trim().Length > 0)
                {
                    if (txt_colonia.Text.Trim().Length > 0)
                    {
                        if (txt_ciudad.Text.Trim().Length > 0)
                        {
                            if (mtb_telefono.Text.Trim().Length == 10)
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

		public void registrar_domicilio()
		{
			string telefono_original = mtb_telefono.Text.Replace("-","");
			string sin_parentesis = telefono_original.Replace("(","");
			string telefono = sin_parentesis.Replace(")","");
			
			Dictionary<string, string> datos_domicilio = new Dictionary<string, string>();
			datos_domicilio.Add("tipo", txt_tipo.Text);
			datos_domicilio.Add("calle",txt_calle.Text);
			datos_domicilio.Add("numero_exterior", txt_numero_exterior.Text);
			datos_domicilio.Add("numero_interior", txt_numero_interior.Text);
			datos_domicilio.Add("colonia", txt_colonia.Text);
			datos_domicilio.Add("ciudad", txt_ciudad.Text);
			datos_domicilio.Add("municipio", txt_municipio.Text);
			datos_domicilio.Add("estado", txt_estado.Text);
			datos_domicilio.Add("codigo_postal", txt_codigo_postal.Text);
			datos_domicilio.Add("telefono", telefono);
			datos_domicilio.Add("pais", txt_pais.Text);
			datos_domicilio.Add("comentarios", txt_comentarios.Text);

            if(!cliente_domicilio_id.Equals(""))
            {
                datos_domicilio.Add("cliente_domicilio_id",cliente_domicilio_id);
            }

			DAO_Clientes dao_clientes = new DAO_Clientes();
			DTO.DTO_Validacion validacion = dao_clientes.registrar_domicilio(cliente_id_hex,datos_domicilio);

			if (validacion.status)
			{
				MessageBox.Show(this, "Domicilio registrado correctamente", "Registro Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.Close();
			}
			else
			{
				MessageBox.Show(this, validacion.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
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

        private void Registrar_domicilios_Load(object sender, EventArgs e)
        {
            if (cliente_domicilio_id.Equals(""))
            {
                this.Text = "Registrar Domicilio";
                txt_tipo.Enabled = false;
                txt_calle.Enabled = false;
                txt_numero_exterior.Enabled = false;
                txt_numero_interior.Enabled = false;
                txt_colonia.Enabled = false;
                txt_ciudad.Enabled = false;
                txt_municipio.Enabled = false;
                txt_estado.Enabled = false;
                txt_codigo_postal.Enabled = false;
                txt_pais.Enabled = false;
                mtb_telefono.Text = "";
                txt_comentarios.Enabled = false;
            }
            else
            {
                this.Text = "Editar Domicilio";

                DAO_Clientes dao_clientes = new DAO_Clientes();
                var data = dao_clientes.get_domicilio_data_object(cliente_domicilio_id);
                txt_tipo.Text = data.etiqueta;
                txt_calle.Text = data.calle;
                txt_numero_exterior.Text = data.numero_exterior;
                txt_numero_interior.Text = data.numero_interior;
                txt_colonia.Text = data.colonia;
                txt_ciudad.Text = data.ciudad;
                txt_municipio.Text = data.municipio;
                txt_estado.Text = data.estado;
                txt_codigo_postal.Text = data.codigo_postal;
                txt_pais.Text = data.pais;
                mtb_telefono.Text = data.telefono.ToString();
                txt_comentarios.Text = data.comentarios;
                btn_guardar_cliente.Enabled = true;
            }
            
            
        }

        void validar_telefono_cliente()
        {
            if(mtb_telefono.Text.Trim().Length > 0)
            {
                DAO_Clientes dao_clientes = new DAO_Clientes();
                string cliente_id_hex_result = dao_clientes.get_cliente_id_by_telefono(Convert.ToInt64(mtb_telefono.Text.Trim()));

                if (!cliente_id_hex_result.Equals(""))
                {
                    var cliente_encontrado = dao_clientes.get_informacion_cliente(cliente_id_hex_result);

                    DialogResult dr = MessageBox.Show(this, string.Format("Este telefono se encuentra asignado al cliente {0}, desea proseguir con el alta?", cliente_encontrado.nombre.ToUpper()), "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        habilitar_campos(true);
                        txt_tipo.Focus();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    habilitar_campos(true);
                }
            }
        }

        void habilitar_campos(bool habilitar)
        {
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

            txt_tipo.Focus();
        }

        private void mtb_telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);   
        }

        private void mtb_telefono_Leave(object sender, EventArgs e)
        {
            mtb_telefono.Text = mtb_telefono.Text.Replace(" ", "");
            //validar_telefono_cliente();
        }

        private void mtb_telefono_KeyDown_1(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    if (mtb_telefono.Text.Trim().Length > 0)
                    {
                        validar_telefono_cliente();
                    }
                    break;
            }
        }
	}
}
