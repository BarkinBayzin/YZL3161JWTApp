using JWTAppBackOffice.Core.DTOs;
using JWTAppBackOffice.Core.Features.CQRS.Commands;
using JWTAppBackOffice.Core.Features.CQRS.Queries;
using JWTAppBackOffice.Infrastructure.Tools;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTAppBackOffice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /*
            1 - User Register => mebmer rolü ile beraber register edilecek.
            2 - Username ve password doğruysa token döneceğim.
         */

        // api/Auth/Register
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterUserCommandRequest request)
        {
            await _mediator.Send(request);
            return Created("",request);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SignIn(CheckUserQueryRequest request)
        {
            CheckUserResponseDto userDto = await _mediator.Send(request);
            if (userDto != null && userDto.IsExists)
            {
                // kullanıcım var, artık token yaratmalıyım.

                var token = JwtGenerator.GenerateToken(userDto);

                return Created("",token);
            }

            return BadRequest("Username or password is invalid..!");
        }
    }
}
