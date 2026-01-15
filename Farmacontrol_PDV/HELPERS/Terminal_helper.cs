using System;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Farmacontrol_PDV.DTO;

namespace Farmacontrol_PDV.HELPERS
{
    /// <summary>
    /// Helper class for communication with the Terminal Payment API (TotalPos API)
    /// API runs on http://localhost:5077
    /// </summary>
    public static class Terminal_helper
    {
        private const string API_BASE_URL = "http://localhost:5077";
        private const int TIMEOUT_MS = 120000; // 120 seconds for card operations

        private static bool _isInitialized = false;

        /// <summary>
        /// Indicates if the Terminal API has been initialized successfully
        /// </summary>
        public static bool IsInitialized
        {
            get { return _isInitialized; }
        }

        /// <summary>
        /// Initialize the Terminal SDK. Must be called before processing any payments.
        /// </summary>
        /// <returns>DTO_Terminal_Response with the result of the initialization</returns>
        public static DTO_Terminal_Response Initialize()
        {
            try
            {
                string url = $"{API_BASE_URL}/api/initialize";
                string response = MakePostRequest(url, "{}");

                var result = JsonConvert.DeserializeObject<DTO_Terminal_Response>(response);

                if (result != null && result.success)
                {
                    _isInitialized = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                return new DTO_Terminal_Response
                {
                    success = false,
                    aprobada = false,
                    codigoRespuesta = "99",
                    leyenda = $"Error de conexión: {ex.Message}",
                    message = ex.Message
                };
            }
        }

        /// <summary>
        /// Check if the Terminal API is running and healthy
        /// </summary>
        /// <returns>true if API is healthy, false otherwise</returns>
        public static bool CheckHealth()
        {
            try
            {
                string url = $"{API_BASE_URL}/api/health";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Timeout = 5000; // 5 second timeout for health check
                request.ContentType = "application/json";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Process a sale/payment through the terminal
        /// </summary>
        /// <param name="amount">Amount to charge</param>
        /// <param name="reference">Reference/folio for the transaction</param>
        /// <returns>DTO_Terminal_Response with the result of the sale</returns>
        public static DTO_Terminal_Response ProcessSale(decimal amount, string reference)
        {
            try
            {
                // Check if API is initialized
                if (!_isInitialized)
                {
                    var initResult = Initialize();
                    if (!initResult.success && !_isInitialized)
                    {
                        return new DTO_Terminal_Response
                        {
                            success = false,
                            aprobada = false,
                            codigoRespuesta = "98",
                            leyenda = "Terminal no inicializada. Por favor verifique que el terminal esté conectado.",
                            message = "Terminal not initialized"
                        };
                    }
                }

                string url = $"{API_BASE_URL}/api/sale";
                
                var requestBody = new
                {
                    Amount = amount,
                    Reference = reference
                };

                string jsonBody = JsonConvert.SerializeObject(requestBody);
                string response = MakePostRequest(url, jsonBody);

                var result = JsonConvert.DeserializeObject<DTO_Terminal_Response>(response);
                return result ?? new DTO_Terminal_Response
                {
                    success = false,
                    aprobada = false,
                    codigoRespuesta = "97",
                    leyenda = "Respuesta inválida del terminal",
                    message = "Invalid response from terminal"
                };
            }
            catch (WebException webEx)
            {
                string errorMessage = "Error de conexión con el terminal";
                
                if (webEx.Status == WebExceptionStatus.Timeout)
                {
                    errorMessage = "Tiempo de espera agotado. La operación tardó demasiado.";
                }
                else if (webEx.Status == WebExceptionStatus.ConnectFailure)
                {
                    errorMessage = "No se pudo conectar con el terminal. Verifique que el servicio esté ejecutándose.";
                }

                return new DTO_Terminal_Response
                {
                    success = false,
                    aprobada = false,
                    codigoRespuesta = "96",
                    leyenda = errorMessage,
                    message = webEx.Message
                };
            }
            catch (Exception ex)
            {
                return new DTO_Terminal_Response
                {
                    success = false,
                    aprobada = false,
                    codigoRespuesta = "99",
                    leyenda = $"Error inesperado: {ex.Message}",
                    message = ex.Message
                };
            }
        }

        /// <summary>
        /// Process a refund through the terminal
        /// </summary>
        /// <param name="amount">Amount to refund</param>
        /// <param name="reference">Reference for the refund</param>
        /// <param name="financialReference">Financial reference from original sale</param>
        /// <returns>DTO_Terminal_Response with the result of the refund</returns>
        public static DTO_Terminal_Response ProcessRefund(decimal amount, string reference, string financialReference)
        {
            try
            {
                if (!_isInitialized)
                {
                    var initResult = Initialize();
                    if (!initResult.success && !_isInitialized)
                    {
                        return new DTO_Terminal_Response
                        {
                            success = false,
                            aprobada = false,
                            codigoRespuesta = "98",
                            leyenda = "Terminal no inicializada",
                            message = "Terminal not initialized"
                        };
                    }
                }

                string url = $"{API_BASE_URL}/api/refund";

                var requestBody = new
                {
                    Amount = amount,
                    Reference = reference,
                    FinancialReference = financialReference
                };

                string jsonBody = JsonConvert.SerializeObject(requestBody);
                string response = MakePostRequest(url, jsonBody);

                var result = JsonConvert.DeserializeObject<DTO_Terminal_Response>(response);
                return result ?? new DTO_Terminal_Response
                {
                    success = false,
                    aprobada = false,
                    codigoRespuesta = "97",
                    leyenda = "Respuesta inválida del terminal",
                    message = "Invalid response from terminal"
                };
            }
            catch (Exception ex)
            {
                return new DTO_Terminal_Response
                {
                    success = false,
                    aprobada = false,
                    codigoRespuesta = "99",
                    leyenda = $"Error: {ex.Message}",
                    message = ex.Message
                };
            }
        }

        /// <summary>
        /// Makes a POST request to the specified URL with the given JSON body
        /// </summary>
        private static string MakePostRequest(string url, string jsonBody)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Timeout = TIMEOUT_MS;

            byte[] data = Encoding.UTF8.GetBytes(jsonBody);
            request.ContentLength = data.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Get a user-friendly message for a response code
        /// </summary>
        /// <param name="code">Response code from the terminal</param>
        /// <returns>User-friendly message in Spanish</returns>
        public static string GetResponseMessage(string code)
        {
            switch (code)
            {
                case "00":
                    return "Transacción aprobada";
                case "01":
                    return "Referir a emisor - Contacte a su banco";
                case "05":
                    return "Transacción no autorizada";
                case "14":
                    return "Número de tarjeta inválido";
                case "51":
                    return "Fondos insuficientes";
                case "96":
                    return "Error en el sistema del terminal";
                case "97":
                    return "Respuesta inválida del terminal";
                case "98":
                    return "Terminal no inicializada";
                case "99":
                    return "Error de conexión";
                default:
                    return $"Código de respuesta: {code}";
            }
        }
    }
}
