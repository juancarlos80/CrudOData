using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using WebApiExample2.Models;

namespace WebApiExample2.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogController : ControllerBase
{
    private readonly BloggingContext _context;

    public BlogController(BloggingContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    [EnableQuery]
    public List<Blog> GetAllBlog()
    {
        return _context.Blogs.ToList();
    }
}