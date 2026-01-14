using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.FORMS.ventas.consulta_ventas;
using Farmacontrol_PDV.HELPERS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farmacontrol_PDV.FORMS.reportes.ventas
{
    public partial class Ventas_principal : Form
    {
        public Ventas_principal()
        {
            InitializeComponent();
            get_fechas();
        }

        void get_fechas()
        {
            List<DateTime> fechas = new List<DateTime>();

            DateTime actual = Convert.ToDateTime(Misc_helper.fecha()).Date;

            fechas.Add(actual);

            for (int x = 1; x < 30; x++ )
            {
                int sub = x * -1;
                DateTime dateForButton = Convert.ToDateTime(Misc_helper.fecha()).AddDays(sub);

                fechas.Add(dateForButton.Date);
            }

            cbb_fechas.DataSource = fechas;
        }

        private void btn_cargar_Click(object sender, EventArgs e)
        {
            DAO_Ventas dao = new DAO_Ventas();
            dgv_ventas.DataSource = dao.get_reporte_ventas(Convert.ToDateTime(cbb_fechas.SelectedValue).ToString("yyyy-MM-dd"));
            dgv_ventas.ClearSelection();
        }

        private void dgv_ventas_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (save_dialog_ventas.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                generar_pdf(save_dialog_ventas.FileName);
                Cursor.Current = Cursors.Default;
            }
        }

        void generar_pdf(string direccion_archivo)
		{
			try
			{
				//worker.ReportProgress(30);
				DAO_Ventas dao = new DAO_Ventas();
                var resul_ventas = dao.get_reporte_ventas(Convert.ToDateTime(cbb_fechas.SelectedValue).ToString("yyyy-MM-dd"));

				Report_venta report_venta = new Report_venta();

				using (ReportDocument crystalReport = report_venta)
				{
					crystalReport.SetDataSource(resul_ventas);
					crystalReport.SetParameterValue("titulo_reporte", "Ventas del dia "+Convert.ToDateTime(cbb_fechas.SelectedValue).ToString("dd/MMM/yy").ToUpper().Replace(".",""));

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

            private void dgv_ventas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
            {
                reimprimir_venta();
            }

        void reimprimir_venta()
        {
            if (dgv_ventas.SelectedRows.Count > 0)
            {
                long venta_id = Convert.ToInt64(dgv_ventas.SelectedRows[0].Cells["venta_id"].Value);

                Consulta_ventas_principal consulta = new Consulta_ventas_principal(venta_id);
                consulta.ShowDialog();
            }
        }

            private void dgv_ventas_KeyDown(object sender, KeyEventArgs e)
            {
                var keycode = Convert.ToInt32(e.KeyCode);

                switch(keycode)
                {
                    case 13:
                        if(dgv_ventas.SelectedRows.Count > 0)
                        {
                            reimprimir_venta();
                        }
                    break;
                }
            }
    }
}
