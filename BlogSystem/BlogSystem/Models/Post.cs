using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogSystem.Models
{ 
	public class Post
	{
		public int ID { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public string Content { get; set; }

		[StringLength(20)]
		public string Username { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime Timestamp { get; set; }

		// each Post contains one User
		public virtual User User { get; set; }

	}
}