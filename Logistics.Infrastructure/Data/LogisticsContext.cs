using System;
using Logistics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Logistics.Infrastructure.Data
{
    public partial class LogisticsContext : DbContext
    {
        public LogisticsContext()
        {
        }

        public LogisticsContext(DbContextOptions<LogisticsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ocorrencia> Ocorrencia { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Ocorrencia>(entity =>
            {
                entity.Property(e => e.HoraOcorrencia).HasColumnType("datetime");

                entity.Property(e => e.TipoOcorrencia)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.Ocorrencia)
                    .HasForeignKey(d => d.IdPedido)
                    .HasConstraintName("fk_PedOcorrencia");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable("Pedido");

                entity.Property(e => e.HoraPedido).HasColumnType("datetime");

                entity.Property(e => e.IndConcluido).HasColumnName("indConcluido");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
