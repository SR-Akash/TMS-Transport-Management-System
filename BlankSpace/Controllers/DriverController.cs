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
    public class DriverController : Controller
    {
        private readonly DatabaseContext _context;
        [TempData]
        public string msg { get; set; }
        public DriverController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult NewDriver()
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
        public IActionResult NewDriver(DriverVm d)
        {
            var i = _context.Drivers.Where(x => x.LicenseNumber == d.LicenseNumber).FirstOrDefault();
            Driver di = new Driver()
            { DriverId = d.DriverVmId,
                Name = d.Name,
                Address = d.Address,
                LicenseNumber = d.LicenseNumber,
                Mobile = d.Mobile
            };
            if(i==null)
            {
                _context.Drivers.Add(di);
                _context.SaveChanges();
                msg = "New Driver Added!";
                return RedirectToAction(nameof(DriverList));
            }
            else
            {
                msg = "Driver Already Exist!";
                return RedirectToAction(nameof(NewDriver));
            }
         
          
        }

        public IActionResult DriverList()
        {
            if (HttpContext.Session.GetString("UserRole") != null)
            {
                var s = _context.Drivers.AsNoTracking().ToList();
                var dr = new List<DriverVm>();
                int c = 1;
                foreach (var item in s)
                {
                    DriverVm se = new DriverVm()
                    {
                        DriverVmId = item.DriverId,
                        Name = item.Name,
                        Address = item.Address,
                        Mobile = item.Mobile,
                        LicenseNumber = item.LicenseNumber

                    };
                    se.DriverSerial = c;
                    c++;
                    dr.Add(se);
                }
                return View(dr);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }
        public IActionResult UpdateDriver(int id)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var item = _context.Drivers.AsNoTracking().Where(s => s.DriverId == id).FirstOrDefault();
                DriverVm se = new DriverVm()
                {
                    DriverVmId = item.DriverId,
                    Name = item.Name,
                    Address = item.Address,
                    Mobile = item.Mobile,
                    LicenseNumber = item.LicenseNumber,

                };
                return View(se);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }
        [HttpPost]
        public IActionResult UpdateDriver(DriverVm d)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                //var i = _context.Drivers.Where(x => x.LicenseNumber == d.LicenseNumber|| x.Name == d.Name).FirstOrDefault();
                Driver di = new Driver()
                {
                    DriverId = d.DriverVmId,
                    Name = d.Name,
                    Address = d.Address,
                    LicenseNumber = d.LicenseNumber,
                    Mobile = d.Mobile

                };
              
                    _context.Drivers.Update(di);
                    _context.SaveChanges();
                    msg = "Edited Successfully!";
                    return RedirectToAction(nameof(DriverList));
             
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        public IActionResult DeleteDriver(int id)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var item = _context.Drivers.AsNoTracking().Where(s => s.DriverId == id).FirstOrDefault();
                DriverVm se = new DriverVm()
                {
                    DriverVmId = item.DriverId,
                    Name = item.Name,
                    Address = item.Address,
                    Mobile = item.Mobile,
                    LicenseNumber = item.LicenseNumber,

                };
                return View(se);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }
        [HttpPost]
        public IActionResult DeleteDriver(DriverVm se)
        {
            _context.Drivers.Remove(_context.Drivers.AsNoTracking().Where(s => s.DriverId == se.DriverVmId).FirstOrDefault());
            _context.SaveChanges();
            msg = "Deleted Successfully";
            return RedirectToAction("DriverList");
        }
        public IActionResult DriverDetails(int id)

        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var item = _context.Drivers.AsNoTracking().Where(s => s.DriverId == id).FirstOrDefault();
                DriverVm se = new DriverVm()
                {
                    DriverVmId = item.DriverId,
                    Name = item.Name,
                    Address = item.Address,
                    Mobile = item.Mobile,
                    LicenseNumber = item.LicenseNumber,

                };
                return View(se);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }
        public IActionResult AssignBus()
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var driver = _context.Drivers.AsNoTracking().ToList();
                ViewBag.Driver = new SelectList(driver, "DriverId", "Name");
                var bus = _context.Buses.AsNoTracking().ToList();
                ViewBag.Bus = new SelectList(bus, "BusId", "CoachName");


                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }
        [HttpPost]
        public IActionResult AssignBus(AssignedDriverVm a)
        {
            var sa = _context.AssignedDrivers.AsNoTracking().
                Where(s => s.BusId == a.BusId || s.DriverId == a.DriverId).FirstOrDefault();
            if(sa!=null)
            {
                msg = "Driver Already Assigned For This Bus";
                return RedirectToAction(nameof(AssignBus));
            }

            AssignedDriver ab = new AssignedDriver() {
                AssignedDriverId = 0,
                BusId = a.BusId,
                DriverId = a.DriverId
            };
            _context.AssignedDrivers.Add(ab);
            _context.SaveChanges();
            msg = "Bus Assign Successfully";
            return RedirectToAction("AssignBusList");
        }
        public IActionResult AssignBusList()
        {
            if (HttpContext.Session.GetString("UserRole") != null)
            {
                var assignDriverList = _context.AssignedDrivers.
                                AsNoTracking().Include(s => s.Bus).
                                Include(s => s.Driver).ToList();
                var sent = new List<AssignedDriverVm>();
                int c = 1;
                foreach (var item in assignDriverList)
                {
                    AssignedDriverVm a = new AssignedDriverVm()
                    {

                        DriverName = item.Driver.Name,
                        CoachName = item.Bus.CoachName,
                        AssignedDriverId = item.AssignedDriverId,
                        Serial = c

                    };
                    c++;
                    sent.Add(a);

                }
                return View(sent);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        public IActionResult DetailsAssignDriver(int id)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var assignDriver = _context.AssignedDrivers.
                                  AsNoTracking().Include(s => s.Bus).
                                  Include(s => s.Driver).Where(s => s.AssignedDriverId == id).
                                  FirstOrDefault();


                AssignedDriverVm a = new AssignedDriverVm()
                {

                    DriverName = assignDriver.Driver.Name,
                    CoachName = assignDriver.Bus.CoachName,
                    BusNumber = assignDriver.Bus.BusNumber,
                    DriverMobile = assignDriver.Driver.Mobile,
                    TotalSeat = assignDriver.Bus.TotalSeat


                };


                return View(a);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }
        public IActionResult DeleteAssignedDriver(int id)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var assignDriver = _context.AssignedDrivers.
                                  AsNoTracking().Include(s => s.Bus).
                                  Include(s => s.Driver).Where(s => s.AssignedDriverId == id).
                                  FirstOrDefault();


                AssignedDriverVm a = new AssignedDriverVm()
                {

                    DriverName = assignDriver.Driver.Name,
                    CoachName = assignDriver.Bus.CoachName,
                    BusNumber = assignDriver.Bus.BusNumber,
                    DriverMobile = assignDriver.Driver.Mobile,
                    TotalSeat = assignDriver.Bus.TotalSeat,
                    AssignedDriverId = assignDriver.AssignedDriverId

                };


                return View(a);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        } 
        [HttpPost]
        public IActionResult DeleteAssignedDriver(AssignedDriverVm id2)
        {
            var assignDriver = _context.AssignedDrivers.
                                AsNoTracking().Where(s => s.AssignedDriverId == id2.AssignedDriverId).
                                FirstOrDefault();
            _context.AssignedDrivers.Remove(assignDriver);
            _context.SaveChanges();
            return RedirectToAction("AssignBusList");
        }
        public IActionResult UpdateAssignedDriver(int id)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var assignDriver = _context.AssignedDrivers.
                                   AsNoTracking().Where(s => s.AssignedDriverId == id).
                                   FirstOrDefault();

                var driver = _context.Drivers.AsNoTracking().ToList();
                ViewBag.Driver = new SelectList(driver, "DriverId", "Name");
                var bus = _context.Buses.AsNoTracking().ToList();
                ViewBag.Bus = new SelectList(bus, "BusId", "CoachName");


                AssignedDriverVm a = new AssignedDriverVm()
                {

                    DriverId = assignDriver.DriverId,
                    BusId = assignDriver.BusId,
                    AssignedDriverId = assignDriver.AssignedDriverId

                };
                return View(a);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        } 
        [HttpPost]
        public IActionResult UpdateAssignedDriver(AssignedDriverVm ab)
        {
           

           

            
            AssignedDriver a = new AssignedDriver()
            {

                DriverId = ab.DriverId,
                BusId = ab.BusId,
                AssignedDriverId = ab.AssignedDriverId

            };
            _context.AssignedDrivers.Update(a);
            _context.SaveChanges();

            return RedirectToAction("AssignBusList");
        }
           
    }


    }
