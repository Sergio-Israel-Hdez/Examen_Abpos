using System;
using System.Collections.Generic;

namespace Examen_Abpos.Models.DB
{
    public partial class Usuario
    {
        public Usuario()
        {
            Actividad = new HashSet<Actividad>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Rol { get; set; }

        public virtual ICollection<Actividad> Actividad { get; set; }
    }
}
