using MediatR;
using Asp.Versioning;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using vsa_journey.Application.Responses;

namespace vsa_journey.Features.Products.Controller;

[ApiVersion(1)]
[ApiController]
[Route("v{v:apiVersion}/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductController> _logger;
    private readonly IApiResponse _apiResponse;


    public ProductController(IMediator mediator, ILogger<ProductController> logger, IApiResponse apiResponse)
    {
        _mediator = mediator;
        _logger = logger;
        _apiResponse = apiResponse;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct()
    {
        var requestPath = HttpContext.Request.Path.Value;
        return Created(requestPath,_apiResponse.Created());
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        return Ok(_apiResponse.Success());
    }


    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        return Ok(_apiResponse.Success());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id)
    {
        return Ok(_apiResponse.Success());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        return Ok(_apiResponse.Success());
    }

    private async Task<IActionResult> HandleMediatorResult<T>(Task<Result<T>> task)
    {
        var result = await task;

        if (result.IsSuccess)
        {
            var successMessage = result.Successes.FirstOrDefault()?.Message ?? "Operation successful";
            var data = result.ValueOrDefault;
            
         //   return Ok(_apiResponse.)

        }

        return Ok();
    }
}