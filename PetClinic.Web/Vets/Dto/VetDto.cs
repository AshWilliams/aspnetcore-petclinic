using System.Collections.Generic;

namespace PetClinic.Vets.Dto
{
    public class VetDto
    {
        public string Uuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<VetSpecialtyDto> Specialties { get; set; } = new List<VetSpecialtyDto>();
    }
}