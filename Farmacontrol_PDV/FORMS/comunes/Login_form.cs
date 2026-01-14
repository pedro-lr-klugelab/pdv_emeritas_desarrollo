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
    public partial class Login_form : Form
    {
		DAO_Login login = new DAO_Login();
		DTO_Validacion validacion = new DTO_Validacion();
		
		public int? empleado_id;
		public string empleado_nombre;
		public string usuario;
		public string password;
		
		private string funcion = "";
		private bool validar_funcion = false;


        public Login_form()
        {
            InitializeComponent();
			validar_funcion = false;
        }

		public Login_form(string funcion)
		{
			InitializeComponent();
			this.funcion = funcion;
			validar_funcion = true;
		}

		private void boton_aceptar_Click(object sender, EventArgs e)
		{
			validar_password();
		}

        private void boton_cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_aplicacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 27)
            {
                if(txt_password.TextLength == 0 && txt_usuario.TextLength == 0)
                {
                    this.Close();
                }
            }
        }

        private void txt_usuario_KeyDown(object sender, KeyEventArgs e)
        {
            txt_usuario.ContextMenu = new ContextMenu();
			try
			{

                if (e.Control == true)
                {
                    MessageBox.Show(this, "Es necesario escanear tu gafete");
                }



				var keycode = Convert.ToInt32(e.KeyCode);

				switch(keycode)
				{
					case 27:
						if (txt_usuario.Text.Trim().Length > 0)
						{
							txt_usuario.Text = "";
						}		
					break;
					case 13:
						if(txt_usuario.Text.Trim().Length > 0)
						{
							validacion = new DTO_Validacion();

							if(txt_usuario.Text.Trim().Length == 34)
							{

								if (txt_usuario.Text.Substring(0, 1).Equals("%") && txt_usuario.Text.Substring(txt_usuario.TextLength - 1).Equals("_"))
								{
									validacion = login.validar_fcid(txt_usuario.Text);

									if (validacion.status == true)
									{
										if (validar_funcion)
										{
											/*
											 *	VALIDAR FUNCION COMPLETA
											 * 
											 */
											validar_permisos_funcion((int)validacion.elemento_id, validacion.elemento_nombre);
											
										}
										else
										{
											empleado_id = validacion.elemento_id;
											empleado_nombre = validacion.elemento_nombre;
											this.Close();
										}
									}
									else
									{
										MessageBox.Show(this, "El FCID es inválido o se encuentra desactivado");
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
					break;
				}
			}
			catch(Exception ex)
			{
				Log_error.log(ex);
			}
        } 

        private void txt_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 27)
            {
                txt_usuario.Enabled = true;
                txt_password.Text = "";
                txt_password.Enabled = false;
                txt_usuario.Focus();
            }

            if (txt_password.Text.ToString().Equals(""))
            {
                boton_aceptar.Enabled = false;
            }
            else
            {
                boton_aceptar.Enabled = true;
            }

            if (Convert.ToInt32(e.KeyCode) == 13 && Convert.ToInt32(txt_password.TextLength) > 0)
            {
                validar_password();
            }   
        }

        public void validar_password()
        {
            if (Convert.ToInt32(txt_usuario.TextLength) > 0)
            {
                validacion = login.validar_usuario_password(txt_usuario.Text, txt_password.Text);

                if (validacion.status == true)
                {
					if(validar_funcion)
					{
						/*
						 *	VALIDAR FUNCION COMPLETA
						 * 
						 */
						validar_permisos_funcion((int)validacion.elemento_id, validacion.elemento_nombre); ;
					}
					else
					{
						empleado_id = validacion.elemento_id;
						empleado_nombre = validacion.elemento_nombre;
						usuario = txt_usuario.Text;
						password = txt_password.Text;
						this.Close();
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

		private void login_form_Shown(object sender, EventArgs e)
		{
			form_reset();
		}

		private void form_reset()
		{
			empleado_id = null;
			empleado_nombre = "";

			txt_usuario.Enabled = true;
			txt_password.Enabled = true;

			txt_usuario.Text = "";
			txt_password.Text = "";

			txt_usuario.Focus();
		}

		public void validar_permisos_funcion(int empleado_id, string nombre)
		{
			DTO_Validacion validacion = login.validar_funcion_empleado((int)empleado_id, funcion);

			if (validacion.status)
			{
				this.empleado_id = empleado_id;
				empleado_nombre = nombre;
				this.Close();
			}
			else
			{
				MessageBox.Show(this, validacion.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				form_reset();
			}
		}

        private void txt_usuario_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
