using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShorten.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinkShorten.Persistence
{
    public class LinkShortenContext : DbContext
  {
        private int _currentIndex = 1;
        public LinkShortenContext(DbContextOptions<LinkShortenContext> optins)
            : base(optins)
        {
        }
        public DbSet<ShortenedCustomLink> Links { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ShortenedCustomLink>(e => {
                e.HasKey(l => l.Id);
            });
        }
    }
}