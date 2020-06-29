using Forum.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Data
{
    public interface IPostReply
    {
        PostReply GetById(int id);
        Task Edit(int id, string message);
        Task Delete(int id);
        IEnumerable<PostReply> GetPostsRepliesByPostId(int id);
        Task DeletePostRepliesList(IEnumerable<PostReply> postReplies);
        
    }
}
