using TvMaze.Domain.Shows;
using TvMaze.SharedKernel.Entities;

namespace TvMaze.Domain.Networks;

public class Network
{
    public Guid SysId { get; set; }
    public int Id { get; set; }
    public string? Name { get; set; }
    public Country Country { get; set; }
    public string? OfficialSite { get; set; }
    public virtual List<Show> Shows { get; set; }
}