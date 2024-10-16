using Microsoft.EntityFrameworkCore;
using HelpCasa.Models;

namespace HelpCasa.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; } // Para gerenciar usuários
    public DbSet<Servico> Servicos { get; set; } // Para gerenciar serviços
  }
}
