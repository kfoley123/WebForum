using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebForum.Models;

namespace WebForum.Data
{
    public class WebForumContext : DbContext
    {
        public WebForumContext (DbContextOptions<WebForumContext> options)
            : base(options)
        {
        }

        public DbSet<WebForum.Models.Discussion> Discussion { get; set; } = default!;
        public DbSet<WebForum.Models.Comment> Comment { get; set; } = default!;
    }
}
