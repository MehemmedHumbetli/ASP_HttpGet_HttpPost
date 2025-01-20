using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Hosting;
using WebApplication2.Entites;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostEnvironment _hostingEnvironment;

        public HomeController(IHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var rootPath = _hostingEnvironment.ContentRootPath;
            var filePath = Path.Combine(rootPath, "Helpers", "User.json");
            List<User> users = null;

            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                users = JsonConvert.DeserializeObject<List<User>>(json);
            }

            return View(users);
        }
    }
}
