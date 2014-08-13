using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSystem.Models
{
	public class User
	{
		public int ID { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string PasswordSalt { get; set; }

		// each User has multiple Posts
		public virtual ICollection<Post> Posts { get; set; }
	}

	// model used to log in user
	public class LoginModel
	{
		[Required]
		[StringLength(20)]
		public string Username { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[StringLength(20, MinimumLength = 6)]
		public string Password { get; set; }

		[Display(Name = "Remember Me? ")]
		public bool RememberMe { get; set; }
	}

	// model used to register user
	public class RegisterModel
	{
		[Required]
		[StringLength(20)]
		public string Username { get; set; }

		[Required]
		[EmailAddress]
		[StringLength(150)]
		[Display(Name = "Email Address")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[StringLength(20, MinimumLength = 6)]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		[Compare("Password", ErrorMessage = "Passwords do not match.")]
		public string ConfirmPw { get; set; }

	}
}