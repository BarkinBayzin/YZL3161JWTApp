using JWTAppBackOffice.Core.DTOs;
using MediatR;

namespace JWTAppBackOffice.Core.Features.CQRS.Queries
{
    public class GetAllSuppliersQueryRequest:IRequest<List<SupplierListDto>>
    {
    }
}
