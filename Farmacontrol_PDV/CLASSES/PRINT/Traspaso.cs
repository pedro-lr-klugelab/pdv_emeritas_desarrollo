using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.HELPERS;
using System.Data;
using Farmacontrol_PDV.DTO;

namespace Farmacontrol_PDV.CLASSES.PRINT
{
	class Diferencias
	{
		private long traspaso_id;
		private StringBuilder ticket = new StringBuilder();
        bool reimpresion = false;
		public void construccion_ticket(long traspaso_id, bool reimpresion = false)
		{
			this.traspaso_id = traspaso_id;
            this.reimpresion = reimpresion;

			DAO_Traspasos dao_traspasos = new DAO_Traspasos();
			var traspaso_ticket = dao_traspasos.get_traspaso_ticket(traspaso_id);
			traspaso_ticket.detallado_traspaso_ticket = dao_traspasos.get_productos_diferencia(traspaso_id);

			//ticket.AppendLine(POS_Control.font_condensed + POS_Control.font_size_0);
            //ticket.AppendLine(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			//ticket.Append(POS_Control.font_size_16 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_16][ALIGN_CENTER]");

			ticket.AppendLine("**LISTA DE DIFERENCIAS**");
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
			ticket.AppendLine("--------------------------------------------------------");

            if (reimpresion)
            {
                //ticket.Append(POS_Control.font_size_64 + POS_Control.align_center);
                ticket.Append("[FONT_SIZE_64][ALIGN_CENTER]");
                ticket.AppendLine("[REIMPRESION]");
                //ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
                ticket.Append("[ALIGN_CENTER][FONT_SIZE_0]");
                ticket.AppendLine("--------------------------------------------------------");
            }

			DAO_Sucursales dao_sucursales = new DAO_Sucursales();
			string sucursal_id = HELPERS.Config_helper.get_config_local("sucursal_id");
			var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(sucursal_id));
			
			ticket.Append(POS_Control.printer_reset + POS_Control.font_condensed + POS_Control.font_size_0);
			ticket.AppendLine(string.Format("{0} - RFC:{1}\n{2}\n{3} COLONIA {4}\n{5} CP {6}, TEL: {7}", sucursal_data.razon_social, sucursal_data.rfc, sucursal_data.nombre, sucursal_data.direccion, sucursal_data.colonia, sucursal_data.ciudad, sucursal_data.codigo_postal, sucursal_data.telefono));

			ticket.AppendLine(string.Format("FOLIO: #{0}", traspaso_ticket.traspaso_id));
			ticket.AppendLine(string.Format("TERMINAL: {0}", Misc_helper.get_nombre_terminal((int)traspaso_ticket.terminal_id).ToUpper()));
			ticket.AppendLine(string.Format("CREADO POR: {0}", traspaso_ticket.nombre_empleado_captura.ToUpper()));
			ticket.AppendLine(string.Format("TERMINADO POR: {0}", traspaso_ticket.nombre_empleado_termina.ToUpper()));
            ticket.AppendLine(string.Format("FECHA CREADO: {0}", (traspaso_ticket.fecha_creado != null) ? Misc_helper.fecha(traspaso_ticket.fecha_creado.ToString().ToUpper(), "LEGIBLE") : " - "));
            ticket.AppendLine(string.Format("FECHA TERMINADO: {0}", (traspaso_ticket.fecha_terminado != null) ? Misc_helper.fecha(traspaso_ticket.fecha_terminado.ToString(), "LEGIBLE") : " - "));

            if(traspaso_ticket.comentarios.Trim().Length > 0)
            {
                ticket.AppendLine(string.Format("COMENTARIOS: {0}", traspaso_ticket.comentarios.ToUpper()));
            }

			//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			ticket.AppendLine("COD   PRODUCTO                                  PRECIO C");
			ticket.AppendLine("CADUCIDAD                LOTE                 ENV    REC");
			ticket.AppendLine("--------------------------------------------------------");

			long total_piezas = 0;

			foreach (DTO_Detallado_traspaso_ticket detallado_ticket in traspaso_ticket.detallado_traspaso_ticket)
			{
				ticket.AppendLine(string.Format("{0,5} {1,-37}{2,13:C4}",
					detallado_ticket.amecop,
					detallado_ticket.nombre,
					detallado_ticket.precio_costo)
				);

				foreach (Tuple<string, string, int, int, int> cad_lotes in detallado_ticket.caducidades_lotes)
				{
					ticket.AppendLine(string.Format("{0,9} {1,-33}{2,6} {3,6}",
						HELPERS.Misc_helper.fecha(cad_lotes.Item1,"CADUCIDAD"),
						HELPERS.Misc_helper.PadBoth(cad_lotes.Item2, 33),
						cad_lotes.Item4,cad_lotes.Item3)
					);

					total_piezas += Convert.ToInt64(cad_lotes.Item3);
				}
			}

			ticket.AppendLine("\n");
			ticket.AppendLine("                                           -------------");
			ticket.AppendLine(string.Format("{0,28} {1,27}", "TOTAL PIEZAS: ", total_piezas));
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_32 + POS_Control.align_center);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_32][ALIGN_CENTER]");

			ticket.AppendLine("RESOLUCION DE");
			ticket.AppendLine("CONFLICTOS");
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_center);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_CENTER]");
			ticket.AppendLine("--------------------------------------------------------");
			ticket.Append("\n");
			ticket.AppendLine(" **ENVIAR VIRTUAL**");
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
			ticket.AppendLine("COD   PRODUCTO                                  PRECIO C");
			ticket.AppendLine("CADUCIDAD                LOTE                       CANT");
			ticket.AppendLine("--------------------------------------------------------");

			foreach (DTO_Detallado_traspaso_ticket detallado_ticket in traspaso_ticket.detallado_traspaso_ticket)
			{
				foreach (Tuple<string, string, int, int, int> cad_lotes in detallado_ticket.caducidades_lotes)
				{
					if(cad_lotes.Item4 > cad_lotes.Item3)
					{
						ticket.AppendLine(string.Format("{0,5} {1,-37}{2,13:C4}",
							detallado_ticket.amecop,
							detallado_ticket.nombre,
							detallado_ticket.precio_costo)
						);

						ticket.AppendLine(string.Format("{0,9} {1,-33}{2,6} {3,6}",
							HELPERS.Misc_helper.fecha(cad_lotes.Item1,"CADUCIDAD"),
							HELPERS.Misc_helper.PadBoth(cad_lotes.Item2, 33),
							"", 
							cad_lotes.Item4 - cad_lotes.Item3)
						);
					}
				}
			}

			ticket.AppendLine("\n");

			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_center);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_CENTER]");
			ticket.AppendLine(" **PEDIR VIRTUAL**");
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
			ticket.AppendLine("COD   PRODUCTO                                  PRECIO C");
			ticket.AppendLine("CADUCIDAD                LOTE                       CANT");
			ticket.AppendLine("--------------------------------------------------------");

  

			foreach (DTO_Detallado_traspaso_ticket detallado_ticket in traspaso_ticket.detallado_traspaso_ticket)
			{
				foreach (Tuple<string, string, int, int, int> cad_lotes in detallado_ticket.caducidades_lotes)
				{
					if (cad_lotes.Item4 < cad_lotes.Item3)
					{
                        

						ticket.AppendLine(string.Format("{0,5} {1,-37}{2,13:C4}",
							detallado_ticket.amecop,
							detallado_ticket.nombre,
							detallado_ticket.precio_costo)
						);


						ticket.AppendLine(string.Format("{0,9} {1,-33}{2,6} {3,6}",
							HELPERS.Misc_helper.fecha(cad_lotes.Item1,"CADUCIDAD"),
							HELPERS.Misc_helper.PadBoth(cad_lotes.Item2, 33),
							"", 
							cad_lotes.Item3 - cad_lotes.Item4)
						);
					}
				}
			}

			
            POS_Control.finTicket(ticket);
		}

		public bool print()
		{
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "TRASPASO", traspaso_id, true, false,false,reimpresion);
		}

		public void Dispose()
		{

		}
	}

	class Etiqueta_bulto
	{
		private long traspaso_id;
		private StringBuilder ticket = new StringBuilder();
		DAO_Sucursales dao_sucursales = new DAO_Sucursales();
		DAO_Traspasos dao_traspaso = new DAO_Traspasos();

		public void construccion_ticket(long traspaso_id, int numero_etiqueta, int total_etiquetas, string hash, long total_piezas)
		{
			this.traspaso_id = traspaso_id;

			string [] split_hash = hash.Split('$');

			var sucursal_origen_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(split_hash[0]));
			var sucursal_destino_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(split_hash[1]));

			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
            //ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_CENTER]");
			ticket.AppendLine("--------------------------------------------------------");
			ticket.AppendLine("ORIGEN: " + sucursal_origen_data.nombre);
			ticket.AppendLine("DESTINO: " + sucursal_destino_data.nombre);
			ticket.AppendLine("TOTAL PIEZAS: " + total_piezas);
			ticket.AppendLine("--------------------------------------------------------");
			//ticket.Append(POS_Control.font_size_32 + POS_Control.align_center);
           // ticket.Append("[FONT_SIZE_32][ALIGN_CENTER]");
			ticket.AppendLine("** BULTOS **");
			ticket.AppendLine("FOLIO: #"+traspaso_id);
			ticket.AppendLine(Misc_helper.PadBoth(string.Format("BULTO {0} de {1}", numero_etiqueta, total_etiquetas),56));
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_center);
                        // ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_CENTER]");
			//ticket.AppendLine(POS_Control.barcode(string.Format("{0}${1}${2}",hash,numero_etiqueta,total_etiquetas)));

            // Encode(hash)
            ticket.AppendLine(string.Format("[BARCODE:{0}]", string.Format("{0}", Encode(hash))));
                                                                                            
            POS_Control.finTicket(ticket);
		}

		public bool print()
		{
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "ETIQUETA_BULTOS", traspaso_id, true);
		}

		public void Dispose()
		{

		}

        public static string Encode(string hash)
        {
            string hash_validador = "";

            int total = 0;
            for (int i = 0; i < hash.Length; i++)
            {
                string digito = Convert.ToString(hash[i]);
                if( !digito.Equals("$") )
                {
                    total = total + Int32.Parse(digito);
                }

            }

            int verificador_sistema =   total + 7;

            hash_validador = Convert.ToString(verificador_sistema) + "$" + hash + "$" + Convert.ToString(total);

            return reversa(hash_validador);
        }

        public static string reversa( string cadena )
        {
       
            char[] arr = cadena.ToCharArray();
            Array.Reverse(arr);  
            return new string(arr);
        }

	}

	class Traspaso
	{
		private long traspaso_id;
		private StringBuilder ticket = new StringBuilder();

		public void construccion_ticket(long traspaso_id, bool reimpresion = false)
		{
			this.traspaso_id = traspaso_id;

			DAO_Traspasos dao_traspasos = new DAO_Traspasos();
			var traspaso_ticket = dao_traspasos.get_traspaso_ticket(traspaso_id);
			traspaso_ticket.detallado_traspaso_ticket = dao_traspasos.get_detallado_traspaso_ticket(traspaso_id);

			//ticket.AppendLine(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			//ticket.Append(POS_Control.font_size_64 + POS_Control.align_center);
            ticket.Append("[FONT_SIZE_64][ALIGN_CENTER]");
			ticket.AppendLine("*TRASPASO*");
			ticket.AppendLine(traspaso_ticket.tipo);
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0 + POS_Control.align_left);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0][ALIGN_LEFT]");
			ticket.AppendLine("--------------------------------------------------------");
			
			if (reimpresion)
			{
                //ticket.Append(POS_Control.font_size_64 + POS_Control.align_center);
                ticket.Append("[FONT_SIZE_64][ALIGN_CENTER]");
                ticket.AppendLine("[REIMPRESION]");
				//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
                ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
				ticket.AppendLine("--------------------------------------------------------");
			}

            DAO_Sucursales dao_sucursales = new DAO_Sucursales();
            //ticket.Append(POS_Control.font_size_16 + POS_Control.align_left);
            ticket.Append("[FONT_SIZE_16][ALIGN_LEFT]");
            var sucursal_destino = dao_sucursales.get_sucursal_data(traspaso_ticket.sucursal_id);
            var sucursal_origine = dao_sucursales.get_sucursal_data(Convert.ToInt32(Config_helper.get_config_local("sucursal_id")));
            ticket.AppendLine("ORIGEN: "+sucursal_origine.nombre.ToUpper());


            int sucursal_id_local = Convert.ToInt32(Config_helper.get_config_local("sucursal_id"));

            if (sucursal_id_local == traspaso_ticket.sucursal_id)
                ticket.AppendLine("DESTINO: " + "ENLACE VITAL");
            else
                ticket.AppendLine("DESTINO: " + sucursal_destino.nombre.ToUpper());
            

            //ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");

            ticket.AppendLine("--------------------------------------------------------");
			
			ticket.AppendLine(string.Format("FOLIO: #{0}", traspaso_ticket.traspaso_id));
			ticket.AppendLine(string.Format("TERMINAL: {0}", Misc_helper.get_nombre_terminal((int)traspaso_ticket.terminal_id)));
			ticket.AppendLine(string.Format("CREADO POR: {0}", traspaso_ticket.nombre_empleado_captura));
			ticket.AppendLine(string.Format("TERMINADO POR: {0}", traspaso_ticket.nombre_empleado_termina));
            ticket.AppendLine(string.Format("FECHA CREADO: {0}", (traspaso_ticket.fecha_creado != null) ? Misc_helper.fecha(traspaso_ticket.fecha_creado.ToString(),"LEGIBLE") : " - "));
            ticket.AppendLine(string.Format("FECHA TERMINADO: {0}", (traspaso_ticket.fecha_terminado != null) ? Misc_helper.fecha(traspaso_ticket.fecha_terminado.ToString(), "LEGIBLE") : " - "));
			
			//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
            ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			ticket.AppendLine("COD   PRODUCTO                                  PRECIO C");
			ticket.AppendLine("CADUCIDAD                LOTE                       CANT");
			ticket.AppendLine("--------------------------------------------------------");

			long total_piezas = 0;
            decimal total_importe = 0;

			foreach (DTO_Detallado_traspaso_ticket detallado_ticket in traspaso_ticket.detallado_traspaso_ticket)
			{
				ticket.AppendLine(string.Format("{0,5} {1,-37}{2,13:C4}",
					detallado_ticket.amecop,
					detallado_ticket.nombre,
					detallado_ticket.precio_costo)
				);

                

				foreach (Tuple<string, string, int, int, int> cad_lotes in detallado_ticket.caducidades_lotes)
				{
					ticket.AppendLine(string.Format("{0,9} {1,-33}{2,13}",
						HELPERS.Misc_helper.fecha(cad_lotes.Item1, "CADUCIDAD"),
						HELPERS.Misc_helper.PadBoth(cad_lotes.Item2, 33),
						cad_lotes.Item3)
					);

					total_piezas += Convert.ToInt64(cad_lotes.Item3);
                    total_importe += (detallado_ticket.precio_costo * Convert.ToInt64(cad_lotes.Item3));
				}
                
			}

			if(total_piezas == 0)
			{
				//ticket.Append(POS_Control.align_center + POS_Control.font_size_16);
                ticket.Append("[ALIGN_CENTER][FONT_SIZE_16]");
				ticket.AppendLine("\nTRASPASO TERMINADO VACIO");
				//ticket.Append(POS_Control.align_left + POS_Control.font_size_0);
                ticket.Append("[ALIGN_LEFT][FONT_SIZE_0]");
			}

			if (!reimpresion)
			{
				for (int i = 1; i <= traspaso_ticket.numero_bultos; i++)
				{
					Etiqueta_bulto etiqueta = new Etiqueta_bulto();
					etiqueta.construccion_ticket(traspaso_id, i, traspaso_ticket.numero_bultos, traspaso_ticket.hash, total_piezas);
					etiqueta.print();
				}
			}

			ticket.AppendLine("\n");
			ticket.AppendLine("                                           -------------");
			ticket.AppendLine(string.Format("{0,28} {1,27}", "TOTAL PIEZAS: ", total_piezas));
            ticket.AppendLine(string.Format("{0,28} {1,27}", "IMPORTE TOTAL: ", total_importe.ToString("C")));
			//ticket.Append(POS_Control.font_condensed + POS_Control.font_size_0);
            ticket.Append("[FONT_CONDENSED][FONT_SIZE_0]");
			ticket.AppendLine("--------------------------------------------------------");
			//ticket.AppendLine(POS_Control.align_center); ticket.AppendLine(POS_Control.align_center);
            ticket.AppendLine("[ALIGN_CENTER][ALIGN_CENTER]");
			//ticket.AppendLine((traspaso_ticket.traspado_padre_id == null) ? (traspaso_ticket.es_para_venta > 0) ? "" : POS_Control.barcode(traspaso_ticket.hash) : "");
            ticket.AppendLine((traspaso_ticket.traspado_padre_id == null) ? (traspaso_ticket.es_para_venta > 0) ? "" : string.Format("[BARCODE:{0}]", Encode(traspaso_ticket.hash)) : "");
            POS_Control.finTicket(ticket);
		}

		public bool print()
		{
            return HELPERS.Print_new_helper.print((long)Misc_helper.get_terminal_id(), ticket.ToString(), "TRASPASO", traspaso_id, true);
		}

		public void Dispose()
		{

		}

        public static string Encode(string hash)
        {
            string hash_validador = "";

            int total = 0;
            for (int i = 0; i < hash.Length; i++)
            {
                string digito = Convert.ToString(hash[i]);
                if (!digito.Equals("$"))
                {
                    total = total + Int32.Parse(digito);
                }

            }
 
            int verificador_sistema = total + 7;

            hash_validador = Convert.ToString(verificador_sistema) + "$" + hash + "$" + Convert.ToString(total);

            return reversa(hash_validador);
        }

        public static string reversa(string cadena)
        {

            char[] arr = cadena.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }




	}
}
