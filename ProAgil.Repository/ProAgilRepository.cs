using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext context;

        public ProAgilRepository(ProAgilContext context)
        {
            this.context = context;
            //Liberando o Tracker de forma geral
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; 
        }

        //Gerais
        void IProAgilRepository.Add<T>(T entity)
        {
            this.context.Add(entity); 
        }

        void IProAgilRepository.Update<T>(T entity)
        {
            this.context.Update(entity); 
        }

        void IProAgilRepository.Delete<T>(T entity)
        {
            this.context.Remove(entity); 
        }

        async Task<bool> IProAgilRepository.SaveChangesAsync()
        {
            return (await this.context.SaveChangesAsync()) > 0; 
        }


        //Eventos
        async Task<Evento[]> IProAgilRepository.GetAllEventoAsync(bool includePalestrantes)
        {
            IQueryable<Evento> query = this.context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais); 

            if(includePalestrantes)
            {
                query = query
                    .Include(pe =>pe.PalestrantesEvento)
                    .ThenInclude(p => p.Palestrante); 
            }

            query.OrderByDescending(c => c.DateTime); 
            return await query.ToArrayAsync(); 

        }

        async Task<Evento[]> IProAgilRepository.GetAllEventoAsyncByTema(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = this.context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais); 

            if(includePalestrantes)
            {
                query = query
                    .Include(pe =>pe.PalestrantesEvento)
                    .ThenInclude(p => p.Palestrante); 
            }

            query.OrderByDescending(c => c.DateTime).
                Where(c => c.Tema.ToLower().Contains(tema.ToLower())); 

            return await query.ToArrayAsync(); 
        }

        async Task<Evento> IProAgilRepository.GetEventoAsyncId(int EventoId, bool includePalestrantes)
        {
            IQueryable<Evento> query = this.context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais); 

            if(includePalestrantes)
            {
                query = query
                    .Include(pe =>pe.PalestrantesEvento)
                    .ThenInclude(p => p.Palestrante); 
            }

            query.OrderByDescending(c => c.DateTime).
                Where(c => c.Id == EventoId); 
                
            return await query.FirstOrDefaultAsync(); 
        }

        //Palestrantes
       async Task<Palestrante[]> IProAgilRepository.GetAllPalestrantesAsyncByName(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = this.context.Palestrantes
                .Include(c => c.RedesSociais); 

            if(includeEventos)
            {
                query = query
                    .Include(pe =>pe.PalestrantesEvento)
                    .ThenInclude(e => e.Evento); 
            }

            query = query.Where(p => p.Nome.ToUpper().Contains(nome.ToUpper())); 
                
            return await query.ToArrayAsync();  
        }
        async Task<Palestrante> IProAgilRepository.GetPalestranteAsync(int PalestranteId, bool includeEventos)
        {
            IQueryable<Palestrante> query = this.context.Palestrantes
                .Include(c => c.RedesSociais); 

            if(includeEventos)
            {
                query = query
                    .Include(pe =>pe.PalestrantesEvento)
                    .ThenInclude(e => e.Evento); 
            }

            query.OrderBy(c => c.Nome).
                Where(p => p.Id == PalestranteId);
                
            return await query.FirstOrDefaultAsync(); 
        }

    }
}