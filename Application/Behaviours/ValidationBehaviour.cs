using MediatR;
using FluentValidation;
using FluentValidation.Results;

namespace vsa_journey.Application.Behaviours;

public sealed class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

      var context = new ValidationContext<TRequest>(request);
      var validationFailures = await Task.WhenAll(_validators.Select(validator => validator.ValidateAsync(context)));

      var errors = validationFailures
          .Where(validationResult => !validationResult.IsValid)
          .SelectMany(validationResult => validationResult.Errors)
          .Select(validationFailure => new ValidationFailure(
              validationFailure.PropertyName,
              validationFailure.ErrorMessage
              ))
          .ToList();
      
      if (errors.Any())
      {
          throw new ValidationException(errors);
      }
      
      return await next();
    }
} 


