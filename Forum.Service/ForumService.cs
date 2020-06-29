using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.Data;
using Forum.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Forum.Service
{
    public class ForumService : IForum
    {
        private readonly ApplicationDbContext _context;
        private readonly IPost _postService;
        private readonly IPostReply _postReplyService;

        public ForumService(ApplicationDbContext context, IPost postService, IPostReply postReplyService)
        {
            _context = context;
            _postService = postService;
            _postReplyService = postReplyService;
        }

        public async Task Create(Data.Models.Forum forum)
        {
            _context.Add(forum);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var forum = GetById(id);
            var posts = _postService.GetPostsByForumId(id);
            foreach (var post in posts)
            {
                var postsReplies = _postReplyService.GetPostsRepliesByPostId(post.Id);
                await _postReplyService.DeletePostRepliesList(postsReplies);
            }

            await _postService.DeletePostList(posts);
                
            _context.Remove(forum);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<ApplicationUser> GetActiveUsers(int forumId)
        {
            var posts = GetById(forumId).Posts;

            if(posts == null || !posts.Any())
            {
                return new List<ApplicationUser>();
            }

            return _postService.GetAllUsers(posts);
        }

        public IEnumerable<Data.Models.Forum> GetAll(string groupId)
        {
            return _context.Forums.Where(f => f.GroupId == groupId)
                .Include(forum=>forum.Posts);
        }

        public Data.Models.Forum GetById(int id)
        {
            var forum = _context.Forums
                .Where(f => f.Id == id)
                .Include(f => f.Posts)
                .ThenInclude(f => f.User)
                .Include(f => f.Posts)
                .ThenInclude(f => f.Replies)
                .ThenInclude(f => f.User)
                .Include(f => f.Posts)
                .ThenInclude(p => p.Forum)
                .FirstOrDefault();
            if (!(forum is null))
            {
                if (forum.Posts == null)
                {
                    forum.Posts = new List<Post>();
                }
            }


            return forum;
        }

        public IEnumerable<Data.Models.Forum> GetByGroupId(string id)
        {
            var forum = _context.Forums
                .Where(f => f.GroupId == id)
                .Include(f => f.Posts)
                .ThenInclude(f => f.User)
                .Include(f => f.Posts)
                .ThenInclude(f => f.Replies)
                .ThenInclude(f => f.User)
                .Include(f => f.Posts)
                .ThenInclude(p => p.Forum);

            foreach (var x in forum)
                if (x.Posts == null)
                {
                    x.Posts = new List<Post>();
                }

            return forum;
        }

        public Post GetLatestPost(int forumId)
        {
            var posts = GetById(forumId).Posts;

            if(posts != null)
            {
                return GetById(forumId).Posts
                    .OrderByDescending(post => post.Created)
                    .FirstOrDefault();
            }

            return new Post();
        }

        public bool HasRecentPost(int id)
        {
            const int hoursAgo = 12;
            var window = DateTime.Now.AddHours(-hoursAgo);
            return GetById(id).Posts.Any(post => post.Created >= window);
        }

        public async Task Add(Data.Models.Forum forum)
        {
            _context.Add(forum);
            await _context.SaveChangesAsync();
        }

        public async Task SetForumImage(int id, Uri uri)
        {
            var forum = GetById(id);
            forum.ImageUrl = uri.AbsoluteUri;
            _context.Update(forum);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Post> GetFilteredPosts(string searchQuery)
        {
            return _postService.GetFilteredPosts(searchQuery);
        }

        public IEnumerable<Post> GetFilteredPosts(int forumId, string searchQuery)
        {
            if (forumId == 0) return _postService.GetFilteredPosts(searchQuery);

            var forum = GetById(forumId);

            return string.IsNullOrEmpty(searchQuery)
                ? forum.Posts
                : forum.Posts.Where(post
                    => post.Title.Contains(searchQuery) || post.Content.Contains(searchQuery));
        }

        public async Task UpdateForumDescription(int id, string description)
        {
            var forum = GetById(id);
            forum.Description = description;

            _context.Update(forum);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateForumTitle(int id, string title)
        {
            var forum = GetById(id);
            forum.Title = title;

            _context.Update(forum);
            await _context.SaveChangesAsync();
        }
    }
}
