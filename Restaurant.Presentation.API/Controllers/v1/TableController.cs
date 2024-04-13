using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Dto.Orders;
using Restaurant.Core.Application.Dto.Tables;
using Restaurant.Core.Application.Interfaces.IServices;
using Restaurant.Core.Application.Services;

namespace Restaurant.Presentation.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    public class TableController : BaseApiController
    {

        private readonly ITableService _TableService;
        public TableController(ITableService TableService)
        {
            _TableService = TableService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Waiter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TablesBaseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var table = await _TableService.GetAllWithInclude();
                if (table == null || table.Count == 0)
                {
                    return NotFound();
                }
                return Ok(table);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("Id")]
        [Authorize(Roles = "Admin,Waiter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TablesBaseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetByid(int Id)
        {
            try
            {
                var table = await _TableService.GetByIdWithInclude(Id);
                if (table == null)
                {
                    return NotFound();
                }
                return Ok(table);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TableAddDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(TableAddDto Dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                await _TableService.Add(Dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        [HttpPatch("Id")]
        [Authorize(Roles = "Waiter")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(TableStatusDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> ChangeStatus(TableStatusDto Dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                await _TableService.ChangeTableStatus(Dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("Id")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TableUpdateDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Put(TableUpdateDto Dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var table = await _TableService.UpdateTableInfo(Dto);
                return Ok(table);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("Id_")]
        [Authorize(Roles = "Waiter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TableOrderDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetTableOrders(int Id)
        {
            try
            {
                var table = await _TableService.GetByIdWithInclude(Id);
                if (table == null)
                {
                    return NoContent();
                }
                var ordersundertable = await _TableService.GetOrderTablesById(Id);
                return Ok(ordersundertable);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
