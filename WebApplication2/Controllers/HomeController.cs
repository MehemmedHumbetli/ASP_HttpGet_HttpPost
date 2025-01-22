using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Hosting;
using WebApplication2.Entites;
using Newtonsoft.Json;
using System.Collections.Generic;
using WebApplication2.Models;

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
            var users = GetUsersFromJson();
            var viewModel = new UserViewModel
            {
                Users = users
            };
            return View(viewModel);
        }

        public IActionResult Details()
        {
            var users = GetUsersFromJson();
            var viewModel = new UserViewModel
            {
                Users = users
            };
            return View(viewModel);
        }

        private List<User> GetUsersFromJson()
        {
            var rootPath = _hostingEnvironment.ContentRootPath;
            var filePath = Path.Combine(rootPath, "Helpers", "User.json");
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<User>>(json);
            }
        }

        [HttpGet]
        public IActionResult Add() 
        {
            var vm = new UserAddViewModel();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(UserAddViewModel viewModel)
        {
            var newUser = new Entites.User
            {
                Name = viewModel.User.Name,
                Surname = viewModel.User.Surname,
                Age = viewModel.User.Age,
                Image = viewModel.User.Image
            };

            var users = GetUsersFromJson();
            users.Add(newUser);

            var rootPath = _hostingEnvironment.ContentRootPath;
            var filePath = Path.Combine(rootPath, "Helpers", "User.json");
            var json = JsonConvert.SerializeObject(users, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);

            return View(viewModel);
        }

    }
}
