﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.tae_diestel_new;
using PXSecurity.Datalogic.Classes;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
	class Ticket_venta_tae : IDisposable
	{
		private long venta_id;
		public StringBuilder ticket = new StringBuilder();
		DAO_Ventas dao_ventas = new DAO_Ventas();
        bool reimpresion = false;
        string tae_diestel_enc_key = Config_helper.get_config_global("tae_diestel_enc_key");

        public void construccion_ticket(long venta_id, cCampo[] campos)
        {
            this.venta_id = venta_id;

            DAO_Ventas dao_ventas = new DAO_Ventas();
            DAO_Clientes dao_clientes = new DAO_Clientes();
            DTO_Ventas_ticket venta_ticket = dao_ventas.get_informacion_ticket_venta(venta_id);

            //ticket.AppendLine(POS_Control.logo);
            ticket.Append("[LOGO]");
            //ticket.AppendLine(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");
            //ticket.Append(POS_Control.font_size_112 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_112][ALIGN_CENTER]");
            ticket.AppendLine("VENTA");
            //ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_CENTER]");
            ticket.AppendLine("--------------------------------------------------------");

            DAO_Sucursales dao_sucursales = new DAO_Sucursales();
            string sucursal_id = HELPERS.Config_helper.get_config_local("sucursal_id");
            var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(sucursal_id));
            //ticket.Append(POS_Control.printer_reset + POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine(string.Format("{0}RFC:{1}\n{2}\n{3} COLONIA {4}\n{5} CP {6}, TEL: {7}", "", sucursal_data.rfc, "SUCURSAL " + sucursal_data.nombre, sucursal_data.direccion, sucursal_data.colonia, sucursal_data.ciudad, sucursal_data.codigo_postal, sucursal_data.telefono));

            ticket.AppendLine(string.Format("TERMINAL: {0}", venta_ticket.nombre_terminal));
            ticket.AppendLine(string.Format("FOLIO: #{0}", venta_ticket.venta_folio));

            if (venta_ticket.cotizacion_id != null)
            {
                ticket.AppendLine(string.Format("ATENDIO: {0}", venta_ticket.empleado_atendio));
            }

            ticket.AppendLine(string.Format("CAJERO: {0}", venta_ticket.nombre_empleado));
            ticket.AppendLine(string.Format("FECHA CREADO: {0}", Misc_helper.fecha(venta_ticket.fecha_creado.ToString(), "LEGIBLE")));
            ticket.AppendLine(string.Format("FECHA TERMINADO: {0}", Misc_helper.fecha(venta_ticket.fecha_terminado.ToString(), "LEGIBLE")));

            if (venta_ticket.comentarios.Trim().Length > 0)
            {
                ticket.AppendLine(string.Format("COMENTARIOS: {0}", venta_ticket.comentarios.ToUpper()));
            }

            if (venta_ticket.numero_transaccion > 0)
            {
                ticket.AppendLine("--------------------------------------------------------");
                ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
                ticket.AppendLine("INFORMACION TAE");
                ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
                ticket.AppendLine(string.Format("NO. TRANSACCION : {0}", venta_ticket.numero_transaccion));
                ticket.AppendLine(string.Format("CAJA            : {0}", Misc_helper.get_terminal_id()));

                ticket.AppendLine(string.Format("{0}", campos[0].sValor.ToString()));
                ticket.AppendLine(string.Format("REFERENCIA      : {0}", PXCryptography.PXDecryptFX(campos[1].sValor.ToString(), tae_diestel_enc_key)));
                ticket.AppendLine(string.Format("AUTORIZACION    : {0}", campos[2].sValor.ToString()));
                ticket.AppendLine(string.Format("MONTO           : ${0}", campos[3].sValor.ToString()));
                ticket.AppendLine(string.Format("{0}", campos[4].sValor.ToString()));
            }

            //ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");

            //ticket.Append(POS_Control.font_size_64 + POS_Control.align_center); 
            ticket.Append("[FONT_SIZE_64][ALIGN_CENTER]");
            ticket.AppendLine("[REIMPRESION]");

            //ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");

            ticket.AppendLine("COD   PRODUCTO                                  PRECIO U");
            ticket.AppendLine("--------------------------------------------------------");

            foreach (DTO_Detallado_ventas_ticket detallado_ticket in venta_ticket.detallado_ventas_ticket)
            {
                ticket.AppendLine(string.Format("{0,5} {1,-37}{2,13:C2}",
                    detallado_ticket.amecop,
                    detallado_ticket.nombre,
                    detallado_ticket.precio_unitario)
                );


                long cantidad_cad = 0;

                foreach (Tuple<string, string, int> cad_lotes in detallado_ticket.caducidades_lotes)
                {
                    /*
                    ticket.AppendLine(string.Format("{0,9} {1,-33}{2,13}",
                        (cad_lotes.Item1.Equals("SIN CAD") || cad_lotes.Item1.Equals("0000-00-00") || cad_lotes.Item1.Equals(" ")) ? "SIN CAD" : HELPERS.Misc_helper.fecha(cad_lotes.Item1, "CADUCIDAD"),
                        HELPERS.Misc_helper.PadBoth(cad_lotes.Item2, 33),
                        cad_lotes.Item3)
                    );
                    */
                    cantidad_cad += cad_lotes.Item3;
                }

                decimal total_parcial_importe = (detallado_ticket.precio_unitario * cantidad_cad) - ((detallado_ticket.precio_unitario * cantidad_cad) * detallado_ticket.descuento);

                ticket.AppendLine(string.Format("{0,16}{1,11:C2} - {2,9:P2} = {3,14:C2}", "IMPORTE: ", (detallado_ticket.precio_unitario * detallado_ticket.cantidad), (detallado_ticket.descuento), total_parcial_importe));
            }

            ticket.AppendLine("\n");
            ticket.AppendLine(string.Format("{0,56}", "-----------------"));
            ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "SUBTOTAL:", venta_ticket.subtotal));
            ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "IVA:", venta_ticket.iva));
            ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "IEPS:", venta_ticket.ieps));
            ticket.AppendLine(string.Format("{0,56}", "-----------------"));
            ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "TASA 0%:", venta_ticket.excento));

            string tasa_gravado = string.Format("TASA {0}%:", Misc_helper.pct_iva_global());

            ticket.AppendLine(string.Format("{0,38} {1,17:C2}", tasa_gravado, venta_ticket.gravado));
            ticket.AppendLine(string.Format("{0,56}", "-----------------"));
            ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "TOTAL:", venta_ticket.total));
            ticket.AppendLine(POS_Control.align_left + "IMPORTE CON LETRAS: SON " + Misc_helper.NumtoLe(venta_ticket.total));
            ticket.AppendLine("--------------------------------------------------------");
            ticket.AppendLine("TIPO DE PAGO                        CUENTA         MONTO");
            ticket.AppendLine("--------------------------------------------------------");
            var formas_pago = dao_ventas.get_tipos_pagos_venta(venta_id);

            decimal total_cambio = 0;

            foreach (DTO_Formas_pago pago in formas_pago)
            {
                if (pago.entrega_cambio)
                {
                    total_cambio += pago.monto - pago.importe;
                }

                ticket.AppendLine(string.Format("{0:33} {1:8} {2:13}", pago.nombre.PadRight(33, ' ').Substring(0, 33), pago.cuenta.PadLeft(8, ' ').Substring(0, 8).ToUpper(), pago.monto.ToString("C2").PadLeft(13, ' ')));
                //ticket.AppendLine("//////////");
            }

            ticket.AppendLine(string.Format("{0,56}", "-----------------"));
            ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "CAMBIO:", total_cambio));


            //ticket.Append(POS_Control.font_size_64 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_64][ALIGN_CENTER]");
            ticket.AppendLine("[REIMPRESION]");

            //ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            //ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");
            //ticket.AppendLine(POS_Control.align_center);
            ticket.Append("[ALIGN_CENTER]");
            //ticket.AppendLine(POS_Control.align_center);
            ticket.Append("[ALIGN_CENTER]");
            //ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_center);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_CENTER]");
            ticket.AppendLine("NOTA DE VENTA AL PUBLICO EN GENERAL EL PAGO SE REALIZA");
            ticket.AppendLine("EN UNA SOLA EXHIBICION Y SE INCLUYE EN LA NOTA FINAL");
            ticket.AppendLine("DEL DIA PARA EFECTOS FISCALES AL PAGO");
            ticket.AppendLine("REGIMEN GENERAL DE LEY PERSONAS MORALES");
            ticket.AppendLine("LUGAR DE EXPEDICION: MERIDA, YUC.");
            ticket.AppendLine("DOMICILIO FISCAL");
            ticket.AppendLine(sucursal_data.domicilio_fiscal.ToUpper());
            ticket.Append("[ALIGN_CENTER][ALIGN_CENTER]");
            //ticket.AppendLine(POS_Control.align_center); ticket.AppendLine(POS_Control.align_center);
            ticket.Append("[ALIGN_CENTER][ALIGN_CENTER]");
            ticket.AppendLine(POS_Control.barcode(string.Format("{0}${1}", Config_helper.get_config_local("sucursal_id").PadLeft(3, '0'), venta_id.ToString().PadLeft(3, '0'))));
            POS_Control.finTicket(ticket);
        }

        public bool print()
        {
            var venta_data = dao_ventas.get_venta_data(venta_id);
            return HELPERS.Print_new_helper.print(venta_data.terminal_id, ticket.ToString(), "VENTA", venta_data.venta_folio, true, false, false, reimpresion);
        }

        public void Dispose()
        {

        }
    }
}
