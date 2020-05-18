using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mvc_Sample.Models;

namespace Mvc_Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFeatureManager _featureManger;

        public HomeController(ILogger<HomeController> logger, IFeatureManager featureManger)
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
    }
}
