
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilContext: DbContext
    {
        public ProAgilContext(DbContextOptions<ProAgilContext> options) : base(options){}
        public DbSet<Evento> Eventos {get; set;}
        public DbSet<Lote> Lotes {get; set;}
        public DbSet<Palestrante> Palestrantes {get; set;}
        public DbSet<PalestranteEvento> PalestrantesEvento {get; set;}
        public DbSet<RedeSocial> RedesSociais {get; set;}

        /*Cria um modelo de chaves para PalestranteEvento*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PalestranteEvento>()
            .HasKey(PE => new {PE.EventoId,PE.PalestranteId});

        }
    }
}