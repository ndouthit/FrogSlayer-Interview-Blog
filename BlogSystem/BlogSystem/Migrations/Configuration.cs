namespace BlogSystem.Migrations
{
	using BlogSystem.Models;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogSystem.Models.MainDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BlogSystem.Models.MainDbContext context)
        {
			context.Users.AddOrUpdate(u => u.Username,

				new User
				{
					ID = 1,
					Username = "admin",
					Email = "admin@localhost.net",
					// password = 12345678
					Password = "cEpxfXjYTpvQ3QVhNkZ3VwpyR6hyFlqoGtZu4tIC4O/vTqeTwWmuXwTSYs+yIP0RqgXTxN8ZT6sIGMd/ec/CJQ==",
					PasswordSalt = "100000.iz4jK4hHS8XxstsWhdprmfOm1/SrDZQz/K5jQAtTPpeynw=="
				}
			);

			context.Posts.AddOrUpdate(p => p.Username,

				new Post
				{
					ID = 1,
					Title = "This is your first blog post!",
					Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam venenatis posuere augue, ut eleifend nisi dapibus ac. Mauris dapibus nibh vitae nibh dictum laoreet. Maecenas vel congue dui. Aliquam sit amet varius nisi. Duis dapibus viverra enim, ac sagittis risus vestibulum nec. Pellentesque blandit elementum molestie. Morbi pretium dolor eu diam ultricies, eget laoreet dolor faucibus.",
					Timestamp = DateTime.Now,
					Username = "admin"
				}
			);
        }
    }
}
