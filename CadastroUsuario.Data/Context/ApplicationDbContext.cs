using CadastroUsuario.Domain.Entities;
using Microsoft.EntityFrameworkCore;



namespace CadastroUsuario.Infra.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Pessoa> Pessoas { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        //modelBuilder.ApplyConfiguration(new ArquivoMap());
        //modelBuilder.ApplyConfiguration(new ProtocoloMap());
        //modelBuilder.ApplyConfiguration(new SetorMap());
        //modelBuilder.ApplyConfiguration(new AgendamentoMap());
        //modelBuilder.ApplyConfiguration(new AutoConstatacaoMap());
        //modelBuilder.ApplyConfiguration(new BoletimMap());
        //modelBuilder.ApplyConfiguration(new CadastroCredenciadaMap());
        //modelBuilder.ApplyConfiguration(new CadastroEmpresaMap());
        //modelBuilder.ApplyConfiguration(new CadastroPlacaMap());
        //modelBuilder.ApplyConfiguration(new CadastroVinculadasMap());

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}