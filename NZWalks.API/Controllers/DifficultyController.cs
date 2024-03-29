using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IDifficultyRepository difficultyRepository;
        private readonly IMapper mapper;

        public DifficultyController(NZWalksDbContext dbContext, IDifficultyRepository difficultyRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.difficultyRepository = difficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var difficultyDomain = await difficultyRepository.GetAllAsync();

            return Ok(mapper.Map<List<DifficultyDTO>>(difficultyDomain));
        }
        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid Id)
        {
            var domain=await difficultyRepository.GetByIdAsync(Id);

            if (domain == null) return NotFound();

            return Ok(mapper.Map<DifficultyDTO>(domain));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddDifficultyRequestDTO addDifficultyRequestDTO)
        {
            var domain = mapper.Map<Difficulty>(addDifficultyRequestDTO);

            domain = await difficultyRepository.CreateAsync(domain);

            var dto = mapper.Map<DifficultyDTO>(domain);

          return CreatedAtAction(nameof(GetById),new {Id=domain.Id},dto);
        }

        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid Id,[FromBody]UpdateDifficultyRequestDTO updateDifficultyRequestDTO)
        {
            var domain = mapper.Map<Difficulty>(updateDifficultyRequestDTO);
            domain = await difficultyRepository.UpdateAsync(Id, domain);

            if (domain == null) return NotFound();

            return Ok(mapper.Map<DifficultyDTO>(domain));
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var domain = await difficultyRepository.DeleteAsync(Id);
            if (domain == null) return NotFound();

            return Ok(mapper.Map<DifficultyDTO>(domain));
        }
    }
}
