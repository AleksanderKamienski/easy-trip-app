using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Forum.Data;
using Forum.Data.Models;
using Forum.Service;
using Forum.Web.Models.ApplicationUser;
using Forum.Web.Models.Forum;
using Forum.Web.Models.Group;
using Forum.Web.Models.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Forum.Web.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroup _groupService;
        private readonly IUsersGroups _usersGroupService;
        private readonly IForum _forumService;
        private readonly IApplicationUser _userService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IConfiguration _configuration;
        private readonly IUpload _uploadService;
        

        private static UserManager<ApplicationUser> _userManager;

        public GroupController(IGroup groupService, IApplicationUser userService,
            UserManager<ApplicationUser> userManager, IAuthorizationService authorizationService,
            IForum forumService, IUsersGroups usersGroupService, IConfiguration configuration,
            IUpload uploadService)
        {
            _groupService = groupService;
            _userService = userService;
            _usersGroupService = usersGroupService;
            _userManager = userManager;
            _authorizationService = authorizationService;
            _forumService = forumService;
            _configuration = configuration;
            _uploadService = uploadService;
        }
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var groupsNormalUser = _groupService.GetGroupsSimpleUser(userId).Select(f => new GroupListingModel
            {
                Id = f.Id,
                Name = f.Name,
                Description = f.Description,
                ImageUrl = f.ImageUrl,
            });

            var groupsAdmin = _groupService.GetGroupsAdminUser(userId).Select(f => new GroupListingModel
            {
                Id = f.Id,
                Name = f.Name,
                Description = f.Description,
                ImageUrl = f.ImageUrl,
            });

            var groupsOther = _groupService.GetGroupsNotAdminNotUser(userId).Select(f => new GroupListingModel
            {
                Id = f.Id,
                Name = f.Name,
                Description = f.Description,
                ImageUrl = f.ImageUrl,
            });

            var groupListingModelsNormalUser = groupsNormalUser as IList<GroupListingModel> ?? groupsNormalUser.ToList();
            var groupListingModelsAdmin = groupsAdmin as IList<GroupListingModel> ?? groupsAdmin.ToList();
            var groupListingModelsOther = groupsOther as IList<GroupListingModel> ?? groupsOther.ToList();

            var model = new GroupIndexModel
            {
                GroupUser = groupListingModelsNormalUser.OrderBy(group => group.Name),
                GroupsAdmin = groupListingModelsAdmin.OrderBy(group => group.Name),
                GroupsOther = groupListingModelsOther.OrderBy(group => group.Name),
                UserId = userId
            };

            return View(model);

        }
        //public IActionResult ChosenGroup(string id)
        //{
        //    var group = _groupService.GetById(id);

        //    var model = new GroupListingModel
        //    {
        //        Id = group.Id,
        //        Name = group.Name,
        //        Description = group.Description,
        //        ImageUrl = group.ImageUrl
        //    };

        //    return View(model);
        //}
        public IActionResult Join(string id)
        {
            var group = _groupService.GetById(id);
            var model = new GroupLeaveModel
            {
                Name = group.Name,
                Id = group.Id

            };

            return View(model);
        }
        public async Task<IActionResult> ConfirmJoin(string groupId)
        {
            var userId = _userManager.GetUserId(User);
            var userGroupRelation = new ApplicationUserGroup { ApplicationUserId = userId, GroupId = groupId };
            await _usersGroupService.Add(userGroupRelation);

            return RedirectToAction("ChosenGroupAsync", "Group", new { id = groupId });
        }

        public IActionResult CancelJoin(string id)
        {
            return RedirectToAction("Index");
        }


        public IActionResult Leave(string id)
        {
            var group = _groupService.GetById(id);
            var model = new GroupLeaveModel
            {
                Name = group.Name,
                Id = group.Id
            };

            return View(model);
        }

        public IActionResult Delete(string id)
        {
            var group = _groupService.GetById(id);
            var model = new DeleteGroupModel
            {
                GroupId = id,
                Name = group.Name
            };

            return View(model);
        }

        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var group = _groupService.GetById(id);
            await _groupService.Delete(group);

            return RedirectToAction("Index");
        }

        public IActionResult Cancel(string Id)
        {
            return RedirectToAction("ChosenGroupAsync", "Group", new { id = Id });
        }

        public IActionResult Create(string Id)
        {
            var model = new AddGroupModel
            {
                AdminId = Id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddGroup(AddGroupModel model)
        {

            var imageUri = "";

            if (model.ImageUpload != null)
            {
                var blockBlob = PostGroupImage(model.ImageUpload);
                imageUri = blockBlob.Uri.AbsoluteUri;
            }

            else
            {
                imageUri = "/images/users/default.png";
            }

            var group = new Group()
            {

                Name = model.Title,
                Description = model.Description,
                ImageUrl = imageUri,
                AdminId = model.AdminId

            };
            if (group.Name != null && group.Description != null)
                await _groupService.Add(group);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ConfirmLeave(string groupId)
        {
            var userId = _userManager.GetUserId(User);
            await _usersGroupService.DeleteUserGroup(userId, groupId);

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> ChosenGroupAsync(string id)
        {
            var group = _groupService.GetById(id);
            var forum = _forumService.GetByGroupId(id);
            var userId = _userManager.GetUserId(User);

            if (group == null)
            {
                return new NotFoundResult();
            }

           var authorizationResult = await _authorizationService
           .AuthorizeAsync(User, group, "EditPolicy");


            if (authorizationResult.Succeeded)
            {
                var model = new GroupListingModel
                {
                    Id = group.Id,
                    Name = group.Name,
                    Description = group.Description,
                    ImageUrl = group.ImageUrl,
                    Forums = forum,
                    AdminId = group.AdminId,
                    CurrentUserid = userId
                };

                return View(model);
            }
            else if (User.Identity.IsAuthenticated)
            {
                return new ForbidResult();
            }
            else
            {
                return new ChallengeResult();
            }
        }
        public CloudBlockBlob PostGroupImage(IFormFile file)
        {
            var connectionString = _configuration.GetConnectionString("AzureStorageAccountConnectionString");
            var container = _uploadService.GetBlobContainer(connectionString);
            var parsedContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
            var filename = Path.Combine(parsedContentDisposition.FileName.ToString().Trim('"'));
            var blockBlob = container.GetBlockBlobReference(filename);
            blockBlob.UploadFromStreamAsync(file.OpenReadStream());

            return blockBlob;
        }
    }
}
