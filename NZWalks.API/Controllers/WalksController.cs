using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;


namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly NZWalksDbContext dbConext;
        private readonly IWalksRepository walksRepository;

        public WalksController(NZWalksDbContext dbContext,IWalksRepository walkRepository)
        {
            this.dbConext = dbContext;
            this.walksRepository = walkRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await walksRepository.GetAllAsync();

            var walksDTO = new List<WalkDTO>();
            foreach(var walkDomain in walksDomainModel)
            {
                walksDTO.Add(new WalkDTO()
                {
                    Id = walkDomain.Id,
                    Name = walkDomain.Name,
                    Description = walkDomain.Description,
                    LengthInKm=walkDomain.LengthInKm,
                    WalkImageUrl = walkDomain.WalkImageUrl,
                    DifficultyId = walkDomain.DifficultyId,
                    RegionId = walkDomain.RegionId,

                }); 
            }

            return Ok(walksDTO);
        }
    }
}
