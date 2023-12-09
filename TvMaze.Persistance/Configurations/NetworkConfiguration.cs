using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TvMaze.Domain.Networks;
using TvMaze.Persistence.Constants;

namespace TvMaze.Persistence.Configurations;

internal sealed class NetworkConfiguration : IEntityTypeConfiguration<Network>
{
    public void Configure(EntityTypeBuilder<Network> builder)
    {
        builder.ToTable(TableNames.Networks);
        builder.HasKey(x => x.SysId);

        builder.ComplexProperty(s => s.Country);
    }
}