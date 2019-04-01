namespace PetClinic.Common.Models.Vets
{
    public class VetSpecialty
    {
        public int VetId { get; set; }
        public Vet Vet { get; set; }

        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
    }
}