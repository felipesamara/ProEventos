using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistense.Contextos;
using ProEventos.Persistense.Contratos;

namespace ProEventos.Persistense
{
    public class PalestrantePersist : IPalestrantesPersist
    {

        private readonly ProEventosContext _context;

        public PalestrantePersist(ProEventosContext context)
        {
            this._context = context;

        }
         public async Task<Palestrante> GetAllPalestranteByIdAsync(int PalestranteId, bool includeEventos = false)
        {
           IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedesSociais);
           if(includeEventos)          
               query = query.Include(p => p.PalestrantesEvento).ThenInclude(pe => pe.Evento);
           query = query.AsNoTracking().OrderBy(p => p.Id).Where(p => p.Id == PalestranteId);

           return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
          IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedesSociais);
           if(includeEventos)          
               query = query.AsNoTracking().Include(p => p.PalestrantesEvento).ThenInclude(pe => pe.Evento);
           query = query.OrderBy(p => p.Id);

           return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string Nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedesSociais);
           if(includeEventos)          
               query = query.Include(p => p.PalestrantesEvento).ThenInclude(pe => pe.Evento);
           query = query.AsNoTracking().OrderBy(p => p.Id).Where(p => p.Nome.ToLower().Contains(Nome.ToLower()));

           return await query.ToArrayAsync();
        }
    }
}