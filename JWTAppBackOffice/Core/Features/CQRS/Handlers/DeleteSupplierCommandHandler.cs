using JWTAppBackOffice.Core.Application.Interfaces;
using JWTAppBackOffice.Core.Domain;
using JWTAppBackOffice.Core.Features.CQRS.Commands;
using MediatR;

namespace JWTAppBackOffice.Core.Features.CQRS.Handlers
{
    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommandRequest>
    {
        private readonly IRepository<Supplier> _repository;

        public DeleteSupplierCommandHandler(IRepository<Supplier> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteSupplierCommandRequest request, CancellationToken cancellationToken)
        {
            var deletedEntity = await _repository.GetByIdAsync(request.Id);
            if (deletedEntity != null) await _repository.RemoveAsync(deletedEntity);
            return Unit.Value;
        }
    }
}
