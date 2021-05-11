using System;

namespace Uniwebsidad.Models
{
    public class Mensaje
    {
        public int Id { get; set; }
        public String IdAmistad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdUsuarioCreador { get; set; }
        public int IdUsuarioReceptor { get; set; }
    }
}