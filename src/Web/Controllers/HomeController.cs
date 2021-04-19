using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISearchService _searchService;
        private readonly IIndexingService _indexingService; 

        public HomeController(ILogger<HomeController> logger, ISearchService searchService, IIndexingService indexingService)
        {
            _logger = logger;
            _searchService = searchService;
            _indexingService = indexingService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult Add() {
            return View();
        }

        [HttpGet]
        public IActionResult Search([FromQuery]string query, [FromQuery] int pageSize = 30, [FromQuery] int page = 1) {
            var model = _searchService.Search(query, page, pageSize);
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddPageViewModel model) {
            if (ModelState.IsValid)
            {
                _indexingService.IndexPage(model.Url);
                RedirectToAction("Index");
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
