using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Farmacontrol_PDV.FORMS.comunes
{
	public partial class Busqueda_domicilios : Form
	{
		private string cliente_id;
		private string nombre_cliente;
		public string cliente_domicilio_id = "";

		public Busqueda_domicilios(string nombre,string cliente_id)
		{
			this.cliente_id = cliente_id;
			this.nombre_cliente = nombre;
			InitializeComponent();
		}

		private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					cliente_domicilio_id = dgv_domicilios.SelectedRows[0].Cells["columna_cliente_domicilio_id"].Value.ToString();
					this.Close();
				break;
				case 27:
					cliente_domicilio_id = "";
					this.Close();
				break;
			}
		}

		private void domicilios_Load(object sender, EventArgs e)
		{
			txt_nombre_cliente.Text = nombre_cliente;
			DAO.DAO_Clientes dao_clientes = new DAO.DAO_Clientes();
			dgv_domicilios.DataSource = dao_clientes.get_domicilios(cliente_id);
		}
	}
}
