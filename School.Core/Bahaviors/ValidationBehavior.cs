using FluentValidation;
using MediatR;

namespace School.Core.Bahaviors;
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
       where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // First we check if there are any validators attached to the incoming request
        if (_validators.Any())
        {
            // If available, we create a new validation context using the request, and validate it. This would return the ValidationResults array.
            // the context is the class that we make validation on it
            var context = new ValidationContext<TRequest>(request);

            // From this array of results, we filter out the failures (where the error in not null).
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(res => res.Errors)
                .Where(f => f != null)
                .ToList();


            // And finally, we throw this as FluentValidation.ValidationException and pass the errored out messages
            if (failures.Any())
            {
                var message = failures.Select(x => x.ErrorMessage).FirstOrDefault();

                throw new ValidationException(message);

            }
        }

        // If no validation failures occur, the method calls next(),
        // which allows the request to proceed to the next pipeline behavior or request handler or query handler.
        return next();
    }
}