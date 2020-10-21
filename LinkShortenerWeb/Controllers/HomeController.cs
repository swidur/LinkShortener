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
            Shortening existing = _context.Shortening.Where(s => s.Slug.Equals(slug)).FirstOrDefault();
            if (existing != null)
            {
                return Redirect(existing.Link);
            }
            else
            {
                return BadRequest("No such slug");
            }
        }

        public IActionResult CreateShortening(string link, string? slug)
        {
            if (slug == null)
            {
                return BadRequest("Empty slug");
            }

            _context.Shortening.Add(new Shortening
            {
                Link = link,
                Slug = slug
            });
            _context.SaveChanges();

            return Ok($"Created {slug} for link: {link}");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
