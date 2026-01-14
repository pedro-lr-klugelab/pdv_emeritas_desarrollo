using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Farmacontrol_PDV.DTO;
using System.Windows.Forms;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Terminales
	{
		Conector conector = new Conector();

        public bool imprime_folio_cotizacion()
        {
            int terminal_id = (int)get_terminal_id();

            string sql = @"
                SELECT
                    imprime_folio_cotizacion
                FROM
                    farmacontrol_local.terminales
                WHERE
                    terminal_id = @terminal_id    
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("terminal_id",terminal_id);

            conector.Select(sql, parametros);

            return Convert.ToBoolean(conector.result_set.Rows[0]["imprime_folio_cotizacion"]);
        }
		
		public List<DTO_Terminal> get_all_terminales()
		{
			List<DTO_Terminal> terminales = new List<DTO_Terminal>();

			string sql = @" SELECT * FROM farmacontrol_local.terminales";

			Dictionary<string,object> parametros = new Dictionary<string,object>();

			conector.Select(sql,parametros);

			foreach(DataRow row in conector.result_set.Rows)
			{
				DTO_Terminal terminal = new DTO_Terminal();
				terminal.terminal_id = Convert.ToInt32(row["terminal_id"]);
				terminal.nombre = row["nombre"].ToString();
				terminal.direccion_ip = row["direccion_ip"].ToString();
				terminal.es_caja = Convert.ToBoolean(row["es_caja"]);
				terminal.serie_facturas = row["serie_facturas"].ToString();
				terminal.serie_facturas_cortes = row["serie_facturas_cortes"].ToString();
				terminal.serie_nc = row["serie_nc"].ToString();
				terminal.permitir_impresion_remota = Convert.ToBoolean(row["permitir_impresion_remota"]);

				terminales.Add(terminal);
			}

			return terminales;
		}

        public void set_permitir_impresion_remota(bool permitir)
        {
            string sql = @"
                UPDATE
                    farmacontrol_local.terminales
                SET
                    permitir_impresion_remota = @permitir
                WHERE
                    terminal_id = @terminal_id
            ";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("permitir",permitir == true ? 1 : 0);
            parametros.Add("terminal_id",Convert.ToInt32(Misc_helper.get_terminal_id()));

            conector.Update(sql, parametros);
        }

		public bool get_terminal_permite_impresion_remota(int? terminal_id = null)
		{

			if(terminal_id == null)
			{
				terminal_id = get_terminal_id();
			}

			string sql = @"
				SELECT
					permitir_impresion_remota
				FROM
					farmacontrol_local.terminales
				WHERE
					terminal_id = @terminal_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("terminal_id", Misc_helper.get_terminal_id());

			conector.Select(sql, parametros);

			if (conector.result_set.Rows.Count > 0)
			{
				return Convert.ToBoolean(conector.result_set.Rows[0]["permitir_impresion_remota"]);
			}

			return false;
		}

		public string get_ip_terminal(int? terminal_id = null)
		{
			if(terminal_id == null)
			{
				terminal_id = get_terminal_id();
			}

			string sql = @"
				SELECT
					direccion_ip
				FROM
					farmacontrol_local.terminales
				WHERE
					terminal_id = @terminal_id	
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("terminal_id",terminal_id);

			conector.Select(sql,parametros);

			return conector.result_set.Rows[0]["direccion_ip"].ToString();
		}

		public bool get_terminal_es_caja()
		{
			string sql = @"
				SELECT
					es_caja
				FROM
					farmacontrol_local.terminales
				WHERE
					terminal_id = @terminal_id
                AND 
                    serie_facturas IS NOT NULL
                AND
                    serie_facturas_cortes IS NOT NULL
                AND
                    serie_nc IS NOT NULL
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("terminal_id",Misc_helper.get_terminal_id());

			conector.Select(sql,parametros);

			if(conector.result_set.Rows.Count > 0)
			{
				return Convert.ToBoolean(conector.result_set.Rows[0]["es_caja"]);
			}

			return false;
		}

		public string get_terminal_serie_facturas_cortes(int? terminal_id = null)
		{
            if (terminal_id == null)
            {
                terminal_id = this.get_terminal_id();
            }

			string sql = @"
				SELECT
					serie_facturas_cortes
				FROM
					farmacontrol_local.terminales
				WHERE
					terminal_id = @terminal_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("terminal_id", terminal_id);

			conector.Select(sql, parametros);

			return conector.result_set.Rows[0]["serie_facturas_cortes"].ToString();
		}

		public string get_terminal_serie_facturas(int? terminal_id = null)
		{
            if(terminal_id == null)
            {
                terminal_id = this.get_terminal_id();
            }

			string sql  = @"
				SELECT
					serie_facturas
				FROM
					farmacontrol_local.terminales
				WHERE
					terminal_id = @terminal_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("terminal_id",terminal_id);

			conector.Select(sql,parametros);

			return conector.result_set.Rows[0]["serie_facturas"].ToString();
		}

		public string get_terminal_serie_notas_credito(int? terminal_id = null)
		{
			string sql = @"
				SELECT
					serie_nc
				FROM
					farmacontrol_local.terminales
				WHERE
					terminal_id = @terminal_id
			";

			Dictionary<string, object> parametros = new Dictionary<string, object>();

            if(terminal_id == null)
            {
                terminal_id = this.get_terminal_id();
            }

			parametros.Add("terminal_id", terminal_id);

			conector.Select(sql, parametros);

			return conector.result_set.Rows[0]["serie_nc"].ToString();
		}

		public string get_terminal_nombre(int terminal_id)
		{
			string sql = @"
				SELECT
					nombre
				FROM
					farmacontrol_local.terminales
				WHERE
					terminal_id = @terminal_id
			";

			Dictionary<string,object> parametros = new Dictionary<string,object>();
			parametros.Add("terminal_id",terminal_id);

			conector.Select(sql,parametros);

			var result = conector.result_set;

			if(result.Rows.Count > 0)
			{
				return result.Rows[0]["nombre"].ToString();
			}

			return "";
		}

        public void checar_ip_terminal()
        {
            string query = @"SELECT 
							SUBSTRING_INDEX(host,':',1) as ip
						FROM 
							information_schema.processlist";

            Dictionary<string, object> parameters = new Dictionary<string, object>();

            conector.Select(query, parameters);

            var result = conector.result_set;

            foreach(DataRow row in result.Rows)
            {
                MessageBox.Show(row["ip"].ToString());
            
            }
        }

		public int? get_terminal_id()	
        {
           // checar_ip_terminal();
			int? terminal_id = null;

          /* string sql = @"
                SELECT TRUE;
            ";*/

           // Dictionary<string, object> parametros = new Dictionary<string, object>();

           // conector.Select(sql, parametros);

           string  sql = @"
				SELECT
					terminal_id
				FROM
					farmacontrol_local.terminales
				WHERE
					direccion_ip = (
						SELECT 
							SUBSTRING_INDEX(host,':',1) as 'ip' 
						FROM 
							information_schema.processlist 
						WHERE 
							ID=connection_id()
					)
				LIMIT 1
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();

			conector.Select(sql,parametros);

			var result = conector.result_set;

			if(result.Rows.Count > 0)
			{
				terminal_id = Convert.ToInt32(result.Rows[0]["terminal_id"]);
			}
   			terminal_id = 3;
           // terminal_id = 1;
			return terminal_id;
		}

        public long get_tae_diestel_pos_id(int terminal_id)
        {
            string sql = @"
				SELECT
					tae_diestel_pos_id
				FROM
					farmacontrol_local.terminales
				WHERE
					terminal_id = @terminal_id
			";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("terminal_id", terminal_id);

            conector.Select(sql, parametros);

            var result = conector.result_set;

            if (result.Rows.Count > 0)
            {
                return Convert.ToInt64(result.Rows[0]["tae_diestel_pos_id"]);
            }

            return 0;
        }
	}
}
