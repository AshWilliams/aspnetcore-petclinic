using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PetClinic.Common.Models.Vets;
using PetClinic.Database;

namespace Domain.Vets
{
    public class GetSpecialtiesQuery: IRequest<List<Specialty>> 
    {
    }
    
    public class GetSpecialitiesHandler: IRequestHandler<GetSpecialtiesQuery, List<Specialty>>
    {
        private readonly PetClinicDbContext _dbContext;

        public GetSpecialitiesHandler(PetClinicDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public Task<List<Specialty>> Handle(GetSpecialtiesQuery request, CancellationToken cancellationToken)
        {
            return _dbContext.Specialties.ToListAsync(cancellationToken);
        }
    }
}