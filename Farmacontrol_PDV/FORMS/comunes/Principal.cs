using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
		
		// Terminal payment service processes
		private static Process proceso_simulador_java = null;
		private static Process proceso_terminal_api = null;
		
		// Timer for enabling menu after terminal initialization
		private System.Windows.Forms.Timer timer_habilitar_menu = null;
		private const int TIEMPO_ESPERA_TERMINAL_MS = 10000; // 10 seconds

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

				// Disable menu while terminal services initialize
				menu_principal.Enabled = false;
				this.Text = "Farmacontrol PDV - Inicializando terminal de pagos...";
				
				// Configure timer to enable menu after 10 seconds
				timer_habilitar_menu = new System.Windows.Forms.Timer();
				timer_habilitar_menu.Interval = TIEMPO_ESPERA_TERMINAL_MS;
				timer_habilitar_menu.Tick += new EventHandler(habilitar_menu_despues_de_espera);
				timer_habilitar_menu.Start();
				
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
				
				// Store employee name for later (when menu gets enabled)
				this.Tag = login_helper.empleado_nombre;
			}
        }
		
		/// <summary>
		/// Re-enable the menu after the terminal initialization wait period
		/// </summary>
		private void habilitar_menu_despues_de_espera(object sender, EventArgs e)
		{
			timer_habilitar_menu.Stop();
			timer_habilitar_menu.Dispose();
			timer_habilitar_menu = null;
			
			menu_principal.Enabled = true;
			
			string version = "";
			string empleado_nombre = this.Tag?.ToString() ?? "";
			this.Text = String.Format("Farmacontrol PDV {0} - {1}", version, empleado_nombre);
			this.Tag = null;
			
			System.Diagnostics.Debug.WriteLine("[Terminal] Menu enabled after initialization wait period");
		}

		public void inicio_servidor_impresion()
		{
            ImpresionIP.conectar();
		}

		/// <summary>
		/// Initialize the Terminal Payment API in background
		/// This starts the Java simulator and API server, then initializes the SDK
		/// </summary>
		public void inicializar_terminal_api()
		{
			try
			{
				// Get the base path for terminal services
				string basePath = AppDomain.CurrentDomain.BaseDirectory;
				
				// For development: Navigate from bin\Debug to project root, then to integrations
				// basePath = C:\...\Farmacontrol_PDV\bin\Debug\
				// We need: C:\...\integrations\terminal-api\
				string projectRoot = Path.GetFullPath(Path.Combine(basePath, "..", "..", ".."));
				string terminalApiPath = Path.Combine(projectRoot, "integrations", "terminal-api");
				
				string simulatorPath = Path.Combine(terminalApiPath, "simulador", "Servidor", "main", "webapp", "WEB-INF", "views");
				string apiPath = Path.Combine(terminalApiPath, "src", "TotalPosApi", "bin", "Debug", "net48");
				
				string simulatorJar = Path.Combine(simulatorPath, "Simulador-1.0.0-SNAPSHOT.jar");
				string apiExePath = Path.Combine(apiPath, "TotalPosApi.exe");
				
				// Log paths for debugging
				System.Diagnostics.Debug.WriteLine($"[Terminal] Base path: {basePath}");
				System.Diagnostics.Debug.WriteLine($"[Terminal] Project root: {projectRoot}");
				System.Diagnostics.Debug.WriteLine($"[Terminal] Simulator JAR: {simulatorJar} (exists: {File.Exists(simulatorJar)})");
				System.Diagnostics.Debug.WriteLine($"[Terminal] API EXE: {apiExePath} (exists: {File.Exists(apiExePath)})");
				
				// Check if API is already running
				if (!Terminal_helper.CheckHealth())
				{
					System.Diagnostics.Debug.WriteLine("[Terminal] API not running. Starting services...");
					
					// Start Java Simulator if not already running (silent mode)
					if (File.Exists(simulatorJar))
					{
						try
						{
							ProcessStartInfo simulatorStartInfo = new ProcessStartInfo
							{
								FileName = "java",
								Arguments = $"-jar \"{simulatorJar}\"",
								WorkingDirectory = simulatorPath,
								UseShellExecute = false,
								CreateNoWindow = true
							};
							proceso_simulador_java = Process.Start(simulatorStartInfo);
							System.Diagnostics.Debug.WriteLine("[Terminal] Java Simulator started");
							
							// Wait for simulator to initialize
							Thread.Sleep(5000);
						}
						catch (Exception ex)
						{
							System.Diagnostics.Debug.WriteLine($"[Terminal] Failed to start Java Simulator: {ex.Message}");
						}
					}
					else
					{
						System.Diagnostics.Debug.WriteLine($"[Terminal] Simulator JAR not found at: {simulatorJar}");
					}
					
					// Start TotalPos API if not already running (silent mode)
					if (File.Exists(apiExePath))
					{
						try
						{
							ProcessStartInfo apiStartInfo = new ProcessStartInfo
							{
								FileName = apiExePath,
								WorkingDirectory = apiPath,
								UseShellExecute = false,
								CreateNoWindow = true
							};
							proceso_terminal_api = Process.Start(apiStartInfo);
							System.Diagnostics.Debug.WriteLine("[Terminal] TotalPos API started");
							
							// Wait for API to initialize
							Thread.Sleep(3000);
						}
						catch (Exception ex)
						{
							System.Diagnostics.Debug.WriteLine($"[Terminal] Failed to start TotalPos API: {ex.Message}");
						}
					}
					else
					{
						System.Diagnostics.Debug.WriteLine($"[Terminal] TotalPosApi.exe not found at: {apiExePath}");
					}
				}
				else
				{
					System.Diagnostics.Debug.WriteLine("[Terminal] API already running");
				}
				
				// Now try to initialize the SDK
				if (Terminal_helper.CheckHealth())
				{
					var result = Terminal_helper.Initialize();
					if (result.IsSuccessful)
					{
						System.Diagnostics.Debug.WriteLine("[Terminal] API initialized successfully");
					}
					else
					{
						System.Diagnostics.Debug.WriteLine($"[Terminal] API initialization warning: {result.DisplayMessage}");
					}
				}
				else
				{
					System.Diagnostics.Debug.WriteLine("[Terminal] API still not available after starting services.");
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"[Terminal] Initialization error: {ex.Message}");
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
				
				// Stop terminal payment services
				detener_servicios_terminal();
            }
		}
		
		/// <summary>
		/// Stop terminal payment services when application closes
		/// </summary>
		private void detener_servicios_terminal()
		{
			try
			{
				// Stop TotalPos API
				if (proceso_terminal_api != null && !proceso_terminal_api.HasExited)
				{
					proceso_terminal_api.Kill();
					proceso_terminal_api.Dispose();
					proceso_terminal_api = null;
					Console.WriteLine("TotalPos API stopped");
				}
				
				// Stop Java Simulator
				if (proceso_simulador_java != null && !proceso_simulador_java.HasExited)
				{
					proceso_simulador_java.Kill();
					proceso_simulador_java.Dispose();
					proceso_simulador_java = null;
					Console.WriteLine("Java Simulator stopped");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error stopping terminal services: {ex.Message}");
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