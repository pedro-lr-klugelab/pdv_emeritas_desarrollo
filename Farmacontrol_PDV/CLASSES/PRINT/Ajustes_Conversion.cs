using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
    class Ticket_Ajustes_conversion
    {
        private long ajuste_existencia_id;
        private StringBuilder ticket = new StringBuilder();
        private bool reimpresion = false;

        public void construccion_ticket(long ajuste_existencia_id, bool reimpresion = false)
        {
            this.ajuste_existencia_id = ajuste_existencia_id;
            this.reimpresion = reimpresion;

            var dao = new DAO_Ajustes_existencias();
            var ajuste = dao.get_informacion_ajuste_existencia(ajuste_existencia_id);
            var detalles = dao.get_detallado_ajuste_ticket(ajuste_existencia_id);

            generarEncabezado(ajuste);
            generarDetalle(detalles);
            generarPie(detalles);

            POS_Control.finTicket(ticket);
        }

        private void generarEncabezado(DTO_Ajustes_existencias ajuste)
        {
            var dao_sucursales = new DAO_Sucursales();
            string sucursal_id = Config_helper.get_config_local("sucursal_id");
            var sucursal = dao_sucursales.get_sucursal_data(Convert.ToInt32(sucursal_id));

            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");
            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
            ticket.AppendLine("*AJUSTES DE CONVERSION*");
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
            ticket.AppendLine("--------------------------------------------------------");

            if (reimpresion)
            {
                ticket.Append("[FONT_SIZE_64][ALIGN_CENTER]");
                ticket.AppendLine("[REIMPRESION]");
                ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
                ticket.AppendLine("--------------------------------------------------------");
            }

            ticket.AppendLine($"{sucursal.nombre} RFC:{sucursal.rfc}");
            ticket.AppendLine($"{sucursal.direccion} COLONIA {sucursal.colonia}");
            ticket.AppendLine($"{sucursal.ciudad} CP {sucursal.codigo_postal}, TEL: {sucursal.telefono}");

            ticket.AppendLine($"FOLIO: #{ajuste.ajuste_existencia_id}");
            ticket.AppendLine($"TERMINAL: {Misc_helper.get_nombre_terminal((int)ajuste.terminal_id)}");
            ticket.AppendLine($"CREADO POR: {ajuste.nombre_empleado_captura}");
            ticket.AppendLine($"TERMINADO POR: {ajuste.nombre_empleado_termina}");
            ticket.AppendLine($"FECHA CREADO: {formatearFecha(ajuste.fecha_creado)}");
            ticket.AppendLine($"FECHA TERMINADO: {formatearFecha(ajuste.fecha_terminado)}");

            if (!string.IsNullOrWhiteSpace(ajuste.comentarios))
                ticket.AppendLine($"COMENTARIOS: {ajuste.comentarios.ToUpper()}");

            ticket.AppendLine("--------------------------------------------------------");
            ticket.AppendLine("COD   PRODUCTO");
            ticket.AppendLine("CADUCIDAD            LOTE           EX-ANT   CANT    DIF");
            ticket.AppendLine("--------------------------------------------------------");
        }

        private void generarDetalle(List<DTO_Detallado_ajuste_ticket> detalles)
        {
            foreach (var item in detalles)
            {
                ticket.AppendLine($"{item.amecop,5} {item.nombre,-50}");

                foreach (var cad in item.caducidades_lotes)
                {
                    ticket.AppendLine(string.Format(
                        "{0,9} {1,-25} {2,6} {3,6} {4,6}",
                        Misc_helper.fecha(cad.Item1, "CADUCIDAD"),
                        Misc_helper.PadBoth(cad.Item2, 25),
                        cad.Item3, cad.Item4, cad.Item5));
                }
            }
        }

        private void generarPie(List<DTO_Detallado_ajuste_ticket> detalles)
        {
            long total_productos = detalles.Sum(d => d.caducidades_lotes.Count);

            if (total_productos == 0)
            {
                ticket.Append("[ALIGN_CENTER][FONT_SIZE_16]");
                ticket.AppendLine("\nAJUSTE TERMINADO VACIO");
                ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            }

            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");
        }

        private string formatearFecha(object fecha)
        {
            return fecha != null ? Misc_helper.fecha(fecha.ToString(), "LEGIBLE") : " - ";
        }

        public bool print()
        {
            var dao = new DAO_Ajustes_existencias();
            var ajuste = dao.get_informacion_ajuste_existencia(ajuste_existencia_id);

            return Print_new_helper.print(
                ajuste.terminal_id,
                ticket.ToString(),
                "AJUSTE_EXISTENCIA",
                ajuste_existencia_id,
                true,
                false,
                false,
                reimpresion
            );
        }
    }
}
