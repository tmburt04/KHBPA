using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using KHBPA.Models;
using System.ServiceModel.Syndication;

namespace KHBPA.Controllers
{
    public class PostsController : Controller
    {
        private BlogModel model = new BlogModel();
        private const int PostsPerPage = 4;
        private const int PostsPerFeed = 25;

        // GET: Posts
        public ActionResult Index(int? id)
        {
            int pageNumber = id ?? 0;
            IEnumerable<Post> posts =
                (from post in model.Posts
                where post.DateTime < DateTime.Now
                orderby post.DateTime descending
                select post).Skip(pageNumber * PostsPerPage).Take(PostsPerPage + 1);
            ViewBag.IsPreviousLinkVisible = pageNumber > 0;
            ViewBag.IsNextLinkVisible = posts.Count() > PostsPerPage;
            ViewBag.PageNumber = pageNumber;
            return View(posts.Take(PostsPerPage));
        }
        //Make an RSS feed of our blog
        public ActionResult RSS()
        {
            IEnumerable<SyndicationItem> posts =
                (from post in model.Posts
                 where post.DateTime < DateTime.Now
                 orderby post.DateTime descending
                 select post).Take(PostsPerFeed).ToList().Select(x => GetSyndicationItem(x));
                
            SyndicationFeed feed = new SyndicationFeed("KYHBPA", "KYHBPA Blog", new Uri("https://kyhbpa.org"), posts);
            Rss20FeedFormatter formattedFeed = new Rss20FeedFormatter(feed);
            return new FeedResultController(formattedFeed);
        }

        private SyndicationItem GetSyndicationItem(Post post)
        {
            return new SyndicationItem(post.Title, post.Body, new Uri("https://khbpa.org/posts/details" + post.ID));
        }

        public ActionResult Details(int id)
        {
            Post post = GetPost(id);
            return View(post);
        }

        [ValidateInput(false)]
        public ActionResult Comment(int id, string name, string email, string body)
        {
            Post post = GetPost(id);
            Comment comment = new Models.Comment();
            comment.Post = post;
            comment.DateTime = DateTime.Now;
            comment.Name = name;
            comment.Email = email;
            comment.Body = body;
            model.Comments.Add(comment);
            model.SaveChanges();
            return RedirectToAction("Details", new { id = id });
        }
        
        public ActionResult Tags(string id)
        {
            Tag tag = GetTag(id);
            return View("Index", tag.Posts);
        }

        //GET: UPDATE
        [ValidateInput(false)]
        public ActionResult Update(int? id, string title, string body, DateTime dateTime, string tags)
        {
           if(!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index");
            }

            Post post = GetPost(id);
            post.Title = title;
            post.Body = body;
            post.DateTime = dateTime;
            post.Tags.Clear();

            tags = tags ?? string.Empty;
            string[] tagNames = tags.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string tagName in tagNames)
            {
                post.Tags.Add(GetTag(tagName));
            }
            if(!id.HasValue)
            {
               model.Posts.Add(post);
            }            
            model.SaveChanges();
            return RedirectToAction("Details", new { id = post.ID });

        }

        public ActionResult Delete (int id)
        {
            if(User.IsInRole("Admin"))
            {
                Post post = GetPost(id);
                model.Posts.Remove(post);
                model.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteComment(int id)
        {
            if(User.IsInRole("Admin"))
            {
                Comment comment = model.Comments.Where(x => x.ID == id).First();
                model.Comments.Remove(comment);
                model.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            Post post = GetPost(id);
            StringBuilder tagList = new StringBuilder();
            foreach (Tag tag in post.Tags)
            {
                tagList.AppendFormat("{0} ", tag.Name);
            }
            ViewBag.Tags = tagList.ToString();
            return View(post);
        }

        private Post GetPost(int? id)
        {

            return id.HasValue ? model.Posts.Where(x => x.ID == id).First() : new Post() { ID = -1 };
                     
        }

        private Tag GetTag(string tagName)
        {
            return model.Tags.Where(x => x.Name == tagName).FirstOrDefault() ?? new Tag() { Name = tagName };
        }

    }
}