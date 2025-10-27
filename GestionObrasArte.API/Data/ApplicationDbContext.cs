using GestionObrasArte.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionObrasArte.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Artista> Artistas { get; set; }
        public DbSet<TipoPintura> TiposPintura { get; set; }
        public DbSet<Pintura> Pinturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Artista>(entity =>
            {
                entity.HasKey(e => e.IdArtista);
                entity.Property(e => e.NombreArtista).IsRequired();
                entity.Property(e => e.ApellidosArtista).IsRequired();
                entity.Property(e => e.AñoNacimientoArtista).IsRequired();
            });

            modelBuilder.Entity<TipoPintura>(entity =>
            {
                entity.HasKey(e => e.IdTipoPintura);
                entity.Property(e => e.TítuloTipoPintura).IsRequired();
            });

            modelBuilder.Entity<Pintura>(entity =>
            {
                entity.HasKey(e => e.IdPintura);
                entity.Property(e => e.TituloPintura).IsRequired();
                entity.Property(e => e.Precio).HasColumnType("DECIMAL");

                entity.HasOne(d => d.Artista)
                      .WithMany()
                      .HasForeignKey(d => d.Fk_IdArtista);

                entity.HasOne(d => d.TipoPintura)
                      .WithMany()
                      .HasForeignKey(d => d.FK_IdTipoPintura);
            });
        }
    }
}