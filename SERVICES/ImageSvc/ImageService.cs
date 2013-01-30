using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using POCO;
using System.Web;
using System.Drawing.Imaging;

namespace SERVICES.ImageSvc
{
    public class ImageService : IImageService
    {
        public SafeDrivingEntities context { get; set; }

        public ImageService()
        {
            context = SafeDrivingEntities.contexteDatas;
            ImgQuality = 90;
            OutputFormat = ImageFormat.Jpeg;
        }

        public ImageFormat OutputFormat { get; set; }

        public int ImgQuality { get; set; }

        public enum ImageFormat
        {
            Bmp = 0,
            Gif = 2,
            Jpeg = 1,
            Png = 4
        }

        public Resultat<Image> StreamToImage(byte[] buff)
        {
            return Resultat<Image>.SafeExecute<ImageService>(
               result =>
               {
                   MemoryStream ms = new MemoryStream(buff);
                   Image img = Image.FromStream(ms);
                   result.Valeur = img;
               });
        }

        public  Resultat<byte[]> ImagetoStream(Image img)
        {
            return Resultat<byte[]>.SafeExecute<ImageService>(
               result =>
               {
                   MemoryStream stream = new MemoryStream();
                   img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                   byte[] array = stream.ToArray();
                   result.Valeur = array;
               });
        }

        public byte[] Resize(HttpPostedFileBase postedFile, int maxWidth, int maxHeight, Image defaultImage)
        {
            Image sourceImage;
            Image image2;

            if(postedFile == null)
            {
                    sourceImage = defaultImage;
                    image2 = this.Resize(sourceImage, maxWidth, maxHeight);
            }
            else
            {
                sourceImage = System.Drawing.Image.FromStream(postedFile.InputStream);
                image2 = this.Resize(sourceImage, maxWidth, maxHeight);
            }
            
            sourceImage.Dispose();
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)this.ImgQuality);
            ImageCodecInfo encoder = ImageCodecInfo.GetImageEncoders()[(int)this.OutputFormat];
            MemoryStream stream = new MemoryStream();
            image2.Save(stream, encoder, encoderParams);
            byte[] buffer = stream.GetBuffer();
            image2.Dispose();
            stream.Close();
            return buffer;
        }

        internal System.Drawing.Image Resize(System.Drawing.Image sourceImage, int maxWidth, int maxHeight)
        {
            System.Drawing.Image source = new Bitmap(sourceImage);
            int width = sourceImage.Width;
            int height = sourceImage.Height;
            if (width > maxWidth)
            {
                height = (height * maxWidth) / width;
                width = maxWidth;
            }
            if (height > maxHeight)
            {
                width = (width * maxHeight) / height;
                height = maxHeight;
            }
            if ((width != sourceImage.Width) || (height != sourceImage.Height))
            {
                source = new Bitmap(source, width, height);
            }
            return source;
        }

    }
}
