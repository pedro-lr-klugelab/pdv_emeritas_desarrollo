using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
    class Devolucion
    {
        private long venta_id;
        private StringBuilder ticket = new StringBuilder();
        DAO_Ventas dao_ventas = new DAO_Ventas();
        bool reimpresion = false;

        public void construccion_ticket(long venta_id, bool refacturacion, bool reimpresion = false)
        {
            this.venta_id = venta_id;
            this.reimpresion = reimpresion;

            DAO_Clientes dao_clientes = new DAO_Clientes();
            DAO_Cancelaciones dao_cancelaciones = new DAO_Cancelaciones();
            DTO_Ventas_ticket venta_ticket = dao_ventas.get_informacion_ticket_venta(venta_id);

            var info_cancelacion = dao_cancelaciones.get_cancelacion_data(venta_id);

            //ticket.AppendLine(POS_Control.logo + POS_Control.align_left);

            DAO_Sucursales dao_sucursales = new DAO_Sucursales();
            string sucursal_id = HELPERS.Config_helper.get_config_local("sucursal_id");
            var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(sucursal_id));

            //ticket.Append(POS_Control.printer_reset + POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
            ticket.Append("[LOGO]");
            ticket.AppendLine(string.Format("{0}RFC:{1}\n{2}\n{3} COLONIA {4}\n{5} CP {6}, TEL: {7}", "", sucursal_data.rfc, sucursal_data.nombre, sucursal_data.direccion, sucursal_data.colonia, sucursal_data.ciudad, sucursal_data.codigo_postal, sucursal_data.telefono));

            ticket.AppendLine(string.Format("FOLIO: #{0}", venta_ticket.venta_folio));
            if (venta_ticket.cotizacion_id != null)
            {
                ticket.AppendLine(string.Format("ATENDIO: {0}", venta_ticket.empleado_atendio));
            }

            ticket.AppendLine(string.Format("CAJERO: {0}", venta_ticket.nombre_empleado));
            ticket.AppendLine(string.Format("TERMINAL: {0}", venta_ticket.nombre_terminal));
            ticket.AppendLine(string.Format("FECHA CREADO: {0}", Misc_helper.fecha(venta_ticket.fecha_creado.ToString(), "LEGIBLE")));
            ticket.AppendLine(string.Format("FECHA TERMINADO: {0}", Misc_helper.fecha(venta_ticket.fecha_terminado.ToString(), "LEGIBLE")));

            //ticket.AppendLine(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine("********************************************************");

            if (refacturacion)
            {
                //ticket.Append(POS_Control.font_size_48 + POS_Control.align_center);
                ticket.Append("[FONT_SIZE_48][ALIGN_CENTER]");
                ticket.AppendLine("CANCELADO POR");
                ticket.AppendLine("REFACTURACION");
                //ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0);
                ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
            }
            else
            {
                //ticket.Append(POS_Control.font_size_80 + POS_Control.align_center);
                ticket.Append("[FONT_SIZE_80][ALIGN_CENTER]");
                ticket.AppendLine("CANCELADO");
                //ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0);				
                ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
            }

            ticket.AppendLine("ESTA VENTA FUE CANCELADA CON EL FOLIO #" + info_cancelacion.Rows[0]["cancelacion_id"].ToString());

            //var existe_factura = WebServicePac_helper.existe_factura(venta_id);

          //  if (existe_factura.status) OJOJOJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJ
          //  {
         //       ticket.AppendLine("ESTA VENTA FUE FACTURADA CON EL FOLIO #" + venta_ticket.venta_folio + " AL RFC " + existe_factura.rfc_receptor.ToUpper());
          //  }

            if (info_cancelacion.Rows[0]["nueva_venta_id"].ToString() != "")
            {
                long nueva_venta_id_tmp = Convert.ToInt64(info_cancelacion.Rows[0]["nueva_venta_id"]);
                var venta_data_tmp = dao_ventas.get_venta_data(nueva_venta_id_tmp);
                ticket.AppendLine(" Y FUE SUSTITUIDA POR LA VENTA #" + venta_data_tmp.venta_folio);

                Ticket_venta ticket_venta = new Ticket_venta();
                //ticket_venta.construccion_ticket(Convert.ToInt64(info_cancelacion.Rows[0]["nueva_venta_id"]));
                ticket_venta.construccion_ticket(venta_data_tmp.venta_id);
                ticket_venta.print();
                ticket_venta.print();
            }

            //ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_center);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");

            ticket.AppendLine(string.Format("MOTIVO: {0}", info_cancelacion.Rows[0]["comentarios"]));

            if (refacturacion == false)
            {
                ticket.AppendLine("********************************************************");
                ticket.AppendLine("REQUISITOS DE CANCELACION");
                //ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
                ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
                ticket.AppendLine("   NOMBRE: _____________________________________________\n");
                ticket.AppendLine("DIRECCION: _____________________________________________\n");
                ticket.AppendLine("           _____________________________________________\n");
                ticket.AppendLine(" TELEFONO: _________ - _________ - ____________\n");
                ticket.AppendLine("     #IFE: _____________________________________________\n");
                ticket.AppendLine("    EMAIL: _____________________________________________\n");
                ticket.AppendLine("  IMPORTE: _____________________\n\n\n");
                //ticket.Append(POS_Control.align_center + POS_Control.font_size_0);
                ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");
                ticket.AppendLine("________________________________");
                ticket.AppendLine("FIRMA");
                ticket.AppendLine("********************************************************");
            }

            //ticket.AppendLine(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");

            if (venta_ticket.domicilio != "")
            {
                var domicilio = dao_clientes.get_domicilio_data(venta_ticket.domicilio);
                //ticket.Append(POS_Control.font_size_32 + POS_Control.align_center);
                ticket.Append("[FONT_SIZE_32][ALIGN_CENTER]");
                ticket.AppendLine("VENTA A DOMICILIO");
                //ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
                ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
                ticket.AppendLine("NOMBRE: " + domicilio.Rows[0]["nombre"].ToString().ToUpper());
                ticket.AppendLine("TIPO: " + domicilio.Rows[0]["tipo"].ToString().ToUpper());
                ticket.AppendLine("DIRECCION: " + domicilio.Rows[0]["direccion"].ToString().ToUpper());
                ticket.AppendLine("TELEFONO: " + domicilio.Rows[0]["telefono"].ToString().ToUpper());
                ticket.AppendLine("--------------------------------------------------------");
            }

            if (venta_ticket.credito != "")
            {
                var credito = dao_clientes.get_cliente_credito_data(venta_ticket.credito);
                //ticket.Append(POS_Control.font_size_32 + POS_Control.align_center);
                ticket.Append("[FONT_SIZE_32][ALIGN_CENTER]");
                ticket.AppendLine("VENTA A CREDITO");
                //ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
                ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
                ticket.AppendLine("NOMBRE: " + credito.Rows[0]["nombre"].ToString().ToUpper());
                ticket.AppendLine("DIRECCION: " + credito.Rows[0]["direccion"].ToString().ToUpper());
                ticket.AppendLine("TELEFONO: " + credito.Rows[0]["telefono"].ToString().ToUpper());
                //ticket.Append(POS_Control.align_center);
                ticket.Append("[ALIGN_CENTER]");
                ticket.Append("\n "); ticket.Append("\n "); ticket.Append("\n ");
                ticket.AppendLine("___________________________");
                ticket.AppendLine("firma");
                ticket.AppendLine("--------------------------------------------------------");
            }

            /*
            if (venta_ticket.rfc_registro != "")
            {
                var credito = dao_clientes.get_rfc_data(venta_ticket.rfc_registro);
                ticket.Append(POS_Control.font_size_32 + POS_Control.align_center);
                ticket.AppendLine("PARA FACTURACION");
                ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
                ticket.AppendLine("RFC: " + credito.Rows[0]["rfc"].ToString().ToUpper());
                ticket.AppendLine("RAZON SOCIAL: " + credito.Rows[0]["razon_social"].ToString().ToUpper());
                ticket.AppendLine("--------------------------------------------------------");
            }
             * */

            //ticket.Append(POS_Control.font_size_64 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_64][ALIGN_CENTER]");
            ticket.AppendLine("[REIMPRESION]");

            //ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");

            ticket.AppendLine("COD   PRODUCTO                                  PRECIO U");
            ticket.AppendLine("CADUCIDAD                LOTE                       CANT");
            ticket.AppendLine("--------------------------------------------------------");

            foreach (DTO_Detallado_ventas_ticket detallado_ticket in venta_ticket.detallado_ventas_ticket)
            {
                ticket.AppendLine(string.Format("{0,5} {1,-37}{2,13:C4}",
                    detallado_ticket.amecop,
                    detallado_ticket.nombre,
                    detallado_ticket.precio_unitario)
                );

                foreach (Tuple<string, string, int> cad_lotes in detallado_ticket.caducidades_lotes)
                {
                    ticket.AppendLine(string.Format("{0,9} {1,-33}{2,13}",
                        HELPERS.Misc_helper.fecha(cad_lotes.Item1, "CADUCIDAD"),
                        HELPERS.Misc_helper.PadBoth(cad_lotes.Item2, 33),
                        cad_lotes.Item3)
                    );
                }

                ticket.AppendLine(string.Format("{0,16}{1,11:C2} - ({2,9})% = {3,11:C2}", "IMPORTE: ", (detallado_ticket.precio_unitario * detallado_ticket.cantidad), (detallado_ticket.descuento * 100), detallado_ticket.subtotal));

            }

            ticket.AppendLine("\n");
            ticket.AppendLine(string.Format("{0,56}", "-----------------"));
            ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "SUBTOTAL:", venta_ticket.subtotal));
            ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "IVA:", venta_ticket.iva));
            ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "IEPS:", venta_ticket.ieps));
            ticket.AppendLine(string.Format("{0,56}", "-----------------"));
            ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "TASA 0%:", venta_ticket.excento));

            string tasa_gravado = string.Format("TASA {0}%:", Misc_helper.pct_iva_global());

            ticket.AppendLine(string.Format("{0,38} {1,17:C2}", tasa_gravado, venta_ticket.gravado, Misc_helper.pct_iva_global()));
            ticket.AppendLine(string.Format("{0,56}", "-----------------"));
            ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "TOTAL:", venta_ticket.total));
            //ticket.Append(POS_Control.font_size_64 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_64][ALIGN_CENTER]");
            ticket.AppendLine("[REIMPRESION]");
            //ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            //ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
            ticket.AppendLine("--------------------------------------------------------");
            //ticket.AppendLine(POS_Control.align_center);
            ticket.Append("[ALIGN_CENTER][ALIGN_CENTER][ALIGN_CENTER]");
            //ticket.AppendLine(POS_Control.align_center);
            //ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_center);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_CENTER]");
            ticket.AppendLine("NOTA DE VENTA AL PUBLICO EN GENERAL EL PAGO SE REALIZA");
            ticket.AppendLine("EN UNA SOLA EXHIBICION Y SE INCLUYE EN LA NOTA FINAL");
            ticket.AppendLine("DEL DIA PARA EFECTOS FISCALES AL PAGO");
            ticket.AppendLine("REGIMEN GENERAL DE LEY PERSONAS MORALES");
            ticket.AppendLine("LUGAR DE EXPEDICION: MERIDA, YUC.");
            ticket.AppendLine("DOMICILIO FISCAL");
            ticket.AppendLine(sucursal_data.domicilio_fiscal.ToUpper());

            //ticket.AppendLine(POS_Control.align_center); ticket.AppendLine(POS_Control.align_center);
            ticket.Append("[ALIGN_CENTER][ALIGN_CENTER]");
            ticket.AppendLine(POS_Control.barcode(string.Format("{0}${1}", HELPERS.Config_helper.get_config_local("sucursal_id").PadLeft(3, '0'), venta_id.ToString().PadLeft(3, '0'))));
            POS_Control.finTicket(ticket);
        }

        public bool print()
        {
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "DEVOLUCION", venta_id, true, false, false, reimpresion);
        }

        public void Dispose()
        {

        }


    }
}
