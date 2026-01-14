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
	public partial class Consulta_existencias_caducidades : Form
	{
		private long sucursal_id;
		private long articulo_id;

		public Consulta_existencias_caducidades(long sucursal_id, long articulo_id)
		{
			this.sucursal_id = sucursal_id;
			this.articulo_id = articulo_id;
			InitializeComponent();
		}

		public void get_existencia_caducidades()
		{
			dgv_caducidades.DataSource = DAO_Articulos.get_existencia_caducidades(sucursal_id, articulo_id);
			dgv_caducidades.ClearSelection();
		}

		private void Consulta_existencias_caducidades_Shown(object sender, EventArgs e)
		{
			get_existencia_caducidades();
		}
	}
}
