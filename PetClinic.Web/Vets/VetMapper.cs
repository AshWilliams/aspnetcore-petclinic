using System.Linq;
using PetClinic.Common.Models.Vets;
using PetClinic.Vets.Dto;

namespace PetClinic.Vets
{
    public class VetMapper
    {
        public VetDto ToDto(Vet vet)
        {
            var dto = new VetDto
            {
                Uuid = vet.Uuid.ToString(),
                FirstName = vet.FirstName,
                LastName = vet.LastName,
                Specialties = vet.Specialties.Select(s => ToDto(s.Specialty)).ToList()
            };

            return dto;
        }

        public VetSpecialtyDto ToDto(Specialty specialty)
        {
            var dto = new VetSpecialtyDto
            {
                Uuid = (specialty?.Uuid).GetValueOrDefault(),
                Name = specialty?.Name
            };

            return dto;
        }
    }
}