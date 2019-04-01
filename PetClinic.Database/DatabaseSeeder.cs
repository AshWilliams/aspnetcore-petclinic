using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetClinic.Common.Models.Owners;
using PetClinic.Common.Models.Pets;
using PetClinic.Common.Models.Vets;
using PetClinic.Common.Models.Visits;

namespace PetClinic.Database
{
    public class DatabaseSeeder
    {
        private readonly ILogger _logger;

        private static readonly Guid RadiologyUuid = Guid.Parse("7954228a-df66-4559-a0d9-ec82d6899cea");
        private static readonly Guid SurgeryUuid = Guid.Parse("5299f683-b0b6-4f68-8393-4a3333c35b27");
        private static readonly Guid DentistryUuid = Guid.Parse("b747fbc8-f248-422f-83b2-49230d64b586");

        private static readonly Guid GeorgeFranklinUuid = Guid.Parse("99860d7b-55ba-4be1-8591-fee5f9e0aeb5");
        private static readonly Guid BettyDavisUuid = Guid.Parse("4ac3f4ff-4a88-4e4b-9948-3a01549210d7");
        private static readonly Guid EduardoRodriquezUuid = Guid.Parse("e5785902-e511-4bfc-a428-7f5b4a499c03");
        private static readonly Guid HaroldDavisUuid = Guid.Parse("ff03b89a-a3b4-42a9-976d-b5bc2bc3e460");
        private static readonly Guid PeterMcTavishUuid = Guid.Parse("bd08c18c-613b-4bf6-a88d-96fef4c05085");
        private static readonly Guid JeanColemanUuid = Guid.Parse("19d873e6-5120-4847-9ce8-e56cfc2582bd");
        private static readonly Guid JeffBlackUuid = Guid.Parse("15fde776-2ce2-48a9-9f22-41d84907a824");
        private static readonly Guid MariaEscobitoUuid = Guid.Parse("ff044d19-d238-4c34-952f-50fdf5fc1ffc");
        private static readonly Guid DavidSchroederUuid = Guid.Parse("2b15b19a-7f02-472d-ad54-c91f5dd62bfe");
        private static readonly Guid CarlosEstabanUuid = Guid.Parse("f66af27c-6fda-4f23-abb9-3f3940c5708d");

        private PetClinicDbContext DbContext { get; }
        private List<Specialty> Specialties { get; set; }
        private List<Vet> Vets { get; set; }
        private List<Owner> Owners { get; set; }
        private List<Pet> Pets { get; set; }

        public DatabaseSeeder(PetClinicDbContext dbContext, ILogger logger)
        {
            _logger = logger;
            DbContext = dbContext;
        }

        public void Seed()
        {
            _logger.LogInformation("Starting Database seeding");
            DbContext.Database.EnsureCreated();

            Specialties = SeedSpecialties();
            Vets = SeedVets();
            Owners = SeedOwners();
            Pets = SeedPets();

            DbContext.SaveChanges();
            _logger.LogInformation("Database seeding complete");
        }

        private List<Specialty> SeedSpecialties()
        {
            _logger.LogInformation("Seeding Specialties");
            var specialties = new List<Specialty>()
            {
                new Specialty()
                {
                    Name = "Radiology",
                    Uuid = RadiologyUuid,
                },
                new Specialty()
                {
                    Name = "Surgery",
                    Uuid = SurgeryUuid
                },
                new Specialty()
                {
                    Name = "Dentistry",
                    Uuid = DentistryUuid,
                }
            };

            var specUuids = specialties.Select(spec => spec.Uuid);
            var foundSpecialties = DbContext.Specialties.Where(s => specUuids.Contains(s.Uuid)).ToImmutableList();

            var fetchedSpecialities = specialties.Select(spec =>
            {
                var foundSpecialty = foundSpecialties.FirstOrDefault(fs => fs.Uuid == spec.Uuid);
                return foundSpecialty ?? spec;
            }).ToList();

            var toInsert = fetchedSpecialities.Where(spec => spec.Id == 0).ToImmutableList();
            if (toInsert.Any())
            {
                _logger.LogInformation($"Will insert {toInsert.Count} specialities");
                DbContext.AddRange(toInsert);
            }
            else
            {
                _logger.LogInformation("No specialities to insert");
            }

            return fetchedSpecialities;
        }

        private List<Vet> SeedVets()
        {
            _logger.LogInformation("Seeding Vets");

            var radiology = Specialties.First(s => s.Uuid == RadiologyUuid);
            var surgery = Specialties.First(s => s.Uuid == SurgeryUuid);
            var dentistry = Specialties.First(s => s.Uuid == DentistryUuid);

            var vets = new List<Vet>
            {
                new Vet
                {
                    Uuid = Guid.Parse("1de1964d-f3e9-44c1-867f-c664b0a19ef9"),
                    FirstName = "James",
                    LastName = "Carter",
                },
                new Vet
                {
                    Uuid = Guid.Parse("7514f6d3-6c86-46b9-a689-8cd25cddd51e"),
                    FirstName = "Helen",
                    LastName = "Leary",
                    Specialties = new List<VetSpecialty>
                    {
                        new VetSpecialty
                        {
                            Specialty = radiology,
                            SpecialtyId = radiology.Id
                        }
                    }
                },
                new Vet
                {
                    Uuid = Guid.Parse("5820115a-1fc6-4d0a-b84a-d10c8cf3fd43"),
                    FirstName = "Linda",
                    LastName = "Douglas",
                    Specialties = new List<VetSpecialty>
                    {
                        new VetSpecialty
                        {
                            Specialty = surgery,
                            SpecialtyId = surgery.Id,
                        },
                        new VetSpecialty
                        {
                            Specialty = dentistry,
                            SpecialtyId = dentistry.Id
                        }
                    }
                },
                new Vet
                {
                    Uuid = Guid.Parse("6ec6d4c8-30b7-4e95-830a-46f5f82ee122"),
                    FirstName = "Rafael",
                    LastName = "Ortega",
                    Specialties = new List<VetSpecialty>
                    {
                        new VetSpecialty
                        {
                            Specialty = surgery,
                            SpecialtyId = surgery.Id,
                        }
                    }
                },
                new Vet()
                {
                    Uuid = Guid.Parse("83456030-cdd4-4ebb-a7ed-30e47d9b3307"),
                    FirstName = "Henry",
                    LastName = "Stevens",
                    Specialties = new List<VetSpecialty>
                    {
                        new VetSpecialty
                        {
                            Specialty = radiology,
                            SpecialtyId = radiology.Id,
                        }
                    }
                },
                new Vet()
                {
                    Uuid = Guid.Parse("d8428840-577d-4693-9fcb-8ecb9d77e52f"),
                    FirstName = "Sharon",
                    LastName = "Jenkins"
                }
            };

            var vetUuids = vets.Select(v => v.Uuid);

            var foundVets = DbContext.Vets
                .Include(v => v.Specialties)
                .ThenInclude(s => s.Specialty)
                .Where(vet => vetUuids.Contains(vet.Uuid))
                .ToImmutableList();

            var fetchedVets = vets.Select(vet =>
            {
                var found = foundVets.FirstOrDefault(fv => fv.Uuid == vet.Uuid);
                return found ?? vet;
            }).ToList();

            var toInsert = fetchedVets.Where(v => v.Id == 0).ToImmutableList();
            if (toInsert.Any())
            {
                _logger.LogInformation($"Will insert {toInsert.Count} vets");
                DbContext.AddRange(toInsert);
            }
            else
            {
                _logger.LogInformation("No vets to insert");
            }

            return fetchedVets;
        }

        private List<Owner> SeedOwners()
        {
            _logger.LogInformation("Seeding Owners");
            var owners = new List<Owner>
            {
                new Owner
                {
                    Uuid = GeorgeFranklinUuid,
                    FirstName = "George",
                    LastName = "Franklin",
                    PhoneNumber = "5199349027",
                    Address = new Address
                    {
                        Line1 = "2200 Garafraxa St",
                        City = "Toronto",
                        State = "Ontario",
                        Country = "Canada",
                        PostalCode = "N0H2N0",
                    },
                },
                new Owner
                {
                    Uuid = BettyDavisUuid,
                    FirstName = "Betty",
                    LastName = "Davis",
                    PhoneNumber = "2506499244",
                    Address = new Address
                    {
                        Line1 = "2166 Carlson Road",
                        Line2 = "Unit 5",
                        City = "Toronto",
                        State = "Ontario",
                        Country = "Canada",
                        PostalCode = "V2L5E5"

                    }
                },
                new Owner
                {
                    Uuid = EduardoRodriquezUuid,
                    FirstName = "Eduardo",
                    LastName = "Rodriquez",
                    PhoneNumber = "6047983247",
                    Address = new Address
                    {
                        Line1 = "2801 Wellington Ave",
                        City = "Toronto",
                        State = "Ontario",
                        Country = "Canada",
                        PostalCode = "V2P2M1"
                    }
                },
                new Owner
                {
                    Uuid = HaroldDavisUuid,
                    FirstName = "Harold",
                    LastName = "Davis",
                    PhoneNumber = "5199794954",
                    Address = new Address
                    {
                        Line1 = "3959 Lauzon Parkway",
                        Line2 = "Unit 1",
                        City = "Toronto",
                        State = "Ontario",
                        Country = "Canada",
                        PostalCode = "N8N1L7"
                    }
                },
                new Owner
                {
                    Uuid = PeterMcTavishUuid,
                    FirstName = "Peter",
                    LastName = "McTavish",
                    PhoneNumber = "4186537697",
                    Address = new Address
                    {
                        Line1 = "574 Garneau St",
                        City = "Toronto",
                        State = "Ontario",
                        Country = "Canada",
                        PostalCode = "G1V3V5"
                    }
                },
                new Owner
                {
                    Uuid = JeanColemanUuid,
                    FirstName = "Jean",
                    LastName = "Coleman",
                    PhoneNumber = "8199628720",
                    Address = new Address
                    {
                        Line1 = "4346 De la Providence Avenue",
                        City = "Toronto",
                        State = "Ontario",
                        Country = "Canada",
                        PostalCode = "J8Y3Y5"
                    }

                },
                new Owner
                {
                    Uuid = JeffBlackUuid,
                    FirstName = "Jeff",
                    LastName = "Black",
                    PhoneNumber = "2508831371",
                    Address = new Address
                    {
                        Line1 = "1221 Blanshard",
                        City = "Toronto",
                        State = "Ontario",
                        Country = "Canada",
                        PostalCode = "V8W2H9"
                    }
                },
                new Owner
                {
                    Uuid = MariaEscobitoUuid,
                    FirstName = "Maria",
                    LastName = "Escobito",
                    PhoneNumber = "7059193919",
                    Address = new Address
                    {
                        Line1 = "3493 Paris St",
                        Line2 = "Unit 337",
                        City = "Toronto",
                        State = "Ontario",
                        Country = "Canada",
                        PostalCode = "P3E4K2"
                    }
                },
                new Owner
                {
                    Uuid = DavidSchroederUuid,
                    FirstName = "David",
                    LastName = "Schroeder",
                    PhoneNumber = "2502406207",
                    Address = new Address
                    {
                        Line1 = "2004 Roger Street",
                        City = "Toronto",
                        State = "Ontario",
                        Country = "Canada",
                        PostalCode = "V9T5H3"
                    }
                },
                new Owner
                {
                    Uuid = CarlosEstabanUuid,
                    FirstName = "Carlos",
                    LastName = "Estaban",
                    PhoneNumber = "6133425954",
                    Address = new Address
                    {
                        Line1 = "2711 Parkdale Ave",
                        City = "Toronto",
                        State = "Ontario",
                        Country = "Canada",
                        PostalCode = "K6V4X4"
                    }
                }
            };

            var ownerUuids = owners.Select(o => o.Uuid);
            var foundOwners = DbContext.Owners.Where(o => ownerUuids.Contains(o.Uuid)).ToImmutableList();

            var fetchedOwners = owners.Select(owner =>
            {
                var foundOwner = foundOwners.FirstOrDefault(fo => fo.Uuid == owner.Uuid);
                return foundOwner ?? owner;
            }).ToList();

            var toInsert = fetchedOwners.Where(o => o.Id == 0).ToImmutableList();
            if (toInsert.Any())
            {
                _logger.LogInformation($"Will insert {toInsert.Count} owners");
                DbContext.AddRange(toInsert);
            }
            else
            {
                _logger.LogInformation("No owner to insert");
            }

            return fetchedOwners;
        }

        private List<Pet> SeedPets()
        {
            _logger.LogInformation("Seeding Pets");
            
            var pets = new List<Pet>
            {
                new Pet
                {
                    Uuid = Guid.Parse("f71ba646-bd4f-4220-ad16-c54a54b5a2c4"),
                    Name = "Leo",
                    BirthDate = DateTime.Parse("2015-09-07T00:00:00Z"),
                    Owner = Owners.Find(o => o.Uuid == GeorgeFranklinUuid),
                    Type = PetType.Cat,
                    Visits = new List<Visit>
                    {
                        new Visit
                        {
                            Uuid = Guid.NewGuid(),
                            Date = DateTime.Parse("2015-09-25T13:00:00Z"),
                            AssignedVet = Vets[0],
                            Notes = "Rabies shot"
                        },
                        new Visit
                        {
                            Uuid = Guid.NewGuid(),
                            Date = DateTime.Parse("2016-09-25T14:30:00Z"),
                            AssignedVet = Vets[0],
                            Notes = "Neuter"
                        }
                    }
                },
                new Pet
                {
                    Uuid = Guid.Parse("f08bf54f-a429-49e4-8750-78bd00e8bb80"),
                    Name = "Basil",
                    BirthDate = DateTime.Parse("2017-01-22T00:00:00Z"),
                    Owner = Owners.Find(o => o.Uuid == BettyDavisUuid),
                    Type = PetType.Hamster,
                    Visits = new List<Visit>
                    {
                        new Visit
                        {
                            Uuid = Guid.NewGuid(),
                            Date = DateTime.Parse("2017-01-30T20:30:00Z"),
                            AssignedVet = Vets[1],
                            Notes = "Shots"
                        }
                    }
                },
                new Pet
                {
                    Uuid = Guid.Parse("6c0c6747-9b92-4ddc-ac35-32de088fef47"),
                    Name = "Rosy",
                    BirthDate = DateTime.Parse("2007-04-17T00:00:00Z"),
                    Owner = Owners.Find(o => o.Uuid == EduardoRodriquezUuid),
                    Type = PetType.Dog,
                    Visits = new List<Visit>
                    {
                        new Visit
                        {
                            Uuid = new Guid(),
                            Date = DateTime.Parse("2007-05-05T13:00:00Z"),
                            AssignedVet = Vets[5],
                            Notes = "Shots"
                        }
                    }
                },
                new Pet
                {
                    Uuid = Guid.Parse("11a22010-a624-43fa-aa0e-79f65b9c0d38"),
                    Name = "Jewel",
                    Type = PetType.Dog,
                    Owner = Owners.Find(o => o.Uuid == EduardoRodriquezUuid),
                    BirthDate = DateTime.Parse("2017-05-25T00:00:00Z"),
                },
                new Pet
                {
                    Uuid = Guid.Parse("5ab90d5d-fcd8-4090-bc87-eb256fc14eb1"),
                    Name = "Iggy",
                    Type = PetType.Lizard,
                    Owner = Owners.Find(o => o.Uuid == HaroldDavisUuid),
                    BirthDate = DateTime.Parse("2019-01-02T00:00:00Z")
                },
                new Pet
                {
                    Uuid = Guid.Parse("f8dab28f-1c86-4041-851e-491a0f2ba731"),
                    Name = "George",
                    Type = PetType.Snake,
                    BirthDate = DateTime.Parse("2018-12-14T00:00:00Z"),
                    Owner = Owners.Find(o => o.Uuid == PeterMcTavishUuid),
                },
                new Pet
                {
                    Uuid = Guid.Parse("bcc3632e-24a2-4e11-a25a-b2fa1acb0ff4"),
                    Name = "Samantha",
                    Type = PetType.Cat,
                    BirthDate = DateTime.Parse("2007-07-10T00:00:00Z"),
                    Owner = Owners.Find(o => o.Uuid == JeanColemanUuid)
                },
                new Pet
                {
                    Uuid = Guid.Parse("710038dd-9cc0-4a6a-ae5d-a7930db790b4"),
                    Name = "Max",
                    Type = PetType.Dog,
                    BirthDate = DateTime.Parse("2009-02-14T00:00:00Z"),
                    Owner = Owners.Find(o => o.Uuid == JeanColemanUuid),
                },
                new Pet
                {
                    Uuid = Guid.Parse("66dcf9be-d5f0-4939-9890-1038f96eec26"),
                    Name = "Lucky",
                    Type = PetType.Bird,
                    BirthDate = DateTime.Parse("2016-08-18T00:00:00Z"),
                    Owner = Owners.Find(o => o.Uuid == JeffBlackUuid)
                },
                new Pet
                {
                    Uuid = Guid.Parse("6feccb06-0b0a-4404-9c15-7459a34e93a3"),
                    Name = "Mulligan",
                    Type = PetType.Dog,
                    BirthDate = DateTime.Parse("2014-05-17T00:00:00Z"),
                    Owner = Owners.Find(o => o.Uuid == MariaEscobitoUuid)
                },
                new Pet
                {
                    Uuid = Guid.Parse("88905de4-f7f2-4fee-87d6-6340feeaeb35"),
                    Name = "Freddy",
                    Type = PetType.Bird,
                    BirthDate = DateTime.Parse("2016-11-27T00:00:00Z"),
                    Owner = Owners.Find(o => o.Uuid == DavidSchroederUuid)
                },
                new Pet
                {
                    Uuid = Guid.Parse("a3bfce82-bce2-4b07-9635-4d140e290323"),
                    Name = "Lucky",
                    Type = PetType.Dog,
                    BirthDate = DateTime.Parse("2012-12-12T00:00:00Z"),
                    Owner = Owners.Find(o => o.Uuid == CarlosEstabanUuid)
                },
                new Pet
                {
                    Uuid = Guid.Parse("e9bf1f05-5faa-4dbc-9c6d-7ec61978c888"),
                    Name = "Sly",
                    Type = PetType.Snake,
                    BirthDate = DateTime.Parse("2011-09-11T00:00:00Z"),
                    Owner = Owners.Find(o => o.Uuid == CarlosEstabanUuid)
                },
            };

            var petUuids = pets.Select(p => p.Uuid);
            var foundPets = DbContext.Pets.Where(p => petUuids.Contains(p.Uuid)).ToImmutableList();

            var fetchedPets = pets.Select(pet =>
            {
                var found = foundPets.FirstOrDefault(p => p.Uuid == pet.Uuid);
                return found ?? pet;
            }).ToList();

            var toInsert = fetchedPets.Where(p => p.Id == 0).ToImmutableList();
            if (toInsert.Any())
            {
                _logger.LogInformation($"Will insert {toInsert.Count} pets");
                DbContext.AddRange(toInsert);
            }
            else
            {
                _logger.LogInformation("No pets to insert");
            }

            return fetchedPets;
        }
    }
}