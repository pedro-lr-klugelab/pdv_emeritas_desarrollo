using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.FORMS.comunes;

namespace Farmacontrol_PDV.FORMS.catalogos.kardex_articulos
{
	public partial class Kardex_articulos_principal : Form
	{
		DAO_Articulos dao_articulos = new DAO_Articulos();
		DTO_Articulo dto_articulo = new DTO_Articulo();
		
		long numero_filas_articulo = 0;
		long pagina_actual = 0;
		long total_paginas = 0;

		public void paginacion_kardex()
		{
			dto_articulo = dao_articulos.get_articulo(txt_amecop.Text);

			if (dto_articulo.Articulo_id != null)
			{
                buscar();	
			}
			else
			{
				txt_nombre.Text = "";
				dgv_kardex.DataSource = null;
				bloqueo_paginacion();
				MessageBox.Show(this, "Código de producto no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

            txt_amecop.Text = "";
		}

        void buscar()
        {
            txt_nombre.Text = dto_articulo.Nombre;
            numero_filas_articulo = dao_articulos.get_num_rows_kardex((int)dto_articulo.Articulo_id);
            decimal numero_paginas_actual = Convert.ToDecimal(numero_filas_articulo) / 100;
            total_paginas = Convert.ToInt64(Math.Ceiling(numero_paginas_actual));
            txt_pagina_actual.Text = "" + 1;
            pagina_actual = 0;
            lbl_total_paginas.Text = "" + total_paginas;

            dgv_kardex.DataSource = dao_articulos.get_kardex_articulo((int)dto_articulo.Articulo_id, pagina_actual);

            dgv_kardex.ClearSelection();
            bloqueo_paginacion(false);

            colorear_rows();
        }

		public Kardex_articulos_principal()
		{
			InitializeComponent();
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

            Busqueda_productos busqueda_productos = new Busqueda_productos(true,amecop);
            busqueda_productos.FormClosed += new FormClosedEventHandler(busqueda_productos_Close);
            busqueda_productos.ShowDialog();
            txt_amecop.Focus();
        }

        public void busqueda_producto()
        {
            if (txt_amecop.Text.Substring(0, 1).Equals("?"))
            {
                form_busqueda_producto();
            }
            else
            {
                paginacion_kardex();
            }
        }

        public void busqueda_productos_Close(object sender, FormClosedEventArgs e)
        {
            if (Busqueda_productos.articulo_articulo_id != null)
            {
                txt_amecop.Text = Busqueda_productos.articulo_amecop;
                txt_nombre.Text = Busqueda_productos.articulo_producto;
                paginacion_kardex();
            }
        }

		private void txt_amecop_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(txt_amecop.Text.Trim().Length > 0)
					{
                        busqueda_producto();
					}
				break;
				case 27:
					txt_nombre.Text = "";
					txt_amecop.Text = "";
					dgv_kardex.AutoGenerateColumns = false;
					dgv_kardex.DataSource = null;
					bloqueo_paginacion();
				break;
			}
		}

		public void bloqueo_paginacion(bool bloquear = true)
		{
			btn_atras.Enabled = (bloquear) ? false : true ;
			btn_siguiente.Enabled = (bloquear) ? false : true;
			btn_inicio.Enabled = (bloquear) ? false : true;
			btn_fin.Enabled = (bloquear) ? false : true;
			txt_pagina_actual.Enabled = (bloquear) ? false : true;

			if(bloquear)
			{
				txt_pagina_actual.Text = "";
				lbl_total_paginas.Text = "";
			}
		}

		private void dgv_kardex_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
            /*
			if(dgv_kardex.Rows.Count > 0)
			{
				if (dgv_kardex.Columns["c_cantidad"].Index == e.ColumnIndex)
				{
					if (Convert.ToInt32(e.Value) > 0)
					{
                        dgv_kardex.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 218, 218);
					}
					else
					{
                        dgv_kardex.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(210, 246, 206);
					}

                    if(Convert.ToBoolean(dgv_kardex.Rows[e.RowIndex].Cells["es_importado"].Value))
                    {
                        dgv_kardex.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(187, 214, 242);
                        Console.WriteLine("OK");
                    }
				}
			}
             * */
		}

        void colorear_rows()
        {
            if (dgv_kardex.Rows.Count > 0)
            {
                foreach(DataGridViewRow row in dgv_kardex.Rows)
                {
                    if (Convert.ToInt32(row.Cells["c_cantidad"].Value) > 0)
                    {
                        dgv_kardex.Rows[row.Index].DefaultCellStyle.BackColor = Color.FromArgb(255, 218, 218);
                    }
                    else
                    {
                        dgv_kardex.Rows[row.Index].DefaultCellStyle.BackColor = Color.FromArgb(210, 246, 206);
                    }

                    if (Convert.ToBoolean(dgv_kardex.Rows[row.Index].Cells["es_importado"].Value))
                    {
                        dgv_kardex.Rows[row.Index].DefaultCellStyle.BackColor = Color.FromArgb(187, 214, 242);
                    }
                }
            }
        }

		private void txt_amecop_Enter(object sender, EventArgs e)
		{
			dgv_kardex.ClearSelection();
		}

		private void btn_siguiente_Click(object sender, EventArgs e)
		{
			var pagina = pagina_actual + 1;

			if(pagina_actual < (total_paginas - 1))
			{
				long cantidad_inicio = pagina * 100;

				if (cantidad_inicio >= 0)
				{
					pagina_actual = pagina;
					txt_pagina_actual.Text = "" + (pagina_actual + 1);
					dgv_kardex.DataSource = dao_articulos.get_kardex_articulo((int)dto_articulo.Articulo_id, cantidad_inicio);
				}	
			}
		}

		private void btn_atras_Click(object sender, EventArgs e)
		{
			var pagina = pagina_actual - 1;

			if(pagina >= 0)
			{
				pagina_actual -= 1;
				long cantidad_inicio = pagina_actual * 100;
				if (cantidad_inicio >= 0)
				{
					txt_pagina_actual.Text = "" + ( pagina_actual + 1);
					dgv_kardex.DataSource = dao_articulos.get_kardex_articulo((int)dto_articulo.Articulo_id, cantidad_inicio);
				}	
			}
		}

		private void btn_inicio_Click(object sender, EventArgs e)
		{
			pagina_actual = 0;
			txt_pagina_actual.Text = "" + (pagina_actual + 1);
			dgv_kardex.DataSource = dao_articulos.get_kardex_articulo((int)dto_articulo.Articulo_id, 0);
		}

		private void btn_fin_Click(object sender, EventArgs e)
		{
			pagina_actual = (total_paginas - 1);
			long cantidad_inicio = pagina_actual * 100;
			txt_pagina_actual.Text = "" + (pagina_actual + 1);
			dgv_kardex.DataSource = dao_articulos.get_kardex_articulo((int)dto_articulo.Articulo_id, cantidad_inicio);
		}

		private void Kardex_articulos_principal_Shown(object sender, EventArgs e)
		{
			bloqueo_paginacion();
		}

		private void txt_pagina_actual_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(Convert.ToInt64(txt_pagina_actual.Text) > 0)
					{
						if (Convert.ToInt64(txt_pagina_actual.Text) <= total_paginas)
						{
							pagina_actual = (Convert.ToInt64(txt_pagina_actual.Text) - 1);
							var cantidad_inicio = pagina_actual * 100;
							dgv_kardex.DataSource = dao_articulos.get_kardex_articulo((int)dto_articulo.Articulo_id, cantidad_inicio);
						}
						else
						{
							txt_pagina_actual.Text = "" + (pagina_actual + 1);
							MessageBox.Show(this, "Página fuera del rango de busqueda", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
					else
					{
						txt_pagina_actual.Text = "" + (pagina_actual + 1);
						MessageBox.Show(this,"La página que busca debe ser mayor a 0","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					}
				break;
				case 27:
					txt_amecop.Focus();
				break;
			}
		}

		private void txt_pagina_actual_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar != (char)8 && !char.IsNumber(e.KeyChar))
			{
				e.Handled = true;
			}
		}

        private void chb_agrupar_CheckedChanged(object sender, EventArgs e)
        {
            buscar();
        }
	}
}
