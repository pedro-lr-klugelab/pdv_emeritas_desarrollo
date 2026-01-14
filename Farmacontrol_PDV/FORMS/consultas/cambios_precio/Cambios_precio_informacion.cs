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
using Farmacontrol_PDV.CLASSES.PRINT;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Farmacontrol_PDV.HELPERS;
using System.Diagnostics;

namespace Farmacontrol_PDV.FORMS.consultas.cambios_precio
{
	public partial class Cambios_precio_informacion : Form
	{
		long cambio_precio_id;

		public Cambios_precio_informacion(long cambio_precio_id)
		{
			this.cambio_precio_id = cambio_precio_id;	
			InitializeComponent();
		}

		void get_detallado(bool excluir_existencias = false, bool excluir_sin_cambios = false)
		{
			DAO_Cambio_precios dao = new DAO_Cambio_precios();
			dgv_articulos.DataSource = dao.get_detallado_cambio_precio(cambio_precio_id,excluir_existencias,excluir_sin_cambios);
			dgv_articulos.ClearSelection();
		}

		private void Cambios_precio_informacion_Shown(object sender, EventArgs e)
		{
			get_detallado();
		}

		private void chb_sin_existencia_CheckedChanged(object sender, EventArgs e)
		{
			get_detallado(chb_sin_existencia.Checked, chb_sin_cambio.Checked);
		}

		private void chb_sin_cambio_CheckedChanged(object sender, EventArgs e)
		{
			get_detallado(chb_sin_existencia.Checked,chb_sin_cambio.Checked);
		}

		private void btn_imprimir_ticket_Click(object sender, EventArgs e)
		{
            DAO_Cambio_precios dao = new DAO_Cambio_precios();

            if (dao.imprimir_reetiquetado(cambio_precio_id))
            {
                Cambio_precios ticket = new Cambio_precios();
                ticket.construccion_ticket(cambio_precio_id);
                ticket.print();
            }
            else
            {
                MessageBox.Show(this, "NO HAY PRODUCTOS CON EXISTENCIA PARA RE-ETIQUETAR","Información",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
		}

		private void btn_pdf_Click(object sender, EventArgs e)
		{
			if(save_dialog_cambios_precio.ShowDialog() == DialogResult.OK)
			{
				Cursor.Current = Cursors.WaitCursor;
				generar_pdf(save_dialog_cambios_precio.FileName);
				Cursor.Current = Cursors.Default;
			}
		}

		void generar_pdf(string direccion_archivo)
		{
			try
			{
				//worker.ReportProgress(30);
				DAO_Cambio_precios dao = new DAO_Cambio_precios();
				var resul_existencias = dao.get_detallado_cambio_precio_reporte(cambio_precio_id);

				Report_cambio_precios cambio_precios = new Report_cambio_precios();

				using (ReportDocument crystalReport = cambio_precios)
				{
					var cambio_data = dao.get_cambio_precio_data(cambio_precio_id);
					crystalReport.SetDataSource(resul_existencias);
					crystalReport.SetParameterValue("titulo_reporte", "Cambio de precios");
					crystalReport.SetParameterValue("folio", "#"+cambio_precio_id.ToString());
                    crystalReport.SetParameterValue("fecha", Misc_helper.fecha(cambio_data.fecha_creado.ToString(), "LEGIBLE"));
					crystalReport.SetParameterValue("mayorista", cambio_data.mayorista.ToUpper());

					//worker.ReportProgress(40);

					ExportOptions CrExportOptions;
					DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
					PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
					direccion_archivo = direccion_archivo + "_" +Misc_helper.uuid() + ".pdf";
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
                    Process.Start(direccion_archivo);
					//worker.ReportProgress(100);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
				Log_error.log(ex);
			}
		}
	}
}
