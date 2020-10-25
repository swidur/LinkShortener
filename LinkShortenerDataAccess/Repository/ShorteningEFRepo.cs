using LinkShortenerDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinkShortenerDataAccess.Repository
{
    public class ShorteningEFRepo : IShorteningRepo
    {
        private readonly ShortenerDbContext _context;

        public ShorteningEFRepo(ShortenerDbContext context)
        {
            this._context = context;
        }

        public void CreateShortening(Shortening shortening)
        {
            
            if (String.IsNullOrEmpty(shortening.Slug) ||
                String.IsNullOrEmpty(shortening.Link))
            {
                throw new ArgumentException(nameof(shortening));
            }
            _context.Add(shortening);
            _context.SaveChanges();
        }

        public void DeleteShortening(Shortening shortening)
        {
            if (String.IsNullOrEmpty(shortening.Slug) ||
                String.IsNullOrEmpty(shortening.Link))
            {
                throw new ArgumentException(nameof(shortening));
            }

            //Possible tracking issue, must be tested.
            shortening.IsDeleted = true;
            _context.SaveChanges();
        }

        public IEnumerable<Shortening> GetAllShorteningsByAny(string where, string userId)
        {
            where = where.ToLower();
            List<Shortening> result = _context.Shortening
                .Where(s => s.Link.ToLower().Contains(where) ||
                        s.Slug.ToLower().Contains(where))
                .Where(s => s.User.Equals(userId))
                .ToList();

            return result;
        }

        public Shortening GetShorteningById(int id)
        {
            return _context.Shortening.Find(id);
        }

        public bool SaveChanges(ShortenerDbContext context)
        {
            return (context.SaveChanges() >= 0);
        }

        public void UpdateShortening(Shortening shortening)
        {
            //Not implemented
        }
    }
}
