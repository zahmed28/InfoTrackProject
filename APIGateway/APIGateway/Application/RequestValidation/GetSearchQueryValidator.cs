using APIGateway.Application.Queries;
using FluentValidation;
namespace APIGateway.Application.RequestValidation
{
    public class GetSearchQueryValidator : AbstractValidator<GetSearchQuery>
    {
        public GetSearchQueryValidator()
        {
            RuleFor(x => x.Keyword).NotEmpty().WithMessage("Keyword is required");
            RuleFor(x => x.URL).NotEmpty().WithMessage("URL is required");           
        }
    }
}
