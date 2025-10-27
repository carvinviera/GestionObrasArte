using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionObrasArte.Shared.Models
{
    public class Pintura
    {
        public int IdPintura { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        public string TituloPintura { get; set; } = string.Empty;

        public int Fk_IdArtista { get; set; }

        public int FK_IdTipoPintura { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser positivo")]
        public decimal Precio { get; set; }

        // Propiedades de navegación (útiles para DTOs o vistas)
        public Artista? Artista { get; set; }
        public TipoPintura? TipoPintura { get; set; }
    }
}
