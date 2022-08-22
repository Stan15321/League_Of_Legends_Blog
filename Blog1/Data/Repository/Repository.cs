using Blog1.Models;
using Blog1.Models.Comments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog1.Data.Repository
{
    public class Repository : IRepository
    {
        private AppDbContext _ctx;
        

        public Repository(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public void AddPost(Post post)
        {
            _ctx.Posts.Add(post);
            
        }

        public List<Post> GetAllPosts()
        {
            return _ctx.Posts.Where(x => true).ToList();
        }
        public List<Post> GetAllPosts(string Category)
        {
            return _ctx.Posts
                .Where(post => post.Category.ToLower().Equals(Category.ToLower()))
                .ToList();
        }

        public Post GetPost(int id)
        {
            return _ctx.Posts
                .Include(p=> p.MainComments)
                .ThenInclude(mc=>mc.SubComments)
                .FirstOrDefault(p => p.Id == id);
        }

        public void RemovePost(int id)
        {
            _ctx.Posts.Remove(GetPost(id));
        }    
       
        public void UpdatePost(Post post)
        {
            _ctx.Posts.Update(post);
        }
        public async Task<bool> SaveChangesAsync()
        {
            if (await _ctx.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public void AddSubComment(SubComment comment)
        {
            _ctx.SubComments.Add(comment);
        }
    }
}
