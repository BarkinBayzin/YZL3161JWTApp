using JWTAppBackOffice.Core.Application.Interfaces;
using JWTAppBackOffice.Core.Domain;
using JWTAppBackOffice.Core.Features.CQRS.Commands;
using MediatR;

namespace JWTAppBackOffice.Core.Features.CQRS.Handlers
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommandRequest>
    {
        private readonly IRepository<Supplier> _repository;

        public CreateSupplierCommandHandler(IRepository<Supplier> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateSupplierCommandRequest request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new Supplier
            {
                CompanyName = request.CompanyName,
                Freight = request.Freight
            });
            return Unit.Value;
        }
    }
}
