using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Escola_Sprint.Data;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connect = builder.Configuration.GetConnectionString("Conexao");

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connect, ServerVersion.AutoDetect(connect)));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
// app.MapOpenApi();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
