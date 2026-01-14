using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
    class DTO_Corte
    {
        public long corte_id { set; get; }
        public long venta_inicial { set; get; }
        public long venta_final { set; get; }
        public long corte_folio { set; get; }
        public long empleado_id { set; get; }
        public DateTime? fecha { set; get; }
        public string tipo { set; get; }
        public decimal importe_bruto { set; get; }
        public decimal importe_excento { set; get; }
        public decimal importe_descuento_excento { set; get; }
        public decimal importe_gravado { set; get; }
        public decimal importe_descuento_gravado { set; get; }
        public decimal importe_iva { set; get; }
        public decimal importe_ieps { set; get; }
        public decimal importe_total { set; get; }
        public decimal importe_cancelaciones { set; get; }
        public decimal vales_emitidos { set; get; }
        public decimal importe_prepagado { set; get; }
        public decimal importe_prepagado_canjeado { set; get; }
        public decimal importe_prepagado_cancelado { set; get; }
        public string nombre_empleado { set; get; }
        public string nombre_terminal { set; get; }
    }

    // Item genérico (para Vaucheras y Extras)
    public class DTO_ItemResumen
    {
        public string Concepto { get; set; }
        public decimal Cantidad { get; set; }          // para extras puede ser 0 si no aplica
        public decimal Importe { get; set; }
    }

    // Denominaciones
    public class DTO_Denominaciones
    {
        public decimal Morralla { get; set; }
        public int D20 { get; set; }
        public int D50 { get; set; }
        public int D100 { get; set; }
        public int D200 { get; set; }
        public int D500 { get; set; }
        public int D1000 { get; set; }
        public decimal Total { get; set; }
    }

    // Paquete completo para el ticket
    public class DTO_ResumenVaucherasTicket
    {
        public string Turno { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        public List<DTO_ItemResumen> Vaucheras { get; set; } = new List<DTO_ItemResumen>();
        public decimal TotalVaucherasCantidad { get; set; }
        public decimal TotalVaucherasImporte { get; set; }

        public List<DTO_ItemResumen> Extras { get; set; } = new List<DTO_ItemResumen>();
        public decimal TotalExtras { get; set; }

        public DTO_Denominaciones Denoms { get; set; } = new DTO_Denominaciones();

        public decimal GranTotal { get; set; }
    }

}
