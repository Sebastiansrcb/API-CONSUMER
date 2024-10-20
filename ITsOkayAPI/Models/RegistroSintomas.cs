using System.ComponentModel.DataAnnotations;

namespace ITsOkayAPI.Models
{
    public class RegistroSintomas
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdRegistro { get; set; }

        [Required]
        public int IdSintoma { get; set; }
    }
}
