using Application.UoW;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValuesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
           var result = await _unitOfWork.GetReadRepository<Product>().GetAllAsync();
            return Ok(result);
        }
    }
}
