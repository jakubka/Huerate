/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;

namespace Huerate.Services.ImageProcessing
{
    public class QrCodeService : IQrCodeService
    {
        public Stream GetQrCodeStream(string encodedString, ImageFormat imageFormat)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
            QrCode qrCode = qrEncoder.Encode(encodedString);

            GraphicsRenderer renderer = new GraphicsRenderer(new FixedModuleSize(8, QuietZoneModules.Two));

            MemoryStream ms = new MemoryStream();

            renderer.WriteToStream(qrCode.Matrix, imageFormat, ms);

            ms.Position = 0;

            return ms;
        }
    }
}