using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using XmlDemoApplication.Models;
using XmlDemoApplication.Services;

namespace XmlDemoApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataService _dataService;

        public HomeController(ILogger<HomeController> logger, IDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetBookData()
        {
            return Content(JsonSerializer.Serialize(_dataService.GetData().Catalog));
        }

        [HttpPost]
        public IActionResult BorrowBook(string bookId, string username)
        {
            (bool success, string msg) operationResult = _dataService.BorrowBook(bookId, username);
            
            return StatusCode(operationResult.success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest, operationResult.msg);
        }

        [HttpPost]
        public IActionResult ReturnBook(string bookId, string username)
        {
            (bool success, string msg) operationResult = _dataService.ReturnBook(bookId, username);

            return StatusCode(operationResult.success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest, operationResult.msg);
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
