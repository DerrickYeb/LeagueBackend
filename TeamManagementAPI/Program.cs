using System.Reflection;
using Application.Extensions;
using FluentValidation.AspNetCore;
using Infrastructure.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddApplication().AddInfrastructure(config);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(p =>
{
    p.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Team API - Microservice",
        Description = "Team Backend API for Premier League App backend using microservice architecture",
        Contact = new OpenApiContact()
        {
            Name = "Derrick Yeboah",
            Email = "derrickyeboah999@hotmail.com",
            Url = new Uri("https://derry.io")
        },
        License = new OpenApiLicense()
        {
            Name = "MIT",
            Url = new Uri("https://derry.io")
        }
    });

    //var file = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var path = Path.Combine(Directory.GetCurrentDirectory(),"TeamManagementAPI.xml");
    p.IncludeXmlComments(path);

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();