using Microsoft.AspNetCore.Mvc;

namespace dotnet60_example.Controllers
{
    public class ErrorController : BaseController
    {
        [HttpGet]
        public IActionResult InternalServerError()
        {
            return View();
        }

        [HttpGet]
        public new IActionResult NotFound()
        {
            return View();
        }

        [HttpGet]
        public new IActionResult Unauthorized()
        {
            return View();
        }
    }
}
