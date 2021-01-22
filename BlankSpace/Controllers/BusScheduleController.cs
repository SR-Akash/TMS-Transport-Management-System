using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlankSpace.Database;
using BlankSpace.Models;
using BlankSpace.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BlankSpace.Controllers
{
    public class BusScheduleController : Controller
    {
        private readonly DatabaseContext _context;
        [TempData]
        public string msg { get; set; }
        public BusScheduleController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [HttpPost]
        public JsonResult check(int id)
        {
            var d = _context.Places.AsNoTracking().Where(s => s.PlaceId != id).ToList();
            var slist = new SelectList(d, "PlaceId", "PlaceName");

            return Json(slist);
        }

        public IActionResult AddSchedule()
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var bus = _context.Buses.AsNoTracking().ToList();
                ViewBag.Bus = new SelectList(bus, "BusId", "CoachName");

                var start = _context.Places.AsNoTracking().ToList();
                ViewBag.Start = new SelectList(start, "PlaceId", "PlaceName");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        } 

        [HttpPost]
        public IActionResult AddSchedule(BusScheduleVm a)
        {
            var time = _context.BusSchedules.FirstOrDefault(m=>m.Time==a.Time && m.BusId == a.BusId); 
            if(time==null)
            {
                BusSchedule s = new BusSchedule()
                {
                    BusScheduleId = 0,
                    Destination = a.Destination,
                    StartingFrom = a.StartingFrom,
                    Time = a.Time,
                    BusId = a.BusId,
                    TicketPrice = a.TicketPrice
                };

                _context.BusSchedules.Add(s);
                _context.SaveChanges();
                msg = "Schedule Added";
                return RedirectToAction(nameof(ScheduleList));
            }
            else
            {
                msg = "Schedule Already Created";
                return RedirectToAction(nameof(AddSchedule));
            }
            
        }

        public IActionResult ScheduleList()
        {
            if (HttpContext.Session.GetString("UserRole") != null)
            {
                var ss = _context.BusSchedules.AsNoTracking().Include(sp => sp.Bus).ToList();
                var p = _context.Places.AsNoTracking().ToList();
                var pp = _context.Places.AsNoTracking().ToList();
                var s = from a in ss
                        join ab in p on a.StartingFrom equals ab.PlaceId
                        join aaab in pp on a.Destination equals aaab.PlaceId
                        select new
                        {
                            BusScheduleId = a.BusScheduleId,
                            DestinationName = aaab.PlaceName,
                            StartingFromName = ab.PlaceName,
                            Time = a.Time,

                            CoachName = a.Bus.CoachName,
                            TicketPrice = a.TicketPrice
                        };
                var sent = new List<BusScheduleVm>();
                int c = 1;
                foreach (var item in s)
                {
                    BusScheduleVm sp = new BusScheduleVm()
                    {
                        BusScheduleId = item.BusScheduleId,
                        DestinationName = item.DestinationName,
                        StartingFromName = item.StartingFromName,
                        Time = item.Time,
                        Serial = c,
                        CoachName = item.CoachName,
                        TicketPrice = item.TicketPrice
                    };
                    sent.Add(sp);
                    c++;
                }
                return View(sent);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult ScheduleListForAll()
        {
            if (HttpContext.Session.GetString("UserRole") != null)
            {
                var ss = _context.BusSchedules.AsNoTracking().Include(sp => sp.Bus).ToList();
                var p = _context.Places.AsNoTracking().ToList();
                var pp = _context.Places.AsNoTracking().ToList();
                var s = from a in ss
                        join ab in p on a.StartingFrom equals ab.PlaceId
                        join aaab in pp on a.Destination equals aaab.PlaceId
                        select new
                        {
                            BusScheduleId = a.BusScheduleId,
                            DestinationName = aaab.PlaceName,
                            StartingFromName = ab.PlaceName,
                            Time = a.Time,

                            CoachName = a.Bus.CoachName,
                            TicketPrice = a.TicketPrice
                        };
                var sent = new List<BusScheduleVm>();
                int c = 1;
                foreach (var item in s)
                {
                    BusScheduleVm sp = new BusScheduleVm()
                    {
                        BusScheduleId = item.BusScheduleId,
                        DestinationName = item.DestinationName,
                        StartingFromName = item.StartingFromName,
                        Time = item.Time,
                        Serial = c,
                        CoachName = item.CoachName,
                        TicketPrice = item.TicketPrice
                    };
                    sent.Add(sp);
                    c++;
                }
                return View(sent);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult DetailsSchedule(int id)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var s = _context.BusSchedules.AsNoTracking().Where(sa => sa.BusScheduleId == id).Include(p => p.Bus).FirstOrDefault();
                var Destination = _context.Places.Where(ss => ss.PlaceId == s.Destination).FirstOrDefault();
                var Start = _context.Places.Where(ss => ss.PlaceId == s.StartingFrom).FirstOrDefault();

                BusScheduleVm sp = new BusScheduleVm()
                {
                    BusScheduleId = s.BusScheduleId,
                    DestinationName = Destination.PlaceName,
                    StartingFromName = Start.PlaceName,
                    Time = s.Time,
                    TicketPrice = s.TicketPrice,
                    CoachName = s.Bus.CoachName
                };
                return View(sp);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult DeleteSchedule(int id)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var s = _context.BusSchedules.AsNoTracking().Where(sa => sa.BusScheduleId == id).Include(a => a.Bus).FirstOrDefault();
                var Destination = _context.Places.Where(ss => ss.PlaceId == s.Destination).FirstOrDefault();
                var Start = _context.Places.Where(ss => ss.PlaceId == s.StartingFrom).FirstOrDefault();

                BusScheduleVm sp = new BusScheduleVm()
                {
                    BusScheduleId = s.BusScheduleId,
                    DestinationName = Destination.PlaceName,
                    StartingFromName = Start.PlaceName,
                    Time = s.Time,
                    TicketPrice = s.TicketPrice,
                    CoachName = s.Bus.CoachName
                };
                return View(sp);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult DeleteSchedule(BusScheduleVm a)
        {
            var s = _context.BusSchedules.AsNoTracking().Where(sa => sa.BusScheduleId == a.BusScheduleId).FirstOrDefault();

            _context.BusSchedules.Remove(s);
            _context.SaveChanges();
            msg = "Deleted Successfully!";
            return RedirectToAction(nameof(ScheduleList));
        } 

        public IActionResult UpdateSchedule(int  id) 
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var bus = _context.Buses.AsNoTracking().ToList();
                ViewBag.Bus = new SelectList(bus, "BusId", "CoachName");
                var Route1 = _context.Places.AsNoTracking().ToList();
                ViewBag.Road = new SelectList(Route1, "PlaceId", "PlaceName");
                var s = _context.BusSchedules.AsNoTracking().Where(sa => sa.BusScheduleId == id).FirstOrDefault();

                BusScheduleVm sp = new BusScheduleVm()
                {
                    BusScheduleId = s.BusScheduleId,
                    Destination = s.Destination,
                    StartingFrom = s.StartingFrom,
                    Time = s.Time,
                    BusId = s.BusId,
                    TicketPrice = s.TicketPrice
                };
                return View(sp);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult UpdateSchedule(BusScheduleVm a)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                BusSchedule s = new BusSchedule()
                {
                    BusScheduleId = a.BusScheduleId,
                    Destination = a.Destination,
                    StartingFrom = a.StartingFrom,
                    Time = a.Time,
                    BusId = a.BusId,
                    TicketPrice = a.TicketPrice


                };
                _context.BusSchedules.Update(s);
                _context.SaveChanges();
                return RedirectToAction("ScheduleList");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult AddRoute()
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }

        [HttpPost]
        public IActionResult AddRoute(PlaceVm a)
        {
            var i = _context.Places.Where(x => x.PlaceName == a.PlaceName).FirstOrDefault();
            Place b = new Place()
            {
                PlaceId = a.PlaceVmId,
                PlaceName = a.PlaceName
            };
            if(i==null)
            {
                _context.Places.Add(b);
                _context.SaveChanges();
                msg = "New Route Added!";
                return RedirectToAction(nameof(RouteList));
            }
            else
            {
                msg = " Route Already Exist!";
                return RedirectToAction(nameof(AddRoute));
            }
            
        }

        public IActionResult UpdateRoute(int id) 
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var item = _context.Places.AsNoTracking().Where(s => s.PlaceId == id).FirstOrDefault();

                PlaceVm p = new PlaceVm()
                {
                    PlaceName = item.PlaceName,
                    PlaceVmId = item.PlaceId,
                };

                return View(p);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult UpdateRoute(PlaceVm a)
        {
            var i = _context.Places.Where(x => x.PlaceName == a.PlaceName).FirstOrDefault();
            Place p = new Place()
            {
                PlaceName = a.PlaceName,
                PlaceId = a.PlaceVmId,
            };
            if(i==null)
            {
                _context.Places.Update(p);
                _context.SaveChanges();
                msg = "Route Updated";
                return RedirectToAction(nameof(RouteList));
            }
            else
            {
                msg = "Route Already Exist!";
                return RedirectToAction(nameof(UpdateRoute));
            }
        }

        public IActionResult DeleteRoute(int id) 
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                try
                {
                    var sp = _context.Places.Where(s => s.PlaceId == id).FirstOrDefault();
                    _context.Places.Remove(sp);
                    _context.SaveChanges();
                    msg = "Route Deleted Successfully";
                    return RedirectToAction("RouteList");
                }
                catch (Exception)
                {
                    ViewBag.Error = "You can not delete this as it is used in another table ";
                    return RedirectToAction("RouteList");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult RouteList()
        {
            if (HttpContext.Session.GetString("UserRole") != null)
            {

                var a = _context.Places.AsNoTracking().ToList();
                var s = new List<PlaceVm>();
                int c = 1;
                foreach (var item in a)
                {
                    PlaceVm p = new PlaceVm()
                    {
                        PlaceName = item.PlaceName,
                        PlaceVmId = item.PlaceId,
                        Serial = c
                    };
                    s.Add(p);
                    c++;
                }
                return View(s);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

    }
}