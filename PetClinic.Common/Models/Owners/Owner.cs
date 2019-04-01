using System;
using System.Collections.Generic;
using PetClinic.Common.Models.Pets;

namespace PetClinic.Common.Models.Owners
{
    public class Owner
    {
        public int Id { get; set; }
        public Guid Uuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Pet> Pets { get; set; } = new List<Pet>();
    }
}