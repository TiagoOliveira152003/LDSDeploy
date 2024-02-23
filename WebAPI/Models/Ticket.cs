using ProjetoLDS.Models;
using WebAPI.Enums;

namespace WebAPI.Models
{
    public class Ticket
    {

        public int Id { get; set; }

        public ContaUtilizador ContaUtilizador { get; set; }

        public string Titulo { get; set; }

        public string Texto { get; set; }

        public EnumStatusTicket Status { get; set; }

        public Ticket() { }
    }
}
