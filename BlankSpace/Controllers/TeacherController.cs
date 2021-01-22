using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlankSpace.Database;
using Microsoft.AspNetCore.Http;
using BlankSpace.Models;
using BlankSpace.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BlankSpace.Controllers
{
    public class TeacherController : Controller
    {
        private readonly DatabaseContext _context;
        [TempData]
        public string msg { get; set; }
        public TeacherController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {   
                return View();
        }

        public IActionResult AddTeacher()
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
        public IActionResult AddTeacher(Teacher d)
        {
            var i = _context.Teacher.Where(x => x.UniversityId == d.UniversityId).FirstOrDefault();
            Teacher di = new Teacher()
            {
                TeacherId = d.TeacherId,
                TeacherName = d.TeacherName,
                UniversityId = d.UniversityId,
                Email = d.Email,
                Password = d.Password,
                Phone = d.Phone,
                RoleTypeId = 2,
                RoleType = "Teacher"
            };

            if (i == null)
            {
                _context.Teacher.Add(di);
                _context.SaveChanges();
                msg = " Teacher Added Successful";

                return RedirectToAction(nameof(AddTeacher));
            }
            else
            {
                msg = "This Teacher is Already Exist!";
                return RedirectToAction(nameof(AddTeacher));
            }
        }

        public IActionResult TeacherList(string search)
        {
            var result = from m in _context.Teacher select m;

            if (!string.IsNullOrEmpty(search))
            {
                var sdr = result.Where(p => p.TeacherName.Contains(search));
                return View(sdr.ToList());
            }
            else
            {
                var dr = _context.Teacher.ToList();

                return View(dr);
            }

        }

        public IActionResult UpdateTeacher(int id)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var item = _context.Teacher.AsNoTracking().Where(s => s.TeacherId == id).FirstOrDefault();
                Teacher aa = new Teacher()
                {
                    TeacherId = item.TeacherId,
                    TeacherName = item.TeacherName,
                    UniversityId = item.UniversityId,
                    Email = item.Email,
                    Password = item.Password,
                    Phone = item.Phone
                };
                return View(aa);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public IActionResult UpdateTeacher(Teacher val)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                //var res = _context.Teacher.Where(x => x.UniversityId == val.UniversityId).FirstOrDefault();
                Teacher di = new Teacher()
                {
                    TeacherId = val.TeacherId,
                    TeacherName = val.TeacherName,
                    UniversityId = val.UniversityId,
                    Email = val.Email,
                    Password = val.Password,
                    Phone = val.Phone
                };

                    _context.Teacher.Update(di);
                    _context.SaveChanges();
                    msg = "Teacher Updated Successfully";
                    return RedirectToAction(nameof(TeacherList));
          
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult DeleteTeacher(int id)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var item = _context.Teacher.AsNoTracking().Where(s => s.TeacherId == id).FirstOrDefault();
                Teacher se = new Teacher()
                {
                    TeacherId = item.TeacherId,
                    TeacherName = item.TeacherName,
                    UniversityId = item.UniversityId,
                    Email = item.Email,
                    Password = item.Password,
                    Phone = item.Phone
                };
                return View(se);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public IActionResult DeleteTeacher(Teacher se)
        {
            _context.Teacher.Remove(_context.Teacher.AsNoTracking().Where(s => s.TeacherId == se.TeacherId).FirstOrDefault());
            _context.SaveChanges();
            msg = "Teacher Deleted Successfully";
            return RedirectToAction("TeacherList");
        }

        public IActionResult TeacherDetails(int id)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var details = _context.Teacher.AsNoTracking().Where(s => s.TeacherId == id).FirstOrDefault();

                Teacher a = new Teacher()
                {
                    TeacherName = details.TeacherName,
                    UniversityId = details.UniversityId,
                    Email = details.Email,
                    Password = details.Password,
                    Phone = details.Phone
                };

                return View(a);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
