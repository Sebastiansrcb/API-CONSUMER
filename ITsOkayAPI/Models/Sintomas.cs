using System.ComponentModel.DataAnnotations;

namespace ITsOkayAPI.Models
{
    public class Sintoma
    {
        [Key]
        public int IdSintoma { get; set; }

        [Required]
        [StringLength(180)]
        public string Nombre { get; set; }
    }
}
