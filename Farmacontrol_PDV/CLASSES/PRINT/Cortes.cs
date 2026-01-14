using System;
using System.Text;
using System.Collections.Generic;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
    class Cortes : IDisposable
    {
        private long corte_id;
        private readonly StringBuilder ticket = new StringBuilder();          // Ticket principal
        private readonly StringBuilder ticketFacturas = new StringBuilder();  // Ticket aparte de facturas
        private DTO_Corte corte;

        public void construccion_ticket(long corte_id, bool reimpresion = false)
        {
            this.corte_id = corte_id;

            DAO_Cortes dao_corte = new DAO_Cortes();
            corte = dao_corte.get_informacion_corte(corte_id);

            bool es_total = corte.tipo.Equals("TOTAL");
            var tipos_cambio = es_total
                ? dao_corte.get_tipos_cambio_total(corte.corte_id)
                : dao_corte.get_tipos_cambio(corte.corte_id);

            // ===== Encabezado común =====
            ticket.Clear();
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");
            ticket.Append("[FONT_SIZE_48][ALIGN_CENTER]");
            ticket.AppendLine($"CORTE {corte.tipo.ToUpper()}");
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
            ticket.AppendLine("--------------------------------------------------------");

            if (reimpresion) reimpresionTicket(ticket);

            DAO_Sucursales dao_sucursales = new DAO_Sucursales();
            string sucursal_id = Config_helper.get_config_local("sucursal_id");
            var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(sucursal_id));

            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine($"{sucursal_data.razon_social} - RFC:{sucursal_data.rfc}");
            ticket.AppendLine($"{sucursal_data.nombre}");
            ticket.AppendLine($"{sucursal_data.direccion} COLONIA {sucursal_data.colonia}");
            ticket.AppendLine($"{sucursal_data.ciudad} CP {sucursal_data.codigo_postal}, TEL: {sucursal_data.telefono}");

            string nombre_terminal = Misc_helper.get_nombre_terminal((int)(Misc_helper.get_terminal_id() ?? 0));
            ticket.AppendLine($"TERMINAL: {nombre_terminal}");
            ticket.AppendLine($"FOLIO: #{corte.corte_folio}");
            ticket.AppendLine($"VENTAS DEL {(es_total ? "DIA" : "CORTE")} DEL FOLIO #{corte.venta_inicial} - #{corte.venta_final}");
            ticket.AppendLine($"CREADO POR: {corte.nombre_empleado}");
            ticket.AppendLine($"FECHA: {(corte.fecha != null ? Misc_helper.fecha(corte.fecha.ToString(), "LEGIBLE") : " - ")}");
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");

            ticket.Append("[ALIGN_LEFT][FONT_SIZE_16]");
            ticket.AppendLine("VENTAS POR TIPO DE PAGO:");
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");

            foreach (var cambio in tipos_cambio)
                ticket.AppendLine(string.Format("{0,39}: {1,15}", cambio.Item1, cambio.Item2.ToString("C2")));

            if (corte.importe_total != 0)
            {
                corte.importe_cancelaciones = dao_corte.total_ventas_canceladas(corte_id, es_total);

                ticket.Append("[ALIGN_LEFT][FONT_SIZE_16]");
                ticket.AppendLine("DETALLADO:");
                ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
                ticket.AppendLine(string.Format("{0,39}: {1,15}", "  VENTAS BRUTAS", corte.importe_bruto.ToString("C2")));
                ticket.AppendLine(string.Format("{0,39}: {1,15}", "- CANCELACIONES", corte.importe_cancelaciones.ToString("C2")));
                ticket.AppendLine(string.Format("{0,39}: {1,15}", "= VENTAS TOTALES", (corte.importe_bruto - corte.importe_cancelaciones).ToString("C2")));
                ticket.AppendLine(@"                                         _______________");
                ticket.AppendLine(string.Format("{0,39}: {1,15}", "+ TASA 0%", corte.importe_excento.ToString("C2")));

                string tasa_gravado = $"TASA {Misc_helper.pct_iva_global()}%:";
                ticket.AppendLine(string.Format("{0,39}: {1,15}", "+ " + tasa_gravado, corte.importe_gravado.ToString("C2")));
                ticket.AppendLine(string.Format("{0,39}: {1,15}", "+ IVA", corte.importe_iva.ToString("C2")));
                ticket.AppendLine(string.Format("{0,39}: {1,15}", "+ IEPS", corte.importe_ieps.ToString("C2")));
                ticket.AppendLine(@"                                         _______________");
                ticket.AppendLine(string.Format("{0,39}: {1,15}", "= TOTAL", corte.importe_total.ToString("C2")));
                ticket.AppendLine(string.Format("{0,39}: {1,15}", "+ VALES EMITIDOS", corte.vales_emitidos.ToString("C2")));
                ticket.AppendLine(string.Format("{0,39}: {1,15}", "  PREPAGOS REALIZADOS", corte.importe_prepagado.ToString("C2")));
                ticket.AppendLine(string.Format("{0,39}: {1,15}", "- PREPAGOS CANCELADOS", corte.importe_prepagado_cancelado.ToString("C2")));
                ticket.AppendLine(@"                                         _______________");
                ticket.AppendLine(string.Format("{0,39}  {1,15}", "=", Convert.ToDecimal((corte.vales_emitidos + corte.importe_total + corte.importe_prepagado) - corte.importe_prepagado_cancelado).ToString("C2")));

                ticket.Append("\n");

                // --- VALES ---
                DAO_Vales_efectivo dao_vales = new DAO_Vales_efectivo();
                var vales_canjeados = dao_vales.get_vales_canjeados(corte.corte_id);
                var vales_cancelados = dao_vales.get_vales_cancelados(corte.corte_id);

                if (vales_canjeados.Count > 0)
                {
                    ticket.Append("[ALIGN_LEFT][FONT_SIZE_16]");
                    ticket.AppendLine("VALES CANJEADOS:");
                    ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
                    ticket.AppendLine("--------------------------------------------------------");
                    ticket.AppendLine("IDENT    FECHA                                     MONTO");
                    ticket.AppendLine("--------------------------------------------------------");

                    foreach (DTO_Vale vale in vales_canjeados)
                        ticket.AppendLine(string.Format("{0,8} {1,33} {2,13}",
                            vale.vale_efectivo_id.Substring(0, 8).PadRight(8, ' ').ToUpper(),
                            Misc_helper.fecha(vale.fecha_canje.ToString()).PadRight(33, ' '),
                            vale.total.ToString("C2").PadLeft(13, ' ')));

                    ticket.AppendLine("--------------------------------------------------------");
                }

                if (vales_cancelados.Count > 0)
                {
                    ticket.Append("[ALIGN_LEFT][FONT_SIZE_16]");
                    ticket.AppendLine("VALES CANCELADOS:");
                    ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
                    ticket.AppendLine("--------------------------------------------------------");
                    ticket.AppendLine("IDENT    FECHA                                     MONTO");
                    ticket.AppendLine("--------------------------------------------------------");

                    foreach (DTO_Vale vale in vales_cancelados)
                        ticket.AppendLine(string.Format("{0,8} {1,33} {2,13}",
                            vale.vale_efectivo_id.Substring(0, 8).PadRight(8, ' ').ToUpper(),
                            Misc_helper.fecha(vale.fecha_cancelacion.ToString()).PadRight(33, ' '),
                            vale.total.ToString("C2").PadLeft(13, ' ')));

                    ticket.AppendLine("--------------------------------------------------------");
                }

                ticket.Append("\n");

                // --- PREPAGOS ---
                DAO_Prepago dao_prepago = new DAO_Prepago();
                var prepagos_realizados = dao_prepago.get_prepagos_realizados_corte(corte_id, es_total);
                var prepagos_cancelados = dao_prepago.get_prepagos_cancelados_corte(corte_id, es_total);

                if (prepagos_realizados.Count > 0 || prepagos_cancelados.Count > 0)
                {
                    ticket.Append("[ALIGN_CENTER][FONT_SIZE_16]");
                    ticket.AppendLine("LISTADO DE RECIBOS");
                    ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
                    ticket.AppendLine("--------------------------------------------------------");

                    ticket.AppendLine("PREPAGOS REALIZADOS");
                    ticket.AppendLine("--------------------------------------------------------");
                    ticket.AppendLine("FOLIO                   FECHA                   MONTO   ");
                    ticket.AppendLine("--------------------------------------------------------");

                    bool movReal = false;
                    foreach (DTO_Prepago prepago in prepagos_realizados)
                    {
                        ticket.AppendLine(string.Format("{0,-9} {1,-30} {2,15}",
                            prepago.prepago_id,
                            Misc_helper.fecha(prepago.fecha_pago.ToString()),
                            prepago.monto.ToString("C2")));
                        movReal = true;
                    }
                    if (!movReal)
                    {
                        ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]"); ticket.AppendLine("-- SIN MOVIMIENTOS --"); ticket.Append("[ALIGN_LEFT]");
                    }
                    else ticket.AppendLine("--------------------------------------------------------");

                    ticket.Append("\n");

                    ticket.AppendLine("PREPAGOS CANCELADOS:");
                    ticket.AppendLine("--------------------------------------------------------");
                    ticket.AppendLine("FOLIO                   FECHA                   MONTO   ");
                    ticket.AppendLine("--------------------------------------------------------");

                    bool movCanc = false;
                    foreach (DTO_Prepago prepago in prepagos_cancelados)
                    {
                        ticket.AppendLine(string.Format("{0,-9} {1,-30} {2,15}",
                            prepago.prepago_id,
                            Convert.ToDateTime(prepago.fecha_cancelado).ToString("dd/MMM/yyy h:mm:ss tt").ToUpper().Replace(".", ""),
                            prepago.monto.ToString("C2")));
                        movCanc = true;
                    }
                    if (!movCanc)
                    {
                        ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]"); ticket.AppendLine("-- SIN MOVIMIENTOS --"); ticket.Append("[ALIGN_LEFT]");
                    }
                    else ticket.AppendLine("--------------------------------------------------------");

                    ticket.Append("\n");
                }


            }
            else
            {
                ticket.Append("\n\n");
                ticket.AppendLine("********************************************************");
                ticket.Append("[FONT_CONDENSED][FONT_SIZE_32][ALIGN_CENTER]");
                ticket.AppendLine("CORTE GENERADO");
                ticket.AppendLine("SIN VENTAS");
                ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
                ticket.AppendLine("********************************************************");
                ticket.Append("\n\n");
            }

            // ======= (NUEVO) Sección FACTURAS: construir UNA sola vez su cuerpo =======
            StringBuilder cuerpoFacturas = null;

            if (corte.tipo.Equals("PARCIAL"))
            {
                DAO_Ventas dao = new DAO_Ventas();
                var ventas_facturadas = dao.get_ventas_facturadas_corte_parcial(corte.corte_id);

                if (ventas_facturadas.Count > 0)
                {
                    DAO_Pago_tipos dao_pagos = new DAO_Pago_tipos();
                    var tipos_pago = dao_pagos.get_pago_tipos(null);

                    cuerpoFacturas = new StringBuilder();
                    cuerpoFacturas.AppendLine("FOLIO             IMPORTE                 METODO DE PAGO");
                    cuerpoFacturas.AppendLine("--------------------------------------------------------");

                    foreach (DTO_Pago_tipos tipo_pago in tipos_pago)
                    {
                        decimal total_tipo = 0;
                        foreach (DTO_Ventas_facturadas vf in ventas_facturadas)
                        {
                            if (tipo_pago.nombre.Equals(vf.metodo_pago))
                            {
                                cuerpoFacturas.AppendLine(string.Format("{0,11} {1,13} {2,30}",
                                    vf.folio.ToString().PadRight(11, ' ').Substring(0, 11),
                                    vf.importe.ToString("C2").PadLeft(13, ' ').Substring(0, 13),
                                    vf.metodo_pago.PadLeft(30, ' ').Substring(0, 30)));
                                total_tipo += vf.importe;
                            }
                        }
                        if (total_tipo > 0)
                        {
                            cuerpoFacturas.AppendLine(string.Format("{0,42} {1,13}",
                                "TOTAL: ".PadLeft(42, ' ').Substring(0, 42),
                                total_tipo.ToString("C2").PadLeft(13, ' ').Substring(0, 13)));
                            cuerpoFacturas.AppendLine("--------------------------------------------------------");
                        }
                    }

                    // (A) Agregar la sección de facturas al FINAL del ticket principal
                    ticket.Append("\n");
                    ticket.Append("[ALIGN_CENTER][FONT_SIZE_16]");
                    ticket.AppendLine("VENTAS FACTURADAS");
                    ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
                    ticket.AppendLine("--------------------------------------------------------");
                    ticket.Append(cuerpoFacturas.ToString());
                }
            }

            // Cierre del ticket principal
            ticket.AppendLine("--------------------------------------------------------");
            POS_Control.finTicket(ticket);

            // (B) Ticket aparte solo de facturas (si aplica)
            ticketFacturas.Clear();
            if (cuerpoFacturas != null && cuerpoFacturas.Length > 0)
            {
                ticketFacturas.Append("[LOGO]");
                ticketFacturas.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
                ticketFacturas.AppendLine("--------------------------------------------------------");
                ticketFacturas.Append("[FONT_SIZE_48][ALIGN_CENTER]");
                ticketFacturas.AppendLine("VENTAS FACTURADAS");
                ticketFacturas.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
                ticketFacturas.AppendLine("--------------------------------------------------------");

                ticketFacturas.AppendLine($"{sucursal_data.razon_social} - RFC:{sucursal_data.rfc}");
                ticketFacturas.AppendLine($"SUCURSAL {sucursal_data.nombre}");
                ticketFacturas.AppendLine($"{sucursal_data.direccion} COLONIA {sucursal_data.colonia}");
                ticketFacturas.AppendLine($"{sucursal_data.ciudad} CP {sucursal_data.codigo_postal}, TEL: {sucursal_data.telefono}");
                ticketFacturas.AppendLine($"TERMINAL: {nombre_terminal}");
                ticketFacturas.AppendLine($"FOLIO CORTE: #{corte.corte_folio}");
                ticketFacturas.AppendLine($"FECHA: {(corte.fecha != null ? Misc_helper.fecha(corte.fecha.ToString(), "LEGIBLE") : " - ")}");
                ticketFacturas.AppendLine("--------------------------------------------------------");

                ticketFacturas.Append(cuerpoFacturas.ToString());
                POS_Control.finTicket(ticketFacturas);
                if (reimpresion)
                    reimpresionTicket(ticketFacturas);
            }
        }

        public bool print()
        {
            long terminal = (long)(Misc_helper.get_terminal_id() ?? 0);

            bool ok1 = Print_new_helper.print(terminal, ticket.ToString(), "CORTE", corte.corte_folio, true);

            bool ok2 = true;
            if (ticketFacturas.Length > 0)
                ok2 = Print_new_helper.print(terminal, ticketFacturas.ToString(), "CORTE - FACTURAS", corte.corte_folio, true);

            return ok1 && ok2;
        }

        private void reimpresionTicket(StringBuilder t)
        {
            t.Append("[FONT_SIZE_64][ALIGN_CENTER]");
            t.AppendLine("[REIMPRESION]");
            t.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            t.AppendLine("--------------------------------------------------------");
        }

        public void Dispose() { }
    }
}
