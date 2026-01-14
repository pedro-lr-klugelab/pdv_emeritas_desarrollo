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
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.CLASSES;
using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.FORMS.inventarios.control_antibioticos
{
    public partial class articulos_pendientes : Form
    {
        public DTO_control_antibiotico dto_tmp = new DTO_control_antibiotico();

        public articulos_pendientes()
        {
            InitializeComponent();
            muestra_pendientes();
        }

        private void muestra_pendientes()
        {
            DAO_Antibioticos dao_control_ab = new DAO_Antibioticos();
            dgv_control_AB.DataSource = dao_control_ab.get_ventas_sin_receta();
            dgv_control_AB.ClearSelection();
        }

        private void dgv_control_AB_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            seleccionar_producto();
            
        }

        public void seleccionar_producto()
        {
            DTO_control_antibiotico dto_temp = new DTO_control_antibiotico();
            dto_temp.control_antibiotico_id = (long)dgv_control_AB.Rows[dgv_control_AB.CurrentRow.Index].Cells["control_antibiotico_id"].Value;
            dto_temp.articulo_id = (long)dgv_control_AB.Rows[dgv_control_AB.CurrentRow.Index].Cells["articulo_id"].Value;
            dto_temp.amecop = dgv_control_AB.Rows[dgv_control_AB.CurrentRow.Index].Cells["amecop"].Value.ToString().Trim('*').Trim();

            this.dto_tmp = dto_temp;

            this.Close();
        }

        private void dgv_control_AB_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_control_AB_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    seleccionar_producto();
                break;
            }
        }
    }
}
