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
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.CLASSES;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.CLASSES.PRINT;


namespace Farmacontrol_PDV.FORMS.ventas.tae
{
    public partial class Elije_tae_proveedor : Form
    {
        public DTO_Tae_proveedores return_tae_prov = new DTO_Tae_proveedores();

        public Elije_tae_proveedor()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void elije_tae_proveedor_Load(object sender, EventArgs e)
        {
            DAO_Tae dao_tae = new DAO_Tae();
            dgv_prov_tae.DataSource = dao_tae.get_proveedores_tae();
            dgv_prov_tae.ClearSelection();
        }

        private void Elije_tae_proveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 27)
            {
                this.Close();
            }
        }

        private void dgv_prov_tae_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var row = dgv_prov_tae.SelectedRows[0];
                return_tae_prov = new DTO_Tae_proveedores();
                return_tae_prov.fabricante_id = Convert.ToInt32(row.Cells["fabricante_id"].Value);
                return_tae_prov.nombre = row.Cells["nombre"].Value.ToString();
                
                this.Close();
            }
        }
    }
}
