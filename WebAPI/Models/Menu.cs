using ProjetoLDS.Models;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Menu
    {
        [Key]
        [Required]
        public string Name { get; set; }

        public List<Item>? Items { get; set; }

        public Menu() { }

        public Menu(IEnumerable<Item> items)
        {
            Items = new List<Item>(items);
        }
    }
}
