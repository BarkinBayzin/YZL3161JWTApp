using AutoMapper;
using JWTAppBackOffice.Core.Application.Interfaces;
using JWTAppBackOffice.Core.Domain;
using JWTAppBackOffice.Core.DTOs;
using JWTAppBackOffice.Core.Features.CQRS.Queries;
using MediatR;

namespace JWTAppBackOffice.Core.Features.CQRS.Handlers
{
    public class GetSupplierQueryHandler : IRequestHandler<GetSupplierQueryRequest, SupplierListDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Supplier> _repository;

        public GetSupplierQueryHandler(IMapper mapper, IRepository<Supplier> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<SupplierListDto> Handle(GetSupplierQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetByFilterAsync(x => x.Id == request.Id);
            return _mapper.Map<SupplierListDto>(data);
        }
    }
}
