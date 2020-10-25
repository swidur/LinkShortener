using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortenerDataAccess;
using LinkShortenerDataAccess.Models;
using LinkShortenerDataAccess.Repository;
using LinkShortenerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace LinkShortenerWeb.Controllers
{
    public class ShorteningController : Controller
    {
        private readonly ShortenerDbContext _context;
        private readonly IShorteningRepo _repo;

        public ShorteningController(IShorteningRepo repo)
        {
            this._repo = repo;
        }
        public IActionResult Index()
        {
            return View("../Home/Index");
        }

        [HttpGet]
        [Route("/{slug}")]
        public IActionResult GetLinkFromSlug(string slug)
        {
            Shortening existing = _repo.GetShorteningBySlug(slug);

            if (existing != null)
            {
                string savedLink = existing.Link;

                if (!savedLink.ToLower().StartsWith("http://") &
                    !savedLink.ToLower().StartsWith("https://"))
                {
                    return new RedirectResult($"http://{savedLink}");
                }
                else
                    return new RedirectResult(savedLink);
            }
            else
            {
                ViewBag.Response = BadRequest("No such slug");
                return View("../Home/Index");
            }
        }

        [HttpPost]
        [Route("/Home/Shorten")]
        public IActionResult CreateShortening(ShortenerModel model)
        {
            var newShortening = new Shortening
            {
                Link = model.Link,
                Slug = model.Slug
            };

            try
            {
                _repo.CreateShortening(newShortening);
            }
            catch (ArgumentException ae)
            {
                ViewBag.Response = BadRequest(ae.Message);
            }
            catch (DbUpdateException)
            {
                ViewBag.Response = Conflict("Slug already exists");
            }

            return View("../Home/Index", model);
        }
    }
}
