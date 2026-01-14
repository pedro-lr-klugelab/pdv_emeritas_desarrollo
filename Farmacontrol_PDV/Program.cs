using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Farmacontrol_PDV.FORMS.comunes;
using System.Net;
using System.Threading;
using System.Drawing;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
			try
			{
				var terminal_id = Misc_helper.get_terminal_id();

				if(terminal_id == null)
				{
					//MessageBox.Show("Acceso DENEGADO, terminal NO REGISTRADA");
					Registro_terminal registro = new Registro_terminal();
					registro.ShowDialog();

					if(registro.reiniciar)
					{
						Application.Restart();
					}

					return;
				}

				//WebRequest.DefaultWebProxy = new WebProxy();
                AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);


				bool isNew = false;
				Mutex mtx = new Mutex(true, "Farmacontrol_PDV", out isNew);
				if (!isNew)
				{
					return;
				}

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new Principal());
			}
		catch(Exception exception)
			{
				Log_error.log(exception);
			}
        }

        static void CurrentDomain_UnhandledException (object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;

                Log_error.log(ex);

                MessageBox.Show("Ops! Por favor reporte el fallo a su desarrollador"
                        + " Información:\n\n" + ex.Message + ex.StackTrace,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Application.Exit();
            }
        }
    }
}