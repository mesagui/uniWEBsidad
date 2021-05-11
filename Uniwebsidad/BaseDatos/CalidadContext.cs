using Uniwebsidad.BaseDatos.Maps;
using Uniwebsidad.Models;
using Microsoft.EntityFrameworkCore;

namespace Uniwebsidad.BaseDatos
{

    public class CalidadContext: DbContext
    {
        public DbSet<Amistad> _Amistades { get; set; }
        public DbSet<Categoria> _Categorias { get; set; }
      
        public DbSet<Curso> _Cursos { get; set; }
        public DbSet<DetalleUsuarioCurso> _DetalleUsuarioCursos { get; set; }
        public DbSet<Mensaje> _Mensajes { get; set; }
        public DbSet<Usuario> _Usuarios { get; set; }
        public DbSet<Video> _Videos { get; set; }
        

        public CalidadContext(DbContextOptions<CalidadContext> options)
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AmistadMap());
            modelBuilder.ApplyConfiguration(new CategoriaMap());
           
            modelBuilder.ApplyConfiguration(new CursoMap());
            modelBuilder.ApplyConfiguration(new DetalleUsuarioCursoMap());
            modelBuilder.ApplyConfiguration(new VideoMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            

        }
    }
}