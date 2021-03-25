using System;
using System.Collections.Generic;

namespace Examen_Abpos.Models.DB
{
    public partial class Actividad
    {
        public int IdActividad { get; set; }
        public decimal DuracionLlamada { get; set; }
        public string DescripLlamada { get; set; }
        public bool? Resuelto { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int? IdTipo { get; set; }

        public virtual TipoLlamada IdTipoNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
