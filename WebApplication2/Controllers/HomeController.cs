using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Hosting;
using WebApplication2.Entites;
using Newtonsoft.Json;
using System.Collections.Generic;
using WebApplication2.Models;
using System.Linq;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostEnvironment _hostingEnvironment;

        public HomeController(IHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        //Index ve Details eyni isi gorur sadece biri adi ve soyadi view eliyir digeri ise full propertileri ile view eliyir
        #region  
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
        #endregion

        //.Jsondan datani elde edirik (Oxuyuruq)
        #region
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
        #endregion

        [HttpGet]
        public IActionResult Add()
        {
            var vm = new UserAddViewModel();
            return View(vm);
        }

        //View da input-a daxil olunan datani post vasitesi ile elde edirik
        #region
        [HttpPost]
        public IActionResult Add(UserAddViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newUser = new Entites.User
                {
                    Id = viewModel.User.Id,
                    Name = viewModel.User.Name,
                    Surname = viewModel.User.Surname,
                    Age = viewModel.User.Age,
                    Image = viewModel.User.Image
                };

                var users = GetUsersFromJson();
                users.Add(newUser);

                WriteToJson(users);

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
        #endregion

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var users = GetUsersFromJson();

            var userToRemove = users.FirstOrDefault(u => u.Id == id);

            users.Remove(userToRemove);

            WriteToJson(users);
            return RedirectToAction("Index");
        }

        // Datani .json-a yazan method
        #region
        private void WriteToJson(List<User> users)
        {
            var rootPath = _hostingEnvironment.ContentRootPath;
            var filePath = Path.Combine(rootPath, "Helpers", "User.json");

            var json = JsonConvert.SerializeObject(users, Formatting.Indented);

            System.IO.File.WriteAllText(filePath, json);
        }
        #endregion
    }
}
