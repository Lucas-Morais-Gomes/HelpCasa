using Microsoft.EntityFrameworkCore;
using HelpCasa.Data;
using HelpCasa.Services;
using System.Reflection;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o CORS
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowSpecificOrigin", builder =>
  {
    builder.WithOrigins("http://localhost:3000")
             .AllowAnyHeader()
             .AllowAnyMethod();
  });
});

// Adicione os serviços ao contêiner
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
  c.IncludeXmlComments(xmlPath);
});

// Adiciona o ApplicationDbContext para usar o PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Carregar configurações de email
var emailConfig = builder.Configuration
                         .GetSection("EmailConfiguration")
                         .Get<EmailConfiguration>();

// Verifica se o emailConfig não é nulo
if (emailConfig is null)
{
  throw new InvalidOperationException("Email configuration is missing in appsettings.json");
}

builder.Services.AddSingleton(emailConfig);

builder.Services.AddScoped<IEmailSender, EmailSender>();

// Adiciona suporte para controladores
builder.Services.AddControllers();

var app = builder.Build();

// Configuração do pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    c.RoutePrefix = string.Empty;
  });
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin"); // Adiciona o CORS ao pipeline

app.UseAuthorization();

app.MapControllers();

app.Run();
