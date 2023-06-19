using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteAdmissional.Domain.Aggregates;

namespace TesteAdmissional.Infra.Data.EntityConfigurations;

public class ContatoEntityTypeConfiguration : IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.ToTable("contatos");

        builder.HasKey(e => e.Id);

        builder.Ignore(e => e.DomainEvents);

        builder.Property(e => e.Idade).IsRequired();
        builder.Property(e => e.Ativo).IsRequired();
        builder.Property(e => e.Nome).IsRequired();
        builder.Property(e => e.Sexo).IsRequired();
        builder.Property(e => e.Telefone).IsRequired();
        builder.Property(e => e.DataNascimento).IsRequired();

        builder.OwnsOne<Cargo>(e => e.Cargo, r =>
        {
            r.ToTable("cargos");

            r.HasKey(e =>
                new
                {
                    e.Ativo,
                    e.Descricao
                });

            r.Property(e => e.Ativo).IsRequired();
            r.Property(e => e.Descricao).IsRequired();
        });
    }
}