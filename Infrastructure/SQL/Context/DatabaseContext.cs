using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.SQL.Context
{
    public class DatabaseContext : IdentityDbContext<User, Role, string>
    {
        private readonly IConfiguration _iConfiguration;
        public DatabaseContext(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration iConfiguration) : base(options)
        {
            _iConfiguration = iConfiguration;
        }

        public override DbSet<User> Users { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<PhotoTweet> PhotoTweets { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public DbSet<TweetTags> TweetTags{ get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserTagged> UserTaggeds{ get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(_iConfiguration["connection"]);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
            
        //    modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(x => new { x.LoginProvider, x.ProviderKey });
        //    modelBuilder.Entity<IdentityUserRole<string>>().HasKey(x => new { x.RoleId, x.UserId });
        //    modelBuilder.Entity<IdentityUserToken<string>>().HasKey(x => new { x.LoginProvider, x.Name });

        ////    modelBuilder.Entity<User>().Ignore(x => x.NormalizedEmail);
        //}
    }
}
