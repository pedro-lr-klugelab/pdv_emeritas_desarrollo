using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.DTO;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Facturacion
	{
		Conector conector = new Conector();

        public long get_folio_nc()
        {
            string sql = @"
                SELECT
                    COALESCE(MAX(folio), 0) +1 AS folio
                FROM
                    farmacontrol_local.notas_credito
                LIMIT 1
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            conector.Select(sql, parametros);

            return Convert.ToInt64(conector.result_set.Rows[0]["folio"]);
        }

        public void registrar_nota_credito(long elemento_id, long terminal_id, string tipo_padre)
        {
            string sql = @"
                SELECT
                    COALESCE(MAX(folio), 0) +1 AS folio
                FROM
                    farmacontrol_local.notas_credito
                LIMIT 1
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            conector.Select(sql, parametros);

            long folio = Convert.ToInt64(conector.result_set.Rows[0]["folio"]);

            sql = @"
                INSERT INTO
                    farmacontrol_local.notas_credito
                SET
                    terminal_id = @terminal_id,
                    elemento_id = @elemento_id,
                    tipo_padre = @tipo_padre,
                    folio = @folio,
                    fecha = NOW()
            ";

            parametros = new Dictionary<string, object>();
            parametros.Add("terminal_id",terminal_id);
            parametros.Add("elemento_id",elemento_id);
            parametros.Add("tipo_padre",tipo_padre);
            parametros.Add("folio",folio);

            conector.Insert(sql, parametros);

        }

		public bool existe_factura(long venta_id)
		{
			string sql = @"
				SELECT
					fecha_facturado
				FROM
					farmacontrol_local.ventas
				WHERE
					venta_id = @venta_id
				AND
					fecha_facturado IS NOT NULL
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("venta_id",venta_id);

			conector.Select(sql,parametros);

			return (conector.result_set.Rows.Count > 0);
		}


        public string fecha_existe_factura(long venta_id)
        {
            string sql = @"
				SELECT
					fecha_facturado
				FROM
					farmacontrol_local.ventas
				WHERE
					venta_id = @venta_id
				AND
					fecha_facturado IS NOT NULL
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("venta_id", venta_id);

            conector.Select(sql, parametros);

            return conector.result_set.Rows[0]["fecha_facturado"].ToString();
        }


		public string get_uuid_factura(long venta_id)
		{
			string sql = @"
				SELECT
					uuid
				FROM
					farmacontrol_local.detallado_facturas
				JOIN farmacontrol_local.facturas USING(factura_id)
				WHERE
					detallado_facturas.venta_id = @venta_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("venta_id",venta_id);

			conector.Select(sql,parametros);
			
			return conector.result_set.Rows[0]["uuid"].ToString();
		}

		public long registrar_factura(long venta_id)
		{
			string sql = @"
				UPDATE 
					farmacontrol_local.ventas
				SET
					fecha_facturado = NOW()
				WHERE
					venta_id = @venta_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("venta_id",venta_id);

			conector.Insert(sql,parametros);

			return conector.insert_id;
		}
	}
}
