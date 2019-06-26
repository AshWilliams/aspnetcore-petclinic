using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PetClinic.Common.Models.Vets;
using PetClinic.Database;

namespace Domain.Vets
{
    public class GetAllVetsQuery: IRequest<List<Vet>> {}
    
    public class GetVetsHandler: IRequestHandler<GetAllVetsQuery, List<Vet>>
    {
        private readonly PetClinicDbContext _dbContext;
        
        public GetVetsHandler(PetClinicDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public Task<List<Vet>> Handle(GetAllVetsQuery request, CancellationToken cancellationToken)
        {
            return _dbContext.Vets
                .Include(vet => vet.Specialties)
                .ThenInclude(vetSpec => vetSpec.Specialty)
                .ToListAsync(cancellationToken);
        }
    }
}