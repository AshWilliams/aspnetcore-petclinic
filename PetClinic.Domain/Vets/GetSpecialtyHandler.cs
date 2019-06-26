using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PetClinic.Common.Models.Vets;
using PetClinic.Database;

namespace Domain.Vets
{
    public class GetSpecialtyQuery : IRequest<Specialty>
    {
        public Guid Uuid { get; set; }
    }
    
    public class GetSpecialtyHandler: IRequestHandler<GetSpecialtyQuery, Specialty>
    {
        private readonly PetClinicDbContext _dbContext;

        public GetSpecialtyHandler(PetClinicDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public Task<Specialty> Handle(GetSpecialtyQuery request, CancellationToken cancellationToken)
        {
            return _dbContext.Specialties.FirstOrDefaultAsync(s => s.Uuid == request.Uuid, cancellationToken);
        }
    }
}