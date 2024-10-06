using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Resources;

namespace School.Core.Bahaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, IStringLocalizer<SharedResources> localizer)
        {
            _validators = validators;
            _localizer = localizer;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // First we check if there are any validators attached to the incoming request
            if (_validators.Any())
            {
                // If available, we create a new validation context using the request, and validate it asynchronously.
                var context = new ValidationContext<TRequest>(request);
                var failures = new List<ValidationFailure>();

                // Validate each validator asynchronously
                foreach (var validator in _validators)
                {
                    var result = await validator.ValidateAsync(context, cancellationToken);
                    failures.AddRange(result.Errors);
                }

                // Filter out any failures
                failures = failures.Where(f => f != null).ToList();

                // If there are any validation failures, throw a ValidationException
                if (failures.Any())
                {
                    var message = failures.Select(x => $"{_localizer[x.PropertyName]}: {x.ErrorMessage}").FirstOrDefault();
                    throw new ValidationException(message);
                }
            }

            // If no validation failures occur, call next() to proceed with the request
            return await next();
        }
    }
}
