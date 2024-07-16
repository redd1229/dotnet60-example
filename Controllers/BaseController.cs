using dotnet60_example.Filters;
using Microsoft.AspNetCore.Mvc;

namespace dotnet60_example.Controllers
{
    [SessionFilter]
    [TypeFilter(typeof(ExceptionLoggingFilterAttribute))]
    public class BaseController : Controller
    {
    }
}
