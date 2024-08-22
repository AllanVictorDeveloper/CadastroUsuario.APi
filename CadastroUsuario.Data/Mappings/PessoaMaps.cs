using CadastroUsuario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;


namespace SisConFin.Infra.Data.Mappings
{
    public class PessoaMaps : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .HasMaxLength(20)
                .HasColumnType("VARCHAR(20)")
                .IsRequired();

            builder.Property(x => x.SobreNome)
               .HasMaxLength(100)
               .HasColumnType("VARCHAR(100)")
               .IsRequired();

            builder.Property(x => x.CPF)
                .HasMaxLength(11)
                .HasColumnType("VARCHAR(11)")
                .IsRequired();

            builder.HasIndex(x => x.CPF)
                .IsUnique()
                .HasDatabaseName("IX_CPF_Unique");

            builder.Property(x => x.DataNascimento)
                .HasColumnType("DATE")
                .IsRequired();

            builder.Property(x => x.DataCadastro)
                .HasColumnType("DATE")
                .IsRequired();

            // Configuração do relacionamento com a entidade Foto
            builder.HasMany(x => x.Fotos)
                .WithOne(f => f.Pessoa)
                .HasForeignKey(f => f.PessoaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Pessoas");
        }

    }
}