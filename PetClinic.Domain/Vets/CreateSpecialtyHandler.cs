using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PetClinic.Common.Models.Vets;
using PetClinic.Database;

namespace Domain.Vets
{
    public class CreateSpecialtyCommand : IRequest<Specialty>
    {
        public string Name { get; set; }
    }
    
    public class CreateSpecialtyHandler: IRequestHandler<CreateSpecialtyCommand, Specialty>
    {
        private readonly PetClinicDbContext _dbContext;

        public CreateSpecialtyHandler(PetClinicDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Specialty> Handle(CreateSpecialtyCommand request, CancellationToken cancellationToken)
        {
            var uuid = Guid.NewGuid();
            var entity = new Specialty
            {
                Uuid = uuid,
                Name = request.Name
            };

            await _dbContext.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}