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
    public class GroupService : IGroup
    {
        private readonly ApplicationDbContext _context;
        private readonly IForum _forumService;
        private readonly IUsersGroups _usersGroupsService;

        public GroupService(ApplicationDbContext context, IForum forumService,
             IUsersGroups usersGroupsService)
        {
            _context = context;
            _forumService = forumService;
            _usersGroupsService = usersGroupsService;
        }
        public async Task Create(Group group)
        {
            _context.Add(group);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Group group)
        {
            var forums = _forumService.GetByGroupId(group.Id).ToList();

            foreach (var forum in forums)
            {
                await _forumService.Delete(forum.Id); 
            }
            var usersId = _usersGroupsService.GetUsersByGroupId(group.Id).ToList();

            foreach (var userId in usersId)
            {
                await _usersGroupsService.DeleteUserGroup(userId, group.Id);
            }

            _context.Remove(group);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Group> GetGroupsSimpleUser(string id)
        {
            return _context.Groups.Where(x => x.UserGroups.Any(r => r.ApplicationUserId.Equals(id)));
        }
        public IEnumerable<Group> GetGroupsAdminUser(string id)
        {
            return _context.Groups.Where(x => x.AdminId == id);
        }
        public IEnumerable<Group> GetGroupsNotAdminNotUser(string id)
        {
            return _context.Groups.Where(x => !x.UserGroups.Any(r => r.ApplicationUserId.Equals(id))).Where(x => x.AdminId != id);
        }
        public IEnumerable<Group> GetAll()
        {
            return _context.Groups;
        }

        public Group GetById(string id)
        {
            return _context.Groups.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task Add(Data.Models.Group group)
        {
            _context.Add(group);
            await _context.SaveChangesAsync();
        }
    }
}
