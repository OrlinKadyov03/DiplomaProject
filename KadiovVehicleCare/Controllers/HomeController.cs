using KadiovVehicleCare.Interfaces;
using KadiovVehicleCare.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KadiovVehicleCare.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceRepository _serviceRepository;

        public HomeController(ILogger<HomeController> logger,IServiceRepository serviceRepository)
        {
            _logger = logger;
            _serviceRepository = serviceRepository;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _serviceRepository.GetAllAsync();
            return View(services);
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
    }
}
