using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LinkShortenerWeb.Models;
using LinkShortenerWeb.DataAccess;

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

        [Route("/{slug}")]
        public IActionResult GetLinkFromSlug(string slug)
        {
            Shortener existing = _context.Shortening.Where(s => s.Slug.Equals(slug)).FirstOrDefault();
            string savedLink = existing.Link;

            if (existing != null)
            {
                if (!savedLink.ToLower().StartsWith("http://"))
                {
                    return new RedirectResult($"http://{savedLink}");
                }
                else
                    return new RedirectResult(savedLink);
            }
            else
            {
                return BadRequest("No such slug");
            }
        }

        [HttpPost]
        [Route("/Home/CreateShortening")]
        public IActionResult CreateShortening(ShortenerModel model)
        {

            if (model.Slug == null)
            {
                return BadRequest("Empty slug");
            }

            _context.Shortening.Add(new Shortener
            {
                Link = model.Link,
                Slug = model.Slug
            });
            _context.SaveChanges();

            return Ok($"Created https://basictestapp-01.herokuapp.com/{model.Slug}");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
