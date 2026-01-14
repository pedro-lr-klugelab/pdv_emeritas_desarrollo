/*
 * The Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;


namespace Farmacontrol_PDV.CLASSES
{
    public static class ExtBitmap
    {
        public static Image CopyToSquareCanvas(Image sourceBitmap, int canvasWidthLenght)
        {
            float ratio = 1.0f;
            int maxSide = sourceBitmap.Width > sourceBitmap.Height ?
                          sourceBitmap.Width : sourceBitmap.Height;

            ratio = (float)maxSide / (float)canvasWidthLenght;

            Bitmap bitmapResult = (sourceBitmap.Width > sourceBitmap.Height ?
                                    new Bitmap(canvasWidthLenght, (int)(sourceBitmap.Height / ratio))
                                    : new Bitmap((int)(sourceBitmap.Width / ratio), canvasWidthLenght));

            using (Graphics graphicsResult = Graphics.FromImage(bitmapResult))
            {
                graphicsResult.CompositingQuality = CompositingQuality.HighQuality;
                graphicsResult.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsResult.PixelOffsetMode = PixelOffsetMode.HighQuality;

                graphicsResult.DrawImage(sourceBitmap,
                                        new Rectangle(0, 0,
                                            bitmapResult.Width, bitmapResult.Height),
                                        new Rectangle(0, 0,
                                            sourceBitmap.Width, sourceBitmap.Height),
                                            GraphicsUnit.Pixel);
                graphicsResult.Flush();
            }

            return (Image)bitmapResult;
        }

        public static Bitmap Contrast(Bitmap sourceBitmap, int threshold)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                                        sourceBitmap.Width, sourceBitmap.Height),
                                        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);

            sourceBitmap.UnlockBits(sourceData);

            double contrastLevel = Math.Pow((100.0 + threshold) / 100.0, 2);
            double blue = 0;
            double green = 0;
            double red = 0;

            for (int k = 0; k + 4 < pixelBuffer.Length; k += 4)
            {
                blue = ((((pixelBuffer[k] / 255.0) - 0.5) * contrastLevel) + 0.5) * 255.0;
                green = ((((pixelBuffer[k + 1] / 255.0) - 0.5) * contrastLevel) + 0.5) * 255.0;
                red = ((((pixelBuffer[k + 2] / 255.0) - 0.5) * contrastLevel) + 0.5) * 255.0;

                if(blue > 255)
                { blue = 255; }
                else if(blue < 0)
                { blue = 0; }

                if (green > 255)
                { green = 255; }
                else if (green < 0)
                { green = 0; }

                if (red > 255)
                { red = 255; }
                else if (red < 0)
                { red = 0; }

                pixelBuffer[k] = (byte)blue;
                pixelBuffer[k + 1] = (byte)green;
                pixelBuffer[k + 2] = (byte)red;
            }

            Bitmap res_Bitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height, PixelFormat.Format24bppRgb);
            BitmapData res_Data = res_Bitmap.LockBits(new Rectangle(0, 0, res_Bitmap.Width, res_Bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(pixelBuffer, 0, res_Data.Scan0, pixelBuffer.Length);
            res_Bitmap.UnlockBits(res_Data);
            return res_Bitmap;
        }

        public static Image Contraste(Image _currentBitmap, double contrast)
        {
            Bitmap temp = (Bitmap)_currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            if (contrast < -100) contrast = -100;
            if (contrast > 100) contrast = 100;
            double contraste = Math.Pow((100.0 + contrast) / 100, 2);
            //contrast *= contrast;

            Color c;
            double pR = 0;
            double pG = 0;
            double pB = 0;

            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);

                    pR = (((((c.R / 255.0) - 0.5) * contraste) + 0.5) * 255);
                    
                    if (pR < 0) pR = 0;
                    if (pR > 255) pR = 255;

                    pG = (((((c.G / 255.0) - 0.5) * contraste) + 0.5) * 255);

                    if (pG < 0) pG = 0;
                    if (pG > 255) pG = 255;
                    
                    pB = (((((c.B / 255.0) - 0.5) * contraste) + 0.5) * 255);
                    
                    if (pB < 0) pB = 0;
                    if (pB > 255) pB = 255;
                    
                    bmap.SetPixel(i, j, Color.FromArgb((byte)pR, (byte)pG, (byte)pB));
                }
            }
            _currentBitmap = (Bitmap)bmap.Clone();

            return (Image)_currentBitmap;
        }

        public static Image Brillo(Image Image, int Value)
        {
            Bitmap TempBitmap = (Bitmap)Image;
            float FinalValue = (float)Value / 255.0f;
            Bitmap NewBitmap = new Bitmap(TempBitmap.Width, TempBitmap.Height);
            Graphics NewGraphics = Graphics.FromImage(NewBitmap);
            float[][] FloatColorMatrix ={
                     new float[] {1, 0, 0, 0, 0},
                     new float[] {0, 1, 0, 0, 0},
                     new float[] {0, 0, 1, 0, 0},
                     new float[] {0, 0, 0, 1, 0},
                     new float[] {FinalValue, FinalValue, FinalValue, 1, 1}
                 };

            ColorMatrix NewColorMatrix = new ColorMatrix(FloatColorMatrix);
            ImageAttributes Attributes = new ImageAttributes();
            Attributes.SetColorMatrix(NewColorMatrix);
            NewGraphics.DrawImage(Image, new Rectangle(0, 0, Image.Width, Image.Height), 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel, Attributes);
            Attributes.Dispose();
            NewGraphics.Dispose();
            return (Image)Image;
        }
    }
}

