using System.Linq;
using System.Threading.Tasks;
using Domain.Vets;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetClinic.Vets.Dto;

namespace PetClinic.Vets
{
    [Route("/api/vets")]
    [ApiController]
    public class VetsController: ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly VetMapper _vetMapper;

        public VetsController(IMediator mediator)
        {
            _mediator = mediator;
            _vetMapper = new VetMapper();
        }
        
        /// <summary>
        /// Gets all vets.
        /// </summary>
        /// <returns>An entity containing the list of vets.</returns>
        [HttpGet]
        public async Task<ActionResult<GetVetsResponse>> GetAllVets()
        {
            var vets = await _mediator.Send(new GetAllVetsQuery());
            var vetDtos = vets.Select(_vetMapper.ToDto).ToList();

            return Ok(new GetVetsResponse
            {
                Vets = vetDtos
            });
        }
    }
}