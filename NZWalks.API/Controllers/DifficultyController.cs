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

        public DifficultyController(NZWalksDbContext dbContext,IDifficultyRepository difficultyRepository,IMapper mapper)
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
    }
}
