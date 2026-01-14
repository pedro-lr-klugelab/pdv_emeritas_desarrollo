using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
	class Caducidades
	{
		private StringBuilder ticket = new StringBuilder();

		public void construccion_ticket(int numero_meses)
		{
			DAO_Caducidades dao = new DAO_Caducidades();
			var caducidades = dao.get_lista_caducidades(numero_meses);

			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_48 + POS_Control.align_center);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_48][ALIGN_CENTER]");
			ticket.AppendLine("PROXIMAS");
			ticket.AppendLine("CADUCIDADES");
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_CENTER]");

			ticket.AppendLine("--------------------------------------------------------");
			ticket.AppendLine("COD   PRODUCTO                             CAD        EX");
			ticket.AppendLine("--------------------------------------------------------");

			Dictionary<long,long> control = new Dictionary<long,long>();

			foreach (var detallado in caducidades)
			{
				if(!control.ContainsKey(detallado.articulo_id))
				{
					List<Tuple<string, string, string, long>> lista_impresion = new List<Tuple<string, string, string, long>>();

					foreach (var detallado_content in caducidades)
					{
						if (detallado_content.articulo_id == detallado.articulo_id)
						{

							Tuple<string, string, string, long> tup = new Tuple<string, string, string, long>(
								detallado_content.amecop,
								detallado_content.producto,
								detallado_content.caducidad,
								detallado_content.existencia
							);

							lista_impresion.Add(tup);
						}
					}

					if (lista_impresion.Count > 1)
					{
						int count = 1;

						foreach (Tuple<string, string, string, long> tuple in lista_impresion)
						{
							string amecop = tuple.Item1.ToString();

							string producto_vacio = "";

							ticket.AppendLine(string.Format("{0:5} {1:31} {2,8} {3,9}",
								(count.Equals(1)) ? "*" + amecop.Substring(amecop.Length - 4, 4) : producto_vacio.PadRight(5,' '),
								(count.Equals(1)) ? (tuple.Item2.Length > 31) ? tuple.Item2.Substring(0, 31) : tuple.Item2.PadRight(31, ' ') : producto_vacio.PadRight(31,' '),
								tuple.Item3,
								tuple.Item4)
							);

							count++;
						}
					}
					else
					{
						var tuple = lista_impresion[0];
						string amecop = tuple.Item1.ToString();

						ticket.AppendLine(string.Format("{0:5} {1:31} {2,8} {3,9}",
							"*" + amecop.Substring(amecop.Length - 4, 4),
							(tuple.Item2.Length > 31) ? tuple.Item2.Substring(0, 31) : tuple.Item2.PadRight(31, ' '),
							tuple.Item3,
							tuple.Item4)
						);
					}

					control.Add(detallado.articulo_id,detallado.existencia);
				}
			}

			ticket.AppendLine("--------------------------------------------------------");
            POS_Control.finTicket(ticket);
		}

		public bool print()
		{
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "CADUCIDADES", 0, true);
		}

       
	}
}
