using Bogus;
using FluentAssertions;
using Moq;
using TvMaze.Application.Abstractions.Data;
using TvMaze.Application.Shows.Create;
using TvMaze.Domain.Shows;
using TvMaze.SharedKernel.Contracts.TvMazeApi;
using TvMaze.SharedKernel.Core;

namespace TvMaze.Application.UnitTest;

public class CreateShowCommandHandlerTest
{
    private readonly Mock<IShowRepository> _showRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public CreateShowCommandHandlerTest()
    {
        _showRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenTheShowNotExistsInDb()
    {
        // Arrange
        var tvMazeShow = GetFakeTvMazeShowData(true);
        var command = new CreateShowCommand(tvMazeShow);

        _showRepositoryMock.Setup(
                x => x.IsAlreadyShowInDbAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var handler = new CreateShowCommandHandler(_showRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        Result<Guid> result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenTheShowAlreadyExistsInDb()
    {
        // Arrange
        var tvMazeShow = GetFakeTvMazeShowData(false, 1);
        var command = new CreateShowCommand(tvMazeShow);

        _showRepositoryMock.Setup(
                x => x.IsAlreadyShowInDbAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new CreateShowCommandHandler(_showRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        Result<Guid> result = await handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ShowErrors.AlreadyExists(tvMazeShow.id));
    }

    [Fact]
    public async Task Handle_Should_NotCallUnitOfWork_WhenShowAlreadyExistsInDb()
    {
        // Arrange
        var tvMazeShow = GetFakeTvMazeShowData(false, 1);
        var command = new CreateShowCommand(tvMazeShow);

        _showRepositoryMock.Setup(
                x => x.IsAlreadyShowInDbAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new CreateShowCommandHandler(_showRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

    private TvMazeShow GetFakeTvMazeShowData(bool isForRandomId, int? id = null)
    {
        var fakeTvShowSchedule = new Faker<SharedKernel.Contracts.TvMazeApi.Schedule>()
            .RuleFor(a => a.time, f => f.Date.SoonTimeOnly().ToString("HH:mm"))
            .RuleFor(a => a.days, f => new string[] { f.Date.Weekday(), f.Date.Weekday(), f.Date.Weekday() });

        var fakeTvShowRating = new Faker<SharedKernel.Contracts.TvMazeApi.Rating>()
            .RuleFor(a => a.average, f => f.Random.Decimal(0.00m, 100m));

        var fakeTvShowNetworkCountry = new Faker<SharedKernel.Contracts.TvMazeApi.Country>()
            .RuleFor(a => a.name, f => f.Address.Country())
            .RuleFor(a => a.code, f => f.Address.CountryCode())
            .RuleFor(a => a.timezone, f => f.Date.TimeZoneString());

        var fakeTvShowNetwork = new Faker<SharedKernel.Contracts.TvMazeApi.Network>()
            .RuleFor(a => a.id, f => f.Random.Number(100, 200))
            .RuleFor(a => a.name, f => f.Company.CompanyName())
            .RuleFor(a => a.name, f => f.Internet.Url())
            .RuleFor(a => a.country, f => fakeTvShowNetworkCountry.Generate());

        var fakeTvShowExternal = new Faker<SharedKernel.Contracts.TvMazeApi.Externals>()
            .RuleFor(a => a.tvrage, f => f.Random.Number(10000, 99999))
            .RuleFor(a => a.thetvdb, f => f.Random.Number(10000, 99999))
            .RuleFor(a => a.imdb, f => f.Random.String(11, 11));

        var fakeTvShowImage = new Faker<SharedKernel.Contracts.TvMazeApi.Image>()
            .RuleFor(a => a.medium, f => f.Internet.UrlRootedPath("jpg"))
            .RuleFor(a => a.original, f => f.Internet.UrlRootedPath("jpg"));

        var fakeTvShowLinksSelf = new Faker<SharedKernel.Contracts.TvMazeApi.Self>()
            .RuleFor(a => a.href, f => f.Internet.Url());

        var fakeTvShowLinksPreviousEpisode = new Faker<SharedKernel.Contracts.TvMazeApi.Previousepisode>()
            .RuleFor(a => a.href, f => f.Internet.Url());

        var fakeTvShowLinks = new Faker<SharedKernel.Contracts.TvMazeApi.Links>()
            .RuleFor(a => a.self, f => fakeTvShowLinksSelf.Generate())
            .RuleFor(a => a.previousepisode, f => fakeTvShowLinksPreviousEpisode.Generate());

        var fakeTvMazeShow = new Faker<TvMazeShow>()
            .RuleFor(a => a.id, f => isForRandomId ? f.Random.Number(1, 200) : id)
            .RuleFor(a => a.url, f => f.Internet.Url())
            .RuleFor(a => a.name, f => f.Name.FirstName())
            .RuleFor(a => a.type, f => f.Name.LastName())
            .RuleFor(a => a.language, f => f.Random.RandomLocale())
            .RuleFor(a => a.genres, f => new string[] { f.Music.Genre(), f.Music.Genre(), f.Music.Genre() })
            .RuleFor(a => a.status, f => f.Finance.TransactionType())
            .RuleFor(a => a.runtime, f => f.Random.Number(1, 200))
            .RuleFor(a => a.averageRuntime, f => f.Random.Number(1, 200))
            .RuleFor(a => a.premiered, f => f.Date.Past().ToString("yyyy-MM-dd"))
            .RuleFor(a => a.ended, f => f.Date.Past().ToString("yyyy-MM-dd"))
            .RuleFor(a => a.officialSite, f => f.Internet.Url())
            .RuleFor(a => a.schedule, f => fakeTvShowSchedule.Generate())
            .RuleFor(a => a.rating, f => fakeTvShowRating.Generate())
            .RuleFor(a => a.weight, f => f.Random.Number(1, 100))
            .RuleFor(a => a.network, f => fakeTvShowNetwork.Generate())
            .RuleFor(a => a.externals, f => fakeTvShowExternal.Generate())
            .RuleFor(a => a.image, f => fakeTvShowImage.Generate())
            .RuleFor(a => a.summary, f => f.Lorem.Sentences(3))
            .RuleFor(a => a.updated, f => f.Random.Number(1, 100))
            .RuleFor(a => a._links, f => fakeTvShowLinks.Generate());
        return fakeTvMazeShow.Generate();
    }
}