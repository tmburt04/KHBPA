using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KHBPA.Models
{
    public class News
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string ArticleUrl { get; set; }
        public DateTime UploadDate { get; set; }
    }
}