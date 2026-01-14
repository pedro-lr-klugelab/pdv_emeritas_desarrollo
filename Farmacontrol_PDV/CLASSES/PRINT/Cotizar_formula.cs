using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
	class Cotizar_formula
	{
		private StringBuilder ticket = new StringBuilder();

		public void construccion_ticket(string cliente, string doctor, List<DTO_Detallado_formulas> detallado, string comentarios)
		{
			//ticket.AppendLine(POS_Control.font_size_32 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_32][ALIGN_CENTER]");
			ticket.AppendLine("FORMULA MAGISTRAL");
			//ticket.AppendLine(POS_Control.font_size_0 + POS_Control.align_left);
            ticket.Append("[FONT_SIZE_0][ALIGN_LEFT]");
			ticket.AppendLine("--------------------------------------------------------");
			ticket.AppendLine("CLIENTE: "+cliente);
			ticket.AppendLine("DOCTOR: "+doctor);
			ticket.AppendLine("--------------------------------------------------------");
			ticket.AppendLine("COD   PRODUCTO                         CANT     PRECIO U");
			ticket.AppendLine("--------------------------------------------------------");

			decimal subtotal = 0;
			decimal total = 0;

			foreach (DTO_Detallado_formulas det_ticket in detallado)
			{
				ticket.AppendLine(string.Format("{0,5} {1,-27} {2,8} {3,13:C2}",
					(det_ticket.articulo_id != null) ? (det_ticket.amecop.Length > 4) ? "*"+det_ticket.amecop.Substring(det_ticket.amecop.Length -4,4) : "*"+det_ticket.amecop : "*N/A",
					(det_ticket.nombre.Length > 27) ? det_ticket.nombre.Substring(0, 27) : det_ticket.nombre,
					det_ticket.cantidad.ToString("#.##"),
					det_ticket.subtotal.ToString("C2"))
				);
				
				if(det_ticket.comentarios.Trim().Length > 0)
				{
					ticket.AppendLine(string.Format("@NOTA: {0}", det_ticket.comentarios));	
				}

				subtotal += det_ticket.subtotal;
				total += det_ticket.total;
			}

			ticket.AppendLine("\n");
			ticket.AppendLine(string.Format("{0,56}", "-----------------"));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "SUBTOTAL:", subtotal));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "IVA:", Convert.ToDecimal(subtotal * Convert.ToDecimal(0.16))));
			ticket.AppendLine(string.Format("{0,56}", "-----------------"));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "TOTAL:", total));

			if(comentarios.Trim().Length > 0)
			{
				ticket.AppendLine("--------------------------------------------------------");
				//ticket.AppendLine(POS_Control.font_size_32 + POS_Control.align_center);
                ticket.Append("[FONT_SIZE_32][ALIGN_CENTER]");
				ticket.AppendLine("NOTAS");
				//ticket.AppendLine(POS_Control.font_size_0 + POS_Control.align_left);
                ticket.Append("[FONT_SIZE_0][ALIGN_LEFT]");
				ticket.AppendLine(comentarios);
			}

			ticket.AppendLine("--------------------------------------------------------");
            POS_Control.finTicket(ticket);
		}

        public void construccion_ticket(string formula_id, int sucursal_id)

        {
            DAO_Formulas dao_formula = new DAO_Formulas();
			DAO_Empleados dao_empleados = new DAO_Empleados();

			var informacion_formula  =  dao_formula.get_informacion_formula(formula_id);
			var informacion_empleado = dao_empleados.get_empleado_data((int)informacion_formula.empleado_id);
			var detallado = dao_formula.get_detallado_formulas(formula_id);
			//ticket.AppendLine(POS_Control.logo);
			//ticket.AppendLine(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
			//ticket.AppendLine(POS_Control.font_size_32 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_32][ALIGN_CENTER]");
			ticket.AppendLine("FORMULA MAGISTRAL");
			//ticket.AppendLine(POS_Control.font_size_0 + POS_Control.align_left);
            ticket.Append("[FONT_SIZE_0][ALIGN_LEFT]");
			ticket.AppendLine("--------------------------------------------------------");
			ticket.AppendLine("FOLIO: #"+informacion_formula.sucursal_folio.ToString());
			ticket.AppendLine("CLIENTE: " + informacion_formula.nombre_cliente);
			ticket.AppendLine("DOCTOR: " + informacion_formula.nombre_doctor);
			ticket.AppendLine("EMPLEADO: "+ informacion_empleado.Nombre);
			ticket.AppendLine("--------------------------------------------------------");
			ticket.AppendLine("COD   PRODUCTO                         CANT     PRECIO U");
			ticket.AppendLine("--------------------------------------------------------");

			decimal subtotal = 0;
			decimal importe_iva = 0;
			decimal total = 0;

			
			foreach (DTO_Detallado_formulas det_ticket in detallado)
			{
				ticket.AppendLine(string.Format("{0,5} {1,-27} {2,8} {3,13:C2}",
					(det_ticket.articulo_id != null) ? (det_ticket.amecop.Length > 4) ? "*" + det_ticket.amecop.Substring(det_ticket.amecop.Length - 4, 4) : "*" + det_ticket.amecop : "*N/A",
					(det_ticket.nombre.Length > 27) ? det_ticket.nombre.Substring(0, 27) : det_ticket.nombre,
					det_ticket.cantidad.ToString("#.##"),
					det_ticket.subtotal.ToString("C2"))
				);

				if (det_ticket.comentarios.Trim().Length > 0)
				{
					ticket.AppendLine(string.Format("@NOTA: {0}", det_ticket.comentarios));
				}

				subtotal += det_ticket.subtotal;
				total += det_ticket.total;
			}

			importe_iva = Convert.ToDecimal(subtotal * Convert.ToDecimal(0.16));

			ticket.AppendLine("\n");
			ticket.AppendLine(string.Format("{0,56}", "-----------------"));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "SUBTOTAL:", subtotal));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "IVA:", importe_iva));
			ticket.AppendLine(string.Format("{0,56}", "-----------------"));
			ticket.AppendLine(string.Format("{0,38} {1,17:C2}", "TOTAL:", total));

			if (informacion_formula.comentarios.Trim().Length > 0)
			{
				ticket.AppendLine("--------------------------------------------------------");
				//ticket.AppendLine(POS_Control.font_size_32 + POS_Control.align_center);
                ticket.Append("[FONT_SIZE_32][ALIGN_CENTER]");
				ticket.AppendLine("NOTAS");
				//ticket.AppendLine(POS_Control.font_size_0 + POS_Control.align_left);
                ticket.Append("[FONT_SIZE_0][ALIGN_LEFT]");
				ticket.AppendLine(informacion_formula.comentarios);
			}

			ticket.AppendLine("--------------------------------------------------------");
            ticket.Append("[ALIGN_CENTER][ALIGN_CENTER]");
            ticket.AppendLine(POS_Control.barcode(string.Format("{0}${1}", sucursal_id.ToString().PadLeft(3, '0'), informacion_formula.sucursal_folio.ToString().PadLeft(3, '0'))));
            POS_Control.finTicket(ticket);
		}

		public bool print()
		{
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "COTIZAR_FORMULA", 0, true); 
		}

		public void Dispose()
		{
			ticket = new StringBuilder();
		}

       
	}
}
