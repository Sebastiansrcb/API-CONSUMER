using System.ComponentModel.DataAnnotations;

namespace ITsOkayAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; } 
        public string Numero { get; set; }
        public string Instituto { get; set; }
        public string? LastToken { get; set; }
    }
}
