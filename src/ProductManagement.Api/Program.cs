using ProductManagement.Api.Extensions;
using ProductManagement.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddNSwagSwagger();
builder.Services.AddDefaultServices(builder.Configuration);

var app = builder.Build();

app.UseNSwagSwagger();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
