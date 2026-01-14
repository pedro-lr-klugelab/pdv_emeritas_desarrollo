using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.DTO;
using System.Data;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
	class Inventario
	{
		private long inventario_id;

		private StringBuilder ticket = new StringBuilder();
        bool reimpresion = false;

		public void construccion_ticket_no_inventariados(long inventario_id, bool reimpresion = false)
		{
			this.inventario_id = inventario_id;
            this.reimpresion = reimpresion;

			DAO_Inventarios dao_inventarios = new DAO_Inventarios();
			var inventario_ticket = dao_inventarios.get_informacion_inventario(inventario_id);
			var no_inventariados = dao_inventarios.get_detallado_no_inventariados(inventario_id);


			//ticket.AppendLine(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			//ticket.Append(POS_Control.font_size_64 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_64][ALIGN_CENTER]");
			ticket.AppendLine("INVENTARIO");
			//ticket.Append(POS_Control.font_size_16 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
			ticket.AppendLine("NO INVENTARIADOS");
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
			ticket.AppendLine("--------------------------------------------------------");

            if (reimpresion)
            {
                reimpresionTicket();
            }

			ticket.AppendLine(string.Format("FOLIO: #{0}", inventario_ticket.inventario_id));

			ticket.AppendLine(string.Format("TERMINAL: {0}", Misc_helper.get_nombre_terminal((int)inventario_ticket.terminal_id)));
			ticket.AppendLine(string.Format("CREADO POR: {0}", inventario_ticket.nombre_empleado_captura));
			ticket.AppendLine(string.Format("TERMINADO POR: {0}", inventario_ticket.nombre_empleado_termina));
            ticket.AppendLine(string.Format("FECHA INICIO: {0}", (inventario_ticket.fecha_inicio != null) ? Misc_helper.fecha(inventario_ticket.fecha_inicio.ToString(),"LEGIBLE") : " - "));
            ticket.AppendLine(string.Format("FECHA FIN: {0}", (inventario_ticket.fecha_fin != null) ? Misc_helper.fecha(inventario_ticket.fecha_fin.ToString(),"LEGIBLE") : " - "));
			//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			ticket.AppendLine("COD   PRODUCTO                                  PRECIO C");
			ticket.AppendLine("CADUCIDAD                LOTE                       CANT");
			ticket.AppendLine("--------------------------------------------------------");

			long total_piezas = 0;

			foreach (DTO_Detallado_no_inventariados row in no_inventariados)
			{
				ticket.AppendLine(string.Format("{0,5} {1,-37}{2,13:C4}",
					"*"+row.amecop,
					row.nombre,
					row.precio_costo.ToString("C2"))
				);

				foreach (Tuple<string, string, int> cad_lotes in row.caducidades_lotes)
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
				ticket.AppendLine("\nNO INVENTARIADOS VACIO");
				//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
                ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
			}

			ticket.AppendLine("\n");
			ticket.AppendLine("                                           -------------");
			ticket.AppendLine(string.Format("{0,28} {1,27}", "TOTAL PIEZAS: ", total_piezas));
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
            POS_Control.finTicket(ticket);
		}

		public void construccion_ticket_diferencias(long inventario_id, bool reimpresion = false)
		{
			this.inventario_id = inventario_id;

			DAO_Inventarios dao_inventarios = new DAO_Inventarios();
			var inventario_ticket = dao_inventarios.get_informacion_inventario(inventario_id);
			var dic_diferencias = dao_inventarios.get_detallado_diferencias_ticket(inventario_id);


			//ticket.AppendLine(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			//ticket.Append(POS_Control.font_size_64 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE][ALIGN_CENTER]");
			ticket.AppendLine("INVENTARIO");
			//ticket.Append(POS_Control.font_size_16 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
			ticket.AppendLine("DIFERENCIAS");
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
			ticket.AppendLine("--------------------------------------------------------");

			if (reimpresion)
			{
				//ticket.Append(POS_Control.font_size_64 + POS_Control.align_center); 
                ticket.Append("[FONT_SIZE_64][ALIGN_CENTER]");
                ticket.AppendLine("\n** COPIA **\n");
				//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
                ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
				ticket.AppendLine("--------------------------------------------------------");
			}

			DAO_Sucursales dao_sucursales = new DAO_Sucursales();
			string sucursal_id = HELPERS.Config_helper.get_config_local("sucursal_id");
			var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(sucursal_id));
			
			//ticket.Append(POS_Control.printer_reset + POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine(string.Format("{0} - RFC:{1}\n{2}\n{3} COLONIA {4}\n{5} CP {6}, TEL: {7}", sucursal_data.razon_social, sucursal_data.rfc, sucursal_data.nombre, sucursal_data.direccion, sucursal_data.colonia, sucursal_data.ciudad, sucursal_data.codigo_postal, sucursal_data.telefono));

			ticket.AppendLine(string.Format("FOLIO: #{0}", inventario_ticket.inventario_id));

			ticket.AppendLine(string.Format("TERMINAL: {0}", Misc_helper.get_nombre_terminal((int)inventario_ticket.terminal_id)));
			ticket.AppendLine(string.Format("CREADO POR: {0}", inventario_ticket.nombre_empleado_captura));
			ticket.AppendLine(string.Format("TERMINADO POR: {0}", inventario_ticket.nombre_empleado_termina));
            ticket.AppendLine(string.Format("FECHA INICIO: {0}", (inventario_ticket.fecha_inicio != null) ? Misc_helper.fecha(inventario_ticket.fecha_inicio.ToString(),"LEGIBLE") : " - "));
            ticket.AppendLine(string.Format("FECHA FIN: {0}", (inventario_ticket.fecha_fin != null) ? Misc_helper.fecha(inventario_ticket.fecha_fin.ToString(),"LEGIBLE") : " - "));
			//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");

			bool tiene_piezas_sobrante = false;
			bool tiene_piezas_faltante = false;

			var lista_sobrantes = dic_diferencias["sobrantes"];
			var lista_faltantes = dic_diferencias["faltantes"];
			decimal total_sobrantes = 0;
			decimal total_faltantes = 0;

			ticket.AppendLine("--------------------------------------------------------");
			//ticket.Append(POS_Control.font_size_16 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
			ticket.AppendLine("LISTA DE SOBRANTES");
			//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			ticket.AppendLine("COD   PRODUCTO                         DIF         MONTO");
			ticket.AppendLine("--------------------------------------------------------");

			foreach(DTO_Detallado_diferencias sobrante in lista_sobrantes)
			{
				ticket.AppendLine(string.Format("{0,5} {1,-30} {2,5} {3,13:C2}",
					"*" + sobrante.amecop,
					sobrante.nombre,
					sobrante.diferencia,
					sobrante.sobrante
					)
				);

				total_sobrantes += (decimal)sobrante.sobrante;

				tiene_piezas_sobrante = true;
			}

			if (!tiene_piezas_sobrante)
			{
				//ticket.Append(POS_Control.align_center + POS_Control.font_size_16);
                ticket.Append("[ALIGN_CENTER][FONT_SIZE_16]");
				ticket.AppendLine("\nLISTA DE SOBRANTES VACIA");
				//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
                ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
			}

			ticket.AppendLine("\n");
			ticket.AppendLine("                                           -------------");
			ticket.AppendLine(string.Format("{0,35} {1,20}", "TOTAL SOBRANTES: ", total_sobrantes.ToString("C2")));
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			ticket.AppendLine("\n");

			ticket.AppendLine("--------------------------------------------------------");
			//ticket.Append(POS_Control.font_size_16 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
			ticket.AppendLine("LISTA DE FALTANTES");
			//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			ticket.AppendLine("COD   PRODUCTO                         DIF         MONTO");
			ticket.AppendLine("--------------------------------------------------------");

			foreach (DTO_Detallado_diferencias sobrante in lista_faltantes)
			{
				ticket.AppendLine(string.Format("{0,5} {1,-27} {2,8} {3,13:C2}",
					"*" + sobrante.amecop,
					sobrante.nombre,
					sobrante.diferencia,
					sobrante.faltante
					)
				);

				total_faltantes = (decimal)sobrante.faltante;
				tiene_piezas_faltante = true;
			}

			if (!tiene_piezas_faltante)
			{
				//ticket.Append(POS_Control.align_center + POS_Control.font_size_16);
                ticket.Append("[ALIGN_CENTER][FONT_SIZE_16]");
				ticket.AppendLine("\nLISTA DE FALTANTES VACIA");
				//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
                ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
			}
			ticket.AppendLine("\n");
			ticket.AppendLine("                                           -------------");
			ticket.AppendLine(string.Format("{0,35} {1,20}", "TOTAL FALTANTES: ", total_faltantes.ToString("C2")));
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
            POS_Control.finTicket(ticket);
		}

		public bool print()
		{
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "INVENTARIO", inventario_id, true, false,false,reimpresion);
		}

		public void Dispose()
		{

		}

        
        public void reimpresionTicket()
        {
            //ticket.Append(POS_Control.font_size_64 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_64][ALIGN_CENTER]");
            ticket.AppendLine("[REIMPRESION]");
            //ticket.Append(POS_Control.align_left + POS_Control.font_size_0);}
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");
        }
	}
}
