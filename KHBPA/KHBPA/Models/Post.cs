using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KHBPA.Models
{
    public class Post
    {
        public virtual int ID { get; set; }

        public virtual string Title { get; set; }

        public virtual string ShortDescription { get; set; }

        public virtual string Descirption { get; set; }

        public virtual string Meta { get; set; }

        public virtual string UrlSlug { get; set; }

        public virtual bool Published { get; set; }

        public virtual DateTime PostedOn { get; set; }

        public virtual Category Category { get; set; }
        
        public virtual IList<Tag> Tags { get; set; }

    }
}