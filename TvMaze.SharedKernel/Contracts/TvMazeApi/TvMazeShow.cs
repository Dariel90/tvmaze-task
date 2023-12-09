namespace TvMaze.SharedKernel.Contracts.TvMazeApi;

public record TvMazeShow
(
    int id,
    string url,
    string name,
    string type,
    string language,
    string[] genres,
    string status,
    int runtime,
    int averageRuntime,
    string premiered,
    string ended,
    string officialSite,
    Schedule schedule,
    Rating rating,
    int weight,
    Network network,
    object webChannel,
    object dvdCountry,
    Externals externals,
    Image image,
    string summary,
    int updated,
    Links _links
);

public record Schedule
(
    string time,
    string[] days
);

public record Rating
(
    decimal average
);

public record Network
(
    int id,
    string name,
    Country country,
    string officialSite
);

public record Country
(
    string name,
    string code,
    string timezone
);

public record Externals
(
    int tvrage,
    int thetvdb,
    string imdb
);

public record Image
(
    string medium,
    string original
);

public record Links
(
    Self self,
    Previousepisode previousepisode
);

public record Self
(
    string href
);

public record Previousepisode
(
    string href
);