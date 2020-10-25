using LinkShortenerDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkShortenerDataAccess.Repository
{
    public interface IShorteningRepo
    {
        public IEnumerable<Shortening> GetAllShorteningsByAny(string where, string userId);
        public Shortening GetShorteningById(int id);
        public Shortening GetShorteningBySlug(string slug);
        public void CreateShortening(Shortening shortening);
        public void UpdateShortening(Shortening shortening);
        public void DeleteShortening(Shortening shortening);
        bool SaveChanges(ShortenerDbContext context);
    }
}
