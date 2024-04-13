using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Dto.Orders;
using Restaurant.Core.Application.Interfaces.IServices;

namespace Restaurant.Presentation.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(Roles = "Waiter")]
    public class OrderController : BaseApiController
    {

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrdersDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Get()
        {
            try
            {
                var orders = await _orderService.GetAllWithInclude();
                if(orders == null)
                {
                    return NotFound();
                }
                return Ok(orders);
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); }
        }

        [HttpGet("Id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrdersDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                var orders = await _orderService.GetByIdWithInclude(Id);
                if (orders == null)
                {
                    return NotFound();
                }
                return Ok(orders);
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderAddDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Add(OrderAddDto Dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
               var result = await _orderService.Add(Dto);
                return CreatedAtAction(nameof(Get), new {id = result.Id}, result);
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(OrderAddDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                await _orderService.Remove(Id);
                return NoContent();
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); }
        }


        [HttpPut("Id")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(OrderPlatesUpdate))]
        [ProducesResponseType (StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Put(OrderPlatesUpdate vm)
        {
            try
            {
                await _orderService.UpdatePlateFromOrders(vm);
                return NoContent();
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); }
        }
    }
}
