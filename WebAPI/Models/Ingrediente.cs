using System.ComponentModel.DataAnnotations;

using WebAPI.Enums;


namespace WebAPI.Models
{
    public class Ingrediente
    {
        [Key]
        [MaxLength(20)]

        public string Name { get; set; }
        public EnumTypeComida TypeComida { get; set; }
        public int Stock { get; set; }

        /// <summary>
        /// Preço do ingrediente, se 0 significa que é impossivel adicionar ou remover de um pedido
        /// </summary>
        public int Price { get; set; }
        public string? Image { get; set; }

        public Ingrediente() { }

        public Ingrediente(string name, EnumTypeComida typeComida, int stock)
        {
            Name = name;
            Price = -1;
            TypeComida = typeComida;
            Stock = stock;
        }

        public Ingrediente(string name, EnumTypeComida typeComida, int stock,int price)
        {
            Name = name;
            Price = price;
            TypeComida = typeComida;
            Stock = stock;
        }

        /// <summary>
        /// Adicionar stock existente
        /// </summary>
        /// <param name="stock"></param>
        public void AddStock(int stock)
        {
            Stock += stock;
        }

        /// <summary>
        /// Usar ingrediente
        /// </summary>
        public void Use()
        {
            --Stock;
        }

        /// <summary>
        /// Verificar se há stock
        /// </summary>
        /// <returns> True se houver stock, false se nao houver </returns>
        public bool CheckStock()
        {
            return Stock > 0;
        }

        /// <summary>
        /// Verficar se é possivel adicionar e remover de um tem
        /// </summary>
        /// <returns> True se for possivel adicionar e remover de um item </returns>
        public bool CanEdit()
        {
            return Price != -1;
        }

    }
}
