using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Examen_Abpos.Models.DB
{
    public partial class abposus_dbContext : DbContext
    {
        public abposus_dbContext()
        {
        }

        public abposus_dbContext(DbContextOptions<abposus_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actividad> Actividad { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=abposus_db;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actividad>(entity =>
            {
                entity.HasKey(e => e.IdActividad)
                    .HasName("PK__Activida__DCD34883FB069727");

                entity.Property(e => e.IdActividad).HasColumnName("id_actividad");

                entity.Property(e => e.DescripLlamada)
                    .IsRequired()
                    .HasColumnName("descrip_llamada")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionLlamada)
                    .HasColumnName("duracion_llamada")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnName("fecha_registro")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.MotivoLlamada)
                    .IsRequired()
                    .HasColumnName("motivo_llamada")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Resuelto).HasColumnName("resuelto");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Actividad)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Actividad__id_us__44FF419A");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__4E3E04ADE787212B");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rol).HasColumnName("rol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
