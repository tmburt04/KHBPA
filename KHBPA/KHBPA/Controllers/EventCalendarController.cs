using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KHBPA.Models;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using DHTMLX.Scheduler.Controls;
using System.Data.Entity;

namespace KHBPA.Controllers
{
    public class EventCalendarController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
    }

}
