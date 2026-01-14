using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.FORMS.comunes;

namespace Farmacontrol_PDV.FORMS.inventarios.creacion_inventarios
{
	public partial class Informacion_general_inventario : Form
	{
		DAO_Inventarios dao_inventarios = new DAO_Inventarios();
		private long inventario_id;
		public bool no_inventariados_check = false;
		public bool diferencias_check = false;
		private bool finalizar_jornada_inventario;
		public bool inventario_terminado = false;

		public Informacion_general_inventario(bool no_inventariados, bool diferencias, long inventario_id, bool finalizar = false)
		{
			this.finalizar_jornada_inventario = finalizar;
			this.inventario_id = inventario_id;
			this.no_inventariados_check = no_inventariados;
			this.diferencias_check = diferencias;
			InitializeComponent();
		}

		private void Informacion_general_inventario_Load(object sender, EventArgs e)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				tbp_finalizar.Parent =  (finalizar_jornada_inventario) ? tbc_principal: null;
				var informacion_inventario = dao_inventarios.get_informacion_inventario(inventario_id);

				dgv_no_inventariados.DataSource = dao_inventarios.get_no_inventariados(inventario_id, (informacion_inventario.fecha_fin.Equals(null)) ? true : false);
				dgv_diferencias.DataSource = dao_inventarios.get_productos_diferencias(inventario_id);
				var calculo_inventarios = dao_inventarios.get_calculo_inventarios(inventario_id);

				decimal sum_total_ni = 0;
				decimal sum_total_di_fa = 0;
				decimal sum_total_di_so = 0;

				if(dgv_no_inventariados.Rows.Count > 0)
				{
					DataTable tabla_ni = (DataTable)dgv_no_inventariados.DataSource;
					sum_total_ni = (decimal)tabla_ni.Compute("Sum(total)", "True");	
				}

				if(dgv_diferencias.Rows.Count > 0)
				{
					DataTable tabla_di = (DataTable)dgv_diferencias.DataSource;

					var result_suma1 = tabla_di.Compute("Sum(faltante)", "True").ToString();
					var result_suma2 = tabla_di.Compute("Sum(sobrante)", "True").ToString();

					sum_total_di_fa = (result_suma1.ToString().Equals("")) ? 0 : Convert.ToDecimal(result_suma1);
					sum_total_di_so = (result_suma2.ToString().Equals("")) ? 0 : Convert.ToDecimal(result_suma2);
				}

				txt_total_no_inventariados.Text = sum_total_ni.ToString("C2");
				txt_sobrante.Text = sum_total_di_so.ToString("C2");
				txt_faltante.Text = sum_total_di_fa.ToString("C2");

				lbl_inventario_previo.Text = calculo_inventarios["inventario_previo"].ToString("C2");
				lbl_inventario_actual.Text = calculo_inventarios["inventario_actual"].ToString("C2");	

				chb_diferencias.Checked = diferencias_check;
				chb_no_inventariados.Checked = no_inventariados_check;

				Cursor.Current = Cursors.Default;
			}
			catch(Exception ex)
			{
				Log_error.log(ex);
			}
		}

		private void Informacion_general_inventario_Shown(object sender, EventArgs e)
		{
			dgv_no_inventariados.ClearSelection();
			dgv_diferencias.ClearSelection();
		}

		private void chb_no_inventariados_CheckedChanged(object sender, EventArgs e)
		{
			no_inventariados_check = chb_no_inventariados.Checked;
		}

		private void chb_diferencias_CheckedChanged(object sender, EventArgs e)
		{
			diferencias_check = chb_diferencias.Checked;
		}

		private void tbc_principal_Selected(object sender, TabControlEventArgs e)
		{
			dgv_diferencias.ClearSelection();
		}

		private void dgv_diferencias_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			dgv_diferencias.ClearSelection();
		}

		private void dgv_diferencias_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (dgv_diferencias.Rows.Count > 0)
			{
				switch (e.ColumnIndex)
				{
					case 2:
						if (e.Value.ToString().Length > 2)
						{
							e.CellStyle.BackColor = (e.Value.ToString().Substring(e.Value.ToString().Length - 2).Equals("NI")) ? Color.FromArgb(255, 218, 218) : Color.White;
						}
						else
						{
							e.CellStyle.BackColor = Color.White;
						}
					break;
				}
			}
		}

		private void btn_imprimir_no_inventariados_Click(object sender, EventArgs e)
		{
			Inventario ticket_inventario =  new Inventario();
			ticket_inventario.construccion_ticket_no_inventariados(inventario_id);
			ticket_inventario.print();
		}

		private void btn_imprimir_diferencias_Click(object sender, EventArgs e)
		{
			Inventario ticket_inventario = new Inventario();
			ticket_inventario.construccion_ticket_diferencias(inventario_id);
			ticket_inventario.print();
		}

		private void btn_terminar_inventario_Click(object sender, EventArgs e)
		{
			Login_form login = new Login_form("terminar_inventario");
			login.ShowDialog();

			if (login.empleado_id != null)
			{
				if (diferencias_check.Equals(true) && no_inventariados_check.Equals(true))
				{
					DialogResult dr = MessageBox.Show(this, "¿Estas seguro de querer terminar la jornada de inventario?", "Terminar Inventario", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

					if (dr == DialogResult.Yes)
					{
						var dto_validacion = dao_inventarios.terminar_jornada_inventario(inventario_id, (long)login.empleado_id);

						if (dto_validacion.status)
						{
							inventario_terminado = dto_validacion.status;
							MessageBox.Show(this, dto_validacion.informacion, "Inventario", MessageBoxButtons.OK, MessageBoxIcon.Information);
							this.Close();
						}
						else
						{
							MessageBox.Show(this, dto_validacion.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
				else
				{
					MessageBox.Show(this, "Es necesario confirmar que estas de acuerdo con la información proporcionada en las pestañas NO INVENTARIADOS y DIFERENCIAS","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
			}
		}
	}
}
