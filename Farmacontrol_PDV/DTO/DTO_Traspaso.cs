using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Traspaso_complementario
	{
		public long detallado_traspaso_id { set; get; }
		public string amecop { set; get; }
		public string producto { set; get; }
		public string caducidad { set; get; }
		public string lote { set; get; }
		public long cantidad_origen { set; get; }
		public long cantidad { set; get; }
		public string problema { set; get; }
		public string solucion { set; get; }
	}

	class DTO_Traspaso
	{
		public int			traspaso_id					{ get; set; }
		public int?			traspado_padre_id			{ get; set; }
		public int?			terminal_id					{ get; set; }
		public int			sucursal_id					{ get; set; }
		public int?			empleado_id					{ get; set; }
		public int?			termina_empleado_id			{ get; set; }
		public int?			cancela_empleado_id			{ get; set; }
		public int?			remote_id					{ get; set; }
		public int?			conciliacion_impresion_id	{ get; set; }
		public string		tipo						{ get; set; }
		public int			es_para_venta				{ get; set; }
		
		public DateTime?	fecha_creado				{ get; set; }
		public DateTime?	fecha_recibido				{ get; set; }
		public DateTime?	fecha_iniciado				{ get; set; }
		public DateTime?	fecha_terminado				{ get; set; }
		public DateTime?	fecha_etiquetado			{ get; set; }
		public DateTime?	fecha_terminado_destino		{ get; set; }
		public DateTime?	fecha_cancelado				{ get; set; }
		
		public int			numero_bultos				{ get; set; }
		public string		motivo_cancelacion			{ get; set; }
		public string		comentarios					{ get; set; }
		public string		hash						{ get; set; }

		public string		nombre_empleado_captura		{ get; set; }
		public string		nombre_empleado_termina		{ get; set; }
		public bool			result						{ get; set; }
		public string		informacion					{ get; set; }

		public List<DTO_Detallado_traspaso> detallado_traspaso { get; set; }
	}

	class DTO_Traspaso_ticket
	{
		public int traspaso_id { get; set; }
		public int? traspado_padre_id { get; set; }
		public int? terminal_id { get; set; }
		public int sucursal_id { get; set; }
		public int? empleado_id { get; set; }
		public int? termina_empleado_id { get; set; }
		public int? cancela_empleado_id { get; set; }
		public int? remote_id { get; set; }
		public int? conciliacion_impresion_id { get; set; }
		public string tipo { get; set; }
		public int es_para_venta { get; set; }

		public DateTime? fecha_creado { get; set; }
		public DateTime? fecha_recibido { get; set; }
		public DateTime? fecha_iniciado { get; set; }
		public DateTime? fecha_terminado { get; set; }
		public DateTime? fecha_etiquetado { get; set; }
		public DateTime? fecha_terminado_destino { get; set; }
		public DateTime? fecha_cancelado { get; set; }

		public int numero_bultos { get; set; }
		public string motivo_cancelacion { get; set; }
		public string comentarios { get; set; }
		public string hash { get; set; }

		public string nombre_empleado_captura { get; set; }
		public string nombre_empleado_termina { get; set; }
		public List<DTO_Detallado_traspaso_ticket> detallado_traspaso_ticket { set; get; }
	}

	class DTO_Detallado_traspaso_ticket
	{
		public int articulo_id { set; get; }
		public string amecop { set; get; }
		public string nombre { set; get; }
		public decimal precio_costo { set; get; }
		public decimal total { set; get; }
		public List<Tuple<string, string, int, int, int>> caducidades_lotes { set; get; }
	}

}