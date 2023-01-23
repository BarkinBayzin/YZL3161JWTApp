using JWTAppBackOffice.Core.Application.Interfaces;
using JWTAppBackOffice.Core.Domain;
using JWTAppBackOffice.Core.Features.CQRS.Commands;
using MediatR;

namespace JWTAppBackOffice.Core.Features.CQRS.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest>
    {
        private readonly IRepository<Category> _repository;

        public UpdateCategoryCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var updatedCategory = await _repository.GetByIdAsync(request.Id);

            if(updatedCategory != null)
            {
                updatedCategory.Definition = request.Definition;
                await _repository.UpdateAsync(updatedCategory);
            }

            return Unit.Value;
        }
    }
}
