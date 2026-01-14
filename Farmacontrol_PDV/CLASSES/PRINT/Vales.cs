using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
    class Vales
    {
        private StringBuilder ticket = new StringBuilder();


        public void construccion_ticket(string nombre_cliente, decimal importe, string comentarios, long empleado_id )
        {
            DAO_Sucursales dao_sucursales = new DAO_Sucursales();
            string sucursal_id = HELPERS.Config_helper.get_config_local("sucursal_id");
            var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(sucursal_id));
            DAO_Empleados dao_empleados = new DAO_Empleados();

            var empleado_data = dao_empleados.get_empleado_data((int)empleado_id);

            ticket.AppendLine(POS_Control.logo);
           
            ticket.AppendLine(string.Format("{0}\n{1}\n{2} COLONIA {3}\n{4} CP {5}, TEL: {6}", "",  sucursal_data.nombre, sucursal_data.direccion, sucursal_data.colonia, sucursal_data.ciudad, sucursal_data.codigo_postal, sucursal_data.telefono));
            ticket.AppendLine("FECHA: " + Misc_helper.fecha("", "ISO"));
            ticket.AppendLine("********************************************************");
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
            ticket.Append("[FONT_SIZE_0][ALIGN_CENTER]");
      
            ticket.AppendLine("Empleado: " + empleado_data.Nombre.ToUpper());
            ticket.AppendLine("Cliente: " + nombre_cliente.ToUpper());

            ticket.AppendLine("Comentarios: " + comentarios.ToUpper());

            ticket.AppendLine("**CODIGO          PRODUCTO                     CANTIDAD*");
            ticket.AppendLine(" \n");
            ticket.AppendLine("____________   ___________________________     ________ ");
            ticket.AppendLine(" \n");
            ticket.AppendLine("____________   ___________________________     ________ ");
            ticket.AppendLine(" \n");
            ticket.AppendLine("____________   ___________________________     ________ ");
            ticket.AppendLine(" \n");
            
            

            ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");
            ticket.AppendLine("********************************************************");
            ticket.AppendLine("*                                                      *");
            ticket.AppendLine("*                                                      *");
            ticket.AppendLine("*                                                      *");
            ticket.AppendLine("*                                                      *");
            ticket.AppendLine("*                                                      *");
            ticket.AppendLine("*                                                      *");
            ticket.AppendLine("*  -------------------------------------------------   *");
            ticket.AppendLine("*                        SELLO                         *");
            ticket.AppendLine("*                                                      *");
            ticket.AppendLine("********************************************************");
            ticket.AppendLine("ESTE VALE TIENE UNA VIGENCIA DE 30 DIAS");
            ticket.Append("[ALIGN_CENTER]");
            ticket.AppendLine("DUDAS Y SUGERENCIAS : \n");
            ticket.AppendLine("comentarios@emeritafarmacias.com");
            ticket.AppendLine("www.emeritafarmacias.com");

            ticket.Append("[CORTE]");
        }

        public bool print()
        {
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "VALE_FARMACIA",  0, true);
        }


    }
}
