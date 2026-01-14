using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.DTO;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
	class Devoluciones_mayorista
	{
		private long devolucion_id;
		private StringBuilder ticket = new StringBuilder();
        bool reimpresion = false;

		public void construccion_ticket(long devolucion_id, bool reimpresion = false)
		{
			this.devolucion_id = devolucion_id;

			DAO_Devoluciones dao_devoluciones = new DAO_Devoluciones();
			var devolucion_ticket = dao_devoluciones.get_devolucion_ticket(devolucion_id);
			devolucion_ticket.detallado_devolucion_ticket = dao_devoluciones.get_detallado_devolucion_ticket(devolucion_id);

			//ticket.AppendLine(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine("");
			ticket.AppendLine("--------------------------------------------------------");
			//ticket.Append(POS_Control.font_size_64 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_64][ALIGN_CENTER]");
			ticket.AppendLine("DEVOLUCION");
			//ticket.Append(POS_Control.font_size_16 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
			ticket.AppendLine("DEVOLUCION A MAYORISTA");
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
			ticket.AppendLine("--------------------------------------------------------");

            if (reimpresion)
            {
                //ticket.Append(POS_Control.font_size_64 + POS_Control.align_center);
                ticket.Append("[FONT_SIZE_64][ALIGN_CENTER]");
                ticket.AppendLine("[REIMPRESION]");
                //ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
                ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
                ticket.AppendLine("--------------------------------------------------------");
            }

			DAO_Sucursales dao_sucursales = new DAO_Sucursales();
			string sucursal_id = HELPERS.Config_helper.get_config_local("sucursal_id");
			var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(sucursal_id));
			
			//ticket.Append(POS_Control.printer_reset + POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine(string.Format("{0}RFC:{1}\n{2}\n{3} COLONIA {4}\n{5} CP {6}, TEL: {7}", "", sucursal_data.rfc, sucursal_data.nombre, sucursal_data.direccion, sucursal_data.colonia, sucursal_data.ciudad, sucursal_data.codigo_postal, sucursal_data.telefono));

			ticket.AppendLine(string.Format("FOLIO: #{0}", devolucion_ticket.devolucion_id));
			ticket.AppendLine(string.Format("FOLIO ENTRADA: {0}", (devolucion_ticket.entrada_id == 0) ? "SIN ENTRADA ASOCIADA" : "#"+devolucion_ticket.entrada_id));

			ticket.AppendLine(string.Format("TERMINAL: {0}", Misc_helper.get_nombre_terminal((int)devolucion_ticket.terminal_id)));
			ticket.AppendLine(string.Format("CREADO POR: {0}", devolucion_ticket.nombre_empleado_captura));
			ticket.AppendLine(string.Format("TERMINADO POR: {0}", devolucion_ticket.nombre_empleado_termina));
            ticket.AppendLine(string.Format("FECHA CREADO: {0}", (devolucion_ticket.fecha_creado != null) ? Misc_helper.fecha(devolucion_ticket.fecha_creado.ToString(),"LEGIBLE") : " - "));
            ticket.AppendLine(string.Format("FECHA TERMINADO: {0}", (devolucion_ticket.fecha_terminado != null) ? Misc_helper.fecha(devolucion_ticket.fecha_terminado.ToString(),"LEGIBLE") : " - "));
			//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			ticket.AppendLine("COD   PRODUCTO                                  PRECIO C");
			ticket.AppendLine("CADUCIDAD                LOTE                       CANT");
			ticket.AppendLine("--------------------------------------------------------");

			long total_piezas = 0;

			foreach (DTO_Detallado_devolucion_mayorista_ticket detallado_ticket in devolucion_ticket.detallado_devolucion_ticket)
			{
				ticket.AppendLine(string.Format("{0,5} {1,-37}{2,13:C4}",
					detallado_ticket.amecop,
					detallado_ticket.nombre,
					detallado_ticket.precio_costo)
				);

				foreach (Tuple<string, string, int> cad_lotes in detallado_ticket.caducidades_lotes)
				{
					ticket.AppendLine(string.Format("{0,9} {1,-33}{2,13}",
						HELPERS.Misc_helper.fecha(cad_lotes.Item1,"CADUCIDAD"),
						HELPERS.Misc_helper.PadBoth(cad_lotes.Item2, 33),
						cad_lotes.Item3)
					);

					total_piezas += Convert.ToInt64(cad_lotes.Item3);
				}
			}

			if (total_piezas == 0)
			{
				//ticket.Append(POS_Control.align_center + POS_Control.font_size_16);
                ticket.Append("[ALIGN_CENTER][FONT_SIZE_16]");
				ticket.AppendLine("\nDEVOLUCION TERMINADA VACIA");
				//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
                ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
			}

			ticket.AppendLine("\n");
			ticket.AppendLine("                                           -------------");
			ticket.AppendLine(string.Format("{0,28} {1,27}", "TOTAL PIEZAS: ", total_piezas));
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			//ticket.AppendLine(POS_Control.align_center); ticket.AppendLine(POS_Control.align_center);
            POS_Control.finTicket(ticket);
		}

		public bool print()
		{
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "DEVOLUCIONES_MAYORISTA", devolucion_id, true, false,false,reimpresion);
		}

		public void Dispose()
		{

		}

        
	}
}
