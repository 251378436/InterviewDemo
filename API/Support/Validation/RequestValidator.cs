using API.Contracts.Client;
using FluentValidation;

namespace API.Support.Validation
{
    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(model => model.FirstName).NotEmpty().MaximumLength(10);
            RuleFor(model => model.LastName).NotEmpty().MaximumLength(10);
        }
    }
}
