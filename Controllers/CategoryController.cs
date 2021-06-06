using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetData([FromServices] DataContext dbContext)
        {
            try
            {
                var categories = await dbContext.Categories.AsNoTracking().ToListAsync();
                return Ok(categories);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não existem categorias no banco de dados" });
            }

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetDataById(int id, [FromServices] DataContext dbContext)
        {
            try
            {
                var category = await dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return NotFound(new { message = "Categoria não encontrada" });

                return Ok(category);

            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possivel retornar uma categoria" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateData([FromBody] Category model,
         [FromServices] DataContext dbContext)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {

                dbContext.Categories.Add(model);
                await dbContext.SaveChangesAsync();
                return Created(new Uri($"{Request.Path}/{model.Id}", UriKind.Relative), model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi posssivel criar a categoria" });
            }

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Category>> UpdateData(int id, [FromBody] Category model,
        [FromServices] DataContext dbContext)
        {
            var category = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound(new { message = "Categoria não encontrada" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                category.Title = model.Title;
                dbContext.Update(category);
                await dbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Registro já atualizado" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possivel atualizar a categoria" });
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<List<Category>>> DeleteData(int id, [FromServices] DataContext dbContext)
        {
            var category = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound(new { message = "Categoria não encontrada" });

            try
            {
                dbContext.Categories.Remove(category);
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possivel deletar a categoria" });
            }

        }

    }
}