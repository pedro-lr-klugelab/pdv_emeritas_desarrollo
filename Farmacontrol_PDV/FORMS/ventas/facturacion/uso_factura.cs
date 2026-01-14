using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.FORMS.comunes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Farmacontrol_PDV.FORMS.ventas.facturacion
{
    public partial class uso_factura : Form
    {
        public string tipo_uso_cfdi = "";
        public string tipo_regimen = "";


        public uso_factura()
        {
            InitializeComponent();
        }

        private void btn_cancelar_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void uso_factura_Shown(object sender, System.EventArgs e)
        {

            cbb_tipo_cfdi.DataSource = new BindingSource(new Dictionary<string, string>(){
                {"G01-ADQUISICION DE MERCANCIAS","G01"},
                {"G02-DEVOLUCIONES,DESCUENTOS O BONIFICACIONES","G02"},
                {"G03-GASTOS EN GENERAL","G03"},
                {"D01-HONORARIOS MEDICOS,DENTALES Y GASTOS HOSPITALARIOS","D01"},
                {"D02-GASTOS MEDICOS POR INCAPACIDAD O DISCAPACIDAD","D02"},
                {"D04-DONATIVOS","D04"},
                {"D07-PRIMAS POR SEGUROS DE GASTOS MEDICOS","D07"},
                {"P01-POR DEFINIR","P01"},
                {"S01-SIN EFECTOS FISCALES","S01"},
			}, null);

            cbb_tipo_cfdi.DisplayMember = "Key";
            cbb_tipo_cfdi.ValueMember = "Value";

            cbb_tipo_cfdi.DroppedDown = true;
            cbb_tipo_cfdi.Focus();


            cbx_regimen_fiscal.DataSource = new BindingSource(new Dictionary<string, string>(){
                {"601-GENERAL DE LEY DE PERSONAS MORALES","601"},
                {"603-PERSONAS MORALES CON FINES NO LUCRATIVOS","603"},
                {"605-SUELDOS Y SALARIOS E INGRESOS ASIMILADOS A SALARIOS","605"},
                {"606-ARRENDAMIENTO","606"},
                {"607-REGIMEN DE ENAJENACION O ADQUISICIÓN DE BIENES","607"},
                {"608-DEMÁS INGRESOS","608"},
                {"610-RESIDENTES EN EL EXTRANJERO SIN ESTABLECIMIENTO PERMANENTE EN MEXICO","610"},
                {"611-INGRESOS POR DIVIDENDOS","611"},
                {"612-PERSONAS FÍSICAS CON ACTIVIDADES EMPRESARIALES Y PROFESIONALES","612"},
                {"614-INGRESOS POR INTERESES","614"},
                {"615-REGIMEN DE LOS INGRESOS POR OBTENCIÓN DE PREMIOS","615"},
                {"616-SIN OBLIGACIONES FISCALES","616"},
                {"620-SOCIEDADES COOPERATIVAS DE PRODUCCIÓN QUE OPTAN POR DIFERIR SUS INGRESOS","620"},
                {"621-INCORPORACIÓN FISCAL","621"},
                {"622-ACTIVIDADES AGRICOLAS, GANADERAS, SILVICOLAS Y PESQUERAS","622"},
                {"623-OPCIONAL PARA GRUPOS DE SOCIEDADES","623"},
                {"624-COORDINADOS","624"},
                {"625-REGIMEN DE LAS ACTIVIDADES EMPRESARIALES CON INGRESOS A TRAVES DE PLATAFORMAS TECNOLOGICAS","625"},
                {"626-REGIMEN SIMPLIFICADO DE CONFIANZA","626"},
                
			}, null);

            cbx_regimen_fiscal.DisplayMember = "Key";
            cbx_regimen_fiscal.ValueMember = "Value";

            cbx_regimen_fiscal.DroppedDown = false;


        }

        private void label1_Click(object sender, System.EventArgs e)
        {

        }

        private void btn_aceptar_Click(object sender, System.EventArgs e)
        {
            this.acepta_cfdi();
        }

        public void acepta_cfdi()
        {
            //tengo que enviar el valor del cfdi 
            tipo_uso_cfdi = cbb_tipo_cfdi.SelectedValue.ToString();
            tipo_regimen = cbx_regimen_fiscal.SelectedValue.ToString();
           ///return tipo_uso_cfdi;
            this.Close();

        }
    }
}
