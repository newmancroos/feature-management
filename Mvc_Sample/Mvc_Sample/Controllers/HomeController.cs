using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;
using Mvc_Sample.Models;

namespace Mvc_Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFeatureManager _featureManger;

        //IFeatureManagerSnapshot maintain same Feature flage status (IReadOnlyCollection/off)
        //for the request life time
        //public HomeController(ILogger<HomeController> logger, IFeatureManager featureManger)
        public HomeController(ILogger<HomeController> logger, IFeatureManagerSnapshot featureManger)
        {
            _logger = logger;
            _featureManger = featureManger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> GetEmployee()
        {
            List<Employee> employees = null;
            if (await _featureManger.IsEnabledAsync(nameof(FeatureFlage.ListEmployees)))
            { 
                employees = new List<Employee>
                {
                    new Employee { Id=1, FirstName = "Newman", LastName="Croos"},
                    new Employee { Id=2, FirstName = "Nithin", LastName="Croos"}
                };
            }
            return View(employees);
        }

        public async Task<IActionResult> GetUsers()
        {
            List<User> users = null;
            if (await _featureManger.IsEnabledAsync(nameof(FeatureFlage.ListUsers)))
            {
                users = new List<User>
                {
                    new User { Id=1, FirstName = "Newman", LastName="Croos", UserName = "Newmancroos"},
                    new User { Id=2, FirstName = "Nithin", LastName="Croos", UserName = "NithinCroos"}
                };
            }
            return View(users);
        }
        public async Task<IActionResult> GetCountries()
        {
            List<Country> countries = null;
            if (await _featureManger.IsEnabledAsync(nameof(FeatureFlage.ListCountries)))
            {
                countries = new List<Country>
                {
                    new Country { Id=1, Name = "United State of America" },
                    new Country { Id=2, Name = "India" },
                    new Country { Id=3, Name = "France" },
                    new Country { Id=4, Name = "Italy" }
                };
            }
            return View(countries);
        }

        [FeatureGate(FeatureFlage.ListAzureEmp)]
        public IActionResult GetEmployeeById(int Id)
        {
            List<Employee> employees = null;
                employees = new List<Employee>
                {
                    new Employee { Id=1, FirstName = "Newman", LastName="Croos"},
                    new Employee { Id=2, FirstName = "Nithin", LastName="Croos"}
                };
            return Ok(employees.FirstOrDefault(x => x.Id == Id));
        }
    }
}
