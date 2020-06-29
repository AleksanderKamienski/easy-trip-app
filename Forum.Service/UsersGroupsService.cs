using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Forum.Data.Models;
using Forum.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Forum.Service
{
    public class UsersGroupsService : IUsersGroups
    {
        private readonly ApplicationDbContext _context;

        public UsersGroupsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(ApplicationUserGroup userGroupRelation)
        {
            _context.Add(userGroupRelation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserGroup(string _userId, string _groupId)
        {
            var _userGroup = GetByIdGroupUser(_groupId, _userId);
            _context.Remove(_userGroup);
            await _context.SaveChangesAsync();
        }
        public ApplicationUserGroup GetByIdGroupUser(string groupId, string userId)
        {
            return _context.ApplicationUserGroup.Where(x => x.GroupId == groupId).Where(x => x.ApplicationUserId == userId).SingleOrDefault();
        }
        public IEnumerable<string> GetUsersByGroupId(string groupId)
        {
            return  _context.ApplicationUserGroup.Where(x => x.GroupId == groupId).Select(x => x.ApplicationUserId);
        }
    }
}
