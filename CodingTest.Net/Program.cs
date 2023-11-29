using AutoMapper;
using CodingTest.Net;
using CodingTest.Net.Domain.Interfaces;
using CodingTest.Net.Infra.Data.Context;
using CodingTest.Net.Infra.Data.Repository;
using CodingTest.Net.Model;
using CodingTest.Net.Service.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Code Test", Version = "v1" , Description = "Code test"});

    var filePath = Path.Combine(AppContext.BaseDirectory, "CodingTest.Net.xml");
    c.IncludeXmlComments(filePath);
});

builder.Services.AddSingleton<ArchiveJsonContext>();
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<ICustomerService, CustomerService>();
builder.Services.AddSingleton<IAtmService, AtmService>();

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseSwagger(options => options.SerializeAsV2 = true);
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Code Test v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
