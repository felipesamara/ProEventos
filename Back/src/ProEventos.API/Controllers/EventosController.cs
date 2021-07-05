using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Persistense.Contextos;
using ProEventos.Domain;
using ProEventos.Application.Contratos;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventosService _eventosService;

        public EventosController(IEventosService eventosService)
        {
            this._eventosService = eventosService;
            
        }

        [HttpGet]
        public async Task<IActionResult>  Get()
        {
            try
            {
                var eventos = await _eventosService.GetAllEventosAsync(true);
                if(eventos == null) return NotFound("NENHUM EVENTO ENCONTRADO.");
                return Ok(eventos);
            }   
            catch (Exception ex)
            {               
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventosService.GetEventoByIdAsync(id,true);
                if(evento == null) return NotFound("NENHUM EVENTO ENCONTRADO POR ID.");
                return Ok(evento);
            }   
            catch (Exception ex)
            {     
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }

        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var evento = await _eventosService.GetAllEventosByTemaAsync(tema,true);
                if(evento == null) return NotFound("EVENTOS POR TEMA NÃO ENCONTRADOS.");
                return Ok(evento);
            }   
            catch (Exception ex)
            {     
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                var evento = await _eventosService.AddEventos(model);
                if(evento == null) return BadRequest("ERRO AO TENTAR ADICIONAR EVENTO");
                return Ok(evento);
            }   
            catch (Exception ex)
            {     
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar eventos. Erro: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                var evento = await _eventosService.UpdateEvento(id, model);
                if(evento == null) return BadRequest($"ERRO AO TENTAR ALTERAR EVENTO");
                return Ok(evento);
            }   
            catch (Exception ex)
            {     
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar eventos. Erro: {ex.Source}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if(await _eventosService.DeleteEvento(id))
                    return Ok("DELETADO");
                else
                    return BadRequest("EVENTO NÃO DELETADO");
            }   
            catch (Exception ex)
            {     
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar eventos. Erro: {ex.Message}");
            }
        }
    }
}
