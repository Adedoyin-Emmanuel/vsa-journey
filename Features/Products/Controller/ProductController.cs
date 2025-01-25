using MediatR;
using Asp.Versioning;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using vsa_journey.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using vsa_journey.Features.Authentication.Policies;
using vsa_journey.Features.Products.CreateProduct.Command;
using vsa_journey.Features.Products.GetAllProducts.Query;
using vsa_journey.Features.Products.GetProductById.Query;
using vsa_journey.Features.Products.UpdateProduct.Command;

namespace vsa_journey.Features.Products.Controller;

[ApiVersion(1)]
[ApiController]
[Route("v{v:apiVersion}/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IApiResponse _apiResponse;
    private readonly ILogger<ProductController> _logger;


    public ProductController(IMediator mediator, ILogger<ProductController> logger, IApiResponse apiResponse)
    {
        _logger = logger;
        _mediator = mediator;
        _apiResponse = apiResponse;
    }

    [Authorize(Policy=PolicyNames.SuperAdminOrAdmin)]
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductCommand command)
    {
        
        var createProductResult = await _mediator.Send(command);
        if (createProductResult.IsSuccess)
        {
            return Created("",_apiResponse.Created(message: createProductResult.Successes!.FirstOrDefault()!.Message,
                data: createProductResult.ValueOrDefault));
        }
        
        var errors = createProductResult.Errors.Select(error => error.Message);
        
        return BadRequest(_apiResponse.BadRequest(errors));
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] GetAllProductsQuery query)
    {
        return await HandleMediatorResult(_mediator.Send(query));
    }


    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var query = new GetProductByIdQuery { Id = id };

        var getProductByIdResult = await _mediator.Send(query);

        if (getProductByIdResult.IsFailed)
        {
            /*
             * Now you might be wondering why is he not using the HandleMediatorResult private method?
             * Well any failed result is a product not found
             * we need to return a 404 error instead
             */
            return NotFound(_apiResponse.NotFound(getProductByIdResult.Errors.FirstOrDefault()!.Message));
        }
        
        return Ok(_apiResponse.Ok(getProductByIdResult.ValueOrDefault));
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductCommand updateProductCommand, [FromRoute] Guid id)
    {
        var command = updateProductCommand with { Id = id };
        
        var updateProductResult = await _mediator.Send(command);

        if (updateProductResult.IsFailed)
        {
            var errors = updateProductResult.Errors.Select(error => error.Message);
            return NotFound(_apiResponse.NotFound(errors!.FirstOrDefault()));
        }
        
        return Ok(_apiResponse.Ok(updateProductResult.ValueOrDefault));
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        return Ok(_apiResponse.Ok());
    }
    
    
    
    private async Task<IActionResult> HandleMediatorResult<T>(Task<Result<T>> task)
    {
        var result = await task;

        if (result.IsSuccess)
        {
            var data = result.ValueOrDefault;
            var success = result.Successes.FirstOrDefault();
            var message = success?.Message ?? "Operation successful";

            return Ok(_apiResponse.Ok(data, message));
        }
        
        var errors = result.Errors.Select(error => error.Message);

        return BadRequest(_apiResponse.BadRequest(errors));
    }
}