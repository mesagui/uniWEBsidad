using System;

namespace Uniwebsidad.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public int IdCategoria { get; set; }
        public String Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdCreador { get; set; }
    }
}