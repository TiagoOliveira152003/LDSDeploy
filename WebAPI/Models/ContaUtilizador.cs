using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoLDS.Models
{
    public class ContaUtilizador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        [EmailAddress]
        public string? Email { get; set; }


        public ContaUtilizador()
        {
            
        }
    }
}