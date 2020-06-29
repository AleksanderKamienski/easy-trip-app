using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data
{
    public interface IGroup
    {
        Task Create(Group group);
        Task Delete(Group group);
        IEnumerable<Group> GetGroupsSimpleUser(string id);
        IEnumerable<Group> GetGroupsAdminUser(string id);
        Group GetById(string id);
        Task Add(Group forum);
        IEnumerable<Group> GetAll();
        IEnumerable<Group> GetGroupsNotAdminNotUser(string id);

    }
}
