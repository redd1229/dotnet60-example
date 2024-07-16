using dotnet60_example.Service.Interface;
using dotnet60_example.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace dotnet60_example.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService;

        public HomeController(ILogger<HomeController> logger,
                              IHomeService homeService)
        {
            _logger = logger;
            _homeService = homeService;
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
            return View(new ErrorVM { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult GetRectangle()
        {
            try
            {

                var data = _homeService.GetRectangle();
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(new ResultVM
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = $"Error: {ex.Message}"
                });
            }
        }
    }
}