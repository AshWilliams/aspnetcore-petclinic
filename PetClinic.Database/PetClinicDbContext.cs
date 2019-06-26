using System.Transactions;
using Microsoft.EntityFrameworkCore;
using PetClinic.Common.Models.Owners;
using PetClinic.Common.Models.Pets;
using PetClinic.Common.Models.Vets;
using PetClinic.Common.Models.Visits;

namespace PetClinic.Database
{
    public class PetClinicDbContext: DbContext
    {
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Vet> Vets { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Visit> Visits { get; set; }

        public PetClinicDbContext()
        {
            
        }

        public PetClinicDbContext(DbContextOptions<PetClinicDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetupSpecialities(modelBuilder);
            SetupVets(modelBuilder);
            SetupVetSpecialities(modelBuilder);
            SetupOwners(modelBuilder);
            SetupPet(modelBuilder);
            SetupVisits(modelBuilder);
        }

        private void SetupSpecialities(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<Specialty>();

            builder.HasKey(s => s.Id);
            builder.HasIndex(s => s.Uuid).IsUnique();
            
            builder.Property(s => s.Uuid)
                .IsRequired();
            builder.Property(s => s.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
        
        private void SetupVets(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<Vet>();

            builder.HasKey(v => v.Id);
            builder.HasIndex(v => v.Uuid).IsUnique();

            builder.Property(v => v.Uuid)
                .IsRequired();
            
            builder.Property(v => v.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(v => v.LastName)
                .HasMaxLength(50)
                .IsRequired();
        }

        private void SetupVetSpecialities(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<VetSpecialty>();

            builder.HasKey(vs => new {vs.VetId, SpecialityId = vs.SpecialtyId});

            builder.HasOne(vs => vs.Vet)
                .WithMany(v => v.Specialties)
                .HasForeignKey(vs => vs.VetId);

            builder.HasOne(vs => vs.Specialty)
                .WithMany(s => s.VetSpecialties)
                .HasForeignKey(vs => vs.SpecialtyId);
        }

        private void SetupOwners(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<Owner>();

            builder.HasKey(o => o.Id);
            builder.HasIndex(o => o.Uuid).IsUnique();

            builder.Property(o => o.Uuid)
                .IsRequired();
            builder.Property(o => o.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(o => o.LastName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(o => o.PhoneNumber)
                .IsRequired()
                .HasMaxLength(10);
            
            var addressBuilder = builder.OwnsOne(o => o.Address);
            addressBuilder.Property(a => a.Line1)
                .HasMaxLength(100)
                .IsRequired();
            addressBuilder.Property(a => a.Line2)
                .HasMaxLength(100);
            addressBuilder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(100);
            addressBuilder.Property(a => a.State)
                .IsRequired()
                .HasMaxLength(100);
            addressBuilder.Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(100);
            addressBuilder.Property(a => a.PostalCode)
                .IsRequired()
                .HasMaxLength(6);
        }
        
        private void SetupPet(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<Pet>();

            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Uuid).IsUnique();

            builder.Property(p => p.Uuid)
                .IsRequired();
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.Type)
                .IsRequired();
            builder.Property(p => p.BirthDate)
                .IsRequired();
            
            builder.HasOne(p => p.Owner)
                .WithMany(o => o.Pets)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(p => p.OwnerId);
        }

        private void SetupVisits(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<Visit>();

            builder.HasKey(v => v.Id);
            builder.HasIndex(v => v.Uuid).IsUnique();

            builder.Property(v => v.Uuid)
                .IsRequired();
            
            builder.Property(v => v.Date)
                .IsRequired();

            builder.Property(v => v.Notes)
                .IsUnicode();

            builder.HasOne(v => v.AssignedVet)
                .WithMany(vet => vet.Visits)
                .IsRequired()
                .HasForeignKey(v => v.VetId);

            builder.HasOne(v => v.Pet)
                .WithMany(p => p.Visits)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(v => v.PetId);
        }
    }
}