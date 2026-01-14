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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.DTO;

namespace Farmacontrol_PDV.FORMS.consultas.caducidades
{
	public partial class Caducidades_principal : Form
	{
		public Caducidades_principal()
		{
			InitializeComponent();
			construir_meses();
            dgv_articulos.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //  dgv_reporte_ceros.MultiSelect = false;
            dgv_articulos.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
		}

		void construir_meses()
		{
			chb_meses.DataSource = new BindingSource(new Dictionary<int,int>(){
                {1,1},
                {2,2},
                {3,3},
                {4,4},
                {5,5},
				{6,6}
			},null);

			chb_meses.DisplayMember = "Key";
			chb_meses.ValueMember = "Value";
		}

		void get_caducidades()
		{
            Cursor = Cursors.WaitCursor;
			DAO_Caducidades dao = new DAO_Caducidades();
			dgv_articulos.DataSource = dao.get_lista_caducidades((int)chb_meses.SelectedValue);
			dgv_articulos.ClearSelection();
            Cursor = Cursors.Default;
		}

		private void btn_generar_Click(object sender, EventArgs e)
		{
			get_caducidades();
		}

		private void dgv_articulos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex == 0)
			{
				return;
			}

			if (e.ColumnIndex == dgv_articulos.Columns["amecop"].Index || e.ColumnIndex == dgv_articulos.Columns["producto"].Index)
			{
				if (dgv_articulos.Rows[e.RowIndex].Cells["articulo_id"].Value.Equals(dgv_articulos.Rows[e.RowIndex - 1].Cells["articulo_id"].Value))
				{
					if (IsRepeatedCellValue(e.RowIndex, e.ColumnIndex))
					{
						e.Value = string.Empty;
						e.FormattingApplied = true;
					}
				}
			}
		}

		private bool IsRepeatedCellValue(int rowIndex, int colIndex)
		{
			DataGridViewCell currCell = dgv_articulos.Rows[rowIndex].Cells[colIndex];
			DataGridViewCell prevCell = dgv_articulos.Rows[rowIndex - 1].Cells[colIndex];

			if ((currCell.Value == prevCell.Value) || (currCell.Value != null && prevCell.Value != null && currCell.Value.ToString() == prevCell.Value.ToString()))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private void dgv_articulos_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;

			if (e.RowIndex < 1 || e.ColumnIndex < 0)
			{
				return;
			}

			if (IsRepeatedCellValue(e.RowIndex, e.ColumnIndex))
			{
				e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
			}
			else
			{
				e.AdvancedBorderStyle.Top = dgv_articulos.AdvancedCellBorderStyle.Top;
			}
		}

		private void btn_ticket_Click(object sender, EventArgs e)
		{
			Caducidades c = new Caducidades();
			c.construccion_ticket((int)chb_meses.SelectedValue);
			c.print();
		}

		private void btn_reporte_Click(object sender, EventArgs e)
		{
			if(save_dialog_caducidades.ShowDialog() == DialogResult.OK)
			{
				generar_pdf(save_dialog_caducidades.FileName);
			}
		}

		List<DTO_Caducidades>  get_preformato_caducidades(List<DTO_Caducidades> caducidades)
		{
			List<DTO_Caducidades> lista_general_caducidades = new List<DTO_Caducidades>();

			Dictionary<long,long> control = new Dictionary<long,long>();

			foreach (var detallado in caducidades)
			{
				if(!control.ContainsKey(detallado.articulo_id))
				{
					List<Tuple<string, string, string, long>> lista_impresion = new List<Tuple<string, string, string, long>>();

					foreach (var detallado_content in caducidades)
					{
						if (detallado_content.articulo_id == detallado.articulo_id)
						{

							Tuple<string, string, string, long> tup = new Tuple<string, string, string, long>(
								detallado_content.amecop,
								detallado_content.producto,
								detallado_content.caducidad,
								detallado_content.existencia
							);

							lista_impresion.Add(tup);
						}
					}

					if (lista_impresion.Count > 1)
					{
						int count = 1;

						foreach (Tuple<string, string, string, long> tuple in lista_impresion)
						{
							string amecop = tuple.Item1.ToString();

							DTO_Caducidades dto_tmp = new DTO_Caducidades();
							dto_tmp.amecop = (count.Equals(1)) ? amecop : "";
							dto_tmp.producto = (count.Equals(1)) ? tuple.Item2 : "";
							dto_tmp.caducidad = tuple.Item3;
							dto_tmp.existencia = tuple.Item4;

							lista_general_caducidades.Add(dto_tmp);

							count++;
						}
					}
					else
					{
						var tuple = lista_impresion[0];
						string amecop = tuple.Item1.ToString();

						DTO_Caducidades dto_tmp = new DTO_Caducidades();
						dto_tmp.amecop = amecop;
						dto_tmp.producto = tuple.Item2;
						dto_tmp.caducidad = tuple.Item3;
						dto_tmp.existencia = tuple.Item4;

						lista_general_caducidades.Add(dto_tmp);
					}

					control.Add(detallado.articulo_id,detallado.existencia);
				}
			}

			return lista_general_caducidades;
		}

		void generar_pdf(string direccion_archivo)
		{
			try
			{
				DAO_Caducidades dao = new DAO_Caducidades();
				var result = dao.get_lista_caducidades((int)chb_meses.SelectedValue);
				var resul_existencias = get_preformato_caducidades(result);

				Report_caducidades report_cad = new Report_caducidades();

				using (ReportDocument crystalReport = report_cad)
				{
					crystalReport.SetDataSource(resul_existencias);
					crystalReport.SetParameterValue("titulo_reporte", "Proximas Caducidades");
                    crystalReport.SetParameterValue("fecha", Misc_helper.fecha(Misc_helper.fecha(),"LEGIBLE"));

					ExportOptions CrExportOptions;
					DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
					PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
					direccion_archivo = direccion_archivo + "_" + Convert.ToDateTime(Misc_helper.fecha()).ToString("ddMMMyyyy_hhmmsstt").ToUpper() + ".pdf";
					CrDiskFileDestinationOptions.DiskFileName = direccion_archivo;

					CrExportOptions = crystalReport.ExportOptions;
					{
						CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
						CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
						CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
						CrExportOptions.FormatOptions = CrFormatTypeOptions;
					}

					crystalReport.Export();
				}
			}
			catch (Exception ex)
			{
				Log_error.log(ex);
			}
		}

	}

	public class GroupByGrid : DataGridView
	{

		protected override void OnCellFormatting(

		   DataGridViewCellFormattingEventArgs args)
		{

			// Call home to base

			base.OnCellFormatting(args);


			// First row always displays

			if (args.RowIndex == 0)

				return;


			if (IsRepeatedCellValue(args.RowIndex, args.ColumnIndex))
			{

				args.Value = string.Empty;

				args.FormattingApplied = true;

			}

		}


		private bool IsRepeatedCellValue(int rowIndex, int colIndex)
		{

			DataGridViewCell currCell =

			   Rows[rowIndex].Cells[colIndex];

			DataGridViewCell prevCell =

			   Rows[rowIndex - 1].Cells[colIndex];


			if ((currCell.Value == prevCell.Value) ||

			   (currCell.Value != null && prevCell.Value != null &&

			   currCell.Value.ToString() == prevCell.Value.ToString()))
			{

				return true;

			}

			else
			{

				return false;
			}

		}


		protected override void OnCellPainting(

		   DataGridViewCellPaintingEventArgs args)
		{
			base.OnCellPainting(args);


			args.AdvancedBorderStyle.Bottom =

			   DataGridViewAdvancedCellBorderStyle.None;


			// Ignore column and row headers and first row

			if (args.RowIndex < 1 || args.ColumnIndex < 0)

				return;


			if (IsRepeatedCellValue(args.RowIndex, args.ColumnIndex))
			{

				args.AdvancedBorderStyle.Top =

				   DataGridViewAdvancedCellBorderStyle.None;

			}

			else
			{

				args.AdvancedBorderStyle.Top = AdvancedCellBorderStyle.Top;

			}

		}

	}
}
