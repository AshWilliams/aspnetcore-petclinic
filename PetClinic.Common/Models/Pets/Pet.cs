using System;
using System.Collections.Generic;
using PetClinic.Common.Models.Owners;
using PetClinic.Common.Models.Visits;

namespace PetClinic.Common.Models.Pets
{
    public class Pet
    {
        public int Id { get; set; }
        public Guid Uuid { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public PetType Type { get; set; }

        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
        
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
    }
}