using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using WebApiExample2.Models;

namespace WebApiExample2.Controllers;

public class ProductsController: ODataController
{
    private readonly BloggingContext _context;
    public ProductsController(BloggingContext context)
    {
        _context = context;
    }

    [EnableQuery(PageSize = 2)]
    public List<Product> Get()
    {
        return _context.Products.ToList();
    }
}