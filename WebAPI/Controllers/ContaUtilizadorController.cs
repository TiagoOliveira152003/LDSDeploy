using Microsoft.AspNetCore.Mvc;
using ProjetoLDS.Models;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaUtilizadorController : ControllerBase
    {
        private readonly BurgerShopContext context;
        public ContaUtilizadorController(BurgerShopContext dBProjectContext) => this.context = dBProjectContext;

        [HttpPost]
        public ActionResult<ContaUtilizador> AddConta()
        {
            ContaUtilizador conta = new ContaUtilizador();
            context.ContaUtilizador.Add(conta);
            context.SaveChanges();
            return CreatedAtAction(nameof(AddConta), new { id = conta.Id }, conta);
        }
    }
}
