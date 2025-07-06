using FlagX0.Models;
using FlagX0.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace FlagX0.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class FlagsController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IAddFlagUseCase _addFlagUseCase;

        public FlagsController(
            ILogger<HomeController> logger, IAddFlagUseCase addFlagUseCase)
        {
            _logger = logger;
            _addFlagUseCase = addFlagUseCase;
        }

        [HttpGet("example/{text}")]
        public IActionResult Example(string text)
        {
            return View("index", new IndexViewModel()
            {
                Content = text
            });
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View(new FlagViewModel());
        }

        [HttpPost("create")]
        public async Task<IActionResult>
        AddFlagToDatabase(FlagViewModel request)
        {
            var userId =
            User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isCreated = await
            _addFlagUseCase.Execute(request.Name, request.IsEnabled/*, userId*/);
            return RedirectToAction("Index");
        }

        public bool IsSubsequence(string s, string t)
        {
            StringBuilder result = new StringBuilder();
            int index2 = 0;

            for (int i = 0; i < s.Length; i++)
            {
                for (int j = index2; j < t.Length; j++)
                {
                    if(s[i] == t[j])
                    {
                        result.Append(t[j]);
                        index2 = j + 1;
                        break;
                    }
                }
            }

            return result.ToString() == s;
        }
    }
}
