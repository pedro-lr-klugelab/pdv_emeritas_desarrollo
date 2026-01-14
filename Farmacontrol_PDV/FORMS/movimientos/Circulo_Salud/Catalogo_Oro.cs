using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Farmacontrol_PDV.DTO;
using System.Configuration;

namespace Farmacontrol_PDV.FORMS.movimientos.Circulo_Salud
{
    public partial class Catalogo_Oro : Form
    {

        DTO_WebServiceCirculoOro val = new DTO_WebServiceCirculoOro();


        public Catalogo_Oro(DTO_WebServiceCirculoOro obj)
        {
            InitializeComponent();

            val.catalogo = obj.catalogo;
            dtgvCatalogo.DataSource = val.catalogo;
           
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string textoBusqueda = txtBusqueda.Text.ToLower();
            BuscarEnGrid(dtgvCatalogo, textoBusqueda);

        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }*/

        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string textoBusqueda = txtBusqueda.Text.ToLower();
                BuscarEnGrid(dtgvCatalogo, textoBusqueda);
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
