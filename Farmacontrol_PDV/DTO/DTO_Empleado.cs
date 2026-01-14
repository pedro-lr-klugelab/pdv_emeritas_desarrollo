using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
    class DTO_Empleado
    {
        private int? empleado_id;
        private string nombre;
        private Boolean activo;
        private string fcid;
        private Boolean es_admin;
        private Boolean es_root;
        private Boolean puede_login;
        private string usuario;
        private string password;

        public int? Empleado_id
        {
            get { return empleado_id; }
            set { empleado_id = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public Boolean Activo
        {
            get { return activo; }
            set { activo = value; }
        }

        public string Fcid
        {
            get { return fcid; }
            set { fcid = value; }
        }

        public Boolean Es_admin
        {
            get { return es_admin; }
            set { es_admin = value; }
        }

        public Boolean Es_root
        {
            get { return es_root; }
            set { es_root = value; }
        }

        public Boolean Puede_login
        {
            get { return puede_login; }
            set { puede_login = value; }
        }

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
