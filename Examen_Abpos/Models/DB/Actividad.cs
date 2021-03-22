using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Examen_Abpos.Models.DB
{
    public partial class Actividad
    {
        public int IdActividad { get; set; }
        [Required]
        public decimal DuracionLlamada { get; set; }
        [Required]
        public string MotivoLlamada { get; set; }
        [Required]
        public string DescripLlamada { get; set; }
        [Required]
        public bool Resuelto { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
