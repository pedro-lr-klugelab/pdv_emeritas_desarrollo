using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.DAO;
using System.Xml;
using System.Xml.Linq;
using Farmacontrol_PDV.HELPERS;
/*using SF_CFDI_WS_TypeLib;*/

namespace Farmacontrol_PDV.CLASSES
{
	class Facturacion
	{
		DAO_Ventas dao_ventas = new DAO_Ventas();
		DAO_Facturacion dao_facturacion = new DAO_Facturacion();

		//private string user = "testing@solucionfactible.com";
		//private string password = "a0123456789";
		//private string user = "";
		//private string password = "";

		private string serie = "";

		public Facturacion()
		{
			DAO_Terminales dao_terminales = new DAO_Terminales();
			serie = dao_terminales.get_terminal_serie_facturas();
			//user = Config_helper.get_config_global("facturacion_usuario");
			//password = Config_helper.get_config_global("facturacion_password");
		}

		public DTO_Validacion enviar_correo(long venta_id, string correos)
		{
			DTO_Validacion validacion = new DTO_Validacion();

			int facturacion_diseno_mail = Convert.ToInt32(HELPERS.Config_helper.get_config_global("facturacion_diseno_mail"));
			string facturacion_asunto = HELPERS.Config_helper.get_config_global("facturacion_asunto");
			string facturacion_mensaje_personalizado = HELPERS.Config_helper.get_config_global("facturacion_mensaje_personalizado");
			int facturacion_diseno_pdf = Convert.ToInt32(HELPERS.Config_helper.get_config_local("facturacion_diseno_pdf"));

			string[] split_correos = correos.Split(new Char[] { ',' });

			string string_xml = @"<envios>";

			foreach (string correos_individuales in split_correos)
			{
				if (correos_individuales.Trim() != "")
				{
					string_xml += string.Format(@"<envio destinatarios='{0}' disenoPDF='{1}' folio='{2}' serie='{3}' />", correos_individuales, "368838", venta_id, "PRUEBAS");
				}
			}

			string_xml += @"</envios>";

			/*
			var result = facturacion_lib.enviarCFDI(user, password, 1551,facturacion_asunto,facturacion_mensaje_personalizado,"false", string_xml, 1);

			validacion.informacion = result.ToString();
			 * */
				
			return validacion;
		}

		public DTO_Validacion importar(long venta_id, DTO_Rfc dto_rfc)
		{
                /*
			CFDI cfdi = new CFDI();

			DTO_Validacion validacion = new DTO_Validacion();
			DAO_Terminales dao_terminales = new DAO_Terminales();

			string cadena_original = dao_ventas.get_informacion_factura(venta_id,dao_terminales.get_terminal_serie_facturas(),dto_rfc, string.Join(",", dto_rfc.correos_electronicos.ToArray()),false);

			Log_error.log(cadena_original);

			string cadena = @"|10568|E|PÚBLICO EN GENERAL|XAXX010101000|MÉXICO||||NEBULOSA|||||23042014:16:42:39|PZA|5|1|CONCEPTO UNO|16|FALSE|0||||COMPROBANTE DE EJEMPLO|PESOS MEXICANOS|MXN|1.00|PAGO EN UNA SOLA EXHIBICIÓN|CONTADO|EFECTIVO|CD JUAREZ; CD MEXICO|01012011; 05032010|6516516651,65169816951|
|108|E|PÚBLICO EN GENERAL|XAXX010101000|MÉXICO||||NEBULOSA|||||23042014:16:42:39|EA|7|5|CONCEPTO DOS|16|FALSE|0||||COMPROBANTE DE EJEMPLO|PESOS MEXICANOS|MXN|1.00|PAGO EN UNA SOLA EXHIBICIÓN|CONTADO|EFECTIVO|CD JUAREZ; CD MEXICO, GUADALAJARA|01013254; 05032985 , 06236589|651651875,65169813268 ; 614651989853|
|108|E|PÚBLICO EN GENERAL|XAXX010101000|MÉXICO||||NEBULOSA|||||23042014:16:42:39|KG|3.2|2.80|CONCEPTO TRES|16|FALSE|0||||COMPROBANTE DE EJEMPLO|PESOS MEXICANOS|MXN|1.00|PAGO EN UNA SOLA EXHIBICIÓN|CONTADO|EFECTIVO|CD JUAREZ; CD MEXICO, GUADALAJARA|01012011; 05032010 , 06082009|6516516651,65169816951 ; 614651989362|
|108|E|PÚBLICO EN GENERAL|XAXX010101000|MÉXICO||||NEBULOSA|||||23042014:16:42:39|PZA|8|9|CONCEPTO CUATRO|16|FALSE|0||||COMPROBANTE DE EJEMPLO|PESOS MEXICANOS|MXN|1.00|PAGO EN UNA SOLA EXHIBICIÓN|CONTADO|EFECTIVO|CD JUAREZ; CD MEXICO, GUADALAJARA|00268011; 05032998 , 06080359|6516511985,65169817820 ; 614651989035|";

			string conector_base64 = EncodeTo64(cadena);

			try
			{
				string correos = "";

				foreach (string correo in dto_rfc.correos_electronicos)
				{
					correos += (correos.Equals("")) ? correo : "," + correo;
				}

				validacion = validar_resultado_creacion(cfdi.importarCFDIBase64(user, password, conector_base64, 1), venta_id, correos);

				Log_error.log("Status: "+validacion.status + " Informacion: "+validacion.informacion);
			}
			catch(Exception e)
			{
				Log_error.log(e.ToString());
			}

			return validacion;
                 * */
            return new DTO_Validacion();
		}

		public DTO_Validacion validar_resultado_creacion(string result_xml, long venta_id, string correos)
		{
			DTO_Validacion validacion = new DTO_Validacion();

			int status_encabezado = 0;
			string mensaje_encabezado = "";
			string status;
			string mensaje;
			string uuid = "";
			long folio = 0;
			string serie;
			string email;
			string operacion;

			XmlDocument xml = new XmlDocument();
			xml.LoadXml(result_xml);

			XmlNodeList xnList = xml.SelectNodes("/RespuestaCreacion");

			foreach (XmlNode xn in xnList)
			{
				status_encabezado = Convert.ToInt32(xn["status"].InnerText);
				mensaje_encabezado = xn["mensaje"].InnerText.ToString();
			}

			if(status_encabezado == 200)
			{
				validacion.status = true;
				validacion.informacion = status_mensaje(status_encabezado);
				xnList = xml.SelectNodes("/RespuestaCreacion/resultadosCreacion/ResultadoCreacion");

				foreach (XmlNode xn in xnList)
				{
					status = xn["estatus"].InnerText;
					mensaje = xn["mensaje"].InnerText;
					uuid = xn["uuid"].InnerText;
					folio = Convert.ToInt64(xn["folio"].InnerText);
					serie = xn["serie"].InnerText;
					email = xn["email"].InnerText;
					operacion = xn["uuid"].InnerText;
				}

				Dictionary<string,string> informacion_factura = obtener_datos(uuid,Convert.ToInt32(folio));

				//dao_facturacion.registrar_factura(venta_id, informacion_factura,correos);
			}
			else
			{
				validacion.status = false;
				validacion.informacion = status_mensaje(status_encabezado);
			}

			return validacion;
		}

		public Dictionary<string,string> obtener_datos(string uuid,int folio)
		{
			string xml_response = "";//facturacion_lib.obtenerDatos(user,password,uuid,folio,serie,1);

			string xml = "";

			XmlDocument xml_document = new XmlDocument();
			xml_document.LoadXml(xml_response);

			string encode_string = "";
			foreach (XmlNode node in xml_document.SelectNodes("//comprobantes"))
			{
				encode_string = xml = node["xml"].InnerText;
			}

			xml = DecodeBase64(encode_string);

			XNamespace cfdi = @"http://www.sat.gob.mx/cfd/3";
			XNamespace tfd = @"http://www.sat.gob.mx/TimbreFiscalDigital";

			var xdocument_parse = XDocument.Parse(xml);

			var elemento_xml_comprobante = xdocument_parse.Element(cfdi + "Comprobante");
			string total = elemento_xml_comprobante.Attribute("total").Value;

			var elemento_xml_emisor = xdocument_parse.Element(cfdi + "Comprobante").Element(cfdi + "Emisor");
			string rfc_emisor = elemento_xml_emisor.Attribute("rfc").Value;

			var elemento_xml_receptor = xdocument_parse.Element(cfdi + "Comprobante").Element(cfdi + "Receptor");
			string rfc_receptor = elemento_xml_receptor.Attribute("rfc").Value;

			var elemento_xml_complemento = xdocument_parse.Element(cfdi + "Comprobante").Element(cfdi + "Complemento").Element(tfd + "TimbreFiscalDigital");

			string codigo_qr = string.Format("?re={0}&rr={1}&tt={2}&id={3}",rfc_emisor,rfc_receptor,PadBoth(total,17),uuid);
			string cadena_original = "||"+elemento_xml_complemento.Attribute("version").Value+"|"+elemento_xml_complemento.Attribute("UUID").Value+"|"+elemento_xml_complemento.Attribute("FechaTimbrado").Value+ "|" + elemento_xml_complemento.Attribute("selloCFD").Value + "|" + elemento_xml_complemento.Attribute("noCertificadoSAT").Value + "||";			
			string sello_digital = elemento_xml_complemento.Attribute("selloCFD").Value.ToString();
			string sello_digital_sat = elemento_xml_complemento.Attribute("selloSAT").Value.ToString();


			Dictionary<string,string> elementos_factura = new Dictionary<string,string>();
			elementos_factura.Add("cadena_original",cadena_original);
			elementos_factura.Add("sello_digital", sello_digital);
			elementos_factura.Add("sello_digital_sat", sello_digital_sat);
			elementos_factura.Add("codigo_qr",codigo_qr);
			elementos_factura.Add("uuid", uuid);

			return elementos_factura;
		}

		public string PadBoth(string source, int length)
		{
			int spaces = length - source.Length;
			int padLeft = spaces / 2 + source.Length;
			return source.PadLeft(padLeft,'0').PadRight(length,'0');
		}

		public string status_mensaje(int codigo)
		{
			string mensaje = "";

			switch(codigo)
			{
				case 200:
					mensaje = "El proceso de creación se ha completado correctamente.";
				break;
				case 500:
					mensaje = "Han ocurrido errores que no han permitido completar el proceso. Reintentar";
				break;
				case 501:
					mensaje = "Error de conexión a la base de datos. Reintentar";
				break;
				case 502:
					mensaje = "Han ocurrido errores al intentar recuperar datos o almacenarlos en la base de datos";
				break;
				case 503:
					mensaje = "Se ha alcanzado el límite de licencias de acceso concurrente a base de datos";
				break;
				case 601:
					mensaje = "Error de autenticación, verifique usuario y contraseña";
				break;
				case 602:
					mensaje = "La cuenta de usuario se encuentra bloqueada";
				break;
				case 603:
					mensaje = "La contraseña de la cuenta ha expirado";
				break;
				case 604:
					mensaje = "Ha excedido el número máximo permitido de intentos de autenticación fallidos, la cuenta se bloqueará.";
				break;
				case 610:
					mensaje = "La acción solicitada no está soportada en la implementación porque no se ha configurado para tal fin o no es posible realizarla. En el método importar significa que la implementación no tiene un Conector de importación de comprobantes configurado";
				break;
				case 611:
					mensaje = "No se han especificado todos los parámetros necesarios para realizar la operación. En el caso del método importar significa que el Conector de importación asignado a esta implementación no puede ser utilizado a través de este en este WebService debido a limitaciones propias del conector.";
				break;
				case 612:
					mensaje = "Archivo malformado. El formato de archivo o secuencia binaria no corresponde a la esperada.";
				break;
				case 632:
					mensaje = "Se ha superado el límite de uso justo para la implementación";
				break;
				case 631:
					mensaje = "La fecha de pago del contrato de la implementación ha expirado";
				break;
				case 630:
					mensaje = "El contrato de la implementación ha expirado";
				break;
				case 626:
					mensaje = "Error de configuración de la implementación";
				break;
				case 625:
					mensaje = "La acción no se puede completar porque requiere que se ejecute una acción previa";
				break;
				case 624:
					mensaje = "Violación de restricción de unicidad";
				break;
				case 623:
					mensaje = "Datos no encontrados";
				break;
				case 622:
					mensaje = "Operación no soportada para la implementación";
				break;
				case 621:
					mensaje = "Argumento no válido";
				break;
				case 620:
					mensaje = "No tiene permiso para realizar la acción.";
				break;
				case 613:
					mensaje = "La secuencia numérica ha llegado al final";
				break;
				case 633:
					mensaje = "La implementación se encuentra inactiva";
				break;
			}

			return mensaje;
		}

		public string DecodeBase64(string encodedString)
		{
			byte[] data = Convert.FromBase64String(encodedString);
			string decodedString = Encoding.UTF8.GetString(data);
			return decodedString;
		}

		public string EncodeTo64(string toEncode)
		{
			byte[] toEncodeAsBytes
				  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
			string returnValue
				  = System.Convert.ToBase64String(toEncodeAsBytes);
			return returnValue;
		}
	}
}
