using Acuerdos.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Acuerdos.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<AcuerdoComercial> AcuerdosComerciales => Set<AcuerdoComercial>();
        public DbSet<TarifaAcuerdo> TarifasAcuerdos => Set<TarifaAcuerdo>();
        public DbSet<TarifaConsumo> TarifasConsumo => Set<TarifaConsumo>();
        public DbSet<AgenteComercial> AgentesComerciales => Set<AgenteComercial>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AcuerdoComercial>(entity =>
            {
                entity.ToTable("AcuerdosComerciales");
                entity.HasKey(e => e.IdAcuerdo);
                entity.Property(e => e.Ambito).HasMaxLength(100);
                entity.Property(e => e.CodFormaPago).HasMaxLength(10);
                entity.HasOne(a => a.AgenteComercial)
                      .WithMany() 
                      .HasForeignKey(a => a.IdAgente)
                      .IsRequired(false);
            });

            modelBuilder.Entity<TarifaAcuerdo>(entity =>
            {
                entity.ToTable("TarifasAcuerdos");
                entity.HasKey(e => e.IdTarifaAcuerdo);
                entity.HasOne(ta => ta.AcuerdoComercial)
                      .WithMany(ac => ac.Tarifas)
                      .HasForeignKey(ta => ta.IdAcuerdo);
            });

            modelBuilder.Entity<TarifaConsumo>(entity =>
            {
                entity.ToTable("TarifasConsumo");
                entity.HasKey(e => e.IdTarifa);
                entity.Property(e => e.Nombre).HasMaxLength(100);
                entity.Property(e => e.CodTarifaAcceso).HasMaxLength(3);
            });

            modelBuilder.Entity<AgenteComercial>(entity =>
            {
                entity.ToTable("AgentesComerciales");
                entity.HasKey(e => e.IdAgente);
                entity.Property(e => e.Nombre).HasMaxLength(100);
                entity.Property(e => e.Estado).HasMaxLength(50);
                entity.Property(e => e.NIF).HasMaxLength(20);
            });
        }
    }
}
