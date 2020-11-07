using Microsoft.EntityFrameworkCore;
using OnlineAuction.Data.DbEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Data.Context
{
    public class OnlineAuctionContext : DbContext
    {
        public OnlineAuctionContext(DbContextOptions<OnlineAuctionContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Artists> Artists { get; set; }
        public DbSet<EffortPictures> EffortPictures { get; set; }
        public DbSet<Efforts> Efforts { get; set; }
        public DbSet<Exhibitions> Exhibitions { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<SiteSettings> SiteSettings { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<FormSettings> FormSettings { get; set; }
        public DbSet<SocialMediaSettings> SocialMediaSettings { get; set; }
    }
}
