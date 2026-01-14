using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
    class Ticket_reporte_letra : IDisposable
    {
        private StringBuilder ticket = new StringBuilder();
        
        private string letra;

        public void construccion_ticket(string letra)
        {
            this.letra = letra;
            DAO_Articulos dao_articulos = new DAO_Articulos();
            List<Busqueda_articulos_existencias> resultados_busqueda = dao_articulos.get_articulos_data_existencias(letra);
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");
            ticket.Append("[FONT_SIZE_64][ALIGN_CENTER]");
            ticket.AppendLine("EXISTENCIAS");
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");
            ticket.AppendLine("COD        PRODUCTO                    EXIS     PRECIO U");
            ticket.AppendLine("--------------------------------------------------------");

            foreach (Busqueda_articulos_existencias detallado_ticket in resultados_busqueda)
            {
                ticket.AppendLine(string.Format("{0:5} {1:29} {2,6} {3,13:C4}",
                    detallado_ticket.amecop.Substring(detallado_ticket.amecop.Length - 5),
                    (detallado_ticket.nombre.Length > 29) ? detallado_ticket.nombre.Substring(0, 29) : detallado_ticket.nombre.PadRight(29, ' '),
                    detallado_ticket.existencia_total,
                    detallado_ticket.precio_publico.ToString("C2"))
                );

                //ticket.AppendLine(string.Format("{0,16}{1,11:C2} - ({2,9})% = {3,11:C2}", "IMPORTE: ", (detallado_ticket.precio_unitario * detallado_ticket.cantidad), (detallado_ticket.descuento * 100), detallado_ticket.subtotal));
            }
            ticket.AppendLine("\n");
            ticket.AppendLine(string.Format("{0,56}", "-----------------"));
            /*
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
             * */
            POS_Control.finTicket(ticket);
        }

        public void imprimeSeleccionados(List<Busqueda_articulos_existencias> itemSelecionados) 
        {
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");
            ticket.Append("[FONT_SIZE_64][ALIGN_CENTER]");
            ticket.AppendLine("EXISTENCIAS");
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");
            ticket.AppendLine("COD   PRODUCTO                                  PRECIO U");
            ticket.AppendLine("CADUCIDAD                LOTE                       CANT");
            ticket.AppendLine("--------------------------------------------------------");
            
            string tmp = "";

            foreach (Busqueda_articulos_existencias item in itemSelecionados)
            {
                if (item.amecop.Length > 6)
                {

                    if (tmp != item.nombre)
                    {
                        ticket.AppendLine(string.Format("{0,5} {1,-37}{2,13:C2}",
                            item.amecop.Substring(item.amecop.Length - 5),
                            (item.nombre.Length > 29) ? item.nombre.Substring(0, 29) : item.nombre.PadRight(29, ' '),
                            item.precio_publico.ToString("C2"))
                        );

                        ticket.AppendLine(string.Format("{0,9} {1,-33}{2,13}",
                            (item.caducidad.Equals("SIN CAD")) ? "SIN CAD" : item.caducidad,
                            HELPERS.Misc_helper.PadBoth(item.lote, 29),
                            item.existencia_total.ToString()));
                    }
                    else
                    {
                        ticket.AppendLine(string.Format("{0,9} {1,-33}{2,13}",
                            (item.caducidad.Equals("SIN CAD")) ? "SIN CAD" : item.caducidad,
                            HELPERS.Misc_helper.PadBoth(item.lote, 29),
                            item.existencia_total.ToString()));
                    }

                    tmp = item.nombre;
                }
            }
            ticket.AppendLine("--------------------------------------------------------");
            POS_Control.finTicket(ticket);
        }

        public bool print()
        {
            string timestamp = GetTimestamp(DateTime.Now);
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "REP LETRA", Convert.ToInt32(timestamp), true);
            //return HELPERS.Print_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "COTIZACION", cotizacion_id, true);
        }

        public void Dispose()
        {
            ticket = new StringBuilder();
            DAO_Articulos dao_articulos = new DAO.DAO_Articulos();
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("ddHHmmss");
        }

        
    }
}
