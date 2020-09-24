using System.Collections.Generic;
using apiPacientes.Models;
using apiPacientes.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace apiPacientes.Controllers
{
    [Route("api/[Controller]")]
    public class PacientesController : Controller
    {
        // Injeção de Dependencia
        private readonly IPacienteRepository pacienteRepository;
        public PacientesController(IPacienteRepository pacienteRepository)
        {
            this.pacienteRepository = pacienteRepository;
        }
        /*
        [HttpGet]
        public string GetAll()
        {
            return "Olá";
        }

        [HttpGet("{id}", Name="GetPaciente")]
        public string GetPacienteById(long id)
        {
            return "Paciente: " + id;
        }
        */
        [HttpGet] 
        public IEnumerable<Paciente> GetAll() // Retorna um Json de Paciente
        {
            return pacienteRepository.GetAll();
        }

        [HttpGet("{id}", Name="GetPaciente")]
        public IActionResult GetById(long id)
        {
            var paciente = pacienteRepository.Find(id);
            if(paciente == null)
                return NotFound(); // Status code 404;
            return new ObjectResult(paciente);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Paciente paciente)
        {
            if(paciente == null)
                return BadRequest(); // Status Code 400
            pacienteRepository.Add(paciente);
            return CreatedAtRoute("GetPaciente", new{id=paciente.id}, paciente);
        }

        [HttpPut]
        public IActionResult Update(long id, [FromBody] Paciente paciente)
        {
            var pacienteUp = pacienteRepository.Find(id);
            if(pacienteUp == null)
                return NotFound();
            if(paciente == null || paciente.id != id)
                return BadRequest();
            
            // Regra de Negocio
            // Só vou atualizar 2 campos: Comorbidade e Grau
            pacienteUp.comorbidade = paciente.comorbidade;
            pacienteUp.grau = paciente.grau;
            pacienteRepository.Update(pacienteUp);
            return new NoContentResult(); // Status code 204 o servidor processou a requisição
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var paciente = pacienteRepository.Find(id);
            if(paciente == null)
                return NotFound();
            pacienteRepository.Remove(id);
            // Status code 204 o servidor processou a requisição
            return new NoContentResult();
        }
    }
}