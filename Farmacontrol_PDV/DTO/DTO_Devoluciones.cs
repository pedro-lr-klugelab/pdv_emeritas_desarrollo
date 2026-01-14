using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Devoluciones
	{
		public long		devolucion_id					{ set; get; }
		public int?		terminal_id						{ set; get; }
		public long		mayorista_id					{ set; get; }
		public long?	empleado_id						{ set; get; }
		public long?	 termina_empleado_id			{ set; get; }
		public long		entrada_id						{ set; get; }
		public string	solicitud_devolucion_folio		{ set; get; }

		public DateTime?	fecha_creado					{ set; get; }
		public DateTime?	fecha_terminado					{ set; get; }
		public DateTime?	fecha_afectado					{ set; get; }
		public DateTime?	fecha_auditada					{ set; get; }

		public string	nota_credito_folio				{ set; get; }
		public DateTime?	solicitud_devolucion_fecha		{ set; get; }
		public DateTime?	solicitud_devolucion_fecha_formato		{ set; get; }
		public string	nota_credito_fecha				{ set; get; }
		public decimal	nota_credito_importe			{ set; get; }
		public string	comentarios						{ set; get; }
		public string	nombre_empleado_captura			{ set; get; }
		public string	nombre_empleado_termina			{ set; get; }
	}

	class DTO_Detallado_devoluciones
	{
		public long		detallado_devolucion_id	{ set; get; }
		public long		devolucion_id			{ set; get; }
		public int		articulo_id				{ set; get; }
		public string	caducidad				{ set; get; }
		public string	lote					{ set; get; }
		public decimal	precio_costo			{ set; get; }
		public decimal	pct_descuento			{ set; get; }
		public decimal	importe_descuento		{ set; get; }
		public decimal	importe					{ set; get; }
		public int		cantidad				{ set; get; }
		public decimal	subtotal				{ set; get; }
		public decimal	pct_iva					{ set; get; }
		public decimal	importe_iva				{ set; get; }
		public decimal	total					{ set; get; }
		public string	motivo					{ set; get; }
	}


}
