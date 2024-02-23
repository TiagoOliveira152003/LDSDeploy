using ProjetoLDS.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using WebAPI.Enums;

namespace WebAPI.Models
{
    public class Item : ItemCompra
    {

        public List<Ingrediente>? Ingredientes { get; set; }
        public EnumTypeComida TypeComida { get; }

        public Item()
        {

        }

        public Item(EnumTypeComida typeComida, string name, int preco)
        {
            TypeComida = typeComida;
            Name = name;
            Preco = preco;
        }

        /// <summary>
        /// Usar item, consiste em remover o stock dos ingredientes
        /// </summary>
        public override void Use()
        {
            foreach (var item in Ingredientes)
            {
                item.Use();
            }
        }

        /// <summary>
        /// Adicionar Ingredientes ao item
        /// </summary>
        /// <param name="ingrediente"> Ingrediente a adicionar </param>
        private void AddIngrediente(Ingrediente ingrediente)
        {
            Ingredientes.Add(ingrediente);
        }

        /// <summary>
        /// Adicionar uma lista de ingredientes ao item
        /// </summary>
        /// <param name="ingredientes"> Ingredientes a adicionar </param>
        private void AddIngredientes(IEnumerable<Ingrediente> ingredientes)
        {
            foreach (var ingrediente in ingredientes)
            {
                Ingredientes.Add(ingrediente);
            }
        }
    }
}