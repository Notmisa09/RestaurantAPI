using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Dto.Plates;
using Restaurant.Core.Application.Interfaces.IServices;

namespace Restaurant.Presentation.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PlatesController : BaseApiController
    {
        private readonly IPlateService _plateservices;

        public PlatesController(IPlateService plateservice)
        {
                _plateservices = plateservice;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlateDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var plateslist = await _plateservices.GetAllWithIncludePlateInfo();
                if (plateslist == null || plateslist.Count == 0)
                {
                    return NotFound();
                }
                return Ok(plateslist);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("Id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlateDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                var plates = await _plateservices.GetByIdWithInfo(Id);
                if( plates == null)
                {
                    return NotFound();  
                }
                return Ok(plates);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PlatesAddDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(PlatesAddDto platesAddDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
               var result = await _plateservices.Add(platesAddDto);
                return CreatedAtAction(nameof(Get), new {id = result.Id}, result);
            }
            catch (Exception ex){ return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); }
        }


        [HttpPut("Id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlatesAddDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(PlatesAddDto platesAddDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                await _plateservices.UpdateWithIngridients(platesAddDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
