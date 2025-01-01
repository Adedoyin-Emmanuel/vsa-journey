using MediatR;
using Asp.Versioning;
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
    private readonly HttpContext _httpContext;


    public ProductController(IMediator mediator, ILogger<ProductController> logger, IApiResponse apiResponse, HttpContext httpContext)
    {
        _mediator = mediator;
        _logger = logger;
        _apiResponse = apiResponse;
        _httpContext = httpContext;
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateProduct()
    {
        var requestPath = _httpContext.Request.Path.Value;
        return Created(requestPath,_apiResponse.Created());
    }

    [HttpGet]
    [Route("")]
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
}