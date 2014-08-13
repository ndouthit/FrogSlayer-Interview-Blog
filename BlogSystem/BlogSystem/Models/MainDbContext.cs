using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogSystem.Models
{
	public class MainDbContext : DbContext
	{
		public MainDbContext()
			: base("MainDbContext")
		{
		}

		public DbSet<Post> Posts { get; set; }
		public DbSet<User> Users { get; set; }
	}
}