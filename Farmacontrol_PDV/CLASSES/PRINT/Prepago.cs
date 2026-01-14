using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DAO;
using System.Data;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
	class Prepago
	{
		StringBuilder ticket = new StringBuilder();
		DAO_Prepago dao_prepago = new DAO_Prepago();
		DTO_Prepago prepago = new DTO_Prepago();
        DTO_Cliente_domicilio_data cliente_dom_data = new DTO_Cliente_domicilio_data();
		List<DTO_Detallado_prepago> detallado_prepago = new List<DTO_Detallado_prepago>();

		public void construccion_ticket(long prepago_id)
		{
			prepago = dao_prepago.get_informacion_prepago(prepago_id);
			detallado_prepago = dao_prepago.get_detallado_prepago(prepago_id);

			DAO_Sucursales dao_sucursales = new DAO_Sucursales();
			string sucursal_id = HELPERS.Config_helper.get_config_local("sucursal_id");
			var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(sucursal_id));

            DTO_Cliente_domicilios cte_domicilios = new DTO_Cliente_domicilios();

            DAO_Clientes dao_clientes = new DAO_Clientes();

            

			//ticket.AppendLine(POS_Control.logo);
            ticket.Append("[LOGO]");
			//ticket.Append(POS_Control.printer_reset + POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE]");
			ticket.AppendLine(string.Format("{0} - RFC:{1}\n{2}\n{3} COLONIA {4}\n{5} CP {6}, TEL: {7}", sucursal_data.razon_social, sucursal_data.rfc, sucursal_data.nombre, sucursal_data.direccion, sucursal_data.colonia, sucursal_data.ciudad, sucursal_data.codigo_postal, sucursal_data.telefono));

            ticket.AppendLine("FECHA: " + Misc_helper.fecha(prepago.fecha_pago.ToString(), "LEGIBLE"));
			ticket.AppendLine("EMPLEADO: " + prepago.nombre_empleado.ToUpper());
			
            if (prepago.cliente_id != "")
            {
                ticket.AppendLine("CLIENTE: " + prepago.nombre_cliente.ToUpper());
                cte_domicilios = dao_clientes.get_domicilio_default(prepago.cliente_id);

                cliente_dom_data = dao_clientes.get_domicilio_data_object(cte_domicilios.cliente_domicilio_id);

                ticket.AppendLine(cliente_dom_data.calle + " " + cliente_dom_data.numero_exterior + " " + cliente_dom_data.colonia + " " + cliente_dom_data.ciudad);
            }
            else
            {
                ticket.AppendLine("CLIENTE: N/A");
            }
            
			ticket.AppendLine("********************************************************");
			//ticket.Append(POS_Control.printer_reset + POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
			//ticket.Append(POS_Control.font_size_0 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_0][ALIGN_CENTER]");
			ticket.AppendLine("ESTE PREPAGO AMPARA MERCANCIA EQUIVALENTE A:");
			//ticket.Append(POS_Control.align_center + POS_Control.font_size_48);
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_48]");
			ticket.AppendLine(string.Format("{0:C2}", prepago.monto));
			//ticket.Append(POS_Control.align_center + POS_Control.font_size_0);
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");
			ticket.AppendLine("PESOS MXN EN LA SUCURSAL * "+sucursal_data.nombre+" *");
			ticket.AppendLine("********************************************************");
			//ticket.Append(POS_Control.align_center + POS_Control.font_size_32);
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_32]");
			ticket.AppendLine("ENCARGO PREPAGADO");
			//ticket.Append(POS_Control.align_center + POS_Control.font_size_0);
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");
			ticket.AppendLine("********************************************************");
			//ticket.Append(POS_Control.font_size_16);
            ticket.Append("[FONT_SIZE_16]");
			ticket.AppendLine("PRODUCTOS ENCARGADOS");
			//ticket.Append(POS_Control.align_center + POS_Control.font_size_0);
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			ticket.AppendLine("COD   PRODUCTO                          CANT      PRECIO");
			ticket.AppendLine("--------------------------------------------------------");
			//ticket.Append(POS_Control.align_center + POS_Control.font_size_0);
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");

			foreach (DTO_Detallado_prepago row in detallado_prepago)
			{
                ///ticket.AppendLine(string.Format("{0,5} {1,-28} {2,13:C2} {3,7}",
                ///((row.precio_publico * row.pct_iva) + row.precio_publico)
                decimal importe = row.cantidad * row.precio_publico;

                decimal importe_descuento = importe - ( importe * row.pct_descuento  );

                decimal subtotal = importe_descuento + ( importe_descuento * row.pct_iva );

                ticket.AppendLine(string.Format("{0,5} {1,-28} {2,7} {3,13:C2}",
					row.amecop,
					row.producto.Substring(0, (row.producto.Length >= 28) ? 28 : row.producto.Length),
                    row.cantidad,
					subtotal
					
				));
			}

			ticket.AppendLine("--------------------------------------------------------");
			//ticket.Append(POS_Control.align_center + POS_Control.font_size_48);
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_48]");
			ticket.AppendLine("COPIA CLIENTE");
			//ticket.Append(POS_Control.font_size_16);
            ticket.Append("[FONT_SIZE_16]");
			ticket.AppendLine("NO VALIDO SIN SELLO");
			//ticket.Append(POS_Control.align_center + POS_Control.font_size_0);
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");
			ticket.AppendLine("********************************************************");
			ticket.AppendLine("*                                                      *");
			ticket.AppendLine("*                                                      *");
			ticket.AppendLine("*                                                      *");
			ticket.AppendLine("*                                                      *");
			ticket.AppendLine("*                                                      *");
			ticket.AppendLine("*                                                      *");
			ticket.AppendLine("*                                                      *");
			ticket.AppendLine("*                                                      *");
			ticket.AppendLine("*                                                      *");
			ticket.AppendLine("*                                                      *");
			ticket.AppendLine("*                                                      *");
			ticket.AppendLine("********************************************************");
			ticket.AppendLine("ESTE PREPAGO ES CANJEABLE EXCLUSIVAMENTE POR PRODUCTOS Y");
			ticket.AppendLine("NO CAUSA ENTREGA DE CAMBIO.\n");
			ticket.AppendLine("AL MOMENTO DE SU CANJE ESTE PREPAGO PERDERA SU VALOR Y");
			ticket.AppendLine("SERA RETENIDO POR LA SUCURSAL.");

			ticket.AppendLine("LUGAR DE EXPEDICION: MERIDA, YUC.");

            int sucursal_mod = Convert.ToInt32(sucursal_id);

            string hash = string.Format("{0}${1}", sucursal_mod, prepago.codigo);
			ticket.AppendLine(sucursal_data.domicilio_fiscal);
			ticket.Append("\n");
			ticket.AppendLine(POS_Control.code39(hash));

            POS_Control.finTicket(ticket);
		}

		public bool print()
		{
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "ENCARGO PREPAGADO", prepago.prepago_id, true);
		}
	}

	class Prepago_sucursal
	{
		StringBuilder ticket = new StringBuilder();
		DAO_Prepago dao_prepago = new DAO_Prepago();
		DTO_Prepago prepago = new DTO_Prepago();
		List<DTO_Detallado_prepago> detallado_prepago = new List<DTO_Detallado_prepago>();
        DTO_Cliente_domicilio_data cliente_dom_data = new DTO_Cliente_domicilio_data();

		public void construccion_ticket(long prepago_id, string copia="COPIA FARMACIA")
		{
			prepago = dao_prepago.get_informacion_prepago(prepago_id);
			detallado_prepago = dao_prepago.get_detallado_prepago(prepago_id);
			DAO_Sucursales dao_sucursales = new DAO_Sucursales();

            DTO_Cliente_domicilios cte_domicilios = new DTO_Cliente_domicilios();

            DAO_Clientes dao_clientes = new DAO_Clientes();

			string sucursal_id = HELPERS.Config_helper.get_config_local("sucursal_id");
			var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(sucursal_id));
			//ticket.AppendLine(POS_Control.logo);
            ticket.Append("[LOGO]");
			//ticket.Append(POS_Control.printer_reset + POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine(string.Format("{0} - RFC:{1}\n{2}\n{3} COLONIA {4}\n{5} CP {6}, TEL: {7}", sucursal_data.razon_social, sucursal_data.rfc, sucursal_data.nombre, sucursal_data.direccion, sucursal_data.colonia, sucursal_data.ciudad, sucursal_data.codigo_postal, sucursal_data.telefono));
            ticket.AppendLine("FECHA: " + Misc_helper.fecha(prepago.fecha_pago.ToString(), "LEGIBLE"));
			ticket.AppendLine("EMPLEADO: " + prepago.nombre_empleado.ToUpper());
            if (prepago.cliente_id != "")
            {
                ticket.AppendLine("CLIENTE: " + prepago.nombre_cliente.ToUpper());
                cte_domicilios = dao_clientes.get_domicilio_default(prepago.cliente_id);

                cliente_dom_data = dao_clientes.get_domicilio_data_object(cte_domicilios.cliente_domicilio_id);

                ticket.AppendLine(cliente_dom_data.calle + " " + cliente_dom_data.numero_exterior + " " + cliente_dom_data.colonia + " " + cliente_dom_data.ciudad);
            }
            else
            {
                ticket.AppendLine("CLIENTE: N/A");
            }
			ticket.AppendLine("********************************************************");
            //ticket.Append(POS_Control.printer_reset + POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[PRINTER_RESET][FONT_CONDENSED][FONT_SIZE_0]");
			//ticket.Append(POS_Control.font_size_0 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_0][ALIGN_CENTER]");
			ticket.AppendLine("EL INGRESO PREPAGADO FUE EQUIVALENTE A:");
			//ticket.Append(POS_Control.align_center + POS_Control.font_size_48);
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_48]");
			ticket.AppendLine(string.Format("{0:C2}", prepago.monto));
			//ticket.Append(POS_Control.align_center + POS_Control.font_size_0);
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");
			ticket.AppendLine("********************************************************");
			//ticket.Append(POS_Control.align_center + POS_Control.font_size_32);
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_32]");
			ticket.AppendLine("ENCARGO PREPAGADO");
			//ticket.Append(POS_Control.align_center + POS_Control.font_size_0);
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");
			ticket.AppendLine("********************************************************");
			//ticket.Append(POS_Control.align_center + POS_Control.font_size_48);
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_48]");
			ticket.AppendLine(copia);
			//ticket.Append(POS_Control.align_center + POS_Control.font_size_0);
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");

            if(prepago.comentario.Trim().Length > 0)
            {
                ticket.AppendLine("COMENTARIO: " + prepago.comentario);
            }
			
			//ticket.Append(POS_Control.align_center + POS_Control.font_size_0);
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			//ticket.Append(POS_Control.font_size_16);
            ticket.Append("[FONT_SIZE_16]");
			ticket.AppendLine("PRODUCTOS ENCARGADOS");
			//ticket.Append(POS_Control.align_center + POS_Control.font_size_0);
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
            ticket.AppendLine("COD   PRODUCTO                          CANT     PRECIO ");
			ticket.AppendLine("--------------------------------------------------------");
			//ticket.Append(POS_Control.align_center + POS_Control.font_size_0);
            ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");

			foreach (DTO_Detallado_prepago row in detallado_prepago)
			{

                decimal importe = row.cantidad * row.precio_publico;

                decimal importe_descuento = importe - ( importe * row.pct_descuento  );

                decimal subtotal = importe_descuento + ( importe_descuento * row.pct_iva );

                //ticket.AppendLine(string.Format("{0,5} {1,-28} {2,13:C2} {3,7}",
                ticket.AppendLine(string.Format("{0,5} {1,-28} {2,7} {3,13:C2}",
					row.amecop,
					row.producto.Substring(0, (row.producto.Length >= 28) ? 28 : row.producto.Length),
                    row.cantidad,
					subtotal
					
				));
			}

			ticket.AppendLine("--------------------------------------------------------");

			ticket.AppendLine(sucursal_data.domicilio_fiscal);
			ticket.Append("\n");
			ticket.Append("\n");
			ticket.Append("\n");

            POS_Control.finTicket(ticket);
		}

		public bool print()
		{
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "ENCARGO PREPAGADO", prepago.prepago_id, true);
		}

        

	}
}
