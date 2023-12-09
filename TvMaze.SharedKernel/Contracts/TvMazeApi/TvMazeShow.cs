namespace TvMaze.SharedKernel.Contracts.TvMazeApi;

public sealed class TvMazeShow
{
    public int id { get; set; }
    public string url { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public string language { get; set; }
    public string[] genres { get; set; }
    public string status { get; set; }
    public int runtime { get; set; }
    public int averageRuntime { get; set; }
    public string premiered { get; set; }
    public string ended { get; set; }
    public string officialSite { get; set; }
    public Schedule schedule { get; set; }
    public Rating rating { get; set; }
    public int weight { get; set; }
    public Network network { get; set; }
    public object webChannel { get; set; }
    public object dvdCountry { get; set; }
    public Externals externals { get; set; }
    public Image image { get; set; }
    public string summary { get; set; }
    public int updated { get; set; }
    public Links _links { get; set; }
}

public sealed class Schedule
{
    public string time { get; set; }
    public string[] days { get; set; }
};

public sealed class Rating
{
    public decimal average { get; set; }
};

public sealed class Network
{
    public int id { get; set; }
    public string name { get; set; }
    public Country country { get; set; }
    public string officialSite { get; set; }
}

public sealed class Country
{
    public string name { get; set; }
    public string code { get; set; }
    public string timezone { get; set; }
}

public sealed class Externals
{
    public int tvrage { get; set; }
    public int thetvdb { get; set; }
    public string imdb { get; set; }
}

public sealed class Image
{
    public string medium { get; set; }
    public string original { get; set; }
}

public sealed class Links
{
    public Self self { get; set; }
    public Previousepisode previousepisode { get; set; }
}

public sealed class Self
{
    public string href { get; set; }
}

public sealed class Previousepisode
{
    public string href { get; set; }
}