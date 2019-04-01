using System;
using PetClinic.Common.Models.Pets;
using PetClinic.Common.Models.Vets;

namespace PetClinic.Common.Models.Visits
{
    public class Visit
    {
        public int Id { get; set; }
        public Guid Uuid { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }

        public int VetId { get; set; }
        public Vet AssignedVet { get; set; }

        public int PetId { get; set; }
        
        public Pet Pet { get; set; }
    }
}