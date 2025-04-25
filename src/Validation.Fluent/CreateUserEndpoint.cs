using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Validation.Fluent;

public static class CreateUserEndpoint
{
    public static void MapCreateUserEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("api/users", async (
            CreateUserRequest request,
            [FromServices] IValidator<CreateUserRequest> validator) =>
        {
            var validationResult = await validator.ValidateAsync(request);

            return !validationResult.IsValid
                ? Results.ValidationProblem(validationResult.ToDictionary())
                : Results.Ok();
        });
    }
}

public record CreateUserRequest(string Name, string Email);

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
    }
}