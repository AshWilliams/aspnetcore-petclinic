using System.Collections.Generic;

namespace PetClinic.Vets.Dto
{
    public class GetVetsResponse
    {
        public List<VetDto> Vets { get; set; } = new List<VetDto>();
    }
}