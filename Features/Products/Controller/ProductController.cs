using MediatR;
using Asp.Versioning;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using vsa_journey.Application.Responses;
using vsa_journey.Features.Products.CreateProduct;
using vsa_journey.Infrastructure.Services.FileUpload;

namespace vsa_journey.Features.Products.Controller;

[ApiVersion(1)]
[ApiController]
[Route("v{v:apiVersion}/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IApiResponse _apiResponse;
    private readonly ILogger<ProductController> _logger;
    private readonly IFileUploadService _fileUploadService;


    public ProductController(IMediator mediator, ILogger<ProductController> logger, IApiResponse apiResponse, IFileUploadService fileUploadService)
    {
        _logger = logger;
        _mediator = mediator;
        _apiResponse = apiResponse;
        _fileUploadService = fileUploadService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductCommand command, IFormFileCollection files)
    {
        var filesUploadResult = await _fileUploadService.UploadFilesAsync(files);

        if (filesUploadResult.IsFailed)
        {
            var fileUploadResultErrors = filesUploadResult.Errors.Select(error => error.Message);
            return BadRequest(_apiResponse.BadRequest(fileUploadResultErrors));
        }
        
        var createProductResult = await _mediator.Send(command);
        if (createProductResult.IsSuccess)
        {
            return Created("",_apiResponse.Ok(message: createProductResult.Successes!.FirstOrDefault()!.Message,
                data: createProductResult.ValueOrDefault));
        }
        
        var errors = createProductResult.Errors.Select(error => error.Message);
        
        return BadRequest(_apiResponse.BadRequest(errors));
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        return Ok(_apiResponse.Ok());
    }


    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        return Ok(_apiResponse.Ok());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id)
    {
        return Ok(_apiResponse.Ok());
    }

    [HttpDelete]
    [Route("{id}")]
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