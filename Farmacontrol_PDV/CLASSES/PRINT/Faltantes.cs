using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
	class Faltantes
	{
		
		private StringBuilder ticket = new StringBuilder();
		private long reporte_faltantes_id;

		public void construccion_ticket(long sucursal_id, long reporte_faltantes_id)
		{
			DAO_Sucursales suc = new DAO_Sucursales();
			this.reporte_faltantes_id = reporte_faltantes_id;
			var sucursal_data = suc.get_sucursal_data((int)sucursal_id);
			var detallado_ticket = DAO_Faltantes.get_detallado_faltantes(sucursal_id, reporte_faltantes_id);	
			/*ticket.AppendLine(POS_Control.printer_reset + POS_Control.font_condensed + POS_Control.font_size_0);*/
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			/*ticket.Append(POS_Control.font_size_16 + POS_Control.align_center);*/
            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
			ticket.AppendLine("LISTA DE SURTIDO");
			ticket.AppendLine(sucursal_data.nombre.ToUpper());
			/*ticket.Append(POS_Control.align_left + POS_Control.font_size_0);*/
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			ticket.AppendLine("COD   PRODUCTO                      EX A    EX S     SUG");
			ticket.AppendLine("--------------------------------------------------------");

			foreach (DTO_Detallado_faltante det in detallado_ticket)
			{
				ticket.AppendLine(string.Format("{0,5} {1,26} {2,7} {3,7} {4,7}",
					"*"+det.amecop.Substring(det.amecop.Length -4),
					(det.producto.Length > 26) ? det.producto.Substring(0, 26) : det.producto,
					det.existencia_almacen,
					det.existencia_sucursal,
					det.sugerid_almacen)
				);
			}
			ticket.AppendLine("\n");
			ticket.AppendLine("--------------------------------------------------------");
            POS_Control.finTicket(ticket);
		}

		public bool print()
		{
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "FALTANTES", reporte_faltantes_id, true);
		}
	}
}
