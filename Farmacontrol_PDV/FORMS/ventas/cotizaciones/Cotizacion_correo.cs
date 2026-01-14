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
using System.Text.RegularExpressions;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.FORMS.ventas.cotizaciones
{
	public partial class Cotizacion_correo : Form
	{
		long cotizacion_id;
		DTO_Cliente_correo cliente_seleccionado = new DTO_Cliente_correo();
		public bool envio_correo = false;
		public string nombre_cliente;
		public string direccion_cliente;
		public string colonia_cliente;
		public string ciudad_cliente;
		public string estado_cliente;
		public string destinatarios;
        public string mensaje_personalizado;

		DAO_Clientes dao_clientes = new DAO_Clientes();

		public Cotizacion_correo(long cotizacion_id)
		{
			this.cotizacion_id = cotizacion_id;
			InitializeComponent();
			tbp_direccion_cliente.Parent = null;
			tbp_correos.Parent = null;
            txt_mensaje_personalizado.Text = Config_helper.get_config_global("email_default_cuerpo_cotizacion");
		}

		private void txt_busqueda_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					dgv_clientes.DataSource = dao_clientes.busqueda_clientes_correo(txt_busqueda.Text);
					dgv_clientes.ClearSelection();
				break;
				case 27:
					txt_busqueda.Text = "";
				break;
				case 40:
					if(dgv_clientes.Rows.Count > 0)
					{
						dgv_clientes.CurrentCell = dgv_clientes.Rows[0].Cells["tipo_cliente"];
						dgv_clientes.Rows[0].Selected = true;
						dgv_clientes.Focus();
					}
				break;
			}
		}

		private void Cotizacion_correo_Shown(object sender, EventArgs e)
		{
			txt_busqueda.Focus();
		}

		private void btn_tbp_busqueda_siguiente_Click(object sender, EventArgs e)
		{
			btn_siguiente_busqueda();
		}

		private void btn_siguiente_busqueda()
		{
			if (dgv_clientes.SelectedRows.Count > 0)
			{
				var row = dgv_clientes.SelectedRows[0].Cells;
				cliente_seleccionado.elemento_id = row["elemento_id"].Value.ToString();
				cliente_seleccionado.nombre = row["nombre"].Value.ToString();
				cliente_seleccionado.tipo = row["tipo_cliente"].Value.ToString();

				if (cliente_seleccionado.tipo.Equals("PERSONA"))
				{
					txt_tbp_direccion_cliente_nombre_cliente.Text = cliente_seleccionado.nombre;
					dgv_direcciones.DataSource = dao_clientes.get_cliente_domicilio_data(cliente_seleccionado.elemento_id);
					tbp_direccion_cliente.Parent = tabControl1;
					dgv_direcciones.ClearSelection();
					tbp_busqueda.Parent = null;
				}
				else
				{
					var result = dao_clientes.get_cliente_correo_informacion_direccion(cliente_seleccionado.elemento_id, cliente_seleccionado.tipo);

					txt_tbp_correo_nombre_cliente.Text = cliente_seleccionado.nombre;
					txt_direccion.Text = string.Format("{0}, {1}, {2}, {3}", result.direccion, result.colonia, result.ciudad, result.estado);
					tbp_correos.Parent = tabControl1;
					tbp_busqueda.Parent = null;
					txt_correo.Focus();
				}
			}
			else
			{
				MessageBox.Show(this, "Es necesario seleccionar un cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btn_tbp_direccion_cliente_atras_Click(object sender, EventArgs e)
		{
			tbp_busqueda.Parent = tabControl1;
			txt_busqueda.Focus();
			tbp_direccion_cliente.Parent = null;
		}

		private void btn_tbp_direccion_cliente_siguiente_Click(object sender, EventArgs e)
		{
			if(dgv_direcciones.SelectedRows.Count > 0)
			{
				tbp_correos.Parent = tabControl1;
				txt_correo.Focus();

				txt_tbp_correo_nombre_cliente.Text = cliente_seleccionado.nombre;
				txt_direccion.Text = dgv_direcciones.SelectedRows[0].Cells["direccion"].Value.ToString();

				tbp_direccion_cliente.Parent = null;
			}
			else
			{
				MessageBox.Show(this,"Es necesario asignar una dirección","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		private void btn_tbp_correos_atras_Click(object sender, EventArgs e)
		{
			if(cliente_seleccionado.tipo.Equals("PERSONA"))
			{
				tbp_direccion_cliente.Parent = tabControl1;	
			}
			else
			{
				tbp_busqueda.Parent = tabControl1;
			}

			tbp_correos.Parent = null;
		}

		private void txt_correo_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					//agregar_correo();		
				break;
			}
		}

		public void agregar_correo()
		{
            /*
			if (txt_correo.Text.Trim().Length > 0)
			{
				if (validar_email(txt_correo.Text))
				{
					if(!ltb_correos.Items.Contains(txt_correo.Text))
					{
						ltb_correos.Items.Add(txt_correo.Text);	
					}

					txt_correo.Text = "";
					txt_correo.Focus();
				}
				else
				{
					MessageBox.Show(this, "El correo electronico no tiene un formato válido, verifique!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txt_correo.Focus();
				}
			}
			else
			{
				MessageBox.Show(this, "Es necesario escribir un correo para poder añadirlo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				txt_correo.Focus();
			}
             */
		}

		public Boolean validar_email(String email)
		{
			String expresion;
			expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
			if (Regex.IsMatch(email, expresion))
			{
				if (Regex.Replace(email, expresion, String.Empty).Length == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		private void ltb_correos_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 46:
                    /*
					if (ltb_correos.SelectedItems.Count > 0)
					{
						ltb_correos.Items.RemoveAt(ltb_correos.SelectedIndex);
						txt_correo.Focus();
					}
                     */
				break;
			}
		}

		private void btn_agregar_correos_Click(object sender, EventArgs e)
		{
			agregar_correo();
		}

		private void btn_enviar_correo_Click(object sender, EventArgs e)
		{
			if(txt_correo.Text.Trim().Length > 0)
			{
                string[] correos_split = txt_correo.Text.Trim('\r').Split('\n');
                string tmp;
                
                int i = 0;
                int j = correos_split.Length;
                string[] test = new string[j];

                bool email_incorrecto = false;

                if(correos_split.Length > 0)
                {
                    foreach(string correo in correos_split)
                    {
                        tmp = correo.TrimEnd('\r', '\n');
                        test[i] = tmp;
                        if(!validar_email(tmp))
                        {
                            MessageBox.Show(this,"Este email \""+correo+"\" no es valido verifique!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            email_incorrecto = true;
                        }
                        i++;
                    }
                }

                if(!email_incorrecto)
                {
                    nombre_cliente = txt_tbp_correo_nombre_cliente.Text;
                    mensaje_personalizado = txt_mensaje_personalizado.Text;

				    direccion_cliente = txt_direccion.Text;
				    destinatarios = String.Join(",",test);
				    envio_correo = true;
				    this.Close();	
                }
			}
			else
			{
				MessageBox.Show(this,"Es necesario asignar almenos un correo para poder enviar la cotización","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		private void dgv_clientes_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if (dgv_clientes.SelectedRows.Count > 0)
					{
						btn_siguiente_busqueda();
					}
				break;
			}
		}

		private void dgv_clientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if(dgv_clientes.SelectedRows.Count > 0)
			{
				btn_siguiente_busqueda();
			}
		}

        private void txt_correo_TextChanged(object sender, EventArgs e)
        {

        }

	}
}
