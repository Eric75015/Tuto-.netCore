using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploreCaliforniaTuto.Controllers;
using ExploreCaliforniaTuto.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ExploreCaliforniaTuto.api
{
    [Route("api/posts/{postKey}/comments")]
    public class CommentsController : Controller
    {
        private readonly BlogDataContext _db;

        public CommentsController(BlogDataContext db)
        {
            _db = db;
        }

        // GET: api/values
        [HttpGet]
        public IQueryable<Comment> Get(string postKey)
        {
            return _db.Comments.Where(o => o.Post.Key == postKey);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Comment Get(long id)
        {
            var com = _db.Comments.FirstOrDefault(o => o.Id == id);

            return com;
        }

        // POST api/values
        [HttpPost]
        public Comment Post(string postKey,[FromBody]Comment comment)
        {
            var post = _db.Posts.FirstOrDefault(o => o.Key == postKey);

            if (post == null)
            {
                return null;
            }
            comment.Post = post;
            comment.Posted = DateTime.Now;
            comment.Author = User.Identity.Name;

            _db.Comments.Add(comment);
            _db.SaveChanges();
            return comment;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]Comment comment)
        {
            var com = _db.Comments.FirstOrDefault(o => o.Id == id);

            if (com == null)
                return NotFound();

            com.Body = comment.Body;
            _db.SaveChanges();
            return Ok();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var com = _db.Comments.FirstOrDefault(o => o.Id == id);
            if (com != null)
            {
                _db.Comments.Remove(com);
                _db.SaveChanges();
            }

        }
    }
}
