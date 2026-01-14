using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.FORMS.comunes;

namespace Farmacontrol_PDV.HELPERS
{
	class Login_helper
	{
		public int? empleado_id { get; set; }
		public string empleado_nombre { get; set; }
		public string usuario { set; get; }
		public string password { set; get; }

		Login_form login_form = new Login_form();

		public int? pide_login()
		{
			empleado_id = null;
			empleado_nombre = "";

			login_form.ShowDialog();
			
			empleado_id = login_form.empleado_id;
			empleado_nombre = login_form.empleado_nombre;
			usuario = login_form.usuario;
			password = login_form.password;

			return empleado_id;
		}
	}
}
