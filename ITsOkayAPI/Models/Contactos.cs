using System.ComponentModel.DataAnnotations;

namespace ITsOkayAPI.Models
{
    public class Contactos
    {
        [Key]
        public int IdContacto { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(10)]
        public string Numero { get; set; }
    }
}
