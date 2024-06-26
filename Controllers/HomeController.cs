using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Errors.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ThrowException()
        {
            throw new Exception("This is a test exception");
        }


        [Route("Home/Error")]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if(exceptionHandlerPathFeature?.Error != null)
            {
                _logger.LogError(exceptionHandlerPathFeature.Error, "An error occurred");
            }

            return View();
        }
    }
}
