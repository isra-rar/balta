using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext dbContext)
        {
            var products = await dbContext.Products.Include(x => x.Category).AsNoTracking().ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<Product>>> GetById(int id, [FromServices] DataContext dbContext)
        {
            var product = await dbContext.Products.Include(x => x.Category)
            .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            
            return Ok(product);
        }

        [HttpGet("categories/{id:int}")]
        public async Task<ActionResult<List<Product>>> GetByCategory(int id, [FromServices] DataContext dbContext)
        {
            var products = await dbContext.Products.Include(x => x.Category)
            .AsNoTracking().Where(x => x.CategoryId == id).ToListAsync();
            
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromServices] DataContext dbContext,
        [FromBody] Product model)
        {
            if (ModelState.IsValid)
            {
                dbContext.Products.Add(model);
                await dbContext.SaveChangesAsync();
                return Created(new Uri($"{Request.Path}/{model.Id}", UriKind.Relative), model);
            }
            else
                return BadRequest(ModelState);
        }
    }
}