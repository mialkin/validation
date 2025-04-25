using FluentValidation;
using Serilog;
using Validation.Fluent;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.WriteTo.Console();
});

var services = builder.Services;

services.AddValidatorsFromAssemblyContaining<CreateUserRequest>();
// ValidatorOptions.Global.LanguageManager = new LanguageManager { Culture = new CultureInfo("ru") };

builder.Services.AddScoped<IValidator<CreateUserRequest>, CreateUserRequestValidator>();

var application = builder.Build();

application.MapGet("/", () => "Validation.Fluent");
application.MapCreateUserEndpoint();

application.Run();