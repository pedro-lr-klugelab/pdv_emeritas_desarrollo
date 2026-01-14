using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
	class Recibo_efectivo_prepago
	{
		private StringBuilder ticket = new StringBuilder();
		private long prepago_id;

		public void construccion_ticket(long prepago_id)
		{
			this.prepago_id = prepago_id;
			
			DAO_Clientes dao_clientes = new DAO_Clientes();
			DAO_Prepago dao_prepago = new DAO_Prepago();
			DAO_Vales_efectivo dao_vales = new DAO_Vales_efectivo();
			DAO_Sucursales dao_sucursales = new DAO_Sucursales();

			var informacion_prepago = dao_prepago.get_informacion_prepago(prepago_id);
			var informacion_cliente = dao_clientes.get_informacion_cliente(informacion_prepago.cliente_id);
			var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(Config_helper.get_config_local("sucursal_id")));

			var detallado_prepago = dao_prepago.get_detallado_prepago(prepago_id);
			decimal total_ticket = dao_vales.get_total_devolucion_prepago(prepago_id);

			ticket.AppendLine("--------------------------------------------------------");
			//ticket.Append(POS_Control.printer_reset + POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[PRINTER_RESET][FONT_CONDENSE][FONT_SIZE_0]");
			//ticket.Append(POS_Control.font_size_16 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
			ticket.AppendLine("DEVOLUCION DE EFECTIVO");
			//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			//ticket.Append(POS_Control.font_size_16 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
			ticket.AppendLine("DATOS DEL CLIENTE");
			//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
			ticket.AppendLine("NOMBRE: "+informacion_cliente.nombre);
			ticket.AppendLine("--------------------------------------------------------");
			//ticket.Append(POS_Control.font_size_16 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
			ticket.AppendLine(string.Format("IMPORTE: {0}",total_ticket.ToString("C2")));
			//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");

			ticket.AppendLine(string.Format("RECIBI DE {0},  LA CANTIDAD DE {1} ({2}), COMO DEVOLUCION AL IMPORTE TOTAL DE UN ENCARGO PREPAGADO, CELEBRADO EL DIA {3}",
				sucursal_data.razon_social,
				total_ticket.ToString("C2"),
				Misc_helper.NumtoLe(total_ticket).ToUpper(),
				Convert.ToDateTime(informacion_prepago.fecha_pago).ToLongDateString().ToUpper()
			));

			ticket.Append(" \n");
			//ticket.Append(POS_Control.align_center);
            ticket.Append("[ALIGN_CENTER]");
			//ticket.AppendLine(POS_Control.align_center+"PRODUCTOS DENTRO DE LA DEVOLUCION"+POS_Control.align_left);
            ticket.Append("[ALIGN_CENTER]PRODUCTOS DENTRO DE LA DEVOLUCION[ALIGN_LEFT]");

			ticket.AppendLine("--------------------------------------------------------");
			ticket.AppendLine("COD   PRODUCTO                         CANT     PRECIO U");
			ticket.AppendLine("--------------------------------------------------------");

			foreach (DTO_Detallado_prepago row in detallado_prepago)
			{
				ticket.AppendLine(string.Format("{0,5} {1,-28} {2,7} {3,13:C2}",
					row.amecop,
					row.producto.Substring(0, (row.producto.Length >= 28) ? 28 : row.producto.Length),
					row.cantidad,
					((row.precio_publico * row.pct_iva) + row.precio_publico)
				));
			}

			ticket.AppendLine("--------------------------------------------------------");
			ticket.AppendLine("\n");
			//ticket.AppendLine(POS_Control.align_center+informacion_cliente.nombre);
            ticket.Append("[ALIGN_CENTER]");
            ticket.AppendLine(informacion_cliente.nombre);
			ticket.AppendLine("\n");
			ticket.AppendLine("\n");
			ticket.AppendLine("___________________________");
			ticket.AppendLine("FIRMA");

			/*
			ticket.AppendLine(string.Format("{0,56}", "-----------------"));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "SUBTOTAL:", cotizacion_ticket.subtotal));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "IVA:", cotizacion_ticket.iva));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "IEPS:", cotizacion_ticket.ieps));
			ticket.AppendLine(string.Format("{0,56}", "-----------------"));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "EXENTO:", cotizacion_ticket.excento));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "GRAVADO:", cotizacion_ticket.gravado));
			ticket.AppendLine(string.Format("{0,56}", "-----------------"));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "TOTAL:", cotizacion_ticket.total));
			 * */

			ticket.AppendLine("--------------------------------------------------------");
            POS_Control.finTicket(ticket);
		}
		
		public bool print()
		{
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "RECIBO_EFECTIVO", 0, true);
		}

		public void Dispose()
		{
			ticket = new StringBuilder();
		}

        
	}
}
