using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Steganography.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase image)
        {
            var original = new Bitmap(image.InputStream);
            var encrypted = new Bitmap(original.Width, original.Height);
            var text = "Hello World";
            for (int i = 0; i < original.Width; i++)
            {
                for (int k = 0; k < original.Height; k++)
                {
                    var pixel = original.GetPixel(iLoop, kLoop);
                    if(iLoop == 0 && kLoop < text.Length)
                    {
                        var letter = text[kLoop];
                        pixel = Color.FromArgb(pixel.R, pixel.G, letter);
                    }
                    encrypted.SetPixel(iLoop, kLoop, pixel);
                }
            }
            using (var ms = new MemoryStream())
            {
                encrypted.Save(ms, ImageFormat.Jpeg);
                return File(ms.ToArray(), "image/jpg");
            }
        }
    }
}