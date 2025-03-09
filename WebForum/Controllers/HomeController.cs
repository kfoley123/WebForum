using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebForum.Data;
using WebForum.Models;
using Microsoft.AspNetCore.Identity;

namespace WebForum.Controllers

{
    public class HomeController : Controller
    {
        private readonly WebForumContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(WebForumContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var discussions = await _context.Discussion
                .OrderByDescending(d => d.CreateDate)
                .Include(d => d.Comments)
                .Include(d => d.ApplicationUser)
                .ToListAsync();

            if (discussions.Count == 0)
                //added this to debug after deleting all discussions 
            {
                ViewBag.Message = "No discussions available.";
            }

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

        public async Task<IActionResult> Profile(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.Users
                .Where(u => u.Id == id)
                .Select(u => new UserProfileViewModel
                {
                    Id = u.Id,
                    Name = u.UserName,
                    Location = u.Location,
                    ProfilePicture = u.ProfileImage,
                    Discussions = _context.Discussion
                        .Where(d => d.ApplicationUserId == id)
                        .OrderByDescending(d => d.CreateDate)
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}