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

namespace Farmacontrol_PDV.FORMS.inventarios.ajuste_caducidades_lotes
{
	public partial class Pedir_amecop : Form
	{
		DAO_Articulos dao_articulos = new DAO_Articulos();
		public int? articulo_id	 = null;
		public long existencia_total = 0;
		public string nombre = "";
		public DTO_Articulo articulo;

		public Pedir_amecop()
		{
			InitializeComponent();
		}

		private void txt_amecop_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(txt_amecop.Text.Trim().Length > 0)
					{
						var cantidad_uso = dao_articulos.get_cantidad_articulo_modulos(txt_amecop.Text);

						bool en_uso = false;
						string en_uso_por = "";
						string folios = "";
						long total_piezas = 0;

						foreach(var item in cantidad_uso)
						{
							if(!item.Key.Equals("total"))
							{
								if(item.Value.Item1 > 0)
								{
									en_uso = true;
									en_uso_por = item.Key;
									folios = item.Value.Item2;
									break;
								}
							}
							else
							{
								total_piezas = item.Value.Item1;
							}
						}

						if(en_uso)
						{
							MessageBox.Show(this,"Este producto se encuentra en uso por el modulo "+en_uso_por+" en los folios: "+folios,"Producto en uso",MessageBoxButtons.OK,MessageBoxIcon.Error);
						}
						else
						{
							if(total_piezas > 0)
							{
								articulo = dao_articulos.get_articulo(txt_amecop.Text);
								articulo_id = articulo.Articulo_id;
								nombre = articulo.Nombre;
								existencia_total = total_piezas;
								this.Close();
							}
							else
							{
								MessageBox.Show(this,"Producto sin existencia, imposible ajustar","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
							}
						}
					}
				break;
				case 27:
					if(txt_amecop.TextLength > 0)
					{
						txt_amecop.Text = "";
					}
					else
					{
						articulo_id = null;
						existencia_total = 0;
						nombre = "";
						this.Close();
					}
				break;
			}
		}

        private void txt_amecop_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
	}
}
