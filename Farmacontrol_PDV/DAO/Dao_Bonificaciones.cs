using CrystalDecisions.CrystalReports.Engine;
using Farmacontrol_PDV.FORMS.movimientos.Circulo_Salud;
using Farmacontrol_PDV.HELPERS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farmacontrol_PDV.DAO
{
    class Dao_Bonificaciones
    {
        Conector conector = new Conector();

        public long set_bonificacion(long venta_id = 0 ,string tarjeta = "", string transaccion = "",long empleado_id = 0)///falta los productos
        {
            string sql = @"
                INSERT INTO
                    farmacontrol_local.bonificaciones
                SET
                    fecha = NOW(),
                    tarjeta = @tarjeta,
                    transaccion = @transaccion,  
                    folio_venta = ( 
                                        SELECT
                                              venta_folio
                                        FROM
                                              farmacontrol_local.ventas 
                                        WHERE
                                              venta_id = @venta_id
                                    ),
                    empleado_id  = @empleado_id,
                    modified = NOW();
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("tarjeta", tarjeta);
            parametros.Add("venta_id", venta_id);
            parametros.Add("transaccion", transaccion);
            parametros.Add("empleado_id", empleado_id);

            conector.Insert(sql, parametros);

            long insert_id = 0;

            if (conector.insert_id > 0)
            {
                insert_id = Convert.ToInt64(conector.insert_id);

            }

            return insert_id;
        }

        public long set_ajuste(long empleado_id, long id_bonificacion, long venta_id,string comentarios = "")
        {

            if (comentarios == "")
            {
               comentarios = "BONIFICACION #" + id_bonificacion + " DE LA NOTA #" + @venta_id;
            }

            string sql = @"
                INSERT INTO
                    farmacontrol_local.ajustes_existencias
                SET
                    terminal_id = 1,
				    empleado_id = @empleado_id,
					termina_empleado_id = @empleado_id,
					fecha_creado = NOW(),
				    fecha_terminado = NOW(),
					comentarios = @comentarios
            ";

            

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("empleado_id", empleado_id);
            parametros.Add("comentarios", comentarios);
          

            conector.Insert(sql, parametros);

            long insert_id = 0;

            if (conector.insert_id > 0)
            {
                insert_id = Convert.ToInt64(conector.insert_id);

            }

            return insert_id;

        }
        public bool is_existe_producto( string amecop, long venta_id  )
        {
            bool valido = false;

            string sql = @"
				SELECT
				   farmacontrol_global.articulos.articulo_id
				FROM
					farmacontrol_local.detallado_ventas
                INNER JOIN 
                    farmacontrol_global.articulos
                USING(articulo_id)
                WHERE
                    venta_id = @venta_id	
                AND 
                    amecop_original = @amecop
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("venta_id", venta_id);
            parametros.Add("amecop", amecop);

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                valido = true;
            }

         
            return valido;
        
        }


        public long max_existe_producto(string amecop, long venta_id)
        {

            string sql = @"
				SELECT
				   farmacontrol_global.articulos.articulo_id,
                   SUM(cantidad) AS cantidad_vendida
				FROM
					farmacontrol_local.detallado_ventas
                INNER JOIN 
                    farmacontrol_global.articulos
                USING(articulo_id)
                WHERE
                    venta_id = @venta_id	
                AND 
                    amecop_original = @amecop
                GROUP BY
                    articulo_id
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("venta_id", venta_id);
            parametros.Add("amecop", amecop);

            conector.Select(sql, parametros);

            var resul_set = conector.result_set;
            long cantidad_max_vendida = 0;
            foreach (DataRow row in resul_set.Rows)
            {
                cantidad_max_vendida = Convert.ToInt64(row["cantidad_vendida"].ToString());
            }

            return cantidad_max_vendida;

        }

        public bool bonificacion_insertada( long bonifiacion_id,string codigo,long cantidad, long ajuste_id )
        {
            bool valido = false;

            string sql = @"
                    SELECT
                        articulo_id,
                        existencia,
                        CONVERT(caducidad USING utf8) AS caducidad,
                        lote
                    FROM
                        farmacontrol_local.existencias
                    LEFT JOIN
                        farmacontrol_global.articulos
                    USING(articulo_id)
                    WHERE
                        amecop_original = @codigo
                    ORDER BY
                            caducidad DESC
                    LIMIT 1

            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("codigo", codigo);

            conector.Select(sql, parametros);
            long existencia = 0;
            long articulo_id = 0;
            long existencia_posterior = 0;
            var result_set = conector.result_set;

            if (result_set.Rows.Count > 0)
            {
                var row = result_set.Rows[0];
               // existencia_id = Convert.ToInt64(row["existencia_id"]);
                articulo_id = Convert.ToInt64(row["articulo_id"]);
                existencia = Convert.ToInt64(row["existencia"]);
                string caducidad = row["caducidad"].ToString();
                string lote = row["lote"].ToString();

                existencia_posterior = existencia - cantidad;

                if (result_set.Rows.Count > 0)
                {

                        sql = @"
                            INSERT INTO
                                farmacontrol_local.detallado_ajustes_existencias
                            SET
                                 ajuste_existencia_id = @ajuste_id,
								 articulo_id          = @articulo_id,
								 lote				  = @lote,
								 caducidad            = @caducidad,
							     existencia_anterior  = @ex_art,
								 cantidad             = @ex_art - @cantidad,
								 diferencia			  = (@ex_art - @cantidad) - existencia_anterior
                         ";

                        parametros.Add("ajuste_id", ajuste_id);
                        parametros.Add("articulo_id", articulo_id);
                        parametros.Add("lote", lote);
                        parametros.Add("caducidad", caducidad);
                        parametros.Add("ex_art", existencia);
                        parametros.Add("cantidad",cantidad );
                      

                        conector.Insert(sql, parametros);


                        ///INSERTANDO EN LA TABLA DE KARDEX
                        ///
                        sql = @"
                            INSERT INTO
                                farmacontrol_local.kardex
                            SET
                                terminal_id = @terminal_id,
							    fecha_datetime = NOW(),
								fecha_date = CURDATE(),
							    articulo_id = @articulo_id,
								caducidad   = @caducidad,
								lote        = @lote,
								tipo        = 'AJUSTE_EXISTENCIA',
								elemento_id = @ajuste_id,
								folio       = @ajuste_id,
								existencia_anterior = @ex_art,
								cantidad   = -@cantidad,
								existencia_posterior = 	 @ex_art - @cantidad,
								es_importado = 0
                         ";


                        parametros.Add("terminal_id", Misc_helper.get_terminal_id());
                     
                        conector.Insert(sql, parametros);

                        //ACTUALIZANDO LA TABLA DE EXISTENCIAS

                        sql = @"
				            UPDATE
								farmacontrol_local.existencias
							SET 
								existencia = existencia - @cantidad
							WHERE
								articulo_id = @articulo_id
							AND 
								lote = @lote
							AND 
								caducidad = @caducidad
							LIMIT 1
			            ";
                        conector.Update(sql, parametros);
                
                    }

                //}

                 
            }

            valido = true;


            return valido;        
        }

    }
}
