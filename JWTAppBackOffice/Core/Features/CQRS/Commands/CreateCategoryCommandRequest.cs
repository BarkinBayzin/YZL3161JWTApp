using MediatR;

namespace JWTAppBackOffice.Core.Features.CQRS.Commands
{
    public class CreateCategoryCommandRequest : IRequest
    {
        public string Definition { get; set; }
    }
}
