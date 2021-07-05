using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistense.Contratos
{
    public interface IPalestrantesPersist
    {
         Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false);

         Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string Nome, bool includeEventos = false);

         Task<Palestrante> GetAllPalestranteByIdAsync(int PalestranteId, bool includeEventos = false);
         
    }
}