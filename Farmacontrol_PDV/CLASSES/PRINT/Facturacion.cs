using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
	class Facturacion
	{
		private StringBuilder ticket = new StringBuilder();
		DAO_Ventas dao_ventas = new DAO_Ventas();
		private FacturaWSP factura_wsp;
		private long venta_id;
        bool reimpresion = false;

		public void construccion_ticket(long venta_id, FacturaWSP factura_wsp, bool reimpresion, bool es_nc = false)
		{
			this.venta_id = venta_id;
			this.factura_wsp = factura_wsp;
            this.reimpresion = reimpresion;

			DAO_Ventas dao_ventas = new DAO_Ventas();
			DAO_Clientes dao_clientes = new DAO_Clientes();
			DTO_Ventas_ticket venta_ticket = dao_ventas.get_informacion_ticket_venta(venta_id);

			/*ticket.AppendLine(POS_Control.logo);*/
            //ticket.Append("[LOGO]");
			/*ticket.AppendLine(POS_Control.font_condensed + POS_Control.font_size_0);*/
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");

			if(es_nc)
			{
				/*ticket.Append(POS_Control.font_size_80 + POS_Control.align_center);*/
                ticket.Append("[FONT_SIZE_80][ALIGN_CENTER]");
				ticket.AppendLine("NOTA DE");
				ticket.AppendLine("CREDITO");
				/*ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);*/
                ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
				ticket.AppendLine("--------------------------------------------------------");
			}
			else
			{
                /*ticket.Append(POS_Control.font_size_80 + POS_Control.align_center);*/
				ticket.Append("[FONT_SIZE_80][ALIGN_CENTER]");
				ticket.AppendLine("FACTURA");
                if(factura_wsp.fecha_cancelado != null)
                {
                    ticket.Append("CANCELADA");
                }

				/*ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);*/
                ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
				ticket.AppendLine("--------------------------------------------------------");
			}	
			
			DAO_Sucursales dao_sucursales = new DAO_Sucursales();
			string sucursal_id = HELPERS.Config_helper.get_config_local("sucursal_id");
			var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(sucursal_id));
			
			/*ticket.Append(POS_Control.printer_reset + POS_Control.font_condensed + POS_Control.font_size_0);*/
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine(string.Format("{0}RFC:{1}\n{2}\n{3} COLONIA {4}\n{5} CP {6}, TEL: {7}", "", sucursal_data.rfc, sucursal_data.nombre, sucursal_data.direccion, sucursal_data.colonia, sucursal_data.ciudad, sucursal_data.codigo_postal, sucursal_data.telefono));
			
			ticket.AppendLine(string.Format("FOLIO: #{0}", venta_ticket.venta_folio));
			ticket.AppendLine(string.Format("TERMINAL: {0}", venta_ticket.nombre_terminal));
			ticket.AppendLine(string.Format("EMPLEADO: {0}", venta_ticket.nombre_empleado));
            ticket.AppendLine(string.Format("FECHA CREADO: {0}", Misc_helper.fecha(venta_ticket.fecha_creado.ToString(),"LEGIBLE")));
            ticket.AppendLine(string.Format("FECHA TERMINADO: {0}", Misc_helper.fecha(venta_ticket.fecha_terminado.ToString(),"LEGIBLE")));
            ticket.AppendLine(string.Format("FECHA TIMBRADO: {0}", Misc_helper.fecha(factura_wsp.fecha_timbrado.ToString(), "LEGIBLE")));
			/*ticket.Append(POS_Control.align_left + POS_Control.font_size_0);*/
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");


            DAO_Rfcs dao_rfcs = new DAO_Rfcs();
            //var data = dao_rfcs.get_data_rfc_rfc(factura_wsp.rfc_receptor);

            /*ticket.Append(POS_Control.font_size_32 + POS_Control.align_center);*/
            ticket.Append("[FONT_SIZE_32][ALIGN_CENTER]");
            ticket.AppendLine("DATOS FISCALES");
            /*ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);*/
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");

            ticket.AppendLine("RFC: "+factura_wsp.rfc_receptor);
            ticket.AppendLine("RAZON SOCIAL: " + factura_wsp.razon_social);
            //ticket.AppendLine("DOMICILIO FISCAL: "+string.Format("CALLE {0} NUM {1}, {2}. {3}, {4} ",data.calle, data.numero_exterior,data.colonia,data.ciudad,data.estado));
            ticket.AppendLine("DOMICILIO FISCAL: " + string.Format("CALLE {0} NUM {1}, {2}. {3}, {4} ", factura_wsp.calle, factura_wsp.numero_exterior, factura_wsp.colonia, factura_wsp.ciudad, factura_wsp.estado));
            ticket.AppendLine("--------------------------------------------------------");

			if (venta_ticket.domicilio != "")
			{
				var domicilio = dao_clientes.get_domicilio_data(venta_ticket.domicilio);

				/*ticket.Append(POS_Control.font_size_32 + POS_Control.align_center);*/
                ticket.Append("[FONT_SIZE_32][ALIGN_CENTER]");
				ticket.AppendLine("VENTA A DOMICILIO");
				/*ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);*/
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

				/*ticket.Append(POS_Control.font_size_32 + POS_Control.align_center);*/
                ticket.Append("[FONT_SIZE_32][ALIGN_CENTER]");
				ticket.AppendLine("VENTA A CREDITO");
				/*ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);*/
                ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
				ticket.AppendLine("NOMBRE: " + credito.Rows[0]["nombre"].ToString().ToUpper());
				ticket.AppendLine("DIRECCION: " + credito.Rows[0]["direccion"].ToString().ToUpper());
				ticket.AppendLine("TELEFONO: " + credito.Rows[0]["telefono"].ToString().ToUpper());
				/*ticket.Append(POS_Control.align_center);*/
                ticket.Append("[ALIGN_CENTER]");
				ticket.Append("\n "); ticket.Append("\n "); ticket.Append("\n ");
				ticket.AppendLine("___________________________");
				ticket.AppendLine("firma");
				ticket.AppendLine("--------------------------------------------------------");
			}

            /*ticket.Append(POS_Control.font_size_64 + POS_Control.align_center);*/
            ticket.Append("[FONT_SIZE_64][ALIGN_CENTER]");
            ticket.AppendLine("[REIMPRESION]");

			/*ticket.Append(POS_Control.align_left + POS_Control.font_size_0);*/
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
						HELPERS.Misc_helper.fecha(cad_lotes.Item1,"CADUCIDAD"),
						HELPERS.Misc_helper.PadBoth(cad_lotes.Item2, 33),
						cad_lotes.Item3)
					);
				}

                Decimal import = (Convert.ToDecimal(detallado_ticket.precio_unitario) * Convert.ToInt32(detallado_ticket.cantidad));

                Decimal import_desc = import * (Convert.ToDecimal(detallado_ticket.descuento));

                Decimal subtotal_calculado = import - import_desc;

                ticket.AppendLine(string.Format("{0,16}{1,11:C2} - ({2,9})% = {3,11:C2}", "IMPORTE: ", (detallado_ticket.precio_unitario * detallado_ticket.cantidad), (detallado_ticket.descuento * 100), subtotal_calculado));

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
            }

            ticket.AppendLine(string.Format("{0,56}", "-----------------"));
            ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "CAMBIO:", total_cambio));

			if (reimpresion) { 
                /*ticket.Append(POS_Control.font_size_64 + POS_Control.align_center);*/
                ticket.Append("[FONT_SIZE_64][ALIGN_CENTER]");
                ticket.AppendLine("** COPIA **"); 
            }
			/*ticket.Append(POS_Control.align_left + POS_Control.font_size_0);*/
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");

			ticket.AppendLine("--------------------------------------------------------");

			/*
			 * CONCAT_WS('|','|',version,uuid,fecha_timbrado,sello_digital,certificado_sat,'|') AS cadena_original,
				sello_digital,
				sello_sat AS timbre_fiscal,
				CONCAT('?re=',rfc_emisor,'&rr=',rfc_receptor,'&tt=',importe_total,'&id=',uuid) AS codigo_qr
			 * */

			/*ticket.Append(POS_Control.font_size_16 + POS_Control.align_center);*/
            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
			ticket.AppendLine("CADENA ORIGINAL");
			/*ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);*/
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_CENTER]");
			ticket.AppendLine(string.Format("||{0}|{1}|{2}|{3}|{4}||",factura_wsp.version,factura_wsp.uuid,factura_wsp.fecha_timbrado,factura_wsp.sello_digital,factura_wsp.certificado_sat));
			ticket.AppendLine("--------------------------------------------------------");
			/*ticket.Append(POS_Control.font_size_16 + POS_Control.align_center);*/
            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
			ticket.AppendLine("SELLO DIGITAL");
			/*ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);*/
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
			ticket.AppendLine(factura_wsp.sello_digital);
			ticket.AppendLine("--------------------------------------------------------");
			/*ticket.Append(POS_Control.font_size_16 + POS_Control.align_center);*/
            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
			ticket.AppendLine("SELLO DIGITAL DEL SAT");
			/*ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);*/
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
			ticket.AppendLine(factura_wsp.sello_sat);
			ticket.AppendLine("--------------------------------------------------------");
			/*ticket.Append(POS_Control.font_size_16 + POS_Control.align_center);*/
            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
			ticket.AppendLine(POS_Control.qr_code(string.Format("?re={0}&rr={1}&tt={2}&id={3}",factura_wsp.rfc_emisor,factura_wsp.rfc_receptor,venta_ticket.total,factura_wsp.uuid)));
			/*ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0);*/
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine("ESTE DOCUMENTO ES UNA REPRESENTACION IMPRESA DE UN CFDI");
			ticket.AppendLine("CUALQUIER CORRECCION A ESTA FACTURA DEBERA REALIZARSE");
			ticket.AppendLine("DURANTE EL MISMO MES Y AÑO DE SU EMISION.");
			
            /*ticket.AppendLine(POS_Control.align_center);*/
            ticket.Append("[ALIGN_CENTER]");
            /*ticket.AppendLine(POS_Control.align_center);*/
            ticket.Append("[ALIGN_CENTER]");

            string tmpsucursal = Config_helper.get_config_local("sucursal_id").Trim().PadLeft(3, '0');

            //ticket.AppendLine(POS_Control.barcode(string.Format("{0}${1}", Config_helper.get_config_local("sucursal_id").PadLeft(3, '0'), venta_id.ToString().PadLeft(3, '0'))));
            ticket.AppendLine(POS_Control.barcode(string.Format("{0}${1}", tmpsucursal, venta_id.ToString().PadLeft(3, '0'))));

            POS_Control.finTicket(ticket);
		}

		public bool print()
		{
            var venta_data = dao_ventas.get_venta_data(venta_id);
            return HELPERS.Print_new_helper.print(venta_data.terminal_id, ticket.ToString(), "FACTURACION", venta_data.venta_folio, true, false,false,reimpresion);
		}

		public void Dispose()
		{

		}
	}
}
