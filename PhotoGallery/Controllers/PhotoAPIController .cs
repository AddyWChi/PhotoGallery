using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace PhotoGallery.Controllers
{
    using PhotoGallery.Models;
    using System.Globalization;
    using System.IO;
    using System.Web;

    public class PhotoAPIController : ApiController
    {
        private PhotoGalleryContext db = new PhotoGalleryContext();

        /// <summary>
        /// Get a list of all photos from a directory
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        [HttpGet]
        public IQueryable<Photo> GetPhotos(string folder)
        {
            // Security 
            if (folder.Contains('\\') || folder.Contains('/'))
            {
                throw new ArgumentOutOfRangeException("folder", "Invalid character.");
            }

            List<Photo> list = new List<Photo>();
            string baseUrl = HttpContext.Current.Request.Url.OriginalString.ToString().Replace(
                                HttpContext.Current.Request.Url.PathAndQuery,
                                string.Empty);
            string domainUrl = baseUrl + "/" + folder;
            string path = Path.Combine(HttpContext.Current.Server.MapPath("/"), folder);
            DirectoryInfo photoDir = new DirectoryInfo(path);
            FileInfo[] rawFiles = photoDir.GetFiles();

            // Security
            var imgFiles = from img in rawFiles
                           where img.Extension.EndsWith("jpg", ignoreCase: true, culture: new CultureInfo("en-US"))
                                || img.Extension.EndsWith("jpeg", ignoreCase: true, culture: new CultureInfo("en-US"))
                                || img.Extension.EndsWith("bmp", ignoreCase: true, culture: new CultureInfo("en-US"))
                           select img;
            var i = 1;
            foreach (var file in imgFiles.OrderBy(f => f.Name))
            {
                Photo p = new Photo(domainUrl + "/" + file, file.FullName, "Img" + i++);
                list.Add(p);
            }

            return list.AsQueryable();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhotoExists(string id)
        {
            return db.Photos.Count(e => e.Photo_Id == id) > 0;
        }
    }
}