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

namespace Farmacontrol_PDV.FORMS.ventas.elaborar_formula
{
	public partial class Elaborar_formula_principal : Form
	{
		BindingList<DTO_Formulas_pendientes> data = new BindingList<DTO_Formulas_pendientes>();
		System.Threading.Timer _timer;

		public Elaborar_formula_principal()
		{
			InitializeComponent();
			dgv_formulas_pendientes.DataSource = data;
			get_formulas_pendientes();
			Elaborar_formula_principal.CheckForIllegalCrossThreadCalls = false;
			_timer = new System.Threading.Timer(TimerWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
		}

		void TimerWork(object obj)
		{
			DAO_Formulas dao_formulas = new DAO_Formulas();
			var response_formulas = dao_formulas.get_formulas_pendientes();
			List<DTO_Formulas_pendientes> SortedList = response_formulas.OrderBy(o => o.fecha_creado).ToList();

			if (response_formulas.Count != data.Count)
			{
				data.Clear();

				foreach (var item in SortedList)
				{
					data.Add(item);
				}

				dgv_formulas_pendientes.Refresh();
				dgv_formulas_pendientes.ClearSelection();
			}
			else
			{
				foreach (var item_data in data)
				{
					foreach (var item in response_formulas)
					{
						if (item_data.formula_id == item.formula_id)
						{
							item_data.status = item.status;
						}
					}
				}

				dgv_formulas_pendientes.Refresh();
			}	
		}

		void get_formulas_pendientes()
		{
			data.Clear();
			DAO_Formulas dao_formulas = new DAO_Formulas();
			var response_formulas = dao_formulas.get_formulas_pendientes();
			List<DTO_Formulas_pendientes> SortedList = response_formulas.OrderBy(o => o.fecha_creado).ToList();

			foreach(var item in SortedList)
			{
				data.Add(item);
			}

			dgv_formulas_pendientes.ClearSelection();
		}

		private void dgv_formulas_pendientes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if(e.ColumnIndex == dgv_formulas_pendientes.Columns["status"].Index)
			{
				dgv_formulas_pendientes.Rows[e.RowIndex].DefaultCellStyle.BackColor = (e.Value.ToString().Equals("POR ELABORAR")) ? Color.FromArgb(251, 249, 203) : Color.FromArgb(255, 218, 218);
			}
		}

		private void Elaborar_formula_principal_Shown(object sender, EventArgs e)
		{
			dgv_formulas_pendientes.ClearSelection();
		}

		private void dgv_formulas_pendientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if(dgv_formulas_pendientes.SelectedRows.Count > 0)
			{
				_timer.Dispose();
				long sucursal_id = Convert.ToInt64(dgv_formulas_pendientes.SelectedRows[0].Cells["sucursal_id"].Value);	
				string formula_id = dgv_formulas_pendientes.SelectedRows[0].Cells["formula_id"].Value.ToString();
				Formula_sucursal formula = new Formula_sucursal(sucursal_id,formula_id);
				formula.ShowDialog();
				get_formulas_pendientes();
				_timer =  new System.Threading.Timer(TimerWork,null,TimeSpan.Zero,TimeSpan.FromSeconds(5));
			}
		}

		private void Elaborar_formula_principal_FormClosing(object sender, FormClosingEventArgs e)
		{
			_timer.Dispose();
		}
	}
}
