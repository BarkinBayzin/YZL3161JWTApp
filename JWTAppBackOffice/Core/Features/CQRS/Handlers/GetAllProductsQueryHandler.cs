using AutoMapper;
using JWTAppBackOffice.Core.Application.Interfaces;
using JWTAppBackOffice.Core.Domain;
using JWTAppBackOffice.Core.DTOs;
using JWTAppBackOffice.Core.Features.CQRS.Queries;
using MediatR;

namespace JWTAppBackOffice.Core.Features.CQRS.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, List<ProductListDto>>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductListDto>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            List<Product> data = await _repository.GetAllAsync();
            return _mapper.Map<List<ProductListDto>>(data);
        }
    }
}
