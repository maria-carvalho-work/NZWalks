﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            var walkDifficultiesDomain = await walkDifficultyRepository.GetAllAsync();

            // Convert Domain to DTOs
            var walkDifficultiesDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficultiesDomain);

            return Ok(walkDifficultiesDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyById")]
        public async Task<IActionResult> GetWalkDifficultyById(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.GetAsync(id);
            if (walkDifficulty == null)
            {
                return NotFound();
            }

            // Convert Domain to DTOs
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync(
            Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            // Validate Model

            //if (!ValidateAddWalkDifficultyAsync(addWalkDifficultyRequest))
            //{
            //    return BadRequest(ModelState);
            //}

            // Convert DTO to Domain model
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty
            {
                Code = addWalkDifficultyRequest.Code
            };

            // Call repository
            walkDifficultyDomain = await walkDifficultyRepository.AddAsync(walkDifficultyDomain);

            // Convert Domain to DTO
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            // Return response
            return CreatedAtAction(nameof(GetWalkDifficultyById),
                new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id,
            Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            //if (!ValidateUpdateWalkDifficultyAsync(updateWalkDifficultyRequest))
            //{
            //    return BadRequest(ModelState);
            //}

            // Convert DTO to Domiain Model
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty
            {
                Code = updateWalkDifficultyRequest.Code
            };

            // Call repository to update
            walkDifficultyDomain = await walkDifficultyRepository.UpdateAsync(id, walkDifficultyDomain);

            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }

            // Convert Domain to DTO
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            // Return response
            return Ok(walkDifficultyDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficulty(Guid id)
        {
            var walkDifficultyDomain = await walkDifficultyRepository.DeleteAsync(id);
            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }

            // Convert to DTO
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);
            return Ok(walkDifficultyDTO);
        }

    }

    #region Private methods

    //private bool ValidateAddWalkDifficultyAsync(Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
    //{
    //    if (addWalkDifficultyRequest == null)
    //    {
    //        ModelState.AddModelError(nameof(addWalkDifficultyRequest),
    //            $"Walk Difficulty data is required");

    //        return false;
    //    }

    //    if (string.IsNullOrWhiteSpace(addWalkDifficultyRequest.Code))
    //    {
    //        ModelState.AddModelError(nameof(addWalkDifficultyRequest.Code),
    //            $"{nameof(addWalkDifficultyRequest.Code)} cannot be null, empty or whitespace");
    //    }

    //    if (ModelState.ErrorCount > 0)
    //    {
    //        return false;
    //    }

    //    return true;
    //}

    //private bool ValidateUpdateWalkDifficultyAsync(Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
    //{
    //    if (updateWalkDifficultyRequest == null)
    //    {
    //        ModelState.AddModelError(nameof(updateWalkDifficultyRequest),
    //            $"Region data is required");

    //        return false;
    //    }

    //    if (string.IsNullOrWhiteSpace(updateWalkDifficultyRequest.Code))
    //    {
    //        ModelState.AddModelError(nameof(updateWalkDifficultyRequest.Code),
    //            $"{nameof(updateWalkDifficultyRequest.Code)} cannot be null, empty or whitespace");
    //    }

    //    if (ModelState.ErrorCount > 0)
    //    {
    //        return false;
    //    }

    //    return true;
    //}

    #endregion
}