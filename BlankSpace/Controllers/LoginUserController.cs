using BlankSpace.Database;
using BlankSpace.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlankSpace.Controllers
{
    public class LoginUserController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<Student> _userStudent;

        //public IActionResult LoginUserProfile()
        //{

        //    HttpContext.Session.ge("UserName", teacher.Email);


        //}


       
    }
}
