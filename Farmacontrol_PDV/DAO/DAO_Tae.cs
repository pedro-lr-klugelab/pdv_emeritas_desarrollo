using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.FORMS.comunes;

namespace Farmacontrol_PDV.DAO
{
    class DAO_Tae
    {
        Conector conector = new Conector();

        public List<DTO_Tae_proveedores> get_proveedores_tae()
        {
            List<DTO_Tae_proveedores> lista_proveedores_tae = new List<DTO_Tae_proveedores>();

            string sql = @"
				SELECT 
	                DISTINCT(t1.fabricante_id) AS fabricante_id,
                    t0.nombre AS nombre
                FROM 
	                farmacontrol_global.articulos AS t1
                JOIN 
	                farmacontrol_global.fabricantes AS t0 USING (fabricante_id)
                WHERE
	                t1.tipo_articulo = 'VIRTUAL'
                AND
	                t1.mayorista_id = 258
			";

            conector.Select(sql);

            foreach (DataRow row in conector.result_set.Rows)
            {

                DTO_Tae_proveedores tae_prov = new DTO_Tae_proveedores();
                tae_prov.fabricante_id = Convert.ToInt32(row["fabricante_id"]);
                tae_prov.nombre = row["nombre"].ToString();

                lista_proveedores_tae.Add(tae_prov);
            }

            return lista_proveedores_tae;
        }

        public List<DTO_servicios_proveedor> get_servicios_prov(long fabricante_id)
        {
            List<DTO_servicios_proveedor> lista_servicios = new List<DTO_servicios_proveedor>();

            string sql = @"
                SELECT
                    t0.articulo_id AS articulo_id,
                    t0.fabricante_id AS fabricante_id,
                    t0.nombre AS nombre,	
                    t1.sku AS sku
                FROM 
                    farmacontrol_global.articulos AS t0
                JOIN
                    farmacontrol_global.tae_diestel AS t1 USING(articulo_id)
                WHERE
                    t0.fabricante_id = @fabricante_id                    
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("fabricante_id", fabricante_id);

            conector.Select(sql, parametros);

            foreach (DataRow row in conector.result_set.Rows)
            {

                DTO_servicios_proveedor serv_prov = new DTO_servicios_proveedor();
                serv_prov.articulo_id = Convert.ToInt64(row["articulo_id"]);
                serv_prov.fabricante_id = Convert.ToInt64(row["fabricante_id"]);
                serv_prov.nombre = row["nombre"].ToString();
                serv_prov.sku = row["sku"].ToString();

                lista_servicios.Add(serv_prov);
            }

            return lista_servicios;
        }

        public DTO_tae_diestel get_info_tae_por_sku(string sku)
        {
            DTO_tae_diestel info_tae = new DTO_tae_diestel();

            string sql = @"
                SELECT
	                t1.articulo_id AS articulo_id,
	                t1.fabricante_id AS fabricante_id,
	                t1.sku AS sku,
                    t1.precio_publico,
                    t1.descuento
                FROM 
	                farmacontrol_global.tae_diestel t1
                WHERE
	                t1.sku = @sku
                LIMIT 1             
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("sku", sku);

            conector.Select(sql, parametros);

            foreach (DataRow row in conector.result_set.Rows)
            {
                info_tae.articulo_id = Convert.ToInt64(row["articulo_id"]);
                info_tae.fabricante_id = Convert.ToInt64(row["fabricante_id"]);
                info_tae.sku = row["sku"].ToString();
                info_tae.precio_publico = Convert.ToDecimal(row["precio_publico"]);
                info_tae.descuento = Convert.ToDecimal(row["descuento"]);
            }

            return info_tae;
        }

        public DTO_servicios_proveedor get_detalle_tae_por_sku(string sku)
        {
            DTO_servicios_proveedor serv_prov = new DTO_servicios_proveedor();

            string sql = @"
                SELECT
	                t0.articulo_id AS articulo_id,
	                t0.fabricante_id AS fabricante_id,
                    (SELECT t2.nombre FROM farmacontrol_global.fabricantes AS t2 WHERE t2.fabricante_id = t0.fabricante_id) as nombre_fabricante,
	                t0.nombre AS nombre,	
	                t1.sku AS sku,
                    t1.precio_publico
                FROM 
	                farmacontrol_global.articulos AS t0
                JOIN
	                farmacontrol_global.tae_diestel AS t1 USING(articulo_id)
                WHERE
	                t1.sku = @sku
                LIMIT 1             
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("sku", sku);

            conector.Select(sql, parametros);

            foreach (DataRow row in conector.result_set.Rows)
            {
                serv_prov.articulo_id       = Convert.ToInt64(row["articulo_id"]);
                serv_prov.fabricante_id     = Convert.ToInt64(row["fabricante_id"]);
                serv_prov.nombre_fabricante = row["nombre_fabricante"].ToString();
                serv_prov.nombre            = row["nombre"].ToString();
                serv_prov.sku               = row["sku"].ToString();
                serv_prov.precio_publico    = Convert.ToDecimal(row["precio_publico"]);
            }

            return serv_prov;
        }

        public bool inserta_control_transaccion_tae(long sucursal_id, long terminal_id)
        {
            long numero_transaccion = 0;
            bool ok = false;

            numero_transaccion = get_numero_transaccion(sucursal_id, terminal_id);
            string sql = "";
            if (numero_transaccion == 0)
            {
                numero_transaccion++;

                sql = @"
                    INSERT INTO farmacontrol_local.control_transaccion_tae
                    VALUES
                    (
                        @sucursal_id,
                        @terminal_id,
                        @numero_transaccion
                    )
                ";

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("sucursal_id", sucursal_id);
                parametros.Add("terminal_id", terminal_id);
                parametros.Add("numero_transaccion", numero_transaccion);

                conector.Insert(sql, parametros);

                ok = (conector.filas_afectadas > 0);
            }
            else
            {
                numero_transaccion++;

                sql = @"
                    UPDATE farmacontrol_local.control_transaccion_tae
                    SET
                        numero_transaccion = @numero_transaccion
                    WHERE
                        sucursal_id = @sucursal_id
                    AND
                        terminal_id =  @terminal_id
                ";

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("sucursal_id", sucursal_id);
                parametros.Add("terminal_id", terminal_id);
                parametros.Add("numero_transaccion", numero_transaccion);

                conector.Update(sql, parametros);
                ok = (conector.filas_afectadas > 0);
            }

            return ok;
        }

        public int get_numero_transaccion (long sucursal_id, long terminal_id)
        {
            int numero_transaccion= 0;

            string sql = @"
                SELECT 
                    COALESCE(MAX(numero_transaccion), 0) AS numero_transaccion
                FROM
                    farmacontrol_local.control_transaccion_tae
                WHERE
                    sucursal_id = @sucursal_id
                AND
                    terminal_id = @terminal_id
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("sucursal_id", sucursal_id);
            parametros.Add("terminal_id", terminal_id);

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                numero_transaccion = Convert.ToInt32(conector.result_set.Rows[0]["numero_transaccion"]);
            }

            return numero_transaccion;
        }

        public bool inserta_log_tae_diestel(string sku, long sucursal_id, long terminal_id, string referencia, long numero_autorizacion, long? venta_id, long numero_transaccion, string tipo, bool exito, string respuesta)
        {
            string sql = @"
                INSERT INTO farmacontrol_local.log_tae_diestel (
                    fecha,
                    sku,                
                    sucursal_id,        
                    terminal_id,        
                    referencia,        
                    numero_autorizacion,
                    venta_id,           
                    numero_transaccion,
                    tipo_ws,
                    exito, 
                    respuesta
                )                        
                VALUES (
                    NOW(),
                    @sku,
                    @sucursal_id,
                    @terminal_id,
                    @referencia,
                    @numero_autorizacion,
                    @venta_id,
                    @numero_transaccion,
                    @tipo,
                    @exito,
                    @respuesta
                )
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("sku", sku);
            parametros.Add("sucursal_id", sucursal_id);
            parametros.Add("terminal_id", terminal_id);
            parametros.Add("referencia", referencia);
            parametros.Add("numero_autorizacion", numero_autorizacion);
            parametros.Add("venta_id", venta_id);
            parametros.Add("numero_transaccion", numero_transaccion);
            parametros.Add("tipo", tipo);
            parametros.Add("exito", exito);
            parametros.Add("respuesta", respuesta);

            conector.Insert(sql, parametros);

            return (conector.filas_afectadas > 0);
        }
    }
}
