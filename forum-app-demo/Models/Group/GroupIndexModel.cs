using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.Models.Group
{
    public class GroupIndexModel
    {
        public IEnumerable<GroupListingModel> GroupUser { get; set; }
        public IEnumerable<GroupListingModel> GroupsAdmin { get; set; }
        public IEnumerable<GroupListingModel> GroupsOther { get; set; }
        public string UserId { get; set; }
      
    }
}
