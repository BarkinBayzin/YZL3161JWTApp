using AutoMapper;
using JWTAppBackOffice.Core.Application.Interfaces;
using JWTAppBackOffice.Core.Domain;
using JWTAppBackOffice.Core.DTOs;
using JWTAppBackOffice.Core.Features.CQRS.Queries;
using MediatR;

namespace JWTAppBackOffice.Core.Features.CQRS.Handlers
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQueryRequest, List<CategoryListDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Category> _repository;

        public GetAllCategoriesQueryHandler(IMapper mapper, IRepository<Category> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<CategoryListDto>> Handle(GetAllCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<CategoryListDto>>(await _repository.GetAllAsync());
        }
    }
}
