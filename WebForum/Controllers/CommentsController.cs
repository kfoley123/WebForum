using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebForum.Data;
using WebForum.Models;
using Microsoft.AspNetCore.Identity;

namespace WebForum.Controllers
{

    [Authorize]
    public class CommentsController : Controller
    {
        private readonly WebForumContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentsController(WebForumContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Comments/Create

      

        public IActionResult Create(int discussionId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized(); // Ensure user is authenticated
            }

            var comment = new Comment
            {
                DiscussionId = discussionId,
                ApplicationUserId = userId // not 100% sure if this is necessary here 
            };

            return View(comment);
        }



        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Title,Content,DiscussionId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                // Get the current user's ID
                comment.ApplicationUserId = _userManager.GetUserId(User); // Fix: Use ApplicationUserId

                if (comment.ApplicationUserId == null)
                {
                    return Unauthorized(); // Ensure user is authenticated
                }

                // Assign creation date
                comment.CreateDate = DateTime.UtcNow;

                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Discussions", new { id = comment.DiscussionId });
            }

            ViewData["DiscussionId"] = new SelectList(_context.Discussion, "DiscussionId", "DiscussionId", comment.DiscussionId);
            return View(comment);
        }



        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.CommentId == id);
        }
    }
}
