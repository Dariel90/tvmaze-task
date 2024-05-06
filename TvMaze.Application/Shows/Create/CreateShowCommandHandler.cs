using TvMaze.Application.Abstractions.Data;
using TvMaze.Application.Messaging;
using TvMaze.Domain.Shows;
using TvMaze.SharedKernel.Core;

namespace TvMaze.Application.Shows.Create;

internal sealed class CreateShowCommandHandler(IShowRepository showRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateShowCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateShowCommand command, CancellationToken cancellationToken)
    {
        if (await showRepository.IsAlreadyShowInDbAsync(command.MazeShow.id, cancellationToken))
        {
            return Result.Failure<Guid>(ShowErrors.AlreadyExists(command.MazeShow.id));
        }

        var show = Show.Create(Guid.NewGuid(), command.MazeShow);

        showRepository.Insert(show);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return show.SysId;
    }
}