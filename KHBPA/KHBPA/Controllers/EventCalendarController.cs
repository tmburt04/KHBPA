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
        
        public readonly ApplicationDbContext _db = new ApplicationDbContext();

        

        public ActionResult Index()
        {
            //try
            //{
            var scheduler = new DHXScheduler(this); //initializes dhtmlxScheduler
            scheduler.LoadData = true;// allows loading data
            scheduler.EnableDataprocessor = true;// enables DataProcessor in order to enable implementation CRUD operations
                                                 //var form = scheduler.Lightbox.SetExternalLightboxForm("Views/EventCalendar/_LightBox");


            scheduler.Lightbox.Add(new LightboxText("location", "Location") { Height = 50 });


            scheduler.Lightbox.AddDefaults();
            scheduler.Calendars.AttachMiniCalendar();
            scheduler.Config.first_hour = 5;
            scheduler.Config.hour_date = "%h:%i %a";

            //var map = new MapView { ApiKey = "" };


            //scheduler.Views.Add(map);


            return View(scheduler);
        }
        public ActionResult Data()
        {//events for loading to scheduler
            var events = _db.Events;
            var formatedEvents = new List<object>();
            foreach (var ev in events)
            {
                var formatingEvents = new
                {
                    id = ev.ID,
                    start_date = ev.EventStartDate.ToString(),
                    end_date = ev.EventEndDate.ToString(),
                    location=ev.EventLocation
                };
                formatedEvents.Add(formatingEvents);
            }
            return Json(formatedEvents, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult Data()
        //{
        //    var events = _db.Events;

        //    var formatedEvents = new List<object>();
        //    foreach (var ev in events)
        //    {
        //        var formattingEvent = new
        //        {
        //            id = ev.ID,
        //            start_date = ev.EventStartDate.ToString(),
        //            end_date = ev.EventEndDate.ToString(),
        //            text = ev.EventDescription,
        //            location = ev.EventLocation

        //        };
        //        formatedEvents.Add(formattingEvent);
        //    }
        //    return Json(formatedEvents, JsonRequestBehavior.AllowGet);

        //}
        //public ContentResult Data()
        //{
        //    return (new SchedulerAjaxData(
        //       new ApplicationDbContext().Events
        //        .Select(e => new { e.ID, e.EventDescription, e.EventStartDate, e.EventEndDate })
        //        )
        //        );
        //}
        //public ContentResult Data()
        //{
        //    var data = new SchedulerAjaxData(new ApplicationDbContext().Events);
        //    return (ContentResult)data;
        //}

        public ActionResult Save(string id, string location, string text, string start_date, string end_date, FormCollection formData)
        {
            var action = new DataAction(formData);
            var existingEvent = _db.Events.FirstOrDefault(e => e.ID.ToString() == id);
            var newStartDate = Convert.ToDateTime(start_date);
            var newEndDate = Convert.ToDateTime(end_date);

            try
            {
                switch (action.Type)
                {
                    case DataActionTypes.Insert: // your Insert logic
                        var newEvent = new Events()
                        {
                            EventStartDate = newStartDate,
                            EventEndDate = newEndDate,
                            EventDescription = text,
                            EventLocation = location
                        };

                        _db.Events.Add(newEvent);
                        break;
                    case DataActionTypes.Delete: // your Delete logic

                        _db.Events.Remove(existingEvent);
                        break;
                    default:// "update" // your Update logic
                        existingEvent.EventStartDate = newStartDate;
                        existingEvent.EventEndDate = newEndDate;
                        existingEvent.EventDescription = text;
                        existingEvent.EventLocation = location;

                        break;
                }
                _db.SaveChanges();

                action.TargetId = existingEvent.ID;
            }


            catch (Exception e)
            {
                action.Type = DataActionTypes.Error;
            }

            return (new AjaxSaveResponse(action));
        }



    }

}
