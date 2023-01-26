using JWTAppBackOffice.Core.DTOs;
using MediatR;

namespace JWTAppBackOffice.Core.Features.CQRS.Queries
{
    public class GetSupplierQueryRequest:IRequest<SupplierListDto>
    {
        public int Id { get; set; }

        public GetSupplierQueryRequest(int id)
        {
            Id = id;
        }
    }
}
