using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DTO;

namespace Farmacontrol_PDV.FORMS.ventas.elaborar_formula
{
	public partial class Formula_articulos_local : Form
	{
		List<DTO_Detallado_formula_elaborada> detallado = new List<DTO_Detallado_formula_elaborada>();
		public bool terminar_formula = false;

        public Formula_articulos_local(List<DTO_Detallado_formula_elaborada> detallado)
        {
            this.detallado = detallado;
            InitializeComponent();
            dgv_articulos.DataSource = detallado;

            dgv_articulos.DataBindingComplete += (s, e) =>
            {
                if (dgv_articulos.Columns.Contains("cantidad"))
                {
                    dgv_articulos.Columns["cantidad"].DefaultCellStyle.Format = "N2";
                    dgv_articulos.Columns["cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    var tipo = dgv_articulos.Columns["cantidad"].ValueType;
                    
                }
            };
        }


        private void Formula_articulos_local_Shown(object sender, EventArgs e)
		{
			dgv_articulos.ClearSelection();
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

        private void btn_finalizar_Click(object sender, EventArgs e)
        {
            terminar_formula = true;

            foreach (DataGridViewRow row in dgv_articulos.Rows)
            {
                if (row.IsNewRow) continue;

                var cellValue = row.Cells["articulo_usado"].Value;

                if (cellValue == null || !Convert.ToBoolean(cellValue))
                {
                    terminar_formula = false;
                    break;
                }
            }

            if (!terminar_formula)
            {
                MessageBox.Show(this, "Es necesario que los productos aquí mostrados sean los mismos que los usados en la fórmula", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.Close();
            }
        }

    }
}
