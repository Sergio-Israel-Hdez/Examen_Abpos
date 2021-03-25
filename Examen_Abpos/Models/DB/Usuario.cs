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
        [MaxLength(100)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        public int Rol { get; set; }

        public virtual ICollection<Actividad> Actividad { get; set; }
    }
}
