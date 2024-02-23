using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly BurgerShopContext DbContext;
        public StockController(BurgerShopContext burgerShopContext) => this.DbContext = burgerShopContext;

        //GET
        [HttpGet]
        public ActionResult<IEnumerable<Ingrediente>> GetIngredients()
        {
            if (DbContext.Ingredientes == null)
            {
                return NotFound();
            }
            return Ok(DbContext.Ingredientes.ToList());
        }
        //GET ID
        [HttpGet("{id}")]
        public ActionResult<Ingrediente> GetIngredient(string name)
        {
            if (DbContext.Ingredientes == null)
            {
                return NotFound();
            }
            var ingrediente = DbContext.Ingredientes.SingleOrDefault(s => s.Name == name);
            if (ingrediente == null)
            {
                return NotFound();
            }
            return Ok(ingrediente);
        }

        [HttpPost]
        public ActionResult<Ingrediente> Add(Ingrediente ingrediente)
        {
            DbContext.Ingredientes.Add(ingrediente);
            DbContext.SaveChanges();
            return CreatedAtAction(nameof(Add), new { id = ingrediente.Name }, ingrediente);
        }

        [HttpPut("{name}")]
        public IActionResult Edit(string name, Ingrediente ingredient)
        {
            if (!ingredient.Name.Equals(name))
            {
                return BadRequest();
            }
            DbContext.Ingredientes.Entry(ingredient).State = EntityState.Modified;
            DbContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            if (DbContext.Ingredientes == null)
            {
                return NotFound();
            }
            var ingredient = DbContext.Ingredientes.SingleOrDefault(s => s.Name == name);
            if (ingredient == null)
            {
                return NotFound();
            }
            DbContext.Ingredientes.Remove(ingredient);
            DbContext.SaveChanges();
            return NoContent();
        }
    }
}
