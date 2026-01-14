using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.FORMS.consultas.faltantes
{
	public partial class Faltantes_principal : Form
	{
		public Faltantes_principal()
		{
			InitializeComponent();
		}

		void get_faltantes()
		{
			dgv_reportes_faltantes.DataSource = DAO_Faltantes.get_faltantes();
			dgv_reportes_faltantes.ClearSelection();
		}

		private void Faltantes_principal_Shown(object sender, EventArgs e)
		{
			get_faltantes();
		}

		private void dgv_reportes_faltantes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if(dgv_reportes_faltantes.SelectedRows.Count > 0)
			{
				long reporte_faltantes_id = Convert.ToInt64(dgv_reportes_faltantes.SelectedRows[0].Cells["reporte_faltantes_id"].Value);
				Faltantes_sucursal faltantes_sucursal = new Faltantes_sucursal(reporte_faltantes_id);
				faltantes_sucursal.ShowDialog();
			}
			else
			{
				MessageBox.Show(this,"Es necesario seleccionar almenos un reporte a consultar","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}
	}
}
