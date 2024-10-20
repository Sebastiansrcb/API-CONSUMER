using System.ComponentModel.DataAnnotations;

namespace ITsOkayAPI.Models
{
    public class Pacientes
    {
        [Key]
        public int IdRelacion { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public int IdPsicologo { get; set; }
    }
}
