using Desafio.s2.Domain.Jogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.s2.Data.Mapping
{
    public class JogoMapping : IEntityTypeConfiguration<Jogo>
    {
        public void Configure(EntityTypeBuilder<Jogo> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(e => e.Nome)
               .HasColumnType("varchar(150)")
               .IsRequired();

            builder.Property(e => e.Excluido);               

            builder.Property(e => e.ThumbnailCapaJogo)
                .HasColumnType("varchar(max)");

            builder.HasOne(e => e.Categoria)
                    .WithMany(e => e.Jogos)
                    .HasForeignKey(e => e.CategoriaId)
                    .IsRequired(false);

            builder.HasOne(e => e.EmprestadoPara)
                    .WithMany(e => e.Jogos)
                    .HasForeignKey(e => e.EmprestadoParaId)
                    .IsRequired(false);

            builder.Property(e => e.IdUsuario);

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.CascadeMode);

            builder.ToTable("Jogo");
        }
    }
}