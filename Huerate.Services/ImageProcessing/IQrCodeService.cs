/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Drawing.Imaging;
using System.IO;

namespace Huerate.Services.ImageProcessing
{
    public interface IQrCodeService
    {
        Stream GetQrCodeStream(string encodedString, ImageFormat imageFormat);
    }
}
