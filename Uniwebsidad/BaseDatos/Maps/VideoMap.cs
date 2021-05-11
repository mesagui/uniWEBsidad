using Uniwebsidad.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Uniwebsidad.BaseDatos.Maps
{
    public class VideoMap: IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.ToTable("Video");
            builder.HasKey(o => o.Id);

            

        }
    }
}