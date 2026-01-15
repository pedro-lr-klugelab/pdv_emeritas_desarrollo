using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;
using System.Threading;
using Farmacontrol_PDV.FORMS.opciones.configuracion;
using System.Diagnostics;
//using System.Deployment.Application;
using Farmacontrol_PDV.CLASSES;

namespace Farmacontrol_PDV.FORMS.comunes
{
    public partial class Principal : Form
    {
        public static int? empleado_id;
		Login_helper login_helper = new Login_helper();
		Thread servidor_impresion = null;

        public Principal()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
			DAO_Menus dao_menus = new DAO_Menus();
			List<DTO_Menu> lista_menus = new List<DTO_Menu>();

			lista_menus = dao_menus.get_menus();
			
			foreach(DTO_Menu menu in lista_menus)
			{
				ToolStripMenuItem menu_item = new ToolStripMenuItem();
				menu_item.Name = menu.Nombre;
				menu_item.Text = menu.Nombre;

				List<DTO_Submenu> lista_submenus = menu.Submenus;

				foreach(DTO_Submenu submenu in lista_submenus)
				{
					ToolStripMenuItem submenu_item = new ToolStripMenuItem();
					submenu_item.Name = submenu.Submenu_id.ToString();
					submenu_item.Text = submenu.Nombre.ToString();
					submenu_item.Click += new EventHandler(validar_permisos_submenu);

					menu_item.DropDownItems.Add(submenu_item);
				}

				menu_principal.Items.Add(menu_item);
			}

			ToolStripMenuItem opciones = new ToolStripMenuItem();		
			opciones.Name = "menu_opciones";
			opciones.Text = "Opciones";

			ToolStripMenuItem opcion_cambiar_usuario = new ToolStripMenuItem();
			opcion_cambiar_usuario.Name = "opcion_cambiar_usuario";
			opcion_cambiar_usuario.Text = "Cambiar Usuario";
			//opcion_cambiar_usuario.Click += new EventHandler(cambiar_usuario);

			ToolStripMenuItem opcion_cerrar_sesion = new ToolStripMenuItem();
			opcion_cerrar_sesion.Name = "opcion_cerrar_sesion";
			opcion_cerrar_sesion.Text = "Cerrar Sesión";
			//opcion_cerrar_sesion.Click += new EventHandler(cerrar_sesion);

			ToolStripMenuItem opcion_configuracion = new ToolStripMenuItem();
			opcion_configuracion.Name = "opcion_configuracion";
			opcion_configuracion.Text = "Configuración";
			//opcion_configuracion.Click += new EventHandler(configuracion);

			/*
			opciones.DropDownItems.Add(opcion_cambiar_usuario);
			opciones.DropDownItems.Add(opcion_cerrar_sesion);
			opciones.DropDownItems.Add(opcion_configuracion);
			 */

			//menu_principal.Items.Add(opciones);

			login_helper.pide_login();

			if (login_helper.empleado_id == null)
			{
				Environment.Exit(0);
			}
			else
			{
				empleado_id = login_helper.empleado_id;

				DAO_Empleados dao_empleados = new DAO_Empleados();
				var empleado_data = dao_empleados.get_empleado_data((int)empleado_id);

				Properties.Configuracion.Default.usuario = Crypto_helper.Encrypt(empleado_data.Usuario);
				Properties.Configuracion.Default.password = Crypto_helper.Encrypt(empleado_data.Password);

				DAO_Terminales dao_terminales = new DAO_Terminales();

				if(dao_terminales.get_terminal_permite_impresion_remota())
				{
					//imp = new CLASSES.ImpresionIP();
					servidor_impresion = new Thread(new ThreadStart(inicio_servidor_impresion));
					servidor_impresion.Start();
				}

				// Initialize Terminal Payment API in background
				Thread terminal_init = new Thread(new ThreadStart(inicializar_terminal_api));
				terminal_init.Start();

                string version = "";
                /*
                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                {
                    System.Deployment.Application.ApplicationDeployment cd =
                    System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
                    version = cd.CurrentVersion.ToString();
                    // show publish version in title or About box...
                }*/



				this.Text = String.Format("Farmacontrol PDV {0} - {1}",version, login_helper.empleado_nombre);
			}
        }

		public void inicio_servidor_impresion()
		{
            ImpresionIP.conectar();
		}

		/// <summary>
		/// Initialize the Terminal Payment API in background
		/// This allows card payments to be processed through the terminal
		/// </summary>
		public void inicializar_terminal_api()
		{
			try
			{
				// Check if API is already running
				if (Terminal_helper.CheckHealth())
				{
					// API is running, initialize SDK
					var result = Terminal_helper.Initialize();
					if (result.IsSuccessful)
					{
						Console.WriteLine("Terminal API initialized successfully");
					}
					else
					{
						Console.WriteLine($"Terminal API initialization warning: {result.DisplayMessage}");
					}
				}
				else
				{
					Console.WriteLine("Terminal API not running. Card payments will attempt to initialize on first use.");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Terminal API initialization error: {ex.Message}");
				// Don't show error to user - terminal payments will try to initialize when needed
			}
		}

		public void configuracion(object sender, EventArgs e)
		{
			Login_form login = new Login_form();
			login.ShowDialog();
			
			if(login.empleado_id != null)
			{
				Configuracion_principal config =  new Configuracion_principal();
				config.MdiParent = this;
				config.Show();
			}

		}

		public void cambiar_usuario()
		{
			login_helper.pide_login();

			if(login_helper.empleado_id != null)
			{
				empleado_id = login_helper.empleado_id;
				this.Text = String.Format("Farmacontrol PDV - {0}", login_helper.empleado_nombre);
			}

			foreach (Form form_busqueda in this.MdiChildren)
			{
				form_busqueda.Close();
			}
		}

		public void cerrar_sesion()
		{
			if (MessageBox.Show("Desea cerrar su sesión?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				foreach (Form form_busqueda in this.MdiChildren)
				{
					form_busqueda.Close();
				}

				empleado_id = null;

				this.Text = "Farmacontrol PDV";

				login_helper.pide_login();

				if (login_helper.empleado_id == null)
				{
					Environment.Exit(0);
				}
				else
				{
					empleado_id = login_helper.empleado_id;
					this.Text = String.Format("Farmacontrol PDV - {0}", login_helper.empleado_nombre);
				}
			}
		}

		public void validar_permisos_submenu(object sender, EventArgs e)
		{
			DAO_Login dao_login = new DAO_Login();
			ToolStripItem submenu = (ToolStripItem)sender;
			int permiso_id = int.Parse(submenu.Name.ToString());
			string controller = dao_login.get_controller(permiso_id);

			string [] split_controller = controller.Split(':');

			if (split_controller[0].Equals("funcion"))
			{
				Type thisType = this.GetType();
				MethodInfo theMethod = thisType.GetMethod(split_controller[1].ToString());
				theMethod.Invoke(this, null);
				return;
			}

			try
			{
				DAO_Login login = new DAO_Login();
				int? global_empleado_id = Principal.empleado_id;
				DTO_Validacion validacion = login.validar_permisos_ventana(global_empleado_id, permiso_id);

				if (validacion.status == true)
				{
					if(dao_login.modulo_usa_caja(permiso_id))
					{
						if(Misc_helper.soy_caja())
						{
							creacion_ventanas(controller, permiso_id, validacion.elemento_id);
						}
						else
						{
							MessageBox.Show(this,"Solo las terminales que son cajas tienen acceso a este modulo","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
						}
					}
					else
					{
						creacion_ventanas(controller, permiso_id, validacion.elemento_id);
					}
				}
				else
				{
					login_helper.pide_login();

					if(login_helper.empleado_id != null)
					{
						validacion = login.validar_permisos_ventana(login_helper.empleado_id, permiso_id);

						if(validacion.status == true)
						{
							creacion_ventanas(controller, permiso_id, login_helper.empleado_id);
						}
						else
						{
							MessageBox.Show(this,"No tienes permisos para abrir este módulo","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
							validar_permisos_submenu(sender, e);
						}
					}

				}
			}
			catch (Exception exepcion)
			{
				MessageBox.Show(this, "Error: "+exepcion.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void creacion_ventanas(string controller, int permiso_id, int? empleado_id_ventana)
		{
            try
            {
                Form formulario = (Form)Activator.CreateInstance(Type.GetType("Farmacontrol_PDV.FORMS." + controller));
                Boolean existe_form = false;

                foreach (Form form_busqueda in this.MdiChildren)
                {
                    if (form_busqueda.GetType() == formulario.GetType() && form_busqueda != formulario)
                    {
                        existe_form = true;
                        formulario = form_busqueda;
                    }
                }

                formulario.Tag = empleado_id_ventana;
                if (formulario.IsDisposed == false)
                {
                    if (existe_form == true)
                    {
                        formulario.Location = new Point(
                            (Screen.PrimaryScreen.WorkingArea.Width - formulario.Width) / 2,
                            (Screen.PrimaryScreen.WorkingArea.Height - formulario.Height) / 2
                        );

                        formulario.Focus();
                    }
                    else
                    {
                        formulario.MdiParent = this;
                        formulario.Show();
                    }
                }
            }
            catch (Exception exepcion){
                MessageBox.Show(this, "Error: "+exepcion.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            }
		}

		private void principal_FormClosing(object sender, FormClosingEventArgs e)
		{
			if(MessageBox.Show("Desea salir de la aplicación?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			{
				e.Cancel = true;
			}
            else
            {
                DAO_Terminales dao_ter = new DAO_Terminales();

                if (servidor_impresion != null)
                {
                    if (servidor_impresion.IsAlive)
                    {
                        try
                        {
                            ImpresionIP.cerrar_conexion = true;
                            ImpresionIP.client.Close();
                            ImpresionIP.server.Stop();

                            servidor_impresion = null;
                            
                            servidor_impresion.Interrupt();
                            servidor_impresion.Join();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }
            }
		}

        private void Principal_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch(keycode)
            {
                case 27:
                    if(e.Shift)
                    {
                        Log_principal log = new Log_principal();
                        log.ShowDialog();
                    }
                break;
            }
        }
    }
}