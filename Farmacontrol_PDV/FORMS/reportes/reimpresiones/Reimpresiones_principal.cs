using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.HELPERS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;





namespace Farmacontrol_PDV.FORMS.reportes.reimpresiones
{
    public partial class Reimpresiones_principal : Form
    {
        public Reimpresiones_principal()
        {
            InitializeComponent();
        }

        public void get_tipos_impresiones()
        {
            DAO_Impresiones dao = new DAO_Impresiones();
            var tipos = dao.get_tipos_impresion();

            Dictionary<string, string> lista_tipos = new Dictionary<string, string>();

            lista_tipos.Add("TODOS LOS TIPOS","");

            foreach(string tipo in tipos)
            {
                string value = tipo;
                string key = tipo.Replace("_", "");

                lista_tipos.Add(key,value);
            }

            cbb_kardex_tipo.DataSource = new BindingSource(lista_tipos,null);
            cbb_kardex_tipo.DisplayMember = "Key";
            cbb_kardex_tipo.ValueMember = "Value";

            get_fechas();
        }
        
        void get_fechas()
        {
            List<DateTime> fechas = new List<DateTime>();

            DateTime actual = Convert.ToDateTime(Misc_helper.fecha()).Date;

            fechas.Add(actual);

            for (int x = 1; x < 30; x++)
            {
                int sub = x * -1;
                DateTime dateForButton = Convert.ToDateTime(Misc_helper.fecha()).AddDays(sub);

                fechas.Add(dateForButton.Date);
            }

            cbb_fecha.DataSource = fechas;
        }

        private void Reimpresiones_principal_Load(object sender, EventArgs e)
        {
            get_tipos_impresiones();
        }

        void get_impresiones()
        {
            DAO_Impresiones dao_impresiones = new DAO_Impresiones();
            dgv_impresiones.DataSource = dao_impresiones.get_impresiones(Convert.ToDateTime(cbb_fecha.SelectedValue).ToString("yyyy-MM-dd"),cbb_kardex_tipo.SelectedValue.ToString());
            dgv_impresiones.ClearSelection();
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            get_impresiones();
        }

        private void btn_reimprimir_Click(object sender, EventArgs e)
        {
            if(dgv_impresiones.SelectedRows.Count > 0)
            {
                Login_form login = new Login_form();
                login.ShowDialog();

                if(login.empleado_id != null)
                {
                    DAO_Login dao_login = new DAO_Login();

                    if(dao_login.empleado_es_encargado((long)login.empleado_id))
                    {

                        long impresion_id = Convert.ToInt64(dgv_impresiones.SelectedRows[0].Cells["impresion_id"].Value);

                        string tipo_reinpresion = dgv_impresiones.SelectedRows[0].Cells["tipo"].Value.ToString();

                        if (tipo_reinpresion != "VENTA" && tipo_reinpresion != "FACTURACION")
                        {   
                            Print_new_helper.print_force(impresion_id);
                        }
                        else 
                        {

                            Motivo_reimpresiones motivos = new Motivo_reimpresiones(impresion_id, (long)login.empleado_id);
                            motivos.ShowDialog();

                        }

                    }
                    else
                    {
                        MessageBox.Show(this, "Solo el encargado de la sucursal puede reimprimir", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);        
                    }
                }
            }
            else
            {
                MessageBox.Show(this,"Es necesario seleccionar la tira la tira que desea reimprimir","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void cbb_kardex_tipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



    }
}
