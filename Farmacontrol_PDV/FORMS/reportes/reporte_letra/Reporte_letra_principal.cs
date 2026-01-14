using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.FORMS.reportes.reporte_letra;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.CLASSES.PRINT;

namespace Farmacontrol_PDV.FORMS.reportes.reporte_letra
{
    public partial class Reporte_letra_principal : Form
    {
        public Reporte_letra_principal()
		{
			InitializeComponent();
            cargarAbecedario();
           
			
		}

        public void cargarAbecedario()
        {
            Dictionary<string, string> abc = new Dictionary<string,string>();
            abc.Add("A", "A");
            abc.Add("B", "B");
            abc.Add("C", "C");
            abc.Add("D", "D");
            abc.Add("E", "E");
            abc.Add("F", "F");
            abc.Add("G", "G");
            abc.Add("H", "H");
            abc.Add("I", "I");
            abc.Add("J", "J");
            abc.Add("K", "K");
            abc.Add("L", "L");
            abc.Add("M", "M");
            abc.Add("N", "N");
            abc.Add("Ñ", "Ñ");
            abc.Add("O", "O");
            abc.Add("P", "P");
            abc.Add("Q", "Q");
            abc.Add("R", "R");
            abc.Add("S", "S");
            abc.Add("T", "T");
            abc.Add("U", "U");
            abc.Add("V", "V");
            abc.Add("W", "W");
            abc.Add("X", "X");
            abc.Add("Y", "Y");
            abc.Add("Z", "Z");

            cbb_letras.DataSource = new BindingSource(abc, null);
            cbb_letras.DisplayMember = "Key";
            cbb_letras.ValueMember = "Value";
        }

        private void cbb_letras_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 40:
                    if (dgv_articulos.Rows.Count > 0)
                    {
                        dgv_articulos.CurrentCell = dgv_articulos.Rows[0].Cells["amecop"];
                        dgv_articulos.Rows[0].Selected = true;
                        dgv_articulos.Focus();
                    }
                    break;
                case 27:
                    cbb_letras.Text = "";
                    break;
                case 13:
                    if (cbb_letras.Text.Trim().Length > 0)
                    {
                        buscar();
                    }
                    break;
            }
        }

        private void buscar()
        {
            Cursor = Cursors.WaitCursor;
            DAO_Articulos art = new DAO_Articulos();
            dgv_articulos.DataSource = art.get_articulos_data_existencias(cbb_letras.SelectedValue.ToString());
            dgv_articulos.ClearSelection();
            txt_resultados.Text = dgv_articulos.Rows.Count.ToString();
            Cursor = Cursors.Default;
        }

        private void cbb_letras_SelectedIndexChanged(object sender, EventArgs e)
        {
                buscar();
                dgv_articulos.Focus();
                dgv_articulos.CurrentCell = dgv_articulos.Rows[0].Cells[2];
                txt_resultados.Text = dgv_articulos.RowCount.ToString();
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private void btn_buscar_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbb_letras.Text.Trim().Length > 0)
            {
                buscar();
            }
        }

        private void Reporte_letra_principal_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 80:
                    if (e.Control)
                    {
                        if (dgv_articulos.Rows.Count > 0)
                        {
                           
                            Ticket_reporte_letra ticket_reporte_letra = new Ticket_reporte_letra();
                            ticket_reporte_letra.construccion_ticket(cbb_letras.Text);
                            ticket_reporte_letra.print();
                             
                        }
                    }
                    break;
            }
        }

        private void btn_imprimir_Click(object sender, EventArgs e)
        {
            Ticket_reporte_letra ticket_reporte_letra = new Ticket_reporte_letra();
            ticket_reporte_letra.construccion_ticket(cbb_letras.Text);
            ticket_reporte_letra.print();
        }

        private void smi_imprimir_Click(object sender, EventArgs e)
        {
            List<Busqueda_articulos_existencias> lst_itemSeleccionados = new List<Busqueda_articulos_existencias>();
            if (dgv_articulos.SelectedRows.Count != 0 ){
                foreach (DataGridViewRow r in dgv_articulos.SelectedRows)
                {
                    Busqueda_articulos_existencias temp = new Busqueda_articulos_existencias();
                    temp.precio_publico = (decimal) r.Cells["precio_publico"].Value;
                    temp.amecop = (string) r.Cells["amecop"].Value;
                    temp.nombre = (string) r.Cells["producto"].Value;
                    temp.existencia_total = (long) r.Cells["total"].Value;
                    temp.caducidad = (string) r.Cells["caducidad"].Value;
                    temp.lote = (string) r.Cells["lote"].Value;
                    lst_itemSeleccionados.Add( temp );
                }
            }

            lst_itemSeleccionados = lst_itemSeleccionados.OrderBy(a => a.nombre).ToList();
            Ticket_reporte_letra ticket_reporte_letra = new Ticket_reporte_letra();
            ticket_reporte_letra.imprimeSeleccionados(lst_itemSeleccionados);
            ticket_reporte_letra.print();
        }

        private void cbb_letras_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void dgv_articulos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
