using System.ComponentModel.DataAnnotations.Schema;
using WebForum.Data;

namespace WebForum.Models
{
    public class Discussion
    {

        public int DiscussionId { get; set; } 
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public string ImageFileName { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; } = DateTime.Now;

        // Navigation property 
        public List<Comment> Comments { get; set; } = new List<Comment>(); // Initialized to empty list instead of nullable

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        // Foreign key (AspNetUsers table)
        //becasue I already have discussions that I want to keep, I need to make this nullable
        public string? ApplicationUserId { get; set; } = string.Empty;

        // Navigation property
        public ApplicationUser? ApplicationUser { get; set; } // nullable!!!

    }
}
