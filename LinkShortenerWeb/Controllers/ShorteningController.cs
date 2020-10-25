using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortenerDataAccess;
using LinkShortenerDataAccess.Models;
using LinkShortenerWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortenerWeb.Controllers
{
    public class ShorteningController : Controller
    {
        private readonly ShortenerDbContext _context;

        public ShorteningController(ShortenerDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View("Home/Index");
        }

        [HttpGet]
        [Route("/{slug}")]
        public IActionResult GetLinkFromSlug(string slug)
        {
            Shortening existing = _context.Shortening.Where(s => s.Slug.Equals(slug)).FirstOrDefault();

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
                return View("../Home/Index");
            }
        }

        [HttpPost]
        [Route("/Home/Shorten")]
        public IActionResult CreateShortening(ShortenerModel model)
        {

            if (String.IsNullOrEmpty(model.Link))
            {
                ViewBag.Response = BadRequest("Empty link");
                return View("../Home/Index", model);
            }

            if (String.IsNullOrEmpty(model.Slug))
            {
                ViewBag.Response = BadRequest("Empty slug");
                return View("../Home/Index", model);
            }


            var newShortener = new Shortening
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
                ViewBag.Response = BadRequest($"Already exists {ex.InnerException.Message}");
                return View("../Home/Index", model);
            }

            return View("../Home/Index", model);
        }
    }
}
