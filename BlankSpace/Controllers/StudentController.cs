using BlankSpace.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlankSpace.Models;
using BlankSpace.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace BlankSpace.Controllers
{
    public class StudentController : Controller
    {
       
        private readonly DatabaseContext _context;
        [TempData]
        public string msg { get; set; }
        public StudentController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddStudent()
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
        public IActionResult AddStudent(Student d)
        {
            var i = _context.Student.Where(x => x.UniversityId == d.UniversityId).FirstOrDefault();

            Student di = new Student()
            {
                StudentId = d.StudentId,
                StudentName = d.StudentName,
                UniversityId = d.UniversityId,
                Email = d.Email,
                Password = d.Password,
                Phone = d.Phone,
                RoleTypeId = 4,
                RoleType = "Student"
            };

            if(i==null)
            {
                _context.Student.Add(di);
                _context.SaveChanges();
                msg = " Student Added Successful";

                return RedirectToAction(nameof(AddStudent));
            }
            else
            {
                msg = "Student Already Exist!";
                return RedirectToAction(nameof(AddStudent));
            }
        }

        public IActionResult StudentList(string search)
        {
       
            var result = from m in _context.Student select m;
            
            if (!string.IsNullOrEmpty(search))
            {
                var sdr = result.Where(p => p.StudentName.Contains(search));
                return View(sdr.ToList());
            }
            else
            {
                var dr = _context.Student.ToList();

                return View(dr);
            }
            

        }

        public IActionResult UpdateStudent(int id)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var item = _context.Student.AsNoTracking().Where(s => s.StudentId == id).FirstOrDefault();
                Student aa = new Student()
                {
                    StudentId = item.StudentId,
                    StudentName = item.StudentName,
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
        public IActionResult UpdateStudent(Student val)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                //var res = _context.Student.Where(x => x.UniversityId == val.UniversityId).FirstOrDefault();
                Student di = new Student()
                {
                    StudentId = val.StudentId,
                    StudentName = val.StudentName,
                    UniversityId = val.UniversityId,
                    Email = val.Email,
                    Password = val.Password,
                    Phone = val.Phone
                };
               
                    _context.Student.Update(di);
                    _context.SaveChanges();
                    msg = "Student Updated Successfully";
                    return RedirectToAction(nameof(StudentList));
              
      
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        public IActionResult DeleteStudent(int id)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var item = _context.Student.AsNoTracking().Where(s => s.StudentId == id).FirstOrDefault();
                Student se = new Student()
                {
                    StudentId = item.StudentId,
                    StudentName = item.StudentName,
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
        public IActionResult DeleteStudent(Student se)
        {
            _context.Student.Remove(_context.Student.AsNoTracking().Where(s => s.StudentId == se.StudentId).FirstOrDefault());
            _context.SaveChanges();
            msg = "Student Deleted Successfully";
            return RedirectToAction("StudentList");
        }

        public IActionResult StudentDetails(int id)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var details = _context.Student.AsNoTracking().Where(s => s.StudentId == id).FirstOrDefault();

                Student a = new Student()
                {
                    StudentName = details.StudentName,
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
