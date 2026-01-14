using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.CLASSES;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.FORMS.movimientos.apartado_mercancia
{
	public partial class Apartado_mercancia_principal : Form
	{
		DAO_Articulos dao_articulos = new DAO_Articulos();
		DAO_Apartado_mercancia dao_apartado = new DAO_Apartado_mercancia();
		BindingList<DTO_Apartado_mercancia> data = new BindingList<DTO_Apartado_mercancia>();
		System.Threading.Timer _timer_busqueda = null;

		public Apartado_mercancia_principal()
		{
			Apartado_mercancia_principal.CheckForIllegalCrossThreadCalls = false;
			InitializeComponent();
		}

		private void Apartado_mercancia_principal_Load(object sender, EventArgs e)
		{
			dgv_apartados.DataSource = data;

			var result = dao_apartado.get_apartados();

			data.Clear();

			foreach(DTO_Apartado_mercancia articulo in result)
			{
				data.Add(articulo);
			}

			dgv_apartados.ClearSelection();

			txt_amecop.Focus();

			_timer_busqueda = new System.Threading.Timer(TimerWork_busqueda, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
		}

		public void TimerWork_busqueda(object obj)
		{
			DAO_Apartado_mercancia dao_apar = new DAO_Apartado_mercancia();
			var result = dao_apar.get_apartados();
			
			if(result.Count != data.Count)
			{
				data.Clear();

				foreach(DTO_Apartado_mercancia articulo in result)
				{
					data.Add(articulo);
				}

				dgv_apartados.Refresh();
				dgv_apartados.ClearSelection();
			}
		}

		public void limpiar_informacion()
		{
			txt_amecop.Enabled = true;
			txt_cantidad.Enabled = false;
			txt_cantidad.Value = 1;

			txt_amecop.Text = "";

			cbb_caducidad.Items.Clear();
			cbb_lote.Items.Clear();

			txt_producto.Text = "";

			txt_amecop.Focus();
		}

		public void rellenar_informacion_producto(DTO_Articulo articulo)
		{

			txt_producto.Text = articulo.Nombre;
			cbb_caducidad.Enabled = true;

			if (articulo.Caducidades.Rows.Count > 0)
			{
				cbb_caducidad.Items.Clear();

				foreach (DataRow row in articulo.Caducidades.Rows)
				{
					ComboBoxItem item = new ComboBoxItem();
                    item.Text = Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD") + " (" + row["existencia"] + ")";
					item.Value = row["caducidad"].ToString();
					item.elemento_id = articulo.Articulo_id;
					cbb_caducidad.Items.Add(item);
				}
			}
			else
			{
				ComboBoxItem item = new ComboBoxItem();
				item.Text = Misc_helper.fecha(null,"CADUCIDAD");
				item.Value = "0000-00-00";
				item.elemento_id = articulo.Articulo_id;

				cbb_caducidad.Items.Add(item);
			}

			cbb_caducidad.DroppedDown = true;
			cbb_caducidad.SelectedIndex = 0;
			cbb_caducidad.Focus();
			txt_amecop.Enabled = false;
		}

        void form_busqueda_producto()
        {
            string amecop = "";

            if (txt_amecop.Text.Trim().Length > 0)
            {
                if (txt_amecop.Text.Substring(0, 1).Equals("?"))
                {
                    amecop = txt_amecop.Text.Substring(1, txt_amecop.Text.Length - 1).Trim();
                }
            }

            Busqueda_productos busqueda_productos = new Busqueda_productos(amecop);
            busqueda_productos.FormClosed += new FormClosedEventHandler(busqueda_productos_Close);
            busqueda_productos.ShowDialog();
            txt_cantidad.Focus();
        }

		public void busqueda_producto()
		{
            if (txt_amecop.Text.Substring(0, 1).Equals("?"))
            {
                form_busqueda_producto();
            }
            else
            {
                DTO_Articulo articulo = dao_articulos.get_articulo(txt_amecop.Text);

                if (articulo.Articulo_id != null)
                {
                    rellenar_informacion_producto(articulo);
                }
                else
                {
                    txt_amecop.Text = "";
                    MessageBox.Show(this, "Producto No encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
		}

		public void busqueda_productos_Close(object sender, FormClosedEventArgs e)
		{
			if (Busqueda_productos.articulo_articulo_id != null)
			{
				txt_amecop.Text = Busqueda_productos.articulo_amecop;
				txt_producto.Text = Busqueda_productos.articulo_producto;

				cbb_caducidad.Items.Add(Busqueda_productos.caducidad_item);
				cbb_lote.Items.Add(Busqueda_productos.lote_item);

				cbb_lote.SelectedIndex = cbb_lote.FindStringExact(Busqueda_productos.lote_item.Text.ToString());
				cbb_caducidad.SelectedIndex = cbb_caducidad.FindStringExact(Busqueda_productos.caducidad_item.Text.ToString());

				cbb_caducidad.Enabled = false;
				cbb_lote.Enabled = false;

				txt_cantidad.Enabled = true;
				txt_cantidad.Value = 1;
				txt_cantidad.Select(0,txt_cantidad.Text.Length);
				txt_cantidad.Focus();
			}
		}

		private void txt_amecop_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 27:
					limpiar_informacion();
				break;
				case 13:
					if(txt_amecop.Text.Trim().Length > 0)
					{
						busqueda_producto();	
					}
				break;
				case 114:
                form_busqueda_producto();
					/*Busqueda_productos busqueda_productos = new Busqueda_productos("");
					busqueda_productos.FormClosed += new FormClosedEventHandler(busqueda_productos_Close);
					busqueda_productos.ShowDialog();
					txt_cantidad.Focus();
                     * */
				break;
				case 40:
				if (dgv_apartados.Rows.Count > 0)
				{
					dgv_apartados.Enabled = true;
					dgv_apartados.Focus();
					dgv_apartados.CurrentCell = dgv_apartados.Rows[0].Cells["c_amecop"];
					dgv_apartados.Rows[0].Selected = true;
				}
				break;
			}
		}

		private void cbb_caducidad_KeyDown(object sender, KeyEventArgs e)
		{
			if (Convert.ToInt32(e.KeyCode) == 13)
			{
				ComboBoxItem item = (ComboBoxItem)cbb_caducidad.SelectedItem;
				busqueda_lotes(item);
				cbb_caducidad.Text = item.Text;
				cbb_caducidad.Enabled = false;
			}
			else if (Convert.ToInt32(e.KeyCode) == 27)
			{
				txt_amecop.Enabled = true;
				txt_amecop.Focus();
				txt_producto.Text = "";
				cbb_caducidad.Items.Clear();
				cbb_caducidad.Enabled = false;
			}
		}

		private void cbb_lote_KeyDown(object sender, KeyEventArgs e)
		{
			if (Convert.ToInt32(e.KeyCode) == 13)
			{
				ComboBoxItem item = (ComboBoxItem)cbb_lote.SelectedItem;
				txt_cantidad.Enabled = true;
				txt_cantidad.Value = 1;
                txt_cantidad.Select(0, txt_cantidad.Text.Length);
				txt_cantidad.Focus();

				cbb_lote.Text = item.Text;
				cbb_lote.Enabled = false;
			}
			else if (Convert.ToInt32(e.KeyCode) == 27)
			{
				cbb_caducidad.Enabled = true;
				cbb_caducidad.Focus();
				cbb_caducidad.DroppedDown = true;
				cbb_lote.Items.Clear();
				cbb_lote.Enabled = false;

			}
		}

		public void busqueda_lotes(ComboBoxItem item)
		{
			DataTable result_lotes = dao_articulos.get_lotes((int)item.elemento_id, item.Value.ToString());
			cbb_lote.Enabled = true;

			if (result_lotes.Rows.Count > 0)
			{
				cbb_lote.Items.Clear();
				foreach (DataRow row in result_lotes.Rows)
				{
					ComboBoxItem item_lote = new ComboBoxItem();
					item_lote.Text = row["lote"].ToString();
					item_lote.Value = row["lote"].ToString();
					item.elemento_id = item.elemento_id;
					cbb_lote.Items.Add(item_lote);
				}
			}
			else
			{
				ComboBoxItem item_lote = new ComboBoxItem();
				item_lote.Text = "SIN LOTE";
				item_lote.Value = " ";
				item.elemento_id = item.elemento_id;
				cbb_lote.Items.Add(item_lote);
			}

			cbb_lote.DroppedDown = true;
			cbb_lote.SelectedIndex = 0;
			cbb_lote.Focus();
		}

		private void txt_cantidad_KeyDown(object sender, KeyEventArgs e)
		{
			if (Convert.ToInt32(e.KeyCode) == 27)
			{
				limpiar_informacion();
			}

			if (Convert.ToInt32(e.KeyCode) == 13 && txt_cantidad.Value > 0)
			{
				//validar_producto_apartado();
                insertar_producto_apartado();
			}
		}

		public void validar_producto_apartado()
		{
			Destino_apartado destino_apartado = new Destino_apartado();
			destino_apartado.ShowDialog();

			if(destino_apartado.sucursal_id != null)
			{
				if(!destino_apartado.tipo.Equals(""))
				{
					//insertar_producto_apartado(null,destino_apartado.tipo);
				}
				else
				{
					//insertar_producto_apartado(destino_apartado.sucursal_id,"SUCURSAL");
				}
			}
		}

		public void insertar_producto_apartado()
		{
			ComboBoxItem item_cad = (ComboBoxItem)cbb_caducidad.SelectedItem;
			ComboBoxItem item_lote = (ComboBoxItem)cbb_lote.SelectedItem;

			int articulo_id = (int)item_cad.elemento_id;

			int existencia_vendible = dao_articulos.get_existencia_vendible(txt_amecop.Text, item_cad.Value.ToString(), item_lote.Value.ToString());

			if (existencia_vendible >= Convert.ToInt64(txt_cantidad.Value))
			{
                Destino_apartado destino_apartado = new Destino_apartado();
                destino_apartado.ShowDialog();

                if (destino_apartado.sucursal_id != null)
                {
                    if (!destino_apartado.tipo.Equals(""))
                    {
                        dgv_apartados.DataSource = dao_apartado.agregar_producto_apartado_mercancia(articulo_id, item_cad.Value.ToString(), item_lote.Value.ToString(), Convert.ToInt64(txt_cantidad.Value), destino_apartado.tipo, null);
                        limpiar_informacion();
                    }
                    else
                    {
                        dgv_apartados.DataSource = dao_apartado.agregar_producto_apartado_mercancia(articulo_id, item_cad.Value.ToString(), item_lote.Value.ToString(), Convert.ToInt64(txt_cantidad.Value), "SUCURSAL", destino_apartado.sucursal_id);
                        limpiar_informacion();
                    }
                }
			}
			else
			{
				MessageBox.Show(this, "La cantidad disponible para separar es de " + existencia_vendible, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void Apartado_mercancia_principal_Shown(object sender, EventArgs e)
		{
			txt_amecop.Focus();
		}

		private void txt_amecop_Enter(object sender, EventArgs e)
		{
			dgv_apartados.ClearSelection();
		}

		private void dgv_apartados_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
                case 67:
                if (e.Control)
                {
                    Clipboard.SetText(dgv_apartados.SelectedRows[0].Cells["c_amecop"].Value.ToString());
                }
                break;
				case 27:
					txt_amecop.Focus();
				break;
				case 46:
				if (dgv_apartados.SelectedRows.Count > 0)
				{
					//if (dgv_apartados.SelectedRows[0].Cells["c_destino_sin_formato"].Value.ToString().Equals("SUCURSAL") || dgv_apartados.SelectedRows[0].Cells["c_destino_sin_formato"].Value.ToString().Equals("CAMBIO_FISICO"))
					//{
					    long detallado_apartado_id = Convert.ToInt64(dgv_apartados.SelectedRows[0].Cells["apartado_id"].Value);
						dgv_apartados.DataSource = dao_apartado.eliminar_apartado(detallado_apartado_id);
						txt_amecop.Focus();
						dgv_apartados.ClearSelection();
					//}
				}
				break;
			}
		}

		private void Apartado_mercancia_principal_FormClosing(object sender, FormClosingEventArgs e)
		{
			_timer_busqueda.Dispose();
		}

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbb_caducidad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

	}
}
