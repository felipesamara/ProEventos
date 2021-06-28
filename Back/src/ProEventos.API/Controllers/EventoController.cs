using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {

       public IEnumerable<Evento> _evento = new Evento[]
       {
         
            new Evento()
            {
               EventoId = 1,
               Tema = "Angular e outros",
               Local ="localização",
               QtdPessoas = 10,
               Lote = "1 lote",
               DataEvento = DateTime.Now.AddDays(2).ToString(),
               ImagemURL = "foto.png"
            },
            new Evento()
            {                
               EventoId = 2,
               Tema = "Angular e API CORE",
               Local ="PRAIA GRANDE",
               QtdPessoas = 20,
               Lote = "2 lote",
               DataEvento = DateTime.Now.AddDays(5).ToString(),
               ImagemURL = "foto2.png"
            }
      };         

        public EventoController(ILogger<EventoController> logger)
        {
          
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
           return _evento;
           
        }

        [HttpGet("{id}")]
        public IEnumerable<Evento> GetById(int id)
        {
           return _evento.Where(x => x.EventoId == id);
           
        }
       
        [HttpPost]
        public string Post()
        {
           return "EXEMPLO POST";
        }
        [HttpPut("{id}")]
        public string Put(int id)
        {
           return $"EXEMPLO DE PUT {id}";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
           return $"EXEMPLO DE DELETE com id {id}";
        }
    }
}
