using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.FORMS.comunes;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Clientes
	{
		Conector conector = new Conector();

        public string get_cliente_id_by_telefono(long telefono)
        {
            string sql = @"
                SELECT
                    cliente_id
                FROM
                    farmacontrol_global.clientes_domicilios
                WHERE
                    clientes_domicilios.telefono = @telefono
                LIMIT 1
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("telefono",telefono);

            conector.Select(sql, parametros);

            return (conector.result_set.Rows.Count > 0) ? conector.result_set.Rows[0]["cliente_id"].ToString() : "";
        }

		public DTO_Cliente_domicilios get_domicilio_default(string cliente_id)
		{
			DTO_Cliente_domicilios domicilio = new DTO_Cliente_domicilios();

			string sql = @"
				SELECT
					cliente_domicilio_id,
					etiqueta AS tipo,
					CONCAT_WS(' ', clientes_domicilios.calle, clientes_domicilios.colonia, clientes_domicilios.municipio, clientes_domicilios.ciudad) AS direccion,
					IF(telefono = 0, 'N/A',telefono) AS telefono
				FROM
					farmacontrol_global.clientes_domicilios
				WHERE
					cliente_id = @cliente_id
				ORDER BY modified DESC
				LIMIT 1
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("cliente_id",cliente_id);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				DataRow row = conector.result_set.Rows[0];
                domicilio.cliente_domicilio_id = row["cliente_domicilio_id"].ToString();
				domicilio.direccion = row["direccion"].ToString();
				domicilio.tipo = row["tipo"].ToString();
				domicilio.telefono = row["telefono"].ToString();
			}

			return domicilio;
		}

		public DataTable get_clientes(string busqueda)
		{
			string sql = @"
				SELECT
					cliente_id AS cliente_id,
					nombre,
					comentarios
				FROM
					farmacontrol_global.clientes
				WHERE
					nombre LIKE @busqueda
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("busqueda", "%" + busqueda.Replace(" ","%") + "%");

			conector.Select(sql, parametros);

			return conector.result_set;
		}

		public DTO_Cliente_correo_informacion_direccion get_cliente_correo_informacion_direccion(string elemento_id, string tipo)
		{
			DTO_Cliente_correo_informacion_direccion informacion = new DTO_Cliente_correo_informacion_direccion();
			string sql;

			if(tipo.Equals("PERSONA"))
			{
				sql = @"
					SELECT
						clientes_domicilios.calle AS direccion,
						colonia,
						ciudad,
						estado,
						'MEXICO' AS pais
					FROM
						farmacontrol_global.clientes_domicilios
					WHERE
						clientes_domicilios.cliente_domicilio_id = @id
				";

			}
			else
			{
				sql = @"
					SELECT
						CONCAT_WS(calle,numero_exterior,numero_interior) AS direccion,
						colonia,
						ciudad,
						estado,
						'MEXICO' AS pais
					FROM
						farmacontrol_global.rfc_registros
					WHERE
						rfc_registro_id = @id
				";
			}

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("id",elemento_id);

			conector.Select(sql,parametros);

			var row = conector.result_set.Rows[0];

			informacion.direccion = row["direccion"].ToString();
			informacion.colonia = row["colonia"].ToString();
			informacion.ciudad = row["ciudad"].ToString();
			informacion.estado = row["estado"].ToString();

			return informacion;
		}

		public List<DTO_Cliente_correo> busqueda_clientes_correo(string busqueda)
		{
			List<DTO_Cliente_correo> lista_clientes_correo = new List<DTO_Cliente_correo>();

			string sql = @"
				SELECT
					*
				FROM
				(
				SELECT
					'PERSONA' AS tipo,
					cliente_id AS elemento_id,
					nombre
				FROM
					farmacontrol_global.clientes
				WHERE
					nombre LIKE @busqueda
	
				UNION
	
				SELECT
					'EMPRESA' AS tipo,
					rfc_registro_id AS elemento_id,
					CONCAT(razon_social, ' (', rfc, ')') AS nombre
				FROM
					farmacontrol_global.rfc_registros
				WHERE
					razon_social LIKE @busqueda
				OR
					rfc LIKE @busqueda
				) AS tmp
				ORDER BY nombre
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("busqueda","%"+busqueda.Replace(" ","%")+"%");

			conector.Select(sql,parametros);

			foreach(DataRow row in conector.result_set.Rows)
			{
				DTO_Cliente_correo cliente_correo = new DTO_Cliente_correo();
				cliente_correo.elemento_id = row["elemento_id"].ToString();
				cliente_correo.nombre = row["nombre"].ToString();
				cliente_correo.tipo = row["tipo"].ToString();

				lista_clientes_correo.Add(cliente_correo);
			}

			return lista_clientes_correo;
		}

		public string get_cliente_id_cliente_domicilio(string cliente_domicilio_id)
		{
			string sql = @"
				SELECT
					cliente_id AS cliente_id
				FROM
					farmacontrol_local.clientes_domiclios
				WHERE
					cliente_domicilio_id = @cliente_domicilio_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("cliente_domicilio_id",cliente_domicilio_id);

			conector.Select(sql,parametros);

			return conector.result_set.Rows[0]["cliente_id"].ToString();
		}

		public DTO_Cliente get_informacion_cliente(string cliente_id)
		{
			DTO_Cliente cliente = new DTO_Cliente();
			
			string sql = @"
				SELECT
					cliente_id AS cliente_id,
					empleado_id,
					carnet_id,
					nombre,
					credito_activado,
					bloqueo_morosidad,
					limite_credito,
					plazo,
					pct_descuento_adicional,
					ventas_mayoreo,
					comentarios
				FROM
					farmacontrol_global.clientes
				WHERE
					cliente_id = @cliente_id
			";


			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("cliente_id",cliente_id);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				var row = conector.result_set.Rows[0];

				cliente.cliente_id = row["cliente_id"].ToString();

				long? long_null = null;

				cliente.empleado_id = (row["empleado_id"].ToString().Equals("")) ? long_null : Convert.ToInt64(row["empleado_id"]);
				cliente.carnet_id = (row["carnet_id"].ToString().Equals("")) ? long_null : Convert.ToInt64(row["carnet_id"]);
				cliente.nombre = row["nombre"].ToString();

				cliente.credito_activado = Convert.ToInt32(row["credito_activado"]);
				cliente.limite_credito = Convert.ToDecimal(row["limite_credito"]);
				cliente.bloqueo_morosidad = Convert.ToBoolean(row["bloqueo_morosidad"]);
				cliente.plazo = Convert.ToInt32(row["plazo"]);
				cliente.pct_descuento_adicional = Convert.ToDecimal(row["pct_descuento_adicional"]);
				cliente.ventas_mayoreo = Convert.ToInt32(row["ventas_mayoreo"]);
				cliente.comentarios = row["comentarios"].ToString();
			}

			return cliente;
		}

		public DataTable get_clientes_mayoreo(string busqueda)
		{
			string sql = @"
				SELECT
					cliente_id AS cliente_id,
					nombre,
					comentarios
				FROM
					farmacontrol_global.clientes
				WHERE
					ventas_mayoreo IS TRUE
				AND
					nombre LIKE @busqueda
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("busqueda","%"+busqueda.Replace(" ","%")+"%");

			conector.Select(sql,parametros);

			return conector.result_set;
		}

		public List<DTO_Ventas_domicilio> get_ventas_domicilio(string cliente_id)
		{
			Rest_parameters parametros = new Rest_parameters();
			parametros.Add("cliente_id",cliente_id);
			var result = Rest_helper.make_request<List<DTO_Ventas_domicilio>>("clientes/get_ventas_domicilio",parametros);
			return result;
		}

		public DataTable get_ventas_facturadas(string cliente_id)
		{
			string sql = @"
				SELECT
					venta_id,
					DATE_FORMAT(ventas_facturadas.fecha_terminado,'%Y-%m-%d %H:%i:%s') AS fecha,
					ventas_facturadas.correo_electronico AS correo_electronico,
					ventas_facturadas.tipo_pago AS tipo_pago,
					ventas_facturadas.comentarios AS comentarios,
					UPPER(ventas_facturadas.uuid) AS uuid
				FROM
					farmacontrol_local.ventas_facturadas
				JOIN farmacontrol_local.detallado_ventas_facturadas USING(venta_facturada_id)
				WHERE
					venta_id IN 
					(
						SELECT
							DISTINCT(ventas.venta_id) AS venta_id
						FROM
							farmacontrol_local.ventas
						LEFT JOIN farmacontrol_global.clientes_creditos ON
							clientes_creditos.cliente_id = ventas.cliente_credito_id
						LEFT JOIN farmacontrol_global.clientes_domicilios USING(cliente_domicilio_id)
						WHERE
							ventas.cliente_credito_id = @cliente_id
						OR
							clientes_domicilios.cliente_id = @cliente_id
					)
				AND
					ventas_facturadas.fecha_terminado IS NOT NULL
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("cliente_id",cliente_id);

			conector.Select(sql,parametros);

			var result_set = conector.result_set;

			foreach (DataRow row in result_set.Rows)
			{
				try
				{
                    row["fecha"] = HELPERS.Misc_helper.fecha(row["caducidad"].ToString());
				}catch(Exception exception)
				{
					Log_error.log(exception);
				}
			}

			return result_set;
		}

		public List<DTO_Ventas_credito> get_ventas_credito(string cliente_id)
		{
			List<DTO_Ventas_credito> ventas_credito = new List<DTO_Ventas_credito>();

			string sql = @"
				SELECT
					clientes_creditos.cliente_credito_id AS cliente_credito_id,
					sucursales.nombre AS sucursal,
					clientes_creditos.sucursal_id AS sucursal_id,
					clientes_creditos.venta_id AS venta_id,
					CONCAT_WS('-',terminal,venta_folio) AS folio,
					clientes_creditos.fecha_venta AS fecha_compra,
					clientes_creditos.total AS importe_compra,
					( clientes_creditos.total - SUM(COALESCE(clientes_creditos_abonos.importe, 0)) ) AS por_pagar
				FROM
					farmacontrol_global.clientes_creditos
				JOIN farmacontrol_global.sucursales USING(sucursal_id)
				LEFT JOIN farmacontrol_global.clientes_creditos_abonos USING(cliente_credito_id)
				WHERE
					clientes_creditos.cliente_id = @cliente_id
				GROUP BY clientes_creditos.cliente_credito_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("cliente_id",cliente_id);

			conector.Select(sql,parametros);

			var result = conector.result_set;

			Dictionary<int,string> sucursales_ventas = new Dictionary<int,string>();

			if(result.Rows.Count > 0)
			{
				foreach (DataRow row in result.Rows)
				{
					DTO_Ventas_credito ven_cre = new DTO_Ventas_credito();
					ven_cre.cliente_credito_id = row["cliente_credito_id"].ToString();
					ven_cre.sucursal = row["sucursal"].ToString();
					ven_cre.sucursal_id = Convert.ToInt32(row["sucursal_id"]);
					ven_cre.venta_id = Convert.ToInt64(row["venta_id"]);
					ven_cre.folio = row["folio"].ToString();
					ven_cre.fecha_compra = Convert.ToDateTime(row["fecha_compra"]);
					ven_cre.importe_compra = Convert.ToDecimal(row["importe_compra"]);
					ven_cre.por_pagar = Convert.ToDecimal(row["por_pagar"]);

					ventas_credito.Add(ven_cre);
				}

				var sucursales = DAO_Sucursales.get_sucursales();

				foreach(DTO_Sucursal suc in sucursales)
				{
                    if(suc.es_farmacontrol == 1)
                    {
                        List<long> lista_ventas = new List<long>();

                        foreach (DataRow row in result.Rows)
                        {
                            if (Convert.ToInt32(row["sucursal_id"]) == suc.sucursal_id)
                            {
                                lista_ventas.Add(Convert.ToInt64(row["venta_id"]));
                            }

                        }

                        if (sucursales_ventas.ContainsKey(suc.sucursal_id) == false)
                        {
                            sucursales_ventas.Add(suc.sucursal_id, string.Join(",", lista_ventas.ToArray()));
                        }
                    }
				}

                if (false)//if(sucursales_ventas.Count > 0)
                {
                    Rest_parameters param = new Rest_parameters();
                    param.Add("sucursales_ventas", Rest_helper.serialize(sucursales_ventas));
                    var resalt_validacion = Rest_helper.make_request<List<DTO_Ventas_facturadas_sucursal>>("clientes/validar_ventas_facturadas", param);

                    foreach (DTO_Ventas_credito vc in ventas_credito)
                    {
                        foreach (DTO_Ventas_facturadas_sucursal vfs in resalt_validacion)
                        {
                            if (vfs.sucursal_id == vc.sucursal_id && vfs.venta_id == vc.venta_id)
                            {
                                vc.es_factura = vfs.es_factura;
                                break;
                            }
                        }
                    }
                }
			}

			return ventas_credito;
		}

		public DTO_Validacion registrar_domicilio(string cliente_id, Dictionary<string, string> datos_domicilio)
		{
			DTO_Validacion validacion = new DTO_Validacion();
			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("cliente_id", cliente_id);

			string uuid_cliente_domicilio = Misc_helper.uuid();

			string sql = @"
				INSERT INTO
					farmacontrol_global.clientes_domicilios
				SET
					cliente_domicilio_id = @cliente_domicilio_id,
					cliente_id = @cliente_id,
					etiqueta = @etiqueta,
					calle = @calle,
					numero_exterior = @numero_exterior,
					numero_interior = @numero_interior,
					colonia = @colonia,
					ciudad = @ciudad,
					municipio = @municipio,
					estado = @estado,
					codigo_postal = @codigo_postal,
					pais = @pais,
					telefono = @telefono,
					comentarios = @comentarios,
                    fecha_agregado = @fecha_agregado
				ON DUPLICATE KEY UPDATE
					cliente_domicilio_id = cliente_domicilio_id,
                    etiqueta = @etiqueta,
					calle = @calle,
					numero_exterior = @numero_exterior,
					numero_interior = @numero_interior,
					colonia = @colonia,
					ciudad = @ciudad,
					municipio = @municipio,
					estado = @estado,
					codigo_postal = @codigo_postal,
					pais = @pais,
					telefono = @telefono,
					comentarios = @comentarios,
                    fecha_agregado = @fecha_agregado
			";

			parametros.Add("cliente_domicilio_id", datos_domicilio.ContainsKey("cliente_domicilio_id") ? datos_domicilio["cliente_domicilio_id"] : uuid_cliente_domicilio );
			parametros.Add("etiqueta", datos_domicilio["tipo"]);
			parametros.Add("calle", datos_domicilio["calle"]);
			parametros.Add("numero_exterior", datos_domicilio["numero_exterior"]);
			parametros.Add("numero_interior", datos_domicilio["numero_interior"]);
			parametros.Add("colonia", datos_domicilio["colonia"]);
			parametros.Add("ciudad", datos_domicilio["ciudad"]);
			parametros.Add("municipio", datos_domicilio["municipio"]);
			parametros.Add("estado", datos_domicilio["estado"]);
			parametros.Add("codigo_postal", datos_domicilio["codigo_postal"]);
			parametros.Add("pais", datos_domicilio["pais"]);
			parametros.Add("telefono", (datos_domicilio["telefono"].ToString().Trim().Equals("")) ? 0 : Convert.ToInt64(datos_domicilio["telefono"]));
			parametros.Add("comentarios", datos_domicilio["comentarios"]);
            parametros.Add("fecha_agregado",Misc_helper.fecha());

			conector.Insert(sql, parametros);

			sql = @"
				SELECT
					nombre
				FROM
					farmacontrol_global.clientes_domicilios
				LEFT JOIN farmacontrol_global.clientes USING(cliente_id)
				WHERE
					cliente_domicilio_id = @cliente_domicilio_id
			 ";

			conector.Select(sql, parametros);

			DTO_Validacion result = new DTO_Validacion();

			if (conector.result_set.Rows.Count > 0)
			{
				result.status = true;
				DAO_Cola_operaciones.insertar_cola_operaciones(Convert.ToInt64(FORMS.comunes.Principal.empleado_id), "rest/clientes", "registrar_domicilio", parametros, "PARA REGISTRO AL SERVIDOR PRINCIPAL");
			}
			else
			{
				result.status = false;
				result.informacion = "No pudo ser registrado correctamente el domicilio del cliente";
			}

			return result;
		}

		public void actualizar_cliente(string cliente_id, string nombre, decimal limite_credito, bool creadito_activado, string comentarios, int? empleado_id)
		{
			string sql = @"
				UPDATE 
					clientes
				SET
					nombre = @nombre,
					comentarios = @comentarios,
                    crea_empleado_id = @empleado_id
				WHERE
					cliente_id = @cliente_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("cliente_id",cliente_id);
			parametros.Add("nombre",nombre);
            parametros.Add("limite_credito",limite_credito);
            parametros.Add("credito_activado",creadito_activado ? 1 : 0);
			parametros.Add("comentarios",comentarios);
            parametros.Add("empleado_id", empleado_id);

			conector.Update(sql,parametros);

            DAO_Cola_operaciones.insertar_cola_operaciones((long)Principal.empleado_id, "rest/clientes", "actualizar_cliente_pdv", parametros, "PARA ENVIO A SERVIDOR PRINCIPAL");
		}

        public DTO_Cliente_domicilio_data get_domicilio_data_object(string cliente_domicilio_id)
        {
            DTO_Cliente_domicilio_data data = new DTO_Cliente_domicilio_data();

            string sql = @"
                SELECT
                    *
                FROM
                    farmacontrol_global.clientes_domicilios
                WHERE
                    cliente_domicilio_id = @cliente_domicilio_id
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("cliente_domicilio_id",cliente_domicilio_id);

            conector.Select(sql,parametros);

            if(conector.result_set.Rows.Count > 0)
            {
                var row = conector.result_set.Rows[0];
                data.etiqueta = row["etiqueta"].ToString();
                data.cliente_domicilio_id = row["cliente_domicilio_id"].ToString();
                data.cliente_id = row["cliente_id"].ToString();
                data.calle = row["calle"].ToString();
                data.numero_exterior = row["numero_exterior"].ToString();
                data.numero_interior = row["numero_interior"].ToString();
                data.colonia = row["colonia"].ToString();
                data.ciudad = row["ciudad"].ToString();
                data.municipio = row["municipio"].ToString();
                data.estado = row["estado"].ToString();
                data.codigo_postal = row["codigo_postal"].ToString();
                data.pais = row["pais"].ToString();
                data.telefono = Convert.ToInt64(row["telefono"]);
                data.comentarios = row["comentarios"].ToString();
                data.fecha_agregado = Convert.ToDateTime(row["fecha_agregado"]);
            }

            return data;
        }

		public DataTable get_cliente_domicilio_data(string cliente_id)
		{
			string sql = @"
				SELECT
					clientes_domicilios.cliente_domicilio_id AS cliente_domicilio_id,
					etiqueta AS tipo,
					CONCAT_WS(' ', clientes_domicilios.calle, CONCAT('#',REPLACE(clientes_domicilios.numero_exterior, '#','')), clientes_domicilios.numero_interior,clientes_domicilios.colonia, clientes_domicilios.municipio, clientes_domicilios.ciudad) AS direccion,
					IF(clientes_domicilios.telefono = 0, 'N/A',clientes_domicilios.telefono) AS telefono,
                    clientes_domicilios.comentarios AS comentarios
				FROM
					farmacontrol_global.clientes_domicilios
				LEFT JOIN farmacontrol_global.clientes USING(cliente_id)
				WHERE
					cliente_id = @cliente_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("cliente_id",cliente_id);

			conector.Select(sql,parametros);

			return conector.result_set;
		}

		public DataTable get_cliente_data(string cliente_id)
		{
			string sql = @"
				SELECT
					cliente_id AS cliente_id,
					empleado_id,
					carnet_id,
					nombre,
					limite_credito,
					bloqueo_morosidad,
					plazo,
					credito_activado,
					pct_descuento_adicional,
					ventas_mayoreo,
					comentarios,
					fecha_agregado
				FROM
					farmacontrol_global.clientes
				WHERE
					cliente_id = @cliente_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("cliente_id",cliente_id);

			conector.Select(sql,parametros);

			return conector.result_set;
		}

		public DTO_Validacion registrar_cliente(Dictionary<string,string> datos_cliente)
		{
            /*
             * 
             * Agregar cola operaciones o intentar registrarlo al global directamente
             * 
             */ 


			DTO_Validacion validacion = new DTO_Validacion();
			Dictionary<string,object> parametros = new Dictionary<string,object>();

			string uuid_cliente = Misc_helper.uuid();

			string sql = @"
				INSERT INTO
					farmacontrol_global.clientes
				SET
					cliente_id = @cliente_id,
					nombre = @nombre,
                    fecha_agregado = @fecha_agregado,
                    crea_empleado_id = @empleado_id
				
			";

			parametros.Add("cliente_id",uuid_cliente);
			parametros.Add("nombre", datos_cliente["nombre"]);
            parametros.Add("fecha_agregado",Misc_helper.fecha());
            parametros.Add("empleado_id", datos_cliente["empleado_id"]);

			conector.Insert(sql,parametros);

			string uuid_cliente_domicilio = Misc_helper.uuid();

			sql = @"
				INSERT INTO
					farmacontrol_global.clientes_domicilios
				SET
					cliente_domicilio_id = @cliente_domicilio_id,
					cliente_id = @cliente_id,
					etiqueta = @etiqueta,
					calle = @calle,
					numero_exterior = @numero_exterior,
					numero_interior = @numero_interior,
					colonia = @colonia,
					ciudad = @ciudad,
					municipio = @municipio,
					estado = @estado,
					codigo_postal = @codigo_postal,
					pais = @pais,
					telefono = @telefono,
					comentarios = @comentarios,
                    fecha_agregado = @fecha_agregado
			
			";

			parametros.Add("cliente_domicilio_id", uuid_cliente_domicilio);
			parametros.Add("etiqueta", datos_cliente["tipo"]);
			parametros.Add("calle", datos_cliente["calle"]);
			parametros.Add("numero_exterior", datos_cliente["numero_exterior"]);
			parametros.Add("numero_interior", datos_cliente["numero_interior"]);
			parametros.Add("colonia", datos_cliente["colonia"]);
			parametros.Add("ciudad", datos_cliente["ciudad"]);
			parametros.Add("municipio", datos_cliente["municipio"]);
			parametros.Add("estado", datos_cliente["estado"]);
			parametros.Add("codigo_postal", datos_cliente["codigo_postal"]);
			parametros.Add("pais", datos_cliente["pais"]);
			parametros.Add("telefono", (datos_cliente["telefono"].ToString().Trim().Equals("")) ? 0 : Convert.ToInt64(datos_cliente["telefono"]));
			parametros.Add("comentarios", datos_cliente["comentarios"]);

            DAO_Cola_operaciones.insertar_cola_operaciones((long)Principal.empleado_id, "rest/clientes", "registrar_cliente_sucursal", parametros, "PARA ENVIO AL SERVIDOR PRINCIPAL");
			conector.Insert(sql,parametros);

			 sql = @"
				SELECT
					nombre
				FROM
					farmacontrol_global.clientes_domicilios
				LEFT JOIN farmacontrol_global.clientes USING(cliente_id)
				WHERE
					cliente_domicilio_id = @cliente_domicilio_id
			 ";

			 conector.Select(sql,parametros);

			 DTO_Validacion result = new DTO_Validacion();

			 if(conector.result_set.Rows.Count > 0)
			 {
				 result.status = true;
                 result.informacion = uuid_cliente_domicilio;
			 }
			 else
			 {
				result.status = false;
				result.informacion = "No pudo ser registrado correctamente el domicilio del cliente";
			 }

			 return result;
		}

		public DataTable get_clientes_by_nombre(string nombre)
		{
            //IF(clientes_domicilios.telefono = 0,'N/A',clientes_domicilios.telefono) AS telefono,
			string sql = @"
				SELECT
					clientes.cliente_id as cliente_id,
					clientes.nombre AS nombre,
					CAST( clientes_domicilios.telefono AS CHAR(14) ) AS telefono,
					IF(COUNT(clientes_domicilios.etiqueta) > 1, 'VARIOS', clientes_domicilios.etiqueta) AS tipo,
					CONCAT_WS(' ', clientes_domicilios.calle, CONCAT('#',REPLACE(clientes_domicilios.numero_exterior, '#','')), clientes_domicilios.numero_interior, clientes_domicilios.colonia, clientes_domicilios.municipio, clientes_domicilios.ciudad) AS direccion
				FROM
					farmacontrol_global.clientes
				LEFT JOIN farmacontrol_global.clientes_domicilios USING(cliente_id)
				WHERE
					clientes.nombre LIKE @nombre
				OR
					clientes_domicilios.telefono LIKE @nombre
				GROUP BY
					clientes.cliente_id
				ORDER BY clientes.nombre ASC
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("nombre","%"+nombre.Replace(" ","%")+"%");

			conector.Select(sql,parametros);
			
			return conector.result_set;
		}

		public DataTable get_rfc_data(string rfc_registro_id)
		{
			string sql = @"
				SELECT
					rfc,
					razon_social
				FROM
					rfc_registros
				WHERE
					rfc_registro_id = @rfc_registro_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("rfc_registro_id",rfc_registro_id);

			conector.Select(sql,parametros);

			return conector.result_set;
		}

		public DataTable get_cliente_credito_data(string cliente_credito_id)
		{
            DataTable result_query = new DataTable();
            //IF(clientes_domicilios.telefono = 0, 'N/A', clientes_domicilios.telefono) AS telefono
			string sql = @"
				SELECT
					clientes.nombre AS nombre,
                    clientes.plazo AS plazo,
					CONCAT_WS(' ', clientes_domicilios.calle, clientes_domicilios.colonia, clientes_domicilios.municipio, clientes_domicilios.ciudad) AS direccion,
					CAST( clientes_domicilios.telefono AS CHAR(14) ) AS telefono
				FROM
					farmacontrol_global.clientes
				LEFT JOIN farmacontrol_global.clientes_creditos USING(cliente_id)
				LEFT JOIN farmacontrol_global.clientes_domicilios USING(cliente_id)
				WHERE
					clientes.cliente_id = @cliente_credito_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("cliente_credito_id", cliente_credito_id);

			conector.Select(sql,parametros);

            result_query = conector.result_set;

            if (result_query.Rows.Count > 0)
            {
                foreach (DataRow row in result_query.Rows)
                {
                    if (DBNull.Value.Equals(row["telefono"]) || row["telefono"].ToString() == "0" )
                    {
                        row["telefono"] = "N/A";
                    }
                }
            }

            return result_query;

			//return conector.result_set;
		}

		public DataTable get_domicilio_data(string cliente_domicilio_id)
		{
            DataTable result_query = new DataTable();
            //IF(clientes_domicilios.telefono = 0, 'N/A', clientes_domicilios.telefono) AS telefono,
			string sql = @"
				SELECT
					clientes.nombre AS nombre,
					etiqueta AS tipo,
					CONCAT_WS(' ', clientes_domicilios.calle, CONCAT('#',REPLACE(clientes_domicilios.numero_exterior, '#','')), clientes_domicilios.numero_interior, clientes_domicilios.colonia, clientes_domicilios.municipio, clientes_domicilios.ciudad) AS direccion,
				    CAST( clientes_domicilios.telefono AS CHAR(14) ) AS telefono,
                    clientes_domicilios.comentarios AS comentarios
				FROM
					farmacontrol_global.clientes_domicilios
				LEFT JOIN farmacontrol_global.clientes USING(cliente_id)
				WHERE
					cliente_domicilio_id = @cliente_domicilio_id
			";

			Dictionary<string,object> parametros =  new Dictionary<string,object>();
			parametros.Add("cliente_domicilio_id", cliente_domicilio_id);

			conector.Select(sql,parametros);
            result_query = conector.result_set;

            if (result_query.Rows.Count > 0)
            {
                foreach (DataRow row in result_query.Rows)
                {
                    if (DBNull.Value.Equals(row["telefono"]) || row["telefono"].ToString() == "0")
                    {
                        row["telefono"] = "N/A";
                    }
                }
            }

            return result_query;
			//return conector.result_set;
		}

		public DataTable get_clientes_creditos(string nombre,bool es_numeros = false)
		{
			/*string sql = @"
				SELECT
					cliente_id AS cliente_id,
					clientes.nombre AS nombre,
					clientes.limite_credito AS credito_total,
					clientes.limite_credito - COALESCE(clientes_creditos_total,0) AS credito_disponible
				FROM
					farmacontrol_global.clientes
				LEFT JOIN 
                (
                    SELECT
                        clientes.cliente_id AS cliente_id, 
                        clientes.nombre AS nombre,
                        clientes.limite_credito AS limite_credito,
                        SUM(COALESCE(clientes_creditos.total, 0)) AS clientes_creditos_total
                    FROM
                        farmacontrol_global.clientes_creditos
                    JOIN farmacontrol_global.clientes USING(cliente_id)
                    WHERE
                        clientes.nombre LIKE @nombre
                    AND
                        clientes_creditos.fecha_saldado IS NULL
                    GROUP BY clientes.cliente_id
                    LIMIT 50
                )
                AS tmp_clientes
                USING(cliente_id)
				LEFT JOIN farmacontrol_global.clientes_domicilios USING(cliente_id)
				WHERE
                (
                    clientes.nombre LIKE @nombre
                OR
					clientes_domicilios.telefono LIKE @nombre
                )
				GROUP BY
					clientes.cliente_id
				LIMIT 50
			";
             * 
            */
            //AND
							//clientes_creditos.fecha_saldado IS NULL
            /*string sql = @"
               	SELECT
						temp.cliente_id as cliente_id,
						temp.nombre as nombre,
						temp.credito_total as credito_total,
						temp.credito_total - sum( temp.total_disponible ) as credito_disponible
					FROM(
						SELECT
							clientes.cliente_id AS cliente_id, 
							clientes.nombre AS nombre,
							clientes.limite_credito AS credito_total,
							total - COALESCE(SUM(clientes_creditos_abonos.importe), 0) AS total_disponible
						FROM
							farmacontrol_global.clientes_creditos
						JOIN farmacontrol_global.clientes USING(cliente_id)
						LEFT JOIN 
							 farmacontrol_global.clientes_creditos_abonos
						USING( cliente_credito_id )	
						LEFT JOIN farmacontrol_global.clientes_domicilios USING(cliente_id)
						WHERE
						(
							clientes.nombre LIKE @nombre
						OR
							clientes_domicilios.telefono LIKE @nombre
						)					
						
						GROUP BY cliente_credito_id
						ORDER BY  clientes.nombre
						
						LIMIT 50
						
					) as temp
					group by 
					  temp.cliente_id
					LIMIT 50

            ";*/
            /*
             *WHERE
						(
							clientes.nombre LIKE @nombre
						OR
							clientes_domicilios.telefono LIKE @nombre
						)		 
             * 
             */

            string sql = @"
               	    SELECT
						temp.cliente_id as cliente_id,
						temp.nombre as nombre,
						temp.credito_total as credito_total,
						temp.credito_total - COALESCE( sum( temp.total_disponible ),0 ) as credito_disponible,
                        temp.credito_activado as cliente_activo
					FROM(
						SELECT
							clientes.cliente_id AS cliente_id, 
							clientes.nombre AS nombre,
							clientes.limite_credito AS credito_total,
							total - COALESCE(SUM(clientes_creditos_abonos.importe), 0) AS total_disponible,
                            credito_activado
						FROM
							farmacontrol_global.clientes
						LEFT JOIN farmacontrol_global.clientes_creditos USING(cliente_id)
						LEFT JOIN 
							 farmacontrol_global.clientes_creditos_abonos
						USING( cliente_credito_id )	
						LEFT JOIN farmacontrol_global.clientes_domicilios USING(cliente_id)
						WHERE					
							clientes_domicilios.telefono LIKE @nombre					
						GROUP BY cliente_credito_id
						ORDER BY  clientes.nombre

					) as temp
					group by 
					  temp.cliente_id
					LIMIT 50
            ";
            if (es_numeros == false)
            {      
                 sql = @"
               	    SELECT
						temp.cliente_id as cliente_id,
						temp.nombre as nombre,
						temp.credito_total as credito_total,
						temp.credito_total - COALESCE( sum( temp.total_disponible ),0 ) as credito_disponible,
                        temp.credito_activado as cliente_activo
					FROM(
						SELECT
							clientes.cliente_id AS cliente_id, 
							clientes.nombre AS nombre,
							clientes.limite_credito AS credito_total,
							total - COALESCE(SUM(clientes_creditos_abonos.importe), 0) AS total_disponible,
                            credito_activado
						FROM
							farmacontrol_global.clientes
						LEFT JOIN farmacontrol_global.clientes_creditos USING(cliente_id)
						LEFT JOIN 
							 farmacontrol_global.clientes_creditos_abonos
						USING( cliente_credito_id )	
						LEFT JOIN farmacontrol_global.clientes_domicilios USING(cliente_id)
						WHERE					
							 clientes.nombre LIKE @nombre					
						GROUP BY cliente_credito_id
						ORDER BY  clientes.nombre
						
					) as temp
					group by 
					  temp.cliente_id
					LIMIT 50
            ";
            }
           

			Dictionary<string,object> parametros = new Dictionary<string,object>();

	     	parametros.Add("nombre", "%" + nombre.Replace(" ","%") + "%");
            

			conector.Select(sql,parametros);
         

			return conector.result_set;
		}

		public DataTable get_domicilios(string cliente_domicilio_id)
		{
			try
			{
				string sql= @"
					SELECT
						clientes_domicilios.cliente_domicilio_id AS cliente_domicilio_id,
						etiqueta AS tipo,
						CONCAT_WS(' ', clientes_domicilios.calle,CONCAT('#',REPLACE(clientes_domicilios.numero_exterior, '#','')), clientes_domicilios.numero_interior, clientes_domicilios.colonia, clientes_domicilios.municipio, clientes_domicilios.ciudad) AS direccion
					FROM
						farmacontrol_global.clientes_domicilios
					WHERE
						cliente_id = @cliente_id
				";

				Dictionary<string,object> parametros =  new Dictionary<string,object>();
				parametros.Add("cliente_id",cliente_domicilio_id);

				conector.Select(sql,parametros);
			}
			catch(Exception exception)
			{
				Log_error.log(exception);
			}

			return conector.result_set;
		}

		public DataTable get_clientes_domicilios(string nombre, bool es_nombre = true)
		{
            DataTable result_query = new DataTable();

			try
			{
                //IF(clientes_domicilios.telefono = 0, 'N/A', clientes_domicilios.telefono) AS telefono
                //MATCH(clientes.nombre) AGAINST(@busqueda IN BOOLEAN MODE)
                string sql = "";

				Dictionary<string,object> parametros = new Dictionary<string,object>();
     
                string busqueda_nombre = nombre.Trim();
                busqueda_nombre = "+"+busqueda_nombre.Replace( " ", " +" );

                string busqueda = nombre.Trim();
                if (es_nombre)
                {

                    
                    sql = @"
					SELECT
						clientes.nombre AS nombre,
						CAST( clientes_domicilios.telefono AS CHAR(15) ) AS telefono,
						clientes.cliente_id as cliente_id,
						clientes_domicilios.cliente_domicilio_id AS cliente_domicilio_id,
						IF(COUNT(clientes_domicilios.etiqueta) > 1, 'VARIOS', clientes_domicilios.etiqueta) AS tipo,
						CONCAT_WS(' ', clientes_domicilios.calle, CONCAT('#',REPLACE(clientes_domicilios.numero_exterior, '#','')), clientes_domicilios.numero_interior, clientes_domicilios.colonia,clientes_domicilios.municipio, clientes_domicilios.ciudad) AS direccion
					FROM
						farmacontrol_global.clientes_domicilios
					LEFT JOIN farmacontrol_global.clientes USING(cliente_id)
					WHERE
						MATCH(clientes.nombre) AGAINST(@busqueda_nombre IN BOOLEAN MODE)
                    AND 
                        clientes_domicilios.telefono IS NOT NULL
                    AND 
                        clientes.nombre != ''
					GROUP BY
						clientes.cliente_id
					ORDER BY 
                        clientes.nombre ASC
                    
				";
                     

                    /*
                    sql = @"
					        SELECT
						        COALESCE(TRIM(clientes.nombre),'SINNOMBRE') AS nombre,
						        CAST( clientes_domicilios.telefono AS CHAR(15) ) AS telefono,
						        clientes.cliente_id as cliente_id,
						        clientes_domicilios.cliente_domicilio_id AS cliente_domicilio_id,
						        IF(COUNT(clientes_domicilios.etiqueta) > 1, 'VARIOS', clientes_domicilios.etiqueta) AS tipo,
						        CONCAT_WS(' ', clientes_domicilios.calle, CONCAT('#',REPLACE(clientes_domicilios.numero_exterior, '#','')), clientes_domicilios.numero_interior, clientes_domicilios.colonia,clientes_domicilios.municipio, clientes_domicilios.ciudad) AS direccion
					        FROM
						        farmacontrol_global.clientes_domicilios
					        LEFT JOIN farmacontrol_global.clientes USING(cliente_id)
                            WHERE
                                 clientes_domicilios.telefono IS NOT NULL
                            AND 
                                 clientes.nombre != ''
     
				            GROUP BY
						        clientes.cliente_id
				            ORDER BY 
                                clientes.nombre ASC

                    ";
 
                    */


                }
                else
                {

                    sql = @"
					SELECT
						clientes.nombre AS nombre,
						CAST( clientes_domicilios.telefono AS CHAR(15) ) AS telefono,
						clientes.cliente_id as cliente_id,
						clientes_domicilios.cliente_domicilio_id AS cliente_domicilio_id,
						IF(COUNT(clientes_domicilios.etiqueta) > 1, 'VARIOS', clientes_domicilios.etiqueta) AS tipo,
						CONCAT_WS(' ', clientes_domicilios.calle, CONCAT('#',REPLACE(clientes_domicilios.numero_exterior, '#','')), clientes_domicilios.numero_interior, clientes_domicilios.colonia,clientes_domicilios.municipio, clientes_domicilios.ciudad) AS direccion
					FROM
						farmacontrol_global.clientes_domicilios
					LEFT JOIN farmacontrol_global.clientes USING(cliente_id)
					WHERE
						clientes_domicilios.telefono = @busqueda
					GROUP BY
						clientes.cliente_id
					ORDER BY
                        clientes.nombre ASC
                    LIMIT 10
				";

                }

                parametros.Add("busqueda", busqueda);
                parametros.Add("busqueda_nombre", busqueda_nombre);
				conector.Select(sql,parametros);

                result_query = conector.result_set;

                if( result_query.Rows.Count > 0 )
                {
                        
                    foreach(DataRow row in result_query.Rows)
					{

                        if (row["telefono"].ToString() == "0" )
						{
                            row["telefono"] = "N/A";
						}

                        if (row["nombre"].ToString() == "" )
                        {
                            row["nombre"] = "SINNOMBRE";
                        }


					}
                    /*
                    if (es_nombre)
                    {
                        result_query = result_query.AsEnumerable().Where(row => row.Field<string>("nombre").Contains(busqueda_nombre.ToUpper())).CopyToDataTable();

                    }
                  */
                }

			}
			catch(Exception exception)
			{
				Log_error.log(exception);
			}

			
            return result_query;
		}

        public DataTable get_historico_data(string cliente_id)
        {
            string sql = @"

                SELECT
	                CAST(amecop_original AS UNSIGNED) AS txtcodigo,
	                farmacontrol_global.articulos.nombre as txtdescripcion,
	                SUM(cantidad) as total,
                    MAX(fecha_terminado) as fecha,
					farmacontrol_global.clientes_domicilios.cliente_id as domicilio
                FROM
                 farmacontrol_local.ventas
                INNER JOIN 
                  farmacontrol_local.detallado_ventas
                USING(venta_id)  
                LEFT JOIN 
                   farmacontrol_global.articulos
                USING(articulo_id)
				LEFT JOIN 
				   farmacontrol_global.clientes_domicilios
			    USING(cliente_domicilio_id)
                WHERE
				(
                 cliente_domicilio_id IS NOT NULL
				OR 
                  cliente_credito_id IS NOT NULL
				)
				AND 
				(  
				   UPPER(cliente_credito_id) = UPPER(@cliente_id)
				OR 
				   UPPER(farmacontrol_global.clientes_domicilios.cliente_id)  = UPPER(@cliente_id)
				)
				GROUP BY 
                  txtcodigo
				ORDER BY 
					fecha DESC 	
			";
            //cliente_id = @cliente_id
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("cliente_id", cliente_id);

            conector.Select(sql, parametros);

            return conector.result_set;
        }
    }
}
