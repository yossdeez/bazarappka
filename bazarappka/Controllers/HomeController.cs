using System.Diagnostics;
using bazarappka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace bazarappka.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BazarContext _context;

        public HomeController(BazarContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Statistics()
        {
            var model = new StatisticsViewModel
            {
                TotalVehicles = await _context.AutaInfos
                    .CountAsync(),

                LowMileage = await _context.AutaInfos
                    .CountAsync(a => a.Mileage < 100000),

                HighMileage = await _context.AutaInfos
                    .CountAsync(a => a.Mileage > 200000),

                GasolineVehicles = await _context.AutaInfos
                    .CountAsync(a => a.Fuel == FuelType.Gasoline),

                DieselVehicles = await _context.AutaInfos
                    .CountAsync(a => a.Fuel == FuelType.Diesel),

                CngLpgVehicles = await _context.AutaInfos
                    .CountAsync(a => a.Fuel == FuelType.CngLpg),

                ElectricVehicles = await _context.AutaInfos
                    .CountAsync(a => a.Fuel == FuelType.Electric),

                PerfectVehicles = await _context.AutaInfos
                    .CountAsync(a => a.Condition == ConditionType.Perfect),

                ModerateVehicles = await _context.AutaInfos
                    .CountAsync(a => a.Condition == ConditionType.Moderate),

                AcceptableVehicles = await _context.AutaInfos
                    .CountAsync(a => a.Condition == ConditionType.Acceptable),
                
                BadVehicles = await _context.AutaInfos
                    .CountAsync(a => a.Condition == ConditionType.Bad),
                
                BranNewVehicles = await _context.AutaInfos
                    .CountAsync(a => a.Condition == ConditionType.Perfect && a.Year >= DateTime.Now.Year - 2 && a.Mileage < 25000),
                
                ClassicCars = await _context.AutaInfos
                    .CountAsync(a => a.Condition == ConditionType.Perfect && a.Year <= DateTime.Now.Year - 30),
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
    }
}
