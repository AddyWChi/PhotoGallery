using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoGallery.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    public class UploadController : ApiController
    {
        public async Task<HttpResponseMessage> PostFile()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string result = "Error";
            string root = HttpContext.Current.Server.MapPath("~/RawImage");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data and return an async task.
                await Request.Content.ReadAsMultipartAsync(provider);

                string description = provider.FormData.GetValues("description")[0];
                string fileNameNew = provider.FormData.GetValues("targetFileName")[0];

                // This illustrates how to get the file names for uploaded files.
                foreach (var file in provider.FileData)
                {
                    var filePathNew = Path.Combine(root, fileNameNew);
                    FileInfo fileInfo = new FileInfo(file.LocalFileName);
                    if (File.Exists(filePathNew))
                    {
                        result = "File with same name already exist.";
                        fileInfo.Delete();
                        continue;
                    }

                    fileInfo.CopyTo(filePathNew);
                    fileInfo.Delete();
                    result = "Success";
                }

                return new HttpResponseMessage()
                {
                    Content = new StringContent(result)
                };
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}