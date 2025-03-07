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
    public class WebForumContext : IdentityDbContext
    {
        public WebForumContext(DbContextOptions<WebForumContext> options)
            : base(options)
        {
        }

        public DbSet<WebForum.Models.Discussion> Discussion { get; set; } = default!;
        public DbSet<WebForum.Models.Comment> Comment { get; set; } = default!;
    }
}
