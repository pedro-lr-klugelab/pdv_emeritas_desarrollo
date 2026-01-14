using Farmacontrol_PDV.CLASSES;
using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.FORMS.ventas.facturacion;
using Farmacontrol_PDV.HELPERS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Farmacontrol_PDV.FORMS.comunes
{
    public partial class Log_principal : Form
    {
        public Log_principal()
        {
            InitializeComponent();
            get_logfile();
        }



        void get_logfile()
        {
            string fecha = Misc_helper.fecha();
            fecha = fecha.Replace('/', '_');

            string nombre_archivo = "log_" + fecha + ".txt";
            if (File.Exists(nombre_archivo))
            {
                string text = System.IO.File.ReadAllText(nombre_archivo);
                txt_log.AppendText(text);
                txt_log.ScrollToCaret();
            }
            else
            {
                txt_log.Text = "ARCHIVO DE REGISTRO VACIO";
            }
        }

        public DTO_Pago_tipos show_pagos()
        {
            Tipos_pago tipos = new Tipos_pago();
            tipos.ShowDialog();

            if(tipos.return_pago_tipos.pago_tipo_id == 0)
            {
                return show_pagos();
            }

            return tipos.return_pago_tipos;
        }

        public void cuenta_pagos()
        {
            /*
            Cuenta_pago_tipo tipos = new Cuenta_pago_tipo(metodo_pago);
            tipos.ShowDialog();

            if (tipos.cuenta == "")
            {
                return cuenta_pagos(metodo_pago);
            }

            return tipos.cuenta;
             * */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*Cortes corte = new Cortes();
            corte.construccion_ticket(80, true);
            corte.print();
            */
            Devolucion dev = new Devolucion();
            dev.construccion_ticket(Convert.ToInt64(txt_folio_devolucion.Text.Trim()), false, false);
            dev.print();
            /*
            Facturacion_v12 facturacion = new Facturacion_v12();
            facturacion.ShowDialog();
             */
            /*var metodo_pago = show_pagos();

            if(metodo_pago.usa_cuenta)
            {
                MessageBox.Show(cuenta_pagos(metodo_pago.nombre));
            }*/

            

            /*
            Devolucion ticke = new Devolucion();
            ticke.construccion_ticket(3663, false, true);
            ticke.print();
             */

            /*Regex reg = new Regex(@"[A-Z0-9_-]\\${0,1}[A-Z0-9_-]+");

            string input = @"
15$654987
05$_$87654
0005-as#$5487 po
";

            string output = reg.Replace(input, delegate(Match match)
            {
                string coincidencia = match.ToString();

                txt_salida.AppendText(coincidencia+"\n");

                return coincidencia;
            });*/
        }

    }
}
