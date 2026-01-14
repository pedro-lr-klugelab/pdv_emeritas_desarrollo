using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.CLASSES.PRINT;
using System.Windows.Forms;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Rfcs
	{
		Conector conector = new Conector();

		public List<DTO_Rfc_factura> get_facturas_rfc(string rfc)
		{
			List<DTO_Rfc_factura> lista_facturas = new List<DTO_Rfc_factura>();
			Rest_parameters param = new Rest_parameters();
			param.Add("rfc",rfc);
			return Rest_helper.make_request<List<DTO_Rfc_factura>>("rfc/get_facturas_rfc",param);
		}
		
		public bool es_tipo_rfc(string rfc)
		{
			string sql = @"
				SELECT
					tipo_rfc
				FROM
					farmacontrol_global.rfc_registros
				WHERE
					rfc = @rfc
				LIMIT 1
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("rfc",rfc);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				if(conector.result_set.Rows[0]["tipo_rfc"].ToString().Equals("RFC"))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		public DTO_Rfc get_rfc_publico_general_extrangero()
		{
			DTO_Rfc dto_rfc = new DTO_Rfc();

			string sql = @"
				SELECT
					rfc_registro_id,
					rfc_registros.rfc AS rfc,
					rfc_registros.razon_social AS razon_social,
					rfc_registros.calle AS calle,
					rfc_registros.numero_exterior AS numero_exterior,
					rfc_registros.numero_interior AS numero_interior,
                    sucursales.codigo_postal AS codigo_postal,
					rfc_registros.correo_electronico AS correo_electronico
				FROM
					farmacontrol_global.rfc_registros
                JOIN farmacontrol_global.sucursales ON
                    sucursales.sucursal_id = @sucursal_id
				WHERE
					tipo_rfc = 'PGE'
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("sucursal_id",Convert.ToInt64(Config_helper.get_config_local("sucursal_id")));

			conector.Select(sql, parametros);

			var result = conector.result_set;

			if (result.Rows.Count > 0)
			{
				var row = result.Rows[0];

				dto_rfc.rfc_registro_id = row["rfc_registro_id"].ToString().ToUpper();
				dto_rfc.rfc = row["rfc"].ToString();
				dto_rfc.razon_social = row["razon_social"].ToString();
				dto_rfc.calle = row["calle"].ToString();
				dto_rfc.numero_exterior = row["numero_exterior"].ToString();
				dto_rfc.numero_interior = row["numero_interior"].ToString();
                dto_rfc.codigo_postal = row["codigo_postal"].ToString();

				string[] split_correos = row["correo_electronico"].ToString().Split(',');
				List<string> correos_electronicos = new List<string>();

				foreach (string correo in split_correos)
				{
					correos_electronicos.Add(correo);
				}

				dto_rfc.correos_electronicos = correos_electronicos;

			}

			return dto_rfc;
		}

		public DTO_Rfc get_rfc_publico_general_mexicano()
		{
			DTO_Rfc dto_rfc =  new DTO_Rfc();

			string sql = @"
				SELECT
					rfc_registro_id,
					rfc_registros.rfc AS rfc,
					rfc_registros.razon_social AS razon_social,
					rfc_registros.calle AS calle,
					rfc_registros.numero_exterior AS numero_exterior,
					rfc_registros.numero_interior AS numero_interior,
                    sucursales.codigo_postal AS codigo_postal,
					rfc_registros.correo_electronico AS correo_electronico
				FROM
					farmacontrol_global.rfc_registros
                JOIN farmacontrol_global.sucursales ON
                    sucursales.sucursal_id = @sucursal_id
				WHERE
					tipo_rfc = 'PGM'
				LIMIT 1
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("sucursal_id",Convert.ToInt64(Config_helper.get_config_local("sucursal_id")));

			conector.Select(sql, parametros);

			var result = conector.result_set;

			if (result.Rows.Count > 0)
			{
				var row = result.Rows[0];

				dto_rfc.rfc_registro_id = row["rfc_registro_id"].ToString().ToUpper();
				dto_rfc.rfc = row["rfc"].ToString();
				dto_rfc.razon_social = row["razon_social"].ToString();
				dto_rfc.calle = row["calle"].ToString();
				dto_rfc.numero_exterior = row["numero_exterior"].ToString();
				dto_rfc.numero_interior = row["numero_interior"].ToString();
                dto_rfc.codigo_postal = row["codigo_postal"].ToString();

				string[] split_correos = row["correo_electronico"].ToString().Split(',');
				List<string> correos_electronicos = new List<string>();

				foreach (string correo in split_correos)
				{
					correos_electronicos.Add(correo);
				}

				dto_rfc.correos_electronicos = correos_electronicos;

			}

			return dto_rfc;
		}

		public DTO_Validacion existe_rfc(string rfc)
		{
			DTO_Validacion val = new DTO_Validacion();
			val.status = false;

			string sql = @"
				SELECT
					rfc_registro_id
				FROM
					farmacontrol_global.rfc_registros
				WHERE
					rfc = @rfc
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("rfc",rfc);

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				val.status = true;
				val.informacion = conector.result_set.Rows[0]["rfc_registro_id"].ToString().ToUpper();
			}

			return val;
		}

		public DTO_Validacion registrar_rfc(DTO_Rfc dto)
		{
			DTO_Validacion val = new DTO_Validacion();

			string uuid = Misc_helper.uuid();

			string sql = @"
				INSERT INTO
					farmacontrol_global.rfc_registros
				SET
					rfc_registro_id = @rfc_registro_id,
					rfc = @rfc,
					razon_social = @razon_social,
					calle = @calle,
					numero_exterior = @numero_exterior,
					numero_interior = @numero_interior,
					colonia = @colonia,
					codigo_postal = @codigo_postal,
					ciudad = @ciudad,
					municipio = @municipio,
					estado = @estado,
					correo_electronico = @correo_electronico,
					fecha_agregado = NOW()
				ON DUPLICATE KEY UPDATE
					rfc_registro_id = @rfc_registro_id
			";

			string correos = "";

			foreach (string correo in dto.correos_electronicos)
			{
				correos += (correos.Equals("")) ? correo : "," + correo;
			}

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("rfc_registro_id",uuid);
			parametros.Add("rfc", dto.rfc);
			parametros.Add("razon_social", dto.razon_social);
			parametros.Add("calle", dto.calle);
			parametros.Add("numero_exterior", dto.numero_exterior);
			parametros.Add("numero_interior", dto.numero_interior);

			parametros.Add("colonia",dto.colonia);
			parametros.Add("codigo_postal",dto.codigo_postal);
			parametros.Add("ciudad",dto.ciudad);
			parametros.Add("municipio",dto.municipio);
			parametros.Add("estado",dto.estado);

			parametros.Add("correo_electronico", correos);

			conector.Insert(sql,parametros);

			if (conector.filas_afectadas > 0)
			{
                DialogResult dr = MessageBox.Show("¿Desea imprimir los datos guardados para que sean confirmados por el cliente?","Imprimir informacion RFC",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

                if(dr == DialogResult.Yes)
                {
                    Rfc ticket = new Rfc();
                    ticket.construccion_ticket(uuid);
                    ticket.print();
                }

				val.status = true;
				val.informacion = "la informacion del RFC fue registrada correctamente";
				val.elemento_nombre = uuid;
				DAO_Cola_operaciones.insertar_cola_operaciones(Convert.ToInt64(FORMS.comunes.Principal.empleado_id), "rest/rfc", "registrar_rfc", parametros, "Para registro al servidor principal");
			}
			else
			{
				val.status = false;
				val.informacion = "Ocurrio un error al intentar registrar la informacion del RFC, notifique a su administrador";
			}

			return val;
		}

		public DTO_Validacion actualizar_rfc(DTO_Rfc dto)
		{
            DTO_Rfc rfc_actualizar = new DTO_Rfc();
            rfc_actualizar = dto;

			DTO_Validacion val = new DTO_Validacion();

			string sql = @"
				UPDATE 
					farmacontrol_global.rfc_registros
				SET
					rfc = @rfc,
					razon_social = @razon_social,
					
					calle = @calle,
					numero_exterior = @numero_exterior,
					numero_interior = @numero_interior,
					colonia = @colonia,
					ciudad = @ciudad,
					municipio = @municipio,
					estado = @estado,
					codigo_postal = @codigo_postal,
					correo_electronico = @correo_electronico
				WHERE
					rfc_registro_id = @rfc_registro_id
			";

			string correos = "";

			foreach (string correo in dto.correos_electronicos)
			{
				correos += (correos.Equals("")) ? correo : "," + correo;
			}

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("rfc",dto.rfc);
			parametros.Add("razon_social",dto.razon_social);
			parametros.Add("calle",dto.calle);
			parametros.Add("numero_exterior",dto.numero_exterior);
			parametros.Add("numero_interior",dto.numero_interior);
			parametros.Add("colonia",dto.colonia);
			parametros.Add("ciudad",dto.ciudad);
			parametros.Add("municipio",dto.municipio);
			parametros.Add("estado",dto.estado);
			parametros.Add("codigo_postal",dto.codigo_postal);

			parametros.Add("correo_electronico",correos);
			parametros.Add("rfc_registro_id",dto.rfc_registro_id);

			conector.Update(sql,parametros);

			if(conector.filas_afectadas > 0)
			{
				/*
				Rfc ticket = new Rfc();
				ticket.construccion_ticket(dto.rfc_registro_id);
				ticket.print();
				*/
				val.status = true;
				val.informacion = "la informacion del RFC fue actualizada correctamente";
				DAO_Cola_operaciones.insertar_cola_operaciones(Convert.ToInt64(FORMS.comunes.Principal.empleado_id),"rest/rfc","actualizar_rfc",parametros,"Para registro al servidor principal");
			}
			else
			{
				val.status = false;
				val.informacion = "Ocurrio un error al intentar actualizar la informacion del RFC, notifique a su administrador";
			}

			return val;
		}

        public DTO_Rfc get_data_rfc_rfc(string rfc)
        {
            DTO_Rfc dto_rfc = new DTO_Rfc();

            string sql = @"
				SELECT
					rfc_registro_id,
					rfc,
					razon_social,
					calle,
					numero_exterior,
					numero_interior,
					colonia,
					ciudad,
					municipio,
					estado,
					pais,
					codigo_postal,
					tipo_rfc,
					correo_electronico
				FROM
					farmacontrol_global.rfc_registros as t0
				WHERE
					rfc = @rfc
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("rfc", rfc);

            conector.Select(sql, parametros);

            var result = conector.result_set;

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];

                dto_rfc.rfc_registro_id = row["rfc_registro_id"].ToString().ToUpper();
                dto_rfc.rfc = row["rfc"].ToString();
                dto_rfc.razon_social = row["razon_social"].ToString();
                dto_rfc.calle = row["calle"].ToString();
                dto_rfc.tipo_rfc = row["tipo_rfc"].ToString();
                dto_rfc.numero_exterior = row["numero_exterior"].ToString();
                dto_rfc.numero_interior = row["numero_interior"].ToString();
                dto_rfc.colonia = row["colonia"].ToString();
                dto_rfc.ciudad = row["ciudad"].ToString();
                dto_rfc.municipio = row["municipio"].ToString();
                dto_rfc.estado = row["estado"].ToString();
                dto_rfc.pais = row["pais"].ToString();
                dto_rfc.codigo_postal = row["codigo_postal"].ToString();

                string[] split_correos = row["correo_electronico"].ToString().Split(',');
                List<string> correos_electronicos = new List<string>();

                foreach (string correo in split_correos)
                {
                    correos_electronicos.Add(correo);
                }

                dto_rfc.correos_electronicos = correos_electronicos;

            }

            return dto_rfc;
        }

		public DTO_Rfc get_data_rfc(string rfc_registro_id)
		{
			DTO_Rfc dto_rfc = new DTO_Rfc();

			string sql = @"
				SELECT
					rfc_registro_id,
					rfc,
					razon_social,
					calle,
					numero_exterior,
					numero_interior,
					colonia,
					ciudad,
					municipio,
					estado,
					pais,
					codigo_postal,
					tipo_rfc,
					correo_electronico
				FROM
					farmacontrol_global.rfc_registros
				WHERE
					rfc_registro_id = @rfc_registro_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("rfc_registro_id",rfc_registro_id);

			conector.Select(sql,parametros);

			var result = conector.result_set;

			if(result.Rows.Count > 0)
			{
				var row = result.Rows[0];

				dto_rfc.rfc_registro_id = row["rfc_registro_id"].ToString().ToUpper();
				dto_rfc.rfc = row["rfc"].ToString();
				dto_rfc.razon_social = row["razon_social"].ToString();
				dto_rfc.calle = row["calle"].ToString();
				dto_rfc.tipo_rfc = row["tipo_rfc"].ToString();
				dto_rfc.numero_exterior = row["numero_exterior"].ToString();
				dto_rfc.numero_interior = row["numero_interior"].ToString();
				dto_rfc.colonia = row["colonia"].ToString();
				dto_rfc.ciudad = row["ciudad"].ToString();
				dto_rfc.municipio = row["municipio"].ToString();
				dto_rfc.estado = row["estado"].ToString();
				dto_rfc.pais = row["pais"].ToString();
				dto_rfc.codigo_postal = row["codigo_postal"].ToString();

				string[] split_correos = row["correo_electronico"].ToString().Trim().Split(',');

                if(split_correos.Length > 0)
                {
                    List<string> correos_electronicos = new List<string>();

                    foreach (string correo in split_correos)
                    {
                        if(correo.Trim().Length > 0)
                        {
                            correos_electronicos.Add(correo);
                        }
                    }

                    dto_rfc.correos_electronicos = correos_electronicos;
                }
			}

			return dto_rfc;
		}

		public DataTable get_rfc_registros(string nombre)
		{
			string sql = @"
				SELECT
					UPPER(rfc_registro_id) AS rfc_registro_id,
					rfc,
					razon_social,
					CONCAT_WS(' ', rfc_registros.calle, rfc_registros.numero_exterior, rfc_registros.numero_interior, rfc_registros.colonia, rfc_registros.municipio, rfc_registros.ciudad) AS direccion,
					correo_electronico
				FROM
					farmacontrol_global.rfc_registros
				WHERE
					rfc LIKE @nombre
				OR
					razon_social LIKE @nombre
				AND
					tipo_rfc = 'RFC'
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();

			parametros.Add("nombre","%"+nombre.Replace(" ","%")+"%");

			conector.Select(sql,parametros);

			return conector.result_set;
		}

		public string[] get_rfc_registros_combobusqueda(string busqueda)
		{
			string sql = @"
				SELECT
					razon_social
				FROM
					farmacontrol_global.rfc_registros
				WHERE
					razon_social LIKE @busqueda
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("busqueda","%"+busqueda.Replace(" ","%")+"%");

			conector.Select(sql,parametros);

			string[] lista_registros =  new string[conector.result_set.Rows.Count];

			int count = 0;

			foreach(DataRow row in conector.result_set.Rows)
			{
				lista_registros[count] = row["razon_social"].ToString();
				count++;
			}

			return lista_registros;
		}
	}
}
