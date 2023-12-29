using JewsJewelry.API.Extensions;
using JewsJewelry.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegistrationControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.RegistrationSwagger();
builder.Services.AddDbContextFactory<JewelryContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);
builder.Services.RegistrationSRC();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{

    app.CustomizeSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
