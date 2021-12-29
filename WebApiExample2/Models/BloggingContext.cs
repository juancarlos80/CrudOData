using Microsoft.EntityFrameworkCore;

namespace WebApiExample2.Models;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Product> Products { get; set; }

    public BloggingContext( DbContextOptions<BloggingContext> contextOptions)
        :base(contextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>().ToTable("Blog");
        modelBuilder.Entity<Post>().ToTable("Post");
        modelBuilder.Entity<Product>().ToTable("Product");
    }
}