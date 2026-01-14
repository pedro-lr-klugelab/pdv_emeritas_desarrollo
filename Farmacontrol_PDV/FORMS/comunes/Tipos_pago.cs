using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farmacontrol_PDV.FORMS.comunes
{
    public partial class Tipos_pago : Form
    {

        public DTO_Pago_tipos return_pago_tipos = new DTO_Pago_tipos();
        public bool es_tae = false;

        public Tipos_pago(bool es_tae = false)
        {
            InitializeComponent();
            this.es_tae = es_tae;
        }

        public void get_tipos_pago()
        {
            DAO_Pago_tipos dao = new DAO_Pago_tipos();
            dgv_tipos_pago.DataSource = dao.get_pago_tipos(null, true, es_tae);
        }

        private void Tipos_pago_KeyDown(object sender, KeyEventArgs e)
        {
            if(Convert.ToInt32(e.KeyCode) == 27)
            {
                this.Close();
            }
        }

        private void Tipos_pago_Shown(object sender, EventArgs e)
        {
            get_tipos_pago();
        }

        private void dgv_tipos_pago_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                var row = dgv_tipos_pago.SelectedRows[0];
                return_pago_tipos = new DTO_Pago_tipos();
                return_pago_tipos.entrega_cambio = Convert.ToBoolean(row.Cells["entrega_cambio"].Value);
                return_pago_tipos.pago_tipo_id = Convert.ToInt64(row.Cells["pago_tipo_id"].Value);
                return_pago_tipos.usa_cuenta = Convert.ToBoolean(row.Cells["usa_cuenta"].Value);
                return_pago_tipos.nombre = row.Cells["nombre"].Value.ToString();
                this.Close();
            }
        }
    }
}
