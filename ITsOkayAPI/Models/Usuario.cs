using System.ComponentModel.DataAnnotations;

namespace ITsOkayAPI.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        public string Rol { get; set; }  // Puede ser un Enum en lugar de string, dependiendo de tu lógica

        [StringLength(11)]
        public string Numero { get; set; }

        [StringLength(255)]
        public string Instituto { get; set; }
    }
}
