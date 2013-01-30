using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Web;

namespace SERVICES.ImageSvc
{
    public interface IImageService
    {
        /// <summary>
        /// Convertit un byte[] en Image
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        Resultat<Image> StreamToImage(byte[] buff);

        /// <summary>
        /// Convertit une image en byte[]
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        Resultat<byte[]> ImagetoStream(Image img);

        /// <summary>
        /// Resize de l'image avec en parametre le fichier uploadé, le width et le height souhaité
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        byte[] Resize(HttpPostedFileBase postedFile, int maxWidth, int maxHeight, Image defaultImage);
    }
}
