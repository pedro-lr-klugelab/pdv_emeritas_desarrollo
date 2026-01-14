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
using Farmacontrol_PDV.FORMS.consultas.consulta_existencias;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.FORMS.consultas.consulta_existencias
{
	public partial class Consulta_existencias : Form
	{
		public Consulta_existencias()
		{
			InitializeComponent();
			get_sucursales();
		}

		private void get_sucursales()
		{
			List<DTO_Sucursal> lista_sucursales = DAO_Sucursales.get_sucursales();
			Dictionary<string, long> dic_sucursales = new Dictionary<string, long>();

			foreach (DTO_Sucursal sucursal in lista_sucursales)
			{
				dic_sucursales.Add(sucursal.nombre, Convert.ToInt64(sucursal.sucursal_id));
			}

			dic_sucursales.Add("GLOBAL", 0);

			cbb_sucursales.DataSource = new BindingSource(dic_sucursales, null);
			cbb_sucursales.DisplayMember = "Key";
			cbb_sucursales.ValueMember = "Value";
		}

		private void txt_buscar_KeyDown(object sender, KeyEventArgs e)
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
					txt_buscar.Text = "";
					break;
				case 13:
					if (txt_buscar.Text.Trim().Length > 0)
					{
						buscar();
					}
					break;
			}
		}

		private void buscar()
		{
			if (txt_buscar.Text.Trim().Length > 0)
			{

				DAO_Sucursales suc = new DAO_Sucursales();

				var sucursal_data = suc.get_sucursal_data(Convert.ToInt32(cbb_sucursales.SelectedValue));

				/*if(Convert.ToInt32(cbb_sucursales.SelectedValue) == 0 || sucursal_data.es_farmacontrol > 0)
				{*/
					var resultados_busqueda = DAO_Articulos.busqueda_articulos_existencias(Convert.ToInt64(cbb_sucursales.SelectedValue), txt_buscar.Text);

					foreach (DataRow ro in resultados_busqueda.Rows)
					{
                        ro["caducidad"] = Misc_helper.fecha(ro["caducidad"].ToString(), "CADUCIDAD");
					}

					dgv_articulos.DataSource = resultados_busqueda;
					dgv_articulos.ClearSelection();
				/*}
				else
				{
					MessageBox.Show(this,"La sucursal seleccionada no cuenta con el sistema actualizado, es imposible obtener su información","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					txt_buscar.Focus();
				}*/
			}
		}

		private void grid_articulos_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
                case 67:
                    if (e.Control)
                    {
                        Clipboard.SetText(dgv_articulos.SelectedRows[0].Cells["amecop"].Value.ToString());
                    }
                break;
				case 27:
					txt_buscar.Focus();
					break;
				case 13:
					consulta_existencias();
					break;
			}
		}

		private void consulta_existencias()
		{
			if (dgv_articulos.SelectedRows.Count > 0)
			{
				long articulo_id = Convert.ToInt64(dgv_articulos.SelectedRows[0].Cells["articulo_id"].Value);
				string amecop = dgv_articulos.SelectedRows[0].Cells["amecop"].Value.ToString().Replace("*", "").Trim();
				string nombre = dgv_articulos.SelectedRows[0].Cells["producto"].Value.ToString();
				Consulta_existencias_sucursales consulta_sucursales = new Consulta_existencias_sucursales(articulo_id, amecop, nombre);
				consulta_sucursales.ShowDialog();
			}
		}

		private void dgv_articulos_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			consulta_existencias();
		}

		private void Consulta_existencias_Shown(object sender, EventArgs e)
		{
			txt_buscar.Focus();
		}

		private void txt_buscar_Enter(object sender, EventArgs e)
		{
			dgv_articulos.ClearSelection();
		}

		private void cbb_sucursales_SelectionChangeCommitted(object sender, EventArgs e)
		{
			buscar();
			txt_buscar.Focus();
		}

		private void dgv_articulos_CellMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e)
		{
			consulta_existencias();
		}

		private void dgv_articulos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			
		}

        private void txt_buscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbb_sucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            buscar();
            txt_buscar.Focus();
        }
	}
}
