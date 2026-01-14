using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Farmacontrol_PDV.HELPERS;
using System.Net.Mail;
using System.Windows.Forms;

namespace Farmacontrol_PDV
{
	public static class Log_error
	{
		public static void log(Exception exepcion)		
		{
			try
			{
                Console.WriteLine(exepcion.ToString());

				string fecha = Convert.ToDateTime(Misc_helper.fecha()).ToShortDateString();
				fecha = fecha.Replace('/','_');

				string nombre_archivo = "log_"+fecha+".txt";

				FileStream fs = null;

				if (!File.Exists(nombre_archivo))
				{
					fs = File.Create(nombre_archivo);
					fs.Close();
				}

				using (StreamWriter w = File.AppendText(nombre_archivo))
				{
                    var stringBuilder = new StringBuilder();

					w.Write("\r\nLog Entry : ");
					w.WriteLine("{0} {1}", Convert.ToDateTime(Misc_helper.fecha()).ToLongTimeString(), Convert.ToDateTime(Misc_helper.fecha()).ToLongDateString());
					w.WriteLine("  :");
					w.WriteLine("  :{0}", FlattenException(exepcion));
					w.WriteLine("\r\n");
				}
			}
			catch(Exception exception)
			{
                Console.WriteLine(exception.ToString());
			}
		}

        public static string FlattenException(Exception exception)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Nombre terminal: "+Misc_helper.get_nombre_terminal());
            stringBuilder.AppendLine("Terminal id: " + Misc_helper.get_terminal_id());

            while (exception != null)
            {
                stringBuilder.AppendLine(exception.Message);
                stringBuilder.AppendLine(exception.StackTrace);

                exception = exception.InnerException;
            }

            string email = Config_helper.get_config_global("email_debug");
            //bool status = Email_helper.Envio_email(email, "Log_error", "", "", stringBuilder.ToString(), null, "");
            Console.WriteLine(stringBuilder.ToString());

            MessageBox.Show(stringBuilder.ToString());
            //MessageBox.Show("Envio de log al debug, Estatus: "+status);

            return stringBuilder.ToString();
        }
	}


   

}
