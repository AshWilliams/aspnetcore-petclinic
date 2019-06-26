using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Vets;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetClinic.Vets.Dto;

namespace PetClinic.Vets
{
    [Route("/api/vets/specialties")]
    [ApiController]
    public class SpecialtyController: ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly VetMapper _mapper;

        public SpecialtyController(IMediator mediator)
        {
            _mediator = mediator;
            _mapper = new VetMapper();
        }
        
        [HttpGet("")]
        public async Task<ActionResult<GetSpecialtiesResponse>> GetSpecialties()
        {
            var specialties = await _mediator.Send(new GetSpecialtiesQuery());
            var specialtyDtos = specialties.Select(_mapper.ToDto).ToList();

            return Ok(specialtyDtos);
        }

        [HttpGet("{uuid}")]
        public async Task<ActionResult<VetSpecialtyDto>> GetSpecialty(string uuid)
        {
            var success = Guid.TryParse(uuid, out var result);

            if (!success)
            {
                return BadRequest("Invalid UUID was passed");
            }

            var specialty = await _mediator.Send(new GetSpecialtyQuery { Uuid = result });
            var dto = _mapper.ToDto(specialty);

            return Ok(dto);
        }

        [HttpPost("")]
        public async Task<ActionResult<VetSpecialtyDto>> Create([FromBody] CreateSpecialtyRequest request)
        {
            var command = new CreateSpecialtyCommand
            {
                Name = request.Name
            };

            var specialty = await _mediator.Send(command);
            var dto = _mapper.ToDto(specialty);

            return Created("", dto);
        }
    }
}