﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.HELPERS;


namespace Farmacontrol_PDV.CLASSES.PRINT
{
	class Ticket_venta : IDisposable
	{
		private long venta_id;
		public StringBuilder ticket = new StringBuilder();
		DAO_Ventas dao_ventas = new DAO_Ventas();
        bool reimpresion = false;

		public void construccion_ticket(long venta_id, bool reimpresion = false)
		{
			this.venta_id = venta_id;
            this.reimpresion = reimpresion;

			DAO_Ventas dao_ventas = new DAO_Ventas();
			DAO_Clientes dao_clientes = new DAO_Clientes();
			DTO_Ventas_ticket venta_ticket = dao_ventas.get_informacion_ticket_venta(venta_id);

			//ticket.AppendLine(POS_Control.logo);
			//ticket.Append("[LOGO]");
            //ticket.AppendLine("[LOGO]");
			//ticket.AppendLine(POS_Control.font_condensed + POS_Control.font_size_0);

			//ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
			//ticket.AppendLine("--------------------------------------------------------");
			//ticket.Append(POS_Control.font_size_112 + POS_Control.align_center);
            //ticket.Append("[FONT_SIZE_32][FONT_CONDENSED][ALIGN_CENTER]");
            //ticket.AppendLine("ATENCION");
            //ticket.AppendLine("NO TENEMOS VENTA");
            //ticket.AppendLine("POR CAMBACEO");
            //ticket.Append("\n "); ticket.Append("\n ");
            ticket.Append("[FONT_SIZE_0][FONT_CONDENSED][ALIGN_CENTER]");
			ticket.AppendLine("VENTA");
			ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_CENTER]");
			//ticket.AppendLine("--------------------------------------------------------");
           
			DAO_Sucursales dao_sucursales = new DAO_Sucursales();
			string sucursal_id = HELPERS.Config_helper.get_config_local("sucursal_id");
			var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(sucursal_id));
			//ticket.Append(POS_Control.printer_reset + POS_Control.font_condensed + POS_Control.font_size_0);
			//ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");//original
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine(string.Format("{0}\n{1} COLONIA {2}\n{3} CP {4}, TEL: {5}", "EMERITA FARMACIAS "+sucursal_data.nombre, sucursal_data.direccion, sucursal_data.colonia, sucursal_data.ciudad, sucursal_data.codigo_postal, sucursal_data.telefono));

			//ticket.AppendLine(string.Format("TERMINAL: {0}", venta_ticket.nombre_terminal));
            ticket.AppendLine(string.Format("FOLIO: #{0}  FECHA: {1}", venta_ticket.venta_folio, Misc_helper.fecha(venta_ticket.fecha_terminado.ToString(), "LEGIBLE") ));

            if (venta_ticket.cotizacion_id != null)
            {
                ticket.AppendLine(string.Format("ATENDIO: {0}", venta_ticket.empleado_atendio));
            }
            else
            {
                ticket.AppendLine(string.Format("CAJERO: {0}",venta_ticket.nombre_empleado));
            }
			
			//ticket.AppendLine(string.Format("FECHA CREADO: {0}",Misc_helper.fecha(venta_ticket.fecha_creado.ToString(),"LEGIBLE")));
           //ticket.AppendLine(string.Format("FECHA: {0}", Misc_helper.fecha(venta_ticket.fecha_terminado.ToString(), "LEGIBLE")));
            
            if(venta_ticket.comentarios.Trim().Length > 0)
            {
                ticket.AppendLine(string.Format("COMENTARIOS: {0}",venta_ticket.comentarios.ToUpper()));
            }

			//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
			//ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");//original
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");

			if(venta_ticket.domicilio != "")
			{
				var domicilio = dao_clientes.get_domicilio_data(venta_ticket.domicilio);

				//ticket.Append(POS_Control.font_size_32 + POS_Control.align_center);
				ticket.Append("[FONT_SIZE_32][ALIGN_CENTER]");
				ticket.AppendLine("VENTA A DOMICILIO");
				//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0+POS_Control.align_left);
				ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
				ticket.AppendLine("NOMBRE: " + domicilio.Rows[0]["nombre"].ToString().ToUpper());
				ticket.AppendLine("TIPO: " + domicilio.Rows[0]["tipo"].ToString().ToUpper());
				ticket.AppendLine("DIRECCION: " + domicilio.Rows[0]["direccion"].ToString().ToUpper());
				ticket.AppendLine("TELEFONO: " + domicilio.Rows[0]["telefono"].ToString().ToUpper());

                string comentarios = domicilio.Rows[0]["comentarios"].ToString();

                if(comentarios.Trim().Length > 0)
                {
                    ticket.AppendLine("COMENTARIOS: "+comentarios);
                }
                
				ticket.AppendLine("--------------------------------------------------------");
			}

			if(venta_ticket.credito != "")
			{
				var credito = dao_clientes.get_cliente_credito_data(venta_ticket.credito);
				var domicilio_credito = dao_clientes.get_domicilio_default(venta_ticket.credito);

				//ticket.Append(POS_Control.font_size_32 + POS_Control.align_center);
				ticket.Append("[FONT_SIZE_32][ALIGN_CENTER]");
				ticket.AppendLine("VENTA A CREDITO");
				//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
				ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
                ticket.AppendLine(string.Format("DEBO Y PAGARE INCONDICIONALMENTE POR ESTE DOCUMENTO A LA ORDEN DE \"{0}\" A MAS TARDAR {1} LA CANTIDAD DE {2}, VALOR RECIBIDO A MI ENTERA SATISFACCION. LA FIRMA DE ESTE DOCUMENTO IMPLICA QUE AL NO PAGARSE A SU VENCIMIENTO, SE PODRA EXIGIR EL TOTAL DEL ADEUDO.", sucursal_data.razon_social, Convert.ToDateTime(Misc_helper.fecha()).AddDays(Convert.ToInt32(credito.Rows[0]["plazo"])).ToString("dd/MMM/yyyy").ToUpper().Replace(".", ""), venta_ticket.total.ToString("C2")));
				//ticket.AppendLine(POS_Control.font_size_16 + POS_Control.align_center);
				ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");
				ticket.Append("\n ");
				//ticket.Append(POS_Control.align_center);
				ticket.Append("[ALIGN_CENTER]");
				ticket.AppendLine("NOMBRE Y DATOS DEL DEUDOR");
				//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
				ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
				ticket.AppendLine("NOMBRE: " + credito.Rows[0]["nombre"].ToString());
				ticket.AppendLine("DIRECCION: " + domicilio_credito.direccion);
				ticket.AppendLine("TELEFONO: " + domicilio_credito.telefono);
				//ticket.Append(POS_Control.align_center);
				ticket.Append("[ALIGN_CENTER]");
				ticket.Append("\n "); 
				ticket.AppendLine("ACEPTO");
				ticket.Append("\n "); ticket.Append("\n "); ticket.Append("\n ");
				ticket.AppendLine("___________________________");
				ticket.AppendLine("FIRMA");
				ticket.AppendLine("--------------------------------------------------------");
			}

			/*
			if(venta_ticket.rfc_registro != "")
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
			//ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");

			ticket.AppendLine("COD   PRODUCTO                                  PRECIO U");
			ticket.AppendLine("CADUCIDAD                LOTE                       CANT");
			ticket.AppendLine("--------------------------------------------------------");

			foreach(DTO_Detallado_ventas_ticket detallado_ticket in venta_ticket.detallado_ventas_ticket)
			{
				ticket.AppendLine(string.Format("{0,5} {1,-37}{2,13:C2}",
					detallado_ticket.amecop,
					detallado_ticket.nombre,
					detallado_ticket.precio_unitario)
				);


                long cantidad_cad = 0;

				foreach(Tuple<string,string,int> cad_lotes in detallado_ticket.caducidades_lotes)
				{
					ticket.AppendLine(string.Format("{0,9} {1,-33}{2,13}",
                        (cad_lotes.Item1.Equals("SIN CAD") || cad_lotes.Item1.Equals("0000-00-00") || cad_lotes.Item1.Equals(" ")) ? "SIN CAD" : HELPERS.Misc_helper.fecha(cad_lotes.Item1, "CADUCIDAD"),
						HELPERS.Misc_helper.PadBoth(cad_lotes.Item2, 33),
						cad_lotes.Item3)
					);

                    cantidad_cad += cad_lotes.Item3;
				}

                decimal total_parcial_importe = (detallado_ticket.precio_unitario * cantidad_cad) - ((detallado_ticket.precio_unitario * cantidad_cad) * detallado_ticket.descuento);

				ticket.AppendLine(string.Format("{0,16}{1,11:C2} - {2,9:P2} = {3,14:C2}", "IMPORTE: ", (detallado_ticket.precio_unitario * detallado_ticket.cantidad), (detallado_ticket.descuento), total_parcial_importe));
			}

			//ticket.AppendLine("\n");
			//ticket.AppendLine(string.Format("{0,56}", "-----------------"));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "SUBTOTAL:", venta_ticket.subtotal));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "IVA:", venta_ticket.iva));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "IEPS:", venta_ticket.ieps));
			//ticket.AppendLine(string.Format("{0,56}", "-----------------"));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "TASA 0%:", venta_ticket.excento));

            string tasa_gravado = string.Format("TASA {0}%:", Misc_helper.pct_iva_global());

			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", tasa_gravado, venta_ticket.gravado));
			ticket.AppendLine(string.Format("{0,56}", "-----------------"));
			
            float precio_dolar = (float)dao_ventas.get_tipo_cambio();
            float dolar = (float)venta_ticket.total / precio_dolar;

           // ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "TOTAL:", venta_ticket.total));
            //ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "TOTAL US :", dolar));

            ticket.AppendLine(string.Format("{0,4}{1,11:C2}  {2,21} {3,17:C2}", "US", dolar, "TOTAL:", venta_ticket.total));

            ticket.AppendLine(POS_Control.align_center + " SON " + Misc_helper.NumtoLe(venta_ticket.total));
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");
            //ticket.AppendLine("--------------------------------------------------------");
            ticket.AppendLine("TIPO DE PAGO                        CUENTA         MONTO");
            //ticket.AppendLine("--------------------------------------------------------");
            var formas_pago = dao_ventas.get_tipos_pagos_venta(venta_id);

            decimal total_cambio = 0;

            foreach(DTO_Formas_pago pago in formas_pago)
            {
                if(pago.entrega_cambio)
                {
                    total_cambio += pago.monto - pago.importe;
                }

                ticket.AppendLine(string.Format("{0:33} {1:8} {2:13}",pago.nombre.PadRight(33,' ').Substring(0,33),pago.cuenta.PadLeft(8,' ').Substring(0,8).ToUpper(),pago.monto.ToString("C2").PadLeft(13,' ')));
                //ticket.AppendLine("//////////");
            }

            //ticket.AppendLine(string.Format("{0,56}", "-----------------"));
            ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "CAMBIO:", total_cambio));

            
            //ticket.Append(POS_Control.font_size_64 + POS_Control.align_center);
	        ticket.Append("[FONT_SIZE_64][ALIGN_CENTER]");
            ticket.AppendLine("[REIMPRESION]");

			//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
			//ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");ORIGINAL
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0);
			ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
			//ticket.AppendLine("--------------------------------------------------------");
			//ticket.AppendLine(POS_Control.align_center);
         

			ticket.Append("[ALIGN_CENTER]");
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_CENTER]");

            ticket.AppendLine(string.Format("{0,38}{1,17:C2}", " USD BUY :", precio_dolar));
           // ticket.AppendLine("--------------------------------------------------------");
			//ticket.AppendLine(POS_Control.align_center);
			ticket.Append("[ALIGN_CENTER]");
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_center);
			ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_CENTER]");
            ticket.AppendLine("NOTA DE VENTA AL PUBLICO EN GENERAL EL PAGO SE REALIZA EN UNA SOLA EXHIBICION Y SE INCLUYE EN LA FACTURA FINAL DEL DIA PARA EFECTOS FISCALES AL PAGO REGIMEN GENERAL DE LEY PERSONAS MORALES");			//ticket.AppendLine("EN UNA SOLA EXHIBICION Y SE INCLUYE EN LA FACTURA FINAL");
			//ticket.AppendLine("DEL DIA PARA EFECTOS FISCALES AL PAGO");
			//ticket.AppendLine("REGIMEN GENERAL DE LEY PERSONAS MORALES");
			ticket.AppendLine( string.Format("MERIDA, YUC.     RFC: {0}", sucursal_data.rfc)  );
            //ticket.AppendLine("DOMICILIO FISCAL");
            //ticket.AppendLine(string.Format("RFC: {0}", sucursal_data.rfc));
			ticket.AppendLine(sucursal_data.domicilio_fiscal.ToUpper());
            //ticket.AppendLine("COMENTARIOS Y SUGERENCIAS");
            ticket.AppendLine("www.emeritafarmacias.com");
            ticket.Append("[ALIGN_CENTER][ALIGN_CENTER]");
			//ticket.AppendLine(POS_Control.align_center); ticket.AppendLine(POS_Control.align_center);
			ticket.Append("[ALIGN_CENTER][ALIGN_CENTER]");
            
            string tmpsucursal = Config_helper.get_config_local("sucursal_id").Trim().PadLeft(3, '0');

            if( this.reimpresion == false)
            { 
                ticket.AppendLine(POS_Control.barcode(string.Format("{0}${1}", tmpsucursal, venta_id.ToString().PadLeft(3, '0'))));
                ticket.AppendLine(POS_Control.abrir_cajon);
            }
            POS_Control.finTicket(ticket);
		}

		public bool print()
		{
            var venta_data = dao_ventas.get_venta_data(venta_id);
            return HELPERS.Print_new_helper.print(venta_data.terminal_id, ticket.ToString(), "VENTA", venta_data.venta_folio, true,false,false,reimpresion);
		}

		public void Dispose()
		{

		}

        
	}
}