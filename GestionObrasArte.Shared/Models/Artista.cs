using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionObrasArte.Shared.Models
{
    public class Artista
    {
        public int IdArtista { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string NombreArtista { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son obligatorios")]
        public string ApellidosArtista { get; set; } = string.Empty;

        [Range(1, 2025, ErrorMessage = "El año de nacimiento no es válido")]
        public int AñoNacimientoArtista { get; set; }
    }
}
