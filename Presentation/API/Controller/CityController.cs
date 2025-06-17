using Application.Features.Cities.Commands.Create;
using Application.Features.Cities.Dtos;
using Application.Features.Cities.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitCircleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        public readonly IMediator _mediatr;

        public CityController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateCityDto dto)
        {
            var commandRequest = new CreateCityCommandRequest(dto);
            var city = await _mediatr.Send(commandRequest);
            return CreatedAtAction(nameof(Create), city);
        }

        [HttpGet]

        public async Task<IActionResult> GetAllCities()
        {
            var cities = _mediatr.Send(new GetAllCitiesQueryRequest()); 
            if(cities != null)
            {
                return Ok(cities);
            }
            return BadRequest();
        }
    }
}
