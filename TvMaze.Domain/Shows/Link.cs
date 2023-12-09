using System.ComponentModel.DataAnnotations.Schema;

namespace TvMaze.Domain.Shows;

public class Link
{
    public Url Self { get; set; }
    public Url PreviousEpisode { get; set; }

    public Link()
    {
    }

    public Link(string self, string previousEpisode)
    {
        Self = new Url(self);
        PreviousEpisode = new Url(previousEpisode);
    }
}

[ComplexType]
public record Url(string Href);