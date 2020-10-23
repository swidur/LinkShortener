using LinkShortenerWeb.DataAccess;
using LinkShortenerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;

namespace LinkShortenerWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShortenerDbContext _context;

        public HomeController(ILogger<HomeController> logger, ShortenerDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("/")]
        [Route("/Home")]
        [Route("/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/{slug}")]
        public IActionResult GetLinkFromSlug(string slug)
        {
            Shortener existing = _context.Shortening.Where(s => s.Slug.Equals(slug)).FirstOrDefault();

            if (existing != null)
            {
                string savedLink = existing.Link;

                if (!savedLink.ToLower().StartsWith("http://"))
                {
                    return new RedirectResult($"http://{savedLink}");
                }
                else
                    return new RedirectResult(savedLink);
            }
            else
            {
                slug = null;
                ViewBag.Response = BadRequest("No such slug");
                return View("Index");
            }
        }


        [HttpPost]
        [Route("/Home/Shorten")]
        public IActionResult CreateShortening(ShortenerModel model)
        {

            if (String.IsNullOrEmpty(model.Link))
            {
                ViewBag.Response = BadRequest("Empty link");
                return View("Index", model);
            }

            if (String.IsNullOrEmpty(model.Slug))
            {
                ViewBag.Response = BadRequest("Empty slug");
                return View("Index", model);
            }
          

            var newShortener = new Shortener
            {
                Link = model.Link,
                Slug = model.Slug
            };

            _context.Shortening.Add(newShortener);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.Response = BadRequest("Already exists");
                return View("Index", model);
            }

            return View("Index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
