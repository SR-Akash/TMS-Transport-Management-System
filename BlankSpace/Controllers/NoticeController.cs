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
    public class NoticeController : Controller
    {
        private readonly DatabaseContext _context;
        [TempData]
        public string msg { get; set; }
        public NoticeController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult NoticeListForAll()
        {
            if (HttpContext.Session.GetString("UserRole") != null)
            {
                var s = _context.Notice.AsNoTracking().ToList();
                var dr = new List<Notice>();
                int c = 1;
                foreach (var item in s)
                {
                    Notice se = new Notice()
                    {
                        NoticeId = item.NoticeId,
                        NoticeDate = item.NoticeDate,
                        NoticeName = item.NoticeName
                    };
                    se.NoticeId = c;
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

        public IActionResult AddNotice()
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
        public IActionResult AddNotice(Notice d)
        {
            var a = _context.Notice.Where(x => x.NoticeDate == d.NoticeDate && x.NoticeName == d.NoticeName).FirstOrDefault();
            Notice di = new Notice()
            {
                NoticeId = d.NoticeId,
                NoticeDate = d.NoticeDate,
                NoticeName = d.NoticeName
            };
           
            if(a==null)
            {

                _context.Notice.Add(di);
                _context.SaveChanges();
                msg = "Successful";

                return RedirectToAction(nameof(NoticeList));
            }
            else
            {
                msg = "Notice Can't be duplicated";
                return RedirectToAction(nameof(AddNotice));
            }

        }

        public IActionResult NoticeList()
        {
            if (HttpContext.Session.GetString("UserRole") != null)
            {
                var s = _context.Notice.AsNoTracking().ToList();
                var dr = new List<Notice>();
                int c = 1;
                foreach (var item in s)
                {
                    Notice se = new Notice()
                    {
                        NoticeId = item.NoticeId,
                        NoticeDate = item.NoticeDate,
                        NoticeName = item.NoticeName
                    };
                    se.NoticeId = c;
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

        public IActionResult UpdateNotice(int id)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var item = _context.Notice.AsNoTracking().Where(s => s.NoticeId == id).FirstOrDefault();
                Notice aa = new Notice()
                {
                    NoticeId = item.NoticeId,
                    NoticeDate = item.NoticeDate,
                    NoticeName = item.NoticeName
                };
                return View(aa);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public IActionResult UpdateNotice(Notice val)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var che = _context.Notice.Where(x => x.NoticeDate == val.NoticeDate && x.NoticeName == val.NoticeName).FirstOrDefault();
                Notice di = new Notice()
                {
                    NoticeId = val.NoticeId,
                    NoticeDate = val.NoticeDate,
                    NoticeName = val.NoticeName

                };
                if(che==null)
                {
                    _context.Notice.Update(di);
                    _context.SaveChanges();
                    msg = "Notice Updated Successfully";
                    return RedirectToAction(nameof(NoticeList));
                }
                else
                {
                    msg = "Notice Already Exist";
                    return RedirectToAction(nameof(UpdateNotice));
                }
        
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public IActionResult DeleteNotice(int id)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var item = _context.Notice.AsNoTracking().Where(s => s.NoticeId == id).FirstOrDefault();
                Notice se = new Notice()
                {
                    NoticeId = item.NoticeId,
                    NoticeDate = item.NoticeDate,
                    NoticeName = item.NoticeName
                  
                };
                return View(se);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public IActionResult DeleteNotice(Notice se)
        {
            _context.Notice.Remove(_context.Notice.AsNoTracking().Where(s => s.NoticeId == se.NoticeId).FirstOrDefault());
            _context.SaveChanges();
            msg = "Deleted Successfully";
            return RedirectToAction("NoticeList");
        }

        public IActionResult NoticeDetails(int id)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var details = _context.Notice.AsNoTracking().Where(s => s.NoticeId == id).FirstOrDefault();

                Notice a = new Notice()
                {
                    NoticeDate = details.NoticeDate,
                    NoticeName = details.NoticeName
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
