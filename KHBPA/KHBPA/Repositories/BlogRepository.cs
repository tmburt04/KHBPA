using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KHBPA.Models;

namespace KHBPA.Repositories
{
    public class BlogRepository
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        public IList<Post> Posts(int pageNo, int pageSize)
        {
            var posts = _db.Post
                .Where(p => p.Published)
                .OrderByDescending(p => p.PostedOn)
                .Skip(pageNo * pageSize)
                .Take(pageSize)
                //.Fetch(p => p.Category)
                .ToList();

            var postIds = posts.Select(p => p.ID).ToList();

            return _db.Post
                .Where(p => postIds.Contains(p.ID))
                .OrderByDescending(p => p.PostedOn)
                //.FetchMany(p => p.Tags)
                .ToList();

        }

        public int TotalPosts()
        {
            return _db.Post.Where(p => p.Published != false).Count();
        }

        public IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize)
        {

            var posts = _db.Post
                .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                .OrderByDescending(p => p.PostedOn)
                .Skip(pageNo * pageSize)
                .Take(pageSize)
                //.Fetch(p => p.Category)
                .ToList();
            var postIds = posts.Select(p => p.ID).ToList();

            return _db.Post
                .Where(p => postIds.Contains(p.ID))
                .OrderByDescending(p => p.PostedOn)
                //.FetchMany(p => p.Tags)
                .ToList();
        }

        public int TotalPostsForCategory(string categorySlug)
        {
            return _db.Post
                .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                .Count();
        }

        public Category Category(string categorySlug)
        {
            return _db.Category
                .FirstOrDefault(t => t.UrlSlug.Equals(categorySlug));
        }

        public IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize)
        {
            var posts = _db.Post
                .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                .OrderByDescending(p => p.PostedOn)
                .Skip(pageNo * pageSize)
                .Take(pageSize)
                //.Fetch(p => p.Category)
                .ToList();

            var postIds = posts.Select(p => p.ID).ToList();

            return _db.Post
                .Where(p => postIds.Contains(p.ID))
                .OrderByDescending(p => p.PostedOn)
                //.FetchMany(p => p.Tags)
                .ToList();
        }

        public int TotalPostsForTag(string tagSlug)
        {
            return _db.Post
               .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
               .Count();
        }

        public Tag Tag(string tagSlug)
        {
            return _db.Tag
                .FirstOrDefault(t => t.UrlSlug.Equals(tagSlug));
        }


    }

  

    
}
