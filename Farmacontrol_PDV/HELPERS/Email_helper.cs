using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using Farmacontrol_PDV.DTO;
using System.Net;
using Farmacontrol_PDV.DAO;
using System.Windows.Forms;

namespace Farmacontrol_PDV.HELPERS
{
	class Email_helper
	{
		public static bool Envio_email(string destinatarios, string tipo_reporte, string email_origen = "", string password="", string cuerpo_mensaje = "", System.IO.Stream archivo_adjunto = null, string nombre_archivo = "")
		{
			try
			{
				DAO_Sucursales dao_sucursal = new DAO_Sucursales();
				var sucursal_data = dao_sucursal.get_sucursal_data(Convert.ToInt32(Config_helper.get_config_local("sucursal_id")));

				if(email_origen.Equals("") || password.Equals(""))
				{
					email_origen = sucursal_data.email;
					password = sucursal_data.email_password;
				}

				MailMessage mail = new MailMessage(email_origen, destinatarios);
                mail.From = new MailAddress(sucursal_data.email,sucursal_data.email_nombre);

				mail.Subject = tipo_reporte;

                StringBuilder pie_correo = new StringBuilder();
                pie_correo.AppendLine("\n\n");
                pie_correo.AppendLine(string.Format("{0} ({1})", sucursal_data.razon_social, sucursal_data.nombre));
                pie_correo.AppendLine(string.Format("{0}, {1}", sucursal_data.direccion, sucursal_data.colonia));
                pie_correo.AppendLine(string.Format("{0}, {1}, {2}", sucursal_data.ciudad, sucursal_data.estado, sucursal_data.codigo_postal));
                pie_correo.AppendLine(string.Format("{0}", sucursal_data.telefono));
                pie_correo.AppendLine(string.Format("{0}", sucursal_data.email));

                mail.Body = cuerpo_mensaje + pie_correo.ToString();
                

				mail.ReplyToList.Add(sucursal_data.email);
				
				SmtpClient client = new SmtpClient();
                client.Host = "smtp.server297.com";
				client.Timeout = 5000;
				client.Port = 587;
				client.UseDefaultCredentials = false;
				client.DeliveryMethod = SmtpDeliveryMethod.Network;
				client.Credentials = new NetworkCredential(email_origen, password);

                if(archivo_adjunto != null)
                {
                    mail.Attachments.Add(new Attachment(archivo_adjunto, nombre_archivo));
                }


				client.SendAsync(mail,true);

				return true;
			}
			catch(Exception ex)
			{
                ex.ToString();
				return false;
			}
		}
	}
}
