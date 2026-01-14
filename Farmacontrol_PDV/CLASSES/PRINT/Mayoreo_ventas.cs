using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.DTO;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
	class Mayoreo_ventas : IDisposable
	{
		private long mayoreo_venta_id;
		private StringBuilder ticket = new StringBuilder();
		DAO_Ventas_mayoreo dao_mayoreo = new DAO_Ventas_mayoreo();
        bool reimpresion = false;

		public void construccion_ticket(long mayoreo_venta_id, bool reimpresion = false, bool es_revision = false)
		{
			this.mayoreo_venta_id = mayoreo_venta_id;
            this.reimpresion = reimpresion;

			var mayoreo_venta_data = dao_mayoreo.get_venta_mayoreo_data(mayoreo_venta_id);
			var detallado = dao_mayoreo.get_detallado_ticket_mayoreo_ventas(mayoreo_venta_id);
				
			//ticket.AppendLine(POS_Control.logo);
			//ticket.AppendLine(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			//ticket.Append(POS_Control.font_size_48 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_48][ALIGN_CENTER]");
			ticket.AppendLine("MAYOREO");
			ticket.AppendLine((es_revision) ? "*REVISION*" : "*CAPTURA*");
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
			ticket.AppendLine("--------------------------------------------------------");

			string estado = "";

			if (mayoreo_venta_data.fecha_terminado != null)
			{
				if (mayoreo_venta_data.fecha_inicio_verifiacion != null)
				{
					if (mayoreo_venta_data.fecha_fin_verificacion != null)
					{
						estado = "TERMINADO";
					}
					else
					{
						estado = "EN REVISION";
					}
				}
				else
				{
					estado = "REVISION PENDIENTE";
				}
			}
			else
			{
				estado = "CAPTURANDO";
			}

			//ticket.Append(POS_Control.font_size_32 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_32][ALIGN_CENTER]");
			ticket.AppendLine(estado);
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
			ticket.AppendLine("--------------------------------------------------------");
			//ticket.Append(POS_Control.font_size_16 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
			ticket.AppendLine("INFORMACION CAPTURA");
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
			ticket.AppendLine("--------------------------------------------------------");

			DAO_Sucursales dao_sucursales = new DAO_Sucursales();
			string sucursal_id = HELPERS.Config_helper.get_config_local("sucursal_id");
			var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(sucursal_id));
			
			//ticket.Append(POS_Control.printer_reset + POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine(string.Format("{0} - RFC:{1}\n{2}\n{3} COLONIA {4}\n{5} CP {6}, TEL: {7}", sucursal_data.razon_social, sucursal_data.rfc, sucursal_data.nombre, sucursal_data.direccion, sucursal_data.colonia, sucursal_data.ciudad, sucursal_data.codigo_postal, sucursal_data.telefono));

			ticket.AppendLine(string.Format("FOLIO: #{0}", mayoreo_venta_data.mayoreo_venta_id));
			ticket.AppendLine(string.Format("TERMINAL: {0}", (mayoreo_venta_data.terminal_id != null ) ? Misc_helper.get_nombre_terminal((int)mayoreo_venta_data.terminal_id) : "SIN TERMINAL"));
			ticket.AppendLine(string.Format("CAPTURA: {0}", mayoreo_venta_data.nombre_empleado_captura.ToUpper()));
			ticket.AppendLine(string.Format("TERMINA: {0}", mayoreo_venta_data.nombre_empleado_termina.ToUpper()));
            ticket.AppendLine(string.Format("FECHA CAPTURADO: {0}", (mayoreo_venta_data.fecha_creado != null) ? Misc_helper.fecha(mayoreo_venta_data.fecha_creado.ToString(),"LEGIBLE") : " - "));
            ticket.AppendLine(string.Format("FECHA TERMINADO: {0}", (mayoreo_venta_data.fecha_terminado != null) ? Misc_helper.fecha(mayoreo_venta_data.fecha_terminado.ToString(), "LEGIBLE") : " - "));

			if(es_revision)
			{
				//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
                ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
				ticket.AppendLine("--------------------------------------------------------");
				//ticket.Append(POS_Control.font_size_16 + POS_Control.align_center);
                ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
				ticket.AppendLine("INFORMACION REVISION");
				//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
                ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
				ticket.AppendLine("--------------------------------------------------------");
				ticket.AppendLine(string.Format("TERMINAL: {0}", (mayoreo_venta_data.terminal_id_revision != null ) ? Misc_helper.get_nombre_terminal((int)mayoreo_venta_data.terminal_id_revision) : "SIN TERMINAL"));
				ticket.AppendLine(string.Format("INICIA: {0}", mayoreo_venta_data.nombre_empleado_inicio_verificacion.ToUpper()));
				ticket.AppendLine(string.Format("FINALIZA: {0}", mayoreo_venta_data.nombre_empleado_fin_verificacion.ToUpper()));
                ticket.AppendLine(string.Format("FECHA INICIADO: {0}", (mayoreo_venta_data.fecha_inicio_verifiacion != null) ? Misc_helper.fecha(mayoreo_venta_data.fecha_inicio_verifiacion.ToString(), "LEGIBLE") : " - "));
                ticket.AppendLine(string.Format("FECHA FINALIZADO: {0}", (mayoreo_venta_data.fecha_fin_verificacion != null) ? Misc_helper.fecha(mayoreo_venta_data.fecha_fin_verificacion.ToString(), "LEGIBLE") : " - "));
			}

            if (reimpresion)
            {
                reimpresionTicket();
            }

			//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			ticket.AppendLine("COD   PRODUCTO                                  PRECIO C");
			ticket.AppendLine("CADUCIDAD                LOTE                       CANT");
			ticket.AppendLine("--------------------------------------------------------");

			decimal subtotal = 0;
			decimal total = 0;
			decimal importe_iva = 0;

			foreach (DTO_Detallado_mayoreo_ventas_ticket row in detallado)
			{
				ticket.AppendLine(string.Format("{0,5} {1,-37}{2,13:C2}",
					row.amecop,
					(row.nombre.Length >= 36) ? row.nombre.Substring(0,37).ToString() : row.nombre,
					row.precio_unitario)
				);

				//Log_error.log("Subtotal: "+subtotal + "subtotal_captura: "+row.subtotal_captura + " subtotal_revision: "+row.subtotal_revision);

				subtotal += (es_revision) ? row.subtotal_revision : row.subtotal_captura;
				total += (es_revision) ? row.total_revision : row.total_captura;
				importe_iva += (es_revision) ? row.importe_iva_revision : row.importe_iva_captura;

				foreach (Tuple<string, string, int, int> cad_lotes in row.caducidades_lotes)
				{
					ticket.AppendLine(string.Format("{0,9} {1,-33}{2,13}",
                        HELPERS.Misc_helper.fecha(cad_lotes.Item1, "CADUCIDAD"),
						HELPERS.Misc_helper.PadBoth(cad_lotes.Item2, 33),
						(es_revision) ? cad_lotes.Item4 : cad_lotes.Item3
						)
					);
				}
			}

			ticket.AppendLine("\n");
			ticket.AppendLine(string.Format("{0,56}", "-----------------"));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "SUBTOTAL:", subtotal));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "IMPORTE IVA:", importe_iva));
			ticket.AppendLine(string.Format("{0,56}", "-----------------"));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "TOTAL:", total));
			
			//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");

            POS_Control.finTicket(ticket);
		}

		public bool print()
		{
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "VENTA", mayoreo_venta_id, true);
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
