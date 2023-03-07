using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper) 
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            var walks = await walkRepository.GetAllAsync();

            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walks);

            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]

        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walk = await walkRepository.GetAsync(id);

            if (walk == null)
            {
                return NotFound();
            }

            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);


            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddwalkAsync(Models.DTO.AddWalkRequest addWalkRequest)
        {
            // Request to Domain Model

            var walk = new Models.Domain.Walk()
            {
                
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
                
            };

            //Pass Details to Repository

            walk = await walkRepository.AddAsync(walk);

            // Convert back to DTO

            var walkDTO = new Models.DTO.Walk()
            {
                Id = walk.Id,
              
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId
            };

            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO );
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeletewalkAsync(Guid id)
        {
            // Delete walk from database

            var walk = await walkRepository.DeleteAsync(id);

            // if null NotFound

            if (walk == null)
            {
                return NotFound();
            }

            // Convert response to DTO

            var walkDTO = new Models.DTO.Walk()
            {
                Id = walk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId
            };


            // return Ok response

            return Ok(walkDTO);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAstync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            // Convert DTO to Domain Model

            var walk = new Models.Domain.Walk()
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };

            // Update walk using Repository

            walk = await walkRepository.UpdateAsync(id, walk);

            // If Null then NotFound

            if (walk == null)
            {
                return NotFound();
            }

            // Convert Domain back to DTO

            var walkDTO = new Models.DTO.Walk()
            {
                Id = walk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId
            };

            return Ok(walkDTO);
        }

    }
}
