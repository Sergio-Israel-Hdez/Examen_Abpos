using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Examen_Abpos.Models.DB
{
    public partial class Usuario
    {
        public Usuario()
        {
            Actividad = new HashSet<Actividad>();
        }

        public int IdUsuario { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public int Rol { get; set; }

        public virtual ICollection<Actividad> Actividad { get; set; }
    }
}
