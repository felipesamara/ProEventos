using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistense.Contratos
{
    public interface IEventosPersist
    {
         Task<Evento[]> GetAllEventosByTemaAsync(string Tema, bool includePalestrantes = false);

         Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false);

         Task<Evento> GetEventoByIdAsync(int EventoId, bool includePalestrantes = false);       
         
    }
}