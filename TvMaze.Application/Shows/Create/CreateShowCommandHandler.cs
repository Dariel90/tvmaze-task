using TvMaze.Application.Abstractions.Data;
using TvMaze.Application.Messaging;
using TvMaze.Domain.Shows;
using TvMaze.SharedKernel.Core;

namespace TvMaze.Application.Shows.Create;

internal sealed class CreateShowCommandHandler : ICommandHandler<CreateShowCommand, Guid>
{
    private readonly IShowRepository _showRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateShowCommandHandler(IShowRepository showRepository, IUnitOfWork unitOfWork)
    {
        _showRepository = showRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateShowCommand command, CancellationToken cancellationToken)
    {
        if (await _showRepository.IsAlreadyShowInDbAsync(command.MazeShow.id, cancellationToken))
        {
            return Result.Failure<Guid>(ShowErrors.AlreadyExists(command.MazeShow.id));
        }

        var show = Show.Create(Guid.NewGuid(), command.MazeShow);

        _showRepository.Insert(show);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return show.SysId;
    }
}