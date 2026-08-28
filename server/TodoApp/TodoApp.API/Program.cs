using TodoApp.API.Helpers.Extensions;
using TodoApp.API.Helpers.Middlewares;
using TodoApp.Application.Helpers.Jwt;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddCorsPolicy(configuration);

services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
services.AddApiAuthentication(configuration);

services.AddDbConnection(configuration);
services.AddRepositories();
services.AddServices();
services.AddControllers();
services.AddHelpers(configuration);

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddSwaggerConfig();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseCors(configuration["Cors:PolicyName"]!);

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<ValidationExceptionMiddleware>();
   
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
