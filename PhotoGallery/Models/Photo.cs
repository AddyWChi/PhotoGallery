using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGallery.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    /// <summary>
    /// Represent an instance of photo
    /// </summary>
    public class Photo
    {
        /// <summary>
        /// Gets or sets unique photo Id.
        /// </summary>
        [Key]
        [HiddenInput]
        [ScaffoldColumn(false)]
        public string Photo_Id { get; set; }

        /// <summary>
        /// Concurrency checking.
        /// The Timestamp attribute specifies that this column will be 
        /// included in the Where clause of Update and Delete commands 
        /// sent to the database.
        /// </summary>
        [Timestamp]
        public Byte[] Timestamp { get; set; }

        /// <summary>
        /// Location of file.
        /// </summary>
        public string PhyicalFilePath { get; set; }

        /// <summary>
        /// Url path.
        /// </summary>
        public string UrlPath { get; set; }

        /// <summary>
        /// Description of photo.
        /// </summary>
        public string Description { get; set; }

        public Photo(string urlPath, string filePath, string description)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("Value not valid.", "filePath");
            }

            this.UrlPath = urlPath;
            this.PhyicalFilePath = filePath;
            this.Description = description;
        }
    }
}