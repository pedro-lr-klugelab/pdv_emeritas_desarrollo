using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Farmacontrol_PDV.FORMS.movimientos.devolucion_rechazada
{
	public partial class Rechazo_con_factura : Form
	{
		public bool traspaso = false;
		public bool apartado_mercancia = false;
		public bool conservarlos = false;

		public Rechazo_con_factura()
		{
			InitializeComponent();
		}

		private void btn_aceptar_Click(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show(this,"¿Es correcta tu seleccion? Al cerrar esta ventana no podran realizarse ningun tipo de cambio","Información",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

			if(dr == DialogResult.Yes)
			{
				traspaso = radio_auditoria.Checked;
				apartado_mercancia = radio_apartado_mercancia.Checked;
				conservarlos = radio_conservarlos.Checked;	
				this.Close();
			}
		}
	}
}
