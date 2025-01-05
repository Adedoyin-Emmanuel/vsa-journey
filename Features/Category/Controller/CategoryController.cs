using MediatR;
using FluentResults;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using vsa_journey.Application.Responses;
using vsa_journey.Features.Category.GetAllCategories.Query;



namespace vsa_journey.Features.Category.Controller;


[ApiVersion(1)]
[ApiController]
[Route("v{v:apiVersion}/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CategoryController> _logger;
    private readonly IApiResponse _apiResponse;


    public CategoryController(IMediator mediator, ILogger<CategoryController> logger, IApiResponse apiResponse)
    {
        _mediator = mediator;
        _logger = logger;
        _apiResponse = apiResponse;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllCategories(GetAllCategoriesQuery query)
    {
       return await HandleMediatorResult(_mediator.Send(query));
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