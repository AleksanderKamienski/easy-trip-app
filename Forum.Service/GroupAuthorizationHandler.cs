using Forum.Data;
using Forum.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Service
{
    public class GroupAuthorizationHandler : AuthorizationHandler<GroupRequirement, Group>
    {
        private static UserManager<ApplicationUser> _userManager;
        private readonly IGroup _groupService;
        public GroupAuthorizationHandler(IGroup groupService, UserManager<ApplicationUser> userManager)
        {
            _groupService = groupService;
            _userManager = userManager;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            GroupRequirement requirement, Group resource)
        {
            var userId = _userManager.GetUserId(context.User);
            var groupsUser = _groupService.GetGroupsSimpleUser(userId);
            var groupsAdmin = _groupService.GetGroupsAdminUser(userId);

            foreach (var x in groupsUser)
            {
                if (x.Equals(resource))
                    context.Succeed(requirement);
            }

            foreach (var x in groupsAdmin)
            {
                if (x.Equals(resource))
                    context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class GroupRequirement : IAuthorizationRequirement { }

    public static class Operations
    {
        public static OperationAuthorizationRequirement Create =
            new OperationAuthorizationRequirement { Name = nameof(Create) };
        public static OperationAuthorizationRequirement Read =
            new OperationAuthorizationRequirement { Name = nameof(Read) };
        public static OperationAuthorizationRequirement Update =
            new OperationAuthorizationRequirement { Name = nameof(Update) };
        public static OperationAuthorizationRequirement Delete =
            new OperationAuthorizationRequirement { Name = nameof(Delete) };
    }
}
