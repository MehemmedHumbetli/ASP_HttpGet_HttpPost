using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json;

namespace WebApplication2.Controllers
{
    public string path = "User.json";

    public class HomeController : Controller
    {
        public IActionResult ViewResult()
        {
            return View();
        }
    }
}
