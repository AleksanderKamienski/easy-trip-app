using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.Models.Group
{
    public class GroupListingModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string AdminId { get; set; }
        public string CurrentUserid { get; set; }
        public IEnumerable<Data.Models.Forum> Forums { get; set; }
    }
}
