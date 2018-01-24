using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Test.Core.Data;
using Test.Core.Domain.Student;
using Test.Services;
using Test.Web.Models;

namespace Test.Web.Controllers
{
    public class HomeController : Controller
    {
        private StudentService _ss;

        public HomeController(StudentService ss)
        {
            _ss = ss;
        }

        public IActionResult Index()
        {
            var data = _ss.GetStudents();
            return Json(JsonConvert.SerializeObject(data));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
