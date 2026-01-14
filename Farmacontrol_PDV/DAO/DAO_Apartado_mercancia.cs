using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.DTO;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Apartado_mercancia
	{
		Conector conector = new Conector();

        public bool existe_apartado_sucursal(long sucursal_id)
        {
            string sql = @"
                SELECT
                    apartado_id
                FROM
                    farmacontrol_local.apartados
                WHERE
                    sucursal_id = @sucursal_id
            ";

            Dictionary<string,object> parametros = new Dictionary<string,object>();
            parametros.Add("sucursal_id",sucursal_id);

            conector.Select(sql, parametros);

            return (conector.result_set.Rows.Count > 0) ? true : false;
        }

		public void eliminar_apartado_prepago(long prepago_id)
		{
			string sql = @"
				DELETE FROM
					farmacontrol_local.apartados
				WHERE
					prepago_id = @prepago_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("prepago_id",prepago_id);

			conector.Delete(sql,parametros);
		}

		public List<DTO_Detallado_ventas_vista_previa> get_productos_prepago_parcial(long prepago_id)
		{
			List<DTO_Detallado_ventas_vista_previa> lista = new List<DTO_Detallado_ventas_vista_previa>();

			string sql = @"
				SELECT
					(
						SELECT 
							amecop
						FROM 
							farmacontrol_global.articulos_amecops 
						WHERE 
							articulo_id = farmacontrol_local.apartados.articulo_id
						ORDER BY
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS amecop,
					articulos.nombre AS producto,
					apartados.articulo_id AS articulo_id,
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					cantidad
				FROM 
					farmacontrol_local.apartados
				JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					apartados.prepago_id = @prepago_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("prepago_id",prepago_id);

			conector.Select(sql,parametros);

			var result = conector.result_set;

			if(result.Rows.Count > 0)
			{
				foreach(DataRow row in result.Rows)
				{
					DTO_Detallado_ventas_vista_previa vista = new DTO_Detallado_ventas_vista_previa();
					vista.amecop = row["amecop"].ToString();
					vista.producto = row["producto"].ToString();
					vista.articulo_id = Convert.ToInt64(row["articulo_id"]);
                    vista.caducidad = Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");
					vista.lote = row["lote"].ToString();
					vista.cantidad = Convert.ToInt64(row["cantidad"]);

					lista.Add(vista);
				}
			}


			return lista;
		}

		public List<DTO_Apartado_mercancia> eliminar_apartado(long apartado_id)
		{
			string sql = @"
				DELETE FROM
					farmacontrol_local.apartados
				WHERE
					apartado_id = @apartado_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("apartado_id",apartado_id);

			conector.Delete(sql,parametros);

			return get_apartados();
		}

		public List<DTO_Apartado_mercancia> agregar_producto_apartado_mercancia(int articulo_id, string caducidad, string lote, long cantidad, string tipo, int? sucursal_id, long? prepago_id = null)
		{
			string sql = @"
				INSERT INTO
					farmacontrol_local.apartados
				SET
					articulo_id = @articulo_id,
					caducidad = @caducidad,
					lote = @lote,
					tipo = @tipo,
					sucursal_id = @sucursal_id,
					prepago_id = @prepago_id,
					cantidad = @cantidad,
					fecha_apartado = NOW(),
					fecha_expiracion = DATE_ADD(NOW(), INTERVAL 1 DAY)
				ON DUPLICATE KEY UPDATE
					cantidad = cantidad + @cantidad,
                    fecha_apartado = NOW(),
                    fecha_expiracion = DATE_ADD(NOW(), INTERVAL 1 DAY)
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("articulo_id",articulo_id);
			parametros.Add("caducidad",caducidad);
			parametros.Add("lote",lote);
			parametros.Add("tipo",tipo);
			parametros.Add("sucursal_id",sucursal_id);
			parametros.Add("cantidad",cantidad);
			parametros.Add("prepago_id",prepago_id);

			conector.Insert(sql,parametros);

			return get_apartados();
		}

		public List<DTO_Apartado_mercancia> get_apartados()
		{
			List<DTO_Apartado_mercancia> lista = new List<DTO_Apartado_mercancia>();

			string sql = @"
				SELECT
					apartado_id,
					tipo AS destino_sin_formato,
					IF(sucursal_id IS NULL, tipo, (SELECT nombre FROM farmacontrol_global.sucursales WHERE sucursal_id = farmacontrol_local.apartados.sucursal_id)) AS destino,
					(
						SELECT 
							amecop
						FROM 
							farmacontrol_global.articulos_amecops 
						WHERE 
							articulo_id = farmacontrol_local.apartados.articulo_id
						ORDER BY
							articulos_amecops.amecop_principal 
						DESC
						LIMIT 1
					) AS amecop,
					articulos.nombre AS producto,
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					cantidad,
					CAST(DATE_FORMAT(fecha_apartado,'%Y-%m-%d %h:%i:%s') AS CHAR(50)) AS fecha_apartado,
					CAST(DATE_FORMAT(fecha_expiracion,'%Y-%m-%d %h:%i:%s') AS CHAR(50)) AS fecha_expiracion
				FROM
					farmacontrol_local.apartados
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				GROUP BY
					apartado_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			conector.Select(sql,parametros);

			var result = conector.result_set;

			foreach(DataRow row in result.Rows)
			{
				DTO_Apartado_mercancia articulo = new DTO_Apartado_mercancia();
				articulo.amecop = row["amecop"].ToString();
				articulo.apartado_id = Convert.ToInt64(row["apartado_id"]);
                articulo.caducidad = (row["caducidad"].ToString().Equals("")) ? " " : Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");
				articulo.cantidad = Convert.ToInt64(row["cantidad"]);
				articulo.destino = row["destino"].ToString().Replace("_"," ");
				articulo.destino_sin_formato = row["destino_sin_formato"].ToString();
				articulo.fecha_apartado = Convert.ToDateTime(row["fecha_apartado"]);
				articulo.fecha_expiracion = Convert.ToDateTime(row["fecha_expiracion"]);
				articulo.lote = row["lote"].ToString();
				articulo.producto = row["producto"].ToString();

				lista.Add(articulo);
			}

			return lista;
		}
	}
}
