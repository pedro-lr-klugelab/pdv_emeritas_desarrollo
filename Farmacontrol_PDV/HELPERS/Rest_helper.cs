using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using RestSharp;
using RestSharp.Deserializers;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.FORMS.comunes;

namespace Farmacontrol_PDV.HELPERS
{
	class Rest_parameters
	{
		public Dictionary<string, object> parameters = new Dictionary<string, object>();

		public void Add(string key, object value)
		{
			parameters.Add(key, value);
		}
	}

	class Rest_helper
	{
		public static T make_request<T>(string url, Rest_parameters rest_parameters = null, string ip_destino = null, string mensaje = "Procesando...")
		{
			Cursor.Current = Cursors.WaitCursor;

			if (ip_destino == null)
			{
				ip_destino = Properties.Configuracion.Default.main_server;
			}

			string test_url = String.Format("http://{0}/", ip_destino);
			string test_user = Properties.Configuracion.Default.usuario;
			string test_password = Properties.Configuracion.Default.password;
			/*
			Log_error.log("URL:"+test_url);
			Log_error.log("USER:" + test_user);
			Log_error.log("PASSWORD:" + test_password);
			 * */

            RestClient client = new RestClient(String.Format("http://{0}/", ip_destino));
            client.Authenticator = new HttpBasicAuthenticator(
                Properties.Configuracion.Default.usuario,
                Properties.Configuracion.Default.password
            );

            /*
			RestClient client = new RestClient()
			{
				BaseUrl = String.Format("http://{0}/", ip_destino),
				Authenticator = new HttpBasicAuthenticator(
					Properties.Configuracion.Default.usuario,
					Properties.Configuracion.Default.password
				)
			};*/

			RestRequest request = new RestRequest(String.Format("rest/{0}", url), Method.POST);

			if (rest_parameters == null)
			{
				rest_parameters = new Rest_parameters();
			}

			rest_parameters.Add("encrypted_empleado_id", Crypto_helper.Encrypt(Properties.Configuracion.Default.empleado_id.ToString()));
            //
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", "<Valid Json>", ParameterType.RequestBody);
            //

			foreach (KeyValuePair<string, object> parametro in rest_parameters.parameters)
			{
				request.AddParameter(parametro.Key, parametro.Value);
			}

			request.Timeout = Properties.Configuracion.Default.rest_timeout;
			request.RequestFormat = DataFormat.Json;

			T result = Activator.CreateInstance<T>();

			try
			{
				IRestResponse response = client.Execute(request);

				if (response.ResponseStatus == ResponseStatus.Error)
				{
					string error_message;

					switch (response.ErrorMessage)
					{
						case "The operation has timed out":
							error_message = "Ha expirado el tiempo máximo de espera";
							break;

						default:
							error_message = response.ErrorMessage;
							break;
					}

					throw new Exception(error_message);
				}

				switch (response.StatusCode)
				{
					case HttpStatusCode.Unauthorized:
						throw new Exception("Ha fallado la autenticación");

					case HttpStatusCode.InternalServerError:
						throw new Exception("El servidor ha reportado una falla interna");
					default: break;

				}

				if (response.StatusCode == HttpStatusCode.Unauthorized)
				{
					throw new Exception("Ha fallado la autenticación");
				}

				JsonDeserializer json_deserializer = new JsonDeserializer();
				result = json_deserializer.Deserialize<T>(response);

				if (result.GetType().GetProperty("debug_information") != null && result.GetType().GetProperty("debug_information").GetValue(result, null) != null)
				{
					MessageBox.Show(result.GetType().GetProperty("debug_information").GetValue(result, null).ToString(), "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

				if (result.GetType().GetProperty("error_information") != null && result.GetType().GetProperty("error_information").GetValue(result, null) != null)
				{
					throw new Exception(result.GetType().GetProperty("error_information").GetValue(result, null).ToString());
				}

			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

			}

			Cursor.Current = Cursors.Default;
			return result;
		}

        public static T make_request_conexion<T>(string url, Rest_parameters rest_parameters = null, string ip_destino = null, string mensaje = "Procesando...")
        {
            Cursor.Current = Cursors.WaitCursor;

            ip_destino = "192.168.1.250";

            //ip_destino = "172.16.1.5"; //MODO PRUEBA

            string test_url = String.Format("http://{0}/", ip_destino);
            string test_user = Properties.Configuracion.Default.usuario;
            string test_password = Properties.Configuracion.Default.password;
            /*
            Log_error.log("URL:"+test_url);
            Log_error.log("USER:" + test_user);
            Log_error.log("PASSWORD:" + test_password);
             * */

            RestClient client = new RestClient(String.Format("http://{0}/", ip_destino));
            client.Authenticator = new HttpBasicAuthenticator(
                Properties.Configuracion.Default.usuario,
                Properties.Configuracion.Default.password
            );

            /*
			RestClient client = new RestClient()
			{
				BaseUrl = String.Format("http://{0}/", ip_destino),
				Authenticator = new HttpBasicAuthenticator(
					Properties.Configuracion.Default.usuario,
					Properties.Configuracion.Default.password
				)
			};*/

            url = "facturacion/generar_enlace_facturacion";
           // url = "facturacion/generar_enlace_facturacion_prueba";////MODO PRUEBA

            RestRequest request = new RestRequest(String.Format("rest/{0}", url), Method.POST);

            if (rest_parameters == null)
            {
                rest_parameters = new Rest_parameters();
            }

            rest_parameters.Add("encrypted_empleado_id", Crypto_helper.Encrypt(Properties.Configuracion.Default.empleado_id.ToString()));
            //
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", "<Valid Json>", ParameterType.RequestBody);
            //

            foreach (KeyValuePair<string, object> parametro in rest_parameters.parameters)
            {
                request.AddParameter(parametro.Key, parametro.Value);
            }

            request.Timeout = Properties.Configuracion.Default.rest_timeout;
            request.RequestFormat = DataFormat.Json;

            T result = Activator.CreateInstance<T>();

            try
            {
                IRestResponse response = client.Execute(request);

                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    string error_message;

                    switch (response.ErrorMessage)
                    {
                        case "The operation has timed out":
                            error_message = "Ha expirado el tiempo máximo de espera";
                            break;

                        default:
                            error_message = response.ErrorMessage;
                            break;
                    }

                    throw new Exception(error_message);
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new Exception("Ha fallado la autenticación");

                    case HttpStatusCode.InternalServerError:
                        throw new Exception("El servidor ha reportado una falla interna");
                    default: break;

                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Ha fallado la autenticación");
                }

                JsonDeserializer json_deserializer = new JsonDeserializer();
                result = json_deserializer.Deserialize<T>(response);

                if (result.GetType().GetProperty("debug_information") != null && result.GetType().GetProperty("debug_information").GetValue(result, null) != null)
                {
                    MessageBox.Show(result.GetType().GetProperty("debug_information").GetValue(result, null).ToString(), "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (result.GetType().GetProperty("error_information") != null && result.GetType().GetProperty("error_information").GetValue(result, null) != null)
                {
                    throw new Exception(result.GetType().GetProperty("error_information").GetValue(result, null).ToString());
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            Cursor.Current = Cursors.Default;
            return result;
        }

        public static T make_request_envio_correo<T>(string url, Rest_parameters rest_parameters = null, string ip_destino = null, string mensaje = "Procesando...")
        {
            Cursor.Current = Cursors.WaitCursor;

            ip_destino = "192.168.1.250";

            string test_url = String.Format("http://{0}/", ip_destino);
            string test_user = Properties.Configuracion.Default.usuario;
            string test_password = Properties.Configuracion.Default.password;
            /*
            Log_error.log("URL:"+test_url);
            Log_error.log("USER:" + test_user);
            Log_error.log("PASSWORD:" + test_password);
             * */

            RestClient client = new RestClient(String.Format("http://{0}/", ip_destino));
            client.Authenticator = new HttpBasicAuthenticator(
                Properties.Configuracion.Default.usuario,
                Properties.Configuracion.Default.password
            );

            /*
			RestClient client = new RestClient()
			{
				BaseUrl = String.Format("http://{0}/", ip_destino),
				Authenticator = new HttpBasicAuthenticator(
					Properties.Configuracion.Default.usuario,
					Properties.Configuracion.Default.password
				)
			};*/
            url = "facturacion/enviar_correo";
            RestRequest request = new RestRequest(String.Format("rest/{0}", url), Method.POST);

            if (rest_parameters == null)
            {
                rest_parameters = new Rest_parameters();
            }

            rest_parameters.Add("encrypted_empleado_id", Crypto_helper.Encrypt(Properties.Configuracion.Default.empleado_id.ToString()));
            //
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", "<Valid Json>", ParameterType.RequestBody);
            //

            foreach (KeyValuePair<string, object> parametro in rest_parameters.parameters)
            {
                request.AddParameter(parametro.Key, parametro.Value);
            }

            request.Timeout = Properties.Configuracion.Default.rest_timeout;
            request.RequestFormat = DataFormat.Json;

            T result = Activator.CreateInstance<T>();

            try
            {
                IRestResponse response = client.Execute(request);

                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    string error_message;

                    switch (response.ErrorMessage)
                    {
                        case "The operation has timed out":
                            error_message = "Ha expirado el tiempo máximo de espera";
                            break;

                        default:
                            error_message = response.ErrorMessage;
                            break;
                    }

                    throw new Exception(error_message);
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new Exception("Ha fallado la autenticación");

                    case HttpStatusCode.InternalServerError:
                        throw new Exception("El servidor ha reportado una falla interna");
                    default: break;

                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Ha fallado la autenticación");
                }

                JsonDeserializer json_deserializer = new JsonDeserializer();
                result = json_deserializer.Deserialize<T>(response);

                if (result.GetType().GetProperty("debug_information") != null && result.GetType().GetProperty("debug_information").GetValue(result, null) != null)
                {
                    MessageBox.Show(result.GetType().GetProperty("debug_information").GetValue(result, null).ToString(), "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (result.GetType().GetProperty("error_information") != null && result.GetType().GetProperty("error_information").GetValue(result, null) != null)
                {
                    throw new Exception(result.GetType().GetProperty("error_information").GetValue(result, null).ToString());
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            Cursor.Current = Cursors.Default;
            return result;
        }

        public static T make_request_obtener_datos<T>(string url, Rest_parameters rest_parameters = null, string ip_destino = null, string mensaje = "Procesando...")
        {
            Cursor.Current = Cursors.WaitCursor;

            ip_destino = "192.168.1.250";

            string test_url = String.Format("http://{0}/", ip_destino);
            string test_user = Properties.Configuracion.Default.usuario;
            string test_password = Properties.Configuracion.Default.password;
            /*
            Log_error.log("URL:"+test_url);
            Log_error.log("USER:" + test_user);
            Log_error.log("PASSWORD:" + test_password);
             * */

            RestClient client = new RestClient(String.Format("http://{0}/", ip_destino));
            client.Authenticator = new HttpBasicAuthenticator(
                Properties.Configuracion.Default.usuario,
                Properties.Configuracion.Default.password
            );

            /*
			RestClient client = new RestClient()
			{
				BaseUrl = String.Format("http://{0}/", ip_destino),
				Authenticator = new HttpBasicAuthenticator(
					Properties.Configuracion.Default.usuario,
					Properties.Configuracion.Default.password
				)
			};*/
            url = "facturacion/get_informacion_Factura";
            RestRequest request = new RestRequest(String.Format("rest/{0}", url), Method.POST);

            if (rest_parameters == null)
            {
                rest_parameters = new Rest_parameters();
            }

            rest_parameters.Add("encrypted_empleado_id", Crypto_helper.Encrypt(Properties.Configuracion.Default.empleado_id.ToString()));
            //
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", "<Valid Json>", ParameterType.RequestBody);
            //

            foreach (KeyValuePair<string, object> parametro in rest_parameters.parameters)
            {
                request.AddParameter(parametro.Key, parametro.Value);
            }

            request.Timeout = Properties.Configuracion.Default.rest_timeout;
            request.RequestFormat = DataFormat.Json;

            T result = Activator.CreateInstance<T>();

            try
            {
                IRestResponse response = client.Execute(request);

                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    string error_message;

                    switch (response.ErrorMessage)
                    {
                        case "The operation has timed out":
                            error_message = "Ha expirado el tiempo máximo de espera";
                            break;

                        default:
                            error_message = response.ErrorMessage;
                            break;
                    }

                    throw new Exception(error_message);
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new Exception("Ha fallado la autenticación");

                    case HttpStatusCode.InternalServerError:
                        throw new Exception("El servidor ha reportado una falla interna");
                    default: break;

                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Ha fallado la autenticación");
                }

                JsonDeserializer json_deserializer = new JsonDeserializer();
                result = json_deserializer.Deserialize<T>(response);

                if (result.GetType().GetProperty("debug_information") != null && result.GetType().GetProperty("debug_information").GetValue(result, null) != null)
                {
                    MessageBox.Show(result.GetType().GetProperty("debug_information").GetValue(result, null).ToString(), "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (result.GetType().GetProperty("error_information") != null && result.GetType().GetProperty("error_information").GetValue(result, null) != null)
                {
                    throw new Exception(result.GetType().GetProperty("error_information").GetValue(result, null).ToString());
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            Cursor.Current = Cursors.Default;
            return result;
        }

        public static T make_request_obtener_ceros<T>(string url, Rest_parameters rest_parameters = null, string ip_destino = null, string mensaje = "Procesando...")
        {
            Cursor.Current = Cursors.WaitCursor;

            ip_destino = "192.168.1.250";
            //ip_destino = "172.16.1.5";

            string test_url = String.Format("http://{0}/", ip_destino);
            string test_user = Properties.Configuracion.Default.usuario;
            string test_password = Properties.Configuracion.Default.password;
            /*
            Log_error.log("URL:"+test_url);
            Log_error.log("USER:" + test_user);
            Log_error.log("PASSWORD:" + test_password);
             * */

            RestClient client = new RestClient(String.Format("http://{0}/", ip_destino));
            client.Authenticator = new HttpBasicAuthenticator(
                Properties.Configuracion.Default.usuario,
                Properties.Configuracion.Default.password
            );

            /*
			RestClient client = new RestClient()
			{
				BaseUrl = String.Format("http://{0}/", ip_destino),
				Authenticator = new HttpBasicAuthenticator(
					Properties.Configuracion.Default.usuario,
					Properties.Configuracion.Default.password
				)
			};*/
            url = "ceros/get_ceros_sucursal";
            RestRequest request = new RestRequest(String.Format("{0}", url), Method.POST);

            if (rest_parameters == null)
            {
                rest_parameters = new Rest_parameters();
            }

            rest_parameters.Add("encrypted_empleado_id", Crypto_helper.Encrypt(Properties.Configuracion.Default.empleado_id.ToString()));
            //
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", "<Valid Json>", ParameterType.RequestBody);
            //

            foreach (KeyValuePair<string, object> parametro in rest_parameters.parameters)
            {
                request.AddParameter(parametro.Key, parametro.Value);
            }

            request.Timeout = Properties.Configuracion.Default.rest_timeout;
            request.RequestFormat = DataFormat.Json;

            T result = Activator.CreateInstance<T>();

            try
            {
                IRestResponse response = client.Execute(request);

                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    string error_message;

                    switch (response.ErrorMessage)
                    {
                        case "The operation has timed out":
                            error_message = "Ha expirado el tiempo máximo de espera";
                            break;

                        default:
                            error_message = response.ErrorMessage;
                            break;
                    }

                    throw new Exception(error_message);
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new Exception("Ha fallado la autenticación");

                    case HttpStatusCode.InternalServerError:
                        throw new Exception("El servidor ha reportado una falla interna");
                    default: break;

                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Ha fallado la autenticación");
                }

                JsonDeserializer json_deserializer = new JsonDeserializer();
               
                result = json_deserializer.Deserialize<T>(response);

                if (result.GetType().GetProperty("debug_information") != null && result.GetType().GetProperty("debug_information").GetValue(result, null) != null)
                {
                    MessageBox.Show(result.GetType().GetProperty("debug_information").GetValue(result, null).ToString(), "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (result.GetType().GetProperty("error_information") != null && result.GetType().GetProperty("error_information").GetValue(result, null) != null)
                {
                    throw new Exception(result.GetType().GetProperty("error_information").GetValue(result, null).ToString());
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            Cursor.Current = Cursors.Default;
            return result;
        }

        public static T make_request_valida_almacen<T>(string url, Rest_parameters rest_parameters = null, string ip_destino = null, string mensaje = "Procesando...")
        {
            Cursor.Current = Cursors.WaitCursor;

            ip_destino = "192.168.1.250";
         

            string test_url = String.Format("http://{0}/", ip_destino);
            string test_user = Properties.Configuracion.Default.usuario;
            string test_password = Properties.Configuracion.Default.password;
           

            RestClient client = new RestClient(String.Format("http://{0}/", ip_destino));
            client.Authenticator = new HttpBasicAuthenticator(
                Properties.Configuracion.Default.usuario,
                Properties.Configuracion.Default.password
            );


            url = "almacen_traspasos/almacen_sucursal";
            RestRequest request = new RestRequest(String.Format("{0}", url), Method.POST);

            if (rest_parameters == null)
            {
                rest_parameters = new Rest_parameters();
            }

            rest_parameters.Add("encrypted_empleado_id", Crypto_helper.Encrypt(Properties.Configuracion.Default.empleado_id.ToString()));
            //
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", "<Valid Json>", ParameterType.RequestBody);
            //

            foreach (KeyValuePair<string, object> parametro in rest_parameters.parameters)
            {
                request.AddParameter(parametro.Key, parametro.Value);
            }

            request.Timeout = Properties.Configuracion.Default.rest_timeout;
            request.RequestFormat = DataFormat.Json;

            T result = Activator.CreateInstance<T>();

            try
            {
                IRestResponse response = client.Execute(request);

                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    string error_message;

                    switch (response.ErrorMessage)
                    {
                        case "The operation has timed out":
                            error_message = "Ha expirado el tiempo máximo de espera";
                            break;

                        default:
                            error_message = response.ErrorMessage;
                            break;
                    }

                    throw new Exception(error_message);
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new Exception("Ha fallado la autenticación");

                    case HttpStatusCode.InternalServerError:
                        throw new Exception("El servidor ha reportado una falla interna");
                    default: break;

                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Ha fallado la autenticación");
                }

                JsonDeserializer json_deserializer = new JsonDeserializer();

                result = json_deserializer.Deserialize<T>(response);

                if (result.GetType().GetProperty("debug_information") != null && result.GetType().GetProperty("debug_information").GetValue(result, null) != null)
                {
                    MessageBox.Show(result.GetType().GetProperty("debug_information").GetValue(result, null).ToString(), "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (result.GetType().GetProperty("error_information") != null && result.GetType().GetProperty("error_information").GetValue(result, null) != null)
                {
                    throw new Exception(result.GetType().GetProperty("error_information").GetValue(result, null).ToString());
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            Cursor.Current = Cursors.Default;
            return result;
        }

        public static T make_request_valida_folio_almacen<T>(string url, Rest_parameters rest_parameters = null, string ip_destino = null, string mensaje = "Procesando...")
        {
            Cursor.Current = Cursors.WaitCursor;

            ip_destino = "192.168.1.250";


            string test_url = String.Format("http://{0}/", ip_destino);
            string test_user = Properties.Configuracion.Default.usuario;
            string test_password = Properties.Configuracion.Default.password;


            RestClient client = new RestClient(String.Format("http://{0}/", ip_destino));
            client.Authenticator = new HttpBasicAuthenticator(
                Properties.Configuracion.Default.usuario,
                Properties.Configuracion.Default.password
            );


            url = "almacen_traspasos/set_actualiza_pagina_almacen";
            RestRequest request = new RestRequest(String.Format("{0}", url), Method.POST);

            if (rest_parameters == null)
            {
                rest_parameters = new Rest_parameters();
            }

            rest_parameters.Add("encrypted_empleado_id", Crypto_helper.Encrypt(Properties.Configuracion.Default.empleado_id.ToString()));
            //
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", "<Valid Json>", ParameterType.RequestBody);
            //

            foreach (KeyValuePair<string, object> parametro in rest_parameters.parameters)
            {
                request.AddParameter(parametro.Key, parametro.Value);
            }

            request.Timeout = Properties.Configuracion.Default.rest_timeout;
            request.RequestFormat = DataFormat.Json;

            T result = Activator.CreateInstance<T>();

            try
            {
                IRestResponse response = client.Execute(request);

                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    string error_message;

                    switch (response.ErrorMessage)
                    {
                        case "The operation has timed out":
                            error_message = "Ha expirado el tiempo máximo de espera";
                            break;

                        default:
                            error_message = response.ErrorMessage;
                            break;
                    }

                    throw new Exception(error_message);
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new Exception("Ha fallado la autenticación");

                    case HttpStatusCode.InternalServerError:
                        throw new Exception("El servidor ha reportado una falla interna");
                    default: break;

                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Ha fallado la autenticación");
                }

                JsonDeserializer json_deserializer = new JsonDeserializer();

                result = json_deserializer.Deserialize<T>(response);

                if (result.GetType().GetProperty("debug_information") != null && result.GetType().GetProperty("debug_information").GetValue(result, null) != null)
                {
                    MessageBox.Show(result.GetType().GetProperty("debug_information").GetValue(result, null).ToString(), "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (result.GetType().GetProperty("error_information") != null && result.GetType().GetProperty("error_information").GetValue(result, null) != null)
                {
                    throw new Exception(result.GetType().GetProperty("error_information").GetValue(result, null).ToString());
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            Cursor.Current = Cursors.Default;
            return result;
        }

		public static string serialize(object input)
		{
			RestRequest request = new RestRequest();
			request.RequestFormat = DataFormat.Json;

			return request.JsonSerializer.Serialize(input);
		}

        public static T make_request_cancelar_factura<T>(string url, Rest_parameters rest_parameters = null, string ip_destino = null, string mensaje = "Procesando...")
        {
            Cursor.Current = Cursors.WaitCursor;

            
           ip_destino = "192.168.1.250";
            

            string test_url = String.Format("http://{0}/", ip_destino);
            string test_user = Properties.Configuracion.Default.usuario;
            string test_password = Properties.Configuracion.Default.password;
          

            RestClient client = new RestClient(String.Format("http://{0}/", ip_destino));
            client.Authenticator = new HttpBasicAuthenticator(
                Properties.Configuracion.Default.usuario,
                Properties.Configuracion.Default.password
            );

            /*
			RestClient client = new RestClient()
			{
				BaseUrl = String.Format("http://{0}/", ip_destino),
				Authenticator = new HttpBasicAuthenticator(
					Properties.Configuracion.Default.usuario,
					Properties.Configuracion.Default.password
				)
			};*/

            url = "facturacion/cancelar_nota_facturada_credito";
            RestRequest request = new RestRequest(String.Format("rest/{0}", url), Method.POST);

            if (rest_parameters == null)
            {
                rest_parameters = new Rest_parameters();
            }

            rest_parameters.Add("encrypted_empleado_id", Crypto_helper.Encrypt(Properties.Configuracion.Default.empleado_id.ToString()));
            //
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", "<Valid Json>", ParameterType.RequestBody);
            //

            foreach (KeyValuePair<string, object> parametro in rest_parameters.parameters)
            {
                request.AddParameter(parametro.Key, parametro.Value);
            }

            request.Timeout = Properties.Configuracion.Default.rest_timeout;
            request.RequestFormat = DataFormat.Json;

            T result = Activator.CreateInstance<T>();

            try
            {
                IRestResponse response = client.Execute(request);

                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    string error_message;

                    switch (response.ErrorMessage)
                    {
                        case "The operation has timed out":
                            error_message = "Ha expirado el tiempo máximo de espera";
                            break;

                        default:
                            error_message = response.ErrorMessage;
                            break;
                    }

                    throw new Exception(error_message);
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new Exception("Ha fallado la autenticación");

                    case HttpStatusCode.InternalServerError:
                        throw new Exception("El servidor ha reportado una falla interna");
                    default: break;

                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Ha fallado la autenticación");
                }

                JsonDeserializer json_deserializer = new JsonDeserializer();
                result = json_deserializer.Deserialize<T>(response);

                if (result.GetType().GetProperty("debug_information") != null && result.GetType().GetProperty("debug_information").GetValue(result, null) != null)
                {
                    MessageBox.Show(result.GetType().GetProperty("debug_information").GetValue(result, null).ToString(), "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (result.GetType().GetProperty("error_information") != null && result.GetType().GetProperty("error_information").GetValue(result, null) != null)
                {
                    throw new Exception(result.GetType().GetProperty("error_information").GetValue(result, null).ToString());
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            Cursor.Current = Cursors.Default;
            return result;
        }

        public static T make_request_cancelar_vm<T>(string url, Rest_parameters rest_parameters = null, string ip_destino = null, string mensaje = "Procesando...")
        {
            Cursor.Current = Cursors.WaitCursor;


            ip_destino = "192.168.1.250";


            string test_url = String.Format("http://{0}/", ip_destino);
            string test_user = Properties.Configuracion.Default.usuario;
            string test_password = Properties.Configuracion.Default.password;


            RestClient client = new RestClient(String.Format("http://{0}/", ip_destino));
            client.Authenticator = new HttpBasicAuthenticator(
                Properties.Configuracion.Default.usuario,
                Properties.Configuracion.Default.password
            );

            /*
			RestClient client = new RestClient()
			{
				BaseUrl = String.Format("http://{0}/", ip_destino),
				Authenticator = new HttpBasicAuthenticator(
					Properties.Configuracion.Default.usuario,
					Properties.Configuracion.Default.password
				)
			};*/

            url = "facturacion/cancelar_nota_facturada_vm";
            RestRequest request = new RestRequest(String.Format("rest/{0}", url), Method.POST);

            if (rest_parameters == null)
            {
                rest_parameters = new Rest_parameters();
            }

            rest_parameters.Add("encrypted_empleado_id", Crypto_helper.Encrypt(Properties.Configuracion.Default.empleado_id.ToString()));
            //
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", "<Valid Json>", ParameterType.RequestBody);
            //

            foreach (KeyValuePair<string, object> parametro in rest_parameters.parameters)
            {
                request.AddParameter(parametro.Key, parametro.Value);
            }

            request.Timeout = Properties.Configuracion.Default.rest_timeout;
            request.RequestFormat = DataFormat.Json;

            T result = Activator.CreateInstance<T>();

            try
            {
                IRestResponse response = client.Execute(request);

                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    string error_message;

                    switch (response.ErrorMessage)
                    {
                        case "The operation has timed out":
                            error_message = "Ha expirado el tiempo máximo de espera";
                            break;

                        default:
                            error_message = response.ErrorMessage;
                            break;
                    }

                    throw new Exception(error_message);
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new Exception("Ha fallado la autenticación");

                    case HttpStatusCode.InternalServerError:
                        throw new Exception("El servidor ha reportado una falla interna");
                    default: break;

                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Ha fallado la autenticación");
                }

                JsonDeserializer json_deserializer = new JsonDeserializer();
                result = json_deserializer.Deserialize<T>(response);

                if (result.GetType().GetProperty("debug_information") != null && result.GetType().GetProperty("debug_information").GetValue(result, null) != null)
                {
                    MessageBox.Show(result.GetType().GetProperty("debug_information").GetValue(result, null).ToString(), "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (result.GetType().GetProperty("error_information") != null && result.GetType().GetProperty("error_information").GetValue(result, null) != null)
                {
                    throw new Exception(result.GetType().GetProperty("error_information").GetValue(result, null).ToString());
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            Cursor.Current = Cursors.Default;
            return result;
        }




        /*****
         * 
         * CLASE PARA ENVIO A ENLACE VITAL
         * 
         * 
         * **/
        public static T enlace_webservice_lealtad<T>(string url, Rest_parameters rest_parameters = null, string ip_destino = null, string mensaje = "Procesando..." )
        {
            Cursor.Current = Cursors.WaitCursor;
           // ip_destino = "192.168.1.250";
            string test_url = String.Format("http://{0}/", ip_destino);
            string test_user = Properties.Configuracion.Default.usuario;
            string test_password = Properties.Configuracion.Default.password;

            RestClient client = new RestClient(String.Format("http://{0}/", ip_destino));
            client.Authenticator = new HttpBasicAuthenticator(
                Properties.Configuracion.Default.usuario,
                Properties.Configuracion.Default.password
            );

           // url = "facturacion/cancelar_nota_facturada_vm";
            RestRequest request = new RestRequest(String.Format("{0}", url), Method.POST);
            if (rest_parameters == null)
            {
                rest_parameters = new Rest_parameters();
            }

            rest_parameters.Add("encrypted_empleado_id", Crypto_helper.Encrypt(Properties.Configuracion.Default.empleado_id.ToString()));          
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", "<Valid Json>", ParameterType.RequestBody);
          
            foreach (KeyValuePair<string, object> parametro in rest_parameters.parameters)
            {
                request.AddParameter(parametro.Key, parametro.Value);
            }

            request.Timeout = Properties.Configuracion.Default.rest_timeout;
            request.RequestFormat = DataFormat.Json;

            T result = Activator.CreateInstance<T>();

            try
            {
                IRestResponse response = client.Execute(request);

                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    string error_message;

                    switch (response.ErrorMessage)
                    {
                        case "The operation has timed out":
                            error_message = "Ha expirado el tiempo máximo de espera";
                            break;

                        default:
                            error_message = response.ErrorMessage;
                            break;
                    }

                    throw new Exception(error_message);
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new Exception("Ha fallado la autenticación");

                    case HttpStatusCode.InternalServerError:
                        throw new Exception("El servidor ha reportado una falla interna");
                    default: break;

                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Ha fallado la autenticación");
                }

                JsonDeserializer json_deserializer = new JsonDeserializer();
                result = json_deserializer.Deserialize<T>(response);

                if (result.GetType().GetProperty("debug_information") != null && result.GetType().GetProperty("debug_information").GetValue(result, null) != null)
                {
                    MessageBox.Show(result.GetType().GetProperty("debug_information").GetValue(result, null).ToString(), "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (result.GetType().GetProperty("error_information") != null && result.GetType().GetProperty("error_information").GetValue(result, null) != null)
                {
                    throw new Exception(result.GetType().GetProperty("error_information").GetValue(result, null).ToString());
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            Cursor.Current = Cursors.Default;
            return result;
        }

        #region SOY TU FAN 
        

        public static T enlace_webservice_soyfan<T>(string url, Rest_parameters rest_parameters = null, string ip_destino = null, string mensaje = "Procesando...")
        {
            Cursor.Current = Cursors.WaitCursor;
            string test_url = String.Format("http://{0}/", ip_destino);
            string test_user = Properties.Configuracion.Default.usuario;
            string test_password = Properties.Configuracion.Default.password;

            RestClient client = new RestClient(String.Format("http://{0}/", ip_destino));
            client.Authenticator = new HttpBasicAuthenticator(
                Properties.Configuracion.Default.usuario,
                Properties.Configuracion.Default.password
            );

            // url = "facturacion/cancelar_nota_facturada_vm";
            RestRequest request = new RestRequest(String.Format("{0}", url), Method.POST);
            if (rest_parameters == null)
            {
                rest_parameters = new Rest_parameters();
            }

            rest_parameters.Add("encrypted_empleado_id", Crypto_helper.Encrypt(Properties.Configuracion.Default.empleado_id.ToString()));
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", "<Valid Json>", ParameterType.RequestBody);

            foreach (KeyValuePair<string, object> parametro in rest_parameters.parameters)
            {
                request.AddParameter(parametro.Key, parametro.Value);
            }

            request.Timeout = Properties.Configuracion.Default.rest_timeout;
            request.RequestFormat = DataFormat.Json;

            T result = Activator.CreateInstance<T>();

            try
            {
                IRestResponse response = client.Execute(request);

                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    string error_message;

                    switch (response.ErrorMessage)
                    {
                        case "The operation has timed out":
                            error_message = "Ha expirado el tiempo máximo de espera";
                            break;

                        default:
                            error_message = response.ErrorMessage;
                            break;
                    }

                    throw new Exception(error_message);
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new Exception("Ha fallado la autenticación");

                    case HttpStatusCode.InternalServerError:
                        throw new Exception("El servidor ha reportado una falla interna");
                    default: break;

                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Ha fallado la autenticación");
                }

                JsonDeserializer json_deserializer = new JsonDeserializer();
                result = json_deserializer.Deserialize<T>(response);

                if (result.GetType().GetProperty("debug_information") != null && result.GetType().GetProperty("debug_information").GetValue(result, null) != null)
                {
                    MessageBox.Show(result.GetType().GetProperty("debug_information").GetValue(result, null).ToString(), "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (result.GetType().GetProperty("error_information") != null && result.GetType().GetProperty("error_information").GetValue(result, null) != null)
                {
                    throw new Exception(result.GetType().GetProperty("error_information").GetValue(result, null).ToString());
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            Cursor.Current = Cursors.Default;
            return result;
        }

        #endregion


        #region CIRCULO SALUD


        public static T enlace_webservice_Circulo_Oro<T>(string url, Rest_parameters rest_parameters = null, string ip_destino = null, string mensaje = "Procesando...")
        {
            Cursor.Current = Cursors.WaitCursor;
            string test_url = String.Format("http://{0}/", ip_destino);
            string test_user = Properties.Configuracion.Default.usuario;
            string test_password = Properties.Configuracion.Default.password;

            RestClient client = new RestClient(String.Format("http://{0}/", ip_destino));
            client.Authenticator = new HttpBasicAuthenticator(
                Properties.Configuracion.Default.usuario,
                Properties.Configuracion.Default.password
            );

            // url = "facturacion/cancelar_nota_facturada_vm";
            RestRequest request = new RestRequest(String.Format("{0}", url), Method.POST);
            if (rest_parameters == null)
            {
                rest_parameters = new Rest_parameters();
            }

            rest_parameters.Add("encrypted_empleado_id", Crypto_helper.Encrypt(Properties.Configuracion.Default.empleado_id.ToString()));
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", "<Valid Json>", ParameterType.RequestBody);

            foreach (KeyValuePair<string, object> parametro in rest_parameters.parameters)
            {
                request.AddParameter(parametro.Key, parametro.Value);
            }

            request.Timeout = Properties.Configuracion.Default.rest_timeout;
            request.RequestFormat = DataFormat.Json;

            T result = Activator.CreateInstance<T>();

            try
            {
                IRestResponse response = client.Execute(request);

                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    string error_message;

                    switch (response.ErrorMessage)
                    {
                        case "The operation has timed out":
                            error_message = "Ha expirado el tiempo máximo de espera";
                            break;

                        default:
                            error_message = response.ErrorMessage;
                            break;
                    }

                    throw new Exception(error_message);
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new Exception("Ha fallado la autenticación");

                    case HttpStatusCode.InternalServerError:
                        throw new Exception("El servidor ha reportado una falla interna");
                    default: break;

                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Ha fallado la autenticación");
                }

                JsonDeserializer json_deserializer = new JsonDeserializer();
                result = json_deserializer.Deserialize<T>(response);

                if (result.GetType().GetProperty("debug_information") != null && result.GetType().GetProperty("debug_information").GetValue(result, null) != null)
                {
                    MessageBox.Show(result.GetType().GetProperty("debug_information").GetValue(result, null).ToString(), "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (result.GetType().GetProperty("error_information") != null && result.GetType().GetProperty("error_information").GetValue(result, null) != null)
                {
                    throw new Exception(result.GetType().GetProperty("error_information").GetValue(result, null).ToString());
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            Cursor.Current = Cursors.Default;
            return result;
        }

        #endregion





    }
}
