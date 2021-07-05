using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistense.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventosService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventosPersist _eventoPersist;

        public EventoService(IGeralPersist geralPersist, IEventosPersist eventoPersist)
        {
            this._eventoPersist = eventoPersist;
            this._geralPersist = geralPersist;

        }
        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                _geralPersist.Add<Evento>(model);
                if(await _geralPersist.SaveChangesAsync())
                {
                   return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
                }
                 return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception($"ERRO AO SALVAR {ex.Message}");
            }   
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
               
                if(evento == null) return null;
                
                _geralPersist.Update<Evento>(model);               
                model.Id = evento.Id;
                if(await _geralPersist.SaveChangesAsync())
                {                
                   return await _eventoPersist.GetEventoByIdAsync(model.Id, false);                
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
                if(evento == null) throw new Exception("EVENTO NÃO FOI ECONTRADO PARA DELETAR");
                _geralPersist.Delete<Evento>(evento);
                
                return await _geralPersist.SaveChangesAsync();
                
            }
            catch (System.Exception)
            {
                throw new Exception("ERRO AO ALTERAR");
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int EventoId, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetEventoByIdAsync(EventoId, includePalestrantes);
                if(eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception($"ERRO AO CONSULTAR POR id {ex.Message}");
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrantes);
                if(eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                
                throw new Exception($"ERRO AO CONSULTAR TODOS EVENTOS {ex.Message}");
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string Tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosByTemaAsync(Tema, includePalestrantes);
                if(eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception($"ERRO AO CONSULTAR POR TEMA {ex.Message}");
            }
        }

        
    }
}