using JewsJewelry.API.Extensions;
using JewsJewelry.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegistrationControllers();

//builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegistrationSwagger();
builder.Services.AddDbContextFactory<JewelryContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);
//builder.Services.AddSwaggerGen();
builder.Services.RegistrationSRC();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
    app.CustomizeSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
