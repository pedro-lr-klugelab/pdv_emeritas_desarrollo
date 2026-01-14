using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
	class Ticket_Ajustes_existencias
	{
		private long ajuste_existencia_id;
		private StringBuilder ticket = new StringBuilder();
        bool reimpresion = false;

		public void construccion_ticket(long ajuste_existencia_id, bool reimpresion = false)
		{
			this.ajuste_existencia_id = ajuste_existencia_id;
            this.reimpresion = reimpresion;

			DAO_Ajustes_existencias dao_ajustes_existencias = new DAO_Ajustes_existencias();
			var ajuste_ticket = dao_ajustes_existencias.get_informacion_ajuste_existencia(ajuste_existencia_id);
			var detallado_ajuste = dao_ajustes_existencias.get_detallado_ajuste_ticket(ajuste_existencia_id);

			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			//ticket.Append(POS_Control.font_size_16 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
			ticket.AppendLine("*AJUSTES DE EXISTENCIAS*");
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");

			ticket.AppendLine("--------------------------------------------------------");

            if(reimpresion)
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

			ticket.AppendLine(string.Format("FOLIO: #{0}", ajuste_ticket.ajuste_existencia_id));
			ticket.AppendLine(string.Format("TERMINAL: {0}", Misc_helper.get_nombre_terminal((int)ajuste_ticket.terminal_id)));
			ticket.AppendLine(string.Format("CREADO POR: {0}", ajuste_ticket.nombre_empleado_captura));
			ticket.AppendLine(string.Format("TERMINADO POR: {0}", ajuste_ticket.nombre_empleado_termina));
            ticket.AppendLine(string.Format("FECHA CREADO: {0}", (ajuste_ticket.fecha_creado != null) ? Misc_helper.fecha(ajuste_ticket.fecha_creado.ToString(),"LEGIBLE") : " - "));
            ticket.AppendLine(string.Format("FECHA TERMINADO: {0}", (ajuste_ticket.fecha_terminado != null) ? Misc_helper.fecha(ajuste_ticket.fecha_terminado.ToString(),"LEGIBLE") : " - "));

            if(!ajuste_ticket.comentarios.Trim().Equals(""))
            {
                ticket.AppendLine(string.Format("COMENTARIOS: {0}", ajuste_ticket.comentarios.ToUpper()));
            }

			//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			ticket.AppendLine("COD   PRODUCTO                                          ");
			ticket.AppendLine("CADUCIDAD            LOTE           EX-ANT   CANT    DIF");
			ticket.AppendLine("--------------------------------------------------------");

			long total_productos = 0;

			foreach (DTO_Detallado_ajuste_ticket detallado_ticket in detallado_ajuste)
			{
				ticket.AppendLine(string.Format("{0,5} {1,-50}",
					detallado_ticket.amecop,
					detallado_ticket.nombre)
				);

				foreach (Tuple<string, string, int, int, int> cad_lotes in detallado_ticket.caducidades_lotes)
				{
					ticket.AppendLine(string.Format("{0,9} {1,-25} {2,6} {3,6} {4,6}",
						HELPERS.Misc_helper.fecha(cad_lotes.Item1, "CADUCIDAD"),
						HELPERS.Misc_helper.PadBoth(cad_lotes.Item2, 25),
						cad_lotes.Item3,
						cad_lotes.Item4,
						cad_lotes.Item5
						)
					);

					total_productos += 1;
				}
			}

			if (total_productos == 0)
			{
				//ticket.Append(POS_Control.align_center + POS_Control.font_size_16);
                ticket.Append("[ALIGN_CENTER][FONT_SIZE_16]");
				ticket.AppendLine("\nAJUSTE TERMINADO VACIO");
				//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
                ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
			}

			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
            POS_Control.finTicket(ticket);
		}

		public bool print()
		{
            DAO_Ajustes_existencias dao_ajustes_existencias = new DAO_Ajustes_existencias();
            var ajuste_ticket = dao_ajustes_existencias.get_informacion_ajuste_existencia(ajuste_existencia_id);

            return HELPERS.Print_new_helper.print(ajuste_ticket.terminal_id, ticket.ToString(), "AJUSTE_EXISTENCIA", ajuste_existencia_id, true, false,false,reimpresion);
		}

		public void Dispose()
		{

		}

	}
}
