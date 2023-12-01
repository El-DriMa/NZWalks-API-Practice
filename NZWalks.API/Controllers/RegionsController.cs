using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Data.Common;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        public  async Task<IActionResult> GetAll()
        {
            //domain models
            var regionsDomain = await regionRepository.GetAllAsync();

            //map domain models to DTOs
            var regionDTOs = new List<RegionDTO>();
            foreach (var regionDomain in regionsDomain)
            {
                regionDTOs.Add(new RegionDTO()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    ImageUrl = regionDomain.ImageUrl
                });
            }

            //return DTOs
            return Ok(regionDTOs);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            var regionDomain = await regionRepository.GetByIdAsync(id);

            //Find() only for id

            if (regionDomain == null)
                return NotFound();

            //map/convert domain model to DTO

            var regionDTO = new RegionDTO()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                ImageUrl = regionDomain.ImageUrl
            };

            return Ok(regionDTO);
        }

        //POST to create new region
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            //convert DTO to domain model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                ImageUrl = addRegionRequestDTO.ImageUrl
            };

            //use domain model to create region

             regionDomainModel= await regionRepository.CreateAsync(regionDomainModel);
           
            //map domain model to DTO
            var regionDTO = new RegionDTO()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                ImageUrl = regionDomainModel.ImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { Id = regionDTO.Id }, regionDTO);
        }

        //Update region
        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            var regionDomainModel = new Region
            {
                Code = updateRegionRequestDTO.Code,
                Name = updateRegionRequestDTO.Name,
                ImageUrl = updateRegionRequestDTO.ImageUrl
            };

            regionDomainModel=await regionRepository.UpdateAsync(Id,regionDomainModel);

            if (regionDomainModel == null)
                return NotFound();

            regionDomainModel.Code = updateRegionRequestDTO.Code;
            regionDomainModel.Name = updateRegionRequestDTO.Name;
            regionDomainModel.ImageUrl = updateRegionRequestDTO.ImageUrl;

            await dbContext.SaveChangesAsync();

            var regionDTO = new Region
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                ImageUrl = regionDomainModel.ImageUrl
            };
            //always pass dto to client
            return Ok(regionDTO);
        }

        [HttpDelete]
        [Route("{Id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(Id);
            if (regionDomainModel == null)
                return NotFound();

            return Ok();
        }
    }
}
