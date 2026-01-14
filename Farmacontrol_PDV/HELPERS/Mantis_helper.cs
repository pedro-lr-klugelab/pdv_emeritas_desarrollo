using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Windows.Forms;
using System.ServiceModel.Description;
using Farmacontrol_PDV.Mantis_connect;

namespace Farmacontrol_PDV.HELPERS
{
	class Mantis_helper
	{
		public static void reportar_incidencia(Exception exception)
		{
			reportar_incidencia(exception, "", "");
		}

		public static void reportar_incidencia(Exception exception, string informacion_adicional = "")
		{
			reportar_incidencia(exception, informacion_adicional, "");
		}

		public static void reportar_incidencia(Exception exception, string informacion_adicional = "", string origen = "")
		{
			try
			{
				string mantis_project = "Farmacontrol PDV (Terminal)";
				string mantis_username = "farmabot";
				string mantis_password = "farmabot";
				string mantis_handler = "mijaelhernandez";

				string mantis_categoria = "Reporte Automatizado";


				MantisConnectPortTypeClient cliente_mantis = new MantisConnectPortTypeClient();

				IssueData incidente = new IssueData();

				incidente.project = new ObjectRef() { name = mantis_project };
				incidente.category = mantis_categoria;
				incidente.severity = new ObjectRef() { id = "70" };
				incidente.handler = new AccountData() { name = mantis_handler };
				incidente.summary = exception.Message;

				StringBuilder description_builder = new StringBuilder();

				if(origen != String.Empty)
				{
					description_builder.AppendLine(String.Format("Origen de la falla: {0}", origen));
				}

				description_builder.AppendLine(String.Format("Mensaje de Excepcion: {0}", exception.Message.ToString()));
				description_builder.AppendLine();
				description_builder.AppendLine(String.Format("Stack Trace: {0}", exception.StackTrace.ToString()));
				description_builder.AppendLine();
				description_builder.AppendLine(String.Format("Excepcion: {0}", exception.ToString()));

				incidente.description = description_builder.ToString();

				if(informacion_adicional != String.Empty)
				{
					incidente.additional_information = informacion_adicional;
				}

				cliente_mantis.mc_issue_add(mantis_username, mantis_password, incidente);
			}
			catch(Exception mantis_error)
			{
				MessageBox.Show(mantis_error.Message.ToString(), "Error en Mantis", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
