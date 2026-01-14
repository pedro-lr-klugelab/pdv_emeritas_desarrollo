using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Threading;
using System.Diagnostics;
using PdfSharp.Drawing.Layout;
using Farmacontrol_PDV.CLASSES;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.DAO;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace Farmacontrol_PDV.FORMS.reportes.valuacion_inventario
{
	public partial class Valuacion_inventario_principal : Form
	{
		DAO_Existencias dao_existencias = new DAO_Existencias();
		string direccion_archivo = "";
		private Form loading = new Form();
		private int reporte_porcentaje = 0;
		BackgroundWorker worker = new BackgroundWorker();

		public Valuacion_inventario_principal()
		{
			InitializeComponent();
			construccion_loading();
		}

		public void construccion_loading()
		{
			loading.Text = "";
			loading.ControlBox = false;
			loading.StartPosition = FormStartPosition.CenterScreen;
			loading.FormBorderStyle = FormBorderStyle.FixedSingle;
			loading.Size = new Size(250, 75);

			Label lbl_descripcion = new Label();
			lbl_descripcion.Text = "Generando reporte...";
			lbl_descripcion.Name = "lbl_descripcion";
			lbl_descripcion.AutoSize = true;
			lbl_descripcion.Visible = true;
			lbl_descripcion.Location = new Point(10, 10);

			ProgressBar pgb_porcentage = new ProgressBar();
			pgb_porcentage.Name = "pgb_porcentaje";
			pgb_porcentage.IsAccessible = true;
			pgb_porcentage.Size = new Size(230, 25);
			pgb_porcentage.Location = new Point(10, 30);

			loading.Controls.Add(lbl_descripcion);
			loading.Controls.Add(pgb_porcentage);
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Valuacion_inventario_principal_Shown(object sender, EventArgs e)
		{
			if (folder_dialog.ShowDialog() == DialogResult.OK)
			{
				textBox1.Text = folder_dialog.SelectedPath;
				direccion_archivo = folder_dialog.SelectedPath;

				worker.DoWork += new DoWorkEventHandler(worker_DoWork);
				worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
				worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
				worker.WorkerReportsProgress = true;

				worker.RunWorkerAsync();

				loading.ShowDialog();
			}
			else
			{
				this.Close();
			}
		}

		void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			var control_porcentaje = loading.Controls["pgb_porcentaje"] as ProgressBar;
			control_porcentaje.Value = reporte_porcentaje;
		}

		public void worker_DoWork(object sender, DoWorkEventArgs e)
		{
			generar_pdf();
		}

		public void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			loading.Close();
		}

		public void generar_pdf()
		{
			try
			{
				worker.ReportProgress(30);
				var resul_existencias = dao_existencias.get_valuacion_inventario();

				Report_valuacion_inventario valuacion_inventario = new Report_valuacion_inventario();

				using(ReportDocument crystalReport = valuacion_inventario)
				{
					crystalReport.SetDataSource(resul_existencias);
					crystalReport.SetParameterValue("titulo_reporte", "Valuacion de inventario");
					worker.ReportProgress(40);

					ExportOptions CrExportOptions;
					DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
					PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
					direccion_archivo = direccion_archivo + "\\"+Misc_helper.uuid()+"_valuacion_inventario.pdf";
					CrDiskFileDestinationOptions.DiskFileName = direccion_archivo;

					worker.ReportProgress(70);

					CrExportOptions = crystalReport.ExportOptions;
					{
						CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
						CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
						CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
						CrExportOptions.FormatOptions = CrFormatTypeOptions;
					}

					crystalReport.Export();
					worker.ReportProgress(100);
				}
			}
			catch(Exception ex)
			{
				Log_error.log(ex);
			}
		}

		private void btn_abrir_Click(object sender, EventArgs e)
		{
			Process.Start(direccion_archivo);
		}
	}
}
