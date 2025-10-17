using Application.Interface;
using Application.Interface.IService;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services for Dependency Injection

// Repositorio
builder.Services.AddScoped<IEventoRepository, EventoRepository>();

// Servicio
builder.Services.AddScoped<IEventoService, EventoService>();


// Configuracion del secret Json
if (builder.Environment.IsDevelopment())
{
    var postgresSettings = builder.Configuration
        .GetSection("secreto").
        Get<PostgresSettings>();

    var connectionString = $"Host={postgresSettings?.Host};" +
        $"Port={postgresSettings?.Port};" +
        $"Database={postgresSettings?.Database};" +
        $"Username={postgresSettings?.UserName};" +
        $"Password={postgresSettings?.Password}; Ssl Mode=Require;Trust Server Certificate=true;" ;

    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
    builder.Services.Configure<PostgresSettings>(builder.Configuration.GetSection("secreto"));
}
else
{
    var postgresSettings = new PostgresSettings
    {
        Host = Environment.GetEnvironmentVariable("HOST") ?? string.Empty,
        Port = Environment.GetEnvironmentVariable("PORT") ?? "5432",
        Database = Environment.GetEnvironmentVariable("DATABASE") ?? string.Empty,
        UserName = Environment.GetEnvironmentVariable("USER") ?? string.Empty,
        Password = Environment.GetEnvironmentVariable("PASSWORD") ?? string.Empty
    };

    var connectionString = $"Host={postgresSettings.Host};" +
        $"Port={postgresSettings.Port};" +
        $"Database={postgresSettings.Database};" +
        $"Username={postgresSettings.UserName};" +
        $"Password={postgresSettings.Password}; Ssl Mode=Require;Trust Server Certificate=true;";

    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
}

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
