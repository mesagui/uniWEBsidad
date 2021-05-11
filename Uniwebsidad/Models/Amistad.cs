using System;

namespace Uniwebsidad.Models
{
    public class Amistad
    {
        public int Id { get; set; }
        public int IdUsuario1 { get; set; }
        public int IdUsuario2 { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}