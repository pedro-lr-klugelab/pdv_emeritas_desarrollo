using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.FORMS.movimientos.recepciones_mayoristas
{
	public partial class Articulos_empaques : Form
	{
		long articulo_id;
		long entrada_id;
		string caducidad;
		string lote;
		int cantidad;
		DAO_Articulos dao_articulos = new DAO_Articulos();

		public bool registrar_empaque = false;

		public Articulos_empaques(long articulo_id, long entrada_id, string caducidad, string lote, int cantidad)
		{
			this.articulo_id = articulo_id;
			this.entrada_id = entrada_id;
			this.caducidad = caducidad;
			this.lote = lote;
			this.cantidad = cantidad;

			InitializeComponent();
		}

		void get_articulos_componente()
		{
			var result = dao_articulos.get_articulos_empaque(articulo_id, caducidad, lote, cantidad);;

			foreach (DataRow row in result.Rows)
			{
				row["caducidad"] = Misc_helper.fecha(row["caducidad"].ToString(),"CADUCIDAD");
			}

			dgv_productos.DataSource = result;

			dgv_productos.ClearSelection();
		}

		private void Articulos_empaques_Shown(object sender, EventArgs e)
		{
			get_articulos_componente();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if(rdb_contenido.Checked == true || rdb_empaque.Checked == true)
			{
				if(rdb_empaque.Checked)
				{
					DialogResult dr = MessageBox.Show(this,"Se registrara solo el empaque, ¿Desea continuar?","Empaque",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

					if(dr == DialogResult.Yes)
					{
						registrar_empaque = true;
						this.Close();	
					}
				}
				else
				{
					DialogResult dr = MessageBox.Show(this, "Se registraran los productos que se encuentran dentro del empaque de manera individual, ¿Desea continuar?", "Registro individual", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

					if (dr == DialogResult.Yes)
					{
						foreach(DataGridViewRow row in dgv_productos.Rows)
						{
							DAO_Entradas dao_entradas = new DAO_Entradas();

							var insert_id = dao_entradas.insertar_producto(
									entrada_id,
									row.Cells["c_amecop"].Value.ToString(),
									Misc_helper.CadtoDate(row.Cells["c_caducidad"].Value.ToString()),
									row.Cells["c_lote"].Value.ToString(),
									Convert.ToInt64(row.Cells["c_cantidad"].Value),
									articulo_id
							);	
						}
						
						MessageBox.Show(this,"Se ha registrado los productos correctamente","Información",MessageBoxButtons.OK,MessageBoxIcon.Information);	
						this.Close();
					}
				}
			}
			else
			{
				MessageBox.Show(this,"Para continuar, es necesario indicar la manera en la que se registrara el producto","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}
	}
}
