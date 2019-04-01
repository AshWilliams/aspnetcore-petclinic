using System;
using System.Collections.Generic;
using PetClinic.Common.Models.Visits;

namespace PetClinic.Common.Models.Vets
{
    public class Vet
    {
        public int Id { get; set; }
        public Guid Uuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public ICollection<VetSpecialty> Specialties { get; set; } = new List<VetSpecialty>();
        public ICollection<Visit> Visits { get; set; }
    }
}