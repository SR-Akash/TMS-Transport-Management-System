using BlankSpace.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlankSpace.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BlankSpace.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly DatabaseContext _context;
        [TempData]
        public string msg { get; set; }
        public ExpenseController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateExpense()
        {
            var expenseviewmodel = new Expense();
            var buslist = _context.Buses.ToList();
            ViewBag.buslist = buslist;
            return View(expenseviewmodel);
        }

        [HttpPost]
        public ActionResult CreateExpense(Expense ex)
        {
           
            _context.Expense.Add(ex);
            _context.SaveChanges();
            msg = "Expense Added Succesfully";
            return RedirectToAction(nameof(CreateExpense));
        }

        public IActionResult GetExpensebybusId()
        {
            var expenS = new ExpenseSearch();
            var buslist = _context.Buses.ToList();
            ViewBag.buslist = buslist;
            return View(expenS);
        }
    
        public IActionResult ExpenseReport(DateTime fromdate,DateTime todate,int Busid)
        {
            var expenslist = _context.Expense.Where(x => x.Date >= fromdate && x.Date <= todate && x.BusId == Busid).ToList();
            return View(expenslist);
        }


        public IActionResult ExpenseList()
        {
            if (HttpContext.Session.GetString("UserRole") != null)
            {
                var s = _context.Expense.AsNoTracking().ToList();
                var dr = new List<Expense>();
                //int c = 1;
                foreach (var item in s)
                {
                    Expense se = new Expense()
                    {
                        ExpenseId = item.ExpenseId,
                        BusId = item.BusId,
                        Date = item.Date,
                        Amount = item.Amount,
                        Narration = item.Narration,
                        DriverName = item.DriverName
                    };
                    //se.ExpenseId = c;
                    //c++;
                    dr.Add(se);
                }
                return View(dr);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public IActionResult UpdateExpense(int id)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var item = _context.Expense.AsNoTracking().Where(s => s.ExpenseId == id).FirstOrDefault();
                Expense aa = new Expense()
                {
                    ExpenseId = item.ExpenseId,
                    BusId = item.BusId,
                    Date = item.Date,
                    Amount = item.Amount,
                    Narration = item.Narration,
                    DriverName = item.DriverName
                };
                return View(aa);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public IActionResult UpdateExpense(Expense val)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                //var che = _context.Expense.Where(x => x.ExpenseId == val.ExpenseId ).FirstOrDefault();
                Expense di = new Expense()
                {
                    ExpenseId = val.ExpenseId,
                    BusId = val.BusId,
                    Date = val.Date,
                    Amount = val.Amount,
                    Narration = val.Narration,
                    DriverName = val.DriverName

                };
               
                    _context.Expense.Update(di);
                    _context.SaveChanges();
                    msg = "Expense Updated Successfully";
                    return RedirectToAction(nameof(ExpenseList));
               

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        public IActionResult ExpenseDetails(int id)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var details = _context.Expense.AsNoTracking().Where(s => s.ExpenseId == id).FirstOrDefault();

                Expense a = new Expense()
                {
                    ExpenseId = details.ExpenseId,
                    BusId = details.BusId,
                    Date = details.Date,
                    Amount = details.Amount,
                    Narration = details.Narration,
                    DriverName = details.DriverName
                };

                return View(a);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public IActionResult DeleteExpense(int id)
        {
            if (HttpContext.Session.GetString("UserRole") == "1")
            {
                var item = _context.Expense.AsNoTracking().Where(s => s.ExpenseId == id).FirstOrDefault();
                Expense se = new Expense()
                {
                    ExpenseId = item.ExpenseId,
                    BusId = item.BusId,
                    Date = item.Date,
                    Amount = item.Amount,
                    Narration = item.Narration,
                    DriverName = item.DriverName

                };
                return View(se);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult DeleteExpense(Expense se)
        {
            _context.Expense.Remove(_context.Expense.AsNoTracking().Where(s => s.ExpenseId == se.ExpenseId).FirstOrDefault());
            _context.SaveChanges();
            msg = "Deleted Successfully";
            return RedirectToAction("ExpenseList");
        }

    }
}
