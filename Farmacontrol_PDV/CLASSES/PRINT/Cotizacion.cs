using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.DTO;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
	class Ticket_folio_cotizacion : IDisposable
	{
		private StringBuilder ticket = new StringBuilder();
		private long cotizacion_id;

		public void construccion_ticket(long cotizacion_id)
		{
			this.cotizacion_id = cotizacion_id;
			DAO_Cotizaciones dao_cotizaciones = new DAO_Cotizaciones();
			DTO.DTO_Cotizacion_ticket cotizacion_ticket = dao_cotizaciones.get_informacion_ticket_cotizacion(cotizacion_id);
			//ticket.AppendLine(POS_Control.font_size_16 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
			ticket.AppendLine("FOLIO: #"+cotizacion_id.ToString());
			ticket.AppendLine("TOTAL: "+cotizacion_ticket.total.ToString("C2"));
			ticket.AppendLine(POS_Control.barcode(cotizacion_id.ToString().PadLeft(6, '0')));
            POS_Control.finTicket(ticket);
		}

		public bool print()
		{
            DAO_Terminales dao = new DAO_Terminales();
			return (dao.imprime_folio_cotizacion()) ? HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(),ticket.ToString(),"COTIZACION",cotizacion_id,true) : true;
		}
		
		public void Dispose()
		{
			ticket = new StringBuilder();
		}
	}

	class Ticket_cotizacion : IDisposable
	{
		private StringBuilder ticket = new StringBuilder();
		private DAO.DAO_Cotizaciones dao_cotizaciones = new DAO.DAO_Cotizaciones();
		private long cotizacion_id;

		public void construccion_ticket(long cotizacion_id)
		{
			this.cotizacion_id = cotizacion_id;

			DTO.DTO_Cotizacion_ticket cotizacion_ticket = dao_cotizaciones.get_informacion_ticket_cotizacion(cotizacion_id);
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");
            ticket.Append("[FONT_SIZE_64][ALIGN_CENTER]");
            ticket.AppendLine("COTIZACION");
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");
            ticket.AppendLine("COD   PRODUCTO                         CANT     PRECIO U");
            ticket.AppendLine("--------------------------------------------------------");

			foreach (DTO.DTO_Detallado_cotizacion_ticket detallado_ticket in cotizacion_ticket.detallado_cotizacion_ticket)
			{
				ticket.AppendLine(string.Format("{0:5} {1:29} {2,6} {3,13:C4}",
					detallado_ticket.amecop,
					(detallado_ticket.nombre.Length > 29) ? detallado_ticket.nombre.Substring(0,29) : detallado_ticket.nombre,
					detallado_ticket.cantidad,
					detallado_ticket.precio_unitario)
				);

				ticket.AppendLine(string.Format("{0,16}{1,11:C2} - ({2,9})% = {3,11:C2}","IMPORTE: ",(detallado_ticket.precio_unitario * detallado_ticket.cantidad),(detallado_ticket.descuento * 100),detallado_ticket.subtotal));
			}
			ticket.AppendLine("\n");
			ticket.AppendLine(string.Format("{0,56}","-----------------"));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "SUBTOTAL:", cotizacion_ticket.subtotal));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "IVA:", cotizacion_ticket.iva));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "IEPS:", cotizacion_ticket.ieps));
			ticket.AppendLine(string.Format("{0,56}", "-----------------"));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "TASA 0%:", cotizacion_ticket.excento));

            string tasa_gravado = string.Format("TASA {0}%:", Misc_helper.pct_iva_global());

			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", tasa_gravado, cotizacion_ticket.gravado));
			ticket.AppendLine(string.Format("{0,56}", "-----------------"));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "TOTAL:", cotizacion_ticket.total));
			ticket.AppendLine("--------------------------------------------------------");
            //nuevo joel
            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
            ticket.AppendLine("**PRECIO SUJETO A CAMBIOS**");
           
            ticket.AppendLine("\n");
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine(string.Format("*TICKET NO VALIDO COMO COMPROBANTE DE PAGO,"));
            ticket.AppendLine(string.Format("*EN CASO DE NO RECIBIR SU TICKET DE COMPRA"));
            ticket.AppendLine(string.Format("*FAVOR DE REPORTARLO : "));
            ticket.AppendLine("\n");
            ticket.Append("[ALIGN_CENTER]");
            ticket.AppendLine("comentarios@emeritafarmacias.com");
            ticket.AppendLine("www.emeritafarmacias.com");
            ticket.AppendLine("\n");
            ticket.AppendLine("GRACIAS POR SU PREFERENCIA!!");
            //

            POS_Control.finTicket(ticket);
		}

		public bool print()
		{
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "COTIZACION", cotizacion_id, true);
            //return HELPERS.Print_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "COTIZACION", cotizacion_id, true);
		}

		public void Dispose()
		{
			ticket = new StringBuilder();
			dao_cotizaciones = new DAO.DAO_Cotizaciones();
		}
	}

    class Ticket_test
    {
        private StringBuilder ticket = new StringBuilder();
        private DAO_Cotizaciones dao_cotizaciones = new DAO_Cotizaciones();
        private long cotizacion_id;

        public void construccion_ticket(long cotizacion_id)
        {
            this.cotizacion_id = cotizacion_id;

            DTO_Cotizacion_ticket cotizacion_ticket = dao_cotizaciones.get_informacion_ticket_cotizacion(cotizacion_id);

            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");
            ticket.Append("[FONT_SIZE_64][ALIGN_CENTER]");
            ticket.AppendLine("COTIZACION");
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");
            ticket.AppendLine("COD   PRODUCTO                         CANT     PRECIO U");
            ticket.AppendLine("--------------------------------------------------------");

            foreach (DTO.DTO_Detallado_cotizacion_ticket detallado_ticket in cotizacion_ticket.detallado_cotizacion_ticket)
            {
                ticket.AppendLine(string.Format("{0:5} {1:29} {2,6} {3,13:C4}",
                    detallado_ticket.amecop,
                    (detallado_ticket.nombre.Length > 29) ? detallado_ticket.nombre.Substring(0, 29) : detallado_ticket.nombre,
                    detallado_ticket.cantidad,
                    detallado_ticket.precio_unitario)
                );

                ticket.AppendLine(string.Format("{0,16}{1,11:C2} - ({2,9})% = {3,11:C2}", "IMPORTE: ", (detallado_ticket.precio_unitario * detallado_ticket.cantidad), (detallado_ticket.descuento * 100), detallado_ticket.subtotal));
            }
            ticket.AppendLine("\n");
            ticket.AppendLine(string.Format("{0,56}", "-----------------"));
            ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "SUBTOTAL:", cotizacion_ticket.subtotal));
            ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "IVA:", cotizacion_ticket.iva));
            ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "IEPS:", cotizacion_ticket.ieps));
            ticket.AppendLine(string.Format("{0,56}", "-----------------"));
            ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "TASA 0%:", cotizacion_ticket.excento));

            string tasa_gravado = string.Format("TASA {0}%:", Misc_helper.pct_iva_global());

            ticket.AppendLine(string.Format("{0,38} {1,17:C2}", tasa_gravado, cotizacion_ticket.gravado));
            ticket.AppendLine(string.Format("{0,56}", "-----------------"));
            ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "TOTAL:", cotizacion_ticket.total));
            ticket.AppendLine("--------------------------------------------------------");
            POS_Control.finTicket(ticket);
        }

        public bool print()
        {
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "COTIZACION", cotizacion_id, true);
        }
    }
}
