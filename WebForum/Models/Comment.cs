

using WebForum.Data;

namespace WebForum.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        
        public string Content { get; set; } = string.Empty;
        
        public DateTime CreateDate { get; set; } = DateTime.Now;

        //foreign key 
        public int DiscussionId {  get; set; }

        // Navigation property
        public Discussion? Discussion { get; set; } //nullable

        // Foreign key (AspNetUsers table)
        public string? ApplicationUserId { get; set; }

        // Navigation property
        public ApplicationUser? ApplicationUser { get; set; } // nullable!!!
    }
}
