using APIGateway.Application.RequestValidation;
using APIGateway.Middleware;
using FluentValidation.AspNetCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin",
    builder => builder.WithOrigins("http://localhost:4200") // Angular's default port
                       .AllowAnyHeader()
                       .AllowAnyMethod());
});
// Add services to the container.

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetSearchQueryValidator>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddConsole();
var rateLimiterConfig = builder.Configuration.GetSection("RateLimiterOptions").Get<FixedWindowRateLimiterOptions>();

builder.Services.Configure<RateLimiterOptions>(options =>
{
    options.FixedWindowOptions = rateLimiterConfig;
});

builder.Services.AddHttpClient();
var configuration = builder.Configuration;
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowMyOrigin");
app.MapControllers();

app.Run();
