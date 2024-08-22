using CadastroUsuario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadastroUsuario.Infra.Data.Mappings
{
    internal class FotoMaps : IEntityTypeConfiguration<Foto>
    {
        public void Configure(EntityTypeBuilder<Foto> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DataCadastro)
               .HasColumnType("DATE")
               .IsRequired();

            builder.Property(i => i.Extensao)
                .HasColumnType("VARCHAR(100)")
                .IsRequired();

            builder.Property(i => i.NomeHash)
                .HasColumnType("VARCHAR(255)")
                .IsRequired();

            builder.Property(i => i.Imagem)
                .HasColumnType("BYTEA")
                .IsRequired();

            builder.HasOne(i => i.Pessoa)
                .WithMany(p => p.Fotos) // Define a coleção de fotos na entidade Pessoa
                .HasForeignKey(i => i.PessoaId) // Define a chave estrangeira corretamente
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade); // Configura o comportamento de exclusão

            builder.ToTable("Fotos");
        }
    }
}
