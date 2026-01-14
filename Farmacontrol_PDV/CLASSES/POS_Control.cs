using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Farmacontrol_PDV.CLASSES
{
	class POS_Control
	{
        public static void finTicket(StringBuilder ticket)
        {
            //ticket.AppendLine(POS_Control.align_center); 
            ticket.AppendLine("[ALIGN_CENTER]");
            //ticket.AppendLine(POS_Control.align_center); 
         //   ticket.AppendLine("[ALIGN_CENTER]");
            //ticket.AppendLine(POS_Control.align_center); 
          //  ticket.AppendLine("[ALIGN_CENTER]");
            //ticket.AppendLine(POS_Control.align_center); 
          //  ticket.AppendLine("[ALIGN_CENTER]");
            //ticket.AppendLine(POS_Control.align_center); 
          //  ticket.AppendLine("[ALIGN_CENTER]");
            //ticket.AppendLine(POS_Control.align_center); 
          // ticket.AppendLine("[ALIGN_CENTER]");
            //ticket.AppendLine(POS_Control.corte);
            ticket.AppendLine("[CORTE]");
        }

        public static string reimpresion = "* COPIA *";

		public static string printer_reset = String.Concat(
			Convert.ToChar(27),
			Convert.ToChar(64),
			Convert.ToChar(0)
		);

		public static string font_normal = String.Concat(
			Convert.ToChar(27),
			Convert.ToChar(77),
			Convert.ToChar(48)
		);

		public static string font_condensed = String.Concat(
			Convert.ToChar(27),
			Convert.ToChar(77),
			Convert.ToChar(49)
		);

		public static string font_size_0 = String.Concat(
			Convert.ToChar(29),
			Convert.ToChar(33),
			Convert.ToChar(0)
		);

		public static string font_size_16 = String.Concat(
			Convert.ToChar(29),
			Convert.ToChar(33),
			Convert.ToChar(16)
		);

		public static string font_size_32 = String.Concat(
			Convert.ToChar(29),
			Convert.ToChar(33),
			Convert.ToChar(32)
		);

		public static string font_size_48 = String.Concat(
			Convert.ToChar(29),
			Convert.ToChar(33),
			Convert.ToChar(48)
		);

		public static string font_size_64 = String.Concat(
			Convert.ToChar(29),
			Convert.ToChar(33),
			Convert.ToChar(64)
		);

		public static string font_size_80 = String.Concat(
			Convert.ToChar(29),
			Convert.ToChar(33),
			Convert.ToChar(80)
		);

		public static string font_size_96 = String.Concat(
			Convert.ToChar(29),
			Convert.ToChar(33),
			Convert.ToChar(96)
		);

		public static string font_size_112 = String.Concat(
			Convert.ToChar(29),
			Convert.ToChar(33),
			Convert.ToChar(112)
		);

		public static string abrir_cajon = String.Concat(
			Convert.ToChar(27),
			Convert.ToChar(112),
			Convert.ToChar(48),
			Convert.ToChar(50),
			Convert.ToChar(1),
			Convert.ToChar(0)
		);
      
		public static string logo = String.Concat(
			Convert.ToChar(29),
			Convert.ToChar(40),
			Convert.ToChar(76),
			Convert.ToChar(6),
			Convert.ToChar(0),
			Convert.ToChar(48),
			Convert.ToChar(69),
			Convert.ToChar(32),
			Convert.ToChar(32),
			Convert.ToChar(1),
			Convert.ToChar(1)
		);

		public static string align_left = String.Concat(
			Convert.ToChar(27),
			"a0",
			Convert.ToChar(0)
		);

		public static string align_center = String.Concat(
			Convert.ToChar(27),
			"a1",
			Convert.ToChar(0)
		);

		public static string align_right = String.Concat(
			Convert.ToChar(27),
			"a2",
			Convert.ToChar(0)
		);

		public static string corte = String.Concat(
			Convert.ToChar(29),
			"V",
			Convert.ToChar(65),
			Convert.ToChar(0)
		);

		public static string barcode(string valor)
		{
			string barcode_string = String.Concat(
				Convert.ToChar(29), //ALTURA
				Convert.ToChar(104),
				Convert.ToChar(50),

				Convert.ToChar(29), //ANCHURA
				Convert.ToChar(119),
				Convert.ToChar(2),

				Convert.ToChar(29), //FUENTE HRI
				Convert.ToChar(102),
				Convert.ToChar(49),

				Convert.ToChar(29), //DONDE SE PONEN LOS CARACTERES
				Convert.ToChar(72),
				Convert.ToChar(50), //48NOIMPRIME,49ARRIBA,50ABAJO,51ARRIBAYABAJO

				Convert.ToChar(29), //IMPRIME EL CODIGO
				Convert.ToChar(107),
				Convert.ToChar(6),
				String.Format("A{0}A", valor.Trim())
			);

			return barcode_string;
		}

		public static string code39(string valor)
		{
			string barcode_string = String.Concat(
				Convert.ToChar(29), //ALTURA
				Convert.ToChar(104),
				Convert.ToChar(50),

				Convert.ToChar(29), //ANCHURA
				Convert.ToChar(119),
				Convert.ToChar(2),

				Convert.ToChar(29), //FUENTE HRI
				Convert.ToChar(102),
				Convert.ToChar(49),

				Convert.ToChar(29), //DONDE SE PONEN LOS CARACTERES
				Convert.ToChar(72),
				Convert.ToChar(50), //48NOIMPRIME,49ARRIBA,50ABAJO,51ARRIBAYABAJO

				Convert.ToChar(29), //IMPRIME EL CODIGO
				Convert.ToChar(107),
				Convert.ToChar(4),
				String.Format("*{0}*", valor.Trim())
			);

			return barcode_string;
		}

		public static string qr_code(string valor)
		{
			string texto = valor.Trim();
			int largo = texto.Length + 3;

			string barcode_string = String.Concat(
				Convert.ToChar(29), //TAMANIO
				Convert.ToChar(40),
				Convert.ToChar(107),
				Convert.ToChar(3),
				Convert.ToChar(0),
				Convert.ToChar(49),
				Convert.ToChar(67),
				Convert.ToChar(6),


				Convert.ToChar(29), //CORRECCION
				Convert.ToChar(40),
				Convert.ToChar(107),
				Convert.ToChar(3),
				Convert.ToChar(0),
				Convert.ToChar(49),
				Convert.ToChar(69),
				Convert.ToChar(48),

				Convert.ToChar(29), //GUARDA QR
				Convert.ToChar(40),
				Convert.ToChar(107),
				Convert.ToChar(largo),
				Convert.ToChar(0),
				Convert.ToChar(49),
				Convert.ToChar(80),
				Convert.ToChar(48),
				texto,

				Convert.ToChar(29), //IMPRIME QR
				Convert.ToChar(40),
				Convert.ToChar(107),
				Convert.ToChar(3),
				Convert.ToChar(0),
				Convert.ToChar(49),
				Convert.ToChar(81),
				Convert.ToChar(48)
			);

			return barcode_string;
		}
	}

    class POS_Caracter
    {
        Dictionary<string, string> keys_field = new Dictionary<string, string>();
        Dictionary<string, string> keys_method = new Dictionary<string, string>();

        public POS_Caracter()
        {
            set_file_keys();
        }

        void set_file_keys()
        {

            FieldInfo[] fields = typeof(POS_Control).GetFields();

            Array.Sort(
                fields, delegate(FieldInfo field_info1, FieldInfo field_info2)
                { 
                    return field_info1.Name.CompareTo(field_info2.Name); 
                }
            );

            foreach (FieldInfo field in fields)
            {
                string key = string.Format("[{0}]",field.Name.ToUpper());
                string value = field.GetValue(null).ToString();

                if(!keys_field.ContainsKey(key))
                {
                    keys_field.Add(key, value);
                }
            }

            MethodInfo[] methodInfos = typeof(POS_Control).GetMethods(BindingFlags.Public | BindingFlags.Static);

            Array.Sort(methodInfos,
                    delegate(MethodInfo methodInfo1, MethodInfo methodInfo2)
                    { return methodInfo1.Name.CompareTo(methodInfo2.Name); });

            foreach (MethodInfo methodInfo in methodInfos)
            {
                string key = string.Format("[{0}]", methodInfo.Name.ToUpper());
                string value = methodInfo.Name;

                if (!keys_method.ContainsKey(key))
                {
                    keys_method.Add(key, value);
                }
            }
        }

        public string replace_label(string input)
        {
            Regex reg = new Regex(@"\[\w+:{0,1}[A-Z0-9_\$-]*\]+");

            string output = reg.Replace(input, delegate(Match match)
            {
                string coincidencia = match.ToString();
                return (keys_field.ContainsKey(coincidencia)) ? keys_field[coincidencia] : keys_method_replace(coincidencia) ;
            });

            return output;
        }

        public string keys_method_replace(string label)
        {
            string[] label_split = label.Split(':');
            string output = "";

            if(label_split.Length.Equals(2))
            {
                string allow_label = label_split[0].Replace("[","").Replace("]","");
                string allow_content = label_split[1].Replace("[", "").Replace("]", "");

                if(allow_label.Equals("BARCODE"))
                {
                    output = POS_Control.barcode(allow_content);
                }
                else if(allow_label.Equals("CODE39"))
                {
                    output = POS_Control.code39(allow_content);
                }
                else if(allow_label.Equals("QR_CODE"))
                {
                    output = POS_Control.qr_code(allow_content);
                }
            }

            return output;
        }

        
    }
}
