using Uniwebsidad.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Uniwebsidad.BaseDatos.Maps
{
    public class AmistadMap: IEntityTypeConfiguration<Amistad>
    {
        public void Configure(EntityTypeBuilder<Amistad> builder)
        {
            builder.ToTable("Amistad");
            builder.HasKey(o => o.Id);

            

        }
    }
}