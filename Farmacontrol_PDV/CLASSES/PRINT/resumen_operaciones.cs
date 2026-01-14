using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using System.Data;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
    class resumen_operaciones
    {

        private StringBuilder ticket = new StringBuilder();
        

        public void construccion_ticket()
        {

            DAO_Entradas entradas = new DAO_Entradas();
 
            DataTable informacion_entradas = entradas.get_entradas_dia();
                       
            //ticket.Append(POS_Control.printer_reset + POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[PRINTER_RESET][FONT_CONDENSE][FONT_SIZE_0]");
            //ticket.Append(POS_Control.font_size_16 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
            ticket.AppendLine("RESUMEN");
            //ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            ticket.AppendLine("------------------------------------------------");
            //ticket.Append(POS_Control.font_size_16 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
            ticket.AppendLine("*Entradas*");

            DAO_Sucursales dao_sucursales = new DAO_Sucursales();
            string sucursal_id = HELPERS.Config_helper.get_config_local("sucursal_id");
            var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(sucursal_id));
            
           ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");

            ticket.AppendLine(string.Format("SUCURSAL : {0}", sucursal_data.nombre));

            //ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            ticket.AppendLine(string.Format("FECHA: {0}", Misc_helper.fecha()));
            
            
      
            //ticket.Append(POS_Control.align_center);
         ///   ticket.Append("[ALIGN_CENTER]");
            //ticket.AppendLine(POS_Control.align_center+"PRODUCTOS DENTRO DE LA DEVOLUCION"+POS_Control.align_left);

            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
            ticket.AppendLine("--------------------------------");
            ticket.AppendLine("FOLIO      MAYORISTA     FACT    ");
            ticket.AppendLine("--------------------------------");

            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
            foreach (DataRow dr in informacion_entradas.Rows)
            {
                ticket.AppendLine(string.Format("{0,5} {1,-28} {2,13} ",
                    dr["folio"],
                    dr["mayorista"].ToString().Substring(0, (dr["mayorista"].ToString().Length >= 28) ? 28 : dr["mayorista"].ToString().Length),
                    dr["factura"]
                    
                ));
            }


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
