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
        [MaxLength(100)]
        public string DescripLlamada { get; set; }
        [Required]
        public bool Resuelto { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaRegistro { get; set; }
        [Required]        
        public int? IdTipo { get; set; }

        public virtual TipoLlamada IdTipoNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
