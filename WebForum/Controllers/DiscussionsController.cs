using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebForum.Data;
using WebForum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Azure.Identity;

namespace WebForum.Controllers
{
    [Authorize]
    public class DiscussionsController : Controller
    {
        private readonly WebForumContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DiscussionsController(WebForumContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var discussions = await _context.Discussion
                                             .Where(d => d.ApplicationUserId == userId)
                                             .Include(d => d.ApplicationUser)
                                             .Include(d => d.Comments)
                                             .OrderByDescending(d => d.CreateDate) 
                                             .ToListAsync();
            return View(discussions);
        }

        private bool DiscussionExists(int id)
        {
            return _context.Discussion.Any(e => e.DiscussionId == id);
        }

        // GET: Discussions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussion
                .Include(d => d.Comments)
                .Include(d => d.ApplicationUser)
                .FirstOrDefaultAsync(m => m.DiscussionId == id);
            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        // GET: Discussions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Discussions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiscussionId,Title,Content,ImageFile")] Discussion discussion)
        {
            if (ModelState.IsValid)
            {
                // Get the logged-in user's ID.
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                {
                    return Unauthorized();
                }
                // Associate the discussion with the logged-in user.
                discussion.ApplicationUserId = userId;
                discussion.CreateDate = DateTime.UtcNow;

                // Handle image file upload if one was provided.
                if (discussion.ImageFile != null && discussion.ImageFile.Length > 0)
                {
                    var fileName = Path.GetRandomFileName() + Path.GetExtension(discussion.ImageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                    // Create the directory if it doesn't exist.
                    var directoryPath = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await discussion.ImageFile.CopyToAsync(stream);
                    }

                    discussion.ImageFileName = fileName;
                }

                _context.Add(discussion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discussion);
        }



        // GET: Discussions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussion.FindAsync(id);
            if (discussion == null)
            {
                return NotFound();
            }

            // Ensure that the current user is the owner of the discussion.
            var currentUserId = _userManager.GetUserId(User);
            if (discussion.ApplicationUserId != currentUserId)
            {
                return Forbid(); // Alternatively, redirect to an "Access Denied" page.
            }

            return View(discussion);
        }

        // POST: Discussions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiscussionId,Title,Content,ImageFileName,CreateDate")] Discussion discussion)
        {
            if (id != discussion.DiscussionId)
            {
                return NotFound();
            }

            // Retrieve the existing discussion from the database.
            var existingDiscussion = await _context.Discussion.FindAsync(id);
            if (existingDiscussion == null)
            {
                return NotFound();
            }

            // Verify that the logged-in user is the owner.
            var currentUserId = _userManager.GetUserId(User);
            if (existingDiscussion.ApplicationUserId != currentUserId)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update only the allowed fields
                    existingDiscussion.Title = discussion.Title;
                    existingDiscussion.Content = discussion.Content;
                    existingDiscussion.ImageFileName = discussion.ImageFileName; 

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscussionExists(discussion.DiscussionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(discussion);
        }



        // GET: Discussions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussion
                .FirstOrDefaultAsync(m => m.DiscussionId == id);
            if (discussion == null)
            {
                return NotFound();
            }

            // Check if the user is the owner
            var currentUserId = _userManager.GetUserId(User);
            if (discussion.ApplicationUserId != currentUserId)
            {
                return Forbid();
            }

            return View(discussion);
        }

        // POST: Discussions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
       public async Task<IActionResult> DeleteConfirmed(int id)
{
    var discussion = await _context.Discussion.FindAsync(id);
    if (discussion == null)
    {
        return NotFound();
    }

    // Check owner before deletion
    var currentUserId = _userManager.GetUserId(User);
    if (discussion.ApplicationUserId != currentUserId)
    {
        return Forbid();
    }

    _context.Discussion.Remove(discussion);
    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
}
    }
}
