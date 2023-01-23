using JWTAppBackOffice.Core.DTOs;
using MediatR;

namespace JWTAppBackOffice.Core.Features.CQRS.Queries
{
    public class GetAllCategoriesQueryRequest:IRequest<List<CategoryListDto>>
    {
    }
}
