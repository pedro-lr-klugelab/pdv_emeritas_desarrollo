using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
using RestSharp;
using RestSharp.Deserializers;
using Farmacontrol_PDV.CLASSES;

namespace Farmacontrol_PDV.HELPERS
{
    class Red_helper
    {
        public static Boolean checa_online(string direccion_ip)
        {
			try{
				Ping ping = new Ping();
				String host = direccion_ip;
				byte[] buffer = new byte[32];
				int timeout = 5000;
				PingOptions pingOptions = new PingOptions();
				PingReply reply = ping.Send(host, timeout, buffer, pingOptions);
			
				if (reply.Status == IPStatus.Success)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch(NetworkInformationException exception)
			{
				Log_error.log(exception);
				return false;
			}
        }

		public static Boolean checa_rest(string direccion_ip = null)
		{
			try
			{
				if (direccion_ip == null)
				{
					direccion_ip = Properties.Configuracion.Default.main_server;
				}

				RestClient client = new RestClient(String.Format("http://{0}/", direccion_ip));
				RestRequest request = new RestRequest("rest/main/checa_rest", Method.GET);

				request.Timeout = 5000;
				request.RequestFormat = DataFormat.Json;

				IRestResponse response = client.Execute(request);                
				JsonDeserializer json_deserializer = new JsonDeserializer();

				Checa_rest_result result = json_deserializer.Deserialize<Checa_rest_result>(response); 

				if(result.result)
				{
					return true;
				}
                else
                {
                    Exception ex = new Exception(response.ErrorMessage);
                    Log_error.log(ex);
                }
			}
			catch (Exception exception)
			{
				Log_error.log(exception);
			}

			return false;
		}

        public static Boolean checa_rest_online(string direccion_ip = null)
        {

            try
            {
                if (direccion_ip == null)
                {
                    direccion_ip = Properties.Configuracion.Default.main_server;
                }
                string ip_local = "192.168.1.250";
                RestClient client = new RestClient(String.Format("http://{0}/", ip_local));
                RestRequest request = new RestRequest("rest/main/checa_rest_sucursal_destino", Method.POST);

                request.AddParameter("ip_sucursal", direccion_ip);
                request.Timeout = 5000;
                request.RequestFormat = DataFormat.Json;

                IRestResponse response = client.Execute(request);
                JsonDeserializer json_deserializer = new JsonDeserializer();

                Checa_rest_result result = json_deserializer.Deserialize<Checa_rest_result>(response);

                if (result.result)
                {
                    return true;
                }
                else
                {
                    Exception ex = new Exception(response.ErrorMessage);
                    Log_error.log(ex);
                }
            }
            catch (Exception exception)
            {
                Log_error.log(exception);
            }

            return false;
        
        
        }


        //NUEVOS METODOS
        public static Boolean afectar_traspaso_origen_sucursal(int remote_id, int sucursal_id, int traspaso_origen)
        {
             try
            {
                string ip_sucursal = Sucursales_helper.get_ip_sucursal(sucursal_id);
                string sucursal_local = "192.168.1.250";
                RestClient client = new RestClient(String.Format("http://{0}/", sucursal_local));
                RestRequest request = new RestRequest("rest/traspasos/afectar_traspaso_sucursal", Method.POST);
               
               // Rest_parameters parameters = new Rest_parameters();
                //parameters.Add("traspaso_id", 12942);//parameters.Add("traspaso_id", traspaso_origen);
               // parameters.Add("remote_id", 888);//parameters.Add("remote_id", remote_id);
               // parameters.Add("sucursal_ip", "172.16.1.38");//parameters.Add("sucursal_ip", ip_sucursal);
                request.AddParameter("traspaso_id", traspaso_origen);//folio de la sucursal recibe
                request.AddParameter("remote_id", remote_id);//traspaso de la sucursal origen(envia)
                request.AddParameter("sucursal_ip", ip_sucursal);
                request.Timeout = 5000;
                request.RequestFormat = DataFormat.Json;

                IRestResponse response = client.Execute(request);
                JsonDeserializer json_deserializer = new JsonDeserializer();
                //return "";

                Valida_traspaso_result result = json_deserializer.Deserialize<Valida_traspaso_result>(response);
                if (result.result)
                {
                    return true;
                }
                else
                {
                    return false;
                }
   

             }
             catch (Exception exception)
             {
                 Log_error.log(exception);
             }

            return false;
        }
    }
}
