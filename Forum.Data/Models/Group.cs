using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Forum.Data.Models
{
    public class Group
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string AdminId { get; set; }
        public virtual IEnumerable<ApplicationUserGroup> UserGroups { get; set; }
        public virtual IEnumerable<Forum> ForumId { get; set; }
    }
}
