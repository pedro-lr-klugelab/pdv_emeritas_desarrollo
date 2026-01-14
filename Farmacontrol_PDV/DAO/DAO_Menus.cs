using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacontrol_PDV.DTO;
using MySql.Data.MySqlClient;
using System.IO;
using System.Data;

namespace Farmacontrol_PDV.DAO
{
	class DAO_Menus
	{
		Conector conector = new Conector();

		public List<DTO_Menu> get_menus()
		{
			List<DTO_Menu> menus = new List<DTO_Menu>();

			try
			{
				string sql = @"
					SELECT
						*
					FROM
						farmacontrol_global.modulos
					WHERE
						padre_id IS NULL
					ORDER BY posicion ASC
				";

				conector.Select(sql);

				DataTable result = conector.result_set;

				foreach (DataRow row in result.Rows)
				{
					DTO_Menu menu = new DTO_Menu();

					menu.Menu_id = int.Parse(row["modulo_id"].ToString());
					menu.Nombre = row["nombre"].ToString();
					menu.Activo = Convert.ToBoolean(row["activo"].ToString());
					menu.Posicion = int.Parse(row["posicion"].ToString());

					List<DTO_Submenu> submenu = new List<DTO_Submenu>();

					menu.Submenus = get_submenus(menu.Menu_id);
					menus.Add(menu);
				}
			}
			catch (MySqlException excepcion)
			{
				Log_error.log(excepcion);
			}

			return menus;
		}

		public List<DTO_Submenu> get_submenus(int menu_id)
		{
			List<DTO_Submenu> list_submenus = new List<DTO_Submenu>();

			try
			{
				string sql = @"
					SELECT 
						* 
					FROM 
						farmacontrol_global.modulos
					WHERE 
						padre_id = @menu_id
					AND
						activo IS TRUE
                    AND
                        para_sucursal IS TRUE
					ORDER BY posicion ASC 
				";

				Dictionary<string, object> parametros = new Dictionary<string, object>();
				parametros.Add("menu_id", menu_id);

				conector.Select(sql, parametros);

				DataTable result = conector.result_set;

				foreach (DataRow row in result.Rows)
				{
					DTO_Submenu submenu_item = new DTO_Submenu();

					submenu_item.Submenu_id = int.Parse(row["modulo_id"].ToString());
					submenu_item.Nombre = row["nombre"].ToString();
					submenu_item.Controller = row["controller"].ToString();

					list_submenus.Add(submenu_item);
				}
			}
			catch (MySqlException ex)
			{
				Log_error.log(ex);
			}

			return list_submenus;
		}
	}
}
