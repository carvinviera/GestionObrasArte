using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionObrasArte.Shared.Models
{
    public class TipoPintura
    {
        public int IdTipoPintura { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        public string TítuloTipoPintura { get; set; } = string.Empty;
    }
}