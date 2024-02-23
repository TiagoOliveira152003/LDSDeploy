using Microsoft.AspNetCore.Mvc;
using ProjetoLDS.Models;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PedidosController : ControllerBase
	{
		private readonly BurgerShopContext context;

		public PedidosController(BurgerShopContext dBProjectContext) => this.context = dBProjectContext;

		[HttpGet]
		public ActionResult<IEnumerable<Pedido>> GetPedidos()
		{
			if (context.Pedidos == null)
				return NotFound();
			return Ok(context.Pedidos);
		}

		[HttpGet("{id}")]
		public ActionResult<Pedido> GetPedido(int id)
		{
			if (context.Pedidos == null)
				return NotFound();

			var pedido = context.Pedidos.SingleOrDefault(p => p.Id == id);

			if (pedido == null)
				return NotFound(id);

			return Ok(pedido);
		}


		/// <summary>
		/// Criar um Pedido
		/// </summary>
		/// <param name="idUtilizador">Utilizador que fara o pedido</param>
		/// <param name="itemsPedido">Items pedidos com as suas alterações</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult<Pedido> CriarPedido(Pedido pedidoCliente)
		{

			ContaUtilizador contaUtilizador = context.ContaUtilizador.Find(pedidoCliente.ContaUtilizador.Id);

			if (contaUtilizador == null)
				return BadRequest();

			List<ItemPedido> itemsConfirm = new List<ItemPedido>();

			foreach (var item in pedidoCliente.ItensPedido)
			{
				ItemCompra itemCompra = context.ItemCompras.Find(item.ItemCompra.Name);

				if (itemCompra == null)
					return BadRequest();

				if (item.EditIngredientes != null)
				{
					List<EditIngredientes> editIngredientesConfirm = new List<EditIngredientes>();
					foreach (var editIngrediente in item.EditIngredientes)
					{
						EditIngredientes editIngredienteConfirm = ConfirmEditIgrediente(editIngrediente);
						if (editIngredienteConfirm == null)
							return BadRequest(editIngrediente);

						editIngredientesConfirm.Add(editIngredienteConfirm);
					}

					itemsConfirm.Add(new ItemPedido(itemCompra, itemCompra.Preco, editIngredientesConfirm));
				}
				else
					itemsConfirm.Add(new ItemPedido(itemCompra, itemCompra.Preco));

			}

			Pedido pedido = new Pedido(contaUtilizador, itemsConfirm);

			context.Pedidos.Add(pedido);
			context.SaveChanges();
			return CreatedAtAction(nameof(CriarPedido), new { id = pedido.Id }, pedido);

		}


		/// <summary>
		/// Corrigir o editIngredientes, enviado pelo utilizador
		/// </summary>
		/// <param name="editIngredientes">Ingrediente enviado pelo utilizador</param>
		/// <returns>Ingrediente Corrigido</returns>
		private EditIngredientes? ConfirmEditIgrediente(EditIngredientes editIngredientes)
		{
			Ingrediente ingrediente = context.Ingredientes.Find(editIngredientes.Ingrediente.Name);

			if (ingrediente == null)
				return null;

			Item item = context.Items.Find(editIngredientes.Item.Name);

			if (item == null)
				return null;

			if (!ingrediente.CanEdit())
				return null;

			if (ingrediente.TypeComida != item.TypeComida)
				return null;

			return new EditIngredientes(item, ingrediente, editIngredientes.Active);
		}
	}
}
