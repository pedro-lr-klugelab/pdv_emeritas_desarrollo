using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.DAO;
using System.IO;
using Farmacontrol_PDV.ACP;
using Farmacontrol_PDV.CLASSES;
using System.Xml.Serialization;

namespace Farmacontrol_PDV.HELPERS
{
	class PdfWSP
	{
		public bool status { set; get; }
		public string mensaje { set; get; }
		public byte[] pdf { set; get; }
	}

	class WebServicePac_helper
	{
		public static string port = "ACP-CFDIPort";

		public static string md5(string password)
		{
			System.Security.Cryptography.MD5 md5;
			md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			Byte[] encodedBytes = md5.ComputeHash(ASCIIEncoding.Default.GetBytes(password));

			return System.Text.RegularExpressions.Regex.Replace(BitConverter.ToString(encodedBytes).ToLower(), @"-", "");
		}

        public static FacturaWSP existe_nota_credito(long venta_id)
        {
            Rest_parameters parametros = new Rest_parameters();
            parametros.Add("venta_id", venta_id);
            parametros.Add("sucursal_id", Config_helper.get_config_local("sucursal_id"));

            return Rest_helper.make_request<FacturaWSP>("facturacion_sf/existe_nota_credito", parametros);
        }

		public static FacturaWSP existe_factura(long venta_id)
		{   
            //FacturaWSP val = new FacturaWSP();
            
			Rest_parameters parametros = new Rest_parameters();
			parametros.Add("venta_id",venta_id);
			parametros.Add("sucursal_id",Config_helper.get_config_local("sucursal_id"));

            return Rest_helper.make_request<FacturaWSP>("facturacion33_sf/existe_factura", parametros);
            
            //val.status = true;
            //return val;
		}

		public static FacturaWSP obtenerDatos(long venta_id, long? sucursal_id = null)
		{
			FacturaWSP val = new FacturaWSP();
			Rest_parameters parametros = new Rest_parameters();
			parametros.Add("venta_id", venta_id);
			parametros.Add("sucursal_id", (sucursal_id == null) ? Convert.ToInt32(Config_helper.get_config_local("sucursal_id")) : sucursal_id );

			//val = Rest_helper.make_request<FacturaWSP>("facturacion33_sf/get_informacion_Factura", parametros);
            val = Rest_helper.make_request_obtener_datos<FacturaWSP>("facturacion33_sf/get_informacion_Factura", parametros);

			return val;
		}

		public static DTO_Validacion cancelar(long venta_id)
		{
            
			DTO_Validacion val = new DTO_Validacion();
               
			Rest_parameters parametros = new Rest_parameters();
			parametros.Add("venta_id", venta_id);
			parametros.Add("sucursal_id", Convert.ToInt32(Config_helper.get_config_local("sucursal_id")));

            val = Rest_helper.make_request_cancelar_factura<DTO_Validacion>("facturacion33_sf/cancelar", parametros);
            
			return val;
		}

        public static DTO_Validacion envio_cancelacion(long venta_id)
        {

            DTO_Validacion val = new DTO_Validacion();

            Rest_parameters parametros = new Rest_parameters();
            parametros.Add("venta_id", venta_id);
            parametros.Add("sucursal_id", Convert.ToInt32(Config_helper.get_config_local("sucursal_id")));

            val = Rest_helper.make_request<DTO_Validacion>("facturacion33_sf/enviar_aviso_cancelacion", parametros);

         
            return val;
        }

		public static DTO_Validacion get_email_factura(long factura_id)
		{
			DTO_Validacion val = new DTO_Validacion();
            
			Rest_parameters parametros = new Rest_parameters();
			parametros.Add("factura_id", factura_id);

			val = Rest_helper.make_request<DTO_Validacion>("facturacion_sf/get_email_factura", parametros);
            
            //val.status = true;
			return val;
		}

		public static DTO_Validacion enviar(long venta_id, string[] correos_electronicos, bool es_nc = false, long? folio_nc = null)
		{
			DTO_Validacion val = new DTO_Validacion();
            
			Rest_parameters parametros = new Rest_parameters();
			parametros.Add("venta_id",venta_id);
			parametros.Add("sucursal_id",Convert.ToInt32(Config_helper.get_config_local("sucursal_id")));
			parametros.Add("destinatarios",string.Join(",",correos_electronicos));
            parametros.Add("folio_nc",folio_nc);
			parametros.Add("es_nc",(es_nc) ? 1 : 0);

            //val = Rest_helper.make_request<DTO_Validacion>("pruebasfacturacion/enviar_correo", parametros);
            //val = Rest_helper.make_request<DTO_Validacion>("facturacion33_sf/enviar_correo", parametros);
            val = Rest_helper.make_request_envio_correo<DTO_Validacion>("facturacion33_sf/enviar_correo", parametros);
			return val;
		}

		public static FacturaWSP importar(long venta_id, string baseConector64, bool es_nota_credito, bool es_cancelacion = true)
		{
			FacturaWSP objFactura = new FacturaWSP();
            
			try
			{	
				Rest_parameters parametros = new Rest_parameters();
				parametros.Add("conector_txt", baseConector64);
                parametros.Add("venta_id", venta_id);

                DAO_Terminales dao_terminales = new DAO_Terminales();
                parametros.Add("serie", (es_nota_credito) ? dao_terminales.get_terminal_serie_notas_credito() : dao_terminales.get_terminal_serie_facturas());

				DAO_Ventas dao_ventas = new DAO_Ventas();
				var venta_data = dao_ventas.get_venta_data(venta_id);

                if (es_cancelacion)
                {
                    parametros.Add("folio", venta_data.venta_folio);
                }
                else
                {
                    long folio = dao_ventas.get_folio_nota_credito();
                    parametros.Add("folio", folio);
                }
				
				parametros.Add("tipo",(es_nota_credito) ? "NC" : "FA");
				parametros.Add("fecha_venta", Convert.ToDateTime(venta_data.fecha_terminado).ToString("yyyy-MM-dd hh:mm:ss"));

				parametros.Add("sucursal_id",Convert.ToInt32(Config_helper.get_config_local("sucursal_id")));



               //objFactura = Rest_helper.make_request<FacturaWSP>("facturacion33_sf/importar", parametros);
                objFactura = Rest_helper.make_request_conexion<FacturaWSP>("pruebasfacturacion/importar", parametros);
                //objFactura = Rest_helper.make_request<FacturaWSP>("pruebasfacturacion/importar", parametros);

				if(objFactura.status)
				{
					if(es_nota_credito == false)
					{
						DAO_Facturacion dao_facturacion = new DAO_Facturacion();
						dao_facturacion.registrar_factura(venta_id);
					}
				}
			}
			catch(Exception ex)
			{
				Log_error.log(ex);
			}
            
            //objFactura.status = true;
			return objFactura;
		}


        public static DTO_Validacion cancelar_nota_vm(long venta_id)
        {
            DTO_Validacion val = new DTO_Validacion();

            Rest_parameters parametros = new Rest_parameters();
            parametros.Add("venta_id", venta_id);

            val = Rest_helper.make_request_cancelar_vm<DTO_Validacion>("facturacion/cancelar_nota_facturada_vm", parametros);


            return val;
        }

	}
}
