using System.Linq.Expressions;

namespace WebApiExample2.Models;

public static class InitBloggingData
{
    public static void Initialize(BloggingContext context)
    {
        if (!context.Products.Any())
        {
            var products = new Product[]
            {
                new Product {Id = new Guid(), Name = "Product A", Price = 2, Category = "Basic"},
                new Product {Id = new Guid(), Name = "Product B", Price = 12, Category = "Basic"},
                new Product {Id = new Guid(), Name = "Product C", Price = 3, Category = "Basic"},
                new Product {Id = new Guid(), Name = "Product D", Price = 56, Category = "Advance"},
            };
        
            context.Products.AddRange(products);
            context.SaveChanges();    
        }

        
        
        if (!context.Blogs.Any())
        {
            var blogs = new Blog []
            {
                new Blog {BlogId = 1, Url = "http://asdasd1", Posts = new List<Post>(), Title = "TT1"},
                new Blog {BlogId = 2, Url = "http://asdasd2", Posts = new List<Post>(), Title = "TT2"},
                new Blog {BlogId = 3, Url = "http://asdasd3", Posts = new List<Post>(), Title = "TT3"},
                new Blog {BlogId = 4, Url = "http://asdasd4", Posts = new List<Post>(), Title = "TT4"}
            };
        
            context.Blogs.AddRange( blogs );
            context.SaveChanges();

            var posts = new Post[]
            {
                new Post {Blog = blogs.ElementAt(0), Content = "Content post 1", BlogId = 1, Title = "Title post 1", PostId = 1},
                new Post {Blog = blogs.ElementAt(1), Content = "Content post 2", BlogId = 1, Title = "Title post 2", PostId = 2},
                new Post {Blog = blogs.ElementAt(2), Content = "Content post 3", BlogId = 2, Title = "Title post 3", PostId = 3},
                new Post {Blog = blogs.ElementAt(2), Content = "Content post 4", BlogId = 2, Title = "Title post 4", PostId = 4},
                new Post {Blog = blogs.ElementAt(2), Content = "Content post 5", BlogId = 2, Title = "Title post 5", PostId = 5},
                new Post {Blog = blogs.ElementAt(3), Content = "Content post 6", BlogId = 3, Title = "Title post 6", PostId = 6},
                new Post {Blog = blogs.ElementAt(3), Content = "Content post 7", BlogId = 3, Title = "Title post 7", PostId = 7},
                new Post {Blog = blogs.ElementAt(3), Content = "Content post 8", BlogId = 3, Title = "Title post 8", PostId = 8},
                new Post {Blog = blogs.ElementAt(3), Content = "Content post 9", BlogId = 3, Title = "Title post 9", PostId = 9},
                new Post {Blog = blogs.ElementAt(3), Content = "Content post 10", BlogId = 3, Title = "Title post 10", PostId = 10}
            };

            context.Posts.AddRange( posts );
            context.SaveChanges();
        }
    }
}