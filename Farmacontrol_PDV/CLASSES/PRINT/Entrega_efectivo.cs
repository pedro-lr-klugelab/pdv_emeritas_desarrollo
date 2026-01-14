using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.HELPERS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
    class Entrega_efectivo
    {
        private long entrega_efectivo;
        public StringBuilder ticket = new StringBuilder();
        bool reimpresion = false;

        public void construccion_ticket(long entrega_efectivo_id, bool reimpresion = false)
        {
            this.entrega_efectivo = entrega_efectivo_id;
            this.reimpresion = reimpresion;

            /*ticket.AppendLine(POS_Control.font_condensed + POS_Control.font_size_0);*/
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");
            /*ticket.Append(POS_Control.font_size_32 + POS_Control.align_center);*/
            ticket.Append("[FONT_SIZE_32][ALIGN_CENTER]");
            ticket.AppendLine("RETIRO DE EFECTIVO");
            /*ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);*/
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_CENTER]");
            ticket.AppendLine("--------------------------------------------------------");
            /*ticket.Append(POS_Control.font_condensed + POS_Control.font_size_48 + POS_Control.align_center);*/
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_48][ALIGN_CENTER]");
            ticket.AppendLine("#"+entrega_efectivo_id.ToString());
            /*ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);*/
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
            ticket.AppendLine("--------------------------------------------------------");
            DAO_Sucursales dao_sucursales = new DAO_Sucursales();
            string sucursal_id = HELPERS.Config_helper.get_config_local("sucursal_id");
            var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(sucursal_id));
            /*ticket.Append(POS_Control.printer_reset + POS_Control.font_condensed + POS_Control.font_size_0);*/
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine(string.Format("{0} - RFC:{1}\n{2}\n{3} COLONIA {4}\n{5} CP {6}, TEL: {7}", sucursal_data.razon_social, sucursal_data.rfc, "SUCURSAL " + sucursal_data.nombre, sucursal_data.direccion, sucursal_data.colonia, sucursal_data.ciudad, sucursal_data.codigo_postal, sucursal_data.telefono));

            /*ticket.Append(POS_Control.align_left + POS_Control.font_size_0);*/
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");

            /*ticket.Append(POS_Control.font_size_64 + POS_Control.align_center);*/
            ticket.Append("[FONT_SIZE_64][ALIGN_CENTER]");
            ticket.AppendLine("[REIMPRESION]");
            /*ticket.Append(POS_Control.align_left + POS_Control.font_size_0);*/
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");


            DAO_Entregas_efectivo dao_entregas = new DAO_Entregas_efectivo();
            var entrega_data = dao_entregas.get_entrega_efectivo_data(entrega_efectivo_id);


            ticket.AppendLine(string.Format("FECHA: {0}", Misc_helper.fecha(entrega_data.fecha.ToString(), "LEGIBLE")));
            ticket.AppendLine(string.Format("ENTREGA: {0}", entrega_data.nombre_empleado));
            ticket.AppendLine(string.Format("RECIBE: {0}", entrega_data.quien_recibe));
            ticket.AppendLine(string.Format("COMENTARIO: {0}", entrega_data.comentario));
            ticket.AppendLine("IMPORTE:");

            /*ticket.Append(POS_Control.font_condensed + POS_Control.font_size_32 + POS_Control.align_center);*/
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_32][ALIGN_CENTER]");
            ticket.AppendLine(entrega_data.importe.ToString("C2"));
            /*ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_center);*/
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_CENTER]");

            /*ticket.AppendLine(POS_Control.align_center);*/
            ticket.AppendLine("[ALIGN_CENTER]"); 
            /*ticket.AppendLine(POS_Control.align_center);*/
            ticket.AppendLine("[ALIGN_CENTER]");
            /*ticket.AppendLine(POS_Control.align_center);*/
            ticket.AppendLine("[ALIGN_CENTER]");
            /*ticket.AppendLine(POS_Control.align_center);*/
            ticket.AppendLine("[ALIGN_CENTER]");
            /*ticket.AppendLine(POS_Control.align_center);*/
            ticket.AppendLine("[ALIGN_CENTER]");
            /*ticket.AppendLine(POS_Control.align_center);*/
            ticket.AppendLine("[ALIGN_CENTER]");

            /*ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_center);*/
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_CENTER]");
            ticket.AppendLine("-------------------------");
            ticket.AppendLine("FIRMA");

            /*
                FECHA: XXXXXXX (fecha y hora)
                ENTREGA: XXXXXXXXXXX
                RECIBE: XXXXXXXXXXXXXX
                COMENTARIO: XXXXXXXXXXXXXXXXXX
             */

            POS_Control.finTicket(ticket);
        }

        public bool print()
        {
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "ENTREGA_EFECTIVO", entrega_efectivo, true, false, false, reimpresion);
        }
    }
}
