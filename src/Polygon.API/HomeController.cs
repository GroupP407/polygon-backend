using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Polygon.API
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/api/test")]
        public ActionResult TestMethod()
        {
            _logger.LogInformation("Вывод логгера{@_logger}", _logger);
            return Ok();
        }
    }
}