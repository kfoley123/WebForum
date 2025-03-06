using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebForum.Data;
using WebForum.Models;

namespace WebForum.Controllers
{
    public class CommentsController : Controller
    {
        private readonly WebForumContext _context;

        public CommentsController(WebForumContext context)
        {
            _context = context;
        }

        // GET: Comments/Create
   

        public IActionResult Create(int discussionId)
        {
            var comment = new Comment { DiscussionId = discussionId };
            return View(comment);
        }


        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Title,Content,CreateDate,DiscussionId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
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
