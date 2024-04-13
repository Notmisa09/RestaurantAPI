using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Dto.Account;
using Restaurant.Core.Application.Enum;
using Restaurant.Core.Application.Interfaces.IServices;

namespace Restaurant.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticationAsync(AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthetnticateAsync(request));
        }

        [HttpPost("registerAdmin")]
        public async Task<IActionResult> RegisterAdmin(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterUserAsync(request, origin, RolesEnum.Admin.ToString()));
        }

        [HttpPost("RegisterWaiter")]
        public async Task<IActionResult> RegisterWaiter(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterUserAsync(request, origin, RolesEnum.Waiter.ToString()));
        }
    }
}