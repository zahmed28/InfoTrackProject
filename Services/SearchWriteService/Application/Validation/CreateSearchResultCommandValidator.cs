using FluentValidation;
using SearchWriteService.Application.Commands;

namespace SearchResultWriteService.Application.Validation
{
    public class CreateSearchResultCommandValidator : AbstractValidator<CreateSearchResultCommand>
    {
        public CreateSearchResultCommandValidator()
        {
            RuleFor(x => x.Query).NotEmpty().WithMessage("Query is required.");
            RuleFor(x => x.ResultURL).NotEmpty().WithMessage("Result URL is required.");           
        }
    }

}
