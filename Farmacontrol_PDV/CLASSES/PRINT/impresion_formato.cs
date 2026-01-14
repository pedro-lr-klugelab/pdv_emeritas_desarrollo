using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;
using System;
using System.Text;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
    public class Ticket_ResumenVaucheras : IDisposable
    {
        private readonly StringBuilder ticket = new StringBuilder();
        private DTO_Corte corte;

        // Línea separadora y ancho total del ticket
        private const string SEP = "--------------------------------------------------------";
        private static readonly int TOTAL = SEP.Length;
        string nombre_terminal = Misc_helper.get_nombre_terminal((int)(Misc_helper.get_terminal_id() ?? 0));

        // Anchos de columnas
        private const int W_CANT = 8;
        private static readonly int MID = TOTAL / 2;
        private static readonly int W_CONCEPTO = MID - (W_CANT / 2);
        private static readonly int W_RIGHT_BLOCK = TOTAL - (W_CONCEPTO + W_CANT);
        private const int W_IMPORTE_MIN = 13;

        public void Construir(DTO_ResumenVaucherasTicket dto)
        {
            DAO_Sucursales dao_sucursales = new DAO_Sucursales();
            string sucursal_id = Config_helper.get_config_local("sucursal_id");
            var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(sucursal_id));

            // === Encabezado ===
            ticket.Append("[LOGO]");
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine(SEP);
            ticket.Append("[FONT_SIZE_48][ALIGN_CENTER]");
            ticket.AppendLine("DETALLES CORTE");
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
            ticket.AppendLine(SEP);

            // Datos de la sucursal 
            ticket.AppendLine($"SUCURSAL: {sucursal_data.nombre}");
            ticket.AppendLine($"TURNO: {dto.Turno}");
            ticket.AppendLine($"FECHA: {dto.Fecha:dd/MM/yyyy HH:mm:ss}");
            ticket.AppendLine(SEP);

            // === RESUMEN VAUCHERAS ===
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_16]");
            ticket.AppendLine("RESUMEN VAUCHERAS");
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            ticket.AppendLine(SEP);
            ticket.AppendLine(
                FitLeft("Concepto", W_CONCEPTO) +
                CenterIn("Cantidad", W_CANT) +
                RightIn("Importe", W_RIGHT_BLOCK)
            );
            ticket.AppendLine(SEP);

            foreach (var it in dto.Vaucheras)
            {
                ticket.AppendLine(
                    FitLeft(it.Concepto ?? "", W_CONCEPTO) +
                    CenterIn(it.Cantidad.ToString(), W_CANT) +
                    RightIn(it.Importe.ToString("C2"), W_RIGHT_BLOCK)
                );
            }

            ticket.AppendLine(SEP);
            ticket.AppendLine(
                FitLeft("TOTAL:", W_CONCEPTO) +
                CenterIn(dto.TotalVaucherasCantidad.ToString(), W_CANT) +
                RightIn(dto.TotalVaucherasImporte.ToString("C2"), W_RIGHT_BLOCK)
            );

            // === EXTRAS ===
            ticket.Append("\n");
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_16]");
            ticket.AppendLine("EXTRAS");
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            ticket.AppendLine(SEP);
            ticket.AppendLine(
                FitLeft("Concepto", W_CONCEPTO) +
                CenterIn("Cantidad", W_CANT) +
                RightIn("Importe", W_RIGHT_BLOCK)
            );
            ticket.AppendLine(SEP);

            foreach (var it in dto.Extras)
            {
                ticket.AppendLine(
                    FitLeft(it.Concepto ?? "", W_CONCEPTO) +
                    CenterIn(it.Cantidad.ToString(), W_CANT) +
                    RightIn(it.Importe.ToString("C2"), W_RIGHT_BLOCK)
                );
            }

            ticket.AppendLine(SEP);
            ticket.AppendLine(
                FitLeft("TOTAL EXTRAS:", W_CONCEPTO) +
                new string(' ', W_CANT) +
                RightIn(dto.TotalExtras.ToString("C2"), W_RIGHT_BLOCK)
            );

            // === DENOMINACIONES ===
            ticket.Append("\n");
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_16]");
            ticket.AppendLine("DENOMINACIONES");
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            ticket.AppendLine(SEP);
            ticket.AppendLine(
                FitLeft("Denominacion", W_CONCEPTO) +
                CenterIn("Cantidad", W_CANT) +
                RightIn("Valor", W_RIGHT_BLOCK)
            );
            ticket.AppendLine(SEP);

            // Para billetes/monedas
            void denom(string nombre, int cantidad, decimal unit)
            {
                ticket.AppendLine(
                    FitLeft(nombre, W_CONCEPTO) +
                    CenterIn(cantidad.ToString(), W_CANT) +
                    RightIn((cantidad * unit).ToString("C2"), W_RIGHT_BLOCK)
                );
            }

            // Para MORRALLA: se interpreta como valor directo en pesos (decimal)
            void denomMorralla(decimal valor)
            {
                string cant = valor.ToString(valor % 1m == 0m ? "0" : "0.##");
                ticket.AppendLine(
                    FitLeft("MORRALLA", W_CONCEPTO) +
                    CenterIn(cant, W_CANT) +
                    RightIn(valor.ToString("C2"), W_RIGHT_BLOCK)
                );
            }

            denomMorralla(dto.Denoms.Morralla);
            denom("$20", dto.Denoms.D20, 20m);
            denom("$50", dto.Denoms.D50, 50m);
            denom("$100", dto.Denoms.D100, 100m);
            denom("$200", dto.Denoms.D200, 200m);
            denom("$500", dto.Denoms.D500, 500m);
            denom("$1000", dto.Denoms.D1000, 1000m);

            ticket.AppendLine(SEP);
            ticket.AppendLine(
                FitLeft("TOTAL EFECTIVO:", W_CONCEPTO) +
                new string(' ', W_CANT) +
                RightIn(dto.Denoms.Total.ToString("C2"), W_RIGHT_BLOCK)
            );

            // === GRAN TOTAL ===
            ticket.Append("\n");
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_16]");
            ticket.AppendLine($"GRAN TOTAL: {dto.GranTotal.ToString("C2")}");
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            ticket.AppendLine(SEP);

            POS_Control.finTicket(ticket);
        }

        public bool Print()
        {
            long terminal = (long)(Misc_helper.get_terminal_id() ?? 0);
            long folio = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
            return Print_new_helper.print(terminal, ticket.ToString(), "DETALLADO CORTE", folio, true);
        }

        public void Dispose() { }

        // ===== Helpers =====
        private static string FitLeft(string text, int width)
        {
            if (text == null) text = "";
            if (text.Length > width) return text.Substring(0, width);
            return text.PadRight(width, ' ');
        }

        private static string RightIn(string text, int width)
        {
            if (text == null) text = "";
            if (width < W_IMPORTE_MIN) width = W_IMPORTE_MIN;
            if (text.Length > width) return text.Substring(0, width);
            return text.PadLeft(width, ' ');
        }

        private static string CenterIn(string text, int width)
        {
            if (text == null) text = "";
            if (text.Length >= width) return text.Substring(0, width);
            int total = width - text.Length;
            int left = Math.Max(0, (total / 2) - 1);
            int right = total - left;
            return new string(' ', left) + text + new string(' ', right);
        }
    }
}
