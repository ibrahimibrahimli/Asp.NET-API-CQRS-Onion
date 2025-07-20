using Application.Features.Authentification.Commands.Login;
using Application.Features.Authentification.Commands.RefreshToken;
using Application.Features.Authentification.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitCircleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="request"></param>
        /// <returns>IActionResult</returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand request)
        {
           var result = await _mediator.Send(request);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="request"></param>
        /// <returns>IActionResult</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand request)
        {
            var result = await _mediator.Send(request);
            if(!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(result);
        }

        /// <summary>
        /// Refreshing Access token with Refresh token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand request)
        {
            var result = await _mediator.Send(request);
            if(!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        /// <summary>
        /// Test endpoint protected with [Authorize]
        /// </summary>
        /// <returns></returns>
        [HttpGet("test")]
        [Authorize]
        public IActionResult TestProtected()
        {
            return Ok("You are protected");
        }
    }
}
