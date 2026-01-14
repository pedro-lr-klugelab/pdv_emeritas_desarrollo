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
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.CLASSES.PRINT;

namespace Farmacontrol_PDV.FORMS.reportes.entradas_ab
{
    public partial class Entradas_AB : Form
    {
        public Entradas_AB()
        {
            InitializeComponent();
        }

        private void btn_reporte_Click(object sender, EventArgs e)
        {
            DAO_Antibioticos dao_ab = new DAO_Antibioticos();
            dgv_reporte.DataSource = dao_ab.get_reporte_ab();
            dgv_reporte.ClearSelection();
        }
    }
}
