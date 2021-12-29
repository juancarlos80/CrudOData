using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using WebApiExample2.Models;
using Microsoft.EntityFrameworkCore;

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

    [EnableQuery]
    public SingleResult<Product> Get([FromODataUri] Guid key)
    {
        var result = _context.Products.Where(c => c.Id == key);
        return SingleResult.Create(result);
    }

    [EnableQuery]
    public async Task<IActionResult> Post([FromBody] Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return Created(product);
    }

    [EnableQuery]
    public async Task<IActionResult> Patch([FromODataUri] Guid key, Delta<Product> product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var existingProduct = await _context.Products.FindAsync(key);
        if (existingProduct is null)
        {
            return NotFound();
        }

        product.Patch(existingProduct);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(key))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return Updated(existingProduct);
    }

    [EnableQuery]
    public async Task<IActionResult> Delete([FromODataUri] Guid key)
    {
        Product? existingProduct = await _context.Products.FindAsync(key);        
        if (existingProduct is null)
        {
            return NotFound();
        }

        _context.Products.Remove(existingProduct);
        await _context.SaveChangesAsync();
        return StatusCode(StatusCodes.Status204NoContent);
    }

    public bool ProductExists(Guid key) { 
        return _context.Products.Any( p => p.Id == key);
    }
}