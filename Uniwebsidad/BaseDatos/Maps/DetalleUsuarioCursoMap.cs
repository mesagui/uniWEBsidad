using Uniwebsidad.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Uniwebsidad.BaseDatos.Maps
{
    public class DetalleUsuarioCursoMap: IEntityTypeConfiguration<DetalleUsuarioCurso>
    {
        public void Configure(EntityTypeBuilder<DetalleUsuarioCurso> builder)
        {
            builder.ToTable("DetalleUsuarioCurso");
            builder.HasKey(o => o.Id);

            

        }
    }
}