/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Huerate.Services.ImageProcessing;
using Huerate.Services.Membership;
using Huerate.Services.Settings;
using Huerate.Services.Translations;

namespace Huerate.WebUI.Controllers
{
    public class ImageController : ControllerBase
    {
        private IQrCodeService QrCodeService { get; set; }

        public ImageController(IQrCodeService qrCodeService, IMembershipService membershipService, ISettingsService settingsService, ITranslationService translationService)
            : base(membershipService, settingsService, translationService)
        {
            QrCodeService = qrCodeService;
        }

        public FileResult QrCode(string restaurantCodeName, bool download = false)
        {
            string formUrl = Url.Action("Index", "Form", new {codeName = restaurantCodeName}, "http");

            var qrCodeStream = QrCodeService.GetQrCodeStream(formUrl, ImageFormat.Png);

            var fileResult = File(qrCodeStream, "image/png");

            if (download)
            {
                fileResult.FileDownloadName = restaurantCodeName + "-qrcode.png";
            }

            return fileResult;
        }
    }
}