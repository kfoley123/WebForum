using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebForum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace WebForum.Data
{
    public class WebForumContext : IdentityDbContext<ApplicationUser>
    {
        public WebForumContext(DbContextOptions<WebForumContext> options)
            : base(options)
        {
        }

        public DbSet<WebForum.Models.Discussion> Discussion { get; set; } = default!;
        public DbSet<WebForum.Models.Comment> Comment { get; set; } = default!;

        public DbSet<WebForum.Data.ApplicationUser> ApplicationUser { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<Discussion>().HasOne(discussion => discussion.ApplicationUser).WithMany(user => user.Discussions).HasForeignKey(discussion => discussion.ApplicationUserId);
        //}
    }
}
