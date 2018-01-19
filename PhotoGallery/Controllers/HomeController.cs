using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoGallery.Controllers
{
    using PhotoGallery.Models;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PhotoList()
        {
            string rawImageFolder = "/RawImage/";

            return View(new PhotoVM(rawImageFolder));
        }

        public ActionResult PhotoListWebAPI()
        {
            return View();
        }

        public ActionResult UploadPhoto()
        {
            ViewBag.Title = "Upload Photo";

            return View();
        }

        public ActionResult UploadPhotoAjax()
        {
            ViewBag.Title = "Upload Photo (AJax)";

            return View();
        }
    }
}