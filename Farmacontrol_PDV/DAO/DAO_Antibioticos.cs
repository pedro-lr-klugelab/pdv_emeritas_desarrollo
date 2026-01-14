using Farmacontrol_PDV.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.DAO
{
    class DAO_Antibioticos
    {
        Conector conector = new Conector();

        // Genéricos

        public DTO_info_generica get_info_generica(string movimiento, long elemento_id)
        {
            movimiento = movimiento.Replace("_", " ");
            DTO_info_generica dto = new DTO_info_generica();

            //string sql = @"farmacontrol_global.Control_AB_get_info_generica";
            string sql = "";
            switch (movimiento)
            {
                case "VENTA":
                    sql = @"
                        SELECT
			                fecha_terminado AS  fecha,
			                venta_folio AS folio,
			                '' as comentarios
		                FROM
			                farmacontrol_local.ventas 
		                WHERE
			                venta_id = @elemento_id";
                break;
                case "ENTRADA":
                    sql = @"
                        SELECT
			                fecha_terminado AS  fecha,
			                entrada_id AS folio,
			                '' AS comentarios
		                FROM
			                farmacontrol_local.entradas
		                WHERE
			                entrada_id = @elemento_id
                        ";
                break;
                case "DEVOLUCION CLIENTE":
                    sql = @"
                        SELECT
			                fecha AS  fecha,
			                cancelacion_id AS folio,
			                '' AS comentarios
		                FROM
			                farmacontrol_local.cancelaciones
		                WHERE
			                cancelacion_id = @elemento_id                    
                    ";
                 break;
                 case "DEVOLUCION MAYORISTA":
                        sql = @"

                        SELECT
			                fecha_terminado AS  fecha,
			                devolucion_id AS folio,
			                '' AS comentarios
                        FROM
			                 farmacontrol_local.devoluciones
		                WHERE
			                devolucion_id = @elemento_id               
                            ";
                 break;
                 case "TRASPASO ENTRANTE":

                        sql = @"
                            SELECT
			                    fecha_terminado AS  fecha,
			                    traspaso_id AS folio,
			                    '' AS comentarios
		                    FROM
			                    farmacontrol_local.traspasos
		                    WHERE
			                    traspaso_id = @elemento_id               
		                    AND
			                    tipo = 'RECIBIR';

                        ";
                 break;
                 case "TRASPASO SALIENTE":
                    sql = @"
                        SELECT
	                        fecha_terminado AS  fecha,
	                        traspaso_id AS folio,
	                        '' AS comentarios
                        FROM
	                        farmacontrol_local.traspasos
                        WHERE
	                        traspaso_id = @elemento_id
                        AND
	                        tipo = 'ENVIAR';
                        ";
                 break;
                 case "MERMA":

                    sql = @"
                        SELECT
	                        fecha_terminado AS  fecha,
	                        merma_id AS folio,
	                        ''  AS comentarios
                        FROM
	                        farmacontrol_local.mermas
                        WHERE
	                        merma_id = @elemento_id
                    ";

                 break;



            }
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            //parametros.Add("pmovimiento", movimiento);
            parametros.Add("@elemento_id", elemento_id);

           //conector.Call(sql, parametros);
            conector.Select(sql, parametros);
            if (conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {
                    dto.fecha = Misc_helper.fecha(row["fecha"].ToString(), "LEGIBLE");
                    dto.folio = Convert.ToInt32(row["folio"]);
                    dto.comentarios = row["comentarios"].ToString();
                }
            }

            return dto;
        }

        public List<DTO_detallado_generico> get_detallado_generico(string tipo, long elemento_id)
        {
            tipo = tipo.Replace("_", " ");
            List<DTO_detallado_generico> lista_detallado_generica = new List<DTO_detallado_generico>();
            //string sql = @"farmacontrol_global.Control_AB_get_detallado_generica";
            string sql = "";

            switch (tipo)
            {
                case "VENTA":
                    sql = @"
                        SELECT
			                'VENTA' AS movimiento,
                            @elemento_id AS elemento_id,
			                (
				                SELECT
					                amecop
				                FROM
					                farmacontrol_global.articulos_amecops
				                WHERE
					                articulos_amecops.articulo_id = detallado_ventas.articulo_id
				                ORDER BY articulos_amecops.amecop_principal DESC
				                LIMIT 1
			                ) AS amecop,
			                articulos.nombre AS producto,
                            articulos.articulo_id AS articulo_id,
                            ventas.fecha_terminado AS fecha,
			                date_format(detallado_ventas.caducidad, '%d/%m/%Y') AS caducidad,
			                detallado_ventas.lote As lote,
			                detallado_ventas.cantidad AS cantidad,
			                IF(articulos.clase_antibiotico_id IS NULL,0,1) AS es_antibiotico,
			                0 AS contiene_controlados
		                FROM
			                farmacontrol_local.detallado_ventas
		                JOIN farmacontrol_local.ventas USING(venta_id)
		                JOIN farmacontrol_global.articulos USING(articulo_id)
		                WHERE
			                venta_id = @elemento_id
		                GROUP BY
			                detallado_ventas.articulo_id,detallado_ventas.caducidad, detallado_ventas.lote
                        HAVING   
                           producto NOT LIKE '%(III)%' AND producto NOT LIKE '%(II)%' AND producto  NOT LIKE '%(I)%'
                        ";
                break;
                case "ENTRADA":
                    sql = @"
                      SELECT
		                'ENTRADA' AS movimiento,
                         @elemento_id AS elemento_id,
		                (
			                SELECT
				                amecop
			                FROM
				                farmacontrol_global.articulos_amecops
			                WHERE
				                articulos_amecops.articulo_id = detallado_entradas.articulo_id
			                ORDER BY articulos_amecops.amecop_principal DESC
			                LIMIT 1
		                ) AS amecop,
		                articulos.nombre AS producto,
                        articulos.articulo_id AS articulo_id,
                        entradas.fecha_terminado AS fecha,
		                date_format(detallado_entradas.caducidad, '%d/%m/%Y') AS caducidad,
		                detallado_entradas.lote AS lote,
		                detallado_entradas.cantidad AS cantidad,
		                IF(articulos.clase_antibiotico_id IS NULL,0,1) AS es_antibiotico,
		                COALESCE((
			                SELECT 
				                COUNT(DISTINCT(articulos.articulo_id)) 
			                FROM 
				                farmacontrol_local.detallado_entradas 
			                JOIN farmacontrol_global.articulos USING(articulo_id) 
			                WHERE 
				                detallado_entradas.entrada_id = @elemento_id
			                AND 
			                (
				                articulos.nombre LIKE '%(II)%' 
			                OR 
				                articulos.nombre LIKE '%(III)%'
			                )
		                ), 0) AS contiene_controlados
	                FROM
		                farmacontrol_local.detallado_entradas
	                JOIN farmacontrol_local.entradas USING(entrada_id)
	                JOIN farmacontrol_global.articulos USING(articulo_id)
	                WHERE
		                entradas.entrada_id = @elemento_id
	                AND
                        articulos.nombre NOT LIKE '%(I)%'
                    AND
		                articulos.nombre NOT LIKE '%(II)%'
	                AND
		                articulos.nombre NOT LIKE '%(III)%'
	                GROUP BY
		                detallado_entradas.articulo_id,detallado_entradas.caducidad,detallado_entradas.lote;
                      
                     ";


                break;
                case "DEVOLUCION CLIENTE":

                  sql = @"
                        SELECT
			                'DEVOLUCION CLIENTE' AS movimiento,
                            @elemento_id AS elemento_id,
			                (
				                SELECT
					                amecop
				                FROM
					                farmacontrol_global.articulos_amecops
				                WHERE
					                articulos_amecops.articulo_id = detallado_cancelaciones.articulo_id
				                ORDER BY articulos_amecops.amecop_principal DESC
				                LIMIT 1
			                ) AS amecop,
			                articulos.nombre AS producto,
                            articulos.articulo_id AS articulo_id,
                            cancelaciones.fecha AS fecha,
			                date_format(detallado_cancelaciones.caducidad, '%d/%m/%Y') AS caducidad,
			                detallado_cancelaciones.lote AS lote,
			                detallado_cancelaciones.cantidad AS cantidad,
			                IF(articulos.clase_antibiotico_id IS NULL,0,1) AS es_antibiotico,
			                0 AS contiene_controlados
		                FROM
			                farmacontrol_local.detallado_cancelaciones
		                JOIN farmacontrol_local.cancelaciones USING(cancelacion_id)
		                JOIN farmacontrol_global.articulos USING(articulo_id)
		                WHERE
			                cancelacion_id = @elemento_id
		                GROUP BY
			                detallado_cancelaciones.articulo_id,detallado_cancelaciones.caducidad, detallado_cancelaciones.lote;
                        ";

                   break;
                   case "DEVOLUCION MAYORISTA":
                        sql = @"
                             SELECT
			                    'DEVOLUCION MAYORISTA' AS movimiento,
                                @elemento_id AS elemento_id,
			                    (
				                    SELECT
					                    amecop
				                    FROM
					                    farmacontrol_global.articulos_amecops
				                    WHERE
					                    articulos_amecops.articulo_id = detallado_devoluciones.articulo_id
				                    ORDER BY articulos_amecops.amecop_principal DESC
				                    LIMIT 1
			                    ) AS amecop,
			                    articulos.nombre AS producto,
                                articulos.articulo_id AS articulo_id,
                                devoluciones.fecha_terminado AS fecha,
			                    date_format(detallado_devoluciones.caducidad, '%d/%m/%Y') AS caducidad,
			                    detallado_devoluciones.lote AS lote,
			                    detallado_devoluciones.cantidad AS cantidad,
			                    IF(articulos.clase_antibiotico_id IS NULL,0,1) AS es_antibiotico,
			                    0 AS contiene_controlados
		                    FROM
			                    farmacontrol_local.detallado_devoluciones
		                    JOIN farmacontrol_local.devoluciones USING(devolucion_id)
		                    JOIN farmacontrol_global.articulos USING(articulo_id)
		                    WHERE
			                    devolucion_id = @elemento_id
		                    GROUP BY
			                    detallado_devoluciones.articulo_id,detallado_devoluciones.caducidad, detallado_devoluciones.lote;
                        ";
                   break;
                   case "TRASPASO ENTRANTE":
                        sql = @"
                              SELECT
			                        'TRASPASO ENTRANTE' AS movimiento,
                                    @elemento_id AS elemento_id,
			                        (
				                        SELECT
					                        amecop
				                        FROM
					                        farmacontrol_global.articulos_amecops
				                        WHERE
					                        articulos_amecops.articulo_id = detallado_traspasos.articulo_id
				                        ORDER BY articulos_amecops.amecop_principal DESC
				                        LIMIT 1
			                        ) AS amecop,
			                        articulos.nombre AS producto,
                                    articulos.articulo_id AS articulo_id,
                                    traspasos.fecha_terminado AS fecha,
			                        date_format(detallado_traspasos.caducidad, '%d/%m/%Y') AS caducidad,
			                        detallado_traspasos.lote AS lote,
			                        detallado_traspasos.cantidad AS cantidad,
			                        IF(articulos.clase_antibiotico_id IS NULL,0,1) AS es_antibiotico,
			                        COALESCE((
				                        SELECT 
					                        COUNT(DISTINCT(articulos.articulo_id)) 
				                        FROM 
					                        farmacontrol_local.detallado_traspasos 
				                        JOIN farmacontrol_global.articulos USING(articulo_id) 
				                        WHERE 
					                        detallado_traspasos.traspaso_id = @elemento_id
				                        AND 
				                        (
					                        articulos.nombre LIKE '%(II)%' 
				                        OR 
					                        articulos.nombre LIKE '%(III)%'
				                        )
			                        ), 0) AS contiene_controlados
		                    FROM
			                    farmacontrol_local.detallado_traspasos
		                    JOIN farmacontrol_local.traspasos USING(traspaso_id)
		                    JOIN farmacontrol_global.articulos USING(articulo_id)
		                    WHERE
			                    traspasos.traspaso_id = @elemento_id
		                    AND
			                    articulos.nombre NOT LIKE '%(II)%'
		                    AND
			                    articulos.nombre NOT LIKE '%(III)%'
		                    AND
			                    traspasos.tipo = 'RECIBIR'
		                    GROUP BY
			                    detallado_traspasos.articulo_id,detallado_traspasos.caducidad,detallado_traspasos.lote
                              ";
                    break;
                   case "TRASPASO SALIENTE":
                    sql = @"

                     SELECT
			            'TRASPASO SALIENTE' AS movimiento,
                        @elemento_id AS elemento_id,
			            (
				            SELECT
					            amecop
				            FROM
					            farmacontrol_global.articulos_amecops
				            WHERE
					            articulos_amecops.articulo_id = detallado_traspasos.articulo_id
				            ORDER BY articulos_amecops.amecop_principal DESC
				            LIMIT 1
			            ) AS amecop,
			            articulos.nombre AS producto,
                        articulos.articulo_id AS articulo_id,
                        traspasos.fecha_terminado AS fecha,
			            date_format(detallado_traspasos.caducidad, '%d/%m/%Y') AS caducidad,
			            detallado_traspasos.lote AS lote,
			            detallado_traspasos.cantidad AS cantidad,
			            IF(articulos.clase_antibiotico_id IS NULL,0,1) AS es_antibiotico,
			            COALESCE((
				            SELECT 
					            COUNT(DISTINCT(articulos.articulo_id)) 
				            FROM 
					            farmacontrol_local.detallado_traspasos 
				            JOIN farmacontrol_global.articulos USING(articulo_id) 
				            WHERE 
					            detallado_traspasos.traspaso_id = @elemento_id
				            AND 
				            (
					            articulos.nombre LIKE '%(II)%' 
				            OR 
					            articulos.nombre LIKE '%(III)%'
				            )
			            ), 0) AS contiene_controlados
		            FROM
			            farmacontrol_local.detallado_traspasos
		            JOIN farmacontrol_local.traspasos USING(traspaso_id)
		            JOIN farmacontrol_global.articulos USING(articulo_id)
		            WHERE
			            traspasos.traspaso_id = @elemento_id
		            AND
			            articulos.nombre NOT LIKE '%(II)%'
		            AND
			            articulos.nombre NOT LIKE '%(III)%'
		            AND
			            traspasos.tipo = 'ENVIAR'
		            GROUP BY
			            detallado_traspasos.articulo_id,detallado_traspasos.caducidad,detallado_traspasos.lote
                    ";
                   break;
                   case "MERMA":
                       sql = @"

                            SELECT
	                            'MERMA' AS movimiento,
                                 @elemento_id AS elemento_id,
	                            (
		                            SELECT
			                            amecop
		                            FROM
			                            farmacontrol_global.articulos_amecops
		                            WHERE
			                            articulos_amecops.articulo_id = detallado_mermas.articulo_id
		                            ORDER BY articulos_amecops.amecop_principal DESC
		                            LIMIT 1
	                            ) AS amecop,
	                            articulos.nombre AS producto,
                                articulos.articulo_id AS articulo_id,
                                mermas.fecha_terminado AS fecha,
	                            date_format(detallado_mermas.caducidad, '%d/%m/%Y') AS caducidad,
	                            detallado_mermas.lote AS lote,
	                            detallado_mermas.cantidad AS cantidad,
	                            IF(articulos.clase_antibiotico_id IS NULL,0,1) AS es_antibiotico,
	                            0 AS contiene_controlados
                            FROM
	                            farmacontrol_local.detallado_mermas
                            JOIN farmacontrol_local.mermas USING(merma_id)
                            JOIN farmacontrol_global.articulos USING(articulo_id)
                            WHERE
	                            merma_id = @elemento_id
                            GROUP BY
	                            detallado_mermas.articulo_id,detallado_mermas.caducidad, detallado_mermas.lote;
                            ";
                   break;
            
            }

            Dictionary<string, object> parametros = new Dictionary<string, object>();
           // parametros.Add("par_tipo", tipo);
            parametros.Add("elemento_id", elemento_id);

            //conector.Call(sql, parametros);
            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {
                    DTO_detallado_generico dto = new DTO_detallado_generico();

                    dto.movimiento = row["movimiento"].ToString();
                    dto.elemento_id = Convert.ToInt64(row["elemento_id"]);
                    dto.amecop = row["amecop"].ToString();
                    dto.producto = row["producto"].ToString();
                    dto.articulo_id = Convert.ToInt64(row["articulo_id"]);
                    dto.caducidad = Misc_helper.fecha(row["caducidad"].ToString(), "LEGIBLE");
                    dto.lote = row["lote"].ToString();
                    dto.cantidad = Convert.ToInt64(row["cantidad"]);
                    dto.es_antibiotico = Convert.ToInt32(row["es_antibiotico"]);
                    dto.contiene_controlados = Convert.ToInt32(row["contiene_controlados"]);
                    lista_detallado_generica.Add(dto);
                }
            }

            return lista_detallado_generica;
        }

        public bool registra_movimiento(string movimiento, long elemento_id)
        {
            //string sql = @"farmacontrol_global.Control_AB_registra_movimientos";
            string sql = "";
            switch (movimiento)
            {
                case "VENTA":
                    sql = @"
                        INSERT INTO farmacontrol_local.control_antibioticos
		                (
			                SELECT 
				                0 AS control_antibiotico_id,
				                'VENTA' AS movimiento,
			                    articulo_id,
			                    caducidad,
			                    lote,
			                    (cantidad * -1) AS cantidad,
                                NULL AS control_antibioticos_receta_id,
			                    @elemento_id AS elemento_id,
                                NOW() AS modified
			                FROM
				                farmacontrol_local.detallado_ventas
				                JOIN farmacontrol_global.articulos USING (articulo_id)
			                WHERE
				                detallado_ventas.venta_id = @elemento_id
			                AND
				                articulos.clase_antibiotico_id IS NOT NULL
			                GROUP BY detallado_ventas.articulo_id, detallado_ventas.caducidad, detallado_ventas.lote
		                ) ON DUPLICATE KEY UPDATE
		                control_antibiotico_id = control_antibiotico_id";
                 break;
                 case "ENTRADA":

                    sql = @"
                        INSERT INTO farmacontrol_local.control_antibioticos
		                (
			                SELECT 
				                0 AS control_antibiotico_id,
				                'ENTRADA' AS movimiento,
			                    articulo_id,
			                    caducidad,
			                    lote,
			                    cantidad,
                                NULL AS control_antibioticos_receta_id,
			                    @elemento_id AS elemento_id,
                                NOW() AS modified
			                FROM
				                farmacontrol_local.detallado_entradas
				                JOIN farmacontrol_global.articulos USING (articulo_id)
			                WHERE
				                detallado_entradas.entrada_id = @elemento_id
			                AND
				                articulos.clase_antibiotico_id IS NOT NULL
			                GROUP BY detallado_entradas.articulo_id, detallado_entradas.caducidad, detallado_entradas.lote
                        ) ON DUPLICATE KEY UPDATE
		                control_antibiotico_id = control_antibiotico_id;
                       ";
                 break;

                 case "DEVOLUCION_MAYORISTA":

                        sql = @"
                            INSERT INTO farmacontrol_local.control_antibioticos
                            (
	                            SELECT 
		                            0 AS control_antibiotico_id,
		                            'DEVOLUCION MAYORISTA' AS movimiento,
		                            articulo_id,
		                            caducidad,
		                            lote,
		                            (cantidad* -1) as cantidad,
                                    NULL AS control_antibioticos_receta_id,
		                            @elemento_id AS elemento_id,
                                    NOW() AS modified
	                            FROM
		                            farmacontrol_local.detallado_devoluciones
		                            JOIN farmacontrol_global.articulos USING (articulo_id)
	                            E
		                            detallado_devoluciones.devolucion_id = @elemento_id
	                            AND
		                            articulos.clase_antibiotico_id IS NOT NULL
	                            GROUP BY detallado_devoluciones.articulo_id, detallado_devoluciones.caducidad, detallado_devoluciones.lote
                            ) ON DUPLICATE KEY UPDATE
                            control_antibiotico_id = control_antibiotico_id";


                 break;
                 case "DEVOLUCION_CLIENTE":
                 sql = @"
                        INSERT INTO farmacontrol_local.control_antibioticos
		                (
			                SELECT 
				                0 AS control_antibiotico_id,
				               'DEVOLUCION CLIENTE' AS movimiento,
			                   articulo_id,
			                   caducidad,
			                   lote,
			                   cantidad,
                               NULL AS control_antibioticos_receta_id,
			                   @elemento_id AS elemento_id,
                               NOW() AS modified
			                FROM
				                farmacontrol_local.detallado_cancelaciones
				                JOIN farmacontrol_global.articulos USING (articulo_id)
			                WHERE
				                detallado_cancelaciones.cancelacion_id = @elemento_id
			                AND
				                articulos.clase_antibiotico_id IS NOT NULL
			                GROUP BY detallado_cancelaciones.articulo_id, detallado_cancelaciones.caducidad, detallado_cancelaciones.lote
                        ) ON DUPLICATE KEY UPDATE
		                control_antibiotico_id = control_antibiotico_id

                    ";

                 break;
                 case "TRASPASO_ENTRANTE":
                    sql = @"
                            INSERT INTO farmacontrol_local.control_antibioticos
		                    (
			                    SELECT 
				                   0 AS control_antibiotico_id,
				                   'TRASPASO ENTRANTE' AS movimiento,
			                       articulo_id,
			                       caducidad,
			                       lote,
			                       cantidad,
                                   NULL AS control_antibioticos_receta_id,
			                       @elemento_id AS elemento_id,
                                   NOW() AS modified
			                    FROM
				                    farmacontrol_local.detallado_traspasos
				                    JOIN farmacontrol_local.traspasos USING (traspaso_id)
				                    JOIN farmacontrol_global.articulos USING (articulo_id)
			                    WHERE
				                    detallado_traspasos.traspaso_id = @elemento_id
			                    AND
				                    articulos.clase_antibiotico_id IS NOT NULL
			                    AND 
				                    traspasos.tipo = 'RECIBIR'
			                    GROUP BY detallado_traspasos.articulo_id, detallado_traspasos.caducidad, detallado_traspasos.lote
                            ) ON DUPLICATE KEY UPDATE
		                    control_antibiotico_id = control_antibiotico_id 
                       ";

                   break;
                   case "MERMA":
                      sql = @"
                            INSERT INTO farmacontrol_local.control_antibioticos
		                    (
			                    SELECT 
				                   0 AS control_antibiotico_id,
				                   'MERMA' AS movimiento,
			                       articulo_id,
			                       caducidad,
			                       lote,
			                       (cantidad * -1) as cantidad,
                                   NULL AS control_antibioticos_receta_id,
			                       @elemento_id AS elemento_id,
                                   NOW() AS modified
			                    FROM
				                    farmacontrol_local.detallado_mermas
				                    JOIN farmacontrol_global.articulos USING (articulo_id)
			                    WHERE
				                    detallado_mermas.merma_id = @elemento_id
			                    AND
				                    articulos.clase_antibiotico_id IS NOT NULL
			                    GROUP BY detallado_mermas.articulo_id, detallado_mermas.caducidad, detallado_mermas.lote
		                    ) ON DUPLICATE KEY UPDATE
		                    control_antibiotico_id = control_antibiotico_id
                            ";
                   break;
            }

            
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            //parametros.Add("movimiento", movimiento);
            parametros.Add("elemento_id", elemento_id);
            
            conector.Insert(sql, parametros);
            //conector.Call(sql, parametros);
            
            //conector.Select(sql, parametros);

            return checa_movimiento(movimiento, elemento_id);
        }

        public bool registra_movimiento_venta(string movimiento, long elemento_id, List<DTO_detallado_generico> lista_dto)
        {
            bool ok = false;
           // string sql = @"
             //       farmacontrol_global.Control_AB_registra_movimientos
               // ";
            string sql = "";
            switch (movimiento)
            {
                case "VENTA":
                    sql = @"
                        INSERT INTO farmacontrol_local.control_antibioticos
		                (
			                SELECT 
				                0 AS control_antibiotico_id,
				                @movimiento AS movimiento,
			                    articulo_id,
			                    caducidad,
			                    lote,
			                    (cantidad * -1) AS cantidad,
                                NULL AS control_antibioticos_receta_id,
			                    @elemento_id AS elemento_id,
                                NOW() AS modified
			                FROM
				                farmacontrol_local.detallado_ventas
				                JOIN farmacontrol_global.articulos USING (articulo_id)
			                WHERE
				                detallado_ventas.venta_id = @elemento_id
			                AND
				                articulos.clase_antibiotico_id IS NOT NULL
			                GROUP BY detallado_ventas.articulo_id, detallado_ventas.caducidad, detallado_ventas.lote
		                ) ON DUPLICATE KEY UPDATE
		                control_antibiotico_id = control_antibiotico_id";
                    break;
                case "ENTRADA":

                    sql = @"
                        INSERT INTO farmacontrol_local.control_antibioticos
		                (
			                SELECT 
				                0 AS control_antibiotico_id,
				                @movimiento AS movimiento,
			                    articulo_id,
			                    caducidad,
			                    lote,
			                    cantidad,
                                NULL AS control_antibioticos_receta_id,
			                    @elemento_id AS elemento_id,
                                NOW() AS modified
			                FROM
				                farmacontrol_local.detallado_entradas
				                JOIN farmacontrol_global.articulos USING (articulo_id)
			                WHERE
				                detallado_entradas.entrada_id = @elemento_id
			                AND
				                articulos.clase_antibiotico_id IS NOT NULL
			                GROUP BY detallado_entradas.articulo_id, detallado_entradas.caducidad, detallado_entradas.lote
                        ) ON DUPLICATE KEY UPDATE
		                control_antibiotico_id = control_antibiotico_id;
                       ";
                    break;

                case "DEVOLUCION_MAYORISTA":

                    sql = @"
                            INSERT INTO farmacontrol_local.control_antibioticos
                            (
	                            SELECT 
		                            0 AS control_antibiotico_id,
		                            @movimiento AS movimiento,
		                            articulo_id,
		                            caducidad,
		                            lote,
		                            (cantidad* -1) as cantidad,
                                    NULL AS control_antibioticos_receta_id,
		                            @elemento_id AS elemento_id,
                                    NOW() AS modified
	                            FROM
		                            farmacontrol_local.detallado_devoluciones
		                            JOIN farmacontrol_global.articulos USING (articulo_id)
	                            WHERE
		                            detallado_devoluciones.devolucion_id = @elemento_id
	                            AND
		                            articulos.clase_antibiotico_id IS NOT NULL
	                            GROUP BY detallado_devoluciones.articulo_id, detallado_devoluciones.caducidad, detallado_devoluciones.lote
                            ) ON DUPLICATE KEY UPDATE
                            control_antibiotico_id = control_antibiotico_id";


                    break;
                case "DEVOLUCION_CLIENTE":
                    sql = @"
                        INSERT INTO farmacontrol_local.control_antibioticos
		                (
			                SELECT 
				                0 AS control_antibiotico_id,
				               @movimiento AS movimiento,
			                   articulo_id,
			                   caducidad,
			                   lote,
			                   cantidad,
                               NULL AS control_antibioticos_receta_id,
			                   @elemento_id AS elemento_id,
                               NOW() AS modified
			                FROM
				                farmacontrol_local.detallado_cancelaciones
				                JOIN farmacontrol_global.articulos USING (articulo_id)
			                WHERE
				                detallado_cancelaciones.cancelacion_id = @elemento_id
			                AND
				                articulos.clase_antibiotico_id IS NOT NULL
			                GROUP BY detallado_cancelaciones.articulo_id, detallado_cancelaciones.caducidad, detallado_cancelaciones.lote
                        ) ON DUPLICATE KEY UPDATE
		                control_antibiotico_id = control_antibiotico_id

                    ";

                    break;
                case "TRASPASO_ENTRANTE":
                    sql = @"
                            INSERT INTO farmacontrol_local.control_antibioticos
		                    (
			                    SELECT 
				                   0 AS control_antibiotico_id,
				                   @movimiento AS movimiento,
			                       articulo_id,
			                       caducidad,
			                       lote,
			                       cantidad,
                                   NULL AS control_antibioticos_receta_id,
			                       @elemento_id AS elemento_id,
                                   NOW() AS modified
			                    FROM
				                    farmacontrol_local.detallado_traspasos
				                    JOIN farmacontrol_local.traspasos USING (traspaso_id)
				                    JOIN farmacontrol_global.articulos USING (articulo_id)
			                    WHERE
				                    detallado_traspasos.traspaso_id = @elemento_id
			                    AND
				                    articulos.clase_antibiotico_id IS NOT NULL
			                    AND 
				                    traspasos.tipo = 'RECIBIR'
			                    GROUP BY detallado_traspasos.articulo_id, detallado_traspasos.caducidad, detallado_traspasos.lote
                            ) ON DUPLICATE KEY UPDATE
		                    control_antibiotico_id = control_antibiotico_id 
                       ";

                    break;
                case "MERMA":
                    sql = @"
                            INSERT INTO farmacontrol_local.control_antibioticos
		                    (
			                    SELECT 
				                   0 AS control_antibiotico_id,
				                   @movimiento AS movimiento,
			                       articulo_id,
			                       caducidad,
			                       lote,
			                       (cantidad * -1) as cantidad,
                                   NULL AS control_antibioticos_receta_id,
			                       @elemento_id AS elemento_id,
                                   NOW() AS modified
			                    FROM
				                    farmacontrol_local.detallado_mermas
				                    JOIN farmacontrol_global.articulos USING (articulo_id)
			                    WHERE
				                    detallado_mermas.merma_id = @elemento_id
			                    AND
				                    articulos.clase_antibiotico_id IS NOT NULL
			                    GROUP BY detallado_mermas.articulo_id, detallado_mermas.caducidad, detallado_mermas.lote
		                    ) ON DUPLICATE KEY UPDATE
		                    control_antibiotico_id = control_antibiotico_id
                            ";
                    break;
            }
            

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("movimiento", movimiento);
            parametros.Add("elemento_id", elemento_id);

           // conector.Call(sql, parametros);
            conector.Insert(sql, parametros);

            if (checa_movimiento(movimiento, elemento_id))
            {
                ok = asigna_receta_articulos(lista_dto);
            }
            return ok;
        }

        public long get_control_receta_id(long control_ab_id)
        {
            string sql = @"
                    SELECT
                        COALESCE(control_antibioticos_receta_id,0) AS control_antibioticos_receta_id
                    FROM
                        farmacontrol_local.control_antibioticos
                    WHERE
                        control_antibiotico_id = @control_ab_id
                ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("control_ab_id", control_ab_id);

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {
                    return Convert.ToInt64(row["control_antibioticos_receta_id"]);
                }
            }
            return 0;
        }

        public bool asigna_receta_articulos(List<DTO_detallado_generico> lista_articulos)
        {
            bool ok = false;
            string sql;
            foreach(DTO_detallado_generico articulo in lista_articulos)
            {
                sql = @"
                        UPDATE farmacontrol_local.control_antibioticos
                        SET
                            control_antibioticos_receta_id = @control_ab_receta_id
                        WHERE
                            elemento_id = @elemento_id
                        AND
                            movimiento = @movimiento
                        AND
                            articulo_id = @articulo_id
                    ";

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("control_ab_receta_id", articulo.receta_id);
                parametros.Add("elemento_id", articulo.elemento_id);
                parametros.Add("movimiento", articulo.movimiento);
                parametros.Add("articulo_id", articulo.articulo_id);

                conector.Update(sql, parametros);

                ok = Convert.ToBoolean(conector.filas_afectadas);
            }

            return ok;
        }

        public bool desasociar_recetas(string movimiento, long elemento_id, List<long?> lista_recetas)
        {
            string sql;
            bool ok;

            sql = @"
                UPDATE 
                    farmacontrol_local.control_antibioticos
                SET
                    control_antibioticos_receta_id = NULL
                WHERE
                    movimiento = @movimiento
                AND
                    elemento_id = @elemento_id
            ";
            Dictionary<string, object> parametros1 = new Dictionary<string, object>();
            parametros1 = new Dictionary<string, object>();
            parametros1.Add("movimiento", movimiento);
            parametros1.Add("elemento_id", elemento_id);

            conector.Update(sql, parametros1);

            ok = Convert.ToBoolean(conector.filas_afectadas);

            if (lista_recetas.Count > 0)
            {
                ok = limpia_recetas(lista_recetas);
            }

            return ok;
        }

        public bool limpia_recetas(List<long?> lista_recetas)
        {
            bool ok = false;
            foreach (long? receta in lista_recetas)
            {
                string sql = @"
                    DELETE FROM farmacontrol_local.control_antibioticos_recetas
                    WHERE
                        control_antibioticos_receta_id = @receta_id
                    ";
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("receta_id", receta);

                conector.Delete(sql, parametros);

                ok = Convert.ToBoolean(conector.filas_afectadas);
            }

            return ok;
        }

        public DTO_control_ab_recetas get_info_receta(long control_ab_receta_id)
        {
            DTO_control_ab_recetas dto_tmp = new DTO_control_ab_recetas();

            string sql = @"
                    SELECT
                        control_antibioticos_receta_id,
                        doctor,
                        direccion_consultorio,
                        cedula_profesional,
                        folio_receta,
                        comentarios
                    FROM
                        farmacontrol_local.control_antibioticos_recetas
                    WHERE
                        control_antibioticos_receta_id = @control_ab_receta_id
                ";

            Dictionary<string, object> parametros = new Dictionary<string,object>();
            parametros.Add("control_ab_receta_id", control_ab_receta_id);

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {
                    dto_tmp.doctor = row["doctor"].ToString();
                    dto_tmp.direccion_consultorio = row["direccion_consultorio"].ToString();
                    long? nullable = null;
                    dto_tmp.cedula_profesional = (row["cedula_profesional"].ToString().Equals(""))? nullable : Convert.ToInt64(row["cedula_profesional"]);
                    dto_tmp.folio_receta = row["folio_receta"].ToString();
                    dto_tmp.comentarios = row["comentarios"].ToString();
                    dto_tmp.control_antibioticos_receta_id = Convert.ToInt64(row["control_antibioticos_receta_id"]);
                }
            }

            return dto_tmp;
        }

        public bool checa_movimiento(string movimiento, long elemento_id)
        {
            bool ok = false;
            //
            movimiento = movimiento.Replace("_", " ");
           // string sql = @"farmacontrol_global.Control_AB_checa_si_existe_movimiento";
            string sql = @"
                SELECT
		            COUNT(*) AS registros
	            FROM
		            farmacontrol_local.control_antibioticos
	            WHERE
		            movimiento = @movimiento
	            AND 
		            elemento_id = @elemento_id";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("movimiento", movimiento);
            parametros.Add("elemento_id", elemento_id);

            //conector.Call(sql, parametros);
            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {
                    int cantidad = Convert.ToInt32(row["registros"]);
                    if (cantidad > 0)
                    {
                        ok = true;
                    }
                }
            }

            return ok;
        }

        public List<DTO_Clase_antibiotico> get_all_antibioticos()
        {
            List<DTO_Clase_antibiotico> lista_antibiotico = new List<DTO_Clase_antibiotico>();

            string sql = @"
                SELECT
                    clase_antibiotico_id,
                    nombre
                FROM
                    farmacontrol_global.clases_antibioticos
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            conector.Select(sql, parametros);

            if(conector.result_set.Rows.Count > 0)
            {
                foreach(DataRow row in conector.result_set.Rows)
                {
                    DTO_Clase_antibiotico dto = new DTO_Clase_antibiotico();
                    dto.clase_antibiotico_id = Convert.ToInt64(row["clase_antibiotico_id"]);
                    dto.nombre = row["nombre"].ToString();

                    lista_antibiotico.Add(dto);
                }
            }

            return lista_antibiotico;
        }

        public double get_existencias_control_antibioticos(string amecop)
        {
            double existencias = 1;
            string sql = @"
                    SELECT COALESCE(SUM(control_antibioticos.cantidad),0) AS cantidad
                    FROM farmacontrol_local.control_antibioticos
                    JOIN farmacontrol_global.articulos_amecops USING (articulo_id)
                    WHERE
	                    articulos_amecops.amecop = @par_amecop 
                ";
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("par_amecop", amecop);
            conector.Select(sql, parametros);

            var row = conector.result_set.Rows[0];

            existencias = (Math.Abs(Convert.ToInt32(row["cantidad"])) * 0.001);

            return existencias;
        }

        public int get_tiempo_existencias(long articulo_id)
        {
            int tmp = 0;
            //SUM(existencia) as existencias
            string sql = @"
                    SELECT 
                       COALESCE(SUM(existencia),0) as existencias
                    FROM 
                        farmacontrol_local.existencias
                    WHERE 
                        articulo_id = @par_articulo_id
                ";
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("par_articulo_id", articulo_id);
            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                var row = conector.result_set.Rows[0];

                tmp = Convert.ToInt32(row["existencias"]);
            }
            return tmp;
        }
        
        public List<DTO_control_antibiotico> get_control_antibioticos_por_amecop(string amecop)
        {
            List<DTO_control_antibiotico> lista_antibioticos = new List<DTO_control_antibiotico>();

            //string sql = "farmacontrol_global.Control_AB_get_control_antibioticos_por_amecop";
            string sql = @"
                SELECT 
		            control_antibioticos.control_antibiotico_id As control_antibiotico_id,
		            control_antibioticos.articulo_id AS articulo_id,
        
		            control_antibioticos.movimiento AS movimiento,

          
		            date_format(control_antibioticos.caducidad, '%d/%m/%Y') AS caducidad,
		            control_antibioticos.lote As lote,
		            control_antibioticos.cantidad AS cantidad,
		            control_antibioticos.control_antibioticos_receta_id AS control_antibioticos_receta_id,
		            control_antibioticos.elemento_id AS elemento_id,
		            articulos_amecops.amecop AS amecop
	             FROM farmacontrol_local.control_antibioticos
	             JOIN farmacontrol_global.articulos_amecops USING (articulo_id)
	             JOIN farmacontrol_global.articulos USING(articulo_id)
	             WHERE
		            articulos_amecops.amecop = @amecop
	            ;
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
           // parametros.Add("par_amecop", amecop);
            parametros.Add("amecop", amecop);
           // conector.Call(sql, parametros);
            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {
                    DTO_control_antibiotico dto = new DTO_control_antibiotico();
                    dto.control_antibiotico_id = Convert.ToInt64(row["control_antibiotico_id"]);
                    dto.amecop = row["amecop"].ToString();

                    string fecha_temp = Control_AB_get_fecha_movimiento( row["movimiento"].ToString(), Convert.ToInt64(row["elemento_id"]) );

                    //dto.fecha = Misc_helper.fecha(row["fecha"].ToString(), "LEGIBLE");
                    dto.fecha = Misc_helper.fecha(fecha_temp, "LEGIBLE");

                    dto.articulo_id = Convert.ToInt32(row["articulo_id"]);
                    dto.movimiento = row["movimiento"].ToString();
                    dto.caducidad = Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");
                    dto.lote = row["lote"].ToString();
                    dto.cantidad = Convert.ToInt64(row["cantidad"]);
                    long? nullable = null;
                    dto.control_antibioticos_receta_id = (row["control_antibioticos_receta_id"].ToString().Equals("")) ? nullable : Convert.ToInt64(row["control_antibioticos_receta_id"]);
                    dto.elemento_id = Convert.ToInt64(row["elemento_id"]);
                    lista_antibioticos.Add(dto);
                }
            }
            lista_antibioticos = lista_antibioticos.AsEnumerable().OrderByDescending(x => x.fecha).ToList();
            return lista_antibioticos;
        }


        //funcion creado por joel , para revisar el total de movimientos de antibioticos y se le resta a la existencia inicial

        public string total_movimiento_existencia_inicial(string amecop)
        {

            string existencia_inicial_ant = "0";
            string articulo_id = "0";
            string sql = @"
                SELECT 
		            control_antibioticos.control_antibiotico_id As control_antibiotico_id,
                    control_antibioticos.articulo_id AS articulo_id,
		            COALESCE(SUM(control_antibioticos.cantidad),0) AS cantidad,
		            control_antibioticos.control_antibioticos_receta_id AS control_antibioticos_receta_id,
		            control_antibioticos.elemento_id AS elemento_id,
		            articulos_amecops.amecop AS amecop
	             FROM farmacontrol_local.control_antibioticos
	             JOIN farmacontrol_global.articulos_amecops USING (articulo_id)
	             JOIN farmacontrol_global.articulos USING(articulo_id)
	             WHERE
		            articulos_amecops.amecop = @amecop
	            ;
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
           // parametros.Add("par_amecop", amecop);
            parametros.Add("amecop", amecop);
           // conector.Call(sql, parametros);
            conector.Select(sql, parametros);
            
            if (conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {
                   //checa el total 
                    existencia_inicial_ant = row["cantidad"].ToString();
                    articulo_id = row["articulo_id"].ToString();
                }

                //revisa la existencia total de la farmacia

                sql = @"
                SELECT 
		           COALESCE(SUM(existencia),0) as existencia_actual
	             FROM 
                     farmacontrol_local.existencias
	             WHERE
		            articulo_id = @articulo_id
	            ;
                ";

                parametros = new Dictionary<string, object>();
                parametros.Add("articulo_id", articulo_id);
           
                conector.Select(sql, parametros);
                Int64 exis_actual = 0;
                Int64 existencia_inicial_antibiotico = 0;
                if (conector.result_set.Rows.Count > 0)
                {
                    foreach (DataRow row in conector.result_set.Rows)
                    {
                        exis_actual = Convert.ToInt64(row["existencia_actual"].ToString());
                    }

                    existencia_inicial_antibiotico = Convert.ToInt64(existencia_inicial_ant.ToString());

                    Int64 diferencia = exis_actual - existencia_inicial_antibiotico;


                    existencia_inicial_ant = diferencia.ToString();

                }



            }


            return existencia_inicial_ant;
        
        
        }



        //funcion creado por Joel
        public string Control_AB_get_fecha_movimiento( string sTipoMovimiento, Int64 elemento_id )
        {
            string sql = "";
            
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            switch (sTipoMovimiento)
            {
                case "VENTA":
                    sql = @"  
                    SELECT
			            fecha_terminado AS fecha 
		            FROM
			            farmacontrol_local.ventas 
		            WHERE
			            venta_id = @elemento_id
                    ";
                break;
                case "ENTRADA":

                    sql = @"
                    SELECT
			            fecha_terminado AS fecha 
		            FROM
			            farmacontrol_local.entradas
		            WHERE
			            entrada_id = @elemento_id";
                break;
                case "DEVOLUCION CLIENTE":
                    sql = @"
                      SELECT
			            fecha AS fecha
		              FROM
			            farmacontrol_local.cancelaciones
		              WHERE
			            cancelacion_id = @elemento_id";
                break;
                case "DEVOLUCION MAYORISTA":

                    sql = @"
                        SELECT
			                fecha_terminado AS  fecha 
		                FROM
			                farmacontrol_local.devoluciones
		                WHERE
			                devolucion_id = @elemento_id";

                break;
                case "TRASPASO ENTRANTE":
                    sql = @"
                        SELECT
			                fecha_terminado AS  fecha 
		                FROM
			                farmacontrol_local.traspasos
		                WHERE
			                traspaso_id = @elemento_id
		                AND
			                tipo = 'RECIBIR';";
                break;
                case "TRASPASO SALIENTE":
                sql = @"
                       SELECT
			                fecha_terminado AS  fecha 
		               FROM
			                farmacontrol_local.traspasos
		               WHERE
			                traspaso_id = @elemento_id
		               AND
			                tipo = 'ENVIAR'";
                break;
                case "MERMA":
                sql = @"
                       SELECT
			                fecha_terminado AS fecha 
		               FROM
			                farmacontrol_local.mermas
		               WHERE
			                merma_id = @elemento_id";
                break;
            }

            parametros.Add("elemento_id", elemento_id);
            
            conector.Select(sql, parametros);

            string fecha = "";
          
            if (conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {
                    fecha =  row["fecha"].ToString();
                }
            }

            return fecha;
        }

        public List<DTO_control_antibiotico> get_control_antibioticos_por_articulo_id(long articulo_id)
        {
            List<DTO_control_antibiotico> lista_antibioticos = new List<DTO_control_antibiotico>();

            //string sql = "farmacontrol_global.Control_AB_get_control_antibioticos_por_articulo_id";
            string sql = @"
                   SELECT 
		                control_antibioticos.control_antibiotico_id As control_antibiotico_id,
		                control_antibioticos.fecha AS fecha,
		                control_antibioticos.articulo_id AS articulo_id,
		                control_antibioticos.movimiento AS movimiento,
		                control_antibioticos.caducidad AS caducidad,
		                control_antibioticos.lote As lote,
		                control_antibioticos.cantidad AS cantidad,
		                control_antibioticos.control_antibioticos_receta_id AS control_antibioticos_receta_id,
                        control_antibioticos.elemento_id AS elemento_id,
                        articulos_amecops.amecop AS amecop
	                FROM farmacontrol_local.control_antibioticos
                    JOIN farmacontrol_global.articulos_amecops USING (articulo_id)
                    JOIN farmacontrol_global.articulos USING(articulo_id)
	                WHERE
		                control_antibioticos.articulo_id = @articulo_id
	                ORDER BY
		                control_antibioticos.control_antibiotico_id DESC";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("articulo_id", articulo_id);
            //conector.Call(sql, parametros);
            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {
                    DTO_control_antibiotico dto = new DTO_control_antibiotico();
                    dto.control_antibiotico_id = Convert.ToInt32(row["control_antibiotico_id"]);
                    dto.articulo_id = Convert.ToInt32(row["articulo_id"]);
                    dto.movimiento = row["movimiento"].ToString();
                    dto.caducidad = Misc_helper.fecha(row["caducidad"].ToString(), "LEGIBLE");
                    dto.lote = row["lote"].ToString();
                    dto.cantidad = Convert.ToInt32(row["cantidad"]);
                    dto.amecop = row["amecop"].ToString();
                    dto.elemento_id = Convert.ToInt64(row["elemento_id"]);
                    dto.control_antibioticos_receta_id = Convert.ToInt64(row["control_antibioticos_receta_id"]);
                    lista_antibioticos.Add(dto);
                }
            }

            return lista_antibioticos;
        }

        public List<DTO_control_antibiotico> get_ventas_sin_receta()
        {
            List<DTO_control_antibiotico> lista_antibioticos = new List<DTO_control_antibiotico>();

            //string sql = "farmacontrol_global.Control_AB_get_ventas_sin_receta";
            string sql = @"
               SELECT 
		            t0.control_antibiotico_id,
		            t0.articulo_id,
		            t2.amecop AS amecop,
		            t1.nombre,
		            t0.fecha,
		            t0.movimiento,
		            t0.cantidad,
		            t0.doctor,
		            t0.cedula,
		            t0.receta,
		            t0.por_ajustar
	            FROM farmacontrol_local.control_antibioticos as t0
	            LEFT JOIN farmacontrol_global.articulos as t1 USING (articulo_id)
	            LEFT JOIN farmacontrol_global.articulos_amecops as t2 USING(articulo_id)
	            WHERE 
		            movimiento = 'VENTA'
	            ORDER BY control_antibiotico_id DESC
                ";

            //conector.Call(sql);
            conector.Select(sql);

            if (conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {
                    DTO_control_antibiotico dto = new DTO_control_antibiotico();
                    dto.control_antibiotico_id = Convert.ToInt32(row["control_antibiotico_id"]);
                    dto.articulo_id = Convert.ToInt32(row["articulo_id"]);
                    dto.amecop = row["amecop"].ToString();
                    lista_antibioticos.Add(dto);
                }
            }

            return lista_antibioticos;
        }

        public List<DTO_control_antibiotico> get_control_ab()
        {
            List<DTO_control_antibiotico> lista_antibioticos = new List<DTO_control_antibiotico>();

            //string sql = "farmacontrol_global.Control_AB_get_control_ab";
            string sql = @"	
                SELECT
		            t0.id_control_antibioticos,
		            t0.articulo_id, 
		            t3.amecop,
		            t0.fecha, 
		            t0.movimiento, 
		            t0.caducidad,
		            t0.lote,
		            t0.cantidad,
		            t0.elemento_id,
		            t0.control_antibioticos_receta_id
	            FROM farmacontrol_local.control_ab as t0
	            LEFT JOIN farmacontrol_global.articulos as t1 USING(articulo_id)
	            LEFT JOIN farmacontrol_global.articulos_amecops t3 USING (articulo_id)";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            
           // conector.Call(sql, parametros);
            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {
                    DTO_control_antibiotico dto = new DTO_control_antibiotico();
                    dto.control_antibiotico_id = Convert.ToInt64(row["control_antibiotico_id"]);
                    dto.articulo_id = Convert.ToInt32(row["articulo_id"]);
                    dto.amecop = row["amecop"].ToString();
                    dto.movimiento = row["movimiento"].ToString();
                    dto.cantidad = Convert.ToInt32(row["cantidad"]);
                    dto.caducidad = Misc_helper.fecha(row["doctor"].ToString(), "CADUCIDAD");
                    dto.lote = row["lote"].ToString();
                    dto.control_antibioticos_receta_id = Convert.ToInt32(row["control_antibioticos_receta_id"]);
                    dto.elemento_id = Convert.ToInt64(row["elemento_id"]);
                    lista_antibioticos.Add(dto);
                }
            }

            return lista_antibioticos;
        }

        public long agregar_datos_receta(DTO_control_ab_recetas dto_receta)
        {
            long result = 0;
            string sql;

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            if (dto_receta.tipo_receta == "T")
            {
                /*
                sql = @"
                    INSERT INTO
                        farmacontrol_local.control_antibioticos_recetas (folio_receta, tipo_receta, comentarios, modified)
                    (
                        SELECT
                            (COALESCE(MAX(folio_receta),0) + 1) as folio_receta,
                            'T' as tipo_receta,
                            @par_comentarios AS comentarios,
                            NOW() AS modified
                        FROM
                            farmacontrol_local.control_antibioticos_recetas
                    )
                ";
                parametros.Add("par_comentarios", dto_receta.comentarios);
                */

                sql = @"
                    INSERT INTO
                        farmacontrol_local.control_antibioticos_recetas
                    SET
                        folio_receta = @folio_receta,
                        doctor = @doctor,
                        direccion_consultorio = @direccion,
                        cedula_profesional = @cedula,
                        comentarios = @comentarios,
                        tipo_receta = 'T',
                        modified = NOW()
                ";
                parametros.Add("folio_receta",dto_receta.folio_receta);
                parametros.Add("doctor", dto_receta.doctor);
                parametros.Add("direccion", dto_receta.direccion_consultorio);
                parametros.Add("cedula", dto_receta.cedula_profesional);
                parametros.Add("comentarios", dto_receta.comentarios);

            }
            else
            {
                sql = @"
                    INSERT INTO
                        farmacontrol_local.control_antibioticos_recetas
                    SET
                        doctor = @doctor,
                        direccion_consultorio = @direccion,
                        cedula_profesional = @cedula,
                        comentarios = @comentarios,
                        tipo_receta = 'P',
                        modified = NOW()
                ";

                parametros.Add("doctor", dto_receta.doctor);
                parametros.Add("direccion", dto_receta.direccion_consultorio);
                parametros.Add("cedula", dto_receta.cedula_profesional);
                parametros.Add("comentarios", dto_receta.comentarios);
            }
            
            conector.Insert(sql, parametros);

            if (conector.insert_id > 0)
            {
                result = conector.insert_id;
            }

            return result;
        }

        public long get_folio_receta(long? control_ab_receta_id)
        {
            long folio = 0;

            string sql = @"
                SELECT
	                COALESCE(folio_receta, 0) as folio_receta
                FROM 
	                farmacontrol_local.control_antibioticos_recetas
                WHERE 
	                control_antibioticos_receta_id = @par_receta_id
                ";
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("par_receta_id", control_ab_receta_id);

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {
                    folio = Convert.ToInt64(row["folio_receta"]);
                }
            }
            return folio;
        }

        public bool borrar_datos_receta(long control_ab_receta_id)
        {
            bool result = false; ;

            string sql = @"
                DELETE FROM
                    farmacontrol_local.control_antibioticos_recetas
                WHERE
                    control_antibioticos_receta_id = @control_ab_receta_id
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("control_ab_receta_id", control_ab_receta_id);
            

            conector.Delete(sql, parametros);

            if (conector.filas_afectadas > 0)
            {
                result = true;
            }

            return result;
        }

        public bool actualiza_datos_receta(DTO_control_ab_recetas dto_receta)
        {
            string sql = @"
                UPDATE farmacontrol_local.control_antibioticos_recetas
                SET
                    doctor = @doctor,
                    direccion_consultorio = @direccion,
                    cedula_profesional = @cedula,
                    folio_receta = @receta,
                    comentarios = @comentarios
                WHERE
                    control_antibioticos_receta_id =  @control_ab_receta_id
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("doctor", dto_receta.doctor);
            parametros.Add("direccion", dto_receta.direccion_consultorio);
            parametros.Add("cedula", dto_receta.cedula_profesional);
            parametros.Add("receta", dto_receta.folio_receta);
            parametros.Add("comentarios", dto_receta.comentarios);
            parametros.Add("control_ab_receta_id", dto_receta.control_antibioticos_receta_id);

            conector.Update(sql, parametros);

            return Convert.ToBoolean(conector.filas_afectadas);

        }

        public bool borrado_manual (long control_antibiotico_id, string tipo_movimiento, long articulo_id)
        {
            bool result = false;

            string sql = @"
                DELETE FROM 
                    farmacontrol_local.control_antibioticos
                WHERE
                    control_antibiotico_id = @control_antibiotico_id
                AND
                    tipo_movimiento = 'VENTA' 
                AND 
                    articulo_id = @articulo_id
                AND 
                    (
	                    (doctor is null or doctor = '')
	                    AND (receta is null or receta =  '')
	                    AND (cedula is null or cedula = '')
                    )
                ";

 
            Dictionary<string, object>  parametros = new Dictionary<string, object>();
            parametros.Add("control_antibiotico_id", control_antibiotico_id);
            parametros.Add("articulo_id", articulo_id);
            

            conector.Delete(sql, parametros);

            if (conector.filas_afectadas > 0)
            {
                result = true;
            }
            return result;
        }

        public List<DTO_reporte_recetas> reporte_recetas(string fecha_ini, string fecha_fin)
        {
            List<DTO_reporte_recetas> lista_movimientos = new List<DTO_reporte_recetas>();

            string sql = @"
                    SELECT	                        t0.control_antibioticos_receta_id 	AS control_ab_receta_id,                            t1.control_antibiotico_id           AS control_antibiotico_id,                            t0.doctor 							AS doctor,                            t0.direccion_consultorio 			AS direccion_consultorio,                            COALESCE(t0.cedula_profesional, 0)	AS cedula_profesional,                            t0.folio_receta 					AS folio_receta,                            t0.comentarios              		AS comentarios,                            t1.elemento_id 						AS venta_id,                            t2.venta_folio 						AS venta_folio,                            t0.modified 						AS fecha_receta,                            t2.fecha_terminado					AS fecha_venta,                            t3.articulo   						AS codigos                        FROM	                        farmacontrol_local.control_antibioticos_recetas AS t0                        LEFT JOIN	                        farmacontrol_local.control_antibioticos AS t1 					USING(control_antibioticos_receta_id )                        LEFT JOIN 	                        farmacontrol_local.ventas AS t2 			ON t1.elemento_id = t2.venta_id                        LEFT JOIN 							(								SELECT									venta_id as venta_id,									GROUP_CONCAT( CONCaT(cast(amecop_original as unsigned),' ',nombre, '  PIEZAS : ', cantidad )  ) AS articulo								FROM 								   farmacontrol_local.detallado_ventas								INNER JOIN 								   farmacontrol_global.articulos								USING(articulo_id)								WHERE 								   clase_antibiotico_id IS NOT NULL								AND								   activo = 1									GROUP BY 									venta_id,articulo_id														) as t3 on t3.venta_id =t2.venta_id                        WHERE	                        DATE(t0.modified) BETWEEN @par_fecha_ini AND @par_fecha_fin                        ORDER BY t0.folio_receta";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("par_fecha_ini", fecha_ini);
            parametros.Add("par_fecha_fin", fecha_fin);
            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {
                    DTO_reporte_recetas dto_rep = new DTO_reporte_recetas();

                    dto_rep.control_ab_receta_id = Convert.ToInt64(row["control_ab_receta_id"]);
                    dto_rep.control_antibiotico_id = Convert.ToInt64(row["control_antibiotico_id"]);
                    dto_rep.doctor = row["doctor"].ToString();
                    dto_rep.direccion_consultorio = row["direccion_consultorio"].ToString();
                    dto_rep.cedula_profesional = Convert.ToInt64(row["cedula_profesional"]);
                    dto_rep.folio_receta = row["folio_receta"].ToString();
                    dto_rep.comentarios = row["comentarios"].ToString();
                    dto_rep.elemento_id = Convert.ToInt64(row["venta_id"]);
                    dto_rep.venta_folio = Convert.ToInt64(row["venta_folio"]);
                    dto_rep.fecha_receta = Misc_helper.fecha(row["fecha_receta"].ToString(), "LEGIBLE");
                    dto_rep.fecha_venta = Misc_helper.fecha(row["fecha_venta"].ToString(), "LEGIBLE");
                    dto_rep.codigos = row["codigos"].ToString();
                    lista_movimientos.Add(dto_rep);
                }
            }

            return lista_movimientos;
        }

        public List<DTO_reporte_ab> get_reporte_ab() 
        {
            List<DTO_reporte_ab> lista_rep = new List<DTO_reporte_ab>();

            string sql = @"
                SELECT
	                tmp.folio AS folio,
	                tmp.mayorista AS mayorista,
	                tmp.termino AS empleado,
	                tmp.comentarios,
	                tmp.fecha_terminado
                FROM
                (

	                (
		                SELECT
			                entradas.entrada_id AS folio,
			                mayoristas.nombre AS mayorista,
			                empleados.nombre AS termino,
			                entradas.comentarios AS comentarios,
			                entradas.fecha_terminado AS fecha_terminado
		                FROM
			                farmacontrol_local.detallado_entradas
		                JOIN farmacontrol_local.entradas USING(entrada_id)
		                LEFT JOIN farmacontrol_global.mayoristas USING(mayorista_id)
		                JOIN farmacontrol_global.articulos USING(articulo_id)
		                JOIN farmacontrol_global.empleados ON
			                empleados.empleado_id = entradas.termina_empleado_id
		                WHERE
			                articulos.clase_antibiotico_id IS NOT NULL
		                AND
			                DATE(entradas.fecha_terminado) BETWEEN '2015-09-01' AND '2015-09-30'
		                AND
			                mayoristas.mayorista_id != 256 
	                )

	                UNION

	                (
		                SELECT
			                entradas.entrada_id AS folio,
			                mayoristas.nombre AS mayorista,
			                empleados.nombre AS termino,
			                entradas.comentarios AS comentarios,
			                entradas.fecha_terminado AS fecha_terminado
		                FROM
			                farmacontrol_local.detallado_entradas
		                JOIN farmacontrol_local.entradas USING(entrada_id)
		                LEFT JOIN farmacontrol_global.mayoristas USING(mayorista_id)
		                JOIN farmacontrol_global.articulos USING(articulo_id)
		                JOIN farmacontrol_global.empleados ON
			                empleados.empleado_id = entradas.termina_empleado_id
		                WHERE
			                articulos.clase_antibiotico_id IS NOT NULL
		                AND
			                DATE(entradas.fecha_terminado) BETWEEN '2015-09-01' AND '2015-09-30'
		                AND
			                mayoristas.mayorista_id = 256 
		                AND
			                entradas.comentarios LIKE '%ALMACEN%'
	                )

                ) AS tmp
                ORDER BY tmp.folio
            ";
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {
                    DTO_reporte_ab dto_rep = new DTO_reporte_ab();

                    dto_rep.folio = Convert.ToInt64(row["folio"]);
                    dto_rep.mayorista = row["mayorista"].ToString();
                    dto_rep.termino = row["termino"].ToString();
                    dto_rep.comentarios = row["comentarios"].ToString();
                    dto_rep.fecha_terminado = row["fecha_terminado"].ToString();

                    lista_rep.Add(dto_rep);
                }
            }
            return lista_rep;
        }

        public bool cambia_ticket_venta(long venta_id, long elemento_id, string movimiento)
        {
            string sql = @"
                    UPDATE
                        farmacontrol_local.control_antibioticos
                    SET
                        elemento_id = @venta_id
                    WHERE
                        movimiento = @movimiento
                    AND
                        elemento_id = @elemento_id;
                ";
            Dictionary<string, object>  parametros = new Dictionary<string, object>();
            parametros.Add("venta_id", venta_id);
            parametros.Add("movimiento", movimiento);
            parametros.Add("elemento_id", elemento_id);

            conector.Update(sql, parametros);

            return Convert.ToBoolean(conector.filas_afectadas);
        }

        public string ajuste_de_antibioticos(long control_antibiotico_id, long articulo_id, string tipo_movimiento, long cantidad )
        {
            string result = "error";

            string sql = @"
                    SELECT 
                         tipo_movimiento, 
                         articulo_id, 
                         control_antibiotico_id
                    FROM 
                        farmacontrol_local.control_antibioticos 
                    WHERE
                        articulo_id = @articulo_id
                    AND 
                        tipo_movimiento = 'ENTRADA'
                    AND 
                        cantidad = ABS(@cantidad)
                    AND 
                        (
	                        (doctor is null or doctor = '')
	                        AND (receta is null or receta =  '')
	                        AND (cedula is null or cedula = '')
                        )
                    ORDER BY control_antibiotico_id ASC 
                    LIMIT 1";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("cantidad", cantidad);
            parametros.Add("articulo_id", articulo_id);

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                sql = @"
                DELETE FROM 
                    farmacontrol_local.control_antibioticos
                WHERE
                    control_antibiotico_id = @control_antibiotico_id
                AND 
                    tipo_movimiento = 'VENTA' 
                AND 
                    articulo_id = @articulo_id
                AND 
                        (
	                        (doctor is null or doctor = '')
	                        AND (receta is null or receta =  '')
	                        AND (cedula is null or cedula = '')
                        )
                ";

                var row = conector.result_set.Rows[0];
                parametros = new Dictionary<string, object>();
                parametros.Add("control_antibiotico_id", control_antibiotico_id);
                parametros.Add("articulo_id", articulo_id);
                parametros.Add("tipo_movimiento", tipo_movimiento);

                conector.Delete(sql, parametros);

                if (conector.filas_afectadas > 0)
                {
                    sql = @"
                    DELETE FROM 
                        farmacontrol_local.control_antibioticos
                    WHERE
                        control_antibiotico_id = @control_antibiotico_id
                    AND
                        tipo_movimiento = 'ENTRADA'
                    AND 
                        articulo_id = @articulo_id
                    AND 
                        (
	                        (doctor is null or doctor = '')
	                        AND (receta is null or receta =  '')
	                        AND (cedula is null or cedula = '')
                        )
                    ";
                    
                    parametros = new Dictionary<string, object>();
                    parametros.Add("control_antibiotico_id", row["control_antibiotico_id"]);
                    parametros.Add("tipo_movimiento", row["tipo_movimiento"]);
                    parametros.Add("articulo_id", row["articulo_id"]);

                    conector.Delete(sql, parametros);

                    if (conector.filas_afectadas > 0)
                    {
                        result = "eliminar";
                    }
                }
            }
            else
            {
                sql = @"
                    UPDATE
                        farmacontrol_local.control_antibioticos
                    SET
                        por_ajustar = 1
                    WHERE
                        control_antibiotico_id = @control_antibiotico_id
                    AND 
                        tipo_movimiento = 'VENTA'
                    AND
                        articulo_id = @articulo_id
                    AND 
                        (
	                        (doctor is null or doctor = '')
	                        AND (receta is null or receta =  '')
	                        AND (cedula is null or cedula = '')
                        )
                ";

                parametros = new Dictionary<string, object>();
                parametros.Add("control_antibiotico_id", control_antibiotico_id);
                parametros.Add("tipo_movimiento", tipo_movimiento);
                parametros.Add("articulo_id", articulo_id);

                conector.Update(sql,parametros);

                if (conector.filas_afectadas > 0)
                {
                    result = "actualizar";
                }
            }
            return result;
        }


        //funcion que obtiene el tipo de antibiotico 
        public string get_tipo_antibiotico(long clase_antibiotico_id)
        {

            string sustancia = "";

            string sql = @"
                SELECT
	                nombre as sustancia
                FROM 
	                farmacontrol_global.clases_antibioticos
                WHERE 
	                clase_antibiotico_id = @clase_antibiotico_id
                ";
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("clase_antibiotico_id", clase_antibiotico_id);

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {
                    sustancia = row["sustancia"].ToString();
                }
            }
           

            return sustancia;
        
        }

        //funcion que obtiene folio autoincrementable 
        public long get_folio_receta_total()
        {
            long folio_receta = 0;

            string sql = @"
                SELECT                   (COALESCE(MAX( folio_receta ),0) +1) as folio                   FROM                   farmacontrol_local.control_antibioticos_recetas                WHERE                tipo_receta = 'T'
                ";
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("clase_antibiotico_id", "T");

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                foreach (DataRow row in conector.result_set.Rows)
                {
                    folio_receta = Convert.ToInt64(row["folio"]);
                }
            }

            return folio_receta;
       
        }


    }
}
