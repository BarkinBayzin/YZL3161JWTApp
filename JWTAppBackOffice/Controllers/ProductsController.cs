using JWTAppBackOffice.Core.Features.CQRS.Commands;
using JWTAppBackOffice.Core.Features.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTAppBackOffice.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await _mediator.Send(new GetAllProductsQueryRequest()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1) return NotFound();
            var result = await _mediator.Send(new GetProductQueryRequest(id));
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0) return NotFound();
            var result = await _mediator.Send(new DeleteProductCommandRequest(id));
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommandRequest request)
        {
            await _mediator.Send(request);
            return Created("", request);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
