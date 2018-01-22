using Desafio.s2.Domain.Amigos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.s2.Data.Mapping
{
    public class AmigoMapping : IEntityTypeConfiguration<Amigo>
    {
        public void Configure(EntityTypeBuilder<Amigo> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(e => e.Nome)
               .HasColumnType("varchar(150)")
               .IsRequired();

            builder.Property(e => e.Email)
              .HasColumnType("varchar(150)")
              .IsRequired();

            builder.Property(e => e.Excluido);

            builder.Property(e => e.IdUsuario);

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.CascadeMode);

            builder
                .HasMany(a => a.Jogos)
                .WithOne(a => a.EmprestadoPara)
                .HasForeignKey(a => a.EmprestadoParaId)
                .IsRequired(false);

            builder.ToTable("Amigo");
        }
    }
}
