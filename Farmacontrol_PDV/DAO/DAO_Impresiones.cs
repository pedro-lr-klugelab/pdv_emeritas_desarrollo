using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Impresiones
	{
		Conector conector =  new Conector();

        public List<DTO_Impresiones> get_impresiones(string fecha, string tipo = "")
        {
            List<DTO_Impresiones> lista_impresiones = new List<DTO_Impresiones>();

            string sql = string.Format(@"
                SELECT
                    impresiones.impresion_id AS impresion_id,
                    impresiones.terminal_id AS terminal_id,
                    terminales.nombre AS nombre_terminal,
                    impresiones.empleado_id AS empleado_id,
                    empleados.nombre AS nombre_empleado,
                    impresiones.tipo AS tipo,
                    impresiones.folio As folio,
                    impresiones.impresora AS impresora,
                    impresiones.modified AS fecha
                FROM
                    farmacontrol_local.impresiones
                JOIN farmacontrol_local.terminales USING(terminal_id)
                JOIN farmacontrol_global.empleados USING(empleado_id)
                WHERE
                   tipo != 'VALE_FARMACIA'
                AND
                    DATE(impresiones.modified) = DATE(@fecha)
                {0}
                ORDER BY impresion_id DESC
            ", (tipo.Equals("")) ? "" : string.Format("AND tipo = '{0}'",tipo));

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("fecha",fecha);

            conector.Select(sql, parametros);

            if(conector.result_set.Rows.Count > 0)
            {
                foreach(DataRow row in conector.result_set.Rows)
                {
                    DTO_Impresiones dto = new DTO_Impresiones();
                    dto.impresion_id = Convert.ToInt64(row["impresion_id"]);
                    dto.terminal_id = Convert.ToInt64(row["terminal_id"]);
                    dto.nombre_terminal = row["nombre_terminal"].ToString();
                    dto.empleado_id = Convert.ToInt64(row["empleado_id"]);
                    dto.nombre_empleado = row["nombre_empleado"].ToString();
                    dto.tipo = row["tipo"].ToString();
                    dto.folio = row["folio"].ToString();
                    dto.impresora = row["impresora"].ToString();
                    dto.fecha = Convert.ToDateTime(row["fecha"]);

                    lista_impresiones.Add(dto);
                }   
            }

            return lista_impresiones;
        }

        public List<string> get_tipos_impresion()
        {
            List<string> lista_tipos = new List<string>();
            //
            string[] tipo = new string[] { "ENTRADA","VENTA","ENTREGA_EFECTIVO","CORTE","FACTURACION","ETIQUETA_BULTOS","TRASPASOS","ENCARGO PREPAGADO","AJUSTE_EXISTENCIA","DEVOLUCION","DEVOLUCION_MAYORISTA","CADUCIDADES","COTIZACION","REP LETRA","VENTAS_ENVIOS" };
            /*
            string sql = @"
                SELECT
                    DISTINCT tipo
                FROM
                    farmacontrol_local.impresiones
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            conector.Select(sql, parametros);

            if(conector.result_set.Rows.Count > 0)
            {
                foreach(DataRow row in conector.result_set.Rows)
                {
                    lista_tipos.Add(row["tipo"].ToString());
                }
            }*/

            foreach(string tipos in tipo)
            {
                lista_tipos.Add(tipos.ToString());
            }


            return lista_tipos;
        }

        public void update_imrpesora_impresion(long impresion_id, string impresora)
        {
            string sql = @"
                UPDATE
                    farmacontrol_local.impresiones
                SET
                    impresora = @impresora
                WHERE
                    impresion_id = @impresion_id
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("impresion_id",impresion_id);
            parametros.Add("impresora",impresora);

            conector.Update(sql, parametros);
        }

        public long get_impresion_id(string tipo, long folio, long terminal_id)
        {
            string sql = @"
                SELECT
                    impresion_id
                FROM
                    farmacontrol_local.impresiones
                WHERE
                    tipo = @tipo
                AND
                    folio = @folio
                AND
                    terminal_id = @terminal_id
                ORDER BY impresion_id DESC
                LIMIT 1
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("tipo",tipo);
            parametros.Add("folio",folio);
            parametros.Add("terminal_id",terminal_id);

            conector.Select(sql, parametros);

            if(conector.result_set.Rows.Count > 0)
            {
                return Convert.ToInt64(conector.result_set.Rows[0]["impresion_id"]);
            }

            return 0;
        }


        public long get_terminal_impresion_id(string tipo, long folio)
        {
            string sql = @"
                SELECT
                    terminal_id
                FROM
                    farmacontrol_local.impresiones
                WHERE
                    tipo = @tipo
                AND
                    folio = @folio
                ORDER BY impresion_id DESC
                LIMIT 1
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("tipo", tipo);
            parametros.Add("folio", folio);

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                return Convert.ToInt64(conector.result_set.Rows[0]["terminal_id"]);
            }

            return 0;
        }


        public string get_texto_impresion(long impresion_id)
        {
            string sql = @"
                SELECT
                    CONVERT(UNCOMPRESS(contenido) USING utf8) AS contenido
                FROM
                    farmacontrol_local.impresiones
                WHERE
                    impresion_id = @impresion_id
                LIMIT 1
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("impresion_id", impresion_id);

            conector.Select(sql, parametros);

            if (conector.result_set.Rows.Count > 0)
            {
                return Misc_helper.DecodeBase64(conector.result_set.Rows[0]["contenido"].ToString());
            }

            return "";
        }

        public string get_texto_impresion(string tipo, long folio, long terminal_id)
        {
            string sql = @"
                SELECT
                    CONVERT(UNCOMPRESS(contenido) USING utf8) AS contenido
                FROM
                    farmacontrol_local.impresiones
                WHERE
                    tipo = @tipo
                AND
                    folio = @folio
                AND
                    terminal_id = @terminal_id
                LIMIT 1
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("tipo",tipo);
            parametros.Add("folio",folio);
            parametros.Add("termina_id",terminal_id);

            conector.Select(sql, parametros);

            if(conector.result_set.Rows.Count > 0)
            {
                return Misc_helper.DecodeBase64(conector.result_set.Rows[0]["contenido"].ToString());
            }

            return "";
        }

		public long registrar_impresion(long terminal_id, string contenido, string tipo, long folio, string impresora = "/dev/impresora_tickets")
		{
			int empleado_id = (int)FORMS.comunes.Principal.empleado_id;

			string sql = @"
				INSERT INTO
					farmacontrol_local.impresiones
				SET
					terminal_id = @terminal_id,
					empleado_id = @empleado_id,
					tipo = @tipo,
					folio = @folio,
					impresora = @impresora,
					contenido = COMPRESS(@contenido)
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("terminal_id",terminal_id);
			parametros.Add("empleado_id",empleado_id);
			parametros.Add("tipo",tipo);
			parametros.Add("folio",folio);
			parametros.Add("impresora", impresora);
			parametros.Add("contenido",HELPERS.Misc_helper.EncodeTo64(contenido));

			conector.Insert(sql,parametros);

            return Convert.ToInt64(conector.insert_id);
		}
	}
}
