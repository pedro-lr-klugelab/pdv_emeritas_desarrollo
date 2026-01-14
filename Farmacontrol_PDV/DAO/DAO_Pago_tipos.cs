using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Farmacontrol_PDV.DTO;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Pago_tipos
	{
		Conector conector = new Conector();

        public DTO_Pago_tipos get_pago_tipo(string etiqueta)
        {
            DTO_Pago_tipos pago = new DTO_Pago_tipos();

            string sql = @"
                SELECT
                    *
                FROM
                    farmacontrol_global.pago_tipos
                WHERE
                    etiqueta = @etiqueta
                LIMIT 1
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("etiqueta",etiqueta);

            conector.Select(sql, parametros);

            if(conector.result_set.Rows.Count > 0)
            {
                var row = conector.result_set.Rows[0];
                pago.nombre = row["nombre"].ToString();
                pago.pago_tipo_id = Convert.ToInt64(row["pago_tipo_id"]);
                pago.entrega_cambio = Convert.ToBoolean(row["entrega_cambio"]);
                pago.es_credito = Convert.ToBoolean(row["es_credito"]);
                pago.es_prepago = Convert.ToBoolean(row["es_prepago"]);
                pago.usa_cuenta = Convert.ToBoolean(row["usa_cuenta"]);
                pago.etiqueta = row["etiqueta"].ToString();
            }

            return pago;
        }

		public List<DTO_Pago_tipos> get_pago_tipos(object obj, bool activo_mostrador = true, bool es_tae = false)
		{
			List<DTO_Pago_tipos> lista_pago_tipos = new List<DTO_Pago_tipos>();

            string sql;

            if (es_tae)
            {
                sql = @"
				    SELECT
					    pago_tipo_id,
					    nombre,
					    es_credito,
					    entrega_cambio,
					    usa_cuenta,
					    es_prepago
				    FROM
					    pago_tipos
				    WHERE
                        nombre = 'EFECTIVO' 
                    AND
					    activo_mostrador = @activo_mostrador
				    ORDER BY posicion
			    ";
            }
            else
            {
                sql = @"
				    SELECT
					    pago_tipo_id,
					    nombre,
					    es_credito,
					    entrega_cambio,
					    usa_cuenta,
					    es_prepago
				    FROM
					    pago_tipos
				    WHERE
					    activo_mostrador = @activo_mostrador
				    ORDER BY posicion
			    ";
            }

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("activo_mostrador",(activo_mostrador == true) ? 1 : 0 );

			conector.Select(sql, parametros);

			var result = conector.result_set;

			if(result.Rows.Count > 0)
			{
				foreach(DataRow row in result.Rows)
				{
					DTO_Pago_tipos pago_tipo = new DTO_Pago_tipos();
					pago_tipo.pago_tipo_id = Convert.ToInt64(row["pago_tipo_id"]);
					pago_tipo.nombre = row["nombre"].ToString();
                    pago_tipo.entrega_cambio = Convert.ToBoolean(row["entrega_cambio"]);
					pago_tipo.es_credito = Convert.ToBoolean(row["es_credito"]);
					pago_tipo.usa_cuenta = Convert.ToBoolean(row["usa_cuenta"]);
					pago_tipo.es_prepago = Convert.ToBoolean(row["es_prepago"]);

					lista_pago_tipos.Add(pago_tipo);
				}
			}

			return lista_pago_tipos;
		}

		public DataTable get_pago_tipos()
		{
			string sql = @"
				SELECT
					pago_tipo_id,
					nombre,
					usa_cuenta
				FROM
					pago_tipos
				WHERE
					activo_mostrador = 1
				ORDER BY posicion
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();

			conector.Select(sql,parametros);

			return conector.result_set;
		}


        public DataTable get_pago_venta(long venta_id)
        {

            DataTable result = new DataTable();

            string sql = @"
				SELECT	                venta_pago_id,	                venta_id,	                GROUP_CONCAT(nombre) as pagos	            FROM 	                farmacontrol_local.ventas_pagos	            INNER JOIN                     farmacontrol_global.pago_tipos	            USING(pago_tipo_id)	            WHERE 	                venta_id IN(				            SELECT				            venta_id			            FROM				            farmacontrol_local.ventas			            WHERE			              corte_parcial_id IS NULL			            AND 			              fecha_facturado IS NULL			            AND 			              fecha_terminado IS NOT NULL 		                AND			              venta_id = @venta_id	            )
               HAVING
                   venta_pago_id is not null
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("venta_id", venta_id);

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                result = conector.result_set;
            }       

            return result;
        }

        public DataTable get_pago_tipos_disponible(long venta_id)
        {
            string sql = @"
				 SELECT                      pago_tipo_id,	                  etiqueta,	                  nombre                  FROM                     farmacontrol_global.pago_tipos                  WHERE                     etiqueta NOT IN('RCAN','NIDE','CFAR','VFAR')                  AND                      activo_mostrador = 1                  AND 	   	                 pago_tipo_id NOT IN(	 		                SELECT		                   pago_tipo_id		                FROM 		                   farmacontrol_local.ventas_pagos		                WHERE		                  venta_id = @venta_id			                 )
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            conector.Select(sql, parametros);
            parametros.Add("venta_id", venta_id);
            return conector.result_set;
        }


        public bool set_nuevo_pago( long venta_pago_id, string pago_tipo_id, long venta_id )
        {
            bool res = false;

            string sql = @"
				  UPDATE
                      farmacontrol_local.ventas_pagos
                   SET
                       pago_tipo_id = ( 
                            SELECT
                                pago_tipo_id
                            FROM 
                               farmacontrol_global.pago_tipos
                            WHERE
                               nombre = @pago_tipo_id
                        )
                   WHERE
                       venta_pago_id = @venta_pago_id
                   AND 
                       venta_id = @venta_id
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("venta_id", venta_id);
            parametros.Add("venta_pago_id", venta_pago_id);
            parametros.Add("pago_tipo_id", pago_tipo_id);

            conector.Update(sql, parametros);

            if (conector.filas_afectadas > 0)
            {
                res = true;
            }

            return res;
        
        }



	}
}
