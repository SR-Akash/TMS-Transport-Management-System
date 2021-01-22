using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlankSpace.Models;
using BlankSpace.ViewModels;
using BlankSpace.Database;
using Microsoft.AspNetCore.Http;

namespace BlankSpace.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;
        [TempData]
        public string msg { get; set; }
        public HomeController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var role = _context.RoleTypes.ToList();
            ViewBag.Role = role;
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserVm u)
        {
            var admin = new User();
            var student = new Student();
            var teacher = new Teacher();
            
            
            if (u.Role == 1)
            {
                admin = _context.Users.Where(s => s.UserName == u.UserName && s.Password == u.Password).FirstOrDefault();
                if (admin != null)
                {
                    HttpContext.Session.SetString("UserName", admin.UserName);
                    HttpContext.Session.SetString("UserRole", admin.RoleTypeId.ToString());

                    return RedirectToAction("Index", "Bus");
                }

                else
                {
                    msg = "Username or Password is Incorrect!!";
                    return RedirectToAction("Index");
                }
            }

           else if (u.Role == 2)
            {
                teacher = _context.Teacher.Where(s => s.Email == u.UserName && s.Password == u.Password).FirstOrDefault();
                if(teacher!=null)
                {
                    HttpContext.Session.SetString("UserName", teacher.Email);
                    HttpContext.Session.SetString("UserRole", teacher.RoleTypeId.ToString());

                    return RedirectToAction("Index", "Teacher");
                }
                else
                {
                    msg = "Username or Password is Incorrect!!";
                    return RedirectToAction("Index");
                }

            }
           else if (u.Role == 4)
            {
                student = _context.Student.Where(s => s.Email == u.UserName && s.Password == u.Password).FirstOrDefault();
                if (student != null)
                {
                    HttpContext.Session.SetString("UserName", student.Email);
                    HttpContext.Session.SetString("UserRole", student.RoleTypeId.ToString());

                    return RedirectToAction("Index", "Student");
                }
                  else
                {
                    msg = "Username or Password is Incorrect!!";
                    return RedirectToAction("Index");
                }
            }

            else
                msg = "Username or Password is Incorrect!!";
            return RedirectToAction("Index");


            ////var result = _context.Users.Where(s => s.UserName == u.UserName && s.Password == u.Password).FirstOrDefault();
            //if (result!=null)
            //{
            //    HttpContext.Session.SetString("UserName", result.UserName);
            //    HttpContext.Session.SetString("UserRole", result.RoleTypeId.ToString());
            //    if (result.RoleTypeId == 1)
            //    {
            //        return RedirectToAction("Index", "Bus");
            //    }
            //    if (result.RoleTypeId == 2)
            //    {
            //        return RedirectToAction("Index", "TicketBooking");
            //    }
            //}
            //else
            //    msg = "Username or Password is Incorrect!!";
            //    return RedirectToAction("Index");



            //return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            ViewBag.Logout = "Logout Successful";
            return RedirectToAction("Index");
        }


    }
}
