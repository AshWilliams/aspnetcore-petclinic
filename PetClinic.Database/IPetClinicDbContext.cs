using Microsoft.EntityFrameworkCore;
using PetClinic.Common.Models.Owners;
using PetClinic.Common.Models.Pets;
using PetClinic.Common.Models.Vets;
using PetClinic.Common.Models.Visits;

namespace PetClinic.Database
{
    public interface IPetClinicDbContext
    {
        DbSet<Specialty> Specialties { get; }
        DbSet<Vet> Vets { get; }
        DbSet<Owner> Owners { get; }
        DbSet<Pet> Pets { get; }
        DbSet<Visit> Visits { get; }
    }
}