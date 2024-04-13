using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Dto.Ingridients;
using Restaurant.Core.Application.Interfaces.IServices;

namespace Restaurant.Presentation.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(Roles= "Admin")]
    public class IngridientController : BaseApiController
    {
        private readonly IingridientsService _ingridientService;

        public IngridientController(IingridientsService ingridientService)
        {
            _ingridientService = ingridientService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IngridientsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var ingridientslist = await _ingridientService.GetAll();
                if (ingridientslist == null || ingridientslist.Count == 0)
                {
                    return NotFound();
                }
                return Ok(ingridientslist);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("Id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IngridientsAddDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                var ingridient = await _ingridientService.GetById(Id);
                if (ingridient == null)
                {
                    return NotFound();
                }
                return Ok(ingridient);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public async Task<IActionResult> Post(IngridientsAddDto ingridient)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var result =  await _ingridientService.Add(ingridient);
                return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); }
        }

        [HttpPut("Id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IngridientsAddDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public async Task<IActionResult> Put(int Id, IngridientsAddDto ingridient)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                await _ingridientService.Update(ingridient, Id);
                return Ok(ingridient);
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); }

        }

        [HttpDelete("Id")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(IngridientsAddDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                await _ingridientService.Remove(Id);
                return NoContent();
            }
            catch (Exception ex) 
            { 
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); 
            }
        }
    }
}
