using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using WebAPI.Data;
using WebAPI.Models;

namespace MVC.Controllers
{
    public class StockMVCController : Controller
    {
        private readonly BurgerShopContext context;
        public StockMVCController(BurgerShopContext dbContext)
        {
            this.context = dbContext;
        }

        /// <summary>
        /// Função para mostrar os ingredientes na web
        /// </summary>
        /// <returns>view com a lista dos items</returns>
        public IActionResult Index()
        {
            List<Ingrediente> ingredientes = context.Ingredientes.ToList();
            return View(ingredientes);
        }

        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Função usada para criar um ingrediente na base de dados
        /// </summary>
        /// <param name="ingrediente">ingrediente a ser enviado no pedido para a base de dados</param>
        /// <returns>retorna a view da lista de ingredientes</returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Ingrediente ingrediente)
        {

            if (context.Ingredientes.Count() >= 10)
            {
                ModelState.AddModelError("Name", "Maximo atingido");
            }

            if (!ModelState.IsValid)
            {
                return View(ingrediente);
            }
            context.Ingredientes.Add(ingrediente);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Edit(string? name)
        {
            Ingrediente ingrediente;
            if (name == null)
            {
                return BadRequest();
            }

            if (name.Equals(""))
            {
                return NotFound();
            }
            ingrediente = context.Ingredientes.Find(name);
            if (ingrediente == null)
            {
                return NotFound();
            }
            return View(ingrediente);
        }


        /// <summary>
        /// Função usada para editar um ingrediente na base de dados
        /// </summary>
        /// <param name="ingrediente">ingrediente a ser editado</param>
        /// <returns>retorna a view da lista de ingredientes</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Ingrediente ingrediente)
        {
            if (!ModelState.IsValid)
                return View(ingrediente);

            // update data
            context.Ingredientes.Update(ingrediente);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string? name)
        {
            Ingrediente ingrediente;

            if (name == null)
                return BadRequest();

            if (name.Equals(""))
                return NotFound();

            ingrediente = context.Ingredientes.Find(name);

            if (ingrediente == null)
                return NotFound();

            return View(ingrediente);
        }

        /// <summary>
        /// Função usada para eliminar um ingrediente da base de dados
        /// </summary>
        /// <param name="name">Nome do ingrediente a ser eliminado</param>
        /// <returns>retorna a view da lista de ingredientes</returns>
        [HttpPost, ActionName("DeletePost")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteIngrediente(string? name)
        {
            Ingrediente ingrediente = context.Ingredientes.Find(name);

            if (ingrediente == null)
                return NotFound();

            context.Ingredientes.Remove(ingrediente);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}