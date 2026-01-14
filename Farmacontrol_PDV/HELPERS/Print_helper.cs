using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Farmacontrol_PDV.CLASSES;
using System.Windows.Forms;

namespace Farmacontrol_PDV.HELPERS
{
	class Print_helper
	{
		public static bool print(long terminal_id, string texto, string tipo, long folio, bool impresora_tickets, bool impresion_remota =  false, bool sin_aviso = false, bool reimpresion = false)
		{
			bool result = false;

            string texto_imprimir = reimpresion ? texto.Replace("[REIMPRESION]", "* COPIA *") : texto.Replace("[REIMPRESION]", "");

			//if(impresion_remota == false)
			//{
				if (impresora_tickets)
				{
					if (Properties.Configuracion.Default.usar_impresora_remota_tickets != "" && Properties.Configuracion.Default.usar_impresora_remota_tickets != "0")
					{
						if (Properties.Configuracion.Default.impresora_remota_tickets != "")
						{
							DTO_Impresion_ip impresion = new DTO_Impresion_ip();
							impresion.texto = texto_imprimir;
							impresion.tipo = tipo;
							impresion.folio = folio;
                            impresion.terminal_id = (long)Misc_helper.get_terminal_id();
                            impresion.reimpresion = reimpresion;
							impresion.impresora_tickets = impresora_tickets;

							byte[] trama_envio;

							BinaryFormatter bf = new BinaryFormatter();
							using (MemoryStream ms = new MemoryStream())
							{
								bf.Serialize(ms, impresion);
								trama_envio = ms.ToArray();
							}

							string impresionb64 = Convert.ToBase64String(trama_envio);

                            try
                            {
                                ClienteImpresionIP.enviar_mensaje(Properties.Configuracion.Default.impresora_remota_tickets, impresionb64);
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
						}
					}
					else
					{

						DAO_Impresiones dao_impresiones = new DAO_Impresiones();
						Print_raw printer = new CLASSES.Print_raw();

						string impresora = (impresora_tickets) ? Properties.Configuracion.Default.impresora_tickets : Properties.Configuracion.Default.impresora_etiquetas;

                        if (impresora.Equals(""))
                        {
                            MessageBox.Show(null, "No se pudo imprimir, No existe una impresora por default, vaya a Opciones/configuración y asigne una.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

						printer.PrinterName = impresora;

						printer.Open("TICKET");

						result = printer.Print(texto_imprimir);

						if (result)
						{
                            if (!reimpresion)
                            {
                                dao_impresiones.registrar_impresion(terminal_id, texto, tipo, folio, impresora);
                            }
						}

						printer.Close();
					}
				}
				else
				{
					if (Properties.Configuracion.Default.usar_impresora_remota_etiquetas != "" && Properties.Configuracion.Default.usar_impresora_remota_etiquetas != "0")
					{
						if (Properties.Configuracion.Default.impresora_remota_etiquetas != "")
						{
							DTO_Impresion_ip impresion = new DTO_Impresion_ip();
							impresion.texto = texto_imprimir;
							impresion.tipo = tipo;
							impresion.folio = folio;
							impresion.impresora_tickets = impresora_tickets;

							byte[] trama_envio;

							BinaryFormatter bf = new BinaryFormatter();
							using (MemoryStream ms = new MemoryStream())
							{
								bf.Serialize(ms, impresion);
								trama_envio = ms.ToArray();
							}

							string impresionb64 = Convert.ToBase64String(trama_envio);

							ClienteImpresionIP.enviar_mensaje(Properties.Configuracion.Default.impresora_remota_etiquetas, impresionb64);
						}
					}
					else
					{
						DAO_Impresiones dao_impresiones = new DAO_Impresiones();
						Print_raw printer = new CLASSES.Print_raw();

						string impresora = (impresora_tickets) ? Properties.Configuracion.Default.impresora_tickets : Properties.Configuracion.Default.impresora_etiquetas;

                        if (impresora.Equals(""))
                        {
                            MessageBox.Show(null, "No se pudo imprimir, No existe una impresora por default, vaya a Opciones/configuración y asigne una.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

						printer.PrinterName = impresora;

						printer.Open("TICKET");

						result = printer.Print(texto_imprimir);

						if (result)
						{
                            if(!reimpresion)
                            {
                                dao_impresiones.registrar_impresion(terminal_id, texto, tipo, folio, impresora);
                            }
						}

						printer.Close();
					}
				}
			/*}
			else
			{
				DAO_Impresiones dao_impresiones = new DAO_Impresiones();
				Print_raw printer = new CLASSES.Print_raw();

				string impresora = (impresora_tickets) ? Properties.Configuracion.Default.impresora_tickets : Properties.Configuracion.Default.impresora_etiquetas;

                if (impresora.Equals(""))
                {
                    MessageBox.Show(null, "No se pudo imprimir, No existe una impresora por default, vaya a Opciones/configuración y asigne una.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

				printer.PrinterName = impresora;

				printer.Open("TICKET");

				result = printer.Print(texto_imprimir);

				if (result)
				{
                    if (!reimpresion)
                    {
                        dao_impresiones.registrar_impresion(terminal_id, texto, tipo, folio, impresora);
                    }
				}

				printer.Close();	
			}*/

			return result;
		}

		public static bool status_impresora(bool impresora_etiquetas)
		{
			ManagementScope scope = new ManagementScope(@"\root\cimv2");
			scope.Connect();

			ManagementObjectSearcher searcher = new
			 ManagementObjectSearcher("SELECT * FROM Win32_Printer");

			string printerName = "";

			foreach (ManagementObject printer in searcher.Get())
			{
				printerName = printer["Name"].ToString();

				if (printerName.Equals((impresora_etiquetas) ? Properties.Configuracion.Default.impresora_etiquetas : Properties.Configuracion.Default.impresora_tickets))
				{
					int state = Int32.Parse(printer["ExtendedPrinterStatus"].ToString());

					if (state == 2)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}

			return false;
		}
	}


    class Print_new_helper
    {
        public static bool print(long terminal_id, string texto, string tipo, long folio, bool impresora_tickets, bool impresion_remota = false, bool sin_aviso = false, bool reimpresion = false)
        {
            DAO_Impresiones dao_impresiones = new DAO_Impresiones();
            string impresora = (impresora_tickets) ? Properties.Configuracion.Default.impresora_tickets : Properties.Configuracion.Default.impresora_etiquetas;

            if (!reimpresion)
            {
                long impresion_id = dao_impresiones.registrar_impresion(terminal_id, texto, tipo, folio, impresora);

                if(impresora_tickets)
                {
                    if (Properties.Configuracion.Default.usar_impresora_remota_tickets != "" && Properties.Configuracion.Default.usar_impresora_remota_tickets != "0")
                    {
                        if (Properties.Configuracion.Default.impresora_remota_tickets != "")
                        {
                            DTO_Impresion_ip_new impresion = new DTO_Impresion_ip_new();
                            impresion.id = impresion_id;
                            impresion.impresora_tickets = impresora_tickets;
                            impresion.reimpresion = reimpresion;

                            envio_trama_impresion(impresion);
                        }
                    }
                    else
                    {
                        imprimir_local(impresion_id, impresora, reimpresion);
                    }
                }
                else
                {
                    if (Properties.Configuracion.Default.usar_impresora_remota_etiquetas != "" && Properties.Configuracion.Default.usar_impresora_remota_etiquetas != "0")
                    {
                        if (Properties.Configuracion.Default.impresora_remota_etiquetas != "")
                        {
                            DTO_Impresion_ip_new impresion = new DTO_Impresion_ip_new();
                            impresion.id = impresion_id;
                            impresion.impresora_tickets = impresora_tickets;
                            impresion.reimpresion = reimpresion;

                            envio_trama_impresion(impresion);
                        }
                    }
                    else
                    {
                        imprimir_local(impresion_id, impresora, reimpresion);
                    }
                }
            }
            else
            {
                long impresion_id = dao_impresiones.get_impresion_id(tipo, folio, terminal_id);

                if(impresion_id > 0)
                {
                    if (impresora_tickets)
                    {
                        if (Properties.Configuracion.Default.usar_impresora_remota_tickets != "" && Properties.Configuracion.Default.usar_impresora_remota_tickets != "0")
                        {
                            if (Properties.Configuracion.Default.impresora_remota_tickets != "")
                            {
                                DTO_Impresion_ip_new impresion = new DTO_Impresion_ip_new();
                                impresion.id = impresion_id;
                                impresion.impresora_tickets = impresora_tickets;
                                impresion.reimpresion = reimpresion;

                                envio_trama_impresion(impresion);
                            }
                        }
                        else
                        {
                            imprimir_local(impresion_id, impresora, reimpresion);
                        }
                    }
                    else
                    {
                        if (Properties.Configuracion.Default.usar_impresora_remota_etiquetas != "" && Properties.Configuracion.Default.usar_impresora_remota_etiquetas != "0")
                        {
                            if (Properties.Configuracion.Default.impresora_remota_etiquetas != "")
                            {
                                DTO_Impresion_ip_new impresion = new DTO_Impresion_ip_new();
                                impresion.id = impresion_id;
                                impresion.impresora_tickets = impresora_tickets;
                                impresion.reimpresion = reimpresion;

                                envio_trama_impresion(impresion);
                            }
                        }
                        else
                        {
                            imprimir_local(impresion_id, impresora, reimpresion);
                        }
                    }
                    //imprimir_local(impresion_id, impresora, reimpresion);
                }
                else
                {
                    impresion_id = dao_impresiones.registrar_impresion(terminal_id, texto, tipo, folio, impresora);

                    if (impresora_tickets)
                    {
                        if (Properties.Configuracion.Default.usar_impresora_remota_tickets != "" && Properties.Configuracion.Default.usar_impresora_remota_tickets != "0")
                        {
                            if (Properties.Configuracion.Default.impresora_remota_tickets != "")
                            {
                                DTO_Impresion_ip_new impresion = new DTO_Impresion_ip_new();
                                impresion.id = impresion_id;
                                impresion.impresora_tickets = impresora_tickets;
                                impresion.reimpresion = reimpresion;

                                envio_trama_impresion(impresion);
                            }
                        }
                        else
                        {
                            imprimir_local(impresion_id, impresora, reimpresion);
                        }
                    }
                    else
                    {
                        if (Properties.Configuracion.Default.usar_impresora_remota_etiquetas != "" && Properties.Configuracion.Default.usar_impresora_remota_etiquetas != "0")
                        {
                            if (Properties.Configuracion.Default.impresora_remota_etiquetas != "")
                            {
                                DTO_Impresion_ip_new impresion = new DTO_Impresion_ip_new();
                                impresion.id = impresion_id;
                                impresion.impresora_tickets = impresora_tickets;
                                impresion.reimpresion = reimpresion;

                                envio_trama_impresion(impresion);
                            }
                        }
                        else
                        {
                            imprimir_local(impresion_id, impresora, reimpresion);
                        }
                    }

                    //MessageBox.Show("La impresion no pudo ser localizada, notifique a su adminstrador","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return false;
                }
            }

            return false;
        }

        public static bool print_force(long impresion_id)
        {
            if (Properties.Configuracion.Default.usar_impresora_remota_tickets != "" && Properties.Configuracion.Default.usar_impresora_remota_tickets != "0")
            {
                if (Properties.Configuracion.Default.impresora_remota_tickets != "")
                {
                    DTO_Impresion_ip_new impresion = new DTO_Impresion_ip_new();
                    impresion.id = impresion_id;
                    impresion.impresora_tickets = true;
                    impresion.reimpresion = false;

                    envio_trama_impresion(impresion);
                }
            }
            else
            {
                imprimir_local(impresion_id, Properties.Configuracion.Default.impresora_tickets, false);
            }

            return false;
        }

        public static void envio_trama_impresion(DTO_Impresion_ip_new impresion)
        {
            byte[] trama_envio;

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, impresion);
                trama_envio = ms.ToArray();
            }

            string impresionb64 = Convert.ToBase64String(trama_envio);

            ClienteImpresionIP.enviar_mensaje(Properties.Configuracion.Default.impresora_remota_etiquetas, impresionb64);
        }

        public static bool print_to_remote(long impresion_id, bool impresora_tickets, bool reimpresion)
        {
            DAO_Impresiones dao_impresion = new DAO_Impresiones();

            string texto = dao_impresion.get_texto_impresion(impresion_id);
            texto = reimpresion ? texto.Replace("[REIMPRESION]", "* COPIA *") : texto.Replace("[REIMPRESION]", "");

            if (!texto.Equals(""))
            {
                string impresora = (impresora_tickets) ? Properties.Configuracion.Default.impresora_tickets : Properties.Configuracion.Default.impresora_etiquetas;

                if (impresora.Equals(""))
                {
                    MessageBox.Show(null, "No se pudo imprimir, No existe una impresora por default, vaya a Opciones/configuración y asigne una.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    Print_raw printer = new CLASSES.Print_raw();
                    printer.PrinterName = impresora;
                    printer.Open("TICKET");

                    bool status_impresion = printer.Print(Misc_helper.label_printer(texto));
                    printer.Close();

                    if (status_impresion)
                    {
                        dao_impresion.update_imrpesora_impresion(impresion_id, impresora);
                    }

                    return status_impresion;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool imprimir_local(long impresion_id,string impresora, bool reimpresion)
        {
            DAO_Impresiones dao_impresion = new DAO_Impresiones();

            string texto = dao_impresion.get_texto_impresion(impresion_id);
            texto = reimpresion ? texto.Replace("[REIMPRESION]", "* COPIA *") : texto.Replace("[REIMPRESION]", "");

            if(!texto.Equals(""))
            {
                if (impresora.Equals(""))
                {
                    MessageBox.Show(null, "No se pudo imprimir, No existe una impresora por default, vaya a Opciones/configuración y asigne una.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    Print_raw printer = new CLASSES.Print_raw();
                    printer.PrinterName = impresora;
                    printer.Open("TICKET");

                    bool status_impresion = printer.Print(Misc_helper.label_printer(texto)); 

                    printer.Close();

                    if(status_impresion)
                    {
                        dao_impresion.update_imrpesora_impresion(impresion_id, impresora);
                    }

                    return status_impresion;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
