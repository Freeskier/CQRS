using Application.Functions.Comment.Queries;
using FluentValidation;

namespace Application.Functions.Comment.Validation
{
    public class GetCommentsForPhotoQueryValidation : AbstractValidator<GetCommentsForPhotoQuery>
    {
        public GetCommentsForPhotoQueryValidation()
        {
            RuleFor(x => x.Photo).NotNull();
            RuleFor(x => x.Photo.Id).NotEmpty();
        }
    }
}