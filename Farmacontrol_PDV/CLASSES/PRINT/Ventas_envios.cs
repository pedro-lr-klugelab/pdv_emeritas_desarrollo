﻿using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
    class Ventas_envios
    {
        private long venta_envio_folio;
        private StringBuilder ticket = new StringBuilder();
        bool reimpresion = false;

        public void construccion_ticket(long venta_envio_folio, bool reimpresion = false)
        {
            this.venta_envio_folio = venta_envio_folio;
            this.reimpresion = reimpresion;

            DAO_Ventas dao_ventas = new DAO_Ventas();

            //ticket.AppendLine(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");
            //ticket.Append(POS_Control.font_size_32 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_32][ALIGN_CENTER]");
            ticket.AppendLine("SERVICIOS A");
            ticket.AppendLine("DOMICILIO");
            //ticket.Append(POS_Control.font_size_32 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_32][ALIGN_CENTER]");
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

            var detallado = dao_ventas.get_ventas_envios(venta_envio_folio);

            

            //ticket.Append(POS_Control.printer_reset + POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine(string.Format("{0} - RFC:{1}\n{2}\n{3} COLONIA {4}\n{5} CP {6}, TEL: {7}", sucursal_data.razon_social, sucursal_data.rfc, "SUCURSAL: " + sucursal_data.nombre, sucursal_data.direccion, sucursal_data.colonia, sucursal_data.ciudad, sucursal_data.codigo_postal, sucursal_data.telefono));
            ticket.AppendLine("--------------------------------------------------------");
            ticket.AppendLine(string.Format("FOLIO: #{0}", venta_envio_folio));
            //detallado[0].
            ticket.AppendLine("DILIGENCIERO: " + detallado[0].empleado.ToUpper());
            ticket.AppendLine("FECHA ENVIADO: " + Misc_helper.fecha(detallado[0].fecha_envio.ToString()));
            //ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");
            ticket.AppendLine("CAJA         FOLIO    PIEZAS       IMPORTE         TOTAL");
            ticket.AppendLine("--------------------------------------------------------");

            decimal total_global = 0;

            foreach (DTO_Envio_ventas_detallado detallado_ticket in detallado)
            {
                ticket.AppendLine("NOMBRE: " + detallado_ticket.nombre_cliente.ToString());
                
                ticket.AppendLine(string.Format("{0,8} {1,9} {2,9} {3,13} {4,13}",
                    detallado_ticket.caja.PadRight(8, ' ').Substring(0, 8),
                    detallado_ticket.folio.ToString().PadLeft(9, ' ').Substring(0, 9),
                    detallado_ticket.piezas.ToString().PadLeft(9, ' ').Substring(0, 9),
                    detallado_ticket.importe.ToString("C2").PadLeft(13, ' ').Substring(0, 13),
                    detallado_ticket.total.ToString("C2").PadLeft(13, ' ')
                    )
                );
                
                total_global += detallado_ticket.total;
                ticket.AppendLine("");
            }

            ticket.AppendLine("\n");
            ticket.Append("[ALIGN_RIGHT]");
            ticket.AppendLine("TOTAL VENTAS: " + total_global.ToString("C2"));
            ticket.AppendLine("--------------------------------------------------------");
            ticket.Append("[ALIGN_CENTER]");
            ticket.AppendLine("TOTALES");
            ticket.AppendLine("--------------------------------------------------------");
            ticket.Append("[ALIGN_LEFT]");
            var totales = dao_ventas.get_pago_tipos_ventas_envios(venta_envio_folio);

            decimal total_totales = 0;

            foreach (var tupla in totales)
            {
                total_totales += tupla.Item2;
                ticket.AppendLine(string.Format("{0,41} {1,13:C2}", tupla.Item1.PadRight(41, ' '), tupla.Item2));
            }

            ticket.AppendLine("\n");
            ticket.Append("[ALIGN_RIGHT]");
            ticket.AppendLine("TOTAL: " + total_totales.ToString("C2"));

            //ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");
            POS_Control.finTicket(ticket);
        }

        public bool print()
        {
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "VENTAS_ENVIOS", venta_envio_folio, true, false, false, reimpresion);
        }
    }
}