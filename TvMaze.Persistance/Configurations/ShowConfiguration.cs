using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TvMaze.Domain.Shows;
using TvMaze.Persistence.Constants;

namespace TvMaze.Persistence.Configurations;

internal sealed class ShowConfiguration : IEntityTypeConfiguration<Show>
{
    public void Configure(EntityTypeBuilder<Show> builder)
    {
        builder.ToTable(TableNames.Shows);
        builder.HasKey(x => x.SysId);

        builder.HasOne(q => q.Network)
            .WithMany(q => q.Shows)
            .HasForeignKey(q => q.NetworkSysId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ComplexProperty(s => s.Externals);

        builder.ComplexProperty(s => s.Rating);
        builder.ComplexProperty(s => s.Image);

        builder.ComplexProperty(s => s.Links);

        builder.OwnsMany(r => r.Genres);
        builder.OwnsMany(s => s.Schedules);
    }
}