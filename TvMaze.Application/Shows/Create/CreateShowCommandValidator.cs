using FluentValidation;

namespace TvMaze.Application.Shows.Create;

internal class CreateShowCommandValidator : AbstractValidator<CreateShowCommand>
{
    public CreateShowCommandValidator()
    {
        RuleFor(x => x.MazeShow).NotEmpty();
    }
}