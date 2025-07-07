using Application.Features.Cities.Commands.Create;
using Application.Features.Cities.Commands.Delete;
using Application.Features.Cities.Dtos;
using Application.Features.Cities.Queries;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public CityController(IMediator mediatr, IMapper mapper)
        {
            _mediatr = mediatr;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateCityDto dto)
        {
            var commandRequest = _mapper.Map<CreateCityCommandRequest>(dto);
            var result = await _mediatr.Send(commandRequest);
            if (!result)
                return BadRequest(result);

            return CreatedAtAction(nameof(Create), result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            var cities = _mediatr.Send(new GetAllCitiesQueryRequest());
            if (cities != null)
            {
                return Ok(cities);
            }
            return BadRequest();
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCity(DeleteCityCommandRequest request)
        {
            var result = await _mediatr.Send(request);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
    }
}
