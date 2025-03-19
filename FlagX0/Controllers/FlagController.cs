using FlagX0.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlagX0.Controllers
{
    [Route("[controller]")]
    public class FlagController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public FlagController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("example/{text}")]
        public IActionResult Example(string text)
        {
            return View("index", new IndexViewModel()
            {
                Content = text
            });
        }
    }
}
