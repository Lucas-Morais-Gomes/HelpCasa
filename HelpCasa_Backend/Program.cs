using Microsoft.EntityFrameworkCore;
using HelpCasa.Data;

var builder = WebApplication.CreateBuilder(args);

// Adicione os serviços ao contêiner
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona o ApplicationDbContext para usar o PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona suporte para controladores
builder.Services.AddControllers();

var app = builder.Build();

// Configuração do pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); // Certifique-se de mapear os controladores da API

app.Run();
