using System;
using System.Collections.Generic;

namespace Examen_Abpos.Models.DB
{
    public partial class TipoLlamada
    {
        public TipoLlamada()
        {
            Actividad = new HashSet<Actividad>();
        }

        public int IdTipo { get; set; }
        public string NombreTipo { get; set; }

        public virtual ICollection<Actividad> Actividad { get; set; }
    }
}
