using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Enums;
using WebAPI.Models;

namespace MVC.Controllers
{
    public class PedidoMVCController : Controller
    {

        private readonly BurgerShopContext context;
        public PedidoMVCController(BurgerShopContext dbContext)

        {
            this.context = dbContext;
        }

        /// <summary>
        /// Função para mostrar pagina inicial com a listagem de pedidos
        /// </summary>
        /// <returns>A pagina web com os pedidos visiveis</returns>
        public IActionResult Index()
        {
            List<Pedido> Pedidos = context.Pedidos.ToList();
            return View(Pedidos);
        }

        /// <summary>
        /// Função usada para detalhar os pedidos na pagina de detalhes na web
        /// </summary>
        /// <param name="id">Id do pedido a ser detalhado</param>
        /// <returns>NotFound se não for encontrado o pedido, View com o pedido se este for encontrado</returns>
        public IActionResult Details(int id)
        {

            var pedido = context.Pedidos
                     .Include(p => p.ItensPedido)
                     .ThenInclude(ip => ip.ItemCompra)
                     .FirstOrDefault(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound(); // Pedido não encontrado
            }

            return View(pedido);
        }

        [HttpPost]
        public IActionResult MudarStatus(int pedidoId, string novoStatus)
        {
            // Encontrar o pedido pelo ID no banco de dados
            var pedido = context.Pedidos.FirstOrDefault(p => p.Id == pedidoId);

            if (pedido != null)
            {
                if (novoStatus.Equals("Preparar"))
                {
                    pedido.Status = (EnumStatusPedido)0;
                } else if (novoStatus.Equals("Pronto"))
                {
                    pedido.Status = (EnumStatusPedido)1;
                } else if (novoStatus.Equals("Entregue"))
                {
                    pedido.Status = (EnumStatusPedido)2;
                } else if (novoStatus.Equals("Cancelado"))
                {
                    pedido.Status = (EnumStatusPedido)3;
                }
                
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return Details(pedidoId);
            }
        }

        /// <summary>
        /// Função Responsavel por apresentar apresentar a View de criação de pedido ao utilizador
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Função que envia um pedido do utilizador para o WebApi
        /// </summary>
        /// <param name="pedidoViewModel">Pedido do Utilizador</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Pedido pedidoViewModel)
        {


            using (HttpClient client = new HttpClient())
            {
                // Configure o endereço base da sua API
                client.BaseAddress = new Uri("https://localhost:7235/");

                // Serializar o objeto pedidoViewModel para JSON e enviá-lo para a API
                HttpResponseMessage response = client.PostAsJsonAsync("api/pedidos", pedidoViewModel).Result;

                // Verificar se a solicitação foi bem-sucedida
                if (response.IsSuccessStatusCode)
                {
                    // Redirecionar para a página desejada após o processamento
                    return RedirectToAction("Index");
                }
                else
                {
                    // Lógica para lidar com falhas na comunicação com a API
                    // Pode ser útil logar ou exibir mensagens de erro
                    var statusCode = response.StatusCode;
                    ModelState.AddModelError(string.Empty, $"Erro na comunicação com a API. Código de status: {statusCode}");
                    var errorContent = response.Content.ReadAsStringAsync().Result;
                    ModelState.AddModelError(string.Empty, $"Erro na comunicação com a API. Detalhes: {errorContent}");
                    return View(pedidoViewModel);
                }
            }
        }


    }
}
