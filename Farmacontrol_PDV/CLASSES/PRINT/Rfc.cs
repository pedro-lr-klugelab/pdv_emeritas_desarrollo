using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
	class Rfc
	{
		private string rfc_egistro_id;
		private StringBuilder ticket = new StringBuilder();

		public void construccion_ticket(string rfc_registro_id, bool reimpresion = false)
		{
			this.rfc_egistro_id = rfc_registro_id;

			DAO_Rfcs dao_rfc = new DAO_Rfcs();

			var rfc = dao_rfc.get_data_rfc(rfc_registro_id);

			ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
			ticket.AppendLine("--------------------------------------------------------");
			ticket.Append(POS_Control.font_condensed + POS_Control.font_size_32 + POS_Control.align_center);
			ticket.AppendLine("INFORMACION RFC");
			ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
			ticket.AppendLine("--------------------------------------------------------");
			ticket.AppendLine(@" RAZON SOCIAL: " + rfc.razon_social.ToUpper());
			ticket.AppendLine(@"          RFC: " + rfc.rfc.ToUpper());
			ticket.AppendLine(@"        CALLE: " + rfc.calle.ToUpper());
			ticket.AppendLine(@" NO. EXTERIOR: " + rfc.numero_exterior.ToUpper());
			ticket.AppendLine(@" NO. INTERIOR: " + rfc.numero_interior.ToUpper());
			ticket.AppendLine(@"CODIGO POSTAL: " + rfc.codigo_postal.ToString().ToUpper());
			ticket.AppendLine(@"      COLONIA: " + rfc.colonia.ToUpper());
			ticket.AppendLine(@"    MUNICIPIO: " + rfc.municipio.ToUpper());
			ticket.AppendLine(@"       CIUDAD: " + rfc.ciudad.ToUpper());
			ticket.AppendLine(@"       ESTADO: " + rfc.estado.ToUpper());
			ticket.AppendLine("--------------------------------------------------------");

			ticket.AppendLine(POS_Control.align_center); ticket.AppendLine(POS_Control.align_center);
			ticket.AppendLine(POS_Control.align_center); ticket.AppendLine(POS_Control.align_center);
			ticket.AppendLine(POS_Control.align_center); ticket.AppendLine(POS_Control.align_center);
			ticket.AppendLine(POS_Control.corte);
		}

		public bool print()
		{
			string impresora = Properties.Configuracion.Default.impresora_tickets;
			DAO_Impresiones dao_impresiones = new DAO_Impresiones();
			CLASSES.Print_raw printer = new CLASSES.Print_raw();

			printer.PrinterName = impresora;

			printer.Open("TICKET");
			bool result = printer.Print(ticket.ToString());

			printer.Close();
			return true;
		}

		public void Dispose()
		{

		}		
	}
}
