using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGallery.Models
{
    using System.Globalization;
    using System.IO;

    public class PhotoVM
                    : List<Photo>
    {
        /// <summary>
		/// Initializes a new instance of the <see cref="PhotoVM"/> class.
        /// </summary>
        /// <param name="folderPath"></param>
        public PhotoVM(string folderPath)
        {
            string baseUrl = HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace(
                                HttpContext.Current.Request.Url.AbsolutePath.ToString(),
                                string.Empty);
            string domainUrl = baseUrl + folderPath;
            string path = HttpContext.Current.Server.MapPath(folderPath);
            DirectoryInfo photoDir = new DirectoryInfo(path);
            FileInfo[] rawFiles = photoDir.GetFiles();
            var imgFiles = from img in rawFiles
                           where img.Extension.EndsWith("jpg", ignoreCase: true, culture: new CultureInfo("en-US"))
                                || img.Extension.EndsWith("jpeg", ignoreCase: true, culture: new CultureInfo("en-US"))
                                || img.Extension.EndsWith("bmp", ignoreCase: true, culture: new CultureInfo("en-US"))
                           select img;
            var i = 1;
            foreach (var file in imgFiles.OrderBy(f => f.Name))
            {
                Photo p = new Photo(domainUrl + file, file.FullName, "Img" + i++);
                this.Add(p);
            }

            return;
        }
    }
}
