using JWTAppBackOffice.Core.Application.Interfaces;
using JWTAppBackOffice.Core.Domain;
using JWTAppBackOffice.Core.Features.CQRS.Commands;
using MediatR;

namespace JWTAppBackOffice.Core.Features.CQRS.Handlers
{
    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommandRequest>
    {
        private readonly IRepository<Supplier> _repository;

        public UpdateSupplierCommandHandler(IRepository<Supplier> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateSupplierCommandRequest request, CancellationToken cancellationToken)
        {
            Supplier updatedSupplier = await _repository.GetByIdAsync(request.Id);
            if (updatedSupplier != null)
            {
                updatedSupplier.CompanyName = request.CompanyName;
                updatedSupplier.Freight = request.Freight;
                await _repository.UpdateAsync(updatedSupplier);
            }
            return Unit.Value;
        }
    }
}
