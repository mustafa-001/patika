using FluentValidation  ;
namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetails
{
    public class GetAuthorDetailsQueryValidator : AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailsQueryValidator()
        {
            RuleFor(query=> query.AuthorId).GreaterThan(0);
        }
    }
}