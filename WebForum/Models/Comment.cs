

using WebForum.Data;

namespace WebForum.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        
        public string Content { get; set; } = string.Empty;
        
        public DateTime CreateDate { get; set; } = DateTime.Now;

        //foreign key 
        //test
        public int DiscussionId {  get; set; }

        // Navigation property
        public Discussion? Discussion { get; set; } //nullable

        // Foreign key (AspNetUsers table)
        //because I already have comments that I want to keep, I need to make this nullable
        public string? ApplicationUserId { get; set; }

        // Navigation property
        public ApplicationUser? ApplicationUser { get; set; } // nullable!!!
    }
}
