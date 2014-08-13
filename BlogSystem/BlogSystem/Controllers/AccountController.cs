using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BlogSystem.Models;

namespace BlogSystem.Controllers
{
    public class AccountController : Controller
    {
		// instantiate database
		private MainDbContext db = new MainDbContext();

        //
        // GET: /Account/
		[HttpGet]
        public ActionResult Index()
        {
			// redirect /Account/ to home page
			return RedirectToAction("Index", "Home"); ;
        }

		[HttpGet]
		[AllowAnonymous]
		public ActionResult LogIn()
		{
			return View();
		}

		[HttpPost]
		public ActionResult LogIn(LoginModel user)
		{
			// verify that the model being passed is valid
			if (ModelState.IsValid)
			{
				// verify that the provided login details are valid
				if (isLoginValid(user.Username, user.Password))
				{
					// if verified, create cookie; persistent if "Remember Me" box is checked
					FormsAuthentication.SetAuthCookie(user.Username, user.RememberMe);

					// once logged in, return to home page
					return RedirectToAction("Index", "Home");
				}

				else
				{
					// if login details are invalid, throw error
					ModelState.AddModelError("", "Incorrect username or password.");
				}
			}

			return View(user);
		}

		[HttpGet]
		public ActionResult LogOut()
		{
			// delete cookie / sign out user
			FormsAuthentication.SignOut();
			return RedirectToAction("Index","Home");
		}

		[HttpGet]
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Register(RegisterModel user)
		{
			// verify that the model being passed is valid
			if (ModelState.IsValid)
			{
				// encrypt user password using PBKDF2 encryption
				var crypto = new SimpleCrypto.PBKDF2();
				var encryptedPw = crypto.Compute(user.Password);

				// create new record for Users table
				var newUser = db.Users.Create();

				// define values for new User record
				newUser.Username = user.Username;
				newUser.Email = user.Email;
				newUser.Password = encryptedPw;			// store encrypted password
				newUser.PasswordSalt = crypto.Salt;		// store password salt

				// add new record to the table and save
				db.Users.Add(newUser);
				db.SaveChanges();

				// once account created, redirect to /Account/Login
				return RedirectToAction("LogIn", "Account");
			}

			else
			{
				// if model is invalid, throw error
				ModelState.AddModelError("", "Account creation failed.");
			}

			return View(user);
		}

		// method used to verify username and password pair
		private bool isLoginValid(string username, string password)
		{
			var crypto = new SimpleCrypto.PBKDF2();

			bool isValid = false;

			// pull User from the database
			var user = db.Users.FirstOrDefault(u => u.Username == username);

			// if a record is found, proceed
			if (user != null)
			{
				// verify that the provided password matches the stored password
				if(user.Password == crypto.Compute(password, user.PasswordSalt))
				{
					isValid = true;
				}
			}

			return isValid;
		}

		// release resources used by AccountController
		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
    }
}
