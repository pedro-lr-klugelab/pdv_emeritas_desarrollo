using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.DTO;


namespace Farmacontrol_PDV.CLASSES.PRINT
{
    class ticket_circulo_oro
    {
        private StringBuilder ticket = new StringBuilder();

        public void construccion_ticket(string tarjeta, string autorizacion, string beneficios, string folio_venta)
        {

            DAO_Sucursales dao_sucursales = new DAO_Sucursales();
            var dto_sucursal = dao_sucursales.get_sucursal_data(Convert.ToInt32(Config_helper.get_config_local("sucursal_id")));
            string nombre_sucursal = "";
            if (dto_sucursal.sucursal_id > 0)
            {
                nombre_sucursal = dto_sucursal.nombre;
            }


            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");
            ticket.Append("[FONT_SIZE_0][ALIGN_CENTER]");
            ticket.AppendLine("Programa: CIRCULO DE LA SALUD");
            ticket.AppendLine("Sucursal : " + nombre_sucursal);
            ticket.AppendLine("Fecha : " + Misc_helper.fecha());
            ticket.AppendLine("Tarjeta : " + tarjeta);
            ticket.AppendLine("Venta : " + folio_venta);
            ticket.AppendLine("Autorizacion : " + autorizacion);

            ticket.AppendLine("--------------------------------------------------------");

            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            /*
            ticket.AppendLine("Beneficios otorgados SOY TU FAN :");
            if (beneficios == "")
            {
                ticket.AppendLine("Ticket Acumulativo : " + folio_venta);
            }
            else
            {
                List<string> promocion = new List<string>();
                promocion = beneficios.Split('*').ToList();

                ticket.AppendLine(promocion[0] + " " + promocion[1] + " : " + promocion[2] + " pz");
            }
             */ 
            ticket.AppendLine("\n");
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
            ticket.Append("[ALIGN_CENTER]");
            //ticket.AppendLine("Farmacos Nacionales, S. A de C.V.,con domicilio en Dr. Pasteur No. 93, Col. Doctores, Alcaldía Cuauhtemoc, C.P. 06720,Ciudad de Mexico, Mexico es el responsable del tratamiento y protección de los datos personales, los cuales seran utilizados con la finalidad principal de proveerle nuestros productos y seran utilizados con la finalidad principal de proveerle nuestros productos y servicios.Consulta nuestro aviso de privacidad en: www.soytufan.mx");


            POS_Control.finTicket(ticket);
        }


        public bool print()
        {
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "RECIBO", 0, true);
        }

        public void Dispose()
        {
            ticket = new StringBuilder();
        }




    }
}
