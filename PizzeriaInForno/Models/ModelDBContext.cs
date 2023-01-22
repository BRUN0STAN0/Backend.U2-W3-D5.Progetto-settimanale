using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PizzeriaInForno.Models
{
    public partial class ModelDBContext : DbContext
    {
        public ModelDBContext()
            : base("name=ModelDBContext")
        {
        }

        public virtual DbSet<DettaglioOrdine> DettaglioOrdine { get; set; }
        public virtual DbSet<Ordine> Ordine { get; set; }
        public virtual DbSet<Pietanza> Pietanza { get; set; }
        public virtual DbSet<Utente> Utente { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ordine>()
                .Property(e => e.Evaso)
                .IsFixedLength();

            modelBuilder.Entity<Ordine>()
                .Property(e => e.DataOrdine)
                .IsFixedLength();

            modelBuilder.Entity<Ordine>()
                .HasMany(e => e.DettaglioOrdine)
                .WithRequired(e => e.Ordine)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pietanza>()
                .Property(e => e.Prezzo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Pietanza>()
                .HasMany(e => e.DettaglioOrdine)
                .WithRequired(e => e.Pietanza)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utente>()
                .Property(e => e.Psw)
                .IsFixedLength();

            modelBuilder.Entity<Utente>()
                .HasMany(e => e.Ordine)
                .WithRequired(e => e.Utente)
                .WillCascadeOnDelete(false);
        }
    }
}
