using AutoMapper;
using JWTAppBackOffice.Core.Application.Interfaces;
using JWTAppBackOffice.Core.Domain;
using JWTAppBackOffice.Core.DTOs;
using JWTAppBackOffice.Core.Features.CQRS.Queries;
using MediatR;

namespace JWTAppBackOffice.Core.Features.CQRS.Handlers
{
    public class GetAllSuppliersQueryHandler : IRequestHandler<GetAllSuppliersQueryRequest, List<SupplierListDto>>
    {

        private readonly IRepository<Supplier> _repository;
        private readonly IMapper _mapper;

        public GetAllSuppliersQueryHandler(IRepository<Supplier> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<SupplierListDto>> Handle(GetAllSuppliersQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetAllAsync();
            return _mapper.Map<List<SupplierListDto>>(data);
        }
    }
}
