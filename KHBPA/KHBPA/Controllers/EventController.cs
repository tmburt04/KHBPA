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

namespace KHBPA.Controllers
{
    public class EventController : Controller
    {
        public ActionResult Index()
        {
            var sched = new DHXScheduler(this);
            sched.Skin = DHXScheduler.Skins.Terrace;
            sched.Config.hour_date = "%h:%i %A";
            sched.LoadData = true;
            sched.EnableDataprocessor = true;
            sched.Highlighter.Enable("highlight_section");
            sched.Lightbox.Add(new LightboxText("location", "Location") { Height = 50 });
            sched.Lightbox.AddDefaults();
            sched.Config.first_hour = 5;
            if (!User.IsInRole("Admin"))
            {
                sched.Config.readonly_form = true;
            }
            return View(sched);
        }

        public ContentResult Data()
        {
            return (new SchedulerAjaxData(
                new ApplicationDbContext().Event
                .Select(e => new { e.id, e.text, e.start_date, e.end_date, e.location })
                )
                );
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
            var changedEvent = DHXEventsHelper.Bind<Event>(actionValues);
            var entities = new ApplicationDbContext();
            try
            {
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        entities.Event.Add(changedEvent);
                        break;
                    case DataActionTypes.Delete:
                        changedEvent = entities.Event.FirstOrDefault(ev => ev.id == action.SourceId);
                        entities.Event.Remove(changedEvent);
                        break;
                    default:// "update"
                        var target = entities.Event.Single(e => e.id == changedEvent.id);
                        DHXEventsHelper.Update(target, changedEvent, new List<string> { "id" });
                        break;
                }
                entities.SaveChanges();
                action.TargetId = changedEvent.id;
            }
            catch (Exception a)
            {
                action.Type = DataActionTypes.Error;
            }

            return (new AjaxSaveResponse(action));
        }
    }
}
       