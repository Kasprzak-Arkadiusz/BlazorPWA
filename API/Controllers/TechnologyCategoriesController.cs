using Application.Commands.TechnologyCategory;
using Application.Common.Interfaces.Services;
using Application.Common.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class TechnologyCategoriesController : BaseApiController
    {
        private readonly ITechnologyCategoryService _technologyCategoryService;

        public TechnologyCategoriesController(ITechnologyCategoryService technologyCategoryService)
        {
            _technologyCategoryService = technologyCategoryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _technologyCategoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateTechnologyCategory createTechnologyCategory)
        {
            try
            {
                var id = await _technologyCategoryService.AddAsync(createTechnologyCategory);
                return Ok(CreateResponse.Success(id));
            }
            catch (Exception e)
            {
                return BadRequest(CreateResponse.Error(e.Message));
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateTechnologyCategory updateTechnologyCategory)
        {
            try
            {
                await _technologyCategoryService.UpdateAsync(updateTechnologyCategory);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _technologyCategoryService.DeleteAsync(id);
                if (!result)
                    return NotFound();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}