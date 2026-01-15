using System;

namespace Farmacontrol_PDV.DTO
{
    /// <summary>
    /// DTO for Terminal Payment API responses
    /// Used to deserialize responses from TotalPos API (http://localhost:5077)
    /// </summary>
    public class DTO_Terminal_Response
    {
        /// <summary>
        /// Response code from the terminal
        /// "00" = Approved, other codes indicate errors
        /// </summary>
        public string codigoRespuesta { get; set; }

        /// <summary>
        /// Message/legend from the terminal (e.g., "APROBADA 623521")
        /// </summary>
        public string leyenda { get; set; }

        /// <summary>
        /// Authorization code for approved transactions
        /// </summary>
        public string autorizacion { get; set; }

        /// <summary>
        /// Financial reference for the transaction
        /// Used for refunds and cancellations
        /// </summary>
        public string referenciaFinanciera { get; set; }

        /// <summary>
        /// Indicates if the transaction was approved
        /// </summary>
        public bool aprobada { get; set; }

        /// <summary>
        /// Success indicator (used in initialize and health check)
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// Additional message from the API
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// Status field (used in health check)
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// Indicates if a cancellation was successful
        /// </summary>
        public bool cancelled { get; set; }

        /// <summary>
        /// Check if the transaction was successful (approved or success)
        /// </summary>
        public bool IsSuccessful
        {
            get
            {
                return aprobada || success || codigoRespuesta == "00";
            }
        }

        /// <summary>
        /// Get a display message for the user
        /// </summary>
        public string DisplayMessage
        {
            get
            {
                if (!string.IsNullOrEmpty(leyenda))
                    return leyenda;
                if (!string.IsNullOrEmpty(message))
                    return message;
                return "Sin mensaje";
            }
        }
    }
}
