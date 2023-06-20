using Microsoft.EntityFrameworkCore;
using TesteAdmissional.Domain.Aggregates;
using TesteAdmissional.Infra.Data.EntityConfigurations;

namespace TesteAdmissional.Infra.Data;

public class TesteAdmissionalContext : DbContext
{
    public TesteAdmissionalContext(DbContextOptions<TesteAdmissionalContext> options) : base(options)
    {
    }

    public DbSet<Contato> Contatos => Set<Contato>();


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ContatoEntityTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}