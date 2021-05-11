namespace Uniwebsidad.Models
{
    public class DetalleUsuarioCurso
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdCurso { get; set; }
        public bool Estado { get; set; }
    }
}