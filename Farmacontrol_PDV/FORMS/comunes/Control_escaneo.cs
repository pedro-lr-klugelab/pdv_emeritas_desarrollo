using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using WIA;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using PdfSharp.Pdf;
using PdfSharp;
using PdfSharp.Drawing;
using System.Net;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.CLASSES;
using Farmacontrol_PDV.FORMS.comunes;

namespace Farmacontrol_PDV.FORMS.movimientos.recepciones_mayoristas
{
	public partial class Control_escaneo : Form
	{
		WIA.CommonDialog dialog = new WIA.CommonDialog();
		List<Image> list_images = new List<Image>();

        /*
        private Bitmap originalBitmap = null;
        private Bitmap previewBitmap = null;
         * */
        //private Bitmap resultBitmap = null;

        int nivel_contraste = Properties.Configuracion.Default.nivel_contraste;
        int nivel_brillo    = Properties.Configuracion.Default.nivel_brillo;

		int width_original  = 0;
		int height_original = 0;

		Image imagen_original;
        Image copia_imagen;

		Point mouseDown;
		Point mousePosNow;

		Label lbl_cerrar = new Label();
		public bool status = false;
		public string nombre_uuid = "";
		private string directorio_destino = "";

		public Control_escaneo(string directorio_destino)
		{
			this.directorio_destino = directorio_destino;
			InitializeComponent();

            txt_contraste.Text = nivel_contraste.ToString();
            txt_brillo.Text = nivel_brillo.ToString();
		}

		public Image CambiarTamanoImagen(Image pImagen, int pAncho, int pAlto)
		{
			Bitmap vBitmap = new Bitmap(pAncho, pAlto);

			using (Graphics vGraphics = Graphics.FromImage((Image)vBitmap))
			{
				vGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				vGraphics.DrawImage(pImagen, 0, 0, pAncho, pAlto);
			}

			return (Image)vBitmap;
		}

        public Bitmap temp_img(Image imagen, int ancho, int alto)
        {
            Bitmap vBitmap = new Bitmap(ancho, alto);

			using (Graphics vGraphics = Graphics.FromImage((Image)vBitmap))
			{
				vGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                vGraphics.DrawImage(imagen, 0, 0, ancho, alto);
			}

			return vBitmap;
        }

		public Device conectar_scanner(String scannerId)
		{
			WIA.DeviceManager manager = new WIA.DeviceManager();
			WIA.Device device = null;

			foreach (WIA.DeviceInfo info in manager.DeviceInfos)
			{
				if (info.DeviceID == scannerId)
				{
					try
					{
						device = info.Connect();

						object ColorIntent = "6146";
						object GrayScale = 2;

						foreach (WIA.Item item in device.Items)
						{
							foreach (WIA.Property itemProperty in item.Properties)
							{
								Object tempNewProperty;

								if (itemProperty.Name.Equals("Horizontal Resolution"))
								{
									tempNewProperty = 150;
									((IProperty)itemProperty).set_Value(ref tempNewProperty);
								}
								else if (itemProperty.Name.Equals("Vertical Resolution"))
								{
									tempNewProperty = 150;
									((IProperty)itemProperty).set_Value(ref tempNewProperty);
								}
								else if (itemProperty.Name.Equals("Horizontal Extent"))
								{
									tempNewProperty = 1275;
									((IProperty)itemProperty).set_Value(ref tempNewProperty);
								}
								else if (itemProperty.Name.Equals("Vertical Extent"))
								{
									tempNewProperty = 1650;
									((IProperty)itemProperty).set_Value(ref tempNewProperty);
								}
							}
						}

						device.Items[1].Properties.get_Item(ref ColorIntent).set_Value(ref GrayScale);
					}
					catch (Exception e)
					{
						Log_error.log(e);
					}

					break;
				}
			}

			return device;
		}

		public Image CompressImage(Image image, int width, int height)
		{
			width = image.Width;
			height = image.Height;
			float horizontalResolution = image.HorizontalResolution;
			float verticalResolution = image.VerticalResolution;

			ImageCodecInfo codec = null;

			foreach (ImageCodecInfo c in ImageCodecInfo.GetImageEncoders())
				if (c.FormatDescription.Equals("JPEG"))
					codec = c;

			System.Drawing.Imaging.Encoder enc = System.Drawing.Imaging.Encoder.Quality;
			EncoderParameters encps = new EncoderParameters(1);

			EncoderParameter encp = new EncoderParameter(enc, Convert.ToInt64(50));
			encps.Param[0] = encp;

			MemoryStream ms = new MemoryStream();

			Bitmap bmp = new Bitmap(image);
			bmp.SetResolution(150, 150);

			image = bmp;
			image.Save(ms, codec, encps);

			return Image.FromStream(ms);
		}

		private void btn_escanear_Click(object sender, EventArgs e)
		{
			if(!Properties.Configuracion.Default.impresora_escaner.Equals(""))
			{
				var deviceManager = new DeviceManager();

				Boolean scanner_default = false;

				for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
				{
					if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
					{
						continue;
					}

					string default_scanner = Properties.Configuracion.Default.impresora_escaner.ToString();

					if (default_scanner.Equals(deviceManager.DeviceInfos[i].Properties["Name"].get_Value()))
					{
						scanner_default = true;
                        
						try
						{
							string scannerId = deviceManager.DeviceInfos[i].DeviceID;
							var device = this.conectar_scanner(scannerId);

							WIA.Item item = device.Items[1] as WIA.Item;
							WIA.ICommonDialog wiaCommonDialog = new WIA.CommonDialog();
							this.TopMost = false;
                            WIA.ImageFile image = null;
							
                            image = (WIA.ImageFile)wiaCommonDialog.ShowTransfer(item, "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}",false);

                            if (image != null && image.FileData != null)
                            {
                                this.TopMost = true;
                                byte[] bytes = (byte[])image.FileData.get_BinaryData();
                                MemoryStream ms = new MemoryStream(bytes);

                                var new_image = Image.FromStream(ms);

                                var compress_image = this.CompressImage(new_image, new_image.Width, new_image.Height);

                                Image tmp = compress_image;

                                imagen_original = compress_image;

                                if (nivel_brillo != 0)
                                {
                                    tmp = ExtBitmap.Brillo(compress_image, nivel_brillo);
                                }

                                if (nivel_contraste != 0)
                                {
                                    tmp = ExtBitmap.Contraste(tmp, nivel_contraste);
                                }

                                copia_imagen = tmp;

                                PictureBox picturebox = new PictureBox { BackColor = Color.WhiteSmoke };
                                picturebox.Image = copia_imagen;
                                picturebox.SizeMode = PictureBoxSizeMode.Zoom;
                                picturebox.Width = 150;
                                picturebox.Height = 130;

                                Button boton = new Button();
                                boton.Text = "X";
                                boton.Padding = new Padding(0, 0, 0, 0);
                                boton.TextAlign = ContentAlignment.MiddleCenter;
                                boton.Size = new Size(20, 20);
                                boton.Location = new Point(126, 0);

                                boton.Parent = picturebox;

                                //ApplyFilter(true);

                                boton.Click += delegate
                                {
                                    foreach (Control controlChotex in panel_contenedor_imagenes.Controls)
                                    {
                                        if (controlChotex is PictureBox)
                                        {
                                            var picture_box = (PictureBox)controlChotex;
                                            picture_box.BorderStyle = BorderStyle.None;
                                        }
                                        else if (controlChotex.HasChildren)
                                        {
                                            foreach (Control controlChild in controlChotex.Controls)
                                            {
                                                if (controlChild is PictureBox)
                                                {
                                                    var picture_box = (PictureBox)controlChild;
                                                    picture_box.BorderStyle = BorderStyle.None;
                                                }
                                            }
                                        }
                                    }

                                    picturebox.Dispose();
                                    pic_image_complete.Image = null;
                                    tcb_zoom.Value = 100;
                                    lbl_porcentaje_zoom.Text = "100%";

                                };

                                picturebox.MouseClick += new MouseEventHandler(picturebox_Click);
                                panel_contenedor_imagenes.Controls.Add(picturebox);
                            }
						}
						catch (Exception ex)
						{
							Log_error.log(ex);
						}
					}
				}

				if (scanner_default == false)
				{
					MessageBox.Show(this,"No existe scanner por default!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
			}
			else
			{
				MessageBox.Show(this,"No existe ningun escaner asociado a esta terminal","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		void picturebox_Click(object sender, MouseEventArgs e)
		{
			var picture = (PictureBox)sender;

			foreach (Control controlChotex in panel_contenedor_imagenes.Controls)
			{
				if (controlChotex is PictureBox)
				{
					var picture_box = (PictureBox)controlChotex;
					picture_box.BorderStyle = BorderStyle.None;
				}
				else if (controlChotex.HasChildren)
				{
					foreach (Control controlChild in controlChotex.Controls)
					{
						if (controlChild is PictureBox)
						{
							var picture_box = (PictureBox)controlChild;
							picture_box.BorderStyle = BorderStyle.None;
						}
					}
				}
			}

			picture.BorderStyle = BorderStyle.Fixed3D;

			var image = (Image)picture.Image;

            Bitmap tmp = temp_img(image, image.Width, image.Height);

			pic_image_complete.Image = null;

			width_original = image.Width;
			height_original = image.Height;
            //imagen_original = tmp;

			pic_image_complete.Height = image.Height;
			pic_image_complete.Width = image.Width;
            pic_image_complete.Image = tmp;
			pic_image_complete.Location = new Point(0, 0);

            //ApplyFilter();
		}

		private void btn_zoom_in_Click(object sender, EventArgs e)
		{
			if (pic_image_complete.Image != null)
			{
				if (tcb_zoom.Value < 200)
				{
					tcb_zoom.Value = tcb_zoom.Value + 50;

					if (tcb_zoom.Value != 100)
					{
						pic_image_complete.Height = (int)((height_original / 100) * Convert.ToInt32(tcb_zoom.Value));
						pic_image_complete.Width  = (int)((width_original  / 100) * Convert.ToInt32(tcb_zoom.Value));

						pic_image_complete.Image = CambiarTamanoImagen(pic_image_complete.Image, (int)((width_original / 100) * Convert.ToInt32(tcb_zoom.Value)), (int)((height_original / 100) * Convert.ToInt32(tcb_zoom.Value)));

						pic_image_complete.Location = new Point(0, 0);

						pic_image_complete.SizeMode = PictureBoxSizeMode.Normal;
						pic_image_complete.Dock = DockStyle.None;
						pic_image_complete.Refresh();
					}
					else
					{
						pic_image_complete.Location = new Point(0, 0);

						pic_image_complete.SizeMode = PictureBoxSizeMode.Zoom;
						pic_image_complete.Dock = DockStyle.Fill;
						pic_image_complete.Refresh();
					}

					lbl_porcentaje_zoom.Text = tcb_zoom.Value + "%";
				}
			}
		}

		private void btn_zoom_out_Click(object sender, EventArgs e)
		{
			if (pic_image_complete.Image != null)
			{
				if (tcb_zoom.Value > 100)
				{
					tcb_zoom.Value = tcb_zoom.Value - 50;

					pic_image_complete.Height = (int)((height_original / 100) * Convert.ToInt32(tcb_zoom.Value));
					pic_image_complete.Width = (int)((width_original / 100) * Convert.ToInt32(tcb_zoom.Value));

					pic_image_complete.Image = CambiarTamanoImagen(pic_image_complete.Image, (int)((width_original / 100) * Convert.ToInt32(tcb_zoom.Value)), (int)((height_original / 100) * Convert.ToInt32(tcb_zoom.Value)));

					if (tcb_zoom.Value != 100)
					{
						pic_image_complete.Location = new Point(0, 0);

						pic_image_complete.SizeMode = PictureBoxSizeMode.Normal;
						pic_image_complete.Dock = DockStyle.None;
						pic_image_complete.Refresh();
					}
					else
					{
						pic_image_complete.Location = new Point(0, 0);

						pic_image_complete.SizeMode = PictureBoxSizeMode.Zoom;
						pic_image_complete.Dock = DockStyle.Fill;
						pic_image_complete.Refresh();
					}

					lbl_porcentaje_zoom.Text = tcb_zoom.Value + "%";
				}
			}
		}

		public void rotate_image_right()
		{
            if (pic_image_complete.Image != null)
            {
                var bmp = new Bitmap(imagen_original);

                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    gfx.Clear(Color.White);
                    gfx.DrawImage(imagen_original, 0, 0, imagen_original.Width, imagen_original.Height);
                }

                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);

                var compress = CompressImage(bmp, bmp.Width, bmp.Height);
                //imagen_original = CompressImage(bmp, bmp.Width, bmp.Height);
                //Bitmap tmp = temp_img(compress, compress.Width, compress.Height);

                pic_image_complete.Image = CompressImage(bmp, bmp.Width, bmp.Height); ;

                pic_image_complete.SizeMode = PictureBoxSizeMode.Zoom;
                pic_image_complete.Dock = DockStyle.Fill;
                pic_image_complete.Refresh();

                height_original = imagen_original.Height;
                width_original = imagen_original.Width;
                tcb_zoom.Value = 100;
                lbl_porcentaje_zoom.Text = tcb_zoom.Value + "%";

                guardar_informacion_imagen_original();
            }
		}

		public void rotate_image_left()
		{
            if (pic_image_complete.Image != null)
            {

                var bmp = new Bitmap(imagen_original);

                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    gfx.Clear(Color.White);
                    gfx.DrawImage(imagen_original, 0, 0, imagen_original.Width, imagen_original.Height);
                }

                bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);

                var compress = CompressImage(bmp, bmp.Width, bmp.Height);
                //imagen_original = CompressImage(bmp, bmp.Width, bmp.Height);
                //Bitmap tmp = temp_img(compress, compress.Width, compress.Height);

                pic_image_complete.Image = CompressImage(bmp, bmp.Width, bmp.Height);

                pic_image_complete.SizeMode = PictureBoxSizeMode.Zoom;
                pic_image_complete.Dock = DockStyle.Fill;
                pic_image_complete.Refresh();

                height_original = imagen_original.Height;
                width_original = imagen_original.Width;
                tcb_zoom.Value = 100;
                lbl_porcentaje_zoom.Text = tcb_zoom.Value + "%";

                guardar_informacion_imagen_original();
            }
		}

		public void guardar_informacion_imagen_original()
		{
			var image = pic_image_complete.Image;

			foreach (Control controlChotex in panel_contenedor_imagenes.Controls)
			{
				if (controlChotex is PictureBox)
				{
					var picture_box = (PictureBox)controlChotex;

					if (picture_box.BorderStyle == BorderStyle.Fixed3D)
					{
						picture_box.Image = image;
						break;
					}
				}
				else if (controlChotex.HasChildren)
				{
					foreach (Control controlChild in controlChotex.Controls)
					{
						if (controlChild is PictureBox)
						{
							var picture_box = (PictureBox)controlChild;

							if (picture_box.BorderStyle == BorderStyle.Fixed3D)
							{
								picture_box.Image = image;
								break;
							}
						}
					}
				}
			}

            Bitmap tmp = temp_img(image, image.Width, image.Height);

            width_original = image.Width;
            height_original = image.Height;
            imagen_original = tmp;

            ApplyFilter();
		}

		private void btn_rotate_left_Click(object sender, EventArgs e)
		{
			rotate_image_left();
		}

		private void btn_rotate_right_Click(object sender, EventArgs e)
		{
			rotate_image_right();
		}

		private void pic_image_complete_MouseUp(object sender, MouseEventArgs e)
		{
			MouseEventArgs mouse = e as MouseEventArgs;

			if (mouse.Button == MouseButtons.Left)
			{
				if(pic_image_complete.SizeMode == PictureBoxSizeMode.Normal)
				{
					mousePosNow = mouse.Location;

					int deltaX = mousePosNow.X - mouseDown.X;
					int deltaY = mousePosNow.Y - mouseDown.Y;

					int newX = pic_image_complete.Location.X + deltaX;
					int newY = pic_image_complete.Location.Y + deltaY;

					pic_image_complete.Location = new Point(newX, newY);	
				}
			}
		}

		private void pic_image_complete_MouseDown(object sender, MouseEventArgs e)
		{
			MouseEventArgs mouse = e as MouseEventArgs;

			if (mouse.Button == MouseButtons.Left)
			{
				if(pic_image_complete.SizeMode == PictureBoxSizeMode.Normal)
				{
					mouseDown = mouse.Location;	
				}
			}
		}

		private void pic_image_complete_MouseMove(object sender, MouseEventArgs e)
		{
			if (pic_image_complete.SizeMode == PictureBoxSizeMode.Normal)
			{
				MouseEventArgs mouse = e as MouseEventArgs;

				if (mouse.Button == MouseButtons.Left)
				{
					Point mousePosNow = mouse.Location;

					int deltaX = mousePosNow.X - mouseDown.X;
					int deltaY = mousePosNow.Y - mouseDown.Y;

					int newX = pic_image_complete.Location.X + deltaX;
					int newY = pic_image_complete.Location.Y + deltaY;

					pic_image_complete.Location = new Point(newX, newY);
				}
			}
		}

		private void btn_guardar_Click(object sender, EventArgs e)
		{
			list_images.Clear();

			foreach (Control controlChotex in panel_contenedor_imagenes.Controls)
			{
				if (controlChotex is PictureBox)
				{
					var picture_box = (PictureBox)controlChotex;
					list_images.Add(picture_box.Image);
				}
				else if (controlChotex.HasChildren)
				{
					foreach (Control controlChild in controlChotex.Controls)
					{
						if (controlChild is PictureBox)
						{
							var picture_box = (PictureBox)controlChild;
							list_images.Add(picture_box.Image);
						}
					}
				}
			}

			if (list_images.Count > 0)
			{
				this.SaveAsPDF(list_images, "pdf/facturas.pdf");
			}
			else
			{
				MessageBox.Show(this, "No hay ninguna imagen para procesar","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		public void SaveAsPDF(List<Image> list_image, string filename)
		{
			Upload_file upload_files = new Upload_file(directorio_destino, list_image);
			upload_files.ShowDialog();

			if(upload_files.status)
			{
				status = upload_files.status;
				nombre_uuid = upload_files.nombre_uuid;
				this.Close();
			}
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

        private void ApplyFilter()
        {
            Cursor.Current = Cursors.WaitCursor;
            copia_imagen = imagen_original;

            copia_imagen = ExtBitmap.Brillo(copia_imagen, nivel_brillo);
            copia_imagen = ExtBitmap.Contraste(copia_imagen, nivel_contraste);

            foreach (Control controlChotex in panel_contenedor_imagenes.Controls)
            {
                if (controlChotex is PictureBox)
                {
                    var picture_box = (PictureBox)controlChotex;
                        
                    if (picture_box.BorderStyle.ToString() == "Fixed3D")
                    {
                        picture_box.Image = this.CompressImage(copia_imagen, copia_imagen.Width, copia_imagen.Height);
                    }
                }
                else if (controlChotex.HasChildren)
                {
                    foreach (Control controlChild in controlChotex.Controls)
                    {
                        if (controlChild is PictureBox)
                        {
                            var picture_box = (PictureBox)controlChild;

                            if (picture_box.BorderStyle.ToString() == "Fixed3D")
                            {
                                picture_box.Image = this.CompressImage(copia_imagen, copia_imagen.Width, copia_imagen.Height);
                            }
                        }
                    }
                }
            }

            pic_image_complete.Image = copia_imagen;

            Properties.Configuracion.Default.nivel_brillo = nivel_brillo;
            Properties.Configuracion.Default.nivel_contraste = nivel_contraste;
            Properties.Configuracion.Default.Save();
            Cursor.Current = Cursors.Default;
        }

        private void tcb_brillo_ValueChanged(object sender, EventArgs e)
        {
            pic_image_complete.Height = (int)((height_original / 100) * Convert.ToInt32(tcb_zoom.Value));
            pic_image_complete.Width = (int)((width_original / 100) * Convert.ToInt32(tcb_zoom.Value));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(pic_image_complete.Image != null)
            {
                if(nivel_contraste > -90)
                {
                    nivel_contraste -= 10;
                    txt_contraste.Text = nivel_contraste.ToString();
                    btn_menos_contraste.Enabled = false;
                    ApplyFilter();
                    btn_menos_contraste.Enabled = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pic_image_complete.Image != null)
            {
                if (nivel_contraste <= 90)
                {
                    nivel_contraste += 10;
                    txt_contraste.Text = nivel_contraste.ToString();
                    btn_mas_contraste.Enabled = false;
                    ApplyFilter();
                    btn_mas_contraste.Enabled = true;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pic_image_complete.Image != null)
            {
                if (nivel_brillo > -90)
                {
                    nivel_brillo -= 10;
                    txt_brillo.Text = nivel_brillo.ToString();
                    btn_menos_brillo.Enabled = false;
                    ApplyFilter();
                    btn_menos_brillo.Enabled = true;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pic_image_complete.Image != null)
            {
                if (nivel_brillo <= 90)
                {
                    nivel_brillo += 10;
                    txt_brillo.Text = nivel_brillo.ToString();
                    btn_mas_brillo.Enabled = false;
                    ApplyFilter();
                    btn_mas_brillo.Enabled = true;
                }
            }
        }
	}
}
