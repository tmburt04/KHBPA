using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KHBPA.Repositories;

namespace KHBPA.Models.ViewModels
{
    public class ListViewModel
    {
        public ListViewModel(BlogRepository _blogRepository, int p)
        {
            Posts = _blogRepository.Posts(p - 1, 10);
            TotalPosts = _blogRepository.TotalPosts();
        }

        public ListViewModel(BlogRepository _blogRepository, 
            string text, string type, int p)
        {
            switch (type)
            {
                case "Tag":
                    Posts = _blogRepository.PostsForTag(text, p - 1, 10);
                    TotalPosts = _blogRepository.TotalPostsForTag(text);
                    Tag = _blogRepository.Tag(text);
                    break;
                default:
                    Posts = _blogRepository.PostsForCategory(text, p - 1, 10);
                    TotalPosts = _blogRepository.TotalPostsForCategory(text);
                    Category = _blogRepository.Category(text);
                    break;

            }
            
        }

        public IList<Post> Posts { get; set; }
        public int TotalPosts { get; set; }
        public Category Category { get; set; }
        public Tag Tag { get; set; }
    }
}