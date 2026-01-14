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
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.FORMS.comunes;

namespace Farmacontrol_PDV.FORMS.consultas.desplazamientos
{
	public partial class Desplazamiento_principal : Form
	{
		DAO_Sectores dao_sectores = new DAO_Sectores();

		public Desplazamiento_principal()
		{
			InitializeComponent();
			get_sucursales();
			//get_sectores();
			validacion_tiempo();

			txt_amecop.Focus();
		}

		private void validacion_tiempo()
		{
			var today = DateTime.Today;
			var month = new DateTime(today.Year, today.Month, 1);
			var first = month.AddMonths(-1);
			dtp_inicial.Value = first;
		}

		private void get_sectores()
		{
			var sectores = dao_sectores.get_sectores();

			Dictionary<string,long> dic_secotres = new Dictionary<string,long>();

			foreach(var item in sectores)
			{
				dic_secotres.Add(item.nombre,item.sector_id);
			}

            if(dic_secotres.Count > 0)
            {
                cbb_sectores.DataSource = new BindingSource(dic_secotres, null);
                cbb_sectores.DisplayMember = "Key";
                cbb_sectores.ValueMember = "Value";
            }
		}

		private void get_sucursales()
		{
			long sucursal_id = Convert.ToInt64(Config_helper.get_config_local("sucursal_id"));

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

			cbb_sucursales.SelectedValue = sucursal_id;
		}

		private void Desplazamiento_principal_Shown(object sender, EventArgs e)
		{
            get_sectores();
			txt_amecop.Focus();
		}

		private void txt_amecop_MouseEnter(object sender, EventArgs e)
		{
			dgv_desplazamientos.ClearSelection();
		}

		public void busqueda_productos_Close(object sender, FormClosedEventArgs e)
		{
			if (Busqueda_productos.articulo_articulo_id != null)
			{
				txt_amecop.Text = Busqueda_productos.articulo_amecop.Trim('*').Trim();
				txt_amecop.Focus();
			}
		}

		private void txt_amecop_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 114:
					Busqueda_productos busqueda_productos = new Busqueda_productos("");
					busqueda_productos.FormClosed += new FormClosedEventHandler(busqueda_productos_Close);
					busqueda_productos.ShowDialog();
					txt_amecop.Focus();
				break;
				case 27:
					txt_amecop.Text = "";
				break;
				case 13:
					if(txt_amecop.Text.Trim().Length > 0)
					{
						bool existe_amecop = false;

						foreach(DataGridViewRow row in dgv_desplazamientos.Rows)
						{
							if(row.Cells["amecop"].Value.ToString().Equals(txt_amecop.Text))
							{
								existe_amecop = true;
								break;
							}
						}

						if(existe_amecop)
						{
							MessageBox.Show(this,"Producto ya registrado","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
						}
						else
						{
							buscar_amecop();
						}
					}
				break;
			}
		}

		private void buscar_sector()
		{
			DAO_Articulos dao_articulos = new DAO_Articulos();

			List<DTO_Desplazamientos_item> desplazamientos = new List<DTO_Desplazamientos_item>();

			string articulos_ids = dao_sectores.get_articulos_sectores(Convert.ToInt64(cbb_sectores.SelectedValue));

			if(!articulos_ids.Equals(""))
			{
				DAO_Sucursales suc = new DAO_Sucursales();

				var sucursal_data = suc.get_sucursal_data(Convert.ToInt32(cbb_sucursales.SelectedValue));

				if(sucursal_data.es_farmacontrol == 1)
				{

					desplazamientos = DAO_Ventas.get_desplazamientos(articulos_ids, dtp_inicial.Value.ToString("yyyy-MM-dd"), dtp_final.Value.ToString("yyyy-MM-dd"), Convert.ToInt64(cbb_sucursales.SelectedValue));

					foreach (var item in desplazamientos)
					{
						bool existe_amecop = false;

						foreach (DataGridViewRow row in dgv_desplazamientos.Rows)
						{
							if (row.Cells["amecop"].Value.ToString().Equals(item.amecop))
							{
								row.Cells["existencia"].Value = item.existencia;
								row.Cells["ventas"].Value = item.ventas;
								row.Cells["prox_cad"].Value = item.prox_cd;

								existe_amecop = true;
								break;
							}
						}

						if (!existe_amecop)
						{
							dgv_desplazamientos.Rows.Add(item.articulo_id, item.amecop, item.producto, item.existencia, item.ventas, item.prox_cd);
						}
					}
				}
				else
				{
					MessageBox.Show(this,"La sucursal seleccionada no cuenta con el sistema actualizado, es imposible obtener su información","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				}

				dgv_desplazamientos.ClearSelection();
				txt_amecop.Text = "";
				txt_amecop.Focus();
			}	
			else
			{
				MessageBox.Show(this,"Sector sin productos","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				txt_amecop.Focus();
			}
		}

		private void buscar_amecop()
		{
			DAO_Articulos dao_articulos = new DAO_Articulos();

			List<DTO_Desplazamientos_item> desplazamientos = new List<DTO_Desplazamientos_item>();

			var result = dao_articulos.get_articulo(txt_amecop.Text);

			if(result.Articulo_id > 0)
			{
				DAO_Sucursales suc = new DAO_Sucursales();

				var sucursal_data = suc.get_sucursal_data(Convert.ToInt32(cbb_sucursales.SelectedValue));

				if(sucursal_data.es_farmacontrol == 1)
				{
					desplazamientos = DAO_Ventas.get_desplazamientos(Convert.ToInt64(result.Articulo_id), dtp_inicial.Value.ToString("yyyy-MM-dd"), dtp_final.Value.ToString("yyyy-MM-dd"), Convert.ToInt64(cbb_sucursales.SelectedValue));

					foreach (var item in desplazamientos)
					{
						dgv_desplazamientos.Rows.Add(item.articulo_id, item.amecop, item.producto, item.existencia, item.ventas, item.prox_cd);
					}

					dgv_desplazamientos.ClearSelection();
				}
				else
				{
					MessageBox.Show(this,"La sucursal seleccionada no cuenta con el sistema actualizado, es imposible obtener su información","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				}

				txt_amecop.Text = "";
				txt_amecop.Focus();	
			}
			else
			{
				MessageBox.Show(this,"Producto no encontrado","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				txt_amecop.Focus();
			}
		}

		private void btn_listado_Click(object sender, EventArgs e)
		{
			buscar_sector();
		}

		private void btn_limpiar_Click(object sender, EventArgs e)
		{
			dgv_desplazamientos.Rows.Clear();
			dgv_desplazamientos.ClearSelection();
			txt_amecop.Focus();
		}

		private void cbb_sectores_SelectionChangeCommitted(object sender, EventArgs e)
		{
			
		}

		private void actualizar_cambios()
		{
			DAO_Sucursales suc = new DAO_Sucursales();

			var sucursal_data = suc.get_sucursal_data(Convert.ToInt32(cbb_sucursales.SelectedValue));

			if(sucursal_data.es_farmacontrol == 1)
			{
				string articulos_ids = "";

				if (dgv_desplazamientos.Rows.Count > 0)
				{
					foreach (DataGridViewRow row in dgv_desplazamientos.Rows)
					{
						if (articulos_ids.Equals(""))
						{
							articulos_ids += row.Cells["articulo_id"].Value.ToString();
						}
						else
						{
							articulos_ids += "," + row.Cells["articulo_id"].Value.ToString();
						}
					}


					List<DTO_Desplazamientos_item> desplazamientos = DAO_Ventas.get_desplazamientos(articulos_ids, dtp_inicial.Value.ToString("yyyy-MM-dd"), dtp_final.Value.ToString("yyyy-MM-dd"), Convert.ToInt64(cbb_sucursales.SelectedValue));

					foreach (var item in desplazamientos)
					{
						foreach (DataGridViewRow row in dgv_desplazamientos.Rows)
						{
							if (row.Cells["amecop"].Value.ToString().Equals(item.amecop))
							{
								row.Cells["existencia"].Value = item.existencia;
								row.Cells["ventas"].Value = item.ventas;
								row.Cells["prox_cad"].Value = item.prox_cd;
								break;
							}
						}
					}

				}
			}
			else
			{
				MessageBox.Show(this,"La sucursal seleccionada no cuenta con el sistema actualizado, es imposible obtener su información","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		private void cbb_sucursales_SelectionChangeCommitted(object sender, EventArgs e)
		{
			actualizar_cambios();
		}

		private void dtp_inicial_CloseUp(object sender, EventArgs e)
		{
			actualizar_cambios();
		}

		private void dtp_final_CloseUp(object sender, EventArgs e)
		{
			actualizar_cambios();
		}

        private void dgv_desplazamientos_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch(keycode)
            {
                case 67:
                if (e.Control)
                {
                    Clipboard.SetText(dgv_desplazamientos.SelectedRows[0].Cells["amecop"].Value.ToString());
                }
                break;
            }
        }
	}
}
