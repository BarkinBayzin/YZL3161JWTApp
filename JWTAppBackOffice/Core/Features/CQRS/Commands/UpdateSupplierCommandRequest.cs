using MediatR;

namespace JWTAppBackOffice.Core.Features.CQRS.Commands
{
    public class UpdateSupplierCommandRequest:IRequest
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public decimal Freight { get; set; }
    }
}
