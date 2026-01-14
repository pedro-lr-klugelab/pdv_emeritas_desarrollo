using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
    class DTO_Submenu
    {
        private int submenu_id;
        private string nombre;
        private string controller;

        public int Submenu_id
        {
            get { return submenu_id; }
            set { submenu_id = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value;}
        }

        public string Controller
        {
            get {return controller;}
            set {controller = value;}
        }
    }
}
