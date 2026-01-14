using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using System.Threading;
using System.Diagnostics;
using Farmacontrol_PDV.HELPERS;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Xml.Serialization;
using Farmacontrol_PDV.DTO;

namespace Farmacontrol_PDV.CLASSES
{
	class Reporte
	{
		PdfDocument document = new PdfDocument();
		XFont font_normal = new XFont("Courier new", 10, XFontStyle.Regular);
		XFont font_titulo = new XFont("Arial", 14, XFontStyle.Regular);
		private int margen_pagina = 35;
		public result_reporte result;
		private string texto_titulo ="";
		private string texto_contenido = "";
		private string direccion = "";
		private string nombre_archivo = "";
		private bool landscape = false;
		private Form loading = new Form();
		private int reporte_porcentaje = 0;
		BackgroundWorker worker = new BackgroundWorker();

		/*
		 * 91 CARACTERES DE LARGO EN MODO VERTICAL
		 */ 

		public Reporte()
		{
			construccion_loading();	
		}

		public void construccion_loading()
		{
			loading.Text = "";
			loading.ControlBox = false;
			loading.StartPosition = FormStartPosition.CenterScreen;
			loading.FormBorderStyle = FormBorderStyle.FixedSingle;
			loading.Size = new Size(250,75);

			Label lbl_descripcion = new Label();
			lbl_descripcion.Text = "Generando reporte...";
			lbl_descripcion.Name = "lbl_descripcion";
			lbl_descripcion.AutoSize = true;
			lbl_descripcion.Visible = true;
			lbl_descripcion.Location = new Point(10,10);

			ProgressBar pgb_porcentage = new ProgressBar();
			pgb_porcentage.Name = "pgb_porcentaje";
			pgb_porcentage.IsAccessible = true;
			pgb_porcentage.Size = new Size(230,25);
			pgb_porcentage.Location = new Point(10,30);

			loading.Controls.Add(lbl_descripcion);
			loading.Controls.Add(pgb_porcentage);
		}

		public void construir_contenido(string texto_titulo, string texto_contenido, string direccion, string nombre_archivo, bool landscape)
		{
			this.texto_titulo = texto_titulo;
			this.texto_contenido = texto_contenido;
			this.direccion = direccion;
			this.nombre_archivo = nombre_archivo;
			this.landscape = landscape;

			worker.DoWork += new DoWorkEventHandler(worker_DoWork);
			worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
			worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
			worker.WorkerReportsProgress = true;
			
			worker.RunWorkerAsync();

			loading.ShowDialog();
		}

		void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			var control_porcentaje = loading.Controls["pgb_porcentaje"] as ProgressBar;
			control_porcentaje.Value = reporte_porcentaje;
		}

		public void worker_DoWork(object sender, DoWorkEventArgs e){ 
			generar_pdf();
		}

		public void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			loading.Close();
		}

		public void generar_pdf()
		{
			result_reporte result = new result_reporte();

			string[] contenido_lineas = texto_contenido.Split('\n');
			int numero_lineas_pagina = (landscape) ? 46 : 61;
			int numero_lineas = contenido_lineas.Length - 1;
			int numero_paginas = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(numero_lineas) / numero_lineas_pagina));

			numero_paginas = (numero_paginas == 0) ? 1 : numero_paginas;

			decimal aumento_porcieto = Convert.ToDecimal(100) / Convert.ToDecimal(numero_paginas);
			decimal porciento_total = 0;

			for (int i = 1; i <= numero_paginas; i++)
			{
				porciento_total += aumento_porcieto;
				reporte_porcentaje = Convert.ToInt32(porciento_total);
				worker.ReportProgress(reporte_porcentaje);

				PdfPage page = document.AddPage();
				page.Orientation = (landscape) ? PdfSharp.PageOrientation.Landscape : PdfSharp.PageOrientation.Portrait;

				page.Size = PdfSharp.PageSize.Letter;

				XGraphics gfx = XGraphics.FromPdfPage(page);
				XTextFormatter tf = new XTextFormatter(gfx);

				XRect titulo = new XRect(margen_pagina, 23, page.Width - 70, ((page.Height / 100) * 2));
				gfx.DrawRectangle(XBrushes.White, titulo);
				tf.DrawString(texto_titulo, font_titulo, XBrushes.Black, titulo, XStringFormats.TopLeft);
				gfx.DrawLine(XPens.Black, margen_pagina, 15, page.Width - margen_pagina, 15);
				gfx.DrawLine(XPens.Black, margen_pagina, 44, page.Width - margen_pagina, 44);

				StringBuilder porcion_contenido = new StringBuilder();

				int indice_inicio = ((i - 1) * numero_lineas_pagina);
				int indice_fin = (i * numero_lineas_pagina);

				for (int x = indice_inicio; x < indice_fin; x++)
				{
					if (x <= numero_lineas)
					{
						porcion_contenido.Append(contenido_lineas[x].ToString());
					}
					else
					{
						break;
					}
				}

				XRect rec_contenido = new XRect(margen_pagina, 33 + ((page.Height / 100) * 2), page.Width - (margen_pagina * 2), (page.Height - ((page.Height / 100) * 13)));
				gfx.DrawRectangle(XBrushes.White, rec_contenido);

				tf.DrawString(porcion_contenido.ToString(), font_normal, XBrushes.Black, rec_contenido, XStringFormats.TopLeft);

				XRect rec_pie = new XRect(margen_pagina, (page.Height - ((page.Orientation == PdfSharp.PageOrientation.Landscape) ? 37 : 42)), page.Width - (margen_pagina * 2), ((page.Height / 100) * 2));
				gfx.DrawRectangle(XBrushes.White, rec_pie);

                tf.DrawString(string.Format("{0} Pagina {1} de {2}", Convert.ToDateTime(Misc_helper.fecha()).ToShortDateString(), i, numero_paginas), font_titulo, XBrushes.Black, rec_pie, XStringFormats.TopLeft);

				gfx.DrawLine(XPens.Black, margen_pagina, (page.Height - ((page.Orientation == PdfSharp.PageOrientation.Landscape) ? 30 : 35) - 15), page.Width - margen_pagina, (page.Height - ((page.Orientation == PdfSharp.PageOrientation.Landscape) ? 30 : 35)) - 15);
				gfx.DrawLine(XPens.Black, margen_pagina, (page.Height - ((page.Orientation == PdfSharp.PageOrientation.Landscape) ? 30 : 35)) + 15, page.Width - margen_pagina, (page.Height - ((page.Orientation == PdfSharp.PageOrientation.Landscape) ? 30 : 35)) + 15);
			}

			try
			{
				string filename = string.Format("{0}\\{1}.pdf", direccion, nombre_archivo);
				document.Save(filename);
				result.status = true;
				result.direccion_archivo = filename;
			}
			catch (Exception exception)
			{
				result.status = false;

				if (exception.GetType().IsAssignableFrom(typeof(System.UnauthorizedAccessException)))
				{
					result.error = "Es necesario darle permisos a la carpeta para poder guardar el documento";
				}
				else
				{
					result.error = exception.ToString();
				}
			}

			this.result = result;
		}
	}

	class Reporte_platilla
	{
		public Reporte_platilla(string xml, string plantilla)
		{
			
		}

		/*
		public static YourClass LoadFromXMLString(string xmlText)
		{
			var stringReader = new System.IO.StringReader(xmlText);
			var serializer = new XmlSerializer(typeof(YourClass));
			return serializer.Deserialize(stringReader) as YourClass;
		}
		 * */
	}

	class result_reporte
	{
		public string direccion_archivo { set; get; }
		public bool status { set; get; }
		public string error { set; get; }
	}
}
