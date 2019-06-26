using System.Collections.Generic;

namespace PetClinic.Vets.Dto
{
    public class GetSpecialtiesResponse
    {
        public List<VetSpecialtyDto> Specialties { get; set; } = new List<VetSpecialtyDto>();
    }
}