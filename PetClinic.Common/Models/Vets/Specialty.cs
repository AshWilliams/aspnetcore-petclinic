using System;
using System.Collections.Generic;

namespace PetClinic.Common.Models.Vets
{
    public class Specialty
    {
        public int Id { get; set; }
        public Guid Uuid { get; set; }
        public string Name { get; set; }

        public ICollection<VetSpecialty> VetSpecialties { get; set; }
    }
}