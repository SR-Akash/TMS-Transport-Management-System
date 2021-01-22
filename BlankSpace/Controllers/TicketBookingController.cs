using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlankSpace.Database;
using BlankSpace.Helper;
using BlankSpace.Models;
using BlankSpace.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BlankSpace.Controllers
{
    public class TicketBookingController : Controller
    {
        private readonly DatabaseContext _context;

        public TicketBookingController(DatabaseContext context)
        {
            _context = context;
        }

        public JsonResult CheckRoute(int StartingFrom)
        {
            var n = _context.BusSchedules.AsNoTracking().Where(s => s.StartingFrom == StartingFrom).FirstOrDefault();
            if (n == null)
            {
                return Json(data: false);
            }
            else
            {
                return Json(data: true);
            }
        }
        //[HttpGet]
        //[HttpPost]
        //public JsonResult CheckDes(int id)
        //{
        //    var d = _context.BusSchedules.AsNoTracking().Where(s => s.BusScheduleId == id).FirstOrDefault();
        //    return Json(d.Destination);
        //}
        [HttpGet]
        [HttpPost]
        public JsonResult checkboth(int id, int id2)
        {
            var d = _context.BusSchedules.AsNoTracking().Where(s => s.StartingFrom == id && s.Destination == id2).ToList();
            var bus = _context.Buses.ToList();
            var de = from i in d
                     join b in bus on i.BusId equals b.BusId
                     select new
                     {
                         BusId = i.BusId,
                         CoachName = b.CoachName
                     };

            var slist = new SelectList(de, "BusId", "CoachName");
            return Json(slist);
        }

        [HttpGet]
        [HttpPost]
        public JsonResult checktime(int id, int id2)
        {
            var d = _context.BusSchedules.AsNoTracking().Where(s => s.StartingFrom == id && s.Destination == id2).FirstOrDefault();
            return Json(d.Time);
        }
        public JsonResult checkid(int id, int id2)
        {
            var d = _context.BusSchedules.AsNoTracking().Where(s => s.StartingFrom == id && s.Destination == id2).FirstOrDefault();
            return Json(d.BusScheduleId);
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserRole") == "2")
            {
                var start = _context.Places.AsNoTracking().ToList();
                ViewBag.Start = new SelectList(start, "PlaceId", "PlaceName");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Agent()
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
        public IActionResult Booking(BookingVm b)
        {
            var TicketList = _context.TicketReservations.
                 AsNoTracking().Include(a => a.BusSchedule).
                 Where(s => s.BusSchedule.BusId == b.BusId && s.BusSchedule.StartingFrom == b.StartingFrom
                 && s.BusSchedule.Destination == b.Destination && s.Date == b.Date).ToList();
            if (TicketList.Count() == 0)
            {
                var Seat = _context.Buses.Where(s => s.BusId == b.BusId).FirstOrDefault();
                var Tickets = GenarateTicket.Ticket(Seat.TotalSeat, b.BookingVmId, b.Date);

                var SeatSent = new List<TicketReservationVm>();
                foreach (var item in Tickets)
                {
                    _context.TicketReservations.Add(item);
                    _context.SaveChanges();
                    TicketReservationVm a = new TicketReservationVm()
                    {
                        TicketReservationId = item.TicketReservationId,
                        ConfirmStatus = item.ConfirmStatus,
                        Date = item.Date,
                        AgentId = item.AgentId,
                        PassengerId = item.PassengerId,
                        BusScheduleId = item.BusScheduleId,
                        SeatNumber = item.SeatNumber
                    };
                    if (item.ConfirmStatus == false)
                    {
                        a.MightBeReserve = true;
                    }
                    else
                    {
                        a.MightBeReserve = false;
                    }
                    SeatSent.Add(a);
                }
                var price = _context.BusSchedules.AsNoTracking().
                    Where(s => s.BusScheduleId == Tickets[1].BusScheduleId).FirstOrDefault();
                ViewBag.Price = price.TicketPrice;
                return View(SeatSent);
            }
            else
            {
                var Ticket = _context.TicketReservations.
                 AsNoTracking().Include(a => a.BusSchedule).
                 Where(s => s.BusSchedule.BusId == b.BusId && s.BusSchedule.StartingFrom == b.StartingFrom
                 && s.BusSchedule.Destination == b.Destination
                 && s.Date.Month == b.Date.Month
                 && s.Date.Year == b.Date.Year
                 && s.Date.Day == b.Date.Day).ToList();

                var SeatSent = new List<TicketReservationVm>();
                foreach (var item in Ticket)
                {

                    TicketReservationVm a = new TicketReservationVm()
                    {
                        TicketReservationId = item.TicketReservationId,
                        ConfirmStatus = item.ConfirmStatus,
                        Date = item.Date,
                        AgentId = item.AgentId,
                        PassengerId = item.PassengerId,
                        BusScheduleId = item.BusScheduleId,
                        SeatNumber = item.SeatNumber
                    };
                    if (item.ConfirmStatus == false)
                    {
                        a.MightBeReserve = true;
                    }
                    SeatSent.Add(a);
                }
                var price = _context.BusSchedules.AsNoTracking().
                    Where(s => s.BusScheduleId == Ticket[1].BusScheduleId).FirstOrDefault();
                ViewBag.Price = price.TicketPrice;
                return View(SeatSent);
            }
        }

        [HttpPost]
        public IActionResult ConfirmTicket(List<TicketReservationVm> a, string PassengerName, int PassengerMobile)
        {
            var ab = a.Where(s => s.ConfirmStatus == true && s.MightBeReserve == true).ToList();

            if (ab.Count != 0)
            {
                Passenger p = new Passenger()
                {
                    PassengerId = 0,
                    Mobile = PassengerMobile,
                    Name = PassengerName

                };
                _context.Passengers.Add(p);
                _context.SaveChanges();

                var seatList = new List<string>();

                foreach (var item in ab)
                {
                    TicketReservation c = new TicketReservation()
                    {
                        TicketReservationId = item.TicketReservationId,
                        AgentId = item.AgentId,
                        ConfirmStatus = item.ConfirmStatus,
                        BusScheduleId = item.BusScheduleId,
                        Date = item.Date,
                        PassengerId = p.PassengerId,
                        SeatNumber = item.SeatNumber
                    };

                    _context.TicketReservations.Update(c);
                    _context.SaveChanges();
                    seatList.Add(c.SeatNumber);
                }

                var ss = _context.BusSchedules.AsNoTracking().Include(sp => sp.Bus).ToList();
                var pa = _context.Places.AsNoTracking().ToList();
                var pp = _context.Places.AsNoTracking().ToList();
                var s = from abc in ss
                        join ab1 in pa on abc.StartingFrom equals ab1.PlaceId
                        join aaab in pp on abc.Destination equals aaab.PlaceId
                        where abc.BusScheduleId == ab[0].BusScheduleId
                        select new
                        {
                            DestinationName = aaab.PlaceName,
                            StartingFromName = ab1.PlaceName,
                            Time = abc.Time,

                            CoachName = abc.Bus.CoachName,
                            TicketPrice = abc.TicketPrice,

                        };

                TicketVm t = new TicketVm()
                {
                    PassengerName = p.Name,
                    Mobile = p.Mobile,
                    Seat = seatList,
                };
                foreach (var item in s)
                {
                    t.Destination = item.DestinationName;
                    t.StartFrom = item.StartingFromName;

                    t.TicketPrice = item.TicketPrice;
                    t.Date = a[0].Date;
                    t.BusName = item.CoachName;
                    t.Time = item.Time;
                }
                t.TotalCost = t.TicketPrice * ab.Count();
                return View(t);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Scripts()
        {
            if (HttpContext.Session.GetString("UserRole") == "2")
            {
                var a = _context.Buses.AsEnumerable().ToList();
                ViewBag.Bus = new SelectList(a, "BusId", "CoachName");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
         public IActionResult ScriptsGenarate(ScriptVm aa) 
        {
            if (HttpContext.Session.GetString("UserRole") == "2")
            {
                var a = _context.BusSchedules.AsNoTracking().Where(s => s.BusId == aa.BusId).Include(cp=>cp.Bus).FirstOrDefault();
                var ss = _context.TicketReservations.
                    Include(s => s.Passenger).Include(ab => ab.BusSchedule).ToList();
                var paseenger = _context.Passengers.AsNoTracking().ToList();
                var Query = from t in _context.TicketReservations.Include(x=>x.BusSchedule).ThenInclude(y => y.Bus)
                            .Include(m => m.BusSchedule.Place).
                             Include(s => s.Passenger).ToList()/*/*.Include(ab => ab.BusSchedule)/*where*/
                                                      //t.Date.Day == aa.Date.Day
                                                      //&& t.Date.Month == aa.Date.Month
                                                      //&& t.Date.Year == aa.Date.Year
                            where t.PassengerId > 1
                             && t.Date.Month == aa.Date.Month
                             && t.Date.Year == aa.Date.Year
                             && t.Date.Day == aa.Date.Day
                             &&t.BusSchedule.BusId==aa.BusId
                            group t by t.PassengerId into groupPassenger
                            let temp = (
                            from val in groupPassenger
                            select new
                            {
                                Name=val.Passenger.Name,
                                Mobile=val.Passenger.Mobile,
                                Seat=val.SeatNumber,
                                Statring =val.BusSchedule.Place.PlaceName,
                                BusName=val.BusSchedule.Bus.CoachName,
                            }
                            ) select temp;
                if(Query.Count()==0)
                {
                    ViewBag.SMS = "No Data";
                    return RedirectToAction("Scripts");
                }

                var Destinaio0n =  _context.Places.Where(s => s.PlaceId == a.Destination).FirstOrDefault();
                ViewBag.Destinaion = Destinaio0n.PlaceName;


                var sent = new List<ScriptVm>();
               
               
                foreach (var item in Query)
                {
                    var str = new List<string>();


                    ScriptVm scriptVm = new ScriptVm();
                    int abc = 1;
                    foreach (var v in item)
                    {
                        if (abc == 1)
                        {
                             scriptVm = new ScriptVm();
                        }
                        scriptVm.Name = v.Name;
                        scriptVm.Mobile = v.Mobile;
                        scriptVm.Route = v.Statring;
                        scriptVm.BusName = v.BusName;
                        str.Add(v.Seat);
                        abc++;
                    }
                    scriptVm.Seat = str;
                    sent.Add(scriptVm);
                }

                return View(sent);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

    }
}