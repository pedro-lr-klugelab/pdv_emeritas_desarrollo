using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using Farmacontrol_PDV.DAO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Farmacontrol_PDV.CLASSES;

namespace Farmacontrol_PDV.HELPERS
{
	class Misc_helper
	{
        public static bool validar_codigo_venta(string codigo)
        {
            return Regex.IsMatch(codigo.Trim(), @"^[\d+\${1}\b+]*$");
        }

        public static string label_printer(string texto)
        {
            POS_Caracter pos = new POS_Caracter();
            return pos.replace_label(texto);
        }

        public static int pct_iva_global()
        {
            return (int)Math.Abs(Convert.ToDecimal(Config_helper.get_config_global("pct_iva")) * 100);
        }

		public static string uuid_guiones(string uuid)
		{
			//1F92B363-A0DB-11E4-A057-D02788E35E2C	
			string bloque_uno = uuid.Substring(0,8);
			string bloque_dos = uuid.Substring(8, 4);
			string bloque_tres = uuid.Substring(12, 4);
			string bloque_cuatro = uuid.Substring(16, 4);
			string bloque_cinco = uuid.Substring(20, 12);

			return string.Format("{0}-{1}-{2}-{3}-{4}",bloque_uno,bloque_dos,bloque_tres,bloque_cuatro,bloque_cinco);
		}
		
		public static bool es_numero(string numero)
		{
			long num;
			bool es_num = long.TryParse(numero, out num);
			return es_num;
		}

		public static bool soy_caja()
		{
			DAO_Terminales dao_terminales = new DAO_Terminales();
			return dao_terminales.get_terminal_es_caja();
		}

		public static Array get_enums(string tabla, string columna)
		{
			DAO_Generico dao_generico = new DAO_Generico();
			return dao_generico.get_enum(tabla,columna);
		}

		public static string uuid()
		{
			DAO.Conector conector = new DAO.Conector();
			string sql = @"SELECT UPPER(UUID( )) AS uuid";

			Dictionary<string,object> parametros = new Dictionary<string,object>();

			conector.Select(sql,parametros);

			return conector.result_set.Rows[0]["uuid"].ToString();
		}

		public static string uuid_small()
		{
			DAO.Conector conector = new DAO.Conector();
			string sql = @"SELECT SUBSTRING(UPPER( REPLACE( UUID( ) ,  '-',  '' ) ), 1,8 ) AS uuid";

			Dictionary<string, object> parametros = new Dictionary<string, object>();

			conector.Select(sql, parametros);

			return conector.result_set.Rows[0]["uuid"].ToString();
		}

		public static string CadtoDate(string caducidad)
		{
			if(caducidad.Equals(" ") || caducidad.Equals("SIN CAD") || caducidad.Equals(""))
			{
				return "0000-00-00";
			}

			string [] string_array = caducidad.Split(' ');
			string mes = string_array[0];
			string anio = string_array[1]+"-";

			string fecha = anio;

			if(mes.Equals("ENE"))
			{
				fecha += "01-01";
			}
			else if (mes.Equals("FEB"))
			{
				fecha += "02-01";
			}
			else if (mes.Equals("MAR"))
			{
				fecha += "03-01";
			}
			else if (mes.Equals("ABR"))
			{
				fecha += "04-01";
			}
			else if (mes.Equals("MAY"))
			{
				fecha += "05-01";
			}
			else if (mes.Equals("JUN"))
			{
				fecha += "06-01";
			}
			else if (mes.Equals("JUL"))
			{
				fecha += "07-01";
			}
			else if (mes.Equals("AGO"))
			{
				fecha += "08-01";
			}
			else if (mes.Equals("SEP"))
			{
				fecha += "09-01";
			}
			else if (mes.Equals("OCT"))
			{
				fecha += "10-01";
			}
			else if (mes.Equals("NOV"))
			{
				fecha += "11-01";
			}
			else if (mes.Equals("DIC"))
			{
				fecha += "12-01";
			}

			return fecha;
		}

		public static string DecodeBase64(string encodedString)
		{
			byte[] data = Convert.FromBase64String(encodedString);
			string decodedString = Encoding.UTF8.GetString(data);
			return decodedString;
		}

		public static string EncodeTo64(string toEncode)
		{
			byte[] toEncodeAsBytes
				  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
			string returnValue
				  = System.Convert.ToBase64String(toEncodeAsBytes);
			return returnValue;
		}


        public static string Base64Encode( string cadena)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(cadena);
            return System.Convert.ToBase64String(plainTextBytes);  
        }

		public static string PadBoth(string source, int length)
		{
			int spaces = length - source.Length;
			int padLeft = spaces / 2 + source.Length;
			return source.PadLeft(padLeft).PadRight(length);
		}

		/*public static string fecha(string fecha = null, bool muestra_hora = false, string tipo = "larga", bool sin_espacios = false)
		{
			if (fecha.Equals("0000-00-00") || fecha.Equals("0000-00-00 00:00:00") || fecha.Equals(" ") || fecha.Equals(""))
			{
				if(tipo.Equals("caducidad"))
				{
					return "SIN CAD";
				}
				else
				{
					return " ";
				}
			}

			string result = "";
			
			if(fecha.Length == 10)
			{
				fecha = fecha + " 00:00:00";
			}

			string[] fecha_array = fecha.Split(' ');
			fecha = fecha_array[0];
			string tiempo = fecha_array[1];

			if (tipo != "caducidad" && fecha.Equals("0000-00-00 00:00:00") || tipo != "caducidad" && fecha == "0000-00-00" || fecha == null || ConvertToTimestamp(fecha) < 315554400)
			{
				return "";
			}

			string nombre_dia = Dia_semana(fecha);
			
			string [] contenido_fecha = fecha.Split('-');

			string nombre_mes = Nombre_mes(Convert.ToInt32(contenido_fecha[1])); 
			string dia = contenido_fecha[2];
			string anio = contenido_fecha[0];

			string [] contenido_tiempo = tiempo.Split(':');

			string hora = contenido_tiempo[0]+contenido_tiempo[1];
			string mes_compacto = nombre_mes.Substring(0,3);

			if(tipo.Equals("caducidad"))
			{
				result = mes_compacto+"-"+anio;
			}
			else if(tipo.Equals("larga"))
			{
				result = string.Format("{0} {1} de {2} del {3}",nombre_dia,dia,nombre_mes,anio);
			}else if(tipo.Equals("corta"))
			{
				result = string.Format("{0} {1} {2}", dia, nombre_mes, anio);
			}else if(tipo.Equals("compacta"))
			{
				result = string.Format("{0} {1} {2}", dia, mes_compacto, anio);
			}

			if (muestra_hora)
			{
				result = string.Format("{0} - {1}", result, convert24ToAMPM(tiempo.Substring(0, 5)));
			}

			return result.Replace("-"," ").ToUpper();
		}
        */
       
        public static string fecha(string fecha = "", string tipo = "ISO")
        {
            if (fecha.Equals("") && tipo.Equals("ISO"))
            {
                return DAO_Generic.get_fecha_now();
            }
            else
            {
                string[] formats = new string[] { "d", "D", "f", "F", "g", "G", "m", "M", "o", "O", "r", "R", "s", "t", "T", "u", "U", "Y", "y", "yyyy-MM-dd", "yyyy-MM-dd HH:mm:ss" };

                DateTime fecha_date;

                bool is_fecha = (DateTime.TryParseExact(fecha, formats, new CultureInfo("es-MX"), DateTimeStyles.None, out fecha_date));

                if (is_fecha)
                {
                    if (fecha_date.ToString("HH:mm:ss").Equals("00:00:00"))
                    {
                        string tmp_fecha = "";

                        switch (tipo)
                        {
                            case "ISO":
                                tmp_fecha = Convert.ToDateTime(fecha_date).ToString("yyyy-MM-dd");
                                break;
                            case "LEGIBLE":
                                tmp_fecha = Convert.ToDateTime(fecha_date).ToString("dd/MMM/yyyy").ToUpper().Replace(".", "");
                                break;
                            case "CADUCIDAD":
                                tmp_fecha = Convert.ToDateTime(fecha_date).ToString("MMM yyyy").ToUpper().Replace(".", "");
                                break;
                        }


                        return tmp_fecha;
                    }
                    else
                    {
                        string tmp_fecha = "";

                        switch (tipo)
                        {
                            case "ISO":
                                tmp_fecha = Convert.ToDateTime(fecha_date).ToString("yyyy-MM-dd HH:mm:ss");
                                break;
                            case "LEGIBLE":
                                tmp_fecha = Convert.ToDateTime(fecha_date).ToString("dd/MMM/yyyy hh:mm:ss tt").ToUpper().Replace(". ", "").Replace(".", "");
                                break;
                            case "CADUCIDAD":
                                tmp_fecha = Convert.ToDateTime(fecha_date).ToString("MMM yyyy").ToUpper().Replace(".", "");
                                break;
                        }

                        return tmp_fecha;
                    }
                }
                else
                {
                    return (tipo.Equals("ISO")) ? "0000-00-00" : "SIN CAD";
                }
            }
        }

		public static double ConvertToTimestamp(string fecha_String)
		{	
			double result = 0;

			try
			{
				DateTime fecha = DateTime.ParseExact(fecha_String, "yyyy-MM-dd", CultureInfo.InvariantCulture);
				TimeSpan timestamp = (fecha - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
				result = (double)timestamp.TotalSeconds;
			}
			catch(Exception excepcion)
			{
				Log_error.log(excepcion);
			}

			return result;
		}

		public static string Dia_semana(string fecha_string)
		{
			DateTime? fecha = DateTime.ParseExact(fecha_string, "yyyy-MM-dd", CultureInfo.InvariantCulture);
			string fecha_format = fecha.Value.ToString("dddd", new CultureInfo("es-ES"));
			return RemoveAcentos(fecha_format);
		}

		public static string Nombre_mes(int numero_mes)
		{
			DateTimeFormatInfo fecha = new CultureInfo("es-ES", false).DateTimeFormat;
			return fecha.GetMonthName(numero_mes);
		}

		public static string get_ip()
		{
			string ip_local = "";

			try
			{
				IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

				foreach (IPAddress ip in host.AddressList)
				{
					if (ip.AddressFamily == AddressFamily.InterNetwork)
					{
						break;
					}
				}
			}
			catch(Exception exception)
			{
				Log_error.log(exception);
			}

			return ip_local;
		}

		public static string get_nombre_terminal(int terminal_id = 0)
		{
			if(terminal_id == 0)
			{
				terminal_id = (int)get_terminal_id();
			}

			DAO_Terminales dao_terminales = new DAO_Terminales();	

			return dao_terminales.get_terminal_nombre(terminal_id);
		}

		public static int? get_terminal_id()
		{
			int? terminal_id = null;

			try
			{
				DAO.DAO_Terminales dao_terminales = new DAO.DAO_Terminales();
				terminal_id = dao_terminales.get_terminal_id();
			}
			catch(Exception exception)
			{
				MessageBox.Show("Misc_helper/get_terminal_id: "+exception.Message);
			}

			return terminal_id;
		}

		public static string convert24ToAMPM(string horas)
		{
			string hora_formato = "";
			string tiempo = "AM";
			int hora = Convert.ToInt32(horas.Substring(0, 2));
			int minutos = Convert.ToInt32(horas.Substring(3, 2));

			if(hora > 12)
			{
				tiempo = "PM";
			}

			switch(hora)
			{
				case 13:
					hora_formato = string.Format("{0}:{1}{2}","01",minutos,tiempo);
				break;
				case 14:
					hora_formato = string.Format("{0}:{1}{2}","02", minutos, tiempo);
				break;
				case 15:
					hora_formato = string.Format("{0}:{1}{2}","03", minutos, tiempo);
				break;
				case 16:
					hora_formato = string.Format("{0}:{1}{2}", "04", minutos, tiempo);
				break;
				case 17:
					hora_formato = string.Format("{0}:{1}{2}", "05", minutos, tiempo);
				break;
				case 18:
					hora_formato = string.Format("{0}:{1}{2}", "06", minutos, tiempo);
				break;
				case 19:
					hora_formato = string.Format("{0}:{1}{2}", "07", minutos, tiempo);
				break;
				case 20:
					hora_formato = string.Format("{0}:{1}{2}", "08", minutos, tiempo);
				break;
				case 21:
					hora_formato = string.Format("{0}:{1}{2}", "09", minutos, tiempo);
				break;
				case 22:
					hora_formato = string.Format("{0}:{1}{2}", "10", minutos, tiempo);
				break;
				case 23:
					hora_formato = string.Format("{0}:{1}{2}", "11", minutos, tiempo);
				break;
				case 24:
					hora_formato = string.Format("{0}:{1}{2}", "12", minutos, tiempo);
				break;
				default:
					if(hora > 9)
					{
						hora_formato = string.Format("{0}:{1}{2}", hora, minutos, tiempo);	
					}
					else
					{
						hora_formato = string.Format("{0}:{1}{2}", "0"+hora, minutos, tiempo);
					}
				break;
			}

			return hora_formato;
		}

		public static string RemoveAcentos(string cadena)
		{
			Regex replace_a_Accents = new Regex("[á|à|ä|â]", RegexOptions.Compiled);
			Regex replace_e_Accents = new Regex("[é|è|ë|ê]", RegexOptions.Compiled);
			Regex replace_i_Accents = new Regex("[í|ì|ï|î]", RegexOptions.Compiled);
			Regex replace_o_Accents = new Regex("[ó|ò|ö|ô]", RegexOptions.Compiled);
			Regex replace_u_Accents = new Regex("[ú|ù|ü|û]", RegexOptions.Compiled);
			cadena = replace_a_Accents.Replace(cadena, "a");
			cadena = replace_e_Accents.Replace(cadena, "e");
			cadena = replace_i_Accents.Replace(cadena, "i");
			cadena = replace_o_Accents.Replace(cadena, "o");
			cadena = replace_u_Accents.Replace(cadena, "u");
			return cadena;
		}

		public static string NumtoLe(decimal total)
		{
			int centavos = Convert.ToInt32( (total - Math.Floor(total)) * 100 );
			return (NumtoLeAux(total) + " PESOS " + centavos + "/100 MN").ToUpper();
		}

		private static string NumtoLeAux(decimal total)
		{
			int value = Convert.ToInt32(Math.Floor(total));

			string Num2Text = "";
			if (value < 0) return "menos " + NumtoLeAux(Math.Abs(value));

			if (value == 0) Num2Text = "cero";
			else if (value == 1) Num2Text = "uno";
			else if (value == 2) Num2Text = "dos";
			else if (value == 3) Num2Text = "tres";
			else if (value == 4) Num2Text = "cuatro";
			else if (value == 5) Num2Text = "cinco";
			else if (value == 6) Num2Text = "seis";
			else if (value == 7) Num2Text = "siete";
			else if (value == 8) Num2Text = "ocho";
			else if (value == 9) Num2Text = "nueve";
			else if (value == 10) Num2Text = "diez";
			else if (value == 11) Num2Text = "once";
			else if (value == 12) Num2Text = "doce";
			else if (value == 13) Num2Text = "trece";
			else if (value == 14) Num2Text = "catorce";
			else if (value == 15) Num2Text = "quince";
			else if (value < 20) Num2Text = "dieci" + NumtoLeAux((value - 10));
			else if (value == 20) Num2Text = "veinte";
			else if (value < 30) Num2Text = "veinti" + NumtoLeAux((value - 20));
			else if (value == 30) Num2Text = "treinta";
			else if (value == 40) Num2Text = "cuarenta";
			else if (value == 50) Num2Text = "cincuenta";
			else if (value == 60) Num2Text = "sesenta";
			else if (value == 70) Num2Text = "setenta";
			else if (value == 80) Num2Text = "ochenta";
			else if (value == 90) Num2Text = "noventa";
			else if (value < 100)
			{
				int u = value % 10;
				Num2Text = string.Format("{0} y {1}", NumtoLeAux(((value / 10) * 10)), (u == 1 ? "un" : NumtoLeAux((value % 10))));
			}
			else if (value == 100) Num2Text = "cien";
			else if (value < 200) Num2Text = "ciento " + NumtoLeAux((value - 100));
			else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800))
				Num2Text = NumtoLeAux(((value / 100))) + "cientos";
			else if (value == 500) Num2Text = "quinientos";
			else if (value == 700) Num2Text = "setecientos";
			else if (value == 900) Num2Text = "novecientos";
			else if (value < 1000) Num2Text = string.Format("{0} {1}", NumtoLeAux(((value / 100) * 100)), NumtoLeAux((value % 100)));
			else if (value == 1000) Num2Text = "mil";
			else if (value < 2000) Num2Text = "mil " + NumtoLeAux((value % 1000));
			else if (value < 1000000)
			{
				Num2Text = NumtoLeAux(((value / 1000))) + " mil";
				if ((value % 1000) > 0) Num2Text += " " + NumtoLeAux((value % 1000));
			}
			else if (value == 1000000) Num2Text = "un millón";
			else if (value < 2000000) Num2Text = "un millón " + NumtoLeAux((value % 1000000));
			else if (value < int.MaxValue)
			{
				Num2Text = NumtoLeAux(((value / 1000000))) + " millones";
				if ((value - (value / 1000000) * 1000000) > 0) Num2Text += " " + NumtoLeAux((value - (value / 1000000) * 1000000));
			}

			return Num2Text;
		}
	}
}
