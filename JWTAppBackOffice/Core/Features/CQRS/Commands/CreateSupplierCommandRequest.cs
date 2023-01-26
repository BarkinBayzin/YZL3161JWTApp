using MediatR;

namespace JWTAppBackOffice.Core.Features.CQRS.Commands
{
    public class CreateSupplierCommandRequest:IRequest
    {
        public string CompanyName { get; set; }
        public decimal Freight { get; set; }
    }
}
