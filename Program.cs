using System;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrganizadorTarefa.Data;
using OrganizadorTarefa.Endpoints;
using OrganizadorTarefa.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddTransient<ITarefaService, TarefaService>();

var connectionString = $"Data Source={Path.Combine(AppContext.BaseDirectory, "tarefas.db")}";
Console.WriteLine($"Banco de dados em uso: {connectionString}");

builder.Services.AddDbContext<TarefaContext>(options =>
    options.UseSqlite(connectionString));


// === SWAGGER ===
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API de Tarefas", Version = "v1" });
});

// Enum como string
builder.Services.Configure<JsonOptions>(o =>
    o.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<TarefaContext>();
db.Database.Migrate();

// Mapear endpoints de Tarefa
app.MapTarefaEndpoints();

app.Run();