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
using System.Text.RegularExpressions;

namespace Farmacontrol_PDV.FORMS.catalogos.catalogo_rfc
{
	public partial class Editar_rfc : Form
	{
		DTO_Rfc dto_rfc = new DTO_Rfc();
		DAO_Rfcs dao_rfc = new DAO_Rfcs();
		public bool registro = false;
		public string razon_social = "";
		public string correos = "";

		public string rfc_registro_id = "";

		public Editar_rfc(string rfc_registro_id = "")
		{
			this.rfc_registro_id = rfc_registro_id;
			InitializeComponent();
		}

		private void Editar_rfc_Load(object sender, EventArgs e)
		{
			try
			{
				if (rfc_registro_id.Equals(""))
				{
					rellenar_informacion_nuevo_rfc();
				}
				else
				{
					rellenar_informacion_rfc();
				}	
			}
			catch(Exception ex)
			{
				Log_error.log(ex);
			}
		}

		public void rellenar_informacion_nuevo_rfc()
		{
			this.Text = "Registrar Nuevo RFC";
		}

		void limpiar_informacion()
		{
			txt_identificador_vista.Text = "";
			txt_calle.Text = "";
			txt_ciudad.Text = "";
			txt_codigo_postal.Text = "";
			txt_colonia.Text = "";
			txt_estado.Text = "";
			txt_municipio.Text = "";
			txt_numero_exterior.Text = "";
			txt_numero_interior.Text = "";
			txt_razon_social.Text = "";
			txt_rfc_vista.Text = "";

			txt_correo1.Text = "";
			txt_correo2.Text = "";
			txt_correo3.Text = "";
			txt_correo4.Text = "";
		}

		DTO_Validacion actualizar_rfc()
		{
			DAO_Rfcs dao_rfc = new DAO_Rfcs();
			DTO_Rfc dto_rfc = new DTO_Rfc();

			dto_rfc.rfc_registro_id = txt_identificador_vista.Text;
			dto_rfc.calle = txt_calle.Text;
			dto_rfc.ciudad = txt_ciudad.Text;
			dto_rfc.codigo_postal = txt_codigo_postal.Text;
			dto_rfc.colonia = txt_colonia.Text;
			dto_rfc.correos_electronicos = correos.Split(',').ToList();
			dto_rfc.estado = txt_estado.Text;
			dto_rfc.municipio = txt_municipio.Text;
			dto_rfc.numero_exterior = txt_numero_exterior.Text;
			dto_rfc.numero_interior = txt_numero_interior.Text;
			dto_rfc.pais = txt_pais.Text;
			dto_rfc.razon_social = txt_razon_social.Text;
			dto_rfc.rfc = txt_rfc_vista.Text;

			return dao_rfc.actualizar_rfc(dto_rfc);
		}

		DTO_Validacion registrar_rfc()
		{
			DAO_Rfcs dao_rfc = new DAO_Rfcs();
			DTO_Rfc dto_rfc = new DTO_Rfc();

			dto_rfc.rfc_registro_id = txt_identificador_vista.Text;
			dto_rfc.calle = txt_calle.Text;
			dto_rfc.ciudad = txt_ciudad.Text;
			dto_rfc.codigo_postal = (txt_codigo_postal.Text.Trim().Length > 0) ? txt_codigo_postal.Text.ToString() : "0";
			dto_rfc.colonia = txt_colonia.Text;
			dto_rfc.correos_electronicos = correos.Split(',').ToList();
			dto_rfc.estado = txt_estado.Text;
			dto_rfc.municipio = txt_municipio.Text;
			dto_rfc.numero_exterior = txt_numero_exterior.Text;
			dto_rfc.numero_interior = txt_numero_interior.Text;
			dto_rfc.pais = txt_pais.Text;
			dto_rfc.razon_social = txt_razon_social.Text;
			dto_rfc.rfc = txt_rfc_vista.Text;

			return dao_rfc.registrar_rfc(dto_rfc);
		}

		public void rellenar_informacion_rfc()
		{
			limpiar_informacion();

			DAO_Rfcs dao_rfc = new DAO_Rfcs();
			var rfc_registros = dao_rfc.get_data_rfc(rfc_registro_id);
			rfc_registro_id = rfc_registros.rfc_registro_id;

			int count = 1;

			foreach (string correo in rfc_registros.correos_electronicos)
			{

				switch (count)
				{
					case 1:
						txt_correo1.Text = correo.Trim();
						break;
					case 2:
						txt_correo2.Text = correo.Trim();
						break;
					case 3:
						txt_correo3.Text = correo.Trim();
						break;
					case 4:
						txt_correo3.Text = correo.Trim();
						break;
				}

				count++;
			}

			txt_estado.Text = rfc_registros.estado;
			txt_ciudad.Text = rfc_registros.ciudad;
			txt_municipio.Text = rfc_registros.municipio;
			txt_colonia.Text = rfc_registros.colonia;
			txt_codigo_postal.Text = rfc_registros.codigo_postal.ToString().PadLeft(5,'0');

			txt_identificador_vista.Text = rfc_registro_id.ToUpper();
			txt_rfc_vista.Text = rfc_registros.rfc;
			txt_razon_social.Text = rfc_registros.razon_social;
			txt_calle.Text = rfc_registros.calle;
			txt_numero_exterior.Text = rfc_registros.numero_exterior;
			txt_numero_interior.Text = rfc_registros.numero_interior;
		}

		private void btn_buscar_cp_Click(object sender, EventArgs e)
		{
			/*
			Busqueda_codigos_postales busqueda_codigos_postales = new Busqueda_codigos_postales();
			busqueda_codigos_postales.ShowDialog();
			if (busqueda_codigos_postales.asentamiento_id_g != null)
			{
				txt_codigo_postal.Text = busqueda_codigos_postales.codigo_postal_cp;
				txt_nombre_cp.Text = busqueda_codigos_postales.nombre_cp;
				txt_municipio_cp.Text = busqueda_codigos_postales.municipio_cp;
				txt_ciudad_cp.Text = busqueda_codigos_postales.ciudad_cp;
				txt_estado_cp.Text = busqueda_codigos_postales.estado_cp;
			}
			 * */
		}

		private void btn_sin_cp_Click(object sender, EventArgs e)
		{
			/*
			Sin_codigo_postal sin_codigo_postal = new Sin_codigo_postal();
			sin_codigo_postal.ShowDialog();
			if (sin_codigo_postal.asentamiento_id != null)
			{
				dto_rfc.asentamiento_id = sin_codigo_postal.asentamiento_id;
				var codigo_postal_data = dao_codigos_postales.get_codigo_postal_data((long)dto_rfc.asentamiento_id);

				txt_codigo_postal.Text = codigo_postal_data.Rows[0]["codigo_postal"].ToString();
				txt_nombre_cp.Text = codigo_postal_data.Rows[0]["nombre"].ToString();
				txt_municipio_cp.Text = codigo_postal_data.Rows[0]["municipio"].ToString();
				txt_ciudad_cp.Text = codigo_postal_data.Rows[0]["ciudad"].ToString();
				txt_estado_cp.Text = codigo_postal_data.Rows[0]["estado"].ToString();
			}
			 * */
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		void unir_correos()
		{
			correos = "";

			if (txt_correo1.Text.Trim().Length > 0)
			{
				correos += (correos.Equals("")) ? txt_correo1.Text.Trim() : ", " + txt_correo1.Text.Trim();
			}

			if (txt_correo2.Text.Trim().Length > 0)
			{
				correos += (correos.Equals("")) ? txt_correo2.Text.Trim() : ", " + txt_correo2.Text.Trim();
			}

			if (txt_correo3.Text.Trim().Length > 0)
			{
				correos += (correos.Equals("")) ? txt_correo3.Text.Trim() : ", " + txt_correo3.Text.Trim();
			}

			if (txt_correo4.Text.Trim().Length > 0)
			{
				correos += (correos.Equals("")) ? txt_correo4.Text.Trim() : ", " + txt_correo4.Text.Trim();
			}
		}

		private void btn_guardar_Click(object sender, EventArgs e)
		{
			unir_correos();

			if (txt_identificador_vista.Text.Trim().Length > 0 && txt_razon_social.Text.Trim().Length > 0 && txt_rfc_vista.Text.Trim().Length > 0 && txt_pais.Text.Trim().Length > 0)
			{
				var val = actualizar_rfc();

				if(val.status)
				{
					MessageBox.Show(this, "Información actualizada correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
				}
				else
				{
					MessageBox.Show(this,"Ocurrio un error al intentar actualizar la información, notifique a su administrador.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
			}
			else if (txt_identificador_vista.Text.Trim().Length == 0 && txt_razon_social.Text.Trim().Length > 0 && txt_rfc_vista.Text.Trim().Length > 0 && txt_pais.Text.Trim().Length > 0)
			{
				var result = registrar_rfc();

				if (result.status)
				{
					txt_identificador_vista.Text = result.elemento_nombre;
					rfc_registro_id = result.elemento_nombre;
					rellenar_informacion_rfc();
					MessageBox.Show(this, "Información actualizada correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
				}
				else
				{
					MessageBox.Show(this, "Ocurrio un error al intentar actualizar la información, notifique a su administrador.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				MessageBox.Show(this, "Los siguientes campos son obligatorios: RFC, RAZON SOCIAL y PAIS.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
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

		private void txt_rfc_vista_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Char.ToUpper(e.KeyChar);
		}

		private void txt_razon_social_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Char.ToUpper(e.KeyChar);
		}

		private void txt_rfc_vista_Leave(object sender, EventArgs e)
		{
			if (txt_rfc_vista.Text.Trim().Length > 0)
			{
				Regex regex = new Regex(@"([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])([A-Z0-9]){2}[0-9A]{1})");
				Match match = regex.Match(txt_rfc_vista.Text);

				if (match.Success)
				{
					DAO_Rfcs dao_rfc = new DAO_Rfcs();
					var existe_rfc = dao_rfc.existe_rfc(txt_rfc_vista.Text);

					if (existe_rfc.status)
					{
						var info_rfc = dao_rfc.get_data_rfc(existe_rfc.informacion);

						if (txt_identificador_vista.Text.Equals(""))
						{
							DialogResult dr = MessageBox.Show(this, string.Format("Este RFC pertenece a {0}, ¿Deseas usar su información?", info_rfc.razon_social), "RFC existente", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

							if (dr == DialogResult.Yes)
							{
								rfc_registro_id = existe_rfc.informacion;
								rellenar_informacion_rfc();
							}
							else
							{
								txt_rfc_vista.Text = "";
								txt_rfc_vista.Focus();
							}
						}
						else if (!txt_identificador_vista.Text.Equals(existe_rfc.informacion))
						{
							MessageBox.Show(this, string.Format("No se puede usar este RFC por que esta asignado a otro registro ({0})", info_rfc.razon_social), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							var rfc_anterior = dao_rfc.get_data_rfc(rfc_registro_id);
							txt_rfc_vista.Text = rfc_anterior.rfc;
							txt_rfc_vista.Focus();
						}
					}
				}
				else
				{
					MessageBox.Show(this, "El RFC ingresado tiene errores de formato, verifique.", "RFC Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txt_rfc_vista.Focus();
				}
			}
		}

		private void txt_correo1_Leave(object sender, EventArgs e)
		{
			if (txt_correo1.Text.Trim().Length > 0)
			{
				if (!validar_email(txt_correo1.Text))
				{
					MessageBox.Show(this, "La dirección de correo " + txt_correo1.Text + " es inválida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txt_correo1.Focus();
				}
			}
		}

		private void txt_correo2_Leave(object sender, EventArgs e)
		{
			if (txt_correo2.Text.Trim().Length > 0)
			{
				if (!validar_email(txt_correo2.Text))
				{
					MessageBox.Show(this, "La dirección de correo " + txt_correo2.Text + " es inválida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txt_correo2.Focus();
				}
			}
		}

		private void txt_correo3_Leave(object sender, EventArgs e)
		{
			if (txt_correo3.Text.Trim().Length > 0)
			{
				if (!validar_email(txt_correo3.Text))
				{
					MessageBox.Show(this, "La dirección de correo " + txt_correo3.Text + " es inválida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txt_correo3.Focus();
				}
			}
		}

		private void txt_correo4_Leave(object sender, EventArgs e)
		{
			if (txt_correo4.Text.Trim().Length > 0)
			{
				if (!validar_email(txt_correo4.Text))
				{
					MessageBox.Show(this, "La dirección de correo " + txt_correo4.Text + " es inválida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txt_correo4.Focus();
				}
			}
		}

		public Boolean validar_email(String email)
		{
			String expresion;
			//expresion = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
			//  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
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

		private void txt_correo1_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = (e.KeyChar.Equals(' '));
		}

		private void txt_correo2_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = (e.KeyChar.Equals(' '));
		}

		private void txt_correo3_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = (e.KeyChar.Equals(' '));
		}

		private void txt_correo4_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = (e.KeyChar.Equals(' '));
		}

        private void txt_codigo_postal_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
	}
}
