using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoLDS.Models
{
    public class ItemCompra
    {
        [Key]
        [Required, MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Preço atual do itemCompra
        /// </summary>
        public int Preco { get; set; }

        public ItemCompra() { }

        public ItemCompra(string name, int preco)
        {
            Name = name;
            Preco = preco;
        }

        public virtual void Use() { throw new Exception(); }
    }
}