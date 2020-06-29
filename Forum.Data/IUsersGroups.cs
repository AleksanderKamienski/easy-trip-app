using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data
{
    public interface IUsersGroups
    {
        Task DeleteUserGroup(string _userId, string _groupId);
        ApplicationUserGroup GetByIdGroupUser(string groupId, string userId);
        IEnumerable<string> GetUsersByGroupId(string groupId);
        Task Add(ApplicationUserGroup userGroupRelation);
    }
}
