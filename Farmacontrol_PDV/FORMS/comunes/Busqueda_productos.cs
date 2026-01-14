using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.FORMS.comunes
{
    public partial class Busqueda_productos : Form
    {
		public static string	articulo_amecop					= null;
		public static string	articulo_producto				= null;
		public static int?		articulo_articulo_id			= null;
		public static string	articulo_caducidad				= null;
		public static string	articulo_lote					= null;
		public static int?		articulo_existencia_vendible	= null;
		public static CLASSES.ComboBoxItem caducidad_item		= null;
		public static CLASSES.ComboBoxItem lote_item			= null;
        public static decimal pct_descuento = 0;

		private bool sin_restriccion = false;

		BindingList<Busqueda_articulos_existencias> data = new BindingList<Busqueda_articulos_existencias>();

		System.Threading.Timer _timer_busqueda = null;
		string busqueda = "";
		bool busqueda_inactivos = false;

        public Busqueda_productos(string busqueda)
        {
            InitializeComponent();
			Busqueda_productos.CheckForIllegalCrossThreadCalls = false;
			articulo_amecop					= null;
			articulo_producto				= null;
			articulo_articulo_id			= null;
			articulo_caducidad				= null;
			articulo_lote					= null;
			articulo_existencia_vendible	= null;
			caducidad_item					= null;
			lote_item						= null;

			dgv_articulos.LostFocus += new EventHandler(dgv_articulos_LostFocus);
			dgv_articulos.GotFocus += new EventHandler(dgv_articulos_GotFocus);
			dgv_articulos.DataSource = data;

            txt_nombre_producto.Text = busqueda;

            progBarBusqueda.Show();//
            progBarBusqueda.Value = 50;//

			txt_nombre_producto.GotFocus += new EventHandler(txt_nombre_producto_GotFocus);
			_timer_busqueda = new System.Threading.Timer(TimerWork_busqueda, null, TimeSpan.Zero, TimeSpan.FromSeconds(50));
        }

		public Busqueda_productos(bool sin_restriccion, string busqueda = "")
		{
			this.sin_restriccion = sin_restriccion;
			InitializeComponent();

			articulo_amecop = null;
			articulo_producto = null;
			articulo_articulo_id = null;
			articulo_caducidad = null;
			articulo_lote = null;
			articulo_existencia_vendible = null;
			caducidad_item = null;
			lote_item = null;

            progBarBusqueda.Show();//
            progBarBusqueda.Value = 50;//
            

			dgv_articulos.LostFocus += new EventHandler(dgv_articulos_LostFocus);
			dgv_articulos.GotFocus += new EventHandler(dgv_articulos_GotFocus);
			txt_nombre_producto.GotFocus += new EventHandler(txt_nombre_producto_GotFocus);

			dgv_articulos.DataSource = data;

            if(busqueda != "")
            {
                txt_nombre_producto.Text = busqueda;
            }

			txt_nombre_producto.GotFocus += new EventHandler(txt_nombre_producto_GotFocus);
			_timer_busqueda = new System.Threading.Timer(TimerWork_busqueda, null, TimeSpan.Zero, TimeSpan.FromSeconds(50));
		}

        void validar_grid_colores()
        {
            foreach (DataGridViewRow row in dgv_articulos.Rows)
            {
                if (Convert.ToBoolean(row.Cells["es_antibiotico"].Value))
                {
                    dgv_articulos.Rows[row.Index].DefaultCellStyle.BackColor = Color.FromArgb(179, 206, 252);
                }
                else
                {
                    dgv_articulos.Rows[row.Index].DefaultCellStyle.BackColor = Color.Empty;
                }
            }
        }

		public void DoWork_busqueda(object objeto)
		{
            try
            {
                dgv_articulos.AutoGenerateColumns = false;

                if (busqueda.Trim() != "")
                {
                    DAO_Articulos dao_articulos = new DAO_Articulos();
                    var result = dao_articulos.get_articulos_data(busqueda, busqueda_inactivos);

                    if (result.Count == data.Count)
                    {
                        foreach (Busqueda_articulos_existencias art in data)
                        {
                            foreach (Busqueda_articulos_existencias art_re in result)
                            {
                                if (art.articulo_id == art_re.articulo_id && art.caducidad == art_re.caducidad && art.lote == art_re.lote)
                                {
                                    bool existe_cambio = false;

                                    if (art.existencia_apartados != art_re.existencia_apartados)
                                    {
                                        existe_cambio = true;
                                        art.existencia_apartados = art_re.existencia_apartados;
                                    }

                                    if (art.existencia_cambio_fisico != art_re.existencia_cambio_fisico)
                                    {
                                        existe_cambio = true;
                                        art.existencia_cambio_fisico = art_re.existencia_cambio_fisico;
                                    }

                                    if (art.existencia_devoluciones != art_re.existencia_devoluciones)
                                    {
                                        existe_cambio = true;
                                        art.existencia_devoluciones = art_re.existencia_devoluciones;
                                    }

                                    if (art.existencia_mayoreo != art_re.existencia_mayoreo)
                                    {
                                        existe_cambio = true;
                                        art.existencia_mayoreo = art_re.existencia_mayoreo;
                                    }

                                    if (art.existencia_mayoreo != art_re.existencia_mayoreo)
                                    {
                                        existe_cambio = true;
                                        art.existencia_mayoreo = art_re.existencia_mayoreo;
                                    }

                                    if (art.existencia_mermas != art_re.existencia_mermas)
                                    {
                                        existe_cambio = true;
                                        art.existencia_mermas = art_re.existencia_mermas;
                                    }

                                    if (art.existencia_prepago != art_re.existencia_prepago)
                                    {
                                        existe_cambio = true;
                                        art.existencia_prepago = art_re.existencia_prepago;
                                    }

                                    if (art.existencia_total != art_re.existencia_total)
                                    {
                                        existe_cambio = true;
                                        art.existencia_total = art_re.existencia_total;
                                    }

                                    if (art.existencia_traspasos != art_re.existencia_traspasos)
                                    {
                                        existe_cambio = true;
                                        art.existencia_traspasos = art_re.existencia_traspasos;
                                    }

                                    if (art.existencia_vendible != art_re.existencia_vendible)
                                    {
                                        existe_cambio = true;
                                        art.existencia_vendible = art_re.existencia_vendible;
                                    }

                                    if (art.existencia_ventas != art_re.existencia_ventas)
                                    {
                                        existe_cambio = true;
                                        art.existencia_ventas = art_re.existencia_ventas;
                                    }

                                    if (existe_cambio)
                                    {
                                        dgv_articulos.Refresh();
                                    }

                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        //busqueda_activa = false;

                        if (data.Count > 0)
                        {
                            data.Clear();
                        }
                        
                        /*if (result.Count > 0)
                        {*/
                            foreach (Busqueda_articulos_existencias art in result)
                            {
                                data.Add(art);
                            }
                        //}

                        //busqueda_activa = true;
                    }

                    foreach (DataGridViewRow row in dgv_articulos.Rows)
                    {
                        int activo = Convert.ToInt32(dgv_articulos.Rows[row.Index].Cells["c_activo"].Value);

                        if (activo == 0)
                        {
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 218, 218);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Log_error.log(ex);
            }
		}

		public void TimerWork_busqueda(object obj)
		{
			try
			{
				DoWork_busqueda(this);
			}
			catch (Exception ex)
			{
				Log_error.log(ex);
			}
		}

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        public void buscar()
        {
            progBarBusqueda.Show();//
            progBarBusqueda.Value = 50;//
            try
            {
                dgv_articulos.AutoGenerateColumns = false;
                DAO.DAO_Articulos dao_articulos = new DAO.DAO_Articulos();
                busqueda = txt_nombre_producto.Text;
                busqueda_inactivos = chb_inactivos.Checked;

                var result = dao_articulos.get_articulos_data(txt_nombre_producto.Text.ToString(), chb_inactivos.Checked);
                progBarBusqueda.Value = 70;
                
                data.Clear();

                foreach (Busqueda_articulos_existencias art in result)
                {
                    data.Add(art);
                }

                lbl_time.Text = String.Format("Tiempo de consulta: {0} milisegundos.", dao_articulos.execution_time.ToString());
                dgv_articulos.ClearSelection();

                foreach (DataGridViewRow row in dgv_articulos.Rows)
                {
                    int activo = Convert.ToInt32(dgv_articulos.Rows[row.Index].Cells["c_activo"].Value);

                    if (activo == 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 218, 218);
                    }
                }
                progBarBusqueda.Value = 90;
                validar_grid_colores();
                progBarBusqueda.Value = 100;
                progBarBusqueda.Hide();
            }
            catch(Exception ex)
            {
                Log_error.log(ex);
            }
        }

        private void txt_nombre_producto_KeyDown(object sender, KeyEventArgs e)
        {
			int keyCode = Convert.ToInt32(e.KeyCode);
			
			switch(keyCode)
			{
				case 13:
                    if (txt_nombre_producto.TextLength > 3)
                    {
                        buscar();
                    }
                    else
                    {
                        MessageBox.Show(this, "Busqueda incompleta por falta de información ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
				break;
				case 27:
				if (txt_nombre_producto.Text.Equals(""))
					{
                        _timer_busqueda.Dispose();
						this.Close();
					}
					else
					{
						txt_nombre_producto.Text = "";
					}
				break;
				case 40:
					if(dgv_articulos.RowCount > 0)
					{
						dgv_articulos.Focus();
					}
				break;
				default:
				break;
			}
        }

		private void txt_nombre_producto_GotFocus(object sender, EventArgs e)
		{
			txt_nombre_producto.SelectAll();
		}

		private void dgv_articulos_LostFocus(object sender, EventArgs e)
		{
			dgv_articulos.ClearSelection();
		}

		private void dgv_articulos_GotFocus(object sender, EventArgs e)
		{
			dgv_articulos.Rows[0].Selected = true;
		}

		private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			Log_error.log(e.Exception);
		}

		private void dgv_articulos_KeyDown(object sender, KeyEventArgs e)
		{
            var keycode = Convert.ToInt32(e.KeyCode);

            switch(keycode)
            {
                case 67:
                    if(e.Control)
                    {
                        Clipboard.SetText(dgv_articulos.SelectedRows[0].Cells["amecop"].Value.ToString());
                    }
                break;
                case 13:
                    seleccionar_producto();
                break;
                case 27:
                    txt_nombre_producto.Focus();
                break;
            }
		}

        void seleccionar_producto()
        {
            try
            {
                if (dgv_articulos.SelectedRows.Count > 0)
                {
                    if (sin_restriccion || Convert.ToInt32(dgv_articulos.Rows[dgv_articulos.CurrentRow.Index].Cells["existencia_vendible"].Value) > 0)
                    {
                        articulo_amecop = dgv_articulos.Rows[dgv_articulos.CurrentRow.Index].Cells["amecop"].Value.ToString().Trim('*').Trim();
                        articulo_producto = dgv_articulos.Rows[dgv_articulos.CurrentRow.Index].Cells["producto"].Value.ToString();
                        articulo_articulo_id = int.Parse(dgv_articulos.Rows[dgv_articulos.CurrentRow.Index].Cells["articulo_id"].Value.ToString());
                        articulo_caducidad = dgv_articulos.Rows[dgv_articulos.CurrentRow.Index].Cells["caducidad_sin_formato"].Value.ToString();
                        articulo_lote = dgv_articulos.Rows[dgv_articulos.CurrentRow.Index].Cells["lote"].Value.ToString();
                        articulo_existencia_vendible = int.Parse(dgv_articulos.Rows[dgv_articulos.CurrentRow.Index].Cells["existencia_vendible"].Value.ToString());
                        pct_descuento = Convert.ToDecimal(dgv_articulos.Rows[dgv_articulos.CurrentRow.Index].Cells["col_pct_descuento"].Value.ToString());

                        caducidad_item = new CLASSES.ComboBoxItem();
                        caducidad_item.Text = dgv_articulos.Rows[dgv_articulos.CurrentRow.Index].Cells["caducidad"].Value.ToString();

                        caducidad_item.Value = Misc_helper.CadtoDate(dgv_articulos.Rows[dgv_articulos.CurrentRow.Index].Cells["caducidad"].Value.ToString());
                        caducidad_item.elemento_id = int.Parse(dgv_articulos.Rows[dgv_articulos.CurrentRow.Index].Cells["articulo_id"].Value.ToString());

                        lote_item = new CLASSES.ComboBoxItem();
                        lote_item.Text = dgv_articulos.Rows[dgv_articulos.CurrentRow.Index].Cells["lote"].Value.ToString();
                        lote_item.Value = dgv_articulos.Rows[dgv_articulos.CurrentRow.Index].Cells["lote"].Value.ToString();
                        lote_item.elemento_id = int.Parse(dgv_articulos.Rows[dgv_articulos.CurrentRow.Index].Cells["articulo_id"].Value.ToString());

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(this, "No tienes existencias para este producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exception)
            {
                Log_error.log(exception);
            }
        }


		private void chb_inactivos_CheckedChanged(object sender, EventArgs e)
		{
			buscar();
			txt_nombre_producto.Focus();
		}

		private void rastrear_piezas_uso_Click(object sender, EventArgs e)
		{
			if(dgv_articulos.SelectedRows.Count > 0)
			{
				DTO_Articulo_generic articulo = new DTO_Articulo_generic();
				articulo.articulo_id = Convert.ToInt64(dgv_articulos.SelectedRows[0].Cells["articulo_id"].Value);
				articulo.amecop = dgv_articulos.SelectedRows[0].Cells["amecop"].Value.ToString();
				articulo.producto = dgv_articulos.SelectedRows[0].Cells["producto"].Value.ToString();
				articulo.caducidad = Misc_helper.CadtoDate(dgv_articulos.SelectedRows[0].Cells["caducidad"].Value.ToString());
				articulo.lote = dgv_articulos.SelectedRows[0].Cells["lote"].Value.ToString();

				Rastreo_piezas rastreo = new Rastreo_piezas(articulo);
				rastreo.ShowDialog();
			}
			else
			{
				MessageBox.Show(this,"Es necesario tener seleccionado algun producto","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		private void Busqueda_productos_FormClosing(object sender, FormClosingEventArgs e)
		{
            txt_nombre_producto.Text = "";
			_timer_busqueda.Dispose();
		}

		private void dgv_articulos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
            try
            {
                if (e.ColumnIndex != dgv_articulos.Columns["es_antibiotico"].Index)
                {
                    if (e.Value.ToString().Equals("0"))
                    {
                        e.Value = "-";
                    }
                }   
            }
            catch(Exception ex)
            {
                Log_error.log(ex);
            }
		}

        private void Busqueda_productos_Shown(object sender, EventArgs e)
        {
            progBarBusqueda.Show();
            progBarBusqueda.Value = 40;
            if(!txt_nombre_producto.Text.Trim().Equals(""))
            {
                buscar();
            }
        }

        private void txt_nombre_producto_TextChanged(object sender, EventArgs e)
        {

        }

        private void Busqueda_productos_Load(object sender, EventArgs e)
        {

        }

        private void dgv_articulos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
