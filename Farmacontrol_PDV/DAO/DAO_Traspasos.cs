using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.HELPERS;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Traspasos
	{
		Conector conector = new Conector();
		DAO_Articulos dao_articulos = new DAO_Articulos();
		public string informacion = "";

		public DTO_Validacion get_traspaso_recibido(long traspaso_id, int sucursal_id)
		{
			DTO_Validacion val = new DTO_Validacion();
			val.status = false;

			DAO_Sucursales dao_sucursales = new DAO_Sucursales();
			DTO_Sucursal dto_sucursal = dao_sucursales.get_sucursal_data(sucursal_id);

			if (Red_helper.checa_rest(dto_sucursal.ip_sucursal) == false)
			{
				val.status = false;
				val.informacion = String.Format("La sucursal {0} ({1}) no responde la petición, intente de nuevo más tarde.", dto_sucursal.nombre, dto_sucursal.ip_sucursal);
			}
			else
			{
				Rest_parameters parameters = new Rest_parameters();
				parameters.Add("traspaso_id", traspaso_id);
				Result_nonquery result = Rest_helper.make_request<Result_nonquery>("traspasos/get_traspaso_recibido", parameters, dto_sucursal.ip_sucursal);

				if(result.status)
				{
					val.status = false;
					val.informacion = "Este traspaso ya fue recibido, imposible cancelarlo";
				}
				else
				{
					val.status = true;
				}
			}

			return val;
		}

		public bool existe_traspaso_venta(string hash)
		{
			string sql = @"
				SELECT
					traspaso_id
				FROM
					farmacontrol_local.traspasos
				WHERE
					hash = @hash
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("hash",hash);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				return true;
			}

			return false;
		}

		public DataTable get_productos_para_venta(List<Tuple<int,string,string,int>> lista_detallado)
		{
			DataTable productos = new DataTable();
			productos.Columns.Add("articulo_id",typeof(int));
			productos.Columns.Add("amecop", typeof(string));
			productos.Columns.Add("nombre", typeof(string));
			productos.Columns.Add("caducidad",typeof(string));
			productos.Columns.Add("lote",typeof(string));
			productos.Columns.Add("precio_publico", typeof(decimal));
			productos.Columns.Add("pct_descuento", typeof(decimal));
			productos.Columns.Add("importe_descuento", typeof(decimal));
			productos.Columns.Add("importe", typeof(decimal));
			productos.Columns.Add("cantidad", typeof(int));
			productos.Columns.Add("subtotal", typeof(decimal));
			productos.Columns.Add("pct_iva", typeof(decimal));
			productos.Columns.Add("importe_iva", typeof(decimal));
			productos.Columns.Add("importe_ieps", typeof(decimal));
			productos.Columns.Add("total", typeof(decimal));

			foreach(var tuple in lista_detallado)
			{
				string sql = @"
					SELECT
						articulo_id,
						nombre,
						(
							SELECT								
								ABS(amecop) AS amecop
							FROM
								farmacontrol_global.articulos_amecops
							WHERE
								articulos_amecops.articulo_id = articulos.articulo_id
							ORDER BY articulos_amecops.amecop_principal DESC
							LIMIT 1
						) AS amecop,
						@caducidad AS caducidad,
						@lote AS lote,
						precio_publico,
						pct_descuento,
						FORMAT((precio_publico * pct_descuento), 4) AS importe_descuento,
						precio_publico - (precio_publico * pct_descuento) AS importe,
						
						@cantidad AS cantidad,
	
						(precio_publico - (precio_publico * pct_descuento)) * @cantidad AS subtotal,
						pct_iva,
						((precio_publico - (precio_publico * pct_descuento)) * @cantidad) * pct_iva AS importe_iva,
						tipo_ieps,
						ieps,
						IF(tipo_ieps = 'PCT', ((precio_publico - (precio_publico * pct_descuento)) * ieps) , ieps) AS importe_ieps,
						((precio_publico - (precio_publico * pct_descuento)) * @cantidad) + (((precio_publico - (precio_publico * pct_descuento)) * @cantidad) * pct_iva) AS total
					FROM
						articulos
					WHERE
						articulo_id = @articulo_id
					LIMIT 1
				";
				
				int articulo_id = tuple.Item1;
				string caducidad = tuple.Item2;
				string lote = tuple.Item3;
				int cantidad = tuple.Item4;

				Dictionary<string,object> parametros = new Dictionary<string,object>();
				parametros.Add("articulo_id",articulo_id);
				parametros.Add("caducidad",caducidad);
				parametros.Add("lote",lote);
				parametros.Add("cantidad",cantidad);

				conector.Select(sql,parametros);

				if(conector.result_set.Rows.Count > 0)
				{
					var row = conector.result_set.Rows[0];
					productos.Rows.Add(
						Convert.ToInt32(row["articulo_id"]),
						row["amecop"].ToString(),
						row["nombre"].ToString(),
                        Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD"),
						row["lote"].ToString(),
						Convert.ToDecimal(row["precio_publico"]),
						Convert.ToDecimal(row["pct_descuento"]),
						Convert.ToDecimal(row["importe_descuento"]),
						Convert.ToDecimal(row["importe"]),
						Convert.ToInt32(row["cantidad"]),
						Convert.ToDecimal(row["subtotal"]),
						Convert.ToDecimal(row["pct_iva"]),
						Convert.ToDecimal(row["importe_iva"]),
						Convert.ToDecimal(row["importe_ieps"]),
						Convert.ToDecimal(row["total"])
					);
				}
			}
			
			return productos;
		}

		public long crear_traspaso_venta(long empleado_id, int sucursal_id)
		{
			string sql = @"
				INSERT INTO
					farmacontrol_local.traspasos
				SET
					terminal_id = @terminal_id,
					sucursal_id = @sucursal_id,
					empleado_id = @empleado_id,
					tipo = @tipo,
					fecha_creado = NOW(),
					es_para_venta = 1
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("terminal_id",Convert.ToInt32(Misc_helper.get_terminal_id()));
			parametros.Add("sucursal_id",sucursal_id);
			parametros.Add("empleado_id",empleado_id);
			parametros.Add("tipo","ENVIAR");

			conector.Insert(sql,parametros);

			if(conector.insert_id > 0)
			{
				return Convert.ToInt64(conector.insert_id);
			}

			return 0;
		}

		public Tuple<DTO_Validacion,DTO_Traspaso> importar_traspaso_para_venta(string hash, bool registrar = false)
		{
			DTO_Validacion val = new DTO_Validacion();
			val.status = false;

			DAO_Sucursales dao_sucursales = new DAO_Sucursales();
			string[] split_hash = hash.Split('$');
			int sucursal_origen = Convert.ToInt32(split_hash[1]);

			DTO_Sucursal dto_sucursal = dao_sucursales.get_sucursal_data(sucursal_origen);
			DTO_Traspaso dto_traspaso = new DTO_Traspaso();

			if (Red_helper.checa_rest(dto_sucursal.ip_sucursal) == false)
			{
				val.status = false;
				val.informacion = String.Format("La sucursal {0} ({1}) no responde la petición, intente de nuevo más tarde.", dto_sucursal.nombre, dto_sucursal.ip_sucursal);
			}
			else
			{
				Rest_parameters parameters = new Rest_parameters();
				parameters.Add("hash", hash);
				parameters.Add("fecha_recibido",(registrar) ? 1 : 0);

				dto_traspaso = Rest_helper.make_request<DTO_Traspaso>("traspasos/importar_traspaso_para_venta", parameters, dto_sucursal.ip_sucursal);

				if (dto_traspaso.result)
				{
					if(registrar)
					{
						afectar_traspaso_origen(hash);
						long traspaso_local_id = insertar_traspaso(sucursal_origen, dto_traspaso);
						
						parameters = new Rest_parameters();
						parameters.Add("hash",hash);
						parameters.Add("remote_id",traspaso_local_id);

						Rest_helper.make_request<DTO_Traspaso>("traspasos/set_remote_id_hash", parameters, dto_sucursal.ip_sucursal);

						terminar_traspaso(traspaso_local_id,0,(int)FORMS.comunes.Principal.empleado_id,false,true);
						afectar_traspaso_local(traspaso_local_id);
					}

					val.status = true;
					val.informacion = "Traspaso importado correctamente";
				}
				else
				{
					val.status = false;
					val.informacion = dto_traspaso.informacion;
				}	
			}

			Tuple<DTO_Validacion,DTO_Traspaso> tuple = new Tuple<DTO_Validacion,DTO_Traspaso>(val,dto_traspaso);

			return tuple;
		}

		public DTO_Validacion importar_traspaso_complementario(string hash)
		{
			DTO_Validacion val = new DTO_Validacion();
			val.status = false;

			DAO_Sucursales dao_sucursales = new DAO_Sucursales();
			string [] split_hash = hash.Split('$');
			int sucursal_origen = Convert.ToInt32(split_hash[1]);

			DTO_Sucursal dto_sucursal = dao_sucursales.get_sucursal_data(sucursal_origen);

			DTO_Traspaso dto_traspaso = new DTO_Traspaso();

			string[] hash_array = hash.Split('$');
			string hash_reconstruido = "";

			if(hash_array.Length == 4)
			{
				string uuid = hash_array[3];
				hash_reconstruido = Misc_helper.uuid_guiones(uuid);
			}

			hash = string.Format("{0}${1}${2}${3}",hash_array[0],hash_array[1],hash_array[2],hash_reconstruido);

			Rest_parameters parameters = new Rest_parameters();
			parameters.Add("hash", hash);
			dto_traspaso = Rest_helper.make_request<DTO_Traspaso>("traspasos/importar_traspaso_complementario", parameters, dto_sucursal.ip_sucursal);

			if(dto_traspaso.result)
			{
				insertar_traspaso(sucursal_origen, dto_traspaso);

				val.status = true;
				val.informacion = "Traspaso complementario registrado correctamente";
			}
			else
			{
				val.informacion = dto_traspaso.informacion;
			}

			return val;
		}

		public long crear_traspaso_complemetario(long traspaso_padre_id)
		{
			DTO_Traspaso dto_traspaso = get_informacion_traspaso(traspaso_padre_id);

			long nuevo_traspaso = crear_trapaso(Convert.ToInt64(Principal.empleado_id),(int)dto_traspaso.sucursal_id,traspaso_padre_id);

			string sql = @"
				UPDATE
					farmacontrol_local.traspasos
				SET
					remote_id = @remote_id
				WHERE
					traspaso_id = @traspaso_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("remote_id",dto_traspaso.remote_id);
			parametros.Add("traspaso_id",nuevo_traspaso);

			conector.Update(sql,parametros);

			sql = @"
				INSERT INTO
					farmacontrol_local.detallado_traspasos
				(
					SELECT
						0 AS detallado_traspaso_id,
						@traspaso_id AS traspaso_id,
						articulo_id,
						caducidad,
						lote,
						precio_costo,
						NULL AS cantidad_origen,
						NULL AS cantidad_recibida,
						(cantidad_origen - cantidad) AS cantidad,
						NULL AS accion,
						( precio_costo * (cantidad_origen - cantidad) ) AS total,
						NOW() AS modified
					FROM
						farmacontrol_local.detallado_traspasos
					WHERE
						traspaso_id = @traspaso_padre_id
					AND
						accion = 'DEVOLVER_VIRTUAL'
				)
			";

			parametros = new Dictionary<string,object>();
			parametros.Add("traspaso_id",nuevo_traspaso);
			parametros.Add("traspaso_padre_id",traspaso_padre_id);

			conector.Insert(sql,parametros);

			return nuevo_traspaso;
		}

		public void igualar_traspaso_padre(long traspaso_padre_id)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.detallado_traspasos
				SET
					cantidad_recibida = cantidad
				WHERE
					traspaso_id = @traspaso_padre_id
				AND
					cantidad_origen < cantidad
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("traspaso_padre_id",traspaso_padre_id);

			conector.Update(sql,parametros);
		}

		public long crear_traspaso_complementario_enviar(long traspaso_padre_id, List<DTO_Traspaso_complementario> detallado)
		{
			DTO_Traspaso dto_traspaso = get_informacion_traspaso(traspaso_padre_id);

			long nuevo_traspaso = crear_trapaso(Convert.ToInt64(Principal.empleado_id), (int)dto_traspaso.sucursal_id, traspaso_padre_id);

			string sql = @"
				UPDATE
					farmacontrol_local.traspasos
				SET
					remote_id = @remote_id
				WHERE
					traspaso_id = @traspaso_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("remote_id", dto_traspaso.remote_id);
			parametros.Add("traspaso_id", nuevo_traspaso);

			conector.Update(sql, parametros);

			foreach(DTO_Traspaso_complementario det_com in detallado)
			{
				sql = @"
					INSERT INTO
						farmacontrol_local.detallado_traspasos
					(
						SELECT
							0 AS detallado_traspaso_id,
							@traspaso_id AS traspaso_id,
							articulos.articulo_id,
							@caducidad AS caducidad,
							@lote AS lote,
							precio_costo,
							NULL AS cantidad_origen,
							NULL AS cantidad_recibida,
							@cantidad AS cantidad,
							NULL AS accion,
							( precio_costo * @cantidad ) AS total,
							NOW() AS modified
						FROM
							farmacontrol_global.articulos_amecops
						JOIN farmacontrol_global.articulos USING(articulo_id)
						WHERE
							articulos_amecops.amecop = @amecop
					)
				";

				parametros = new Dictionary<string, object>();
				parametros.Add("amecop",det_com.amecop);
				parametros.Add("caducidad",det_com.caducidad);
				parametros.Add("lote",det_com.lote);
				parametros.Add("cantidad",(det_com.cantidad - det_com.cantidad_origen));
				parametros.Add("traspaso_id", nuevo_traspaso);

				conector.Insert(sql, parametros);
			}

			return nuevo_traspaso;
		}

		public DataTable get_traspaso_complementario_enviar(long traspaso_id, long sucursal_destino)
		{
			Rest_parameters parametros = new Rest_parameters();
			parametros.Add("remote_id",traspaso_id);
			parametros.Add("sucursal_id",Convert.ToInt32(Config_helper.get_config_local("sucursal_id")));

			DAO_Sucursales dao_sucursales = new DAO_Sucursales();
			var sucursal_data = dao_sucursales.get_sucursal_data((int)sucursal_destino);

			List<DTO_Traspaso_complementario> result =  Rest_helper.make_request<List<DTO_Traspaso_complementario>>("traspasos/get_traspaso_complementario_enviar",parametros,sucursal_data.ip_sucursal);
			return result.ToDataTable();
		}

		public DataTable get_traspaso_complementario(long traspaso_id)
		{
			string sql = @"
				SELECT
					detallado_traspaso_id,
					(
						SELECT
							ABS(amecop) AS amecop
						FROM
							farmacontrol_global.articulos_amecops
						WHERE
							articulos_amecops.articulo_id = detallado_traspasos.articulo_id
						ORDER BY articulos_amecops.amecop_principal DESC
						LIMIT 1
					) AS amecop,
					nombre AS producto,
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					COALESCE(cantidad_origen,0) AS cantidad_origen,
					cantidad,
					IF(COALESCE(cantidad_origen,0) > cantidad,'FALTA FISICO','FISICO DE MAS') AS problema,
					IF(COALESCE(cantidad_origen,0) > cantidad,'DEVOLVER VIRTUAL','PEDIR VIRTUAL') AS solucion
				FROM
					farmacontrol_local.detallado_traspasos
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					traspaso_id = @traspaso_id
				AND
					COALESCE(cantidad_origen,0) > cantidad
				ORDER BY
					detallado_traspaso_id
				ASC
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("traspaso_id",traspaso_id);

			conector.Select(sql,parametros);
			
			return conector.result_set;
		}

		public void afectar_traspaso_local(long traspaso_id)
		{
            //cantidad_origen AS cantidad

			string sql = @"
				SELECT
					articulo_id,
					CAST(DATE_FORMAT(caducidad,'%Y-%m-%d') AS CHAR(50)) AS caducidad,
					COALESCE(lote, ' ') AS lote,
					IF(cantidad_origen = 0,cantidad_recibida,cantidad_origen ) AS cantidad
				FROM
					farmacontrol_local.detallado_traspasos
				WHERE
					traspaso_id = @traspaso_id
				GROUP BY
					articulo_id,caducidad,lote
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("traspaso_id",traspaso_id);

			conector.Select(sql,parametros);

			var detallado = conector.result_set;

			List<Tuple<int,string,string,int>> lista_productos = new List<Tuple<int,string,string,int>>();

			foreach(DataRow row in detallado.Rows)
			{
				int articulo_id = Convert.ToInt32(row["articulo_id"]);
				string caducidad = row["caducidad"].ToString();
				string lote = row["lote"].ToString();
				int cantidad = Convert.ToInt32(row["cantidad"]);

				Tuple<int,string,string,int> tupla = new Tuple<int,string,string,int>(articulo_id,caducidad,lote,cantidad);
				lista_productos.Add(tupla);
			}

            dao_articulos.afectar_existencias_entrada(lista_productos, "TRASPASO", traspaso_id, traspaso_id);
		}

        //METODO PARA OBTENER LOS PRODUCTOS RECIBIDOS CON INSIDENCIAS
        public void afectar_traspaso_local_conflicto(long traspaso_id)
        {
            string sql = @"
			    SELECT					articulo_id,					CAST(DATE_FORMAT(caducidad,'%Y-%m-%d') AS CHAR(50)) AS caducidad,					COALESCE(lote, ' ') AS lote,					cantidad_recibida AS cantidad				FROM					farmacontrol_local.detallado_traspasos				WHERE					traspaso_id = @traspaso_id				GROUP BY					articulo_id,caducidad,lote
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("traspaso_id", traspaso_id);

            conector.Select(sql, parametros);

            var detallado = conector.result_set;

            List<Tuple<int, string, string, int>> lista_productos = new List<Tuple<int, string, string, int>>();

            foreach (DataRow row in detallado.Rows)
            {
                int articulo_id = Convert.ToInt32(row["articulo_id"]);
                string caducidad = row["caducidad"].ToString();
                string lote = row["lote"].ToString();
                int cantidad = Convert.ToInt32(row["cantidad"]);

                Tuple<int, string, string, int> tupla = new Tuple<int, string, string, int>(articulo_id, caducidad, lote, cantidad);
                lista_productos.Add(tupla);
            }

            dao_articulos.afectar_existencias_entrada(lista_productos, "TRASPASO", traspaso_id, traspaso_id);
        
        }


		public DTO_Validacion afectar_traspaso_origen(string hash)
		{
			string[] split_hash = hash.Split('$');
			string ip_sucursal = Sucursales_helper.get_ip_sucursal(Convert.ToInt32(split_hash[1]));

			DTO_Validacion val = new DTO_Validacion();

			Rest_parameters parameters = new Rest_parameters();
			parameters.Add("hash", hash);
			Result_nonquery result = Rest_helper.make_request<Result_nonquery>("traspasos/afectar_traspaso_complementario", parameters, ip_sucursal);

			if (result.status)
			{
				val.status = result.status;
			}
			else
			{
				val.status = result.status;
				val.informacion = result.error_information;
			}

			return val;
		}

		public DTO_Validacion afectar_traspaso_origen(long traspaso_origen, int sucursal_id, long remote_id)
		{
			string ip_sucursal = Sucursales_helper.get_ip_sucursal(sucursal_id);
			DTO_Validacion val = new DTO_Validacion();

			Rest_parameters parameters = new Rest_parameters();
			parameters.Add("traspaso_id", traspaso_origen);
			parameters.Add("remote_id",remote_id);

			Result_nonquery result = Rest_helper.make_request<Result_nonquery>("traspasos/afectar_traspaso", parameters,ip_sucursal);
			if(result.status)
			{
				val.status = result.status;
			}
			else
			{
				val.status = result.status;
				val.informacion = result.error_information;
			}

			return val;
		}

        //NUEVOS METODOS
        public DTO_Validacion afectar_traspaso_origen_sucursal(long traspaso_origen, int sucursal_id, long remote_id)
        {
            string ip_sucursal = Sucursales_helper.get_ip_sucursal(sucursal_id);
            DTO_Validacion val = new DTO_Validacion();

           /* Rest_parameters parameters = new Rest_parameters();
            parameters.Add("traspaso_id", traspaso_origen);
            parameters.Add("remote_id", remote_id);
            parameters.Add("sucursal_ip", ip_sucursal);*/

           // var result = Rest_helper.make_request_local(traspaso_origen, remote_id, ip_sucursal);

           /*
            if (result.status)
            {
                val.status = result.status;
            }
            else
            {
                val.status = result.status;
                val.informacion = result.error_information;
            }
            */
            val.status = true;
            val.informacion = "CORRECTO";
            return val;
            
        }


		public int set_resolucion_conflictos(Dictionary<long,string> lista_acciones)
		{
			int count_solucionados = 0;
			string sql = "";

			foreach(var item in lista_acciones)
			{
				sql = @"
					UPDATE
						farmacontrol_local.detallado_traspasos
					SET
						accion = @accion
					WHERE
						detallado_traspaso_id = @detallado_traspaso_id
				";

				Dictionary<string,object> parametros = new Dictionary<string,object>();
				string [] accion = item.Value.ToString().Split(' ');

				parametros.Add("accion",(accion[0]+"_"+accion[1]));
				parametros.Add("detallado_traspaso_id", item.Key);

				conector.Update(sql,parametros);

				count_solucionados += conector.filas_afectadas;
			}

			return count_solucionados;
		}

		public DataTable get_productos_conflictos(long traspaso_id)
		{
			string sql = @"
				SELECT
					detallado_traspaso_id,
					(
						SELECT
							ABS(amecop) AS amecop
						FROM
							farmacontrol_global.articulos_amecops
						WHERE
							articulos_amecops.articulo_id = detallado_traspasos.articulo_id
						ORDER BY articulos_amecops.amecop_principal DESC
						LIMIT 1
					) AS amecop,
					nombre AS producto,
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					COALESCE(cantidad_origen,0) AS cantidad_origen,
					cantidad,
					IF(COALESCE(cantidad_origen,0) > COALESCE(cantidad,0),'FALTA FISICO','FISICO DE MAS') AS problema,
					IF(COALESCE(cantidad_origen,0) > COALESCE(cantidad,0),'DEVOLVER VIRTUAL','PEDIR VIRTUAL') AS solucion
				FROM
					farmacontrol_local.detallado_traspasos
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					traspaso_id = @traspaso_id
				AND
					COALESCE(cantidad_origen,0) != cantidad
				ORDER BY
					detallado_traspaso_id
				ASC
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("traspaso_id",traspaso_id);

			conector.Select(sql,parametros);

			var result = conector.result_set;

			foreach(DataRow row in result.Rows)
			{
                row["caducidad"] = Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");
			}

			return conector.result_set;
		}

		public DataTable get_lotes(int articulo_id, string caducidad, long traspaso_id)
		{
			DataTable result = new DataTable();

			try
			{
				string sql = @"
					SELECT
						lote
					FROM
						farmacontrol_local.detallado_traspasos
					WHERE
						articulo_id = @articulo_id
					AND
						caducidad = @caducidad
					AND
						traspaso_id = @traspaso_id
				";

				Dictionary<string, object> parametros = new Dictionary<string, object>();
				parametros.Add("articulo_id", articulo_id);
				parametros.Add("caducidad", caducidad);
				parametros.Add("traspaso_id", traspaso_id);

				conector.Select(sql, parametros);

				result = conector.result_set;
			}
			catch (Exception excepcion)
			{
				Log_error.log(excepcion);
			}

			return result;
		}

		public DataTable get_caducidades(string amecop, long traspaso_id)
		{
			DataTable result = new DataTable();
            //DATE_FORMAT(caducidad,'%Y-%m-%d %H:%i:%s') AS caducidad,
			try
			{
				string sql = @"
					SELECT
					    caducidad,
						detallado_traspasos.articulo_id,
                        COALESCE((
                            SELECT
                                    SUM(existencia) as existencia
                            FROM 
                                   farmacontrol_local.existencias
                            WHERE 
                                   articulo_id = articulos_amecops.articulo_id 
                            AND 
                                    farmacontrol_local.existencias.caducidad = detallado_traspasos.caducidad  
                            GROUP BY 
                                    farmacontrol_local.existencias.articulo_id
                        ),0) as existencia
					FROM
						farmacontrol_local.detallado_traspasos
					LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
					JOIN farmacontrol_global.articulos_amecops USING(articulo_id)
					WHERE
						articulos_amecops.amecop = @amecop
					AND
						detallado_traspasos.traspaso_id = @traspaso_id
					AND
						cantidad_origen IS NOT NULL
					AND
						caducidad != '0000-00-00'
                    GROUP BY
                        detallado_traspasos.articulo_id,caducidad
				";

				Dictionary<string, object> parametros = new Dictionary<string, object>();
				parametros.Add("amecop", amecop);
				parametros.Add("traspaso_id", traspaso_id);

				conector.Select(sql, parametros);

				result = conector.result_set;
			}
			catch (Exception excepcion)
			{
				Log_error.log(excepcion);
			}

			return result;
		}

		public List<DTO_Detallado_traspaso_ticket> get_productos_diferencia(long traspaso_id)
		{
			List<DTO_Detallado_traspaso_ticket> detallado_ticket = get_detallado_traspaso_ticket(traspaso_id);
			
			List<DTO_Detallado_traspaso_ticket> lista_eliminacion_producto = new List<DTO_Detallado_traspaso_ticket>();

			foreach(DTO_Detallado_traspaso_ticket row in detallado_ticket)
			{
				var cad_lotes = row.caducidades_lotes;

				List<Tuple<string,string,int,int,int>> lista_eliminaciones = new List<Tuple<string,string,int,int,int>>();

				foreach(var tupla in cad_lotes)
				{
					if(tupla.Item3 == tupla.Item4)
					{
						lista_eliminaciones.Add(tupla);
					}
				}

				foreach(var elimanacion in lista_eliminaciones)
				{
					cad_lotes.Remove(elimanacion);
				}

				if(cad_lotes.Count == 0)
				{
					lista_eliminacion_producto.Add(row);
				}
			}

			foreach(var eliminacion in lista_eliminacion_producto)
			{
				detallado_ticket.Remove(eliminacion);
			}

			return detallado_ticket;
		}

		public DataTable eliminar_detallado_traspaso(long traspaso_id, long detallado_traspaso_id)
		{
			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("detallado_traspaso_id", detallado_traspaso_id);

			string sql = @"
				SELECT
					cantidad_origen
				FROM
					farmacontrol_local.detallado_traspasos
				WHERE
					detallado_traspaso_id = @detallado_traspaso_id
			";

			conector.Select(sql,parametros);

			if(conector.result_set.Rows[0]["cantidad_origen"].ToString() == "0")
			{
				sql = @"
					DELETE FROM
						farmacontrol_local.detallado_traspasos
					WHERE
						detallado_traspaso_id = @detallado_traspaso_id
				";

				conector.Delete(sql, parametros);	
			}
			else
			{
				sql = @"
					UPDATE
						farmacontrol_local.detallado_traspasos
					SET
						cantidad = 0
					WHERE
						detallado_traspaso_id = @detallado_traspaso_id
				";

				conector.Update(sql, parametros);
			}

			return get_detallado_traspaso(traspaso_id);
		}

		public DTO_Validacion asociar_terminal(long traspaso_id, int empleado_id)
		{
			DTO_Validacion validacion = new DTO_Validacion();
			validacion.status = false;
			validacion.informacion = "";

			string sql = @"
				SELECT
					terminal_id
				FROM
					farmacontrol_local.traspasos
				WHERE
					traspaso_id = @traspaso_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("traspaso_id",traspaso_id);

			conector.Select(sql,parametros);

			var result = conector.result_set;

			int? nullable = null;

			int? terminal_id = (result.Rows[0]["terminal_id"].ToString().Equals("")) ? nullable : Convert.ToInt32(result.Rows[0]["terminal_id"]);

			if(terminal_id == null)
			{
				sql = @"
					UPDATE
						farmacontrol_local.traspasos
					SET
						terminal_id = @terminal_id,
						empleado_id = @empleado_id
					WHERE
						traspaso_id = @traspaso_id
				";

				parametros.Add("terminal_id",Misc_helper.get_terminal_id());
				parametros.Add("empleado_id",empleado_id);

				conector.Update(sql,parametros);

				if(conector.filas_afectadas > 0)
				{
					validacion.status = true;
					validacion.informacion = "Traspaso asociado a esta terminal correctamente";
				}
				else
				{
					validacion.informacion = "Ocurrio un error al intentar asociar la terminal, notifique a su administrador";
				}
			}
			else
			{
				validacion.informacion = "Este traspaso ya cuenta con una terminal";
			}

			return validacion;
		}

		public DTO_Validacion desasociar_terminal(long traspaso_id, int empleado_id)
		{
			DTO_Validacion validacion = new DTO_Validacion();
			validacion.status = false;

			string sql = @"
				SELECT
					traspasos.empleado_id,
					empleados.nombre
				FROM
					farmacontrol_local.traspasos
				LEFT JOIN farmacontrol_global.empleados USING(empleado_id)
				WHERE
					traspaso_id = @traspaso_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("traspaso_id",traspaso_id);

			conector.Select(sql,parametros);

			var result = conector.result_set;

			int empleado_id_creacion = Convert.ToInt32(result.Rows[0]["empleado_id"]);
			string nombre_empleado = result.Rows[0]["nombre"].ToString();

			if(empleado_id_creacion == empleado_id)
			{	
				sql = @"
					UPDATE
						farmacontrol_local.traspasos
					SET
						terminal_id = NULL
					WHERE
						traspaso_id = @traspaso_id
				";				

				conector.Update(sql,parametros);

				if(conector.filas_afectadas > 0)
				{
					validacion.status = true;
					validacion.informacion = "Terminal desasociada coreectamente";
				}
				else
				{
					validacion.informacion = "Ocurrio un error al intentar desasociar esta terminal, notifique a su administrador";
				}
			}
			else
			{
				validacion.informacion = string.Format("Solo {0} puede desasociar la terminal de este traspaso",nombre_empleado);
			}

			return validacion;
		}

		public int cancelar_traspaso(long traspaso_id, long empleado_id)
		{
			string sql = @"
				SELECT
					IF(fecha_recibido IS NULL, 1, 0) AS puede_cancelar
				FROM
					farmacontrol_local.traspasos
				WHERE
					traspaso_id = @traspaso_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("traspaso_id",traspaso_id);

			conector.Select(sql,parametros);

			if(Convert.ToBoolean(conector.result_set.Rows[0]["puede_cancelar"]))
			{
				sql = @"
					UPDATE
						farmacontrol_local.traspasos
					SET
						fecha_cancelado = NOW(),
						cancela_empleado_id = @empleado_id
					WHERE
						traspaso_id = @traspaso_id
				";

				parametros.Add("empleado_id",empleado_id);

				conector.Update(sql,parametros);

				return conector.filas_afectadas;
			}

			return 0;
		}

		public List<Tuple<string, string, int, int, int>> get_detallado_caducidades(long traspaso_id, int articulo_id)
		{
			string sql = @"
				SELECT
					CONVERT(caducidad USING utf8) AS caducidad,
					lote,
					COALESCE(cantidad_origen,0) AS cantidad_origen,
					COALESCE(cantidad_recibida,0) AS cantidad_recibida,
					COALESCE(cantidad,0) AS cantidad
				FROM
					farmacontrol_local.detallado_traspasos
				WHERE
					traspaso_id = @traspaso_id
				AND
					articulo_id = @articulo_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("traspaso_id", traspaso_id);
			parametros.Add("articulo_id", articulo_id);

			conector.Select(sql, parametros);

			List<Tuple<string, string, int, int, int>> lista_caducidades = new List<Tuple<string, string, int, int, int>>();

			foreach (DataRow row in conector.result_set.Rows)
			{
				Tuple<string, string, int, int, int> tupla = new Tuple<string, string, int, int, int>(row["caducidad"].ToString(), row["lote"].ToString(), Convert.ToInt32(row["cantidad"]), Convert.ToInt32(row["cantidad_origen"]), Convert.ToInt32(row["cantidad_recibida"]));
				lista_caducidades.Add(tupla);
			}

			return lista_caducidades;
		}

		public List<DTO_Detallado_traspaso_ticket> get_detallado_traspaso_ticket(long traspaso_id)
		{
            //RPAD(CONCAT('*',SUBSTRING(amecop, LENGTH(amecop)-3) ),5,' ') AS amecop
			string sql = @"
				SELECT
					articulos.articulo_id,
					(	
						SELECT
							amecop
						FROM
							farmacontrol_global.articulos_amecops
						WHERE
							articulos_amecops.articulo_id = detallado_traspasos.articulo_id
						ORDER BY articulos_amecops.amecop_principal
						LIMIT 1
					) AS amecop,
					RPAD(nombre, 37, ' ') AS nombre,
					detallado_traspasos.precio_costo,
					SUM(detallado_traspasos.cantidad) AS cantidad,
					SUM(detallado_traspasos.total) AS total
				FROM
					farmacontrol_local.detallado_traspasos
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					traspaso_id = @traspaso_id
				GROUP BY
					articulo_id
				ORDER BY nombre ASC
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("traspaso_id", traspaso_id);

			conector.Select(sql, parametros);

			var result_set = conector.result_set;

			List<DTO_Detallado_traspaso_ticket> lista_detallado_traspaso = new List<DTO_Detallado_traspaso_ticket>();

			foreach (DataRow row in result_set.Rows)
			{
				DTO_Detallado_traspaso_ticket detallado_ticket = new DTO_Detallado_traspaso_ticket();
				detallado_ticket.articulo_id = Convert.ToInt32(row["articulo_id"]);

                string var_temp_amecop = row["amecop"].ToString();
                int tam_var = var_temp_amecop.Length;
                String Var_Sub = "*" + var_temp_amecop.Substring((tam_var - 3), 3);
                string amecop_temp = Var_Sub.PadRight(5, ' ');
                detallado_ticket.amecop = amecop_temp;
				//detallado_ticket.amecop = row["amecop"].ToString();
				detallado_ticket.nombre = row["nombre"].ToString();
				detallado_ticket.precio_costo = Convert.ToDecimal(row["precio_costo"]);
				detallado_ticket.total = Convert.ToDecimal(row["total"]);
				detallado_ticket.caducidades_lotes = get_detallado_caducidades(traspaso_id, detallado_ticket.articulo_id);
				lista_detallado_traspaso.Add(detallado_ticket);
			}

			return lista_detallado_traspaso;
		}

		public DTO_Traspaso_ticket get_traspaso_ticket(long traspaso_id)
		{
			string sql = @"
				SELECT
					traspaso_id,
					traspaso_padre_id,
					terminal_id,
					sucursal_id,
					empleado_id,
					termina_empleado_id,
					cancela_empleado_id,
					remote_id,
					conciliacion_impresion_id,
					tipo,
					es_para_venta,
					fecha_creado,
					fecha_recibido,
					fecha_iniciado,
					fecha_terminado,
					fecha_etiquetado,
					fecha_terminado_destino,
					fecha_cancelado,
					numero_bultos,
					motivo_cancelacion,
					comentarios,
					hash,
					IF(terminal_id IS NULL,'TRASPASO GENERADO POR EL SISTEMA',(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = traspasos.empleado_id) ) AS nombre_empleado_captura,
					IF(terminal_id IS NULL,'TRASPASO GENERADO POR EL SISTEMA',(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = traspasos.termina_empleado_id) ) AS nombre_empleado_termina
				FROM
					farmacontrol_local.traspasos
				WHERE
					traspaso_id = @traspaso_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("traspaso_id", traspaso_id);
			conector.Select(sql, parametros);

			DTO_Traspaso_ticket traspaso_ticket = new DTO_Traspaso_ticket();

			foreach (DataRow traspaso_row in conector.result_set.Rows)
			{
				int? nullable = null;
				 
				traspaso_ticket.traspaso_id = Convert.ToInt32(traspaso_row["traspaso_id"]);
				traspaso_ticket.traspado_padre_id = (traspaso_row["traspaso_padre_id"].ToString() != "") ? Convert.ToInt32(traspaso_row["traspaso_padre_id"]) : nullable;
				traspaso_ticket.terminal_id = (traspaso_row["terminal_id"].ToString() != "") ? Convert.ToInt32(traspaso_row["terminal_id"]) : nullable;
				traspaso_ticket.sucursal_id = Convert.ToInt32(traspaso_row["sucursal_id"]);
				traspaso_ticket.empleado_id = (traspaso_row["empleado_id"].ToString() != "") ? Convert.ToInt32(traspaso_row["empleado_id"]) : nullable;
				traspaso_ticket.termina_empleado_id = (traspaso_row["termina_empleado_id"].ToString() != "") ? Convert.ToInt32(traspaso_row["termina_empleado_id"]) : nullable;
				traspaso_ticket.cancela_empleado_id = (traspaso_row["cancela_empleado_id"].ToString() != "") ? Convert.ToInt32(traspaso_row["cancela_empleado_id"]) : nullable;
				traspaso_ticket.remote_id = (traspaso_row["remote_id"].ToString() != "") ? Convert.ToInt32(traspaso_row["remote_id"]) : nullable;
				traspaso_ticket.conciliacion_impresion_id = (traspaso_row["conciliacion_impresion_id"].ToString() != "") ? Convert.ToInt32(traspaso_row["conciliacion_impresion_id"]) : nullable;
				traspaso_ticket.tipo = traspaso_row["tipo"].ToString();
				traspaso_ticket.es_para_venta = Convert.ToInt32(traspaso_row["es_para_venta"]);

				DateTime? date_nullable = null;

				traspaso_ticket.fecha_creado = (traspaso_row["fecha_creado"].ToString().Equals("")) ? date_nullable : Convert.ToDateTime(traspaso_row["fecha_creado"]);
				traspaso_ticket.fecha_recibido = (traspaso_row["fecha_recibido"].ToString().Equals("")) ? date_nullable : Convert.ToDateTime(traspaso_row["fecha_recibido"]);
				traspaso_ticket.fecha_iniciado = (traspaso_row["fecha_iniciado"].ToString().Equals("")) ? date_nullable : Convert.ToDateTime(traspaso_row["fecha_iniciado"]);
				traspaso_ticket.fecha_terminado = (traspaso_row["fecha_terminado"].ToString().Equals("")) ? date_nullable : Convert.ToDateTime(traspaso_row["fecha_terminado"]);
				traspaso_ticket.fecha_etiquetado = (traspaso_row["fecha_etiquetado"].ToString().Equals("")) ? date_nullable : Convert.ToDateTime(traspaso_row["fecha_etiquetado"]);
				traspaso_ticket.fecha_terminado_destino = (traspaso_row["fecha_terminado_destino"].ToString().Equals("") ? date_nullable : Convert.ToDateTime(traspaso_row["fecha_terminado_destino"]));
				traspaso_ticket.fecha_cancelado = (traspaso_row["fecha_cancelado"].ToString().Equals("")) ? date_nullable : Convert.ToDateTime(traspaso_row["fecha_cancelado"]);
				
				traspaso_ticket.numero_bultos = Convert.ToInt32(traspaso_row["numero_bultos"]);
				traspaso_ticket.motivo_cancelacion = traspaso_row["motivo_cancelacion"].ToString();
				traspaso_ticket.comentarios = traspaso_row["comentarios"].ToString();
				traspaso_ticket.hash = traspaso_row["hash"].ToString();
				traspaso_ticket.nombre_empleado_captura = traspaso_row["nombre_empleado_captura"].ToString();
				traspaso_ticket.nombre_empleado_termina = traspaso_row["nombre_empleado_termina"].ToString();
			}

			return traspaso_ticket;
		}

		public void afectar_traspasos_enviar_localmente(long traspaso_id)
		{
			string sql = @"
					UPDATE 
						farmacontrol_local.traspasos
					SET
						fecha_recibido = NOW(),
						fecha_terminado_destino = NOW(),
						remote_id = 0
					WHERE
						traspaso_id = @traspaso_id
			";

			Dictionary<string,object> parametros = new Dictionary<string, object>();
			parametros.Add("traspaso_id", traspaso_id);

			conector.Update(sql, parametros);

			sql = @"
				SELECT
					articulo_id,
					CAST(caducidad as CHAR(50)) AS caducidad,
					lote,
					SUM(cantidad) AS cantidad
				FROM
					farmacontrol_local.detallado_traspasos
				WHERE
					traspaso_id = @traspaso_id
				GROUP BY
					articulo_id,caducidad,lote
			";

			conector.Select(sql, parametros);

			var detallado_traspasos = conector.result_set;

			List<Tuple<int, string, string, int>> productos = new List<Tuple<int, string, string, int>>();

			foreach (DataRow row in detallado_traspasos.Rows)
			{
				int articulo_id = Convert.ToInt32(row["articulo_id"]);
				string caducidad = row["caducidad"].ToString();
				string lote = row["lote"].ToString();
				int cantidad = Convert.ToInt32(row["cantidad"]);

				Tuple<int, string, string, int> detallado = new Tuple<int, string, string, int>(articulo_id, caducidad, lote, cantidad);
				productos.Add(detallado);
			}

			DAO_Articulos dao_articulos = new DAO_Articulos();
			dao_articulos.afectar_existencias_salida(productos, "TRASPASO",traspaso_id, traspaso_id);
		}

		public int terminar_traspaso(long traspaso_id, long numero_bultos, int empleado_id,bool complemento = false, bool para_venta = false)
		{
			DTO_Traspaso dto_traspaso =  new DTO_Traspaso();
			dto_traspaso = get_informacion_traspaso(traspaso_id);

			string sql = @"
				UPDATE
					farmacontrol_local.traspasos
				SET
					fecha_terminado = NOW(),
					hash = @hash,
					termina_empleado_id = @termina_empleado_id,
					numero_bultos = @numero_bultos
				WHERE
					traspaso_id = @traspaso_id
			";

			string hash = (complemento || para_venta) ? string.Format("{0}${1}${2}${3}", ((para_venta) ? "TV" : "TC"), Config_helper.get_config_local("sucursal_id").Trim(), dto_traspaso.sucursal_id, Misc_helper.uuid().ToUpper()) : string.Format("{0}${1}${2}", Config_helper.get_config_local("sucursal_id").Trim(), dto_traspaso.sucursal_id, dto_traspaso.traspaso_id);

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("hash",(dto_traspaso.hash.Equals("")) ? hash : dto_traspaso.hash);
			parametros.Add("traspaso_id",traspaso_id);
			parametros.Add("termina_empleado_id", empleado_id);
			parametros.Add("numero_bultos",numero_bultos);

			conector.Update(sql,parametros);

			int filas_afectadas = conector.filas_afectadas;

			int sucursal_destino = dto_traspaso.sucursal_id;

			DAO_Sucursales dao_sucursales = new DAO_Sucursales();

			var sucursal_data = dao_sucursales.get_sucursal_data(sucursal_destino);

            int sucursal_id_local = Convert.ToInt32(Config_helper.get_config_local("sucursal_id"));
            bool afecta_promocion = false;

            if (sucursal_destino == sucursal_id_local)
                afecta_promocion = true;
            else
                afecta_promocion = false;

			if(sucursal_data.es_farmacontrol == 0 || afecta_promocion == true)
			{
				afectar_traspasos_enviar_localmente(traspaso_id);
			}

			return filas_afectadas;
		}

     




		public int guardar_comentario(long traspaso_id, string comentario)
		{
			string sql = @"
				UPDATE
					farmacontrol_local.traspasos
				SET
					comentarios = @comentarios
				WHERE
					traspaso_id = @traspaso_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("comentarios",comentario);
			parametros.Add("traspaso_id",traspaso_id);

			conector.Update(sql,parametros);

			return conector.filas_afectadas;
		}

		public DataTable insertar_detallado(string amecop, string caducidad, string lote, int cantidad, long? traspaso_id, string tipo)
		{
			int cantidad_recibida = 0;

			if(tipo.Equals("RECIBIR"))
			{
				cantidad_recibida = cantidad;
			}

			string sql = @"
				INSERT INTO
					farmacontrol_local.detallado_traspasos
					(
						SELECT
							0 as detallado_traspaso_id,
							@traspaso_id AS traspaso_id,
							articulo_id,
							@caducidad AS caducidad,
							@lote AS lote,
							articulos.precio_costo,
							0 AS cantidad_origen,
							@cantidad_recibida AS cantidad_recibida,
							@cantidad AS cantidad,
							NULL AS accion,
							(articulos.precio_costo * @cantidad) AS total,
							NOW() AS modified
						FROM
							articulos
						LEFT JOIN farmacontrol_global.articulos_amecops USING(articulo_id)
						WHERE
							articulos_amecops.amecop = @amecop
					)
				ON DUPLICATE KEY UPDATE
					cantidad = COALESCE(cantidad,0) + @cantidad,
					total = total + (articulos.precio_costo * @cantidad),
					cantidad_recibida = COALESCE(cantidad_recibida,0) + @cantidad_recibida
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("traspaso_id", traspaso_id);
			parametros.Add("amecop", amecop);
			parametros.Add("caducidad", caducidad);
			parametros.Add("lote", lote);
			parametros.Add("cantidad", cantidad);
			parametros.Add("cantidad_recibida",cantidad_recibida);

			conector.Insert(sql, parametros);

			sql = @"
				UPDATE
					farmacontrol_local.traspasos
				SET
					fecha_iniciado = IF(fecha_iniciado IS NULL, NOW(), fecha_iniciado)
				WHERE
					traspaso_id = @traspaso_id
			";

			conector.Update(sql,parametros);

			return get_detallado_traspaso((long)traspaso_id);
		}

		public long crear_trapaso(long empleado_id, int sucursal_id, long? traspaso_padre_id = null, bool incluir_apartado = false)
		{
			int terminal_id =  (int)Misc_helper.get_terminal_id();

			string sql = string.Format(@"
				INSERT INTO
					farmacontrol_local.traspasos
				SET
					terminal_id = @terminal_id,
					sucursal_id = @sucursal_id,
					empleado_id = @empleado_id,
					tipo = @tipo,
					fecha_creado = NOW()
					{0}
			",(traspaso_padre_id == null) ? "" : ", traspaso_padre_id = "+traspaso_padre_id);

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("terminal_id",terminal_id);
			parametros.Add("sucursal_id",sucursal_id);
			parametros.Add("empleado_id",empleado_id);
			parametros.Add("tipo","ENVIAR");

			conector.Insert(sql,parametros);

			if(conector.insert_id > 0)
			{
                sql = @"
                    INSERT INTO
                        farmacontrol_local.detallado_traspasos
                    (
                        SELECT
                            0 as detallado_traspaso_id,
							@traspaso_id AS traspaso_id,
							articulo_id,
							apartados.caducidad AS caducidad,
							apartados.lote AS lote,
							articulos.precio_costo,
							0 AS cantidad_origen,
							0 AS cantidad_recibida,
							apartados.cantidad AS cantidad,
							NULL AS accion,
							(articulos.precio_costo * apartados.cantidad) AS total,
							NOW() AS modified
                        FROM
                            farmacontrol_local.apartados
                        JOIN farmacontrol_global.articulos USING(articulo_id)
                        WHERE
                            apartados.sucursal_id = @sucursal_id
                    )
                ";

                long traspaso_id = Convert.ToInt64(conector.insert_id);

                parametros = new Dictionary<string, object>();
                parametros.Add("traspaso_id",traspaso_id);
                parametros.Add("sucursal_id",sucursal_id);

                conector.Insert(sql, parametros);

                sql = @"
                    DELETE FROM
                        farmacontrol_local.apartados
                    WHERE
                        sucursal_id = @sucursal_id
                ";

                conector.Delete(sql, parametros);

				return traspaso_id;
			}

			return 0;
		}

		public DataTable get_detallado_traspaso(long traspaso_id)
		{
			string sql = @"
				SELECT
					detallado_traspaso_id,
					(
						SELECT
							ABS(amecop) AS amecop
						FROM
							farmacontrol_global.articulos_amecops
						WHERE
							articulos_amecops.articulo_id = detallado_traspasos.articulo_id
						ORDER BY articulos_amecops.amecop_principal DESC
						LIMIT 1
					) AS amecop,
					articulos.nombre AS producto,
					CAST(DATE_FORMAT(caducidad,'%Y-%m-%d') AS CHAR(50)) AS caducidad,
					lote,
					detallado_traspasos.precio_costo,
					COALESCE(cantidad_origen, 0) AS cantidad_origen,
					COALESCE(cantidad_recibida,0) AS cantidad_recibida,
					COALESCE(cantidad,0) AS cantidad,
					accion,
					total
				FROM
					farmacontrol_local.detallado_traspasos
				LEFT JOIN farmacontrol_global.articulos USING(articulo_id)
				WHERE
					traspaso_id = @traspaso_id
				GROUP BY
					detallado_traspaso_id
				ORDER BY
					detallado_traspaso_id
				ASC
			";


			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("traspaso_id",traspaso_id);

			conector.Select(sql,parametros);

			var result = conector.result_set;

			foreach(DataRow row in result.Rows)
			{
                row["caducidad"] = Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD");	
			}

			return result;
		}

		public long get_traspaso_siguiente(long traspaso_id)
		{
			string sql = @"
				SELECT
					traspaso_id
				FROM
					farmacontrol_local.traspasos
				WHERE
					traspaso_id > @traspaso_id
				ORDER BY
					traspaso_id
				ASC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("traspaso_id", traspaso_id);

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["traspaso_id"]);
			}

			return 0;
		}

		public long get_traspaso_atras(long traspaso_id)
		{
			string sql = @"
				SELECT
					traspaso_id
				FROM
					farmacontrol_local.traspasos
				WHERE
					traspaso_id < @traspaso_id
				ORDER BY
					traspaso_id
				DESC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("traspaso_id",traspaso_id);

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["traspaso_id"]);
			}

			return 0;
		}

		public long get_traspaso_inicio()
		{
			string sql = @"
				SELECT
					traspaso_id
				FROM
					farmacontrol_local.traspasos
				ORDER BY
					traspaso_id
				ASC
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["traspaso_id"]);
			}

			return 0;
		}

		public DTO_Traspaso get_informacion_traspaso(long traspaso_id)
		{
			DTO_Traspaso dto_traspaso = new DTO_Traspaso();
			string sql = @"
				SELECT
					traspaso_id,
					traspaso_padre_id,
					terminal_id,
					sucursal_id,
					empleado_id,
					termina_empleado_id,
					cancela_empleado_id,
					remote_id,
					conciliacion_impresion_id,
					tipo,
					es_para_venta,
					fecha_creado,
					fecha_recibido,
					fecha_iniciado,
					fecha_terminado,
					fecha_etiquetado,
					fecha_terminado_destino,
					fecha_cancelado,
					numero_bultos,
					motivo_cancelacion,
					comentarios,
					hash,
					IF(empleado_id IS NULL,'TRASPASO GENERADO POR EL SISTEMA',(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = traspasos.empleado_id) ) AS nombre_empleado_captura,
					IF(empleado_id IS NULL,'TRASPASO GENERADO POR EL SISTEMA',(SELECT nombre FROM farmacontrol_global.empleados WHERE empleado_id = traspasos.termina_empleado_id) ) AS nombre_empleado_termina
				FROM
					farmacontrol_local.traspasos
				WHERE
					traspaso_id = @traspaso_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("traspaso_id",traspaso_id);

			conector.Select(sql,parametros);

			var traspaso_data = conector.result_set;

			if(traspaso_data.Rows.Count > 0)
			{
				var traspaso_row  = traspaso_data.Rows[0];
				int? nullable = null;

				try
				{
					dto_traspaso.traspaso_id				= Convert.ToInt32(traspaso_row["traspaso_id"]);
					dto_traspaso.traspado_padre_id			= (traspaso_row["traspaso_padre_id"].ToString() != "") ? Convert.ToInt32(traspaso_row["traspaso_padre_id"]) : nullable;
					dto_traspaso.terminal_id				= (traspaso_row["terminal_id"].ToString() != "") ? Convert.ToInt32(traspaso_row["terminal_id"]) : nullable;
					dto_traspaso.sucursal_id				= Convert.ToInt32(traspaso_row["sucursal_id"]);
					dto_traspaso.empleado_id				= (traspaso_row["empleado_id"].ToString() != "") ? Convert.ToInt32(traspaso_row["empleado_id"]) : nullable;
					dto_traspaso.termina_empleado_id		= (traspaso_row["termina_empleado_id"].ToString() != "") ? Convert.ToInt32(traspaso_row["termina_empleado_id"]) : nullable;
					dto_traspaso.cancela_empleado_id		= (traspaso_row["cancela_empleado_id"].ToString() != "") ? Convert.ToInt32(traspaso_row["cancela_empleado_id"]) : nullable;
					dto_traspaso.remote_id					= (traspaso_row["remote_id"].ToString() != "") ? Convert.ToInt32(traspaso_row["remote_id"]) : nullable;
					dto_traspaso.conciliacion_impresion_id	= (traspaso_row["conciliacion_impresion_id"].ToString() != "") ? Convert.ToInt32(traspaso_row["conciliacion_impresion_id"]) : nullable;
					dto_traspaso.tipo						= traspaso_row["tipo"].ToString();
					dto_traspaso.es_para_venta				= Convert.ToInt32(traspaso_row["es_para_venta"]);

					DateTime? date_nullable = null;

					dto_traspaso.fecha_creado				= (traspaso_row["fecha_creado"].ToString().Equals(""))				? date_nullable : Convert.ToDateTime(traspaso_row["fecha_creado"]);
					dto_traspaso.fecha_recibido				= (traspaso_row["fecha_recibido"].ToString().Equals(""))			? date_nullable : Convert.ToDateTime(traspaso_row["fecha_recibido"]);
					dto_traspaso.fecha_iniciado				= (traspaso_row["fecha_iniciado"].ToString().Equals(""))			? date_nullable : Convert.ToDateTime(traspaso_row["fecha_iniciado"]);
					dto_traspaso.fecha_terminado			= (traspaso_row["fecha_terminado"].ToString().Equals(""))			? date_nullable : Convert.ToDateTime(traspaso_row["fecha_terminado"]);
					dto_traspaso.fecha_etiquetado			= (traspaso_row["fecha_etiquetado"].ToString().Equals(""))			? date_nullable : Convert.ToDateTime(traspaso_row["fecha_etiquetado"]);
					dto_traspaso.fecha_terminado_destino	= (traspaso_row["fecha_terminado_destino"].ToString().Equals("")	? date_nullable : Convert.ToDateTime(traspaso_row["fecha_terminado_destino"]));
					dto_traspaso.fecha_cancelado			= (traspaso_row["fecha_cancelado"].ToString().Equals(""))			? date_nullable : Convert.ToDateTime(traspaso_row["fecha_cancelado"]);
					
					dto_traspaso.numero_bultos				= Convert.ToInt32(traspaso_row["numero_bultos"]);
					dto_traspaso.motivo_cancelacion			= traspaso_row["motivo_cancelacion"].ToString();
					dto_traspaso.comentarios				= traspaso_row["comentarios"].ToString();
					dto_traspaso.hash						= traspaso_row["hash"].ToString();
					dto_traspaso.nombre_empleado_captura	= traspaso_row["nombre_empleado_captura"].ToString();
					dto_traspaso.nombre_empleado_termina	= traspaso_row["nombre_empleado_termina"].ToString();
				}
				catch(Exception ex)
				{
					Log_error.log(ex);
				}
			}

			return dto_traspaso;
		}

		public long get_traspaso_fin()
		{
			string sql= @"
				SELECT
					traspaso_id
				FROM
					farmacontrol_local.traspasos
				ORDER BY
					traspaso_id
				DESC
				LIMIT 1
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				return Convert.ToInt64(conector.result_set.Rows[0]["traspaso_id"]);
			}

			return 0;
		}

        //AND
        //fecha_terminado BETWEEN DATE_SUB(CURDATE(), INTERVAL 1 YEAR) AND CURDATE()

        public void validar_remote_id(int sucursal_id, int remote_id)
		{
			string sql = @"
				SELECT
					traspaso_id
				FROM
					farmacontrol_local.traspasos
				WHERE
					sucursal_id = @sucursal_id
				AND
					tipo = @tipo
				AND
					remote_id = @remote_id	
				
			";

		 	Dictionary<string, object> parametros = new Dictionary<string, object>();

			parametros.Add("sucursal_id", sucursal_id);
			parametros.Add("tipo", "RECIBIR");
			parametros.Add("remote_id", remote_id);

			conector.Select(sql, parametros);

			DataTable result = conector.result_set;

			if(result.Rows.Count > 0)
			{
				throw new Exception(String.Format("Ese traspaso ya ha sido importado con el folio {0}.", result.Rows[0]["traspaso_id"]));
			}
		}

		public long insertar_traspaso(int sucursal_origen, DTO_Traspaso traspaso_data)
		{
			Dictionary<string, object> parametros;
			long insert_id;
			string sql;

			sql = @"
				INSERT INTO
					farmacontrol_local.traspasos
				SET
					sucursal_id					= @sucursal_id,
					remote_id					= @remote_id,
					traspaso_padre_id			= @traspaso_padre_id,
					tipo						= @tipo,
					es_para_venta				= @es_para_venta,
					fecha_creado				= @fecha_creado,
					fecha_recibido				= NOW(),
					numero_bultos				= @numero_bultos,
					hash						= @hash
			";

			parametros = new Dictionary<string, object>();

			string [] hash_split = traspaso_data.hash.Split('$');
			int sucursal_origen_id = Convert.ToInt32((hash_split[0] == "TC" || hash_split[0] == "TV") ? hash_split[1] : hash_split[0]);
			parametros.Add("sucursal_id", sucursal_origen_id);
			parametros.Add("remote_id", traspaso_data.traspaso_id);
			parametros.Add("traspaso_padre_id", (hash_split[0] == "TC" || hash_split[0] == "TV") ? traspaso_data.remote_id : null);
			parametros.Add("tipo", "RECIBIR");
			parametros.Add("es_para_venta", traspaso_data.es_para_venta);
			parametros.Add("fecha_creado", traspaso_data.fecha_creado);
			parametros.Add("fecha_etiquetado", traspaso_data.fecha_etiquetado);
			parametros.Add("numero_bultos", traspaso_data.numero_bultos);
			parametros.Add("hash", traspaso_data.hash);

			insert_id = conector.Insert(sql, parametros);

			if (insert_id > 0)
			{
				foreach (DTO_Detallado_traspaso detallado_traspaso in traspaso_data.detallado_traspaso)
				{
					sql = @"
						INSERT INTO
							farmacontrol_local.detallado_traspasos
						SET
							traspaso_id			= @traspaso_id,
							articulo_id			= @articulo_id,
							caducidad			= @caducidad,
							lote				= @lote,
							precio_costo		= @precio_costo,
							cantidad_origen		= @cantidad_origen,
							cantidad			= @cantidad,
							cantidad_recibida	= @cantidad,
							total				= @total
					";

					parametros = new Dictionary<string,object>();

					parametros.Add("traspaso_id", insert_id);
					parametros.Add("articulo_id", detallado_traspaso.articulo_id);
					parametros.Add("caducidad", detallado_traspaso.caducidad);
					parametros.Add("lote", detallado_traspaso.lote);
					parametros.Add("precio_costo", detallado_traspaso.precio_costo);
					parametros.Add("cantidad_origen", detallado_traspaso.cantidad);
					parametros.Add("total", detallado_traspaso.total);
					parametros.Add("cantidad", (hash_split[0] == "TC" || hash_split[0] == "TV") ? detallado_traspaso.cantidad : 0);

					conector.Insert(sql, parametros);
				}

				Rest_parameters param = new Rest_parameters();
				param.Add("traspaso_id",traspaso_data.traspaso_id);
				param.Add("remote_id",insert_id);

				DAO_Sucursales dao_suc = new DAO_Sucursales();
				var sucursal_data = dao_suc.get_sucursal_data(sucursal_origen);
			}
			else
			{
				throw new Exception("No se pudo insertar el traspaso");
			}


			return insert_id;
		}


        public bool existe_articulos_venta(long traspaso_id,long venta_id)
        {
            bool valido = false;
            string sql = @"
				SELECT                  traspaso_id,                  articulo_id                FROM                   farmacontrol_local.detallado_traspasos                WHERE                   traspaso_id = @traspaso_id
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("traspaso_id", traspaso_id);

            conector.Select(sql, parametros);
          

            if (conector.result_set.Rows.Count > 0)
            {

                var result = conector.result_set;
                parametros = new Dictionary<string, object>();
                parametros.Add("venta_id", traspaso_id);

                foreach (DataRow row in result.Rows)
                {

                    sql = @"
				        SELECT                            articulo_id                        FROM                            farmacontrol_local.detallado_ventas                        WHERE 	                        venta_id = @venta_id                        AND                              articulo_id = @articulo_id
			       ";

                    parametros = new Dictionary<string, object>();
                    parametros.Add("venta_id", venta_id);
                    parametros.Add("articulo_id", row["articulo_id"].ToString());

                    conector.Select(sql, parametros);

                    if (conector.result_set.Rows.Count > 0)
                    {
                        valido = true;
                    }
                    else
                    {
                        return false;
                    }
                   
                }

            }
            else 
            {
                valido = false;
            }

           
            return valido;
        }


	}
}
