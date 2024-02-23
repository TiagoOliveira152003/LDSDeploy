using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly BurgerShopContext DbContext;
        public ItemsController(BurgerShopContext burgerShopContext) => this.DbContext = burgerShopContext;

        //GET
        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetItems()
        {
            if (DbContext.Items == null)
            {
                return NotFound();
            }
            return Ok(DbContext.Items.ToList());
        }
        //GET ID
        [HttpGet("{name}")]
        public ActionResult<Item> GetItem(String name)
        {
            if (DbContext.Items == null)
            {
                return NotFound();
            }
            var Item = DbContext.Items.SingleOrDefault(s => s.Name == name);
            if (Item == null)
            {
                return NotFound();
            }
            return Ok(Item);
        }

        [HttpPost]
        public ActionResult<Item> Add(Item Item)
        {
            DbContext.Items.Add(Item);
            DbContext.SaveChanges();
            return CreatedAtAction(nameof(Add), new { name = Item.Name }, Item);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, Item item)
        {
            if (!item.Name.Equals(id))
            {
                return BadRequest();
            }
            DbContext.Items.Entry(item).State = EntityState.Modified;
            DbContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{name}")]
        public IActionResult Delete(String name)
        {
            if (DbContext.Items == null)
            {
                return NotFound();
            }
            var item = DbContext.Items.SingleOrDefault(s => s.Name == name);
            if (item == null)
            {
                return NotFound();
            }
            DbContext.Items.Remove(item);
            DbContext.SaveChanges();
            return NoContent();
        }
    }
}
