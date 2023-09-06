using FluentValidation;
using MediatR;

namespace MyOwnBank.Pipeline;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is not null)
        {
            var result = await _validator.ValidateAsync(request, cancellationToken);
            if (!result.IsValid)
            {
                throw new Exception();
            }

            await _validator.ValidateAndThrowAsync(request, cancellationToken);
        }

        return await next();
    }
}
