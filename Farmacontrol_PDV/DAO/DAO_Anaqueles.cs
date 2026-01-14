using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using System.Data;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Anaqueles
	{
		Conector conector = new Conector();

		public List<DTO_Detallado_anaquel> registrar_articulo_anaquel(long anaquel_id, long articulo_id)
		{
			string sql = @"
				INSERT INTO
					farmacontrol_local.anaqueles_articulos
				SET
					anaquel_id = @anaquel_id,
					articulo_id = @articulo_id
				ON DUPLICATE KEY UPDATE
					anaquel_id = @anaquel_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("articulo_id",articulo_id);
			parametros.Add("anaquel_id",anaquel_id);

			conector.Insert(sql,parametros);

			return get_detallado_anaqueles(anaquel_id);
		}

		public bool eliminar_articulo_detallado_anaquel(long articulo_id)
		{
			string sql = @"
				DELETE FROM
					farmacontrol_local.anaqueles_articulos
				WHERE
					articulo_id = @articulo_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("articulo_id",articulo_id);

			conector.Delete(sql,parametros);

			return (conector.filas_afectadas > 0);
		}

		public List<DTO_Detallado_anaquel> get_detallado_anaqueles(long anaquel_id)
		{
			List<DTO_Detallado_anaquel> detallado = new List<DTO_Detallado_anaquel>();

			string sql = @"
				SELECT
					articulos.articulo_id,
					articulos.nombre,
					(	
						SELECT
							abs(amecop) AS amecop
						FROM
							farmacontrol_global.articulos_amecops
						WHERE
							articulos_amecops.articulo_id = articulos.articulo_id
						ORDER BY articulos_amecops.amecop_principal DESC
						LIMIT 1
					) AS amecop
				FROM
					farmacontrol_local.anaqueles_articulos
				JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					anaquel_id = @anaquel_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("anaquel_id",anaquel_id);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{	
				foreach(DataRow row in conector.result_set.Rows)
				{
					DTO_Detallado_anaquel det_anaquel = new DTO_Detallado_anaquel();
					det_anaquel.articulo_id = Convert.ToInt64(row["articulo_id"]);
					det_anaquel.amecop = Convert.ToInt64(row["amecop"]);
					det_anaquel.nombre = row["nombre"].ToString();

					detallado.Add(det_anaquel);
				}
			}

			return detallado;
		}
		public bool actualizar_anaquel(long anaquel_id, string nombre)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.anaqueles
				SET
					nombre = @nombre
				WHERE
					anaquel_id = @anaquel_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("nombre",nombre);
			parametros.Add("anaquel_id",anaquel_id);

			conector.Update(sql,parametros);

			return (conector.filas_afectadas > 0);
		}

		public bool existe_anaquel(string nombre)
		{
			string sql = @"
				SELECT
					anaquel_id
				FROM
					farmacontrol_local.anaqueles
				WHERE
					nombre = @nombre
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("nombre",nombre);

			conector.Select(sql,parametros);

			return (conector.result_set.Rows.Count > 0);
		}
		
		public void eliminar_detallado_anaquel(long anaquel_id)
		{
			string sql = @"
				DELETE FROM
					farmacontrol_local.anaqueles_articulos
				WHERE
					anaquel_id = @anaquel_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("anaquel_id", anaquel_id);

			conector.Delete(sql, parametros);
		}


		public List<DTO_Anaquel> eliminar_anaquel(long anaquel_id)
		{
			eliminar_detallado_anaquel(anaquel_id);

			string sql = @"
				DELETE FROM
					farmacontrol_local.anaqueles
				WHERE
					anaquel_id = @anaquel_id
			";


			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("anaquel_id",anaquel_id);

			conector.Delete(sql,parametros);

			return get_anaqueles();
		}
		

		public List<DTO_Anaquel> registrar_anaquel(string nombre, long posicion)
		{
			string sql = @"
				INSERT INTO
					farmacontrol_local.anaqueles
				SET
					nombre = @nombre,
					posicion = @posicion
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("nombre",nombre);
			parametros.Add("posicion",posicion);

			conector.Insert(sql,parametros);

			return get_anaqueles();
		}

		public List<DTO_Anaquel> get_anaqueles()
		{
			List<DTO_Anaquel> lista_anaqueles = new List<DTO_Anaquel>();

			string sql = @"
				SELECT
					anaquel_id,
					nombre,
					posicion,
					COALESCE(SUM(anaqueles_articulos.articulo_id), 0) AS numero_productos
				FROM
					farmacontrol_local.anaqueles
				LEFT JOIN farmacontrol_local.anaqueles_articulos USING(anaquel_id)
				GROUP BY anaqueles.anaquel_id
				ORDER BY anaqueles.nombre ASC
				
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				foreach(DataRow row in conector.result_set.Rows)
				{
					DTO_Anaquel anaquel = new DTO_Anaquel();
					anaquel.anaquel_id = Convert.ToInt64(row["anaquel_id"]);
					anaquel.nombre = row["nombre"].ToString();
					anaquel.posicion = Convert.ToInt64(row["posicion"]);
					anaquel.numero_productos = Convert.ToInt64(row["numero_productos"]);

					lista_anaqueles.Add(anaquel);
				}
			}


			return lista_anaqueles;
		}
	}
}
