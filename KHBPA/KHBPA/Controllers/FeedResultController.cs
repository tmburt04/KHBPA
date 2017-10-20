using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ServiceModel.Syndication;
using System.Xml;

namespace KHBPA.Controllers
{
    public class FeedResultController : ActionResult
    {
        private SyndicationFeedFormatter formattedFeed;

        public FeedResultController(SyndicationFeedFormatter formattedFeed)
        {
            this.formattedFeed = formattedFeed;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/rrs+xml";
            using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output))
            {
                formattedFeed.WriteTo(writer);
            }
        }
    }
}