using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetData()
        {
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetDataById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateData([FromBody] string body)
        {
            return Created("","");
        }

        [HttpPut]
        public async Task<ActionResult<Category>> UpdateData([FromBody] string body)
        {
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public string DeleteData(int id)
        {
            return "Olá Mundo!";
        }

    }
}