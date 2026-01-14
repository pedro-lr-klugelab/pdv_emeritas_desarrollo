using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.FORMS.consultas.cambios_precio
{
	public partial class Cambios_precio_principal : Form
	{
		public Cambios_precio_principal()
		{
			InitializeComponent();
			get_cambio_precio();
		}

		void get_cambio_precio()
		{
			DAO_Cambio_precios dao = new DAO_Cambio_precios();
			dgv_cambio_precios.DataSource = dao.get_cambio_precios();
			dgv_cambio_precios.ClearSelection();
		}

		private void dgv_cambio_precios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if(dgv_cambio_precios.SelectedRows.Count > 0)
			{
				long cambio_precio_id = Convert.ToInt64(dgv_cambio_precios.SelectedRows[0].Cells["cambio_precio_id"].Value);
				Cambios_precio_informacion info = new Cambios_precio_informacion(cambio_precio_id);
				info.ShowDialog();
			}
		}
	}
}
