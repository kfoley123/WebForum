﻿using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using WebForum.Models;

namespace WebForum.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData] // property is included in download of personal data.
        public string Name { get; set; } = string.Empty;

        [PersonalData]
        public string Bio { get; set; } = string.Empty;

        [PersonalData]
        public string Location { get; set; } = string.Empty;

        [PersonalData]
        public bool IsForHire { get; set; } = false;

        [PersonalData]
        public string ProfileImage { get; set; } = string.Empty;

        public List<Discussion> Discussions { get; set; } = new List<Discussion>();
    }
}
