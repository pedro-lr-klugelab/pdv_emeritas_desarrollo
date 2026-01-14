using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.FORMS.consultas.consulta_existencias
{
	public partial class Consulta_existencias_sucursales : Form
	{
		private long articulo_id;
		private string amecop;
		private string nombre;

		public Consulta_existencias_sucursales(long articulo_id, string amecop, string nombre)
		{
			this.articulo_id = articulo_id;
			this.amecop = amecop;
			this.nombre = nombre;
			InitializeComponent();
		}

		public void get_informacion_sucursales()
		{
			txt_amecop.Text = amecop;
			txt_nombre.Text = nombre;
			dgv_articulos.DataSource = DAO_Articulos.get_consulta_existencias(articulo_id);
			dgv_articulos.ClearSelection();
			get_total_global();
		}

		private void get_total_global()
		{
			long total = 0;

			foreach (DataGridViewRow row in dgv_articulos.Rows)
			{
				total += Convert.ToInt64(row.Cells["total"].Value);
			}

			lbl_total_global.Text = total.ToString();
		}

		private void Consulta_existencias_sucursales_Shown(object sender, EventArgs e)
		{
			get_informacion_sucursales();
		}

		private void dgv_articulos_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 9)
			{
				long sucursal_id = Convert.ToInt64(dgv_articulos.Rows[e.RowIndex].Cells["sucursal_id"].Value);
				Consulta_existencias_caducidades existencia_caducidades = new Consulta_existencias_caducidades(sucursal_id, articulo_id);
				existencia_caducidades.ShowDialog();
			}
		}
	}
}
