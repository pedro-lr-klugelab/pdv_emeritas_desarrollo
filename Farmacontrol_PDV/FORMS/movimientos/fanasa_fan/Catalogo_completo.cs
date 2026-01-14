using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.CLASSES;
using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.DAO;
using Newtonsoft.Json;
using System.Configuration;


namespace Farmacontrol_PDV.FORMS.movimientos.fanasa_fan
{
    public partial class Catalogo_completo : Form
    {   
        public string ip_servidor = ConfigurationManager.AppSettings["server_beneficios"];
        private DataTable datcatalogo = new DataTable();
        private DataSet ds = new DataSet();


        public Catalogo_completo()
        {
            InitializeComponent();
            this.carga_catagolo();
        }

        public void carga_catagolo()
        {
            DTO_WebServiceSoyFan val = new DTO_WebServiceSoyFan();
            Catalogo_fanasa catalogo = new Catalogo_fanasa();
            Rest_parameters parametros = new Rest_parameters();
            string nombre_empleado = "farmaboot";

            dtCatalogoSoyFan.DataSource = null;

            val = Rest_helper.enlace_webservice_soyfan<DTO_WebServiceSoyFan>("webservice/get_catalogo_soy_fan", parametros, ip_servidor);

            if (val.status)
            {
                dynamic array_catalogo = JsonConvert.DeserializeObject(val.catalogo);


                dtCatalogoSoyFan.DataSource = array_catalogo;

              
               
            }
            else
            {
               
                MessageBox.Show(this, "Error al obtener el catalogo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCodigoBusqueda_TextChanged(object sender, EventArgs e)
        {

            if (this.txtCodigoBusqueda.Text.Trim() != "")
            {
                BuscarEnGrid(dtCatalogoSoyFan, txtCodigoBusqueda.Text.Trim());
            }

        }



        public static void BuscarEnGrid(DataGridView dgv, string termino, int[] cols = null)
        {
            termino = (termino ?? "").Trim();
            bool showAll = string.IsNullOrEmpty(termino);

            if (cols == null)
            {
                cols = Enumerable.Range(0, dgv.Columns.Count).ToArray();
            }

            CurrencyManager cm = null;
            if (dgv.DataSource != null)
                cm = (CurrencyManager)dgv.BindingContext[dgv.DataSource, dgv.DataMember ?? ""];

            cm?.SuspendBinding();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                bool visible = showAll || cols.Any(ci =>
                {
                    var v = row.Cells[ci].Value;
                    return v != null &&
                           v.ToString().IndexOf(termino, StringComparison.OrdinalIgnoreCase) >= 0;
                });

                row.Visible = visible;
            }

            cm?.ResumeBinding();
        }

    }
}
