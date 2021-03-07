using FluentValidation;

namespace BluePrism.TechTest.Business.Commands.WriteFasterPathToFile
{
    public class WriteShortestPathToFileCommandValidator : AbstractValidator<WriteShortestPathToFileCommand>
    {
        private const int ExactLengthWords = 4;

        public WriteShortestPathToFileCommandValidator()
        {
            RuleFor(x => x.Start).NotEmpty().Length(ExactLengthWords);
            RuleFor(x => x.End).NotEmpty().Length(ExactLengthWords);
        }
    }
}
