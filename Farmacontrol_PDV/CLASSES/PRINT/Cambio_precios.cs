using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
	class Cambio_precios
	{
		private long cambio_precio_id;
		private StringBuilder ticket = new StringBuilder();
        bool reimpresion = false;

		public void construccion_ticket(long cambio_precio_id, bool reimpresion = false)
		{
			this.cambio_precio_id = cambio_precio_id;
            this.reimpresion = reimpresion;

			DAO_Cambio_precios dao = new DAO_Cambio_precios();
			var detallado_cambio_precios = dao.get_detallado_cambio_precio(cambio_precio_id,true,true);

			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_48 + POS_Control.align_center);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_48][ALIGN_CENTER]");
			ticket.AppendLine("PRODUCTOS A");
			ticket.AppendLine("REETIQUETAR");
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
			ticket.AppendLine("--------------------------------------------------------");

			var cambio_precio_data = dao.get_cambio_precio_data(cambio_precio_id);

			ticket.AppendLine("FOLIO: "+cambio_precio_data.cambio_precio_id.ToString());
            ticket.AppendLine("FECHA: " + cambio_precio_data.fecha_creado.ToString("dd/MMM/yyy h:mm:ss").ToUpper().Replace(".", ""));
			ticket.AppendLine("MAYORISTA: "+cambio_precio_data.mayorista.ToUpper());
			ticket.AppendLine("--------------------------------------------------------");
			ticket.AppendLine("COD   PRODUCTO                   PUB. ANT.     PUB. NVO.");
			ticket.AppendLine("--------------------------------------------------------");

			foreach (var detallado in detallado_cambio_precios)
			{
				string amecop = detallado.amecop.ToString();

				ticket.AppendLine(string.Format("{0:5} {1:22} {2,13} {3,13}",
					"*"+amecop.Substring(amecop.Length - 4, 4),
					(detallado.producto.Length > 22) ? detallado.producto.Substring(0, 22) : detallado.producto.PadRight(22,' '),
					detallado.precio_publico_anterior.ToString("C2"),
					detallado.precio_publico_nuevo.ToString("C2"))
				);
			}

			ticket.AppendLine("--------------------------------------------------------");
            POS_Control.finTicket(ticket);
		}

		public bool print()
		{
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "CAMBIO_PRECIOS", cambio_precio_id,true);
		}

        
	}
}
