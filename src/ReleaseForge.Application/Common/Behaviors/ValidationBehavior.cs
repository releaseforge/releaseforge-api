using FluentValidation;
using MediatR;

namespace ReleaseForge.Application.Common.Behaviors;

internal sealed class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next(cancellationToken);

        var context = new ValidationContext<TRequest>(request);
        var failures = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var errors = failures
            .SelectMany(r => r.Errors)
            .Where(e => e is not null)
            .ToArray();

        if (errors.Length != 0)
            throw new Exceptions.ValidationException(errors);

        return await next(cancellationToken);
    }
}
