using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using System.Data;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Cambio_precios
	{
		Conector conector = new Conector();

		public DTO_Cambio_precio get_cambio_precio_data(long cambio_precio_id)
		{
			string sql = @"
				SELECT
					cambio_precio_id,
					fecha_creado,
					mayoristas.nombre AS mayorista,
					COUNT(detallado_cambio_precio_id) AS numero_productos
				FROM
					farmacontrol_global.cambio_precios
				JOIN farmacontrol_global.mayoristas USING(mayorista_id)
				LEFT JOIN detallado_cambio_precios USING(cambio_precio_id)
				WHERE
					cambio_precio_id = @cambio_precio_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("cambio_precio_id",cambio_precio_id);

			conector.Select(sql,parametros);

			DTO_Cambio_precio dto = new DTO_Cambio_precio();

			if(conector.result_set.Rows.Count > 0)
			{
				var row = conector.result_set.Rows[0];

				dto.cambio_precio_id = Convert.ToInt64(row["cambio_precio_id"]);
				dto.fecha_creado = Convert.ToDateTime(row["fecha_creado"]);
				dto.mayorista = row["mayorista"].ToString();
				dto.numero_productos = Convert.ToInt64(row["numero_productos"]);
			}

			return dto;
		}

		public List<DTO_Detallado_cambio_precios_reporte> get_detallado_cambio_precio_reporte(long cambio_precio_id)
		{
			List<DTO_Detallado_cambio_precios_reporte> lista_detallado = new List<DTO_Detallado_cambio_precios_reporte>();

			var detallado = get_detallado(cambio_precio_id);

			foreach (DataRow row in detallado.Rows)
			{
				DTO_Detallado_cambio_precios_reporte dto = new DTO_Detallado_cambio_precios_reporte();
				dto.amecop = "*"+row["amecop"].ToString().Substring(row["amecop"].ToString().Length - 4,4);
				dto.existencia = Convert.ToInt64(row["existencia"]);
				dto.producto = row["producto"].ToString();
				dto.precio_costo_anterior = Convert.ToDecimal(row["precio_costo_anterior"]);
				dto.precio_costo_nuevo = Convert.ToDecimal(row["precio_costo_nuevo"]);
				dto.incr_costo = Convert.ToDecimal(Convert.ToDecimal(row["incr_publico"]) * 100);
				dto.incr_publico = Convert.ToDecimal(Convert.ToDecimal(row["incr_costo"]) * 100);
				dto.precio_publico_anterior = Convert.ToDecimal(row["precio_publico_anterior"]);
				dto.precio_publico_nuevo = Convert.ToDecimal(row["precio_publico_nuevo"]);

				lista_detallado.Add(dto);
			}

			return lista_detallado;
		}

        public bool imprimir_reetiquetado(long cambio_precio_id)
        {
            string sql = @"
               SELECT
					COALESCE(COUNT(articulos.nombre), 0) AS productos
				FROM
					farmacontrol_global.detallado_cambio_precios
				JOIN farmacontrol_global.articulos USING(articulo_id)
				LEFT JOIN farmacontrol_local.existencias USING(articulo_id)
				WHERE
					detallado_cambio_precios.articulo_ignorado IS FALSE
				AND
					cambio_precio_id = @cambio_precio_id
				AND 
                    existencia != 0
                AND detallado_cambio_precios.precio_publico_anterior != detallado_cambio_precios.precio_publico
				ORDER BY articulos.nombre ASC
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("cambio_precio_id", cambio_precio_id);

            conector.Select(sql, parametros);

            if(conector.result_set.Rows.Count > 0)
            {
                return (Convert.ToInt64(conector.result_set.Rows[0]["productos"]) > 0);
            }

            return false;
        }

		DataTable get_detallado(long cambio_precio_id, bool excluir_sin_existencia = false, bool excluir_sin_cambios_precio = false)
		{
			string complemento = string.Format("{0} {1}",
				(excluir_sin_existencia)
					? "AND existencia != 0" : "",
				(excluir_sin_cambios_precio)
					? "AND detallado_cambio_precios.precio_publico_anterior != detallado_cambio_precios.precio_publico" : "");

			string sql = string.Format(@"
				SELECT
					(
						SELECT
							ABS(amecop) AS amecop
						FROM
							farmacontrol_global.articulos_amecops
						WHERE
							articulos_amecops.articulo_id = detallado_cambio_precios.articulo_id
						ORDER BY articulos_amecops.amecop_principal DESC
						LIMIT 1
					) AS amecop,
					(precio_publico_anterior + (precio_publico_anterior * articulos.pct_iva)) AS precio_publico_anterior,
					(detallado_cambio_precios.precio_publico + ( detallado_cambio_precios.precio_publico * articulos.pct_iva )) AS precio_publico_nuevo,
					(precio_costo_anterior + ( precio_costo_anterior * articulos.pct_iva )) AS precio_costo_anterior,
					(detallado_cambio_precios.precio_costo + ( detallado_cambio_precios.precio_costo * articulos.pct_iva ) ) AS precio_costo_nuevo,
					articulos.nombre AS producto,
					(
						(detallado_cambio_precios.precio_publico - detallado_cambio_precios.precio_publico_anterior) 
						/ detallado_cambio_precios.precio_publico
					) AS incr_publico,
					(
						(detallado_cambio_precios.precio_costo - detallado_cambio_precios.precio_costo_anterior) 
						/ detallado_cambio_precios.precio_costo
					) AS incr_costo,
					SUM(COALESCE(existencias.existencia,0)) AS existencia
				FROM
					farmacontrol_global.detallado_cambio_precios
				JOIN farmacontrol_global.articulos USING(articulo_id)
				LEFT JOIN farmacontrol_local.existencias USING(articulo_id)
				WHERE
					detallado_cambio_precios.articulo_ignorado IS FALSE
				AND
					cambio_precio_id = @cambio_precio_id
				{0}
				GROUP BY detallado_cambio_precios.articulo_id
				ORDER BY articulos.nombre ASC
			", complemento);

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("cambio_precio_id", cambio_precio_id);

			conector.Select(sql, parametros);

			return conector.result_set;
		}

		public List<DTO_Detallado_cambio_precios> get_detallado_cambio_precio(long cambio_precio_id, bool excluir_sin_existencia = false, bool excluir_sin_cambios_precio = false)
		{
			List<DTO_Detallado_cambio_precios> lista_detallado = new List<DTO_Detallado_cambio_precios>();

			var detallado = get_detallado(cambio_precio_id,excluir_sin_existencia,excluir_sin_cambios_precio);

			foreach(DataRow row in detallado.Rows)
			{
				DTO_Detallado_cambio_precios dto = new DTO_Detallado_cambio_precios();
				dto.amecop = Convert.ToInt64(row["amecop"]);
				dto.existencia = Convert.ToInt64(row["existencia"]);
				dto.producto = row["producto"].ToString();
				dto.precio_costo_anterior = Convert.ToDecimal(row["precio_costo_anterior"]);
				dto.precio_costo_nuevo = Convert.ToDecimal(row["precio_costo_nuevo"]);
				dto.incr_costo = Convert.ToDecimal(row["incr_publico"]);
				dto.incr_publico = Convert.ToDecimal(row["incr_costo"]);
				dto.precio_publico_anterior = Convert.ToDecimal(row["precio_publico_anterior"]);
				dto.precio_publico_nuevo = Convert.ToDecimal(row["precio_publico_nuevo"]);

				lista_detallado.Add(dto);
			}

			return lista_detallado;
		}

		public List<DTO_Cambio_precio> get_cambio_precios()
		{
			List<DTO_Cambio_precio> lista_cambio_precio = new List<DTO_Cambio_precio>();

			string sql = @"
				SELECT
					cambio_precio_id,
					fecha_creado,
					mayoristas.nombre AS mayorista,
					COUNT(detallado_cambio_precio_id) AS numero_productos
				FROM
					farmacontrol_global.cambio_precios
				JOIN farmacontrol_global.mayoristas USING(mayorista_id)
				LEFT JOIN detallado_cambio_precios USING(cambio_precio_id)
				WHERE
					DATE(fecha_creado) BETWEEN DATE_SUB(CURDATE(), INTERVAL 1 MONTH) AND CURDATE()
				GROUP BY cambio_precio_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			
			conector.Select(sql,parametros);
			
			foreach(DataRow row in conector.result_set.Rows)
			{
				DTO_Cambio_precio dto = new DTO_Cambio_precio();
				dto.cambio_precio_id = Convert.ToInt64(row["cambio_precio_id"]);
				dto.fecha_creado = Convert.ToDateTime(row["fecha_creado"]);
				dto.mayorista = row["mayorista"].ToString();
				dto.numero_productos = Convert.ToInt64(row["numero_productos"]);

				lista_cambio_precio.Add(dto);
			}

			return lista_cambio_precio;
		}
	}
}
