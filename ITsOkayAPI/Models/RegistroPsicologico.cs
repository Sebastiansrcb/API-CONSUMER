using System.ComponentModel.DataAnnotations;

namespace ITsOkayAPI.Models
{
    public class RegistroPsicologico
    {
        [Key]
        public int IdRegistro { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public string Hora { get; set; }

        [Required]
        public string DiaSemana { get; set; }  // Esto también puede ser un Enum en lugar de string

        [Required]
        public string EstadoHumor { get; set; }

        [Required]
        public string LugarAtaque { get; set; }
    }
}
