using Microsoft.EntityFrameworkCore;
using HelpCasa.Models;

namespace HelpCasa.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } // Para gerenciar usuários
    public DbSet<Service> Services { get; set; } // Para gerenciar serviços
  }
}
