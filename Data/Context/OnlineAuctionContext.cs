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

        #region Users
        public DbSet<Users> Users { get; set; }
        #endregion

        #region Artists
        public DbSet<Artists> Artists { get; set; }

        #endregion

        #region Efforts

        public DbSet<EffortPictures> EffortPictures { get; set; }
        public DbSet<Efforts> Efforts { get; set; }

        #endregion

        #region Exhibitions
        public DbSet<Exhibitions> Exhibitions { get; set; }
        #endregion

        #region News
        public DbSet<News> News { get; set; }

        #endregion

        #region Settings

        public DbSet<Languages> Languages { get; set; }
        public DbSet<SiteSettings> SiteSettings { get; set; }
        public DbSet<FormSettings> FormSettings { get; set; }
        public DbSet<SocialMediaSettings> SocialMediaSettings { get; set; }
        public DbSet<SocialMediaTypes> SocialMediaTypes { get; set; }

        #endregion

        #region Pages
        public DbSet<Pages> Pages { get; set; }
        public DbSet<PageBanner> PageBanners { get; set; }
        public DbSet<PageSpecifications> PageSpecifications { get; set; }

        #endregion
    }
}
