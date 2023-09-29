using System.Diagnostics;
using System.Text.Json.Serialization;
using AirTravelService.Api;
using AirTravelService.DataAccess.PostgresSql;
using AirTravelService.ReadModel.PostgresSql;
using AirTravelService.Service;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Serilog;

Activity.DefaultIdFormat = ActivityIdFormat.W3C;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var connectionString = builder.Configuration.GetConnectionString("main")!;
builder.Services.AddPostgresSqlRepositories(connectionString);
builder.Services.AddPostgresSqlReadModel(connectionString);
builder.Services.AddTravelServices();

builder.Services.AddProblemDetails(options =>
{
    options.ValidationProblemStatusCode = 400;
    options.IncludeExceptionDetails = (_, _) => builder.Environment.IsDevelopment();
    options.MapFluentValidationException();
    options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
    options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);
    options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();