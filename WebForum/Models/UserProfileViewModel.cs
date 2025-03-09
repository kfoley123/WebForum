using WebForum.Models;


namespace WebForum.Models
{
    public class UserProfileViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public List<Discussion> Discussions { get; set; } = new List<Discussion>();
    }
}
