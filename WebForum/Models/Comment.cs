using System.Drawing;

namespace WebForum.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        
        public DateTime CreateDate { get; set; } = DateTime.Now;

        //foreign key
        public int DiscussionId {  get; set; }

        // Navigation property - "many" side of the relationship
        public Discussion? Discussion { get; set; } //nullable
    }
}
