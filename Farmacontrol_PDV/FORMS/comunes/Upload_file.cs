using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PdfSharp.Pdf;
using PdfSharp;
using PdfSharp.Drawing;
using Farmacontrol_PDV.HELPERS;
using System.Net;
using System.IO;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.FORMS.comunes
{
	public partial class Upload_file : Form
	{
		private List<Image> list_image;
		public bool status = false;
		public string error = "";
		public string nombre_uuid = "";
		private string directorio_envio = "";

		public Upload_file(string directorio_envio, List<Image> list_image)
		{
			this.directorio_envio = directorio_envio;
			this.list_image = list_image;
			InitializeComponent();
		}

		public void guardar_pdf()
		{
			try
			{
                PdfDocument doc = new PdfDocument();

                for (int x = 0; x < list_image.Count; x++)
                {
                    PdfPage page = doc.AddPage();
                    //page.Orientation = (list_image.ElementAt(x).Width > list_image.ElementAt(x).Height) ? PageOrientation.Landscape : PageOrientation.Portrait;
                    page.Orientation = PageOrientation.Portrait;
                    page.Size = PageSize.A0;
                    //page.Size = PageSize.A4;
                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    XImage ximage = XImage.FromGdiPlusImage(list_image.ElementAt(x));

                    gfx.DrawImage(ximage, 0, 0);
                }

                //nombre_uuid = "test_"+Misc_helper.uuid() + ".pdf";
                nombre_uuid = Misc_helper.uuid() + ".pdf";

                //doc.Save("C:\\shared\\scans\\" + nombre_uuid);
                doc.Save(nombre_uuid);
                
				this.TopMost = false;

                //string uriString = string.Format("ftp://{0}/farmacontrol_docs/{1}/{2}", Properties.Configuracion.Default.main_server, directorio_envio, nombre_uuid);
                string uriString = string.Format("ftp://{0}/farmacontrol_docs/{1}/{2}", "emeritafarmacias.com", directorio_envio, nombre_uuid);
                //carpeta_destino = farmacontrol_docs
                /*
                
				WebClient myWebClient = new WebClient();
				myWebClient.Credentials = new NetworkCredential("administrador", "f4rm41520");
            
				string fileName = nombre_uuid;
				myWebClient.UploadFileAsync(new Uri(uriString), "STOR", nombre_uuid);
				myWebClient.UploadProgressChanged += WebClientUploadProgressChanged;
                */

                FtpWebRequest ftpRequest;
                FtpWebResponse ftpResponse;

                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(uriString));
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                ftpRequest.Proxy = null;
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                //ftpRequest.Credentials = new NetworkCredential("administrador", "cf4rm41520");
                ftpRequest.Credentials = new NetworkCredential("emeritafarma", "87794689");

                FileInfo ff = new FileInfo(nombre_uuid);
                byte[] fileContents = new byte[ff.Length];

                using (FileStream fr = ff.OpenRead())
                {
                    fr.Read(fileContents, 0, Convert.ToInt32(ff.Length));
                }

                lbl_progress.Text = 20 + "%";
                pgb_upload.Value = 20;

                using (Stream writer = ftpRequest.GetRequestStream())
                {
                    writer.Write(fileContents, 0, fileContents.Length);
                    lbl_progress.Text = 50 + "%";
                    pgb_upload.Value = 50;
                }

                lbl_progress.Text = 70 + "%";
                pgb_upload.Value = 70;
                    
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                Console.Write(ftpResponse.StatusDescription);
                status = true;
                lbl_progress.Text = 100 + "%";
                pgb_upload.Value = 100;
                this.Close();
                
			}
			catch (Exception ex)
			{
                MessageBox.Show(this,ex.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				status = false;
				error = "Ocurrio un error al intentar guardar el archivo \n Informacion detallada: "+ex.ToString();
				MessageBox.Show(this,error,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				File.Delete(nombre_uuid);
				this.Close();
			}
		}

		void WebClientUploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
		{
			lbl_progress.Text = e.ProgressPercentage+"%";
			pgb_upload.Value = e.ProgressPercentage;

			if(e.ProgressPercentage == 100)
			{
				status = true;
				File.Delete(nombre_uuid);
				MessageBox.Show(this, "Archivo guardado en el servidor", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.Close();
			}
		}

		private void Upload_file_Shown(object sender, EventArgs e)
		{
			guardar_pdf();
		}

        private void Upload_file_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(status)
            {
                File.Delete(nombre_uuid);
                MessageBox.Show(this, "Archivo guardado en el servidor", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
	}
}
