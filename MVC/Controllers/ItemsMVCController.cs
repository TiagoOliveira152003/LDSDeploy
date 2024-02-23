using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace MVC.Controllers
{
    public class ItemsMVCController : Controller
    {
        private readonly BurgerShopContext context;
        public ItemsMVCController(BurgerShopContext dbContext)
        {
            this.context = dbContext;
        }

        /// <summary>
        /// Função para mostrar os items na web
        /// </summary>
        /// <returns>view com a lista dos items</returns>
        public IActionResult Index()
        {
            List<Item> items = context.Items.ToList();
            return View(items);
        }

        
        public IActionResult Create()
        {
            Item item = new Item();
            item.Ingredientes = context.Ingredientes.ToList();
            ViewBag.Item = item;
            return View();
        }

        /// <summary>
        /// Função usada para criar um item na web pelo administrador e guardar na base de dados o mesmo
        /// </summary>
        /// <returns>retorna a view do index dos items</returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Item item, string[] ingredientesSelecionados)
        {

            if (context.Items.Count() >= 10)
            {
                ModelState.AddModelError("Name", "Maximo atingido");
            }

            List<Ingrediente> todosIngredientes = context.Ingredientes.ToList();
            List<Ingrediente> ingredientesDoItem = todosIngredientes
                .Where(ingrediente => ingredientesSelecionados.Contains(ingrediente.Name))
                .ToList();
            foreach (var ing in ingredientesSelecionados)
            {
                IngredienteAtribuido ingredienteAtribuido = new IngredienteAtribuido();
                ingredienteAtribuido.ItemName = item.Name;
                ingredienteAtribuido.Name = ing;
                context.IngredienteAtribuidos.Add(ingredienteAtribuido);
                context.SaveChanges();
            }
            context.Items.Add(item);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string? name)
        {
            Item item;
            if (name == null)
            {
                return BadRequest();
            }

            if (name.Equals(""))
            {
                return NotFound();
            }
            item = context.Items.Find(name);
            item.Ingredientes = context.Ingredientes.ToList();
            ViewBag.Item = item;
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        /// <summary>
        /// Função usada para editar um item já existente na base de dados
        /// </summary>
        /// <param name="item">item a ser editado</param>
        /// <param name="ingredientesSelecionados">Ingredientes selecionados na checkbox da pagina da web</param>
        /// <returns>retorna a view do index dos items</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item item, string[] ingredientesSelecionados)
        {
            List<Ingrediente> todosIngredientes = context.Ingredientes.ToList();
            List<Ingrediente> ingredientesDoItem = todosIngredientes
                .Where(ingrediente => ingredientesSelecionados.Contains(ingrediente.Name))
                .ToList();
            var itemsToRemove = context.IngredienteAtribuidos
                .Where(ing => ing.ItemName == item.Name)
                .ToList();

            foreach (var ing in itemsToRemove)
            {
                context.IngredienteAtribuidos.Remove(ing);
            }

            foreach (var ing in ingredientesSelecionados)
            {
                IngredienteAtribuido ingredienteAtribuido = new IngredienteAtribuido();
                ingredienteAtribuido.ItemName = item.Name;
                ingredienteAtribuido.Name = ing;
                context.IngredienteAtribuidos.Add(ingredienteAtribuido);
                context.SaveChanges();
            }

            context.Items.Update(item);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string? name)
        {
            Item item;

            if (name == null)
                return BadRequest();

            if (name.Equals(""))
                return NotFound();

            item = context.Items.Find(name);

            if (item == null)
                return NotFound();

            return View(item);
        }

        /// <summary>
        /// Função usada para eliminar um item da base de dados
        /// </summary>
        /// <param name="name">Nome do item a ser eliminado</param>
        /// <returns>retorna a view do index dos items</returns>
        [HttpPost, ActionName("DeletePost")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteItem(string? name)
        {
            Item item = context.Items.Find(name);

            if (item == null)
                return NotFound();
            var itemsToRemove = context.IngredienteAtribuidos
               .Where(ing => ing.ItemName == item.Name)
               .ToList();

            foreach (var ing in itemsToRemove)
            {
                context.IngredienteAtribuidos.Remove(ing);
            }
            context.Items.Remove(item);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
