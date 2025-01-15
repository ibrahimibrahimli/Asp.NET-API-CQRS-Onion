using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandController : ControllerBase
    {

        private readonly IMediator mediator;

        public BrandController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
         
        public async Task<IActionResult> CreateBrand(CreateBrandCommandRequest request)
        {
           await mediator.Send(request);
            return Ok();
        }

        [HttpGet]

        public async Task<IActionResult> GetAllBrands()
        {
            var response = await mediator.Send(new GetAllBrandsQueryRequest());
            return Ok(response);    
        }
    }
}
