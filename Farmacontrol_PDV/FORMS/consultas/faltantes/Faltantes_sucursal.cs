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
using Farmacontrol_PDV.CLASSES.PRINT;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.FORMS.consultas.faltantes
{
	public partial class Faltantes_sucursal : Form
	{
		long reporte_faltantes_id;
		public Faltantes_sucursal(long reporte_faltantes_id)
		{
			this.reporte_faltantes_id = reporte_faltantes_id;
			InitializeComponent();
		}

		void get_sucursales()
		{
			var sucursales = DAO_Faltantes.get_sucursales_faltantes(reporte_faltantes_id);
			dgv_sucursales.Rows.Clear();

			List<sucursal_temp> lista_sucursales = new List<sucursal_temp>();

			foreach(DTO_Sucursal suc in sucursales)
			{
				sucursal_temp tmp = new sucursal_temp();
				tmp.sucursal_id = suc.sucursal_id;
				tmp.nombre		= suc.nombre;

				lista_sucursales.Add(tmp);
			}

			dgv_sucursales.DataSource = lista_sucursales;
			dgv_sucursales.ClearSelection();
		}

		private void Faltantes_sucursal_Shown(object sender, EventArgs e)
		{
			get_sucursales();
		}

		private void btn_imprimir_ticket_Click(object sender, EventArgs e)
		{
			if(dgv_sucursales.SelectedRows.Count > 0)
			{
				long sucursal_id = Convert.ToInt64(dgv_sucursales.SelectedRows[0].Cells["sucursal_id"].Value);
				Faltantes ticket = new Faltantes();
				ticket.construccion_ticket(sucursal_id,reporte_faltantes_id);
				ticket.print();
			}
			else
			{
				MessageBox.Show(this,"Es necesario seleccionar al menos una sucursal","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		private void btn_reporte_completo_Click(object sender, EventArgs e)
		{
			if(dgv_sucursales.SelectedRows.Count > 0)
			{
				long sucursal_id = Convert.ToInt64(dgv_sucursales.SelectedRows[0].Cells["sucursal_id"].Value);

				if(save_dialog_faltantes.ShowDialog() == DialogResult.OK)
				{
					generar_pdf(sucursal_id, save_dialog_faltantes.FileName);
				}
			}
			else
			{
				MessageBox.Show(this,"Es necesario seleccionar al menos una sucursal","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			
		}

		void generar_pdf(long sucursal_id,  string direccion_archivo)
		{
			try
			{
				//worker.ReportProgress(30);
				DAO_Cambio_precios dao = new DAO_Cambio_precios();
				DAO_Sucursales suc = new DAO_Sucursales();
				var sucursal_data = suc.get_sucursal_data((int)sucursal_id);
				var resul_existencias = DAO_Faltantes.get_detallado_faltantes(sucursal_id,reporte_faltantes_id);

				Report_faltantes faltantes = new Report_faltantes();

				using (ReportDocument crystalReport = faltantes)
				{
					crystalReport.SetDataSource(resul_existencias);
					crystalReport.SetParameterValue("titulo_reporte", "LISTA DE SURTIDO \""+sucursal_data.nombre+"\"");

					//worker.ReportProgress(40);

					ExportOptions CrExportOptions;
					DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
					PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
					direccion_archivo = direccion_archivo + "_" + Misc_helper.uuid() + ".pdf";
					CrDiskFileDestinationOptions.DiskFileName = direccion_archivo;

					//worker.ReportProgress(70);

					CrExportOptions = crystalReport.ExportOptions;
					{
						CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
						CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
						CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
						CrExportOptions.FormatOptions = CrFormatTypeOptions;
					}

					crystalReport.Export();
					//worker.ReportProgress(100);
				}
			}
			catch (Exception ex)
			{
				Log_error.log(ex);
			}
		}
	}

	class sucursal_temp
	{
		public long sucursal_id { set; get; }
		public string nombre { set; get; }
	}
}
