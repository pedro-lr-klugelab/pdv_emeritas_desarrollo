using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	public class Result_boolean
	{
		public bool status { get; set; }
		public string debug_information { get; set; }
		public string error_information { get; set; }
		public string custom_message { get; set; }
	}

	public class Result_nonquery
	{
		public bool status { get; set; }
		public int affected_rows { get; set; }
		public long insert_id { get; set; }
		public string error_information { get; set; }
		public string custom_message { get; set; }
	}

	public class DTO_Validacion
	{
		public bool status { get; set; }
		public string informacion { get; set; }
		public int elemento_id { get; set; }
		public string elemento_nombre { get; set; }
	}

    public class DTO_WebServiceEnlaceVital
    {
        public bool status { get; set; }
        public string transaccion { get; set; }
        public string transactionitems { get; set; }
        public string mensaje { get; set; }
        public string autorizacion { get; set; }
        public string mensajeticket { get; set; }
    }

    public class DTO_WebServiceSoyFan
    {
        public bool status { get; set; }
        public string idcliente { get; set; }
        public string mensaje { get; set; }
        public string cardAuthNum { get; set; }
        public string giftAuthNum { get; set; }
        public string promocion { get; set; }
        public string guardar_gif { get; set; }
        public string guardar_list { get; set; }
        public string codigos_participantes { get; set; }
        public string catalogo { get; set; }
    }


    public class DTO_WebServiceCirculoOro
    {
        public bool huboError { get; set; }
        public string sesion { get; set; }
        public string mensajeError { get; set; }
        public string nombre_completo { get; set; }
        public string fecha_activacion { get; set; }
        public string fecha_vigencia { get; set; }
        public string NoAutorizacion { get; set; }
        public List<detallado_venta_circulo_oro> datos { get; set; }
        public List<catalogo_promociones_oro> catalogo { get; set; }
        public List<catalogo_compras_oro> compras { get; set; }
    }

    public class detallado_venta_circulo_oro
    {
        public string amecop { get; set; }
        public string nombre { get; set; }
        public string precio { get; set; }
        public string piezas { get; set; }
        public string beneficio { get; set; }
    
    }

    public class catalogo_promociones_oro
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Regla { get; set; }
        public string FechaFin { get; set; }
        public string Politicas { get; set; }
    
    }

    public class catalogo_compras_oro
    {
        public string Fecha { get; set; }
        public string Farmacia { get; set; }
        public string Codigo { get; set; }
        public string Presentacion { get; set; }
        public string PiezasCompradas { get; set; }
        public string ObsequiosEntregados { get; set; }
        public string ObsequiosPendientes { get; set; }
        public string DescuentosMonto { get; set; }
        public string Descuentosporcentaje { get; set; }
        public string Programa { get; set; }
    }


    public class Catalogo_fanasa
    {
        public string sku { get; set; }
        public string descripcion { get; set; }
        public string detalle { get; set; }
    }

    public class Clientes_domicilio
    {
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string cliente_id { get; set; }
        public string tipo { get; set; }
        public string direccion { get; set; }
    
    
    }

    public class ListtoDataTableConverter
    {
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}
