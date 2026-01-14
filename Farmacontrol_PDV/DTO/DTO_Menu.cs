using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
    class DTO_Menu
    {
        private int menu_id;
        private string nombre;
        private Boolean activo;
        private int posicion;
        private List<DTO.DTO_Submenu> submenus;

        public int Menu_id
        {
            get { return menu_id; }
            set { menu_id = value; }
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

        public int Posicion
        {
            get { return posicion; }
            set { posicion = value; }
        }

        public List<DTO.DTO_Submenu> Submenus
        {
            get { return submenus; }
            set { submenus = value; }
        }
    }
}
