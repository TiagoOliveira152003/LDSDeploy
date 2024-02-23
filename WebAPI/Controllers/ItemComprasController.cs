using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoLDS.Models;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemComprasController : Controller
    {
        private readonly BurgerShopContext context;
        public ItemComprasController(BurgerShopContext dBProjectContext) => this.context = dBProjectContext;

        [HttpPost]
        public ActionResult<ItemCompra> CreateCompra(string name, int preco)
        {
            ItemCompra itemCompra = new ItemCompra(name,preco);
            context.ItemCompras.Add(itemCompra);
            context.SaveChanges();
            return CreatedAtAction(nameof(CreateCompra), new { name = itemCompra.Name }, itemCompra);
        }
    }
}
