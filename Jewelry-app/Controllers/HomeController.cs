using Jewelry_app.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.Entity;

namespace Jewelry_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? id)
        {
            Group group = DataLayer.Instance.GroupsAllIncludes.ToList().Find(g=>g.ID == id);
            if(id == null)
            {
                group = DataLayer.Instance.GroupsAllIncludes.ToList().FirstOrDefault();
                return View(group);
            }
            return View(group);
        }
        public IActionResult Details(int? id)
        {
            Item item = DataLayer.Instance.Items.Include(i=>i.Prices).Include(i=>i.Images).ToList().Find(i=>i.ID == id);
            if (id == null)
            {
                return View("Index");
            }
            return View(item);
        }
        public IActionResult test()
        {
            return View(new Price());
        }
        [HttpPost]
        public IActionResult test(Price price)
        {
            return View(price);
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