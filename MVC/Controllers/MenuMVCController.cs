using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace MVC.Controllers
{
    public class MenuMVCController : Controller
    {

        private readonly BurgerShopContext context;
        public MenuMVCController(BurgerShopContext dbContext)

        {
            this.context = dbContext;
        }

        /// <summary>
        /// Função para mostrar pagina inicial com a listagem de menus
        /// </summary>
        /// <returns>A pagina web com os menus visiveis</returns>
        public IActionResult Index()
        {
            List<Menu> Menus = context.Menus.ToList();
            return View(Menus);
        }

        /// <summary>
        /// Função usada para detalhar os menus na pagina de detalhes na web
        /// </summary>
        /// <param name="id">nome do menu a ser detalhado</param>
        /// <returns>NotFound se não for encontrado o menu, View com o menu se este for encontrado</returns>
        public IActionResult Details(string Name)
        {

            var menu = context.Menus
                    .Include(m => m.Items) // Certifique-se de incluir os itens relacionados
                    .FirstOrDefault(m => m.Name == Name); // Ou outro critério de busca

            if (menu == null)
            {
                return NotFound(); // Menu não encontrado
            }

            return View(menu);
        }


    }
}
