using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KHBPA.Models
{
    public class Document
    {
        public int ID { get; set; }

        public string DocumentName { get; set; }

        public DateTime UploadDate { get; set; }

        public string UploadedBy { get; set; }

        public byte[] FileBytes { get; set; }

        public string ContentType { get; set; }

        public string Discriminator { get; set; }

        public int ContentLength { get; set; }
    }
}