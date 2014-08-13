using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSystem.Models;

namespace BlogSystem.Controllers
{
    public class HomeController : Controller	// PostController
    {
		// instantiate database
        private MainDbContext db = new MainDbContext();

        // GET: /Home/
		[HttpGet]
        public ActionResult Index()
        {
			// display posts in descending order, i.e. newest first
			return View(db.Posts.OrderByDescending(p => p.Timestamp).ToList());
        }

        // GET: /Home/Details/5
		[HttpGet]
        public ActionResult Details(int id = 0)
        {
			// search for post using provided ID
            Post post = db.Posts.Find(id);

			// if post is not found, throw 404 error
            if (post == null)
            {
                return HttpNotFound();
            }

			// else continue /Home/Details/5
            return View(post);
        }

		// GET: /Home/Create
		[HttpGet]
		[Authorize]
        public ActionResult Create()
        {
            return View();
        }

		// POST: /Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
			// verify that provided model is valid
            if (ModelState.IsValid)
            {
				// pull username from stored cookie
				post.Username = User.Identity.Name;

				// add timestamp to post
				post.Timestamp = DateTime.Now;

				// store new Post record into Posts table and save
                db.Posts.Add(post);
                db.SaveChanges();

				// once post has been created, return to home page
                return RedirectToAction("Index", "Home");
            }

			else
			{
				// if model is invalid, throw error
				ModelState.AddModelError("", "Post creation failed.");
			}

            return View(post);
        }

		// GET: /Home/Edit/5
		[HttpGet]
		[Authorize]
        public ActionResult Edit(int id = 0)
        {
			// search for post using provided ID
            Post post = db.Posts.Find(id);

			// if post is not found, throw 404 error
            if (post == null)
            {
                return HttpNotFound();
            }

			// else continue to /Home/Edit/5
            return View(post);
        }

		// POST: /Home/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
			// verify that provided model is valid
            if (ModelState.IsValid)
            {
				// update and save edited values
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();

				// once edits have been completed, return to home page
                return RedirectToAction("Index", "Home");
            }

			else
			{
				// if model is invalid, throw error
				ModelState.AddModelError("", "Post editing failed.");
			}

            return View(post);
        }

		// GET: /Home/Delete/5
		[HttpGet]
		[Authorize]
        public ActionResult Delete(int id = 0)
        {
			// search for post using provided ID
            Post post = db.Posts.Find(id);

			// if post not found, throw 404 error
            if (post == null)
            {
                return HttpNotFound();
            }

			// else continue to /Home/Delete/5
            return View(post);
        }

		// POST: /Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			// search for post using provided ID
            Post post = db.Posts.Find(id);

			// once confirmed, remove record from Posts table and save
            db.Posts.Remove(post);
            db.SaveChanges();

			// once post is deleted, return to home page
            return RedirectToAction("Index", "Home");
        }

		// release resources used by HomeController
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}