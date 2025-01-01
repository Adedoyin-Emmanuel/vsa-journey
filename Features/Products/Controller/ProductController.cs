using Asp.Versioning;
using MediatR;
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
}