using Microsoft.EntityFrameworkCore;
using ProjetoLDS.Models;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class BurgerShopContext:DbContext
    {
        public BurgerShopContext(DbContextOptions<BurgerShopContext> options) : base(options) { }

        public DbSet<ItemCompra> ItemCompras { get; set; }

        public DbSet<ItemPedido> ItemPedidos { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<ContaUtilizador> ContaUtilizador { get; set; }

        public DbSet<Ingrediente> Ingredientes { get; set; }

        public DbSet<Ticket> Ticket { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<EditIngredientes> EditIngredientes { get;set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<IngredienteAtribuido> IngredienteAtribuidos { get; set; }

        public object Ingrediente { get; set; }
    }
}
