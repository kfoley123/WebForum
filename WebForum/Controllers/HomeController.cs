using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebForum.Data;
using WebForum.Models;
namespace WebForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebForumContext _context;

        public HomeController(WebForumContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var discussions = await _context.Discussion
                .OrderByDescending(d => d.CreateDate)
                .Include(d => d.Comments)
                .ToListAsync();

            return View(discussions);
        }

        public async Task<IActionResult> GetDiscussion(int id)
        {
            var discussion = await _context.Discussion
                .Include(d => d.Comments)
                .FirstOrDefaultAsync(d => d.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}