using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Forum.Data.Models
{
    public class ApplicationUserGroup
    {
        [Key]
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string GroupId { get; set; }
    }
}
