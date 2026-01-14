using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
    class DTO_Ventas_pagos
    {
        public long pago_tipo_id { set; get; }
        public bool es_credito { set; get; }
        public bool usa_cuenta { set; get; }
        public string nombre { set; get; }
        public string cuenta { set; get; }
    }

    class DTO_Ventas_facturadas
    {
        public long folio { set; get; }
        public decimal importe { set; get; }
        public string metodo_pago { set; get; }
    }

    class DTO_Venta_envio
    {
        public long venta_envio_id { set; get; }
        public long venta_envio_folio { set; get; }
        public long venta_id { set; get; }
        public long empleado_id { set; get; }
        public DateTime? fecha_envio { set; get; }
        public DateTime? fecha_retorno { set; get; }
    }

    class DTO_Formas_pago
    {
        public string nombre { set; get; }
        public string cuenta { set; get; }
        public decimal importe { set; get; }
        public decimal monto { set; get; }
        public bool entrega_cambio { set; get; }
    }

    class DTO_Envio_ventas_detallado
    {
        public string caja { set; get; }
        public string empleado { set; get; }
        public long folio { set; get; }
        public DateTime fecha_terminado { set; get; }
        public DateTime fecha_envio { set; get; }
        public long articulos { set; get; }
        public long piezas { set; get; }
        public decimal importe { set; get; }
        public decimal total { set; get; }
        public string cliente_domicilio_id { set; get; }
        public string nombre_cliente { set; get; }
    }

    class DTO_Reporte_venta
    {
        public long venta_id { set; get; }
        public long venta_folio { set; get; }
        public string fecha { set; get; }
        public string terminal { set; get; }
        public string empleado { set; get; }
        public long articulos { set; get; }
        public long piezas { set; get; }
        public decimal total { set; get; }
    }

    class DTO_Reporte_articulo_vendidos
    {
        public long articulo_id { set; get; }
        public string amecop_original { set; get; }
        public string nombre { set; get; }
        public long vendido { set; get; }
        public string fecha_date { set; get; }
    
    }

    class DTO_Detallado_ventas_existencia
    {
        public string amecop { set; get; }
        public string nombre { set; get; }
        public string caducidad { set; get; }
        public string lote { set; get; }
        public long cantidad { set; get; }
        public long existencia_vendible { set; get; }
    }

	class DTO_Totales
	{
		public long piezas { set; get; }
		public decimal subtotal { set; get; }
		public decimal gravado { set; get; }
		public decimal excento { set; get; }
		public decimal importe_iva { set; get; }
		public decimal importe_ieps { set; get; }
		public decimal total { set; get; }
	}

	class DTO_Ventas_facturadas_sucursal
	{
		public int sucursal_id { set; get; }
		public long venta_id { set; get; }
		public string es_factura { set; get; }
	}

	class DTO_Venta
	{
		public long venta_id { set; get; }
		public long terminal_id { set; get; }
		public long venta_folio { set; get; }
		public long empleado_id { set; get; }
		public long? cotizacion_id { set; get; }
		public long? traspaso_id { set; get; }
		public string cliente_credito_id { set; get; }
		public string cliente_domicilio_id { set; get; }
		public long? cupon_id { set; get; }
		public long? corte_parcial_id { set; get; }
		public long? corte_total_id { set; get; }
		public DateTime? fecha_creado { set; get; }
		public DateTime? fecha_terminado { set; get; }
		public DateTime? fecha_facturada { set; get; }

		public string comentarios { set; get; }
		
		public string nombre_empleado { set; get; }
		public string empleado_fcid { set; get; }
		public string nombre_cliente_credito { set; get; }
		public string servicio_domicilio { set; get; }
	}

	class DTO_Ventas_ticket
	{
		public long? venta_id { set; get; }
        public long? cotizacion_id { set; get; }
        public string empleado_atendio { set; get; }
		public long venta_folio { set; get; }
		public string nombre_empleado { set; get; }
		public string nombre_terminal { set; get; }
		public string credito { set; get; }
		public string domicilio { set; get; }
		public List<DTO_Detallado_ventas_ticket> detallado_ventas_ticket { set; get; }
		public decimal excento { set; get; }
		public decimal gravado { set; get; }
		public decimal subtotal { set; get; }
		public decimal ieps { set; get; }
		public decimal iva { set; get; }
		public int piezas { set; get; }
		public decimal total { set; get; }
		public DateTime fecha_creado { set; get; }
		public DateTime? fecha_terminado { set; get; }
        public string comentarios { set; get; }
        public string es_tae { set; get; }
        public long numero_transaccion { set; get; }
	}

	class DTO_Detallado_ventas_ticket
	{
		public int articulo_id { set; get; }
		public string amecop { set; get; }
		public string nombre { set; get; }
		public decimal precio_unitario { set; get; }
		public int cantidad { set; get; }
		public decimal importe { set; get; }
		public decimal subtotal { set; get; }
		public decimal descuento { set; get; }
		public decimal total { set; get; }
        public string nombre_fabricante {set;get;}
		public List<Tuple<string, string, int>> caducidades_lotes { set; get; }
	}
	
	public class DTO_Detallado_ventas_vista_previa
	{
		public long articulo_id { set; get; }
		public string amecop { set; get; }
		public string producto { set; get; }
		public string caducidad { set; get; }
		public string lote { set; get; }
		public long cantidad { set; get; }
	}
}
